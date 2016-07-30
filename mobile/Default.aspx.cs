using Accounts.BLL;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accounts.Web.mobile
{
    public partial class Default : BasePage
    {
        public static int dq_count = 0; //即将到期
        public static int waitg_count = 0; //待收款
        public static int waitr_count = 0; //待还款
        public static int late_count = 0; //延期中
        public static int finish_count = 0;   //已完成
        public static string username = string.Empty;
        public static string userid = string.Empty;
        public static string cash = string.Empty;
        public static string totalmoney = string.Empty;
        public static string headpic = string.Empty;
        public static int CountNum = 0; //总记录数
        public static string Menu = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                GetTodoCount();
                BindPerson();
                BindDataSource(1);
                hiduserid.Value = GetLoginUser().Userid;
                Menu = GetMenu(hiduserid.Value,0);
            }
        }

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
                     database, strWhere, GetLoginUser().Userid);
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
                string where = string.Format("Where IsDelete=0 and Creator='{0}' and TempId ='5' and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 30", GetLoginUser().Userid);
                where += " and isFinish=0";
                return where;
            }
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
                    @"select a.LoginName,b.cash,b.total_money,b.head_pic,a.UserId from Com_UserLogin a LEFT JOIN Com_UserInfos b ON a.UserId=b.Userid where a.UserId='{0}'", GetLoginUser().Userid);
            select.CommandText = sql;
            DataTable dt = select.ExecuteDataSet().Tables[0];
            username = dt.Rows[0]["LoginName"].ToString();
            cash = dt.Rows[0]["cash"].ToString();
            totalmoney = dt.Rows[0]["total_money"].ToString();
            headpic = dt.Rows[0]["head_pic"].ToString();
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex)
        {
            //得到总条目数
            ISelectDataSourceFace select = new SelectSQL();
            select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"SELECT count(0) FROM  View_Account
                                        {0}", Where);
            CountNum = select.ExecuteCount();
            int countn = CountNum >= 20 ? 20 : CountNum;
            //hidallcount.Value = CountNum.ToString();
            //hidtotal.Value = countn.ToString();
            //hidtotalpage.Value = (countn % 5 == 0 ? countn / 5 : countn / 5 + 1).ToString();
            string content = string.Empty;
            for (int i = 0; i < countn; i++)
            {
                content += string.Format(@"
                    <dl class='am-accordion-item' id='item{0}0' style='display:none'>
                    <dt class='am-accordion-title' style='font-size:11px'>
                        <img id='item{0}1'  style='width: 15px; height: 15px; vertical-align: middle;' />&nbsp;<span id='item{0}2'></span><span style='float:right;padding-left:5px' id='item{0}5'></span><span style='float:right;padding-left:3px' id='item{0}4'></span><span style='padding-right:5px;float:right' id='item{0}3'></span>
                    </dt>
                    <dd class='am-accordion-bd am-collapse' id='dditem{0}0'>
                        <div class='am-accordion-content' ></div>
                    </dd>
                </dl>", i); 
            }
            //hiddetail.Value = content == "" ? "<div style='text-align:center'>暂时没有内容</div>" : content;
            hiddetail.Value = content;
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}