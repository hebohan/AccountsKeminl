using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Accounts.BLL;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL.Login;
using Accounts.BLL.Common;

namespace Accounts.Web.AccountsManage
{
    public partial class HistoryMoneyList : BasePage
    {
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                BindControl();
                BindDataSource(1);
            }
        }

        public string Where
        {
            get
            {
                string where = "";
                if (!string.IsNullOrEmpty(work.Value))
                {
                    where += string.Format(" and TotalMoney like '%{0}%'", work.Value.Trim());
                }
                if (!string.IsNullOrEmpty(sDate.Value))
                {
                    where += string.Format(" and Cast(CreateTime as date)>='{0}'", sDate.Value);
                }
                if (!string.IsNullOrEmpty(eDate.Value))
                {
                    where += string.Format(" and Cast(CreateTime as date)<='{0}'", eDate.Value);
                }
                return where;
            }
        }

        private void BindControl()
        {
            ///绑定排序
            Order.Items.Clear();
            Order.Items.Add(new ListItem("请选择", ""));
            Order.Items.Add(new ListItem("按总资产从高到低", "1"));
            Order.Items.Add(new ListItem("按总资产从低到高", "2"));
            Order.Items.Add(new ListItem("按现金从高到低", "3"));
            Order.Items.Add(new ListItem("按现金从低到高", "4"));
            Order.Items.Add(new ListItem("按收支为正", "5"));
            Order.Items.Add(new ListItem("按收支为负", "6"));
            Order.Items.Add(new ListItem("按收支为总资产", "7"));
            Order.Items.Add(new ListItem("按收支为现金", "8"));

        }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex)
        {
            userid = GetLoginUser().Userid;

            this.ListData.DataSource = null;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT
                                                    id*1 as bh,
                                                    CreateTime,
                                                    Cash,
                                                    TotalMoney,
                                                    D_value,
                                                    type,
                                                    PayRemark
                                            FROM  History_Money
                                            WHERE  Creator={0} {1}",
                                  userid, Where);
            
            if (!string.IsNullOrEmpty(Order.SelectedValue))
            {
                switch(Order.SelectedValue){

                    case "1":
                        select.AddOrderBy("TotalMoney", Sort.Descending);
                        break;
                    case "2":
                        select.AddOrderBy("TotalMoney", Sort.Ascending);
                        break;
                    case "3":
                        select.AddOrderBy("Cash", Sort.Descending);
                        break;
                    case "4":
                        select.AddOrderBy("Cash", Sort.Ascending);
                        break;
                    case "5":
                        sql += " and D_value >=0";
                        break;
                    case "6":
                        sql += " and D_value <0";
                        break;
                    case "7":
                        sql += " and type='总资产'";
                        break;
                    case "8":
                        sql += " and type='现金'";
                        break;

                }
            }
            select.CommandText = sql;
            select.AddOrderBy("CreateTime", Sort.Descending);
            select.AddOrderBy("bh", Sort.Descending);
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
            sql = string.Format(@"  SELECT count(0)
                                    FROM  History_Money WHERE  Creator={0} {1}",
                                  userid,Where);
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
            WebPager1.CurrentPageIndex = 1;
            BindDataSource(1);
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            try
            {
                string[] deleIds = hdelid.Value.Split(',');
                IDeleteDataSourceFace delete = null;
                foreach (string deleId in deleIds)
                {
                    delete = new DeleteSQL("History_Money");
                    delete.DataBaseAlias = "common";
                    delete.AddWhere("Id", deleId);
                    delete.Transaction = tran;
                    delete.ExecuteNonQuery();

                }
                tran.Commit();
                BindDataSource(WebPager1.CurrentPageIndex);
            }
            catch
            {
                tran.Rollback();
            }
        }
    }
}