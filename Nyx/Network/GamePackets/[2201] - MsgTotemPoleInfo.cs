using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Game.ConquerStructures.Society;

namespace Nyx.Server.Network.GamePackets
{
    public class ArsenalTab : Writer, Interfaces.IPacket
    {
        byte[] Buffer;
        public ArsenalTab(bool create)
        {
            if (create)
            {
                Buffer = new byte[252];
                Writer.Write(244, 0, Buffer);
                Writer.Write(2201, 2, Buffer);
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint SharedBattlepower
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        
        public uint HeroDonation
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint dwParam2
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint HeroSharedBattlepower
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public uint ArsenalCount
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public void AppendArsenal(Arsenal arsenal)
        {
            int offset = 28 + 24 * (arsenal.Position - 1);
            if (!arsenal.Unlocked)
            {
                offset += 16;
               Writer.Write(arsenal.Unlocked ? 1 : 0, offset, Buffer); offset += 4;
                Write(arsenal.Position, offset, Buffer); offset += 4;
            }
            else
            {
                Write(arsenal.SharedBattlePower, offset, Buffer); offset += 4;
                Write(arsenal.Enhancement, offset, Buffer); offset += 4;
                Write(arsenal.Donation, offset, Buffer); offset += 4;
               Writer.Write(arsenal.EnhancementExpirationDate(), offset, Buffer); offset += 4;
               Writer.Write(arsenal.Unlocked ? 1 : 0, offset, Buffer); offset += 4;
                Write(arsenal.Position, offset, Buffer); offset += 4;
            }
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Encode()
        {
            return Buffer;
        }
    }
    public class ArsenalView : Writer, Interfaces.IPacket
    {
        public const uint
            Unlock = 0,
            Inscribe = 1,
            View = 4;

        byte[] Buffer;

        public ArsenalView(bool create, uint itemCount = 0)
        {
            if (create)
            {
                Buffer = new byte[56 + itemCount * 40];
                Writer.Write((ushort)(48 + itemCount * 40), 0, Buffer);
                Writer.Write(2202, 2, Buffer);
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint BeginAt
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint EndAt
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint ArsenalType
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public int TotalInscribed
        {
            get { return BitConverter.ToInt32(Buffer, 20); }
            set { Writer.Write(value, 20, Buffer); }
        }

        public uint SharedBattlepower
        {
            get { return BitConverter.ToUInt32(Buffer, 24); }
            set { Write(value, 24, Buffer); }
        }

        public uint Enchantment
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Write(value, 28, Buffer); }
        }

        public int EnchantmentExpirationDate
        {
            get { return BitConverter.ToInt32(Buffer, 32); }
            set { Writer.Write(value, 32, Buffer); }
        }

        public uint Donation
        {
            get { return BitConverter.ToUInt32(Buffer, 36); }
            set { Write(value, 36, Buffer); }
        }

        public uint Count
        {
            get { return BitConverter.ToUInt32(Buffer, 40); }
            set { Write(value, 40, Buffer); }
        }

        public void AppendItem(Arsenal.ArsenalItem item)
        {
            int offset = (int)(44 + 40 * Count); Count++;
            Write(item.UID, offset, Buffer); offset += 4;
            Write(item.Rank, offset, Buffer); offset += 4;
            Writer.Write(item.Owner, offset, Buffer); offset += 16;
            Write(item.ID, offset, Buffer); offset += 4;
            Buffer[offset] = (byte)(item.ID % 10); offset++;
            Buffer[offset] = item.Plus; offset++;
            Buffer[offset] = item.SocketOne; offset++;
            Buffer[offset] = item.SocketTwo; offset++;
            Write(item.BattlePower, offset, Buffer); offset += 4;
            Write(item.DonationWorth, offset, Buffer); offset += 4;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Encode()
        {
            return Buffer;
        }
    }
    public class ArsenalCommand : Writer, Interfaces.IPacket
    {
        public const uint
            Unlock = 0,
            Inscribe = 1,
            Uninscribe = 2,
            Enchant = 3,
            View = 4;

        byte[] Buffer;
        public ArsenalCommand()
        {

        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint dwParam2
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint dwParam3
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Encode()
        {
            return Buffer;
        }
    }
}
