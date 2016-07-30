using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// RoleList 的摘要说明
    /// </summary>
    public class RoleList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Accounts.BLL.Tb_Roles bll = new Accounts.BLL.Tb_Roles();
            List<Accounts.Model.Tb_Roles> list = bll.GetModelList("");
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (Accounts.Model.Tb_Roles item in list) {
                sb.Append("{\"Id\":"+item.Id+",");
                sb.Append("\"Name\":\"" + item.RolesName + "\",");
                sb.Append("\"Remark\":\"" + item.Remark + "\"},");
            }
            if (sb.Length > 1)
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