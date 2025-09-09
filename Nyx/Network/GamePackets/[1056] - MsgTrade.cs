using System;
using System.Linq;

namespace Nyx.Server.Network.GamePackets
{
    public class Trade : Writer, Interfaces.IPacket
    {
        public const ushort Request = 1,
            Close = 2,
            ShowTable = 3,
            HideTable = 5,
            AddItem = 6,
            SetMoney = 7,
            ShowMoney = 8,
            Accept = 10,
            RemoveItem = 11,
            ShowConquerPoints = 12,
            SetConquerPoints = 13;


        byte[] Buffer;

        public Trade(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[20];
                Writer.Write(12, 0, Buffer);
                Writer.Write(1056, 2, Buffer);
            }
        }
        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set {Writer.Write(value, 12, Buffer); }
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
    public class PopupLevelBP : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        public PopupLevelBP()
        {
            Buffer = new byte[40];
            Writer.Write(0x20, 0, Buffer);
            Writer.Write(0x817, 2, Buffer);
        }
        public PopupLevelBP(Client.GameClient Receiver, Client.GameClient Requester)
        {
            Buffer = new byte[40];
            Writer.Write(32, 0, Buffer);
            Writer.Write(2071, 2, Buffer);
            if (Receiver.Enemy.ContainsKey(Requester.Entity.UID))
            {
                Enemy = 1;
            }
            if (Receiver.Friends.ContainsKey(Requester.Entity.UID))
            {
                Friend = 1;
            }
            if (Receiver.Team != null)
            {
                if (Receiver.Team.Teammates.Contains(Requester))
                {
                    Team = 1;
                }
            }
            if (Receiver.Mentor != null)
            {
                if (Receiver.Mentor.Name == Requester.Entity.Name)
                {
                    Mentor = 1;
                }
            }

            if (Receiver.Apprentices.ContainsKey(Requester.Entity.UID))
            {
                Mentor = 1;
            }
            if (Receiver.Partners.ContainsKey(Requester.Entity.UID))
            {
                Partner = 1;
            }
            if (Receiver.Guild != null)
            {
                if (Receiver.Guild.Members.ContainsKey(Requester.Entity.UID))
                {
                    Guild = 1;
                }
            }
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

        public byte Marry
        {
            get { return Buffer[20]; }
            set { Write(value, 20, Buffer); }
        }

        public byte Friend
        {
            get { return Buffer[21]; }
            set { Write(value, 21, Buffer); }
        }

        public byte Enemy
        {
            get { return Buffer[27]; }
            set { Write(value, 27, Buffer); }
        }

        public byte Team
        {
            get { return Buffer[25]; }
            set { Write(value, 25, Buffer); }
        }

        public byte Guild
        {
            get { return Buffer[26]; }
            set { Write(value, 26, Buffer); }
        }

        public byte Partner
        {
            get { return Buffer[22]; }
            set { Write(value, 22, Buffer); }
        }

        public byte Mentor
        {
            get { return Buffer[23]; }
            set { Write(value, 23, Buffer); }
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
    public class TradePartnerInfo : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public TradePartnerInfo(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[64];
                Writer.Write(56, 0, Buffer);
                Writer.Write(2047, 2, Buffer);
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

        public byte Class
        {
            get { return Buffer[13]; }
            set { Buffer[13] = value; }
        }

        public ushort PKPoints
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set { Writer.Write(value, 14, Buffer); }
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

        public void Fill(Interfaces.IKnownPerson person)
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
            }
        }
    }
    public class TradePartner : Writer, Interfaces.IPacket
    {
        public const byte
            RequestPartnership = 0,
            RejectRequest = 1,
            BreakPartnership = 4,
            AddPartner = 5;

        byte[] Buffer;

        public TradePartner(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[40];
                Writer.Write(32, 0, Buffer);
                Writer.Write(2046, 2, Buffer);
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

        public int HoursLeft
        {
            get { return BitConverter.ToInt32(Buffer, 10); }
            set
            {
                if (value < 0)
                    value = 0;
                Writer.Write(value * 60, 10, Buffer);
            }
        }

        public string Name
        {
            get
            {
                return System.Text.Encoding.Default.GetString(Buffer, 16, 16);
            }
            set
            {
                Writer.Write(value, 16, Buffer);
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