using Accounts.BLL.Common;
using Accounts.BLL.Login;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accounts.BLL;

namespace Accounts.Web
{
    public partial class main : BasePage
    {
        public static string RoleData { get; set; }
        public static int dq_count = 0; //即将到期
        public static int waitg_count = 0; //待收款
        public static int waitr_count = 0; //待还款
        public static int late_count = 0; //延期中
        public static int finish_count = 0;   //已完成
        protected static string jdyj_zf = string.Empty;
        protected static string jdyj_bm = string.Empty;
        public static string username = string.Empty;
        public static string userid = string.Empty;
        public static string cash = string.Empty;
        public static string totalmoney = string.Empty;
        public static string headpic = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["User"] != null)
                {
                    userid = GetLoginUser().Userid;
                    hiduserid.Value = userid;
                    GetTodoCount();
                    BindDataSource(1);
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }

            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public string GetTodoCount()
        {
            string msg = string.Empty;
            string dq_string = "  and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30 and isFinish=0";
            string waitg_string = " and status = 'wait_receive'";
            string waitr_string = " and status = 'wait_repay'";
            string late_string = " and status = 'late_pay'";
            string finish_string = " and IsFinish='1'";

            dq_count = GetCount(dq_string);
            waitg_count = GetCount(waitg_string);
            waitr_count = GetCount(waitr_string);
            late_count = GetCount(late_string);
            finish_count = GetCount(finish_string);
            return msg;
        }

        /// <summary>
        /// 获取待办账单数量
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            int count = 0;
            ISelectDataSourceFace select = new SelectSQL();
            string database = "dbo.View_Account";

            select.DataBaseAlias = "common";
            string sql =
                string.Format(
                    @"SELECT COUNT(*) FROM {0} WHERE Creator='{2}' and IsDelete=0 and (TempId='5' or TempId='21') {1}",
                     database, strWhere, userid);
            select.CommandText = sql;
            object objnum = select.ExecuteScalar();
            if (objnum != null)
            {
                count = Convert.ToInt32(objnum);
            }
            return count;
        }

        public string Where
        {
            get
            {
                string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId ='5' and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30",userid);
                where += " and isFinish=0";
                return where;
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex)
        {
            BindPerson();
            this.ListData.DataSource = null;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select 
                                                TempId,
	                                            id,
	                                            zd_name,
                                                ds_money,
	                                            tz_people,
	                                            dq_time,
                                                DATEDIFF (day,GETDATE(),dq_time) as dq_day,
	                                            status
	                                            FROM View_Account
                                        {0}", Where);
            select.CommandText = sql;
            select.AddOrderBy("dq_time", Sort.Ascending);
            DataSet ds = select.ExecuteDataSet(WebPager1.PageSize, pageIndex);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dw = ds.Tables[0].Rows[i];
            //    string dq_day = dw["dq_day"].ToString();
            //    if(Convert.ToInt32(dq_day) < 0)
            //    {
            //        SetLate(dw["id"].ToString(), dq_day);
            //    }
            //}
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
            sql = string.Format(@"  SELECT  count(0)
                                    FROM  View_Account
                                        {0}", Where);
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
        /// 获取用户信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public void BindPerson()
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql =
                string.Format(
                    @"select a.LoginName,b.cash,b.total_money,b.head_pic,a.UserId from Com_UserLogin a LEFT JOIN Com_UserInfos b ON a.UserId=b.Userid where a.UserId='{0}'", userid);
            select.CommandText = sql;
            DataTable dt = select.ExecuteDataSet().Tables[0];
            username = dt.Rows[0]["LoginName"].ToString();
            cash = dt.Rows[0]["cash"].ToString();
            totalmoney = dt.Rows[0]["total_money"].ToString();
            headpic = dt.Rows[0]["head_pic"].ToString();
            //cash = cash.Substring(0, cash.Length - 2);
            //totalmoney = totalmoney.Substring(0, totalmoney.Length - 2);
            
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_OnServerClick(object sender, EventArgs e)
        {
            string path = Server.MapPath("~");
            if (picfile.HasFile)
            {
                string fe = System.IO.Path.GetExtension(picfile.FileName).ToLower();
                if (fe != ".jpg" && fe != ".png" && fe != ".gif")
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", "JsPrint('请上传图片！')",
                        true);
                }
                else
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHMMss") + fe;
                    path += "head_image\\" + filename;
                    picfile.SaveAs(path);

                    IUpdateDataSourceFace update = new UpdateSQL("Com_UserInfos");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("head_pic", filename);
                    update.AddWhere("Userid", userid);
                    int i = update.ExecuteNonQuery();
                    if(i>0)
                    {
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "上传成功！", "success_update"), true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "上传失败！", "fail_update"), true);
                    }


                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", "JsPrint('请先选择图片！')",true);
            }
        }
        
    }
}