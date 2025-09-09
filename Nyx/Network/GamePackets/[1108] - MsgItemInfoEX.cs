using System;
using System.Drawing;
using Nyx.Server.Game;

namespace Nyx.Server.Network.GamePackets
{
    public class BoothItem2 : Interfaces.IPacket
    {
        private Byte[] mData;
        public void ParseItem(ConquerItem i)
        {
            ItemID = i.ID;
            ItemIdentifier = i.UID;
            Durability = i.Durability;
            MaxDurability = i.MaximDurability;
            SocketProgress = i.SocketProgress;
            SocketOne = (Byte)i.SocketOne;
            SocketTwo = (Byte)i.SocketTwo;
            Effect = (UInt16)i.Effect;
            Plus = i.Plus;
            Bless = i.Bless;
            Bound = i.Bound;
            Enchant = i.Enchant;
            Suspicious = i.Suspicious;
            Lock = Convert.ToBoolean(i.Lock);
            Color = (UInt32)i.Color;
            PlusProgress = i.PlusProgress;
            StackSize = i.StackSize;
            TimeLeftInMinutes = i.TimeLeftInMinutes;
            Signature = i.Signature;
            Stars = i.Stars;
            PerfectionProgress = i.PerfectionProgress;
            OwnerUID = i.OwnerUID;
            Owner = i.Owner;
        }
        public BoothItem2()
        {
            mData = new Byte[143 + 8];
            Writer.Write(((UInt16)(mData.Length - 8)), 0, mData);
            Writer.Write((UInt16)1108, 2, mData);
        }
        public UInt32 ItemIdentifier
        {
            get { return BitConverter.ToUInt32(mData, 4); }
            set { Writer.Write(value, 4, mData); }
        }
        public UInt32 Identifier
        {
            get { return BitConverter.ToUInt32(mData, 8); }
            set { Writer.Write(value, 8, mData); }
        }
        public UInt32 Cost
        {
            get { return BitConverter.ToUInt32(mData, 12); }
            set { Writer.Write(value, 12, mData); }
        }
        public UInt32 ItemID
        {
            get { return BitConverter.ToUInt32(mData, 16 + 4); }
            set { Writer.Write(value, 16 + 4, mData); }
        }
        public UInt16 Durability
        {
            get { return BitConverter.ToUInt16(mData, 20 + 4); }
            set { Writer.Write(value, 20 + 4, mData); }
        }
        public UInt16 MaxDurability
        {
            get { return BitConverter.ToUInt16(mData, 22 + 4); }
            set { Writer.Write(value, 22 + 4, mData); }
        }
        public CostTypes CostType
        {
            get { return (CostTypes)BitConverter.ToUInt16(mData, 24 + 4); }
            set { Writer.Write((UInt16)value, 24 + 4, mData); }
        }
        public PacketHandler.Positions Position
        {
            get { return (PacketHandler.Positions)mData[26 + 4]; }
            set { mData[26 + 4] = (Byte)value; }
        }
        public UInt32 SocketProgress
        {
            get { return BitConverter.ToUInt32(mData, 28 + 3); }
            set { Writer.Write(value, 28 + 3, mData); }
        }
        public Byte SocketOne
        {
            get { return mData[35]; }
            set { mData[35] = value; }
        }
        public Byte SocketTwo
        {
            get { return mData[36]; }
            set { mData[36] = value; }
        }
        public UInt16 Effect
        {
            get { return BitConverter.ToUInt16(mData, 36 + 1); }
            set { Writer.Write((UInt16)value, 36 + 1, mData); }
        }
        public Byte Plus
        {
            get { return mData[42]; }
            set { mData[42] = value; }
        }
        public Byte Bless
        {
            get { return mData[42 + 1]; }
            set { mData[42 + 1] = value; }
        }
        public Boolean Bound
        {
            get { return mData[43 + 1] == 0 ? false : true; }
            set { mData[43 + 1] = (Byte)(value ? 1 : 0); }
        }
        public Byte Enchant
        {
            get { return mData[44 + 1]; }
            set { mData[44 + 1] = value; }
        }
        public Boolean Suspicious
        {
            get { return mData[53 + 1] == 0 ? false : true; }
            set { mData[53 + 1] = (Byte)(value ? 1 : 0); }
        }
        public Boolean Lock
        {
            get { return mData[54 + 1] == 0 ? false : true; }
            set { mData[54 + 1] = (Byte)(value ? 1 : 0); }
        }
        public UInt32 Color
        {
            get { return BitConverter.ToUInt32(mData, 54); }
            set { Writer.Write((UInt32)value, 54, mData); }
        }
        public UInt32 PlusProgress
        {
            get { return BitConverter.ToUInt32(mData, 56); }
            set { Writer.Write(value, 56, mData); }
        }
        public uint TimeLeftInMinutes
        {
            get { return BitConverter.ToUInt32(mData, 68); }
            set { Writer.Write(value, 68, mData); }
        }
        public UInt16 StackSize
        {
            get { return BitConverter.ToUInt16(mData, 72); }
            set { Writer.Write(value, 72, mData); }
        }
        public UInt32 PurificationID
        {
            get { return BitConverter.ToUInt32(mData, 78); }
            set { Writer.Write(value, 78, mData); }
        }
        public byte Stars
        {
            get { return mData[82]; }
            set { mData[82] = value; }
        }
        public uint OwnerUID
        {
            get { return BitConverter.ToUInt32(mData, 90); }
            set { Writer.Write(value, 90, mData); }
        }
        public string Owner
        {
            get { return System.Text.Encoding.Default.GetString(mData, 94, 16); }
            set { Writer.Write(value, 94, mData); }
        }
        public string Signature
        {
            get { return System.Text.Encoding.Default.GetString(mData, 110, 36); }
            set { Writer.Write(value, 110, mData); }
        }
        public uint PerfectionProgress
        {
            get { return BitConverter.ToUInt32(mData, 86); }
            set { Writer.Write(value, 86, mData); }
        }
        public byte[] Encode()
        {
            return mData;
        }
        public void Deserialize(byte[] buffer)
        {
            mData = buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(mData);
        }
        public enum CostTypes : ushort
        {
            Gold = 1,
            CPs = 3,
            ViewEquip = 4
        }
    }
    public class BoothItem : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        public BoothItem(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[143 + 8];
                Writer.Write(143, 0, Buffer);
                Writer.Write(1108, 2, Buffer);
            }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint BoothID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint Cost
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public uint ID
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }
        public ushort Durability
        {
            get { return BitConverter.ToUInt16(Buffer, 24); }
            set {Writer.Write(value, 24, Buffer); }
        }
        public ushort MaximDurability
        {
            get { return BitConverter.ToUInt16(Buffer, 26); }
            set {Writer.Write(value, 26, Buffer); }
        }
        public uint CostType
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Write(value, 28, Buffer); }
        }
        public uint SocketProgress
        {
            get { return BitConverter.ToUInt32(Buffer, 28 + 3); }
            set { Write(value, 28 + 3, Buffer); }
        }
        public Enums.Gem SocketOne
        {
            get { return (Enums.Gem)Buffer[35]; }
            set { Buffer[35] = (byte)value; }
        }
        public Enums.Gem SocketTwo
        {
            get { return (Enums.Gem)Buffer[36]; }
            set { Buffer[36] = (byte)value; }
        }
        public Enums.ItemEffect Effect
        {
            get { return (Enums.ItemEffect)BitConverter.ToUInt16(Buffer, 37); }
            set {Writer.Write((ushort)value, 37, Buffer); }
        }
        public byte Plus
        {
            get { return Buffer[42]; }
            set { Buffer[42] = value; }
        }
        public byte Bless
        {
            get { return Buffer[43]; }
            set { Buffer[43] = value; }
        }
        public bool Bound
        {
            get { return Buffer[44] == 0 ? false : true; }
            set { Buffer[44] = (byte)(value ? 1 : 0); }
        }
        public byte Enchant
        {
            get { return Buffer[45]; }
            set { Buffer[45] = value; }
        }
        public bool Suspicious
        {
            get { return Buffer[54] == 0 ? false : true; }
            set { Buffer[54] = (byte)(value ? 1 : 0); }
        }
        public byte Lock
        {
            get { return Buffer[55]; }
            set { Buffer[55] = value; }
        }
        public Enums.Color Color
        {
            get { return (Enums.Color)BitConverter.ToUInt32(Buffer, 54); }
            set { Write((uint)value, 54, Buffer); }
        }
        public uint PlusProgress
        {
            get { return BitConverter.ToUInt32(Buffer, 56); }
            set { Write(value, 56, Buffer); }
        }
        public bool Inscribed
        {
            get { return (BitConverter.ToUInt16(this.Buffer, 65) == 1); }
            set { Writer.Write(value ? ((byte)1) : ((byte)0), 65, this.Buffer); }
        }
        public uint TimeLeftInMinutes
        {
            get { return BitConverter.ToUInt32(Buffer, 68); }
            set { Writer.Write(value, 68, Buffer); }
        }
        public ushort StackSize
        {
            get { return BitConverter.ToUInt16(Buffer, 72); }
            set {Writer.Write(value, 72, Buffer); }
        }
        public uint PurificationID
        {
            get { return BitConverter.ToUInt32(Buffer, 76); }
            set { Write(value, 76, Buffer); }
        }
        public byte Stars
        {
            get { return Buffer[82]; }
            set { Buffer[82] = value; }
        }
        public uint OwnerUID
        {
            get { return BitConverter.ToUInt32(Buffer, 90); }
            set { Writer.Write(value, 90, Buffer); }
        }
        public string Owner
        {
            get { return System.Text.Encoding.Default.GetString(Buffer, 94, 16); }
            set { Writer.Write(value, 94, Buffer); }
        }
        public string Signature
        {
            get { return System.Text.Encoding.Default.GetString(Buffer, 110, 36); }
            set { Writer.Write(value, 110, Buffer); }
        }
        public uint PerfectionProgress
        {
            get { return BitConverter.ToUInt32(Buffer, 86); }
            set { Writer.Write(value, 86, Buffer); }
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
        public override int GetHashCode()
        {
            return (int)this.UID;
        }
        public void Fill(Game.ConquerStructures.BoothItem item, uint boothID)
        {
            UID = item.Item.UID;
            BoothID = boothID;
            Cost = item.Cost;
            ID = item.Item.ID;
            Durability = item.Item.Durability;
            MaximDurability = item.Item.MaximDurability;
            CostType = (byte)item.Cost_Type;
            SocketOne = item.Item.SocketOne;
            SocketTwo = item.Item.SocketTwo;
            Effect = item.Item.Effect;
            Bound = item.Item.Bound;
            Plus = item.Item.Plus;
            Bless = item.Item.Bless;
            Enchant = item.Item.Enchant;
            SocketProgress = item.Item.SocketProgress;
            Color = item.Item.Color;
            PlusProgress = item.Item.PlusProgress;
            StackSize = item.Item.StackSize;
            Inscribed = item.Item.Inscribed;
            TimeLeftInMinutes = item.Item.TimeLeftInMinutes;
            Signature = item.Item.Signature;
            Stars = item.Item.Stars;
            PerfectionProgress = item.Item.PerfectionProgress;
            OwnerUID = item.Item.OwnerUID;
            Owner = item.Item.Owner;
        }
        public void Fill(ConquerItem item, uint boothID)
        {
            UID = item.UID;
            BoothID = boothID;
            ID = item.ID;
            Durability = item.Durability;
            MaximDurability = item.MaximDurability;
            Buffer[24] = (byte)4;
            Buffer[26] = (byte)item.Position;
            SocketOne = item.SocketOne;
            SocketTwo = item.SocketTwo;
            Effect = item.Effect;
            Plus = item.Plus;
            Bound = item.Bound;
            Bless = item.Bless;
            Enchant = item.Enchant;
            SocketProgress = item.SocketProgress;
            Color = item.Color;
            PlusProgress = item.PlusProgress;
            StackSize = item.StackSize;
            Inscribed = item.Inscribed;
            TimeLeftInMinutes = item.TimeLeftInMinutes;
            Signature = item.Signature;
            Stars = item.Stars;
            PerfectionProgress = item.PerfectionProgress;
            OwnerUID = item.OwnerUID;
            Owner = item.Owner;
        }
    }
}