using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_ACTIVE_TEST : CMPP_Request
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
}
