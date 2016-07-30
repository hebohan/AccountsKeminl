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
    public partial class DealDetail : BasePage
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
                        content = "<tr>";
                        string id = DTRequest.GetQueryString("id");
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText = "select * from History_Money where id = '" + id + "' and Creator='" + GetLoginUser().Userid + "'";
                        DataTable dt = select.ExecuteDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dttt = GetTodayMoney(new Accounts.BLL.BasePage().GetLoginUser().Userid, dt.Rows[0]["CreateTime"].ToString());
                            string payid = dt.Rows[0]["Payid"].ToString();
                            string payremark = dt.Rows[0]["PayRemark"].ToString();
                            DataTable dt_ODetail = GetOrderDetail(payid);
                            content += payid.Length < 10 ? payid.Length == 0 ? (payremark.Length == 0 ? "<td>该交易记录在功能开发前产生，未记录详情</td></tr>" : "<td>该交易记录由<font color='#6F918A'>" + payremark + "</font>产生</td></tr>") : "<td>关联账单：&nbsp;<a href='AccountDetail.aspx?id=" + payid + "&tempid=5' target='_blank'><font color='green'>" + Getzd_name(payid) + "</font></a></td></tr>" : "<td>支付流水号：<font color='red'>" + payid + "</font></td><tr>";
                            content += "<tr><td>交易后总资产：<font color='orange'>" + dt.Rows[0]["TotalMoney"].ToString() + "</font>元&nbsp;" + (dt.Rows[0]["type"].ToString() == "总资产" ? (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString()) >= 0 ? "(<font color='green'>+" + dt.Rows[0]["D_value"].ToString() + "</font>元)" : "(<font color='red'>" + dt.Rows[0]["D_value"].ToString() + "</font>元)") : "") + "</td></tr>";
                            content += "<tr><td>交易后现金：<font color='orange'>" + dt.Rows[0]["Cash"].ToString() + "</font>元&nbsp;" + (dt.Rows[0]["type"].ToString() == "现金" ? (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString()) >= 0 ? "(<font color='green'>+" + dt.Rows[0]["D_value"].ToString() + "</font>元)" : "(<font color='red'>" + dt.Rows[0]["D_value"].ToString() + "</font>元)") : "") + "</td></tr>";
                            content += (payid.Length < 10 && payid.Length > 0) ? "<tr><td style='word-wrap:break-word;'>账单详情：" + GetZD_Detail(payid).Rows[0]["zd_detail"].ToString() : "";
                            content += (payid.Length < 10 && payid.Length > 0) ? "<tr><td style='word-wrap:break-word;'>账单备注：" + GetZD_Detail(payid).Rows[0]["Remark"].ToString() : "";
                            //检测是否为本网站订单
                            if (payid.Length > 11)
                            {
                                string CheckResult = CheckIsOrder(dt_ODetail.Rows[0]["orderid"].ToString());
                                content += (CheckResult == "" ? "<tr><td>第三方支付订单编号：" + dt_ODetail.Rows[0]["orderid"].ToString() : "<tr><td>关联支付订单编号：<a href='../OrderManage/OrderDetail.aspx?id=" + CheckResult + "'>" + dt_ODetail.Rows[0]["orderid"].ToString() + "</a>");
                                content += (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString())) < 0 ? "<tr><td>收款人：<font color='gray'>" + GetInUser("", dt_ODetail.Rows[0]["BussinessCode"].ToString(), "") + "</font>" : "";
                                content += (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString())) >= 0 ? "<tr><td>付款人：<font color='gray'>" + GetInUser("", "", dt_ODetail.Rows[0]["payer"].ToString()) + "</font>" : "";
                            } 
                            content += dt.Rows[0]["UserRemark"].ToString().Length > 0 ?"<tr><td style='word-wrap:break-word;'>用户备注：<font color='#6F918A'>" + dt.Rows[0]["UserRemark"].ToString() + "</font></td></tr>":"";
                            if (dttt.Rows.Count > 0)
                            {
                                content += "<tr><td>&nbsp;</td></tr><tr><td>距该记录最近日期><font color='#6F918A'>" + string.Format("{0:yyyy年MM月dd日 H:mm:ss}", dttt.Rows[0]["CreateTime"]) + "<</font></td></tr><tr><td>您的总资产为<font color='orange'>" + dttt.Rows[0]["total_money"].ToString() + "</font>元，现金为<font color='orange'>" + dttt.Rows[0]["cash"].ToString() + "</font>元</td></tr>";
                            }
                            else
                            {
                                content += "<tr><td>您没有与之相近的资产记录</td></tr>";
                            }
                        }
                        else
                        {
                            content += "<td style='text-align:center'>该账单未被记录或不属于你</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        content += "<tr><td style='text-align:center'>第三方订单详细记录不存在或已被删除！</td></tr>";
                        hidcontent.Value = content;
                    }

                }
                else
                {
                    content += "页面打开错误！";
                }
                hidcontent.Value = content;
            }
            
        }

        public string Getzd_name(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select zd_name from FormContent where info_id = '" + id + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if(dt.Rows.Count>0)
            {
                return dt.Rows[0]["zd_name"].ToString();
            }
            return "点此查看";
        }

        public DataTable GetZD_Detail(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select zd_detail,Remark from View_Account where info_id = '" + id + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public DataTable GetOrderDetail(string payid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select orderid,BussinessCode,payer from Order_Record where payid = '" + payid + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public string GetUserName(string BussinessCode)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select UserRealName from Com_UserInfos where paykeyMd5 = '" + BussinessCode + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserRealName"].ToString();
            }
            return "无记录";
        }

        public string GetPayerName(string username)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select UserRealName from View_Users where LoginName = '" + username + "'";
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["UserRealName"].ToString();
            }
            return "无记录";
        }

        public DataTable GetTodayMoney(string userid,string time)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select top 1 cash,total_money,CreateTime,ABS(datediff(d,CONVERT(varchar(100), CreateTime, 23),'{0}')) as flag from EveryDay_Money_Record where Userid = '{1}' order by flag asc", time, userid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;
        }

        public string CheckIsOrder(string orderid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select id from OrderList where orderid='{0}'", orderid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["id"].ToString();
            }
            return "";
        }

        public string GetInUser(string userid,string BussCode,string payer)
        {
            string value = string.Empty;
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select a.UserRealName,a.Mobile from Com_UserInfos a,Com_UserLogin b  where a.UserId=b.Userid and (a.UserId='{0}' or a.paykeyMd5='{1}' or b.LoginName='{2}')", userid, BussCode,payer);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                string realname = dt.Rows[0]["UserRealName"].ToString();
                string mobile = dt.Rows[0]["Mobile"].ToString();
                value = "*" + (realname.Length <= 3 ? realname.Substring(1, realname.Length - 1) : realname.Substring(2, realname.Length - 2)) + "(" + mobile.Substring(0, 3) + "****" + mobile.Substring(mobile.Length - 4, 4) + ")";
            }
            return value;
        }
    }
}