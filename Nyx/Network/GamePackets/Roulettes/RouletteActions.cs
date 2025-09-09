using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class RouletteActions : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;

        public enum RoulettType
        {
            Info = 5,
            Join = 0,
            Watch = 4,
        }

        public RouletteActions(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[9 + 8];
                Writer.Write(9, 0, Buffer);
                Writer.Write(2805, 2, Buffer);
            }
        }

        public RoulettType Type
        {
            get { return (RoulettType) Buffer[4]; }
            set { Buffer[4] = (byte) value; }
        }

        public uint TableID
        {
            get { return BitConverter.ToUInt32(Buffer, 5); }
            set { Write((ushort) value, 5, Buffer); }
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
            this.Buffer = buffer;
        }
    }
}