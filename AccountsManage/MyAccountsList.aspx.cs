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
    public partial class MyAccountsList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                    BindControl();
                    btnDel.Visible = true;
                    hidBtnDel.Visible = true;
                    BindDataSource(1);
            }
        }

        private void BindControl()
        {
            //绑定账单类型
            ddlTemp.Items.Clear();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select id,name from AccountTemplate where is_account=1 and (Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0)) order by sort_id";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlTemp.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
            }

            Status.Items.Clear();
            Status.Items.Add(new ListItem("7天内到期", "dq"));
            Status.Items.Add(new ListItem("待收款", "wait_receive"));
            Status.Items.Add(new ListItem("待还款", "wait_repay"));
            Status.Items.Add(new ListItem("待存款", "wait_deposit"));
            Status.Items.Add(new ListItem("延期中", "late_pay"));
        }

        public string Where
        {
                get
            {
                string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId ='5' and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30", GetLoginUser().Userid);
                where += " and isFinish=0";
                if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
                {
                    where += string.Format(" and TempId={0}", ddlTemp.SelectedValue);
                }
                if (!string.IsNullOrEmpty(work.Value))
                {
                    where += string.Format(" and zd_name like '%{0}%'", work.Value.Trim());
                }
                if (!string.IsNullOrEmpty(Status.SelectedValue))
                {
                    if (Status.SelectedValue == "dq")
                    {
                        where += string.Format(" and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 7");
                    }
                    else
                    {
                        where += string.Format(" and status='{0}'", Status.SelectedValue);
                    }
                    
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
            string sql = string.Format(@"select 
                                                TempId,
	                                            id,
	                                            zd_name,
	                                            tz_money,
                                                ds_money,
	                                            tz_people,
                                                zd_detail,
	                                            dq_time,
                                                DATEDIFF (day,CreateTime,dq_time) as tz_day,
                                                DATEDIFF (day,GETDATE(),dq_time) as dq_day,
	                                            status
	                                            FROM View_Account
                                        {0}", Where);
            select.CommandText = sql;
            select.AddOrderBy("dq_time", Sort.Ascending);
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
                                    FROM  View_Account {0}",
                                    Where);
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
            IUpdateDataSourceFace delete = null;
            foreach (string deleId in deleIds)
            {
                delete = new UpdateSQL("Account");
                delete.DataBaseAlias = "common";
                delete.AddWhere("Id", deleId);
                delete.AddFieldValue("IsDelete", 1);
                delete.Transaction = tran;
                delete.ExecuteNonQuery();
            }
            tran.Commit();
            BindDataSource(WebPager1.CurrentPageIndex);
        }


        protected void btnSearch_OnServerClick(object sender, EventArgs e)
        {
            BindDataSource(1);
        }
    }
}