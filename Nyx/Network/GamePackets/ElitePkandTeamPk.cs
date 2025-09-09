using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Client;
using Nyx.Server.Game;
using Nyx.Server.Network.GamePackets;
using Nyx.Server;
using Nyx.Server.Network;
using System.Collections.Concurrent;
using Nyx.Server.Game.Features.Tournaments;

namespace Nyx.Server.Network.GamePackets
{
    public class ElitePKMatchStats : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public ElitePKMatchStats()
        {
            Buffer = new byte[56 + 8];
            Writer.Write(56, 0, Buffer);
            Writer.Write(2222, 2, Buffer);
        }

        public void Append(ElitePK.Match match)
        {
            int offset = 4;
            var array = match.FightersStats;
            if (array.Length >= 2)
            {
                AppendPlayer(array[0], offset); offset += 24;
                AppendPlayer(array[1], offset);
            }
        }

        private void AppendPlayer(ElitePK.FighterStats player, int offset)
        {
            Write(player.UID, offset, Buffer); offset += 4;
            Writer.Write(player.Name, offset, Buffer); offset += 16;
            Write(player.Points, offset, Buffer);
        }

        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class ElitePKBrackets : Writer, Interfaces.IPacket
    {
        public const byte
            InitialList = 0,
            StaticUpdate = 1,
            GUIEdit = 2,
            UpdateList = 3,
            RequestInformation = 4,
            StopWagers = 5,
            EPK_State = 6;

        byte[] Buffer;

        public ElitePKBrackets(bool create, int matches = 0)
        {
            if (create)
            {
                //if (matches != 0) matches++;
                Buffer = new byte[124 + 100 * matches + 8];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(2219, 2, Buffer);
            }
        }

        public byte Type
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public byte Page
        {
            get { return Buffer[6]; }
            set { Buffer[6] = value; }
        }

        public byte Unknown
        {
            get { return Buffer[8]; }
            set { Buffer[8] = value; }
        }

        public uint MatchCount
        {
            get { return BitConverter.ToUInt32(Buffer, 10); }
            set { Write(value, 10, Buffer); }
        }

        public ushort Group
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set { Writer.Write(value, 14, Buffer); }
        }

        public ushort GUIType
        {
            get { return BitConverter.ToUInt16(Buffer, 16); }
            set { Writer.Write(value, 16, Buffer); }
        }

        public ushort TimeLeft
        {
            get { return BitConverter.ToUInt16(Buffer, 18); }
            set { Writer.Write(value, 18, Buffer); }
        }

        public uint TotalMatches
        {
            get { return BitConverter.ToUInt16(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public bool OnGoing
        {
            get { return Buffer[20] == 1; }
            set { Write(value, 20, Buffer); }
        }

        private int offset = 24;

        public void Append(ElitePK.Match match)
        {
            if (Type != GUIEdit)
            {
                Write(match.ID, offset, Buffer);
                if (match.Flag == ElitePK.Match.StatusFlag.FinalMatch)
                    match.Flag = ElitePK.Match.StatusFlag.AcceptingWagers;
            }
            else
                if (GUIType >= ElitePK.States.GUI_Top4Qualifier)
                    match.Flag = ElitePK.Match.StatusFlag.FinalMatch;

            if (GUIType >= ElitePK.States.GUI_Top4Qualifier)
                if (match.Flag == ElitePK.Match.StatusFlag.OK)
                    match.Flag = ElitePK.Match.StatusFlag.Watchable;

            offset += 4;
            Writer.Write((ushort)match.Players.Length, offset, Buffer); offset += 2;
            Writer.Write((ushort)match.Index, offset, Buffer); offset += 2;
            if (match.Players.Length == 1)
                Writer.Write((ushort)ElitePK.Match.StatusFlag.OK, offset, Buffer);
            else
                Writer.Write((ushort)match.Flag, offset, Buffer); offset += 2;
            for (int i = 0; i < match.Players.Length; i++)
            {
                AppendPlayer(match.MatchStats[i], offset);
                offset += 30;
            }
            if (Type == GUIEdit && GUIType >= ElitePK.States.GUI_Top4Qualifier)
            {
                Write(0, offset, Buffer); offset += 4;
                Write(0, offset, Buffer); offset += 2;
                Writer.Write(match.Index, offset, Buffer); offset += 4;
                Write((uint)ElitePK.Match.StatusFlag.FinalMatch, offset, Buffer); offset += 4;
                Write(Group, offset, Buffer); offset += 4;
                var stats = match.MatchStats.FirstOrDefault(p => p.Lost);
                if (stats == null) stats = match.MatchStats[0];
                Write(stats.UID, offset, Buffer); offset += 4;
                Write(4, offset, Buffer); offset += 4;
                Writer.Write(stats.Name.Substring(0, 4), offset, Buffer); offset += 4;
            }
            else
            {
                if (match.Players.Length == 2) offset += 30;
                if (match.Players.Length == 1) offset += 60;
            }
        }

        private void AppendPlayer(ElitePK.FighterStats stats, int _offset)
        {
            Write(stats.UID, _offset, Buffer); _offset += 4;
            Write(stats.Mesh, _offset, Buffer); _offset += 4;
            Writer.Write(stats.Name, _offset, Buffer); _offset += 16;
            Writer.Write((int)stats.Flag, _offset, Buffer); _offset += 4;
            Write(stats.Advance, _offset, Buffer);
        }

        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class ElitePKMatchUI : Writer, Interfaces.IPacket
    {
        public const byte
            Information = 1,
            BeginMatch = 2,
            Effect = 3,
            EndMatch = 4;

        public const uint
            Effect_Win = 1,
            Effect_Lose = 0;

        byte[] Buffer;

        public ElitePKMatchUI(bool create)
        {
            if (create)
            {
                Buffer = new byte[52 + 8];
                Writer.Write(52, 0, Buffer);
                Writer.Write(2218, 2, Buffer);
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint OpponentUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public string OpponentName
        {
            get { return Program.Encoding.GetString(Buffer, 16, 16).Trim(); }
            set { Writer.Write(value, 16, Buffer); }
        }

        public uint TimeLeft
        {
            get { return BitConverter.ToUInt32(Buffer, 44); }
            set { Write(value, 44, Buffer); }
        }

        public void Append(GameClient opponent)
        {
            OpponentUID = opponent.Entity.UID;
            OpponentName = opponent.Entity.Name;
        }

        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class ElitePKRanking : Writer, Interfaces.IPacket
    {
        public const byte Top8 = 0, Top3 = 2;

        private byte[] buffer;
        public ElitePKRanking(bool create)
        {
            if (create)
            {
                buffer = new byte[356];
                Writer.Write(348, 0, buffer);
                Writer.Write(2223, 2, buffer);
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(buffer, 4); }
            set { Write(value, 4, buffer); }
        }

        public uint Group
        {
            get { return BitConverter.ToUInt32(buffer, 8); }
            set { Write(value, 8, buffer); }
        }

        public uint GroupStatus
        {
            get { return BitConverter.ToUInt32(buffer, 12); }
            set { Write(value, 12, buffer); }
        }

        public uint Count
        {
            get { return BitConverter.ToUInt32(buffer, 16); }
            set { Write(value, 16, buffer); }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(buffer, 20); }
            set { Write(value, 20, buffer); }
        }

        private int Index = 0;
        public void Append(ElitePK.FighterStats stats, int rank)
        {
            int offset = 32 + Index * 36; Index++;
            Writer.Write(rank, offset, buffer); offset += 4;
            Writer.Write(stats.Name, offset, buffer); offset += 16;
            Write(stats.Mesh, offset, buffer); offset += 4;
            Write(stats.UID, offset, buffer);
        }

        public byte[] Encode()
        {
            return buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Encode());
        }


        public void Deserialize(byte[] buffer)
        {
            this.buffer = buffer;
        }
    }
    public class ElitePKWager : Writer, Interfaces.IPacket
    {
        public const byte
            SetWager = 2;

        byte[] Buffer;

        public ElitePKWager()
        {
        }

        public byte Type
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint WagedUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint Wager
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class ElitePKWagersList : Writer, Interfaces.IPacket
    {
        public const byte
            SendList = 3,
            RequestList = 4;

        byte[] Buffer;

        public ElitePKWagersList(bool create, int matchcount = 0)
        {
            if (create)
            {
                Buffer = new byte[12 + matchcount * 16 + 8];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(1065, 2, Buffer);
            }
        }

        public byte Group
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public byte Page //not sure.
        {
            get { return Buffer[6]; }
            set { Buffer[6] = value; }
        }

        public uint TotalMatches
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint WagedUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public void Append(ElitePK.Match match, int index)
        {
            int offset = 12 + index * 16;
            var array = match.FightersStats;
            if (array.Length == 2)
            {
                Write(array[0].UID, offset, Buffer); offset += 4;
                Write(array[1].UID, offset, Buffer); offset += 4;
                Write(array[0].Wager, offset, Buffer); offset += 4;
                Write(array[1].Wager, offset, Buffer); offset += 4;
            }
        }

        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class ElitePKWatch : Writer, Interfaces.IPacket
    {
        public const byte
            RequestView = 0,
            Watchers = 2,
            Leave = 3,
            Fighters = 4;

        byte[] Buffer;

        public ElitePKWatch(bool create, int watchers = 0)
        {
            if (create)
            {
                Buffer = new byte[26 + watchers * 36 + 8];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(2211, 2, Buffer);
                WatcherCount = 2;
            }
        }

        public byte Type
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 6); }
            set { Write(value, 6, Buffer); }
        }

        public uint ID
        {
            get { return BitConverter.ToUInt32(Buffer, 10); }
            set { Write(value, 10, Buffer); }
        }

        public uint WatcherCount
        {
            get { return BitConverter.ToUInt32(Buffer, 14); }
            set { Write(value, 14, Buffer); }
        }

        public uint dwCheers1
        {
            get { return BitConverter.ToUInt32(Buffer, 18); }
            set { Write(value, 18, Buffer); }
        }

        public uint dwCheers2
        {
            get { return BitConverter.ToUInt32(Buffer, 22); }
            set { Write(value, 22, Buffer); }
        }

        int index = 0;
        public void Append(string name)
        {
            int offset = 26 + index * 32;
            Writer.Write(name, offset, Buffer);
            index++;
        }
        public void Append(uint mesh, string name)
        {
            int offset = 26 + index * 36;
            Write(mesh, offset, Buffer); offset += 4;
            Writer.Write(name, offset, Buffer);
            index++;
        }

        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class SkillEliteSetTeamName : Writer
    {
        byte[] packet;
        public byte[] Encode() { return packet; }
        public const byte Apprend = 0, SuccessfulName = 1, RenameWasSuccessfulName = 2, Remove = 3;
        public SkillEliteSetTeamName(Game.Features.Tournaments.TeamElitePk.GamePackets ID)
        {
            packet = new byte[56];
            Writer.Write(48, 0, packet);
            if (ID == Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePkBrackets)
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.SkillEliteSetTeamName, 2, packet);
            else
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.TeamEliteSetTeamName, 2, packet);

        }

        public uint Type { get { return BitConverter.ToUInt32(packet, 4); } set { Write(value, 4, packet); } }
        public uint TeamID { get { return BitConverter.ToUInt32(packet, 8); } set { Write(value, 8, packet); } }
        public string TeamName
        {
            get { return PacketHandler.ReadString(packet, 12, 32); }
            set { Writer.Write(value, 12, packet); }
        }
    }
    public class TeamElitePkBrackets
    {
        public const byte
            InitialList = 0,
            StaticUpdate = 1,
            GUIEdit = 2,
             UpdateList = 3,
            RequestInformation = 4,
            EPK_State = 5;//6

        byte[] packet;
        public byte[] Encode()
        {
            return packet;
        }
        public TeamElitePkBrackets(Game.Features.Tournaments.TeamElitePk.GamePackets ID, int extraLength = 0)
        {
            if (extraLength > 5)
                return;
            packet = new byte[(int)((ushort)(190 + extraLength * 154))];
            Writer.Write((ushort)(packet.Length - 8), 0, packet);
            Writer.Write((ushort)ID, 2, packet);
        }

        public byte Type
        {
            get { return packet[4]; }
            set { Writer.Write(value, 4, packet); }
        }

        public byte Page
        {
            get { return packet[6]; }
            set { Writer.Write(value, 6, packet); }
        }

        public byte ListCount
        {
            get { return packet[8]; }
            set { Writer.Write(value, 8, packet); }
        }

        public uint MatchCount
        {
            get { return BitConverter.ToUInt32(packet, 10); }
            set { Writer.Write(value, 10, packet); }
        }

        public ushort Group
        {
            get { return BitConverter.ToUInt16(packet, 14); }
            set { Writer.Write(value, 14, packet); }
        }

        public ushort GUIType
        {
            get { return BitConverter.ToUInt16(packet, 16); }
            set { Writer.Write(value, 16, packet); }
        }

        public ushort TimeLeft
        {
            get { return BitConverter.ToUInt16(packet, 18); }
            set { Writer.Write(value, 18, packet); }
        }

        public uint TotalMatchesOnRoom
        {
            get { return BitConverter.ToUInt32(packet, 20); }
            set { Writer.Write(value, 20, packet); }
        }
        public bool OnGoing
        {
            get { return packet[20] == 1; }
            set { Writer.Write((byte)(value ? 1 : 0), 20, packet); }
        }
        private ushort offset = 24;
        public void Append(TeamElitePk.Match match)
        {
            if (Type != GUIEdit)
                Writer.Write(match.ID, offset, packet);

            offset += 4;
            Writer.Write((ushort)match.MatchStats.Length, offset, packet); offset += 2;
            Writer.Write((ushort)match.Index, offset, packet); offset += 2;
            if (match.MatchStats.Length == 1)
                Writer.Write((ushort)TeamElitePk.Match.StatusFlag.OK, offset, packet);
            else
                Writer.Write((ushort)match.Flag, offset, packet);

            offset += 2;

            for (int i = 0; i < match.MatchStats.Length; i++)
            {
                AppendPlayer(match.MatchStats[i], offset);
                offset += 48;
            }
            if (match.MatchStats.Length == 2) offset += 48;
            if (match.MatchStats.Length == 1) offset += 96;

        }
        private void AppendPlayer(TeamElitePk.FighterStats stats, ushort _offset)
        {
            Writer.Write(stats.Team.UID, _offset, packet); _offset += 4;
            Writer.Write(stats.LeaderUID, _offset, packet); _offset += 4;
            Writer.Write(stats.LeaderMesh, _offset, packet); _offset += 4;
            Writer.Write(stats.Name, _offset, packet); _offset += 32;
            Writer.Write((uint)stats.Flag, _offset, packet); _offset += 4;
        }
        public void Send(GameClient client)
        {
            client.Send(packet);
        }



        public void Deserialize(byte[] buffer)
        {
            packet = buffer;
        }

    }
    public class TeamElitePKMatchStats : Writer
    {
        byte[] packet;
        public TeamElitePKMatchStats(Game.Features.Tournaments.TeamElitePk.GamePackets ID)
        {
            packet = new byte[128 + 8];
            Writer.Write(128, 0, packet);
            if (ID == Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePkBrackets)
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePKMatchStats, 2, packet);
            else
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.TeamElitePKMatchStats, 2, packet);

        }
        public void Append(Game.Features.Tournaments.TeamElitePk.Match match)
        {
            ushort offset = 4;
            var array = match.MatchStats;
            AppendPlayer(array[0], offset); offset += 60;//60
            AppendPlayer(array[1], offset);
        }
        public uint val1 = 0;
        public uint val2 = 0;
        public uint val3 = 0;
        public uint val4 = 0;

        private void AppendPlayer(Game.Features.Tournaments.TeamElitePk.FighterStats team, ushort offset)
        {
            Write(team.LeaderUID, offset, packet); offset += 4;
            Write(team.Team.UID, offset, packet); offset += 4;
            Writer.Write(team.Name, offset, packet); offset += 32;//???
            Write(val1, offset, packet);
            offset += 4;
            Write(val2, offset, packet);
            offset += 4;
            Write(val3, offset, packet);
            offset += 4;
            Write(val4, offset, packet);
            offset += 4;
            Write(team.Points, offset, packet);
        }
        public void Send(GameClient client)
        {
            client.Send(packet);
        }

        public byte[] Encode()
        {
            return packet;
        }

        public void Deserialize(byte[] buffer)
        {
            packet = buffer;
        }

    }
    public class TeamElitePKMatchUI : Writer
    {
        byte[] packet;

        public const byte
            BeginMatch = 2,
            Effect = 3,
            EndMatch = 4,
            Information = 7,
            Reward = 8;

        public const uint
            Effect_Win = 1,
            Effect_Lose = 0;
        public TeamElitePKMatchUI(Game.Features.Tournaments.TeamElitePk.GamePackets ID)
        {
            packet = new byte[60];
            Writer.Write(52, 0, packet);
            if (ID == Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePkBrackets)
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePKMatchUI, 2, packet);
            else
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.TeamElitePKMatchUI, 2, packet);

        }
        public uint Type
        {
            get { return BitConverter.ToUInt32(packet, 4); }
            set { Write(value, 4, packet); }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(packet, 8); }
            set { Write(value, 8, packet); }
        }

        public uint OpponentUID
        {
            get { return BitConverter.ToUInt32(packet, 12); }
            set { Write(value, 12, packet); }
        }

        public string OpponentName
        {
            get { return PacketHandler.ReadString(packet, 16, 16); }
            set { Writer.Write(value, 16, packet); }
        }

        public uint TimeLeft
        {
            get { return BitConverter.ToUInt32(packet, 44); }
            set { Write(value, 44, packet); }
        }

        public void Append(Game.ConquerStructures.Team opponent)
        {
            OpponentUID = opponent.UID;
            if (opponent.EliteFighterStats != null)
                OpponentName = opponent.EliteFighterStats.Name;

        }
        public void Send(GameClient client)
        {
            client.Send(packet);
        }

        public byte[] Encode()
        {
            return packet;
        }

        public void Deserialize(byte[] buffer)
        {
            packet = buffer;
        }

    }
    public class TeamElitePkTop : Writer
    {
        byte[] packet;
        public const byte Top3 = 2, Top8 = 0;
        public TeamElitePkTop(Game.Features.Tournaments.TeamElitePk.GamePackets ID)
        {
            packet = new byte[428];
            Writer.Write((ushort)(packet.Length - 8), 0, packet);
            if (ID == Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePkBrackets)
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.SkillElitePkTop, 2, packet);
            else
                Writer.Write((ushort)Game.Features.Tournaments.TeamElitePk.GamePackets.TeamElitePkTop, 2, packet);

        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(packet, 4); }
            set { Write(value, 4, packet); }
        }//top 3, top 8
        public uint Group
        {
            get { return BitConverter.ToUInt32(packet, 8); }
            set { Write(value, 8, packet); }
        }//8
        public uint GroupStatus
        {
            get { return BitConverter.ToUInt32(packet, 12); }
            set { Write(value, 12, packet); }
        }
        public uint Count
        {
            get { return BitConverter.ToUInt32(packet, 16); }
            set { Write(value, 16, packet); }
        }
        public uint ClientUID
        {
            get { return BitConverter.ToUInt32(packet, 20); }
            set { Write(value, 20, packet); }
        }

        private int Index = 0;
        public void Append(TeamElitePk.FighterStats stats, int rank)
        {
            ushort offset = (ushort)(20 + Index * 44); Index++;
            Write(stats.LeaderUID, offset, packet); offset += 4;
            Write((uint)rank, offset, packet); offset += 4;
            Writer.Write(stats.Name, offset, packet); offset += 32;
            Write(stats.LeaderMesh, offset, packet); offset += 4;
        }
        public void Send(GameClient client)
        {
            client.Send(packet);
        }

        public byte[] Encode()
        {
            return packet;
        }

        public void Deserialize(byte[] buffer)
        {
            packet = buffer;
        }
    }
}
namespace Nyx.Server.Game.Features.Tournaments
{
    public class ElitePkStatus
    {
        public void GetSkillElitePkReward(Client.GameClient client, byte Rank)
        {
            uint ITEM_ID = 0;
            switch (Rank)
            {
                case 0:
                    ITEM_ID = (uint)(721300 + GetEliteGroup(client.Entity.Level));
                    break;
                case 1:
                    ITEM_ID = (uint)(721304 + GetEliteGroup(client.Entity.Level));
                    break;
                case 2:
                    ITEM_ID = (uint)(721308 + GetEliteGroup(client.Entity.Level));
                    break;
                default:
                    ITEM_ID = (uint)(721312 + GetEliteGroup(client.Entity.Level));
                    break;

            }
            client.Inventory.Add(ITEM_ID, 0, 1);
        }
        public void GetTeamElitePkReward(Client.GameClient client, byte Rank)
        {
            uint ITEM_ID = 0;
            switch (Rank)
            {
                case 0:
                    ITEM_ID = (uint)(720794 + GetEliteGroup(client.Entity.Level));
                    break;
                case 1:
                    ITEM_ID = (uint)(720798 + GetEliteGroup(client.Entity.Level));
                    break;
                case 2:
                    ITEM_ID = (uint)(720802 + GetEliteGroup(client.Entity.Level));
                    break;
                default:
                    ITEM_ID = (uint)(720806 + GetEliteGroup(client.Entity.Level));
                    break;

            }
            client.Inventory.Add(ITEM_ID, 0, 1);
        }

        public static byte[] BlockOpenArena = new byte[] { 20, 0, 86, 4, 255, 87, 14, 0, 160, 187, 13, 0, 103, 0, 21, 64, 204, 17, 0, 0, 84, 81, 83, 101, 114, 118, 101, 114 };

        public byte GetEliteGroup(byte level)
        {
            if (level >= 130)
                return 3;
            if (level >= 120)
                return 3;
            if (level >= 100)
                return 1;
            else
                return 0;
        }

        public class States
        {
            public const byte
                GUI_Top8Ranking = 0,
                GUI_Knockout = 3,
                GUI_Top8Qualifier = 4,
                GUI_Top4Qualifier = 5,
                GUI_Top2Qualifier = 6,
                GUI_Top3 = 7,
                GUI_Top1 = 8,
                GUI_ReconstructTop = 9;

            public const byte
                T_Organize = 0,
                T_CreateMatches = 1,
                T_Wait = 2,
                T_Fights = 3,
                T_Finished = 4,
                T_ReOrganize = 5;
        }

    }
    public class TeamElitePk : ElitePkStatus
    {
        public enum GamePackets : ushort
        {
            SkillEliteSetTeamName = 2260,
            SkillElitePkTop = 2253,
            SkillElitePkBrackets = 2252,
            SkillElitePKMatchStats = 2251,
            SkillElitePKMatchUI = 2250,
            TeamEliteSetTeamName = 2240,
            TeamElitePkTop = 2233,
            TeamElitePkBrackets = 2232,
            TeamElitePKMatchStats = 2231,
            TeamElitePKMatchUI = 2230,
        }
        public class TeamTournament
        {
            public static uint WaitingAreaID = 2068;
            public static TeamElitePk[] Tournaments;
            public static bool Opened = false;
            public static void Create()
            {
                try
                {
                    Tournaments = new TeamElitePk[4];
                    for (byte x = 0; x < Tournaments.Length; x++)
                        Tournaments[x] = new TeamElitePk(x, GamePackets.TeamElitePkBrackets);

                    //LoadTop(Tournaments, "TeamElitePkTop8");
                }
                catch (Exception)
                {
                   
                }

            }
            public static bool GetReward(Client.GameClient client, out byte rank, out byte elitestage)
            {
                if (Tournaments != null)

                    for (byte x = 0; x < Tournaments.Length; x++)
                    {
                        if (Tournaments[x] != null)
                        {
                            if (Tournaments[x].Top8 != null)
                            {
                                for (byte i = 0; i < Tournaments[x].Top8.Length; i++)
                                {
                                    if (Tournaments[x].Top8[i] != null)
                                    {
                                        if (Tournaments[x].Top8[i].LeaderUID == client.Entity.UID)
                                        {

                                            if (Tournaments[x].Top8[i].RceiveReward == 0)
                                            {
                                                rank = (byte)(i + 1);
                                                elitestage = x;
                                                Tournaments[x].Top8[i].RceiveReward = 1;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                //   TeamElitePk.TeamTournament.Save();
                rank = 0;
                elitestage = 0;
                return false;
            }
            public static void Open()
            {
                if (!Opened)
                {
                    Opened = true;
                    for (byte x = 0; x < Tournaments.Length; x++)
                    {
                        Tournaments[x].SubscribeTimer(1);
                        Tournaments[x].AllowJoin = true;
                    }
                }
            }
            public static bool Join(Client.GameClient client, byte Grope)
            {
                if (client.Team == null)
                    return false;

                if (Grope <= 3)
                {
                    client.Team.SetEliteFighterStats(true);
                    return Tournaments[Grope].SignUp(client);
                }
                return false;
            }
        }
        public class SkillTeamTournament
        {
            public static bool GetReward(Client.GameClient client, out byte rank, out byte elitestage)
            {
                if (Tournaments != null)

                    for (byte x = 0; x < Tournaments.Length; x++)
                    {
                        if (Tournaments[x] != null)
                        {
                            if (Tournaments[x].Top8 != null)
                            {
                                for (byte i = 0; i < Tournaments[x].Top8.Length; i++)
                                {
                                    if (Tournaments[x].Top8[i] != null)
                                    {
                                        if (Tournaments[x].Top8[i].LeaderUID == client.Entity.UID)
                                        {

                                            if (Tournaments[x].Top8[i].RceiveReward == 0)
                                            {
                                                rank = (byte)(i + 1);
                                                elitestage = x;
                                                Tournaments[x].Top8[i].RceiveReward = 1;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                rank = 0;
                elitestage = 0;
                return false;
            }
            public static uint WaitingAreaID = 2068;
            public static TeamElitePk[] Tournaments;
            public static bool Opened = false;
            public static void Create()
            {
                Tournaments = new TeamElitePk[4];
                for (byte x = 0; x < Tournaments.Length; x++)
                    Tournaments[x] = new TeamElitePk(x, GamePackets.SkillElitePkBrackets);
            }
            public static void Open()
            {
                if (!Opened)
                {
                    Opened = true;
                    for (byte x = 0; x < Tournaments.Length; x++)
                    {
                        Tournaments[x].SubscribeTimer(1);
                        Tournaments[x].AllowJoin = true;
                    }
                }
            }
            public static bool Join(Client.GameClient client, byte Grope)
            {
                if (client.Team == null)
                    return false;

                if (Grope <= 3)
                {
                    client.Team.SetEliteFighterStats(true);
                    return Tournaments[Grope].SignUp(client);
                }
                return false;
            }
        }
        public static void SaveTop(TeamElitePk[] elit, string Ini_file)
        {
            if (elit == null) return;
            using (Database.Write _wr = new Database.Write("database//" + Ini_file + ".ini"))
            {
                uint count = 0;
                for (byte x = 0; x < elit.Length; x++)
                    if (elit[x].Top8 != null)
                        count += (byte)elit[x].Top8.Length;
                string[] lines = new string[count];
                uint amount = 0;
                for (byte x = 0; x < elit.Length; x++)
                {
                    if (elit[x].Top8 != null)
                        for (byte i = 0; i < elit[x].Top8.Length; i++)
                        {
                            lines[amount] = x + "^" + elit[x].Top8[i].ToString();
                            amount++;
                        }
                }

                _wr.Add(lines, lines.Length).Execute(Database.Mode.Open);
            }
        }
        public static void LoadTop(TeamElitePk[] elit, string Ini_file)
        {
            try
            {
                using (Database.Read read = new Database.Read("database//" + Ini_file + ".ini"))
                {
                    if (read.Reader())
                    {
                        byte[] counts = new byte[4];
                        byte group = 0;
                        while (read.UseRead())
                        {
                            string[] line = read.ReadString("").Split('^');
                            group = byte.Parse(line[0]);
                            if (elit[group].Top8 == null)
                                elit[group].Top8 = new FighterStats[8];
                            if (elit[group].Top8.Length < 8)
                                elit[group].Top8 = new FighterStats[8];

                            FighterStats fight = new FighterStats(0, "", 0, null);
                            fight.Load(line[1]);
                            elit[group].Top8[counts[group]] = fight;
                            counts[group]++;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        public class FighterStats
        {
            public enum StatusFlag : int
            {
                None = 0,
                Fighting = 2,
                Lost = 4,
                Qualified = 3,
                Waiting = 1,
            }

            public Game.ConquerStructures.Team Team;

            public string Name;
            public uint LeaderUID;
            public uint LeaderMesh;

            public bool OnNextMatch = false;
            public uint Cheers;
            public uint Points;
            public StatusFlag Flag;
            public bool Teleported;

            public byte RceiveReward;
            public byte Title;
            public byte Rank;

            public bool Waiting { get { return Flag == StatusFlag.Waiting; } }
            public bool NoFlag { get { return Flag == StatusFlag.None; } }
            public bool Fighting { get { return Flag == StatusFlag.Fighting; } }
            public bool Winner { get { return Flag == StatusFlag.Qualified; } }
            public bool Lost { get { return Flag == StatusFlag.Lost; } }


            public FighterStats(uint id, string name, uint mesh, Game.ConquerStructures.Team temate)
            {
                LeaderUID = id;
                Name = name;
                LeaderMesh = mesh;
                Team = temate;
            }
            public void Reset(bool noflag = false)
            {
                Points = 0;
                Cheers = 0;
                if (!noflag)
                {
                    if (!Lost)
                        Flag = StatusFlag.Waiting;
                }
            }
            public FighterStats Clone()
            {
                FighterStats stats = new FighterStats(LeaderUID, Name, LeaderMesh, Team);
                stats.Points = this.Points;
                stats.Flag = this.Flag;

                return stats;
            }
            public override string ToString()
            {
                if (Name.Contains('^'))
                    Name.Replace("^", "");
                if (Name.Contains('#'))
                    Name.Replace('#', ' ');
                StringBuilder build = new StringBuilder();
                build.Append(LeaderUID + "#" + Name + "#" + LeaderMesh + "#" + Rank + "#" + Title + "#" + RceiveReward + "");
                return build.ToString();
            }
            public void Load(string Line)
            {
                string[] data = Line.Split('#');
                LeaderUID = uint.Parse(data[0]);
                Name = data[1];
                LeaderMesh = uint.Parse(data[2]);
                Rank = byte.Parse(data[3]);
                Title = byte.Parse(data[4]);
                RceiveReward = byte.Parse(data[5]);
            }
        }

        public class Match
        {
            public enum StatusFlag : int
            {
                SwitchOut = 0,
                Watchable = 1,
                Waiting = 3,
                OK = 2,
            }
            public IEnumerable<Client.GameClient> Players
            {
                get
                {

                    foreach (var team in MatchStats)
                        foreach (var player in team.Team.Players)
                            yield return player;
                }
            }
            public FighterStats[] MatchStats;
            public GamePackets T_ID;
            public bool OnGoing { get { return Flag == StatusFlag.Watchable; } }
            public int GroupID { get { return (int)ID / 100000 - 1; } }

            public uint TimeLeft
            {
                get
                {
                    int val = 0;
                    if (MatchStats.Length == 1)
                        val = 0;
                    else
                        val = (int)((ImportTime.AddMinutes(4).TotalMilliseconds - Time32.Now.TotalMilliseconds) / 1000);
                    if (val < 0)
                        val = 0;
                    return (uint)val;
                }
            }
            public void Commit()
            {
                var matchStats = new FighterStats[MatchStats.Length];
                for (int i = 0; i < matchStats.Length; i++)
                {
                    matchStats[i] = MatchStats[i].Clone();
                }
                MatchStats = matchStats;
            }
            public ConcurrentDictionary<uint, Client.GameClient> Watchers;

            public FighterStats Return(Func<FighterStats, bool> fn)
            {
                foreach (var stat in MatchStats)
                    if (stat != null)
                        if (fn(stat))
                        {
                            if (T_ID == GamePackets.SkillElitePkBrackets)
                                return SkillTeamTournament.Tournaments[GroupID].Teams[stat.Team.UID].EliteFighterStats;
                            else
                                return TeamTournament.Tournaments[GroupID].Teams[stat.Team.UID].EliteFighterStats;
                            // return ElitePKTournament.Tournaments[GroupID].Players[stat.UID];
                        }
                MatchStats[0].Flag = FighterStats.StatusFlag.Qualified;
                return MatchStats[0];
                //return null;
            }

            public uint ID;

            public StatusFlag Flag;
            public bool Inside;
            public bool Done;
            public bool Exported;

            public bool Completed;
            public ushort Index;

            public Time32 ImportTime;
            public Map Map;
            private Time32 DoneStamp;

            private int Imports;


            public void AliveMatch()
            {

                if (Flag != StatusFlag.OK)
                {
                    if (Inside && !Done)
                    {
                        foreach (var dismised in MatchStats)
                        {
                            if (Flag != StatusFlag.OK)
                            {
                                if (MatchStats.Length == 2)
                                {
                                    if (MatchStats[0].Winner)
                                        return;
                                    if (MatchStats[1].Winner)
                                        return;
                                }
                                if (!dismised.Team.OnPopulates)
                                {
                                    if (dismised.Waiting)
                                    {
                                        dismised.Flag = FighterStats.StatusFlag.Lost;
                                        dismised.Team.EliteFighterStats.Flag = FighterStats.StatusFlag.Lost;
                                    }
                                    if (!dismised.Lost)
                                    {
                                        this.End(dismised.Team);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public Match(GamePackets ID, params FighterStats[] teamates)
            {
                try
                {
                    Map = Kernel.Maps[700].MakeDynamicMap();
                    T_ID = ID;
                    if (teamates.Length == 1)
                    {
                        Flag = StatusFlag.OK;
                        MatchStats = new FighterStats[1];
                        MatchStats[0] = teamates[0].Team.EliteFighterStats;
                        MatchStats[0].Team.EliteMatch = this;
                        MatchStats[0].Points = 0;
                        MatchStats[0].Cheers = 0;
                        MatchStats[0].Flag = FighterStats.StatusFlag.Qualified;
                        Flag = StatusFlag.OK;
                    }
                    else
                    {
                        Watchers = new ConcurrentDictionary<uint, Client.GameClient>();
                        MatchStats = new FighterStats[teamates.Length];
                        for (int i = 0; i < teamates.Length; i++)
                        {
                            MatchStats[i] = teamates[i].Team.EliteFighterStats;
                            MatchStats[i].Team.EliteMatch = this;
                            MatchStats[i].Flag = FighterStats.StatusFlag.Waiting;
                            MatchStats[i].Points = 0;
                            MatchStats[i].Cheers = 0;
                            MatchStats[i].Teleported = false;

                        }
                        if (MatchStats.Length == 3)
                            MatchStats[0].Flag = FighterStats.StatusFlag.None;

                        Imports = 0;
                        Flag = StatusFlag.Waiting;
                    }
                }
                catch (Exception) {  }
            }
            public void Import(StatusFlag flag = StatusFlag.OK)
            {
                Imports++;
                Exported = Done = false;
                Inside = true;
                Flag = flag;
                ImportTime = Time32.Now;

                if (MatchStats.Length > 1)
                {
                    Game.ConquerStructures.Team team1 = null, team2 = null;
                    for (int i = 0; i < MatchStats.Length; i++)
                        if (!MatchStats[i].NoFlag && !MatchStats[i].Lost)
                            if (team1 == null)
                                team1 = MatchStats[i].Team;
                            else
                                team2 = MatchStats[i].Team;

                    team1.EliteFighterStats.Flag = FighterStats.StatusFlag.Fighting;
                    team2.EliteFighterStats.Flag = FighterStats.StatusFlag.Fighting;
                    importPlayer(team1, team2);
                    importPlayer(team2, team1);

                    Network.GamePackets.TeamElitePkBrackets brackets = new Network.GamePackets.TeamElitePkBrackets(T_ID, 1);
                    {
                        brackets.Group = (ushort)GroupID;
                        brackets.GUIType = (ushort)ElitePKTournament.Tournaments[GroupID].State;
                        brackets.TotalMatchesOnRoom = brackets.MatchCount = 1;
                        brackets.Type = Network.GamePackets.TeamElitePkBrackets.StaticUpdate;
                        brackets.Append(this);
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(brackets.Encode());
                    }
                }
                else
                {
                    Done = true;
                    Flag = StatusFlag.OK;
                    Exported = true;
                    Completed = true;
                    MatchStats[0].Flag = FighterStats.StatusFlag.Qualified;
                }
            }
            private void importPlayer(Game.ConquerStructures.Team teamone, Game.ConquerStructures.Team opponent)
            {
                if (MatchStats.Length > 1)
                {
                    teamone.EliteFighterStats.Teleported = true;
                    opponent.EliteFighterStats.Teleported = true;
                    //ushort x = 0; ushort y = 0;
                    foreach (var player in teamone.Players)
                    {
                        if (player.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Ride))
                        {
                            player.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                        }
                        if (T_ID == GamePackets.SkillElitePkBrackets)
                        {
                            //  player.LoadItemStats();
                            player.Entity.InSkillPk = true;
                        }
                        var coords = Map.RandomCoordinates();
                        player.Entity.Teleport(Map.BaseID, Map.ID, coords.Item1, coords.Item2);
                        player.Entity.BringToLife();
                        player.Entity.SkillTeamWatchingElitePKMatch = null;

                        if (T_ID == GamePackets.SkillElitePkBrackets)
                            player.LoadItemStats();//.MyEquip.SendAllItems(player.Player.MyEquip.aleternante);
                        Network.GamePackets.TeamElitePKMatchUI ui = new Network.GamePackets.TeamElitePKMatchUI(T_ID);
                        {
                            ui.Append(opponent);
                            ui.TimeLeft = TimeLeft;
                            ui.Type = Network.GamePackets.TeamElitePKMatchUI.BeginMatch;
                            player.Send(ui.Encode());
                        }
                        player.Send(CreateUpdate().Encode());
                        player.CantAttack = Time32.Now.AddSeconds(11);
                        player.Entity.PrevPKMode = player.Entity.PKMode;
                        player.Entity.PKMode = Enums.PKMode.Team;

                        Network.GamePackets.Data dat = new Network.GamePackets.Data(true);
                        {
                            dat.UID = player.Entity.UID;
                            dat.ID = Network.GamePackets.Data.ChangePKMode;
                            dat.dwParam = (uint)player.Entity.PKMode;
                            player.Send(dat.Encode());
                        }
                        //player.Entity.OnDeath = p => { p.Owner.Team.EliteMatch.End(p.Owner.Team); };
                    }
                }
                else
                {
                    MatchStats[0].Flag = FighterStats.StatusFlag.Qualified;
                    Done = true;
                    Flag = StatusFlag.OK;
                    Exported = true;
                    Completed = true;
                }
            }
            public void End(Game.ConquerStructures.Team team)
            {
                try
                {

                    if (MatchStats.Length == 1)
                    {
                        MatchStats[0].Flag = FighterStats.StatusFlag.Qualified;
                        Flag = StatusFlag.OK;
                        return;
                    }
                    if (Done) return;
                    Done = true;
                    DoneStamp = Time32.Now;
                    Flag = StatusFlag.OK;
                    team.EliteFighterStats.Flag = FighterStats.StatusFlag.Lost;
                    Network.GamePackets.TeamElitePKMatchUI ui = new Network.GamePackets.TeamElitePKMatchUI(T_ID);
                    {
                        ui.Type = Network.GamePackets.ElitePKMatchUI.Effect;
                        ui.dwParam = Network.GamePackets.ElitePKMatchUI.Effect_Lose;
                        team.SendMesageTeam(ui.Encode(), 0);

                        var team_win = targetOfWin(team);
                        if (team_win != null)
                        {
                            ui.Append(team_win.Team);
                            ui.dwParam = Network.GamePackets.TeamElitePKMatchUI.Effect_Win;

                            foreach (var target in team_win.Team.Players)
                            {
                                target.Entity.InSkillPk = false;
                                target.Send(ui.Encode());
                                ui.Type = Network.GamePackets.TeamElitePKMatchUI.EndMatch;
                                team.SendMesageTeam(ui.Encode(), 0);
                                target.Send(ui.Encode());

                                if (T_ID == GamePackets.SkillElitePkBrackets)
                                    target.Inventory.Add(720981, 0, 1);

                                if (target.Team.EliteFighterStats.Fighting)
                                    target.Team.EliteMatch.End(target.Team);


                                ui.Type = Network.GamePackets.TeamElitePKMatchUI.Reward;
                                target.Send(ui.Encode());

                                if (Imports == 2 || MatchStats.Length != 3 || MatchStats[0] == null)
                                {
                                    team_win.Flag = FighterStats.StatusFlag.Qualified;
                                }
                                else
                                {
                                    var dictionar = MatchStats.Where(p => !p.Lost).ToArray();
                                    if (dictionar.Length == 2)
                                    {
                                        Flag = StatusFlag.SwitchOut;
                                        team_win.Flag = FighterStats.StatusFlag.Waiting;
                                        team_win.OnNextMatch = true;
                                    }
                                    else
                                        team_win.Flag = FighterStats.StatusFlag.Qualified;
                                }
                            }

                        }
                    }
                    Network.GamePackets.TeamElitePkBrackets brackets = new Network.GamePackets.TeamElitePkBrackets(T_ID, 1);
                    {
                        brackets.Group = (ushort)GroupID;
                        brackets.GUIType = (ushort)ElitePKTournament.Tournaments[GroupID].State;
                        brackets.TotalMatchesOnRoom = brackets.MatchCount = 1;
                        brackets.Type = Network.GamePackets.TeamElitePkBrackets.StaticUpdate;
                        brackets.Append(this);
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(brackets.Encode());
                    }
                    foreach (var clien in Watchers.Values)
                        if (clien.Entity.MapID == Map.ID)
                            LeaveWatch(clien);
                }
                catch (Exception) {  }
            }
            public void Export()
            {
                try
                {
                    if (Time32.Now > DoneStamp.AddSeconds(3) || TimeLeft == 0)
                    {
                        #region !Done
                        if (!Done)
                        {
                            if (MatchStats.Length == 1)
                            {
                                MatchStats[0].Flag = FighterStats.StatusFlag.Qualified;
                                Flag = StatusFlag.OK;
                                Exported = true;

                                Exported = true;
                                Completed = true;
                                return;
                            }
                            else
                            {
                                var fighters = MatchStats.Where(p => p.Fighting).ToArray();
                                if (fighters.Length > 1)
                                {
                                    if (fighters[0].Fighting && fighters[1].Fighting)
                                    {
                                        if (fighters[0].Points == fighters[1].Points)
                                            End(fighters.First(p => p.Team.UID == fighters[0].Team.UID).Team);
                                        else
                                        {
                                            if (fighters[0].Points > fighters[1].Points)
                                                End(fighters.First(p => p.Team.UID == fighters[1].Team.UID).Team);
                                            else
                                                End(fighters.First(p => p.Team.UID == fighters[0].Team.UID).Team);
                                        }
                                    }
                                }
                                else if (fighters.Length == 1)
                                    End(fighters.First(p => p.Team.UID == fighters[0].Team.UID).Team);
                            }
                        }
                        #endregion
                        foreach (var teams in MatchStats)
                        {
                            if (teams != null)
                            {
                                if (!teams.Waiting && teams.Teleported || teams.OnNextMatch)
                                {
                                    foreach (var player in teams.Team.Players)
                                    {

                                        if (player.Entity.MapID == Map.ID)
                                        {
                                            player.Entity.InSkillPk = false;
                                            if (T_ID == GamePackets.TeamElitePkBrackets)
                                            {
                                                var map = TeamTournament.Tournaments[GroupID].WaitingArea;
                                                var coords = map.RandomCoordinates();
                                                player.Entity.Teleport(map.ID, coords.Item1, coords.Item2);
                                            }

                                            else
                                            {
                                                player.LoadItemStats();
                                                var map = SkillTeamTournament.Tournaments[GroupID].WaitingArea;
                                                var coords = map.RandomCoordinates();
                                                player.Entity.Teleport(map.ID, coords.Item1, coords.Item2);
                                            }
                                            if (player.Entity.Hitpoints == 0)
                                                player.Entity.BringToLife();
                                        }
                                    }
                                    teams.Teleported = true;
                                }
                            }
                        }

                        Exported = true;
                        Completed = true;
                    }
                }
                catch (Exception) {  }
            }

            public void SwitchBetween()
            {
                if (Imports == 1 && MatchStats.Length == 3 && MatchStats[0] != null && !MatchStats[0].Lost)
                {
                    MatchStats[0].Flag = FighterStats.StatusFlag.Waiting;
                    if (MatchStats[1].Winner)
                    {
                        MatchStats[1].Teleported = false;
                        MatchStats[1].Flag = FighterStats.StatusFlag.Waiting;

                    }
                    if (MatchStats[2].Winner)
                    {
                        MatchStats[2].Teleported = false;
                        MatchStats[2].Flag = FighterStats.StatusFlag.Waiting;
                    }
                    Completed = false;
                }
            }
            public TeamElitePk.FighterStats targetOfWin(Game.ConquerStructures.Team team)
            {
                var dictionar = MatchStats.Where(p => p.Fighting).ToArray();
                for (int i = 0; i < dictionar.Length; i++)
                    if (dictionar[i] != null)
                        if (dictionar[i].Team.UID != team.UID)
                            return dictionar[i];
                return null;
            }
            public Network.GamePackets.TeamElitePKMatchStats CreateUpdate()
            {
                Network.GamePackets.TeamElitePKMatchStats stats = new Network.GamePackets.TeamElitePKMatchStats(T_ID);
                stats.Append(this);
                return stats;
            }
            public void BeginWatch(Client.GameClient client)
            {

                if (Watchers.ContainsKey(client.Entity.UID)) return;
                Watchers.TryAdd(client.Entity.UID, client);
                client.Entity.SkillTeamWatchingElitePKMatch = this;
                var coords = Map.RandomCoordinates();
                client.Entity.Teleport(Map.BaseID, Map.ID, coords.Item1, coords.Item2);

                Network.GamePackets.ElitePKWatch watch = new Network.GamePackets.ElitePKWatch(true, MatchStats.Length);

                watch.ID = ID;
                watch.Type = Network.GamePackets.ElitePKWatch.Fighters;
                watch.dwCheers1 = MatchStats[0].Cheers;
                watch.dwCheers2 = MatchStats[1].Cheers;
                watch.Append(MatchStats[0].Name);
                watch.Append(MatchStats[1].Name);
                client.Send(watch.Encode());
                watch = new Network.GamePackets.ElitePKWatch(true, Watchers.Count);
                watch.ID = ID;
                watch.dwCheers1 = MatchStats[0].Cheers;
                watch.dwCheers2 = MatchStats[1].Cheers;
                foreach (var pClient in Watchers.Values)
                    watch.Append(pClient.Entity.Mesh, pClient.Entity.Name);
                client.Send(watch.Encode());
                client.Send(CreateUpdate().Encode());

                UpdateWatchers();
            }

            public void Update()
            {
                var update = CreateUpdate();
                foreach (var player in Players)
                    if (player != null)
                        if (player.Team.EliteFighterStats.Fighting)
                            player.Send(update.Encode());
            }
            public void UpdateWatchers()
            {
                Network.GamePackets.ElitePKWatch watch = new Network.GamePackets.ElitePKWatch(true, Watchers.Count);
                {
                    watch.ID = ID;
                    watch.Type = Network.GamePackets.ElitePKWatch.Watchers;
                    watch.dwCheers1 = MatchStats[0].Cheers;
                    watch.dwCheers2 = MatchStats[1].Cheers;
                    foreach (var pClient in Watchers.Values)
                        watch.Append(pClient.Entity.Mesh, pClient.Entity.Name);
                    foreach (var pClient in Watchers.Values)
                        pClient.Send(watch.Encode());
                    foreach (var pClient in Players)
                        if (pClient != null)
                            pClient.Send(watch.Encode());
                }
            }
            public void LeaveWatch(Client.GameClient client)
            {
                if (Watchers.TryRemove(client.Entity.UID, out client))
                {
                    client.Entity.SkillTeamWatchingElitePKMatch = null;
                    Network.GamePackets.ElitePKWatch watch = new Network.GamePackets.ElitePKWatch(true);
                    {
                        watch.ID = ID;
                        watch.Type = Network.GamePackets.ElitePKWatch.Leave;
                        client.Send(watch.Encode());
                        client.Entity.PreviousTeleport();
                    }
                }
            }

            public void Cheer(Client.GameClient client, uint fighter)
            {
                MatchStats.First(p => p != null && p.Team.Contain(fighter)).Team
                    .EliteFighterStats.Cheers++;
                UpdateWatchers();
            }
        }

        public FighterStats[] Top8 = new FighterStats[0];

        public ConcurrentDictionary<uint, Game.ConquerStructures.Team> Teams;
        public ConcurrentDictionary<uint, Match> Matches;
        private Match[] MatchArray;
        private Match[] Top4MatchArray;
        private Match[] ExtendedMatchArray;
        private Counter MatchCounter;
        private ushort MatchIndex;

        public bool Alive = false;
        public DateTime ConstructTop8 = new DateTime();
        public Map WaitingArea;
        public int State;
        private int pState = States.T_Organize;
        public int GroupID;
        private bool willAdvance;

        private Time32 pStamp;
        public GamePackets ID;
        public ushort TimeLeft
        {
            get
            {
                int value = (int)((pStamp.TotalMilliseconds - Time32.Now.TotalMilliseconds) / 1000);
                if (value < 0) return 0;
                return (ushort)value;
            }
        }
        public TeamElitePk(int group, GamePackets T_ID)
        {
            if (!Alive)
            {

                ID = T_ID;
                Alive = true;
                Teams = new ConcurrentDictionary<uint, ConquerStructures.Team>();
                Matches = new ConcurrentDictionary<uint, Match>();

                GroupID = group;
                MatchCounter = new Counter((uint)(GroupID * 100000 + 100000));

                WaitingArea = Kernel.Maps[(ushort)ElitePKTournament.WaitingAreaID].MakeDynamicMap();
                Constants.PKForbiddenMaps.Add(WaitingArea.ID);

                State = States.GUI_Top8Ranking;
                pState = States.T_Organize;
            }
        }

        public bool SignUp(Client.GameClient client)
        {
            if (AllowJoin)
            {
                var coords = WaitingArea.RandomCoordinates();
                client.Entity.Teleport(WaitingArea.ID, coords.Item1, coords.Item2);


                if (!Teams.ContainsKey(client.Team.UID))
                    Teams.TryAdd(client.Team.UID, client.Team);

                Network.GamePackets.TeamElitePKMatchUI ui = new Network.GamePackets.TeamElitePKMatchUI(ID);
                {
                    ui.Type = Network.GamePackets.TeamElitePKMatchUI.Information;
                    ui.Append(client.Team);
                    client.Send(ui.Encode());
                    ui.Type = 5;
                    client.Send(ui.Encode());
                }
                if (client.Team.TeamLider(client))
                {
                    Network.GamePackets.SkillEliteSetTeamName teamname = new Network.GamePackets.SkillEliteSetTeamName(ID);
                    {
                        teamname.TeamID = client.Team.UID;
                        teamname.TeamName = client.Team.EliteFighterStats.Name;
                        client.Send(teamname.Encode());
                    }
                }
                client.MessageBox("You`ve been teleported to the waiting area of Skill Team PK Tournament.");
                return true;
            }
            return false;
        }

        DateTime StarTimer = new DateTime();
        private bool AllowJoin = false;
        private IDisposable Subscriber;
        public void SubscribeTimer(int minutes)
        {
            Alive = true;
            State = 0;
            StarTimer = DateTime.Now;
            StarTimer = StarTimer.AddMinutes(minutes);
            AllowJoin = true;
            if (Subscriber == null)
                Subscriber = World.Subscribe(timerCallback, 1000);
        }

        public void Purge()
        {
            foreach (var team in Teams.Values)
            {
                if (State == States.GUI_Top8Qualifier)
                {
                    if (Teams.Count > 8)
                    {
                        if (team.EliteFighterStats.Lost || !team.OnPopulates)// && player.Player.MyLocation.DinamicID != WaitingArea)
                        {
                            Game.ConquerStructures.Team remover;
                            Teams.TryRemove(team.UID, out remover);
                        }
                    }
                }
                else if (team.EliteFighterStats.Lost)
                {
                    if (Teams.Count > 8)
                    {
                        Game.ConquerStructures.Team remover;
                        Teams.TryRemove(team.UID, out remover);
                    }
                }
            }
        }
        private void timerCallback(int tim)
        {
            try
            {

                if (!Alive)
                {
                    if (ID == GamePackets.SkillElitePkBrackets)
                        SkillTeamTournament.Opened = false;
                    else
                        TeamTournament.Opened = false;
                    Subscriber.Dispose();
                    return;
                }
                #region ReconstructTop
                if (State == States.GUI_ReconstructTop)
                {
                    if (DateTime.Now > ConstructTop8.AddMinutes(2))
                    {
                        State = States.GUI_Top8Ranking;
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = false }.Encode());
                        Alive = false;
                    }
                    return;
                }
                #endregion
                DateTime Now64 = DateTime.Now;
                if (Now64 > StarTimer)
                {
                    SkillTeamTournament.Opened = false;
                    TeamTournament.Opened = false;
                    #region GetState
                    if (State == States.GUI_Top8Ranking)
                    {
                        willAdvance = Teams.Count > 8;
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = true }.Encode());
                        foreach (var team in Teams.Values)
                        {
                            foreach (var player in team.Players)
                            {
                                if (player.Team != null)
                                {
                                    Network.GamePackets.SkillEliteSetTeamName teamname = new Network.GamePackets.SkillEliteSetTeamName(ID);
                                    {
                                        teamname.Type = Network.GamePackets.SkillEliteSetTeamName.SuccessfulName;
                                        teamname.TeamID = player.Team.UID;
                                        teamname.TeamName = player.Team.EliteFighterStats.Name;
                                        player.Send(teamname.Encode());

                                        if (player.Team.TeamLider(player))
                                        {
                                            teamname.Type = Network.GamePackets.SkillEliteSetTeamName.Remove;
                                            player.Send(teamname.Encode());
                                        }
                                    }
                                    //TeamPK A~prize~for~every~participant~in~the~Team~PK~Tournament.~Right~click~it~to~get~1~hour`s~double~EXP.
                                    if (ID == GamePackets.TeamElitePkBrackets)
                                        player.Inventory.Add(720793, 0, 1);
                                }
                            }
                        }
                        if (Teams.Count < 8)
                        {
                            if (Teams.Count == 1)
                            {
                                foreach (var team in Teams.Values)
                                {
                                    foreach (var player in team.Players)
                                    {
                                        if (player.Team != null && player.Entity.UID == player.Team.Lider.Entity.UID)
                                        {
                                            player.MessageBox("Congratulations! You are the Champion of Team Pk!!");
                                            player.Entity.AddTopStatus(
                                                            (ulong)(((int)TitlePacket.Titles.Team_PK_Champion_High_)),
                                                            DateTime.Now.AddDays(7));
                                            player.Inventory.Add(721303, 0, 1);
                                        }
                                        player.Entity.Teleport(1002, 300, 278);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var team in Teams.Values)
                                {
                                    foreach (var player in team.Players)
                                    {
                                        player.Entity.Teleport(1002, 300, 278);

                                        player.MessageBox("Should Have 8 Teams to Start!");
                                    }
                                }
                            }
                            Teams.Clear();
                            Alive = false;
                            foreach (var client in Kernel.GamePool.Values)
                                client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = false }.Encode());

                            return;
                        }
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = true }.Encode());
                        Top8 = new FighterStats[8];
                        if (Teams.Count == 8)
                            State = States.GUI_Top4Qualifier;
                        else if (willAdvance && Teams.Count <= 24)
                            State = States.GUI_Top8Qualifier;
                        else
                            State = States.GUI_Knockout;
                    }
                    #endregion
                    #region Knockout
                    if (State == States.GUI_Knockout)
                    {
                        AllowJoin = false;
                        if (pState == States.T_Organize)
                        {
                            if (willAdvance && Teams.Count <= 24)
                            {
                                State = States.GUI_Top8Qualifier;
                            }
                            else
                            {
                                MatchIndex = 0;
                                var array = Teams.Values.ToArray();
                                ushort counter = 0;
                                bool NoPar = array.Length % 2 == 0;
                                if (NoPar)
                                {
                                    for (ushort x = 0; x < array.Length; x++)
                                    {
                                        int r = counter;
                                        counter++;
                                        int t = counter;
                                        counter++;
                                        if (counter <= array.Length)
                                        {
                                            Match match = new Match(ID, array[r].EliteFighterStats, array[t].EliteFighterStats);
                                            match.Index = MatchIndex++;
                                            match.ID = MatchCounter.Next;
                                            Matches.TryAdd(match.ID, match);
                                        }
                                    }
                                }
                                else
                                {
                                    for (ushort x = 0; x < array.Length - 1; x++)
                                    {
                                        int r = counter;
                                        counter++;
                                        int t = counter;
                                        counter++;
                                        if (counter <= array.Length)
                                        {
                                            Match match = new Match(ID, array[r].EliteFighterStats, array[t].EliteFighterStats);
                                            match.Index = MatchIndex++;
                                            match.ID = MatchCounter.Next;
                                            Matches.TryAdd(match.ID, match);
                                        }
                                    }
                                    Match math_bye = new Match(ID, array[array.Length - 1].EliteFighterStats);
                                    math_bye.Index = MatchIndex++;
                                    math_bye.ID = MatchCounter.Next;
                                    Matches.TryAdd(math_bye.ID, math_bye);
                                }
                                pStamp = Time32.Now.AddSeconds(60);
                                pState = States.T_Wait;
                                MatchArray = (from match in Matches.Values orderby match.MatchStats.Length descending select match).ToArray();
                                var brackets = CreateBrackets(MatchArray, 0);
                                foreach (var packet in brackets)
                                {
                                    foreach (var client in Kernel.GamePool.Values)
                                    {
                                        client.Send(packet.Encode());
                                    }
                                }
                            }
                        }
                        else if (pState == States.T_Wait)
                        {
                            if (TimeLeft == 0)
                            {
                                int doneMatches = 0;

                                foreach (var match in MatchArray)
                                {
                                    match.AliveMatch();
                                    if (!match.Inside)
                                    {
                                        match.Import(Match.StatusFlag.Watchable);
                                    }
                                    else
                                    {
                                        if (!match.Exported && (match.Done || match.TimeLeft == 0))
                                        {
                                            match.Export();
                                        }
                                        if (match.Exported || match.MatchStats.Length == 1)
                                            doneMatches++;
                                    }
                                }
                                if (doneMatches == Matches.Count)
                                {
                                    Matches.Clear();
                                    Purge();
                                    pState = States.T_Organize;
                                }
                            }
                        }
                    }
                    #endregion
                    #region Top8Qualifier
                    if (State == States.GUI_Top8Qualifier)
                    {
                        if (pState == States.T_Organize)
                        {
                            if (Teams.Count == 8)
                            {
                                State = States.GUI_Top4Qualifier;
                            }
                            else
                            {
                                MatchIndex = 0;
                                var array = Teams.Values.ToArray();
                                int[] taken = new int[array.Length];

                                if (array.Length <= 16)
                                {
                                    ushort counter = 0;
                                    int t1Group = array.Length - 8;
                                    int lim = taken.Length / 2;
                                    for (int i = 0; i < t1Group; i++)
                                    {
                                        int r = counter;
                                        counter++;
                                        int t = counter;
                                        counter++;
                                        Match match = new Match(ID, array[r].EliteFighterStats, array[t].EliteFighterStats);
                                        match.Index = MatchIndex++;
                                        match.ID = MatchCounter.Next;
                                        Matches.TryAdd(match.ID, match);
                                    }
                                    for (int i = 0; i < 8 - t1Group; i++)
                                    {
                                        ushort r = counter;
                                        counter++;
                                        Match match = new Match(ID, array[r].EliteFighterStats);
                                        match.Index = MatchIndex++;
                                        match.ID = MatchCounter.Next;
                                        Matches.TryAdd(match.ID, match);
                                    }
                                }
                                else
                                {
                                    ushort counter = 0;
                                    int t3GroupCount = array.Length - 16;
                                    for (int i = 0; i < t3GroupCount; i++)
                                    {
                                        int r = counter;
                                        counter++;
                                        int t = counter;
                                        counter++;
                                        int y = counter;
                                        counter++;
                                        Match match = new Match(ID, array[r].EliteFighterStats, array[t].EliteFighterStats, array[y].EliteFighterStats);
                                        match.Index = MatchIndex++;
                                        match.ID = MatchCounter.Next;
                                        Matches.TryAdd(match.ID, match);
                                    }
                                    int t2GroupCount = array.Length - counter;
                                    for (int i = 0; i < t2GroupCount / 2; i++)
                                    {
                                        int r = counter;
                                        counter++;
                                        int t = counter;
                                        counter++;
                                        Match match = new Match(ID, array[r].EliteFighterStats, array[t].EliteFighterStats);
                                        match.Index = MatchIndex++;
                                        match.ID = MatchCounter.Next;
                                        Matches.TryAdd(match.ID, match);
                                    }
                                }
                                pStamp = Time32.Now.AddSeconds(60);
                                pState = States.T_Wait;
                                MatchArray = (from match in Matches.Values orderby match.MatchStats.Length descending select match).ToArray();

                                var brackets = CreateBrackets(MatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                foreach (var packet in brackets)
                                {
                                    foreach (var client in Kernel.GamePool.Values)
                                        client.Send(packet.Encode());
                                }
                                Network.GamePackets.TeamElitePkBrackets brea = new Network.GamePackets.TeamElitePkBrackets(0);
                                brea.Type = Network.GamePackets.TeamElitePkBrackets.EPK_State;
                                brea.OnGoing = true;
                                foreach (var client in Kernel.GamePool.Values)
                                    client.Send(brea.Encode());
                            }
                        }
                        else if (pState == States.T_Wait)
                        {
                            if (TimeLeft == 0)
                            {
                                int doneMatches = 0;
                                foreach (var match in MatchArray)
                                {
                                    match.AliveMatch();
                                    if (!match.Inside)
                                    {
                                        if (match.MatchStats.Length > 1)
                                        {
                                            match.Import(Match.StatusFlag.Watchable);
                                        }
                                        else
                                        {
                                            match.Inside = true;
                                            match.Completed = true;
                                            match.Done = true;
                                            match.MatchStats
                                                .First(p => p != null).Team
                                                .EliteFighterStats.Flag = FighterStats.StatusFlag.Qualified;
                                        }
                                    }
                                    else
                                    {
                                        if (!match.Exported && (match.Done || match.TimeLeft == 0))
                                        {
                                            match.Export();
                                            if (match.MatchStats.Length == 3)
                                                match.SwitchBetween();
                                            else
                                                match.Flag = Match.StatusFlag.OK;
                                        }

                                        if (match.Exported || match.MatchStats.Length == 1)
                                            doneMatches++;
                                    }
                                }
                                if (doneMatches == Matches.Count)
                                {
                                    bool finishedRound = true;
                                    foreach (var match in MatchArray)
                                    {
                                        if (!match.Completed)
                                        {
                                            finishedRound = false;
                                            break;
                                        }
                                    }
                                    if (!finishedRound)
                                    {
                                        foreach (var match in MatchArray)
                                        {
                                            if (!match.Completed)
                                            {
                                                foreach (var stats in match.MatchStats)
                                                    stats.Reset(true);
                                                match.Inside = false;
                                                match.Done = false;
                                                match.Exported = false;
                                                if (match.MatchStats.Length != 1)
                                                    match.Flag = Match.StatusFlag.Waiting;
                                            }
                                        }
                                        pStamp = Time32.Now.AddSeconds(60);
                                        pState = States.T_Wait;
                                        var brakets = CreateBrackets(MatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                        foreach (var packet in brakets)
                                        {
                                            foreach (var client in Kernel.GamePool.Values)
                                                client.Send(packet.Encode());
                                        }

                                    }
                                    else
                                    {
                                        Matches.Clear();
                                        Purge();
                                        pState = States.T_Organize;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region Top4Qualifier
                    if (State == States.GUI_Top4Qualifier)
                    {
                        if (pState == States.T_Organize)
                        {
                            try
                            {
                                MatchIndex = 0;
                                var array = Teams.Values.ToArray();
                                int[] taken = new int[array.Length];
                                int lim = taken.Length / 2;
                                for (int i = 0; i < taken.Length; i += 2)
                                {
                                    taken[i] = taken[i + 1] = 1;
                                    Match match = new Match(ID, array[i].EliteFighterStats, array[i + 1].EliteFighterStats);
                                    match.Index = MatchIndex++;
                                    //match.Flag = Match.StatusFlag.FinalMatch;
                                    match.ID = MatchCounter.Next;
                                    Matches.TryAdd(match.ID, match);
                                }
                                pStamp = Time32.Now.AddSeconds(60);
                                pState = States.T_Wait;
                                MatchArray = Matches.Values.ToArray();
                                var brackets = CreateBrackets(MatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                foreach (var packet in brackets)
                                {
                                    foreach (var client in Kernel.GamePool.Values)
                                        client.Send(packet.Encode());
                                }
                            }
                            catch (Exception)
                            {
                               
                            }

                        }
                        else if (pState == States.T_Wait)
                        {
                            if (TimeLeft == 0)
                            {
                                try
                                {
                                    int doneMatches = 0;

                                    foreach (var match in MatchArray)
                                    {
                                        match.AliveMatch();
                                        if (!match.Inside)
                                        {
                                            match.Import(Match.StatusFlag.Watchable);
                                        }
                                        else
                                        {
                                            if (!match.Exported && (match.Done || match.TimeLeft == 0))
                                                match.Export();
                                            if (match.Exported)
                                                doneMatches++;
                                        }
                                    }
                                    if (doneMatches == MatchArray.Length)
                                    {
                                        int i = 7;
                                        foreach (var match in MatchArray)
                                        {
                                            foreach (var stats in match.MatchStats)
                                                if (stats.Flag == FighterStats.StatusFlag.Lost)
                                                    Top8[i--] = stats.Clone();//fara clone;
                                            match.Commit();
                                        }
                                        State = States.GUI_Top2Qualifier;
                                        pState = States.T_Organize;
                                    }
                                }
                                catch (Exception)
                                {
                                   
                                }
                            }
                        }
                    }
                    #endregion
                    #region Top2Qualifier
                    if (State == States.GUI_Top2Qualifier)
                    {
                        if (pState == States.T_Organize)
                        {
                            try
                            {
                                MatchIndex = 0;
                                Top4MatchArray = new Match[2];

                                for (int i = 0; i < Top4MatchArray.Length; i++)
                                {
                                    Match match = new Match(ID,//p.winer este
                                        MatchArray[i].Return(p => p.Team.EliteFighterStats.Winner),//.MatchStats.First(p => p.Team.EliteFighterStats.Winner),
                                        MatchArray[i + 2].Return(p => p.Team.EliteFighterStats.Winner));//.MatchStats.First(p => p.Team.EliteFighterStats.Winner));
                                    match.Index = MatchIndex++;
                                    match.ID = MatchCounter.Next;
                                    Top4MatchArray[i] = match;
                                    Matches.TryAdd(match.ID, match);
                                }

                                pStamp = Time32.Now.AddSeconds(60);
                                pState = States.T_Wait;
                                var brackets = CreateBrackets(Top4MatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                foreach (var packet in brackets)
                                {
                                    foreach (var client2 in Kernel.GamePool.Values)
                                    {
                                        client2.Send(packet.Encode());
                                    }
                                }
                            }
                            catch (Exception)
                            {
                               
                            }

                        }
                        else if (pState == States.T_Wait)
                        {
                            if (TimeLeft == 0)
                            {
                                try
                                {
                                    int doneMatches = 0;
                                    foreach (var match in Top4MatchArray)
                                    {
                                        match.AliveMatch();
                                        if (!match.Inside)
                                        {
                                            match.Import(Match.StatusFlag.Watchable);
                                        }
                                        else
                                        {
                                            if (!match.Exported && (match.Done || match.TimeLeft == 0))
                                                match.Export();
                                            if (match.Exported)
                                                doneMatches++;
                                        }
                                    }
                                    if (doneMatches == Top4MatchArray.Length)
                                    {
                                        foreach (var match in Top4MatchArray)
                                            match.Commit();

                                        pState = States.T_Organize;
                                        State = States.GUI_Top3;
                                    }
                                }
                                catch (Exception)
                                {
                                   
                                }
                            }
                        }
                    }
                    #endregion
                    #region Top3
                    if (State == States.GUI_Top3)
                    {
                        if (pState == States.T_Organize)
                        {
                            try
                            {
                                MatchIndex = 0;

                                ExtendedMatchArray = new Match[2];
                                Match match = new Match(ID,
                                    Top4MatchArray[0].Return(p => !p.Team.EliteFighterStats.Winner),//.MatchStats.First(p => !p.Team.EliteFighterStats.Winner),
                                    Top4MatchArray[1].Return(p => !p.Team.EliteFighterStats.Winner));//.MatchStats.First(p => !p.Team.EliteFighterStats.Winner));
                                match.Index = MatchIndex++;
                                match.ID = MatchCounter.Next;
                                ExtendedMatchArray[0] = match;

                                Match finalmatch = new Match(ID,
        Top4MatchArray[0].Return(p => p.Winner).Clone(),//.MatchStats.First(p => p.Team.EliteFighterStats.Winner).Clone(),
        Top4MatchArray[1].Return(p => p.Winner).Clone());//.MatchStats.First(p => p.Team.EliteFighterStats.Winner).Clone());
                                finalmatch.Flag = Match.StatusFlag.SwitchOut;
                                ExtendedMatchArray[1] = finalmatch;

                                Matches.TryAdd(match.ID, match);
                                pStamp = Time32.Now.AddSeconds(60);
                                pState = States.T_Wait;
                                var brackets = CreateBrackets(ExtendedMatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                foreach (var packet in brackets)
                                {
                                    foreach (var client in Kernel.GamePool.Values)
                                        client.Send(packet.Encode());
                                }
                                foreach (var client in Kernel.GamePool.Values)
                                    client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = true }.Encode());
                            }
                            catch (Exception)
                            {
                               
                            }
                        }
                        else if (pState == States.T_Wait)
                        {
                            if (TimeLeft == 0)
                            {
                                try
                                {
                                    var match = ExtendedMatchArray[0];
                                    match.AliveMatch();
                                    if (!match.Inside)
                                    {
                                        match.Import(Match.StatusFlag.Watchable);
                                    }
                                    else
                                    {
                                        if (!match.Exported && (match.Done || match.TimeLeft == 0))
                                            match.Export();
                                        if (match.Exported)
                                        {

                                            //var a_math =ExtendedMatchArray[0].MatchStats;
                                            var top3 = ExtendedMatchArray[0].Return(p => p.Team.EliteFighterStats.Winner);
                                            //.First(p => p.Team.EliteFighterStats.Winner).Team.EliteFighterStats;
                                            var top4 = ExtendedMatchArray[0].Return(p => !p.Team.EliteFighterStats.Winner);
                                            //ExtendedMatchArray[0].MatchStats
                                            //.First(p => !p.Team.EliteFighterStats.Winner).Team.EliteFighterStats;

                                            Top8[2] = top3;
                                            Top8[3] = top4;
                                            top3.Flag = FighterStats.StatusFlag.Lost;

                                            pState = States.T_Organize;
                                            State = States.GUI_Top1;
                                            foreach (var client in Kernel.GamePool.Values)
                                                Rankings(client);
                                            var brackets = CreateBrackets(ExtendedMatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                            foreach (var packet in brackets)
                                            {
                                                foreach (var client in Kernel.GamePool.Values)
                                                    client.Send(packet.Encode());
                                            }

                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                   
                                }

                            }
                        }
                    }
                    #endregion
                    #region Top1
                    if (State == States.GUI_Top1)
                    {
                        if (pState == States.T_Organize)
                        {
                            try
                            {
                                MatchIndex = 0;
                                //  var brackets = CreateBrackets(MatchArray, 0, Client.GamePackets.SkillElitePkBrackets.GUIEdit);
                                //  foreach (var packet in brackets)
                                //       CMD.MapPackets.Enqueue(new CMD.MapPackets.Models() { data = packet.Encode() });
                                Match match = new Match(ID, ExtendedMatchArray[1].MatchStats);
                                ExtendedMatchArray = new Match[1];
                                match.Index = MatchIndex++;
                                match.ID = MatchCounter.Next;
                                ExtendedMatchArray[0] = match;
                                Matches.TryAdd(match.ID, match);
                                pStamp = Time32.Now.AddSeconds(60);
                                pState = States.T_Wait;
                                var brackets = CreateBrackets(ExtendedMatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                foreach (var packet in brackets)
                                {
                                    foreach (var client in Kernel.GamePool.Values)
                                    {
                                        client.Send(packet.Encode());
                                    }
                                }
                                foreach (var client in Kernel.GamePool.Values)
                                {
                                    client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = true }.Encode());

                                }
                            }
                            catch (Exception)
                            {
                               
                            }
                        }
                        else if (pState == States.T_Wait)
                        {
                            if (TimeLeft == 0)
                            {
                                try
                                {
                                    int doneMatches = 0;
                                    foreach (var match in ExtendedMatchArray)
                                    {
                                        match.AliveMatch();
                                        if (!match.Inside)
                                        {
                                            match.Import(Match.StatusFlag.Watchable);
                                        }
                                        else
                                        {
                                            if (!match.Exported && (match.Done || match.TimeLeft == 0))
                                                match.Export();
                                            if (match.Exported)
                                                doneMatches++;
                                        }
                                    }
                                    if (doneMatches == ExtendedMatchArray.Length)
                                    {
                                        try
                                        {
                                            var top1 = ExtendedMatchArray[0].MatchStats
                                                .First(p => p.Team.EliteFighterStats.Winner).Team.EliteFighterStats;
                                            var top2 = ExtendedMatchArray[0].MatchStats
                                                .First(p => !p.Team.EliteFighterStats.Winner).Team.EliteFighterStats;
                                            Top8[0] = top1;
                                            Top8[1] = top2;
                                            ConstructTop8 = DateTime.Now;

                                            State = States.GUI_ReconstructTop;
                                            foreach (var client in Kernel.GamePool.Values)
                                                Rankings(client);
                                            var brackets = CreateBrackets(ExtendedMatchArray, 0, Network.GamePackets.TeamElitePkBrackets.UpdateList);
                                            foreach (var packet in brackets)
                                            {
                                                foreach (var client in Kernel.GamePool.Values)
                                                {
                                                    client.Send(packet.Encode());
                                                }
                                            }
                                            foreach (var client in Kernel.GamePool.Values)
                                                client.Send(new Network.GamePackets.TeamElitePkBrackets(ID) { Type = 6, OnGoing = true }.Encode());
                                        }
                                        catch (Exception)
                                        {
                                           
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                   
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception)
            { }
        }
        public void Update(Client.GameClient client, int page = 0)
        {
            try
            {
                if (State == States.GUI_Top8Ranking)
                {
                    Rankings(client);
                }
                else
                {
                    if (State >= States.GUI_Top4Qualifier)
                    {
                        var brackets = CreateBrackets(MatchArray, 0, Network.GamePackets.TeamElitePkBrackets.GUIEdit);
                        foreach (var packet in brackets)
                            client.Send(packet.Encode());
                    }
                    else
                    {
                        var brackets = CreateBrackets(MatchArray, page);
                        foreach (var packet in brackets)
                            client.Send(packet.Encode());
                    }
                    if (State >= States.GUI_Top1)
                        Rankings(client);
                }
            }
            catch (Exception) {  }
        }
        public void Rankings(Client.GameClient client)
        {
            Network.GamePackets.TeamElitePkTop ranks = new Network.GamePackets.TeamElitePkTop(ID);
            {
                ranks.Type = Network.GamePackets.TeamElitePkTop.Top8;
                ranks.Group = (uint)GroupID;
                ranks.GroupStatus = (uint)State;
                //ranks.UID = client.Entity.UID;
                if (State >= States.GUI_Top1)
                {
                    ranks.Type = Network.GamePackets.TeamElitePkTop.Top3;
                    if (State == States.GUI_Top1)
                    {
                        ranks.Count = 1;
                        ranks.Append(Top8[2], 3);
                    }
                    else
                    {
                        ranks.Count = 3;
                        for (int i = 0; i < 3; i++)
                            ranks.Append(Top8[i], i + 1);
                    }
                    client.Send(ranks.Encode());
                }
                else
                {
                    ranks.Count = (uint)Top8.Length;
                    for (int i = 0; i < ranks.Count; i++)
                        if (Top8[i] != null)
                            ranks.Append(Top8[i], i);
                    client.Send(ranks.Encode());
                }
            }
        }
        private Network.GamePackets.TeamElitePkBrackets[] CreateBrackets(Match[] matches, int page = 0, byte type = 0)
        {
            try
            {
                int lim = 0, count = 0;
                if (matches == null) return new Network.GamePackets.TeamElitePkBrackets[0];
                if (State == States.GUI_Knockout)
                {
                    const int max = 5;
                    int offset = page * max;

                    int Do_count = matches.Length - page * max;

                    if (page * max > matches.Length)
                        Do_count = 0;
                    uint ccount = (uint)Math.Min(max, Do_count);

                    Network.GamePackets.TeamElitePkBrackets brackets = new Network.GamePackets.TeamElitePkBrackets(ID, (int)ccount);
                    brackets.Group = (ushort)GroupID;
                    brackets.GUIType = (ushort)State;
                    brackets.TotalMatchesOnRoom = (ushort)ccount;
                    brackets.Page = (byte)page;
                    brackets.TimeLeft = TimeLeft;
                    brackets.MatchCount = (ushort)matches.Length;
                    brackets.Type = type;
                    for (int i = 0; i < ccount; i++)
                    {
                        var element = matches[i + offset];
                        brackets.Append(element);
                    }
                    Network.GamePackets.TeamElitePkBrackets[] buffer = new Network.GamePackets.TeamElitePkBrackets[1];
                    buffer[0] = brackets;
                    return buffer;
                }
                else if (State == States.GUI_Top8Qualifier)
                {
                    const int max = 5;
                    int offset = page * max;

                    int Do_count = matches.Length - page * max;

                    if (page * max > matches.Length)
                        Do_count = 0;
                    uint ccount = (uint)Math.Min(max, Do_count);

                    Network.GamePackets.TeamElitePkBrackets[] buffer;
                    if (matches.Length > ccount)
                        buffer = new Network.GamePackets.TeamElitePkBrackets[2];
                    else
                        buffer = new Network.GamePackets.TeamElitePkBrackets[1];

                    Network.GamePackets.TeamElitePkBrackets brackets = new Network.GamePackets.TeamElitePkBrackets(ID, (int)ccount);
                    brackets.Group = (ushort)GroupID;
                    brackets.GUIType = (ushort)State;
                    brackets.TotalMatchesOnRoom = (ushort)ccount;
                    brackets.Page = (byte)page;
                    brackets.TimeLeft = TimeLeft;
                    brackets.MatchCount = (ushort)matches.Length;
                    brackets.Type = type;
                    for (int i = 0; i < ccount; i++)
                    {
                        var element = matches[i + offset];
                        brackets.Append(element);
                    }
                    buffer[0] = brackets;
                    if (matches.Length > ccount)
                    {
                        ushort towcount = (ushort)(matches.Length - ccount);
                        Network.GamePackets.TeamElitePkBrackets twobrackets = new Network.GamePackets.TeamElitePkBrackets(ID, (int)towcount);
                        twobrackets.Group = (ushort)GroupID;
                        twobrackets.GUIType = (ushort)State;
                        twobrackets.TotalMatchesOnRoom = (ushort)towcount;
                        twobrackets.Page = (byte)page;
                        twobrackets.TimeLeft = TimeLeft;
                        twobrackets.ListCount = 1;
                        twobrackets.MatchCount = (ushort)matches.Length;
                        twobrackets.Type = type;
                        for (int i = 0; i < towcount; i++)
                        {
                            var element = matches[i + ccount];
                            twobrackets.Append(element);
                        }
                        buffer[1] = twobrackets;
                        return buffer;
                    }
                    return buffer;
                }
                lim = matches.Length;
                count = Math.Min(lim, matches.Length - page * lim);

                if (State >= States.GUI_Top2Qualifier)
                {
                    lim = MatchArray.Length;
                    count = Math.Min(lim, MatchArray.Length - page * lim);
                    Network.GamePackets.TeamElitePkBrackets[] buffer = new Network.GamePackets.TeamElitePkBrackets[2];
                    if (Top4MatchArray == null)
                        return new Network.GamePackets.TeamElitePkBrackets[0];
                    Network.GamePackets.TeamElitePkBrackets qbrackets = new Network.GamePackets.TeamElitePkBrackets(ID, count);
                    {
                        qbrackets.Group = (ushort)GroupID;
                        qbrackets.GUIType = (ushort)State;
                        qbrackets.TotalMatchesOnRoom = (ushort)(count);
                        qbrackets.Page = (byte)page;
                        qbrackets.TimeLeft = TimeLeft;
                        qbrackets.MatchCount = (byte)(MatchArray.Length * 2);
                        qbrackets.Type = type;
                        for (int i = page * lim; i < page * lim + count; i++)
                            qbrackets.Append(MatchArray[i]);
                        buffer[0] = qbrackets;
                    }
                    if (State >= States.GUI_Top3)
                    {
                        lim = 4;
                        count = (byte)(Top4MatchArray.Length + ExtendedMatchArray.Length);
                        Network.GamePackets.TeamElitePkBrackets Twoqbrackets = new Network.GamePackets.TeamElitePkBrackets(ID, count);
                        {
                            Twoqbrackets.Group = (ushort)GroupID;
                            Twoqbrackets.GUIType = (ushort)State;
                            Twoqbrackets.TotalMatchesOnRoom = (ushort)count;
                            Twoqbrackets.Page = (byte)1;
                            Twoqbrackets.TimeLeft = TimeLeft;
                            Twoqbrackets.MatchCount = (byte)((Top4MatchArray.Length + ExtendedMatchArray.Length) * 2);
                            Twoqbrackets.Type = type;
                            for (int i = 0; i < 2; i++)
                                Twoqbrackets.Append(Top4MatchArray[i]);
                            for (int x = 0; x < ExtendedMatchArray.Length; x++)
                                Twoqbrackets.Append(ExtendedMatchArray[x]);
                            buffer[1] = Twoqbrackets;
                        }
                        buffer[1] = Twoqbrackets;
                    }
                    else
                    {
                        lim = 2;
                        count = Top4MatchArray.Length;
                        Network.GamePackets.TeamElitePkBrackets Twoqbrackets = new Network.GamePackets.TeamElitePkBrackets(ID, count);
                        {
                            Twoqbrackets.Group = (ushort)GroupID;
                            Twoqbrackets.GUIType = (ushort)State;
                            // Twoqbrackets.ListCount = 1;
                            Twoqbrackets.TotalMatchesOnRoom = (ushort)count;
                            Twoqbrackets.Page = (byte)1;
                            Twoqbrackets.TimeLeft = TimeLeft;
                            Twoqbrackets.MatchCount = (byte)MatchArray.Length;
                            Twoqbrackets.Type = type;
                            for (int i = page * lim; i < page * lim + count; i++)
                                Twoqbrackets.Append(Top4MatchArray[i]);
                            buffer[1] = Twoqbrackets;
                        }
                    }
                    return buffer;
                }

                Network.GamePackets.TeamElitePkBrackets aqbrackets = new Network.GamePackets.TeamElitePkBrackets(ID, count);
                {
                    aqbrackets.Group = (ushort)GroupID;
                    aqbrackets.GUIType = (ushort)State;
                    aqbrackets.TotalMatchesOnRoom = (ushort)count;
                    aqbrackets.Page = (byte)page;
                    aqbrackets.TimeLeft = TimeLeft;
                    aqbrackets.MatchCount = (ushort)matches.Length;
                    aqbrackets.Type = type;
                    for (int i = page * lim; i < page * lim + count; i++)
                        aqbrackets.Append(matches[i]);
                    Network.GamePackets.TeamElitePkBrackets[] buffer = new Network.GamePackets.TeamElitePkBrackets[1];
                    buffer[0] = aqbrackets;
                    return buffer;
                }
            }
            catch (Exception) { return new Network.GamePackets.TeamElitePkBrackets[0]; }
        }

    }
}
