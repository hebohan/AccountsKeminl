using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

//CMPP 消息定义
namespace Accounts.BLL.Common.Messagess
{

    public class CMPP_Request
    {

    }
    public class CMPP_Response
    {

    }
    public enum CMPP_Command_Id : uint
    {
        CMPP_CONNECT = 0x00000001 //请求连接
        ,
        CMPP_CONNECT_RESP = 0x80000001 //请求连接应答
            ,
        CMPP_TERMINATE = 0x00000002 //终止连接
            ,
        CMPP_TERMINATE_RESP = 0x80000002 //终止连接应答
            ,
        CMPP_SUBMIT = 0x00000004 //提交短信
            ,
        CMPP_SUBMIT_RESP = 0x80000004 //提交短信应答
            ,
        CMPP_DELIVER = 0x00000005 //短信下发
            ,
        CMPP_DELIVER_RESP = 0x80000005 //下发短信应答
            ,
        CMPP_QUERY = 0x00000006 //发送短信状态查询
            ,
        CMPP_QUERY_RESP = 0x80000006 //发送短信状态查询应答
            ,
        CMPP_CANCEL = 0x00000007 //删除短信
            ,
        CMPP_CANCEL_RESP = 0x80000007 //删除短信应答
            ,
        CMPP_ACTIVE_TEST = 0x00000008 //激活测试
            ,
        CMPP_ACTIVE_TEST_RESP = 0x80000008 //激活测试应答
            ,
        CMPP_FWD = 0x00000009 //消息前转
            ,
        CMPP_FWD_RESP = 0x80000009 //消息前转应答
            ,
        CMPP_MT_ROUTE = 0x00000010 //MT路由请求
            ,
        CMPP_MT_ROUTE_RESP = 0x80000010 //MT路由请求应答
            ,
        CMPP_MO_ROUTE = 0x00000011 //MO路由请求
            ,
        CMPP_MO_ROUTE_RESP = 0x80000011 //MO路由请求应答
            ,
        CMPP_GET_MT_ROUTE = 0x00000012 //获取MT路由请求
            ,
        CMPP_GET_MT_ROUTE_RESP = 0x80000012 //获取MT路由请求应答
            ,
        CMPP_MT_ROUTE_UPDATE = 0x00000013 //MT路由更新
            ,
        CMPP_MT_ROUTE_UPDATE_RESP = 0x80000013 //MT路由更新应答
            ,
        CMPP_MO_ROUTE_UPDATE = 0x00000014 //MO路由更新
            ,
        CMPP_MO_ROUTE_UPDATE_RESP = 0x80000014 //MO路由更新应答
            ,
        CMPP_PUSH_MT_ROUTE_UPDATE = 0x00000015 //MT路由更新
            ,
        CMPP_PUSH_MT_ROUTE_UPDATE_RESP = 0x80000015 //MT路由更新应答
            ,
        CMPP_PUSH_MO_ROUTE_UPDATE = 0x00000016 //MO路由更新
            ,
        CMPP_PUSH_MO_ROUTE_UPDATE_RESP = 0x80000016 //MO路由更新应答
            ,
        CMPP_GET_MO_ROUTE = 0x00000017 //获取MO路由请求
            , 
        CMPP_GET_MO_ROUTE_RESP = 0x80000017 //获取MO路由请求应答
    }

    public class Util
    {
        public static string Get_MMDDHHMMSS_String(DateTime dt)
        {
            string s = dt.Month.ToString().PadLeft(2, '0');
            s += dt.Day.ToString().PadLeft(2, '0');
            s += dt.Hour.ToString().PadLeft(2, '0');
            s += dt.Minute.ToString().PadLeft(2, '0');
            s += dt.Second.ToString().PadLeft(2, '0');
            return (s);
        }
        public static string Get_YYYYMMDD_String(DateTime dt)
        {
            string s = dt.Year.ToString().PadLeft(4, '0');
            s += dt.Month.ToString().PadLeft(2, '0');
            s += dt.Day.ToString().PadLeft(2, '0');
            return (s);
        }
    }

    public class MessageHeader //消息头
    {
        public const int Length = 4 + 4 + 4;
        //private byte[] _bytes = new byte[MessageHeader.Length];
        public CMPP_Command_Id Command_Id
        {
            get
            {
                return this._Command_Id;
            }
        }

        public uint Sequence_Id
        {
            get
            {
                return this._Sequence_Id;
            }
        }

        public uint Total_Length
        {
            get
            {
                return this._Total_Length;
            }
        }

        //private CMPP_Command_Id _Command_Id;
        //private uint _Sequence_Id;
        //private uint _Total_Length;

        uint _Total_Length; // 4 Unsigned Integer 消息总长度(含消息头及消息体)
        CMPP_Command_Id _Command_Id; // 4 Unsigned Integer 命令或响应类型
        uint _Sequence_Id; // 4 Unsigned Integer 消息流水号,顺序累加,步长为1,循环使用(一对请求和应答消息的流水号必须相同)

        public MessageHeader(uint Total_Length, CMPP_Command_Id Command_Id, uint Sequence_Id) //发送前
        {
            this._Command_Id = Command_Id;
            this._Sequence_Id = Sequence_Id;
            this._Total_Length = Total_Length;
        }

        public MessageHeader(byte[] bytes)
        {
            byte[] buffer = new byte[4];
            Buffer.BlockCopy(bytes, 0, buffer, 0, buffer.Length);
            Array.Reverse(buffer);  //为什么要反转？？ 将低位移到高位，符合数组顺序，转化为正确的uint。
            this._Total_Length = BitConverter.ToUInt32(buffer, 0);

            Buffer.BlockCopy(bytes, 4, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Command_Id = (CMPP_Command_Id)(BitConverter.ToUInt32(buffer, 0));

            Buffer.BlockCopy(bytes, 8, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Sequence_Id = BitConverter.ToUInt32(buffer, 0);
        }


        public byte[] ToBytes()
        {
            byte[] bytes = new byte[MessageHeader.Length];

            byte[] buffer = BitConverter.GetBytes(this._Total_Length);//对uint采用的可能是右移位操作。
            Array.Reverse(buffer);  //这里为什么也要反转操作？将高位移到低位，符合MessageHeader的帧格式
            Buffer.BlockCopy(buffer, 0, bytes, 0, 4);

            buffer = BitConverter.GetBytes((uint)this._Command_Id);
            Array.Reverse(buffer);
            Buffer.BlockCopy(buffer, 0, bytes, 4, 4);

            buffer = BitConverter.GetBytes(this._Sequence_Id);
            Array.Reverse(buffer);
            Buffer.BlockCopy(buffer, 0, bytes, 8, 4);

            return bytes;
        }

    }

    public class CMPP_CONNECT : CMPP_Request
    {
        public const int BodyLength = 6 + 16 + 1 + 4;

        string _Source_Addr; // 6 Octet String 源地址,此处为SP_Id,即SP的企业代码。
        private string _Password;
        byte[] _AuthenticatorSource; // 16 Octet String 用于鉴别源地址。其值通过单向MD5 hash计算得出,表示如下:
        //   AuthenticatorSource =
        //   MD5(Source_Addr+9 字节的0 +shared secret+timestamp)
        //   Shared secret 由中国移动与源地址实体事先商定,timestamp格式为:MMDDHHMMSS,即月日时分秒,10位。
        uint _Version; // 1 Unsigned Integer 双方协商的版本号(高位4bit表示主版本号,低位4bit表示次版本号),对于3.0的版本,高4bit为3,低4位为0
        uint _Timestamp; // 4 Unsigned Integer 时间戳的明文,由客户端产生,格式为MMDDHHMMSS,即月日时分秒,10位数字的整型,右对齐 。

        private MessageHeader _Header;

        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }

        public byte[] AuthenticatorSource
        {
            get
            {
                return this._AuthenticatorSource;
            }
        }

        public CMPP_CONNECT(string Source_Addr, string Password, DateTime Timestamp, uint Version)
        {
            this._Header = new MessageHeader(MessageHeader.Length + BodyLength, CMPP_Command_Id.CMPP_CONNECT, 1);

            this._Source_Addr = Source_Addr;
            this._Password = Password;

            string s = Util.Get_MMDDHHMMSS_String(Timestamp);
            this._Timestamp = UInt32.Parse(s);

            byte[] buffer = new byte[6 + 9 + this._Password.Length + 10];
            Encoding.ASCII.GetBytes(this._Source_Addr).CopyTo(buffer, 0);
            Encoding.ASCII.GetBytes(this._Password).CopyTo(buffer, 6 + 9);
            Encoding.ASCII.GetBytes(s).CopyTo(buffer, 6 + 9 + this._Password.Length);
            this._AuthenticatorSource = new MD5CryptoServiceProvider().ComputeHash(buffer, 0, buffer.Length);

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < _AuthenticatorSource.Length; i++)
            {
                sBuilder.Append(_AuthenticatorSource[i].ToString("x2"));
            }

            this._Version = Version;
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[MessageHeader.Length + BodyLength];
            int i = 0;

            //header 12
            byte[] buffer = this._Header.ToBytes();
            Buffer.BlockCopy(buffer, 0, bytes, 0, buffer.Length);

            //Source_Addr 6
            i += MessageHeader.Length;
            buffer = Encoding.ASCII.GetBytes(this._Source_Addr);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length);

            //AuthenticatorSource 16
            i += 6;
            buffer = this._AuthenticatorSource;
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //16

            //version 1
            i += 16;
            bytes[i++] = (byte)this._Version; //版本

            //Timestamp
            buffer = BitConverter.GetBytes(this._Timestamp);
            Array.Reverse(buffer);
            buffer.CopyTo(bytes, i);

            StringBuilder sBuilder = new StringBuilder();
            for (int x = 0; x < bytes.Length; x++)
            {
                sBuilder.Append(bytes[x].ToString("x2"));
            }

            return (bytes);
        }
    }

    public class CMPP_CONNECT_RESP : CMPP_Response
    {
        MessageHeader _Header;
        public const int _FixedBodyLength = 4 + 16 + 1;

        uint _Status; // 4 Unsigned Integer 状态
        //   0:正确
        //   1:消息结构错
        //   2:非法源地址
        //   3:认证错
        //   4:版本太高
        //   5~:其他错误
        byte[] _AuthenticatorISMG; // 16 Octet String ISMG认证码,用于鉴别ISMG。
        //   其值通过单向MD5 hash计算得出,表示如下:
        //   AuthenticatorISMG =MD5(Status+AuthenticatorSource+shared secret),Shared secret 由中国移动与源地址实体事先商定,AuthenticatorSource为源地址实体发送给ISMG的对应消息CMPP_Connect中的值。
        //    认证出错时,此项为空。
        uint _Version; // 1 Unsigned Integer 服务器支持的最高版本号,对于3.0的版本,高4bit为3,低4位为0

        public byte[] AuthenticatorISMG
        {
            get
            {
                return this._AuthenticatorISMG;
            }
        }

        public uint Status
        {
            get
            {
                return this._Status;
            }
        }

        public uint Version
        {
            get
            {
                return this._Version;
            }
        }

        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }

        public CMPP_CONNECT_RESP(byte[] bytes)
        {
            //header 12
            int i = 0;
            byte[] buffer = new byte[MessageHeader.Length];
            Buffer.BlockCopy(bytes, 0, buffer, 0, buffer.Length);
            this._Header = new MessageHeader(buffer);

            //status 4
            i += MessageHeader.Length;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Status = BitConverter.ToUInt32(buffer, 0);

            //AuthenticatorISMG 16
            i += 4;
            this._AuthenticatorISMG = new byte[16];
            Buffer.BlockCopy(bytes, MessageHeader.Length + 4, this._AuthenticatorISMG, 0, this._AuthenticatorISMG.Length);

            //version
            i += 16;
            this._Version = bytes[i];
        }
    }

    public class CMPP_SUBMIT : CMPP_Request
    {
        public int _BodyLength;
        //without _Dest_terminal_Id Msg_Content 
        public const int FixedBodyLength = 8
         + 1
         + 1
         + 1
         + 1
         + 10
         + 1
         + 32
         + 1
         + 1
         + 1
         + 1
         + 6
         + 2
         + 6
         + 17
         + 17
         + 21
         + 1
            //+ 32*DestUsr_tl
         + 1
         + 1
            //+ Msg_length
         + 20;

        ulong _Msg_Id; // 8 Unsigned Integer 信息标识。
        uint _Pk_total; // 1 Unsigned Integer 相同Msg_Id的信息总条数,从1开始。
        uint _Pk_number; // 1 Unsigned Integer 相同Msg_Id的信息序号,从1开始。
        uint _Registered_Delivery; // 1 Unsigned Integer 是否要求返回状态确认报告:
        //   0:不需要;
        //   1:需要。
        uint _Msg_level; // 1 Unsigned Integer 信息级别。
        string _Service_Id; // 10 Octet String 业务标识,是数字、字母和符号的组合。
        uint _Fee_UserType; // 1 Unsigned Integer 计费用户类型字段:
        //   0:对目的终端MSISDN计费;
        //   1:对源终端MSISDN计费;
        //   2:对SP计费;
        //   3:表示本字段无效,对谁计费参见Fee_terminal_Id字段。
        string _Fee_terminal_Id; // 32 Octet String 被计费用户的号码,当Fee_UserType为3时该值有效,当Fee_UserType为0、1、2时该值无意义。
        uint _Fee_terminal_type; // 1 Unsigned Integer 被计费用户的号码类型,0:真实号码;1:伪码。
        uint _TP_pId; // 1 Unsigned Integer GSM协议类型。详细是解释请参考GSM03.40中的9.2.3.9。
        uint _TP_udhi; // 1 Unsigned Integer GSM协议类型。详细是解释请参考GSM03.40中的9.2.3.23,仅使用1位,右对齐。
        uint _Msg_Fmt; // 1 Unsigned Integer 信息格式:
        //   0:ASCII串;
        //   3:短信写卡操作;
        //   4:二进制信息;
        //   8:UCS2编码;
        //   15:含GB汉字。。。。。。
        string _Msg_src; // 6 Octet String 信息内容来源(SP_Id)。
        string _FeeType; // 2 Octet String 资费类别:
        //   01:对"计费用户号码"免费;
        //   02:对"计费用户号码"按条计信息费;
        //   03:对"计费用户号码"按包月收取信息费。
        string _FeeCode; // 6 Octet String 资费代码(以分为单位)。
        string _ValId_Time; // 17 Octet String 存活有效期,格式遵循SMPP3.3协议。
        string _At_Time; // 17 Octet String 定时发送时间,格式遵循SMPP3.3协议。
        string _Src_Id; // 21 Octet String 源号码。SP的服务代码或前缀为服务代码的长号码, 网关将该号码完整的填到SMPP协议Submit_SM消息相应的source_addr字段,该号码最终在用户手机上显示为短消息的主叫号码。
        uint _DestUsr_tl; // 1 Unsigned Integer 接收信息的用户数量(小于100个用户)。
        string[] _Dest_terminal_Id; // 32*DestUsr_tl Octet String 接收短信的MSISDN号码。


        uint _Dest_terminal_type; // 1 Unsigned Integer 接收短信的用户的号码类型,0:真实号码;1:伪码。
        uint _Msg_Length; // 1 Unsigned Integer 信息长度(Msg_Fmt值为0时:<160个字节;其它<=140个字节),取值大于或等于0。
        string _Msg_Content; // Msg_length Octet String 信息内容。
        string _LinkID; // 20 Octet String 点播业务使用的LinkID,非点播类业务的MT流程不使用该字段。

        MessageHeader _Header;

        public CMPP_SUBMIT()
        {
            //this._Header = new MessageHeader(MessageHeader._Length + _FixedBodyLength, CMPP_Command_Id.CMPP_SUBMIT, 1);
        }

        public byte[] ToBytes()
        {
            //Msg_Length Msg_Content
            byte[] buf;
            switch (this._Msg_Fmt)
            {
                case 8:  //USC2
                    buf = Encoding.BigEndianUnicode.GetBytes(this._Msg_Content); //USC2编码可能有问题！
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
            this._BodyLength = (int)(FixedBodyLength + 32 * this._Dest_terminal_Id.Length + this._Msg_Length);
            byte[] bytes = new byte[MessageHeader.Length + this._BodyLength];

            int i = 0;

            byte[] buffer = new byte[MessageHeader.Length];
            //header
            this._Header = new MessageHeader((uint)(MessageHeader.Length + this._BodyLength), CMPP_Command_Id.CMPP_SUBMIT, 0);
            buffer = this._Header.ToBytes();
            Buffer.BlockCopy(buffer, 0, bytes, 0, buffer.Length);
            i += MessageHeader.Length;

            //Msg_Id //8 [12,19]
            buffer = new byte[8];
            buffer = BitConverter.GetBytes(this._Msg_Id);
            Array.Reverse(buffer);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //10 //[24,33]

            //_Pk_total
            i += 8;
            bytes[i++] = (byte)this._Pk_total; //[20,20]
            bytes[i++] = (byte)this._Pk_number; //[21,21]
            bytes[i++] = (byte)this._Registered_Delivery; //[22,22]
            bytes[i++] = (byte)this._Msg_level; //[23,23]

            //Service_Id
            buffer = Encoding.ASCII.GetBytes(this._Service_Id);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //10 //[24,33]

            //Fee_UserType
            i += 10;
            bytes[i++] = (byte)this._Fee_UserType; //[34,34]

            //Fee_terminal_Id
            buffer = Encoding.ASCII.GetBytes(this._Fee_terminal_Id);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //32 //[35,66]

            //Fee_terminal_type
            i += 32;
            bytes[i++] = (byte)this._Fee_terminal_type; //[67,67]
            bytes[i++] = (byte)this._TP_pId; //[68,68]
            bytes[i++] = (byte)this._TP_udhi; //[69,69]
            bytes[i++] = (byte)this._Msg_Fmt; //[70,70]

            //Msg_src
            buffer = Encoding.ASCII.GetBytes(this._Msg_src);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //6 //[71,76]

            //FeeType
            i += 6;
            buffer = Encoding.ASCII.GetBytes(this._FeeType);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //2 //[77,78]

            //FeeCode
            i += 2;
            buffer = Encoding.ASCII.GetBytes(this._FeeCode);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //6 //[79,84]

            //ValId_Time
            i += 6;
            //buffer = Encoding.ASCII.GetBytes(this._ValId_Time);
            //Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //17 //[85,101]

            //At_Time
            i += 17;
            //buffer = Encoding.ASCII.GetBytes(this._At_Time);
            //Buffer.BlockCopy(buffer , 0, bytes, i, buffer.Length); //17 //[102,118]

            //Src_Id
            i += 17;
            buffer = Encoding.ASCII.GetBytes(this._Src_Id);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //21 //[119,139]

            //DestUsr_tl
            i += 21;
            this._DestUsr_tl = (uint)this._Dest_terminal_Id.Length;
            bytes[i++] = (byte)this._DestUsr_tl; //[140,140]

            //Dest_terminal_Id
            foreach (string s in this._Dest_terminal_Id)
            {
                buffer = Encoding.ASCII.GetBytes(s);
                Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length);
                i += 32;
            }

            //Dest_terminal_type
            bytes[i++] = (byte)this._Dest_terminal_type;
            //Msg_Length
            bytes[i++] = (byte)this._Msg_Length;

            //Msg_Content
            //buffer = Encoding.
            Buffer.BlockCopy(buf, 0, bytes, i, buf.Length);

            //LinkID
            i += (int)this._Msg_Length;
            buffer = Encoding.ASCII.GetBytes(this._LinkID);
            Buffer.BlockCopy(buffer, 0, bytes, i, buffer.Length); //20
            return bytes;
        }

        public ulong Msg_Id
        {
            get
            {
                return this._Msg_Id;
            }
            set
            {
                this._Msg_Id = value;
            }
        }

        public uint Pk_total
        {
            get
            {
                return this._Pk_total;
            }
            set
            {
                this._Pk_total = value;
            }
        }

        public uint Pk_number
        {
            get
            {
                return this._Pk_number;
            }
            set
            {
                this._Pk_number = value;
            }
        }

        public uint Registered_Delivery
        {
            get
            {
                return this._Registered_Delivery;
            }
            set
            {
                this._Registered_Delivery = value;
            }
        }

        public uint Msg_level
        {
            get
            {
                return this._Msg_level;
            }
            set
            {
                this._Msg_level = value;
            }
        }

        public string Service_Id
        {
            get
            {
                return this._Service_Id;
            }
            set
            {
                this._Service_Id = value;
            }
        }

        public uint Fee_UserType
        {
            get
            {
                return this._Fee_UserType;
            }
            set
            {
                this._Fee_UserType = value;
            }
        }

        public string Fee_terminal_Id
        {
            get
            {
                return this._Fee_terminal_Id;
            }
            set
            {
                this._Fee_terminal_Id = value;
            }
        }

        public uint Fee_terminal_type
        {
            get
            {
                return this._Fee_terminal_type;
            }
            set
            {
                this._Fee_terminal_type = value;
            }
        }

        public uint Tp_pId
        {
            get
            {
                return this._TP_pId;
            }
            set
            {
                this._TP_pId = value;
            }
        }

        public uint Tp_udhi
        {
            get
            {
                return this._TP_udhi;
            }
            set
            {
                this._TP_udhi = value;
            }
        }

        public uint Msg_Fmt
        {
            get
            {
                return this._Msg_Fmt;
            }
            set
            {
                this._Msg_Fmt = value;
            }
        }

        public string Msg_src
        {
            get
            {
                return this._Msg_src;
            }
            set
            {
                _Msg_src = value;
            }
        }

        public string FeeType
        {
            get
            {
                return this._FeeType;
            }
            set
            {
                this._FeeType = value;
            }
        }

        public string FeeCode
        {
            get
            {
                return this._FeeCode;
            }
            set
            {
                this._FeeCode = value;
            }
        }

        public string ValId_Time
        {
            get
            {
                return this._ValId_Time;
            }
            set
            {
                this._ValId_Time = value;
            }
        }

        public string At_Time
        {
            get
            {
                return this._At_Time;
            }
            set
            {
                this._At_Time = value;
            }
        }

        public string Src_Id
        {
            get
            {
                return this._Src_Id;
            }
            set
            {
                this._Src_Id = value;
            }
        }

        public uint DestUsr_tl
        {
            get
            {
                return this._DestUsr_tl;
            }
            set
            {
                this._DestUsr_tl = value;
            }
        }

        public string[] Dest_terminal_Id
        {
            get
            {
                return this._Dest_terminal_Id;
            }
            set
            {
                this._Dest_terminal_Id = value;
            }
        }

        public uint Dest_terminal_type
        {
            get
            {
                return this._Dest_terminal_type;
            }
            set
            {
                this._Dest_terminal_type = value;
            }
        }

        public uint Msg_Length
        {
            get
            {
                return this._Msg_Length;
            }
            set
            {
                this._Msg_Length = value;
            }
        }

        public string Msg_Content
        {
            get
            {
                return this._Msg_Content;
            }
            set
            {
                this._Msg_Content = value;
            }
        }

        public string LinkId
        {
            get
            {
                return this._LinkID;
            }
            set
            {
                this._LinkID = value;
            }
        }


    }

    public class CMPP_SUBMIT_RESP : CMPP_Response
    {
        MessageHeader _Header;
        private uint _Msg_Id;
        private uint _Result;

        public const int BodyLength = 8 + 4;

        public uint Msg_Id
        {
            get
            {
                return this._Msg_Id;
            }
        }

        public uint Result
        {
            get
            {
                return this._Result;
            }
        }

        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }

        public CMPP_SUBMIT_RESP(byte[] bytes)
        {
            int i = 0;
            byte[] buffer = new byte[MessageHeader.Length];
            Buffer.BlockCopy(bytes, 0, buffer, 0, buffer.Length);
            this._Header = new MessageHeader(buffer);

            //Msg_Id
            i += MessageHeader.Length;
            buffer = new byte[8];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Msg_Id = BitConverter.ToUInt32(buffer, 0);

            //Result
            i += 8;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Result = BitConverter.ToUInt32(buffer, 0);
        }
    }


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

    class CMPP_DELIVER_RESP : CMPP_Response
    {
        private MessageHeader _Header;
        private ulong _Msg_Id;
        private uint _Result;
        public const int Bodylength = 8 + 4;

        public CMPP_DELIVER_RESP(ulong Msg_Id, uint Result)
        {
            this._Msg_Id = Msg_Id;
            this._Result = Result;
        }

        public byte[] ToBytes()
        {
            int i = 0;
            byte[] bytes = new byte[MessageHeader.Length + Bodylength];

            byte[] buffer = new byte[MessageHeader.Length];
            //header
            this._Header = new MessageHeader(MessageHeader.Length + Bodylength, CMPP_Command_Id.CMPP_DELIVER_RESP, 0);
            buffer = this._Header.ToBytes();
            Buffer.BlockCopy(buffer, 0, bytes, 0, buffer.Length);
            i += MessageHeader.Length;

            //msg_id 8
            buffer = BitConverter.GetBytes(this._Msg_Id);
            Array.Reverse(buffer);
            buffer.CopyTo(bytes, i);

            //result 4
            i += 8;
            buffer = BitConverter.GetBytes(this._Result);
            Array.Reverse(buffer);
            buffer.CopyTo(bytes, i);
            return bytes;


        }
    }


    class CMPP_Msg_Content //状态报告
    {
        public const int BodyLength = 8 + 7 + 10 + 10 + 32 + 4;
        private uint _Msg_Id; // 8 Unsigned Integer 信息标识。SP提交短信(CMPP_SUBMIT)操作时,与SP相连的ISMG产生的Msg_Id。
        private string _Stat; // 7 Octet String 发送短信的应答结果,含义详见表一。SP根据该字段确定CMPP_SUBMIT消息的处理状态。
        private string _Submit_time; // 10 Octet String YYMMDDHHMM(YY为年的后两位00-99,MM:01-12,DD:01-31,HH:00-23,MM:00-59)。
        private string _Done_time; // 10 Octet String YYMMDDHHMM。
        public CMPP_Msg_Content(byte[] bytes)
        {
            if (bytes.Length == BodyLength)
            {
                int i = 0;
                //_Msg_Id 8
                byte[] buffer = new byte[8];
                Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
                Array.Reverse(buffer);
                this._Msg_Id = BitConverter.ToUInt32(buffer, 0);

                //_Stat 7
                i += 8;
                buffer = new byte[7];
                Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
                this._Stat = Encoding.ASCII.GetString(buffer);

                //_Submit_time 10
                i += 7;
                buffer = new byte[10];
                Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
                this._Submit_time = Encoding.ASCII.GetString(buffer);

                //_Done_time 10
                i += 10;
                buffer = new byte[10];
                Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
                this._Submit_time = Encoding.ASCII.GetString(buffer);

                //Dest_terminal_Id 32
                i += 10;
                buffer = new byte[32];
                Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
                this._Dest_terminal_Id = Encoding.ASCII.GetString(buffer);

                //SMSC_sequence 4
                i += 32;
                buffer = new byte[4];
                Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
                Array.Reverse(buffer);
                this._SMSC_sequence = BitConverter.ToUInt32(buffer, 0);
            }
        }

        public uint Msg_Id
        {
            get
            {
                return this._Msg_Id;
            }
            set
            {
                this._Msg_Id = value;
            }
        }

        public string Stat
        {
            get
            {
                return this._Stat;
            }
            set
            {
                this._Stat = value;
            }
        }

        public string Submit_time
        {
            get
            {
                return this._Submit_time;
            }
            set
            {
                this._Submit_time = value;
            }
        }

        public string Done_time
        {
            get
            {
                return this._Done_time;
            }
            set
            {
                this._Done_time = value;
            }
        }

        public string Dest_terminal_Id
        {
            get
            {
                return this._Dest_terminal_Id;
            }
            set
            {
                this._Dest_terminal_Id = value;
            }
        }

        public uint SMSC_sequence
        {
            get
            {
                return this._SMSC_sequence;
            }
            set
            {
                this._SMSC_sequence = value;
            }
        }

        private string _Dest_terminal_Id; // 32 Octet String 目的终端MSISDN号码(SP发送CMPP_SUBMIT消息的目标终端)。
        private uint _SMSC_sequence; // 4 Unsigned Integer 取自SMSC发送状态报告的消息体中的消息标识。
    }

    class CMPP_QUERY : CMPP_Request
    {
        MessageHeader _Header;

        string _Time;  // 8 Octet String 时间YYYYMMDD(精确至日)。
        uint _Query_Type; // 1 Unsigned Integer 查询类别:
        //   0:总数查询;
        //   1:按业务类型查询。
        string _Query_Code; // 10 Octet String 查询码。
        //   当Query_Type为0时,此项无效;当Query_Type为1时,此项填写业务类型Service_Id.。
        string _Reserve; // 8 Octet String 保留。

        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }

        public string Time
        {
            get
            {
                return this._Time;
            }
        }

        public uint Query_Type
        {
            get
            {
                return this._Query_Type;
            }
        }

        public string Query_Code
        {
            get
            {
                return this._Query_Code;
            }
        }

        public string Reserve
        {
            get
            {
                return this._Reserve;
            }
        }
        public const int BodyLength = 8 + 1 + 10 + 8;
        public CMPP_QUERY(System.DateTime Time, uint Query_Type, string Query_Code, string Reserve)
        {
            this._Time = Util.Get_YYYYMMDD_String(Time);
            this._Query_Type = Query_Type;
            this._Query_Code = Query_Code;
            this._Reserve = Reserve;
        }
        public byte[] ToBytes()
        {
            int i = 0;
            byte[] bytes = new byte[MessageHeader.Length + BodyLength];
            //header
            this._Header = new MessageHeader((uint)(MessageHeader.Length + BodyLength), CMPP_Command_Id.CMPP_QUERY, 0);
            byte[] buffer = new byte[MessageHeader.Length];
            buffer = this._Header.ToBytes();
            buffer.CopyTo(bytes, 0);

            //Time 8
            i += MessageHeader.Length;
            buffer = new byte[10];
            buffer = Encoding.ASCII.GetBytes(this._Time);
            buffer.CopyTo(bytes, i);

            //Query_Type 1
            i += 8;
            bytes[i++] = (byte)this._Query_Type;

            //Query_Code 10
            buffer = new byte[10];
            buffer = Encoding.ASCII.GetBytes(this._Query_Code);
            buffer.CopyTo(bytes, i);

            //Reserve 8
            i += 10;
            buffer = new byte[8];
            buffer = Encoding.ASCII.GetBytes(this._Reserve);
            buffer.CopyTo(bytes, i);

            return bytes;
        }
    }

    public class CMPP_QUERY_RESP
    {
        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }

        public string Time
        {
            get
            {
                return this._Time;
            }
        }

        public uint Query_Type
        {
            get
            {
                return this._Query_Type;
            }
        }

        public string Query_Code
        {
            get
            {
                return this._Query_Code;
            }
        }

        public uint Mt_TlMsg
        {
            get
            {
                return this._MT_TLMsg;
            }
        }

        public uint Mt_Tlusr
        {
            get
            {
                return this._MT_Tlusr;
            }
        }

        public uint Mt_Scs
        {
            get
            {
                return this._MT_Scs;
            }
        }

        public uint Mt_Wt
        {
            get
            {
                return this._MT_WT;
            }
        }

        public uint Mt_Fl
        {
            get
            {
                return this._MT_FL;
            }
        }

        public uint Mo_Scs
        {
            get
            {
                return this._MO_Scs;
            }
        }

        public uint Mo_Wt
        {
            get
            {
                return this._MO_WT;
            }
        }

        public uint Mo_Fl
        {
            get
            {
                return this._MO_FL;
            }
        }

        MessageHeader _Header;
        string _Time;  // 8 Octet String 时间(精确至日)。
        uint _Query_Type; // 1 Unsigned Integer 查询类别:
        //   0:总数查询;
        //   1:按业务类型查询。
        string _Query_Code; // 10 Octet String 查询码。
        uint _MT_TLMsg;  // 4 Unsigned Integer 从SP接收信息总数。
        uint _MT_Tlusr;  // 4 Unsigned Integer 从SP接收用户总数。
        uint _MT_Scs;  // 4 Unsigned Integer 成功转发数量。
        uint _MT_WT;  // 4 Unsigned Integer 待转发数量。
        uint _MT_FL;  // 4 Unsigned Integer 转发失败数量。
        uint _MO_Scs;  // 4 Unsigned Integer 向SP成功送达数量。
        uint _MO_WT;  // 4 Unsigned Integer 向SP待送达数量。
        uint _MO_FL;  // 4 Unsigned Integer 向SP送达失败数量。


        public const int BodyLength = 8  // Octet String 时间(精确至日)。
               + 1  // Unsigned Integer 查询类别:
            //  0:总数查询;
            //  1:按业务类型查询。
               + 10 // Octet String 查询码。
               + 4  // Unsigned Integer 从SP接收信息总数。
               + 4  // Unsigned Integer 从SP接收用户总数。
               + 4  // Unsigned Integer 成功转发数量。
               + 4  // Unsigned Integer 待转发数量。
               + 4  // Unsigned Integer 转发失败数量。
               + 4  // Unsigned Integer 向SP成功送达数量。
               + 4  // Unsigned Integer 向SP待送达数量。
               + 4; // Unsigned Integer 向SP送达失败数量。



        public CMPP_QUERY_RESP(byte[] bytes)
        {
            int i = 0;
            //header 12
            byte[] buffer = new byte[MessageHeader.Length];
            Buffer.BlockCopy(bytes, 0, buffer, 0, buffer.Length);
            this._Header = new MessageHeader(buffer);

            //Time 8
            i += MessageHeader.Length;
            buffer = new byte[8];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._Time = Encoding.ASCII.GetString(buffer);

            //Query_Type 1
            i += 8;
            this._Query_Type = (uint)bytes[i++];

            //Query_Code 10
            buffer = new byte[10];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._Query_Code = Encoding.ASCII.GetString(buffer);

            //MT_TLMsg 4
            i += 10;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MT_TLMsg = BitConverter.ToUInt32(buffer, 0);

            //MT_Tlusr 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MT_Tlusr = BitConverter.ToUInt32(buffer, 0);

            //MT_Scs 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MT_Scs = BitConverter.ToUInt32(buffer, 0);

            //MT_WT 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MT_WT = BitConverter.ToUInt32(buffer, 0);

            //MT_FL 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MT_FL = BitConverter.ToUInt32(buffer, 0);

            //MO_Scs 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MO_Scs = BitConverter.ToUInt32(buffer, 0);

            //MO_WT 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MO_WT = BitConverter.ToUInt32(buffer, 0);

            //MO_FL 4
            i += 4;
            buffer = new byte[4];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._MO_FL = BitConverter.ToUInt32(buffer, 0);
        }
    }


    class CMPP_ACTIVE_TEST
    {
        MessageHeader _Header;

        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }
        public CMPP_ACTIVE_TEST()
        {
            this._Header = new MessageHeader(MessageHeader.Length, CMPP_Command_Id.CMPP_ACTIVE_TEST, 0);
        }
        public byte[] ToBytes()
        {
            return this._Header.ToBytes();
        }
    }

    class CMPP_ACTIVE_TEST_RESP
    {
        MessageHeader _Header;
        byte _Reserved;

        public byte Reserved
        {
            get
            {
                return this._Reserved;
            }
        }
        public MessageHeader Header
        {
            get
            {
                return this._Header;
            }
        }
        public CMPP_ACTIVE_TEST_RESP(byte[] bytes)
        {
            int i = 0;
            //header
            byte[] buffer = new byte[MessageHeader.Length];
            Buffer.BlockCopy(bytes, i, buffer, 0, buffer.Length);
            this._Header = new MessageHeader(buffer);

            //Reserved 1
            i += MessageHeader.Length;
            this._Reserved = bytes[i];
        }
        public byte[] ToBytes()
        {
            return this._Header.ToBytes();
        }
    }

}