using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// loginout 的摘要说明
    /// </summary>
    public class loginout : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Accounts.Model.View_Users viewUser = context.Session["User"] as Accounts.Model.View_Users;
            Accounts.BLL.Com_LoginLog lbll = new Accounts.BLL.Com_LoginLog();
            Accounts.Model.Com_LoginLog lmodel = new Accounts.Model.Com_LoginLog();
            lmodel.LoginDate = DateTime.Now;
            lmodel.LoginIP =  HttpContext.Current.Request.UserHostAddress;
            lmodel.Status = "1";
            lmodel.Userid = viewUser.Userid;
            lbll.Add(lmodel);
            context.Session["User"] = null;
            context.Response.Redirect("../Login.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}