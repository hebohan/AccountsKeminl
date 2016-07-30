using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inspur.Finix.DAL.SQL;
using Accounts.BLL.Common;
using System.Net;
using work.MD5Hash;
using System.Configuration;

namespace Accounts.Web.Pay
{
    public partial class TransferAccount : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            hidorderid.Value = DTRequest.GetQueryString("orderid");
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = "select price,PayeeId from OrderList where Orderid='" + hidorderid.Value + "' and Type='TA'"; //转账类型
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                hidprice.Value = dt.Rows[0]["price"].ToString();
                hidinuser.Value = new BLL.BasePage().GetInUser(dt.Rows[0]["PayeeId"].ToString());
            }
            DataTable InUserInfo = GetInUserInfo(dt.Rows[0]["PayeeId"].ToString());
            string payuser = InUserInfo.Rows[0]["LoginName"].ToString();    //收款人用户名
            string payml = InUserInfo.Rows[0]["paykey"].ToString();         //收款人密令
            string paykeyMd5 = MD5up.MD5(payuser.ToLower() + payml).ToUpper(); //收款人密令md5
            string mode = "TA"; //模式为转账
            //string PayAddKey = ConfigurationManager.AppSettings["PayAddKey"];
            string CheckKey = MD5up.MD5(hidorderid.Value + hidprice.Value + mode + "keminl.cn").ToUpper(); //校验支付码
            string PayUrlHead = ConfigurationManager.AppSettings["PayUrlHead"];
            string payurl = string.Format(@"{5}/pay/keminlpay.aspx?orderid={0}&total={1}&CheckKey={2}&BussinessCode={3}&mode={4}", hidorderid.Value, hidprice.Value, CheckKey, paykeyMd5, mode, PayUrlHead);
            hidurl.Value = payurl;
        }

        public void updateSign(string orderid,string Sign)
        {
            IUpdateDataSourceFace update = new UpdateSQL("case_order");
            update.DataBaseAlias = "common";
            update.AddFieldValue("PaySign", Sign);
            update.AddWhere("order_no", orderid);
            update.ExecuteNonQuery();
        }



        public DataTable GetInUserInfo(string userid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select a.paykey,b.LoginName from Com_UserInfos a left join Com_UserLogin b on a.Userid = b.UserId where a.UserId='{0}'", userid);
            return select.ExecuteDataSet().Tables[0];
        }
    }
}