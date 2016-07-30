using Accounts.BLL.Common.Messagess;
using Inspur.Finix.DAL.SQL;
using Inspur.Finix.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Accounts.BLL.Common
{
    public class Person
    {
        public string Name { get; set; }
        public string Moblie { get; set; }
    }
    public class SMSHepler2CMPP
    {
        /// <summary>
        /// 行业网关IP
        /// </summary>
        public static string IP { get; set; }

        /// <summary>
        /// 端口Port
        /// </summary>
        public static string Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// 计费用户类型字段。0:对目的终端MSISDN计费;1:对源终端MSISDN计费;2:对SP计费;3:表示本字段无效,对谁计费参见Fee_terminal_Id字段。
        /// </summary>
        public static string Fee_UserType { get; set; }

        /// <summary>
        /// 被计费用户的号码,当Fee_UserType为3时该值有效,当Fee_UserType为0、1、2时该值无意义。
        /// </summary>
        public static string Fee_terminal_Id { get; set; }

        /// <summary>
        /// 被计费用户的号码类型,0:真实号码;1:伪码。
        /// </summary>
        public static string Fee_terminal_type { get; set; }

        /// <summary>
        /// 资费类别。01:对"计费用户号码"免费;02:对"计费用户号码"按条计信息费;03:对"计费用户号码"按包月收取信息费。
        /// </summary>
        public static string FeeType { get; set; }

        /// <summary>
        /// 资费代码(以分为单位)
        /// </summary>
        public static string FeeCode { get; set; }

        /// <summary>
        /// 源号码。SP的服务代码或前缀为服务代码的长号码, 网关将该号码完整的填到SMPP协议Submit_SM消息相应的source_addr字段,该号码最终在用户手机上显示为短消息的主叫号码。
        /// </summary>
        public static string Src_Id { get; set; }

        /// <summary>
        /// Service_Id
        /// </summary>
        public static string Service_Id { get; set; }

        static SMSHepler2CMPP()
        {
            IP = DictionaryHelp.GetValueByCode("CMPP_IP");
            Port = DictionaryHelp.GetValueByCode("CMPP_Port");
            UserName = DictionaryHelp.GetValueByCode("CMPP_UserName");
            Password = DictionaryHelp.GetValueByCode("CMPP_Password");
            Fee_UserType = DictionaryHelp.GetValueByCode("CMPP_Fee_UserType");
            Fee_terminal_Id = DictionaryHelp.GetValueByCode("CMPP_Fee_terminal_Id");
            Fee_terminal_type = DictionaryHelp.GetValueByCode("CMPP_Fee_terminal_type");
            FeeType = DictionaryHelp.GetValueByCode("CMPP_FeeType");
            FeeCode = DictionaryHelp.GetValueByCode("CMPP_FeeCode");
            Src_Id = DictionaryHelp.GetValueByCode("CMPP_Src_Id");
            Service_Id = DictionaryHelp.GetValueByCode("CMPP_Service_Id");
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        public static void SendMessage(List<Person> persons, string content, string senduser)
        {
            IDbTransaction tran = Inspur.Finix.DAL.cSqlHelper.GetTransByAlias("common");
            try
            {
                IInsertDataSourceFace insert = new InsertSQL("MessageContent");
                insert.DataBaseAlias = "common";
                insert.Transaction = tran;
                insert.AddFieldValue("ContentBody", content);
                insert.AddFieldValue("Sender", senduser);
                insert.AddFieldValue("SendTime", DateTime.Now);
                insert.AddFieldValue("Is_Delete", false);
                insert.ExecuteNonQuery();

                ISelectDataSourceFace select = new SelectSQL();
                select.DataBaseAlias = "common";
                select.Transaction = tran;
                select.CommandText = "SELECT @@IDENTITY";
                object obj = select.ExecuteScalar();


                for (int i = 0; i < persons.Count; i++)
                {
                    string recieveuser = persons[i].Name;
                    string moblie = persons[i].Moblie;

                    string result;
                    if (moblie.Length == 11)
                    {
                        result = Submit(moblie, content);
                    }
                    else
                    {
                        result = "号码有误";
                    }

                    insert = new InsertSQL("MessageSend");
                    insert.DataBaseAlias = "common";
                    insert.Transaction = tran;
                    insert.AddFieldValue("ContentId", obj);
                    insert.AddFieldValue("Reciever", recieveuser);
                    insert.AddFieldValue("RecieveMobile", moblie);
                    insert.AddFieldValue("RecieveTime", DateTime.Now);
                    insert.AddFieldValue("Status", result);
                    //insert.AddFieldValue("Status", "发送成功");
                    insert.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ExceptionManager.Handle(ex);
            }
        }

        private static string Submit(string moblie, string content)
        {
            try
            {
                TcpClient tc = new TcpClient();
                tc.Connect(IP, Convert.ToInt32(Port));
                string UserID = UserName;

                //CMPP_CONNECT connect = new CMPP_CONNECT(UserID, Password, DateTime.Now, 1);
                CMPP_CONNECT connect = new CMPP_CONNECT(UserID, Password, DateTime.Now, 1);


                byte[] bytes = connect.ToBytes();
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sBuilder.Append(bytes[i].ToString("x2"));
                }

                NetworkStream ns = tc.GetStream();
                if (ns.CanWrite)
                {
                    ns.Write(bytes, 0, bytes.Length);
                }
                if (ns.CanRead)
                {
                    int s = 10; //buffer size
                    bytes = ResponseAsBytes(ns, MessageHeader.Length + Accounts.BLL.Common.Messagess.CMPP_CONNECT_RESP._FixedBodyLength, s);
                    CMPP_CONNECT_RESP connect_resp = new CMPP_CONNECT_RESP(bytes);
                    //PrintHeader(connect_resp.Header);   //认证出错
                    if (connect_resp.Header.Command_Id == CMPP_Command_Id.CMPP_CONNECT_RESP)
                    {
                        if (connect_resp.Status == 0)
                        {
                            CMPP_SUBMIT submit = new CMPP_SUBMIT();
                            //submit.Msg_Id = 0;        //      uint _Msg_Id;      // 8 Unsigned Integer 信息标识。
                            submit.Pk_total = 1; //      uint _Pk_total;      // 1 Unsigned Integer 相同Msg_Id的信息总条数,从1开始。
                            submit.Pk_number = 0; //      uint _Pk_number;     // 1 Unsigned Integer 相同Msg_Id的信息序号,从1开始。
                            submit.Registered_Delivery = 0; //      uint _Registered_Delivery;   // 1 Unsigned Integer 是否要求返回状态确认报告:
                            //      //   0:不需要;
                            //      //   1:需要。
                            submit.Msg_level = 1; //      uint _Msg_level;     // 1 Unsigned Integer 信息级别。
                            submit.Service_Id = Service_Id; //      string _Service_Id;     // 10 Octet String 业务标识,是数字、字母和符号的组合。
                            submit.Fee_UserType = Convert.ToUInt32(Fee_UserType); //      uint _Fee_UserType;     // 1 Unsigned Integer 计费用户类型字段:
                            //      //   0:对目的终端MSISDN计费;
                            //      //   1:对源终端MSISDN计费;
                            //      //   2:对SP计费;
                            //      //   3:表示本字段无效,对谁计费参见Fee_terminal_Id字段。
                            submit.Fee_terminal_Id = Fee_terminal_Id; //      string _Fee_terminal_Id;   // 32 Octet String 被计费用户的号码,当Fee_UserType为3时该值有效,当Fee_UserType为0、1、2时该值无意义。
                            submit.Fee_terminal_type = Convert.ToUInt32(Fee_terminal_type); //      uint _Fee_terminal_type;   // 1 Unsigned Integer 被计费用户的号码类型,0:真实号码;1:伪码。

                            submit.Tp_pId = 0; //      uint _TP_pId;      // 1 Unsigned Integer GSM协议类型。详细是解释请参考GSM03.40中的9.2.3.9。
                            submit.Tp_udhi = 0; //      uint _TP_udhi;      // 1 Unsigned Integer GSM协议类型。详细是解释请参考GSM03.40中的9.2.3.23,仅使用1位,右对齐。
                            submit.Msg_Fmt = 15; //      uint _Msg_Fmt;      // 1 Unsigned Integer 信息格式:
                            //      //   0:ASCII串;
                            //      //   3:短信写卡操作;
                            //      //   4:二进制信息;
                            //      //   8:UCS2编码;
                            //      //   15:含GB汉字。。。。。。
                            submit.Msg_src = UserID; //      string _Msg_src;     // 6 Octet String 信息内容来源(SP_Id)。
                            submit.FeeType = FeeType; //      string _FeeType;     // 2 Octet String 资费类别:
                            //      //   01:对"计费用户号码"免费;
                            //      //   02:对"计费用户号码"按条计信息费;
                            //      //   03:对"计费用户号码"按包月收取信息费。
                            submit.FeeCode = FeeCode; //      string _FeeCode;     // 6 Octet String 资费代码(以分为单位)。
                            //Why not 17?
                            submit.ValId_Time = Util.Get_MMDDHHMMSS_String(DateTime.Now.AddHours(2)) + "032+"; //      string _ValId_Time;     // 17 Octet String 存活有效期,格式遵循SMPP3.3协议。
                            submit.At_Time = Util.Get_MMDDHHMMSS_String(DateTime.Now) + "032+"; //      string _At_Time;     // 17 Octet String 定时发送时间,格式遵循SMPP3.3协议。
                            //spnum
                            submit.Src_Id = Src_Id; //     string _Src_Id;      // 21 Octet String 源号码。SP的服务代码或前缀为服务代码的长号码, 网关将该号码完整的填到SMPP协议Submit_SM消息相应的source_addr字段,该号码最终在用户手机上显示为短消息的主叫号码。
                            submit.Dest_terminal_Id = moblie.Split(new char[] { ',', '，' }); //      string[] _Dest_terminal_Id;   // 32*DestUsr_tl Octet String 接收短信的MSISDN号码。
                            submit.DestUsr_tl = (uint)submit.Dest_terminal_Id.Length; //      uint _DestUsr_tl;     // 1 Unsigned Integer 接收信息的用户数量(小于100个用户)。

                            //
                            //
                            submit.Dest_terminal_type = 0; //      uint _Dest_terminal_type;   // 1 Unsigned Integer 接收短信的用户的号码类型,0:真实号码;1:伪码。
                            submit.Msg_Fmt = 15; //      uint _Msg_Length;     // 1 Unsigned Integer 信息长度(Msg_Fmt值为0时:<160个字节;其它<=140个字节),取值大于或等于0。
                            submit.Msg_Content = content.Trim(); //      string _Msg_Content;    // Msg_length Octet String 信息内容。
                            submit.LinkId = ""; //      string _LinkID;      // 20 Octet String 点播业务使用的LinkID,非点播类业务的MT流程不使用该字段。

                            bytes = submit.ToBytes();
                            if (ns.CanWrite)
                            {
                                ns.Write(bytes, 0, bytes.Length);
                            }
                            if (ns.CanRead)
                            {
                                bytes = ResponseAsBytes(ns, MessageHeader.Length + CMPP_SUBMIT_RESP.BodyLength, s);
                                CMPP_SUBMIT_RESP submit_resp = new CMPP_SUBMIT_RESP(bytes);
                                Console.WriteLine(submit_resp.Msg_Id);
                                Console.WriteLine(submit_resp.Result);
                                //PrintHeader(submit_resp.Header);

                                MessageHeader terminate = new MessageHeader(MessageHeader.Length, CMPP_Command_Id.CMPP_TERMINATE, 1);

                                if (ns.CanWrite)
                                {
                                    ns.Write(terminate.ToBytes(), 0, MessageHeader.Length);
                                }
                                if (ns.CanRead)
                                {
                                    bytes = ResponseAsBytes(ns, MessageHeader.Length, s);
                                    MessageHeader terminate_resp = new MessageHeader(bytes);
                                    // PrintHeader(terminate_resp);
                                }
                                ns.Close();
                                ns = null;
                                return GetRespResult(submit_resp.Result);
                            }
                        }
                    }
                }
                return "发送失败";
            }
            catch (Exception ex)
            {
                ExceptionManager.Handle(ex);
                return "发送失败";
            }
        }

        private static byte[] ResponseAsBytes(NetworkStream Stream, int Length, int BufferSize)
        {
            int l;
            byte[] bytes = new byte[Length];
            l = 0;

            do
            {
                byte[] buffer = new byte[BufferSize];
                int r = Stream.Read(buffer, 0, buffer.Length);
                if (r > 0)
                {
                    Buffer.BlockCopy(buffer, 0, bytes, l, r);
                    l += r;
                }
            } while (Stream.DataAvailable);

            byte[] Bytes = new byte[l];
            Buffer.BlockCopy(bytes, 0, Bytes, 0, Bytes.Length);

            return Bytes;
        }//接收数据流

        private static string GetRespResult(uint r)
        {
            string msg = string.Empty;
            switch (r)
            {
                case 0:
                    msg = "发送成功";
                    break;
                case 1:
                    msg = "消息结构错";
                    break;
                case 2:
                    msg = "命令字错";
                    break;
                case 3:
                    msg = "消息序号重复";
                    break;
                case 4:
                    msg = "消息长度错";
                    break;
                case 5:
                    msg = "资费代码错";
                    break;
                case 6:
                    msg = "超过最大信息长";
                    break;
                case 7:
                    msg = "业务代码错";
                    break;
                case 8:
                    msg = "流量控制错";
                    break;
                case 9:
                    msg = "本网关不负责服务此计费号码";
                    break;
                case 10:
                    msg = "Src_Id错误";
                    break;
                case 11:
                    msg = "Msg_src错误";
                    break;
                case 12:
                    msg = "Fee_terminal_Id错误";
                    break;
                case 13:
                    msg = "Dest_terminal_Id错误";
                    break;
            }
            return msg;
        }

        private static void writeTXT(string str)
        {
            ExceptionManager.Handle(new Exception(str));
        }
    }
}
