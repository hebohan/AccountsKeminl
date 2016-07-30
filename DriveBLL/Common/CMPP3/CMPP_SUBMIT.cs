using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
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

}
