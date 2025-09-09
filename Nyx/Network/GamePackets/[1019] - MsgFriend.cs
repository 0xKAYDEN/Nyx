using System;

namespace Nyx.Server.Network.GamePackets
{
    public class KnownPersons : Writer, Interfaces.IPacket
    {
        public const byte
            RequestFriendship = 10,
            RemovePerson = 14,
            AcceptFriend = 11,
            AddFriend = 15,
            RemoveEnemy = 18,
            AddEnemy = 19;

        byte[] Buffer;

        public KnownPersons(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[80];
                Writer.Write(72, 0, Buffer);
                Writer.Write(1019, 2, Buffer);
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public byte Type
        {
            get { return Buffer[8]; }
            set { Buffer[8] = value; }
        }

        public bool Online
        {
            get { return Buffer[9] == 1; }
            set { Buffer[9] = value == true ? (byte)1 : (byte)0; }
        }

        public byte Unknowen
        {
            get { return Buffer[16]; }
            set { Buffer[16] = value; }
        }
        public string Name
        {
            get
            {
                return System.Text.Encoding.Default.GetString(Buffer, 20, 16);
            }
            set
            {
                Writer.Write(value, 20, Buffer);
            }
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