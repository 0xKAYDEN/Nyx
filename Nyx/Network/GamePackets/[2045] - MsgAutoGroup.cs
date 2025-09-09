using System;
using System.Drawing;
using Nyx.Server.Game;

namespace Nyx.Server.Network.GamePackets
{
    public class Leadership : Writer, Interfaces.IPacket
    {
        public const ushort
            Leader = 1,
            Teammate = 0;

        private byte[] Buffer;

        public Leadership()
        {
            Buffer = new byte[32 + 8];
            Writer.Write(32, 0, Buffer);
            Writer.Write(2045, 2, Buffer);
        }

        //Type = 1 for team
        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint LeaderUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public uint Count
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }
        public uint IsLeader
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
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
            client.Send(Buffer);
        }
    }
}