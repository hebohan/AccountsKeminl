using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using System.Net;
using work.MD5Hash;
using System.Configuration;
using Accounts.BLL.Common;
using Accounts.BLL;

namespace Accounts.Web.ReminderManage
{
    public partial class ReminderDetail : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                string content = string.Empty;
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("id")))
                {
                    try
                    {
                        content = "";
                        string id = DTRequest.GetQueryString("id");
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText = "select * from Reminder where id = '" + id + "' and Creator='" + GetLoginUser().Userid + "'";
                        DataTable dt = select.ExecuteDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            content += "<tr><td><font style='font-weight:bold;'>提醒标题：</font>" + dt.Rows[0]["title"].ToString() + "</td></tr>";
                            content += "<tr><td><font style='font-weight:bold;'>提醒对象：</font>" + dt.Rows[0]["RemindMail"].ToString() + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>提醒内容：</font>" + dt.Rows[0]["content"].ToString() + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>提醒时间：</font><font style='font-weight:bold;' color='red'>" + String.Format("{0:yyyy年MM月dd日  HH 时 mm 分}", dt.Rows[0]["RemindTime"]) + "</font></td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>状态：</font><font style='font-weight:bold;' color='green'>" + new BasePage().GetDicName(dt.Rows[0]["Status"].ToString()) + "</font></td></tr>";
                        }
                        else
                        {
                            content += "<td style='text-align:center'>该提醒事项不存在或不属于你</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        content += "<tr><td style='text-align:center'>系统错误！请稍后再试</td></tr>";
                        hidcontent.Value = content;
                    }

                }
                else
                {
                    content += "页面打开错误！";
                }
                hidcontent.Value = content;
            }
            
        }

        public string Getzd_name(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select zd_name from FormContent where info_id = '" + id + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if(dt.Rows.Count>0)
            {
                return dt.Rows[0]["zd_name"].ToString();
            }
            return "点此查看";
        }

        public DataTable GetZD_Detail(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select zd_detail,Remark from View_Account where info_id = '" + id + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public DataTable GetOrderDetail(string payid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select orderid,BussinessCode,payer from Order_Record where payid = '" + payid + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public string GetUserName(string BussinessCode)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select UserRealName from Com_UserInfos where paykeyMd5 = '" + BussinessCode + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserRealName"].ToString();
            }
            return "无记录";
        }

        public string GetPayerName(string username)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select UserRealName from View_Users where LoginName = '" + username + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserRealName"].ToString();
            }
            return "无记录";
        }

        public DataTable GetTodayMoney(string userid,string time)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select top 1 cash,total_money,CreateTime,ABS(datediff(d,CONVERT(varchar(100), CreateTime, 23),'{0}')) as flag from EveryDay_Money_Record where Userid = '{1}' order by flag asc", time, userid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }
    }
}