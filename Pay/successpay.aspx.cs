using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL.Common;

namespace Intermediary.Web.Pay
{
    public partial class successpay : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DTRequest.GetQueryString("return_url")))
            {
                hidurl.Value = DTRequest.GetQueryString("return_url");
            }
            
        }
    }
}