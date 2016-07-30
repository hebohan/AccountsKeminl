using Accounts.BLL;
using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Accounts.Web.mobile.AccountsManage
{
    public partial class DealDetail : BasePage
    {
        public static string Menu = string.Empty;
        public static string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && GetLoginUser() != null)
            {
                string content = string.Empty;
                if (!string.IsNullOrEmpty(DTRequest.GetQueryString("id")))
                {
                    try
                    {
                        hiddlid.Value = DTRequest.GetQueryString("id");
                        content = "<tr>";
                        string id = DTRequest.GetQueryString("id");
                        hiduserid.Value = GetLoginUser().Userid;
                        Menu = GetMenu(hiduserid.Value, 1);
                        hidpid.Value = id;
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText = "select * from History_Money where id = '" + id + "' and Creator='" + hiduserid.Value + "'";
                        DataTable dt = select.ExecuteDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dttt = GetTodayMoney(hiduserid.Value, dt.Rows[0]["CreateTime"].ToString());
                            string payid = dt.Rows[0]["Payid"].ToString();
                            string payremark = dt.Rows[0]["PayRemark"].ToString();
                            content += payid.Length < 10 ? payid.Length == 0 ? (payremark.Length == 0 ? "<td style='width:100%;'>该交易记录在功能开发前产生，未记录详情</td></tr>" : "<td style='width:100%;'>该交易记录由<font color='#6F918A'>" + payremark + "</font>产生</td></tr>") : "<td style='width:100%;'>关联账单：&nbsp;<a href='AccountDetail.aspx?id=" + payid + "&tempid=5' target='_blank'><font color='green'>" + Getzd_name(payid) + "</font></a></td></tr>" : "<td style='width:100%;'>支付流水号：<font color='red'>" + payid + "</font></td><tr>";
                            content += "<tr><td style='width:100%;'>总资产(交易后)：<font color='orange'>" + dt.Rows[0]["TotalMoney"].ToString() + "</font>元&nbsp;" + (dt.Rows[0]["type"].ToString() == "总资产" ? (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString()) >= 0 ? "(<font color='green'>+" + dt.Rows[0]["D_value"].ToString() + "</font>元)" : "(<font color='red'>" + dt.Rows[0]["D_value"].ToString() + "</font>元)") : "") + "</td></tr>";
                            content += "<tr><td style='width:100%;'>现&nbsp;&nbsp;&nbsp;金(交易后)：<font color='orange'>" + dt.Rows[0]["Cash"].ToString() + "</font>元&nbsp;" + (dt.Rows[0]["type"].ToString() == "现金" ? (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString()) >= 0 ? "(<font color='green'>+" + dt.Rows[0]["D_value"].ToString() + "</font>元)" : "(<font color='red'>" + dt.Rows[0]["D_value"].ToString() + "</font>元)") : "") + "</td></tr>";
                            content += (payid.Length < 10 && payid.Length > 0) ? "<tr><td style='word-wrap:break-word;'>账单详情：" + GetZD_Detail(payid).Rows[0]["zd_detail"].ToString() : "";
                            content += (payid.Length < 10 && payid.Length > 0) ? "<tr><td style='word-wrap:break-word;'>账单备注：<br>" + GetZD_Detail(payid).Rows[0]["Remark"].ToString() : "";
                            content += payid.Length > 11 ? "<tr><td style='width:100%;'>第三方支付订单编号：" + GetOrderDetail(payid).Rows[0]["orderid"].ToString() : "";
                            content += payid.Length > 11 && (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString())) < 0 ? "<tr><td style='width:100%;'>收款人：" + GetUserName(GetOrderDetail(payid).Rows[0]["BussinessCode"].ToString()) : "";
                            content += payid.Length > 11 && (Convert.ToDecimal(dt.Rows[0]["D_value"].ToString())) >= 0 ? "<tr><td style='width:100%;'>付款人：" + GetPayerName(GetOrderDetail(payid).Rows[0]["payer"].ToString()) : "";
                            content += "<tr><td style='word-wrap:break-word;'>用户备注：" + string.Format(@"<input type='button' id='ConfirmBtn' runat='server' onclick='saveremark();' value='提交'  style='height:18px;font-size:xx-small;padding-top:1px !important;display:none' class='am-radius-xl am-btn am-btn-success '/>
                                                     <input type='button' id='RcancleBtn' runat='server' onclick='remarkcancle();' value='取消'  style='height:18px;font-size:xx-small;padding-top:1px !important;display:none' class='am-radius-xl am-btn am-btn-success '/>
                                                     &nbsp;<input type='button' id='remarkbtn' runat='server' onclick='addremark();' value='备注'  style='height:18px;font-size:xx-small;padding-top:1px !important' class='am-radius-xl am-btn am-btn-success '/>") + "<br><font color='#6F918A' id='remarktip'>" + dt.Rows[0]["UserRemark"].ToString() + "</font><div class='am-form-group' style='display:none' id='remark_panel'><textarea style='width:100%;' rows='3' id='remark'>" + dt.Rows[0]["UserRemark"].ToString() + "</textarea></div></td></tr>";
                            
                            if (dttt.Rows.Count > 0)
                            {
                                content += "<tr><td style='width:100%;'>&nbsp;</td></tr><tr><td style='width:75%;'>距该记录最近日期><font color='#6F918A'>" + string.Format("{0:yyyy年MM月dd日 H:mm:ss}", dttt.Rows[0]["CreateTime"]) + "<</font></td></tr><tr><td style='width:100%;'>您的总资产为<font color='orange'>" + dttt.Rows[0]["total_money"].ToString() + "</font>元，现金为<font color='orange'>" + dttt.Rows[0]["cash"].ToString() + "</font>元</td></tr>";
                            }
                            else
                            {
                                content += "<tr><td style='width:100%;'>您没有与之相近的资产记录</td></tr>";
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
            if (dt.Rows.Count > 0)
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

        public DataTable GetTodayMoney(string userid, string time)
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
    }
}