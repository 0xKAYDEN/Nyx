using Nyx.Server.Client;
using Nyx.Server.Game;
using Nyx.Server.Network.GamePackets;
using static Nyx.Server.Network.GamePackets.Game_SubClass;
using static Nyx.Server.Network.PacketHandler;

namespace Nyx.Server.Packets
{
    public class MsgNpc
    {
        [Network.Packet(2031)]
        [Network.Packet(2032)]
        public static async Task Process(GameClient client, byte[] packet)
        {

            if (packet == null)
                return;
            if (client == null)
                return;
            ushort Length = BitConverter.ToUInt16(packet, 0);
            ushort ID = BitConverter.ToUInt16(packet, 2);
            ushort TypeP4 = BitConverter.ToUInt16(packet, 4);
            ushort Offest6 = BitConverter.ToUInt16(packet, 6);
            if (client.Filtering)
                if (client.PacketFilter.Filter(ID))
                    return;
            if (ClientSeal != BitConverter.ToUInt64(packet, Length))
            {
                client.Disconnect();
                return;
            }

            if (client.Action != 2)
                return;
            NpcRequest req = new NpcRequest();
            req.Deserialize(packet);
            if (req.InteractType == 102)
            {
                var quest = req.NpcID - QuestInfo.ActionBase;
                client.Quests.QuitQuest((QuestID)quest);
            }
            if (req.InteractType == NpcReply.MessageBox)
            {

                if (req.OptionID == 255 && client.MessageOK != null)
                    client.MessageOK.Invoke(client);
                else
                    if (client.MessageCancel != null)
                    client.MessageCancel.Invoke(client);

                client.MessageOK = null;
                client.MessageCancel = null;
            }
            else
            {
                if (ID == 2031)
                    client.ActiveNpc = req.NpcID;
                if (req.NpcID == 12)
                {
                    if (client.Entity.VIPLevel > 0)
                    {
                        Data data = new Data(true);
                        data.ID = Data.OpenWindow;
                        data.UID = client.Entity.UID;
                        data.TimeStamp = Time32.Now;
                        data.dwParam = Data.WindowCommands.VIPWarehouse;
                        data.wParam1 = client.Entity.X;
                        data.wParam2 = client.Entity.Y;
                        client.Send(data);
                    }
                }
                Interfaces.INpc npc = null;
                if (req.InteractType == 102)
                {
                    if (client.Guild != null)
                    {
                        if (client.AsMember.Rank == Nyx.Server.Game.Enums.GuildMemberRank.GuildLeader)
                        {
                            client.Guild.ExpelMember(req.Input, false);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 720842)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID == 1)
                        {
                            if (client.Entity.SwordSoul)
                            {
                                client.Send(new Network.GamePackets.Message("The Sword Soul can only be used once a day!", Color.White, Network.GamePackets.Message.System));
                                return;
                            }
                            client.Entity.SwordSoul = true;
                            Database.MonsterInformation mob;
                            Database.MonsterInformation.MonsterInformations.TryGetValue(4170, out mob);
                            client.Map.Spawnthis(mob, client, client.Entity.MapID, (ushort)(client.Entity.X - 2), (ushort)(client.Entity.Y - 2));
                            client.SendScreenSpawn(client.Entity, true);
                            client.Screen.FullWipe();
                            client.Screen.Reload(null);
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 3004259)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID == 1)
                        {
                            if (client.Entity.BansheeSpirit)
                            {
                                client.Send(new Network.GamePackets.Message("The Banshee Spirit can only be used once a day!", Color.White, Network.GamePackets.Message.System));
                                return;
                            }
                            client.Entity.BansheeSpirit = true;
                            Database.MonsterInformation mob;
                            Database.MonsterInformation.MonsterInformations.TryGetValue(7565, out mob);
                            client.Map.Spawnthis(mob, client, client.Entity.MapID, (ushort)(client.Entity.X - 2), (ushort)(client.Entity.Y - 2));
                            client.SendScreenSpawn(client.Entity, true);
                            client.Screen.FullWipe();
                            client.Screen.Reload(null);
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 3004464)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID == 1)
                        {
                            if (client.Inventory.Count <= 35)
                            {
                                if (client.Equipment.TryGetItem((byte)Positions.Left) == null)
                                {
                                    client.MessageBox("Please Equip Wep In Left Hand!");
                                    return;
                                }
                                ConquerItem Item = null;
                                Item = client.Equipment.TryGetItem((byte)Positions.Left);
                                if (Item.ID / 1000 == 601)
                                {
                                    ConquerItem Item2 = new ConquerItem(true);
                                    Item2.UID = ConquerItem.ItemUID.Next;
                                    Item2.ID = 616020 + (Item.ID % 10);
                                    Item2.Inscribed = Item.Inscribed;
                                    Item2.Lock = Item.Lock;
                                    Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                    Item2.Durability = Item2.MaximDurability;
                                    Item2.Bless = Item.Bless;
                                    Item2.Bound = Item.Bound;
                                    Item2.Enchant = Item.Enchant;
                                    Item2.Plus = Item.Plus;
                                    Item2.PlusProgress = Item.PlusProgress;
                                    Item2.Position = Item.Position;
                                    Item2.Purification = Item.Purification;
                                    Item2.RefineItem = Item.RefineItem;
                                    Item2.RefineryStarted = Item.RefineryStarted;
                                    Item2.RefineryTime = Item.RefineryTime;
                                    Item2.SocketOne = Item.SocketOne;
                                    Item2.SocketProgress = Item.SocketProgress;
                                    Item2.SocketTwo = Item.SocketTwo;
                                    Item2.Suspicious = Item.Suspicious;
                                    Item2.StatsLoaded = Item.StatsLoaded;
                                    Item2.SuspiciousStart = Item.SuspiciousStart;
                                    Item2.UnlockEnd = Item.UnlockEnd;
                                    Item2.Unlocking = Item.Unlocking;
                                    client.Equipment.Remove((byte)Item.Position);
                                    client.Equipment.Add(Item2);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    client.CalculateStatBonus();
                                    client.CalculateHPBonus();
                                    client.LoadItemStats();
                                    ClientEquip equips = new ClientEquip();
                                    equips.DoEquips(client);
                                    client.Send(equips);
                                    client.ReshareClan();
                                    if (client.Team != null)
                                        foreach (var teammate in client.Team.Teammates)
                                            teammate.ReshareClan();
                                }
                                else
                                {
                                    client.MessageBox("Please Equip Katana In Left Hand!");
                                    return;
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                                return;
                            }
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                        if (req.OptionID == 2)
                        {
                            if (client.Inventory.Count <= 35)
                            {
                                if (client.Equipment.TryGetItem((byte)Positions.Right) == null)
                                {
                                    client.MessageBox("Please Equip Wep In Right Hand!");
                                    return;
                                }
                                ConquerItem Item = null;
                                Item = client.Equipment.TryGetItem((byte)Positions.Right);
                                if (Item.ID / 1000 == 601)
                                {
                                    ConquerItem Item2 = new ConquerItem(true);
                                    Item2.UID = ConquerItem.ItemUID.Next;
                                    Item2.ID = 616020 + (Item.ID % 10);
                                    Item2.Inscribed = Item.Inscribed;
                                    Item2.Lock = Item.Lock;
                                    Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                    Item2.Durability = Item2.MaximDurability;
                                    Item2.Bless = Item.Bless;
                                    Item2.Bound = Item.Bound;
                                    Item2.Enchant = Item.Enchant;
                                    Item2.Plus = Item.Plus;
                                    Item2.PlusProgress = Item.PlusProgress;
                                    Item2.Position = Item.Position;
                                    Item2.Purification = Item.Purification;
                                    Item2.RefineItem = Item.RefineItem;
                                    Item2.RefineryStarted = Item.RefineryStarted;
                                    Item2.RefineryTime = Item.RefineryTime;
                                    Item2.SocketOne = Item.SocketOne;
                                    Item2.SocketProgress = Item.SocketProgress;
                                    Item2.SocketTwo = Item.SocketTwo;
                                    Item2.Suspicious = Item.Suspicious;
                                    Item2.StatsLoaded = Item.StatsLoaded;
                                    Item2.SuspiciousStart = Item.SuspiciousStart;
                                    Item2.UnlockEnd = Item.UnlockEnd;
                                    Item2.Unlocking = Item.Unlocking;
                                    client.Equipment.Remove((byte)Item.Position);
                                    client.Equipment.Add(Item2);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    client.CalculateStatBonus();
                                    client.CalculateHPBonus();
                                    client.LoadItemStats();
                                    ClientEquip equips = new ClientEquip();
                                    equips.DoEquips(client);
                                    client.Send(equips);
                                    client.ReshareClan();
                                    if (client.Team != null)
                                        foreach (var teammate in client.Team.Teammates)
                                            teammate.ReshareClan();
                                }
                                else
                                {
                                    client.MessageBox("Please Equip Katana In Right Hand!");
                                    return;
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                                return;
                            }
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                }
                if (client.ActiveNpc == 3006016)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (client.Inventory.Count <= 35)
                        {
                            if (client.Equipment.TryGetItem((byte)Positions.Right) == null)
                            {
                                client.MessageBox("Please Equip Wep In Right Hand!");
                                return;
                            }
                            ConquerItem Item = null;
                            Item = client.Equipment.TryGetItem((byte)Positions.Right);
                            if (Item.ID / 1000 == 421)
                            {
                                ConquerItem Item2 = new ConquerItem(true);
                                Item2.UID = ConquerItem.ItemUID.Next;
                                if (req.OptionID == 1)
                                {
                                    Item2.ID = 620020 + (Item.ID % 10);
                                }
                                if (req.OptionID == 2)
                                {
                                    Item2.ID = 619020 + (Item.ID % 10);
                                }
                                Item2.Inscribed = Item.Inscribed;
                                Item2.Lock = Item.Lock;
                                Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                Item2.Durability = Item2.MaximDurability;
                                Item2.Bless = Item.Bless;
                                Item2.Bound = Item.Bound;
                                Item2.Enchant = Item.Enchant;
                                Item2.Plus = Item.Plus;
                                Item2.PlusProgress = Item.PlusProgress;
                                Item2.Position = Item.Position;
                                Item2.Purification = Item.Purification;
                                Item2.RefineItem = Item.RefineItem;
                                Item2.RefineryStarted = Item.RefineryStarted;
                                Item2.RefineryTime = Item.RefineryTime;
                                Item2.SocketOne = Item.SocketOne;
                                Item2.SocketProgress = Item.SocketProgress;
                                Item2.SocketTwo = Item.SocketTwo;
                                Item2.Suspicious = Item.Suspicious;
                                Item2.StatsLoaded = Item.StatsLoaded;
                                Item2.SuspiciousStart = Item.SuspiciousStart;
                                Item2.UnlockEnd = Item.UnlockEnd;
                                Item2.Unlocking = Item.Unlocking;
                                client.Equipment.Remove((byte)Item.Position);
                                client.Equipment.Add(Item2);
                                Item2.Mode = Enums.ItemMode.Update;
                                Item2.Send(client);
                                client.CalculateStatBonus();
                                client.CalculateHPBonus();
                                client.LoadItemStats();
                                ClientEquip equips = new ClientEquip();
                                equips.DoEquips(client);
                                client.Send(equips);
                                client.ReshareClan();
                                if (client.Team != null)
                                    foreach (var teammate in client.Team.Teammates)
                                        teammate.ReshareClan();
                            }
                            else
                            {
                                client.MessageBox("Please Equip Backsword In Right Hand!");
                                return;
                            }
                        }
                        else
                        {
                            client.Send(Constants.FullInventory);
                            return;
                        }
                        client.Inventory.Remove(client.ActiveNpc, 1);
                    }
                }
                if (client.ActiveNpc == 3007564)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID == 1)
                        {
                            if (client.Inventory.Count <= 35)
                            {
                                if (client.Equipment.TryGetItem((byte)Positions.Left) == null)
                                {
                                    client.MessageBox("Please Equip Wep In Left Hand!");
                                    return;
                                }
                                ConquerItem Item = null;
                                Item = client.Equipment.TryGetItem((byte)Positions.Left);
                                if (Item.ID / 1000 == 610)
                                {
                                    ConquerItem Item2 = new ConquerItem(true);
                                    Item2.UID = ConquerItem.ItemUID.Next;
                                    Item2.ID = 622020 + (Item.ID % 10);
                                    Item2.Inscribed = Item.Inscribed;
                                    Item2.Lock = Item.Lock;
                                    Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                    Item2.Durability = Item2.MaximDurability;
                                    Item2.Bless = Item.Bless;
                                    Item2.Bound = Item.Bound;
                                    Item2.Enchant = Item.Enchant;
                                    Item2.Plus = Item.Plus;
                                    Item2.PlusProgress = Item.PlusProgress;
                                    Item2.Position = Item.Position;
                                    Item2.Purification = Item.Purification;
                                    Item2.RefineItem = Item.RefineItem;
                                    Item2.RefineryStarted = Item.RefineryStarted;
                                    Item2.RefineryTime = Item.RefineryTime;
                                    Item2.SocketOne = Item.SocketOne;
                                    Item2.SocketProgress = Item.SocketProgress;
                                    Item2.SocketTwo = Item.SocketTwo;
                                    Item2.Suspicious = Item.Suspicious;
                                    Item2.StatsLoaded = Item.StatsLoaded;
                                    Item2.SuspiciousStart = Item.SuspiciousStart;
                                    Item2.UnlockEnd = Item.UnlockEnd;
                                    Item2.Unlocking = Item.Unlocking;
                                    client.Equipment.Remove((byte)Item.Position);
                                    client.Equipment.Add(Item2);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    client.CalculateStatBonus();
                                    client.CalculateHPBonus();
                                    client.LoadItemStats();
                                    ClientEquip equips = new ClientEquip();
                                    equips.DoEquips(client);
                                    client.Send(equips);
                                    client.ReshareClan();
                                    if (client.Team != null)
                                        foreach (var teammate in client.Team.Teammates)
                                            teammate.ReshareClan();
                                }
                                else
                                {
                                    client.MessageBox("Please Equip Bead In Left Hand!");
                                    return;
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                                return;
                            }
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                        if (req.OptionID == 2)
                        {
                            if (client.Inventory.Count <= 35)
                            {
                                if (client.Equipment.TryGetItem((byte)Positions.Right) == null)
                                {
                                    client.MessageBox("Please Equip Wep In Right Hand!");
                                    return;
                                }
                                ConquerItem Item = null;
                                Item = client.Equipment.TryGetItem((byte)Positions.Right);
                                if (Item.ID / 1000 == 610)
                                {
                                    ConquerItem Item2 = new ConquerItem(true);
                                    Item2.UID = ConquerItem.ItemUID.Next;
                                    Item2.ID = 622020 + (Item.ID % 10);
                                    Item2.Inscribed = Item.Inscribed;
                                    Item2.Lock = Item.Lock;
                                    Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                    Item2.Durability = Item2.MaximDurability;
                                    Item2.Bless = Item.Bless;
                                    Item2.Bound = Item.Bound;
                                    Item2.Enchant = Item.Enchant;
                                    Item2.Plus = Item.Plus;
                                    Item2.PlusProgress = Item.PlusProgress;
                                    Item2.Position = Item.Position;
                                    Item2.Purification = Item.Purification;
                                    Item2.RefineItem = Item.RefineItem;
                                    Item2.RefineryStarted = Item.RefineryStarted;
                                    Item2.RefineryTime = Item.RefineryTime;
                                    Item2.SocketOne = Item.SocketOne;
                                    Item2.SocketProgress = Item.SocketProgress;
                                    Item2.SocketTwo = Item.SocketTwo;
                                    Item2.Suspicious = Item.Suspicious;
                                    Item2.StatsLoaded = Item.StatsLoaded;
                                    Item2.SuspiciousStart = Item.SuspiciousStart;
                                    Item2.UnlockEnd = Item.UnlockEnd;
                                    Item2.Unlocking = Item.Unlocking;
                                    client.Equipment.Remove((byte)Item.Position);
                                    client.Equipment.Add(Item2);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    client.CalculateStatBonus();
                                    client.CalculateHPBonus();
                                    client.LoadItemStats();
                                    ClientEquip equips = new ClientEquip();
                                    equips.DoEquips(client);
                                    client.Send(equips);
                                    client.ReshareClan();
                                    if (client.Team != null)
                                        foreach (var teammate in client.Team.Teammates)
                                            teammate.ReshareClan();
                                }
                                else
                                {
                                    client.MessageBox("Please Equip Bead In Right Hand!");
                                    return;
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                                return;
                            }
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                }
                if (client.ActiveNpc == 3003340)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID == 1)
                        {
                            if (client.Inventory.Count <= 35)
                            {
                                if (client.Equipment.TryGetItem((byte)Positions.Left) == null)
                                {
                                    client.MessageBox("Please Equip Wep In Left Hand!");
                                    return;
                                }
                                ConquerItem Item = null;
                                Item = client.Equipment.TryGetItem((byte)Positions.Left);
                                if (Item.ID / 1000 == 410 || Item.ID / 1000 == 420 || Item.ID / 1000 == 460 || Item.ID / 1000 == 480 || Item.ID / 1000 == 440 || Item.ID / 1000 == 430 || Item.ID / 1000 == 450)
                                {
                                    ConquerItem Item2 = new ConquerItem(true);
                                    Item2.UID = ConquerItem.ItemUID.Next;
                                    Item2.ID = 614020 + (Item.ID % 10);
                                    Item2.Inscribed = Item.Inscribed;
                                    Item2.Lock = Item.Lock;
                                    Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                    Item2.Durability = Item2.MaximDurability;
                                    Item2.Bless = Item.Bless;
                                    Item2.Bound = Item.Bound;
                                    Item2.Enchant = Item.Enchant;
                                    Item2.Plus = Item.Plus;
                                    Item2.PlusProgress = Item.PlusProgress;
                                    Item2.Position = Item.Position;
                                    Item2.Purification = Item.Purification;
                                    Item2.RefineItem = Item.RefineItem;
                                    Item2.RefineryStarted = Item.RefineryStarted;
                                    Item2.RefineryTime = Item.RefineryTime;
                                    Item2.SocketOne = Item.SocketOne;
                                    Item2.SocketProgress = Item.SocketProgress;
                                    Item2.SocketTwo = Item.SocketTwo;
                                    Item2.Suspicious = Item.Suspicious;
                                    Item2.StatsLoaded = Item.StatsLoaded;
                                    Item2.SuspiciousStart = Item.SuspiciousStart;
                                    Item2.UnlockEnd = Item.UnlockEnd;
                                    Item2.Unlocking = Item.Unlocking;
                                    client.Equipment.Remove((byte)Item.Position);
                                    client.Equipment.Add(Item2);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    client.CalculateStatBonus();
                                    client.CalculateHPBonus();
                                    client.LoadItemStats();
                                    ClientEquip equips = new ClientEquip();
                                    equips.DoEquips(client);
                                    client.Send(equips);
                                    client.ReshareClan();
                                    if (client.Team != null)
                                        foreach (var teammate in client.Team.Teammates)
                                            teammate.ReshareClan();
                                }
                                else
                                {
                                    client.MessageBox("Please Equip Blade, Sword, Whip, Hook, Axe, Hammer, Club, Dagger or Scepter In Left Hand!");
                                    return;
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                                return;
                            }
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                        if (req.OptionID == 2)
                        {
                            if (client.Inventory.Count <= 35)
                            {
                                if (client.Equipment.TryGetItem((byte)Positions.Right) == null)
                                {
                                    client.MessageBox("Please Equip Wep In Right Hand!");
                                    return;
                                }
                                ConquerItem Item = null;
                                Item = client.Equipment.TryGetItem((byte)Positions.Right);
                                if (Item.ID / 1000 == 410 || Item.ID / 1000 == 420 || Item.ID / 1000 == 460 || Item.ID / 1000 == 480 || Item.ID / 1000 == 440 || Item.ID / 1000 == 430 || Item.ID / 1000 == 450)
                                {
                                    ConquerItem Item2 = new ConquerItem(true);
                                    Item2.UID = ConquerItem.ItemUID.Next;
                                    Item2.ID = 614020 + (Item.ID % 10);
                                    Item2.Inscribed = Item.Inscribed;
                                    Item2.Lock = Item.Lock;
                                    Item2.MaximDurability = Database.ConquerItemInformation.BaseInformations[Item2.ID].Durability;
                                    Item2.Durability = Item2.MaximDurability;
                                    Item2.Bless = Item.Bless;
                                    Item2.Bound = Item.Bound;
                                    Item2.Enchant = Item.Enchant;
                                    Item2.Plus = Item.Plus;
                                    Item2.PlusProgress = Item.PlusProgress;
                                    Item2.Position = Item.Position;
                                    Item2.Purification = Item.Purification;
                                    Item2.RefineItem = Item.RefineItem;
                                    Item2.RefineryStarted = Item.RefineryStarted;
                                    Item2.RefineryTime = Item.RefineryTime;
                                    Item2.SocketOne = Item.SocketOne;
                                    Item2.SocketProgress = Item.SocketProgress;
                                    Item2.SocketTwo = Item.SocketTwo;
                                    Item2.Suspicious = Item.Suspicious;
                                    Item2.StatsLoaded = Item.StatsLoaded;
                                    Item2.SuspiciousStart = Item.SuspiciousStart;
                                    Item2.UnlockEnd = Item.UnlockEnd;
                                    Item2.Unlocking = Item.Unlocking;
                                    if (!client.Equipment.Free((byte)Positions.Left))
                                    {
                                        client.Equipment.Remove((byte)Positions.Left);
                                    }
                                    client.Equipment.Remove((byte)Item.Position);
                                    client.Equipment.Add(Item2);
                                    Item2.Mode = Enums.ItemMode.Update;
                                    Item2.Send(client);
                                    client.CalculateStatBonus();
                                    client.CalculateHPBonus();
                                    client.LoadItemStats();
                                    ClientEquip equips = new ClientEquip();
                                    equips.DoEquips(client);
                                    client.Send(equips);
                                    client.ReshareClan();
                                    if (client.Team != null)
                                        foreach (var teammate in client.Team.Teammates)
                                            teammate.ReshareClan();
                                }
                                else
                                {
                                    client.MessageBox("Please Equip Blade, Sword, Whip, Hook, Axe, Hammer, Club, Dagger or Scepter In Right Hand!");
                                    return;
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                                return;
                            }
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                }
                #region Activieness
                #region [Champion]ActivePack
                if (client.ActiveNpc == 3005062)
                {
                    if (client.Inventory.Contains(3005062, 1))
                    {
                        if (req.OptionID == 1 || req.OptionID == 11 || req.OptionID == 2)
                        {
                            if (client.Inventory.Count <= 39)
                            {
                                if (req.OptionID == 2)
                                {
                                    Npcs dialog1 = new Npcs(client);
                                    dialog1.Text("Are you sure want to open the pack without using The Magic Lucky Ticket?");
                                    dialog1.Option("Yes.", 11);
                                    dialog1.Option("No.", 255);
                                    dialog1.Send();
                                }
                                if (req.OptionID == 11)
                                {
                                    System.Random r = new System.Random();
                                    int i = r.Next(100);
                                    if (i > 0 && i <= 10)
                                    {
                                        client.Entity.Money += 500000;//500,000 Silver
                                        client.MessageBox("You received 500,000 Silver!", null);
                                    }
                                    if (i > 10 && i <= 20)
                                    {
                                        client.ChiPoints += 50;//50 ChiPoint
                                        client.MessageBox("You received 50 Chi Points!", null);
                                    }
                                    if (i > 20 && i <= 30)
                                    {
                                        client.RacePoints += 200;//200 RacePoints
                                        client.MessageBox("You received 200 Horse Racing Points!", null);
                                    }
                                    if (i > 30 && i <= 40)
                                    {
                                        client.Entity.BoundCps += 20;//20 BoundCps
                                        client.MessageBox("You received 20 BoundCPs (B)!", null);
                                    }
                                    if (i > 40 && i <= 50)
                                    {
                                        client.Inventory.Add(1088001, 0, 5);//5 Meteor
                                        client.MessageBox("You received a 5 Meteor !", null);
                                    }
                                    if (i > 50 && i <= 60)
                                    {
                                        client.Entity.SubClasses.StudyPoints += 200;//200 Point SubClass
                                        client.MessageBox("You received 200 Study Points!", null);
                                    }
                                    if (i > 60 && i <= 70)
                                    {
                                        client.AddBless(2 * 24 * 60 * 60);
                                        client.MessageBox("You received 2 Days Heaven Blessing !", null);
                                    }
                                    if (i > 70 && i <= 80)
                                    {
                                        client.Inventory.AddBound(3003124, 0, 1); //FavoredTrainingPill(B)
                                        client.MessageBox("You received a Favored Training Pill (B)!", null);
                                    }
                                    if (i > 90 && i <= 100)
                                    {
                                        client.Inventory.Add(1088000, 0, 1);//DragonBall
                                        client.MessageBox("You received a Dragon Ball!", null);
                                    }
                                    client.Inventory.Remove(3005062, 1);
                                }
                                if (req.OptionID == 1)
                                {
                                    if (client.Inventory.Contains(3005063, 1) && client.Inventory.Contains(3005062, 1))
                                    {
                                        System.Random r = new System.Random();
                                        int i = r.Next(100);
                                        var space = client.Inventory.Objects.Where(item => item == null).Count();
                                        var a = new uint[] { 2, 4, 8, 16, 32, 64, 128 }[new Random().Next(1, 7)];
                                        var result = (byte)Math.Min(space, a);
                                        if (i > 0 && i <= 10)
                                        {
                                            client.Entity.Money += 1000000 * a;//1,0000,000 Silve
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 1000000 * a + " Silver!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 10 && i <= 20)
                                        {
                                            client.ChiPoints += (uint)(50 * a);//50 ChiPoint
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 50 * a + " ChiPoints!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 20 && i <= 30)
                                        {
                                            client.RacePoints += (uint)(200 * a);//200 RacePoints
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 200 * a + " RacePoints!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 30 && i <= 40)
                                        {
                                            client.Entity.BoundCps += (uint)(20 * a);
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 20 * a + " ConquerPoints (B)!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 40 && i <= 50)
                                        {
                                            client.Inventory.Add(720027, 0, (ushort)a);
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 1 * a + " MeteorScrolls!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 50 && i <= 60)
                                        {
                                            client.Entity.SubClasses.StudyPoints += (byte)(100 * a);//100 Point SubClass
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 100 * a + " Study Points!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 60 && i <= 70)
                                        {
                                            client.Entity.HeavenBlessing += (uint)(1 * a * 24 * 60 * 60);//2 Heaven Blessing
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + a + " Heaven Blessing Days!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 70 && i <= 80)
                                        {
                                            client.Inventory.AddBound(3003124, 0, (byte)a); //FavoredTrainingPill(B)
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + a + " FavoredTrainingPill(B)!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        if (i > 90 && i <= 100)
                                        {
                                            client.Inventory.AddBound(3003124, 0, 1); //FavoredTrainingPill(B)
                                            client.Send(new Network.GamePackets.Message("" + client.Entity.Name + " is so lucky to multiply the reward in the active pack by " + a + " times, and received " + 2 * a + " FavoredTrainingPill(B)!", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                        }
                                        client.Inventory.Remove(3005062, 1);
                                        client.Inventory.Remove(3005063, 1);//MagicLuckyTicket
                                    }
                                    else
                                    {
                                        client.MessageBox("You do not have The Magic Lucky Ticket.", null);
                                    }
                                }
                            }
                            else
                            {
                                client.Send(Constants.FullInventory);
                            }
                        }
                    }
                    return;
                }
                #endregion
                #region DivineActivePack
                if (client.ActiveNpc == 3005065)
                {
                    if (client.Inventory.Contains(3005065, 1))
                    {
                        if (req.OptionID == 1 || req.OptionID == 2 || req.OptionID == 5)
                        {
                            if (req.OptionID == 2)
                            {
                                Npcs dialog1 = new Npcs(client);
                                dialog1.Text("Are you sure you want to use the Magic Lucky Ticket? if lucky enough, you i`ll");
                                dialog1.Text("multiply the reward in the active pack by 2 - 128 times.");
                                dialog1.Option("Yes.", 5);
                                dialog1.Option("No.", 255);
                                dialog1.Send();
                            }
                            if (req.OptionID == 5)
                            {
                                if (client.Inventory.Contains(3005065, 1))
                                {
                                    {
                                        client.Inventory.Add(3005063, 0, 1);
                                        client.Inventory.Remove(3005065, 1);
                                        client.MessageBox("You received 1 Magic Lucky Ticket.", null);
                                    }
                                }

                            }
                            if (req.OptionID == 1)
                            {
                                if (client.Inventory.Contains(3005065, 1))
                                {
                                    client.Inventory.Add(3005062, 0, 1);
                                    client.Inventory.Remove(3005065, 1);
                                    client.MessageBox("You received a [Champion] Active Pack.", null);
                                }
                            }
                        }
                    }
                    return;
                }
                #endregion
                #endregion
                #region Lab
                if (client.ActiveNpc == 721533)
                {

                    if (req.OptionID == 1)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 17))
                        {
                            if (client.Quests.HasQuest(QuestID.EvilLabyrinth))
                            {
                                client.Quests.IncreaseQuestDones((QuestID)6467, 1, 0, 0, 0);
                            }
                            client.Inventory.Remove(721533, 17);
                            client.Inventory.Add(3007530, 0, 1);
                        }
                    }
                    if (req.OptionID == 2)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 3))
                        {
                            client.Inventory.Remove(721533, 3);
                            client.Inventory.Add(721534, 0, 1);
                        }

                    }
                }
                if (client.ActiveNpc == 721535)
                {

                    if (req.OptionID == 1)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 17))
                        {
                            if (client.Quests.HasQuest(QuestID.EvilLabyrinth))
                            {
                                client.Quests.IncreaseQuestDones((QuestID)6467, 1, 1, 1, 0);
                            }
                            client.Inventory.Remove(721535, 17);
                            client.Inventory.Add(3007532, 0, 1);
                        }
                    }
                    if (req.OptionID == 2)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 3))
                        {
                            client.Inventory.Remove(721535, 3);
                            client.Inventory.Add(721536, 0, 1);
                        }

                    }
                }
                if (client.ActiveNpc == 721536)
                {

                    if (req.OptionID == 1)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 17))
                        {
                            if (client.Quests.HasQuest(QuestID.EvilLabyrinth))
                            {
                                client.Quests.IncreaseQuestDones((QuestID)6467, 1, 1, 1, 1);
                            }
                            client.Inventory.Remove(721536, 17);
                            client.Inventory.Add(3007533, 0, 1);
                        }
                    }
                    if (req.OptionID == 2)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 3))
                        {
                            client.Inventory.Remove(721536, 3);
                            client.Inventory.Add(722880, 0, 1);//LabyrinthPermit
                        }

                    }
                }
                if (client.ActiveNpc == 721534)
                {

                    if (req.OptionID == 1)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 17))
                        {
                            if (client.Quests.HasQuest(QuestID.EvilLabyrinth))
                            {
                                client.Quests.IncreaseQuestDones((QuestID)6467, 1, 1, 0, 0);
                            }
                            client.Inventory.Remove(721534, 17);
                            client.Inventory.Add(3007531, 0, 1);
                        }
                    }
                    if (req.OptionID == 2)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 3))
                        {
                            client.Inventory.Remove(721534, 3);
                            client.Inventory.Add(721535, 0, 1);
                        }

                    }
                }
                if (client.ActiveNpc == 3007528)
                {
                    if (client.Inventory.Contains(3007528, 1))
                    {
                        if (req.OptionID == 1)
                        {
                            client.Inventory.Remove(3007528, 1);
                            if (client.Entity.MapID == 1351)
                            {
                                client.Entity.Teleport(1351, 482, 373);
                            }
                            if (client.Entity.MapID == 1352)
                            {
                                client.Entity.Teleport(1352, 669, 471);
                            }
                            if (client.Entity.MapID == 1353)
                            {
                                client.Entity.Teleport(1353, 787, 548);
                            }
                            if (client.Entity.MapID == 1354)
                            {
                                client.Entity.Teleport(1354, 900, 680);
                            }
                            client.MessageBox("The Guiding Bee has taken you to see the teleporting general of this floor.");
                        }
                    }
                    if (req.OptionID == 2)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 1))
                        {
                            if (client.Entity.MapID == 1352)
                            {
                                if (client.Inventory.Contains(3007507))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1352, 161, 169);
                                }
                                if (client.Inventory.Contains(3007508))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1352, 314, 330);
                                }
                                if (client.Inventory.Contains(3007509))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1352, 507, 506);
                                }
                                if (client.Inventory.Contains(3007510))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1352, 486, 290);
                                }
                                if (client.Inventory.Contains(3007511))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1352, 429, 588);
                                }
                            }
                            if (client.Entity.MapID == 1353)
                            {
                                if (client.Inventory.Contains(3007512))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 166, 200);
                                }
                                if (client.Inventory.Contains(3007513))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 265, 103);
                                }
                                if (client.Inventory.Contains(3007514))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 321, 238);
                                }
                                if (client.Inventory.Contains(3007515))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 199, 423);
                                }
                                if (client.Inventory.Contains(3007516))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 660, 442);
                                }
                                if (client.Inventory.Contains(3007517))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 539, 645);
                                }
                                if (client.Inventory.Contains(3007518))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1353, 478, 708);
                                }
                            }
                            if (client.Entity.MapID == 1354)
                            {
                                if (client.Inventory.Contains(3007519))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 314, 39);
                                }
                                if (client.Inventory.Contains(3007520))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 199, 463);
                                }
                                if (client.Inventory.Contains(3007521))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 259, 531);
                                }
                                if (client.Inventory.Contains(3007522))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 424, 654);
                                }
                                if (client.Inventory.Contains(3007523))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 573, 458);
                                }
                                if (client.Inventory.Contains(3007524))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 529, 263);
                                }
                                if (client.Inventory.Contains(3007525))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 702, 660);
                                }
                                if (client.Inventory.Contains(3007526))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 599, 827);
                                }
                                if (client.Inventory.Contains(3007527))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1354, 858, 588);
                                }
                            }
                            if (client.Entity.MapID == 1351)
                            {
                                if (client.Inventory.Contains(3007504))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1351, 203, 210);
                                }
                                if (client.Inventory.Contains(3007505))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1351, 224, 175);
                                }
                                if (client.Inventory.Contains(3007506))
                                {
                                    client.Inventory.Remove(3007528, 1);
                                    client.Entity.Teleport(1351, 380, 273);
                                }
                            }
                        }

                    }
                }
                if (client.ActiveNpc == 3007995)
                {
                    if (client.Inventory.Contains(3007995, 2))
                    {
                        if (req.OptionID == 1)
                        {
                            client.Inventory.Remove(3007995, 1);
                            client.ChiPoints += 200;
                            client.MessageBox("You have got 200 ChiPoints");
                        }
                    }
                    if (req.OptionID == 1)
                    {
                        if (client.Inventory.Contains(client.ActiveNpc, 1))
                        {

                            if (client.Inventory.Contains(3007995))
                            {
                                client.Inventory.Remove(3007995, 1);
                                client.Entity.Teleport(1002, 260, 232);
                            }

                        }

                    }

                }
                #endregion
                if (client.ActiveNpc == 3004249)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID >= 1 && req.OptionID <= 28)
                        {
                            uint IDs = 3004136;
                            if (req.OptionID == 2) IDs = 3004139;
                            else if (req.OptionID == 3) IDs = 3004142;
                            else if (req.OptionID == 4) IDs = 3004144;
                            else if (req.OptionID == 5) IDs = 3004146;
                            else if (req.OptionID == 6) IDs = 3004149;
                            else if (req.OptionID == 7) IDs = 3004154;
                            else if (req.OptionID == 8) IDs = 3004155;
                            else if (req.OptionID == 9) IDs = 3004157;
                            else if (req.OptionID == 10) IDs = 3004158;
                            else if (req.OptionID == 11) IDs = 3004160;
                            else if (req.OptionID == 12) IDs = 3004163;
                            else if (req.OptionID == 13) IDs = 3004164;
                            else if (req.OptionID == 14) IDs = 3004281;
                            else if (req.OptionID == 15) IDs = 3004282;
                            else if (req.OptionID == 16) IDs = 3004283;
                            else if (req.OptionID == 17) IDs = 3004284;
                            else if (req.OptionID == 18) IDs = 3004285;
                            else if (req.OptionID == 19) IDs = 3004286;
                            else if (req.OptionID == 20) IDs = 3004287;
                            else if (req.OptionID == 21) IDs = 3004288;
                            else if (req.OptionID == 22) IDs = 3004289;
                            else if (req.OptionID == 23) IDs = 3004290;
                            else if (req.OptionID == 24) IDs = 3004291;
                            else if (req.OptionID == 25) IDs = 3004292;
                            else if (req.OptionID == 26) IDs = 3004293;
                            else if (req.OptionID == 27) IDs = 3004294;
                            else if (req.OptionID == 28) IDs = 3004295;
                            else if (req.OptionID == 29) IDs = 3004140;
                            ConquerItem _item = new ConquerItem(true);
                            _item.ID = IDs;
                            _item.Durability = _item.MaximDurability = Database.ConquerItemInformation.BaseInformations[IDs].Durability;
                            client.Inventory.Add(_item, Game.Enums.ItemUse.CreateAndAdd);
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 3004247)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID >= 1 && req.OptionID <= 16)
                        {
                            uint IDs = 800020;
                            if (req.OptionID == 2) IDs = 800111;
                            else if (req.OptionID == 3) IDs = 800215;
                            else if (req.OptionID == 4) IDs = 800422;
                            else if (req.OptionID == 5) IDs = 800917;
                            else if (req.OptionID == 6) IDs = 800618;
                            else if (req.OptionID == 7) IDs = 800142;
                            else if (req.OptionID == 8) IDs = 800255;
                            else if (req.OptionID == 9) IDs = 800725;
                            else if (req.OptionID == 10) IDs = 800810;
                            else if (req.OptionID == 11) IDs = 800811;
                            else if (req.OptionID == 12) IDs = 800522;
                            else if (req.OptionID == 13) IDs = 801004;
                            else if (req.OptionID == 14) IDs = 801104;
                            else if (req.OptionID == 15) IDs = 801216;
                            else if (req.OptionID == 16) IDs = 801218;
                            ConquerItem _item = new ConquerItem(true);
                            _item.ID = IDs;
                            _item.Durability = _item.MaximDurability = Database.ConquerItemInformation.BaseInformations[IDs].Durability;
                            client.Inventory.Add(_item, Game.Enums.ItemUse.CreateAndAdd);
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 3004248)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID >= 1 && req.OptionID <= 16)
                        {
                            uint IDs = 823058;
                            if (req.OptionID == 2) IDs = 824018;
                            else if (req.OptionID == 3) IDs = 822071;
                            else if (req.OptionID == 4) IDs = 821033;
                            else if (req.OptionID == 5) IDs = 820073;
                            else if (req.OptionID == 6) IDs = 821034;
                            else if (req.OptionID == 7) IDs = 823060;
                            else if (req.OptionID == 8) IDs = 822072;
                            else if (req.OptionID == 9) IDs = 824020;
                            else if (req.OptionID == 10) IDs = 820076;
                            else if (req.OptionID == 11) IDs = 820074;
                            else if (req.OptionID == 12) IDs = 820075;
                            else if (req.OptionID == 13) IDs = 824019;
                            else if (req.OptionID == 14) IDs = 823059;
                            else if (req.OptionID == 15) IDs = 823061;
                            else if (req.OptionID == 16) IDs = 823062;
                            ConquerItem _item = new ConquerItem(true);
                            _item.ID = IDs;
                            _item.Durability = _item.MaximDurability = Database.ConquerItemInformation.BaseInformations[IDs].Durability;
                            client.Inventory.Add(_item, Game.Enums.ItemUse.CreateAndAdd);
                            client.Inventory.Remove(client.ActiveNpc, 1);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc >= 728525 && client.ActiveNpc <= 728527)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID >= 1 && req.OptionID <= 3)
                        {
                            uint color = 255 | 150 << 16;
                            if (req.OptionID == 2)
                                color = 150 << 8 | 255 << 16;
                            else if (req.OptionID == 3)
                                color = 150 | 255 << 8;
                            byte plus = 1;
                            if (client.ActiveNpc % 10 == 6) plus = 3;
                            else if (client.ActiveNpc % 10 == 7) plus = 6;
                            client.Inventory.Remove(client.ActiveNpc, 1);
                            ConquerItem _item = new ConquerItem(true);
                            _item.ID = 300000;
                            Database.ConquerItemInformation _iteminfos = new Database.ConquerItemInformation(_item.ID, 0);
                            _item.Durability = _item.MaximDurability = _iteminfos.BaseInformation.Durability;
                            _item.Plus = plus;
                            _item.Effect = Game.Enums.ItemEffect.Horse;
                            _item.SocketProgress = color;
                            client.Inventory.Add(_item, Game.Enums.ItemUse.CreateAndAdd);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 3007283)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID >= 1 && req.OptionID <= 3)
                        {
                            uint color = 255 | 150 << 16;
                            if (req.OptionID == 2)
                                color = 150 << 8 | 255 << 16;
                            else if (req.OptionID == 3)
                                color = 150 | 255 << 8;
                            byte plus = 3;
                            client.Inventory.Remove(client.ActiveNpc, 1);
                            ConquerItem _item = new ConquerItem(true);
                            _item.ID = 300000;
                            Database.ConquerItemInformation _iteminfos = new Database.ConquerItemInformation(_item.ID, 0);
                            _item.Durability = _item.MaximDurability = _iteminfos.BaseInformation.Durability;
                            _item.Plus = plus;
                            _item.Effect = Game.Enums.ItemEffect.Horse;
                            _item.SocketProgress = color;
                            _item.Bound = true;
                            client.Inventory.Add(_item, Game.Enums.ItemUse.CreateAndAdd);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc == 3007287)
                {
                    if (client.Inventory.Contains(client.ActiveNpc, 1))
                    {
                        if (req.OptionID >= 1 && req.OptionID <= 3)
                        {
                            uint color = 255 | 150 << 16;
                            if (req.OptionID == 2)
                                color = 150 << 8 | 255 << 16;
                            else if (req.OptionID == 3)
                                color = 150 | 255 << 8;
                            byte plus = 1;
                            client.Inventory.Remove(client.ActiveNpc, 1);
                            ConquerItem _item = new ConquerItem(true);
                            _item.ID = 300000;
                            Database.ConquerItemInformation _iteminfos = new Database.ConquerItemInformation(_item.ID, 0);
                            _item.Durability = _item.MaximDurability = _iteminfos.BaseInformation.Durability;
                            _item.Plus = plus;
                            _item.Effect = Game.Enums.ItemEffect.Horse;
                            _item.SocketProgress = color;
                            _item.Bound = true;
                            client.Inventory.Add(_item, Game.Enums.ItemUse.CreateAndAdd);
                        }
                    }
                    return;
                }
                if (client.ActiveNpc >= 729611 && client.ActiveNpc <= 729614 || client.ActiveNpc == 729703)
                {
                    if (req.OptionID == 1)
                    {
                        if (client.Entity.ConquerPoints >= 5)
                        {
                            if (client.Entity.SpiritBeadsType == 0)
                            {
                                client.IncreaseExperience(client.ExpBall * 2, false);
                                client.AddBless(7 * 60 * 60);
                                client.Entity.SubClasses.StudyPoints += 30;
                                client.Inventory.Add(729304, 0, 1);
                            }
                            if (!client.Quests.HasQuest(QuestID.Spirit_Beads))
                            {
                                client.Quests.Accept(QuestID.Spirit_Beads);
                            }
                            client.Quests.FinishQuest(QuestID.Spirit_Beads);
                            if (client.Entity.SpiritBeadsType == 1)
                            {
                                client.IncreaseExperience(client.ExpBall * 3, false);
                                client.AddBless(10 * 60 * 60);
                                client.Entity.SubClasses.StudyPoints += 40;
                                client.Inventory.Add(729304, 0, 1);
                            }
                            if (client.Entity.SpiritBeadsType == 2)
                            {
                                client.IncreaseExperience(client.ExpBall * 4, false);
                                client.AddBless(15 * 60 * 60);
                                client.Entity.SubClasses.StudyPoints += 50;
                                client.Inventory.Add(729304, 0, 1);
                            }
                            if (client.Entity.SpiritBeadsType == 3)
                            {
                                client.IncreaseExperience(client.ExpBall * 5, false);
                                client.AddBless(24 * 60 * 60);
                                client.Entity.SubClasses.StudyPoints += 80;
                                client.Inventory.Add(729304, 0, 1);
                            }
                            if (client.Entity.SpiritBeadsType == 4)
                            {
                                client.IncreaseExperience(client.ExpBall * 6, false);
                                client.AddBless(48 * 60 * 60);
                                client.Entity.SubClasses.StudyPoints += 100;
                                client.Inventory.Add(729304, 0, 1);
                            }
                            _String str = new _String(true);
                            str.UID = client.Entity.UID;
                            str.TextsCount = 1;
                            str.Type = _String.Effect;
                            str.Texts.Add("end_task");
                            client.SendScreen(str, true);
                            client.Inventory.Remove(client.ActiveNpc, 1);
                            client.Entity.ConquerPoints -= 5;
                        }
                        else
                        {
                            client.MessageBox("You Don't Have 5 CPS!");
                        }
                    }
                    if (req.OptionID == 2)
                    {
                        if (client.Entity.SpiritBeadsType == 0)
                        {
                            client.IncreaseExperience(client.ExpBall * 2, false);
                            client.AddBless(7 * 60 * 60);
                            client.Entity.SubClasses.StudyPoints += 30;
                            client.Inventory.Add(729304, 0, 1);
                        }
                        if (!client.Quests.HasQuest(QuestID.Spirit_Beads))
                        {
                            client.Quests.Accept(QuestID.Spirit_Beads);
                        }
                        client.Quests.FinishQuest(QuestID.Spirit_Beads);
                        if (client.Entity.SpiritBeadsType == 1)
                        {
                            client.IncreaseExperience(client.ExpBall * 3, false);
                            client.AddBless(10 * 60 * 60);
                            client.Entity.SubClasses.StudyPoints += 40;
                            client.Inventory.Add(729304, 0, 1);
                        }
                        if (client.Entity.SpiritBeadsType == 2)
                        {
                            client.IncreaseExperience(client.ExpBall * 4, false);
                            client.AddBless(15 * 60 * 60);
                            client.Entity.SubClasses.StudyPoints += 50;
                            client.Inventory.Add(729304, 0, 1);
                        }
                        if (client.Entity.SpiritBeadsType == 3)
                        {
                            client.IncreaseExperience(client.ExpBall * 5, false);
                            client.AddBless(24 * 60 * 60);
                            client.Entity.SubClasses.StudyPoints += 80;
                            client.Inventory.Add(729304, 0, 1);
                        }
                        if (client.Entity.SpiritBeadsType == 4)
                        {
                            client.IncreaseExperience(client.ExpBall * 6, false);
                            client.AddBless(48 * 60 * 60);
                            client.Entity.SubClasses.StudyPoints += 100;
                            client.Inventory.Add(729304, 0, 1);
                        }
                        _String str = new _String(true);
                        str.UID = client.Entity.UID;
                        str.TextsCount = 1;
                        str.Type = _String.Effect;
                        str.Texts.Add("end_task");
                        client.SendScreen(str, true);
                        client.Inventory.Remove(client.ActiveNpc, 1);
                    }
                }
                if (client.Map.Npcs.TryGetValue(client.ActiveNpc, out npc))
                {
                    if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, npc.X, npc.Y) > 16)
                        return;
                    if (req.OptionID == 255 || (req.OptionID == 0 && ID == 2032))
                        return;
                    if (client.Entity.Dead)
                        return;

                    req.NpcID = client.ActiveNpc;
                    Npcs.GetDialog(req, client);

                }

            }
        }
    }
}
