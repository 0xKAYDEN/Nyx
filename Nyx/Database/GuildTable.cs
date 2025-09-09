using System;
using System.Collections.Generic;
using System.Linq;

namespace Nyx.Server.Database
{
    using Member = Game.ConquerStructures.Society.Guild.Member;
    using Nyx.Server.Game.ConquerStructures.Society;
    using Albetros.Core;
    using Nyx.Server.Network.GamePackets;

    public class GuildTable
    {
        public static void UpdateUnionID(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("UnionID", guild.UnionID)
                .Where("ID", guild.ID))
                cmd.Execute();
        }
        public static void Load()
        {
            Dictionary<uint, SafeDictionary<uint, Member>> dict = new Dictionary<uint, SafeDictionary<uint, Member>>();
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities").Where("guildid", 0, true))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    Member member = new Member(reader.ReadUInt16("guildid"));
                    member.ID = reader.ReadUInt32("uid");
                    member.Name = reader.ReadString("name");
                    member.Level = reader.ReadByte("level");
                    member.Class = reader.ReadByte("Class");
                    member.LastLogin = reader.ReadUInt32("GuildLastLogin");
                    member.Arsenal_Donation = reader.ReadUInt32("GuildArsenalDonation");
                    member.PKDoantion = reader.ReadUInt16("PKPoints");
                    if (Game.ConquerStructures.Nobility.Board.ContainsKey(member.ID))
                    {
                        member.NobilityRank = Game.ConquerStructures.Nobility.Board[member.ID].Rank;
                        member.Gender = Game.ConquerStructures.Nobility.Board[member.ID].Gender;
                    }
                    
                    
                    member.Rank = (Game.Enums.GuildMemberRank)reader.ReadUInt16("guildrank");
                    member.SilverDonation = reader.ReadUInt64("GuildSilverDonation");
                    member.ConquerPointDonation = reader.ReadUInt64("GuildConquerPointDonation");
                    if (!dict.ContainsKey(member.GuildID)) dict.Add(member.GuildID, new SafeDictionary<uint, Member>());
                    dict[member.GuildID].Add(member.ID, member);


                }
            }
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("guilds"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    Guild guild = new Guild(reader.ReadString("LeaderName"));
                    guild.ID = reader.ReadUInt32("Id");
                    guild.Name = reader.ReadString("Name");
                    guild.Wins = reader.ReadUInt32("Wins");
                    guild.CalculateCTFRank(false);
                    guild.Points = reader.ReadUInt32("CTFPoints");
                    guild.CTFReward = reader.ReadUInt32("CTFReward");
                    guild.UnionID = reader.ReadUInt32("UnionID");
                    guild.CTFdonationCPs = reader.ReadUInt32("CTFdonationCPs");
                    guild.CTFdonationSilver = reader.ReadUInt32("CTFdonationSilver");
                    guild.CTFdonationSilverold = reader.ReadUInt32("CTFdonationSilverold");
                    guild.CTFdonationCPsold = reader.ReadUInt32("CTFdonationCPsold");
                    guild.Losts = reader.ReadUInt32("Losts");
                    guild.Bulletin = reader.ReadString("Bulletin");
                    guild.SilverFund = reader.ReadUInt64("SilverFund");
                    guild.CTFPoints = reader.ReadUInt32("CTFPoints");
                    guild.CTFReward = reader.ReadUInt32("CTFReward");
                    guild.ConquerPointFund = reader.ReadUInt32("ConquerPointFund");
                    guild.LevelRequirement = reader.ReadUInt32("LevelRequirement");
                    guild.RebornRequirement = reader.ReadUInt32("RebornRequirement");
                    guild.ClassRequirement = reader.ReadUInt32("ClassRequirement");
                    guild.EnrollmentDate = reader.ReadUInt32("EnrollmentDate");
                    guild.AnnouncementDate = reader.ReadUInt32("AnnouncementDate");
                    if (reader.ReadUInt32("PoleKeeper") == 1)
                    {
                        Game.GuildWar.PoleKeeper = guild;
                    }
                   
                    if (dict.ContainsKey(guild.ID))
                    {
                        guild.Members = dict[guild.ID];
                        guild.MemberCount = (uint)guild.Members.Count;
                    }
                    else
                        guild.Members = new SafeDictionary<uint, Member>();
                    Kernel.Guilds.Add(guild.ID, guild);
                    foreach (var member in guild.Members.Values)
                    {
                        if (member.Rank == Game.Enums.GuildMemberRank.GuildLeader)
                            guild.Leader = member;
                        else if (member.Rank == Game.Enums.GuildMemberRank.DeputyLeader)
                            guild.DeputyLeaderCount++;
                        if (member.Rank == Game.Enums.GuildMemberRank.DeputyLeader)
                            guild.RanksCounts[990]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.HDeputyLeader)
                            guild.RanksCounts[980]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.HonoraryManager)
                            guild.RanksCounts[880]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.HonorarySuperv)
                            guild.RanksCounts[840]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.HonorarySteward)
                            guild.RanksCounts[680]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.Aide)
                            guild.RanksCounts[602]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.Steward)
                            guild.RanksCounts[690]++;
                         if (member.Rank == Game.Enums.GuildMemberRank.Follower)
                            guild.RanksCounts[490]++;
                        guild.ListMember.Add(member);
                    }
                    GuildArsenalTable.Load(guild);
                    #region Sort Guild Doantion
                    int ranks = 0;
                    foreach (var g in guild.ListMember)
                    {

                        g.SilversList = (from entry in guild.ListMember
                                         where entry.SilverDonation > 0
                                         orderby entry.SilverDonation descending
                                         select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = ranks++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss = 0;
                    foreach (var g in guild.Members.Values)
                    {

                        g.TotalDonationsList = (from entry in guild.ListMember
                                                where entry.SilverDonation > 0
                                                orderby entry.SilverDonation descending
                                                select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss2 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.CPSList = (from entry in guild.ListMember
                                     orderby entry.ConquerPointDonation descending
                                     select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss2++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss3 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.PKSList = (from entry in guild.ListMember
                                     orderby entry.PKDoantion descending
                                     select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss3++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss4 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.GuidesList = (from entry in guild.ListMember
                                        orderby entry.GuideDoantion descending
                                        select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss4++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss5 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.ArsenalsList = (from entry in guild.ListMember
                                          orderby entry.Arsenal_Donation descending
                                          select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss5++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss6 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.RosesList = (from entry in guild.ListMember
                                       orderby entry.RoseDonation descending
                                       select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss6++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss7 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.OrchidsList = (from entry in guild.ListMember
                                         orderby entry.OrchidesDonation descending
                                         select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss7++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss8 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.LiliesList = (from entry in guild.ListMember
                                        orderby entry.LiliesDonation descending
                                        select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss8++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    int rankss9 = 0;
                    foreach (var g in guild.ListMember)
                    {
                        g.TulipsList = (from entry in guild.ListMember
                                        orderby entry.TulipDonation descending
                                        select new Game.ConquerStructures.Society.RankingList() { EntityID = entry.ID, Name = entry.Name, rank = rankss9++, SilverDonation = entry.SilverDonation, arsenaldoantion = entry.Arsenal_Donation, pkdoantion = entry.PKDoantion, cpdoantion = entry.ConquerPointDonation, guidedoantion = entry.GuideDoantion, postion = entry.Rank, totaldoantion = entry.TotalDoantion, rosedoantion = entry.RoseDonation, liliesdoantion = entry.LiliesDonation, orchidesdoantion = entry.OrchidesDonation, tuplisedoantion = entry.TulipDonation })
                                             .ToList();
                    }
                    #endregion
                }
            }

            LoadAllyEnemy();
            
        }
        public static void LoadAllyEnemy()
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("guildenemy"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    ushort guildID = reader.ReadUInt16("guildid");
                    ushort enemyID = reader.ReadUInt16("enemyid");
                    if (Kernel.Guilds.ContainsKey(guildID))
                        if (Kernel.Guilds.ContainsKey(enemyID))
                            Kernel.Guilds[guildID].Enemy.Add(enemyID, Kernel.Guilds[enemyID]);
                }
            }
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("guildally"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    ushort guildID = reader.ReadUInt16("guildid");
                    ushort allyID = reader.ReadUInt16("allyid");
                    if (Kernel.Guilds.ContainsKey(guildID))
                        if (Kernel.Guilds.ContainsKey(allyID))
                            Kernel.Guilds[guildID].Ally.Add(allyID, Kernel.Guilds[allyID]);
                }
            }
        }

        public static void UpdateBulletin(Guild guild, string bulletin)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("Bulletin", bulletin)
                .Set("AnnouncementDate", Common.TimeGet(TimeType.Day))
                .Where("ID", guild.ID))
                cmd.Execute();
        }
        public static void SaveFunds(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE)
                .Update("guilds")
                .Set("ConquerPointFund", guild.ConquerPointFund)
                .Set("SilverFund", guild.SilverFund)
                .Set("CTFdonationCPsold", guild.CTFdonationCPsold)
                .Set("CTFdonationSilverold", guild.CTFdonationSilverold)
                .Set("CTFdonationCPs", guild.CTFdonationCPs)
                .Set("CTFdonationSilver", guild.CTFdonationSilver)
                .Where("ID", guild.ID))
                cmd.Execute();
        }
        public static void SaveCTFPoins(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("CTFPoints", guild.CTFPoints)
                .Where("ID", guild.ID))
                cmd.Execute();
        }
        public static void SaveCTFReward(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("CTFReward", guild.CTFReward)
                .Where("ID", guild.ID))
                cmd.Execute();
        }
        public static void Disband(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("entities")
                .Set("guildid", 0)
                .Where("guildid", guild.ID))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.DELETE).Delete("guilds", "id", guild.ID))
                cmd.Execute();
        }
        public static void Create(Guild guild)
        {
            while (true)
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("guilds").Where("id", guild.ID))
                using (var reader = cmd.CreateReader())
                {
                    if (reader.Read())
                        guild.ID = Guild.GuildCounter.Next;
                    else
                        break;
                }
            }
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("guilds")
                .Insert("ID", guild.ID).Insert("name", guild.Name).Insert("Bulletin", "")
                .Insert("SilverFund", 500000).Insert("EnrollmentDate", Common.TimeGet(TimeType.Day)).Insert("LeaderName", guild.LeaderName))
                cmd.Execute();
        }
        public static void ChangeName(Client.GameClient client, string name)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("name", name).Where("ID", client.Guild.ID))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("guilds"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    client.Guild.Name = reader.ReadString("Name");
                }
            }
            Message message = null;
            message = new Message("Congratulations, " + client.Entity.Name + " has change guild name to " + name + " Succesfully!", System.Drawing.Color.White, Message.World);
            foreach (Client.GameClient clients in Kernel.GamePool.Values)
            {
                clients.Send(message);
            }
        }
        public static void SaveLeader(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE)
              .Update("guilds")
              .Set("LeaderName", guild.LeaderName)
              .Where("id", guild.ID))
                cmd.Execute();
        }
        public static void AddEnemy(Guild guild, uint enemy)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("guildenemy")
                .Insert("guildID", guild.ID).Insert("enemyID", enemy))
                cmd.Execute();
        }
        public static void AddAlly(Guild guild, uint ally)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("guildally")
                .Insert("guildID", guild.ID).Insert("allyID", ally))
                cmd.Execute();
        }
        public static void RemoveEnemy(Game.ConquerStructures.Society.Guild guild, uint enemy)
        {
            using (var command = new MySqlCommand(MySqlCommandType.DELETE))
                command.Delete("guildenemy", "GuildID", guild.ID).And("EnemyID", enemy)
                    .Execute();
        }
        public static void RemoveAlly(Game.ConquerStructures.Society.Guild guild, uint ally)
        {
            using (var command = new MySqlCommand(MySqlCommandType.DELETE))
                command.Delete("guildally", "GuildID", guild.ID).And("AllyID", ally)
                    .Execute();
            using (var command = new MySqlCommand(MySqlCommandType.DELETE))
                command.Delete("guildally", "GuildID", ally).And("AllyID", guild.ID)
                    .Execute();
        }
        public static void UpdatePoleKeeper(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeper", 0))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeper", 1).Where("id", guild.ID))
                cmd.Execute();
        }
        public static void UpdatePoleKeeperTc(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperTc", 0))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperTc", 1).Where("id", guild.ID))
                cmd.Execute();
        }
        public static void UpdatePoleKeeperPh(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperPh", 0))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperPh", 1).Where("id", guild.ID))
                cmd.Execute();
        }
        public static void UpdatePoleKeeperAp(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperAp", 0))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperAp", 1).Where("id", guild.ID))
                cmd.Execute();
        }
        public static void UpdatePoleKeeperBi(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperBi", 0))
                cmd.Execute();
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("PoleKeeperBi", 1).Where("id", guild.ID))
                cmd.Execute();
        }
        public static void UpdateGuildWarStats(Guild guild)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("guilds")
                .Set("wins", guild.Wins).Set("losts", guild.Losts)
                .Where("id", guild.ID))
                cmd.Execute();
        }

        internal static void SaveRequirements(Guild guild)
        {
            using (var command = new MySqlCommand(MySqlCommandType.UPDATE))
                command.Update("guilds").Set("LevelRequirement", guild.LevelRequirement)
                    .Set("RebornRequirement", guild.RebornRequirement).Set("ClassRequirement", guild.ClassRequirement)
                    .Where("ID", guild.ID).Execute();
        }
    }
}
