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
    public partial class AccountDetail : BasePage
    {
        public static string userid = string.Empty;
        public static string Menu = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("id")) && !string.IsNullOrEmpty(DTRequest.GetQueryString("tempid")))
                {
                    hidaid.Value = DTRequest.GetQueryString("id");
                    hidtempid.Value = DTRequest.GetQueryString("tempid");
                    hiduserid.Value = GetLoginUser().Userid;
                    Menu = GetMenu(hiduserid.Value, 1);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint",
                        string.Format("JsPrint('{0}','{1}')", "参数不对！", "error"), true);
                }
            }
        }
    }
}