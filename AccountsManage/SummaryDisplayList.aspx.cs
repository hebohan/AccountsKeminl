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
    public partial class SummaryDisplayList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                    BindControl();
                    btnDel.Visible = true;
                    hidBtnDel.Visible = true;
                    BindDataSource(1,"");
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
            ddlTemp.Items.Add(new ListItem("全部", ""));


            //绑定账单状态
            Status.Items.Clear();
            Status.Items.Add(new ListItem("已完成", "finish"));
            Status.Items.Add(new ListItem("未完成", "unfinish"));
            ISelectDataSourceFace select1 = new SelectSQL();
            select1.DataBaseAlias = "common";
            select1.CommandText = "select DicName,DicCode from Dictionary where ParentId = '80'  and Is_Sys =1 or (Creator='" + GetLoginUser().Userid + "' and Is_Sys =0)";
            DataTable dt1 = select1.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Status.Items.Add(new ListItem(dt1.Rows[i]["DicName"].ToString(), dt1.Rows[i]["DicCode"].ToString()));
            }
            Status.Items.Add(new ListItem("全部", ""));

        }

        public string Where
        {
            get
            {
                string where = "and IsDelete=0";
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
                    if (Status.SelectedValue == "finish")
                    {
                        where += string.Format(" and IsFinish =1");
                    }
                    else if (Status.SelectedValue == "unfinish")
                    {
                        where += string.Format(" and IsFinish =0");
                    }
                    else
                    {
                        where += string.Format(" and status='{0}'", Status.SelectedValue);
                    }
                }
                if (!string.IsNullOrEmpty(sDate.Value))
                {
                    where += string.Format(" and dq_time>='{0}'", sDate.Value);
                }
                if (!string.IsNullOrEmpty(eDate.Value))
                {
                    where += string.Format(" and dq_time<='{0}'", eDate.Value);
                }
                return where;
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex,string flag)
        {
            if (flag == "success_delete")
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "删除成功！", "success"), true);
            }
            this.ListData.DataSource = null;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT
                                                    Id,
                                                    TempId ,
                                                    CreateTime ,
                                                    zd_name,
                                                    zd_detail,
                                                    dq_time,
                                                    tz_money,
                                                    ds_money,
                                                    cash,
                                                    total_money,
                                                    DATEDIFF (day,CreateTime,dq_time) as tz_day,
                                                    tz_people,
                                                    isFinish,
                                                    FinishTime,
                                                    status
                                            FROM  View_Account
                                            WHERE  Creator={0} {1}",
                                  GetLoginUser().Userid, Where);
            select.CommandText = sql;
            select.AddOrderBy("FinishTime", Sort.Descending);
            select.AddOrderBy("isFinish", Sort.Ascending);
            select.AddOrderBy("dq_time", Sort.Ascending);
            select.AddOrderBy("CreateTime", Sort.Ascending);
            

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
                                    FROM  View_Account WHERE  Creator={0} {1}",
                                  GetLoginUser().Userid,Where);
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
            BindDataSource(WebPager1.CurrentPageIndex,"");
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
            BindDataSource(WebPager1.CurrentPageIndex, "success_delete");
        }

        protected string GetTemplateName(int tempid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("AccountTemplate");
            select.SelectColumn("name");
            select.AddWhere("id", tempid);
            object obj = select.ExecuteScalar();
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }

        protected void btnSearch_OnServerClick(object sender, EventArgs e)
        {
            WebPager1.CurrentPageIndex = 1;
            BindDataSource(1,"");
        }

        /**/
        /// <summary>
        /// 根据索引和pagesize返回记录
        /// </summary>
        /// <param name="dt">记录集 DataTable</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="pagesize">一页的记录数</param>
        /// <returns></returns>
        public DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
        /// <summary>
        /// 获得总资产-现金
        /// </summary>
        /// <returns></returns>
        public string GetTotalMoney()
        {
            float total=0;
            float temp_cash = 0;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"SELECT
                                                    ds_money,
                                                    cash,
                                                    status
                                            FROM  View_Account
                                            WHERE  Creator={0} and IsDelete=0 and TempId=5 and IsFinish=0",GetLoginUser().Userid);
            DataTable dt =  select.ExecuteDataSet().Tables[0];
            if(dt.Rows.Count>0)
            {
                temp_cash = Convert.ToSingle(dt.Rows[0]["cash"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["status"].ToString())
                    {
                        case "wait_receive":
                            total += Convert.ToSingle(dt.Rows[i]["ds_money"].ToString());
                            break;
                        case "wait_repay":
                            total -= Convert.ToSingle(dt.Rows[i]["ds_money"].ToString());
                            break;
                        case "wait_deposit":
                            total += Convert.ToSingle(dt.Rows[i]["ds_money"].ToString());
                            break;
                        case "late_pay":
                            total += Convert.ToSingle(dt.Rows[i]["ds_money"].ToString());
                            break;
                    }
                    
                }
            }
            if (dt.Rows.Count > 0)
            {
                return (total + temp_cash).ToString();
            }
            else
            {
                return "error";
            }
        }

    }
}