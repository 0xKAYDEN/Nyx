using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Game.ConquerStructures;

namespace Nyx.Server.Network.GamePackets
{
    public class Chi : Writer, Interfaces.IPacket
    {
        public const byte
            Unlock = 0,
            QueryInfo = 1,
            Study = 2,
            BuyStrength = 3,
            BuyStrength2 = 6;

        private byte[] Buffer;

        public Chi(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[8 + 16];
                Writer.Write(16, 0, Buffer);
                Writer.Write(2533, 2, Buffer);
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set {Writer.Write(value, 8, Buffer); }
        }

        public Game.Enums.ChiPowerType Mode
        {
            get { return (Game.Enums.ChiPowerType)Buffer[10]; }
            set { Buffer[10] = (byte)value; }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 11); }
            set { Write(value, 11, Buffer); }
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
    public class ChiPowers : Writer, Interfaces.IPacket
    {
        public const byte
            SpawnWindow = 0,
            Update = 1;

        private byte[] Buffer;

        public ChiPowers(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[8 + 94];
                Writer.Write(94, 0, Buffer);
                Writer.Write(2534, 2, Buffer);
            }
        }

        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 6); }
            set { Write(value, 6, Buffer); }
        }

        public uint Points
        {
            get { return BitConverter.ToUInt32(Buffer, 10); }
            set { Write(value, 10, Buffer); }
        }

        public uint InfoLength
        {
            get { return BitConverter.ToUInt32(Buffer, 18); }
            set { Write(value, 18, Buffer); }
        }

        public ChiPowers AppendInfo(ICollection<ChiPowerStructure> infos)
        {
            foreach (var info in infos)
                AppendInfo(info);
            return this;
        }

        public ChiPowers AppendInfo(ChiPowerStructure info)
        {
            InfoLength++;
            int offset = (int)(22 + (InfoLength - 1) * 17);

            Buffer[offset] = (byte)info.Power;
            offset++;
            foreach (var attribute in info.Attributes)
            {
                Writer.Write(attribute, offset, Buffer);
                offset += 4;
            }
            return this;
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

        public ChiPowers Query(Client.GameClient client, bool update = true)
        {
            UID = client.Entity.UID;
            if (!update) Type = SpawnWindow;
            else Type = Update;
            Points = client.ChiPoints;
            AppendInfo(client.ChiPowers);
            return this;
        }
    }
    public class ChiRetreatRequest : Writer, Nyx.Server.Interfaces.IPacket
    {
        private byte[] Buffer;

        public enum RetreatType
        {
            Info = 0,
            RequestRetreat = 1,
            Retreat = 2,
            RequestRestore = 3,
            Restore = 4,
            RequestExtend = 5,
            Extend = 6,
            RequestPayoff = 7,
            Payoff = 8,
            RequestAbondan = 9,
            Abondan = 10,
            RequestUpdate = 11,
            Update = 12,
            RequestExtend2 = 13,
            Extend2 = 14,
        }

        public ChiRetreatRequest(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[7 + 8];
                Writer.Write(7, 0, Buffer);
                Writer.Write(2536, 2, Buffer);
            }
        }

        public RetreatType Type
        {
            get { return (RetreatType)BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write((ushort)value, 4, Buffer); }
        }

        public Nyx.Server.Game.Enums.ChiPowerType Mode
        {
            get { return (Nyx.Server.Game.Enums.ChiPowerType)Buffer[6]; }
            set { Buffer[6] = (byte)value; }
        }

        public void Send(Nyx.Server.Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
        }
    }
}