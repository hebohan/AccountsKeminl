using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_DELIVER_RESP : CMPP_Response
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

}
