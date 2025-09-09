using System;

namespace Nyx.Server.Network.GamePackets
{
    public class MapStatus : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        public MapStatus()
        {
            Buffer = new byte[28];
            Writer.Write(20, 0, Buffer);
            Writer.Write(1110, 2, Buffer);
        }
        public uint ID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint BaseID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint Status
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer);  }
        }
        public uint Weather
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            throw new NotImplementedException();
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}
