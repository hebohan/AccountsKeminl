using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_QUERY : CMPP_Request
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

}
