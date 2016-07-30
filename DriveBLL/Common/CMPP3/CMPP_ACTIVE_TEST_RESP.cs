using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_ACTIVE_TEST_RESP : CMPP_Response
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
