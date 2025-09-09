using System;
using System.IO;
using System.Text;
using Nyx.Server.Game.ConquerStructures.Society;
using System.Collections.Generic;
using Nyx.Server.Game;

namespace Nyx.Server.Network.GamePackets
{
    public class GuildMemberList
    {
        public ushort Size;
        public ushort Type;
        public ushort SubType;
        public ushort PageNumber;
        public Guild g;

        public GuildMemberList(byte[] packet)
        {
            BinaryReader Reader = new BinaryReader(new MemoryStream(packet));
            Size = Reader.ReadUInt16();
            Type = Reader.ReadUInt16();
            SubType = Reader.ReadUInt16();
            PageNumber = Reader.ReadUInt16();
        }

        public byte[] Build()
        {
            MemoryStream Stream = new MemoryStream();
            BinaryWriter Writer = new BinaryWriter(Stream);

            Writer.Write((ushort)0);
            Writer.Write((ushort)2102);
            Writer.Write((ushort)0);
            Writer.Write((ushort)1);//page
            Writer.Write((ushort)0);
            Writer.Write((ushort)0);
            Writer.Write((ushort)g.Members.Count);//count
            Writer.Write((ushort)0);
            foreach (Nyx.Server.Game.ConquerStructures.Society.Guild.Member m in g.Members.Values)
            {
                for (int i = 0; i < 16; i++)//16 offsets
                {
                    if (i < m.Name.Length)
                    {
                        Writer.Write((byte)m.Name[i]);
                    }
                    else
                        Writer.Write((byte)0);
                }
                Writer.Write((ushort)m.NobilityRank);
                Writer.Write((ushort)0);
                Writer.Write((ushort)1);
                Writer.Write((ushort)0);
                Writer.Write((uint)m.Level);
                Writer.Write((uint)m.Rank);
                Writer.Write((uint)0);
                Writer.Write((uint)m.SilverDonation);
                if (m.Client != null)
                { Writer.Write((byte)1); }
                else
                { Writer.Write((byte)0); }
                Writer.Write((byte)0);
                Writer.Write((ushort)0);
                Writer.Write((ushort)0);
                Writer.Write((ushort)0);
            }
            int packetlength = (int)Stream.Length;
            Stream.Position = 0;
            Writer.Write((ushort)packetlength);
            Stream.Position = Stream.Length;
            Writer.Write(Program.Encoding.GetBytes("TQServer"));
            Stream.Position = 0;
            byte[] buf = new byte[Stream.Length];
            Stream.Read(buf, 0, buf.Length);
            Writer.Close();
            Stream.Close();
            return buf;
        }
    }
    public class GuildDonationList
    {
        public ushort Size;
        public ushort Type;
        public ushort SubType;
        public ushort PageNumber;
        public Guild g;

        public GuildDonationList(byte[] packet)
        {
            BinaryReader Reader = new BinaryReader(new MemoryStream(packet));
            Size = Reader.ReadUInt16();
            Type = Reader.ReadUInt16();
            SubType = Reader.ReadUInt16();
            PageNumber = Reader.ReadUInt16();
        }

        public byte[] Build()
        {
            MemoryStream Stream = new MemoryStream();
            BinaryWriter Writer = new BinaryWriter(Stream);

            Writer.Write((ushort)0);
            Writer.Write((ushort)2102);
            Writer.Write((ushort)SubType);
            Writer.Write((ushort)PageNumber);//page
            Writer.Write((ushort)0);
            Writer.Write((ushort)0);
            Writer.Write((ushort)g.Members.Count);//count
            Writer.Write((ushort)0);
            for (int i = 0; i < 16; i++)//16 offsets
            {
                if (i < "TestName".Length)
                {
                    Writer.Write((byte)"TestName"[i]);
                }
                else
                    Writer.Write((byte)0);
            }
            Writer.Write((ulong)0);
            Writer.Write((uint)130);//level
            Writer.Write((uint)1000);//guildrank
            Writer.Write((uint)0);//unknown
            Writer.Write((uint)10000);//donation
            Writer.Write((byte)1);//online-offline
            Writer.Write((byte)0);
            Writer.Write((ushort)0);
            Writer.Write((ushort)0);
            Writer.Write((ushort)0);
            int packetlength = (int)Stream.Length;
            Stream.Position = 0;
            Writer.Write((ushort)packetlength);
            Stream.Position = Stream.Length;
            Writer.Write(Program.Encoding.GetBytes("TQServer"));
            Stream.Position = 0;
            byte[] buf = new byte[Stream.Length];
            Stream.Read(buf, 0, buf.Length);
            Writer.Close();
            Stream.Close();
            return buf;
        }
    }
    public class GuildCommand : Writer, Interfaces.IPacket
    {
        public const uint
                    JoinRequest = 1,
                    InviteRequest = 2,
                    Quit = 3,
                    Info = 6,
                    Allied = 7,
                    Neutral1 = 8,
                    Enemied = 9,
                    Neutral2 = 10,
                    DonateSilvers = 11,
                    Refresh = 12,
                    Disband = 19,
                    DonateConquerPoints = 20,
                    ChangeGuildRequirements = 24,
                    GuildRequirements = 25,
                    Bulletin = 27,
                    PromoteSendRequest = 28,
                    PromoteAcceptRequest = 29,
                    Discharge = 30,
                    ConfirmPromote = 34,
                    Diseharge = 36,
                    Promote = 37,
                    PromoteInfo = 38,
                    Unknown = 45,
                    GuildBlackList = 47,
                    MakeBlackList = 48,
                    SmsBlackList = 49,
                    RemoveBlackList = 50;

        private byte[] Buffer;
        public GuildCommand(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[8 + 28];
                Writer.Write(28, 0, Buffer);
                Writer.Write(1107, 2, Buffer);
            }
        }

        public GuildCommand(uint leng)
        {
            Buffer = new byte[8 + 28 + leng];
            Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
            Writer.Write(1107, 2, Buffer);
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public string _Str
        {
            get
            {
                return Encoding.Default.GetString(Buffer, 26, Buffer[25]);
            }
            set
            {
                Buffer[25] = (byte)value.Length;
                Writer.Write(value, 26, Buffer);
            }
        }

        public string _str
        {
            set
            {
                Buffer[24] = 1;
                Buffer[25] = (byte)value.Length;
                Writer.Write(value, 26, Buffer);
            }
        }

        public string Name
        {
            get
            {
                return Encoding.Default.GetString(Buffer, 26, Buffer[25]);
            }
            set
            {
                byte[] Saved = Buffer;
                this.Buffer = new byte[28 + 8 + 16 + value.Length];
                if (Saved != null) Saved.CopyTo(Buffer, 0);
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(1107, 2, Buffer);
                Buffer[25] = (byte)value.Length;
                Writer.Write(value, 26, Buffer);
            }
        }
        public List<string> Positions = new List<string>();
        public void Set()
        {
            byte[] Saved = Buffer;
            int len = 0;
            foreach (var Pos in Positions)
                len += Pos.Length;

            this.Buffer = new byte[28 + 16 + 8 + len];
            if (Saved != null) Saved.CopyTo(Buffer, 0);
            Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
            Writer.Write(1107, 2, Buffer);
            Buffer[24] = (byte)(Positions.Count);
            byte start = 25;
            foreach (var Pos in Positions)
            {
                Buffer[start] = (byte)Pos.Length;
                Writer.Write(Pos, (start + 1), Buffer);
                start += (byte)(Pos.Length + 1);
            }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        /// <summary>
        /// Read offset 12 Boolean
        /// </summary>
        public Boolean dwBool
        {
            get { return BitConverter.ToBoolean(Buffer, 12); }
        }
        /// <summary>
        /// Level offset for GuildRequirements
        /// </summary>
        public uint dwParam2
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        /// <summary>
        /// Reborn offset for GuildRequirements
        /// </summary>
        public uint dwParam3
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        /// <summary>
        /// Class offset for GuildRequirements
        /// </summary>
        public uint dwParam4
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }
        public void Deserialize(byte[] Data)
        {
            Buffer = Data;
        }

        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public void SendPromote(Client.GameClient client, ushort typ)
        {
            if (client == null) return;
            if (client.AsMember == null) return;
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            if (client.AsMember.Rank == Enums.GuildMemberRank.GuildLeader)
            {
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.GuildLeader, 1, 1, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.DeputyLeader, (int)client.Guild.RanksCounts[990], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HDeputyLeader, (int)client.Guild.RanksCounts[980], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 650));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonoraryManager, (int)client.Guild.RanksCounts[880], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 320));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonorarySuperv, (int)client.Guild.RanksCounts[840], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 270));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonorarySteward, (int)client.Guild.RanksCounts[680], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 100));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Aide, (int)client.Guild.RanksCounts[602], 6, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Steward, (int)client.Guild.RanksCounts[690], 3, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Follower, (int)client.Guild.RanksCounts[490], 10, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
            }
            if (client.AsMember.Rank == Enums.GuildMemberRank.LeaderSpouse)
            {
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HDeputyLeader, (int)client.Guild.RanksCounts[980], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 650));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonoraryManager, (int)client.Guild.RanksCounts[880], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 320));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonorarySuperv, (int)client.Guild.RanksCounts[840], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 270));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonorarySteward, (int)client.Guild.RanksCounts[680], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 100));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Aide, (int)client.Guild.RanksCounts[602], 10, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Steward, (int)client.Guild.RanksCounts[690], 3, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Follower, (int)client.Guild.RanksCounts[490], 10, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
            }
            if (client.AsMember.Rank == Enums.GuildMemberRank.Manager || client.AsMember.Rank == Enums.GuildMemberRank.HonoraryManager)
            {
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Aide, (int)client.Guild.RanksCounts[602], 6, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
            }
            if (client.AsMember.Rank == Enums.GuildMemberRank.DeputyLeader)
            {
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HDeputyLeader, (int)client.Guild.RanksCounts[980], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 650));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonoraryManager, (int)client.Guild.RanksCounts[880], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 320));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonorarySuperv, (int)client.Guild.RanksCounts[840], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 270));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.HonorarySteward, (int)client.Guild.RanksCounts[680], 4, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 100));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Steward, (int)client.Guild.RanksCounts[690], 3, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Aide, (int)client.Guild.RanksCounts[602], 6, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
                list.Add(this.CreatePromotionString(builder, Enums.GuildMemberRank.Follower, (int)client.Guild.RanksCounts[490], 10, (int)client.Guild.GetMemberPotency(Enums.GuildMemberRank.GuildLeader), 0));
            }
            int extraLength = 0;
            foreach (var str in list) extraLength += str.Length + 1;
            byte[] packet = new byte[28 + 8 + extraLength];
            WriteUInt16((ushort)(packet.Length - 8), 0, packet);
            WriteUInt16((ushort)1107, 2, packet);
            WriteByte((byte)typ, 4, packet);
            WriteStringList(list, 24, packet);
            client.Send(packet);
        }
        private string CreatePromotionString(StringBuilder builder, Enums.GuildMemberRank rank, int occupants, int maxOccupants, int extraBattlePower, int conquerPoints)
        {
            builder.Remove(0, builder.Length);
            builder.Append((int)rank);
            builder.Append(" ");
            builder.Append(occupants);
            builder.Append(" ");
            builder.Append(maxOccupants);
            builder.Append(" ");
            builder.Append(extraBattlePower);
            builder.Append(" ");
            builder.Append(conquerPoints);
            builder.Append(" ");
            return builder.ToString();
        }
    }
}