using Nyx.Server.Client;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Packets
{
    public class MsgItem
    {
        [Network.Packet((ushort)Network.PacketType.MsgItem)]
        public static async Task Process(GameClient client, byte[] packet)
        {
            if (client.Action != 2)
                return;
            ItemUsage usage = new ItemUsage(false);
            usage.Deserialize(packet);
            if (!client.Entity.Dead || usage.ID == ItemUsage.Ping)
            {
                switch (usage.ID)
                {


                    case 53:
                        {
                            uint ItemAdd = BitConverter.ToUInt32(packet, 12);
                            ConquerItem item_new = null;
                            if (client.Inventory.TryGetItem(ItemAdd, out item_new))
                            {
                                uint obtined_points = 0;
                                Database.ConquerItemInformation iteminfo = new Nyx.Server.Database.ConquerItemInformation(ItemAdd, 0);
                                Dictionary<uint, uint> amount = new Dictionary<uint, uint>();//packet[20]);
                                for (ushort i = 88; i < 88 + 4 * packet[24]; i += 4)
                                {
                                    uint item_swap = BitConverter.ToUInt32(packet, i);
                                    if (client.Inventory.ContainsUID(item_swap))
                                    {
                                        ConquerItem item = null;
                                        if (client.Inventory.TryGetItem(item_swap, out item))
                                        {
                                            amount.Add(item_swap, i);
                                        }
                                        switch (item.ID)
                                        {
                                            case 191505:
                                            case 191605:
                                            case 191705:
                                            case 191805:
                                            case 191905:
                                            case 191405:
                                            case 183325:
                                            case 183315:
                                            case 183375:
                                            case 183305:
                                                {
                                                    obtined_points += 300;
                                                    break;
                                                }
                                            default:
                                                obtined_points += 50;
                                                break;
                                        }
                                    }
                                }
                                if (iteminfo.BaseInformation.ConquerPointsWorth > obtined_points)
                                {
                                    uint add_cps = 0;
                                    add_cps = (uint)(iteminfo.BaseInformation.ConquerPointsWorth - obtined_points);
                                    if (add_cps < client.Entity.ConquerPoints)
                                    {
                                        client.Entity.ConquerPoints -= (uint)add_cps;
                                        foreach (uint key in amount.Keys)
                                        {
                                            if (client.Inventory.ContainsUID(key))
                                                client.Inventory.Remove(key, Nyx.Server.Game.Enums.ItemUse.Remove, true);
                                        }
                                        client.Inventory.Add(ItemAdd, 0, 1);

                                    }
                                }
                                else
                                {
                                    foreach (uint key in amount.Keys)
                                    {
                                        if (client.Inventory.ContainsUID(key))
                                            client.Inventory.Remove(key, Nyx.Server.Game.Enums.ItemUse.Remove, true);
                                    }
                                    client.Inventory.Add(ItemAdd, 0, 1);

                                }
                            }
                            break;
                        }
                    case 56:
                        {
                            ConquerItem Sash;
                            if (client.Inventory.TryGetItem(usage.UID, out Sash))
                            {
                                if (client.Entity.ExtraInventory < 300)
                                {
                                    if (Sash.ID == 1100003)
                                    {
                                        if (client.Entity.ExtraInventory < 297)
                                        {
                                            client.Entity.ExtraInventory += 3;
                                        }
                                        else
                                        {
                                            client.Entity.ExtraInventory = 300;
                                        }
                                        client.Inventory.Remove(Sash, Game.Enums.ItemUse.Remove);
                                    }
                                    else if (Sash.ID == 1100006)
                                    {
                                        if (client.Entity.ExtraInventory < 294)
                                        {
                                            client.Entity.ExtraInventory += 6;
                                        }
                                        else
                                        {
                                            client.Entity.ExtraInventory = 300;
                                        }
                                        client.Inventory.Remove(Sash, Game.Enums.ItemUse.Remove);
                                    }
                                    else if (Sash.ID == 1100009)
                                    {
                                        if (client.Entity.ExtraInventory < 288)
                                        {
                                            client.Entity.ExtraInventory += 12;
                                        }
                                        else
                                        {
                                            client.Entity.ExtraInventory = 300;
                                        }
                                        client.Inventory.Remove(Sash, Game.Enums.ItemUse.Remove);
                                    }
                                }
                            }
                            break;
                        }

                    case ItemUsage.MainEquipment:
                    case ItemUsage.AlternateEquipment:
                        {
                            if (client.Entity.ContainsFlag3(Update.Flags3.Assassin))
                            {
                                client.Send(
                                    new Network.GamePackets.Message("You cannot switch between equipments while being an assasin!",
                                        Color.Red, Network.GamePackets.Message.Agate));
                                return;
                            }
                            if (client.Entity.ContainsFlag(Update.Flags.Fly))
                            {
                                client.Send(new Network.GamePackets.Message("You cannot switch equipment during flight.", System.Drawing.Color.Red, Network.GamePackets.Message.Talk));
                                return;
                            }
                            if (client.Equipment.Free(ConquerItem.AlternateRightWeapon) && !client.Equipment.Free(ConquerItem.AlternateLeftWeapon))
                            {
                                client.Send(new Network.GamePackets.Message("Invalid weapons! Missing the important weapons? Unequip the alternative left weapon.", System.Drawing.Color.Red, Network.GamePackets.Message.Talk));
                                return;
                            }
                            foreach (var eq in client.Equipment.Objects)
                            {
                                if (eq != null)
                                {
                                    var itemInfo = Database.ConquerItemInformation.BaseInformations[eq.ID];
                                    if (!((PacketHandler.EquipPassLvlReq(itemInfo, client) || PacketHandler.EquipPassRbReq(itemInfo, client)) && PacketHandler.EquipPassJobReq(itemInfo, client)))
                                    {
                                        client.Send(new Network.GamePackets.Message("You cannot switch equipment because " + ((Game.Enums.ItemPositionName)eq.Position).ToString().Replace("_", "~") + "'" + (string)((eq.Position % 20) == ConquerItem.Boots ? "" : "s") + " stats are not compatible with you (level or profession).", "SYSTEM", System.Drawing.Color.Red, Network.GamePackets.Message.Talk));
                                        return;
                                    }
                                }
                            }
                            client.Entity.AttackPacket = null;
                            client.Entity.RemoveFlag(Update.Flags.Fly);
                            client.AlternateEquipment = usage.ID == ItemUsage.AlternateEquipment;
                            client.LoadItemStats();
                            client.ReshareClan();
                            if (client.Team != null)
                                foreach (var teammate in client.Team.Teammates)
                                    teammate.ReshareClan();
                            client.Equipment.UpdateEntityPacket();
                            WindowsStats Stats = new WindowsStats(client);
                            client.Send(Stats.Encode());
                            client.Send(new ClientEquip(client));
                            break;
                        }
                    case 52:
                        {
                            var item = Database.ConquerItemTable.LoadItem(usage.UID);
                            item.Mode = Nyx.Server.Game.Enums.ItemMode.ChatItem;
                            item.Send(client);
                            break;
                        }
                    case 41://itemtimeaccessories
                        {
                            ConquerItem item = new ConquerItem(true);
                            if (client.Inventory.TryGetItem(usage.UID, out item))
                            {
                                var infos = new Nyx.Server.Database.ConquerItemInformation(item.ID, 0);
                                item.UID = usage.UID;
                                item.TimeStamp = DateTime.Now;
                                item.Minutes = (byte)(infos.BaseInformation.Time == 0 ? 7 : (infos.BaseInformation.Time / 24 / 60));
                                TimeSpan Remain = item.TimeStamp.AddMinutes(item.Minutes) - DateTime.Now;
                                item.TimeLeftInMinutes = (uint)Remain.TotalSeconds;
                                usage.dwParam = 5;
                                client.Send(usage);
                            }
                            break;
                        }

                    case ItemUsage.ArrowReload:
                        {
                            lock (client.ItemSyncRoot)
                               PacketHandler.ReloadArrows(client.Equipment.TryGetItem(ConquerItem.LeftWeapon), client);
                            break;
                        }
                    case ItemUsage.ShowBoothItems:
                        {
                            lock (client.ItemSyncRoot)
                                PacketHandler.ShowBoothItems(usage, client);
                            break;
                        }
                    case ItemUsage.AddItemOnBoothForSilvers:
                    case ItemUsage.AddItemOnBoothForConquerPoints:
                        {
                            lock (client.ItemSyncRoot)
                                PacketHandler.AddItemOnBooth(usage, client);
                            break;
                        }
                    case ItemUsage.BuyFromBooth:
                        {

                            lock (client.ItemSyncRoot)
                                PacketHandler.BuyFromBooth(usage, client);
                            break;
                        }
                    case ItemUsage.RemoveItemFromBooth:
                        {
                            if (client.Booth.ItemList.ContainsKey(usage.UID))
                            {
                                client.Booth.ItemList.Remove(usage.UID);
                                client.SendScreen(usage, true);
                            }
                            break;
                        }
                    case ItemUsage.EquipItem:
                        {
                            lock (client.ItemSyncRoot)
                                PacketHandler.EquipItem(usage, client);
                            WindowsStats WS = new WindowsStats(client);
                            WS.Send(client);
                            break;
                        }
                    case ItemUsage.UnequipItem:
                        {
                            lock (client.ItemSyncRoot)
                                PacketHandler.UnequipItem(usage, client);
                            client.Equipment.UpdateEntityPacket();
                            WindowsStats WS = new WindowsStats(client);
                            WS.Send(client);
                            break;
                        }
                    case 55:
                    case ItemUsage.BuyFromNPC:
                        {
                            lock (client.ItemSyncRoot)
                                PacketHandler.HandleBuyFromNPC(usage, client);
                            break;
                        }
                    case ItemUsage.SellToNPC:
                        {
                            lock (client.ItemSyncRoot)
                                PacketHandler.HandleSellToNPC(usage, client);
                            break;
                        }

                    case ItemUsage.VIPRepair:
                        {
                            PacketHandler.HandleVIPRepair(usage, client);
                            break;
                        }
                    case ItemUsage.Repair:
                        {
                            lock (client.ItemSyncRoot)
                               PacketHandler.HandleRepair(usage, client);
                            break;
                        }
                    case ItemUsage.MeteorUpgrade:
                    case ItemUsage.DragonBallUpgrade:
                        {
                            PacketHandler.UpgradeItem(usage, client);
                            break;
                        }

                    case ItemUsage.Ping:
                        {
                            client.Send(usage);
                            break;
                        }
                    case ItemUsage.ViewWarehouse:
                        {
                            usage.dwParam = client.MoneySave;
                            client.Send(usage);
                            break;
                        }
                    case ItemUsage.WarehouseDeposit:
                        {
                            if (client.Entity.Money >= usage.dwParam)
                            {
                                client.Entity.Money -= usage.dwParam;
                                client.MoneySave += usage.dwParam;
                            }
                            break;
                        }
                    case ItemUsage.WarehouseWithdraw:
                        {
                            if (client.MoneySave >= usage.dwParam)
                            {
                                client.Entity.Money += usage.dwParam;
                                client.MoneySave -= usage.dwParam;
                            }
                            break;
                        }
                    case ItemUsage.DropItem:
                        {
                            PacketHandler.DropItem(usage, client);
                            break;
                        }
                    case ItemUsage.DropMoney:
                        {

                            if (client.Entity.Money >= usage.UID)
                            {
                                ushort X = client.Entity.X, Y = client.Entity.Y;
                                if (client.Map.SelectCoordonates(ref X, ref Y))
                                {
                                    uint ItemID = PacketHandler.MoneyItemID(usage.UID);
                                    FloorItem floorItem = new FloorItem(true);
                                    floorItem.ValueType = FloorItem.FloorValueType.Money;
                                    floorItem.Value = usage.UID;
                                    floorItem.ItemID = ItemID;
                                    floorItem.MapID = client.Map.ID;
                                    floorItem.MapObjType = Game.MapObjectType.Item;
                                    floorItem.X = X;
                                    floorItem.Y = Y;
                                    floorItem.Type = FloorItem.Drop;
                                    floorItem.OnFloor = Time32.Now;
                                    floorItem.UID = FloorItem.FloorUID.Next;
                                    while (client.Map.Npcs.ContainsKey(floorItem.UID))
                                        floorItem.UID = FloorItem.FloorUID.Next;
                                    client.SendScreenSpawn(floorItem, true);
                                    client.Map.AddFloorItem(floorItem);
                                    client.Entity.Money -= usage.UID;

                                }
                            }

                            break;
                        }
                    case ItemUsage.Enchant:
                        {
                            PacketHandler.EnchantItem(usage, client);
                            break;
                        }
                    case ItemUsage.SocketTalismanWithItem:
                        {
                            PacketHandler.SocketTalismanWithItem(usage, client);
                            break;
                        }
                    case ItemUsage.SocketTalismanWithCPs:
                        {
                            PacketHandler.SocketTalismanWithCPs(usage, client);
                            break;
                        }
                    case ItemUsage.RedeemGear:
                        {
                            var item = client.DeatinedItem[usage.UID];
                            if (item != null)
                            {
                                if (DateTime.Now > item.Date.AddDays(7))
                                {
                                    client.Send(new Network.GamePackets.Message("This item is expired!", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft));
                                    return;
                                }
                                if (client.Entity.ConquerPoints >= item.ConquerPointsCost && client.Inventory.Count < 40)
                                {
                                    client.Entity.ConquerPoints -= item.ConquerPointsCost;
                                    Database.EntityTable.UpdateCps(client);
                                    usage.dwParam = client.Entity.UID;
                                    usage.dwExtraInfo3 = item.ConquerPointsCost;
                                    client.Send(usage);
                                    client.Inventory.Add(item.Item, Game.Enums.ItemUse.Add);
                                    Database.ClaimItemTable.Redeem(item, client);
                                    Database.DetainedItemTable.Claim(item, client);
                                    client.DeatinedItem.Remove(item.UID);
                                    if (Kernel.GamePool.ContainsKey(item.GainerUID))
                                    {
                                        GameClient pClient;
                                        if (Kernel.GamePool.TryGetValue(item.GainerUID, out pClient))
                                        {
                                            if (pClient.Entity != null && pClient != null && pClient.ClaimableItem != null)
                                            {
                                                pClient.ClaimableItem[item.UID].OwnerUID = 500;
                                                pClient.ClaimableItem[item.UID].MakeItReadyToClaim();
                                                usage.dwParam = pClient.Entity.UID;
                                                usage.ID = ItemUsage.ClaimGear;
                                                pClient.Send(usage);
                                                pClient.ClaimableItem[item.UID].Send(pClient);
                                            }
                                        }
                                    }
                                    Network.GamePackets.Message message = new Network.GamePackets.Message(" " + client.Entity.Name + " has redeemed his Gear and Pay " + item.ConquerPointsCost + " CPS . Congratulations!", System.Drawing.Color.Wheat, Network.GamePackets.Message.Talk);
                                    foreach (var client2 in Kernel.GamePool.Values)
                                    {
                                        client2.Send(message);
                                    }
                                }
                            }
                            else client.Send(new Network.GamePackets.Message("The item you want to redeem has already been redeemed.", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft));
                            break;
                        }
                    case ItemUsage.ClaimGear:
                        {
                            if (client.Inventory.Count < 40)
                            {
                                var item = client.ClaimableItem[usage.UID];
                                if (item != null)
                                {
                                    if (item.Bound && item.OwnerUID != 500)
                                    {
                                        if (DateTime.Now >= item.Date.AddDays(7))
                                        {
                                            Database.ClaimItemTable.Claim(item, client);
                                            client.ClaimableItem.Remove(item.UID);

                                            usage.dwParam = client.Entity.UID;
                                            usage.dwExtraInfo3 = item.ConquerPointsCost;
                                            client.Send(usage);
                                        }
                                        client.Send(new Network.GamePackets.Message("Unnclaimable item!", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft));
                                        return;
                                    }
                                    if (DateTime.Now < item.Date.AddDays(7) && item.OwnerUID != 500)
                                    {
                                        client.Send(new Network.GamePackets.Message("This item is not expired. You cannot claim it yet!", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft));
                                        return;
                                    }
                                    if (item.OwnerUID == 500)
                                        client.Entity.ConquerPoints += item.ConquerPointsCost;
                                    else
                                    {
                                        client.Inventory.Add(item.Item, Game.Enums.ItemUse.Move);
                                        Network.GamePackets.Message message = new Network.GamePackets.Message("Thank you for arresting " + item.OwnerName + " , " + item.GainerName + ". The arrested one has redeemed his items and you have received a great deal of ConquerPoints as reward. Congratulations!", System.Drawing.Color.Wheat, Network.GamePackets.Message.Talk);
                                        foreach (var client2 in Kernel.GamePool.Values)
                                        {
                                            client2.Send(message);
                                        }
                                    }
                                    Database.ClaimItemTable.Claim(item, client);
                                    client.ClaimableItem.Remove(item.UID);
                                    usage.dwParam = client.Entity.UID;
                                    usage.dwExtraInfo3 = item.ConquerPointsCost;
                                    client.Send(usage);
                                    Network.GamePackets.Message message2 = new Network.GamePackets.Message("Thank you for arresting red/black name players " + client.Entity.Name + " has recived " + item.ConquerPointsCost + " CPS . Congratulations!", System.Drawing.Color.Wheat, Network.GamePackets.Message.Talk);
                                    foreach (var client2 in Kernel.GamePool.Values)
                                    {
                                        client2.Send(message2);
                                    }
                                }
                                else client.Send(new Network.GamePackets.Message("The item you want to claim has already been claimed.", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft));
                            }
                            break;
                        }
                    case ItemUsage.SocketerMan:
                        {
                            PacketHandler.SocketItem(usage, client);
                            client.Send(packet);
                            break;
                        }
                    case 40: { uint ItemAdd = BitConverter.ToUInt32(packet, 8); ConquerItem item_new = null; if (client.Inventory.TryGetItem(ItemAdd, out item_new) || client.Equipment.TryGetItem(ItemAdd) != null) { if (item_new == null) if (client.Equipment.TryGetItem(ItemAdd) != null) item_new = client.Equipment.TryGetItem(ItemAdd); var itemtype = item_new.GetItemType(); if (itemtype == ConquerItem.ItemTypes.GemID) return; ushort pos = Network.PacketHandler.ItemPosition(item_new.ID); if (pos == ConquerItem.Bottle || pos == ConquerItem.Fan || pos == ConquerItem.Garment || pos == ConquerItem.Wing || pos == ConquerItem.LeftWeaponAccessory || pos == ConquerItem.RightWeaponAccessory || pos == ConquerItem.Steed || pos == ConquerItem.SteedArmor || pos == ConquerItem.SteedCrop || pos == ConquerItem.Tower) { client.Send(new Network.GamePackets.Message("Sorry can't bless this item !", System.Drawing.Color.Red, Network.GamePackets.Message.Talk)); return; } if (item_new.Bless >= 7) return; Queue<uint> amount = new Queue<uint>(packet[26]); for (ushort i = 91; i < 91 + 4 * packet[26]; i += 4) { uint uid = BitConverter.ToUInt32(packet, i); if (client.Inventory.ContainsUID(uid)) amount.Enqueue(uid); else return; } byte oldbless = item_new.Bless; if (item_new.Bless == 0 && amount.Count == 5) { item_new.Bless = 1; client.Entity.Update(10, "Aegis1", true); } else if (item_new.Bless == 1 && amount.Count == 1) { item_new.Bless = 3; client.Entity.Update(10, "Aegis2", true); } else if (item_new.Bless == 3 && amount.Count == 3) { item_new.Bless = 5; client.Entity.Update(10, "Aegis3", true); } else if (item_new.Bless == 5 && amount.Count == 5) { item_new.Bless = 7; client.Entity.Update(10, "Aegis4", true); } if (oldbless == item_new.Bless) return; while (amount.Count != 0) client.Inventory.Remove(amount.Dequeue(), Nyx.Server.Game.Enums.ItemUse.Remove, true); item_new.Mode = Nyx.Server.Game.Enums.ItemMode.Update; item_new.Send(client); usage.dwParam = 1; Database.ConquerItemTable.UpdateBless(item_new); } client.Send(packet); break; }
                    case ItemUsage.GemCompose:
                        {
                            #region GemCompose
                            UInt32 Ident = usage.UID;
                            client.Inventory.Remove(Ident, 15);
                            client.Inventory.Add(Ident + 1, 0, 1);
                            usage.dwParam = 1;
                            client.Send(usage);
                            #endregion
                            break;
                        }
                    case ItemUsage.ToristSuper:
                        {
                            #region GemCompose
                            if (client.Entity.Money >= 100000)
                            {
                                client.Inventory.Remove(700002, 1);
                                client.Inventory.Remove(700012, 1);
                                client.Inventory.Remove(700022, 1);
                                client.Inventory.Remove(700032, 1);
                                client.Inventory.Remove(700042, 1);
                                client.Inventory.Remove(700052, 1);
                                client.Inventory.Remove(700062, 1);
                                client.Entity.Money -= 100000;
                                client.Inventory.Add(700072, 0, 1);
                                client.Send(usage);
                                usage.dwParam = 1;
                            }
                            else
                            {
                                client.Send(new Network.GamePackets.Message("Sorry you don`t have 100,000 silver!.", System.Drawing.Color.Red, Network.GamePackets.Message.Talk));
                            }
                            #endregion
                            break;
                        }
                    case ItemUsage.SplitStack:
                        {
                            ConquerItem mainItem = null;
                            ConquerItem minorItem = new ConquerItem(true);
                            Database.ConquerItemInformation infos = null;
                            minorItem.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                            if (client.Inventory.TryGetItem(usage.UID, out mainItem))
                            {
                                infos = new Nyx.Server.Database.ConquerItemInformation(mainItem.ID, 0);
                                if (mainItem.StackSize > 1 && mainItem.StackSize <= infos.BaseInformation.StackSize)
                                {
                                    if (client.Inventory.Count < 40)
                                    {
                                        ushort Amount = (ushort)usage.dwParam;

                                        mainItem.StackSize -= Amount;
                                        mainItem.Mode = Game.Enums.ItemMode.Update;
                                        mainItem.Send(client);
                                        mainItem.Mode = Game.Enums.ItemMode.Default;

                                        minorItem.ID = mainItem.ID;
                                        minorItem.StackSize += Amount;
                                        minorItem.Durability = mainItem.Durability;
                                        client.Inventory.Add(minorItem, Game.Enums.ItemUse.CreateAndAdd);
                                        Database.ConquerItemTable.UpdateStack(mainItem);
                                    }
                                }
                            }
                            break;
                        }
                    case ItemUsage.MergeStackableItems:
                        {
                            ConquerItem mainItem = null;
                            ConquerItem minorItem = new ConquerItem(true);
                            Database.ConquerItemInformation infos = null;
                            minorItem.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
                            if (client.Inventory.TryGetItem(usage.UID, out mainItem) && client.Inventory.TryGetItem(usage.dwParam, out minorItem))
                            {
                                if (mainItem.ID == minorItem.ID)
                                {
                                    infos = new Nyx.Server.Database.ConquerItemInformation(mainItem.ID, 0);
                                    if (mainItem.StackSize < 1)
                                        mainItem.StackSize = 1;
                                    if (minorItem.StackSize < 1)
                                        minorItem.StackSize = 1;

                                    if ((mainItem.StackSize + minorItem.StackSize) <= infos.BaseInformation.StackSize)
                                    {
                                        mainItem.StackSize += minorItem.StackSize;
                                        mainItem.Mode = Game.Enums.ItemMode.Update;
                                        mainItem.Send(client);
                                        mainItem.Mode = Game.Enums.ItemMode.Default;
                                        Database.ConquerItemTable.UpdateStack(mainItem);

                                        client.Inventory.Remove(minorItem, Game.Enums.ItemUse.Remove);
                                    }
                                }
                            }
                            break;
                        }
                    case 34:
                        {
                            break;
                        }
                    case ItemUsage.LowerEquipment:
                        {
                            ConquerItem item = null;
                            if (client.Inventory.TryGetItem(usage.UID, out item))
                            {
                                if (PacketHandler.IsArrow(item.ID))
                                    return;
                                ConquerItem upgrade = null;
                                if (client.Inventory.TryGetItem(usage.UID, out upgrade))
                                {
                                    if (client.Entity.ConquerPoints < 54)
                                    {
                                        client.Send(new Network.GamePackets.Message("You don't have 54CPs.", Color.Red, Network.GamePackets.Message.TopLeft));
                                        return;
                                    }
                                    client.Entity.ConquerPoints -= 54;
                                    Database.ConquerItemInformation infos = new Nyx.Server.Database.ConquerItemInformation(item.ID, item.Plus);
                                    if (infos.BaseInformation.Level <= 15) return;
                                    int startwith = (int)(infos.BaseInformation.ID / 1000);
                                    int endwith = (int)(infos.BaseInformation.ID % 10);
                                    var sitem = infos.LowestID(PacketHandler.ItemMinLevel(PacketHandler.ItemPosition(infos.BaseInformation.ID)));
                                    //  var sitem = Nyx.Server.Database.ConquerItemInformation.BaseInformations.Values.Where(x => (x.ID % 10) == endwith && (x.ID / 1000) == startwith && x.Level >= 15).OrderBy(y => y.Level).First();
                                    item.ID = sitem;
                                    infos = new Nyx.Server.Database.ConquerItemInformation(item.ID, item.Plus);
                                    item.Durability = item.MaximDurability = infos.BaseInformation.Durability;
                                    Database.ConquerItemTable.UpdateItemID(item);
                                    Database.ConquerItemTable.UpdateDurabilityItem(item);
                                    item.Mode = Nyx.Server.Game.Enums.ItemMode.Update;
                                    item.Send(client);
                                    item = PacketHandler.ItemSocket(item, 2);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

        }
    }
}
