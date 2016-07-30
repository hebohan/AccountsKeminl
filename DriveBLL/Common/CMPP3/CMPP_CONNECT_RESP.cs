using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class CMPP_CONNECT_RESP : CMPP_Response
    {
        MessageHeader _Header;
        public const int _FixedBodyLength = 4 + 16 + 1;

        uint _Status;   // 4 Unsigned Integer 状态
                        //   0:正确
                        //   1:消息结构错
                        //   2:非法源地址
                        //   3:认证错
                        //   4:版本太高
                        //   5~:其他错误
        byte[] _AuthenticatorISMG;  // 16 Octet String ISMG认证码,用于鉴别ISMG。
                                    //   其值通过单向MD5 hash计算得出,表示如下:
                                    //   AuthenticatorISMG =MD5(Status+AuthenticatorSource+shared secret),Shared secret 由中国移动与源地址实体事先商定,AuthenticatorSource为源地址实体发送给ISMG的对应消息CMPP_Connect中的值。
                                    //    认证出错时,此项为空。
        uint _Version;              // 1 Unsigned Integer 服务器支持的最高版本号,对于3.0的版本,高4bit为3,低4位为0

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

}
