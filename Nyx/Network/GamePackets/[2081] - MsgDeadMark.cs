using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets 
{
    public class BlackSpotPacket : Writer 
    {
        byte[] packet = new byte[20];
        public BlackSpotPacket()
        {
            Writer.Write(12, 0, packet);
            Writer.Write(2081, 2, packet);
        }

        public byte[] ToArray(bool show, uint uid) 
        {
            if (show)
            {
                Write(0, 4, packet);
            }
            else
            {
                Write(1, 4, packet);
            }
            Write(uid, 8, packet);
            return packet;
        }
    }
}
