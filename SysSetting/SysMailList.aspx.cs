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

namespace Accounts.Web.SysSetting
{
    public partial class SysMailList : BasePage
    {
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
                string where = " 1=1 ";
                if (!string.IsNullOrEmpty(KeyWord.Value))
                {
                    where += string.Format(" and (receiver like '%{0}%' or title like '%{0}%' or content like '%{0}%')", KeyWord.Value);
                }
                if (!string.IsNullOrEmpty(sDate.Value))
                {
                    where += string.Format(" and Cast(addtime as date)>='{0}'", sDate.Value);
                }
                if (!string.IsNullOrEmpty(eDate.Value))
                {
                    where += string.Format(" and Cast(addtime as date)<='{0}'", eDate.Value);
                }
                return where;
            }
        }

        private void BindControl()
        {
            ///绑定排序
            Order.Items.Clear();
            Order.Items.Add(new ListItem("创建时间降序", "1"));
            Order.Items.Add(new ListItem("创建时间升序", "2"));
            Order.Items.Add(new ListItem("发送时间降序", "3"));
            Order.Items.Add(new ListItem("发送时间升序", "4"));
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
            string sql = string.Format(@"SELECT     id*1 as bh,
                                                    receiver,
                                                    title,
                                                    planstime,
                                                    sender,
                                                    addtime,
                                                    sendtime,
                                                    IsSend
                                            FROM  History_Mail
                                            WHERE  {0}", Where);
            
            if (!string.IsNullOrEmpty(Order.SelectedValue))
            {
                switch(Order.SelectedValue){

                    case "1":
                        select.AddOrderBy("addtime", Sort.Descending);
                        break;
                    case "2":
                        select.AddOrderBy("addtime", Sort.Ascending);
                        break;
                    case "3":
                        select.AddOrderBy("sendtime", Sort.Descending);
                        break;
                    case "4":
                        select.AddOrderBy("sendtime", Sort.Ascending);
                        break;

                }
            }
            select.CommandText = sql;
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
                                    FROM  History_Mail WHERE {0}",Where);
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
    }
}