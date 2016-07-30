using Accounts.BLL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accounts.Web.moblie
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void login_Btn_Click(object sender, EventArgs e)
        {
            if (true)
            {
                LoginBll loginbll = new LoginBll();
                string flag = loginbll.ValidateUser(loginName_txt.Value, pwd_txt.Value);
                if (flag == "true")
                {
                    Response.Redirect("Default.aspx");
                }
                else if (flag == "false")
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
    }
}