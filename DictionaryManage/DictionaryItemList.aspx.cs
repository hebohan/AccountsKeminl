using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL;

namespace Accounts.Web.DictionaryManage
{
    public partial class DictionaryItemList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                parentid.Value = Request.QueryString["id"]!=null?Request.QueryString["id"].ToString().Trim():"0";
                backid.Value=GetParentId();
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
            string sql = string.Format("SELECT DicId*1 AS Dicid,Value,DicCode,DicName,IsCatalog,Describe,OrderNum,case Is_Sys when 0 then '否' when 1 then '是' end as Is_Sys FROM dbo.Dictionary where parentid={0} and Is_Sys =1 or (Creator='{1}' and Is_Sys =0)", parentid.Value, new BLL.BasePage().GetLoginUser().Userid);
            select.CommandText = sql;
            select.AddOrderBy("OrderNum",Sort.Ascending);
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
            sql = string.Format("select count(0) from Dictionary where parentid={0} and Is_Sys =1 or (Creator='{1}' and Is_Sys =0)", parentid.Value, new BLL.BasePage().GetLoginUser().Userid);
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
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            string[] deleIds = hdelid.Value.Split(',');
            IDeleteDataSourceFace delete = null;
            foreach (string deleId in deleIds)
            {
                delete = new DeleteSQL("Dictionary");
                delete.DataBaseAlias = "common";
                delete.AddWhere("DicId", deleId);
                delete.Transaction = tran;
                delete.ExecuteNonQuery();
            }
            tran.Commit();
            BindDataSource(WebPager1.CurrentPageIndex);
        }

        private string GetParentId()
        {
            ISelectDataSourceFace select=new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format("Select ParentId from Dictionary where DicId={0}", parentid.Value);
            object obj = select.ExecuteScalar();
            if (obj != null&&obj.ToString()!="")
            {
                return obj.ToString();
            }
            return "0";
        }
    }
}