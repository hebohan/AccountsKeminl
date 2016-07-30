using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_QUERY_RESP : CMPP_Response
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

}
