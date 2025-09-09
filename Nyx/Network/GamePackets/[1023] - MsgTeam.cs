using System;
using System.Drawing;
using Nyx.Server.Game;
namespace Nyx.Server.Network.GamePackets
{
    public class Team : Writer, Interfaces.IPacket
    {
        public enum Mode : uint
        {
            Create = 0,
            JoinRequest = 1,
            ExitTeam = 2,
            AcceptInvitation = 3,
            InviteRequest = 4,
            AcceptJoinRequest = 5,
            Dismiss = 6,
            Kick = 7,
            ForbidJoining = 8,
            UnforbidJoining = 9,
            LootMoneyOff = 10,
            LootMoneyOn = 11,
            LootItemsOff = 12,
            LootItemsOn = 13,
            Unknown = 14,
            TeamLader = 15
        }
        byte[] Buffer;
        public Team()
        {
            Buffer = new byte[16 + 8];
            Writer.Write(16, 0, Buffer);
            Writer.Write(1023, 2, Buffer);
        }
        public Mode Action
        {
            get { return (Mode)Buffer[4]; }
            set { Write((ushort)value, 4, Buffer); }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
        public static void Process(byte[] Packet, Client.GameClient client)
        {
            if (client.Action != 2) return;
            Team Team = new Team();
            Team.Deserialize(Packet);
            switch (Team.Action)
            {
                case Mode.Create:
                    {
                        if (client.Entity.MaxHitpoints == 0) break;
                        client.Team = new Game.ConquerStructures.Team(client);
                        Team.Action = Mode.TeamLader;
                        client.Send(Team.Encode());
                        Team.Action = Mode.Create;
                        client.Send(Team.Encode());
                        break;
                    }
                case Mode.AcceptJoinRequest:
                    {
                        if (client.Team != null && client.Entity.Hitpoints > 0)
                        {
                            if (client.Team.AllowADD() && client.Team.TeamLider(client) && !client.Team.ForbidJoin)
                            {
                                Client.GameClient NewTeammate;
                                if (Kernel.GamePool.TryGetValue(Team.UID, out NewTeammate))
                                {
                                    if (NewTeammate.Team != null) return;
                                    NewTeammate.Team = client.Team;
                                    client.Team.Add(NewTeammate);
                                }
                                if (client.Team.PickupMoney)
                                {
                                    NewTeammate.Send(new Message("Share Silver: On", Color.Yellow, Message.Team));
                                }
                                if (!client.Team.PickupMoney)
                                {
                                    NewTeammate.Send(new Message("Share Silver: Off", Color.Yellow, Message.Team));
                                }
                                if (client.Team.PickupItems)
                                {
                                    NewTeammate.Send(new Message("Share Loots: On", Color.Yellow, Message.Team));
                                }
                                if (!client.Team.PickupItems)
                                {
                                    NewTeammate.Send(new Message("Share Loots: Off", Color.Yellow, Message.Team));
                                }
                            }
                        }
                        break;
                    }
                case Mode.AcceptInvitation:
                    {
                        if (client.Team == null && client.Entity.Hitpoints > 0)
                        {
                            Client.GameClient Leader;
                            if (Kernel.GamePool.TryGetValue(Team.UID, out Leader))
                            {
                                if (Leader.Team != null)
                                {
                                    if (!Leader.Team.AllowADD() || Leader.Team.ForbidJoin) return;
                                    client.Team = Leader.Team;
                                    Leader.Team.Add(client);
                                    if (client.Team.PickupMoney)
                                    {
                                        client.Send(new Message("Share Silver: On", Color.Yellow, Message.Team));
                                    }
                                    if (!client.Team.PickupMoney)
                                    {
                                        client.Send(new Message("Share Silver: Off", Color.Yellow, Message.Team));
                                    }
                                    if (client.Team.PickupItems)
                                    {
                                        client.Send(new Message("Share Loots: On", Color.Yellow, Message.Team));
                                    }
                                    if (!client.Team.PickupItems)
                                    {
                                        client.Send(new Message("Share Loots: Off", Color.Yellow, Message.Team));
                                    }
                                    return;
                                }
                            }
                        }
                        break;
                    }
                case Mode.InviteRequest:
                    {
                        if (client.Team != null)
                        {
                            if (client.Team.AllowADD() && client.Team.TeamLider(client))
                            {
                                Client.GameClient Inv;
                                if (Kernel.GamePool.TryGetValue(Team.UID, out Inv))
                                {
                                    if (Inv.Team == null)
                                    {
                                        PopupLevelBP Relation = new PopupLevelBP();
                                        Relation.Level = client.Entity.Level;
                                        Relation.BattlePower = (uint)client.Entity.BattlePower;
                                        Relation.Receiver = Inv.Entity.UID;
                                        Relation.Requester = client.Entity.UID;
                                        Inv.Send(Relation.Encode());
                                        Team.UID = client.Entity.UID;
                                        client.Send("[Team]The invitation has been send.");
                                        Inv.Send(Team.Encode());
                                    }
                                    else client.Send("[Team]Failed to invite. This Entity has already joined a team.");
                                }
                            }
                        }
                        break;
                    }
                case Mode.JoinRequest:
                    {
                        if (client.Team == null && client.Entity.Hitpoints > 0)
                        {
                            Client.GameClient Leader;
                            if (Kernel.GamePool.TryGetValue(Team.UID, out Leader))
                            {
                                if (Leader.Team != null)
                                {
                                    if (Leader.Team.TeamLider(Leader) && Leader.Team.AllowADD())
                                    {
                                        PopupLevelBP Relation = new PopupLevelBP();
                                        Relation.Level = client.Entity.Level;
                                        Relation.BattlePower = (uint)client.Entity.BattlePower;
                                        Relation.Receiver = Leader.Entity.UID;
                                        Relation.Requester = client.Entity.UID;
                                        Leader.Send(Relation.Encode());
                                        Team.UID = client.Entity.UID;
                                        client.Send("[Team]The request has been send.");
                                        Leader.Send(Team.Encode());
                                    }
                                    else client.Send("[Team]This team is full.");
                                }
                                else client.Send("[Team]This Entity has not created a team.");
                            }
                        }
                        break;
                    }
                case Mode.ExitTeam:
                    {
                        if (client.Entity.MapID == 16414) return;
                        if (client.Team != null)
                        {
                            client.Team.Remove(client, true);
                        }
                        break;
                    }
                case Mode.Dismiss:
                    {
                        if (client.Entity.MapID == 16414) return;
                        if (client != null && client.Team != null)
                        {
                            client.Team.Remove(client, false);
                        }
                        break;
                    }
                case Mode.Kick:
                    {
                        if (client.Entity.MapID == 16414) return;
                        if (client.Team == null) break;
                        Client.GameClient remov;
                        if (client.Team.TryGetMember(Team.UID, out remov))
                        {
                            client.Team.Remove(remov, false);
                        }
                        break;
                    }
                case Mode.ForbidJoining:
                    {
                        if (client.Team != null)
                        {
                            client.Team.SendTeam(Team.Encode(), true);
                            client.Team.ForbidJoin = true;
                        }
                        break;
                    }
                case Mode.UnforbidJoining:
                    {
                        if (client.Team != null)
                        {
                            client.Team.SendTeam(Team.Encode(), true);
                            client.Team.ForbidJoin = false;
                        }
                        break;
                    }
                case Mode.LootMoneyOff:
                    {
                        if (client.Team != null)
                        {
                            client.Team.SendTeam(Team.Encode(), true);
                            client.Team.PickupMoney = false;
                        }
                        break;
                    }
                case Mode.LootMoneyOn:
                    {
                        if (client.Team != null)
                        {
                            client.Team.SendTeam(Team.Encode(), true);
                            client.Team.PickupMoney = true;
                        }
                        break;
                    }
                case Mode.LootItemsOff:
                    {
                        if (client.Team != null)
                        {
                            client.Team.SendTeam(Team.Encode(), true);
                            client.Team.PickupItems = false;
                        }
                        break;
                    }
                case Mode.LootItemsOn:
                    {
                        if (client.Team != null)
                        {
                            client.Team.SendTeam(Team.Encode(), true);
                            client.Team.PickupItems = true;
                        }
                        break;
                    }
            }
        }
    }
    public class TeamMember : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        public TeamMember()
        {
            Buffer = new byte[180];
            Write(172, 0, Buffer);
            Write((ushort)1026, 2, Buffer);
        }
        public string Name
        {
            set
            {
                Buffer[5] = 1;
                Buffer[6] = 1;
                Buffer[7] = 1;
                Write(value, 8, Buffer);
            }
            get { return System.Text.Encoding.ASCII.GetString(Buffer, 8, 16).Trim('\0'); }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 24); }
            set { Write(value, 24, Buffer); }
        }
        public uint Mesh
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Write(value, 28, Buffer); }
        }
        public ushort MaxHitpoints
        {
            get { return BitConverter.ToUInt16(Buffer, 32); }
            set { Write(value, 32, Buffer); }
        }
        public ushort Hitpoints
        {
            get { return BitConverter.ToUInt16(Buffer, 36); }
            set { Write(value, 36, Buffer); }
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            throw new NotImplementedException();
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}
