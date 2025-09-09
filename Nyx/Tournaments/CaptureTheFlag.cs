using System;
using System.Linq;
using System.Text;
using System.Data;
using Nyx.Server.Client;
using Nyx.Server.Network;
using Nyx.Server.Database;
using MySql.Data.MySqlClient;
using Nyx.Server.Game.Attacking;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;
using System.Collections.Concurrent;
using Nyx.Server.Game.ConquerStructures.Society;
using Message = Nyx.Server.Network.GamePackets.Message;


namespace Nyx.Server.Game
{
    public class CaptureTheFlag
    {
        public class Base
        {
            public SobNpcSpawn Flag;
            public ConcurrentDictionary<uint, uint> Scores;
            public uint CapturerID;
            public Base(SobNpcSpawn flag)
            {
                Flag = flag;
                Scores = new ConcurrentDictionary<uint, uint>();
                CapturerID = 0;
            }
            public void Capture()
            {
                if (Scores.Count == 0) Scores.Add((uint)0, (uint)0);
                uint guildId = Scores.OrderByDescending(p => p.Value).First().Key;
                CapturerID = guildId;
                var guild = Kernel.Guilds[guildId];
                Flag.Name = guild.Name;
                Flag.Hitpoints = Flag.MaxHitpoints;
                Kernel.SendScreen(Flag, Flag);
                Scores = new ConcurrentDictionary<uint, uint>();
            }
            public bool IsX2;
        }
        public static MySqlConnection conn = new MySqlConnection();
        public const ushort MapID = 2057;
        private Map Map;
        public Dictionary<uint, Base> Bases;
        public static bool IsWar;
        public static DateTime StartTime;
        public CaptureTheFlag()
        {
            Bases = new Dictionary<uint, Base>();
            if (!Kernel.Maps.ContainsKey((int)2057L))
            {
                new Map(MapID, DMaps.MapPaths[MapID]);
            }
            Map = Kernel.Maps[MapID];
            foreach (var npc in Map.Npcs.Values)
                if (npc is SobNpcSpawn)
                    Bases.Add(npc.UID, new Base((SobNpcSpawn)npc));
            SpawnFlags();
        }
        public void SpawnFlags()
        {
            int toAdd = 40 - Map.StaticEntities.Count;
            for (int i = toAdd; i > 0; i--)
            {
                var coords = Map.RandomCoordinates();
                StaticEntity entity = new StaticEntity((uint)(coords.Item1 * 1000 + coords.Item2), coords.Item1, coords.Item2, MapID);
                entity.DoFlag();
                Map.AddStaticEntity(entity);
            }
        }
        public void GenerateX2Castle()
        {
            if (DateTime.Now >= X2LocationStamp)
            {
                int X2 = Kernel.Random.Next(1, Bases.Count);
                var OldX2 = Bases.Values.ToArray()[X2Castle];
                OldX2.IsX2 = false;
                X2Castle = (int)(Bases.Values.ToArray()[X2].Flag.UID);
                var NewX2 = Bases.Values.ToArray()[X2];
                NewX2.IsX2 = true;
                foreach (var user in Kernel.GamePool.Values)
                {
                    if (user.Entity.MapID == MapID)
                    {
                        SendX2Location(user, NewX2);
                        user.Send(new Message("Bonus Effect fell on No. " + (NewX2.Flag.UID - 101990).ToString() + " Flag Base.", System.Drawing.Color.Red, Message.System));
                    }
                }
                X2LocationStamp = DateTime.Now.AddMinutes(15);
            }
        }
        public void SendX2Location(GameClient user, Base X2)
        {
            byte[] stream = X2LocationPacket((uint)X2Castle, X2.Flag.X, X2.Flag.Y);
            user.Send(stream);
        }
        public int X2Castle;
        public DateTime X2LocationStamp = DateTime.Now;
        public byte[] X2LocationPacket(uint X2Castle, ushort x, ushort y)
        {
            byte[] data = new byte[72];
            Writer.Write((ushort)(data.Length - 8), 0, data);
            Writer.Write(2224, 2, data);
            Writer.Write(12, 4, data);
            Writer.Write(X2Castle, 8, data);
            Writer.Write(1, 28, data);
            Writer.Write(x, 32, data);
            Writer.Write(y, 34, data);
            return data;
        }
        public void BaseOwned(GameClient client)
        {
            byte[] Packet = new byte[68];
            Writer.Write(60, 0, Packet);
            Writer.Write(2224, 2, Packet);
            Writer.Write(7, 4, Packet);
            Writer.Write(client.Entity.UID, 8, Packet);
            client.SendScreen(Packet, true);
        }
        public bool SignUp(GameClient client)
        {
            if (client.Entity.GuildID == 0) return false;
            if (client.Guild == null) return false;
            client.Entity.PKMode = Enums.PKMode.Team;
            client.Send(new Data(true)
            {
                UID = client.Entity.UID,
                ID = Data.ChangePKMode,
                dwParam = (uint)client.Entity.PKMode
            });
            var coords = Map.RandomCoordinates(482, 367, 27);
            client.Entity.Teleport(MapID, coords.Item1, coords.Item2);
            Data data = new Data(true);
            data.UID = client.Entity.UID;
            data.ID = 104;
            data.dwParam = uint.MaxValue;
            data.Data24_Uint = 7733744;
            client.Send(data);
            SendX2Location(client, Bases.Values.Where(i => i.IsX2 == true).FirstOrDefault());
            client.Send(new Message("You entered the Capture the flag arena. Get ready for the challenge!", System.Drawing.Color.Red, Message.System));
            return true;
        }
        public static void ClearHistory()
        {
            using (conn = DataHolder.MySqlConnection)
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                new MySql.Data.MySqlClient.MySqlCommand("UPDATE entities set Exploits='0' Where Exploits >'0';", conn).ExecuteScalar();
                conn.Close();
            }
        }
        public void AroundBase(GameClient client)
        {
            if (client.Entity.MapID != MapID) return;
            if (client.Entity.GuildID == 0) return;
            if (client.Guild == null) return;
            foreach (var _base in Bases.Values)
            {
                if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, _base.Flag.X, _base.Flag.Y) <= 9)
                {
                    if (_base.CapturerID == client.Entity.GuildID)
                    {
                        if (client.Entity.ContainsFlag2((ulong)Update.Flags2.CarryingFlag))
                        {
                            client.Send(generateTimer(0));
                            client.Send(generateEffect(client));
                            client.Entity.RemoveFlag2((ulong)Update.Flags2.CarryingFlag);
                            if (_base.IsX2)
                                AddScore(30, client.AsMember.Guild);
                            else AddScore(15, client.AsMember.Guild);
                            AddExploits(2, client.AsMember);
                            SendScores();
                            ShowMemberExploits(client);
                            Program.World.CTF.SendUpdates();
                            Sector sector = new Sector(client.Entity.X, client.Entity.Y, 326, 446);
                            sector.Arrange(15, 10);
                            BaseOwned(client);
                        }
                    }
                }
            }
        }
        public static bool Attackable(Game.Entity entity)
        {
            return Kernel.GetDistance(entity.X, entity.Y, 482, 367) > 32;
        }
        public void AddScore(uint damage, Guild guild, SobNpcSpawn attacked)
        {
            if (Bases.ContainsKey(attacked.UID))
            {
                {
                    var _base = Program.World.CTF.Bases[attacked.UID];
                    if (!_base.Scores.ContainsKey(guild.ID))
                        _base.Scores.Add(guild.ID, damage);
                    else
                        _base.Scores[guild.ID] += damage;
                }
            }
        }
        public void FlagOwned(SobNpcSpawn attacked)
        {
            if (Bases.ContainsKey(attacked.UID))
            {
                foreach (var player in Kernel.GamePool.Values)
                {
                    if (player.Entity.MapID == MapID)
                    {
                        player.Send(generatePacket(5, (attacked.UID - 101990)));
                    }
                }
            }
        }
        public static void Close()
        {
            foreach (var player in Kernel.GamePool.Values)
            {
                if (player.Entity.MapID == MapID)
                    player.Entity.Teleport(1002, 300, 278);
            }
            var array = Kernel.Guilds.Values.Where(p => p.Points != 0).OrderByDescending(p => p.Points).ToArray();
            for (int i = 0; i < Math.Min(8, array.Length); i++)
            {
                array[i].CalculateCTFRank(true);
                GuildTable.SaveCTFPoins(array[i]);
                if (array[i].CTFReward == 1)
                {
                    foreach (var player in Kernel.GamePool.Values)
                    {
                        new TitleStorage().AddTitle(player, 2002, 3, false);
                        new TitleStorage().AddTitle(client, 2003, 4, false);
                        new TitleStorage().AddTitle(client, 6002, 21, false);
                        player.Entity.GLCTF = 1;
                        player.AsMember.Rank = Enums.GuildMemberRank.GuildLeader;
                        player.Inventory.Add(187405, 0, 1);
                    }
                    array[i].ConquerPointFund += 3000;
                    array[i].SilverFund += 120000000;
                }
                else if (array[i].CTFReward == 2)
                {
                    foreach (var player in Kernel.GamePool.Values)
                    {
                        player.AsMember.Rank = Enums.GuildMemberRank.GuildLeader;
                        player.Inventory.Add(187415, 0, 1);
                    }
                    array[i].ConquerPointFund += 2000;
                    array[i].SilverFund += 100000000;
                }
                else if (array[i].CTFReward == 3)
                {
                    foreach (var player in Kernel.GamePool.Values)
                    {
                        player.AsMember.Rank = Enums.GuildMemberRank.GuildLeader;
                        player.Inventory.Add(187425, 0, 1);
                    }
                    array[i].ConquerPointFund += 1000;
                    array[i].SilverFund += 80000000;
                }
                else if (array[i].CTFReward == 7)
                {
                    array[i].ConquerPointFund += 600;
                    array[i].SilverFund += 65000000;
                }
                else if (array[i].CTFReward == 5)
                {
                    array[i].ConquerPointFund += 500;
                    array[i].SilverFund += 50000000;
                }
                else if (array[i].CTFReward == 6)
                {
                    array[i].ConquerPointFund += 400;
                    array[i].SilverFund += 40000000;
                }
                else if (array[i].CTFReward == 7)
                {
                    array[i].ConquerPointFund += 300;
                    array[i].SilverFund += 30000000;
                }
                else if (array[i].CTFReward == 8)
                {
                    array[i].ConquerPointFund += 200;
                    array[i].SilverFund += 20000000;
                }
                GuildTable.SaveCTFReward(array[i]);
                array[i].CTFdonationCPs = array[i].CTFdonationCPsold;
                array[i].CTFdonationSilver = array[i].CTFdonationSilverold;
                array[i].CTFdonationCPsold = 0;
                array[i].CTFdonationSilverold = 0;
            }
            if (array.Length > 8)
            {
                for (int k = 8; k < array.Length; k++)
                {
                    array[k].Points = 0;
                    GuildTable.SaveCTFPoins(array[k]);
                    foreach (Guild.Member current in array[k].Members.Values)
                    {
                        current.Exploits = 0;
                        current.ExploitsRank = 0;
                    }
                }
            }
        }
        public void SendUpdates(GameClient client)
        {
            if (Time32.Now > client.CTFUpdateStamp.AddSeconds(5))
            {
GenerateX2Castle();
                client.CTFUpdateStamp = Time32.Now;
                var buffer = generateCTFRanking4();
                client.Send(buffer);
                foreach (var _base in Bases.Values)
                {
                    if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, _base.Flag.X, _base.Flag.Y) <= 9)
                    {
                        buffer = generateFlagRanking(_base);
                        client.Send(buffer);
                    }
                }
            }
        }
        public byte[] generateCTFRanking4()
        {
            var array = Kernel.Guilds.Values.Where(p => p.Points != 0).OrderByDescending(p => p.Points).ToArray();
            return generateList4(2, array, p => p.Points);
        }
        public byte[] generateCTFRanking()
        {
            var array = Kernel.Guilds.Values.Where(p => p.Points != 0).OrderByDescending(p => p.Points).ToArray();
            return generateList(2, array, p => p.Points);
        }
        private byte[] generateFlagRanking(Base flag)
        {
            var scores = flag.Scores.OrderByDescending(p => p.Value).ToArray();
            var array = new Guild[Math.Min(5, scores.Length)];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Kernel.Guilds[scores[i].Key];
                array[i].CTFFlagScore = scores[i].Value;
            }
            return generateList(1, array, p => p.CTFFlagScore);
        }
        private byte[] generateList4(int type, Guild[] array = null, Func<Guild, UInt32> select = null)
        {
            byte[] data = null;
            if (array == null)
                data = new byte[48 + 4];
            else
                data = new byte[48 + 4 + (array.Length * 24)];
            Writer.Write(data.Length - 8, 0, data);
            Writer.Write(2224, 2, data);
            Writer.Write(type, 4 + 4, data);
            if (array != null)
            {
                Writer.Write(array.Length, 28 + 4, data);
                for (int i = 0; i < array.Length; i++)
                {
                    int offset = 32 + 4 + (i * 24);
                    Writer.Write(i, offset, data); offset += 4;
                    Writer.Write(select(array[i]), offset, data); offset += 4;
                    Writer.Write(array[i].Name, offset, data); offset += 16;
                }
            }
            return data;
        }
        public byte[] generatePacket2(int type, uint dwParam)
        {
            byte[] data = new byte[48];
            Writer.Write(data.Length - 8, 0, data);
            Writer.Write(2224, 2, data);
            Writer.Write(type, 4, data);
            Writer.Write(dwParam, 8, data);
            return data;
        }
        public void SendUpdates()
        {
            foreach (var player in Kernel.GamePool.Values)
                if (player.Entity.MapID == MapID)
                    SendUpdates(player);
        }
        public byte[] generateTimer(uint time)
        {
            return generatePacket(8, time);
        }
        private byte[] generateList(int type, Guild[] array = null, Func<Guild, UInt32> select = null)
        {
            byte[] data = null;
            if (array == null)
                data = new byte[48];
            else
                data = new byte[48 + (array.Length * 24)];
            Writer.Write(data.Length - 8, 0, data);
            Writer.Write(2224, 2, data);
            Writer.Write(type, 4, data);
            if (array != null)
            {
                Writer.Write(array.Length, 28, data);
                for (int i = 0; i < array.Length; i++)
                {
                    int offset = 32 + (i * 24);
                    Writer.Write(i, offset, data); offset += 4;
                    Writer.Write(select(array[i]), offset, data); offset += 4;
                    Writer.Write(array[i].Name, offset, data); offset += 16;
                }
            }
            return data;
        }
        public byte[] generateEffect(GameClient Client)
        {
            return generatePacket(6, Client.Entity.UID);
        }
        private byte[] generatePacket(int type, uint dwParam)
        {
            byte[] data = new byte[48];
            Writer.Write(data.Length - 8, 0, data);
            Writer.Write(2224, 2, data);
            Writer.Write(type, 4, data);
            Writer.Write(dwParam, 8, data);
            return data;
        }
        public static void ShowMemberExploits(GameClient client)
        {
            if (0 == 0)
            {
                byte[] buffer = new byte[212];
                Writer.Write(204, 0, buffer);
                Writer.Write(1063, 2, buffer);
                Writer.Write(13, 4, buffer);
                Writer.Write(1, 6, buffer);
                if (client.Guild != null)
                {
                    client.Guild = client.AsMember.Guild;
                    if (client.AsMember.Guild != null)
                    {
                        Writer.Write(client.AsMember.Exploits, 18, buffer);
                    }
                    else
                    {
                        Writer.Write(0, 18, buffer);
                    }
                }
                client.Send(buffer);
            }
        }
        public static void AddScore(uint addScore, Guild guild)
        {
            if ((guild != null) && IsWar)
            {
                guild.Points += addScore;
                if (!Guild.Scores.ContainsKey(guild.ID))
                {
                    Guild.Scores.Add(guild.ID, guild);
                }
                GuildTable.SaveCTFPoins(guild);
            }
        }
        public static void SendScores()
        {
            if ((Guild.Scores.Count != 0) && IsWar)
            {
                SortScores(out GuildWar.CurrentTopLeader);
            }
        }
        public static void SortScores(out Guild winner)
        {
            if (1 != 0)
            {
                winner = null;
                List<string> list = new List<string>();
                SortedDictionary<uint, SortEntry<uint, Guild>> source = new SortedDictionary<uint, SortEntry<uint, Guild>>();
                foreach (Guild guild in Guild.Scores.Values)
                {
                    if (Kernel.Guilds.ContainsKey(guild.ID))
                    {
                        if (source.ContainsKey(guild.Points))
                        {
                            source[guild.Points].Values.Add(guild.ID, guild);
                        }
                        else
                        {
                            source.Add(guild.Points, new SortEntry<uint, Guild>());
                            source[guild.Points].Values = new Dictionary<uint, Guild>();
                            source[guild.Points].Values.Add(guild.ID, guild);
                        }
                    }
                }
                byte[] buffer = new byte[908];
                Writer.Write(900, 0, buffer);
                Writer.Write(2224, 2, buffer);
                Writer.Write(2, 4, buffer);
                Writer.Write(0, 12, buffer);
                Writer.Write((uint)Guild.Scores.Count, 28, buffer);
                int offset = 36;
                int num2 = 0;
                foreach (KeyValuePair<uint, SortEntry<uint, Guild>> pair in source.Reverse<KeyValuePair<uint, SortEntry<uint, Guild>>>())
                {
                    foreach (Guild guild in pair.Value.Values.Values)
                    {
                        Writer.Write(guild.Points, offset, buffer);
                        offset += 4;
                        Writer.Write(guild.Name, offset, buffer);
                        offset += 16;
                        Writer.Write((uint)(num2 + 1), offset, buffer);
                        offset += 4;
                        num2++;
                        if (num2 == 3)
                        {
                            break;
                        }
                    }
                    if (num2 == 3)
                    {
                        break;
                    }
                    num2 = 0;
                    foreach (KeyValuePair<uint, SortEntry<uint, Guild>> pair2 in source.Reverse<KeyValuePair<uint, SortEntry<uint, Guild>>>())
                    {
                        foreach (Guild guild in pair2.Value.Values.Values)
                        {
                            guild.CTFReward = (uint)(num2 + 1);
                            num2++;
                        }
                    }
                }
                foreach (GameClient state in Kernel.GamePool.Values)
                {
                    if (state.Entity.MapID == 2057)
                    {
                        state.SendScreen(buffer, true);
                    }
                }
            }
        }
        public static void AddExploits(uint addScore, Guild.Member guild)
        {
            if (((1 != 0) && (guild != null)) && IsWar)
            {
                guild.Exploits += addScore;
                using (var command = new Nyx.Server.Database.MySqlCommand(MySqlCommandType.UPDATE))
                    command.Update("entities").Set("Exploits", guild.Exploits).Where("uid", guild.ID).Execute();
                if (!guild.Guild.Members.ContainsKey(guild.ID))
                {
                    guild.Guild.Members.Add(guild.ID, guild);
                }
            }
        }
        public void CloseList(GameClient client)
        {
            client.Send(generateList(3));
        }
        public static void CTFGuildingScore(GameClient client)
        {
            byte[] buffer = new byte[908];
            Writer.Write(900, 0, buffer);
            Writer.Write(1063, 2, buffer);
            Writer.Write(9, 4, buffer);
            Writer.Write(1, 6, buffer);
            Writer.Write(0, 10, buffer);
            Writer.Write(8, 14, buffer);
            if (client.Guild != null)
            {
                client.Guild = client.AsMember.Guild;
                List<string> list = new List<string>();
                SortedDictionary<uint, SortEntry<uint, Guild>> source = new SortedDictionary<uint, SortEntry<uint, Guild>>();
                foreach (Guild guild in Guild.GuildWinners.Values)
                {
                    if (Kernel.Guilds.ContainsKey(guild.ID))
                    {
                        if (source.ContainsKey(guild.Points))
                        {
                            source[guild.Points].Values.Add(guild.ID, guild);
                        }
                        else
                        {
                            source.Add(guild.Points, new SortEntry<uint, Guild>());
                            source[guild.Points].Values = new Dictionary<uint, Guild>();
                            source[guild.Points].Values.Add(guild.ID, guild);
                        }
                    }
                }
                int offset = 30;
                uint num2 = 1;
                foreach (KeyValuePair<uint, SortEntry<uint, Guild>> pair in source.Reverse<KeyValuePair<uint, SortEntry<uint, Guild>>>())
                {
                    foreach (Guild guild in pair.Value.Values.Values)
                    {
                        Writer.Write(guild.Name, offset, buffer);
                        offset += 16;
                        Writer.Write(guild.Points, offset, buffer);
                        offset += 4;
                        Writer.Write(guild.MemberCount, offset, buffer);
                        offset += 4;
                        Writer.Write((ulong)guild.CTFdonationSilver, offset, buffer);
                        offset += 8;
                        Writer.Write(guild.CTFdonationCPs, offset, buffer);
                        offset += 4;
                        num2++;
                        if (num2 == 9)
                        {
                            break;
                        }
                    }
                }
            }
            client.Send(buffer);
        }
        public static void SortScoresJoining(GameClient client, out Guild winner)
        {
            if (1 != 0)
            {
                winner = null;
                List<string> list = new List<string>();
                SortedDictionary<uint, SortEntry<uint, Guild>> source = new SortedDictionary<uint, SortEntry<uint, Guild>>();
                foreach (Guild guild in Guild.Scores.Values)
                {
                    if (Kernel.Guilds.ContainsKey(guild.ID))
                    {
                        if (source.ContainsKey(guild.Points))
                        {
                            source[guild.Points].Values.Add(guild.ID, guild);
                        }
                        else
                        {
                            source.Add(guild.Points, new SortEntry<uint, Guild>());
                            source[guild.Points].Values = new Dictionary<uint, Guild>();
                            source[guild.Points].Values.Add(guild.ID, guild);
                        }
                    }
                }
                byte[] buffer = new byte[908];
                Writer.Write(900, 0, buffer);
                Writer.Write(2224, 2, buffer);
                Writer.Write(0, 4, buffer);
                client.Guild = client.AsMember.Guild;
                if (client.AsMember.Guild != null)
                {
                    Writer.Write(client.AsMember.Guild.ID, 12, buffer);
                }
                else
                {
                    Writer.Write(0, 12, buffer);
                }
                Writer.Write((uint)Guild.Scores.Count, 28, buffer);
                int offset = 36;
                int num2 = 0;
                foreach (KeyValuePair<uint, SortEntry<uint, Guild>> pair in source.Reverse<KeyValuePair<uint, SortEntry<uint, Guild>>>())
                {
                    foreach (Guild guild in pair.Value.Values.Values)
                    {
                        Writer.Write(guild.Points, offset, buffer);
                        offset += 4;
                        Writer.Write(guild.Name, offset, buffer);
                        offset += 16;
                        Writer.Write((uint)(num2 + 1), offset, buffer);
                        offset += 4;
                        num2++;
                    }
                    if (num2 == 8)
                    {
                        break;
                    }
                }
                foreach (GameClient state in Kernel.GamePool.Values)
                {
                    if (state.Entity.MapID == 2057)
                    {
                        state.SendScreen(buffer, true);
                    }
                }
            }
        }
        public static void Process(byte[] packet, GameClient client)
        {
            if (packet[4] == 7)
                client.Send(Program.World.CTF.generateCTFRanking());
            if (CaptureTheFlag.IsWar)
            {
                byte[] buffer = new byte[212];
                Writer.Write(204, 0, buffer);
                Writer.Write(2224, 2, buffer);
                Writer.Write(11, 4, buffer);
                Writer.Write(1, 8, buffer);
                CaptureTheFlag.CTFGuildingScore(client);
                client.Send(buffer);
            }
            else
            {
                byte[] buffer = new byte[212];
                Writer.Write(204, 0, buffer);
                Writer.Write(2224, 2, buffer);
                Writer.Write(11, 4, buffer);
                Writer.Write(0, 8, buffer);
                new CaptureTheFlagRank(client).Send(client);
                client.Send(packet);
            }
        }

        public static unsafe GameClient client { get; set; }
    }
    public unsafe class CaptureTheFlagRank
    {
        private byte[] Packet;
        public CaptureTheFlagRank(Client.GameClient client)
        {
            Packet = new byte[354];
            Writer.Write(346, 0, Packet);
            Writer.Write(1063, 2, Packet);
            Packet[4] = 9;
            Packet[6] = 1;
            Packet[14] = 8;
            if (client.Guild != null)
                Writer.Write(client.Guild.Points, 18, Packet);
        }
        public byte[] Encode()
        {
            ushort Offset = 30;
            var array = Kernel.Guilds.Values.Where(p => p.Points != 0).OrderByDescending(p => p.Points).ToArray();
            if (array != null)
            {
                for (byte i = 0; i < array.Length; i++)
                {
                    if (i == 9) break;
                    Writer.Write(array[i].Name, Offset, Packet);
                    Offset += 16;
                    Writer.Write(array[i].Points, Offset, Packet);
                    Offset += 4;
                    Writer.Write(array[i].MemberCount, Offset, Packet);
                    Offset += 4;
                    Writer.Write(array[i].CTFdonationSilverold, Offset, Packet);
                    Offset += 8;
                    Writer.Write(array[i].CTFdonationCPsold, Offset, Packet);
                    Offset += 4;
                }
            }
            return Packet;
        }
        public void Send(GameClient client)
        {
            client.Send(Encode());
        }
    }
}