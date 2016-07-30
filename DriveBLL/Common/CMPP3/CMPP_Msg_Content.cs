using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_Msg_Content //状态报告
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
}
