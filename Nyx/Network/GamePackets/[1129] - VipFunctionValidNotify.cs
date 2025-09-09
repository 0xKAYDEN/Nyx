using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class VipStatus
    {
        byte[] Buffer;
        public byte[] Encode()
        {
            return Buffer;
        }
        public VipStatus()
        {
            Buffer = new byte[16];
            Writer.Write(Buffer.Length - 8, 0, Buffer);
            Writer.Write(1129, 2, Buffer);
            Writer.Write(65535, 4, Buffer);
        }
    }
}
