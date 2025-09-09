using System;

namespace Nyx.Server.Network.GamePackets
{
    public class Vigor : Writer, Interfaces.IPacket
    {
        byte[] Buffer = null;

        public Vigor(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[48];
                Writer.Write(40, 0, Buffer);
                Writer.Write(1033, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
                Type = 2;
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint Amount
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            if (Buffer != null)
                client.Send(Buffer);
        }
    }
}
