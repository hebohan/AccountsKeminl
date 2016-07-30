using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Text;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// GetButton 的摘要说明
    /// </summary>
    public class GetButton : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string pageName = context.Request.QueryString["pageName"];
            Accounts.Model.View_Users item = context.Session["User"] as Accounts.Model.View_Users;
            object NagId = Accounts.DBUtility.DbHelperSQL.GetSingle("select Id from Tb_Navigation where LinkAddress like'%" + pageName + "%'");
            DataSet ds = new DataSet();
            if (item.LoginName == "admin")
            {
                ds = Accounts.DBUtility.DbHelperSQL.Query("select ButtonName,BtnCode,Icon from Com_ButtonGroup where Id in(select ButtonId from Com_NavigationAndButton where NavigationId=" + NagId + ") order by sort");
            }
            else
            {
                 object RoleId = Accounts.DBUtility.DbHelperSQL.GetSingle("select RolesId from Tb_RolesAddUser where UserId='" + item.Userid + "'");
                 ds = Accounts.DBUtility.DbHelperSQL.Query("select ButtonName,BtnCode,Icon from Com_ButtonGroup where Id in(select ButtonId from Tb_RolesAndNavigation where RolesId="+RoleId+" and NavigationId="+NagId+") order by sort");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow dr in ds.Tables[0].Rows) {
                sb.Append("{\"ButtonName\":\"" + dr["ButtonName"] + "\",");
                sb.Append("\"BtnCode\":\"" + dr["BtnCode"] + "\",");
                sb.Append("\"Icon\":\"" + dr["Icon"] + "\"},");
            }
            sb.Remove(sb.Length-1,1);
            sb.Append("]");
            context.Response.Write(sb.ToString());
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