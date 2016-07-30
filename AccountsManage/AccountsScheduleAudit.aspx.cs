using Accounts.BLL;
using Accounts.BLL.Common;
using Accounts.Model;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using View_Users = Accounts.BLL.View_Users;
using Accounts.BLL.Login;

namespace Accounts.Web.AccountsManage
{
    public partial class AccountsScheduleAudit : BasePage
    {
        private int id = 0;
        private int tempid = 0;
        public int temp_num = 0;
        public List<string> list;

        protected void Page_Load(object sender, EventArgs e)
        {   
            //绑定状态
            string[] flag = { "already_getmoney", "already_repay", "already_deposit", "bad_pay" };
            list = new List<string>(flag);

            id = DTRequest.GetQueryInt("id");
            tempid = DTRequest.GetQueryInt("tempid");
            if (!IsPostBack)
            {
                
                    if (id > 0)
                    {
                        hidid.Value = id.ToString();
                        //BindControl();
                        if (CheckAuthority(id))
                        {
                            BindData();
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", string.Format("JsPrint('{0}','{1}')", "参数传递不正确，请勿错误操作！", "error"), true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint",
                        string.Format("JsPrint('{0}','{1}')", "参数不对！", "error"), true);
                    }
                }
        }

        private void BindControl(string Key_Word)
        {
            string Key_Word2 = string.Empty;
            string Key_Word3 = string.Empty;
            string Key_Word4 = string.Empty;
            switch (Key_Word)
            {
                case "wait_receive":
                    Key_Word2 = "already_getmoney";
                    Key_Word3 = "late_pay";
                    Key_Word4 = "bad_pay";
                    break;
                case "wait_repay":
                    Key_Word2 = "already_repay";
                    break;
                case "wait_deposit":
                    Key_Word2 = "already_deposit";
                    break;
                case "late_pay":
                    Key_Word2 = "already_getmoney";
                    Key_Word4 = "bad_pay";
                    break;
                    

            }
            //绑定账单状态
            ddlStatus.Items.Clear();
            ISelectDataSourceFace select1 = new SelectSQL();
            select1.DataBaseAlias = "common";
            select1.CommandText = "select DicName,DicCode from Dictionary where DicCode = '" + Key_Word + "' or DicCode = '" + Key_Word2 + "' or DicCode = '" + Key_Word3 + "'or DicCode = '" + Key_Word4 + "'";
            DataTable dt1 = select1.ExecuteDataSet().Tables[0];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                ddlStatus.Items.Add(new ListItem(dt1.Rows[i]["DicName"].ToString(), dt1.Rows[i]["DicCode"].ToString()));
            }
        }


        private void BindData()
        {
            temp_num = IsExist_Schedule(hidid.Value.ToString());
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format("Select IsDelete,url,urlname,zd_name,zd_detail,CreateTime,DATEDIFF(day,GETDATE(),dq_time) as dq_day,DATEDIFF(day,dq_time,GETDATE()) as daycount,Creator,Account_Status,Remark,ds_money,IsFinish,FinishTime,IsExcel from View_Account where Id={0}", id);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                if(dr["url"].ToString() != "")
                {
                    this.url_label.InnerHtml = "&nbsp;&nbsp;&nbsp;<a href='" + dr["url"].ToString() + "' target='_blank' style='color:red'>" + dr["urlname"].ToString() + "</a>";
                    hidurlname.Value = dr["urlname"].ToString();
                    hidurl.Value = dr["url"].ToString();
                    this._name_panel.Style.Add("display", "none");
                    this.u_name.Style.Add("display", "none");
                    this._url_panel.Style.Add("display", "none");
                    this.u_url.Style.Add("display", "none");
                    this.add.Style.Add("display", "none");
                }
                else
                {
                    this.url_label.Style.Add("display", "none");
                    this.modify.Style.Add("display", "none");
                }
                BindControl(dr["Account_Status"].ToString());
                detail.Value = dr["zd_detail"].ToString();
                month.Text = string.Format("{0:yyyyMM}", dr["CreateTime"]);
                zd_status.Text = dr["IsFinish"].ToString() == "True" ? "账单已完成 " + (dr["IsExcel"].ToString() == "True" ? "-->Excel导入" + "-->完成时间：" + string.Format("{0:yyyy-MM-dd H:mm:ss}",dr["FinishTime"]) : "完成时间：" + string.Format("{0:yyyy-MM-dd H:mm:ss}",dr["FinishTime"])) : (int.Parse(dr["dq_day"].ToString()) >= 0 ? dr["dq_day"].ToString() + "天后到期" : "已超期" + System.Math.Abs(int.Parse(dr["dq_day"].ToString())) + "天");
                logger.Text = dr["Creator"]!=DBNull.Value?new View_Users().GetModel(dr["Creator"].ToString()).UserRealName:"";
                logtime.Text = string.Format("{0:yyyy-MM-dd H:mm:ss}",dr["CreateTime"]);
                hidmoney.Value = dr["ds_money"].ToString();
                hiddq_day.Value = dr["dq_day"].ToString();
                hiddaycount.Value = dr["daycount"].ToString();
                hidzd_name.Value = dr["zd_name"].ToString();
                if (dr["IsDelete"].ToString() == "True")
                {
                    this.tip.Style.Add("display", "block");
                    ddlStatus.Enabled = false;
                    btnSave.Visible = false;
                }
                
                if (!string.IsNullOrEmpty(dr["Account_Status"].ToString()))
                {
                    ddlStatus.SelectedValue = dr["Account_Status"].ToString();
                    if (list.Contains(ddlStatus.SelectedValue))
                    {
                        ddlStatus.Enabled = false;
                        btnSave.Visible = false;
                        btnRemark.Visible = true;
                    }
                }
                if (temp_num > 0)
                {
                    zd_remark.Value = dr["Remark"].ToString();
                }
            }
            else
            {
                string msg = string.Format("JsPrint('{0}','{1}')", "进度不存在!", "error");
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsFinish_Schedule(hidid.Value.Trim()))
            {
                temp_num = IsExist_Schedule(hidid.Value.ToString());
                if (temp_num == 0)
                {
                    IInsertDataSourceFace insert = new InsertSQL("Account_Schedule");
                    insert.DataBaseAlias = "common";
                    insert.AddFieldValue("AccountId", hidid.Value.Trim());
                    insert.AddFieldValue("AccountMonth", month.Text.ToString());
                    insert.AddFieldValue("Remark", zd_remark.Value.ToString());
                    insert.AddFieldValue("Creator", GetLoginUser().Userid);
                    //insert.AddFieldValue("FinishTime", DateTime.Now);
                    insert.ExecuteNonQuery();
                }
                else
                {
                    IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                    update1.DataBaseAlias = "common";
                    update1.AddFieldValue("Remark", zd_remark.Value.ToString());
                    update1.AddWhere("AccountId", hidid.Value.Trim());
                    update1.ExecuteNonQuery();
                }
                int daycount = Convert.ToInt32(hiddaycount.Value);
                if (Convert.ToInt32(hiddaycount.Value) > 0) //正常时间内认定状态流程
                {
                    IUpdateDataSourceFace update = new UpdateSQL("Account");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("Account_Status", ddlStatus.SelectedValue);
                    update.AddFieldValue("StatusName", GetDicName(ddlStatus.SelectedValue));
                    if (list.Contains(ddlStatus.SelectedValue))
                    {
                        update.AddFieldValue("isFinish", "1");
                        IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                        update1.DataBaseAlias = "common";
                        update1.AddFieldValue("FinishTime", DateTime.Now);
                        update1.AddWhere("AccountId", hidid.Value.Trim());
                        update1.ExecuteNonQuery();

                        ///设置备注--延期完成的项目
                        string key_word = string.Empty;
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,FinishTime) as late_day from View_Account  where Id = '" + hidid.Value.Trim() + "'";
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
                        IUpdateDataSourceFace update2 = new UpdateSQL("Account_Schedule");
                        update2.DataBaseAlias = "common";
                        update2.AddFieldValue("Remark", key_word);
                        update2.AddWhere("AccountId", id);
                        update2.ExecuteNonQuery();
                    }
                    else
                    {
                        update.AddFieldValue("isFinish", "0");
                    }
                    update.AddWhere("Id", hidid.Value.Trim());
                    update.ExecuteNonQuery();

                    update = new UpdateSQL("FormContent");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("zd_detail", detail.Value.ToString());
                    update.AddFieldValue("status", ddlStatus.SelectedValue);
                    update.AddWhere("info_id", hidid.Value.Trim());
                    update.ExecuteNonQuery();
                }
                else        //提前认定状态流程
                {
                    IUpdateDataSourceFace update = new UpdateSQL("Account");
                    update.DataBaseAlias = "common";
                    if (ddlStatus.SelectedValue != "late_pay")
                    {
                        update.AddFieldValue("Account_Status", ddlStatus.SelectedValue);
                        update.AddFieldValue("StatusName", GetDicName(ddlStatus.SelectedValue));
                        if (list.Contains(ddlStatus.SelectedValue))
                        {
                            update.AddFieldValue("isFinish", "1");
                            IUpdateDataSourceFace update1 = new UpdateSQL("Account_Schedule");
                            update1.DataBaseAlias = "common";
                            update1.AddFieldValue("FinishTime", DateTime.Now);
                            update1.AddWhere("AccountId", hidid.Value.Trim());
                            update1.ExecuteNonQuery();

                            ///设置备注--提前完成的项目
                            string key_word = string.Empty;
                            ISelectDataSourceFace select = new SelectSQL();
                            select.DataBaseAlias = "common";
                            select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,FinishTime) as late_day from View_Account  where Id = '" + hidid.Value.Trim() + "'";
                            DataTable dt = select.ExecuteDataSet().Tables[0];
                            if (dt.Rows[0]["LateSign"].ToString() != "True")
                            {
                                int tday = Math.Abs(Convert.ToInt32(dt.Rows[0]["late_day"].ToString()));
                                key_word = dt.Rows[0]["Remark"].ToString();
                                string keystatus = "提前完成，提前" + tday + "天";
                                if(tday==0)
                                {
                                    keystatus = "准时完成";
                                }
                                key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为" + keystatus;
                                IUpdateDataSourceFace update2 = new UpdateSQL("Account_Schedule");
                                update2.DataBaseAlias = "common";
                                update2.AddFieldValue("Remark", key_word);
                                update2.AddWhere("AccountId", id);
                                update2.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            update.AddFieldValue("isFinish", "0");
                        }
                        update.AddWhere("Id", hidid.Value.Trim());
                        update.ExecuteNonQuery();
                        //更新FormContent表
                        update = new UpdateSQL("FormContent");
                        update.DataBaseAlias = "common";
                        update.AddFieldValue("zd_detail", detail.Value.ToString());
                        update.AddFieldValue("status", ddlStatus.SelectedValue);
                        update.AddWhere("info_id", hidid.Value.Trim());
                        update.ExecuteNonQuery();
                    }
                }
                bool flag = true;
                string msg = string.Empty;
                if (ddlStatus.SelectedValue == "late_pay")
                {
                    flag = SetLate(hidid.Value.Trim(), hiddq_day.Value);
                }
                if (list.Contains(ddlStatus.SelectedValue))
                {
                    string userid = GetLoginUser().Userid;
                    string PayRemark=string.Empty;
                    string PayId = string.Empty;
                    if (ddlStatus.SelectedValue == "already_repay")
                    {
                        PayRemark = "账单<a href='AccountDetail.aspx?id=" + hidid.Value + "&tempid=5'><font color='red'>" + hidzd_name.Value + "</font></a>还款";
                        UpdateCash("cash", hidmoney.Value, "-", userid, PayRemark,hidid.Value,0,"normal");

                    }
                    else if (ddlStatus.SelectedValue == "bad_pay")
                    {
                        
                    }
                    else 
                    {
                        PayRemark = "账单<a href='AccountDetail.aspx?id=" + hidid.Value + "&tempid=5'><font color='green'>" + hidzd_name.Value + "</font></a>收款";
                        UpdateCash("cash", hidmoney.Value, "+", userid, PayRemark, hidid.Value,0,"normal");
                    }
                }
                if(flag != false)
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "认定成功！", "save");
                }
                else
                {
                    msg = string.Format("JsPrint('{0}','{1}')", "未超过账单到期时间，无法设置为延期！", "error_1");
                }
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
            }
            else   //防止用户点了保存按钮后点击浏览器返回
            {
                btnSave.Visible = false;
                ddlStatus.Enabled = false;
                btnRemark.Visible = true;
                zd_status.Text = "账单已完成";
            }
        }

        protected int IsExist_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select id  from Account_Schedule where AccountId ='"+id.ToString()+"'");
            select.CommandText = sql;
            int temp_num = select.ExecuteDataSet().Tables[0].Rows.Count;
            return temp_num;
        }
        
        protected bool IsFinish_Schedule(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"select isFinish from Account where Id ='" + id.ToString() + "'");
            select.CommandText = sql;
            string _isfinish = select.ExecuteDataSet().Tables[0].Rows[0]["isFinish"].ToString();
            if(_isfinish=="True")
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnRemark_Click(object sender, EventArgs e)
        {
            IUpdateDataSourceFace update = new UpdateSQL("Account_Schedule");
            update.DataBaseAlias = "common";
            update.AddFieldValue("Remark", zd_remark.Value.ToString());
            update.AddWhere("AccountId", hidid.Value.Trim());
            update.ExecuteNonQuery();
            string msg = string.Format("JsPrint('{0}','{1}')", "添加备注成功！", "remark");
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msg, true);
        }

        /// <summary>
        /// 设置延期记录
        /// </summary>
        /// <param name="id"></param>
        public bool SetLate(string id, string day)
        {
            string key_word = string.Empty;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select Remark,LateSign,DATEDIFF(day,dq_time,GETDATE()) as daycount from View_Account where AccountId = '" + id + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (Convert.ToInt32(dt.Rows[0]["daycount"].ToString()) <= 0)
            {
                return false;
            }
            else
            {
                if (dt.Rows[0]["LateSign"].ToString() != "True")
                {
                    key_word = dt.Rows[0]["Remark"].ToString();
                    key_word += "\r\n>>>该账单于" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "认定为延期，已延期" + Math.Abs(Convert.ToInt32(day)) + "天";
                    IUpdateDataSourceFace update = new UpdateSQL("Account_Schedule");
                    update.DataBaseAlias = "common";
                    update.AddFieldValue("Remark", key_word);
                    update.AddFieldValue("LateSign", "1");
                    update.AddWhere("AccountId", id);
                    update.ExecuteNonQuery();
                }
                return true;
            }
        }

        public bool CheckAuthority(int accountid)
        {

            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select * from View_Account where id='{0}' and Creator='{1}'",accountid,GetLoginUser().Userid);
            if(select.ExecuteDataSet().Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        
    }
}