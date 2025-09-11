using MySqlX.XDevAPI;
using Nyx.Server.Game;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nyx.Server.Database
{
    public static class EntityTable
    {
        public static bool LoadEntity(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities").Where("UID", client.Account.EntityID))
            using (var reader = new MySqlReader(cmd))
            {
                if (reader.Read())
                {
                    try
                    {
                        Game.IJiangHu hu;
                        client.Entity = new Game.Entity(Game.EntityFlag.Player, false);
                        client.Entity.Name = reader.ReadString("Name").Remove("").Remove("guard").Remove("owner").Remove("PurpleBanshee").Remove("SnowBanshee").Remove("NemesisTyrant").Remove("TeratoDragon").Remove("GoldenOctopus").Remove("SilverOctopus").Remove("kosom").Remove("kosm").Remove("mtnak").Remove("kos").Remove("dick").Remove("mother").Remove("fuck").Remove("ass").Remove("pussy").Remove("bitch").Remove("omk").Remove("sharmot").Remove("sharmoot").Remove("5wl").Remove("5owl").Remove("5awl").Remove("zanya").Remove("zania").Remove("3rs").Remove("kosomen").Remove("mayteen").Remove("mayten").Remove("a7a").Remove("a7eh");
                        foreach (var c in Kernel.Insults)
                        {
                            if (client.Entity.Name.Contains(c))
                                client.Entity.Name.Remove(c);
                        }
                        client.Entity.SwordSoul = reader.ReadBoolean("SwordSoul");
                        client.Entity.BansheeSpirit = reader.ReadBoolean("BansheeSpirit");
                        client.Entity.FirstCredit = reader.ReadByte("FirstCredit");
                        client.Entity.QuizPoints = reader.ReadUInt32("QuizPoints");
                        client.Entity.Nullifications = reader.ReadUInt32("Nullifications");
                        client.Entity.DidBrightFortune = reader.ReadBoolean("DidBrightFortune");
                        client.Entity.StarterPackLevel = reader.ReadByte("StarterPackLevel");
                        client.WarehousePW = reader.ReadUInt32("WarehousePW");
                        client.Entity.AnsweredKingdomQuiz = reader.ReadBoolean("AnsweredKingdomQuiz");
                        client.Entity.Spouse = reader.ReadString("Spouse");
                        client.Entity.DailySignVIPChances = reader.ReadByte("DailySignVIPChances");
                        client.Entity.Owner = client;
                        client.Entity.GLCTF = reader.ReadByte("GLCTF");
                        client.Entity.NormalGems = reader.ReadUInt32("NormalGems");
                        client.MoneySave = reader.ReadUInt32("MoneySave");
                        #region Merchant
                        if (reader.ReadUInt64("Merchant") == 0)
                            client.Entity.Merchant = 0;
                        if (reader.ReadUInt64("Merchant") == 1)
                        {
                            client.Entity.Merchant = 1;
                            client.Entity.Update(Update.Merchant, 255, false);
                        }
                        if (reader.ReadUInt64("Merchant") > 1)
                        {
                            client.Entity.Merchant = reader.ReadUInt64("Merchant");
                            if ((ulong)DateTime.Now.DayOfYear >= client.Entity.Merchant + 5)
                            {
                                client.Send(new Data(true)
                                {
                                    UID = client.Entity.UID,
                                    ID = Data.OpenCustom,
                                    dwParam = Data.CustomCommands.MerchantDone
                                });
                                client.Entity.Merchant = 1;
                                client.Entity.Update(Update.Merchant, 255, false);
                            }
                            else
                            {
                                client.Send(new Data(true)
                                {
                                    UID = client.Entity.UID,
                                    ID = Data.OpenCustom,
                                    dwParam = Data.CustomCommands.MerchantApply
                                });
                            }
                        }
                        #endregion
                        client.Entity.Experience = reader.ReadUInt64("Experience");
                        client.Entity.Money = reader.ReadUInt32("Money");
                        client.Entity.ConquerPoints = reader.ReadUInt32("ConquerPoints");
                        client.Entity.BoundCps = reader.ReadUInt32("BoundCPs");
                        client.Entity.EditNameTimes = reader.ReadByte("EditNameTimes");
                        client.Entity.DailySignRewards = reader.ReadByte("DailySignRewards");
                        if (reader.ReadUInt32("UnionID") > 0)
                        {
                            client.Union = Kernel.Unions[reader.ReadUInt32("UnionID")];
                            client.Offical = reader.ReadUInt32("Offical");
                            client.Harem = reader.ReadUInt32("Harem");
                            client.Guards = reader.ReadUInt32("Guards");
                            client.UnionExploits = reader.ReadUInt32("UnionExploits");
                        }
                        client.Entity.UID = reader.ReadUInt32("UID");
                        client.Entity.MyAchievement = new Game.Achievement(client.Entity);
                        client.Entity.MyAchievement.Load(reader.ReadString("Achievement"));
                        client.Entity.Hitpoints = reader.ReadUInt32("Hitpoints");
                        client.Entity.OnlineTraining = reader.ReadUInt32("OnlineTrainning");
                        client.Entity.BlessedHunting = reader.ReadUInt32("HuntingExp");
                        client.Entity.Body = reader.ReadUInt16("Body");
                        client.Entity.Face = reader.ReadUInt16("Face");
                        client.Entity.Strength = reader.ReadUInt16("Strength");
                        client.Entity.Titles = new ConcurrentDictionary<TitlePacket.Titles, DateTime>();
                        client.Entity.MyTitle = (TitlePacket.Titles)reader.ReadUInt32("My_Title");
                        client.Entity.Agility = reader.ReadUInt16("Agility");
                        client.Entity.ExtraInventory = reader.ReadUInt32("ExtraInventory");
                        client.Entity.Spirit = reader.ReadUInt16("Spirit");
                        client.Entity.DailySignDays = new byte[31];
                        var dailys = reader.ReadString("DailySignedDays").Split('#');
                        if (dailys.Length >= 30)
                        {
                            for (byte x = 0; x < client.Entity.DailySignDays.Length; x++)
                            {
                                client.Entity.DailySignDays[x] = byte.Parse(dailys[x]);
                            }
                        }
                        client.Entity.Vitality = reader.ReadUInt16("Vitality");
                        client.Entity.Atributes = reader.ReadUInt16("Atributes");
                        client.ElitePKStats = new ElitePK.FighterStats(client.Entity.UID, client.Entity.Name, client.Entity.Mesh);
                        client.Entity.SubClass = reader.ReadByte("SubClass");
                        client.Entity.SubClassLevel = reader.ReadByte("SubClassLevel");
                        client.Entity.SubClasses.Active = client.Entity.SubClass;
                        client.Entity.SubClassesActive = client.Entity.SubClass;
                        client.Entity.SubClasses.StudyPoints = reader.ReadUInt16("StudyPoints");
                        client.VirtuePoints = (uint)reader.ReadUInt32("VirtuePoints");
                        client.Entity.Mana = reader.ReadUInt16("Mana");
                        client.Entity.HairStyle = reader.ReadUInt16("HairStyle");
                        client.Entity.MapID = reader.ReadUInt16("MapID");
                        client.Entity.CountryID = reader.ReadUInt16("CountryID");
                        client.Entity.X = reader.ReadUInt16("X");
                        client.Entity.Y = reader.ReadUInt16("Y");
                        if ((Constants.PKFreeMaps.Contains(client.Entity.MapID)
                            && client.Entity.MapID != 6000 && client.Entity.MapID != 6001
                            && client.Entity.MapID != 6002 && client.Entity.MapID != 6003
                            && client.Entity.MapID != 6004 && client.Entity.MapID != 6000)
                            || client.Entity.MapID >= 4000 && client.Entity.MapID <= 4020 || client.Entity.MapID >= 11000
                            || client.Entity.MapID == 1738)
                        {
                            client.Entity.MapID = 1002;
                            client.Entity.X = 300;
                            client.Entity.Y = 278;
                        }
                        if (client.Entity.MapID == 1040)//SkyPass
                        {
                            client.Entity.MapID = 1020;
                            client.Entity.X = 566;
                            client.Entity.Y = 563;
                        }
                        if (client.JustCreated)
                        {
                            if (client.Entity.X == 0 && client.Entity.Y == 0)
                            {
                                client.Entity.MapID = 1002;
                                int R = Kernel.Random.Next(1, 5);
                                if (R == 1)
                                {
                                    client.Entity.X = 266;
                                    client.Entity.Y = 295;
                                }
                                if (R == 2)
                                {
                                    client.Entity.X = 285;
                                    client.Entity.Y = 297;
                                }
                                if (R == 3)
                                {
                                    client.Entity.X = 328;
                                    client.Entity.Y = 239;
                                }
                                if (R == 4)
                                {
                                    client.Entity.X = 324;
                                    client.Entity.Y = 284;
                                }
                                if (R == 5)
                                {
                                    client.Entity.X = 262;
                                    client.Entity.Y = 222;
                                }
                            }
                        }
                        client.BlessTime = reader.ReadUInt32("BlessTime");
                        client.InLottery = client.Entity.MapID == 700;
                        client.LotteryEntries = reader.ReadByte("LotteryEntries");
                        client.Entity.PreviousMapID = reader.ReadUInt16("PreviousMapID");
                        client.Entity.PKPoints = reader.ReadUInt16("PKPoints");
                        client.Entity.Class = reader.ReadByte("Class");
                        client.Entity.Level = reader.ReadByte("Level");
                        client.Entity.FirstRebornClass = reader.ReadByte("FirstRebornClass");
                        client.Entity.SecondRebornClass = reader.ReadByte("SecondRebornClass");
                        if (client.Entity.FirstRebornClass != 0)
                            client.Entity.Reborn = 1;
                        if (client.Entity.SecondRebornClass != 0)
                            client.Entity.Reborn = 2;
                        client.Entity.FirstRebornLevel = reader.ReadByte("FirstRebornLevel");
                        client.Entity.SecondRebornLevel = reader.ReadByte("SecondRebornLevel");
                        client.LastDragonBallUse = DateTime.FromBinary(reader.ReadInt64("LastDragonBallUse"));
                        client.LastResetTime = DateTime.FromBinary(reader.ReadInt64("LastResetTime"));
                        client.Entity.EnlightenPoints = reader.ReadUInt128("EnlightenPoints");
                        client.Entity.EnlightmentTime = reader.ReadUInt16("EnlightmentWait");
                        if (client.Entity.EnlightmentTime > 0)
                        {
                            if (client.Entity.EnlightmentTime % 20 > 0)
                            {
                                client.Entity.EnlightmentTime -= (ushort)(client.Entity.EnlightmentTime % 20);
                                client.Entity.EnlightmentTime += 20;
                            }
                        }
                        client.Entity.ReceivedEnlightenPoints = reader.ReadByte("EnlightsReceived");
                        client.Entity.DoubleExperienceTime = reader.ReadUInt16("DoubleExpTime");
                        client.DoubleExpToday = reader.ReadBoolean("DoubleExpToday");
                        client.Entity.HeavenBlessing = reader.ReadUInt32("HeavenBlessingTime");
                        client.Entity.VIPLevel = reader.ReadByte("VIPLevel");
                        client.Entity.PrevX = reader.ReadUInt16("PreviousX");
                        client.Entity.PrevY = reader.ReadUInt16("PreviousY");
                        client.ExpBalls = reader.ReadByte("ExpBalls");
                        //client.Entity.mdf31 = reader.ReadUInt32("mdf31"); 
                        //client.Entity.BlackList = new List<string>();

                        var BlackList = reader.ReadString("BlackList").Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var person in BlackList)
                        {
                            if (person != null && person != "" && person != "@@")
                                client.Entity.BlackList.Add(person);
                        }
                        client.Entity.ClanId = reader.ReadUInt32("ClanId");
                        client.Entity.ClanRank = (Clan.Ranks)reader.ReadUInt32("ClanRank");
                        if (client.Entity.MapID == 601)
                            client.OfflineTGEnterTime = DateTime.FromBinary(reader.ReadInt64("OfflineTGEnterTime"));
                        Game.ConquerStructures.Nobility.Sort(client.Entity.UID);

                        if (Kernel.Guilds.ContainsKey(reader.ReadUInt32("GuildID")))
                        {
                            client.Guild = Kernel.Guilds[reader.ReadUInt32("GuildID")];
                            if (client.Guild.Members.ContainsKey(client.Entity.UID))
                            {
                                client.AsMember = client.Guild.Members[client.Entity.UID];
                                if (client.AsMember.GuildID == 0)
                                {
                                    client.AsMember = null;
                                    client.Guild = null;
                                }
                                else
                                {
                                    client.Entity.GuildID = (ushort)client.Guild.ID;
                                    client.Entity.GuildRank = (ushort)client.AsMember.Rank;
                                }
                            }
                            else
                                client.Guild = null;
                        }
                        if (!Game.ConquerStructures.Nobility.Board.TryGetValue(client.Entity.UID, out client.NobilityInformation))
                        {
                            client.NobilityInformation = new Nyx.Server.Game.ConquerStructures.NobilityInformation();
                            client.NobilityInformation.EntityUID = client.Entity.UID;
                            client.NobilityInformation.Name = client.Entity.Name;
                            client.NobilityInformation.Donation = 0;
                            client.NobilityInformation.Rank = Nyx.Server.Game.ConquerStructures.NobilityRank.Serf;
                            client.NobilityInformation.Position = -1;
                            client.NobilityInformation.Gender = 1;
                            client.NobilityInformation.Mesh = client.Entity.Mesh;
                            if (client.Entity.Body % 10 >= 3)
                                client.NobilityInformation.Gender = 0;
                        }
                        client.Entity.NobilityRank = client.NobilityInformation.Rank;


                        #region Team Arena
                        Game.TeamArena.ArenaStatistics.TryGetValue(client.Entity.UID, out client.TeamArenaStatistic);
                        if (client.TeamArenaStatistic == null)
                        {
                            client.TeamArenaStatistic = new Nyx.Server.Network.GamePackets.TeamArenaStatistic(true);
                            client.TeamArenaStatistic.EntityID = client.Entity.UID;
                            client.TeamArenaStatistic.Name = client.Entity.Name;
                            client.TeamArenaStatistic.Level = client.Entity.Level;
                            client.TeamArenaStatistic.Class = client.Entity.Class;
                            client.TeamArenaStatistic.Model = client.Entity.Mesh;
                            TeamArenaTable.InsertArenaStatistic(client);
                            client.TeamArenaStatistic.Status = Network.GamePackets.TeamArenaStatistic.NotSignedUp;
                            if (Game.TeamArena.ArenaStatistics.ContainsKey(client.Entity.UID))
                                Game.TeamArena.ArenaStatistics.Remove(client.Entity.UID);
                            Game.TeamArena.ArenaStatistics.Add(client.Entity.UID, client.TeamArenaStatistic);
                        }
                        else if (client.TeamArenaStatistic.EntityID == 0)
                        {
                            client.TeamArenaStatistic = new Nyx.Server.Network.GamePackets.TeamArenaStatistic(true);
                            client.TeamArenaStatistic.EntityID = client.Entity.UID;
                            client.TeamArenaStatistic.Name = client.Entity.Name;
                            client.TeamArenaStatistic.Level = client.Entity.Level;
                            client.TeamArenaStatistic.Class = client.Entity.Class;
                            client.TeamArenaStatistic.Model = client.Entity.Mesh;
                            TeamArenaTable.InsertArenaStatistic(client);
                            client.TeamArenaStatistic.Status = Network.GamePackets.TeamArenaStatistic.NotSignedUp;
                            if (Game.TeamArena.ArenaStatistics.ContainsKey(client.Entity.UID))
                                Game.TeamArena.ArenaStatistics.Remove(client.Entity.UID);
                            Game.TeamArena.ArenaStatistics.Add(client.Entity.UID, client.TeamArenaStatistic);
                        }
                        else
                        {
                            client.TeamArenaStatistic.Level = client.Entity.Level;
                            client.TeamArenaStatistic.Class = client.Entity.Class;
                            client.TeamArenaStatistic.Model = client.Entity.Mesh;
                            client.TeamArenaStatistic.Name = client.Entity.Name;
                        }
                        Game.TeamArena.Clear(client);
                        #endregion
                        #region Arena
                        Game.Arena.ArenaStatistics.TryGetValue(client.Entity.UID, out client.ArenaStatistic);
                        if (client.ArenaStatistic == null)
                        {
                            client.ArenaStatistic = new Nyx.Server.Network.GamePackets.ArenaStatistic(true);
                            client.ArenaStatistic.EntityID = client.Entity.UID;
                            client.ArenaStatistic.Name = client.Entity.Name;
                            client.ArenaStatistic.Level = client.Entity.Level;
                            client.ArenaStatistic.Class = client.Entity.Class;
                            client.ArenaStatistic.Model = client.Entity.Mesh;
                            client.ArenaPoints = ArenaTable.ArenaPointFill(client.Entity.Level);
                            client.ArenaStatistic.LastArenaPointFill = DateTime.Now;
                            ArenaTable.InsertArenaStatistic(client);
                            client.ArenaStatistic.Status = Network.GamePackets.ArenaStatistic.NotSignedUp;
                            if (Game.Arena.ArenaStatistics.ContainsKey(client.Entity.UID))
                                Game.Arena.ArenaStatistics.Remove(client.Entity.UID);
                            Game.Arena.ArenaStatistics.Add(client.Entity.UID, client.ArenaStatistic);
                        }
                        else if (client.ArenaStatistic.EntityID == 0)
                        {
                            client.ArenaStatistic = new Nyx.Server.Network.GamePackets.ArenaStatistic(true);
                            client.ArenaStatistic.EntityID = client.Entity.UID;
                            client.ArenaStatistic.Name = client.Entity.Name;
                            client.ArenaStatistic.Level = client.Entity.Level;
                            client.ArenaStatistic.Class = client.Entity.Class;
                            client.ArenaStatistic.Model = client.Entity.Mesh;
                            client.ArenaPoints = ArenaTable.ArenaPointFill(client.Entity.Level);
                            client.ArenaStatistic.LastArenaPointFill = DateTime.Now;
                            ArenaTable.InsertArenaStatistic(client);
                            client.ArenaStatistic.Status = Network.GamePackets.ArenaStatistic.NotSignedUp;
                            if (Game.Arena.ArenaStatistics.ContainsKey(client.Entity.UID))
                                Game.Arena.ArenaStatistics.Remove(client.Entity.UID);
                            Game.Arena.ArenaStatistics.Add(client.Entity.UID, client.ArenaStatistic);
                        }
                        else
                        {
                            client.ArenaStatistic.Level = client.Entity.Level;
                            client.ArenaStatistic.Class = client.Entity.Class;
                            client.ArenaStatistic.Model = client.Entity.Mesh;
                            client.ArenaStatistic.Name = client.Entity.Name;
                        }
                        client.ArenaPoints = client.ArenaStatistic.ArenaPoints;
                        client.CurrentHonor = client.ArenaStatistic.CurrentHonor;
                        client.HistoryHonor = client.ArenaStatistic.HistoryHonor;
                        Game.Arena.Clear(client);
                        #endregion
                        #region ChampionPoints
                        Game.Arena.ChampionArenaStatistics.TryGetValue(client.Entity.UID, out client.CP);
                        if (client.CP == null)
                        {
                            client.CP = new Nyx.Server.Network.GamePackets.ChampionPoints();
                            client.CP.EntityID = client.Entity.UID;
                            client.CP.Points = 0;
                            client.CP.TodayPoints = 0;
                            client.CP.AllChampionPoints = 0;
                            if (Game.Arena.ChampionArenaStatistics.ContainsKey(client.Entity.UID))
                                Game.Arena.ChampionArenaStatistics.Remove(client.Entity.UID);
                            Game.Arena.ChampionArenaStatistics.Add(client.Entity.UID, client.CP);
                        }
                        else if (client.CP.EntityID == 0)
                        {
                            client.CP = new Nyx.Server.Network.GamePackets.ChampionPoints();
                            client.CP.EntityID = client.Entity.UID;
                            client.CP.Points = 0;
                            client.CP.TodayPoints = 0;
                            client.CP.AllChampionPoints = 0;
                            if (Game.Arena.ChampionArenaStatistics.ContainsKey(client.Entity.UID))
                                Game.Arena.ChampionArenaStatistics.Remove(client.Entity.UID);
                            Game.Arena.ChampionArenaStatistics.Add(client.Entity.UID, client.CP);
                        }
                        else
                        {
                            client.CP.Points = client.ChampionPoints;
                        }
                        client.ChampionPoints = client.CP.Points;
                        #endregion
                        if (Game.JiangHu.JiangHuClients.TryGetValue(client.Entity.UID, out hu))
                        {
                            client.Entity.MyJiang = hu as Nyx.Server.Game.JiangHu;
                            client.Entity.MyJiang.TimerStamp = DateTime.Now;
                        }

                        SetFlowers(client);
                        if (client.JustCreated)
                            client.Inventory.Add(3000550, 0, 1);

                        client.Entity.FullyLoaded = true;
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        return false;
                    }
                }
                else
                    return false;
            }
        }
        public static void TryReset(Client.GameClient client)
        {
            if (DateTime.Now >= client.LastResetTime.AddDays(1))
            {
                DailyQuestTable.Reset(client);
                ActivenessTable.Reset(client);
                if (client.ChiPoints < 4000)
                    client.ChiPoints += (uint)Math.Min(500, 4000 - client.ChiPoints);
                #region EnlightenmentPoints
                if (client.Entity.Level >= 90)
                {
                    client.Entity.EnlightenPoints = 1;
                    if (client.Entity.NobilityRank == Nyx.Server.Game.ConquerStructures.NobilityRank.Knight ||
                        client.Entity.NobilityRank == Nyx.Server.Game.ConquerStructures.NobilityRank.Baron)
                        client.Entity.EnlightenPoints += 1;
                    else if (client.Entity.NobilityRank == Nyx.Server.Game.ConquerStructures.NobilityRank.Earl ||
                        client.Entity.NobilityRank == Nyx.Server.Game.ConquerStructures.NobilityRank.Duke)
                        client.Entity.EnlightenPoints += 2;
                    else if (client.Entity.NobilityRank == Nyx.Server.Game.ConquerStructures.NobilityRank.Prince)
                        client.Entity.EnlightenPoints += 3;
                    else if (client.Entity.NobilityRank == Nyx.Server.Game.ConquerStructures.NobilityRank.King)
                        client.Entity.EnlightenPoints += 4;
                    if (client.Entity.VIPLevel != 0)
                    {
                        if (client.Entity.VIPLevel <= 3)
                            client.Entity.EnlightenPoints += 1;
                        else if (client.Entity.VIPLevel <= 5)
                            client.Entity.EnlightenPoints += 2;
                        else if (client.Entity.VIPLevel == 6)
                            client.Entity.EnlightenPoints += 3;
                    }
                }
                client.Entity.ReceivedEnlightenPoints = 0;
                #endregion
                client.Entity.SwordSoul = false;
                client.Entity.BansheeSpirit = false;
                client.DoubleExpToday = false;
                client.ExpBalls = 0;
                client.LotteryEntries = 0;
                ResetExpball(client);
                ResetLottery(client);
                client.LastResetTime = DateTime.Now;
                client.Entity.AnsweredKingdomQuiz = false;
                client.Quests.Reset(client, false);
                client.Entity.TodayStrikePoints = 0;
                client.Entity.KingdomDeed = 0;
            }
        }
        public static void UpdateData(Client.GameClient client, string column, object value)
        {
            UpdateData(client.Entity.UID, column, value);
        }
        public static void SetFlowers(Client.GameClient client)
        {
            if (client.Entity.Body == 2001 || client.Entity.Body == 2002)
            {
                if (!Game.Features.Flowers.Flowers_Poll.ContainsKey(client.Entity.UID))
                {
                    client.Entity.MyFlowers = new Game.Features.Flowers(client.Entity.UID, client.Entity.Name);
                    Game.Features.Flowers.Flowers_Poll.TryAdd(client.Entity.UID, client.Entity.MyFlowers);
                    return;
                }
                client.Entity.MyFlowers = Nyx.Server.Game.Features.Flowers.Flowers_Poll[client.Entity.UID];
                return;
            }
            else
            {
                if (!Game.Features.Flowers.BoyFlowers.ContainsKey(client.Entity.UID))
                {
                    client.Entity.MyFlowers = new Game.Features.Flowers(client.Entity.UID, client.Entity.Name);
                    Game.Features.Flowers.BoyFlowers.TryAdd(client.Entity.UID, client.Entity.MyFlowers);
                    return;
                }
                client.Entity.MyFlowers = Game.Features.Flowers.BoyFlowers[client.Entity.UID];
                return;
            }
        }
        public static void UpdateData(uint UID, string column, object value)
        {
            if (value is Boolean)
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                    cmd.Update("entities").Set(column, (Boolean)value).Where("UID", UID)
                        .Execute();
            }
            else
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                    cmd.Update("entities").Set(column, value.ToString()).Where("UID", UID)
                        .Execute();
            }
        }
        public static void UpdateGuildRank(uint UID, Nyx.Server.Game.Enums.GuildMemberRank rank)
        {
            UpdateData(UID, "GuildRank", (int)rank);
        }
        public static void LoginNow(Client.GameClient client)
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
            cmd.Update("entities").Set("GuildLastLogin", client.MemberLastLogin).Where("UID", client.Entity.UID).Execute();
        }
        public static void UpdateOnlineStatus(Client.GameClient client, bool online)
        {
            if (online || (!online && client.DoSetOffline))
            {
                UpdateData(client, "Online", online);
            }
            if (online)
            {
                //Console.WriteLine(client.Entity.Name + " has been logged on! IP : " + client.IP + "");
                LoggingService.ClientLoginSuccess(client.Entity.Name, client.IP);
            }
            else
            {
                //Console.WriteLine(client.Entity.Name + " has been logged off! IP : " + client.IP + "");
                LoggingService.ClientDisconnected(client.IP, client.Entity.Name);
            }
        }
        public static void UpdaNyxney(Client.GameClient client)
        {
            UpdateData(client, "Money", client.Entity.Money);
        }
        public static void UpdateCps(Client.GameClient client)
        {
            if (client != null && client.Entity != null)
            {
                UpdateData(client, "ConquerPoints", client.Entity.ConquerPoints);
            }
        }
        public static void UpdateMoney(Client.GameClient client)
        {
            UpdateData(client, "Money", client.Entity.Money);
        }
        public static void UpdateCps(uint uid, uint value)
        {
            using (var cmd2 = new Database.MySqlCommand(Database.MySqlCommandType.SELECT).Select("entities").Where("UID", uid))
            using (var reader = new Database.MySqlReader(cmd2))
            {
                if (reader.Read())
                {
                    value += reader.ReadUInt32("ConquerPoints");
                }
                reader.Close();
            }
            using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE).Update("entities"))
            {
                cmd.Set("ConquerPoints", value).Where("UID", uid).Execute();
            }
        }
        public static void UpdateMoney(uint uid, uint value)
        {
            using (var cmd2 = new Database.MySqlCommand(Database.MySqlCommandType.SELECT).Select("entities").Where("UID", uid))
            using (var reader = new Database.MySqlReader(cmd2))
            {
                if (reader.Read())
                {
                    value += reader.ReadUInt32("Money");
                }
                reader.Close();
            }
            using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE).Update("entities"))
            {
                cmd.Set("Money", value).Where("UID", uid).Execute();
            }
        }
        public static void UpdateLevel(Client.GameClient client)
        {
            UpdateData(client, "Level", client.Entity.Level);
        }
        public static void UpdateGuildID(Client.GameClient client)
        {
            UpdateData(client, "guildid", client.Entity.GuildID);
        }
        public static void UpdateClanID(Client.GameClient client)
        {
            UpdateData(client, "ClanId", client.Entity.ClanId);
        }
        public static void RemoveClan(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))

                cmd.Update("entities").Set("ClanId", 0).Set("ClanDonation", 0).Set("ClanRank", 0)
                    .Where("ClanId", client.Entity.ClanId).Set("mdf31", 0).Execute();

        }
        public static void RemoveClanMember(string name)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                cmd.Update("entities").Set("ClanId", 0).Set("ClanDonation", 0).Set("mdf31", 0).Set("ClanRank", 0).Where("Name", name).Execute();
        }

        public static void UpdateClanRank(Client.GameClient client)
        {
            UpdateData(client, "ClanRank", (uint)client.Entity.ClanRank);
        }
        public static void UpdateClanRank(uint UID, uint rank)
        {
            UpdateData(UID, "ClanRank", rank);
        }
        public static ushort GetClass(string Name)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities").Where("Name", Name))
            using (var reader = new MySqlReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.ReadUInt16("Class");
                }
            }
            return 0;
        }
        public static void UpdateClanDonation(Client.GameClient client)
        {
            UpdateData(client, "clandonation", (uint)client.Entity.ClanRank);
        }
        public static void UpdateGuildRank(Client.GameClient client)
        {
            UpdateData(client, "GuildRank", client.Entity.GuildRank);
        }
        public static void UpdateSkillExp(Client.GameClient client, uint spellid, uint exp)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                cmd.Update("skills").Set("Experience", exp).Where("EntityID", client.Entity.UID).And("ID", spellid).Execute();
        }
        public static void ResetLottery(Client.GameClient client)
        {
            UpdateData(client, "LotteryEntries", 0);
        }
        public static void ResetExpball(Client.GameClient client)
        {
            UpdateData(client, "ExpBalls", 0);
        }
        public static bool SaveEntity(Client.GameClient c, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {
                Game.Entity e = c.Entity;
                if (e.Owner.JustCreated) return true;
                //#region BlackList
                //string Persons = "";
                //if (e.BlackList.Count > 0 && e.BlackList != null)
                //{
                //    foreach (var person in e.BlackList)
                //    {
                //        Persons += person + "@@";
                //    }
                //}
                //#endregion
                #region DailySign
                string DailySign = "";
                foreach (var index in e.DailySignDays)
                {
                    DailySign += index.ToString() + "#";
                }
                #endregion
                #region Wardrobe
                string wardrobe = "";
                if (e.NowEquippedWing.Count > 0)
                {
                    wardrobe += e.NowEquippedWing[0].Split('~')[0] + "-" + e.NowEquippedWing[0].Split('~')[1] + "~";
                }
                else
                {
                    wardrobe += "0-0~";
                }
                if (e.NowEquippedTitle.Count > 0)
                {
                    wardrobe += e.NowEquippedTitle[0].Split('~')[0] + "-" + e.NowEquippedTitle[0].Split('~')[1];
                }
                else
                {
                    wardrobe += "0-0";
                }
                #endregion
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("entities"))
                {
                    cmd.Set("WarehousePW", c.WarehousePW)
                       .Set("Spouse", e.Spouse)
                       .Set("Money", e.Money)
                       .Set("UnionID", e.Owner.UnionID)
                       .Set("Wardrobe", wardrobe)
                       .Set("Harem", e.Owner.Harem)
                       .Set("TotalPerfectionScore", e.TotalPerfectionScore)
                       .Set("Guards", e.Owner.Guards)
                       .Set("BoundCps", e.BoundCps)
                       .Set("DailySignedDays", DailySign)
                       //.Set("BlackList", Persons)
                       .Set("DailySignRewards", e.DailySignRewards)
                       .Set("Offical", e.Owner.Offical)
                       .Set("UnionExploits", e.Owner.UnionExploits)
                       .Set("Merchant", e.Merchant)
                       .Set("EditNameTimes", e.EditNameTimes)
                       .Set("Nullifications", e.Nullifications)
                       .Set("DidBrightFortune", e.DidBrightFortune)
                       .Set("Experience", e.Experience)
                       .Set("DailySignVIPChances", e.DailySignVIPChances)
                       .Set("AnsweredKingdomQuiz", e.AnsweredKingdomQuiz)
                       .Set("ConquerPoints", e.ConquerPoints)
                       .Set("NormalGems", e.NormalGems)
                       .Set("QuizPoints", e.QuizPoints)
                       .Set("FirstCredit", e.FirstCredit)
                       .Set("Body", e.Body)
                       .Set("Face", e.Face)
                       .Set("Class", e.Class)
                       .Set("Level", e.Level)
                       .Set("OnlineTrainning", e.OnlineTraining)
                       .Set("HuntingExp", e.BlessedHunting)
                       .Set("GLCTF", e.GLCTF)
                       .Set("My_Title", (byte)e.MyTitle)
                       .Set("StudyPoints", e.SubClasses.StudyPoints)
                       .Set("HairStyle", e.HairStyle)
                       .Set("EnlightsReceived", e.ReceivedEnlightenPoints)
                       .Set("PKPoints", e.PKPoints)
                       .Set("StarterPackLevel", e.StarterPackLevel)
                       .Set("ExpBalls", c.ExpBalls)
                       .Set("MoneySave", c.MoneySave)
                       .Set("Hitpoints", e.Hitpoints)
                       .Set("LastDragonBallUse", c.LastDragonBallUse.Ticks)
                       .Set("Strength", e.Strength)
                       .Set("Agility", e.Agility)
                       .Set("Spirit", e.Spirit)
                       .Set("Vitality", e.Vitality)
                       .Set("PreviousX", e.PrevX)
                       .Set("PreviousY", e.PrevY)
                       .Set("PreviousMapID", e.PreviousMapID)
                       .Set("Atributes", e.Atributes)
                       .Set("Mana", e.Mana)
                       .Set("VIPLevel", e.VIPLevel)
                       .Set("MapID", e.MapID)
                       .Set("X", e.X)
                       .Set("Y", e.Y)
                       .Set("VirtuePoints", c.VirtuePoints)
                       .Set("EnlightenPoints", e.EnlightenPoints)
                       .Set("LastResetTime", c.LastResetTime.Ticks)
                       .Set("DoubleExpTime", e.DoubleExperienceTime)
                       .Set("DoubleExpToday", c.DoubleExpToday)
                       .Set("ExtraInventory", c.Entity.ExtraInventory)
                       .Set("HeavenBlessingTime", e.HeavenBlessing)
                       .Set("LotteryEntries", c.LotteryEntries)
                       .Set("BlessTime", c.BlessTime)
                       .Set("FirstRebornClass", e.FirstRebornClass)
                       .Set("SecondRebornClass", e.SecondRebornClass)
                       .Set("FirstRebornLevel", e.FirstRebornLevel)
                       .Set("SecondRebornLevel", e.SecondRebornLevel)
                       .Set("EnlightmentWait", e.EnlightmentTime)
                       .Set("SwordSoul", e.SwordSoul)
                       .Set("BansheeSpirit", e.BansheeSpirit)
                       .Set("CountryID", (ushort)e.CountryID)
                       .Set("Achievement", e.MyAchievement.ToString())
                       .Set("ClanId", (uint)e.ClanId)
                       .Set("ClanRank", (uint)e.ClanRank);
                    if (e.MapID == 601)
                        cmd.Set("OfflineTGEnterTime", c.OfflineTGEnterTime.Ticks);
                    else
                        cmd.Set("OfflineTGEnterTime", "0");
                    if (c.AsMember != null)
                    {
                        cmd.Set("GuildID", c.AsMember.GuildID)
                            .Set("CTFCpsReward", 0)
                        .Set("CTFSilverReward", 0)
                        .Set("Exploits", 0)
                        .Set("GuildRank", (ushort)c.AsMember.Rank)
                        .Set("CTFCpsReward", c.AsMember.CTFCpsReward)
                        .Set("CTFSilverReward", c.AsMember.CTFSilverReward)
                        .Set("GuildSilverDonation", c.AsMember.SilverDonation)
                        .Set("GuildConquerPointDonation", c.AsMember.ConquerPointDonation);
                    }
                    else
                    {
                        cmd.Set("GuildID", 0)
                        .Set("GuildRank", (ushort)0)
                        .Set("GuildSilverDonation", 0)
                        .Set("GuildConquerPointDonation", 0);
                    }
                    cmd.Where("UID", e.UID);
                    cmd.Execute();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
         
        }
        public static bool SaveEntity(Client.GameClient c)
        {
            using (var conn = DataHolder.MySqlConnection)
            {
                conn.Open();
                return SaveEntity(c, conn);
            }
        }
        static bool InvalidCharacters(string Name)
        {
            foreach (char c in Name)
            {
                if (Kernel.InvalidCharacters.Contains(c) || (byte)c < 48)
                {
                    return true;
                }
            }
            return false;
        }
        public static Counter EntityUID;
        public static bool CreateEntity(Network.GamePackets.EnitityCreate eC, Client.GameClient client,
            ref string message)
        {
            if (eC.Name.Length > 16)
                eC.Name = eC.Name.Substring(0, 16);
            if (eC.Name == "")
                return false;

            if (InvalidCharacters(eC.Name))
            {
                message = "Invalid characters inside the name.";
                return false;
            }
            using (
                var rdr =
                    new MySqlReader(new MySqlCommand(MySqlCommandType.SELECT).Select("entities").Where("name", eC.Name))
                )
            {
                if (rdr.Read())
                {
                    message = "The chosen name is already in use.";
                    return false;
                }
            }
            client.Entity = new Game.Entity(Game.EntityFlag.Player, false);
            client.Entity.Name = eC.Name;


            if (eC.Class == 1 || eC.Class == 0)
            {
                client.Entity.Class = 100;
                DataHolder.GetStats(100, 1, client);
            }
            if (eC.Class == 5 || eC.Class == 4)
            {
                client.Entity.Class = 40;
                DataHolder.GetStats(40, 1, client);
            }
            if (eC.Class == 12 || eC.Class == 13)
            {
                client.Entity.Class = 70;
                DataHolder.GetStats(70, 1, client);
            }
            if (eC.Class == 2 || eC.Class == 3)
            {
                client.Entity.Class = 10;
                DataHolder.GetStats(10, 1, client);
            }
            if (eC.Class == 14 || eC.Class == 15)
            {
                client.Entity.Class = 80;
                DataHolder.GetStats(80, 1, client);
            }
            if (eC.Class == 10 || eC.Class == 11)
            {
                client.Entity.Class = 60;
                DataHolder.GetStats(60, 1, client);
            }
            if (eC.Class == 8 || eC.Class == 9)
            {
                client.Entity.Class = 50;
                DataHolder.GetStats(50, 1, client);
            }
            if (eC.Class == 6 || eC.Class == 7)
            {
                client.Entity.Class = 20;
                DataHolder.GetStats(20, 1, client);
            }
            client.CalculateStatBonus();
            client.CalculateHPBonus();
            client.Entity.Hitpoints = client.Entity.MaxHitpoints;
            client.Entity.Mana = (ushort)(client.Entity.Spirit * 5);
            client.Entity.Body = eC.Body;
            if (eC.Body == 1003 || eC.Body == 1004)
                client.Entity.Face = (ushort)Kernel.Random.Next(1, 50);
            else
                client.Entity.Face = (ushort)Kernel.Random.Next(201, 250);
            byte Color = (byte)Kernel.Random.Next(4, 8);
            client.Entity.HairStyle = (ushort)(Color * 100 + 10 + (byte)Kernel.Random.Next(4, 9));
            client.Entity.UID = Database.EntityTable.EntityUID.Next;
            client.JustCreated = true;

            while (true)
            {
                using (
                    var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities")
                        .Where("uid", client.Entity.UID))
                using (var reader = cmd.CreateReader())
                {
                    if (reader.Read())
                        client.Entity.UID = Database.EntityTable.EntityUID.Next;
                    else
                        break;
                }
            }
            while (true)
            {
                try
                {
                    using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                        cmd.Insert("entities")
                            .Insert("Name", eC.Name)
                            .Insert("Owner", client.Account.Username)
                            .Insert("Class", client.Entity.Class)
                            .Insert("UID", client.Entity.UID)
                            .Insert("Hitpoints", client.Entity.Hitpoints)
                            .Insert("Mana", client.Entity.Mana)
                            .Insert("Body", client.Entity.Body)
                            .Insert("Face", client.Entity.Face)
                            .Insert("HairStyle", client.Entity.HairStyle)
                            .Insert("Strength", client.Entity.Strength)
                            .Insert("WarehousePW", "")
                            .Insert("Agility", client.Entity.Agility)
                            .Insert("Vitality", client.Entity.Vitality)
                            .Insert("Spirit", client.Entity.Spirit)
                            .Execute();

                    message = "ANSWER_OK";
                    break;
                }
                catch
                {
                    client.Entity.UID = EntityUID.Next;
                }
            }

            using (
                var cmd =
                    new MySqlCommand(MySqlCommandType.UPDATE).Update("configuration")
                        .Set("EntityID", client.Entity.UID)
                        .Where("ServerPort", 0, true))
                cmd.Execute();
            client.Account.EntityID = client.Entity.UID;
            client.Account.Save(client);
            return true;
        }

    }
}