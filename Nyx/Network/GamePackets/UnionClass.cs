using Nyx.Server;
using Nyx.Server.Client;
using Nyx.Server.Database;
using Nyx.Server.Network.GamePackets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nyx.Server.Network;

namespace Nyx.Server.Network.GamePackets.Union
{
    public class UnionClass : Nyx.Server.Network.Writer
    {
        public class Member
        {
            public Member(uint UnionID)
            {
                this.UnionID = UnionID;
            }
            public uint Offical = 0;
            public uint Harem = 0;
            public uint Guards = 0;
            public uint UnionExploits = 0;
            public uint ID;
            public string Name;
            public bool IsOnline
            {
                get
                {
                    return Kernel.GamePool.ContainsKey(ID);
                }
            }
            public byte Level;
            public Nyx.Server.Game.ConquerStructures.NobilityRank NobilityRank;
            public uint Mesh = 0;
            public byte Class;
            public uint UnionID;
            public uint GuildID;
        }
        public uint ID;
        uint _goldbricks = 0;
        public uint GoldBricks
        {
            get
            {
                return _goldbricks;
            }
            set
            {

                _goldbricks = value;
                UpdateDatabase("GoldBricks", value);
            }
        }
        ulong _UnionFund = 0;
        public ulong UnionFund
        {
            get
            {
                return _UnionFund;
            }
            set
            {
                _UnionFund = value;
                UpdateDatabase("unionFund", value);
            }
        }
        string _declaration = "";
        public string Declaration
        {
            get
            {
                return _declaration;
            }
            set
            {
                _declaration = value;
                UpdateDatabase("Declaration", value);
            }
        }
        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                UpdateDatabase("Name", value);
                foreach (var client in Kernel.GamePool.Values)
                {
                    if (client.Union != null)
                        client.Send(SendOverheadLeagueInfo(client));
                }
            }
        }
        string _title = "";
        public string Title
        {
            get
            {

                return _title;
            }
            set
            {

                _title = value;
                UpdateDatabase("Title", value);
                foreach (var client in Kernel.GamePool.Values)
                {
                    if (client.UnionID == this.ID)
                    {
                        client.Send(SendOverheadLeagueInfo(client));
                    }
                }
            }
        }
        public void SendGui(GameClient client, byte type)
        {
            byte[] Buffer = new byte[23 + 8];
            Write((ushort)(Buffer.Length - 8), 0, Buffer);
            Write(2622, 2, Buffer);
            Write(type, 4, Buffer);
            Write(0, 6, Buffer);
            Write(0, 14, Buffer);//ID FOR Union 
            client.Send(Buffer.ToArray());
        }
        public uint[] Coreofficals = new uint[8];
        public uint[] Harem = new uint[10];
        public uint[] Guards = new uint[20];
        public string LeaderName;
        public uint LeaderUID;
        string _bulletin = "";
        public string Bulletin
        {
            get
            {

                return _bulletin;
            }
            set
            {

                _bulletin = value;
                UpdateDatabase("Bulletin", value);
            }
        }
        public List<uint> Guilds;
        public List<Member> Members
        {
            get
            {
                return UnionTable.LoadMembers(this.ID);
            }
        }
        public bool IsLeader(uint uid)
        {
            if (uid == LeaderUID)
            {
                return true;
            }
            return false;
        }
        public bool IsKingdom()
        {
            if (Kernel.ServerKingdom == this.ID)
            {
                return true;
            }
            return false;
        }
        public UnionClass(Nyx.Server.Client.GameClient Owner, string name)
        {
            Guilds = new List<uint>();
            LeaderName = Owner.Entity.Name;
            LeaderUID = Owner.Entity.UID;
            Guilds.Add(Owner.Guild.ID);
            Name = name;
            ID = Union.UnionCounter.Next;
            UnionTable.CreateUnion(this);
            Kernel.Unions.Add(this.ID, this);
            Owner.Union = Kernel.Unions[this.ID];
            Owner.UnionID = Owner.Union.ID;
            Owner.Offical = 1;
            Owner.Harem = 0;
            Owner.Guards = 0;
            Owner.Guild.UnionID = Owner.Union.ID;
            Nyx.Server.Database.GuildTable.UpdateUnionID(Owner.Guild);
            Owner.Send(SendOverheadLeagueInfo(Owner));
            new Nyx.Server.Database.MySqlCommand(Nyx.Server.Database.MySqlCommandType.UPDATE).Update("configuration").Set("UnionID",
            Union.UnionCounter.Now).Where("ServerPort", 0, true).Execute();
            Owner.Guild.ConquerPointFund -= 3000;
        }
        public UnionClass()
        {
            Guilds = new List<uint>();
        }
        public static void UpGradeUnion()
        {
            Union.MsgLeagueOpt response;
            var KingDom = Kernel.Unions.Values.OrderByDescending(U => U.GoldBricks).FirstOrDefault();
	    if (KingDom == null) return;
            Kernel.ServerKingdom = KingDom.ID;
            foreach (var C in Kernel.GamePool.Values)
            {
                if (C.Union != null)
                {
                    if (C.UnionID == KingDom.ID)
                    {
                        C.Union.GetMemberRankOffical(C);
                        C.Union.GetMemberRankHarem(C);
                        C.Union.GetMemberRankGuard(C);
                        if (C.Union.IsLeader(C.Entity.UID))
                        {
                            response = new Union.MsgLeagueOpt(true);
                            response.Action = Union.Actions.KingdomTitleGui;
                            response.dwParam3 = (ushort)Kernel.ServerKingdom;
                            C.Send(response.Encode());
                        }
                    }
                }
                response = new Union.MsgLeagueOpt(true);
                response.Action = Union.Actions.KingdomIcon;
                response.dwParam3 = (ushort)Kernel.ServerKingdom;
                C.Send(response.Encode());
                if (C.Union != null)
                {
                    C.Send(C.Union.SendOverheadLeagueInfo(C));
                }
                C.SendScreen(C.Entity.SpawnPacket, false);
            }
            MySqlCommand com = new MySqlCommand(MySqlCommandType.UPDATE);
        }
        public void AddMember(Nyx.Server.Client.GameClient client)
        {
            client.Union = this;
            client.UnionID = client.Union.ID;
            client.Send(SendOverheadLeagueInfo(client));
        }
        public void GetMemberRankGuard(Nyx.Server.Client.GameClient client)
        {
            if (Guards.Contains(client.Entity.UID))
                client.Guards = 512;
        }
        public void GetMemberRankHarem(Nyx.Server.Client.GameClient client)
        {
            if (Harem.Contains(client.Entity.UID))
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Harem[i] == client.Entity.UID)
                    {
                        if (i == 0)
                            client.Harem = 1024;
                        else if (i == 1 || i == 2)
                            client.Harem = 2048;
                        else client.Harem = 4096;
                    }
                }
            }
        }
        public void GetMemberRankOffical(Nyx.Server.Client.GameClient client)
        {
            if (IsLeader(client.Entity.UID))
            {
                client.Offical = 1;
                return;
            }
            if (Coreofficals.Contains(client.Entity.UID))
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Coreofficals[i] == client.Entity.UID)
                    {
                        if (i == 0)
                            client.Offical = 2;
                        else if (i == 1)
                            client.Offical = 4;
                        else if (i == 2)
                            client.Offical = 8;
                        else if (i == 3)
                            client.Offical = 16;
                        else if (i == 4)
                            client.Offical = 32;
                        else if (i == 5)
                            client.Offical = 64;
                        else if (i == 6)
                            client.Offical = 128;
                        else if (i == 7)
                            client.Offical = 256;
                    }
                }
            }
        }
        public void RemoveHarem(Nyx.Server.Client.GameClient client)
        {
            if (Harem.Contains(client.Entity.UID))
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Harem[i] == client.Entity.UID)
                    {
                        Harem[i] = 0;
                        client.Harem = 0;
                    }
                }
            }
            UnionTable.UpdateHarem(this);
            UnionTable.UpdateData(client.Entity.UID, "Harem", 0);
        }
        public void RemoveOffHarem(uint client)
        {
            if (Harem.Contains(client))
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Harem[i] == client)
                    {
                        Harem[i] = 0;
                        UnionTable.UpdateData(client, "Harem", 0);
                    }
                }
            }
            UnionTable.UpdateHarem(this);
        }
        public void RemoveCoreoffical(Nyx.Server.Client.GameClient client)
        {
            if (Coreofficals.Contains(client.Entity.UID))
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Coreofficals[i] == client.Entity.UID)
                    {
                        Coreofficals[i] = 0;
                        client.Offical = 0;
                    }
                }
            }
            UnionTable.UpdateCoreOfficals(this);
            UnionTable.UpdateData(client.Entity.UID, "Offical", 0);
        }
        public void RemoveOffCoreoffical(uint client)
        {
            if (Coreofficals.Contains(client))
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Coreofficals[i] == client)
                    {
                        Coreofficals[i] = 0;
                        UnionTable.UpdateData(client, "Offical", 0);
                    }
                }
            }
            UnionTable.UpdateCoreOfficals(this);
        }
        public void RemoveGuards(Nyx.Server.Client.GameClient client)
        {
            if (Guards.Contains(client.Entity.UID))
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Guards[i] == client.Entity.UID)
                    {
                        Guards[i] = 0;
                        client.Guards = 0;
                    }
                }
            }
            UnionTable.UpdateGuards(this);
            UnionTable.UpdateData(client.Entity.UID, "Guards", 0);
        }
        public void RemoveOffGuards(uint client)
        {
            if (Guards.Contains(client))
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Guards[i] == client)
                    {
                        Guards[i] = 0;
                        UnionTable.UpdateData(client, "Guards", 0);
                    }
                }
            }
            UnionTable.UpdateGuards(this);
        }
        public void RemoveMember(Nyx.Server.Client.GameClient client)
        {
            var M = Members.Where(c => c.ID == client.Entity.UID).FirstOrDefault();
            if (M != null)
            {
                if (M.UnionID == ID)
                {
                    if (client.Union.LeaderUID == client.Entity.UID)
                    {
                        Kernel.Unions.Remove(client.Union.ID);
                        UnionTable.DeleteUnion(client.Union.ID);
                        foreach (var mem in Kernel.Unions[client.Union.ID].Members)
                        {
                            RemoveCoreoffical(Client.GameClient.GetClientFromName(mem.Name));
                            RemoveHarem(Client.GameClient.GetClientFromName(mem.Name));
                            RemoveGuards(Client.GameClient.GetClientFromName(mem.Name));
                            Client.GameClient.GetClientFromName(mem.Name).Union = null;
                            Client.GameClient.GetClientFromName(mem.Name).Offical = 0;
                            Client.GameClient.GetClientFromName(mem.Name).Harem = 0;
                            Client.GameClient.GetClientFromName(mem.Name).Guards = 0;
                            UnionTable.UpdateData(Client.GameClient.GetClientFromName(mem.Name).Entity.UID, "UnionID", 0);
                            Union.MsgLeagueOpt Res2 = new Union.MsgLeagueOpt(true);
                            Res2.Action = Union.Actions.KingdomIcon;
                            Res2.dwParam3 = Kernel.ServerKingdom;
                            Client.GameClient.GetClientFromName(mem.Name).Send(Res2.Encode());
                            Client.GameClient.GetClientFromName(mem.Name).Send(SendOverheadLeagueInfo(Client.GameClient.GetClientFromName(mem.Name)));
                        }
                    }
                    RemoveCoreoffical(client);
                    RemoveHarem(client);
                    RemoveGuards(client);
                    client.Union = null;
                    client.Offical = 0;
                    client.Harem = 0;
                    client.Guards = 0;
                    UnionTable.UpdateData(client.Entity.UID, "UnionID", 0);
                    Union.MsgLeagueOpt Res = new Union.MsgLeagueOpt(true);
                    Res.Action = Union.Actions.KingdomIcon;
                    Res.dwParam3 = Kernel.ServerKingdom;
                    client.Send(Res.Encode());
                    client.Send(SendOverheadLeagueInfo(client));
                }
            }
        }
        public void RemoveOffMember(uint client)
        {
            var M = Members.Where(c => c.ID == client).FirstOrDefault();
            if (M != null)
            {
                if (M.UnionID == ID)
                {
                    RemoveOffCoreoffical(client);
                    RemoveOffHarem(client);
                    RemoveOffGuards(client);
                    trush.Add(client);
                    UnionTable.UpdateData(client, "UnionID", 0);
                }
            }
        }
        List<uint> trush = new List<uint>();
        List<uint> Stipend = new List<uint>();
        public void AddHarem(GameClient client, ushort RankID, string Name)
        {
            uint Rank = RankID;
            uint i = 0;
            #region Ranks
            if (Rank == 4000)
            {
                Rank = 1024;
                i = 0;
            }
            if (Rank == 4011)
            {
                Rank = 2048;
                i = 1;
            }
            if (Rank == 4012)
            {
                Rank = 2048;
                i = 2;
            }
            if (Rank == 4021)
            {
                Rank = 4096;
                i = 3;
            }
            if (Rank == 4022)
            {
                Rank = 4096;
                i = 4;
            }
            if (Rank == 4023)
            {
                Rank = 4096;
                i = 5;
            }
            if (Rank == 4024)
            {
                Rank = 4096;
                i = 6;
            }
            if (Rank == 4025)
            {
                Rank = 4096;
                i = 7;
            }
            if (Rank == 4026)
            {
                Rank = 4096;
                i = 8;
            }
            if (Rank == 4027)
            {
                Rank = 4096;
                i = 9;
            }
            #endregion
            if (Rank != 0 && Rank != 401 && Rank != 402)
            {
                if (RankID >= 4000 && RankID <= 4027)
                {
                    var C = GameClient.GetClientFromName(Name);
                    if (C != null)
                    {
                        if (C.Union != null)
                        {
                            if (C.Union.ID == client.Union.ID)
                            {
                                if (client.Union.Harem[i] == 0 && !client.Union.Harem.Contains(C.Entity.UID))
                                {
                                    client.Union.Harem[i] = C.Entity.UID;
                                    C.Harem = (ushort)Rank;
                                    UnionTable.UpdateHarem(this);
                                    client.Send(ShowLeagueConcubinesList());
                                }
                            }
                        }
                    }
                }
            }
            else if (!Harem.Contains(client.Entity.UID))
            {
                if (RankID == 401 || RankID == 402)
                {
                    if (RankID == 401)
                    {
                        for (int q = 1; q < 3; q++)
                        {
                            if (Harem[q] == 0)
                            {
                                var C = GameClient.GetClientFromName(Name);
                                if (C != null)
                                {
                                    if (C.Union != null)
                                    {
                                        if (C.Union.ID == client.Union.ID)
                                        {
                                            if (client.Union.Harem[q] == 0 && !client.Union.Harem.Contains(C.Entity.UID))
                                            {
                                                client.Union.Harem[q] = C.Entity.UID;
                                                C.Harem = (ushort)2048;
                                                UnionTable.UpdateHarem(this);
                                                client.Send(ShowLeagueConcubinesList());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int q = 3; q < 10; q++)
                        {
                            if (Harem[q] == 0)
                            {
                                var C = GameClient.GetClientFromName(Name);
                                if (C != null)
                                {
                                    if (C.Union != null)
                                    {
                                        if (C.Union.ID == client.Union.ID)
                                        {
                                            if (client.Union.Harem[q] == 0 && !client.Union.Harem.Contains(C.Entity.UID))
                                            {
                                                client.Union.Harem[q] = C.Entity.UID;
                                                C.Harem = (ushort)4096;
                                                UnionTable.UpdateHarem(this);
                                                client.Send(ShowLeagueConcubinesList());
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void AddGuards(GameClient client, string Name)
        {
            int i = 0;
            var C = GameClient.GetClientFromName(Name);
            if (C != null)
            {
                if (C.Union != null)
                {
                    for (i = 0; i < 20; i++)
                    {
                        if (client.Union.Guards[i] == 0)
                            break;
                    }
                    if (C.Union.ID == client.Union.ID)
                    {
                        if (client.Union.Guards[i] == 0 && !client.Union.Guards.Contains(C.Entity.UID))
                        {
                            client.Union.Guards[i] = C.Entity.UID;
                            C.Guards = 512;
                            UnionTable.UpdateGuards(this);
                            client.Send(ShowLeaguePalaceGuardsList((1)));
                        }
                    }
                }
            }
        }
        public void AddCoreoffical(GameClient client, ushort RankID, string Name)
        {
            uint Rank = RankID;
            uint I = 0;
            #region Ranks
            if (Rank == 2000)
            {
                Rank = 2;
                I = 0;
            }
            if (Rank == 2010)
            {
                Rank = 4;
                I = 1;
            }
            if (Rank == 2020)
            {
                Rank = 8;
                I = 2;
            }
            if (Rank == 2030)
            {
                Rank = 16;
                I = 3;
            }
            if (Rank == 2040)
            {
                Rank = 32;
                I = 4;
            }
            if (Rank == 2050)
            {
                Rank = 64;
                I = 5;
            }
            if (Rank == 2060)
            {
                Rank = 128;
                I = 6;
            }
            if (Rank == 2070)
            {
                Rank = 256;
                I = 7;
            }
            #endregion
            if (Rank != 0)
            {
                if (RankID >= 2000 && RankID <= 2070)
                {
                    var C = GameClient.GetClientFromName(Name);
                    if (C != null)
                    {
                        if (C.Union != null)
                        {
                            if (C.Union.ID == client.Union.ID)
                            {
                                if (client.Union.Coreofficals[I] == 0 && !client.Union.Coreofficals.Contains(C.Entity.UID))
                                {
                                    client.Union.Coreofficals[I] = C.Entity.UID;
                                    C.Offical = (ushort)Rank;
                                    UnionTable.UpdateCoreOfficals(this);
                                    client.Send(ShowLeagueImperialCourtList());
                                }
                            }
                        }
                    }
                }
            }
        }
        public void AddGuild(GameClient client)
        {
            if (Guilds.Contains(client.Guild.ID))
                Guilds.Remove(client.Guild.ID);
            Guilds.Add(client.Guild.ID);
            client.Guild.UnionID = ID;
            GuildTable.UpdateUnionID(client.Guild);
            foreach (var m in client.Guild.Members.Values)
            {
                if (m.IsOnline)
                {
                    if (m.Client != null)
                        AddMember(m.Client);
                }
                else
                {
                    UnionTable.UpdateData(m.ID, "UnionID", client.Guild.UnionID);
                }
            }
        }
        public void RemoveGuild(uint GuildID)
        {
            var Guild = Kernel.Guilds[GuildID];
            if (Guild != null)
            {
                if (Guilds.Contains(GuildID))
                {
                    Guild.UnionID = 0;
                    GuildTable.UpdateUnionID(Guild);
                    Guilds.Remove(GuildID);
                    foreach (var m in Guild.Members.Values)
                    {
                        if (m.IsOnline)
                        {
                            if (m.Client != null)
                                RemoveMember(m.Client);
                        }
                        else
                        {
                            UnionTable.UpdateData(m.ID, "UnionID", Guild.UnionID);
                        }
                    }
                }
            }
        }
        public static string MakeName(string Name)
        {
            List<string> Chr = new List<string>();
            for (byte i = (byte)Name.Length; i < 16; i++)
            {
                Chr.Add(i.ToString().Replace(i.ToString(), "\0"));
            }
            StringBuilder builder = new StringBuilder();
            foreach (string N in Chr)
            {
                builder.Append(N);
            }
            return Name + builder.ToString();
        }
        #region Packets
        public byte[] ShowLeaguePalaceGuardsList(byte Page)
        {
            List<UnionClass.Member> list = new List<Member>();
            var Members = this.Members.Where(m => m.Guards == 512 && Guards.Contains(m.ID)).OrderByDescending(n => n.IsOnline).ToArray();
            list.Clear();
            byte P = 0;
            if (Page == 1)
                P = 0;
            else if (Page == 2)
                P = 1;
            for (int i = (int)P * 10; i < P * 10 + 10; i++)
            {
                if (i < Members.Length)
                {
                    list.Add(Members[i]);
                }
            }
            byte[] buffer = new byte[8 + (41 * list.Count) + 10];
            buffer[0] = (byte)(buffer.Length - 8);
            Write(2628, 2, buffer);
            Write(list.Count, 4, buffer);
            Write(Page, 5, buffer);
            if (list.Count > 0)
            {
                int Offset = 6;
                foreach (var Member in list)
                {
                    if (Member.IsOnline)
                    {
                        var C = GameClient.GetClientFromID(Member.ID);
                        Write((uint)C.Entity.BattlePower, Offset, buffer);
                    }
                    else
                    {
                        Write(0, Offset, buffer);
                    }
                    Offset += 4;
                    Write((uint)Member.Mesh, Offset, buffer); Offset += 4;
                    Write((uint)Member.UnionExploits, Offset, buffer); Offset += 4;
                    Write((uint)Member.NobilityRank, Offset, buffer); Offset += 4;
                    Write((uint)Member.ID, Offset, buffer); Offset += 4;
                    Write((uint)Member.Level, Offset, buffer); Offset += 2;
                    Write((uint)Member.Class, Offset, buffer); Offset += 2;
                    if (Member.IsOnline)
                        buffer[Offset] = 1;
                    else buffer[Offset] = 0;
                    Offset += 1;
                    Write(Member.Name, Offset, buffer);
                    Offset += 16;
                }
            }

            return buffer;
        }
        public byte[] ShowLeagueConcubinesList()
        {
            List<Union.MsgImperialHarem> Packets = new List<Union.MsgImperialHarem>();
            byte[] buffer = new byte[0];
            List<Member> Members = this.Members.Where(m => m.Harem > 0 && Harem.Contains(m.ID)).OrderByDescending(n => n.IsOnline).ToList();
            Union.MsgImperialHaremStartPacket Packet = new Union.MsgImperialHaremStartPacket();
            Union.MsgImperialHarem Packett = null;
            uint I = 0;
            foreach (var mem in Members)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Harem[i] == mem.ID)
                    {
                        if (i == 1 || i == 2)
                            I = (uint)i;
                        else if (i > 2)
                            I = (uint)i - 2;
                    }
                }
                uint ID = 0;
                if (mem.Harem == 1024)
                    ID = 4000;
                if (mem.Harem == 2048)
                    ID = 4010 + I;
                if (mem.Harem == 4096)
                    ID = 4020 + I;
                Packett = new Union.MsgImperialHarem()
                {
                    UID = mem.ID,
                    PositionRank = ID,
                    Name = MakeName(mem.Name),
                    Class = mem.Class,
                    Level = mem.Level,
                    Exploits = mem.UnionExploits,
                    Nobility = (uint)mem.NobilityRank,
                    Online_Offline = mem.IsOnline ? (uint)1 : (uint)0,
                    Mesh = mem.Mesh
                };
                if (mem.IsOnline)
                    Packett.BattlePower = (uint)GameClient.GetClientFromID(mem.ID).Entity.BattlePower;
                Packets.Add(Packett);
            }
            if (Packets.Count > 0)
            {
                Packet = new Union.MsgImperialHaremStartPacket()
                {
                    count = (uint)Packets.Count,
                    Accounts = Packets
                };
                using (var stream = new MemoryStream())
                {
                    Serializer.SerializeWithLengthPrefix(stream, Packet, PrefixStyle.Fixed32);
                    buffer = new byte[stream.Length + 8];
                    Array.Copy(stream.ToArray(), 0, buffer, 0, stream.Length);
                    Write(buffer.Length - 8, 0, buffer);
                    Write(2634, 2, buffer);
                }
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.SerializeWithLengthPrefix(stream, Packet, PrefixStyle.Fixed32);
                    buffer = new byte[6 + 8];
                    Array.Copy(stream.ToArray(), 0, buffer, 0, stream.Length);
                    Write(buffer.Length - 8, 0, buffer);
                    Write(2634, 2, buffer);
                }
            }
            return buffer;
        }
        public byte[] ShowLeagueImperialCourt()
        {
            byte[] buffer;
            List<Member> Members = this.Members.Where(m => m.Offical > 0 && Coreofficals.Contains(m.ID) || m.Offical == 1).OrderByDescending(n => n.IsOnline).ToList();
            int count = Members.Count;
            buffer = new byte[(43 * count) + 8 + 10];
            Write(buffer.Length - 8, 0, buffer);
            Write(2629, 2, buffer);
            Write(1, 4, buffer);
            Write(count, 5, buffer);
            int Offset = 6;
            foreach (var Member in Members)
            {
                ushort ID = 0;
                if (Member.IsOnline)
                {
                    var C = GameClient.GetClientFromID(Member.ID);
                    Write((uint)C.Entity.BattlePower, Offset, buffer);
                }
                else
                {
                    Write(0, Offset, buffer);
                }
                Offset += 4;
                Write((uint)Member.Mesh, Offset, buffer); Offset += 4;
                Write((uint)Member.UnionExploits, Offset, buffer); Offset += 4;
                Write((uint)Member.NobilityRank, Offset, buffer); Offset += 4;
                Write((uint)Member.ID, Offset, buffer); Offset += 4;
                Write((uint)Member.Level, Offset, buffer); Offset += 2;
                Write((uint)Member.Class, Offset, buffer); Offset += 2;
                if (Member.Offical == 1)
                    ID = 1000;
                if (Member.Offical == 2)
                    ID = 2000;
                if (Member.Offical == 4)
                    ID = 2010;
                if (Member.Offical == 8)
                    ID = 2020;
                if (Member.Offical == 16)
                    ID = 2030;
                if (Member.Offical == 32)
                    ID = 2040;
                if (Member.Offical == 64)
                    ID = 2050;
                if (Member.Offical == 128)
                    ID = 2060;
                if (Member.Offical == 256)
                    ID = 2070;
                Write(ID, Offset, buffer); Offset += 2;
                if (Member.IsOnline)
                    buffer[Offset] = 1;
                else buffer[Offset] = 0;
                Offset += 1;
                Write(Member.Name, Offset, buffer);
                Offset += 16;
            }

            return buffer;
        }
        public byte[] ShowLeagueImperialCourtList()
        {
            byte[] buffer;
            List<Member> Members = this.Members.Where(m => m.Offical > 0 && Coreofficals.Contains(m.ID)).OrderByDescending(n => n.IsOnline).ToList();
            int count = Members.Count;
            buffer = new byte[(43 * count) + 8 + 10];
            Write(buffer.Length - 8, 0, buffer);
            Write(2629, 2, buffer);
            Write(1, 4, buffer);
            Write(count, 5, buffer);
            int Offset = 6;
            foreach (var Member in Members)
            {
                ushort ID = 0;
                if (Member.IsOnline)
                {
                    var C = GameClient.GetClientFromID(Member.ID);
                    Write((uint)C.Entity.BattlePower, Offset, buffer);
                }
                else
                {
                    Write(0, Offset, buffer);
                }
                Offset += 4;
                Write((uint)Member.Mesh, Offset, buffer); Offset += 4;
                Write((uint)Member.UnionExploits, Offset, buffer); Offset += 4;
                Write((uint)Member.NobilityRank, Offset, buffer); Offset += 4;
                Write((uint)Member.ID, Offset, buffer); Offset += 4;
                Write((uint)Member.Level, Offset, buffer); Offset += 2;
                Write((uint)Member.Class, Offset, buffer); Offset += 2;
                if (Member.Offical == 2)
                    ID = 2000;
                if (Member.Offical == 4)
                    ID = 2010;
                if (Member.Offical == 8)
                    ID = 2020;
                if (Member.Offical == 16)
                    ID = 2030;
                if (Member.Offical == 32)
                    ID = 2040;
                if (Member.Offical == 64)
                    ID = 2050;
                if (Member.Offical == 128)
                    ID = 2060;
                if (Member.Offical == 256)
                    ID = 2070;
                Write(ID, Offset, buffer); Offset += 2;
                if (Member.IsOnline)
                    buffer[Offset] = 1;
                else buffer[Offset] = 0;
                Offset += 1;
                Write(Member.Name, Offset, buffer);
                Offset += 16;
            }

            return buffer;
        }
        public byte[] ShowLeagueGuildMemList(ushort guildid, ushort page)
        {
            if (this != null && guildid != 0)
            {
                List<UnionClass.Member> list = new List<UnionClass.Member>();
                byte[] buffer;
                byte Page = (byte)page;
                ushort GuildID = guildid;
                var Members = this.Members.Where(m => m.GuildID == GuildID).OrderByDescending(n => n.IsOnline).ToArray();
                list.Clear();
                for (int i = (int)page * 10; i < page * 10 + 10; i++)
                {
                    if (i < Members.Length)
                    {
                        list.Add(Members[i]);
                    }
                }
                buffer = new byte[19 + 8 + 49 * list.Count];
                if (list.Count > 0)
                {
                    Write(buffer.Length - 8, 0, buffer);
                    Write(2626, 2, buffer);
                    Write(GuildID, 4, buffer);
                    if (Kernel.Guilds[GuildID].Leader != null)
                        Write((uint)Kernel.Guilds[GuildID].Leader.ID, 8, buffer);
                    Write(Members.Length, 12, buffer);
                    Write(list.Count, 16, buffer);
                    buffer[18] = 1;
                    int Offset = 19;
                    for (int i = 0; i < list.Count; i++)
                    {
                        var Mem = list[i];
                        Write(Mem.UnionExploits, Offset, buffer); Offset += 4;
                        Write(Mem.Class, Offset, buffer); Offset += 4;
                        Write(Mem.Level, Offset, buffer); Offset += 2;
                        Write(Mem.Mesh, Offset, buffer); Offset += 4;
                        if (Mem.IsOnline)
                            buffer[Offset] = 1;
                        else buffer[Offset] = 0;
                        Offset += 1;
                        Write(Mem.Name, Offset, buffer); Offset += 16;
                        if (Mem.IsOnline)
                        {
                            var C = GameClient.GetClientFromID(Mem.ID);
                            Write(C.UnionTitle, Offset, buffer); Offset += 4;
                        }
                        else
                        {
                            Write(0, Offset, buffer); Offset += 4;
                        }
                        Write((byte)(Game.ConquerStructures.NobilityRank)Mem.NobilityRank, Offset, buffer); Offset += 4;
                        Write(Mem.ID, Offset, buffer); Offset += 4;
                        if (Mem.IsOnline)
                        {
                            var C = GameClient.GetClientFromID(Mem.ID);
                            Write((uint)C.Entity.BattlePower, Offset, buffer);
                        }
                        else
                        {
                            Write(0, Offset, buffer);
                        }
                        Offset += 2;
                    }

                }
                return buffer;
            }
            return new byte[0];
        }
        public byte[] ShowLeagueMemList(ushort guildid, ushort page)
        {
            List<UnionClass.Member> list = new List<UnionClass.Member>();
            byte[] buffer;
            byte Page = (byte)page;
            ushort GuildID = guildid;
            var Members = this.Members.Where(m => m.GuildID == 0 && !trush.Contains(m.ID)).OrderByDescending(n => n.IsOnline).ToArray();
            list.Clear();
            for (int i = (int)page * 10; i < page * 10 + 10; i++)
            {
                if (i < Members.Length)
                {
                    list.Add(Members[i]);
                }
            }
            buffer = new byte[19 + 8 + 49 * list.Count];
            if (list.Count > 0)
            {
                Write(buffer.Length - 8, 0, buffer);
                Write(2626, 2, buffer);
                Write(GuildID, 4, buffer);
                Write(0, 8, buffer);
                Write(Members.Length, 12, buffer);
                Write(list.Count, 16, buffer);
                buffer[18] = 1;
                int Offset = 19;
                for (int i = 0; i < list.Count; i++)
                {
                    var Mem = list[i];
                    Write(Mem.UnionExploits, Offset, buffer); Offset += 4;
                    Write(Mem.Class, Offset, buffer); Offset += 4;
                    Write(Mem.Level, Offset, buffer); Offset += 2;
                    Write(Mem.Mesh, Offset, buffer); Offset += 4;
                    if (Mem.IsOnline)
                        buffer[Offset] = 1;
                    else buffer[Offset] = 0;
                    Offset += 1;
                    Write(Mem.Name, Offset, buffer); Offset += 16;
                    if (Mem.IsOnline)
                    {
                        var C = GameClient.GetClientFromID(Mem.ID);
                        Write((uint)C.UnionTitle, Offset, buffer);
                    }
                    else
                    {
                        Write(0, Offset, buffer);
                    }
                    Write((byte)(Game.ConquerStructures.NobilityRank)Mem.NobilityRank, Offset, buffer); Offset += 4;
                    Write(Mem.ID, Offset, buffer); Offset += 4;
                    if (Mem.IsOnline)
                    {
                        var C = GameClient.GetClientFromID(Mem.ID);
                        Write((uint)C.Entity.BattlePower, Offset, buffer);
                    }
                    else
                    {
                        Write(0, Offset, buffer);
                    }
                    Offset += 2;
                }

            }
            return buffer;
        }
        public byte[] SendLeagueInfo()
        {
            byte[] buffer = new byte[370];
            Write(buffer.Length - 8, 0, buffer);
            Write(2623, 2, buffer);
            Write(UnionFund, 4, buffer);
            Write(GoldBricks, 12, buffer);
            Write(Name, 28, buffer);
            Write(LeaderName, 44, buffer);
            Write(Bulletin, 60, buffer);
            Write(Title, 316, buffer);
            Write("None", 326, buffer);
            Write("None", 342, buffer);
            return buffer;
        }
        public byte[] SendLeagueSynList(byte Page)
        {
            byte[] buffer = new byte[0];
            List<uint> list = new List<uint>();
            list.Clear();
            for (int i = (int)Page * 15; i < Page * 15 + 15; i++)
            {
                if (i < Guilds.Count)
                {
                    list.Add(Guilds[i]);
                }
            }
            buffer = new byte[13 + 8 + 48 * list.Count];
            Write(buffer.Length - 8, 0, buffer);
            Write(2625, 2, buffer);
            Write(Guilds.Count, 4, buffer);
            buffer[8] = (byte)list.Count;
            buffer[10] = 1;
            int Offset = 11;
            for (int i = 0; i < list.Count; i++)
            {
                uint ID = Guilds[i];
                var Guild = Kernel.Guilds[ID];
                Write(Guild.ID, Offset, buffer); Offset += 4;
                Write(Guild.SilverFund, Offset, buffer); Offset += 8;
                Write(Guild.MemberCount, Offset, buffer); Offset += 2;
                Write(Guild.Level, Offset, buffer); Offset += 2;
                Write(Guild.Name, Offset, buffer); Offset += 16;
                Write(Guild.LeaderName, Offset, buffer); Offset += 16;
            }
            Offset += 4;
            return buffer;
        }
        public static byte[] SendLeagueAllegianceList(byte page)
        {
            byte[] buffer;
            byte Page = (byte)page;
            List<UnionClass> list = new List<UnionClass>();
            var Unions = Kernel.Unions.Values.OrderByDescending(x => x.GoldBricks).ThenByDescending(x => x.UnionFund).ThenBy(x => x.ID).ToArray();
            list.Clear();
            for (int i = (int)page * 3; i < page * 3 + 3; i++)
            {
                if (i < Unions.Length)
                {
                    list.Add(Unions[i]);
                }
            }
            buffer = new byte[13 + 8 + 306 * list.Count];
            if (list.Count > 0)
            {

                Write(buffer.Length - 8, 0, buffer);
                Write(2624, 2, buffer);
                buffer[4] = (byte)Unions.Length;
                buffer[8] = (byte)Page;
                Write(list.Count, 10, buffer);
                buffer[12] = (byte)1;
                int Offset = 13;
                for (int i = 0; i < list.Count; i++)
                {
                    var Union = list[i];
                    Write(Union.UnionFund, Offset, buffer); Offset += 8;
                    Write(Union.ID, Offset, buffer); Offset += 4;
                    Write(Union.GoldBricks, Offset, buffer); Offset += 4;
                    Write(Union.Name, Offset, buffer); Offset += 16;
                    Write(Union.LeaderName, Offset, buffer); Offset += 16;

                    Write(Union.Declaration, Offset, buffer); Offset += 256;

                }
            }
            else
            {
                Write(buffer.Length - 8, 0, buffer);
                Write(2624, 2, buffer);

            }
            return buffer;
        }
        public byte[] SendOverheadLeagueInfo(GameClient client)
        {
            byte[] buffer = new byte[0];
            Union.OverheadLeagueInfo Packet = new Union.OverheadLeagueInfo();
            if (client.Union != null)
            {
                Packet = new Union.OverheadLeagueInfo()
                {
                    Type = 0,
                    UID = client.Entity.UID,
                    UnionID = client.Union.ID,
                    PlayerRank = client.Union.IsLeader(client.Entity.UID) ? (uint)1 : (uint)0,
                    UnionRank = client.Union.IsKingdom() ? (uint)1 : (uint)0,
 	     ServerID = client.Entity.ServerID,
                    Name = client.Union.Name
                };
                if (Packet.UnionRank == 1)
                    Packet.Name = client.Union.Title == "" ? client.Union.Name : client.Union.Title + "KDM";
                
                client.Entity.UnionName = Packet.Name;
                client.UnionRank = (byte)Packet.PlayerRank;
                client.UnionType = (byte)Packet.UnionRank;
                client.UnionID = Packet.UnionID;
            }
            if (client.Entity.EntityFlag == Nyx.Server.Game.EntityFlag.Player)
            {
                client.Entity.Update((byte)Update.UnionRank, (ushort)(client.Offical + client.Harem + client.Guards), true);
            }
            return Kernel.FinalizeProtoBuf(Packet, 2631);
        }
        public byte[] SendLeagueRank()
        {
            var Unions = Kernel.Unions.Values.Where(i => i.GoldBricks != 0).OrderByDescending(x => x.GoldBricks).ThenByDescending(x => x.UnionFund).ThenBy(x => x.ID).ToArray();
            int offset = 17;
            byte[] buffer = new byte[17 + 8 + Unions.Length * 40];
            Writer.Write(buffer.Length - 8, 0, buffer);
            Writer.Write(2627, 2, buffer);
            Writer.Write(Unions.Length, 6, buffer);
            Writer.Write(Unions.Length, 7, buffer);
            Writer.Write(Unions.Length, 11, buffer);
            foreach (var union in Unions)
            {
                Writer.Write(union.GoldBricks, offset, buffer); offset += 4;
                Writer.Write(union.Name, offset, buffer); offset += 16;
                Writer.Write(union.LeaderName, offset, buffer); offset += 20;
            }
            return buffer;
        }
        public byte[] SendAllLeagueRank()
        {
            var Unions = Kernel.Unions.Values.Where(i => i.GoldBricks != 0).OrderByDescending(x => x.GoldBricks).ThenByDescending(x => x.UnionFund).ThenBy(x => x.ID).ToArray();

            byte[] buffer = new byte[17 + 8 + Unions.Length * 40];
            Writer.Write(buffer.Length - 8, 0, buffer);
            Writer.Write(2627, 2, buffer);
            buffer[4] = 1;
            buffer[11] = (byte)Math.Min(Unions.Length, 10);
            buffer[10] = 1;
            buffer[6] = (byte)Unions.Length;
            int offset = 13;
            foreach (var union in Unions)
            {
                buffer[offset] = 1; offset += 4;//ServerID
                Writer.Write(union.GoldBricks, offset, buffer); offset += 4;
                Writer.Write(union.Name, offset, buffer); offset += 16;
                Writer.Write(union.LeaderName, offset, buffer); offset += 16;
            }
            return buffer;
        }
        public byte[] SendLeagueOrderStatu()
        {
            return Kernel.FinalizeProtoBuf(new Union.LeagueOrderStatuProto()
            {
                Unknown = 101,
                Unknown2 = 102,
                Unknown3 = 106,
                Unknown4 = 204,
                Unknown5 = 206
            }, 2630);
        }
        #endregion
        #region UpdateData
        private void UpdateDatabase(string column, byte value)
        {
            new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions").Set(column, value).Where("ID", ID).Execute();
        }
        private void UpdateDatabase(string column, long value)
        {
            new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions").Set(column, value).Where("ID", ID).Execute();
        }
        private void UpdateDatabase(string column, ulong value)
        {
            new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions").Set(column, value).Where("ID", ID).Execute();
        }
        private void UpdateDatabase(string column, string value)
        {
            new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions").Set(column, value).Where("ID", ID).Execute();
        }
        private void UpdateDatabase(string column, bool value)
        {
            new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions").Set(column, value).Where("ID", ID).Execute();
        }
        #endregion

        public static GameClient client { get; set; }
    }
}
