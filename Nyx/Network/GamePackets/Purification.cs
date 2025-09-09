using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class Purification : Writer, Interfaces.IPacket
    {
        public const byte Purify = 1,
                            ItemArtifact = 0,
                            Stabilaze3 = 3;

        byte[] Buffer;

        public Purification(bool Create)
        {
            if (Create)
            {
                Buffer = null;
            }
        }

        public byte Mode
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public uint ItemUID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint AddUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
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
