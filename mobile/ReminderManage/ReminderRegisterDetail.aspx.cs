using Accounts.BLL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inspur.Finix.DAL.SQL;
using System.Data;
using Accounts.BLL;
using Accounts.BLL.Common;

namespace Accounts.Web.moblie.ReminderManage
{
    public partial class ReminderRegisterDetail : BasePage
    {
        public static string userid = string.Empty;
        public static string Menu = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                hidaction.Value = DTRequest.GetQueryString("action");
                hiduser.Value = GetLoginUser().Userid;
                reminder.Value = GetLoginUser().Email;
                Menu = GetMenu(hiduser.Value, 1);
                //BindControl();
                if(!string.IsNullOrEmpty(DTRequest.GetQueryString("id")))
                {
                    hidid.Value = DTRequest.GetQueryString("id");
                    if (hidaction.Value == "edit" || hidaction.Value == "mbadd")
                    {
                        BindData();
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint",
                    string.Format("JsPrint('{0}','{1}')", "参数不对！", "error"), true);
            }

        }

        //public void BindControl()
        //{
        //    //绑定账单状态
        //    ddlStatus.Items.Clear();
        //    ISelectDataSourceFace select = new SelectSQL();
        //    select.DataBaseAlias = "common";
        //    select.CommandText = "select DicName,DicCode from Dictionary where ParentId = '98'  and Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0)";
        //    DataTable dt = select.ExecuteDataSet().Tables[0];
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        ddlStatus.Items.Add(new ListItem(dt.Rows[i]["DicName"].ToString(), dt.Rows[i]["DicCode"].ToString()));
        //    }
        //}

        void BindData()
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format("Select * from Reminder where Id={0} and Creator='{1}'", hidid.Value, hiduser.Value);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.reminder.Value = dr["RemindMail"].ToString();
                    this.r_title.Value = dr["title"].ToString();
                    this.r_detail.Value = dr["content"].ToString();
                    this.reminddate.Value = String.Format("{0:yyyy-MM-dd}", dr["RemindTime"]);
                    this.remindtime.Value = String.Format("{0:HH:mm}", dr["RemindTime"]);
                    SaveBtn.Value = "立即修改";
                }
                else
                {
                    string msg = string.Format("JsPrint('{0}','{1}')", "提醒不存在或不属于你", "close");
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "提醒不存在或不属于你", "close");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }
    }
}