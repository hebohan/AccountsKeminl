using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using Inspur.Finix.ExceptionManagement;
using FormField = Accounts.Model.FormField;

namespace Accounts.Web.AccountsManage
{
    public partial class AccountDetail : BasePage
    {
        public int id = 0;
        public int tempid = 0;
        protected static string field_tab_content = "";
        protected static string iframe_content = "";
        protected void Page_Load(object sender, EventArgs e)
        {
                id = DTRequest.GetQueryInt("id");
                tempid = DTRequest.GetQueryInt("tempid");
                tid.Value = tempid.ToString();
                if (!IsPostBack)
                {
                    if (id > 0)
                    {
                        aid.Value = id.ToString();
                        BindData();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<iframe");
                        sb.Append(" src=\"AccountsScheduleAudit.aspx?id=" + id + "&tempid=" + tempid + "\"");
                        sb.Append(" style='width:100%;height:500px'>");
                        sb.Append("</iframe>");
                        iframe_content = sb.ToString();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "参数不对或找不到账单！", "error"), true);
                    }
                }
            }

        #region 获取模板字段

        private List<FormField> GetTemplateFields()
        {
            List<Model.FormField> ls = new List<FormField>();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("AccountTemplate");
            select.SelectColumn("fields");
            select.AddWhere("id", tid.Value);
            object obj = select.ExecuteScalar();

            if (obj != null)
            {
                List<string> fields = obj.ToString().Split(',').ToList();
                if (fields.Count > 0)
                {
                    foreach (string field in fields)
                    {
                        ls.Add(new BLL.FormField().GetModel(int.Parse(field.Trim())));
                    }
                }
            }
            return ls;
        }

        #endregion

        private
        void BindData()
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format("Select * from View_Account where Id={0}", id);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string html = "";
//                html += @"<tr>
//                                        <th style='width:15%;'>账单编号</th>
//                                        <th style='width:25%;'>工作任务</th>
//                                        <th style='width:10%;'>责任领导</th>
//                                        <th style='width:10%;'>责任单位</th>
//                                        <th style='width:40%;'>时间节点</th>
//                                    <tr>";
                List<Model.FormField> ls = GetTemplateFields();
                //获取标头
                html += @"<tr>";
                foreach (var item in ls)
                {
                    switch (item.title)
                    {
                        case "项目详情":
                            html += "<th style='width:40%'>" + item.title + "</th>";
                            break;
                        case "项目名称":
                            html += "<th style='width:20%'>" + item.title + "</th>";
                            break;
                        default:
                            html += "<th >" + item.title + "</th>";
                            break;
                    }
                }
                html += "</tr>";
                //获取值
                html += @"<tr>";
                int i = 1;
                foreach (var item in ls)
                {   

                    string svalue = string.Empty;
                    try
                    {
                        if (item.control_type == "date")
                        {
                            svalue = string.Format("{0:yyyy-MM-dd}", dr[item.name]);
                        }
                        else if (item.control_type == "datetime")
                        {
                            svalue = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dr[item.name]);
                        }
                        else
                        {
                            svalue = dr[item.name].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Handle(ex);
                    }
                    if(i<ls.Count)
                    {
                        html += "<td style='word-wrap:break-word;'>" + svalue + "</td>";
                    }
                    else
                    {
                        html += "<td style='word-wrap:break-word;'>" + GetDicName(svalue) + "</td>";
                    }
                    i++;
                }
                html += "</tr>";
                //html += "<tr>";
                ////项目
                //html += "<td>";
                //html += dr["project_type"];
                //html += "</td>";
                //账单编号
                //html += "<td>";
                //html += dr["tzbh"];
                //html += "</td>";
                ////工作任务
                //html += "<td>";
                //html += dr["work_task"];
                //html += "</td>";
                ////责任领导
                //html += "<td>";
                //html += dr["resp_leader"];
                //html += "</td>";
                ////责任单位
                //html += "<td>";
                //html += dr["resp_dept"];
                //html += "</td>";
                ////时间节点
                //html += "<td>";
                //html += dr["sjjd"];
                //html += "</td>";

                html += "</tr>";
                field_tab_content = html;

                //select = new SelectSQL();
                //select.DataBaseAlias = "common";
                //select.CommandText = "select * from Account_Schedule where AccountId=" +
                //                     id;
                //DataTable sdt = select.ExecuteDataSet().Tables[0];

            }
            else
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "账单不存在!", "error");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }


        public string  return_id()
        {
            string id = id = DTRequest.GetQueryString("id");
            string tempid = DTRequest.GetQueryString("tempid");
            return "?id=" + id + "&tempid=" + tempid;
        }

    }
}