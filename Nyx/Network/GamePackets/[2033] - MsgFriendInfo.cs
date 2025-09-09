using System;

namespace Nyx.Server.Network.GamePackets
{
    public class KnownPersonInfo : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public KnownPersonInfo(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[52];
                Writer.Write(44, 0, Buffer);
                Writer.Write(2033, 2, Buffer);
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint Mesh
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public byte Level
        {
            get { return Buffer[12]; }
            set { Buffer[12] = value; }
        }

        public byte Class //This is for friends
        {
            get { return Buffer[13]; }
            set { Buffer[13] = value; }
        }

        public ushort PKPoints
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set {Writer.Write(value, 14, Buffer); }
        }

        public uint GuildID
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public uint GuildRank
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public string Spouse
        {
            get
            {
                return System.Text.Encoding.Default.GetString(Buffer, 26, 16);
            }
            set
            {
                Writer.Write(value, 26, Buffer);
            }
        }

        public bool Enemy
        {
            get { return Buffer[42] == 1; }
            set { Buffer[42] = value == true ? (byte)1 : (byte)0; }
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

        public void Fill(Interfaces.IKnownPerson person, bool enemy, bool tradepartner)
        {
            if (person.IsOnline)
            {
                UID = person.Client.Entity.UID;
                Mesh = person.Client.Entity.Mesh;
                Level = person.Client.Entity.Level;
                Class = person.Client.Entity.Class;
                PKPoints = person.Client.Entity.PKPoints;
                Spouse = person.Client.Entity.Spouse;
                if (person.Client.Guild != null)
                {
                    if (person.Client.AsMember != null)
                    {
                        GuildID = person.Client.Guild.ID;
                        GuildRank = (ushort)person.Client.AsMember.Rank;
                    }
                }
                Enemy = enemy;
                if (tradepartner)
                    Writer.Write(2047, 2, Buffer);
                
            }
        }
    }
}
