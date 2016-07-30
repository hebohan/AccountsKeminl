using System;
using System.Data;
using System.Web.UI.WebControls;
using Accounts.BLL;
using Inspur.Finix.DAL.SQL;

namespace Accounts.Web.SysSetting
{
    public partial class FormFieldList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                BindDataSource(1);
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
            string sql = "SELECT id*1 as id,name,title,control_type,CASE is_required WHEN 1 THEN '是' ELSE '否' END is_required,CASE is_sys WHEN 1 THEN '是' ELSE '否' END is_sys,sort_id FROM FormField Where is_sys=1 or (Creator='" + GetLoginUser().Userid + "' and is_sys=0) "; 
            select.CommandText = sql;
            select.AddOrderBy("is_sys", Sort.Ascending);
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
            sql = "select count(0) from FormField where is_sys=1 or (Creator ='" + GetLoginUser().Userid + "' and is_sys=0)";
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string[] deleIds = hdelid.Value.Split(',');
            BLL.FormField bll = new BLL.FormField();
            foreach (string deleId in deleIds)
            {
                bll.Delete(int.Parse(deleId));
            }
            BindDataSource(WebPager1.CurrentPageIndex);
        }

        #region 返回字段类型中文名称
        protected string GetTypeCn(string _control_type)
        {
            string type_name = "";
            switch (_control_type)
            {
                case "single-text":
                    type_name = "单行文本";
                    break;
                case "multi-text":
                    type_name = "多行文本";
                    break;
                case "editor":
                    type_name = "编辑器";
                    break;
                case "images":
                    type_name = "图片上传";
                    break;
                case "video":
                    type_name = "视频上传";
                    break;
                case "number":
                    type_name = "数字";
                    break;
                case "date":
                    type_name = "日期";
                    break;
                case "datetime":
                    type_name = "日期时间";
                    break;
                case "checkbox":
                    type_name = "复选框";
                    break;
                case "multi-radio":
                    type_name = "多项单选";
                    break;
                case "multi-checkbox":
                    type_name = "多项多选";
                    break;
                case "dropdownlist":
                    type_name = "下拉列表";
                    break;
                default:
                    type_name = "未知类型";
                    break;
            }
            return type_name;
        }
        #endregion
    }
}