using Nyx.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Network.GamePackets
{
    public class EpicTaoIcon : Writer, Nyx.Server.Interfaces.IPacket
    {
        byte[] Buffer = null;
        public EpicTaoIcon(bool Creat)
        {
            Buffer = new byte[72];
            Writer.Write(64, 0, Buffer);
            Writer.Write(10017, 2, Buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
        }
        public uint Energy
        {
            get { return BitConverter.ToUInt32(Buffer, 56); }
            set { Write(value, 56, Buffer); }
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Nyx.Server.Client.GameClient client)
        {
            if (Buffer != null)
                client.Send(Buffer);
        }
    }
}