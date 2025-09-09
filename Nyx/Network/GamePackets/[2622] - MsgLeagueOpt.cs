using Nyx.Server;
using Nyx.Server.Client;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets.Union
{
    public class Union
    {
        public class MsgLeagueOpt : Nyx.Server.Network.Writer
        {
            private byte[] Packet;
            public MsgLeagueOpt(bool Create)
            {
                if (Create)
                {
                    Packet = new byte[23 + 8];
                    Packet[0] = (byte)(Packet.Length - 8);
                    Write(2622, 2, Packet);

                }
            }

            public Union.Actions Action
            {
                get { return (Union.Actions)BitConverter.ToUInt16(Packet, 4); }
                set { Write((byte)(Union.Actions)value, 4, Packet); }
            }
            public uint dwParam2
            {
                get { return BitConverter.ToUInt32(Packet, 6); }
                set { Write(value, 6, Packet); }
            }
            public uint UID
            {
                get { return BitConverter.ToUInt32(Packet, 10); }
                set { Write(value, 10, Packet); }
            }
            public uint dwParam3
            {
                get { return BitConverter.ToUInt32(Packet, 14); }
                set { Write(value, 14, Packet); }
            }
            public byte[] Encode()
            {
                return Packet;
            }
            public void Deserialize(byte[] buffer)
            {
                Packet = buffer;
            }
        }
        public static Counter UnionCounter = new Counter(0);
        public enum Actions : ushort
        {
            LeagueInfo = 0,
            LeagueSynList = 1,
            LeagueGuildMemList = 2,
            LeagueMemList = 3,
            LeagueAllegianceList = 4,
            KingdomIcon = 5,
            ChangeNameGui = 6,
            CreateLeague = 7,
            JoinLeagueOthers = 8,
            QuitLeague = 9,
            ExpelMember = 10,
            GuildJoinLeague = 11,
            GuildQuitLeague = 12,
            ExpelGuild = 13,
            TransferLeader = 14,
            SendLeagueRank = 15,
            SendAllLeagueRank = 16,
            KingdomTitleGui = 17,
            CreateKingdomTitle = 18,
            Declarationmsg = 20,
            LeaguePalaceGuardsList = 23,
            AppointPalaceGuard = 24,
            RemovePalaceGuard = 25,
            ReSignPalaceGuard = 26,
            Coreofficals = 27,
            AppointCoreOffical = 28,
            RemoveCoreOffical = 29,
            ReSignCoreOffical = 30,
            Officals = 34,
            SendLeagueOrderStatu = 37,
            SaveLeagueOrderStatu = 38,
            Stipend = 39,
            LeagueConcubinesList = 40,
            AppointLeagueConcubines = 41,
            RemoveHarem = 44,
            AppointCore = 47,
            AppointGuard = 48,
            AppointHarem = 49,
            Bulletionmsg = 66,
        }
        public static void Prosses(byte[] packet, GameClient client)
        {
            Union.MsgLeagueOpt LeagueOpt = new Union.MsgLeagueOpt(false);
            LeagueOpt.Deserialize(packet);
            switch (LeagueOpt.Action)
            {
                case Actions.SendLeagueOrderStatu:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.SendLeagueOrderStatu());
                        }
                        break;
                    }
                case Actions.SendAllLeagueRank:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.SendAllLeagueRank());
                        }
                        break;
                    }
                case Union.Actions.SendLeagueRank:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.SendLeagueRank());
                        }
                        break;
                    }
                case Union.Actions.ChangeNameGui:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string NewName = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.Name = NewName;
                                client.Send(client.Union.SendOverheadLeagueInfo(client));
                                foreach (var player in Kernel.GamePool.Values)
                                {
                                    if (player.Union != null)
                                    {
                                        if (player.Union.ID == client.UnionID)
                                        {
                                            client.Send(client.Union.SendOverheadLeagueInfo(player));
                                            client.SendScreenSpawn(client.Entity, true);
                                        }
                                    }
                                }
                                client.SendScreenSpawn(client.Entity, true);
                            }
                        }
                        break;
                    }
                case Actions.RemovePalaceGuard:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                var C = GameClient.GetClientFromID(LeagueOpt.UID);
                                if (C != null)
                                {
                                    client.Union.RemoveGuards(C);
                                    client.Send(client.Union.ShowLeaguePalaceGuardsList(1));
                                }
                                else
                                {
                                    client.Union.RemoveOffGuards(LeagueOpt.UID);
                                    client.Send(client.Union.ShowLeaguePalaceGuardsList(1));
                                }
                            }
                        }
                        break;
                    }
                case Actions.ReSignPalaceGuard:
                    {
                        if (client.Union != null)
                        {
                            client.Union.RemoveGuards(client);
                            client.Send(client.Union.ShowLeaguePalaceGuardsList(1));
                        }
                        break;
                    }
                case Actions.AppointGuard:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string name = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.AddGuards(client, name);
                            }
                        }
                        break;
                    }
                case Actions.AppointPalaceGuard:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string name = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.AddGuards(client, name);
                            }
                        }
                        break;
                    }
                case Actions.LeaguePalaceGuardsList:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.ShowLeaguePalaceGuardsList((byte)LeagueOpt.dwParam3));
                        }
                        break;
                    }
                case Actions.AppointHarem:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string name = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.AddHarem(client, (ushort)LeagueOpt.dwParam3, name);
                            }
                        }
                        break;
                    }
                case Actions.RemoveHarem:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                var C = GameClient.GetClientFromID(LeagueOpt.UID);
                                if (C != null)
                                {
                                    client.Union.RemoveHarem(C);
                                    client.Send(client.Union.ShowLeagueConcubinesList());
                                }
                                else
                                {
                                    client.Union.RemoveOffHarem(LeagueOpt.UID);
                                    client.Send(client.Union.ShowLeagueConcubinesList());
                                }
                            }
                        }
                        break;
                    }
                case Actions.AppointLeagueConcubines:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string name = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.AddHarem(client, (ushort)LeagueOpt.dwParam3, name);
                            }
                        }
                        break;
                    }
                case Actions.Declarationmsg:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string msg = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.Declaration = msg;
                            }
                        }
                        break;
                    }
                case Actions.Bulletionmsg:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string msg = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.Bulletin = msg;
                            }
                        }
                        break;
                    }
                case Actions.CreateKingdomTitle:
                    {
                        if (client.Union != null && client.Union.IsLeader(client.Entity.UID))
                        {
                            string name = Encoding.Default.GetString(packet, 20, packet[19]);
                            if (name.Length <= 5)
                                client.Union.Title = name;
                            client.SendScreenSpawn(client.Entity, true);
                        }
                        break;
                    }
                case Actions.CreateLeague:
                    {
                        if (client.Union == null && client.Guild != null && client.Entity.GuildRank == 1000)
                        {
                            string name = Encoding.Default.GetString(packet, 20, packet[19]);
                            var Cheak = Kernel.Unions.Values.Where(m => m.Name.ToLower() == name.ToLower()).Count();
                            if (Cheak == 0)
                            {
                                client.Union = new UnionClass(client, name);
                            }
                            else
                            {
                                byte[] buffer = new Message("There's a Union with the same name. Please choose another name.", System.Drawing.Color.Red, Message.System).Encode();
                                client.Send(buffer);
                            }
                        }
                        break;
                    }
                case Actions.Coreofficals:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.ShowLeagueImperialCourtList());
                        }
                        break;
                    }
                case Actions.AppointCoreOffical:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string name = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.AddCoreoffical(client, (ushort)LeagueOpt.dwParam3, name);
                            }
                        }
                        break;
                    }
                case Actions.RemoveCoreOffical:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                var C = GameClient.GetClientFromID(LeagueOpt.UID);
                                if (C != null)
                                {
                                    client.Union.RemoveCoreoffical(C);
                                    client.Send(client.Union.ShowLeagueImperialCourtList());
                                }
                                else
                                {
                                    client.Union.RemoveOffCoreoffical(LeagueOpt.UID);
                                    client.Send(client.Union.ShowLeagueImperialCourtList());
                                }
                            }
                        }
                        break;
                    }
                case Actions.ReSignCoreOffical:
                    {
                        if (client.Union != null)
                        {
                            client.Union.RemoveCoreoffical(client);
                            client.Send(client.Union.ShowLeagueImperialCourtList());
                        }
                        break;
                    }
                case Actions.Officals:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.ShowLeagueImperialCourt());
                        }
                        break;
                    }
                case Actions.AppointCore:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                string name = Encoding.Default.GetString(packet, 20, packet[19]);
                                client.Union.AddCoreoffical(client, (ushort)LeagueOpt.dwParam3, name);
                            }
                        }
                        break;
                    }
                case Actions.LeagueConcubinesList:
                    {
                        client.Send(client.Union.ShowLeagueConcubinesList());
                        break;
                    }
                case Actions.LeagueInfo:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.SendLeagueInfo());
                        }
                        break;
                    }
                case Actions.LeagueSynList:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.SendLeagueSynList((byte)LeagueOpt.dwParam3));
                        }
                        break;
                    }
                case Actions.LeagueGuildMemList:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.ShowLeagueGuildMemList((ushort)LeagueOpt.dwParam2, (ushort)LeagueOpt.dwParam3));
                        }
                        break;
                    }
                case Actions.LeagueMemList:
                    {
                        if (client.Union != null)
                        {
                            client.Send(client.Union.ShowLeagueMemList((ushort)LeagueOpt.dwParam2, (ushort)LeagueOpt.dwParam3));
                        }
                        break;
                    }
                case Actions.LeagueAllegianceList:
                    {
                        if (client.Union == null)
                        {
                            client.Send(UnionClass.SendLeagueAllegianceList((byte)LeagueOpt.dwParam3));
                        }
                        break;
                    }
                case Actions.GuildJoinLeague:
                    {
                        if (DateTime.Now >= client.Entity.PledgeStamp.AddMinutes(1))
                        {
                            if (client.Union == null && client.Guild != null && client.Entity.GuildRank == 1000)
                            {
                                var Union1 = Kernel.Unions[LeagueOpt.dwParam3];
                                Union1.AddGuild(client);
                                foreach (var player in Kernel.GamePool.Values)
                                {
                                    player.Send(new Message(client.Entity.Name + " has pledged loyalty to Union " + Union1.Name + ".", System.Drawing.Color.Red, Message.TopLeft));
                                }
                            }
                        }
                        else
                        {
                            client.Send(new Message("After quitting a Union, you need to wait 1 minute to re join.", System.Drawing.Color.Red, Message.TopLeft));
                        }
                        break;
                    }
                case Actions.GuildQuitLeague:
                    {
                        if (client.Union != null && client.Guild != null && client.Entity.GuildRank == 1000)
                        {
                            client.Union.RemoveGuild(client.Guild.ID);
                            client.Entity.PledgeStamp = DateTime.Now;
                        }
                        break;
                    }
                case Actions.JoinLeagueOthers:
                    {
                        if (DateTime.Now >= client.Entity.PledgeStamp.AddMinutes(1))
                        {
                            if (client.Union == null && client.Guild == null)
                            {
                                var Union2 = Kernel.Unions[LeagueOpt.dwParam3];
                                Union2.AddMember(client);
                                foreach (var player in Kernel.GamePool.Values)
                                {
                                    player.Send(new Message(client.Entity.Name + " has pledged loyalty to Union " + Union2.Name + ".", System.Drawing.Color.Red, Message.TopLeft));
                                }
                            }
                        }
                        else
                        {
                            client.Send(new Message("After quitting a Union, you need to wait 1 minute to re join.", System.Drawing.Color.Red, Message.TopLeft));
                        }
                        break;
                    }
                case Actions.QuitLeague:
                    {
                        if (client.Union != null)
                        {
                            var Union3 = client.Union;
                            Union3.RemoveMember(client);
                            client.SendScreenSpawn(client.Entity, true);
                            client.Entity.PledgeStamp = DateTime.Now;
                        }
                        break;
                    }
                case Actions.ExpelMember:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                var C = GameClient.GetClientFromID(LeagueOpt.dwParam3);
                                if (C != null)
                                {
                                    client.Union.RemoveMember(C);
                                }
                                else
                                {
                                    client.Union.RemoveOffMember(LeagueOpt.dwParam3);
                                }
                            }
                        }
                        break;
                    }
                case Actions.ExpelGuild:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                client.Union.RemoveGuild(LeagueOpt.dwParam2);
                            }
                        }
                        break;
                    }
                case Actions.TransferLeader:
                    {
                        if (client.Union != null)
                        {
                            if (client.Union.IsLeader(client.Entity.UID))
                            {
                                var C = GameClient.GetClientFromID(LeagueOpt.UID);
                                if (C != null)
                                {
                                    if (C.Union != null)
                                    {
                                        if (C.Union.ID == client.Union.ID)
                                        {
                                            client.Union.RemoveCoreoffical(C);
                                            client.Union.RemoveHarem(C);
                                            client.Union.RemoveGuards(C);
                                            client.Union.LeaderName = C.Entity.Name;
                                            client.Union.LeaderUID = C.Entity.UID;
                                            UnionTable.UpdateLeader(client.Union);
                                            C.Offical = 1;
                                            client.Offical = 0;
                                            C.Send(C.Union.SendOverheadLeagueInfo(C));
                                            client.Send(client.Union.SendOverheadLeagueInfo(client));
                                        }
                                    }
                                }
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
        [ProtoContract]
        public class OverheadLeagueInfo
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Type;
            [ProtoMember(2, IsRequired = true)]
            public uint UID;
            [ProtoMember(3, IsRequired = true)]
            public uint UnionID;
            [ProtoMember(4, IsRequired = true)]
            public uint PlayerRank;
            [ProtoMember(5, IsRequired = true)]
            public uint UnionRank;
            [ProtoMember(6, IsRequired = true)]
            public uint ServerID;
            [ProtoMember(7, IsRequired = true)]
            public string Name;
        }
        [ProtoContract]
        public class LeagueOrderStatuProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Unknown;
            [ProtoMember(4, IsRequired = true)]
            public uint Unknown2;
            [ProtoMember(7, IsRequired = true)]
            public uint Unknown3;
            [ProtoMember(10, IsRequired = true)]
            public uint Unknown4;
            [ProtoMember(13, IsRequired = true)]
            public uint Unknown5;
        }
        [ProtoContract]
        public class MsgImperialHaremStartPacket
        {
            [ProtoMember(1, IsRequired = true)]
            public uint count;
            [ProtoMember(2, IsRequired = true)]
            public List<MsgImperialHarem> Accounts { get; set; }
        }
        [ProtoContract]
        public class MsgImperialHarem
        {
            [ProtoMember(1, IsRequired = true)]
            public uint UID;
            [ProtoMember(2, IsRequired = true)]
            public uint PositionRank;
            [ProtoMember(3, IsRequired = true)]
            public string Name;
            [ProtoMember(4, IsRequired = true)]
            public uint Class;
            [ProtoMember(5, IsRequired = true)]
            public uint Level;
            [ProtoMember(6, IsRequired = true)]
            public uint BattlePower;
            [ProtoMember(7, IsRequired = true)]
            public uint Exploits;
            [ProtoMember(8, IsRequired = true)]
            public uint Nobility;
            [ProtoMember(9, IsRequired = true)]
            public uint Online_Offline;
            [ProtoMember(10, IsRequired = true)]
            public uint Mesh;
        }
    }
}
