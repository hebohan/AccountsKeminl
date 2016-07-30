using Accounts.BLL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Accounts.BLL;
using System.Web.UI.WebControls;
using Inspur.Finix.DAL.SQL;
using System.Data;

namespace Accounts.Web.moblie
{
    public partial class UpdatePass : BasePage
    {
        public static string Menu = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                Menu = GetMenu(GetLoginUser().Userid, 0);
            }
        }
    }
}