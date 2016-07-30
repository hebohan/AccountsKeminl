using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Accounts.BLL.Common.CMPP3
{
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

}
