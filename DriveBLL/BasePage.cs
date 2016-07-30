using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using Inspur.Finix.DAL.SQL;
using System.Net.Mail;
using System.Configuration;
using System.Net;


namespace Accounts.BLL
{

    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            this.Load += new EventHandler(Base_Load);
        }

        protected void Base_Load(object sender, EventArgs e)
        {
            if (GetLoginUser() != null)
            {
            }
            else
            {
                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                string[] browsertype = { "iphone", "ipod", "ipad", "android", "mobile", "blackberry", "webos", "incognito", "webmate", "bada", "nokia", "lg", "ucweb", "skyfire" };
                List<string> list = new List<string>(browsertype);
                if (list.Contains(browser.Browser.ToLower()))
                {
                    //alert('登录已失效或没有登录，请登录！');
                    Response.Write("<script language=\"javascript\">window.parent.location.href='/mobile/Login.aspx';</script>");
                }
                else
                {
                    Response.Write("<script language=\"javascript\">window.parent.location.href='/Login.aspx';</script>");
                }
            }
        }

        public Model.View_Users GetLoginUser()
        {
            Model.View_Users user = Session["User"] as Model.View_Users;
            return user;
        }

        public bool CheckAuthority(string pagename, string authorityCode)
        {
            Model.View_Users user = GetLoginUser();
            if (user != null)
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format("select Id from Tb_Navigation where LinkAddress like '%/{0}%'",
                    pagename);
                object navId = select.ExecuteScalar();
                if (navId != null)
                {
                    select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.CommandText = string.Format("select RolesId from Tb_RolesAddUser where UserId='{0}'",
                        user.Userid);
                    DataTable roledt = select.ExecuteDataSet().Tables[0];
                    if (roledt.Rows.Count > 0)
                    {
                        string role = string.Empty;
                        foreach (DataRow dr in roledt.Rows)
                        {
                            role += dr["RolesId"].ToString() + ",";
                        }
                        role = role.TrimEnd(',');
                        select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText =
                            string.Format(
                                "select Count(1) from Tb_RolesAndNavigation a join Com_ButtonGroup b on a.ButtonId=b.Id where a.RolesId in ({0}) and a.NavigationId={1} and b.BtnCode='{2}'",
                                role, navId, authorityCode);
                        object obj = select.ExecuteScalar();
                        if (obj != null && Convert.ToInt32(obj) > 0)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }
        /// <summary>
        /// 根据人员id获取部门名称
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetDept(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select Agency from Com_Organization where id in (select OrgId from Com_OrgAddUser where UserId='" + userid + "')";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Agency"].ToString();
            }
            return "";

        }

        /// <summary>
        /// 根据人员id获取部门id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetDeptId(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select id from Com_Organization where id in (select OrgId from Com_OrgAddUser where UserId='" + userid + "')";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["id"].ToString();
            }
            return "";

        }

        /// <summary>
        /// 根据部门id获取部门名称
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetDeptByDeptId(string deptid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select Agency from Com_Organization where id ='" + deptid + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Agency"].ToString();
            }
            return "";
        }
        /// <summary>
        /// 根据人员id获取姓名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetName(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = @" select UserRealName from Com_UserInfos where Userid='" + userid+ "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserRealName"].ToString();
            }
            return "";

        }

        /// <summary>
        /// 根据字典项diccode获取dicname
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetDicName(string DicCode)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = @" select DicName from Dictionary where DicCode='" + DicCode + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["DicName"].ToString();
            }
            return "";

        }

        //public bool UpdateCash(string Key,string addmoney,string sign)
        //{
        //    try
        //    {

        //        ISelectDataSourceFace select = new SelectSQL();
        //        select.DataBaseAlias = "common";
        //        //select.CommandText = "update Com_UserInfos set cash=cash" + sign + "Abs('" + addmoney + "') where Userid = '" + GetLoginUser().Userid + "'";
        //        select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", Key, sign, addmoney ,GetLoginUser().Userid);
        //        select.ExecuteDataSet();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
            
        //}

        public bool UpdateCash(string Key, string addmoney, string sign, string userid, string PayRemark, string PayId,int type,string worktips)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", Key, sign, addmoney, userid);
                select.ExecuteDataSet();
                if (Key == "cash" && worktips=="special")
                {
                    select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", "total_money", sign, addmoney, userid);
                    select.ExecuteDataSet();
                }
                select.CommandText = string.Format(@"select cash,total_money from Com_UserInfos where Userid='{0}'", userid);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if(type==0)
                {
                    SetHisMoney(userid, dt.Rows[0]["cash"].ToString(), dt.Rows[0]["total_money"].ToString(), addmoney, "现金", PayRemark, PayId, sign);
                }
                else
                {
                    SetHisMoney(userid, dt.Rows[0]["cash"].ToString(), dt.Rows[0]["total_money"].ToString(), addmoney, "总资产", PayRemark, PayId, sign);
                }
                
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void SetHisMoney(string userid, string _cash, string _total, string D_value, string type, string PayRemark, string PayId, string Sign)
        {
            string dd_value = Sign == "-" ? "-" + Math.Abs(Convert.ToDecimal(D_value)) : D_value;
            IInsertDataSourceFace insert = new InsertSQL("History_Money");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("Creator", userid);
            insert.AddFieldValue("CreateTime", DateTime.Now);
            insert.AddFieldValue("Cash", _cash);
            insert.AddFieldValue("TotalMoney", _total);
            insert.AddFieldValue("D_value", dd_value);
            insert.AddFieldValue("type", type);
            insert.AddFieldValue("PayRemark", PayRemark);
            insert.AddFieldValue("PayId", PayId);
            insert.ExecuteNonQuery();
        }

        public bool UpdateTotal(string total)
        {
            try
            {

                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"update Com_UserInfos set total_money ={0} where Userid = '{1}'", total, GetLoginUser().Userid);
                select.ExecuteDataSet();
                return true;
            }
            catch
            {
                return false;
            }

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

        public string GetMenu(string userid,int level)  //根目录为0层
        {
            string Menu = string.Empty;
            string levelword = string.Empty;
            string Folder1 = string.Empty;
            string Folder2 = string.Empty;
            switch(level)
            {
                case 0:
                    Folder1 = "AccountsManage/";
                    Folder2 = "ReminderManage/";
                    break;
                case 1:
                    Folder1 = "/mobile/AccountsManage/";
                    Folder2 = "/mobile/ReminderManage/";
                    break;
            }
            DataRow dr = GetUserInfo(userid).Rows[0];
            for (int i = 0; i < level;i++ )
            {
                levelword += "../";
            }
                Menu = string.Format(@"
                <nav data-am-widget='menu' class='am-menu  am-menu-offcanvas1'  data-am-menu-offcanvas> 
                    <a href='javascript: void(0)' class='am-menu-toggle'>
                        <i class='am-menu-toggle-icon am-icon-bars'></i>
                    </a>
                <div class='am-offcanvas' >
                    <div class='am-offcanvas-bar am-offcanvas-bar-flip'>
                        <ul class='am-menu-nav am-avg-sm-1'>
                            <li class='am-parent'>
                                <a href='##'  >个人中心</a>
                                <ul class='am-menu-sub am-collapse  am-avg-sm-2 am-in' style='padding:0px 0 0px 0px !important;'>
                                    <li>
                                        <div class='login-ico-diy' style='margin-top:2.0rem;'>
                    <div style='margin:0'><img src='../{0}head_image/{5}' style='width:90px;height:90px;'></div>
                                        </div>
                                    </li>
                                    <li>
                                        <div><font color='white'><span style='font-size:13px'>用户名:&nbsp;&nbsp;{2}</span></font></div>
                                        <div><font color='white'><span style='font-size:13px'>总资产:&nbsp;&nbsp;<font color='orange'>{3}</font>元</span></font></div>
                                        <div><font color='white'><span style='font-size:13px'>现&nbsp;&nbsp;&nbsp;&nbsp;金:&nbsp;&nbsp;<font color='orange'>{4}</font>元</span></font></div>
                                    </li>
                                </ul>
                            </li>
                            <li class='am-parent'>
                                <a href='##'>账单管理</a>
                                <ul class='am-menu-sub am-collapse  am-avg-sm-3'>
                                    <li>
                                        <a href='{1}AccountRegisterDetail.aspx' >账单录入</a>
                                    </li>
                                    <li>
                                        <a href='{1}AccountsList.aspx' >账单管理</a>
                                    </li>
                                </ul>
                            </li>
                            <li class='am-parent'>
                                <a href='##'>交易管理</a>
                                <ul class='am-menu-sub am-collapse  am-avg-sm-3 '>
                                    <li >
                                        <a href='{1}DealList.aspx' >交易记录</a>
                                    </li>
                                </ul>
                            </li>
                            <li class='am-parent'>
                                <a href='##'>提醒事项</a>
                                <ul class='am-menu-sub am-collapse  am-avg-sm-3 '>
                                    <li >
                                        <a href='{6}ReminderRegisterDetail.aspx' >新增提醒</a>
                                    </li>
                                    <li >
                                        <a href='{6}ReminderList.aspx' >提醒管理</a>
                                    </li>
                                </ul>
                            </li>
                            <li class='am-parent'>
                                <a href='##'>安全中心</a>
                                <ul class='am-menu-sub am-collapse  am-avg-sm-3 '>
                                    <li >
                                        <a href='{0}UpdatePass.aspx' >修改密码</a>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <a href='{0}Default.aspx'>返回首页</a>
                            </li>
                            <li>
                                <a href='../{0}Login.aspx'>退出登录</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>", levelword , Folder1, dr["LoginName"].ToString(), dr["total_money"].ToString(), dr["cash"].ToString(), dr["head_pic"].ToString(),Folder2);
            
            return Menu;
        }

        public DataTable GetUserInfo(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql =
                string.Format(
                    @"select a.LoginName,b.cash,b.total_money,b.head_pic,a.UserId from Com_UserLogin a LEFT JOIN Com_UserInfos b ON a.UserId=b.Userid where a.UserId='{0}'", userid);
            select.CommandText = sql;
            return select.ExecuteDataSet().Tables[0];
        }

        public string GetInUser(string userid)
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
    }
}
