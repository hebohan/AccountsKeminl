using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using Accounts.Model;

namespace Accounts.Web
{
    public partial class UpdatePassWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (Session["User"] == null)
            {
                msg = string.Format("JsPrint('{0}','{1}')", "用户信息失效，请先登录！", "fail");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                return;
            }
            View_Users user = Session["User"] as View_Users;
            string old = Accounts.DBUtility.DESEncrypt.Encrypt(oldpassword.Value);
            if (old != user.LoginPassword)
            {
                msg = string.Format("JsPrint('{0}','{1}')", "旧密码输入错误！", "fail");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
                return;
            }
            string newpsw = Accounts.DBUtility.DESEncrypt.Encrypt(userpassword.Value);
            IUpdateDataSourceFace update = new UpdateSQL();
            update.DataBaseAlias = "common";
            update.CommandText =
                string.Format("UPDATE dbo.Com_UserLogin SET LoginPassword='{0}' WHERE UserId='{1}'", newpsw, user.Userid);
            int result = update.ExecuteNonQuery();
            if (result > 0)
            {
                msg = string.Format("JsPrint('{0}','{1}')", "修改密码成功！", "success");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }
    }
}