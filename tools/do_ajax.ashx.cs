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
using Accounts.BLL;

namespace Accounts.Web.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class do_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "addremark" :    //交易记录添加备注
                    add_remark(context);
                    break;
                case "addurl":    //账单添加活动网址
                    add_url(context);
                    break;
                case "getaccount":
                    get_account_list(context);
                    break;
                case "getdetail":
                    get_account_info(context);
                    break;
                case "getaccountdetail":
                    get_account_detail(context);
                    break;
                case "saveaccount":
                    updateaccount(context);
                    break;
                case "getalist":
                    get_accountlist(context);
                    break;
                case "getplist":
                    get_deallist(context);
                    break;
                case "updatecash":
                    update_cash(context);
                    break;
                case "saveremark":
                    update_userremark(context);
                    break;
                case "resendmail":
                    resend_mail(context);
                    break;
                case "sendnewmail":
                    sendnewmail(context);
                    break;
                case "ac_add":
                    add_account(context);
                    break;
                case "ac_delete":
                    ac_delete(context);
                    break;
                case "addreminder":
                    addreminder(context);
                    break;
                case "getheadpic":
                    getheadpic(context);
                    break;
                case "dl_delete":
                    dl_delete(context);
                    break;
                case "getrlist":
                    get_reminderlist(context);
                    break;
                case "reminder_add":
                    add_reminder(context);
                    break;
                case "tx_delete":
                    delete_reminder(context);
                    break;
                case "ac_count":
                    AC_Count(context);
                    break;
            }
        }

        #region 添加备注====================================
        private void add_remark(HttpContext context)
        {
            string recordid = DTRequest.GetFormString("recordid");
            string remark = DTRequest.GetFormString("remark");
            //更新备注
            if (UpdateRemark(recordid, remark))
            {
                context.Response.Write("{ \"status\":\"true\"}");
            }
            else
            {
                context.Response.Write("{ \"status\":\"false\"}");
            }
        }
        #endregion

        #region 添加活动网址====================================
        private void add_url(HttpContext context)
        {
            string accountid = DTRequest.GetFormString("id");
            string u_name = HttpUtility.UrlDecode(DTRequest.GetFormString("name"));
            string u_url = DTRequest.GetFormString("url");
            string url = "&nbsp;&nbsp;<a href='" + u_url + "' target='_blank' style='color:red'>" + u_name + "</a>";
            //更新备注
            if (UpdateUrl(accountid, u_name, u_url))
            {
                context.Response.Write("{ \"status\":\"true\",\"url\":\"" + url + "\"}");
            }
            else
            {
                context.Response.Write("{ \"status\":\"false\",\"url\":\"\"}");
            }
        }
        #endregion

        #region 个人中心获取账单列表数据====================================
        private void get_account_list(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            int page = DTRequest.GetFormInt("page");
            int everypagenum  = 5; //每页显示条数
            int startnum = (page - 1) * everypagenum + 1; //起始条数
            int endnum = page * everypagenum;           //终止条数
            string text = string.Empty;
            if(startnum<=20) //仅查询前20条
            {
                string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId ='5' and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30  and isFinish=0 ", userid);

                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"
                                                select * from (
                                                select row_number() over (order by dq_time asc,CreateTime asc) as rowid,
	                                            id,
	                                            zd_name,
                                                ds_money,
	                                            dq_time,
                                                DATEDIFF (day,GETDATE(),dq_time) as dq_day,
	                                            status
	                                            FROM View_Account {0} ) a 
                                                where rowid between {1} and {2}
                                                order by rowid
                                                ", where, startnum, endnum);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                string content = string.Empty;
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string zd_name = dt.Rows[i]["zd_name"].ToString().Length < 13 ? dt.Rows[i]["zd_name"].ToString() : dt.Rows[i]["zd_name"].ToString().Substring(0, 7) + "...";
                    string dq_string = int.Parse(dt.Rows[i]["dq_day"].ToString()) >= 0 ? dt.Rows[i]["dq_day"].ToString() + "天后" + (dt.Rows[i]["status"].ToString() == "wait_receive" ? "收款" : (dt.Rows[i]["status"].ToString() == "wait_repay" ? "还款" : (dt.Rows[i]["status"].ToString() == "wait_deposit" ? "存款" : "到期"))) : "已超期" + System.Math.Abs(int.Parse(dt.Rows[i]["dq_day"].ToString())) + "天";
                    string ds_money_string = (dt.Rows[i]["status"].ToString() == "wait_repay" ? "<font color='red'>"+  (Convert.ToDecimal(dt.Rows[i]["ds_money"].ToString()) < 0 ? "": "-") + dt.Rows[i]["ds_money"].ToString() + "元" : "<font color='green'>+" + dt.Rows[i]["ds_money"].ToString() + "元") + "</font>";
                    string dq_date = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dt.Rows[i]["dq_time"].ToString()));
                    string piccolor = int.Parse(dt.Rows[i]["dq_day"].ToString()) <= 7 ? (int.Parse(dt.Rows[i]["dq_day"].ToString()) >= 0 ? "yellow" : "red") : "green";
                    text += dt.Rows[i]["id"].ToString() + "&" + piccolor + "&" + zd_name + "&" + ds_money_string + "&" + dq_string + "&" + dq_date + ",";
                }
                text = text.Length > 0 ? text.Substring(0, text.Length - 1):"";
            }
            context.Response.Write(text);
        }
        #endregion

        #region 账单列表获取数据====================================
        private void get_accountlist(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string type = DTRequest.GetFormString("type"); //类型
            string status = DTRequest.GetFormString("status"); //类型
            string s_string = "";
            string s_order = "order by dq_time asc,CreateTime asc";
            switch (status)
            {
                case "0":   //未完成
                    s_string = "and IsFinish='0'";
                    break;
                case "1":   //待收款
                    s_string = "and status = 'wait_receive'";
                    break;
                case "2":   //待还款
                    s_string = " and status = 'wait_repay'";
                    break;
                case "3":   //待存款
                    s_string = " and status = 'wait_deposit'";
                    break;
                case "4":   //延期中
                    s_string = " and status = 'late_pay'";
                    break;
                case "5":   //本周到期
                    s_string = " and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 7 and IsFinish='0'";
                    break;
                case "6":   //账单坏账
                    s_string = " and status = 'bad_pay'";
                    s_order = "order by FinishTime desc";
                    break;
                case "7":   //全部
                    s_string = "";
                    s_order = "order by IsFinish asc,dq_time asc,FinishTime desc";
                    break;
                case "8":   //本月到期
                    s_string = " and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30 and IsFinish='0'";
                    break;
                case "9":   //已完成
                    s_string = " and IsFinish='1'";
                    s_order = "order by FinishTime desc";
                    break;
                default:
                    s_string = "";
                    break;
            }
            string keyword = DTRequest.GetFormString("keyword"); //关键字
            s_string += string.Format(@" and (zd_name like '%{0}%' or zd_detail like '%{0}%' or remark like '%{0}%' or tz_people like '%{0}%')", keyword);
            
            int page = DTRequest.GetFormInt("page");
            int everypagenum  = 5; //每页显示条数
            int startnum = (page - 1) * everypagenum + 1; //起始条数
            int endnum = page * everypagenum;           //终止条数
            string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId='{1}' {2}", userid,type,s_string);

            int totalcount = GetAccountNum(where);
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"
                                                select * from (
                                                select row_number() over ({3}) as rowid,
	                                            id,
                                                TempId,
	                                            zd_name,
                                                ds_money,
                                                tz_people,
	                                            dq_time,
                                                DATEDIFF (day,GETDATE(),dq_time) as dq_day,
                                                IsFinish,
	                                            status
	                                            FROM View_Account {0} ) a 
                                                where rowid between {1} and {2}
                                                order by rowid
                                                ", where, startnum, endnum, s_order);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            string content = string.Empty;
            string text = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string zd_name = dr["zd_name"].ToString().Length < 13 ? dr["zd_name"].ToString() : dr["zd_name"].ToString().Substring(0, 7) + "...";
                string dq_string = dr["IsFinish"].ToString() == "True" ? "账单已完成" : (int.Parse(dr["dq_day"].ToString()) >= 0 ? dr["dq_day"].ToString() + "天后" + (dr["status"].ToString() == "wait_receive" ? "收款" : (dr["status"].ToString() == "wait_repay" ? "还款" : (dr["status"].ToString() == "wait_deposit" ? "存款" : "到期"))) : "已超期" + System.Math.Abs(int.Parse(dr["dq_day"].ToString())) + "天");
                string ds_money_string = (dr["status"].ToString() == "wait_repay" ? "<font color='red'>" + (Convert.ToDecimal(dt.Rows[i]["ds_money"].ToString()) < 0 ? "" : "-") + dr["ds_money"].ToString() + "元" : "<font color='green'>+" + dr["ds_money"].ToString() + "元") + "</font>";
                string dq_date = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dr["dq_time"].ToString()));
                string piccolor = dr["IsFinish"].ToString()=="True" ? "green" :( int.Parse(dr["dq_day"].ToString()) <= 7 ? (int.Parse(dr["dq_day"].ToString()) >= 0 ? "yellow" : "red") : "green");
                content += string.Format(@"
                        <a href='AccountDetail.aspx?id={0}&tempid={8}'>
                        <dl class='am-accordion-item' style='padding-top:5px'>
                            <dt class='am-accordion-title' style='font-size:11px;'>
                                {7}&nbsp;<img  style='width: 15px; height: 15px; vertical-align: middle;' src='../../Images/{1}.gif'/>&nbsp;{2}<span ></span><span style='float:right;padding-left:5px' >{3}</span><span style='float:right;padding-left:3px'>{4}</span><span style='padding-right:5px;float:right' >{5}</span>
                            </dt>
                            <dd class='am-accordion-bd am-collapse  am-in'>
                                <div class='am-accordion-content'>
                                    <div style='float:right;font-size:xx-small'>投资人：{6}</div>
                                </div>
                            </dd>
                        </dl></a>", dr["id"].ToString(), piccolor, zd_name, dq_date, dq_string, ds_money_string, dr["tz_people"].ToString(), startnum + i, dr["TempId"].ToString());
            }
            context.Response.Write(content + "*&*" + totalcount);
        }
        #endregion

        #region 获取交易记录数据====================================
        private void get_deallist(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string order = DTRequest.GetFormString("order"); //顺序
            string sdate = DTRequest.GetFormString("sdate"); //起始日期
            string edate = DTRequest.GetFormString("edate"); //截止日期

            string where = string.Format("Where Creator='{0}' ", userid);
            if (!string.IsNullOrEmpty(sdate))
            {
                where += string.Format(" and Cast(CreateTime as date)>='{0}'", sdate);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                where += string.Format(" and Cast(CreateTime as date)<='{0}'", edate);
            }

            string _order = string.Empty;  //顺序-拼接字符串
            string time_order = " desc"; //时间排序 -- 默认降序
            switch (order)
            {
                case "1":   //按总资产从高到低
                    _order = "TotalMoney desc,";
                    break;
                case "2":   //按总资产从低到高
                    _order = "TotalMoney asc,";
                    break;
                case "3":   //按现金从高到低
                    _order = "Cash desc,";
                    break;
                case "4":   //按现金从低到高
                    _order = "Cash asc,";
                    break;
                case "5":   //按收支为正
                    where += " and D_value >=0";
                    break;
                case "6":   //按收支为负
                    where += " and D_value <0";
                    break;
                case "7":   //按收支类型为总资产
                    where += " and type='总资产'";
                    break;
                case "8":   //按收支类型为现金
                    where += " and type='现金'";
                    break;
                case "9": //升序
                    time_order = " asc";
                    break;
                default:
                    _order += "";
                    break;
            }
            _order += "CreateTime " + time_order + ",id " + time_order;
            int page = DTRequest.GetFormInt("page");
            int everypagenum = 5; //每页显示条数
            int startnum = (page - 1) * everypagenum + 1; //起始条数
            int endnum = page * everypagenum;           //终止条数

            int totalcount = GetNum(where, "History_Money");
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"
                                                select * from (
                                                select row_number() over (order by {3}) as rowid,
                                                id,
                                                CreateTime,
                                                Cash,
                                                TotalMoney,
                                                D_value,
                                                type,
                                                PayRemark
                                                FROM  History_Money {0} ) a 
                                                where rowid between {1} and {2}
                                                order by rowid
                                                ", where, startnum, endnum, _order);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            string content = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string icon = Convert.ToDecimal(dr["D_value"].ToString()) >= 0 ? "in" : "out";
                string dealmoney = Convert.ToDecimal(dr["D_value"].ToString()) >= 0 ? "</b><font color='green'>+" + dr["D_value"].ToString() + "元</font>" : "</b><font color='red'>" + dr["D_value"].ToString() + "元</font>";
                content += string.Format(@"
                        <dl class='am-accordion-item' style='padding-top:5px'>
                            <dt class='am-accordion-title' style='font-size:11px;'>
                                <img  style='width: 15px; height: 15px; vertical-align: middle;' src='../img/{0}.png'/>&nbsp;{1}<span ></span><span style='float:right;padding-left:5px' >{2}</span>
                            </dt>
                            <dd class='am-accordion-bd am-collapse  am-in'>
                                <div class='am-accordion-content'>
                                    <table style='width:100%;font-size:11px;'>
                                    <tr>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>交易详情：</b><a href='DealDetail.aspx?id={5}' ><font color='#2B6FD5'>点击查看</font></a></div>
                                            <div style='float:right;'><b>{4}{3}</b></div>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </dd>
                        </dl>", icon, dr["PayRemark"].ToString(), string.Format("{0:yyyy-MM-dd H:mm:ss}", dr["CreateTime"]), dealmoney, dr["type"].ToString(), dr["id"].ToString());
            }
            context.Response.Write(content + "*&*" + totalcount);
        }
        #endregion

        #region 个人中心获得账单数据====================================
        private void get_account_info(HttpContext context)
        {
            string id = DTRequest.GetFormString("id"); //账单id
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"
                                                select 
                                                TempId,
	                                            zd_name,
                                                zd_detail,
                                                ds_money,
                                                tz_money,
	                                            tz_people,
	                                            dq_time,
                                                CreateTime,
                                                DATEDIFF (day,GETDATE(),dq_time) as dq_day,
                                                DATEDIFF (day,CreateTime,dq_time) as tz_day,
	                                            status
	                                            FROM View_Account where id={0}",id);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            string content = string.Empty;
            string text = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string zd_name = dr["zd_name"].ToString().Length < 15 ? dr["zd_name"].ToString() : dr["zd_name"].ToString().Substring(0, 7) + "...";
                string dq_string = int.Parse(dr["dq_day"].ToString()) >= 0 ? dr["dq_day"].ToString() + "天后" + (dr["status"].ToString() == "wait_receive" ? "收款" : (dr["status"].ToString() == "wait_repay" ? "还款" : (dr["status"].ToString() == "wait_deposit" ? "存款" : "到期"))) : "已超期" + System.Math.Abs(int.Parse(dr["dq_day"].ToString())) + "天";
                string dq_date = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dr["dq_time"].ToString()));
                string sz_detail = dr["status"].ToString() == "already_repay" || dr["status"].ToString() == "wait_repay" ? (Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) >= 0 ? ("<font color='green' >+" + Math.Abs(Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>") : ("<font color='red' >-" + Math.Abs(Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>")) : (dr["status"].ToString() == "bad_pay" ? ((Convert.ToDecimal(dr["ds_money"].ToString()) > 0 ? "<font color='red'>-" : "<font color='green'>+") + Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) + "元</font>") : (Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString())) >= 0 ? ("<font color='green' >+" + Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>") : ("<font color='red' >-" + Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>")));
                content += string.Format(@"
                                <a href='AccountsManage/AccountDetail.aspx?id={9}&tempid={10}'><div class='am-accordion-content'>
                                    <table style='width:100%;font-size:11px;'>
                                    <tr>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>账单名称：</b><font color='green'>{0}</font></div>
                                        </td>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>投资金额：</b><font color='orange'>{1}元</font></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='2'>
                                            <div style='float:left;padding-left:10px'><b>账单详情：</b>{2}</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='2'>
                                            <div style='float:left;padding-left:10px'><b>投资天数：</b><font color='#2292DD'>{3}天</font></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>录入时间：</b>{4}</div>
                                        </td>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>到期日期：</b><font color='#9E4DB3'>{5}</font></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>投资人：</b>{6}</div>
                                        </td>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>待收金额：</b><font color='orange'>{7}元</font></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>状态：</b>{8}</div>
                                        </td>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>收支情况：</b>{11}</div>
                                        </td>
                                    </tr>
                                </table>
                           </div></a>", zd_name, dr["tz_money"].ToString(), dr["zd_detail"].ToString(), dr["tz_day"].ToString(), string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(dr["CreateTime"].ToString())), dq_date, dr["tz_people"].ToString(), dr["ds_money"].ToString(), new Accounts.BLL.BasePage().GetDicName(dr["status"].ToString()), id, dr["TempId"].ToString(), sz_detail);
            }
            context.Response.Write(content);
        }
        #endregion

        #region 获得账单详细数据====================================
        private void get_account_detail(HttpContext context)
        {
            string id = DTRequest.GetFormString("id"); //账单id
            string userid = DTRequest.GetFormString("userid"); //用户id
            int tempid = Convert.ToInt32(DTRequest.GetFormString("tempid").ToString()); //模板id
            string content = string.Empty;
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select
                                                        IsDelete,
                                                        url,
                                                        urlname,
                                                        zd_name,
                                                        zd_detail,
                                                        CreateTime,
                                                        DATEDIFF(day,GETDATE(),dq_time) as dq_day,
                                                        DATEDIFF(day,dq_time,GETDATE()) as daycount,
                                                        DATEDIFF (day,CreateTime,dq_time) as tz_day,
                                                        Creator,
                                                        Account_Status,
                                                        Remark,
                                                        dq_time,
                                                        tz_money,
                                                        tz_people,
                                                        ds_money,
                                                        IsFinish,
                                                        FinishTime
                                                        from View_Account where TempId='{0}' and Id={1} and Creator='{2}' ", tempid, id, userid); //and IsDelete='0'
                DataTable dt = select.ExecuteDataSet().Tables[0];


                //string text = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string dq_date = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dr["dq_time"].ToString()));
                    string createtime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["CreateTime"]);
                    string finishtime = dr["IsFinish"].ToString() == "True" ? string.Format("{0:yyyy-MM-dd HH:mm:ss}", dr["FinishTime"]) : "账单尚未完成";
                    string sz_detail = dr["Account_Status"].ToString() =="already_repay" || dr["Account_Status"].ToString() =="wait_repay" ? (Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) >= 0 ? ("<font color='green' >+" + Math.Abs(Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>") : ("<font color='red' >-" + Math.Abs(Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>"))  :  (dr["Account_Status"].ToString() =="bad_pay" ?   ((Convert.ToDecimal(dr["ds_money"].ToString()) > 0 ? "<font color='red'>-" : "<font color='green'>+") + Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) + "元</font>")    :(Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString())) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString())) >= 0 ? ("<font color='green' >+" + Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>") : ("<font color='red' >-" + Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()) - Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()))) + "元</font>")));
                    content += string.Format(@"
                <h1 style='text-align:center;font-size:18px;padding-bottom:10px;padding-top:5px'>{0}{8}</h1>
                <div style='padding-left:8%;padding-right:8%;padding-top:10px'>                
                <table style='width:100%;font-size:12px;' >
                    <tr>
                        <td style='width:60%;'>
                            <div style='float:left;'><b>账单名称：</b><font color='green'>{0}</font></div>
                            
                        </td>
                        <td style='width:100%;'>
                            <div style='float:left;'><b>投资金额：</b><font color='orange'>{1}元</font></div>
                        </td>
                    </tr>
                    <tr>
                        <td style='width:50%;;padding-top:3px' colspan='2'>
                            <div style='float:left;'><b>账单详情：</b>{2}</div>
                        </td>
                    </tr>
                    <tr>
                        <td style='width:50%;;padding-top:3px' colspan='2'>
                            <div style='float:left;'><b>投资天数：</b><font color='#2292DD'>{3}天</font></div>
                        </td>
                    </tr>
                    <tr>
                        <td style='width:50%;;padding-top:3px'>
                            <div style='float:left;'><b>投资人：</b>{4}</div>
                        </td>
                        <td style='width:100%;'>
                            <div style='float:left;'><b>待收金额：</b><font color='orange'>{7}元</font></div>
                        </td>
                    </tr>
                    <tr>
                        <td style='width:100%;;padding-top:3px' colspan='2'>
                            <div style='float:left;'><b>录入时间：</b>{5}</div>
                        </td>
                    </tr>
                    <tr>
                        <td style='width:60%;;padding-top:3px'  colspan='2'>
                            <div style='float:left;'><b>完成时间：</b>{9}</div>
                        </td>
                    </tr>
                    <tr>
                        <td style='width:60%;;padding-top:3px'>
                            <div style='float:left;'><b>到期日期：</b><font color='#9E4DB3'>{6}</font></div>
                        </td>
                        <td style='width:100%;'>
                            <div style='float:left;'><b>收支情况：</b>{10}</div>
                        </td>
                    </tr>", dr["zd_name"].ToString(), dr["tz_money"].ToString(), dr["zd_detail"].ToString(), dr["tz_day"].ToString(), dr["tz_people"].ToString(), createtime, dq_date, dr["ds_money"].ToString(), dr["IsFinish"].ToString() == "True" || dr["IsDelete"].ToString() == "True" ? "" : "<span style='font-size:xx-small'>(<a href='AccountRegisterDetail.aspx?action=edit&id=" + id + "'>点此编辑</a>)</span>", finishtime, sz_detail);
                    //显示网址
                    string url_label = string.Empty;
                    //string input_panel_display = "";
                    string add_panel_display = "";
                    string modify_panel_display = ";display:none";
                    if (dr["url"].ToString() != "")
                    {
                        url_label = "<a href='" + dr["url"].ToString() + "' target='_blank' style='color:red'>" + dr["urlname"].ToString() + "</a>";
                        //input_panel_display = ";display:none";
                        add_panel_display = ";display:none";
                        modify_panel_display = "";
                    }
                    content += string.Format(@"
                        <tr>
                            <td style='width:100%;padding-top:5px' colspan='2'>
                                 <div style='float:left;'><b id='url_title'>活动网址:</b>
                                    <label id='url_label' runat='server' style='text-align:left;padding-top:2px{4}'>&nbsp;&nbsp;&nbsp;{0}</label>
                                    <label runat='server' id='_name_panel' style='pading-top:2px;display:none' >名称：</label><input type='text' id='u_name' runat='server' style='width:20%;height:80%;padding-top:2px;display:none'/> 
                                    <label runat='server' id='_url_panel' style='pading-top:2px;display:none' >网址：</label> <input type='text' id='u_url' runat='server' style='width:20%;height:80%;padding-top:2px;display:none'/>
                                    <input type='button' id='new' runat='server' onclick='NewUrl();' value='新增'  style='{3}{6}' {5}/>
                                    <input type='button' id='add' runat='server' onclick='AddUrl();' value='添加'  style='display:none;{6}' {5}/>
                                    <input type='button' id='modify' runat='server' onclick='ModifyUrl();' value='修改'  style='{4}{6}' {5}/>
                                    <input type='button' id='confirm' runat='server' onclick='Confirm();' value='确定'  style='display:none{6}' {5}/>&nbsp;
                                    <input type='button' id='cancle' runat='server' onclick='Cancle();' value='取消'  style='display:none;{6}' {5}/>
                                    <input type='button' id='newcancle' runat='server' onclick='NewCancle();' value='取消'  style='display:none;{6}' {5}/>
                                 </div>
                                <input type='hidden' id='hidurlname' value='{1}'> <input type='hidden' id='hidurl' value='{2}'>
                             </td>
                             
                        </tr>", url_label, dr["urlname"].ToString(), dr["url"].ToString(), add_panel_display, modify_panel_display, "class='am-radius-xl am-btn am-btn-default'", ";height:18px;font-size:xx-small;padding-top:1px !important");

                    content += string.Format(@"
                                        <tr>
                                            <td style='width:50%;;padding-top:3px;' colspan='2' >
                                                <b style='padding-top:13px;text-align:center'>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</b>
                                                <span id='remarktip'>{0}</span>
                                                <div class='am-form-group' style='display:none' id='remark_panel'>
                                                    <textarea style='width:100%;' rows='4' id='remark'>{1}</textarea>
                                                </div>
                                            </td>
                                        </tr>
                                        ", dr["remark"].ToString() == "" ? "未填写备注" : dr["remark"].ToString(), dr["remark"].ToString());
                    content += GetDropDownList(dr["Account_Status"].ToString(), dr["IsDelete"].ToString());
                    bool sign = IsFinish_Schedule(id);
                    content += string.Format(@"
                                            <td style='width:100%;text-align:center;{4}'>
                                                <div style='float:left;padding-left:5%'>
                                                     <input type='button' id='ConfirmBtn' runat='server' onclick='savedata();' value='认定'  style='height:18px;font-size:xx-small;padding-top:1px !important{1}' class='am-radius-xl am-btn am-btn-success '/>
                                                     <input type='button' id='RcancleBtn' runat='server' onclick='remarkcancle({0});' value='取消'  style='height:18px;font-size:xx-small;padding-top:1px !important;display:none' class='am-radius-xl am-btn am-btn-success '/>
                                                     &nbsp;<input type='button' id='remarkbtn' runat='server' onclick='addremark();' value='备注'  style='height:18px;font-size:xx-small;padding-top:1px !important' class='am-radius-xl am-btn am-btn-success '/>
                                                </div>
                                            </td>
                                        </tr>
                                   </table></div>{5}<div style='text-align:center;padding-top:20px;font-size:15px;'>
                    <a href='AccountRegisterDetail.aspx?action=mbadd&id={2}' >
                    <input type='button' value='以此账单为模板创建账单' class='btnAdd' /></a>{3}</div>",
                    sign == true ? "1" : "2", sign == true ? ";display:none" : "", id, dr["IsDelete"].ToString() == "True" ? "" : string.Format(@"<a href='javascript:void(0);' onclick='AC_Delete({0});'><input type='button' value='删除该账单' class='btnDel' /></a>", id), dr["IsDelete"].ToString() == "True" ? "display:none;" : "", dr["IsDelete"].ToString() == "True" ? "<h1 style='text-align:center;font-size:18px;padding-bottom:10px;padding-top:5px;color:red'>此账单已被删除，仅供查阅</h1>" : "");
                }
                else
                {
                    content += "<div style='text-align:center;padding-top:20px'>此条账单不属于你或者已经消失在宇宙中了哦~</div>";
                }
            }
            catch (Exception e){
                content += "<div style='text-align:center;padding-top:20px'>系统出错或参数错误，请稍后再试~</div>";
            }
            context.Response.Write(content);
        }
        #endregion

        #region 更新账单信息====================================
        private void updateaccount(HttpContext context)
        {
            string accountid = DTRequest.GetFormString("id");
            string remark = DTRequest.GetFormString("remark");
            string userid = DTRequest.GetFormString("userid");
            string status = DTRequest.GetFormString("status");
            //string u_name = HttpUtility.UrlDecode(DTRequest.GetFormString("name"));

            //更新账单信息
            string msg = UpdateAccount(accountid, userid, remark, status);
            if (msg == "true")
            {
                msg = "操作成功！";
            }
            context.Response.Write(msg);
        }
        #endregion

        #region 更新用户备注信息====================================
        private void update_userremark(HttpContext context)
        {
            string dealid = DTRequest.GetFormString("payid");
            string remark = DTRequest.GetFormString("remark");
            string userid = DTRequest.GetFormString("userid");

            //更新账单信息
            string msg = Update_UserRemark(dealid, userid, remark);
            if (msg == "true")
            {
                msg = "操作成功！";
            }
            context.Response.Write(msg);
        }
        #endregion

        #region 更新资金====================================
        private void update_cash(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            decimal d_value = Convert.ToDecimal(DTRequest.GetFormString("d_value"));
            //更新资金
            if(d_value != 0)
            {
                if (Update_Cash(userid, d_value))
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
            else
            {
                context.Response.Write("true");
            }
            
        }
        #endregion

        #region 重新发送邮件====================================
        private void resend_mail(HttpContext context)
        {
            try
            {
                string mailid = DTRequest.GetFormString("mailid");
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"select title,content,receiver from History_Mail where id='{0}'", mailid);
                DataRow dr = select.ExecuteDataSet().Tables[0].Rows[0];

                //发送邮件
                if (SendMail(dr["receiver"].ToString(), dr["title"].ToString(), dr["content"].ToString()))
                {
                    IUpdateDataSourceFace update = new UpdateSQL("History_Mail");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("sendtime",DateTime.Now);
                    update.AddFieldValue("IsSend", "1");
                    update.AddWhere("id", mailid);
                    update.ExecuteNonQuery();
                    context.Response.Write("{ \"status\":\"true\"}");
                }
                else
                {
                    context.Response.Write("{ \"status\":\"false\"}");
                }
            }
            catch (Exception e)
            {
                context.Response.Write("{ \"status\":\"false\"}");
            }
        }
        #endregion

        #region 发送新邮件====================================
        private void sendnewmail(HttpContext context)
        {
                string receiver = DTRequest.GetFormString("receiver");
                string title = DTRequest.GetFormString("title");
                string content = DTRequest.GetFormString("content");
                try
                {
                    IInsertDataSourceFace insert = new InsertSQL("History_Mail");
                    insert.DataBaseAlias = "common";
                    insert.AddFieldValue("receiver", receiver);
                    insert.AddFieldValue("title", title);
                    insert.AddFieldValue("content", content);
                    insert.AddFieldValue("sender", ConfigurationManager.AppSettings["MailUserName"]);
                    insert.AddFieldValue("addtime", DateTime.Now);
                    insert.AddFieldValue("IsSend", "0");
                    insert.ExecuteNonQuery();
                    if (SendMail(receiver, title, content))
                    {
                        ISelectDataSourceFace update = new SelectSQL();
                        update.DataBaseAlias = "common";
                        update.CommandText = string.Format(@"update History_Mail set IsSend='1',sendtime='{1}' where receiver='{0}' and addtime = (select top 1 addtime from History_Mail where receiver='{0}' order by addtime desc)", receiver, DateTime.Now);
                        update.ExecuteDataSet();
                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }
                catch (Exception e)
                {
                    context.Response.Write("false");
                }
        }
        #endregion

        #region 添加/修改账单====================================
        private void add_account(HttpContext context)
        {
            string type = DTRequest.GetFormString("type");
            string ac_name = DTRequest.GetFormString("ac_name");
            string tz_money = DTRequest.GetFormString("tz_money");
            string dq_day = DTRequest.GetFormString("dq_day");
            string zd_detail = DTRequest.GetFormString("zd_detail");
            string tz_people = DTRequest.GetFormString("tz_people");
            string ds_money = DTRequest.GetFormString("ds_money");
            string status = DTRequest.GetFormString("status");
            string action = DTRequest.GetFormString("action");
            string userid = DTRequest.GetFormString("userid");
            string id = DTRequest.GetFormString("id"); //账单id
            //增加/修改账单
            string msg = SaveAccount(type, ac_name, tz_money, dq_day, zd_detail, tz_people, ds_money, status, action, userid, id);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
        }
        #endregion

        #region 删除账单====================================
        private void ac_delete(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string id = DTRequest.GetFormString("id"); //账单id
            string msg = DeleteAccount(id,userid);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
        }
        #endregion

        #region 删除交易记录====================================
        private void dl_delete(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string id = DTRequest.GetFormString("id"); //交易记录id
            string msg = DeleteDeal(id, userid);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
        }
        #endregion

        #region 添加/修改提醒事项 PC端====================================
        private void addreminder(HttpContext context)
        {
            string type = DTRequest.GetFormString("type");
            string userid = DTRequest.GetFormString("userid");
            string title = DTRequest.GetFormString("title");
            string content = DTRequest.GetFormString("content");
            string remindman = DTRequest.GetFormString("remindman");
            string remindtime = DTRequest.GetFormString("remindtime");
            string editid = DTRequest.GetFormString("editid");
            //增加/修改账单
            string msg = SaveReminder(type,userid,title,content,remindman,remindtime,editid);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
        }
        #endregion

        #region 获取用户头像====================================
        private void getheadpic(HttpContext context)
        {
            string username = DTRequest.GetFormString("loginname");
            //增加/修改账单
            string headpic = Get_HeadPic(username);
            context.Response.Write(headpic);
        }
        #endregion

        #region 获取提醒事项列表数据====================================
        private void get_reminderlist(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string type = DTRequest.GetFormString("type"); //类型
            string order = DTRequest.GetFormString("order"); //顺序
            string sdate = DTRequest.GetFormString("sdate"); //起始日期
            string edate = DTRequest.GetFormString("edate"); //截止日期
            string keyword = DTRequest.GetFormString("keyword"); //关键字

            string _order = string.Empty;  //顺序-拼接字符串
            string _type = string.Empty; //类型-拼接字符串

            string where = string.Format("Where Creator='{0}' ", userid);
            if (!string.IsNullOrEmpty(sdate))
            {
                where += string.Format(" and Cast(RemindTime as date)>='{0}'", sdate);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                where += string.Format(" and Cast(RemindTime as date)<='{0}'", edate);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                where += string.Format(" and (RemindMail like '%{0}%' or title like '%{0}%' or content like '%{0}%')", keyword);
            }
            //提醒事项类型
            switch (type)
            {
                case "1":
                    _type = " and Status='wait_remind' ";
                    break;
                case "2":
                    _type = " and Status='already_remind' ";
                    break;
                default:
                    _type = "";
                    break;
            }
            where += _type;

            //排序类型
            _order += "IsRemind asc,";
            switch (order)
            {
                case "1":   //提醒时间升序
                    _order += "RemindTime asc";
                    break;
                case "2":   //提醒时间降序
                    _order += "RemindTime desc";
                    break;
                case "3":   //创建时间升序
                    _order += "CreateTime asc";
                    break;
                case "4":   //创建时间降序
                    _order += "CreateTime desc";
                    break;
            }

            int page = DTRequest.GetFormInt("page");
            int everypagenum = 5; //每页显示条数
            int startnum = (page - 1) * everypagenum + 1; //起始条数
            int endnum = page * everypagenum;           //终止条数

            int totalcount = GetNum(where, "Reminder");
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"
                                                select * from (
                                                select row_number() over (order by {3}) as rowid,
                                                id,
                                                title,
                                                content,
                                                RemindMail,
                                                RemindTime,
                                                Status
                                                FROM  Reminder {0} ) a 
                                                where rowid between {1} and {2}
                                                order by rowid
                                                ", where, startnum, endnum, _order);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            string content = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string icon = dr["Status"].ToString() == "wait_remind" ? "alert.gif" : "success.gif";
                content += string.Format(@"
                        <a href='ReminderDetail.aspx?id={6}' ><dl class='am-accordion-item' style='padding-top:5px'>
                            <dt class='am-accordion-title' style='font-size:11px;'>
                               {0} <img  style='width: 15px; height: 15px; vertical-align: middle;' src='../img/{1}'/>&nbsp;{2}<span ></span><span style='float:right;padding-left:5px' >{3}</span>
                            </dt>
                            <dd class='am-accordion-bd am-collapse  am-in'>
                                <div class='am-accordion-content'>
                                    <table style='width:100%;font-size:11px;'>
                                    <tr>
                                        <td>
                                            <div style='float:left;padding-left:10px'><b>提醒详情：</b>{4}</div>
                                            <div style='float:right;'><b>提醒对象：</b>{5}</div>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </dd>
                        </dl></a>", startnum + i, icon, dr["title"].ToString().Length > 13 ? dr["title"].ToString().Substring(0, 13) + "..." : dr["title"].ToString(), string.Format("{0:yyyy-MM-dd HH时 mm分}", dr["RemindTime"]), dr["content"].ToString().Length > 13 ? dr["content"].ToString().Substring(0, 13) + "..." : dr["content"].ToString(), dr["RemindMail"].ToString(), dr["id"].ToString());
            }
            context.Response.Write(content + "*&*" + totalcount);
        }
        #endregion

        #region 添加/修改提醒事项 Mobile端====================================
        private void add_reminder(HttpContext context)
        {
            string reminder = DTRequest.GetFormString("reminder");
            string r_title = DTRequest.GetFormString("r_title");
            string r_detail = DTRequest.GetFormString("r_detail");
            string remindtime = DTRequest.GetFormString("remindtime");
            string action = DTRequest.GetFormString("action");
            string userid = DTRequest.GetFormString("userid");
            string id = DTRequest.GetFormString("id"); //提醒事项id（编辑）
            //增加/修改账单
            string msg = SaveReminder_Mobile(action, r_title, r_detail, remindtime, userid, reminder,id);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
        }
        #endregion

        #region 删除提醒事项====================================
        private void delete_reminder(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string id = DTRequest.GetFormString("id"); //提醒事项id
            string msg = DeleteReminder(id, userid);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
        }
        #endregion

        #region 统计账单====================================
        private void AC_Count(HttpContext context)
        {
            string userid = DTRequest.GetFormString("userid");
            string hidfid = DTRequest.GetFormString("hidfid"); //账单id
            string msg = Account_Count(hidfid, userid);
            if (msg != "false")
            {
                context.Response.Write(msg);
            }
            else
            {
                context.Response.Write("false");
            }
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

        public bool UpdateRemark(string recordid, string remark)
        {
            try
            {
                IUpdateDataSourceFace update = new UpdateSQL("History_Money");
                update.DataBaseAlias = "common";
                update.AddFieldValue("UserRemark", remark);
                update.AddWhere("id", recordid);
                if(update.ExecuteNonQuery()>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool UpdateUrl(string id, string u_name,string u_url)
        {
            try
            {
                IUpdateDataSourceFace update = new UpdateSQL("Account");
                update.DataBaseAlias = "common";
                update.AddFieldValue("urlname", u_name);
                update.AddFieldValue("url", u_url);
                update.AddWhere("id", id);
                if (update.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public string UpdateAccount(string accountid,string userid,string remark,string status)
        {
            try
            {
                if (!IsFinish_Schedule(accountid))
                {
                    int temp_num = IsExist_Schedule(accountid);
                    string[] flagword = { "already_getmoney", "already_repay", "already_deposit", "bad_pay" };
                    List<string> list = new List<string>(flagword);
                    DataRow dr = GetAccountInfo(accountid);
                    if (temp_num == 0)
                    {
                        IInsertDataSourceFace insert = new InsertSQL("Account_Schedule");
                        insert.DataBaseAlias = "common";
                        insert.AddFieldValue("AccountId", accountid);
                        insert.AddFieldValue("AccountMonth", string.Format("{0:yyyyMM}", dr["CreateTime"]));
                        insert.AddFieldValue("Remark", remark);
                        insert.AddFieldValue("Creator", userid);
                        //insert.AddFieldValue("FinishTime", DateTime.Now);
                        insert.ExecuteNonQuery();
                    }
                    else
                    {
                        IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                        update1.DataBaseAlias = "common";
                        update1.AddFieldValue("Remark", remark);
                        update1.AddWhere("AccountId", accountid);
                        update1.ExecuteNonQuery();
                    }
                    #region 正常时间内认定状态流程
                    int daycount = Convert.ToInt32(dr["daycount"].ToString());
                    if (daycount > 0) 
                    {
                        IUpdateDataSourceFace update = new UpdateSQL("Account");
                        update.DataBaseAlias = "common";
                        update.AddFieldValue("Account_Status", status);
                        update.AddFieldValue("StatusName", new BLL.BasePage().GetDicName(status));
                        if (list.Contains(status))
                        {
                            update.AddFieldValue("isFinish", "1");
                            IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                            update1.DataBaseAlias = "common";
                            update1.AddFieldValue("FinishTime", DateTime.Now);
                            update1.AddWhere("AccountId", accountid);
                            update1.ExecuteNonQuery();

                            ///设置备注--延期完成的项目
                            string key_word = string.Empty;
                            ISelectDataSourceFace select = new SelectSQL();
                            select.DataBaseAlias = "common";
                            select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,FinishTime) as late_day from View_Account  where Id = '" + accountid + "'";
                            DataTable dt = select.ExecuteDataSet().Tables[0];

                            key_word = dt.Rows[0]["Remark"].ToString();
                            if (daycount != 0)
                            {
                                if (dt.Rows[0]["LateSign"].ToString() == "True")
                                {
                                    key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为延期完成，共延期" + Convert.ToInt32(dt.Rows[0]["late_day"].ToString()) + "天";
                                }
                                else
                                {
                                    key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为超期完成，共超期" + Convert.ToInt32(dt.Rows[0]["late_day"].ToString()) + "天";
                                }
                            }
                            IUpdateDataSourceFace update2 = new UpdateSQL("Account_Schedule");
                            update2.DataBaseAlias = "common";
                            update2.AddFieldValue("Remark", key_word);
                            update2.AddWhere("AccountId", accountid);
                            update2.ExecuteNonQuery();
                        }
                        else
                        {
                            update.AddFieldValue("isFinish", "0");
                        }
                        update.AddWhere("Id", accountid);
                        update.ExecuteNonQuery();

                        update = new UpdateSQL("FormContent");
                        update.DataBaseAlias = "common";
                        update.AddFieldValue("status", status);
                        //update.AddFieldValue("zd_detail", detail.Value.ToString());
                        update.AddWhere("info_id", accountid);
                        update.ExecuteNonQuery();
                    }
                    #endregion
                    #region 提前认定状态流程
                    else        
                    {
                        IUpdateDataSourceFace update = new UpdateSQL("Account");
                        update.DataBaseAlias = "common";
                        if (status != "late_pay")
                        {
                            update.AddFieldValue("Account_Status", status);
                            update.AddFieldValue("StatusName", new BLL.BasePage().GetDicName(status));
                            if (list.Contains(status))
                            {
                                update.AddFieldValue("isFinish", "1");
                                IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                                update1.DataBaseAlias = "common";
                                update1.AddFieldValue("FinishTime", DateTime.Now);
                                update1.AddWhere("AccountId", accountid);
                                update1.ExecuteNonQuery();

                                ///设置备注--提前完成的项目
                                string key_word = string.Empty;
                                ISelectDataSourceFace select = new SelectSQL();
                                select.DataBaseAlias = "common";
                                select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,FinishTime) as late_day from View_Account  where Id = '" + accountid + "'";
                                DataTable dt = select.ExecuteDataSet().Tables[0];
                                if (dt.Rows[0]["LateSign"].ToString() != "True")
                                {
                                    int tday = Math.Abs(Convert.ToInt32(dt.Rows[0]["late_day"].ToString()));
                                    key_word = dt.Rows[0]["Remark"].ToString();
                                    string keystatus = "提前完成，提前" + tday + "天";
                                    if (tday == 0)
                                    {
                                        keystatus = "准时完成";
                                    }
                                    key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为" + keystatus;
                                    IUpdateDataSourceFace update2 = new UpdateSQL("Account_Schedule");
                                    update2.DataBaseAlias = "common";
                                    update2.AddFieldValue("Remark", key_word);
                                    update2.AddWhere("AccountId", accountid);
                                    update2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                update.AddFieldValue("isFinish", "0");
                            }
                            update.AddWhere("Id", accountid);
                            update.ExecuteNonQuery();

                            update = new UpdateSQL("FormContent");
                            update.DataBaseAlias = "common";
                            //update.AddFieldValue("zd_detail", detail.Value.ToString());
                            update.AddFieldValue("status", status);
                            update.AddWhere("info_id", accountid);
                            update.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    bool flag = true;
                    string msg = string.Empty;
                    if (status == "late_pay")
                    {
                        flag = SetLate(accountid, dr["dq_day"].ToString());
                    }
                    if (list.Contains(status))
                    {
                        string PayRemark = string.Empty;
                        string PayId = string.Empty;
                        if (status == "already_repay")
                        {
                            PayRemark = "账单<a href='AccountDetail.aspx?id=" + accountid + "&tempid=5'><font color='red'>" + dr["zd_name"].ToString() + "</font></a>还款";
                            UpdateCash("cash", dr["ds_money"].ToString(), "-", userid, PayRemark, accountid, 0,"normal");

                        }
                        else if (status == "bad_pay")
                        {

                        }
                        else
                        {
                            PayRemark = "账单<a href='AccountDetail.aspx?id=" + accountid + "&tempid=5'><font color='green'>" + dr["zd_name"].ToString() + "</font></a>收款";
                            UpdateCash("cash", dr["ds_money"].ToString(), "+", userid, PayRemark, accountid, 0, "normal");
                        }
                    }
                    if (flag != false)
                    {
                        msg = "true";
                    }
                    else
                    {
                        msg = "未超过账单到期时间，无法设置为延期！";
                    }
                    return msg;
                }
                else   //账单已完成更新备注
                {
                    IUpdateDataSourceFace update = new UpdateSQL("Account_Schedule");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("Remark", remark);
                    update.AddWhere("AccountId", accountid);
                    update.ExecuteNonQuery();
                    return "更新备注成功！";
                }
            }
            catch(Exception ex)
            {
                return "false";
            }

        }


        public string Update_UserRemark(string dealid, string userid, string remark)
        {
            try
            {
                IUpdateDataSourceFace update = new UpdateSQL("History_Money");
                update.DataBaseAlias = "common";
                update.AddFieldValue("UserRemark", remark);
                update.AddWhere("id",dealid);
                update.AddWhere("Creator", userid);
                if(update.ExecuteNonQuery()>0)
                {
                    return "true";
                }
                return "操作失败，该可能不属于你或已被删除";
            }
            catch(Exception e){
                return "操作过程中发生错误";
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetDropDownList(string Key_Word,string IsDelete)
        {
            string Key_Word2 = string.Empty;
            string Key_Word3 = string.Empty;
            string Key_Word4 = string.Empty;
            switch (Key_Word)
            {
                case "wait_receive":
                    Key_Word2 = "already_getmoney";
                    Key_Word3 = "late_pay";
                    Key_Word4 = "bad_pay";
                    break;
                case "wait_repay":
                    Key_Word2 = "already_repay";
                    break;
                case "wait_deposit":
                    Key_Word2 = "already_deposit";
                    break;
                case "late_pay":
                    Key_Word2 = "already_getmoney";
                    Key_Word4 = "bad_pay";
                    break;
            }
            //绑定账单状态
            string ddliststring = string.Format(@"
                <tr>
                    <td style='width:60%;padding-top:8px' >
                        <b style='float:left;'>状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：</b>
                        <div style='float:left;' class='am-form-group'>");
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select DicName,DicCode from Dictionary where DicCode = '" + Key_Word + "' or DicCode = '" + Key_Word2 + "' or DicCode = '" + Key_Word3 + "'or DicCode = '" + Key_Word4 + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            string tempword = "<select ID='ddlIndustry' runat='server'>";
            if (dt.Rows.Count == 1 || IsDelete=="True")
            {
                tempword = @"<select ID='ddlIndustry' runat='server' disabled='disabled'>";
            }
            ddliststring += tempword;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["DicCode"].ToString() == Key_Word)
                {
                    ddliststring += string.Format(@"<option value='{0}' selected>{1}</option>", dt.Rows[i]["DicCode"].ToString(), dt.Rows[i]["DicName"].ToString());
                }
                else{
                    ddliststring += string.Format(@"<option value='{0}'>{1}</option>",dt.Rows[i]["DicCode"].ToString(),dt.Rows[i]["DicName"].ToString());
                }
            }
            return ddliststring += "</select></div></td>";
        }

        public bool IsFinish_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select isFinish from Account where Id ='" + id.ToString() + "'");
            select.CommandText = sql;
            string _isfinish = select.ExecuteDataSet().Tables[0].Rows[0]["isFinish"].ToString();
            if (_isfinish == "True")
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        protected int IsExist_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select id  from Account_Schedule where AccountId ='" + id.ToString() + "'");
            select.CommandText = sql;
            int temp_num = select.ExecuteDataSet().Tables[0].Rows.Count;
            return temp_num;
        }

        public DataRow GetAccountInfo(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select 
                                                        zd_name,
                                                        CreateTime,
                                                        DATEDIFF(day,GETDATE(),dq_time) as dq_day,
                                                        DATEDIFF(day,dq_time,GETDATE()) as daycount,
                                                        ds_money,
                                                        tz_money,
                                                        Account_Status
                                                        from View_Account where Id={0}", id);
            return select.ExecuteDataSet().Tables[0].Rows[0];
        }

        /// <summary>
        /// 设置延期记录
        /// </summary>
        /// <param name="id"></param>
        public bool SetLate(string id, string day)
        {
            string key_word = string.Empty;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,GETDATE()) as daycount from View_Account where AccountId = '" + id + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (Convert.ToInt32(dt.Rows[0]["daycount"].ToString()) <= 0)
            {
                return false;
            }
            else
            {
                if (dt.Rows[0]["LateSign"].ToString() != "True")
                {
                    key_word = dt.Rows[0]["Remark"].ToString();
                    key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为延期，已延期" + Math.Abs(Convert.ToInt32(day)) + "天";
                    IUpdateDataSourceFace update = new UpdateSQL("Account_Schedule");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("Remark", key_word);
                    update.AddFieldValue("LateSign", "1");
                    update.AddWhere("AccountId", id);
                    update.ExecuteNonQuery();
                }
                return true;
            }
        }

        public bool UpdateCash(string Key, string addmoney, string sign, string userid, string PayRemark, string PayId, int type, string worktips)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", Key, sign, addmoney, userid);
                select.ExecuteDataSet();
                if (Key == "cash" && worktips == "special")
                {
                    select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.CommandText = string.Format(@"update Com_UserInfos set {0}={0}{1}Abs('{2}') where Userid = '{3}'", "total_money", sign, addmoney, userid);
                    select.ExecuteDataSet();
                }
                select.CommandText = string.Format(@"select cash,total_money from Com_UserInfos where Userid='{0}'", userid);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if (type == 0)
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

        public int GetAccountNum(string where)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"
                                                select count(*) 
	                                            FROM View_Account {0}
                                                ", where);
            return select.ExecuteCount();
        }

        //获取总条数
        public int GetNum(string where,string TableName)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"
                                                select count(*) 
	                                            FROM {1} {0}
                                                ", where,TableName);
            return select.ExecuteCount();
        }

        public bool Update_Cash(string userid,decimal d_value)
        {
            try
            {
                ISelectDataSourceFace update = new SelectSQL();
                update.DataBaseAlias = "common";
                update.CommandText = string.Format(@"Update Com_UserInfos set cash=cash+{0},total_money=total_money+{0} where userid ='{1}'", d_value, userid);
                update.ExecuteDataSet();

                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = "select cash,total_money from Com_UserInfos where userid ='" + userid + "'";
                DataRow dr = select.ExecuteDataSet().Tables[0].Rows[0];
                SetHisMoney(userid, dr["cash"].ToString(), dr["total_money"].ToString(), d_value.ToString(), "现金", "用户手动更正金额");

                
                return true;
            }
            catch(Exception e){
                return false;
            }

            

            
        }

        private void SetHisMoney(string userid, string _cash, string _total, string D_value, string type, string PayRemark)
        {
            IInsertDataSourceFace insert = new InsertSQL("History_Money");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("Creator", userid);
            insert.AddFieldValue("CreateTime", DateTime.Now);
            insert.AddFieldValue("Cash", _cash);
            insert.AddFieldValue("TotalMoney", _total);
            insert.AddFieldValue("D_value", D_value);
            insert.AddFieldValue("type", type);
            insert.AddFieldValue("PayRemark", PayRemark);
            insert.ExecuteNonQuery();
        }

        private string SaveAccount(string type, string ac_name, string tz_money, string dq_day, string zd_detail, string tz_people, string ds_money, string status,string action,string userid,string id)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(action) || action == "mbadd")
                {
                    IInsertDataSourceFace insert = new InsertSQL("Account");
                    insert.DataBaseAlias = "common";
                    insert.Transaction = tran;
                    insert.AddFieldValue("TempId", type);
                    insert.AddFieldValue("IsDelete", 0);
                    insert.AddFieldValue("Creator", userid);
                    insert.AddFieldValue("CreateTime", DateTime.Now);
                    insert.ExecuteNonQuery();


                    ISelectDataSourceFace select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.Transaction = tran;
                    select.CommandText = "SELECT @@IDENTITY";
                    object obj = select.ExecuteScalar();

                    insert = new InsertSQL("FormContent");
                    insert.DataBaseAlias = "common";
                    insert.Transaction = tran;
                    insert.AddFieldValue("info_id", obj);
                    insert.AddFieldValue("zd_name",ac_name);
                    insert.AddFieldValue("tz_money",tz_money);
                    insert.AddFieldValue("dq_time",dq_day);
                    insert.AddFieldValue("zd_detail",zd_detail);
                    insert.AddFieldValue("tz_people",tz_people);
                    insert.AddFieldValue("ds_money",ds_money);
                    insert.AddFieldValue("status",status);
                    insert.ExecuteNonQuery();

                    string[] flag = { "already_getmoney", "already_repay", "already_deposit", "bad_pay" };
                    List<string> list = new List<string>(flag);
                    IUpdateDataSourceFace update = new UpdateSQL("Account");
                    update.DataBaseAlias = "common";
                    update.Transaction = tran;
                    update.AddFieldValue("Account_Status", status);
                    update.AddFieldValue("StatusName", GetDicName(status));
                    if (list.Contains(status))
                    {
                        update.AddFieldValue("isFinish", "1");
                    }
                    else
                    {
                        update.AddFieldValue("isFinish", "0");
                    }
                    update.AddWhere("Id", obj);
                    update.ExecuteNonQuery();


                    //获取最新插入的这条数据
                    string calculatorflag = status == "wait_receive" || status == "wait_deposit" || status == "late_pay" ? "+" : (status == "wait_repay" ? "-" : "");
                    if (calculatorflag != "")
                    {
                        DataTable dt = GetNewestAccount(obj.ToString(), tran);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dt.Rows[0]["tz_money"].ToString()) != 0)
                            {
                                UpdateCash("cash", dt.Rows[0]["tz_money"].ToString(), calculatorflag == "+" ? "-" : "+", userid, "账单<a href='AccountDetail.aspx?id=" + dt.Rows[0]["id"].ToString() + "&tempid=" + GetTempId(dt.Rows[0]["id"].ToString(), tran) + "'><font color='red'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>" + (status == "wait_repay" ? "预支收入" : "投资扣款"), dt.Rows[0]["id"].ToString(), 0, "special");
                            }
                            UpdateCash("total_money", dt.Rows[0]["ds_money"].ToString(), calculatorflag, userid, "账单<a href='AccountDetail.aspx?id=" + dt.Rows[0]["id"].ToString() + "&tempid=5'><font color='green'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>录入", dt.Rows[0]["id"].ToString(), 1, "normal");
                        }
                    }
                    tran.Commit();
                    return "添加成功";
                }
                else
                {
                    DataTable dt = GetAInfo(id, tran);
                    string dstatus = dt.Rows[0]["status"].ToString();
                    string old_ds_money = dt.Rows[0]["ds_money"].ToString();
                    string old_tz_money = dt.Rows[0]["tz_money"].ToString();
                    decimal d_value_ds = 0;
                    decimal d_value_tz = 0;
                    if (dstatus == "wait_repay")
                    {
                        d_value_ds = Convert.ToDecimal(old_ds_money) - Convert.ToDecimal(ds_money);
                        d_value_tz = Convert.ToDecimal(tz_money) - Convert.ToDecimal(old_tz_money);
                    }
                    else
                    {
                        d_value_ds = Convert.ToDecimal(ds_money) - Convert.ToDecimal(old_ds_money);
                        d_value_tz = Convert.ToDecimal(old_tz_money) - Convert.ToDecimal(tz_money);
                    }
                    if (d_value_ds != 0)
                    {
                        UpdateCash("total_money", d_value_ds.ToString(), d_value_ds > 0 ? "+" : "-", userid, "账单<a href='AccountDetail.aspx?id=" + id + "&tempid=" + GetTempId(id, tran) + "'><font color='#2222DD'>" + ac_name + "</font></a>修改" + (status == "wait_repay" ? "待还" : "待收") + "金额", id, 1, "normal");
                    }
                    if(d_value_tz != 0)
                    {
                        UpdateCash("cash", d_value_tz.ToString(), d_value_tz > 0 ? "+" : "-", userid, "账单<a href='AccountDetail.aspx?id=" + id + "&tempid=" + GetTempId(id, tran) + "'><font color='#2222DD'>" + ac_name + "</font></a>修改" + (status == "wait_repay" ? "借款" : "投资") + "金额", id, 0, "special");
                    }

                    IUpdateDataSourceFace update = new UpdateSQL("FormContent");
                    update.DataBaseAlias = "common";
                    update.Transaction = tran;
                    update.AddFieldValue("zd_name", ac_name);
                    update.AddFieldValue("tz_money", tz_money);
                    update.AddFieldValue("dq_time", dq_day);
                    update.AddFieldValue("zd_detail", zd_detail);
                    update.AddFieldValue("tz_people", tz_people);
                    update.AddFieldValue("ds_money", ds_money);
                    update.AddWhere("info_id", id);
                    update.ExecuteNonQuery();
                    tran.Commit();
                    return "修改成功";
                }
                
            }
            catch (Exception ex)
            {
                tran.Rollback();
                msg = "false";
            }
            return msg;
        }

        private string SaveReminder_Mobile(string action, string r_title, string r_detail, string remindtime, string userid, string RemindMail,string id)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string msg = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(action) || action == "mbadd")
                {
                    IInsertDataSourceFace insert = new InsertSQL("Reminder");
                    insert.DataBaseAlias = "common";
                    insert.Transaction = tran;
                    insert.AddFieldValue("title", r_title);
                    insert.AddFieldValue("content", r_detail);
                    insert.AddFieldValue("RemindMail", RemindMail);
                    insert.AddFieldValue("Creator",userid);
                    insert.AddFieldValue("RemindTime", remindtime);
                    insert.AddFieldValue("CreateTime", DateTime.Now);
                    insert.AddFieldValue("Status","wait_remind");
                    insert.ExecuteNonQuery();

                    tran.Commit();
                    return "添加成功";
                }
                else
                {
                    IUpdateDataSourceFace update = new UpdateSQL("Reminder");
                    update.DataBaseAlias = "common";
                    update.Transaction = tran;
                    update.AddFieldValue("title", r_title);
                    update.AddFieldValue("content", r_detail);
                    update.AddFieldValue("RemindMail", RemindMail);
                    update.AddFieldValue("RemindTime", remindtime);
                    update.AddWhere("id", id);
                    update.ExecuteNonQuery();
                    tran.Commit();

                    CheckMailExist(id);

                    return "修改成功";
                }

            }
            catch (Exception ex)
            {
                tran.Rollback();
                msg = "false";
            }
            return msg;
        }

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

        public DataTable GetNewestAccount(string infoid, IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = string.Format(@"select top 1 id,zd_name,ds_money,tz_money from View_Account where info_id='{0}'", infoid);
            return select.ExecuteDataSet().Tables[0];
        }

        public DataTable GetAInfo(string id, IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = string.Format(@"select ds_money,tz_money,status from View_Account where info_id='{0}'", id);
            return select.ExecuteDataSet().Tables[0];
        }

        public int GetTempId(string id, IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = string.Format(@"select tempid from Account where id='{0}'", id);
            return Convert.ToInt32(select.ExecuteDataSet().Tables[0].Rows[0]["tempid"].ToString());
        }

        public string DeleteAccount(string id,string userid)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string msg = string.Empty;
            try
            {
                if(CheckDelete(id))
                {
                    IUpdateDataSourceFace delete = new UpdateSQL("Account");
                    delete.DataBaseAlias = "common";
                    delete.Transaction = tran;
                    delete.AddFieldValue("IsDelete", "1");
                    delete.AddWhere("Creator", userid);
                    delete.AddWhere("Id", id);
                    if (delete.ExecuteNonQuery() > 0)
                    {
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.Transaction = tran;
                        select.CommandText = string.Format(@"select zd_name,Account_Status,ds_money,tz_money from View_Account where id='{0}'", id);
                        DataTable dt = select.ExecuteDataSet().Tables[0];
                        string zd_status = dt.Rows[0]["Account_Status"].ToString();
                        string calculatorflag = zd_status == "wait_receive" || zd_status == "wait_deposit" || zd_status == "late_pay" ? "-" : (zd_status == "wait_repay" ? "+" : "");
                        if (calculatorflag != "")
                        {
                            UpdateCash("total_money", dt.Rows[0]["ds_money"].ToString(), calculatorflag, userid, "账单<a href='AccountDetail.aspx?id=" + id + "&tempid=5'><font color='red'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>删除", id, 1, "normal");
                            UpdateCash("cash", dt.Rows[0]["tz_money"].ToString(), calculatorflag == "+" ? "-" : "+", userid, "账单<a href='AccountDetail.aspx?id=" + id + "&tempid=5'><font color='red'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>删除回退", id, 0, "special");
                        }
                        msg = "true";
                    }
                    else
                    {
                        msg = "删除失败，该账单不存在或不属于你";
                    }
                }
                else
                {
                    msg = "删除失败，该账单已被删除";
                }
                tran.Commit();
                return msg;
            }
            catch
            {
                tran.Rollback();
                return "false";
            }
        }

        public string DeleteDeal(string id, string userid)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string msg = string.Empty;
            try
            {
                    IDeleteDataSourceFace delete = new DeleteSQL("History_Money");
                    delete.DataBaseAlias = "common";
                    delete.Transaction = tran;
                    delete.AddWhere("Creator", userid);
                    delete.AddWhere("Id", id);
                    if (delete.ExecuteNonQuery() > 0)
                    {
                        msg = "true";
                    }
                    else
                    {
                        msg = "删除失败，该交易记录不存在或已被删除";
                    }
                tran.Commit();
                return msg;
            }
            catch
            {
                tran.Rollback();
                return "false";
            }
        }

        public string DeleteReminder(string id, string userid)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string msg = string.Empty;
            try
            {
                DeleteMail(id, tran);

                IDeleteDataSourceFace delete = new DeleteSQL("Reminder");
                delete.DataBaseAlias = "common";
                delete.Transaction = tran;
                delete.AddWhere("Creator", userid);
                delete.AddWhere("Id", id);
                if (delete.ExecuteNonQuery() > 0)
                {
                    msg = "true";
                }
                else
                {
                    msg = "删除失败，该提醒事项不存在或已被删除";
                }
                tran.Commit();
                return msg;
            }
            catch
            {
                tran.Rollback();
                return "false";
            }
        }

        protected void DeleteMail(string id, IDbTransaction tran)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.Transaction = tran;
            select.CommandText = "select Status from Reminder where id='" + id + "'";
            if (select.ExecuteDataSet().Tables[0].Rows[0]["Status"].ToString() == "wait_remind")
            {
                IDeleteDataSourceFace delete = new DeleteSQL("History_Mail");
                delete.DataBaseAlias = "common";
                delete.AddWhere("r_linkid", id);
                delete.Transaction = tran;
                delete.ExecuteNonQuery();
            }
        }

        public bool CheckDelete(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select IsDelete from Account where id='" + id + "'";
            if (select.ExecuteDataSet().Tables[0].Rows[0]["IsDelete"].ToString() == "True")
            {
                return false;
            }
            return true;
        }

        //电脑端方法
        public string SaveReminder(string type, string userid, string title, string content, string remindman, string RemindTime,string editid)
        {
            try
            {
                if(type == "add")
                {
                    IInsertDataSourceFace insert = new InsertSQL("Reminder");
                    insert.DataBaseAlias = "common";
                    insert.AddFieldValue("Creator", userid);
                    insert.AddFieldValue("RemindMail", remindman);
                    insert.AddFieldValue("title", title);
                    insert.AddFieldValue("content", content);
                    insert.AddFieldValue("CreateTime", DateTime.Now);
                    insert.AddFieldValue("Status", "wait_remind");
                    insert.AddFieldValue("RemindTime", RemindTime);
                    insert.ExecuteNonQuery();

                    return "add_true";
                }
                else //if(type == "edit")
                {
                    IUpdateDataSourceFace update = new UpdateSQL("Reminder");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("RemindMail", remindman);
                    update.AddFieldValue("title", title);
                    update.AddFieldValue("content", content);
                    update.AddFieldValue("RemindTime", RemindTime);
                    update.AddWhere("id", editid);
                    update.ExecuteNonQuery();

                    CheckMailExist(editid);
                    return "edit_true";
                }
            }
            catch
            {
                return "false";
            }
        }

        public void CheckMailExist(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select id from History_Mail where r_linkid='{0}'", id);
            if (select.ExecuteDataSet().Tables[0].Rows.Count > 0)
            {
                IDeleteDataSourceFace delete = new DeleteSQL("History_Mail");
                delete.DataBaseAlias = "common";
                delete.AddWhere("r_linkid", id);
                delete.ExecuteNonQuery();
            }
            IUpdateDataSourceFace update = new UpdateSQL("Reminder");
            update.DataBaseAlias = "common";
            update.AddFieldValue("IsRemind", "0");
            update.AddWhere("id", id);
            update.ExecuteNonQuery();
        }

        public string Get_HeadPic(string username)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"  select head_pic from Com_UserInfos a left join Com_UserLogin b on a.Userid=b.UserId where LoginName='{0}'", username);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["head_pic"].ToString();
            }
            else
                return "default.gif";
        }

        public string Account_Count(string hidfid, string userid)
        {
            try
            {
                string msg = string.Empty;
                string[] Ids = hidfid.Split(',');
                decimal ds_money = 0;   //待收金额
                decimal tz_money = 0;   //投资金额
                decimal dh_money = 0;   //待还金额
                foreach (string fId in Ids)
                {
                    DataRow dr = GetAccountInfo(fId);
                    tz_money += Math.Abs(Convert.ToDecimal(dr["tz_money"].ToString()));
                    switch (dr["Account_Status"].ToString())
                    {
                        case "wait_receive":
                            ds_money += Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()));
                            break;
                        case "wait_repay":
                            dh_money += Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()));
                            break;
                        case "late_pay":
                            ds_money += Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()));
                            break;
                        case "wait_deposit":
                            ds_money += Math.Abs(Convert.ToDecimal(dr["ds_money"].ToString()));
                            break;
                    }
                }
                msg = string.Format(@"<font style='font-weight:bold;'></font>投资金额<font color='orange'>{0}</font>元，待收金额<font color='orange'>{1}</font>元，待还金额<font color='orange'>{2}</font>元", tz_money.ToString("#0.00"), ds_money.ToString("#0.00"), dh_money.ToString("#0.00"));
                return msg;
            }
            catch
            {
                return "false";
            }
        }
    }
    
}