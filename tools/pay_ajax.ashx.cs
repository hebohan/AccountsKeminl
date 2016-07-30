using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using work.MD5Hash;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL.Common;
using System.Configuration;
using System.Net.Mail;

namespace Accounts.Web.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class pay_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            if(!string.IsNullOrEmpty(context.Request.QueryString["type"]))
            {
                action = context.Request.QueryString["type"];
            }
            switch (action)
            {
                case "keminl_pay_validate" :    //验证支付
                    keminl_pay_post(context);
                    break;
                case "Login":           //验证登录
                    Login(context);
                    break;
                case "selectmoney":     //检查余额
                    CheckMoney(context);
                    break;
                case "payml":           //发送支付密令
                    Sendml(context);
                    break;
                case "checkusername":   //检查转账收款人
                    CheckUserName(context); 
                    break;
                case "suborder":        //添加转账订单
                    Order_Add(context); 
                    break;
                case "checkpay":        //检查支付状态
                    CheckPay(context);
                    break;
                case "getpayurl":       //获取支付订单的支付链接
                    GetPayUrl(context);
                    break;
            }
        }

        #region 验证支付====================================
        private void keminl_pay_post(HttpContext context)
        {
            string orderid = DTRequest.GetFormString("orderid");
            string username = DTRequest.GetFormString("username");
            string password = MD5up.MD5(DTRequest.GetFormString("paypass"));
            string price = DTRequest.GetFormString("price");
            string bussinesscode = DTRequest.GetFormString("bussinesscode");
            string bussinessid = GetBussinessid(bussinesscode);
            string payid = DTRequest.GetFormString("payid");
            string mode = DTRequest.GetFormString("mode");
            //调用函数验证密码
            DataTable dt = GetUserInfo(username, password).Tables[0];
            if(dt.Rows.Count > 0)
            {
                //获取订单金额
                if (price.Trim() != "")
                {
                    string userid = dt.Rows[0]["UserId"].ToString();
                    if (Convert.ToDecimal(dt.Rows[0]["cash"].ToString()) - Convert.ToDecimal(price) >= 0)
                    {
                        //调用扣款
                        string paydetail = "支付流水号为<font color='red'>" + payid + "</font>的订单扣款";
                        if(mode=="TA")
                        {
                            paydetail = "向<font color='gray'>" + GetInUser(bussinessid) + "</font>转账";
                        }
                        if (UpdateTotal("total_money", price, "-", userid) && UpdateCash("cash", price, "-", userid, paydetail, payid))
                        {
                            try
                            {
                                //商户收入金额
                                UpdateTotal("total_money", price, "+", bussinessid);
                                paydetail = "支付流水号为<font color='red'>" + payid + "</font>的订单收款";
                                if(mode == "TA")
                                {
                                    paydetail = "收到来自<font color='gray'>" + GetInUser(userid)+ "</font>的一笔转账";
                                }
                                UpdateCash("cash", price, "+", bussinessid, paydetail, payid);
                                string return_url = string.Empty;
                                if(mode == "DEAL")
                                {
                                    //访问指定url写入CheckValue
                                    string CheckValue = MD5up.MD5("2" + orderid + price);
                                    string notify_url = Get_notify_url(bussinessid) + "?CheckValue=" + CheckValue;
                                    return_url = Get_return_url(bussinessid);

                                    WebClient _client = new WebClient();
                                    string reply = _client.DownloadString(notify_url);
                                }
                                //设置支付状态
                                SetPayStatus(orderid, username);
                                //若是本站转账订单，则直接设置订单状态
                                if (mode == "TA")
                                {
                                    UpdateOrder(orderid, "IsPay");
                                }
                                context.Response.Write("{ \"status\":\"true\" ,\"msg\":\"支付成功\",\"url\":\"" + return_url+ " \"}");
                                return;
                            }
                            catch
                            {
                                //若支付失败则将扣除的金额加上
                                UpdateTotal("total_money", price, "+", bussinessid);
                                UpdateTotal("total_money", price, "+", userid);
                                UpdateCash("cash", price, "+", userid, "支付流水号为<font color='red'>" + payid + "</font>的订单退款", payid);
                                UpdateCash("cash", price, "-", bussinessid, "支付流水号为<font color='red'>" + payid + "</font>的订单退款", payid);
                                
                                context.Response.Write("{ \"status\":\"false\" ,\"msg\":\"支付错误!错误码：3388\",\"url\":\"\"}");
                                return;
                            }
                        }
                        
                    }
                    else
                    {
                        context.Response.Write("{ \"status\":\"false\" ,\"msg\":\"扣款失败,请检查您的账户余额\",\"url\":\"\"}");
                        return;
                    }
                }
                else
                {
                    context.Response.Write("{ \"status\":\"false\" ,\"msg\":\"支付金额错误!错误码：3399\",\"url\":\"\"}");
                    return;
                }
            }
            else
            {
                context.Response.Write("{ \"status\":\"false\" ,\"msg\":\"支付密码错误！\",\"url\":\"\"}");
                return;
            }
        }
        #endregion

        #region 验证登录====================================
        private void Login(HttpContext context)
        {
            string username = DTRequest.GetFormString("username");
            string loginpass = MD5up.MD5(DTRequest.GetFormString("loginpass"));
            DataTable dt = GetUserInfo(username, loginpass).Tables[0];
            if(dt.Rows.Count > 0)
            {
                //context.Response.Write("{ 'status':'true', 'username':'"+ username + "','cash':'"+  + "','msg':'登录成功'}");
                context.Response.Write("{\"status\": \"true\",\"username\":\"" + username + "\",\"cash\":\"" + dt.Rows[0]["cash"].ToString() + "\",\"msg\":\"登录成功\",\"pass\":\"" + loginpass + "\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"status\": \"false\",\"username\":\"\",\"cash\":\"\",\"msg\":\"用户名或密码不正确\",\"pass\":\"" + loginpass + "\"}");
                return;
            }

        }
        #endregion

        #region 检查余额====================================
        private void CheckMoney(HttpContext context)
        {
            string username = DTRequest.GetFormString("username");
            string loginpass = DTRequest.GetFormString("loginpass");
            DataTable dt = GetUserInfo(username, loginpass).Tables[0];
            if (dt.Rows.Count > 0)
            {
                //context.Response.Write("{ 'status':'true', 'username':'"+ username + "','cash':'"+  + "','msg':'登录成功'}");
                context.Response.Write("{\"cash\":\"" + dt.Rows[0]["cash"].ToString() + "\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"cash\":\"\"}");
                return;
            }

        }
        #endregion

        #region 发送支付密令====================================
        private void Sendml(HttpContext context)
        {
            string userid = context.Request.QueryString["userid"];
            string username = context.Request.QueryString["username"];
            try
            {
                if (CheckMailCount(userid))
                {
                    string ml = GetRandomString(6, 1, "");
                    string mlMd5 = MD5up.MD5(username.ToLower() + ml);
                    string title = "账单管理系统-支付密令";
                    string content = string.Format(@"<div >尊敬的<font color='blue'>{0}</font></div>
                                <div style='padding-top:20px'>&nbsp;&nbsp;&nbsp;&nbsp;您申请的支付密令为<font color='red'>{1}</font>，请不要泄漏给他人，同一天仅有一次发送密令的机会。</div><br><div style='padding-top:20px;'>账单管理系统&nbsp;&nbsp;{2}</div>", username, ml, string.Format("{0:yyyy年MM月dd日}", DateTime.Now));
                    SendMail(GetMail(userid), title, content);

                    IUpdateDataSourceFace update = new UpdateSQL("Com_UserInfos");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("paykey", ml);
                    update.AddFieldValue("paykeyMd5", mlMd5.ToUpper());
                    update.AddFieldValue("main_send_count", "1");
                    update.AddWhere("Userid", userid);
                    update.ExecuteNonQuery();
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("full");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("false");
            }

        }
        #endregion

        #region 支付检查被付款对象====================================
        private void CheckUserName(HttpContext context)
        {
            string keyword = DTRequest.GetFormString("loginname");
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select Userid,UserRealName,Mobile from View_Users where LoginName='{0}' or Mobile='{0}' or Email='{0}'", keyword);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if(dt.Rows.Count>0)
                {
                    string realname = dt.Rows[0]["UserRealName"].ToString();
                    string mobile = dt.Rows[0]["Mobile"].ToString();
                    string value = "*" + (realname.Length <= 3 ? realname.Substring(1, realname.Length - 1) : realname.Substring(2, realname.Length - 2)) + "(" + mobile.Substring(0, 3) + "****" + mobile.Substring(mobile.Length - 4, 4) + ")$" + dt.Rows[0]["Userid"].ToString();
                    context.Response.Write(value);
                }
                else
                {
                    context.Response.Write("none");
                }
            }
            catch
            {
                context.Response.Write("false");
            }

        }
        #endregion

        #region 添加转账订单====================================
        private void Order_Add(HttpContext context)
        {
            string OrderId = GetRandomString(2, 1, "").ToUpper() + DateTime.Now.ToString("yyyyMMddHHmmssff");
            string inuserid = DTRequest.GetFormString("inuserid");
            string outuserid = DTRequest.GetFormString("outuserid");
            string money = DTRequest.GetFormString("money");
            money = money.Substring(0,money.Length-3);
            string remark = DTRequest.GetFormString("remark");

            try
            {
                IInsertDataSourceFace insert = new InsertSQL("OrderList");
                insert.DataBaseAlias="common";
                insert.AddFieldValue("Orderid",OrderId);
                insert.AddFieldValue("PayeeId",inuserid);
                insert.AddFieldValue("Price", money);
                insert.AddFieldValue("PayerId",outuserid);
                insert.AddFieldValue("CreateTime",DateTime.Now);
                insert.AddFieldValue("Status", "wait_pay");
                insert.AddFieldValue("Type","TA");
                insert.AddFieldValue("Remark",remark);
                if(insert.ExecuteNonQuery() > 0)
                {
                    context.Response.Write(OrderId);
                }
                else
                {
                    context.Response.Write("false");
                }
            }
            catch
            {
                context.Response.Write("false");
            }

        }
        #endregion

        #region 检查支付状态====================================
        private void CheckPay(HttpContext context)
        {
            string orderid = DTRequest.GetFormString("orderid");
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select id from Order_Record where orderid='{0}' and IsPay=1 ", orderid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                context.Response.Write("{\"status\":\"true\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"false\"}");
                return;
            }
        }
        #endregion

        #region 获取支付链接====================================
        private void GetPayUrl(HttpContext context)
        {
            string msg = string.Empty;
            string id = DTRequest.GetFormString("id");
            string action = DTRequest.GetFormString("action");
            if(action=="pay")
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select Orderid,Price,PayeeId,Type from OrderList where id='{0}' and Status='wait_pay'", id);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable InUserInfo = GetInUserInfo(dt.Rows[0]["PayeeId"].ToString());
                    string payuser = InUserInfo.Rows[0]["LoginName"].ToString();    //收款人用户名
                    string payml = InUserInfo.Rows[0]["paykey"].ToString();         //收款人密令
                    string paykeyMd5 = MD5up.MD5(payuser.ToLower() + payml).ToUpper(); //收款人密令md5
                    string mode = dt.Rows[0]["Type"].ToString(); //模式
                    string price = dt.Rows[0]["Price"].ToString();
                    string orderid = dt.Rows[0]["Orderid"].ToString();
                    string CheckKey = MD5up.MD5(orderid + price + mode + "keminl.cn").ToUpper(); //校验支付码
                    string PayUrlHead = ConfigurationManager.AppSettings["PayUrlHead"];
                    msg = string.Format(@"{5}/pay/keminlpay.aspx?orderid={0}&total={1}&CheckKey={2}&BussinessCode={3}&mode={4}", orderid, price, CheckKey, paykeyMd5, mode, PayUrlHead);
                }
                else
                {
                    msg = "false";
                }
            }
            else if(action == "cancle")
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select Status from OrderList where id='{0}'", id);
                if (select.ExecuteDataSet().Tables[0].Rows[0]["Status"].ToString() == "wait_pay")
                {
                    IUpdateDataSourceFace update = new UpdateSQL("OrderList");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("Status", "cancle");
                    update.AddFieldValue("FinishTime", DateTime.Now);
                    update.AddWhere("id", id);
                    update.ExecuteNonQuery();
                    msg = "c_true";
                }
                else
                {
                    msg = "false";
                }
                    

                
            }
            context.Response.Write(msg);
        }
        #endregion
        //获取用户信息
        public DataSet GetUserInfo(string username, string loginpass)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select a.LoginName,
                                                        b.cash,
                                                        a.UserId 
                                                        from Com_UserLogin a 
                                                        LEFT JOIN Com_UserInfos b 
                                                        ON a.UserId=b.Userid 
                                                        where a.LoginName='{0}' and a.LoginPassword='{1}'", username, loginpass);
            return select.ExecuteDataSet();
        }
        //支付扣除金额
        public bool UpdateCash(string Key, string addmoney, string sign, string userid, string PayRemark, string PayId)
        {
            try
            {

                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", Key, sign, addmoney, userid);
                select.ExecuteDataSet();

                select.CommandText = string.Format(@"select cash,total_money from Com_UserInfos where Userid='{0}'",userid);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                SetHisMoney(userid, dt.Rows[0]["cash"].ToString(), dt.Rows[0]["total_money"].ToString(), addmoney, "现金", PayRemark, PayId,sign);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool UpdateTotal(string Key, string addmoney, string sign, string userid)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", Key, sign, addmoney, userid);
                select.ExecuteDataSet();
                return true;
            }
            catch
            {
                return false;
            }

        }

        //获取商户返回链接
        public string Get_notify_url(string userid)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select notify_url from Com_UserInfos where Userid='{0}'", userid);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if(dt.Rows.Count>0)
                {
                    return dt.Rows[0][0].ToString();
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public string Get_return_url(string userid)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select return_url from Com_UserInfos where Userid='{0}'", userid);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public void SetPayStatus(string orderid, string username)
        {
            IUpdateDataSourceFace update = new UpdateSQL("Order_Record");
            update.DataBaseAlias = "common";
            update.AddFieldValue("IsPay","1");
            update.AddFieldValue("payer", username);
            update.AddWhere("orderid",orderid);
            update.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">随机字符串中英文字符个数</param>
        /// <param name="count">需要获取的个数</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        static string GetRandomString(int length, int count, string separator)
        {
            char[] Chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'R', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            string yymmdd = DateTime.Now.ToString("yyMMdd");
            for (int i = 0; i < count; i++)
            {

                for (int j = 0; j < length; j++)
                {
                    sb.Append(Chars[rd.Next(0, Chars.Length)]);
                }
                if (i < count - 1)
                {
                    sb.Append(separator);
                }
            }
            return sb.ToString();
        }

        public bool CheckMailCount(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select main_send_count from Com_UserInfos where main_send_count<> '1' and Userid='{0}'", userid);
            if (select.ExecuteDataSet().Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }


        public bool SendMail(string receiver, string title, string content)
        {
            try
            {
                string sender = ConfigurationManager.AppSettings["MailUserName"];
                string password = ConfigurationManager.AppSettings["MailPassword"];
                //声明一个Mail对象
                MailMessage mymail = new MailMessage();
                //发件人地址
                //如是自己，在此输入自己的邮箱
                mymail.From = new MailAddress(sender);
                //收件人地址
                mymail.To.Add(new MailAddress(receiver));
                //邮件主题
                mymail.Subject = title;
                //邮件标题编码
                mymail.SubjectEncoding = System.Text.Encoding.UTF8;
                //发送邮件的内容
                mymail.Body = content;
                //邮件内容编码
                mymail.BodyEncoding = System.Text.Encoding.UTF8;
                //添加附件
                //Attachment myfiles = new Attachment(tb_Attachment.PostedFile.FileName);
                //mymail.Attachments.Add(myfiles);
                //抄送到其他邮箱
                //mymail.CC.Add(new MailAddress(tb_cc.Text));
                //是否是HTML邮件
                mymail.IsBodyHtml = true;
                //邮件优先级
                mymail.Priority = MailPriority.High;
                //创建一个邮件服务器类
                SmtpClient myclient = new SmtpClient();
                myclient.Host = "smtp.qq.com";
                //SMTP服务端口
                myclient.Port = 25;
                //使用SSL访问特定的SMTP邮件服务器
                myclient.EnableSsl = true;
                //验证登录
                myclient.Credentials = new NetworkCredential(sender, password);//"@"输入有效的邮件名, "*"输入有效的密码
                myclient.Send(mymail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetMail(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select email FROM Com_UserInfos where Userid={0}", userid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["email"].ToString();
            }
            return null;
        }

        public string GetBussinessid(string BussinessCode)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select Userid from Com_UserInfos where paykeyMd5='{0}'", BussinessCode);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Userid"].ToString();
            }
            return null;
        }

        private void SetHisMoney(string userid, string _cash, string _total, string D_value, string type, string PayRemark, string PayId,string Sign)
        {
            IInsertDataSourceFace insert = new InsertSQL("History_Money");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("Creator", userid);
            insert.AddFieldValue("CreateTime", DateTime.Now);
            insert.AddFieldValue("Cash", _cash);
            insert.AddFieldValue("TotalMoney", _total);
            insert.AddFieldValue("D_value", Sign == "-" ? "-" + D_value : D_value);
            insert.AddFieldValue("type", type);
            insert.AddFieldValue("PayRemark",PayRemark);
            insert.AddFieldValue("PayId",PayId);
            insert.ExecuteNonQuery();
        }

        private string GetInUser(string userid)
        {
            string value = string.Empty;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select UserRealName,Mobile from View_Users where UserId='{0}'", userid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                string realname = dt.Rows[0]["UserRealName"].ToString();
                string mobile = dt.Rows[0]["Mobile"].ToString();
                value = "*" + (realname.Length <= 3 ? realname.Substring(1, realname.Length - 1) : realname.Substring(2, realname.Length - 2)) + "(" + mobile.Substring(0, 3) + "****" + mobile.Substring(mobile.Length - 4, 4) + ")";
            }
            return value;
        }

        private void UpdateOrder(string orderid,string status)
        {
            try{
                if (status == "IsPay")
                {
                    IUpdateDataSourceFace update = new UpdateSQL("OrderList");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("Status", "already_pay");
                    update.AddFieldValue("FinishTime", DateTime.Now);
                    update.AddWhere("Orderid",orderid);
                    update.ExecuteNonQuery();
                }
                
            }
            catch{

            }
            
        }

        public DataTable GetInUserInfo(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select a.paykey,b.LoginName from Com_UserInfos a left join Com_UserLogin b on a.Userid = b.UserId where a.UserId='{0}'", userid);
            return select.ExecuteDataSet().Tables[0];
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}