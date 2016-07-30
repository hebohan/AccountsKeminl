using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// treeHandler 的摘要说明
    /// </summary>
    public class treeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string UserId=context.Request.QueryString["Id"];
            Accounts.BLL.Tb_Roles bll = new Accounts.BLL.Tb_Roles();
            List<Accounts.Model.Tb_Roles> list = bll.GetModelList("");
            Accounts.BLL.Tb_RolesAddUser rbll = new Accounts.BLL.Tb_RolesAddUser();
            Accounts.Model.Tb_RolesAddUser model = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (Accounts.Model.Tb_Roles item in list)
            {
                model = rbll.GetModel(item.Id,UserId);
                sb.Append("{\"id\":"+item.Id+",");
                sb.Append("\"text\":\""+item.RolesName+"\"");
                if (model!=null)
                {
                    sb.Append(",\"checked\":true");
                }
                sb.Append("},");
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