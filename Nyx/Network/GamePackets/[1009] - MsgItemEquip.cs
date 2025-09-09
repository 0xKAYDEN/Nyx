using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Client;
using Nyx.Server.Interfaces;

namespace Nyx.Server.Network.GamePackets
{
    public class ClientEquip : Interfaces.IPacket
    {
        private Byte[] mData;

        public ClientEquip()
        {
            this.mData = new Byte[100];
            Writer.Write((UInt16)(this.mData.Length - 8), 0, mData);
            Writer.Write((UInt16)1009, 2, mData);
            Writer.Write((uint)Time32.timeGetTime().GetHashCode(), 4, mData);
            Writer.Write((UInt16)46, 20, mData);
        }

        public ClientEquip(GameClient c)
        {
            this.mData = new Byte[100];
            Writer.Write((UInt16)(this.mData.Length - 8), 0, mData);
            Writer.Write((UInt16)1009, 2, mData);
            Writer.Write((uint)Time32.timeGetTime().GetHashCode(), 4, mData);
            Writer.Write((UInt16)46, 20, mData);

            DoEquips(c);
        }

        public void DoEquips(GameClient client)
        {
            if (client.Equipment == null) return;
            ConquerItem[] Items = client.Equipment.Objects;
            Writer.Write((uint)Time32.timeGetTime().GetHashCode(), 4, mData);
            Writer.Write(client.Entity.UID, 8, mData);
            AlternativeEquipment = client.AlternateEquipment;
            foreach (var Item in client.Equipment.Objects)
            {
                if (Item == null)
                    continue;
                if (Item.IsWorn)
                {
                    switch (Item.Position)
                    {
                        #region Equipment
                        case ConquerItem.Head:
                            this.Helm = Item.UID; break;
                        case ConquerItem.Necklace:
                            this.Necklace = Item.UID;
                            break;
                        case ConquerItem.Armor:
                            this.Armor = Item.UID;
                            break;
                        case ConquerItem.RightWeapon:
                            this.RHand = Item.UID;
                            break;
                        case ConquerItem.LeftWeapon:
                            this.LHand = Item.UID;
                            break;
                        case ConquerItem.Ring:
                            this.Ring = Item.UID;
                            break;
                        case ConquerItem.Boots:
                            this.Boots = Item.UID;
                            break;
                        case ConquerItem.Garment:
                            this.Garment = Item.UID;
                            break;
                        case ConquerItem.Bottle:
                            this.Talisman = Item.UID; break;
                        case ConquerItem.RightWeaponAccessory:
                            AccessoryOne = Item.UID;
                            break;
                        case ConquerItem.LeftWeaponAccessory:
                            AccessoryTwo = Item.UID;
                            break;
                        case ConquerItem.SteedArmor:
                            SteedArmor = Item.UID;
                            break;
                        case ConquerItem.SteedCrop:
                            SteedTalisman = Item.UID;
                            break;
                        case ConquerItem.Wings:
                            Wings = Item.UID;
                            break;
                        #endregion
                        #region AlternateEquipment
                        case ConquerItem.AlternateArmor:
                            Armor = Item.UID;
                            break;
                        case ConquerItem.AlternateHead:
                            Helm = Item.UID;
                            break;
                        case ConquerItem.AlternateNecklace:
                            Necklace = Item.UID;
                            break;
                        case ConquerItem.AlternateRing:
                            Ring = Item.UID;
                            break;
                        case ConquerItem.AlternateBoots:
                            Boots = Item.UID;
                            break;
                        case ConquerItem.AlternateBottle:
                            Talisman = Item.UID;
                            break;
                        case ConquerItem.AlternateGarment:
                            this.Garment = Item.UID;
                            break;
                        case ConquerItem.AlternateLeftWeapon:
                            LHand = Item.UID;
                            break;
                        case ConquerItem.AlternateRightWeapon:
                            RHand = Item.UID;
                            break;
                        #endregion
                    }
                }
            }
            if (client.ArmorLook > 0) Garment = Armor = uint.MaxValue - 1;
            if (client.HeadgearLook > 0) Helm = uint.MaxValue - 2;
        }

        public void Deserialize(byte[] buffer) { this.mData = buffer; }
        public byte[] Encode()
        { return mData; }
        public void Send(Client.GameClient client) { client.Send(mData); }


        public bool AlternativeEquipment
        {
            get { return this.mData[12] == 1 ? true : false; }
            set { this.mData[12] = value ? (byte)1 : (byte)0; }
        }

        public UInt32 Helm
        {
            get { return BitConverter.ToUInt32(this.mData, 35); }
            set { Writer.Write(value, 35, mData); }
        }

        public UInt32 Necklace
        {
            get { return BitConverter.ToUInt32(this.mData, 39); }
            set { Writer.Write(value, 39, mData); }
        }

        public UInt32 Armor
        {
            get { return BitConverter.ToUInt32(this.mData, 43); }
            set { Writer.Write(value, 43, mData); }
        }
        public uint Wings
        {
            get { return BitConverter.ToUInt32(mData, 87); }
            set { Writer.Write(value, 87, mData); }
        }
        public UInt32 RHand
        {
            get { return BitConverter.ToUInt32(this.mData, 47); }
            set { Writer.Write(value, 47, mData); }
        }

        public UInt32 LHand
        {
            get { return BitConverter.ToUInt32(this.mData, 51); }
            set { Writer.Write(value, 51, mData); }
        }

        public UInt32 Ring
        {
            get { return BitConverter.ToUInt32(this.mData, 55); }
            set { Writer.Write(value, 55, mData); }
        }

        public UInt32 Talisman
        {
            get { return BitConverter.ToUInt32(this.mData, 59); }
            set { Writer.Write(value, 59, mData); }
        }

        public UInt32 Boots
        {
            get { return BitConverter.ToUInt32(this.mData, 63); }
            set { Writer.Write(value, 63, mData); }
        }

        public UInt32 Garment
        {
            get { return BitConverter.ToUInt32(this.mData, 67); }
            set { Writer.Write(value, 67, mData); }
        }

        public uint AccessoryOne
        {
            get { return BitConverter.ToUInt32(mData, 71); }
            set { Writer.Write(value, 71, mData); }
        }

        public uint AccessoryTwo
        {
            get { return BitConverter.ToUInt32(mData, 75); }
            set { Writer.Write(value, 75, mData); }
        }
        public UInt32 SteedArmor
        {
            get { return BitConverter.ToUInt32(this.mData, 79); }
            set { Writer.Write(value, 79, mData); }
        }

        public UInt32 SteedTalisman
        {
            get { return BitConverter.ToUInt32(this.mData, 83); }
            set { Writer.Write(value, 83, mData); }
        }
    }
}