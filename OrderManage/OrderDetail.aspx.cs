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

namespace Accounts.Web.OrderManage
{
    public partial class OrderDetail : BasePage
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
                        content = "";
                        string id = DTRequest.GetQueryString("id");
                        ISelectDataSourceFace select = new SelectSQL();
                        select.DataBaseAlias = "common";
                        select.CommandText = "select * from OrderList where id = '" + id + "' and PayerId='" + GetLoginUser().Userid + "'";
                        DataTable dt = select.ExecuteDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            content += "<tr><td><font style='font-weight:bold;'>订单编号：</font><font color='red'>" + dt.Rows[0]["Orderid"].ToString() + "</font></td></tr>";
                            content += "<tr><td><font style='font-weight:bold;'>订单金额：</font><font color='orange'>" + dt.Rows[0]["price"].ToString() + "元</font></td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>收款人：</font><font color='gray'>" + GetInUser(dt.Rows[0]["PayeeId"].ToString()) + "</font></td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>创建时间：</font>" + String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt.Rows[0]["CreateTime"]) + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>完成时间：</font>" + (dt.Rows[0]["FinishTime"].ToString() == "" ? "订单尚未完成" : String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt.Rows[0]["FinishTime"])) + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>订单类型：</font>" + new BasePage().GetDicName(dt.Rows[0]["Type"].ToString()) + "</td></tr>";
                            content += "<tr><td style='word-wrap:break-word;'><font style='font-weight:bold;'>状态：</font><font style='font-weight:bold;' color='green'>" + new BasePage().GetDicName(dt.Rows[0]["Status"].ToString()) + "</font></td></tr>";
                        }
                        else
                        {
                            content += "<td style='text-align:center'>该订单不存在或不属于你</td></tr>";
                        }
                    }
                    catch (Exception ex)
                    {
                        content += "<tr><td style='text-align:center'>系统错误！请稍后再试</td></tr>";
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
    }
}