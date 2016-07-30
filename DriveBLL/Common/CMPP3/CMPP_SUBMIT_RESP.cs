using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
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

}
