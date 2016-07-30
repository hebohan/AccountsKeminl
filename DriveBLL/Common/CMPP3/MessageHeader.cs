using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounts.BLL.Common.CMPP3
{
    public class MessageHeader //消息头
    {
        public const int Length = 4 + 4 + 4;

        public CMPP_Command_Id Command_Id
        {
            get
            {
                return this._Command_Id;
            }
        }

        public uint Sequence_Id
        {
            get
            {
                return this._Sequence_Id;
            }
        }

        public uint Total_Length
        {
            get
            {
                return this._Total_Length;
            }
        }

        //private CMPP_Command_Id _Command_Id;
        //private uint _Sequence_Id;
        //private uint _Total_Length;

        uint _Total_Length; // 4 Unsigned Integer 消息总长度(含消息头及消息体)
        CMPP_Command_Id _Command_Id; // 4 Unsigned Integer 命令或响应类型
        uint _Sequence_Id; // 4 Unsigned Integer 消息流水号,顺序累加,步长为1,循环使用(一对请求和应答消息的流水号必须相同)

        public MessageHeader(uint Total_Length, CMPP_Command_Id Command_Id, uint Sequence_Id) //发送前
        {
            this._Command_Id = Command_Id;
            this._Sequence_Id = Sequence_Id;
            this._Total_Length = Total_Length;
        }

        public MessageHeader(byte[] bytes)
        {
            byte[] buffer = new byte[4];
            Buffer.BlockCopy(bytes, 0, buffer, 0, buffer.Length);
            Array.Reverse(buffer);  //为什么要反转？？ 将低位移到高位，符合数组顺序，转化为正确的uint。
            this._Total_Length = BitConverter.ToUInt32(buffer, 0);

            Buffer.BlockCopy(bytes, 4, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Command_Id = (CMPP_Command_Id)(BitConverter.ToUInt32(buffer, 0));

            Buffer.BlockCopy(bytes, 8, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            this._Sequence_Id = BitConverter.ToUInt32(buffer, 0);
        }


        public byte[] ToBytes()
        {
            byte[] bytes = new byte[MessageHeader.Length];

            byte[] buffer = BitConverter.GetBytes(this._Total_Length);//对uint采用的可能是右移位操作。
            Array.Reverse(buffer);  //这里为什么也要反转操作？将高位移到低位，符合MessageHeader的帧格式
            Buffer.BlockCopy(buffer, 0, bytes, 0, 4);

            buffer = BitConverter.GetBytes((uint)this._Command_Id);
            Array.Reverse(buffer);
            Buffer.BlockCopy(buffer, 0, bytes, 4, 4);

            buffer = BitConverter.GetBytes(this._Sequence_Id);
            Array.Reverse(buffer);
            Buffer.BlockCopy(buffer, 0, bytes, 8, 4);

            return bytes;
        }

    }

}
