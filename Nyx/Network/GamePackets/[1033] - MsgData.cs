using System;

namespace Nyx.Server.Network.GamePackets
{
    public class ServerTime : Writer, Interfaces.IPacket
    {
        public ServerTime()
        {
            Buffer = new byte[40 + 8];
            Writer.Write(40, 0, Buffer);
            Writer.Write(1033, 2, Buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
        }
        byte[] Buffer;
        public uint Year
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Writer.Write(value - 1900, 12, Buffer); }
        }

        public uint Month
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Writer.Write(value - 1, 16, Buffer); }
        }

        public uint DayOfYear
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Writer.Write(value, 20, Buffer); }
        }
        public uint DayOfMonth
        {
            get { return BitConverter.ToUInt32(Buffer, 24); }
            set { Writer.Write(value, 24, Buffer); }
        }
        public uint Hour
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Writer.Write(value, 28, Buffer); }
        }
        public uint Minute
        {
            get { return BitConverter.ToUInt32(Buffer, 32); }
            set { Writer.Write(value, 32, Buffer); }
        }
        public uint Second
        {
            get { return BitConverter.ToUInt32(Buffer, 36); }
            set { Writer.Write(value, 36, Buffer); }
        }
        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
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
