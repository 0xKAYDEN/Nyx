using System;
using System.Drawing;
using Nyx.Server.Game;
using System.Collections.Generic;
using System.IO;
namespace Nyx.Server.Network.GamePackets
{
    public class ConquerItem : Writer, Interfaces.IPacket
    {
        public bool InWardrobe;

        public const ushort
               Inventory = 0,
               Head = 1,
               Necklace = 2,
               Armor = 3,
               RightWeapon = 4,
               LeftWeapon = 5,
               Ring = 6,
               Bottle = 7,
               Boots = 8,
               Garment = 9,
               Fan = 10,
               Tower = 11,
               Steed = 12,
               SteedCrop = 18,
               RightWeaponAccessory = 15,
               LeftWeaponAccessory = 16,
               SteedArmor = 17,
               Wing = 19,
               Wings = 19,
               Remove = 255,
               AlternateHead = 21,
               AlternateNecklace = 22,
               AlternateArmor = 23,
               AlternateRightWeapon = 24,
               AlternateLeftWeapon = 25,
               AlternateRing = 26,
               AlternateBottle = 27,
               AlternateBoots = 28,
               AlternateGarment = 29;

        public const uint GoldPrize = 2100075;
        public bool IsWorn = false;
        public byte[] Buffer;
        public static Counter ItemUID = new Counter(0);

        private UInt32 mRefineItem = 0;
        private DateTime mRefineryTime;
        public MsgAuction AuctionInfo;
        private ulong suspiciousStart = 0, unlockEnd = 0;
        private bool unlocking = false;
        private uint warehouse = 0;
        public Dictionary<uint, string> Agate_map;
        public DateTime RefineryStarted { get; set; }
        public ConquerItem(bool Create)
        {
            if (Create)
            {
                Agate_map = new Dictionary<uint, string>(10);
                Buffer = new byte[136 + 8];
                Writer.Write(136, 0, Buffer);
                Writer.Write(1008, 2, Buffer);
                Mode = Nyx.Server.Game.Enums.ItemMode.Default;
                StatsLoaded = false;
            }
        }
        ulong minutes = 0;
        public ulong Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
                Database.ConquerItemTable.UpdateMinutes(this);
            }
        }
        DateTime timestamp;
        public DateTime TimeStamp
        {
            get
            {
                return timestamp;
            }
            set
            {
                timestamp = value;
                Database.ConquerItemTable.UpdateTimeStamp(this);
            }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint ID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set
            {
                Write(value, 8, Buffer);
            }
        }
        public void SetID(uint ID)
        {
            Write(ID, 8, Buffer);
        }
        public ushort Durability
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }
        public ushort MaximDurability
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set { Writer.Write(value, 14, Buffer); }
        }
        public string Agate
        {
            get;
            set;
        }
        public Enums.ItemMode Mode
        {
            get { return (Enums.ItemMode)BitConverter.ToUInt16(Buffer, 16); }
            set { Writer.Write((ushort)value, 16, Buffer); }
        }
        public ushort Position
        {
            get { return BitConverter.ToUInt16(Buffer, 18); }
            set { Writer.Write(value, 18, Buffer); }
        }
        public uint Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }
        public uint SocketProgress
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public uint PerfectionProgress
        {
            get { return BitConverter.ToUInt32(Buffer, 76); }
            set { Write(value, 76, Buffer); }
        }
        public Enums.Gem SocketOne
        {
            get { return (Enums.Gem)Buffer[24]; }
            set { Buffer[24] = (byte)value; }
        }
        public Enums.Gem SocketTwo
        {
            get { return (Enums.Gem)Buffer[25]; }
            set { Buffer[25] = (byte)value; }
        }
        public byte SocketOne2
        {
            get { return Buffer[24]; }
        }
        public byte SocketTwo2
        {
            get { return Buffer[25]; }
        }
        public Enums.ItemEffect Effect
        {
            get { return (Enums.ItemEffect)BitConverter.ToUInt16(Buffer, 28); }
            set { Writer.Write((ushort)value, 28, Buffer); }
        }
        public byte Plus
        {
            get { return Buffer[33]; }
            set { Buffer[33] = value; }
        }
        public byte Bless
        {
            get { return Buffer[34]; }
            set { Buffer[34] = value; }
        }
        public bool Bound
        {
            get { return Buffer[35] == 0 ? false : true; }
            set { Buffer[35] = (byte)(value ? 1 : 0); }
        }
        public byte Enchant
        {
            get { return Buffer[36]; }
            set { Buffer[36] = value; }
        }

        public byte NextRed
        {
            get { return Buffer[34]; }
            set { Buffer[34] = value; }
        }
        public byte NextBlue
        {
            get { return Buffer[36]; }
            set { Buffer[36] = value; }
        }
        public byte NextGreen
        {
            get { return Buffer[40]; }
            set { Buffer[40] = value; }
        }

        public bool Suspicious
        {
            get { return Buffer[44] == 0 ? false : true; }
            set { Buffer[44] = (byte)(value ? 1 : 0); }
        }

        public byte Lock
        {
            get { return Buffer[46]; }
            set { Buffer[46] = value; }
        }

        public Enums.Color Color
        {
            get { return (Enums.Color)BitConverter.ToUInt32(Buffer, 48); }
            set { Write((uint)value, 48, Buffer); }
        }
        public uint PlusProgress
        {
            get { return BitConverter.ToUInt32(Buffer, 52); }
            set { Write(value, 52, Buffer); }
        }
        public uint TimeLeftInMinutes
        {
            get { return BitConverter.ToUInt32(Buffer, 60); }
            set { Write(value, 60, Buffer); }
        }
        public bool Inscribed
        {
            get
            {
                return (BitConverter.ToUInt16(this.Buffer, 56) == 1);
            }
            set
            {
                Writer.Write(value ? ((byte)1) : ((byte)0), 56, this.Buffer);
            }
        }
        public ushort StackSize
        {
            get { return BitConverter.ToUInt16(Buffer, 68); }
            set { Writer.Write(value, 68, Buffer); }
        }
        public byte Stars
        {
            get { return Buffer[72]; }
            set { Buffer[72] = value; }
        }
        public uint OwnerUID
        {
            get { return BitConverter.ToUInt32(Buffer, 80); }
            set { Writer.Write(value, 80, Buffer); }
        }
        public string Owner
        {
            get { return System.Text.Encoding.Default.GetString(Buffer, 84, 16); }
            set { Writer.Write(value, 84, Buffer); }
        }
        public string Signature
        {
            get { return System.Text.Encoding.Default.GetString(Buffer, 100, 36); }
            set { Writer.Write(value, 100, Buffer); }
        }
        public ushort MaxStackSize
        {
            get;
            set;
        }
        public DateTime SuspiciousStart
        {
            get { return DateTime.FromBinary((long)suspiciousStart); }
            set { suspiciousStart = (ulong)value.Ticks; }
        }
        public DateTime UnlockEnd
        {
            get { return DateTime.FromBinary((long)unlockEnd); }
            set { unlockEnd = (ulong)value.Ticks; }
        }

        public bool Unlocking
        {
            get { return unlocking; }
            set { unlocking = value; }
        }
        public bool MobDropped
        {
            get;
            set;
        }
        public bool StatsLoaded
        {
            get;
            set;
        }
        public UInt32 RefineItem
        {
            get { return mRefineItem; }
            set { mRefineItem = value; }
        }
        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            throw new NotImplementedException();
        }
        public Double GemBonus(Byte type)
        {
            Double bonus = 0;
            if (SocketOne != Enums.Gem.NoSocket)
            {
                if ((Int32)SocketOne / 10 == type)
                    bonus += ItemSocket.GetGemBonus((Byte)SocketOne);
                if (SocketTwo != Enums.Gem.NoSocket)
                {
                    if ((Int32)SocketTwo / 10 == type)
                        bonus += ItemSocket.GetGemBonus((Byte)SocketTwo);
                }
            }
            return bonus;
        }
        public override string ToString()
        {
            return ID.ToString() + "#"
                + Durability.ToString() + "#"
                + MaximDurability.ToString() + "#"
                + Position.ToString() + "#"
                + SocketProgress.ToString() + "#"
                + ((byte)SocketOne).ToString() + "#"
                + ((byte)SocketTwo).ToString() + "#"
                + ((ushort)Effect).ToString() + "#"
                + Plus.ToString() + "#"
                + Bless.ToString() + "#"
                + (Bound ? "1" : "0") + "#"
                + Enchant.ToString() + "#"
                + (Suspicious ? "1" : "0") + "#"
                + Lock.ToString() + "#"
                + (Unlocking ? "1" : "0") + "#"
                + PlusProgress.ToString() + "#"
                + (Inscribed ? "1" : "0") + "#"
                + suspiciousStart.ToString() + "#"
                + unlockEnd.ToString();
        }
        public ItemAdding.Purification_ Purification;
        public ItemAdding.Refinery_ ExtraEffect;
        public void SendExtras(Client.GameClient client)
        {
            if (client == null)
                return;

            if (RefineItem != 0)
            {
                Refinery.RefineryItem rI = RefineStats;
                if (rI != null)
                {
                    client.Send(new Game_ItemSoul()
                    {
                        ID = rI.Identifier,
                        Identifier = UID,
                        Level = rI.Level,
                        Mode = Game_ItemSoul.Types.Refine,
                        Percent = rI.Percent,
                        Type = 1,
                        Time = (UInt32)(RefineryTime.Subtract(DateTime.Now).TotalSeconds)
                    });
                }
            }

            ItemAdding add = new ItemAdding(true);
            if (Purification.Available)
                add.Append(Purification);
            if (ExtraEffect.Available)
                add.Append(ExtraEffect);
            if (Purification.Available || ExtraEffect.Available)
                client.Send(add);

            if (Lock == 2)
            {
                ItemLock itemLock = new ItemLock(true);
                itemLock.UID = UID;
                itemLock.ID = ItemLock.UnlockDate;
                itemLock.dwParam = (uint)(UnlockEnd.Year * 10000 + UnlockEnd.Month * 100 + UnlockEnd.Day);
                client.Send(itemLock);
            }
        }
        public void SendAgate(Client.GameClient client)
        {
            byte[] packet = new byte[8 + 32 + 48 * Agate_map.Count + 48];
            Writer.Write((ushort)(packet.Length - 8), 0, packet);
            Writer.Write(2110, 2, packet);
            Write(this.UID, 8, packet);
            Write((byte)Agate_map.Count, 12, packet);
            Write((byte)Agate_map.Count, 16, packet);
            Write(Durability, 24, packet);
            Write((byte)Agate_map.Count, 28, packet);
            if (Agate_map.Count > 0)
            {
                int position_next = 32;
                uint x = 0;
                for (; x < Agate_map.Count; x++)
                {
                    Write(x, position_next, packet);
                    position_next += 4;
                    Writer.Write(ushort.Parse(Agate_map[x].Split('~')[0].ToString()), position_next, packet);
                    position_next += 4;
                    Writer.Write(ushort.Parse(Agate_map[x].Split('~')[1].ToString()), position_next, packet);
                    position_next += 4;
                    Writer.Write(ushort.Parse(Agate_map[x].Split('~')[2].ToString()), position_next, packet);
                    position_next += 36;
                }
            }
            client.Send(packet);
        }
        public void Send(Client.GameClient client)
        {
            if (client == null)
                return;

            if (ID == 300000)
            {
                uint G = SocketProgress & 0xFF;
                uint B = (SocketProgress >> 8) & 0xFF;
                uint R = (SocketProgress >> 16) & 0xFF;

                if (NextRed == 0 && NextBlue == 0 && NextGreen == 0)
                {
                    NextRed = (byte)R;
                    NextBlue = (byte)B;
                    NextGreen = (byte)G;
                    Database.ConquerItemTable.UpdateNextSteedColor(this);
                }
            }
            if (Minutes > 0)
            {
                if (DateTime.Now >= TimeStamp.AddMinutes(Minutes))
                {
                    Database.ConquerItemTable.DeleteItem(UID);
                    client.Send(Database.ConquerItemInformation.BaseInformations[ID].Name + " has expired!");
                }
                TimeSpan Remain = TimeStamp.AddMinutes(Minutes) - DateTime.Now;
                TimeLeftInMinutes = (uint)Remain.TotalSeconds;
            }
            client.Send(Buffer);
            if (RefineItem != 0)
            {
                Refinery.RefineryItem rI = RefineStats;
                if (rI != null)
                {
                    client.Send(new Game_ItemSoul()
                    {
                        ID = rI.Identifier,
                        Identifier = UID,
                        Level = rI.Level,
                        Mode = Game_ItemSoul.Types.Refine,
                        Percent = rI.Percent,
                        Type = 1,
                        Time = (UInt32)(RefineryTime.Subtract(DateTime.Now).TotalSeconds)
                    });
                }
            }

            ItemAdding add = new ItemAdding(true);
            if (Purification.Available)
                add.Append(Purification);
            if (ExtraEffect.Available)
                add.Append(ExtraEffect);
            if (Purification.Available || ExtraEffect.Available)
                client.Send(add);

            if (Lock == 2 && (Mode == Enums.ItemMode.Default || Mode == Enums.ItemMode.Update))
            {
                ItemLock itemLock = new ItemLock(true);
                itemLock.UID = UID;
                itemLock.ID = ItemLock.UnlockDate;
                itemLock.dwParam = (uint)(UnlockEnd.Year * 10000 + UnlockEnd.Month * 100 + UnlockEnd.Day);
                client.Send(itemLock);
            }
            Mode = Enums.ItemMode.Default;

        }
        public static Boolean isRune(UInt32 itemid)
        {
            if (itemid >= 729960 && itemid <= 729970)
                return true;
            return false;
        }
        public ushort Vigor
        {
            get;
            set;
        }
        public short BattlePower
        {
            get
            {
                short potBase = 0;
                byte Quality = (byte)(ID % 10);
                if (Quality >= 5)
                    potBase += (byte)(Quality - 5);
                potBase += Plus;
                if (SocketOne != Enums.Gem.NoSocket) potBase++;
                if (SocketTwo != Enums.Gem.NoSocket) potBase++;
                if (((byte)SocketOne) % 10 == 3) potBase++;
                if (((byte)SocketTwo) % 10 == 3) potBase++;

                if (ID / 1000 == 421 || PacketHandler.IsTwoHand(ID))
                    potBase *= 2;

                return potBase;
            }
        }
        public override int GetHashCode()
        {
            return (int)this.UID;
        }
        public override bool Equals(object obj)
        {
            return (obj as ConquerItem).UID == GetHashCode();
        }
        public Refinery.RefineryItem RefineStats
        {
            get
            {
                Refinery.RefineryItem i = null;
                Kernel.DatabaseRefinery.TryGetValue(RefineItem, out i);
                return i;
            }
        }
        public DateTime RefineryTime
        {
            get { return mRefineryTime; }
            set { mRefineryTime = value; }
        }
        public bool IsTwoHander()
        {
            ItemTypes item_type = (ItemTypes)GetItemType();
            bool check = ((UInt16)item_type >= 500 && (UInt16)item_type <= 580 || (UInt16)item_type == 421 || (UInt16)item_type == 612);
            if (check)
            {
                check = (item_type != ItemTypes.ShieldID);
                if (check)
                {
                    check = (item_type != ItemTypes.NinjaSwordID);
                    if (check)
                    {
                        check = (item_type != ItemTypes.MonkBeadsID);
                    }
                    if (check)
                    {
                        check = (item_type != ItemTypes.Rapier);
                    }
                }
            }
            return check;
        }
        public ItemTypes GetItemType()
        {
            return (ItemTypes)(this.ID / 1000);
        }
        public static void CheckItemExtra(ConquerItem i, Client.GameClient c)
        {
            if (i.RefineryTime.Ticks != 0)
            {
                if (DateTime.Now > i.RefineryTime)
                {
                    i.RefineItem = 0;
                    i.RefineryTime = new DateTime(0);

                    Game_ItemSoul expire = new Game_ItemSoul()
                    {
                        Identifier = i.UID
                    };
                    expire.Expired(c);
                    i.Send(c);
                    WindowsStats WS = new WindowsStats(c);
                    WS.Send(c);

                    Database.ConquerItemTable.UpdateRefineryItem(i);
                    Database.ConquerItemTable.UpdateRefineryTime(i);

                    if (!c.Equipment.Free(i.Position))
                    {
                        //c.UnloadItemStats(i, true);
                        c.LoadItemStats();
                        c.ReshareClan();
                        if (c.Team != null)
                            foreach (var teammate in c.Team.Teammates)
                                teammate.ReshareClan();
                    }
                }
            }
        }
        public enum ItemTypes : ushort
        {
            BowID = 500,
            ArrowID = 1050,
            ShieldID = 900,
            BackswordID = 421,
            BladeID = 410,
            WingID = 204,
            SwordID = 420,
            NinjaSwordID = 601,
            MonkBeadsID = 610,
            FanID = 201,
            TowerID = 202,
            CropID = 203,
            GarmentID_1 = 181,
            GarmentID_2 = 182,
            BottleID = 2100,
            GemID = 700,
            PickaxeID = 562,
            RingID = 150,
            BootID = 160,
            SteedArmorID = 200,
            Rapier = 611,
            Pistol = 612,
            NecklaceID = 120,
            BraceletID = 152,
            BagID = 121,
            RightAccessory1 = 350,
            RightAccessory2 = 360,
            RightAccessory3 = 370,
            LeftAccessory = 380
        }
    }
    public  class ItemSocket
    {
        public const Byte
            Phoenix = 0,
            Dragon = 1,
            Fury = 2,
            Rainbow = 3,
            Kylin = 4,
            Violet = 5,
            Moon = 6,
            Tortoise = 7,
            Thunder = 10,
            Glory = 12,

            OpenSocket = 255,
            NoSocket = 0;

        /// <summary>
        /// Pass the entire gem's ID, not just the type.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Double GetGemBonus(Byte value)
        {
            switch (value)
            {
                case 71:
                    return 0.02;
                case 72:
                    return 0.04;
                case 1:
                case 11:
                case 21:
                    return 0.05;
                case 73:
                    return 0.06;
                case 2:
                case 12:
                case 22:
                case 31:
                    return 0.10;
                case 3:
                case 23:
                case 32:
                case 61:
                case 13:
                    return 0.15;
                case 33:
                    return 0.25;
                case 51:
                case 62:
                    return 0.30;
                case 41:
                case 52:
                case 63:
                    return 0.50;
                case 42:
                case 53:
                    return 1.00;
                case 43:
                    return 2.00;

                case 101:
                case 121:
                    return 100;
                case 102:
                case 122:
                    return 300;
                case 103:
                case 123:
                    return 500;

                default:
                    return 0;
            }
        }
    }
}