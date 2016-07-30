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

namespace Accounts.Web.moblie.AccountsManage
{
    public partial class AccountRegisterDetail : BasePage
    {
        public static string userid = string.Empty;
        public static string Menu = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                hidaction.Value = DTRequest.GetQueryString("action");
                hiduser.Value = GetLoginUser().Userid;
                Menu = GetMenu(hiduser.Value, 1);
                BindControl();
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

        public void BindControl()
        {
            //绑定账单类型
            ddlAcType.Items.Clear();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select id,name from AccountTemplate where is_account=1 and (Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0)) order by sort_id";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlAcType.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
            }

            //绑定账单状态
            ddlStatus.Items.Clear();
            select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select DicName,DicCode from Dictionary where ParentId = '80'  and Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0)";
            dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlStatus.Items.Add(new ListItem(dt.Rows[i]["DicName"].ToString(), dt.Rows[i]["DicCode"].ToString()));
            }
        }

        void BindData()
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format("Select * from View_Account where Id={0} and Creator='{1}'", hidid.Value, hiduser.Value);
                DataTable dt = select.ExecuteDataSet().Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.ddlAcType.Value = dr["tempid"].ToString();
                    this.ac_name.Value = dr["zd_name"].ToString();
                    this.tz_money.Value = dr["tz_money"].ToString();
                    this.sDate.Value = String.Format("{0:yyyy-MM-dd}", dr["dq_time"]);
                    this.zd_detail.Value = dr["zd_detail"].ToString();
                    this.tz_people.Value = dr["tz_people"].ToString();
                    this.ds_money.Value = dr["ds_money"].ToString();
                    
                    SaveBtn.Value = "立即修改";
                    if (hidaction.Value == "mbadd")
                    {
                        if (dr["status"].ToString() == "already_repay" || dr["status"].ToString() == "wait_repay")
                        {
                            this.ddlStatus.Value = "wait_repay";
                        }
                        else
                        {
                            this.ddlStatus.Value = "wait_receive";
                        }
                        SaveBtn.Value = "立即录入";
                    }
                    else
                    {
                        this.ddlStatus.Value = dr["status"].ToString();
                    }
                }
                else
                {
                    string msg = string.Format("JsPrint('{0}','{1}')", "账单不存在或不属于你", "close");
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "账单不存在或不属于你", "close");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }
    }
}