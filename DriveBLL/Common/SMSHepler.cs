using Inspur.Finix.DAL.SQL;
using Inspur.Finix.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Accounts.BLL.Common
{
    /// <summary>
    /// 【东时方短信类】
    /// 配置字典SMSBusinessID、SMSAccount、SMSPassword
    /// 东时方接口说明： http://www.xhsms.com/jiekou.aspx
    /// 发送请求示例：(注意编码格式utf8)
    /// http://gateway.woxp.cn:6630/gb2312/web_api/?x_eid=企业ID&x_uid=账号&x_pwd_md5=登陆密码MD5值&x_ac=10&x_gate_id=300&x_target_no=手机号码&x_memo=短信内容
    /// </summary>
    public class SMSHepler
    {
        /// <summary>
        /// 东时方企业ID（个人为0）
        /// </summary>
        public static string BusinessID { get; set; }

        /// <summary>
        /// 东时方账号
        /// </summary>
        public static string Account { get; set; }

        /// <summary>
        /// 东时方密码
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// 短信标签
        /// </summary>
        public static string Tag { get; set; }

        static SMSHepler()
        {
            BusinessID = DictionaryHelp.GetValueByCode("SMSBusinessID");
            Account = DictionaryHelp.GetValueByCode("SMSAccount");
            Password = DictionaryHelp.GetValueByCode("SMSPassword");
            Tag = DictionaryHelp.GetValueByCode("SMSTag");
            //BusinessID = "11654";
            //Account = "sent_10000";
            //Password = "123456";
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns>发送成功条数（≤0为失败）</returns>
        public static int SendMessage(string telephone, string content, string senduser, string recieveuser)
        {
            int result = 0;
            content = "【" + Tag + "】" + content;
            string url = string.Format("http://gateway.woxp.cn:6630/utf8/web_api/?x_eid={0}&x_uid={1}&x_pwd_md5={2}&x_ac=10&x_gate_id=300&x_target_no={3}&x_memo={4}",
                                        BusinessID, Account, EncryptTomd5(Password), telephone, content);
            int.TryParse(GetHtmlFromUrl(url), out result);

            IInsertDataSourceFace insert = new InsertSQL("Message");
            insert.DataBaseAlias = "common";
            insert.AddFieldValue("Sender", senduser);
            insert.AddFieldValue("Reciever", recieveuser);
            insert.AddFieldValue("RecieveMobile", telephone);
            insert.AddFieldValue("Content", content);
            insert.AddFieldValue("SendTime", DateTime.Now);
            insert.AddFieldValue("Status", result);
            insert.ExecuteNonQuery();

            return result;
        }


        public static string GetHtmlFromUrl(string url)
        {
            string strRet = null;

            if (url == null || url.Trim().ToString() == "")
            {
                return strRet;
            }
            //string targeturl = System.Web.HttpUtility.HtmlEncode( url.Trim().ToString());
            string targeturl = url.Trim().ToString();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                strRet = ser.ReadToEnd();
            }
            catch (Exception ex)
            {
                strRet = null;
                ExceptionManager.Handle(ex);
            }
            return strRet;
        }

        //用MD5方式加密字符串函数
        public static string EncryptTomd5(string strPassword)
        {
            ASCIIEncoding objAsc = new ASCIIEncoding();
            MD5 objMD5 = new MD5CryptoServiceProvider();
            byte[] arrRndHashPwd = objMD5.ComputeHash(objAsc.GetBytes(strPassword));
            string strRndHashPwd = BitConverter.ToString(arrRndHashPwd).Replace("-", "");
            return strRndHashPwd;
        }

    }
}
