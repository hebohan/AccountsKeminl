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

namespace Accounts.Web.OrderManage
{
    public partial class OrderList : BasePage
    {
        public string userid = string.Empty;
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
            //绑定提醒状态
            Status.Items.Clear();
            Status.Items.Add(new ListItem("待支付", "0"));
            Status.Items.Add(new ListItem("已支付", "1"));
            Status.Items.Add(new ListItem("已取消", "2"));
            Status.Items.Add(new ListItem("全部", ""));
        }

        public string Where
        {
            get
            {
                string where = "and 1=1";
                if (!string.IsNullOrEmpty(keyword.Value))
                {
                    where += string.Format(" and (Remark like '%{0}%' or Orderid like '%{0}%')", keyword.Value.Trim());
                }
                if (!string.IsNullOrEmpty(Status.SelectedValue))
                {
                    string Status_Value = string.Empty;
                    switch(Status.SelectedValue)
                    {
                        case "0":
                            Status_Value = "wait_pay";  //待支付
                            break;
                        case "1":
                            Status_Value = "already_pay";  //已支付
                            break;
                        case "2":
                            Status_Value = "cancle";  //已取消
                            break; 

                    }
                    where += string.Format(" and Status='{0}' ", Status_Value);
                }
                if (!string.IsNullOrEmpty(sDate.Value))
                {
                    where += string.Format(" and Cast(CreateTime as date)>='{0}' ", sDate.Value);
                }
                if (!string.IsNullOrEmpty(eDate.Value))
                {
                    where += string.Format(" and Cast(CreateTime as date)<='{0}' ", eDate.Value);
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
            userid = GetLoginUser().Userid;
            this.ListData.DataSource = null;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT
                                                    Id * 1 as bh,
                                                    Orderid,
                                                    Price,
                                                    PayeeId,
                                                    CreateTime,
                                                    FinishTime,
                                                    Remark,
                                                    Type,
                                                    Status
                                            FROM  OrderList
                                            WHERE  PayerId={0} {1}",
                                  userid, Where);
            select.CommandText = sql;
            select.AddOrderBy("CreateTime", Sort.Ascending);
            select.AddOrderBy("FinishTime", Sort.Ascending);
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
                                    FROM  OrderList WHERE  PayerId={0} {1}",
                                    userid, Where);
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
            try
            {
                string[] deleIds = hdelid.Value.Split(',');
                IDeleteDataSourceFace delete = null;
                foreach (string deleId in deleIds)
                {
                    //DeleteMail(deleId, tran);

                    delete = new DeleteSQL("OrderList");
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

        protected void btnSearch_OnServerClick(object sender, EventArgs e)
        {
            WebPager1.CurrentPageIndex = 1;
            BindDataSource(1);
        }

        //protected void DeleteMail(string id, IDbTransaction tran)
        //{
        //    ISelectDataSourceFace select = new SelectSQL();
        //    select.DataBaseAlias = "common";
        //    select.Transaction = tran;
        //    select.CommandText = "select Status from Reminder where id='" + id + "'";
        //    if(select.ExecuteDataSet().Tables[0].Rows[0]["Status"].ToString()=="wait_remind")
        //    {
        //        IDeleteDataSourceFace delete = new DeleteSQL("History_Mail");
        //        delete.DataBaseAlias = "common";
        //        delete.AddWhere("r_linkid",id);
        //        delete.Transaction = tran;
        //        delete.ExecuteNonQuery();
        //    }
        //}
    }
}