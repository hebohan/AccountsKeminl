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
    public partial class NewReminder : BasePage
    {
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                BindControl();
                userid = GetLoginUser().Userid;
                hiduserid.Value = userid;
                remindman.Value = GetMail(userid);
                if(!string.IsNullOrEmpty(DTRequest.GetQueryString("type")))
                {
                    hidtype.Value = DTRequest.GetQueryString("type");
                }
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("id")))
                {
                    hidremindid.Value = DTRequest.GetQueryString("id");
                }
                if (hidtype.Value == "edit" || hidtype.Value == "check")
                {
                    BindData();
                }
            }
        }

        private void BindControl()
        {
            //绑定小时
            hour.Items.Clear();
            for (int i = 0; i <= 23;i++ )
            {
                hour.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //绑定分钟
            minute.Items.Clear();
            for (int i = 0; i <= 59; i++)
            {
                minute.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        void BindData()
        {
            try
            {
                if (hidremindid.Value != "0")
                {
                    ISelectDataSourceFace select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.CommandText = string.Format("Select * from Reminder where Id={0} and Creator='{1}'", hidremindid.Value, hiduserid.Value);
                    DataTable dt = select.ExecuteDataSet().Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        this.remindman.Value = dr["RemindMail"].ToString();
                        this.title.Value = dr["title"].ToString();
                        this.content.Value = dr["content"].ToString();
                        this.Date.Value = String.Format("{0:yyyy-MM-dd}", dr["RemindTime"]);
                        this.hour.Value = String.Format("{0:HH}", dr["RemindTime"]);
                        this.minute.Value = String.Format("{0:mm}", dr["RemindTime"]);
                        
                        if(hidtype.Value == "edit")
                        {
                           btnSend.Value = "修改";
                        }
                        //else if(hidtype.Value == "check")
                        //{
                        //    this.remindman_label.InnerText = dr["RemindMail"].ToString();
                        //    this.remindman.Style.Add("display", "none");
                        //    this.remindman_label.Style.Add("display", "block");

                        //    this.title_label.InnerText = dr["title"].ToString();
                        //    this.title.Style.Add("display", "none");
                        //    this.title_label.Style.Add("display", "block");

                        //    this.remindtime_label.InnerText = String.Format("{0:yyyy-MM-dd HH 时 mm 分}", dr["RemindTime"]);
                        //    this.time_panel.Style.Add("display", "none");
                        //    this.remindtime_label.Style.Add("display", "block");

                        //    this.btnCancle.Value = "关闭";
                        //    this.btnpanel.Style.Add("padding-left","170px");
                        //    this.btnSend.Style.Add("display", "none");
                        //}
                       
                    }
                    else
                    {
                        string msg = string.Format("JsPrint('{0}','{1}')", "该提醒不存在或不属于你", "close");
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                    }
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "该提醒不存在或不属于你", "close");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }
            
    }
}