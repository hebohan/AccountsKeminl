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
    public partial class AccountsList : BasePage
    {
        public string s_string = "";
        public float yeb_moey = 0;
        public float totalmoney = 0;
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                BindControl();
                btnDel.Visible = true;
                hidBtnDel.Visible = true;
                if (!string.IsNullOrEmpty(DTRequest.GetString("status")))
                {
                    string status = DTRequest.GetString("status");

                    switch (status)
                    {
                        case "dq":
                            s_string = " and Abs(DATEDIFF(day,dq_time,GETDATE())) <= 31";
                            break;
                        case "wait_receive":
                            s_string = "and status = 'wait_receive'";
                            break;
                        case "wait_repay":
                            s_string = " and status = 'wait_repay'";
                            break;
                        case "late_pay":
                            s_string = " and status = 'late_pay'";
                            break;
                        case "finish":
                            s_string = " and IsFinish='1'";
                            break;
                        default:
                            s_string = "";
                            break;
                    }
                    hidkeyword.Value = s_string;
                }
                BindDataSource(1, hidkeyword.Value,"");
            }

        }

        private void BindControl()
        {
            //绑定账单类型
            userid = GetLoginUser().Userid;
            ddlTemp.Items.Clear();
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select id,name from AccountTemplate where is_account=1 and (Is_Sys =1 or (Creator='" + userid + "' and Is_Sys =0)) and name like '%"+ DateTime.Now.Year.ToString()+ "%' order by sort_id";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlTemp.Items.Add(new ListItem(dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString()));
            }
            //ddlTemp.Items.Add(new ListItem("全部", ""));

            Status.Items.Clear();
            Status.Items.Add(new ListItem("未完成", ""));
            Status.Items.Add(new ListItem("待收款", "wait_receive"));
            Status.Items.Add(new ListItem("待还款", "wait_repay"));
            Status.Items.Add(new ListItem("待存款", "wait_deposit"));
            Status.Items.Add(new ListItem("延期中", "late_pay"));
            Status.Items.Add(new ListItem("7天内到期", "dq"));

            tz_people.Items.Clear();
            tz_people.Items.Add(new ListItem("请选择", ""));
            select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select tz_people from View_Account where Creator = '{0}' and IsFinish='0' {1} group by tz_people", userid, ddlTemp.SelectedValue == "" ? "" : " and tempid='"+ddlTemp.SelectedValue+"'");
            dt = select.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tz_people.Items.Add(new ListItem(dt.Rows[i]["tz_people"].ToString(), dt.Rows[i]["tz_people"].ToString()));
            }
        }

        public string Where
        {
            get
            {
                string where = "and IsDelete=0 and IsFinish=0";
                if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
                {
                    where += string.Format(" and TempId={0}", ddlTemp.SelectedValue);
                }
                if (!string.IsNullOrEmpty(work.Value))
                {
                    where += string.Format(" and (zd_name like '%{0}%' or zd_detail like '%{0}%')", work.Value.Trim());
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
                if (!string.IsNullOrEmpty(tz_people.SelectedValue))
                {
                    where += string.Format(" and tz_people like '%{0}%' ", tz_people.SelectedValue);
                }
                return where;
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        private void BindDataSource(int pageIndex,string key_word,string flag)
        {
            if(flag == "success_delete")
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "删除成功！", "success"), true);
            }
            userid = GetLoginUser().Userid;
            hiduserid.Value = userid;
            //bool sysflag = false; //是否系统自动更正资产
            //绑定资产
            string _cash = "0";
            string _total = GetTotalMoney();  //校正总资产
            if (_total != "error")
            {
                UpdateTotal(_total);
                //sysflag = true;
            }

            this.ListData.DataSource = null;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT
                                                    Id,
                                                    TempId ,
                                                    Creator ,
                                                    CreateTime ,
                                                    zd_name,
                                                    zd_detail,
                                                    dq_time,
                                                    tz_money,
                                                    ds_money,
                                                    cash,
                                                    total_money,
                                                    DATEDIFF (day,CreateTime,dq_time) as tz_day,
                                                    DATEDIFF (day,GETDATE(),dq_time) as dq_day,
                                                    tz_people,
                                                    isFinish,
                                                    status
                                            FROM  View_Account
                                            WHERE  Creator={0} {1}",
                                  userid, key_word == "" ? Where : key_word + " and TempId='5' and IsDelete=0 and IsFinish=0");
            select.CommandText = sql;
            select.AddOrderBy("isFinish", Sort.Ascending);
            select.AddOrderBy("dq_time", Sort.Ascending);
            select.AddOrderBy("CreateTime", Sort.Ascending);
            DataSet ds = select.ExecuteDataSet(WebPager1.PageSize, pageIndex);

            if(ds.Tables[0].Rows.Count > 0)
            {
                _cash = ds.Tables[0].Rows[0]["cash"].ToString();
                if (_total == "error")
                {
                    _total = ds.Tables[0].Rows[0]["cash"].ToString();
                    UpdateTotal(_total);
                }
            }
            else
            {
                ISelectDataSourceFace _select = new SelectSQL();
                _select.DataBaseAlias = "common";
                _select.CommandText = "select cash,total_money from Com_UserInfos where userid='" + userid + "'";
                DataTable dt = _select.ExecuteDataSet().Tables[0];
                _cash = dt.Rows[0]["cash"].ToString();
                if (_total != "error")
                {
                    _total = dt.Rows[0]["total_money"].ToString(); ;
                }
                else
                {
                    _total = dt.Rows[0]["cash"].ToString();
                    UpdateTotal(_total);
                }
            }
            total.Value = _total;
            cash.Value = _cash;
            money_his.Value = _cash;
            //if (sysflag == true)
            //{
                #region 系统自动更正资产
                ISelectDataSourceFace select_money = new SelectSQL();
                select_money.DataBaseAlias = "common";
                select_money.CommandText = "select top 1 Cash,TotalMoney from History_Money where Creator='" + userid + "'order by CreateTime Desc,id Desc";
                DataTable dtt = select_money.ExecuteDataSet().Tables[0];
                if (dtt.Rows.Count > 0)
                {
                    if (dtt.Rows[0]["TotalMoney"].ToString() != _total)
                    {
                        if (dtt.Rows[0]["Cash"].ToString() == _cash)
                        {
                            SetHisMoney(userid, _cash, _total, (Convert.ToDecimal(total.Value) - Convert.ToDecimal(dtt.Rows[0]["TotalMoney"].ToString())).ToString(), "总资产", "系统自动更正总资产");
                        }
                        else
                        {
                            SetHisMoney(userid, _cash, _total, (Convert.ToDecimal(_cash) - Convert.ToDecimal(dtt.Rows[0]["Cash"].ToString())).ToString(), "现金", "系统自动更正现金");
                        }
                    }
                }
                //else
                //{
                //    IInsertDataSourceFace insert = new InsertSQL("History_Money");
                //    insert.DataBaseAlias = "common";
                //    insert.AddFieldValue("Creator", userid);
                //    insert.AddFieldValue("CreateTime", DateTime.Now);
                //    insert.AddFieldValue("Cash", _cash);
                //    insert.AddFieldValue("TotalMoney", _total);
                //    insert.AddFieldValue("D_value", "0");
                //    insert.AddFieldValue("type", "总资产");
                //    insert.ExecuteNonQuery();
                //}
                #endregion
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
            sql = string.Format(@"  SELECT count(0)
                                    FROM  View_Account WHERE  Creator={0} {1}",
                                  userid, key_word == "" ? Where : key_word + " and TempId='5' and IsDelete=0 and IsFinish=0");
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
            BindDataSource(WebPager1.CurrentPageIndex, hidkeyword.Value,"");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                userid = GetLoginUser().Userid;
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

                    ISelectDataSourceFace select = new SelectSQL();
                    select.DataBaseAlias = "common";
                    select.Transaction = tran;
                    select.CommandText = string.Format(@"select zd_name,Account_Status,ds_money,tz_money from View_Account where id='{0}'", deleId);
                    DataTable dt = select.ExecuteDataSet().Tables[0];
                    string zd_status = dt.Rows[0]["Account_Status"].ToString();
                    string calculatorflag = zd_status == "wait_receive" || zd_status == "wait_deposit" || zd_status == "late_pay" ? "-" : (zd_status == "wait_repay" ? "+" : "");
                    if (calculatorflag != "")
                    {
                        UpdateCash("total_money", dt.Rows[0]["ds_money"].ToString(), calculatorflag, userid, "账单<a href='AccountDetail.aspx?id=" + deleId + "&tempid=5'><font color='red'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>删除", deleId, 1, "normal");
                        UpdateCash("cash", dt.Rows[0]["tz_money"].ToString(), calculatorflag == "+" ? "-" : "+", userid, "账单<a href='AccountDetail.aspx?id=" + deleId + "&tempid=5'><font color='red'>" + dt.Rows[0]["zd_name"].ToString() + "</font></a>删除回退", deleId, 0, "special");
                    }
                }
                tran.Commit();
                BindDataSource(WebPager1.CurrentPageIndex, s_string,"success_delete");

            }
            catch (Exception exx){
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "删除失败！", "error"), true);
            }

            
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
            BindDataSource(1,s_string,"");
            hidkeyword.Value = "";
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

        protected void update_money_Click(object sender, EventArgs e)
        {
            userid = GetLoginUser().Userid;
            string cash =  hcash.Value.ToString();
            string total = htotal.Value.ToString();
            IUpdateDataSourceFace update = new UpdateSQL("Com_UserInfos");
            update.DataBaseAlias = "common";
            update.AddFieldValue("cash", cash);
            update.AddFieldValue("total_money", total);
            update.AddWhere("Userid",userid);
            int i = update.ExecuteNonQuery();
            if(i>0)
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "修改成功！", "success_update"), true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "修改失败！", "fail_update"), true);
            }
            //update_money.Visible = false;
        }
        /// <summary>
        /// 获得总资产-现金
        /// </summary>
        /// <returns></returns>
        public string GetTotalMoney()
        {
            userid = GetLoginUser().Userid;
            decimal total=0;
            decimal temp_cash = 0;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"SELECT
                                                    ds_money,
                                                    cash,
                                                    status
                                            FROM  View_Account
                                            WHERE  Creator={0} and IsDelete=0 and TempId=5 and IsFinish=0",userid);
            DataTable dt =  select.ExecuteDataSet().Tables[0];
            if(dt.Rows.Count>0)
            {
                temp_cash = Convert.ToDecimal(dt.Rows[0]["cash"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["status"].ToString())
                    {
                        case "wait_receive":
                            total += Convert.ToDecimal(dt.Rows[i]["ds_money"].ToString());
                            break;
                        case "wait_repay":
                            total -= Math.Abs(Convert.ToDecimal(dt.Rows[i]["ds_money"].ToString()));
                            break;
                        case "wait_deposit":
                            total += Convert.ToDecimal(dt.Rows[i]["ds_money"].ToString());
                            break;
                        case "late_pay":
                            total += Convert.ToDecimal(dt.Rows[i]["ds_money"].ToString());
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
        //_cash：目前现金 //_total：目前总资产// D_value：增减值 //type：变更类型：现金/总资产 // PayRemark：变更备注
        private void SetHisMoney(string userid, string _cash, string _total, string D_value, string type, string PayRemark)
        {
            IInsertDataSourceFace insert = new InsertSQL("History_Money");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("Creator", userid);
            insert.AddFieldValue("CreateTime", DateTime.Now);
            insert.AddFieldValue("Cash", _cash);
            insert.AddFieldValue("TotalMoney", _total);
            insert.AddFieldValue("D_value", D_value);
            insert.AddFieldValue("type", type);
            insert.AddFieldValue("PayRemark", PayRemark);
            insert.ExecuteNonQuery();
        }

        protected void HidbtnFinish_Click(object sender, EventArgs e)
        {
            int failnum = 0;
            string msg = "账单名称为";
            string[] flag = { "already_getmoney", "already_repay", "already_deposit", "bad_pay" };
            List<string> list = new List<string>(flag);
            userid = GetLoginUser().Userid;
            try
            {
                //IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
                string[] finishIds = hidfid.Value.Split(',');
                int temp_num = 0; //账单进度是否记录
                //IUpdateDataSourceFace update = null;
                foreach (string fId in finishIds)
                {
                    DataRow a_dr = GetAccountInfo(fId).Rows[0]; //获取账单信息
                    if(!IsFinish_Schedule(fId))
                    {
                        string a_status = a_dr["Account_Status"].ToString(); //账单目前的状态
                        string a_finish_status = string.Empty; //账单被设置完成时的状态
                        switch (a_status)
                        {
                            case "wait_receive":
                                a_finish_status = "already_getmoney";
                                break;
                            case "wait_repay":
                                a_finish_status = "already_repay";
                                break;
                            case "wait_deposit":
                                a_finish_status = "already_deposit";
                                break;
                            case "late_pay":
                                a_finish_status = "already_getmoney";
                                break;
                        }
                        IUpdateDataSourceFace update = null;
                        temp_num = IsExist_Schedule(fId.ToString()); //检测账单是否已经存在记录
                        if (temp_num == 0)
                        {
                            IInsertDataSourceFace insert = new InsertSQL("Account_Schedule");
                            insert.DataBaseAlias = "common";
                            insert.AddFieldValue("AccountId", fId);
                            insert.AddFieldValue("AccountMonth", string.Format("{0:yyyyMM}", a_dr["CreateTime"]));
                            insert.AddFieldValue("Remark", a_dr["remark"].ToString());
                            insert.AddFieldValue("Creator", userid);
                            insert.ExecuteNonQuery();
                        }
                        else
                        {
                            update = new UpdateSQL("Account_Schedule");
                            update.DataBaseAlias = "common";
                            update.AddFieldValue("Remark", a_dr["remark"].ToString());
                            update.AddWhere("AccountId", fId);
                            update.ExecuteNonQuery();
                        }
                        int daycount = Convert.ToInt32(a_dr["daycount"].ToString());
                        if (daycount > 0) //正常时间内认定状态流程
                        {
                            update = new UpdateSQL("Account");
                            update.DataBaseAlias = "common";
                            update.AddFieldValue("Account_Status", a_finish_status);
                            update.AddFieldValue("StatusName", GetDicName(a_finish_status));
                            if (list.Contains(a_finish_status))
                            {
                                update.AddFieldValue("isFinish", "1");
                                IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                                update1.DataBaseAlias = "common";
                                update1.AddFieldValue("FinishTime", DateTime.Now);
                                update1.AddWhere("AccountId", fId.Trim());
                                update1.ExecuteNonQuery();

                                ///设置备注--延期完成的项目
                                string key_word = string.Empty;
                                ISelectDataSourceFace select = new SelectSQL();
                                select.DataBaseAlias = "common";
                                select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,FinishTime) as late_day from View_Account  where Id = '" + fId.Trim() + "'";
                                DataTable dt = select.ExecuteDataSet().Tables[0];

                                key_word = dt.Rows[0]["Remark"].ToString();
                                if (daycount != 0)
                                {
                                    if (dt.Rows[0]["LateSign"].ToString() == "True")
                                    {
                                        key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为延期完成，共延期" + Convert.ToInt32(dt.Rows[0]["late_day"].ToString()) + "天";
                                    }
                                    else
                                    {
                                        key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为超期完成，共超期" + Convert.ToInt32(dt.Rows[0]["late_day"].ToString()) + "天";
                                    }
                                }
                                update1 = new UpdateSQL("Account_Schedule");
                                update1.DataBaseAlias = "common";
                                update1.AddFieldValue("Remark", key_word);
                                update1.AddWhere("AccountId", fId.Trim());
                                update1.ExecuteNonQuery();
                            }
                            else
                            {
                                update.AddFieldValue("isFinish", "0");
                            }
                            update.AddWhere("Id", fId.Trim());
                            update.ExecuteNonQuery();
                        }
                        else        //提前认定状态流程
                        {
                            update = new UpdateSQL("Account");
                            update.DataBaseAlias = "common";
                            if (a_finish_status != "late_pay")
                            {
                                update.AddFieldValue("Account_Status", a_finish_status);
                                update.AddFieldValue("StatusName", GetDicName(a_finish_status));
                                if (list.Contains(a_finish_status))
                                {
                                    update.AddFieldValue("isFinish", "1");
                                    IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                                    update1.DataBaseAlias = "common";
                                    update1.AddFieldValue("FinishTime", DateTime.Now);
                                    update1.AddWhere("AccountId", fId.Trim());
                                    update1.ExecuteNonQuery();

                                    ///设置备注--提前完成的项目
                                    string key_word = string.Empty;
                                    ISelectDataSourceFace select = new SelectSQL();
                                    select.DataBaseAlias = "common";
                                    select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,FinishTime) as late_day from View_Account  where Id = '" + fId.Trim() + "'";
                                    DataTable dt = select.ExecuteDataSet().Tables[0];
                                    if (dt.Rows[0]["LateSign"].ToString() != "True")
                                    {
                                        int tday = Math.Abs(Convert.ToInt32(dt.Rows[0]["late_day"].ToString()));
                                        key_word = dt.Rows[0]["Remark"].ToString();
                                        string keystatus = "提前完成，提前" + tday + "天";
                                        if (tday == 0)
                                        {
                                            keystatus = "准时完成";
                                        }
                                        key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为" + keystatus;
                                        IUpdateDataSourceFace update2 = new UpdateSQL("Account_Schedule");
                                        update2.DataBaseAlias = "common";
                                        update2.AddFieldValue("Remark", key_word);
                                        update2.AddWhere("AccountId", fId);
                                        update2.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    update.AddFieldValue("isFinish", "0");
                                }
                                update.AddWhere("Id", fId.Trim());
                                update.ExecuteNonQuery();

                            }
                        }
                        //更新FormContent表
                        update = new UpdateSQL("FormContent");
                        update.DataBaseAlias = "common";
                        update.AddFieldValue("zd_detail", a_dr["zd_detail"].ToString());
                        update.AddFieldValue("status", a_finish_status);
                        update.AddWhere("info_id", fId.Trim());
                        update.ExecuteNonQuery();
                        //写入交易记录
                        if (list.Contains(a_finish_status))
                        {
                            string PayRemark = string.Empty;
                            string PayId = string.Empty;
                            if (a_finish_status == "already_repay")
                            {
                                PayRemark = "账单<a href='AccountDetail.aspx?id=" + fId + "&tempid=5'><font color='red'>" + a_dr["zd_name"].ToString() + "</font></a>还款";
                                UpdateCash("cash", a_dr["ds_money"].ToString(), "-", userid, PayRemark, fId, 0, "normal");
                            }
                            else if (a_finish_status == "bad_pay")
                            {

                            }
                            else
                            {
                                PayRemark = "账单<a href='AccountDetail.aspx?id=" + fId + "&tempid=5'><font color='green'>" + a_dr["zd_name"].ToString() + "</font></a>收款";
                                UpdateCash("cash", a_dr["ds_money"].ToString(), "+", userid, PayRemark, fId, 0, "normal");
                            }
                        }
                    }
                    else
                    {
                        failnum++;
                        msg += "<font color='red'>" + a_dr["zd_name"].ToString() + "</font>&nbsp;&nbsp;";
                    }
                }
                msg += "的账单无法设置为已完成";
                if (failnum == 0)
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "批量操作成功", "success_update"), true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "批量操作失败，错误消息：" + msg, "fail_update"), true);
                }
            }
            catch
            {
                msg = "系统出错，无法完成此处批量操作！";
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", msg, "error"), true);
            }
        }



        protected bool IsFinish_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select isFinish from Account where Id ='" + id.ToString() + "'");
            select.CommandText = sql;
            string _isfinish = select.ExecuteDataSet().Tables[0].Rows[0]["isFinish"].ToString();
            if (_isfinish == "True")
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        protected int IsExist_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select id  from Account_Schedule where AccountId ='" + id.ToString() + "'");
            select.CommandText = sql;
            int temp_num = select.ExecuteDataSet().Tables[0].Rows.Count;
            return temp_num;
        }

        public DataTable GetAccountInfo(string id)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"Select IsDelete,url,urlname,zd_name,zd_detail,CreateTime,DATEDIFF(day,GETDATE(),dq_time) as dq_day,DATEDIFF(day,dq_time,GETDATE()) as daycount,Creator,Account_Status,Remark,ds_money,IsFinish,FinishTime,IsExcel from View_Account where Id={0}", id);
                return select.ExecuteDataSet().Tables[0];
            }
            catch{
                return null;
            }
        }

        protected void HidbtnCount_Click(object sender, EventArgs e)
        {

        }
    }
}