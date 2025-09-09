using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Database;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Game.ConquerStructures.Society;
using Message = Nyx.Server.Network.GamePackets.Message;


namespace Nyx.Server.Game
{
    public class GuildWar
    {
        public static SobNpcSpawn Pole, RightGate, LeftGate;
        public static byte TopDlClaim = 0;
        public static byte TopGlClaim = 0;
        public static SobNpcSpawn Poles;

        public static SafeDictionary<uint, Guild> Scores = new SafeDictionary<uint, Guild>();

        public static bool IsWar = false, Flame10th = false;

        public static Time32 ScoreSendStamp = Time32.Now;

        public static Guild PoleKeeper, CurrentTopLeader;

        private static string[] scoreMessages;

        public static DateTime StartTime;

        public static bool Claim
        {
            get { return Program.Vars["gwclaim"]; }
            set { Program.Vars["gwclaim"] = value; }
        }

        public static uint KeeperID
        {
            get { return Program.Vars["gwkeeperid"]; }
            set { Program.Vars["gwkeeperid"] = value; }
        }

        public static void Initiate()
        {
            var Map = Kernel.Maps[1038];
            Pole = (SobNpcSpawn)Map.Npcs[810];
            LeftGate = (SobNpcSpawn)Map.Npcs[516074];
            RightGate = (SobNpcSpawn)Map.Npcs[516075];
        }

        

        public static void Start()
        {
            if (LeftGate == null) return;
            Scores = new SafeDictionary<uint, Guild>(100);
            StartTime = DateTime.Now;
            LeftGate.Mesh = (ushort)(240 + LeftGate.Mesh % 10);
            RightGate.Mesh = (ushort)(270 + LeftGate.Mesh % 10);
            Kernel.SendWorldMessage(new Message("Guild war has began!", System.Drawing.Color.Red, Message.Center), Program.Values);
            LeftGate.Hitpoints = LeftGate.MaxHitpoints;
            RightGate.Hitpoints = RightGate.MaxHitpoints;
            Pole.Hitpoints = Pole.MaxHitpoints;
            foreach (Guild guild in Kernel.Guilds.Values)
            {
                guild.WarScore = 0;
            }
            Update upd = new Update(true);
            upd.UID = LeftGate.UID;
            upd.Append(Update.Mesh, LeftGate.Mesh);
            upd.Append(Update.Hitpoints, LeftGate.Hitpoints);
            foreach (var client in Kernel.GamePool.Values)
            {
                if (client.Entity.MapID != 1038)
                    continue;
                client.Send(upd);
            }
            upd.Clear();
            upd.UID = RightGate.UID;
            upd.Append(Update.Mesh, RightGate.Mesh);
            upd.Append(Update.Hitpoints, RightGate.Hitpoints); 
            foreach (var client in Kernel.GamePool.Values)
            {
                if (client.Entity.MapID != 1038)
                    continue;
                client.Send(upd);
            }
            Claim = false;
            IsWar = true;
        }

        public static void Reset()
        {
            Scores = new SafeDictionary<uint, Guild>(100);

            LeftGate.Mesh = (ushort)(240 + LeftGate.Mesh % 10);
            RightGate.Mesh = (ushort)(270 + LeftGate.Mesh % 10);

            LeftGate.Hitpoints = LeftGate.MaxHitpoints;
            RightGate.Hitpoints = RightGate.MaxHitpoints;
            Pole.Hitpoints = Pole.MaxHitpoints;

            Update upd = new Update(true);
            upd.UID = LeftGate.UID;
            upd.Append(Update.Mesh, LeftGate.Mesh);
            upd.Append(Update.Hitpoints, LeftGate.Hitpoints);
            foreach (var client in Kernel.GamePool.Values)
            {
                if (client.Entity.MapID != 1038)
                    continue;
                client.Send(upd);
            }
            upd.Clear();
            upd.UID = RightGate.UID;
            upd.Append(Update.Mesh, RightGate.Mesh);
            upd.Append(Update.Hitpoints, RightGate.Hitpoints);
            foreach (var client in Kernel.GamePool.Values)
            {
                if (client.Entity.MapID != 1038)
                    continue;
                client.Send(upd);
            }

            foreach (Guild guild in Kernel.Guilds.Values)
            {
                guild.WarScore = 0;
            }

            IsWar = true;
        }

        public static void FinishRound()
        {
            if (PoleKeeper != null)
            {
                if (PoleKeeper.Wins == 0)
                    PoleKeeper.Losts++;
                else
                    PoleKeeper.Wins--;
                Database.GuildTable.UpdateGuildWarStats(PoleKeeper);
            }
            SortScores(out PoleKeeper);
            if (PoleKeeper != null)
            {
                KeeperID = PoleKeeper.ID;
                foreach (var client2 in Kernel.GamePool.Values)
                {
                    client2.Send(
                        new Message(
                            "" + PoleKeeper.Name + " wins!", System.Drawing.Color.WhiteSmoke, Message.Center));
                }
                if (PoleKeeper.Losts == 0)
                    PoleKeeper.Wins++;
                else
                    PoleKeeper.Losts--;
                Database.GuildTable.UpdateGuildWarStats(PoleKeeper);
                Pole.Name = PoleKeeper.Name;
            }
            Pole.Hitpoints = Pole.MaxHitpoints;
            foreach (var client in Kernel.GamePool.Values)
            {
                if (client.Entity.MapID != 1038)
                    continue;
                client.Send(Pole);
            }
            Reset();
        }

        public static void End()
        {
            if (PoleKeeper != null)
            {
                Kernel.SendWorldMessage(new Message("The guild, " + PoleKeeper.Name + ", owned by " + PoleKeeper.LeaderName + " has won this guild war!---Guild war has ended!", System.Drawing.Color.White, Message.Center), Program.GamePool);
            }
            else
            {
                Kernel.SendWorldMessage(new Message("Guild war has ended and there was no winner!", System.Drawing.Color.Red, Message.Center), Program.GamePool);
            }
            IsWar = false;
            UpdatePole(Pole);
            foreach (var client in Program.GamePool)
            {
                client.Entity.RemoveFlag(Update.Flags.TopDeputyLeader);
                client.Entity.RemoveFlag(Update.Flags.TopGuildLeader);
                DateTime.Now.AddDays(7);
                
            }
            foreach (var pclient in Kernel.GamePool.Values)
            {
                try
                {
                    if (pclient.Guild.PoleKeeper)
                    {
                        if (pclient.AsMember.Rank == Enums.GuildMemberRank.GuildLeader)
                        {
                            pclient.Inventory.Add(720020, 0, 1); 
                            pclient.Entity.ConquerPoints += 10000;
                            pclient.Entity.AddTopStatus(Update.Flags.TopGuildLeader, 1, DateTime.Now.AddDays(4));
                            Kernel.SendWorldMessage(new Message("Congratulations! " + pclient.Entity.Name + "The winner guild has Claimed Guild War Prize 10000  CPs!", Message.Center), Program.GamePool);
                        }
                        if (pclient.AsMember.Rank == Enums.GuildMemberRank.DeputyLeader)
                        {
                            pclient.Entity.AddTopStatus(Update.Flags.TopDeputyLeader, 1, DateTime.Now.AddDays(4));
                        }
                    }
                }
                catch
                {

                }
            }
        }

        public static void AddScore(uint addScore, Guild guild)
        {
            if (guild != null)
            {
                guild.WarScore += addScore;
                if (!Scores.ContainsKey(guild.ID))
                    Scores.Add(guild.ID, guild);
                if ((int)Pole.Hitpoints <= 0)
                {
                    FinishRound();
                    return;
                }
            }
        }

        public static void SendScores()
        {
            if (scoreMessages == null)
                scoreMessages = new string[0];
            if (Scores.Count == 0)
                return;
                SortScores(out CurrentTopLeader);

            for (int c = 0; c < scoreMessages.Length; c++)
            {
                Message msg = new Message(scoreMessages[c], System.Drawing.Color.Red,
                    c == 0 ? Message.FirstRightCorner : Message.ContinueRightCorner);
                foreach (var client2 in Kernel.GamePool.Values)
                {
                    if (client2.Entity.MapID != 1038 && client2.Entity.MapID != 6001)
                        continue;
                    client2.Send(msg);
                }
            }
        }

        private static void SortScores(out Guild winner)
        {
            winner = null;
            List<string> ret = new List<string>();

            int Place = 0;
            foreach (Guild guild in Scores.Values.OrderByDescending((p) => p.WarScore))
            {
                if (Place == 0)
                    winner = guild;
                string str = "No." + (Place + 1).ToString() + ": " + guild.Name + "(" + guild.WarScore + ")";
                ret.Add(str);
                Place++;
                if (Place == 4)
                    break;
            }
            scoreMessages = ret.ToArray();
        }

        private static void UpdatePole(SobNpcSpawn pole)
        {
            new Database.MySqlCommand(Nyx.Server.Database.MySqlCommandType.UPDATE)
                .Update("sobnpcs").Set("name", pole.Name).Set("life", Pole.Hitpoints).Where("id", pole.UID).Execute();
        }
    }
}