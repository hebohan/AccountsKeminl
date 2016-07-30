using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Accounts.DBUtility;
using System.Data;
using Accounts.Model;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// UserListHandler 的摘要说明
    /// </summary>
    public class UserListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int row = int.Parse(context.Request["rows"].ToString());
            int page = int.Parse(context.Request["page"].ToString());
            string strWhere = "";
            Accounts.BLL.View_Users bll = new Accounts.BLL.View_Users();
            List<Accounts.Model.View_Users> list = bll.GetList(row, page, strWhere, " Sort asc");
            StringBuilder sb = new StringBuilder();
            if (list.Count == 0)
            {
                sb.Append("{\"total\":0,\"rows\":[]}");
            }
            else
            {
                int count = bll.Selcount("");
                sb.Append("{\"total\":" + count + ",\"rows\":[");
                int i = 1;
                foreach (Accounts.Model.View_Users model in list)
                {

                    sb.Append("{\"Userid\":\"" + model.Userid + "\",");
                    sb.Append("\"LoginName\":\"" + model.LoginName + "\",");
                    sb.Append("\"UserRealName\":\"" + model.UserRealName + "\",");
                    sb.Append("\"Pass\":\"" + model.LoginPassword + "\",");
                    sb.Append("\"Mail\":\"" + model.Email + "\",");
                    sb.Append("\"Mobile\":\"" + model.Mobile + "\",");
                    sb.Append("\"Tel\":\"" + model.Tel + "\",");
                    sb.Append("\"Sort\":\"" + i + "\",");
                    sb.Append("\"Sex\":\"" + (model.Sex.ToString() =="1"?"男":"女")  + "\",");
                    sb.Append("\"Status\":\"" + model.Status + "\",");
                    sb.Append("\"Email\":\"" + model.Email + "\",");
                    sb.Append("\"Roles\":\"" + (model.Rolesid.ToString() == "1" ? "管理员" : "普通用户") + "\",");
                    if (model.Status == 0)
                        sb.Append("\"Status\":\"禁用\"");
                    else if (model.Status == 1)
                        sb.Append("\"Status\":\"正常\"");
                    else
                        sb.Append("\"Status\":\"禁止登录\"");
                    sb.Append("},");
                    i++;
                }
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