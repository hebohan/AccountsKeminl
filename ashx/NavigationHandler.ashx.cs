using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accounts.BLL;
using Accounts.Model;
using System.Text;
using System.Data;
using System.Web.SessionState;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// NavigationHandler 的摘要说明
    /// </summary>
    public class NavigationHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            Accounts.BLL.Tb_Navigation bll = new Accounts.BLL.Tb_Navigation();
            List<Accounts.Model.Tb_Navigation> list = new List<Accounts.Model.Tb_Navigation>();
            Accounts.Model.View_Users item = context.Session["User"] as Accounts.Model.View_Users;
            string strWhere = null;
            if (item.UserRealName == "管理员")
            {
                strWhere = " IsShow=0";
            }
            else
            {
                strWhere = "Id in(select NavigationId from Tb_RolesAndNavigation where RolesId in(select RolesId from Tb_RolesAddUser where UserId='" + item.Userid + "')) and IsShow=0";
            }
            DataSet ds = bll.GetList(strWhere);
            sb.Append(" {\"menus\":[");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = " ParentId=0";
                dv.Sort = "Sort";
                for (int i = 0; i < dv.Count; i++)
                {
                    sb.Append("{\"menuid\":\"" + dv[i]["Id"] + "\",\"icon\":\"" + dv[i]["Icon"] + "\",\"menuname\":\"" + dv[i]["Pagelogo"] + "\",");
                    sb.Append("\"menus\":[");
                    //sb.Append("{" + dv[i]["Pagelogo"] + "," + dv[i]["Icon"] + ",");
                    //sb.Append("<ul>");
                    DataView dv2 = new DataView(ds.Tables[0]);
                    dv2.RowFilter = " ParentId=" + dv[i]["Id"];
                    dv2.Sort = " Sort";
                    for (int j = 0; j < dv2.Count; j++)
                    {
                        sb.Append("{\"menuid\":\"" + dv2[j]["Id"] + "\",\"menuname\":\"" + dv2[j]["Pagelogo"] + "\",\"icon\":\"" + dv2[j]["Icon"].ToString() + "\",\"url\":\"" + dv2[j]["LinkAddress"] + "\"},");
                        //sb.Append("<li><div><a ref=\"" + dv2[j]["Pagelogo"] + "\" href=\"javascript:void(0)\" rel=\"" + dv2[j]["LinkAddress"] + "\" ><span class=\"" + dv2[j]["Icon"].ToString() + "\" >&nbsp;</span><span class=\"nav\">" + dv2[j]["Pagelogo"] + "</span></a></div></li>");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]},");
                   // sb.Append("</ul>}");
                }
                //sb.Remove(0, 1);
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]}");
            }
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