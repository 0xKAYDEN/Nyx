using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class Enlight : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public Enlight(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[40];
                Writer.Write(32, 0, Buffer);
                Writer.Write(1127, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            }
        }

        public uint EnlighterUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint EnlightedUID
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
}
