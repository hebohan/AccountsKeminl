using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accounts.Web.mobile.ReminderManage
{
    public partial class ReminderDetail : BasePage
    {
        public static string Menu = string.Empty;
        public static string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                string content = string.Empty;
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("id")))
                {
                    try
                    {
                        hiddlid.Value = DTRequest.GetQueryString("id");
                        string id = DTRequest.GetQueryString("id");
                        hiduserid.Value = GetLoginUser().Userid;
                        Menu = GetMenu(hiduserid.Value, 1);
                        string editbtnsty = "";
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText = "select * from Reminder where id = '" + id + "' and Creator='" + hiduserid.Value + "'";
                        DataTable dt = select.ExecuteDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            if(dt.Rows[0]["Status"].ToString() == "already_remind")
                            {
                                editbtnsty = "style='display:none'";
                            }
                            content += "<tr><td><font style='font-weight:bold;'>提醒标题：</font>" + dt.Rows[0]["title"].ToString() + "</td></tr>";
                            content += "<tr><td><font style='font-weight:bold;'>提醒对象：</font>" + dt.Rows[0]["RemindMail"].ToString() + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>提醒内容：</font>" + dt.Rows[0]["content"].ToString() + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>提醒时间：</font><font style='font-weight:bold;' color='red'>" + String.Format("{0:yyyy年MM月dd日  HH 时 mm 分}", dt.Rows[0]["RemindTime"]) + "</font></td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>状态：</font><font style='font-weight:bold;' color='green'>" + new BasePage().GetDicName(dt.Rows[0]["Status"].ToString()) + "</font></td></tr>";

                            hidbtncontent.Value = string.Format(@"
                            <a href='ReminderRegisterDetail.aspx?id={0}&action=edit' {1}><input type='button' value='编辑提醒' class='btnEdit' /></a>
                            <a href='javascript:void(0);' onclick='DL_Delete();'><input type='button' value='删除提醒' class='btnDel' /></a>", id,editbtnsty);
                        }
                        else
                        {
                            content += "<td style='text-align:center'>该提醒事项不存在或不属于你</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        content += "<tr><td style='text-align:center'>该提醒事项不存在或已被删除！</td></tr>";
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
    }
}