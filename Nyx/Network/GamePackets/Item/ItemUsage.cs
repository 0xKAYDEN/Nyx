using System;

namespace Nyx.Server.Network.GamePackets
{
    public class ItemUsage : Writer, Interfaces.IPacket
    {
        public uint[] Batch
        {
            get
            {
                uint[] items = new uint[dwExtraInfo];
                for (int i = 0; i < dwExtraInfo; i++)
                {
                    items[i] = BitConverter.ToUInt32(Buffer, 91 + 4 * i);
                }
                return items;
            }
            set
            {
                if (value != null)
                {
                    dwExtraInfo = (uint)value.Length;
                    for (int i = 0; i < dwExtraInfo; i++)
                    {
                        Write(value[i], 91 + 4 * i, Buffer);
                    }
                }
            }
        }
        public const byte
        BuyFromNPC = 1,
        SellToNPC = 2,
        RemoveInventory = 3,
        EquipItem = 4,
        Unknown5 = 5,
        UnequipItem = 6,
        ArrowReload = 8,
        ViewWarehouse = 9,
        WarehouseDeposit = 10,
        WarehouseWithdraw = 11,
        Repair = 14,
        VIPRepair = 15,
        DragonBallUpgrade = 19,
        MeteorUpgrade = 20,
        ShowBoothItems = 21,
        AddItemOnBoothForSilvers = 22,
        RemoveItemFromBooth = 23,
        BuyFromBooth = 24,
        UpdateDurability = 25,
        FireWorks = 26,
        AddItemOnBoothForConquerPoints = 29,
        Ping = 27,
        Enchant = 28,
        RedeemGear = 32,
        ClaimGear = 33,
        Unknown = 34,
        SocketTalismanWithItem = 35,
        SocketTalismanWithCPs = 36,
        DropItem = 37,
        DropMoney = 38,
        GemCompose = 39,
        ItemSell = 55,
        Bless = 40,
        Accessories = 41,
        MainEquipment = 44,
        AlternateEquipment = 45,
        ItemsReturned = 50,
        ToristSuper = 51,
        ChatItem = 52,
        ObtinedPoints = 53,
        LowerEquipment = 54,
        SocketerMan = 43,
        MergeStackableItems = 48,
        SplitStack = 49;

        private byte[] Buffer;

        public ItemUsage(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[112];
                Writer.Write(104, 0, Buffer);
                Writer.Write(1009, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint dwParam2
        {
            get { return BitConverter.ToUInt32(Buffer, 91); }
            set { Write(value, 91, Buffer); }
        }

        public byte ID
        {
            get
            {
                return Buffer[20];
            }
            set
            {
                Buffer[20] = value;
            }
        }

        public uint TimeStamp
        {
            get { return BitConverter.ToUInt32(Buffer, 22); }
            set { Write(value, 22, Buffer); }
        }

        public uint dwExtraInfo
        {
            get { return BitConverter.ToUInt32(Buffer, 26); }
            set { Write(value, 26, Buffer); }
        }

        public uint dwExtraInfo2
        {
            get { return BitConverter.ToUInt32(Buffer, 30); }
            set { Write(value, 30, Buffer); }
        }

        public uint dwExtraInfo3
        {
            get { return BitConverter.ToUInt32(Buffer, 34); }
            set { Write(value, 34, Buffer); }
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
    }
}