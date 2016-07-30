using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Accounts.Web.ashx
{
    /// <summary>
    /// Authority 的摘要说明
    /// </summary>
    public class Authority : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.QueryString["type"] == "save")
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                string Autid = context.Request.QueryString["Autid"];
                Accounts.BLL.Com_NavigationAndButton bll = new Accounts.BLL.Com_NavigationAndButton();
                List<string> list = new List<string>();
                list.Add("delete from Com_NavigationAndButton where NavigationId="+Id);
                string[] str = Autid.Split(',');
                foreach (string BtnId in str) {
                    list.Add("insert into Com_NavigationAndButton(NavigationId,ButtonId)values("+Id+","+BtnId+")");   
                }
                Accounts.DBUtility.DbHelperSQL.ExecuteSqlTran(list);
            }
            else if (context.Request.QueryString["type"] == "auth") {
                string NavigaId = "0";
                if (context.Request["NavigaId"] != null)
                {
                    NavigaId = context.Request["NavigaId"].ToString();
                }
                DataSet ds=Accounts.DBUtility.DbHelperSQL.Query("select [ButtonId] from Com_NavigationAndButton where  NavigationId=" + NavigaId);
                string str = "";
                foreach (DataRow dr in ds.Tables[0].Rows) {
                    if (str != "")
                        str += ",";
                    str += dr["ButtonId"];
                }
                context.Response.Write(str);
            }
            else
            {
               
                // Accounts.BLL.Com_NavigationAndButton Nbll = new Accounts.BLL.Com_NavigationAndButton();
                // DataSet ds = Nbll.GetList(" NavigationId=" + NavigaId);
                Accounts.BLL.Com_ButtonGroup bll = new Accounts.BLL.Com_ButtonGroup();
                List<Accounts.Model.Com_ButtonGroup> list = bll.GetModelList(" 1=1 order by Sort");
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (Accounts.Model.Com_ButtonGroup item in list)
                {
                    sb.Append("{\"Id\":" + item.Id + ",");
                    sb.Append("\"ButtonName\":\"" + item.ButtonName + "\",");
                    sb.Append("\"BtnCode\":\"" + item.BtnCode + "\",");
                    sb.Append("\"Icon\":\"" + item.Icon + "\",");
                    sb.Append("\"Sort\":\"" + item.Sort + "\",");
                    sb.Append("\"Remark\":\"" + item.Remark + "\"");
                    //DataView dv = new DataView(ds.Tables[0]);
                    // dv.RowFilter = " ButtonId=" + item.Id;
                    //if (dv.Count > 0)
                    //  sb.Append(",\"checked\":true");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
                context.Response.Write(sb.ToString());
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