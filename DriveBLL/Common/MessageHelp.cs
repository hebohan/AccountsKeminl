using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Inspur.Finix.DAL.SQL;
using System.Data;
using Inspur.Finix.ExceptionManagement;

namespace Accounts.BLL.Common
{
    public class MessageHelp
    {
        /// <summary>
        /// 集时通账号
        /// </summary>
        public static string Account { get; set; }

        /// <summary>
        /// 集时通账号密码
        /// </summary>
        public static string Password { get; set; }

        static MessageHelp()
        {
            Account = DictionaryHelp.GetValueByCode("SMSAccount");
            Password = DictionaryHelp.GetValueByCode("SMSPassword");
        }

        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="content">发送内容</param>
        /// <param name="templete">短信模板</param>
        /// <param name="receiveuser">接受人</param>
        /// <param name="telephone">要发送的电话</param>
        /// <returns></returns>
        public static bool MessageSend(string content,string templete,string receiveuser,string telephone)
        {
            bool flag = false;
            string serialNo = Guid.NewGuid().ToString();
            string nowtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"<Group Login_Name=""{0}"" Login_Pwd=""{1}"" OpKind=""0"" InterFaceID="""">", Account, EncryptTomd5(Password));
            sb.AppendFormat("<E_Time>{0}</E_Time>", nowtime);
            sb.AppendFormat("<Item>");
            sb.AppendFormat("<Task><Recive_Phone_Number>{0}</Recive_Phone_Number>", telephone);
            sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", content);
            sb.AppendFormat("<Search_ID>{0}</Search_ID>", serialNo);
            sb.Append("</Task>");
            sb.Append("</Item></Group>");          
            string result = PostHttp(sb.ToString(), "http://userinterface.vcomcn.com/Opration.aspx");
            if (result == "00") flag = true;
            IInsertDataSourceFace insert = new InsertSQL("MessageSend");
            insert.DataBaseAlias = "common";
            insert.InsertTable = "MessageSend";
            insert.AddFieldValue("SerialNo", serialNo);
            insert.AddFieldValue("SendUser", "System");
            insert.AddFieldValue("RecieveUser", receiveuser);
            insert.AddFieldValue("RecieveTelephone", telephone);
            insert.AddFieldValue("SendTime", nowtime);
            insert.AddFieldValue("Content", content);
            insert.AddFieldValue("Templete", templete);
            insert.AddFieldValue("Result", result);
            insert.AddFieldValue("SendXml", sb.ToString());
            insert.ExecuteNonQuery();
            return flag;
        }

        /// <summary>
        /// 短信重发
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int ReSend(string id)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.CommandText = string.Format("select * from dbo.MessageSend where ID={0}",
                id);
            DataTable dt = select.ExecuteDataSet().Tables[0];
            if (dt.Rows.Count > 0)
            {
                string nowtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@"<Group Login_Name=""{0}"" Login_Pwd=""{1}"" OpKind=""0"" InterFaceID="""">", Account, EncryptTomd5(Password));
                sb.AppendFormat("<E_Time>{0}</E_Time>", nowtime);
                sb.AppendFormat("<Item>");
                sb.AppendFormat("<Task><Recive_Phone_Number>{0}</Recive_Phone_Number>", dt.Rows[0]["RecieveTelephone"]);
                sb.AppendFormat("<Content><![CDATA[{0}]]></Content>", dt.Rows[0]["Content"]);
                sb.AppendFormat("<Search_ID>{0}</Search_ID>", dt.Rows[0]["SerialNo"]);
                sb.Append("</Task>");
                sb.Append("</Item></Group>");
                string result = PostHttp(sb.ToString(), "http://userinterface.vcomcn.com/Opration.aspx");
                IUpdateDataSourceFace update = new UpdateSQL("MessageSend");
                update.DataBaseAlias = "common";
                update.AddFieldValue("SendTime", nowtime);
                update.AddFieldValue("Result", result);
                update.AddFieldValue("SendXml", sb.ToString());
                update.AddWhere("ID", id);
                update.ExecuteNonQuery();
                if (result == "00")
                {
                    return 1;
                }
                return 0;
            }
            return -1;
           
        }

        //用MD5方式加密字符串函数
        public static string EncryptTomd5(string strPassword)
        {
            ASCIIEncoding objAsc = new ASCIIEncoding();
            MD5 objMD5 = new MD5CryptoServiceProvider();
            byte[] arrRndHashPwd = objMD5.ComputeHash(objAsc.GetBytes(strPassword));
            string strRndHashPwd = "";
            foreach (byte b in arrRndHashPwd)
            {
                if (b < 16)
                    strRndHashPwd = strRndHashPwd + "0" + b.ToString("X");
                else
                    strRndHashPwd = strRndHashPwd + b.ToString("X");
            }
            return strRndHashPwd;
        }

        public static string PostHttp(string postContent, string url)
        {
            string resultstr = string.Empty;
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "text/xml";
            req.Timeout = 1000000;
            Byte[] bytes = encode.GetBytes(postContent);
            req.ContentLength = bytes.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(bytes, 0, bytes.Length);
            sendStream.Close();
            System.Net.WebResponse rep = req.GetResponse();
            Stream getStream = rep.GetResponseStream();
            using (StreamReader sr = new StreamReader(getStream, Encoding.UTF8))
            {
                resultstr = sr.ReadToEnd();
                sr.Close();
            }
            getStream.Close();
            rep.Close();
            return resultstr;
        }

        /// <summary>
        /// 根据模板配置返回实际内容
        /// </summary>
        /// <param name="content">模板内容</param>
        /// <param name="dic">模板键值对</param>
        /// <returns></returns>
        public static string ConvertContent(string content, Dictionary<string, string> dic)
        {
            return dic.Keys.Aggregate(content, (current, key) => current.Replace(key, dic[key]));
        }
    }
}
