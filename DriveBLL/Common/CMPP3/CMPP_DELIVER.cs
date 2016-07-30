using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_DELIVER : CMPP_Request
    {
        public ulong Msg_Id
        {
            get
            {
                return _Msg_Id;
            }
        }

        public string Dest_Id
        {
            get
            {
                return _Dest_Id;
            }
        }

        public string Service_Id
        {
            get
            {
                return _Service_Id;
            }
        }

        public uint TP_pid
        {
            get
            {
                return _TP_pid;
            }
        }

        public uint TP_udhi
        {
            get
            {
                return _TP_udhi;
            }
        }

        public uint Msg_Fmt
        {
            get
            {
                return _Msg_Fmt;
            }
        }

        public string Src_terminal_Id
        {
            get
            {
                return _Src_terminal_Id;
            }
        }

        public uint Src_terminal_type
        {
            get
            {
                return _Src_terminal_type;
            }
        }

        public uint Registered_Delivery
        {
            get
            {
                return _Registered_Delivery;
            }
        }

        public uint Msg_Length
        {
            get
            {
                return _Msg_Length;
            }
        }

        public string Msg_Content
        {
            get
            {
                return _Msg_Content;
            }
        }

        public string LinkId
        {
            get
            {
                return _LinkID;
            }
        }

        ulong _Msg_Id; // 8 Unsigned Integer 信息标识。
        //   生成算法如下:
        //   采用64位(8字节)的整数:
        //   (1)????????? 时间(格式为MMDDHHMMSS,即月日时分秒):bit64~bit39,其中
        //   bit64~bit61:月份的二进制表示;
        //   bit60~bit56:日的二进制表示;
        //   bit55~bit51:小时的二进制表示;
        //   bit50~bit45:分的二进制表示;
        //   bit44~bit39:秒的二进制表示;
        //   (2)????????? 短信网关代码:bit38~bit17,把短信网关的代码转换为整数填写到该字段中;
        //   (3)????????? 序列号:bit16~bit1,顺序增加,步长为1,循环使用。
        //   各部分如不能填满,左补零,右对齐。
        string _Dest_Id; // 21 Octet String 目的号码。
        //   SP的服务代码,一般4--6位,或者是前缀为服务代码的长号码;该号码是手机用户短消息的被叫号码。
        string _Service_Id; // 10 Octet String 业务标识,是数字、字母和符号的组合。
        uint _TP_pid; // 1 Unsigned Integer GSM协议类型。详细解释请参考GSM03.40中的9.2.3.9。
        uint _TP_udhi; // 1 Unsigned Integer GSM协议类型。详细解释请参考GSM03.40中的9.2.3.23,仅使用1位,右对齐。
        uint _Msg_Fmt; // 1 Unsigned Integer 信息格式:
        //   0:ASCII串;
        //   3:短信写卡操作;
        //   4:二进制信息;
        //   8:UCS2编码;
        //   15:含GB汉字。
        string _Src_terminal_Id; // 32 Octet String 源终端MSISDN号码(状态报告时填为CMPP_SUBMIT消息的目的终端号码)。
        uint _Src_terminal_type; // 1 Unsigned Integer 源终端号码类型,0:真实号码;1:伪码。
        uint _Registered_Delivery; // 1 Unsigned Integer 是否为状态报告:
        //   0:非状态报告;
        //   1:状态报告。
        uint _Msg_Length; // 1 Unsigned Integer 消息长度,取值大于或等于0。
        string _Msg_Content; // Msg_length Octet String 消息内容。
        string _LinkID; // 20 Octet String 点播业务使用的LinkID,非点播类业务的MT流程不使用该字段。

        MessageHeader _Header;


        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }

        public const int FixedBodyLength = 8 // Msg_Id Unsigned Integer 信息标识。
            //   生成算法如下:
            //   采用64位(8字节)的整数:
            //   (1)????????? 时间(格式为MMDDHHMMSS,即月日时分秒):bit64~bit39,其中
            //   bit64~bit61:月份的二进制表示;
            //   bit60~bit56:日的二进制表示;
            //   bit55~bit51:小时的二进制表示;
            //   bit50~bit45:分的二进制表示;
            //   bit44~bit39:秒的二进制表示;
            //   (2)????????? 短信网关代码:bit38~bit17,把短信网关的代码转换为整数填写到该字段中;
            //   (3)????????? 序列号:bit16~bit1,顺序增加,步长为1,循环使用。
            //   各部分如不能填满,左补零,右对齐。
         + 21 // Dest_Id Octet String 目的号码。
            //   SP的服务代码,一般4--6位,或者是前缀为服务代码的长号码;该号码是手机用户短消息的被叫号码。
         + 10 // Service_Id Octet String 业务标识,是数字、字母和符号的组合。
         + 1 // TP_pid Unsigned Integer GSM协议类型。详细解释请参考GSM03.40中的9.2.3.9。
         + 1 // TP_udhi Unsigned Integer GSM协议类型。详细解释请参考GSM03.40中的9.2.3.23,仅使用1位,右对齐。
         + 1 // Msg_Fmt Unsigned Integer 信息格式:
            //   0:ASCII串;
            //   3:短信写卡操作;
            //   4:二进制信息;
            //   8:UCS2编码;
            //   15:含GB汉字。
         + 32 // Src_terminal_Id Octet String 源终端MSISDN号码(状态报告时填为CMPP_SUBMIT消息的目的终端号码)。
         + 1 // Src_terminal_type Unsigned Integer 源终端号码类型,0:真实号码;1:伪码。
         + 1 // Registered_Delivery Unsigned Integer 是否为状态报告:
            //   0:非状态报告;
            //   1:状态报告。
         + 1 // Msg_Length Unsigned Integer 消息长度,取值大于或等于0。
            //Msg_length // Msg_Content Octet String 消息内容。
         + 20; // LinkID Octet String 点播业务使用的LinkID,非点播类业务的MT流程不使用该字段。
        public int _BodyLength;

        public CMPP_DELIVER(byte[] bytes)
        {
            int i = 0;
            byte[] buffer = new byte[MessageHeader.Length];
            Buffer.BlockCopy(bytes, 0, buffer, 0, MessageHeader.Length);
            this._Header = new MessageHeader(buffer);

            //Msg_Id 8
            i += MessageHeader.Length;
            buffer = new byte[8];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Msg_Id = BitConverter.ToUInt64(buffer, 0);

            //Dest_Id 21
            i += 8;
            buffer = new byte[21];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._Dest_Id = Encoding.ASCII.GetString(buffer).Trim();

            //Service_Id 20
            i += 21;
            buffer = new byte[10];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._Service_Id = Encoding.ASCII.GetString(buffer).Trim();

            //TP_pid 1
            i += 10;
            this._TP_pid = (uint)bytes[i++];
            this._TP_udhi = (uint)bytes[i++];
            this._Msg_Fmt = (uint)bytes[i++];

            //Src_terminal_Id 32
            buffer = new byte[32];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._Src_terminal_Id = Encoding.ASCII.GetString(buffer).Trim();


            //Src_terminal_type 1
            i += 32;
            this._Src_terminal_type = (uint)bytes[i++];
            this._Registered_Delivery = (uint)bytes[i++];
            this._Msg_Length = (uint)bytes[i++];

            //Msg_Content
            buffer = new byte[this._Msg_Length];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            switch (this._Msg_Fmt)
            {
                case 8:
                    this._Msg_Content = Encoding.BigEndianUnicode.GetString(buffer).Trim();
                    break;
                case 15: //gb2312
                    this._Msg_Content = Encoding.GetEncoding("gb2312").GetString(buffer).Trim();
                    break;
                case 0: //ascii
                case 3: //短信写卡操作
                case 4: //二进制信息
                default:
                    this._Msg_Content = Encoding.ASCII.GetString(buffer).ToString();
                    break;
            }


            //Linkid 20
            i += (int)this._Msg_Length;
            buffer = new byte[20];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._LinkID = Encoding.ASCII.GetString(buffer).Trim();

        }

        public byte[] ToBytes()
        { //Msg_Length Msg_Content
            byte[] buf;
            switch (this._Msg_Fmt)
            {
                case 8:
                    buf = Encoding.BigEndianUnicode.GetBytes(this._Msg_Content);
                    break;
                case 15: //gb2312
                    buf = Encoding.GetEncoding("gb2312").GetBytes(this._Msg_Content);
                    break;
                case 0: //ascii
                case 3: //短信写卡操作
                case 4: //二进制信息
                default:
                    buf = Encoding.ASCII.GetBytes(this._Msg_Content);
                    break;
            }

            this._Msg_Length = (uint)buf.Length;
            this._BodyLength = FixedBodyLength + (int)this._Msg_Length;

            byte[] bytes = new byte[MessageHeader.Length + this._BodyLength];

            int i = 0;

            byte[] buffer = null;
            //header 12
            this._Header = new MessageHeader((uint)(MessageHeader.Length + this._BodyLength), CMPP_Command_Id.CMPP_DELIVER, 0);


            //Msg_Id 8
            i += MessageHeader.Length;
            buffer = new Byte[8];
            buffer = BitConverter.GetBytes(this._Msg_Id);
            Array.Reverse(buffer);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length);

            //Dest_Id 21
            i += 8;
            buffer = new byte[21];
            buffer = Encoding.ASCII.GetBytes(this._Dest_Id);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length);

            //Service_Id 10
            i += 21;
            buffer = new byte[10];
            buffer = Encoding.ASCII.GetBytes(this._Service_Id);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length);

            //TP_pid 1
            i += 10;
            bytes[i++] = (byte)this._TP_pid;
            bytes[i++] = (byte)this._TP_udhi;
            bytes[i++] = (byte)this._Msg_Fmt;

            //Src_terminal_Id 32
            buffer = new byte[32];
            buffer = Encoding.ASCII.GetBytes(this._Src_terminal_Id);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length);

            //Src_terminal_type 1
            i += 32;
            bytes[i++] = (byte)this._Src_terminal_type;
            bytes[i++] = (byte)this._Registered_Delivery;
            bytes[i++] = (byte)this._Msg_Length;

            //Msg_Content
            Buffer.BlockCopy(buf, 0, bytes, i, buf.Length);

            //LinkID
            i += (int)this._Msg_Length;

            return bytes;
        }
    }

}
