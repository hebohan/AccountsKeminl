using Accounts.BLL.Common;
using Inspur.Finix.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using work.MD5Hash;
using Accounts.BLL;

namespace Accounts.Web.Pay
{
    public partial class KeminlPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DTRequest.GetQueryString("orderid")) && !string.IsNullOrEmpty(DTRequest.GetQueryString("total")) && !string.IsNullOrEmpty(DTRequest.GetQueryString("CheckKey")))
            {
                string msg = "";
                if (!CheckPay(DTRequest.GetQueryString("orderid"), DTRequest.GetQueryString("total"), DTRequest.GetQueryString("CheckKey"), "1"))
                {
                    hidorderid.Value = DTRequest.GetQueryString("orderid");
                    hidprice.Value = DTRequest.GetQueryString("total");
                    hidCheckKey.Value = DTRequest.GetQueryString("CheckKey");
                    hidBussinessCode.Value = DTRequest.GetQueryString("BussinessCode");
                    hidMode.Value = DTRequest.GetQueryString("mode");

                    string CheckKey = MD5up.MD5(hidorderid.Value + hidprice.Value + hidMode.Value + "keminl.cn").ToUpper();
                    if (CheckKey == hidCheckKey.Value)
                    {
                        if (CheckBussiness(hidBussinessCode.Value))
                        {
                            string PayId = GetLSNO(hidorderid.Value);
                            if (PayId == "")
                            {
                                PayId = GetRandomString(6, 1, "") + DateTime.Now.ToString("yyyyMMddHHmmssff");
                            }
                            hidPayid.Value = PayId; ;
                            InsertRecord(hidorderid.Value, hidprice.Value, hidCheckKey.Value, PayId, hidBussinessCode.Value,hidMode.Value);
                        }
                        else
                        {
                            msg = "支付参数错误！请勿错误操作！";
                            JSPrint(msg);
                        }
                    }
                    else
                    {
                        msg = "支付参数错误！请勿错误操作！";
                        JSPrint(msg);
                    }
                }
                else
                {
                    msg = "该订单已被支付，请不要重复提交订单！";
                    JSPrint(msg);
                }
            }
        }

        public void InsertRecord(string orderid, string price, string CheckKey, string PayId,string BussinessCode,string mode)
        {
            if (!CheckPay(hidorderid.Value, hidprice.Value, hidCheckKey.Value, "2"))
            {
                IInsertDataSourceFace insert = new InsertSQL("Order_Record");
                insert.DataBaseAlias = "common";
                insert.AddFieldValue("Payid", PayId);
                insert.AddFieldValue("orderid", orderid);
                insert.AddFieldValue("price", price);
                insert.AddFieldValue("AddTime", DateTime.Now);
                insert.AddFieldValue("CheckValue", CheckKey);
                insert.AddFieldValue("BussinessCode", BussinessCode);
                insert.AddFieldValue("Mode", mode);
                insert.AddFieldValue("IsPay", "0");
                insert.ExecuteNonQuery();
            }
        }

        public bool CheckPay(string orderid, string price, string CheckKey, string type)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias= "common";
            select.CommandText = string.Format(@"select id from Order_Record where orderid='{0}' and price='{1}' and CheckValue='{2}' {3}",orderid,price,CheckKey,type=="1"? "and IsPay='1'":"");
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if(dt.Rows.Count>0)
            {
                return true;
            }
            return false;
        }

        public bool CheckBussiness(string BussinessCode)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select Userid from Com_UserInfos where paykeyMd5='{0}'",BussinessCode);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void JSPrint(string msg)
        {
            Response.Write(string.Format(@"
                        <script>alert('{0}');
                        window.opener = null;
                        window.open('', '_self');
                        window.close();</script>", msg));
        }

        static string GetRandomString(int length, int count, string separator)
        {
            char[] Chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'R', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            string yymmdd = DateTime.Now.ToString("yyMMdd");
            for (int i = 0; i < count; i++)
            {

                for (int j = 0; j < length; j++)
                {
                    sb.Append(Chars[rd.Next(0, Chars.Length)]);
                }
                if (i < count - 1)
                {
                    sb.Append(separator);
                }
            }
            return sb.ToString();
        }

        public string GetLSNO(string orderid)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format(@"select Payid from Order_Record where orderid='{0}'", orderid);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Payid"].ToString();
            }
            return "";
        }
    }
}