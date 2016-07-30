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

namespace Accounts.Web.mobile.AccountsManage
{
    public partial class AccountsList : BasePage
    {
        public static string Menu = string.Empty;
        public static string userid = string.Empty;
        public static int CountNum = 0; //总记录数
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                BindControl();
                hiduserid.Value = GetLoginUser().Userid;
                Menu = GetMenu(hiduserid.Value, 1);
                if (!string.IsNullOrEmpty(DTRequest.GetString("status")))
                {
                    string status = DTRequest.GetString("status");
                    switch (status)
                    {
                        case "dq":  //30天内到期
                            ddlStatus.SelectedIndex = 8;
                            break;
                        case "wrc": //待收款
                            ddlStatus.SelectedIndex = 1;
                            break;
                        case "wrp": //待还款
                            ddlStatus.SelectedIndex = 2;
                            break;
                        case "late":    //延期
                            ddlStatus.SelectedIndex = 4;
                            break;
                        case "finish":  //已完成
                            ddlStatus.SelectedIndex = 9;
                            break;
                    }
                    
                }
                
            }
        }

        public string Where
        {
            get
            {
                string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId ='5' and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30", GetLoginUser().Userid);
                where += " and isFinish=0";
                return where;
            }
        }

        private void BindControl()
        {
            //绑定账单类型
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select id,name from AccountTemplate where is_account=1 and (Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0)) order by sort_id";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlType.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
            }


            ddlStatus.Items.Clear();
            ddlStatus.Items.Add(new ListItem("未完成", "0"));
            ddlStatus.Items.Add(new ListItem("待收款", "1"));
            ddlStatus.Items.Add(new ListItem("待还款", "2"));
            ddlStatus.Items.Add(new ListItem("待存款", "3"));
            ddlStatus.Items.Add(new ListItem("延期中", "4"));
            ddlStatus.Items.Add(new ListItem("已完成", "9"));
            ddlStatus.Items.Add(new ListItem("账单坏账", "6"));
            ddlStatus.Items.Add(new ListItem("本周到期", "5"));
            ddlStatus.Items.Add(new ListItem("30天内到期", "8"));
            ddlStatus.Items.Add(new ListItem("全部", "7"));
        }
    }
}