using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accounts.BLL;
using Accounts.BLL.Login;
using Accounts.BLL.Common;

namespace Accounts.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(!string.IsNullOrEmpty(DTRequest.GetQueryString("type")))
                {
                    hidtype.Value = DTRequest.GetQueryString("type");
                }
                Session.Abandon();//清除所有session信息
            }
        }

        protected void login_Btn_Click(object sender, EventArgs e)
        {
            if (true)
            {
                LoginBll loginbll = new LoginBll();
                string flag = loginbll.ValidateUser(loginName_txt.Value.Trim(), pwd_txt.Value.Trim());
                if (flag == "true")
                {
                    Response.Redirect("Default.aspx");
                }
                else if(flag == "false")
                {
                    string msg = string.Format("JsPrint('{0}','{1}')", "登录失败，用户名或密码错误", "fail");
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                }
                else if (flag == "unable")
                {
                    string msg = string.Format("JsPrint('{0}','{1}')", "登录失败，帐号未启用，请联系管理员", "fail");
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                }
                else if (flag == "forbidden")
                {
                    string msg = string.Format("JsPrint('{0}','{1}')", "登录失败，帐号禁止登录，请联系管理员", "fail");
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                }
            }

        }

        ///// <summary>
        ///// 验证码验证
        ///// </summary>
        ///// <returns></returns>
        //bool DataValidate()
        //{
        //    bool result = true;
        //    //string verifyCode = HttpContext.Current.Session["CheckCode"].ToString();
        //    var httpCookie = HttpContext.Current.Request.Cookies["CheckCode"];
        //    if (httpCookie != null)
        //    {
        //        string verifyCode = httpCookie.Value;
        //        if (string.IsNullOrEmpty(verifyCode))
        //        {
        //            string msg = string.Format("JsPrint('{0}','{1}')", "验证码读取失败，请联系系统管理员", "fail");
        //            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        //            return false;
        //        }

        //        switch (System.String.Compare(verifyCode, verifyCode_txt.Text.Trim(), System.StringComparison.OrdinalIgnoreCase))
        //        {
        //            case 0:
        //                break;
        //            default:
        //                string msg = string.Format("JsPrint('{0}','{1}')", "验证码填写错误!", "fail");
        //                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        //                result = false;
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        string msg = string.Format("JsPrint('{0}','{1}')", "验证码读取失败，请联系系统管理员", "fail");
        //        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        //        return false;
        //    }
        //    return result;
        //}
    }
}