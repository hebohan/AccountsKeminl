using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using System.Net;
using work.MD5Hash;
using System.Configuration;
using Accounts.BLL.Common;
using Accounts.BLL;

namespace Accounts.Web.AccountsManage
{
    public partial class AccountsCount : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                string content = string.Empty;
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("id")))
                {
                    try
                    {
                        string userid = GetLoginUser().Userid;
                        content = "<tr>";
                        string[] Ids = DTRequest.GetQueryString("id").ToString().Split(',');
                        decimal ds_money = 0;   //待收金额
                        decimal tz_money = 0;   //投资金额
                        decimal earn_money = 0; //收益
                        foreach (string fId in Ids)
                        {
                            DataTable dt = GetAccountInfo(fId);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                
                            }
                            else
                            {
                                content += "<td style='text-align:center'></td></tr>";
                            }
                        }
                        content += "<tr><td>交易后总资产：<font color='orange'>" + dt.Rows[0]["TotalMoney"].ToString() + "</font>元&nbsp;" + (dt.Rows[0]["type"].ToString() == "总资产" ? (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString()) >= 0 ? "(<font color='green'>+" + dt.Rows[0]["D_value"].ToString() + "</font>元)" : "(<font color='red'>" + dt.Rows[0]["D_value"].ToString() + "</font>元)") : "") + "</td></tr>";
                        content += "<tr><td>交易后现金：<font color='orange'>" + dt.Rows[0]["Cash"].ToString() + "</font>元&nbsp;" + (dt.Rows[0]["type"].ToString() == "现金" ? (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString()) >= 0 ? "(<font color='green'>+" + dt.Rows[0]["D_value"].ToString() + "</font>元)" : "(<font color='red'>" + dt.Rows[0]["D_value"].ToString() + "</font>元)") : "") + "</td></tr>";
                        content += (payid.Length < 10 && payid.Length > 0) ? "<tr><td style='word-wrap:break-word;'>账单详情：" + GetZD_Detail(payid).Rows[0]["zd_detail"].ToString() : "";
                        content += (payid.Length < 10 && payid.Length > 0) ? "<tr><td style='word-wrap:break-word;'>账单备注：" + GetZD_Detail(payid).Rows[0]["Remark"].ToString() : "";
                        content += payid.Length > 11 ? "<tr><td>第三方支付订单编号：" + GetOrderDetail(payid).Rows[0]["orderid"].ToString() : "";
                        content += payid.Length > 11 && (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString())) < 0 ? "<tr><td>收款人：" + GetUserName(GetOrderDetail(payid).Rows[0]["BussinessCode"].ToString()) : "";
                        content += payid.Length > 11 && (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString())) >= 0 ? "<tr><td>付款人：" + GetPayerName(GetOrderDetail(payid).Rows[0]["payer"].ToString()) : "";
                        content += dt.Rows[0]["UserRemark"].ToString().Length > 0 ? "<tr><td style='word-wrap:break-word;'>用户备注：<font color='#6F918A'>" + dt.Rows[0]["UserRemark"].ToString() + "</font></td></tr>" : "";
                        if (dttt.Rows.Count > 0)
                        {
                            content += "<tr><td>&nbsp;</td></tr><tr><td>距该记录最近日期><font color='#6F918A'>" + string.Format("{0:yyyy年MM月dd日 H:mm:ss}", dttt.Rows[0]["CreateTime"]) + "<</font></td></tr><tr><td>您的总资产为<font color='orange'>" + dttt.Rows[0]["total_money"].ToString() + "</font>元，现金为<font color='orange'>" + dttt.Rows[0]["cash"].ToString() + "</font>元</td></tr>";
                        }
                        else
                        {
                            content += "<tr><td>您没有与之相近的资产记录</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        content += "<td style='text-align:center'>页面打开错误！</td></tr></tr>";
                        hidcontent.Value = content;
                    }

                }
                else
                {
                    content += "<td style='text-align:center'>参数错误！</td></tr>";
                }
                hidcontent.Value = content;
            }
            
        }

        public DataTable GetAccountInfo(string id)
        {
            try
            {
                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.CommandText = string.Format(@"Select IsDelete,url,urlname,zd_name,zd_detail,CreateTime,DATEDIFF(day,GETDATE(),dq_time) as dq_day,DATEDIFF(day,dq_time,GETDATE()) as daycount,Creator,Account_Status,Remark,ds_money,IsFinish,FinishTime,IsExcel from View_Account where Id={0} and Creator='{1}'", id, GetLoginUser().Userid);
                return select.ExecuteDataSet().Tables[0];
            }
            catch
            {
                return null;
            }
        }
    }
}