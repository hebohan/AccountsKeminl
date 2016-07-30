using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using work.MD5Hash;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// editpassword 的摘要说明
    /// </summary>
    public class editpassword : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string pass=context.Request.QueryString["pass"];
            Accounts.Model.View_Users viewUser = context.Session["User"] as Accounts.Model.View_Users;
            Accounts.BLL.Com_UserLogin bll = new Accounts.BLL.Com_UserLogin();
            Accounts.Model.Com_UserLogin model = bll.GetModel(viewUser.Userid);
            model.LoginPassword = MD5up.MD5(pass);
            if (bll.Update(model))
            {
                context.Response.Write("true");
            }
            else {
                context.Response.Write("false");
            }
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