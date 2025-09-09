using System;
namespace Nyx.Server.Network.GamePackets
{
    public class RequestInformation : Writer, Interfaces.IPacket
    {
        byte[] Buffer;
        public const uint Request = 1;
        public RequestInformation()
        {
            Buffer = new byte[40];
            Writer.Write(32, 0, Buffer);
            Writer.Write(2071, 2, Buffer);
        }
        public uint Requester
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint Receiver
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint Level
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public uint BattlePower
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }
        public uint Spouse
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }
        public uint Friends
        {
            get { return BitConverter.ToUInt32(Buffer, 21); }
            set { Write(value, 21, Buffer); }
        }
        public uint TradePartner
        {
            get { return BitConverter.ToUInt32(Buffer, 22); }
            set { Write(value, 22, Buffer); }
        }
        public uint Mentor
        {
            get { return BitConverter.ToUInt32(Buffer, 23); }
            set { Write(value, 23, Buffer); }
        }
        public uint Apprentice
        {
            get { return BitConverter.ToUInt32(Buffer, 24); }
            set { Write(value, 24, Buffer); }
        }
        public uint Team
        {
            get { return BitConverter.ToUInt32(Buffer, 25); }
            set { Write(value, 25, Buffer); }
        }
        public uint GuildRank
        {
            get { return BitConverter.ToUInt32(Buffer, 26); }
            set { Write(value, 26, Buffer); }
        }
        public uint Enemy
        {
            get { return BitConverter.ToUInt32(Buffer, 27); }
            set { Write(value, 27, Buffer); }
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