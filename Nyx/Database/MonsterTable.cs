using System;
using System.Collections.Generic;
using System.IO;
using Nyx.Server.Network.GamePackets;
using Message = Nyx.Server.Network.GamePackets.Message;


namespace Nyx.Server.Database
{
    public class MonsterInformation
    {
        public Game.Entity Owner;
        public uint ExcludeFromSend = 0;
        public bool Guard;
        public uint helmet_type;
        public uint armor_type;
        DateTime Now64 = DateTime.Now;
        public uint ID;
        public ushort Mesh;
        public byte Level;
        public uint Type;
        public string Name;
        public uint Hitpoints;
        public ushort Defence;
        public ushort ViewRange;
        public ushort AttackRange;
        public int RespawnTime;
        public uint MinAttack, MaxAttack;
        public byte AttackType;
        public ushort SpellID;
        public uint InSight;
        public bool Boss;
        public Time32 LastMove;
        public int MoveSpeed;
        public int RunSpeed;
        public uint OwnItemID, OwnItemRate;
        public uint OwnItemID2, OwnItemRate2;
        public uint OwnItemID3, OwnItemRate3;
        public int HPPotionID, MPPotionID;
        public int AttackSpeed;
        public int MinimumSpeed
        {
            get
            {
                int min = 2150;
                if (min > MoveSpeed)
                    min = MoveSpeed;
                if (min > RunSpeed)
                    min = RunSpeed;
                if (min > AttackSpeed)
                    min = AttackSpeed;
                return min;
            }
        }
        public uint ExtraExperience;
        public static uint GetIDFromName(string Name)
        {
            foreach (var item in MonsterInformations.Values)
            {
                if (item.Name == Name)
                    return item.ID;
            }
            return 0;
        }
        public ushort BoundX, BoundY;
        public ushort BoundCX, BoundCY;
        public void SendScreen(byte[] buffer)
        {
            foreach (Client.GameClient client in Kernel.GamePool.Values)
            {
                if (client != null)
                {
                    if (client.Entity != null)
                    {
                        if (client.Entity.UID != ExcludeFromSend)
                        {
                            if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, Owner.X, Owner.Y) > 18)
                            {
                                continue;
                            }
                            client.Send(buffer);
                        }
                    }
                }
            }
        }
        public void SendScreen(Interfaces.IPacket buffer)
        {
            SendScreen(buffer.Encode());
        }
        public void SendScreenSpawn(Interfaces.IMapObject _object)
        {
            foreach (Client.GameClient client in Kernel.GamePool.Values)
            {
                if (client != null)
                {
                    if (client.Entity != null)
                    {
                        if (client.Entity.UID != ExcludeFromSend)
                        {
                            if (client.Map.ID == Owner.MapID)
                            {
                                if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, Owner.X, Owner.Y) > 20)
                                {
                                    continue;
                                }
                                _object.SendSpawn(client, false);
                            }
                        }
                    }
                }
            }
        }
        #region Drop Items
        public byte GetItemBless()
        {
            if (Kernel.Rate(1))
                return 1;
            else if (Kernel.Rate(1))
                return 3;
            return 0;
        }
        public byte GetItemPlus()
        {
            if (Kernel.Rate(1))
                return 1;
            else if (Kernel.Rate(1))
                return 2;
            return 0;
        }
        public uint GetItem()
        {
            try
            {
                int Normal = System.Convert.ToInt32(Constants.ItemDropQualityRates[0]);
                int Refined = System.Convert.ToInt32(Constants.ItemDropQualityRates[1]);
                int Unique = System.Convert.ToInt32(Constants.ItemDropQualityRates[2]);
                int Elite = System.Convert.ToInt32(Constants.ItemDropQualityRates[3]);
                int Super = System.Convert.ToInt32(Constants.ItemDropQualityRates[4]);
                int RRR = Kernel.Random.Next(1, 9);
                uint ItemIDD = 0;
                foreach (var item in Database.ConquerItemInformation.BaseInformations.Values)
                {
                    if (item == null || item.ID == 0)
                        continue;
                    #region Hat
                    if (RRR == 5)
                    {
                        if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.Head)
                        {
                            ItemIDD = (uint)Kernel.Random.Next(111000, 118999);
                            if (ItemIDD % 10 == 9)
                                if (!Kernel.Rate(Super))
                                    return 0;
                            if (ItemIDD % 10 == 8)
                                if (!Kernel.Rate(Elite))
                                    return 0;
                            if (ItemIDD % 10 == 7)
                                if (!Kernel.Rate(Unique))
                                    return 0;
                            //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                            //    return 0;
                            return ItemIDD;
                        }
                    }
                    #endregion
                    #region Necklace
                    else if (RRR == 6)
                    {
                        if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.Necklace)
                        {
                            ItemIDD = (uint)Kernel.Random.Next(120000, 121000);
                            if (ItemIDD % 10 == 9)
                                if (!Kernel.Rate(Super))
                                    return 0;
                            if (ItemIDD % 10 == 8)
                                if (!Kernel.Rate(Elite))
                                    return 0;
                            if (ItemIDD % 10 == 7)
                                if (!Kernel.Rate(Unique))
                                    return 0;
                            //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                            //    return 0;
                            return ItemIDD;
                        }
                    }
                    #endregion
                    #region Armor
                    else if (RRR == 2)
                    {
                        if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.Armor)
                        {
                            ItemIDD = (uint)Kernel.Random.Next(130000, 137000);
                            if (ItemIDD % 10 == 9)
                                if (!Kernel.Rate(Super))
                                    return 0;
                            if (ItemIDD % 10 == 8)
                                if (!Kernel.Rate(Elite))
                                    return 0;
                            if (ItemIDD % 10 == 7)
                                if (!Kernel.Rate(Unique))
                                    return 0;
                            //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                            //    return 0;
                            return ItemIDD;
                        }
                    }
                    #endregion
                    #region Ring
                    else if (RRR == 8)
                    {
                        if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.Ring)
                        {
                            ItemIDD = (uint)Kernel.Random.Next(150000, 152000);
                            if (ItemIDD % 10 == 9)
                                if (!Kernel.Rate(Super))
                                    return 0;
                            if (ItemIDD % 10 == 8)
                                if (!Kernel.Rate(Elite))
                                    return 0;
                            if (ItemIDD % 10 == 7)
                                if (!Kernel.Rate(Unique))
                                    return 0;
                            //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                            //    return 0;
                            return ItemIDD;
                        }
                    }
                    #endregion
                    #region Boots
                    else if (RRR == 3)
                    {
                        if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.Boots)
                        {
                            ItemIDD = (uint)Kernel.Random.Next(160013, 160249);
                            if (ItemIDD % 10 == 9)
                                if (!Kernel.Rate(Super))
                                    return 0;
                            if (ItemIDD % 10 == 8)
                                if (!Kernel.Rate(Elite))
                                    return 0;
                            if (ItemIDD % 10 == 7)
                                if (!Kernel.Rate(Unique))
                                    return 0;
                            //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                            //    return 0;
                            return ItemIDD;
                        }
                    }
                    #endregion
                    #region Fan
                    else if (RRR == 9)
                    {
                        if (Kernel.Rate(1))
                        {
                            if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.AttackTalisman)
                            {
                                ItemIDD = (uint)Kernel.Random.Next(201001, 201009);
                                if (ItemIDD % 10 == 9)
                                    if (!Kernel.Rate(Super))
                                        return 0;
                                if (ItemIDD % 10 == 8)
                                    if (!Kernel.Rate(Elite))
                                        return 0;
                                if (ItemIDD % 10 == 7)
                                    if (!Kernel.Rate(Unique))
                                        return 0;
                                //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                                //    return 0;
                                return ItemIDD;
                            }
                        }
                    }
                    #endregion
                    #region Tower
                    else if (RRR == 4)
                    {
                        if (Kernel.Rate(1))
                        {
                            if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.DefenceTalisman)
                            {
                                ItemIDD = (uint)Kernel.Random.Next(202001, 202009);
                                if (ItemIDD % 10 == 9)
                                    if (!Kernel.Rate(Super))
                                        return 0;
                                if (ItemIDD % 10 == 8)
                                    if (!Kernel.Rate(Elite))
                                        return 0;
                                if (ItemIDD % 10 == 7)
                                    if (!Kernel.Rate(Unique))
                                        return 0;
                                //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                                //    return 0;
                                return ItemIDD;
                            }
                        }
                    }
                    #endregion
                    #region Crop
                    else if (RRR == 7)
                    {
                        if (Kernel.Rate(1))
                        {
                            if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.SteedTalisman)
                            {
                                ItemIDD = (uint)Kernel.Random.Next(203001, 203009);
                                if (ItemIDD % 10 == 9)
                                    if (!Kernel.Rate(Super))
                                        return 0;
                                if (ItemIDD % 10 == 8)
                                    if (!Kernel.Rate(Elite))
                                        return 0;
                                if (ItemIDD % 10 == 7)
                                    if (!Kernel.Rate(Unique))
                                        return 0;
                                //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                                //    return 0;
                                return ItemIDD;
                            }
                        }
                    }
                    #endregion
                    #region Wing
                    else if (RRR == 1)
                    {
                        if (Kernel.Rate(1))
                        {
                            if (Network.PacketHandler.GetPositionFromID(item.ID) == Network.PacketHandler.Positions.Wing)
                            {
                                ItemIDD = (uint)Kernel.Random.Next(204001, 204009);
                                if (ItemIDD % 10 == 9)
                                    if (!Kernel.Rate(Super))
                                        return 0;
                                if (ItemIDD % 10 == 8)
                                    if (!Kernel.Rate(Elite))
                                        return 0;
                                if (ItemIDD % 10 == 7)
                                    if (!Kernel.Rate(Unique))
                                        return 0;
                                //if (Database.ConquerItemInformation.BaseInformations[ItemIDD].Level >= 120 && Kernel.Rate(99))
                                //    return 0;
                                return ItemIDD;
                            }
                        }
                    }
                    #endregion
                }
                return ItemIDD;
            }
            catch (Exception)
            {

            }
            return 0;
        }
        #endregion
        public void Drop(Game.Entity killer)
        {
            if (killer == null || Owner == null || Name.Contains("Guard") || killer.Name.Contains("Guard") || killer.Name.Contains("ShadowClone") || killer.ClanName == "ShadowClone" || this == null)
                return;
            #region OwnsItems
            if (OwnItemID != 0 && OwnItemRate != 0)
            {
                if (OwnItemID == 723903 && killer.Owner.Inventory.Contains(723903, 5))
                    return;
                if (OwnItemID == 721100)
                {
                    if (!killer.Owner.Inventory.Contains(721100))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~1!~You~can~give~it~to~the~Stage~Guard~to~take~challenge~again.");
                        killer.Owner.Inventory.Add(721100, 0, 1);
                    }
                    return;
                }
                if (OwnItemID == 721101)
                {
                    if (!killer.Owner.Inventory.Contains(721101))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~2.");
                        killer.Owner.Inventory.Add(721101, 0, 1);
                    }
                    return;
                }
                if (OwnItemID == 721102)
                {
                    if (!killer.Owner.Inventory.Contains(721102))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~3.");
                        killer.Owner.Inventory.Add(721102, 0, 1);
                    }
                    return;
                }
                if (OwnItemID == 721103)
                {
                    if (!killer.Owner.Inventory.Contains(721103))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~4.");
                        killer.Owner.Inventory.Add(721103, 0, 1);
                    }
                    return;
                }
                if (OwnItemID == 721108)
                {
                    if (!killer.Owner.Inventory.Contains(721108))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~L120.");
                        killer.Owner.Inventory.Add(721108, 0, 1);
                    }
                    return;
                }
                if (Kernel.Rate(OwnItemRate))
                {
                    var infoss = Database.ConquerItemInformation.BaseInformations[OwnItemID];
                    ushort XX = Owner.X, YY = Owner.Y;
                    Game.Map MMap = Kernel.Maps[Owner.MapID];
                    if (MMap.SelectCoordonates(ref XX, ref YY))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = OwnItemID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infoss.Durability;
                        floorItem.Item.Durability = infoss.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = OwnItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = XX;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infoss.StackSize;
                        floorItem.Y = YY;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.Owner = killer.Owner;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (MMap.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        MMap.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }

                }
            }
            if (OwnItemID2 != 0 && OwnItemRate2 != 0)
            {
                if (OwnItemID2 == 723903 && killer.Owner.Inventory.Contains(723903, 5))
                    return;
                if (OwnItemID2 == 3006392)
                {
                    killer.Teleport(1011, 211, 194);
                    killer.Owner.Inventory.Add(3006392, 0, 1);
                    return;
                }
                if (OwnItemID == 3006392)
                {
                    killer.Teleport(1011, 211, 194);
                    killer.Owner.Inventory.Add(3006392, 0, 1);
                    return;
                }
                if (OwnItemID2 == 721100)
                {
                    if (!killer.Owner.Inventory.Contains(721100))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~1!~You~can~give~it~to~the~Stage~Guard~to~take~challenge~again.");
                        killer.Owner.Inventory.Add(721100, 0, 1);
                    }
                    return;
                }
                if (OwnItemID2 == 721101)
                {
                    if (!killer.Owner.Inventory.Contains(721101))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~2.");
                        killer.Owner.Inventory.Add(721101, 0, 1);
                    }
                    return;
                }
                if (OwnItemID2 == 721102)
                {
                    if (!killer.Owner.Inventory.Contains(721102))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~3.");
                        killer.Owner.Inventory.Add(721102, 0, 1);
                    }
                    return;
                }
                if (OwnItemID2 == 721103)
                {
                    if (!killer.Owner.Inventory.Contains(721103))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~4.");
                        killer.Owner.Inventory.Add(721103, 0, 1);
                    }
                    return;
                }
                if (OwnItemID2 == 721108)
                {
                    if (!killer.Owner.Inventory.Contains(721108))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~L120.");
                        killer.Owner.Inventory.Add(721108, 0, 1);
                    }
                    return;
                }
                if (Kernel.Rate(OwnItemRate2))
                {
                    var infoss = Database.ConquerItemInformation.BaseInformations[OwnItemID2];
                    ushort XX = Owner.X, YY = Owner.Y;
                    Game.Map MMap = Kernel.Maps[Owner.MapID];
                    if (MMap.SelectCoordonates(ref XX, ref YY))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = OwnItemID2;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infoss.Durability;
                        floorItem.Item.Durability = infoss.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = OwnItemID2;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = XX;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infoss.StackSize;
                        floorItem.Y = YY;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.Owner = killer.Owner;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (MMap.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        MMap.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }

                }
            }
            if (OwnItemID3 != 0 && OwnItemRate3 != 0)
            {
                if (OwnItemID3 == 723903 && killer.Owner.Inventory.Contains(723903, 5))
                    return;
                if (OwnItemID3 == 3006392)
                {
                    killer.Teleport(1011, 211, 194);
                    killer.Owner.Inventory.Add(3006392, 0, 1);
                    return;
                }
                if (OwnItemID3 == 3006392)
                {
                    killer.Teleport(1011, 211, 194);
                    killer.Owner.Inventory.Add(3006392, 0, 1);
                    return;
                }
                if (OwnItemID3 == 721100)
                {
                    if (!killer.Owner.Inventory.Contains(721100))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~1!~You~can~give~it~to~the~Stage~Guard~to~take~challenge~again.");
                        killer.Owner.Inventory.Add(721100, 0, 1);
                    }
                    return;
                }
                if (OwnItemID3 == 721101)
                {
                    if (!killer.Owner.Inventory.Contains(721101))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~2.");
                        killer.Owner.Inventory.Add(721101, 0, 1);
                    }
                    return;
                }
                if (OwnItemID3 == 721102)
                {
                    if (!killer.Owner.Inventory.Contains(721102))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~3.");
                        killer.Owner.Inventory.Add(721102, 0, 1);
                    }
                    return;
                }
                if (OwnItemID3 == 721103)
                {
                    if (!killer.Owner.Inventory.Contains(721103))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~4.");
                        killer.Owner.Inventory.Add(721103, 0, 1);
                    }
                    return;
                }
                if (OwnItemID3 == 721108)
                {
                    if (!killer.Owner.Inventory.Contains(721108))
                    {
                        killer.Owner.MessageBox("You~received~the~Pass~Token~L120.");
                        killer.Owner.Inventory.Add(721108, 0, 1);
                    }
                    return;
                }
                if (Kernel.Rate(OwnItemRate3))
                {
                    var infoss = Database.ConquerItemInformation.BaseInformations[OwnItemID3];
                    ushort XX = Owner.X, YY = Owner.Y;
                    Game.Map MMap = Kernel.Maps[Owner.MapID];
                    if (MMap.SelectCoordonates(ref XX, ref YY))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = OwnItemID3;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infoss.Durability;
                        floorItem.Item.Durability = infoss.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = OwnItemID3;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = XX;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infoss.StackSize;
                        floorItem.Y = YY;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.Owner = killer.Owner;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (MMap.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        MMap.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }

                }
            }
            #endregion
            #region Bloodshed Sea
            if (Name == "NightmareCaptain")
            {
                if (Kernel.Rate(5))
                {
                    uint b = 0;
                    int ban = Kernel.Random.Next(1, 9);
                    switch (ban)
                    {
                        case 1: b = 3004231; break;
                        case 2: b = 3004232; break;
                        case 3: b = 3004234; break;
                        case 4: b = 3004236; break;
                        case 5: b = 3004235; break;
                        case 6: b = 3004237; break;
                        case 7: b = 3004233; break;
                        case 8: b = 3004237; break;
                    }
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    var infoss = Database.ConquerItemInformation.BaseInformations[b];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = infoss.ID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infoss.Durability;
                        floorItem.Item.Durability = infoss.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = infoss.ID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infoss.StackSize;
                        floorItem.Y = Y;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.Owner = killer.Owner;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
            }
            #endregion
            #region TowerofMystery
            if (Name.Contains("GreatPheasant") || Name.Contains("WingedLord") || Name.Contains("WaterTerror") || Name.Contains("RuthlessAsura") || Name.Contains("FlameGiant") || Name.Contains("SoulStrangler") || Name.Contains("DarkGlutton") || Name.Contains("AlienDragon") || Name.Contains("ShadowSpider"))
            {
                if (killer.Owner.Quests.GetQuest(QuestID.TowerOfMystery) == null || killer.Owner.Quests.CheckQuest(QuestID.TowerOfMystery) == QuestPacket.QuestData.QuestStatus.Available)
                    killer.Owner.Quests.Accept(QuestID.TowerOfMystery);
                killer.Owner.Quests.DoneTowerMystery();
                killer.Owner.MessageBox("You`ve successfully defeated the devil on Tower of Mystery " + killer.TOM + "F. Hurry and go claim the Bright Tribe`s reward for you.");
                _String str = new _String(true);
                str.TextsCount = 1;
                str.UID = killer.UID;
                str.Type = 10;
                str.Texts.Add("zf2-281");
                killer.Owner.SendScreen(str, true);
                Nyx.Server.Interfaces.INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 200028;
                npc.Mesh = 40150;
                npc.Name = "BrightTribe`s";
                npc.Type = (Game.Enums.NpcType)2;
                npc.X = 66;
                npc.Y = 66;
                npc.MapID = killer.MapID;
                killer.TOMM.AddNpc(npc);
                foreach (var client in Kernel.GamePool.Values)
                {
                    if (client.Entity.MapID != killer.MapID) continue;
                    client.Screen.Reload(null);
                }
                _String str2 = new _String(true);
                str2.TextsCount = 1;
                str2.X = npc.X;
                str2.Y = npc.Y;
                str2.Type = 9;
                str2.Texts.Add("zf2-280");
                killer.Owner.SendScreen(str2, true);
            }
            #endregion
            #region HeavenRoc(TempestWingQuest)
            if (Name == "HeavenRoc")
            {
                killer.Teleport(1011, 211, 194);
                killer.Owner.Inventory.Add(3006392, 0, 1);
            }
            #endregion
            #region Power Arena
            if (Name == "ArenaSentinel" && killer.MapID == 1730)
            {
                if (killer.KillArena1 < 30)
                {
                    killer.KillArena1 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }
            else if (Name == "ArenaSentinel" && killer.MapID == 1737)
            {
                if (killer.KillArena7 < 90)
                {
                    killer.KillArena7 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }
            else if (Name == "ArenaSentinel" && killer.MapID == 1731)
            {
                if (killer.KillArena2 < 45)
                {
                    killer.KillArena2 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }
            else if (Name == "ArenaSentinel" && killer.MapID == 1732)
            {
                if (killer.KillArena3 < 60)
                {
                    killer.KillArena3 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }
            else if (Name == "ArenaSentinel" && killer.MapID == 1733)
            {
                if (killer.KillArena4 < 35)
                {
                    killer.KillArena4 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }
            else if (Name == "ArenaSentinel" && killer.MapID == 1734)
            {
                if (killer.KillArena5 < 45)
                {
                    killer.KillArena5 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }
            else if (Name == "ArenaSentinel" && killer.MapID == 1735)
            {
                if (killer.KillArena6 < 60)
                {
                    killer.KillArena6 += 1;
                }
                else
                {
                    killer.Owner.Send(new Message("You have flattened enough guardians to claim experience from the Agate now.", System.Drawing.Color.Red, Message.System));
                }
            }

            #endregion
            #region Ganoderma and titan
            if (Name.Contains("Ganoderma") || Name.Contains("Titan"))
            {
                killer.DoubleExperienceTime = 3600;
            }
            #endregion
            #region SnowBanshee

            if (Name == "SnowBanshee")
            {
                int R = Kernel.Random.Next(1, 4);
                if (R == 1)
                {
                    uint ItemID = 0;
                    byte type1 = 51;
                    for (int i = 0; i < 1; i++)
                    {
                        type1 = (byte)Kernel.Random.Next(1, 51);
                        switch (type1)
                        {
                            case 1:
                                ItemID = 800320;
                                break;

                            case 2:
                                ItemID = 822054;
                                break;

                            case 3:
                                ItemID = 800110;
                                break;

                            case 4:
                                ItemID = 820056;
                                break;

                            case 5:
                                ItemID = 822056;
                                break;

                            case 6:
                                ItemID = 822057;
                                break;

                            case 7:
                                ItemID = 822053;
                                break;

                            case 8:
                                ItemID = 800019;
                                break;

                            case 9:
                                ItemID = 800050;
                                break;

                            case 10:
                                ItemID = 800015;
                                break;

                            case 11:
                                ItemID = 800090;
                                break;

                            case 12:
                                ItemID = 800513;
                                break;

                            case 13:
                                ItemID = 800017;
                                break;

                            case 14:
                                ItemID = 800071;
                                break;

                            case 15:
                                ItemID = 800016;
                                break;

                            case 16:
                                ItemID = 823051;
                                break;

                            case 17:
                                ItemID = 800130;
                                break;

                            case 18:
                                ItemID = 800140;
                                break;

                            case 19:
                                ItemID = 800141;
                                break;

                            case 20:
                                ItemID = 800200;
                                break;

                            case 21:
                                ItemID = 800310;
                                break;

                            case 22:
                                ItemID = 800014;
                                break;

                            case 23:
                                ItemID = 800214;
                                break;

                            case 24:
                                ItemID = 800230;
                                break;

                            case 25:
                                ItemID = 800414;
                                break;

                            case 26:
                                ItemID = 822052;
                                break;

                            case 27:
                                ItemID = 800420;
                                break;

                            case 28:
                                ItemID = 800401;
                                break;

                            case 29:
                                ItemID = 800512;
                                break;

                            case 30:
                                ItemID = 823043;
                                break;

                            case 31:
                                ItemID = 800514;
                                break;

                            case 32:
                                ItemID = 800520;
                                break;

                            case 33:
                                ItemID = 800521;
                                break;

                            case 34:
                                ItemID = 800613;
                                break;

                            case 35:
                                ItemID = 800614;
                                break;

                            case 36:
                                ItemID = 800615;
                                break;

                            case 37:
                                ItemID = 824001;
                                break;

                            case 38:
                                ItemID = 800617;
                                break;

                            case 39:
                                ItemID = 800720;
                                break;

                            case 40:
                                ItemID = 800721;
                                break;

                            case 41:
                                ItemID = 800070;
                                break;

                            case 42:
                                ItemID = 800723;
                                break;

                            case 43:
                                ItemID = 800724;
                                break;

                            case 44:
                                ItemID = 800018;
                                break;

                            case 45:
                                ItemID = 820001;
                                break;

                            case 46:
                                ItemID = 820052;
                                break;

                            case 47:
                                ItemID = 820053;
                                break;

                            case 48:
                                ItemID = 820054;
                                break;

                            case 49:
                                ItemID = 820055;
                                break;

                            case 50:
                                ItemID = 800722;
                                break;
                            case 51:
                                ItemID = 801001;
                                break;
                        }
                        var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {

                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = ItemID;
                            floorItem.Item.Plus = floorItem.Item.Plus;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos.StackSize;
                            floorItem.Item.MaximDurability = infos.Durability;
                            floorItem.Item.Durability = infos.Durability;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = ItemID;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.OwnerUID = killer.UID;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))

                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                            Map.AddFloorItem(floorItem);

                            SendScreenSpawn(floorItem);
                        }
                        uint amount = 500000;
                        uint ItemID2 = Network.PacketHandler.MoneyItemID(amount);
                        ushort X2 = Owner.X, Y2 = Owner.Y;
                        if (Kernel.Maps[Owner.MapID].SelectCoordonates(ref X2, ref Y2))
                        {
                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Money;
                            floorItem.Value = amount;
                            floorItem.ItemID = ItemID2;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X2;
                            floorItem.Y = Y2;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Kernel.Maps[Owner.MapID].Npcs.ContainsKey(floorItem.UID))
                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            Map.AddFloorItem(floorItem);
                            SendScreenSpawn(floorItem);
                        }
                        killer.ConquerPoints += 750;
                    }
                }
                else if (R == 2)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        var infos = Database.ConquerItemInformation.BaseInformations[730002];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {

                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = 730002;
                            floorItem.Item.Plus = 2;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos.StackSize;
                            floorItem.Item.MaximDurability = infos.Durability;
                            floorItem.Item.Durability = infos.Durability;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = 730002;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))

                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                            Map.AddFloorItem(floorItem);

                            SendScreenSpawn(floorItem);
                        }
                    }
                }
                else if (R == 3)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        var infos = Database.ConquerItemInformation.BaseInformations[1088000];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {

                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = 1088000;
                            floorItem.Item.Plus = 0;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos.StackSize;
                            floorItem.Item.MaximDurability = infos.Durability;
                            floorItem.Item.Durability = infos.Durability;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = 1088000;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))

                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                            Map.AddFloorItem(floorItem);

                            SendScreenSpawn(floorItem);
                        }
                    }
                }
                else if (R == 4)
                {
                    var infos = Database.ConquerItemInformation.BaseInformations[1088000];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {

                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = 1088000;
                        floorItem.Item.Plus = 0;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos.StackSize;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = 1088000;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))

                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                        Map.AddFloorItem(floorItem);

                        SendScreenSpawn(floorItem);
                    }
                    var infos2 = Database.ConquerItemInformation.BaseInformations[730002];
                    ushort X2 = Owner.X, Y2 = Owner.Y;
                    if (Kernel.Maps[Owner.MapID].SelectCoordonates(ref X, ref Y))
                    {

                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = 730002;
                        floorItem.Item.Plus = 2;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos2.StackSize;
                        floorItem.Item.MaximDurability = infos2.Durability;
                        floorItem.Item.Durability = infos2.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = 730002;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X2;
                        floorItem.Y = Y2;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Kernel.Maps[Owner.MapID].Npcs.ContainsKey(floorItem.UID))

                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                        Map.AddFloorItem(floorItem);

                        SendScreenSpawn(floorItem);
                    }
                }
                Kernel.SpawnBanshee = false;
                foreach (var client in Kernel.GamePool.Values)
                    client.Send(new Message("The Snow Banshee has been destroyed by the team " + killer.Name + " All the team members received 150 Study Points!", System.Drawing.Color.White, Message.Talk));
                if (killer.Owner.Team != null)
                {
                    foreach (Client.GameClient tm in killer.Owner.Team.Teammates)
                    {
                        tm.Entity.SubClasses.StudyPoints += 150;
                    }
                }
                else
                    killer.SubClasses.StudyPoints += 150;
                killer.Owner.ChampionPoints += 100;
               

            }
         
            #endregion
            #region SnowBanshee healthwine

            if (Name == "SnowBanshee" && killer.MapID == 1765)
            {
                uint ItemID = 0;
                byte type4 = 40;
                for (int m = 0; m < 6; m++)
                {
                    type4 = (byte)Kernel.Random.Next(1, 40);
                    switch (type4)
                    {
                        case 1:
                            ItemID = 800014;
                            break;
                        case 2:
                            ItemID = 800015;
                            break;
                        case 3:
                            ItemID = 800016;
                            break;
                        case 4:
                            ItemID = 800017;
                            break;
                        case 5:
                            ItemID = 800110;
                            break;
                        case 6:
                            ItemID = 800214;
                            break;
                        case 7:
                            ItemID = 800320;
                            break;
                        case 8:
                            ItemID = 800415;
                            break;
                        case 9:
                            ItemID = 800512;
                            break;
                        case 10:
                            ItemID = 800513;
                            break;
                        case 11:
                            ItemID = 800613;
                            break;
                        case 12:
                            ItemID = 800616;
                            break;
                        case 13:
                            ItemID = 800720;
                            break;
                        case 14:
                            ItemID = 800722;
                            break;
                        case 15:
                            ItemID = 820056;
                            break;
                        case 16:
                            ItemID = 820057;
                            break;
                        case 17:
                            ItemID = 821014;
                            break;
                        case 18:
                            ItemID = 821026;
                            break;
                        case 19:
                            ItemID = 822053;
                            break;
                        case 20:
                            ItemID = 822055;
                            break;
                        case 21:
                            ItemID = 823054;
                            break;
                        case 22:
                            ItemID = 823053;
                            break;
                        case 23:
                            ItemID = 823052;
                            break;
                        case 24:
                            ItemID = 824014;
                            break;
                        case 25:
                            ItemID = 184375;
                            break;
                        case 26:
                            ItemID = 187305;
                            break;
                        case 27:
                            ItemID = 725016;
                            break;
                        case 28:
                            ItemID = 725018;
                            break;
                        case 29:
                            ItemID = 725019;
                            break;
                        case 30:
                            ItemID = 725020;
                            break;
                        case 31:
                            ItemID = 725021;
                            break;
                        case 32:
                            ItemID = 725022;
                            break;
                        case 33:
                            ItemID = 725023;
                            break;
                        case 34:
                            ItemID = 725024;
                            break;
                        case 35:
                            ItemID = 194300;
                            break;
                        case 36:
                            ItemID = 183365;
                            break;
                        case 37:
                            ItemID = 183315;
                            break;
                        case 38:
                            ItemID = 191405;
                            break;
                        case 39:
                            ItemID = 187355;
                            break;
                        case 40:
                            ItemID = 824001;
                            break;
                    }
                    var infos6 = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos6.Durability;
                        floorItem.Item.Durability = infos6.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                        Kernel.Spawn1 = false;
                    }
                }
                killer.SubClasses.StudyPoints += 200;
                killer.ConquerPoints += 5000;
                Nyx.Server.Kernel.SendWorldMessage(new Nyx.Server.Network.GamePackets.Message("Congratulations! " + killer.Name + " Has Killed " + Name + "and recived 40.000.000 Cps and Droped A Random P6 Dragon Soul Gears.", Message.TopLeft));
                if (Owner.MonsterInfo.ID == 4171)
                {
                    if (Owner.MapID == 1765)
                    {
                        if (Kernel.Maps[1765].TempNpcs[999] != null)
                        {
                            Kernel.Maps[1765].AddNpc(Kernel.Maps[1765].TempNpcs[999]);
                            Kernel.Maps[1765].TempNpcs.Remove(999);
                            var n = Kernel.Maps[1765].Npcs[999] as Interfaces.IMapObject;
                            killer.Owner.SendScreen(new Network.GamePackets.Data(true)

                            {
                                UID = 999,
                                ID = Network.GamePackets.Data.AddEntity
                            }, true);

                        }
                    }
                    foreach (var Ownerr in Program.GamePool)
                    {
                        if (Ownerr.Entity.MapID == 1765)
                        {
                            foreach (Interfaces.INpc npc in Kernel.Maps[1765].Npcs.Values)
                            {
                                if (npc != null)
                                {
                                    if (npc.UID == 999)
                                    {
                                        if (Kernel.GetDistance(npc.X, npc.Y, Ownerr.Entity.X, Ownerr.Entity.Y) < 16)
                                        {
                                            npc.SendSpawn(Ownerr, false);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                return;
            }
            #endregion 
            #region SnowBansheeSoul
            if (Name == "SnowBansheeSoul")
            {
                killer.ConquerPoints += 5000;
                uint ItemID = 0;
                byte type1 = 22;
                for (int i = 0; i < 4; i++)
                {
                    type1 = (byte)Kernel.Random.Next(1, 22);
                    switch (type1)
                    {
                        case 1:
                            ItemID = 822053;
                            break;
                        case 2:
                            ItemID = 822055;
                            break;
                        case 3:
                            ItemID = 820071;
                            break;
                        case 4:
                            ItemID = 820072;
                            break;
                        case 5:
                            ItemID = 823055;
                            break;
                        case 6:
                            ItemID = 823056;
                            break;
                        case 7:
                            ItemID = 823057;
                            break;
                        case 8:
                            ItemID = 821031;
                            break;
                        case 9:
                            ItemID = 821032;
                            break;
                        case 10:
                            ItemID = 800000;
                            break;
                        case 11:
                            ItemID = 824017;
                            break;
                        case 12:
                            ItemID = 800421;
                            break;
                        case 13:
                            ItemID = 800110;
                            break;
                        case 14:
                            ItemID = 800017;
                            break;
                        case 15:
                            ItemID = 800254;
                            break;
                        case 16:
                            ItemID = 800513;
                            break;
                        case 17:
                            ItemID = 800320;
                            break;
                        case 18:
                            ItemID = 800722;
                            break;
                        case 19:
                            ItemID = 800616;
                            break;
                        case 20:
                            ItemID = 800809;
                            break;
                        case 21:
                            ItemID = 800804;
                            break;
                        case 22:
                            ItemID = 800916;
                            break;
                    }
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        FloorItem floorItem = new FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Type = FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                        Kernel.SpawnBansheeSoulKeep = false;
                    }
                    foreach (var client in Kernel.GamePool.Values)
                        client.Send(new Message("The Snow Banshee Soul has been destroyed by the team " + killer.Name + " All the team members received 150 Study Points!", System.Drawing.Color.White, Message.Talk));
                }
                return;
            }
            #endregion
            #region PurpleBanshee

            if (Name == "PurpleBanshee")
            {
                int R = Kernel.Random.Next(1, 3);
                if (R == 1)
                {
                    uint ItemID = 0;
                    byte type1 = 51;
                    for (int i = 0; i < 1; i++)
                    {
                        type1 = (byte)Kernel.Random.Next(1, 51);
                        switch (type1)
                        {
                            case 1:
                                ItemID = 800320;
                                break;

                            case 2:
                                ItemID = 822054;
                                break;

                            case 3:
                                ItemID = 800110;
                                break;

                            case 4:
                                ItemID = 820056;
                                break;

                            case 5:
                                ItemID = 822056;
                                break;

                            case 6:
                                ItemID = 822057;
                                break;

                            case 7:
                                ItemID = 822053;
                                break;

                            case 8:
                                ItemID = 800019;
                                break;

                            case 9:
                                ItemID = 800050;
                                break;

                            case 10:
                                ItemID = 800015;
                                break;

                            case 11:
                                ItemID = 800090;
                                break;

                            case 12:
                                ItemID = 800513;
                                break;

                            case 13:
                                ItemID = 800017;
                                break;

                            case 14:
                                ItemID = 800071;
                                break;

                            case 15:
                                ItemID = 800016;
                                break;

                            case 16:
                                ItemID = 823051;
                                break;

                            case 17:
                                ItemID = 800130;
                                break;

                            case 18:
                                ItemID = 800140;
                                break;

                            case 19:
                                ItemID = 800141;
                                break;

                            case 20:
                                ItemID = 800200;
                                break;

                            case 21:
                                ItemID = 800310;
                                break;

                            case 22:
                                ItemID = 800014;
                                break;

                            case 23:
                                ItemID = 800214;
                                break;

                            case 24:
                                ItemID = 800230;
                                break;

                            case 25:
                                ItemID = 800414;
                                break;

                            case 26:
                                ItemID = 822052;
                                break;

                            case 27:
                                ItemID = 800420;
                                break;

                            case 28:
                                ItemID = 800401;
                                break;

                            case 29:
                                ItemID = 800512;
                                break;

                            case 30:
                                ItemID = 823043;
                                break;

                            case 31:
                                ItemID = 800514;
                                break;

                            case 32:
                                ItemID = 800520;
                                break;

                            case 33:
                                ItemID = 800521;
                                break;

                            case 34:
                                ItemID = 800613;
                                break;

                            case 35:
                                ItemID = 800614;
                                break;

                            case 36:
                                ItemID = 800615;
                                break;

                            case 37:
                                ItemID = 824001;
                                break;

                            case 38:
                                ItemID = 800617;
                                break;

                            case 39:
                                ItemID = 800720;
                                break;

                            case 40:
                                ItemID = 800721;
                                break;

                            case 41:
                                ItemID = 800070;
                                break;

                            case 42:
                                ItemID = 800723;
                                break;

                            case 43:
                                ItemID = 800724;
                                break;

                            case 44:
                                ItemID = 800018;
                                break;

                            case 45:
                                ItemID = 820001;
                                break;

                            case 46:
                                ItemID = 820052;
                                break;

                            case 47:
                                ItemID = 820053;
                                break;

                            case 48:
                                ItemID = 820054;
                                break;

                            case 49:
                                ItemID = 820055;
                                break;

                            case 50:
                                ItemID = 800722;
                                break;
                            case 51:
                                ItemID = 801001;
                                break;
                        }
                        var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {

                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = ItemID;
                            floorItem.OwnerUID = killer.UID;
                            floorItem.Item.Plus = floorItem.Item.Plus;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos.StackSize;
                            floorItem.Item.MaximDurability = infos.Durability;
                            floorItem.Item.Durability = infos.Durability;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = ItemID;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))

                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                            Map.AddFloorItem(floorItem);

                            SendScreenSpawn(floorItem);
                        }
                        killer.ConquerPoints += 750;
                    }
                }
                else
                {
                    uint amount = 500000;
                    uint ItemID2 = Network.PacketHandler.MoneyItemID(amount);
                    ushort X2 = Owner.X, Y2 = Owner.Y;
                    if (Kernel.Maps[Owner.MapID].SelectCoordonates(ref X2, ref Y2))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Money;
                        floorItem.Value = amount;
                        floorItem.ItemID = ItemID2;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X2;
                        floorItem.Y = Y2;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Kernel.Maps[Owner.MapID].Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Kernel.Maps[(ushort)killer.UID].AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (killer.Owner.Team != null)
                {
                    foreach (Client.GameClient tm in killer.Owner.Team.Teammates)
                    {
                        tm.Entity.SubClasses.StudyPoints += 150;
                    }
                }
                else
                    killer.SubClasses.StudyPoints += 150;
                killer.Owner.ChampionPoints += 100;
              
            }
            #endregion
            #region NemesisTyrant
            if (Name == "NemesisTyrant")
            {
                killer.Owner.ChampionPoints += 150;
                if (killer.Owner.Team != null)
                {
                    foreach (Client.GameClient tm in killer.Owner.Team.Teammates)
                    {
                        tm.Entity.SubClasses.StudyPoints += 150;
                    }
                }
                else
                    killer.SubClasses.StudyPoints += 150;
                for (int x = 0; x < 3; x++)
                {
                    var infoss = Database.ConquerItemInformation.BaseInformations[3004181];
                    ushort XX = Owner.X, YY = Owner.Y;
                    Game.Map MMap = Kernel.Maps[Owner.MapID];
                    if (MMap.SelectCoordonates(ref XX, ref YY))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = infoss.ID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infoss.Durability;
                        floorItem.Item.Durability = infoss.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = infoss.ID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = XX;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infoss.StackSize;
                        floorItem.Y = YY;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.Owner = killer.Owner;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (MMap.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        MMap.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                for (int x = 0; x < 3; x++)
                {
                    var infoss = Database.ConquerItemInformation.BaseInformations[720027];
                    ushort XX = Owner.X, YY = Owner.Y;
                    Game.Map MMap = Kernel.Maps[Owner.MapID];
                    if (MMap.SelectCoordonates(ref XX, ref YY))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = infoss.ID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infoss.Durability;
                        floorItem.Item.Durability = infoss.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = infoss.ID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = XX;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infoss.StackSize;
                        floorItem.Y = YY;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.Owner = killer.Owner;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (MMap.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        MMap.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (Kernel.Rate(30))
                {
                    uint ItemID = 0;
                    byte type1 = 27;
                    int xxx = Kernel.Random.Next(1, 3);
                    for (int i = 0; i < xxx; i++)
                    {
                        type1 = (byte)Kernel.Random.Next(1, 27);
                        switch (type1)
                        {
                            case 1:
                                ItemID = 724352;
                                break;
                            case 2:
                                ItemID = 724357;
                                break;
                            case 3:
                                ItemID = 724362;
                                break;
                            case 4:
                                ItemID = 724367;
                                break;
                            case 5:
                                ItemID = 724372;
                                break;
                            case 6:
                                ItemID = 724377;
                                break;
                            case 7:
                                ItemID = 724384;
                                break;
                            case 8:
                                ItemID = 724389;
                                break;
                            case 9:
                                ItemID = 724394;
                                break;
                            case 10:
                                ItemID = 724404;
                                break;
                            case 11:
                                ItemID = 724409;
                                break;
                            case 12:
                                ItemID = 724414;
                                break;
                            case 13:
                                ItemID = 724419;
                                break;
                            case 14:
                                ItemID = 724424;
                                break;
                            case 15:
                                ItemID = 724429;
                                break;
                            case 16:
                                ItemID = 724434;
                                break;
                            case 17:
                                ItemID = 724439;
                                break;
                            case 18:
                                ItemID = 724444;
                                break;
                            case 19:
                                ItemID = 724453;
                                break;
                            case 20:
                                ItemID = 724458;
                                break;
                            case 21:
                                ItemID = 724463;
                                break;
                            case 22:
                                ItemID = 724477;
                                break;
                            case 23:
                                ItemID = 724482;
                                break;
                            case 24:
                                ItemID = 724487;
                                break;
                            case 25:
                                ItemID = 724492;
                                break;
                            case 26:
                                ItemID = 724497;
                                break;
                            case 27:
                                ItemID = 724502;
                                break;
                        }
                        var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {
                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color = (Game.Enums.Color)Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = ItemID;
                            floorItem.Item.Plus = floorItem.Item.Plus;
                            floorItem.Item.MaximDurability = infos.Durability;
                            floorItem.Item.Durability = infos.Durability;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos.StackSize;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = ItemID;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))

                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                            Map.AddFloorItem(floorItem);

                            SendScreenSpawn(floorItem);
                        }
                    }
                }
                killer.ConquerPoints += 500;
                Kernel.SpawnNemesis = false;
                foreach (var client in Kernel.GamePool.Values)
                {
                    client.Entity.SendSysMessage(killer.Name + " defeated the Nemesis Tyrant and found its grand treasures!");
                }
            }
            #endregion
            #region Discity
            if (killer.MapID == 3955)
            {
                if (killer.Owner.Quests.GetQuest(QuestID.DisCity1) == null || killer.Owner.Quests.GetQuest(QuestID.DisCity1).QItem.Status != QuestPacket.QuestData.QuestStatus.Accepted)
                    killer.Owner.Quests.Accept(QuestID.DisCity1);
                if (killer.Owner.Inventory.Contains(723085, 5))
                {
                    killer.Owner.MessageBox("You`ve collected 5 Soul Stones. Talk to Solar Saint (253,057) to challenge the next stage.",
                        p => { killer.Teleport(3955, 256, 60); }, null);
                }
                else if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(723085, 0, 1);
                    killer.Owner.Send(new Message("You received " + 1 + " Soul Stone.", System.Drawing.Color.Red, Message.Talk));
                }
            }
            if (killer.MapID == 3956)
            {
                if (killer.Owner.Quests.GetQuest(QuestID.Discity2) == null || killer.Owner.Quests.GetQuest(QuestID.Discity2).QItem.Status != QuestPacket.QuestData.QuestStatus.Accepted)
                    killer.Owner.Quests.Accept(QuestID.Discity2);
                if (killer.Owner.Quests.GetQuest(QuestID.Discity2).Kills1 >= 600)
                {
                    killer.Owner.MessageBox("You`ve earned 600 Slayer Points as required. Hurry and report back to Solar Saint!",
                        p => { killer.Teleport(3956, 219, 343); }, null);
                }
                else if (Name == "HellTroll")
                {
                    killer.Owner.Quests.GetQuest(QuestID.Discity2).Kills1 += 4;
                    killer.Owner.Send(new Message("You killed " + Name + ", and received 4 Slayer Points!", System.Drawing.Color.Red, Message.Talk));
                }
                else if (Name == "Temptress")
                {
                    killer.Owner.Quests.GetQuest(QuestID.Discity2).Kills1 += 1;
                    killer.Owner.Send(new Message("You killed " + Name + ", and received 1 Slayer Points!", System.Drawing.Color.Red, Message.Talk));
                }
                else if (Name == "Centicore")
                {
                    killer.Owner.Quests.GetQuest(QuestID.Discity2).Kills1 += 2;
                    killer.Owner.Send(new Message("You killed " + Name + ", and received 2 Slayer Points!", System.Drawing.Color.Red, Message.Talk));
                }
                else if (Name == "Naga")
                {
                    killer.Owner.Quests.GetQuest(QuestID.Discity2).Kills1 += 3;
                    killer.Owner.Send(new Message("You killed " + Name + ", and received 3 Slayer Points!", System.Drawing.Color.Red, Message.Talk));
                }
            }
            if (killer.MapID == 3957)
            {
                if (Kernel.GetDistance(killer.X, killer.Y, 417, 373) <= 30)
                {
                    killer.Owner.MessageBox("You`ve successfully killed all the 9 Hell Wraiths. Hurry and report back to Solar Saint.", p => { killer.Teleport(3957, 343, 390); });

                }
                else
                {
                    killer.Owner.MessageBox("The sealing crystal on this zone has been removed. Go and break the next crystal.");
                }
            }
            if (Name == "Syren")
            {
                if (Kernel.Rate(20))
                {
                    killer.Owner.Inventory.Add(3006983, 0, 1);
                    killer.Owner.MessageBox("You Got 1 PlutoTokenScrap ! Please Collect 4 Of This");
                }
            }
            if (Name == "UltimatePluto")
            {
                killer.Owner.MessageBox("You finally killed the Ultimate Pluto, and claimed the ultimate victory. Go and claim your rewards from Solar Saint!",
                    p => {killer.Teleport(3958, 149, 40);}, null);
                killer.KilledPluto = true;
            }
            #endregion
            #region TeratoDragon

            if (Name == "TeratoDragon" && killer.MapID == 1002)
            {
                uint ItemID = 0;
                byte type4 = 51;
                for (int m = 0; m < 1; m++)
                {
                    type4 = (byte)Kernel.Random.Next(1, 51);
                    switch (type4)
                    {
                        case 1:
                            ItemID = 800320;
                            break;

                        case 2:
                            ItemID = 822054;
                            break;

                        case 3:
                            ItemID = 800110;
                            break;

                        case 4:
                            ItemID = 820056;
                            break;

                        case 5:
                            ItemID = 822056;
                            break;

                        case 6:
                            ItemID = 822057;
                            break;

                        case 7:
                            ItemID = 822053;
                            break;

                        case 8:
                            ItemID = 800019;
                            break;

                        case 9:
                            ItemID = 800050;
                            break;

                        case 10:
                            ItemID = 800015;
                            break;

                        case 11:
                            ItemID = 800090;
                            break;

                        case 12:
                            ItemID = 800513;
                            break;

                        case 13:
                            ItemID = 800017;
                            break;

                        case 14:
                            ItemID = 800071;
                            break;

                        case 15:
                            ItemID = 800016;
                            break;

                        case 16:
                            ItemID = 823051;
                            break;

                        case 17:
                            ItemID = 800130;
                            break;

                        case 18:
                            ItemID = 800140;
                            break;

                        case 19:
                            ItemID = 800141;
                            break;

                        case 20:
                            ItemID = 800200;
                            break;

                        case 21:
                            ItemID = 800310;
                            break;

                        case 22:
                            ItemID = 800014;
                            break;

                        case 23:
                            ItemID = 800214;
                            break;

                        case 24:
                            ItemID = 800230;
                            break;

                        case 25:
                            ItemID = 800414;
                            break;

                        case 26:
                            ItemID = 822052;
                            break;

                        case 27:
                            ItemID = 800420;
                            break;

                        case 28:
                            ItemID = 800401;
                            break;

                        case 29:
                            ItemID = 800512;
                            break;

                        case 30:
                            ItemID = 823043;
                            break;

                        case 31:
                            ItemID = 800514;
                            break;

                        case 32:
                            ItemID = 800520;
                            break;

                        case 33:
                            ItemID = 800521;
                            break;

                        case 34:
                            ItemID = 800613;
                            break;

                        case 35:
                            ItemID = 800614;
                            break;

                        case 36:
                            ItemID = 800615;
                            break;

                        case 37:
                            ItemID = 824001;
                            break;

                        case 38:
                            ItemID = 800617;
                            break;

                        case 39:
                            ItemID = 800720;
                            break;

                        case 40:
                            ItemID = 800721;
                            break;

                        case 41:
                            ItemID = 800070;
                            break;

                        case 42:
                            ItemID = 800723;
                            break;

                        case 43:
                            ItemID = 800724;
                            break;

                        case 44:
                            ItemID = 800018;
                            break;

                        case 45:
                            ItemID = 820001;
                            break;

                        case 46:
                            ItemID = 820052;
                            break;

                        case 47:
                            ItemID = 820053;
                            break;

                        case 48:
                            ItemID = 820054;
                            break;

                        case 49:
                            ItemID = 820055;
                            break;

                        case 50:
                            ItemID = 800722;
                            break;
                        case 51:
                            ItemID = 801001;
                            break;
                    }
                    var infos6 = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    if (killer.Owner.Team != null)
                    {
                        foreach (Client.GameClient tm in killer.Owner.Team.Teammates)
                        {
                            tm.Entity.SubClasses.StudyPoints += 50;
                        }
                    }
                    else
                        killer.SubClasses.StudyPoints += 50;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos6.Durability;
                        floorItem.Item.Durability = infos6.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos6.StackSize;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);

                    }
                    killer.ConquerPoints += 500;
                    foreach (var client in Kernel.GamePool.Values)
                        client.Send(new Message("" + killer.Name + "'s team killed the Terato Dragon. All team members received 50 Study Points!", System.Drawing.Color.White, Message.Talk));
                }

            }

            #endregion
            #region Labirinth
            #region Slinger-GoldGhost
            if (Name == "Slinger" || Name == "GoldGhost")
            {
                uint Uid = 0;
                byte type = (byte)Kernel.Random.Next(1, 45);
                switch (type)
                {
                    case 5:
                    case 4:
                    case 6:
                    case 8:
                        if (!killer.Owner.Inventory.Contains(3007529, 5))
                        {
                            killer.Owner.Inventory.Add(3007529, 0, 1);
                        }
                        break;
                    case 32:
                    case 30:
                        if (killer.Owner.Inventory.Contains(3007528, 5))
                            return;
                        killer.Owner.Inventory.Add(3007528, 0, 1);
                        killer.Update(_String.Effect, "waybee", true);
                        break;
                    case 25:
                    case 24:
                        if (!killer.Owner.Inventory.Contains(721537, 5))
                        {
                            killer.Owner.Inventory.Add(721537, 0, 1);
                        }
                        break;
                    case 2:
                    case 7:
                    case 9:
                        Uid = 721533;
                        break;
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                        killer.CycloneStamp = Time32.Now;
                        killer.CycloneTime = 20;
                        killer.AddFlag(Update.Flags.Cyclone);
                        killer.Owner.Send(new Message("You received Storm effect!", System.Drawing.Color.CadetBlue, Message.System));
                        break;
                }
                if (Uid != 0)
                {
                    killer.Owner.Inventory.Add(Uid, 0, 1);
                }
            }
            #endregion
            #region Bladeling-AgileRat
            if (Name == "Bladeling" || Name == "AgileRat")
            {
                uint Uid = 0;
                byte type = (byte)Kernel.Random.Next(1, 45);
                switch (type)
                {
                    case 5:
                    case 6:
                    case 8:
                    case 4:
                        if (!killer.Owner.Inventory.Contains(3007529, 5))
                            killer.Owner.Inventory.Add(3007529, 0, 1);
                        break;
                    
                    case 30:
                    case 32:
                        if (!killer.Owner.Inventory.Contains(3007528, 5))
                        {
                            killer.Owner.Inventory.Add(3007528, 0, 1);
                            killer.Update(_String.Effect, "waybee", true);
                        }
                        break;
                    case 24:
                        if (!killer.Owner.Inventory.Contains(721538, 5))
                        killer.Owner.Inventory.Add(721538, 0, 1);
                        break;
                    case 25:
                        if (!killer.Owner.Inventory.Contains(721538, 5))
                        killer.Owner.Inventory.Add(721538, 0, 1);
                        break;
                    case 2:
                    case 7:
                    case 9:
                        Uid = 721534;
                        break;
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                        killer.CycloneStamp = Time32.Now;
                        killer.CycloneTime = 20;
                        killer.AddFlag(Update.Flags.Cyclone);
                        killer.Owner.Send(new Message("You received Storm effect!", System.Drawing.Color.CadetBlue, Message.System));
                        break;
                }

                if (Uid != 0)
                {
                    killer.Owner.Inventory.Add(Uid, 0, 1);
                }
            }
            #endregion
            #region FiendBat-BlueBird
            if (Name == "FiendBat" || Name == "BlueBird")
            {
                uint Uid = 0;
                byte type = (byte)Kernel.Random.Next(1, 45);
                switch (type)
                {
                    case 5:
                    case 4:
                    case 6:
                    case 8:
                        if (!killer.Owner.Inventory.Contains(3007529, 5))
                            killer.Owner.Inventory.Add(3007529, 0, 1);
                        break;
                    case 30:
                    case 32:
                        if (!killer.Owner.Inventory.Contains(3007528, 5))
                        {
                            killer.Owner.Inventory.Add(3007528, 0, 1);
                            killer.Update(_String.Effect, "waybee", true);
                        }
                        break;
                    case 24:
                    case 25:
                        if (killer.Owner.Inventory.Contains(721539, 5))
                        killer.Owner.Inventory.Add(721539, 0, 1);
                        break;
                    case 2:
                    case 7:
                    case 9:
                        Uid = 721535;
                        break;
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                        killer.CycloneStamp = Time32.Now;
                        killer.CycloneTime = 20;
                        killer.AddFlag(Update.Flags.Cyclone);
                        killer.Owner.Send(new Message("You received Storm effect!", System.Drawing.Color.CadetBlue, Message.System));
                        break;
                }

                if (Uid != 0)
                {
                    killer.Owner.Inventory.Add(Uid, 0, 1);
                }
            }
            #endregion
            #region MinotaurL120
            if (Name == "MinotaurL120")
            {
                uint Uid = 0;
                byte type = (byte)Kernel.Random.Next(1, 45);
                switch (type)
                {
                    case 5:
                    case 4:
                    case 6:
                    case 8:
                        if (!killer.Owner.Inventory.Contains(3007529, 5))
                            killer.Owner.Inventory.Add(3007529, 0, 1);
                        break;
                    case 30:
                    case 32:
                        if (!killer.Owner.Inventory.Contains(3007528, 5))
                        {
                            killer.Owner.Inventory.Add(3007528, 0, 1);
                            killer.Update(_String.Effect, "waybee", true);
                        }
                        break;
                    case 2:
                    case 7:
                    case 9:
                        Uid = 721536;
                        break;
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                        killer.CycloneStamp = Time32.Now;
                        killer.CycloneTime = 20;
                        killer.AddFlag(Update.Flags.Cyclone);
                        killer.Owner.Send(new Message("You received Storm effect!", System.Drawing.Color.CadetBlue, Message.System));
                        break;
                }

                if (Uid != 0)
                {
                    killer.Owner.Inventory.Add(Uid, 0, 1);
                }
            }
            #endregion
            #endregion
            #region ThunderStrike
            if (killer.MapID == 3935)
            {
                if (killer.Owner.Quests.GetQuest(QuestID.ThunderStrike) == null || killer.Owner.Quests.CheckQuest(QuestID.ThunderStrike) == QuestPacket.QuestData.QuestStatus.Available)
                    killer.Owner.Quests.Accept(QuestID.ThunderStrike);
                if (killer.TodayStrikePoints != 500)
                {
                    if (killer.StrikePoints < 300)
                    {
                        int R = 5;
                        if (Name == "RealmSolider")
                        {
                            R = Kernel.Random.Next(1, 5);
                        }
                        if (Name.Contains("MilitaryOfficer"))
                        {
                            killer.Owner.Send(new Message("A flood of enemy soldiers emerged from the haze when their leader was killed. Go and clear them!", System.Drawing.Color.Red, 2005));
                            R = Kernel.Random.Next(20, 20);
                        }
                        if (Name == "KylinChariot")
                        {
                            R = Kernel.Random.Next(50, 50);
                        }
                        killer.TodayStrikePoints += (uint)R;
                        killer.StrikePoints += (uint)R;
                        killer.Owner.Quests.ThunderStrike(QuestID.ThunderStrike, (uint)R);
                        if (killer.TodayStrikePoints >= 300)
                        {
                            killer.StrikePoints = 300;
                            killer.TodayStrikePoints = 300;
                        }
                        killer.Owner.Send(new Message("You've received " + killer.StrikePoints + " Strike Points by killing enemies firecrely. When you earn 300 Strike Points , you can claim a reward from the Kingdom Mission Envoy.", System.Drawing.Color.Red, Message.System));
                    }
                    else
                    {
                        killer.Owner.MessageBox("You`ve completed the [Thunder Strike] mission in the realm. Report back to the Kingdom Mission Envoy to claim your reward.");
                        killer.Owner.Send(new Message("You've earend 300 Strike Points by killing enemies firercely. Go claim your reward from the Kingdom Mission Envoy.", System.Drawing.Color.Red, Message.System));
                    }
                }
            }
            #endregion
            #region BrightFortune
            #region AngerRat
            if (Name == "AngerRat")
            {
                #region AngerRat
                if (killer.Owner.Quests.GetQuest(QuestID.BloodVengeance) != null && killer.Owner.Quests.GetQuest(QuestID.BloodVengeance).Kills < 10)
                {
                    killer.Owner.Quests.IncreaseQuestKills(QuestID.BloodVengeance, 1);
                }
                else if (killer.Owner.Quests.GetQuest(QuestID.BloodVengeance) != null && killer.Owner.Quests.GetQuest(QuestID.BloodVengeance).Kills >= 10)
                {
                    killer.Owner.MessageBox("You`ve eliminated enough number of Anger Rats. Hurry and report back to Chong Yan Elder!",
                       p =>
                       {
                           Data data = new Data(true);
                           data.UID = p.Entity.UID;
                           data.TimeStamp2 = 19162;
                           data.ID = 162;
                           data.wParam1 = 142;
                           data.wParam2 = 320;
                           p.Send(data);
                       }, null);
                }
                #endregion
            }
            #endregion
            #region FlameRockMonste
            if (Name.Contains("FlameRockMonste"))
            {
                #region FlameRockMonste
                if (killer.Owner.Inventory.Contains(3008752, 8))
                {

                    killer.Owner.MessageBox("You`ve collected enough number of Rune Fragments. Go and try to complete the runes on the Forge Furnace.",
                        p =>
                        {
                            Data data = new Data(true);
                            data.UID = p.Entity.UID;
                            data.TimeStamp2 = 19163;
                            data.ID = 162;
                            data.wParam1 = 311;
                            data.wParam2 = 449;
                            p.Send(data);
                        }, null);
                }
                else
                {
                    if (Kernel.Rate(20))
                    {

                        killer.Owner.Inventory.Add(3008752, 0, 1);
                        _String str = new _String(true);
                        str.UID = killer.Owner.Entity.UID;
                        str.TextsCount = 1;
                        str.Type = _String.Effect;
                        str.Texts.Add("zf2-e123");
                        killer.Owner.SendScreen(str, true);

                    }

                }
                #endregion
            }
            #endregion
            #region VioletBatKing
            if (Name == "VioletBatKing")
            {
                #region VioletBatKing
                if (!killer.Owner.Inventory.Contains(3008750, 1) && killer.Owner.Quests.GetQuest(QuestID.SecretOfBright) != null)
                {
                    killer.Owner.Inventory.Add(3008750, 0, 1);
                    killer.Owner.MessageBox("The Violet Bat King fell down and dropped an ancient-style hammer. Hurry and take this hammer to the Forge Furnace.",
                        p =>
                        {
                            Data data = new Data(true);
                            data.UID = p.Entity.UID;
                            data.TimeStamp2 = 19163;
                            data.ID = 162;
                            data.wParam1 = 311;
                            data.wParam2 = 449;
                            p.Send(data);
                        }, null);
                    killer.Owner.Quests.IncreaseQuestKills(QuestID.SecretOfBright, 1);
                }
                #endregion
            }
            #endregion
            #region LavaScorpion
            if (Name == "LavaScorpion")
            {
                #region LavaScorpion
                if (killer.Owner.Quests.GetQuest(QuestID.SacrificetotheDead) != null && killer.Owner.Quests.GetQuest(QuestID.SacrificetotheDead).Kills >= 15)
                {
                    killer.Owner.MessageBox("You`ve defeated enough number of Lava Scorpions. Now, you can appease the sacrificed Bright people.",
                       p =>
                       {
                           Data data = new Data(true);
                           data.UID = p.Entity.UID;
                           data.TimeStamp2 = 19164;
                           data.ID = 162;
                           data.wParam1 = 251;
                           data.wParam2 = 277;
                           p.Send(data);
                       }, null);
                }
                else if (killer.Owner.Quests.GetQuest(QuestID.SacrificetotheDead) != null && killer.Owner.Quests.GetQuest(QuestID.SacrificetotheDead).Kills1 < 15)
                {
                    killer.Owner.Quests.IncreaseQuestKills5(QuestID.SacrificetotheDead, 1);

                }
                #endregion
            }
            #endregion
            #region RoaringSnake
            if (Name == "RoaringSnake")
            {
                #region RoaringSnake
                if (killer.Owner.Inventory.Contains(3008742, 50) && killer.Owner.Quests.GetQuest(QuestID.SorrowofDesolation) != null)
                {
                    killer.Owner.MessageBox("You`ve collect enough number of Building Stones. Go and try to restore the ruined altar.",
                       p =>
                       {
                           Data data = new Data(true);
                           data.UID = p.Entity.UID;
                           data.ID = 162;
                           data.wParam1 = 578;
                           data.wParam2 = 419;
                           p.Send(data);
                       }, null);
                }
                else
                {
                    killer.Owner.Inventory.Add(3008742, 0, 1);
                }
                #endregion
            }
            #endregion
            #region ClawedRockDevil
            if (Name == "ClawedRockDevil")
            {
                #region ClawedRockDevil
                if (killer.Owner.Inventory.Contains(3008743, 1))
                {
                }
                else
                {

                    if (Kernel.Rate(40))
                    {
                        killer.Owner.Inventory.Add(3008743, 0, 1);
                        killer.Owner.MessageBox("You`ve retrieved the Wheel of Nature from the Clawed Rock Devil. Hurry and take the next action at the altar.",
                           p =>
                           {
                               Data data = new Data(true);
                               data.UID = p.Entity.UID;
                               data.TimeStamp2 = 19165;
                               data.ID = 162;
                               data.wParam1 = 573;
                               data.wParam2 = 423;
                               p.Send(data);
                           }, null);

                    }
                }
                #endregion
            }
            #endregion
            #region HornedRockBeast
            if (Name == "HornedRockBeast")
            {
                #region HornedRockBeast
                if (!killer.Owner.Inventory.Contains(3008745, 1))
                {
                    if (killer.Owner.Inventory.Contains(3008754, 5))
                    {

                        killer.Owner.Inventory.Add(3008745, 0, 1);
                        killer.Owner.Inventory.Remove(3008754, 5);
                        killer.Owner.MessageBox("You received the Earth Force! Hurry and transform it into Metal Force through the Wheel of Nature!",
                           p =>
                           {
                               Data data = new Data(true);
                               data.UID = p.Entity.UID;
                               data.TimeStamp2 = 19165;
                               data.ID = 162;
                               data.wParam1 = 578;
                               data.wParam2 = 419;
                               p.Send(data);
                           }, null);

                    }
                    else
                    {

                        if (Kernel.Rate(40))
                        {
                            killer.Owner.Inventory.Add(3008754, 0, 1);

                        }
                    }
                }
                #endregion
            }
            #endregion
            #region HellfireBeast
            if (Name == "HellfireBeast")
            {
                #region HellfireBeast
                if (killer.Owner.Inventory.Contains(3008748, 100))
                {
                    killer.Owner.MessageBox("You`ve collected enough number of Star Ores. Go and try to extract the Essence of Star at the altar.",
                       p =>
                       {
                           Data data = new Data(true);
                           data.UID = p.Entity.UID;
                           data.TimeStamp2 = 19165;
                           data.ID = 162;
                           data.wParam1 = 571;
                           data.wParam2 = 424;
                           p.Send(data);
                       }, null);

                }
                else
                {
                    killer.Owner.Inventory.Add(3008748, 0, 5);

                }
                #endregion
            }
            #endregion
            #endregion
            #region CrystalBounty
            if (killer.MapID == 3935)
            {
                if (killer.Owner.Quests.CheckQuest(QuestID.CrystalBounty) != QuestPacket.QuestData.QuestStatus.Finished)
                {
                    if (killer.Owner.Quests.GetQuest(QuestID.CrystalBounty) == null || killer.Owner.Quests.CheckQuest(QuestID.CrystalBounty) == QuestPacket.QuestData.QuestStatus.Available)
                        killer.Owner.Quests.Accept(QuestID.CrystalBounty);
                    if (killer.Owner.Quests.GetQuest(QuestID.CrystalBounty).Kills < 50)
                    {
                        int R = 5;
                        if (Name == "RealmSolider")
                        {
                            R = Kernel.Random.Next(1, 1);
                        }
                        if (Name.Contains("MilitaryOfficer"))
                        {

                            R = Kernel.Random.Next(1, 1);
                        }
                        if (Name == "KylinChariot")
                        {
                            R = Kernel.Random.Next(10, 10);
                        }
                        killer.Owner.Quests.CrystalBounty(QuestID.CrystalBounty, (uint)R);
                        if (killer.Owner.Quests.GetQuest(QuestID.CrystalBounty).Kills >= 50)
                        {
                            killer.Owner.Send(new Message("You've eliminated 50 enemies. Hurry and claim a bounty from the Realm Crystal.", System.Drawing.Color.Red, Message.System));
                        }
                    }
                    else
                    {
                        killer.Owner.Send(new Message("You've eliminated 50 enemies. Hurry and claim a bounty from the Realm Crystal.", System.Drawing.Color.Red, Message.System));
                    }
                }
            }
            #endregion
            #region BeastsOfLegend
            if (killer.MapID == 3935)
            {
                if (killer.Owner.Quests.GetQuest(QuestID.BeastsOfLegend) == null || killer.Owner.Quests.CheckQuest(QuestID.BeastsOfLegend) == QuestPacket.QuestData.QuestStatus.Available)
                    killer.Owner.Quests.Accept(QuestID.BeastsOfLegend);
                if (killer.Owner.Quests.GetQuest(QuestID.BeastsOfLegend).QItem.Status != QuestPacket.QuestData.QuestStatus.Finished)
                {
                    if (Name.Contains("VermilionBirdDef") || Name.Contains("WhiteTigerDefend") || Name.Contains("AzureDragonDefen") || Name.Contains("BlackTurtleDefen"))
                    {
                        if (Kernel.Rate(30))
                        {
                            Database.MonsterInformation monster = new Database.MonsterInformation();

                            monster.Hitpoints = 500000;
                            monster.Level = 140;
                            if (Name.Contains("AzureDragonDefen"))
                            {
                                monster.Mesh = 711;
                                monster.Name = "AzureDragon";
                                monster.ID = 7885;
                            }
                            if (Name.Contains("BlackTurtleDefen"))
                            {
                                monster.Mesh = 712;
                                monster.Name = "BlackTurtle";
                                monster.ID = 7888;
                            }
                            if (Name.Contains("WhiteTigerDefend"))
                            {
                                monster.Mesh = 709;
                                monster.Name = "WhiteTiger";
                                monster.ID = 7886;
                            }
                            if (Name.Contains("VermilionBirdDef"))
                            {
                                monster.Mesh = 710;
                                monster.Name = "VermilionBird";
                                monster.ID = 7887;
                            }
                            monster.MaxAttack = 2100;
                            monster.AttackRange = 3;
                            monster.AttackType = 2;
                            monster.AttackSpeed = 1000;
                            monster.ViewRange = 20;
                            monster.MoveSpeed = 1000;
                            monster.RunSpeed = 1000;
                            monster.MinAttack = 1000;
                            Game.Entity entity = new Game.Entity(Game.EntityFlag.Monster, false);
                            entity.MapObjType = Game.MapObjectType.Monster;
                            entity.MonsterInfo = monster;
                            entity.MonsterInfo.Owner = entity;
                            entity.Name = monster.Name;
                            entity.MinAttack = monster.MinAttack;
                            entity.MaxAttack = entity.MagicAttack = monster.MaxAttack;
                            entity.Hitpoints = entity.MaxHitpoints = monster.Hitpoints;
                            entity.Body = monster.Mesh;
                            entity.Level = monster.Level;
                            entity.Defence = 90;
                            entity.X = killer.X;
                            entity.Y = killer.Y;
                            entity.UID = Kernel.Maps[3935].EntityUIDCounter.Next;
                            entity.MapID = 3935;
                            entity.SendUpdates = true;
                            killer.Owner.Map.RemoveEntity(entity);
                            killer.Owner.Map.AddEntity(entity);
                            killer.Owner.Screen.Reload(null);
                        }
                    }
                    if (Name == "AzureDragon" || Name == "WhiteTiger" || Name == "BlackTurtle" || Name == "VermilionBird")
                    {
                        if (killer.Owner.Quests.GetQuest(QuestID.BeastsOfLegend).Kills == 0)
                        {
                            killer.Owner.Quests.IncreaseQuestKills(QuestID.BeastsOfLegend, 1);
                            killer.Owner.MessageBox("You`ve defeated the divine beast. Hurry and claim your reward from the Kingdom Mission Envoy!");
                        }
                    }
                }
            }
            #endregion
            #region Scramble
            if (killer.MapID == 3935)
            {
                if (killer.Owner.Quests.GetQuest(QuestID.ScrambleforJustice) == null || killer.Owner.Quests.CheckQuest(QuestID.ScrambleforJustice) == QuestPacket.QuestData.QuestStatus.Available)
                    killer.Owner.Quests.Accept(QuestID.ScrambleforJustice);
                if (killer.Owner.Quests.GetQuest(QuestID.ScrambleforJustice).QItem.Status != QuestPacket.QuestData.QuestStatus.Finished)
                {
                    if (Name == "AlluringWitch")
                    {
                        killer.Owner.MessageBox("You`ve defeated the AlluringWitch! Here's your reward!");
                        killer.Owner.Quests.FinishQuest(QuestID.ScrambleforJustice);
                        killer.Owner.Inventory.Add("SavageBone");
                    }
                }
            }
            #endregion
            #region SnakeIsland
            if (Name == "SnakeKing")
            {
                int R = Kernel.Random.Next(1, 6);
                if (R == 1)
                {
                    int r = Kernel.Random.Next(1, 5);
                    for (int x = 0; x < r; x++)
                    {
                        uint ItemID = 1088001;
                        var infos6 = Database.ConquerItemInformation.BaseInformations[ItemID];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {
                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color =
                                (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = ItemID;
                            floorItem.Item.MaximDurability = infos6.Durability;
                            floorItem.Item.Durability = infos6.Durability;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = ItemID;
                            floorItem.MapID = Owner.MapID;
                            floorItem.OwnerUID = killer.UID;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos6.StackSize;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))
                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            Map.AddFloorItem(floorItem);
                            SendScreenSpawn(floorItem);
                        }
                    }
                }
                if (R == 2)
                {
                    uint ItemID = 1088000;
                    var infos6 = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color =
                            (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos6.Durability;
                        floorItem.Item.Durability = infos6.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos6.StackSize;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (R == 3)
                {
                    uint ItemID = 700013;
                    var infos6 = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color =
                            (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos6.Durability;
                        floorItem.Item.Durability = infos6.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos6.StackSize;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (R == 4)
                {
                    killer.SubClasses.StudyPoints += (ushort)Kernel.Random.Next(1, 2000);
                }
                if (R == 5)
                {
                    uint ItemID = 0;
                    int x = Kernel.Random.Next(1, 10);
                    if (x == 1)
                        ItemID = 723713;
                    if (x == 2)
                        ItemID = 723714;
                    if (x == 3)
                        ItemID = 723715;
                    if (x == 4)
                        ItemID = 723716;
                    if (x == 5)
                        ItemID = 723717;
                    if (x == 6)
                        ItemID = 723718;
                    if (x == 7)
                        ItemID = 723719;
                    if (x == 8)
                        ItemID = 723720;
                    if (x == 9)
                        ItemID = 723721;
                    if (x == 10)
                        ItemID = 723722;
                    killer.Owner.Inventory.Add(ItemID, 0, 1);

                }
            }
            #endregion
            #region SecondQuest
            if (killer.MapID == 1700)
            {
                if (killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageFour))
                {
                    if (Name == "Satan")
                    {
                        Database.MonsterInformation mt;
                        Database.MonsterInformation.MonsterInformations.TryGetValue(3645, out mt);
                        killer.Owner.map.SpawnMonsterNearToHero(mt, killer.Owner);
                    }
                    if (Name == "BeastSatan")
                    {
                        Database.MonsterInformation mt;
                        Database.MonsterInformation.MonsterInformations.TryGetValue(3646, out mt);
                        killer.Owner.map.SpawnMonsterNearToHero(mt, killer.Owner);
                    }
                    if (Name == "FurySatan")
                    {
                        killer.Owner.Quests.FinishQuest(QuestID.SecondQuestStageFour);
                        _String str = new _String(true);
                        str.UID = killer.UID;
                        str.TextsCount = 1;
                        str.Type = _String.Effect;
                        str.Texts.Add("end_task");
                        killer.Owner.SendScreen(str, true);
                        killer.Teleport(1036, 215, 215);
                        foreach (var plyr in Kernel.GamePool.Values)
                        {
                            plyr.Send(new Message(killer.Name + " has passed all tests and completed second rebirth. Congratulations!", System.Drawing.Color.White, Message.Talk));
                        }
                    }
                }
                if (killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageTwo) != null && killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageTwo).QItem.Status == QuestPacket.QuestData.QuestStatus.Accepted && killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageTwo).Kills < 40000)
                {
                    if (Name == "DemonLord" || Name == "SerpentSpirit" || Name == "FiendLord" || Name == "FuryApe" || Name == "HadesLord" || Name == "DemonBat" || Name == "FurySkeleton" || Name == "FearlessBeast")
                    {
                        killer.Owner.Quests.IncreaseQuestKills(QuestID.SecondQuestStageTwo, 500);
                    }
                    else
                        killer.Owner.Quests.IncreaseQuestKills(QuestID.SecondQuestStageTwo, 4);
                }
                if (killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageThree) != null && killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageThree).QItem.Status == QuestPacket.QuestData.QuestStatus.Accepted)
                {
                    if (Name == "Andrew" || Name == "Peter" || Name == "Philip" || Name == "Timothy" || Name == "Daphne" || Name == "Victoria" || Name == "Wayne" || Name == "Theodore")
                    {
                        killer.Owner.Quests.IncreaseQuestKills2(QuestID.SecondQuestStageThree, 1);
                    }
                }
                if (killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageFour) != null && killer.Owner.Quests.GetQuest(QuestID.SecondQuestStageFour).QItem.Status == QuestPacket.QuestData.QuestStatus.Accepted)
                {
                    if (Name == "DemonLord" || Name == "SerpentSpirit" || Name == "FiendLord" || Name == "FuryApe" || Name == "HadesLord" || Name == "DemonBat" || Name == "FurySkeleton" || Name == "FearlessBeast")
                    {
                        uint ItemID = 722727;
                        var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {
                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.Item = new Network.GamePackets.ConquerItem(true);
                            floorItem.Item.Color =
                                (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                            floorItem.Item.ID = ItemID;
                            floorItem.Item.MaximDurability = infos.Durability;
                            floorItem.Item.Durability = infos.Durability;
                            floorItem.Item.MobDropped = true;
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                            floorItem.ItemID = ItemID;
                            floorItem.MapID = Owner.MapID;
                            floorItem.Item.StackSize = 1;
                            floorItem.Item.MaxStackSize = infos.StackSize;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.ItemColor = floorItem.Item.Color;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))
                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            Map.AddFloorItem(floorItem);
                            SendScreenSpawn(floorItem);
                        }
                    }
                }
                if (Kernel.Rate(7) || (Kernel.Rate(80) && Name == "DemonLord" || Name == "SerpentSpirit" || Name == "FiendLord" || Name == "FuryApe" || Name == "HadesLord" || Name == "DemonBat" || Name == "FurySkeleton" || Name == "FearlessBeast"))
                {
                    int r = Kernel.Random.Next(1, 9);
                    uint ItemID = 0;
                    if (r == 1)
                        ItemID = 722733;
                    if (r == 2)
                        ItemID = 722734;
                    if (r == 3)
                        ItemID = 722735;
                    if (r == 4)
                        ItemID = 722737;
                    if (r == 5)
                        ItemID = 722738;
                    if (r == 6)
                        ItemID = 722739;
                    if (r == 7 && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                        ItemID = 722723;
                    if (r == 8 && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                        ItemID = 722724;
                    if (r == 9 && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                        ItemID = 722725;
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color =
                            (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos.StackSize;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (Name == "HillSpirit" && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                {
                    uint ItemID = 722722;
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color =
                            (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos.StackSize;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (Name == "SwiftDevil" && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                {
                    uint ItemID = 722726;
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color =
                            (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos.StackSize;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (Name == "Banshee" && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                {
                    uint ItemID = 722729;
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Nyx.Server.Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color =
                            (Nyx.Server.Game.Enums.Color)Nyx.Server.Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos.StackSize;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                if (Name == "CleansingDevil" && killer.Owner.Quests.HasQuest(QuestID.SecondQuestStageOne))
                {
                    uint ItemID = 722731;
                    killer.Owner.Inventory.Add(ItemID, 0, 1);
                }
            }
            #endregion
            #region Cloud Jar
            if (Name == killer.QuestMob)
            {
                killer.QuestKO++; if (killer.QuestKO >= 300)
                {
                    killer.Owner.Send(new Message("You have killed enough monsters for the quest. Go report to the " + killer.QuestFrom + "Captain.", 2005));
                }
            }
            #endregion
            #region DragonBall
            if (Kernel.Rate(1) && Kernel.Rate(50))
            {
                var infoss = Database.ConquerItemInformation.BaseInformations[1088000];
                ushort XX = Owner.X, YY = Owner.Y;
                Game.Map MMap = Kernel.Maps[Owner.MapID];
                if (MMap.SelectCoordonates(ref XX, ref YY))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.Item = new Network.GamePackets.ConquerItem(true);
                    floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                    floorItem.Item.ID = 1088000;
                    floorItem.Item.Plus = floorItem.Item.Plus;
                    floorItem.Item.MaximDurability = infoss.Durability;
                    floorItem.Item.Durability = infoss.Durability;
                    floorItem.Item.MobDropped = true;
                    floorItem.OwnerUID = killer.UID;
                    floorItem.Item.StackSize = 1;
                    floorItem.Item.MaxStackSize = infoss.StackSize;
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                    floorItem.ItemID = 1088000;
                    floorItem.MapID = Owner.MapID;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = XX;
                    floorItem.Y = YY;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.ItemColor = floorItem.Item.Color;
                    floorItem.Owner = killer.Owner;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (MMap.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    MMap.AddFloorItem(floorItem);
                    SendScreenSpawn(floorItem);
                }
                Data data = new Data(true);
                data.UID = killer.UID;
                data.ID = 165;
                killer.Owner.Send(data);
            }
            #endregion
            #region Meteor
            if (Kernel.Rate(1))
            {
                var infoss = Database.ConquerItemInformation.BaseInformations[1088001];
                ushort XX = Owner.X, YY = Owner.Y;
                Game.Map MMap = Kernel.Maps[Owner.MapID];
                if (MMap.SelectCoordonates(ref XX, ref YY))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.Item = new Network.GamePackets.ConquerItem(true);
                    floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                    floorItem.Item.ID = 1088001;
                    floorItem.Item.Plus = floorItem.Item.Plus;
                    floorItem.Item.MaximDurability = infoss.Durability;
                    floorItem.Item.Durability = infoss.Durability;
                    floorItem.Item.MobDropped = true;
                    floorItem.Item.StackSize = 1;
                    floorItem.Item.MaxStackSize = infoss.StackSize;
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                    floorItem.ItemID = 1088001;
                    floorItem.MapID = Owner.MapID;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = XX;
                    floorItem.Y = YY;
                    floorItem.OwnerUID = killer.UID;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.ItemColor = floorItem.Item.Color;
                    floorItem.Owner = killer.Owner;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (MMap.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    MMap.AddFloorItem(floorItem);
                    SendScreenSpawn(floorItem);
                }
            }
            #endregion
            #region CPs Drop
            #region Bags
            if (Kernel.Rate(Constants.ConquerPointsDropRate))
            {
                if (killer != null && killer.Owner != null && Name != "SnowBanshee" && Name != "PurpleBanshee" && Name != "NemesisTyrant" && Name != "LavaBeast" && Name != "TeratoDragon")
                {
                    
                    if (killer.VIPLevel >= 4)
                    {
                        uint amount = 0;
                        if (Kernel.Rate(55))
                            amount = 5;
                        else if (Kernel.Rate(40))
                            amount = 10;
                        else if (Kernel.Rate(30))
                            amount = 15;
                        else if (Kernel.Rate(20))
                            amount = 20;
                        else if (Kernel.Rate(10))
                            amount = 25;
                        else if (Kernel.Rate(5))
                            amount = 30;
                        else if (Kernel.Rate(1, 200))
                            amount = 50;
                        else if (Kernel.Rate(1, 250))
                            amount = 70;
                        else if (Kernel.Rate(1, 300))
                            amount = 100;
                        if (amount != 0)
                        {
                            killer.ConquerPoints += amount;
                            killer.Owner.Send(Constants.PickupConquerPoints(amount));
                        }
                    }
                    else
                    {
                        uint amount = 0;
                        if (Kernel.Rate(55))
                            amount = 720657;
                        else if (Kernel.Rate(30))
                            amount = 720656;
                        else if (Kernel.Rate(20))
                            amount = 720655;
                        else if (Kernel.Rate(15))
                            amount = 720658;
                        else if (Kernel.Rate(10))
                            amount = 720659;
                        else if (Kernel.Rate(1, 250))
                            amount = 720664;
                        else if (Kernel.Rate(1, 300))
                            amount = 720665;
                        if (amount != 0)
                        {
                            var infoss = Database.ConquerItemInformation.BaseInformations[amount];
                            ushort XX = Owner.X, YY = Owner.Y;
                            Game.Map MMap = Kernel.Maps[Owner.MapID];
                            if (MMap.SelectCoordonates(ref XX, ref YY))
                            {
                                Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                                floorItem.Item = new Network.GamePackets.ConquerItem(true);
                                floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                                floorItem.Item.ID = amount;
                                floorItem.Item.Plus = floorItem.Item.Plus;
                                floorItem.Item.MaximDurability = infoss.Durability;
                                floorItem.Item.Durability = infoss.Durability;
                                floorItem.Item.MobDropped = true;
                                floorItem.OwnerUID = killer.UID;
                                floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                                floorItem.ItemID = amount;
                                floorItem.MapID = Owner.MapID;
                                floorItem.MapObjType = Game.MapObjectType.Item;
                                floorItem.X = XX;
                                floorItem.Item.StackSize = 1;
                                floorItem.Item.MaxStackSize = infoss.StackSize;
                                floorItem.Y = YY;
                                floorItem.Type = Network.GamePackets.FloorItem.Drop;
                                floorItem.OnFloor = Time32.Now;
                                floorItem.Owner = killer.Owner;
                                floorItem.ItemColor = floorItem.Item.Color;
                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                                while (MMap.Npcs.ContainsKey(floorItem.UID))
                                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                                MMap.AddFloorItem(floorItem);
                                SendScreenSpawn(floorItem);
                            }
                        }
                    }
                    
                }
            }
                    #endregion
            #region CPs
            if (Kernel.Rate(100))
            {
                if (killer != null && killer.Owner != null && Name != "SnowBanshee" && Name != "PurpleBanshee" && Name != "NemesisTyrant" && Name != "LavaBeast" && Name != "TeratoDragon")
                {
                    if (killer.InAutoHunt)
                    {
                        killer.ConquerPoints += 1;
                        killer.Owner.Send(new Message("You got 1 CPs by killing a monster! Keep going(AutoHunting).", System.Drawing.Color.Red, Message.TopLeft));
                    }
                    else
                    {
                        uint amount = (uint)Kernel.Random.Next(1, 4);
                        killer.ConquerPoints += amount;
                        killer.Owner.Send(new Message("You got "+amount+" CPs by killing a monster! Keep going.", System.Drawing.Color.Red, Message.TopLeft));

                    }
                }
            }
            #endregion
            #endregion
            #region Gold Drop
            if (Kernel.Rate(Constants.MoneyDropRate))
            {
                if (Kernel.Rate(50))
                {
                    uint amount = (uint)Kernel.Random.Next(Level * 2, Level * 6);
                    if (Level >= 0 && Level <= 5)
                        amount = (uint)Kernel.Random.Next(1, 100);
                    if (amount > 1000000)
                        amount = 6000000;
                    uint ItemID = Network.PacketHandler.MoneyItemID(amount);
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Money;
                        floorItem.Value = amount;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        Map.AddFloorItem(floorItem);
                        SendScreenSpawn(floorItem);
                    }
                }
                else
                {
                    int r = Kernel.Random.Next(1,5);
                    for (int x = 0; x < r; x++)
                    {
                        uint amount = (uint)Kernel.Random.Next(Level * 2, Level * 6);
                        if (Level >= 0 && Level <= 5)
                            amount = (uint)Kernel.Random.Next(1, 100);
                        if (amount > 1000000)
                            amount = 6000000;
                        uint ItemID = Network.PacketHandler.MoneyItemID(amount);
                        ushort X = Owner.X, Y = Owner.Y;
                        Game.Map Map = Kernel.Maps[Owner.MapID];
                        if (Map.SelectCoordonates(ref X, ref Y))
                        {
                            Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                            floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Money;
                            floorItem.Value = amount;
                            floorItem.ItemID = ItemID;
                            floorItem.MapID = Owner.MapID;
                            floorItem.MapObjType = Game.MapObjectType.Item;
                            floorItem.X = X;
                            floorItem.Y = Y;
                            floorItem.Owner = killer.Owner;
                            floorItem.Type = Network.GamePackets.FloorItem.Drop;
                            floorItem.OnFloor = Time32.Now;
                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            while (Map.Npcs.ContainsKey(floorItem.UID))
                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                            Map.AddFloorItem(floorItem);
                            SendScreenSpawn(floorItem);
                        }
                    }
                }
            }
            #endregion
            #region Items Drop
            if (Kernel.Rate(Constants.ItemDropRate))
            {
                uint ItemIDD = GetItem();
                while (ItemIDD == 0 || !Database.ConquerItemInformation.BaseInformations.ContainsKey(ItemIDD))
                {
                    ItemIDD = GetItem();
                }
                var infos = Database.ConquerItemInformation.BaseInformations[ItemIDD];
                ushort X = Owner.X, Y = Owner.Y;
                Game.Map Map = Kernel.Maps[Owner.MapID];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.Item = new Network.GamePackets.ConquerItem(true);
                    floorItem.Item.Color = (Game.Enums.Color)Kernel.Random.Next(4, 8);
                    floorItem.Item.ID = (uint)infos.ID;
                    floorItem.Item.MaximDurability = infos.Durability;
                    floorItem.Item.StackSize = 1;
                    floorItem.Item.Plus = GetItemPlus();
                    floorItem.Item.MaxStackSize = infos.StackSize;
                    floorItem.Item.Durability = (ushort)(Kernel.Random.Next(1,5) * 100);
                    floorItem.Item.Bless = GetItemBless();
                    floorItem.Item.MobDropped = true;
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                    floorItem.ItemID = infos.ID;
                    floorItem.MapID = Owner.MapID;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Item.TimeLeftInMinutes = 0;
                    floorItem.Owner = killer.Owner;
                    floorItem.OwnerUID = killer.UID;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.ItemColor = floorItem.Item.Color;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    SendScreenSpawn(floorItem);
                }

            }
            #endregion
            #region MoonBox
            if (Name == "ToughHorn" && killer.MapID == 1043)
            {
                if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(721010, 0, 1);
                    killer.Owner.MessageBox("You~received~the~Token!");
                }
            }
            if (Name == "ToughHorn" && killer.MapID == 1044)
            {
                if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(721011, 0, 1);
                    killer.Owner.MessageBox("You~received~the~Token!");
                }
            }
            if (Name == "ToughHorn" && killer.MapID == 1045)
            {
                if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(721012, 0, 1);
                    killer.Owner.MessageBox("You~received~the~Token!");
                }
            }
            if (Name == "ToughHorn" && killer.MapID == 1046)
            {
                if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(721013, 0, 1);
                    killer.Owner.MessageBox("You~received~the~Token!");
                }
            }
            if (Name == "ToughHorn" && killer.MapID == 1047)
            {
                if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(721014, 0, 1);
                    killer.Owner.MessageBox("You~received~the~Token!");
                }
            }
            if (Name == "ToughHorn" && killer.MapID == 1048)
            {
                if (Kernel.Rate(5))
                {
                    killer.Owner.Inventory.Add(721015, 0, 1);
                    killer.Owner.MessageBox("You~received~the~Token!");
                }
            }
            #endregion
            #region SpiritBeads
            if (killer != null && killer.EntityFlag == Game.EntityFlag.Player)
            {
                if (killer.Owner.Inventory.Contains(729611, 1)
                || killer.Owner.Inventory.Contains(729612, 1)
                || killer.Owner.Inventory.Contains(729613, 1)
                || killer.Owner.Inventory.Contains(729614, 1)
                || killer.Owner.Inventory.Contains(729703, 1))
                {
                    if (killer.SpiritBeadsCount < 2500)
                    {
                        if (Level < 70)
                        {
                            killer.SpiritBeadsCount++;
                            killer.Owner.Send(new Message("You recevied 1 spirit.", System.Drawing.Color.Red, Message.System));
                        }
                        if (Level >= 70 && Level <= 99)
                        {
                            killer.SpiritBeadsCount += 2;
                            killer.Owner.Send(new Message("You recevied 2 spirit.", System.Drawing.Color.Red, Message.System));
                        }
                        if (Level >= 100 && Level <= 119)
                        {
                            killer.SpiritBeadsCount += 3;
                            killer.Owner.Send(new Message("You recevied 3 spirit.", System.Drawing.Color.Red, Message.System));
                        }
                        if (Level >= 120 && Level <= 140)
                        {
                            killer.SpiritBeadsCount += 4;
                            killer.Owner.Send(new Message("You recevied 4 spirit.", System.Drawing.Color.Red, Message.System));
                        }
                        if (Name.Contains("ThrillingSpook") || Name == "SwordMaster" || Name == "SnowBanshee" || Name == "PurpleBanshee" || Name == "SnowBansheeSoul" || Name == "TeratoDragon")
                        {
                            killer.SpiritBeadsCount += 1000;
                            killer.Owner.Send(new Message("You recevied 1000 spirit.", System.Drawing.Color.Red, Message.System));
                        }
                    }
                }
            }
            #endregion
            #region LavaBeast
            if (Name == "LavaBeast")
            {
                killer.Owner.MessageBox("You~killed~the~Lava~Beast~and~members~in~your~team~each~gained~20~Study~points,~a~refinery~item.");
                if (killer.Owner.Team == null)
                {
                    killer.SubClasses.StudyPoints += 20;
                }
                else
                {
                    foreach (var teammate in killer.Owner.Team.Teammates)
                    {
                        teammate.Entity.SubClasses.StudyPoints += 20;
                    }
                }
                foreach (var client in Kernel.GamePool.Values)
                    client.Send(new Message(killer.Name + " team killed the Lava Beast on Frozen Grotto F5 and each of the team members gained 20 Study Points!", System.Drawing.Color.White, Message.Talk));

                uint ItemID = 0;
                byte type1 = 27;
                for (int i = 0; i < 1; i++)
                {
                    type1 = (byte)Kernel.Random.Next(1, 27);
                    switch (type1)
                    {
                        case 1:
                            ItemID = 724352;
                            break;
                        case 2:
                            ItemID = 724357;
                            break;
                        case 3:
                            ItemID = 724362;
                            break;
                        case 4:
                            ItemID = 724367;
                            break;
                        case 5:
                            ItemID = 724372;
                            break;
                        case 6:
                            ItemID = 724377;
                            break;
                        case 7:
                            ItemID = 724384;
                            break;
                        case 8:
                            ItemID = 724389;
                            break;
                        case 9:
                            ItemID = 724394;
                            break;
                        case 10:
                            ItemID = 724404;
                            break;
                        case 11:
                            ItemID = 724409;
                            break;
                        case 12:
                            ItemID = 724414;
                            break;
                        case 13:
                            ItemID = 724419;
                            break;
                        case 14:
                            ItemID = 724424;
                            break;
                        case 15:
                            ItemID = 724429;
                            break;
                        case 16:
                            ItemID = 724434;
                            break;
                        case 17:
                            ItemID = 724439;
                            break;
                        case 18:
                            ItemID = 724444;
                            break;
                        case 19:
                            ItemID = 724453;
                            break;
                        case 20:
                            ItemID = 724458;
                            break;
                        case 21:
                            ItemID = 724463;
                            break;
                        case 22:
                            ItemID = 724477;
                            break;
                        case 23:
                            ItemID = 724482;
                            break;
                        case 24:
                            ItemID = 724487;
                            break;
                        case 25:
                            ItemID = 724492;
                            break;
                        case 26:
                            ItemID = 724497;
                            break;
                        case 27:
                            ItemID = 724502;
                            break;
                    }
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.StackSize = 1;
                        floorItem.Item.MaxStackSize = infos.StackSize;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.OwnerUID = killer.UID;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Owner = killer.Owner;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))

                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                        Map.AddFloorItem(floorItem);

                        SendScreenSpawn(floorItem);
                    }
                }
            }
            #endregion
            #region SwordMaster
            if (Name == "SwordMaster")
            {
                uint ItemID = 0;
                byte type1 = 30;
                for (int i = 0; i < 4; i++)
                {
                    type1 = (byte)Kernel.Random.Next(1, 30);
                    switch (type1)
                    {
                        case 1:
                            ItemID = 730004;
                            break;
                        case 2:
                            ItemID = 730004;
                            break;
                        case 3:
                            ItemID = 730004;
                            break;
                        case 4:
                            ItemID = 730004;
                            break;
                        case 5:
                            ItemID = 730005;
                            break;
                        case 6:
                            ItemID = 730005;
                            break;
                        case 7:
                            ItemID = 200407;
                            break;
                        case 8:
                            ItemID = 200110;
                            break;
                        case 9:
                            ItemID = 200109;
                            break;
                        case 10:
                            ItemID = 200106;
                            break;
                        case 11:
                            ItemID = 720028;
                            break;
                        case 12:
                            ItemID = 720028;
                            break;
                        case 13:
                            ItemID = 720884;
                            break;
                        case 14:
                            ItemID = 720884;
                            break;
                        case 15:
                            ItemID = 720884;
                            break;
                        case 16:
                            ItemID = 1088000;
                            break;
                        case 17:
                            ItemID = 1088000;
                            break;
                        case 18:
                            ItemID = 1088000;
                            break;
                        case 19:
                            ItemID = 1088000;
                            break;
                        case 20:
                            ItemID = 1088000;
                            break;
                        case 21:
                            ItemID = 723341;
                            break;
                        case 22:
                            ItemID = 723341;
                            break;
                        case 23:
                            ItemID = 723341;
                            break;
                        case 24:
                            ItemID = 723341;
                            break;
                        case 25:
                            ItemID = 723342;
                            break;
                        case 26:
                            ItemID = 723342;
                            break;
                        case 27:
                            ItemID = 723342;
                            break;
                        case 28:
                            ItemID = 200011;
                            break;
                        case 29:
                            ItemID = 200009;
                            break;
                        case 30:
                            ItemID = 200008;
                            break;
                    }
                    var infos = Database.ConquerItemInformation.BaseInformations[ItemID];
                    ushort X = Owner.X, Y = Owner.Y;
                    Game.Map Map = Kernel.Maps[Owner.MapID];
                    if (Map.SelectCoordonates(ref X, ref Y))
                    {
                        Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                        floorItem.Item = new Network.GamePackets.ConquerItem(true);
                        floorItem.Item.Color = (Game.Enums.Color)Kernel.Random.Next(4, 8);
                        floorItem.Item.ID = ItemID;
                        floorItem.Item.Plus = floorItem.Item.Plus;
                        floorItem.Item.MaximDurability = infos.Durability;
                        floorItem.Item.Durability = infos.Durability;
                        floorItem.Item.MobDropped = true;
                        floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                        floorItem.ItemID = ItemID;
                        floorItem.MapID = Owner.MapID;
                        floorItem.MapObjType = Game.MapObjectType.Item;
                        floorItem.X = X;
                        floorItem.Y = Y;
                        floorItem.Type = Network.GamePackets.FloorItem.Drop;
                        floorItem.OnFloor = Time32.Now;
                        floorItem.ItemColor = floorItem.Item.Color;
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                        while (Map.Npcs.ContainsKey(floorItem.UID))

                            floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;

                        Map.AddFloorItem(floorItem);

                        SendScreenSpawn(floorItem);
                    }
                }
                return;
            }
            #endregion
            #region Treasure In The Blue
            if (Name == "CoinsStealer")
            {
                if (Kernel.Rate(6))
                {
                    killer.Owner.Inventory.AddTime(711611, 1 * 60 * 60);
                    killer.Owner.Send(new Message("You activated CopperCoin.", System.Drawing.Color.Red, Message.TopLeft));
                }
                else if (Kernel.Rate(3))
                {
                    killer.Owner.Inventory.AddTime(711610, 1 * 60 * 60);
                    killer.Owner.Send(new Message("You activated SilverCoin.", System.Drawing.Color.Red, Message.TopLeft));
                }
                else if (Kernel.Rate(1))
                {
                    killer.Owner.Inventory.AddTime(711609, 1 * 60 * 60);
                    killer.Owner.Send(new Message("You activated GoldCoin.", System.Drawing.Color.Red, Message.TopLeft));
                }
            }
            if (Name == "SilverOctopus")
            {
                if (Kernel.Rate(50))
                {
                    killer.Owner.Inventory.AddTime(711610, 1 * 60 * 60);
                    killer.Owner.Send(new Message("You activated SilverCoin.", System.Drawing.Color.Red, Message.TopLeft));
                }
            }
            if (Name == "GoldenOctopus")
            {
                int r = Kernel.Random.Next(1, 5);
                uint ItemID = 0;
                if (r == 1)
                    ItemID = 723700;
                if (r == 2)
                    ItemID = 723727;
                if (r == 3)
                    ItemID = 710212;
                if (r == 4)
                    ItemID = 723341;
                if (r == 5)
                    ItemID = 1088001;
                var infoss = Database.ConquerItemInformation.BaseInformations[ItemID];
                ushort XX = Owner.X, YY = Owner.Y;
                Game.Map MMap = Kernel.Maps[Owner.MapID];
                if (MMap.SelectCoordonates(ref XX, ref YY))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.Item = new Network.GamePackets.ConquerItem(true);
                    floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                    floorItem.Item.ID = ItemID;
                    floorItem.Item.Plus = floorItem.Item.Plus;
                    floorItem.Item.MaximDurability = infoss.Durability;
                    floorItem.Item.Durability = infoss.Durability;
                    floorItem.Item.MobDropped = true;
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = Owner.MapID;
                    floorItem.OwnerUID = killer.UID;
                    floorItem.Item.StackSize = 1;
                    floorItem.Item.MaxStackSize = infoss.StackSize;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = XX;
                    floorItem.Owner = killer.Owner;
                    floorItem.Y = YY;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.ItemColor = floorItem.Item.Color;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (MMap.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    MMap.AddFloorItem(floorItem);
                    SendScreenSpawn(floorItem);
                }
            }
            #endregion
            #region ProtectionPill
            if (Kernel.Rate(1, 200) && killer.PKMode == Game.Enums.PKMode.Jiang)
            {
                var infoss = Database.ConquerItemInformation.BaseInformations[3002029];
                ushort XX = Owner.X, YY = Owner.Y;
                Game.Map MMap = Kernel.Maps[Owner.MapID];
                if (MMap.SelectCoordonates(ref XX, ref YY))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.Item = new Network.GamePackets.ConquerItem(true);
                    floorItem.Item.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                    floorItem.Item.ID = 3002029;
                    floorItem.Item.Plus = floorItem.Item.Plus;
                    floorItem.Item.MaximDurability = infoss.Durability;
                    floorItem.Item.Durability = infoss.Durability;
                    floorItem.Item.MobDropped = true;
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.Item;
                    floorItem.ItemID = 3002029;
                    floorItem.OwnerUID = killer.UID;
                    floorItem.MapID = Owner.MapID;
                    floorItem.Item.StackSize = 1;
                    floorItem.Item.MaxStackSize = infoss.StackSize;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = XX;
                    floorItem.Owner = killer.Owner;
                    floorItem.Y = YY;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.ItemColor = floorItem.Item.Color;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (MMap.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    MMap.AddFloorItem(floorItem);
                    SendScreenSpawn(floorItem);
                }
            }
            #endregion
            #region Monster Spawn
            #region Titan
            if (Name.Contains("Titan"))
            {
                Kernel.Titan = false;
                return;
            }
            #endregion
            #region Ganoderma
            if (Name.Contains("Ganoderma"))
            {
                Kernel.Ganoderma = false;
                return;
            }
            #endregion
            #region AlluringWitch
            if (Name == "AlluringWitch")
            {
                Kernel.AlluringWitchHisCrystals = false;
                return;
            }
            #endregion
            #endregion 
        }
        public static SafeDictionary<uint, MonsterInformation> MonsterInformations =
            new SafeDictionary<uint, MonsterInformation>(8000);
        public static void Load()
        {
            using (var command = new MySqlCommand(MySqlCommandType.SELECT))
            {
                command.Select("monsterinfos");
                using (var reader = command.CreateReader())
                {

                    while (reader.Read())
                    {
                        MonsterInformation mf = new MonsterInformation();
                        mf.ID = reader.ReadUInt32("id");
                        mf.Name = reader.ReadString("name");
                        mf.Type = reader.ReadUInt32("type");
                        mf.Mesh = reader.ReadUInt16("lookface");
                        mf.Level = reader.ReadByte("level");
                        mf.Hitpoints = reader.ReadUInt32("life");
                        IniFile IniFile = new IniFile(Constants.MonstersPath);
                        if (IniFile.ReadString(mf.Name, "MaxLife") != "")
                        {
                            if (uint.Parse(IniFile.ReadString(mf.Name, "MaxLife")) != 0)
                            {
                                mf.Hitpoints = uint.Parse(IniFile.ReadString(mf.Name, "MaxLife"));
                                byte boss = byte.Parse(IniFile.ReadString(mf.Name, "Boss"));
                                if (boss == 0)
                                    mf.Boss = false;
                                else mf.Boss = true;
                            }
                        }
                        mf.Guard = mf.Name.Contains("Guard");
                        mf.ViewRange = reader.ReadUInt16("view_range");
                        mf.AttackRange = reader.ReadUInt16("attack_range");
                        mf.Defence = reader.ReadUInt16("defence");
                        mf.AttackType = reader.ReadByte("attack_user");
                        mf.MinAttack = reader.ReadUInt32("attack_min");
                        mf.MaxAttack = reader.ReadUInt32("attack_max");
                        mf.SpellID = reader.ReadUInt16("magic_type");
                        mf.MoveSpeed = reader.ReadInt32("move_speed");
                        mf.RunSpeed = reader.ReadInt32("run_speed");
                        mf.OwnItemID = reader.ReadUInt32("ownitem");
                        mf.OwnItemRate = reader.ReadUInt32("ownitemrate");
                        mf.OwnItemID2 = reader.ReadUInt32("ownitem2");
                        mf.OwnItemRate2 = reader.ReadUInt32("ownitemrate2");
                        mf.OwnItemID3 = reader.ReadUInt32("ownitem3");
                        mf.OwnItemRate3 = reader.ReadUInt32("ownitemrate3");
                        mf.AttackSpeed = reader.ReadInt32("attack_speed");
                        mf.ExtraExperience = reader.ReadUInt32("extra_exp");
                        if (mf.MoveSpeed <= 500)
                            mf.MoveSpeed += 500;
                        if (mf.AttackSpeed <= 500)
                            mf.AttackSpeed += 500;
                        MonsterInformations.Add(mf.ID, mf);
                    }
                }
            }

        }
        public MonsterInformation Copy()
        {
            MonsterInformation mf = new MonsterInformation();
            mf.ID = this.ID;
            mf.Name = this.Name;
            mf.Mesh = this.Mesh;
            mf.Level = this.Level;
            mf.Hitpoints = this.Hitpoints;
            mf.ViewRange = this.ViewRange;
            mf.AttackRange = this.AttackRange;
            mf.AttackType = this.AttackType;
            mf.MinAttack = this.MinAttack;
            mf.MaxAttack = this.MaxAttack;
            mf.SpellID = this.SpellID;
            mf.MoveSpeed = this.MoveSpeed;
            mf.RunSpeed = this.RunSpeed;
            mf.AttackSpeed = this.AttackSpeed;
            mf.BoundX = this.BoundX;
            mf.BoundY = this.BoundY;
            mf.BoundCX = this.BoundCX;
            mf.BoundCY = this.BoundCY;
            mf.RespawnTime = this.RespawnTime;
            mf.ExtraExperience = this.ExtraExperience;
            mf.OwnItemID = this.OwnItemID;
            mf.HPPotionID = this.HPPotionID;
            mf.MPPotionID = this.MPPotionID;
            mf.OwnItemRate = this.OwnItemRate;
            mf.Boss = this.Boss;
            mf.Guard = this.Guard;
            mf.Defence = this.Defence;
            return mf;
        }
    }
}