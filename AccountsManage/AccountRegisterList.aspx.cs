using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Accounts.BLL;
using Accounts.BLL.Common;
using Accounts.BLL.ToExcel;
using Inspur.Finix.DAL.SQL;

namespace Accounts.Web.AccountsManage
{
    public partial class AccountRegisterList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                    BindControl();
                    BindDataSource(1);
            }
        }

        private void BindControl()
        {
            //if (CheckAuthority("AccountRegisterList", "manage"))
            //{
            //    ddlADept.Items.Clear();
            //    ddlADept.DataSource = new Com_Organization().GetList(string.Format(" SupervisorId={0} ",new Com_Organization().GetModel(GetLoginUser().Userid).Id));
            //    ddlADept.DataTextField = "Agency";
            //    ddlADept.DataValueField = "Id";
            //    ddlADept.DataBind();
            //    ddlADept.Items.Insert(0, new ListItem("请选择", ""));
            //}
        }


        public string Where
        {
            get
            {
                string where = "where is_account=1 and (Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0))";
                if (!string.IsNullOrEmpty(name.Value))
                {
                    where += string.Format(" and name like '%{0}%'", name.Value.Trim());
                }
                return where;
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex)
        {
            this.ListData.DataSource = null;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format("SELECT id*1 as id,name,remark,CASE is_sys WHEN 1 THEN '是' ELSE '否' END is_sys,sort_id FROM AccountTemplate {0}",Where);
            select.CommandText = sql;
            select.AddOrderBy("sort_id", Sort.Ascending);
            DataSet ds = select.ExecuteDataSet(WebPager1.PageSize, pageIndex);
            this.ListData.DataSource = ds;
            this.ListData.DataBind();
            ListData.UseAccessibleHeader = true;
            if (ListData.Rows.Count > 0 && ListData.HeaderRow != null)
            {
                ListData.UseAccessibleHeader = true;
                ListData.HeaderRow.TableSection = TableRowSection.TableHeader;
                ListData.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            #region 得到总条目数
            select = new SelectSQL();
            select.DataBaseAlias = "common";
            sql = "select count(0) from AccountTemplate where is_account=1";
            select.CommandText = sql;
            int CountNum = select.ExecuteCount();
            this.WebPager1.RecordCount = CountNum;
            RecordCount.InnerText = string.Format("共{0}条", CountNum);
            if (WebPager1.PageCount <= 1)
            {
                RecordCount.Visible = false;
            }
            #endregion
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WebPager1_PageChanged(object sender, EventArgs e)
        {
            BindDataSource(WebPager1.CurrentPageIndex);
        }


        protected void btnSearch_OnServerClick(object sender, EventArgs e)
        {
            BindDataSource(1);
        }

        protected void Export_OnClick(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string id = btn.ToolTip.ToString();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("AccountTemplate");
            select.SelectColumns("name", "fields");
            select.AddWhere("id", int.Parse(id));
            DataTable obj = select.ExecuteDataSet().Tables[0];
            if (obj.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(obj.Rows[0]["fields"].ToString()))
                {
                    ISelectDataSourceFace nselect = new SelectSQL();
                    nselect.DataBaseAlias = "common";
                    nselect.CommandText = string.Format("Select name,title,sort_id,is_sys from FormField where id in ({0}) order by is_sys asc,sort_id asc", obj.Rows[0]["fields"]);
                    nselect.AddOrderBy("sort_id", Sort.Ascending);
                    DataTable dt = nselect.ExecuteDataSet().Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        List<string> heads = new List<string>();
                        heads.Add("序号");
                        DataTable ndt = new DataTable();
                        ndt.Columns.Add("xh");
                        foreach (DataRow dr in dt.Rows)
                        {
                            ndt.Columns.Add(dr["name"].ToString());
                            heads.Add(dr["title"].ToString() + "#" + dr["name"].ToString());
                        }
                        // ExcelHelper.ExportToExcel(ndt,fields.ToArray(),heads.ToArray(),string.Empty, obj.Rows[0]["name"].ToString());
                        DataSet ds = new DataSet();
                        ds.Merge(ndt);
                        string filename= obj.Rows[0]["name"].ToString() + ".xlsx";
                        string path = Server.MapPath("~") + "Excel\\Template\\" + filename;
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        if (ExcelHelper.CreateExcelFile(path, "", heads))
                        {
                            FileInfo file = new FileInfo(path);
                            Response.Clear();
                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.AddHeader("Content-Transfer-Encoding", "binary");
                            Response.ContentType = "application/octet-stream";
                            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                            Response.WriteFile(file.FullName);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "账单导出失败！", "fail"), true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "该账单模板未选择字段！", "fail"), true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "该账单模板未选择字段！", "fail"), true);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "不存在该账单模板！", "fail"), true);
            }
        }


        protected void btnImport_OnServerClick(object sender, EventArgs e)
        {
            string path = Server.MapPath("~");
            if (excelfile.HasFile)
            {
                string fe = System.IO.Path.GetExtension(excelfile.FileName).ToLower();
                if (fe != ".xls" && fe != ".xlsx")
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", "JsPrint('只能上传Excel文档！')",
                        true);
                }
                else
                {
                    //if (!string.IsNullOrEmpty(ddlADept.SelectedValue))
                    //{
                        string filename = DateTime.Now.ToString("yyyyMMddHHMMss") + fe;
                        path += "Excel\\Account\\" + filename;
                        excelfile.SaveAs(path);
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "impexcel",
                            "ImportExcel('" + filename + "','" + hidtempid.Value + "')",
                            true);
                    //}
                    //else
                    //{
                    //    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", "JsPrint('请选择账单单位！')",
                    //    true);
                    //}
                    
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", "JsPrint('请先上传Excel文档！')",
                        true);
            }
        }
    }
}