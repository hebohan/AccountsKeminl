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

namespace Accounts.Web.SysSetting
{
    public partial class NewMail : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                //检查权限
            }
        }

        //检查权限
        public bool CheckAuthority(string userid,string recordid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select id from History_Money where Creator='{0}' and id='{1}'", userid,recordid);
            if (select.ExecuteDataSet().Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}