﻿using System;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Interfaces;

namespace Nyx.Server.Game.ConquerStructures
{
    public class Equipment
    {
        public bool IsWearingItemUID(uint ItemUID)
        {
            foreach (var obj in Objects)
            {
                if (obj == null) continue;
                if (obj.UID == ItemUID) return true;
            }
            return false;
        }
        public uint GetFullEquipmentPlusPoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.Plus == 1) val += 200;
                        if (item.Plus == 2) val += 600;
                        if (item.Plus == 3) val += 1200;
                        if (item.Plus == 4) val += 1800;
                        if (item.Plus == 5) val += 2600;
                        if (item.Plus == 6) val += 3500;
                        if (item.Plus == 7) val += 4800;
                        if (item.Plus == 8) val += 5800;
                        if (item.Plus == 9) val += 6800;
                        if (item.Plus == 10) val += 7800;
                        if (item.Plus == 11) val += 8800;
                        if (item.Plus == 12) val += 10000;
                    }
                    else
                    {
                        if (item.Plus == 1) val += 400;
                        if (item.Plus == 2) val += 1200;
                        if (item.Plus == 3) val += 2400;
                        if (item.Plus == 4) val += 3600;
                        if (item.Plus == 5) val += 5200;
                        if (item.Plus == 6) val += 7000;
                        if (item.Plus == 7) val += 9600;
                        if (item.Plus == 8) val += 11600;
                        if (item.Plus == 9) val += 13600;
                        if (item.Plus == 10) val += 15600;
                        if (item.Plus == 11) val += 17600;
                        if (item.Plus == 12) val += 20000;
                    }
                }
                return val;
            }
        }
        public uint GetFullEquipmentEnumPoints
        {
            get
            {
                uint Points = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.ID % 10 == 9) Points += 500;
                        if (item.ID % 10 == 8) Points += 300;
                        if (item.ID % 10 == 7) Points += 200;
                        if (item.ID % 10 == 6) Points += 100;
                        if (item.ID % 10 > 0 && item.ID % 10 < 6) Points += 50;
                    }
                    else
                    {
                        if (item.ID % 10 == 9) Points += 1000;
                        if (item.ID % 10 == 8) Points += 600;
                        if (item.ID % 10 == 7) Points += 400;
                        if (item.ID % 10 == 6) Points += 200;
                        if (item.ID % 10 > 0 && item.ID % 10 < 6) Points += 100;
                    }
                }
                return Points;
            }
        }
        public uint GetFullEquipmentSoulPoints
        {
            get
            {
                uint Points = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null || !item.Purification.Available) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.Purification.PurificationLevel == 1) Points += 100;
                        if (item.Purification.PurificationLevel == 2) Points += 300;
                        if (item.Purification.PurificationLevel == 3) Points += 500;
                        if (item.Purification.PurificationLevel == 4) Points += 800;
                        if (item.Purification.PurificationLevel == 5) Points += 1200;
                        if (item.Purification.PurificationLevel == 6) Points += 1600;
                        if (item.Purification.PurificationLevel == 7) Points += 2000;
                    }
                    else
                    {
                        if (item.Purification.PurificationLevel == 1) Points += 200;
                        if (item.Purification.PurificationLevel == 2) Points += 600;
                        if (item.Purification.PurificationLevel == 3) Points += 1000;
                        if (item.Purification.PurificationLevel == 4) Points += 1600;
                        if (item.Purification.PurificationLevel == 5) Points += 2400;
                        if (item.Purification.PurificationLevel == 6) Points += 3200;
                        if (item.Purification.PurificationLevel == 7) Points += 4000;
                    }
                }
                return Points;
            }
        }
        public uint GetFullEquipmentEnchantPoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        var enc = (uint)(item.Enchant);
                        if (enc != 0)
                        {
                            if (enc <= 200) val += enc * 1;
                            if (enc <= 240) val += (uint)(enc * 1.3);
                            if (enc <= 254) val += (uint)(enc * 1.6);
                            if (enc <= 255) val += enc * 2;
                        }
                    }
                    else
                    {
                        var enc = (uint)(item.Enchant);
                        if (enc != 0)
                        {
                            if (enc <= 200) val += enc * 2;
                            if (enc <= 240) val += (uint)(enc * 2.6);
                            if (enc <= 254) val += (uint)(enc * 3.2);
                            if (enc <= 255) val += enc * 4;
                        }
                    }
                }
                return val;
            }
        }
        public uint GetFullEquipmentLevelPoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null || !Database.ConquerItemInformation.BaseInformations.ContainsKey(item.ID)) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        var lvl = (uint)Database.ConquerItemInformation.BaseInformations[item.ID].Level;
                        if (lvl <= 120)
                            val += lvl * 3;
                        else if (lvl <= 130)
                            val += lvl * 5;
                        else if (lvl <= 140)
                            val += lvl * 6;
                    }
                    else
                    {
                        var lvl = (uint)Database.ConquerItemInformation.BaseInformations[item.ID].Level;
                        if (lvl <= 120)
                            val += lvl * 6;
                        else if (lvl <= 130)
                            val += lvl * 10;
                        else if (lvl <= 140)
                            val += lvl * 12;
                    }
                }
                return val;
            }
        }
        public uint GetFullEquipmentGemPoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.SocketOne != (Game.Enums.Gem)0)
                        {
                            if (item.SocketOne2 % 10 == 1) val += 200;
                            if (item.SocketOne2 % 10 == 2) val += 500;
                            if (item.SocketOne2 % 10 == 3) val += 800;
                        }
                        if (item.SocketTwo != (Game.Enums.Gem)0)
                        {
                            if (item.SocketTwo2 % 10 == 1) val += 200;
                            if (item.SocketTwo2 % 10 == 2) val += 500;
                            if (item.SocketTwo2 % 10 == 3) val += 800;
                        }
                    }
                    else
                    {
                        if (item.SocketOne != (Game.Enums.Gem)0)
                        {
                            if (item.SocketOne2 % 10 == 1) val += 400;
                            if (item.SocketOne2 % 10 == 2) val += 1000;
                            if (item.SocketOne2 % 10 == 3) val += 1600;
                        }
                        if (item.SocketTwo != (Game.Enums.Gem)0)
                        {
                            if (item.SocketTwo2 % 10 == 1) val += 400;
                            if (item.SocketTwo2 % 10 == 2) val += 1000;
                            if (item.SocketTwo2 % 10 == 3) val += 1600;
                        }
                    }
                }
                return val;
            }
        }
        public uint GetFullEquipmentPerfecetionLevelPoints
        {
            get
            {
                uint Points = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null || !Database.ConquerItemInformation.BaseInformations.ContainsKey(item.ID)) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (item.Stars >= 1) Points += 180;
                    if (item.Stars >= 2) Points += 180;
                    if (item.Stars >= 3) Points += 180;
                    if (item.Stars >= 4) Points += 180;
                    if (item.Stars >= 5) Points += 180;
                    if (item.Stars >= 6) Points += 180;
                    if (item.Stars >= 7) Points += 180;
                    if (item.Stars >= 8) Points += 180;
                    if (item.Stars >= 9) Points += 180;
                    if (item.Stars >= 10) Points += 2380;
                    if (item.Stars >= 11) Points += 400;
                    if (item.Stars >= 12) Points += 400;
                    if (item.Stars >= 13) Points += 400;
                    if (item.Stars >= 14) Points += 400;
                    if (item.Stars >= 15) Points += 400;
                    if (item.Stars >= 16) Points += 400;
                    if (item.Stars >= 17) Points += 400;
                    if (item.Stars >= 18) Points += 400;
                    if (item.Stars >= 19) Points += 5150;
                    if (item.Stars >= 20) Points += 650;
                    if (item.Stars >= 21) Points += 650;
                    if (item.Stars >= 22) Points += 650;
                    if (item.Stars >= 23) Points += 650;
                    if (item.Stars >= 24) Points += 650;
                    if (item.Stars >= 25) Points += 650;
                    if (item.Stars >= 26) Points += 650;
                    if (item.Stars >= 27) Points += 650;
                    if (item.Stars >= 28) Points += 100;
                    if (item.Stars >= 29) Points += 100;
                    if (item.Stars >= 30) Points += 100;
                    if (item.Stars >= 31) Points += 100;
                    if (item.Stars >= 32) Points += 100;
                    if (item.Stars >= 33) Points += 100;
                    if (item.Stars >= 34) Points += 100;
                    if (item.Stars >= 35) Points += 100;
                    if (item.Stars >= 36) Points += 100;
                    if (item.Stars >= 37) Points += 100;
                    if (item.Stars >= 38) Points += 100;
                    if (item.Stars >= 39) Points += 100;
                    if (item.Stars >= 40) Points += 100;
                    if (item.Stars >= 41) Points += 100;
                    if (item.Stars >= 42) Points += 100;
                    if (item.Stars >= 43) Points += 100;
                    if (item.Stars >= 44) Points += 100;
                    if (item.Stars >= 45) Points += 100;
                    if (item.Stars >= 46) Points += 100;
                    if (item.Stars >= 47) Points += 100;
                    if (item.Stars >= 48) Points += 100;
                    if (item.Stars >= 49) Points += 100;
                    if (item.Stars >= 50) Points += 100;
                    if (item.Stars >= 51) Points += 100;
                    if (item.Stars >= 52) Points += 100;
                    if (item.Stars >= 53) Points += 100;
                    if (item.Stars >= 54) Points += 100;
                    if (Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.Stars >= 1) Points += 180;
                        if (item.Stars >= 2) Points += 180;
                        if (item.Stars >= 3) Points += 180;
                        if (item.Stars >= 4) Points += 180;
                        if (item.Stars >= 5) Points += 180;
                        if (item.Stars >= 6) Points += 180;
                        if (item.Stars >= 7) Points += 180;
                        if (item.Stars >= 8) Points += 180;
                        if (item.Stars >= 9) Points += 180;
                        if (item.Stars >= 10) Points += 2380;
                        if (item.Stars >= 11) Points += 400;
                        if (item.Stars >= 12) Points += 400;
                        if (item.Stars >= 13) Points += 400;
                        if (item.Stars >= 14) Points += 400;
                        if (item.Stars >= 15) Points += 400;
                        if (item.Stars >= 16) Points += 400;
                        if (item.Stars >= 17) Points += 400;
                        if (item.Stars >= 18) Points += 400;
                        if (item.Stars >= 19) Points += 5150;
                        if (item.Stars >= 20) Points += 650;
                        if (item.Stars >= 21) Points += 650;
                        if (item.Stars >= 22) Points += 650;
                        if (item.Stars >= 23) Points += 650;
                        if (item.Stars >= 24) Points += 650;
                        if (item.Stars >= 25) Points += 650;
                        if (item.Stars >= 26) Points += 650;
                        if (item.Stars >= 27) Points += 650;
                        if (item.Stars >= 28) Points += 100;
                        if (item.Stars >= 29) Points += 100;
                        if (item.Stars >= 30) Points += 100;
                        if (item.Stars >= 31) Points += 100;
                        if (item.Stars >= 32) Points += 100;
                        if (item.Stars >= 33) Points += 100;
                        if (item.Stars >= 34) Points += 100;
                        if (item.Stars >= 35) Points += 100;
                        if (item.Stars >= 36) Points += 100;
                        if (item.Stars >= 37) Points += 100;
                        if (item.Stars >= 38) Points += 100;
                        if (item.Stars >= 39) Points += 100;
                        if (item.Stars >= 40) Points += 100;
                        if (item.Stars >= 41) Points += 100;
                        if (item.Stars >= 42) Points += 100;
                        if (item.Stars >= 43) Points += 100;
                        if (item.Stars >= 44) Points += 100;
                        if (item.Stars >= 45) Points += 100;
                        if (item.Stars >= 46) Points += 100;
                        if (item.Stars >= 47) Points += 100;
                        if (item.Stars >= 48) Points += 100;
                        if (item.Stars >= 49) Points += 100;
                        if (item.Stars >= 50) Points += 100;
                        if (item.Stars >= 51) Points += 100;
                        if (item.Stars >= 52) Points += 100;
                        if (item.Stars >= 53) Points += 100;
                        if (item.Stars >= 54) Points += 100;
                    }
                }
                return Points;
            }
        }
        public uint GetFullEquipmentSocketPoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.SocketOne != (Game.Enums.Gem)0) val += 1000;
                        if (item.SocketTwo != (Game.Enums.Gem)0) val += 2500;
                    }
                    else
                    {
                        if (item.SocketOne != (Game.Enums.Gem)0) val += 2000;
                        if (item.SocketTwo != (Game.Enums.Gem)0) val += 5000;
                    }
                }
                return val;
            }
        }
        public uint GetFullEquipmentBlessPoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        val += (uint)(item.Bless * 100);
                    }
                    else
                    {
                        val += (uint)(item.Bless * 200);
                    }
                }
                return val;
            }
        }
        public uint GetFullEquipmentRefinePoints
        {
            get
            {
                uint val = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null || !item.ExtraEffect.Available) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                    if (!Network.PacketHandler.IsTwoHand(item.ID))
                    {
                        if (item.ExtraEffect.EffectLevel == 1) val += 100;
                        if (item.ExtraEffect.EffectLevel == 2) val += 400;
                        if (item.ExtraEffect.EffectLevel == 3) val += 800;
                        if (item.ExtraEffect.EffectLevel == 4) val += 1200;
                        if (item.ExtraEffect.EffectLevel == 5) val += 1600;
                        if (item.ExtraEffect.EffectLevel == 6) val += 2000;
                    }
                    else
                    {
                        if (item.ExtraEffect.EffectLevel == 1) val += 200;
                        if (item.ExtraEffect.EffectLevel == 2) val += 800;
                        if (item.ExtraEffect.EffectLevel == 3) val += 1600;
                        if (item.ExtraEffect.EffectLevel == 4) val += 2400;
                        if (item.ExtraEffect.EffectLevel == 5) val += 3200;
                        if (item.ExtraEffect.EffectLevel == 6) val += 4000;
                    }
                }
                return val;
            }
        }
       public ushort TotalPerfectionLevel
        {
            get
            {
                ushort Count = 0;
                foreach (Network.GamePackets.ConquerItem item in Objects)
                {
                    if (item == null) continue;
                    if (item.Position > 19 || item.Position == 7 || item.Position == 9 || item.Position == 15 || item.Position == 16 || item.Position == 17) continue;
                      Count += (ushort)(Network.PacketHandler.IsTwoHand(item.ID) ? item.Stars * 2 : item.Stars);
                       if (Count >= 648)
                          Count = 648;
                }
                return Count;
            }
        }
        public ConquerItem[] objects;
        Client.GameClient Owner;
        public Equipment(Client.GameClient client)
        {
            Owner = client;
            objects = new ConquerItem[29];
        }
        public void UpdateEntityPacket()
        {
            for (byte Position = 1; Position < 30; Position++)
            {
                if (Free(Position))
                {
                    ClearItemview(Position);
                }
                else
                {
                    var item = TryGetItem(Position);
                    UpdateItemview(item);
                }
            }
            Owner.SendScreen(Owner.Entity.SpawnPacket, false);
        }
        public uint GetGear(byte Position, Client.GameClient C)
        {
            ConquerItem I = C.Equipment.TryGetItem(Position);
            if (I == null)
            {
                return 0;
            }
            return I.UID;
        }
        public bool Add(ConquerItem item)
        {
            if (item != null && objects != null)
            {
                if (objects.Length < item.Position)
                    return false;
                if (item.Position - 1 >= objects.Length) return false;
                if (item.Position - 1 < 0) return false;
                if (objects[item.Position - 1] == null)
                {
                    item.IsWorn = true;
                    UpdateItemview(item);
                    objects[item.Position - 1] = item;
                    item.Position = item.Position;
                    item.Send(Owner);

                    Owner.LoadItemStats();
                    Owner.ReshareClan();
                    if (Owner.Team != null)
                        foreach (var teammate in Owner.Team.Teammates)
                            teammate.ReshareClan();
                    Owner.SendScreenSpawn(Owner.Entity, false);

                    return true;
                }
                else return false;
            }
            else return false;
        }
        public bool Add(ConquerItem item, Enums.ItemUse use)
        {
            if (objects[item.Position - 1] == null)
            {
                objects[item.Position - 1] = item;
                item.Mode = Enums.ItemMode.Default;

                if (use != Enums.ItemUse.None)
                {
                    item.IsWorn = true;
                    UpdateItemview(item);

                    item.Send(Owner);
                    Owner.LoadItemStats();
                    Owner.ReshareClan();
                    if (Owner.Team != null)
                        foreach (var teammate in Owner.Team.Teammates)
                            teammate.ReshareClan();
                }
                return true;
            }
            else return false;
        }
        #region Offsets
        public const int
         Head = 52,
         Garment = 56,
         Armor = 60,
         LeftWeapon = 64,
         RightWeapon = 68,
         LeftWeaponAccessory = 72,
         RightWeaponAccessory = 76,
         Steed = 80,
         MountArmor = 84,
         Wing = 88,
         WingPlus = 92,
         WingProgress = 93,
         ArmorColor = 158,
         LeftWeaponColor = 160,
         HeadColor = 162,
         SteedPlus = 168,
         SteedColor = 174,
         HeadSoul = 217,
         ArmorSoul = 221,
         LeftWeaponSoul = 225,
         RightWeaponSoul = 229;
        #endregion
        public void ClearItemview(uint Position)
        {
            switch ((ushort)Position)
            {
                case Network.GamePackets.ConquerItem.Head:
                    if (Owner.HeadgearLook != 0)
                    {
                        Network.SafeWriter.Write(0, HeadSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(Owner.HeadgearLook, Head, Owner.Entity.SpawnPacket);
                    }
                    else
                    {
                        Network.SafeWriter.Write(0, HeadSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(0, Head, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(0, HeadColor, Owner.Entity.SpawnPacket);
                    }
                    break;
                case Network.GamePackets.ConquerItem.Garment:
                    if (Owner.Entity.MapID != 1081)
                        Network.SafeWriter.Write(0, Garment, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.Armor:
                    if (Owner.ArmorLook != 0)
                    {
                        Network.SafeWriter.Write(0, ArmorSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(Owner.ArmorLook, Armor, Owner.Entity.SpawnPacket);
                    }
                    else
                    {
                        Network.SafeWriter.Write(0, ArmorSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(0, Armor, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(0, ArmorColor, Owner.Entity.SpawnPacket);
                    }
                    break;
                case Network.GamePackets.ConquerItem.RightWeapon:
                    Network.SafeWriter.Write(0, RightWeaponSoul, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, RightWeapon, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.LeftWeapon:
                    Network.SafeWriter.Write(0, LeftWeaponSoul, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, LeftWeapon, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, LeftWeaponColor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.RightWeaponAccessory:
                    Network.SafeWriter.Write(0, RightWeaponAccessory, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.LeftWeaponAccessory:
                    Network.SafeWriter.Write(0, LeftWeaponAccessory, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.Steed:
                    Network.SafeWriter.Write(0, Steed, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, SteedPlus, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, SteedColor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.SteedArmor:
                    Network.SafeWriter.Write(0, MountArmor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.Wings:
                    Network.SafeWriter.Write(0, Wing, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, WingPlus, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(0, WingProgress, Owner.Entity.SpawnPacket);
                    break;
            }
        }
        public void UpdateItemview(ConquerItem item)
        {
            if (item == null) return;
            if (!item.IsWorn) return;
            switch ((ushort)item.Position)
            {
                case Network.GamePackets.ConquerItem.AlternateHead:
                case Network.GamePackets.ConquerItem.Head:
                    if (Owner.HeadgearLook != 0)
                    {
                        Network.SafeWriter.Write(0, HeadSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(Owner.HeadgearLook, Head, Owner.Entity.SpawnPacket);
                    }
                    else
                    {
                        if (item.Purification.Available)
                            Network.SafeWriter.Write(item.Purification.PurificationItemID, HeadSoul, Owner.Entity.SpawnPacket);
                        else Network.SafeWriter.Write(0, HeadSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(item.ID, Head, Owner.Entity.SpawnPacket);
                    }
                    Network.SafeWriter.Write((byte)item.Color, HeadColor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.AlternateGarment:
                case Network.GamePackets.ConquerItem.Garment:
                    Network.SafeWriter.Write(item.ID, Garment, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.AlternateArmor:
                case Network.GamePackets.ConquerItem.Armor:
                    if (Owner.ArmorLook != 0)
                    {
                        Network.SafeWriter.Write(0, ArmorSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(Owner.ArmorLook, Armor, Owner.Entity.SpawnPacket);
                    }
                    else
                    {
                        if (item.Purification.Available)
                            Network.SafeWriter.Write(item.Purification.PurificationItemID, ArmorSoul, Owner.Entity.SpawnPacket);
                        else Network.SafeWriter.Write(0, ArmorSoul, Owner.Entity.SpawnPacket);
                        Network.SafeWriter.Write(item.ID, Armor, Owner.Entity.SpawnPacket);
                    }
                    Network.SafeWriter.Write((byte)item.Color, ArmorColor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.AlternateRightWeapon:
                case Network.GamePackets.ConquerItem.RightWeapon:
                    if (item.Purification.Available)
                        Network.SafeWriter.Write(item.Purification.PurificationItemID, RightWeaponSoul, Owner.Entity.SpawnPacket);
                    else Network.SafeWriter.Write(0, RightWeaponSoul, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(item.ID, RightWeapon, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.RightWeaponAccessory:
                    Network.SafeWriter.Write(item.ID, RightWeaponAccessory, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.AlternateLeftWeapon:
                case Network.GamePackets.ConquerItem.LeftWeapon:
                    if (item.Purification.Available)
                        Network.SafeWriter.Write(item.Purification.PurificationItemID, LeftWeaponSoul, Owner.Entity.SpawnPacket);
                    else Network.SafeWriter.Write(0, LeftWeaponSoul, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write((byte)item.Color, LeftWeaponColor, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(item.ID, LeftWeapon, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.LeftWeaponAccessory:
                    Network.SafeWriter.Write(item.ID, LeftWeaponAccessory, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.Steed:
                    Network.SafeWriter.Write(item.ID, Steed, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write((byte)item.Plus, SteedPlus, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(item.SocketProgress, SteedColor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.SteedArmor:
                    Network.SafeWriter.Write(item.ID, MountArmor, Owner.Entity.SpawnPacket);
                    break;
                case Network.GamePackets.ConquerItem.Wings:
                    Network.SafeWriter.Write(item.ID, Wing, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(item.Plus, WingPlus, Owner.Entity.SpawnPacket);
                    Network.SafeWriter.Write(item.PlusProgress, WingProgress, Owner.Entity.SpawnPacket);
                    break;
            }
        }
        public void Remove(byte Position, byte Position2, byte Position3)
        {
            if (objects[Position - 1] != null)
            {
                objects[Position - 1].Position = Position;
                objects[Position - 1].IsWorn = false;
                objects[Position - 1].Position = 0;
                ItemUsage iu = new ItemUsage(true);
                iu.UID = objects[Position - 1].UID;
                iu.dwParam = Position;
                iu.ID = ItemUsage.UnequipItem;
                Owner.Send(iu.Encode());
                ClearItemview(Position);
                objects[Position - 1] = null;
                Owner.SendScreenSpawn(Owner.Entity, false);
            }
        }
        public bool Remove(byte Position, bool dontAdd = false)
        {
            if (objects[Position - 1] != null)
            {
                if (Owner.Inventory.Count <= 39)
                {
                    if (dontAdd ? true : Owner.Inventory.Add(objects[Position - 1], Enums.ItemUse.Move))
                    {
                        objects[Position - 1].Position = Position;
                        objects[Position - 1].IsWorn = false;
                        objects[Position - 1].Position = 0;
                        if (Position == 12)
                            Owner.Entity.RemoveFlag((ulong)Update.Flags.Ride);
                        if (Position == 4)
                            Owner.Entity.RemoveFlag((ulong)Update.Flags.Fly);
                        ItemUsage iu = new ItemUsage(true);
                        iu.UID = objects[Position - 1].UID;
                        iu.dwParam = Position;
                        iu.ID = ItemUsage.UnequipItem;
                        Owner.Send(iu.Encode());
                        ClearItemview(Position);
                        objects[Position - 1] = null;
                        Owner.SendScreenSpawn(Owner.Entity, false);
                        return true;
                    }
                }
                else
                {
                    Owner.Send("Not enough room in your inventory.");
                    return false;
                }
            }
            return false;
        }
        public bool DestroyArrow(uint Position)
        {
            if (objects[Position - 1] != null)
            {
                objects[Position - 1].Position = (ushort)Position;
                if (objects[Position - 1].ID == 0)
                {
                    objects[Position - 1].Position = 0;
                    Database.ConquerItemTable.DeleteItem(objects[Position - 1].UID);
                    objects[Position - 1] = null;
                    return true;
                }
                if (!Network.PacketHandler.IsArrow(objects[Position - 1].ID))
                    return false;
                objects[Position - 1].IsWorn = false;
                Database.ConquerItemTable.DeleteItem(objects[Position - 1].UID);
                Network.GamePackets.ItemUsage iu = new Network.GamePackets.ItemUsage(true);
                iu.UID = objects[Position - 1].UID;
                iu.dwParam = Position;
                iu.ID = Network.GamePackets.ItemUsage.UnequipItem;
                Owner.Send(iu);
                iu.dwParam = 0;
                iu.ID = Network.GamePackets.ItemUsage.RemoveInventory;
                Owner.Send(iu);
                ClearItemview(Position);
                objects[Position - 1].Position = 0;
                objects[Position - 1] = null;
                return true;
            }
            return false;
        }
        public bool RemoveToGround(uint Position)
        {
            if (Position == 0 || Position > 29)
                return true;
            if (objects[Position - 1] != null)
            {
                objects[Position - 1].Position = (ushort)Position;
                objects[Position - 1].IsWorn = false;
                objects[Position - 1].Position = 0;
                Database.ConquerItemTable.RemoveItem(objects[Position - 1].UID);
                Network.GamePackets.ItemUsage iu = new Network.GamePackets.ItemUsage(true);
                iu.UID = objects[Position - 1].UID;
                iu.dwParam = Position;
                iu.ID = Network.GamePackets.ItemUsage.UnequipItem;
                Owner.Send(iu);
                iu.dwParam = 0;
                iu.ID = Network.GamePackets.ItemUsage.RemoveInventory;
                Owner.Send(iu);

                ClearItemview(Position);
                objects[Position - 1] = null;
                return true;
            }
            return false;
        }
        public ConquerItem[] Objects
        {
            get
            {
                return objects;
            }
        }
        public byte Count
        {
            get
            {
                byte count = 0; foreach (ConquerItem i in objects)
                    if (i != null)
                        count++; return count;
            }
        }
        public bool Free(byte Position)
        {
            return TryGetItem(Position) == null;
        }
        public bool Free(uint Position)
        {
            return TryGetItem((byte)Position) == null;
        }

        public ConquerItem TryGetItem(byte Position)
        {
            ConquerItem item = null;
            if (Position < 1 || Position > 29)
                return item;
            item = objects[Position - 1];
            return item;
        }
        public bool TryGetItem(uint itemGuid, out ConquerItem myItem)
        {
            for (int i = 0; i < Objects.Length; i++)
            {
                var item = Objects[i];
                if (item != null)
                {
                    if (item.UID == itemGuid)
                    {
                        myItem = item;
                        return true;
                    }
                }
            }
            myItem = null;
            return false;
        }
        public ConquerItem TryGetItem(uint uid)
        {
            try
            {
                foreach (ConquerItem item in objects)
                {
                    if (item != null)
                        if (item.UID == uid)
                            return item;
                }
            }
            catch (Exception)
            {

              
            }
            return TryGetItem((byte)uid);
        }

        public bool IsArmorSuper()
        {
            if (TryGetItem(3) != null)
                return TryGetItem(3).ID % 10 == 9;
            return false;
        }
        public bool IsAllSuper()
        {
            for (byte count = 1; count < 12; count++)
            {
                if (count == 5)
                {
                    if (Owner.Entity.Class > 100)
                        continue;
                    if (TryGetItem(count) != null)
                    {
                        if (Network.PacketHandler.IsArrow(TryGetItem(count).ID))
                            continue;
                        if (Network.PacketHandler.IsTwoHand(TryGetItem(4).ID))
                            continue;
                        if (TryGetItem(count).ID % 10 != 9)
                            return false;
                    }
                }
                else
                {
                    if (TryGetItem(count) != null)
                    {
                        if (count != Network.GamePackets.ConquerItem.Bottle && count != Network.GamePackets.ConquerItem.Garment)
                            if (TryGetItem(count).ID % 10 != 9)
                                return false;
                    }
                    else
                        if (count != Network.GamePackets.ConquerItem.Bottle && count != Network.GamePackets.ConquerItem.Garment)
                            return false;
                }
            }
            return true;
        }
        public bool IsHavingGMRobe()
        {
            if (TryGetItem(9) != null)
            {
                if (TryGetItem(9).ID == 137010)
                    return true;
            }
            return false;
        }
        public void ForceEquipments(Equipment equips)
        {
            objects = equips.objects;
        }
    }
}