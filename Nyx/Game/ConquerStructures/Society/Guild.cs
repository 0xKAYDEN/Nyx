using System;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network;
using System.IO;
using System.Text;
using System.Linq;
using Nyx.Server.Game.Features;
using Nyx.Server.Database;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Game.ConquerStructures.Society
{

    public enum ArsenalType
    {
        Headgear,
        Armor,
        Weapon,
        Ring,
        Boots,
        Necklace,
        Fan,
        Tower
    }
    public enum GuildRank
    {
        Agent = 590,
        Aide = 0x25a,
        ArsenalAgent = 0x254,
        ArsFollower = 0x1f0,
        ASupervisor = 0x358,
        CPAgent = 0x255,
        CPFollower = 0x1f1,
        CPSupervisor = 0x359,
        DeputyLeader = 990,
        DeputySteward = 650,
        DLeaderAide = 0x263,
        DLeaderSpouse = 620,
        Follower = 490,
        GSupervisor = 0x356,
        GuideAgent = 0x252,
        GuideFollower = 0x1ee,
        GuildLeader = 0x3e8,
        HDeputyLeader = 980,
        HonoraryManager = 880,
        HonorarySteward = 680,
        HonorarySuperv = 840,
        LeaderSpouse = 920,
        LilyAgent = 0x24f,
        LilyFollower = 0x1eb,
        LilySupervisor = 0x353,
        LSpouseAide = 610,
        Manager = 890,
        ManagerAide = 510,
        ManagerSpouse = 520,
        Member = 200,
        None = 0,
        OrchidAgent = 0x256,
        OrchidFollower = 0x1f2,
        OSupervisor = 0x35a,
        PKAgent = 0x251,
        PKFollower = 0x1ed,
        PKSupervisor = 0x355,
        RoseAgent = 0x250,
        RoseFollower = 0x1ec,
        RoseSupervisor = 0x354,
        SeniorMember = 210,
        SilverAgent = 0x253,
        SilverFollower = 0x1ef,
        SSupervisor = 0x357,
        Steward = 690,
        StewardSpouse = 420,
        Supervisor = 850,
        SupervisorAide = 0x1ff,
        SupervSpouse = 0x209,
        TSupervisor = 0x35b,
        TulipAgent = 0x257,
        TulipFollower = 0x1f3
    }
    public struct RankingList
    {

        public uint EntityID;
        public string Name;
        public ulong SilverDonation;
        public ulong guidedoantion;
        public ulong cpdoantion;
        public ulong arsenaldoantion;
        public ulong totaldoantion;
        public ulong pkdoantion;
        public ulong rosedoantion;
        public ulong liliesdoantion;
        public ulong orchidesdoantion;
        public ulong tuplisedoantion;
        public Enums.GuildMemberRank postion;
        public int rank;
    }
    public class Guild : Writer
    {
        public uint UnionID;
        public static SafeDictionary<uint, Guild> GuildWinners = new SafeDictionary<uint, Guild>(10000);
        public void CalculateCTFRank(bool create_players_reward = false)
        {
            var rank_ctf = Members.Values.Where(p => p.Exploits != 0).OrderByDescending(p => p.Exploits).ToArray();
            for (ushort x = 0; x < rank_ctf.Length; x++)
            {
                var a_mem = rank_ctf[x];
                var mem = Members[a_mem.ID];
                mem.ExploitsRank = (uint)(x + 1);
                if (create_players_reward)
                {
                    uint[] RewardCTF = CalculateRewardCTF(mem.ExploitsRank);
                    mem.CTFSilverReward = RewardCTF[0];
                    mem.CTFCpsReward = RewardCTF[1];
                }
            }
        }
        private uint[] CalculateRewardCTF(uint Rank)
        {
            uint[] rew = new uint[2];
            rew[0] = (CTFdonationSilverold / (Rank + 1));
            rew[1] = (CTFdonationCPsold / (Rank + 1));
            return rew;
        }
        public uint HDeputyLeaderCount;
        public uint DeputyStewardCount;
        public uint ArsenalAgentCount;
        public uint PKSupervisorCount;
        public uint HonoraryManagerCount;
        public uint HonoraryStewardCount;
        public uint AideCount;
        public uint HonorarySupervCount;
        public uint LeaderSpouseCount;
        public uint ManagerCount;

        public static SafeDictionary<uint, Guild> Scores = new SafeDictionary<uint, Guild>(0x2710);
        public uint Points;
        public uint CTFdonationCPs;
        public uint CTFdonationSilver;
        public uint CTFdonationCPsold;
        public uint CTFdonationSilverold;
        // public SafeDictionary<uint, Member> MemberJoined = new SafeDictionary<uint, Member>(0x2710);
        public List<uint> BlackList = new List<uint>();
        public class ClassRequirements
        {
            public const uint
            Trojan = 1,
            Warrior = 2,
            Taoist = 4,
            Archer = 8,
            Ninja = 16,
            Monk = 32,
            Pirate = 64;
        }
        #region BattlePower
        public static float[] SharedBattlepowerPercentage = new float[] { 0, .15f, .20f, .3f, .4f, .5f, .7f, .7f, .8f, .9f, 1f };
        public Arsenal[] Arsenals;
        public bool ArsenalBPChanged = true;
        
        public int UnlockedArsenals
        {
            get
            {
                int num = 0;
                for (int index = 0; index < 8; ++index)
                {
                    if (this.Arsenals[index].Unlocked)
                        ++num;
                }
                return num;
            }
        }
        int arsenal_bp;
        public int ArsenalTotalBattlepower
        {
            get
            {
                return this.arsenal_bp;
            }
            set
            {
                this.arsenal_bp = value;
                foreach (Guild.Member member in this.Members.Values)
                {
                    if (member.IsOnline)
                        member.Client.Entity.GuildBattlePower = this.GetSharedBattlepower(member.Rank);
                }
            }
        }
        public uint GetCurrentArsenalCost()
        {
            int unlockedArsenals = this.UnlockedArsenals;
            if (unlockedArsenals >= 1 && unlockedArsenals <= 2)
                return 5000000U;
            if (unlockedArsenals >= 2 && unlockedArsenals <= 4)
                return 10000000U;
            return unlockedArsenals >= 5 && unlockedArsenals <= 6 ? 15000000U : 20000000U;
        }

        public override int GetHashCode()
        {
            return (int)this.ID;
        }

        public int GetMaxSharedBattlepower(bool force = false)
        {
            if (this.ArsenalBPChanged || force)
            {
                int num1 = 0;
                IOrderedEnumerable<Arsenal> orderedEnumerable = Enumerable.OrderByDescending<Arsenal, uint>((IEnumerable<Arsenal>)this.Arsenals, (Func<Arsenal, uint>)(p => p.TotalSharedBattlePower));
                int num2 = 0;
                foreach (Arsenal arsenal in (IEnumerable<Arsenal>)orderedEnumerable)
                {
                    if (num2 != 5)
                    {
                        num1 += (int)arsenal.TotalSharedBattlePower;
                        ++num2;
                    }
                    else
                        break;
                }
                this.ArsenalTotalBattlepower = num1;
                this.ArsenalBPChanged = false;
                byte num3 = (byte)1;
                foreach (Arsenal arsenal in (IEnumerable<Arsenal>)orderedEnumerable)
                {
                    if (arsenal.TotalSharedBattlePower >= 2U)
                        ++num3;
                }
                this.Level = num3;
            }
            return this.arsenal_bp;
        }

        public uint GetMemberPotency(Enums.GuildMemberRank RankMember)
        {
            uint num = (uint)this.arsenal_bp;
            return num;
        }

        public uint GetSharedBattlepower(int rank)
        {
            return GetMemberPotency((Enums.GuildMemberRank)rank);//(uint)(arsenal_bp * SharedBattlepowerPercentage[rank / 100]);
        } 

        public uint GetSharedBattlepower(Enums.GuildMemberRank rank)
        {
            return this.GetSharedBattlepower((int)rank);
        }
        #endregion
        public void SaveArsenal()
        {
            Database.GuildArsenalTable.Save(this);
        }
        public static Counter GuildCounter;

        public static void GuildProfile(byte[] Packet, Client.GameClient client)
        {
            GuildProfilePacket p = new GuildProfilePacket();
            p.Deserialize(Packet);
            p.Silver = (ushort)client.AsMember.SilverDonation;
            p.Pk = (ushort)client.Entity.PKPoints;
            p.Cps = (uint)client.AsMember.ConquerPointDonation;
            p.Guide = 0;
            client.AsMember.LoadArsenalDonation(client);
            p.Arsenal = (uint)client.AsMember.Arsenal_Donation;
            p.HistorySilvers = 0;
            p.HistoryCps = 0;
            p.HistoryGuide = 0;
            p.HistoryPk = 0;
            client.Send(Packet);
        }

        public class Member : Interfaces.IKnownPerson
        {
            public Member(uint GuildID)
            {
                this.GuildID = GuildID;
            }
            public uint ExploitsRank;
            public uint CTFCpsReward;
            public uint CTFSilverReward;
            public uint Exploits = 10;
            public uint ID
            {
                get;
                set;
            }
            public string Name
            {
                get;
                set;
            }
            public bool IsOnline
            {
                get
                {
                    return Kernel.GamePool.ContainsKey(ID);
                }
            }
            private uint _Class;
            public uint Class
            {
                get
                {
                    return _Class;
                }
                set
                {
                    _Class = value;

                }
            }
            public long LastLogin
            {
                get;
                set;
            }
            public Client.GameClient Client
            {
                get
                {
                    if (!IsOnline) return null;
                    return Kernel.GamePool[ID];
                }
            }
            public ulong SilverDonation
            {
                get;
                set;
            }
            public ulong ConquerPointDonation
            {
                get;
                set;
            }
            public uint GuildID
            {
                get;
                set;
            }
            public Guild Guild
            {
                get
                {
                    return Kernel.Guilds[GuildID];
                }
            }
            public Enums.GuildMemberRank Rank
            {
                get;
                set;
            }
            public byte Level
            {
                get;
                set;
            }
            public NobilityRank NobilityRank
            {
                get;
                set;
            }
            public byte Gender
            {
                get;
                set;
            }

            public List<RankingList> SilversList = new List<RankingList>();
            public List<RankingList> CPSList = new List<RankingList>();
            public List<RankingList> TotalDonationsList = new List<RankingList>();
            public List<RankingList> ArsenalsList = new List<RankingList>();
            public List<RankingList> PKSList = new List<RankingList>();
            public List<RankingList> GuidesList = new List<RankingList>();
            public List<RankingList> TulipsList = new List<RankingList>();
            public List<RankingList> OrchidsList = new List<RankingList>();
            public List<RankingList> RosesList = new List<RankingList>();
            public List<RankingList> LiliesList = new List<RankingList>();

            public ulong TotalDoantion
            {
                get
                {
                    return SilverDonation + ConquerPointDonation + Arsenal_Donation + PKDoantion + GuideDoantion;
                }

            }
            public ulong _Arsenal_Donation;
            public ulong Arsenal_Donation
            {
                get
                {
                    return _Arsenal_Donation;
                }
                set
                {
                    if (value <= 0)
                        value = 0;
                    _Arsenal_Donation = value;
                }
            }
            public void LoadArsenalDonation(Client.GameClient client)
            {
                client.AsMember.Client.GetArsenalDonation();
                using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities").Where("UID", client.Entity.UID))
                using (var reader = new MySqlReader(cmd))
                {
                    if (reader.Read())
                    {
                        client.AsMember.Arsenal_Donation = reader.ReadUInt32("GuildArsenalDonation");
                    }
                }
            }
            private ulong _PKDoantion;
            public ulong PKDoantion
            {
                get
                {
                    return _PKDoantion;
                }
                set
                {
                    _PKDoantion = value;

                }
            }
            private ulong _GuideDoantion;
            public ulong GuideDoantion
            {
                get
                {
                    return _GuideDoantion;
                }
                set
                {
                    _GuideDoantion = value;

                }
            }
            private ulong _RoseDonation;
            public ulong RoseDonation
            {
                get
                {
                    return _RoseDonation;
                }
                set
                {
                    _RoseDonation = value;

                }
            }
            private ulong _OrchidesDonation;
            public ulong OrchidesDonation
            {
                get
                {
                    return _OrchidesDonation;
                }
                set
                {
                    _OrchidesDonation = value;

                }
            }
            private ulong _TulipDonation;
            public ulong TulipDonation
            {
                get
                {
                    return _TulipDonation;
                }
                set
                {
                    _TulipDonation = value;

                }
            }
            private ulong _LiliesDonation;
            public ulong LiliesDonation
            {
                get
                {
                    return _LiliesDonation;
                }
                set
                {
                    _LiliesDonation = value;

                }
            }

        }

        private byte[] Buffer;
        public uint EnrollmentDate;
        public uint AnnouncementDate;
        public uint WarScore;
        public byte GuildLevel
        {
            get { return CalculateLevel(); }
            set
            {
                Buffer[60] = CalculateLevel();
            }
        }
        public byte CalculateLevel()
        {
            if (Arsenals == null) return 1;
            byte Lvl = 1;
            for (int i = 0; i < 8; i++)
                if (Arsenals[i].Donation >= 5000000)
                    Lvl++;
            return Lvl;
        }
        public bool PoleKeeper
        {
            get
            {
                return GuildWar.Pole.Name == Name;
            }
        }

        public Guild(string leadername)
        {
            Buffer = new byte[92 + 8];
            LeaderName = leadername;
            Writer.Write(92, 0, Buffer);
            Writer.Write(1106, 2, Buffer);
            Points = 0;
            Buffer[60] = 1;
            Buffer[48] = 0x2;
            Buffer[49] = 0x1;
            Buffer[60] = (byte)GuildLevel;
            Buffer[75] = 1;
            Buffer[87] = 0x20;
            LevelRequirement = 1;
            Writer.Write((uint)EnrollmentDate, 67, this.Buffer);
            Members = new SafeDictionary<uint, Member>(1000);
            Ally = new SafeDictionary<uint, Guild>(1000);
            Enemy = new SafeDictionary<uint, Guild>(1000);
            Arsenals = new Arsenal[8];
            for (byte i = 0; i < 8; i++)
            {
                Arsenals[i] = new Arsenal(this)
                {
                    Position = (byte)(i + 1)
                };
            }
        }
        public void CalculateCTFRANK(bool create_players_reward = false)
        {
            Guild.Member[] array = (
                from p in this.Members.Values
                where p.Exploits != 0u
                orderby p.Exploits descending
                select p).ToArray<Guild.Member>();
            ushort num = 0;
            while ((int)num < array.Length)
            {
                Guild.Member member = array[(int)num];
                Guild.Member member2 = this.Members[member.ID];
                member2.ExploitsRank = (uint)(num + 1);
                if (create_players_reward)
                {
                    uint[] array2 = this.CalculateRewardCTF(member2.ExploitsRank);
                    member2.CTFSilverReward = array2[0];
                    member2.CTFCpsReward = array2[1];
                }
                num += 1;
            }
        }
       

        public void CreateMembersRank()
        {
            bool flag = false;
            try
            {
                System.Threading.Monitor.Enter(this, ref flag);
                foreach (Guild.Member current in this.Members.Values)
                {
                    if (current.Rank < Enums.GuildMemberRank.LeaderSpouse)
                    {
                        if (this.RanksCounts[(int)current.Rank] > 0)
                        {
                            ushort[] expr_59_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_59_cp_1 = current.Rank;
                            expr_59_cp_0[(int)expr_59_cp_1] = (ushort)(expr_59_cp_0[(int)expr_59_cp_1] - 1);
                        }
                        current.Rank = Enums.GuildMemberRank.Member;
                        ushort[] expr_83_cp_0 = this.RanksCounts;
                        Enums.GuildMemberRank expr_83_cp_1 = current.Rank;
                        expr_83_cp_0[(int)expr_83_cp_1] = (ushort)(expr_83_cp_0[(int)expr_83_cp_1] + 1);
                    }
                }
                byte b = 0;
                Guild.Member[] array = (
                    from memb in this.Members.Values
                    orderby memb.Arsenal_Donation descending
                    select memb).ToArray<Guild.Member>();
                byte b2 = 0;
                while ((int)b2 < array.Length)
                {
                    Guild.Member member = array[(int)b2];
                    if (member.Rank <= Enums.GuildMemberRank.Manager)
                    {
                        if (b < 5)
                        {
                            if (this.RanksCounts[(int)member.Rank] > 0)
                            {
                                ushort[] expr_125_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_125_cp_1 = member.Rank;
                                expr_125_cp_0[(int)expr_125_cp_1] = (ushort)(expr_125_cp_0[(int)expr_125_cp_1] - 1);
                            }
                            member.Rank = Enums.GuildMemberRank.Manager;
                            ushort[] expr_151_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_151_cp_1 = member.Rank;
                            expr_151_cp_0[(int)expr_151_cp_1] = (ushort)(expr_151_cp_0[(int)expr_151_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 7)
                            {
                                if (this.RanksCounts[(int)member.Rank] > 0)
                                {
                                    ushort[] expr_190_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_190_cp_1 = member.Rank;
                                    expr_190_cp_0[(int)expr_190_cp_1] = (ushort)(expr_190_cp_0[(int)expr_190_cp_1] - 1);
                                }
                                member.Rank = Enums.GuildMemberRank.HonoraryManager;
                                ushort[] expr_1BC_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_1BC_cp_1 = member.Rank;
                                expr_1BC_cp_0[(int)expr_1BC_cp_1] = (ushort)(expr_1BC_cp_0[(int)expr_1BC_cp_1] + 1);
                                b += 1;
                            }
                            else
                            {
                                if (b < 9)
                                {
                                    if (this.RanksCounts[(int)member.Rank] > 0)
                                    {
                                        ushort[] expr_1FC_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_1FC_cp_1 = member.Rank;
                                        expr_1FC_cp_0[(int)expr_1FC_cp_1] = (ushort)(expr_1FC_cp_0[(int)expr_1FC_cp_1] - 1);
                                    }
                                    member.Rank = Enums.GuildMemberRank.Supervisor;
                                    ushort[] expr_228_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_228_cp_1 = member.Rank;
                                    expr_228_cp_0[(int)expr_228_cp_1] = (ushort)(expr_228_cp_0[(int)expr_228_cp_1] + 1);
                                    b += 1;
                                }
                                else
                                {
                                    if (b < 13)
                                    {
                                        if (member.Rank <= Enums.GuildMemberRank.Steward)
                                        {
                                            if (this.RanksCounts[(int)member.Rank] > 0)
                                            {
                                                ushort[] expr_279_cp_0 = this.RanksCounts;
                                                Enums.GuildMemberRank expr_279_cp_1 = member.Rank;
                                                expr_279_cp_0[(int)expr_279_cp_1] = (ushort)(expr_279_cp_0[(int)expr_279_cp_1] - 1);
                                            }
                                            member.Rank = Enums.GuildMemberRank.Steward;
                                            ushort[] expr_2A5_cp_0 = this.RanksCounts;
                                            Enums.GuildMemberRank expr_2A5_cp_1 = member.Rank;
                                            expr_2A5_cp_0[(int)expr_2A5_cp_1] = (ushort)(expr_2A5_cp_0[(int)expr_2A5_cp_1] + 1);
                                            b += 1;
                                        }
                                    }
                                    else
                                    {
                                        if (b >= 15)
                                        {
                                            break;
                                        }
                                        if (member.Rank <= Enums.GuildMemberRank.ArsFollower)
                                        {
                                            if (this.RanksCounts[(int)member.Rank] > 0)
                                            {
                                                ushort[] expr_2F0_cp_0 = this.RanksCounts;
                                                Enums.GuildMemberRank expr_2F0_cp_1 = member.Rank;
                                                expr_2F0_cp_0[(int)expr_2F0_cp_1] = (ushort)(expr_2F0_cp_0[(int)expr_2F0_cp_1] - 1);
                                            }
                                            member.Rank = Enums.GuildMemberRank.ArsFollower;
                                            ushort[] expr_31C_cp_0 = this.RanksCounts;
                                            Enums.GuildMemberRank expr_31C_cp_1 = member.Rank;
                                            expr_31C_cp_0[(int)expr_31C_cp_1] = (ushort)(expr_31C_cp_0[(int)expr_31C_cp_1] + 1);
                                            b += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    b2 += 1;
                }
                this.RankArsenalDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.ConquerPointDonation descending
                    select memb).ToArray<Guild.Member>();
                byte b3 = 0;
                while ((int)b3 < array.Length)
                {
                    Guild.Member member2 = array[(int)b3];
                    if (member2.Rank <= Enums.GuildMemberRank.CPSupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member2.Rank] > 0)
                            {
                                ushort[] expr_3C4_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_3C4_cp_1 = member2.Rank;
                                expr_3C4_cp_0[(int)expr_3C4_cp_1] = (ushort)(expr_3C4_cp_0[(int)expr_3C4_cp_1] - 1);
                            }
                            member2.Rank = Enums.GuildMemberRank.CPSupervisor;
                            ushort[] expr_3F0_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_3F0_cp_1 = member2.Rank;
                            expr_3F0_cp_0[(int)expr_3F0_cp_1] = (ushort)(expr_3F0_cp_0[(int)expr_3F0_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member2.Rank <= Enums.GuildMemberRank.CPAgent)
                                {
                                    if (this.RanksCounts[(int)member2.Rank] > 0)
                                    {
                                        ushort[] expr_440_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_440_cp_1 = member2.Rank;
                                        expr_440_cp_0[(int)expr_440_cp_1] = (ushort)(expr_440_cp_0[(int)expr_440_cp_1] - 1);
                                    }
                                    member2.Rank = Enums.GuildMemberRank.CPAgent;
                                    ushort[] expr_46C_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_46C_cp_1 = member2.Rank;
                                    expr_46C_cp_0[(int)expr_46C_cp_1] = (ushort)(expr_46C_cp_0[(int)expr_46C_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member2.Rank <= Enums.GuildMemberRank.CPFollower)
                                {
                                    if (this.RanksCounts[(int)member2.Rank] > 0)
                                    {
                                        ushort[] expr_4B9_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_4B9_cp_1 = member2.Rank;
                                        expr_4B9_cp_0[(int)expr_4B9_cp_1] = (ushort)(expr_4B9_cp_0[(int)expr_4B9_cp_1] - 1);
                                    }
                                    member2.Rank = Enums.GuildMemberRank.CPFollower;
                                    ushort[] expr_4E5_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_4E5_cp_1 = member2.Rank;
                                    expr_4E5_cp_0[(int)expr_4E5_cp_1] = (ushort)(expr_4E5_cp_0[(int)expr_4E5_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b3 += 1;
                }
                this.RankCPDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.PKDoantion descending
                    select memb).ToArray<Guild.Member>();
                byte b4 = 0;
                while ((int)b4 < array.Length)
                {
                    Guild.Member member3 = array[(int)b4];
                    if (member3.Rank <= Enums.GuildMemberRank.PKSupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member3.Rank] > 0)
                            {
                                ushort[] expr_590_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_590_cp_1 = member3.Rank;
                                expr_590_cp_0[(int)expr_590_cp_1] = (ushort)(expr_590_cp_0[(int)expr_590_cp_1] - 1);
                            }
                            member3.Rank = Enums.GuildMemberRank.PKSupervisor;
                            ushort[] expr_5BC_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_5BC_cp_1 = member3.Rank;
                            expr_5BC_cp_0[(int)expr_5BC_cp_1] = (ushort)(expr_5BC_cp_0[(int)expr_5BC_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member3.Rank <= Enums.GuildMemberRank.PKAgent)
                                {
                                    if (this.RanksCounts[(int)member3.Rank] > 0)
                                    {
                                        ushort[] expr_60C_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_60C_cp_1 = member3.Rank;
                                        expr_60C_cp_0[(int)expr_60C_cp_1] = (ushort)(expr_60C_cp_0[(int)expr_60C_cp_1] - 1);
                                    }
                                    member3.Rank = Enums.GuildMemberRank.PKAgent;
                                    ushort[] expr_638_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_638_cp_1 = member3.Rank;
                                    expr_638_cp_0[(int)expr_638_cp_1] = (ushort)(expr_638_cp_0[(int)expr_638_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member3.Rank <= Enums.GuildMemberRank.PKFollower)
                                {
                                    if (this.RanksCounts[(int)member3.Rank] > 0)
                                    {
                                        ushort[] expr_685_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_685_cp_1 = member3.Rank;
                                        expr_685_cp_0[(int)expr_685_cp_1] = (ushort)(expr_685_cp_0[(int)expr_685_cp_1] - 1);
                                    }
                                    member3.Rank = Enums.GuildMemberRank.PKFollower;
                                    ushort[] expr_6B1_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_6B1_cp_1 = member3.Rank;
                                    expr_6B1_cp_0[(int)expr_6B1_cp_1] = (ushort)(expr_6B1_cp_0[(int)expr_6B1_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b4 += 1;
                }
                this.RankPkDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.RoseDonation descending
                    select memb).ToArray<Guild.Member>();
                byte b5 = 0;
                while ((int)b5 < array.Length)
                {
                    Guild.Member member4 = array[(int)b5];
                    if (member4.Rank <= Enums.GuildMemberRank.RoseSupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member4.Rank] > 0)
                            {
                                ushort[] expr_75C_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_75C_cp_1 = member4.Rank;
                                expr_75C_cp_0[(int)expr_75C_cp_1] = (ushort)(expr_75C_cp_0[(int)expr_75C_cp_1] - 1);
                            }
                            member4.Rank = Enums.GuildMemberRank.RoseSupervisor;
                            ushort[] expr_788_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_788_cp_1 = member4.Rank;
                            expr_788_cp_0[(int)expr_788_cp_1] = (ushort)(expr_788_cp_0[(int)expr_788_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member4.Rank <= Enums.GuildMemberRank.RoseAgent)
                                {
                                    if (this.RanksCounts[(int)member4.Rank] > 0)
                                    {
                                        ushort[] expr_7D8_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_7D8_cp_1 = member4.Rank;
                                        expr_7D8_cp_0[(int)expr_7D8_cp_1] = (ushort)(expr_7D8_cp_0[(int)expr_7D8_cp_1] - 1);
                                    }
                                    member4.Rank = Enums.GuildMemberRank.RoseAgent;
                                    ushort[] expr_804_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_804_cp_1 = member4.Rank;
                                    expr_804_cp_0[(int)expr_804_cp_1] = (ushort)(expr_804_cp_0[(int)expr_804_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member4.Rank <= Enums.GuildMemberRank.RoseFollower)
                                {
                                    if (this.RanksCounts[(int)member4.Rank] > 0)
                                    {
                                        ushort[] expr_851_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_851_cp_1 = member4.Rank;
                                        expr_851_cp_0[(int)expr_851_cp_1] = (ushort)(expr_851_cp_0[(int)expr_851_cp_1] - 1);
                                    }
                                    member4.Rank = Enums.GuildMemberRank.RoseFollower;
                                    ushort[] expr_87D_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_87D_cp_1 = member4.Rank;
                                    expr_87D_cp_0[(int)expr_87D_cp_1] = (ushort)(expr_87D_cp_0[(int)expr_87D_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b5 += 1;
                }
                this.RankRosseDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.LiliesDonation descending
                    select memb).ToArray<Guild.Member>();
                byte b6 = 0;
                while ((int)b6 < array.Length)
                {
                    Guild.Member member5 = array[(int)b6];
                    if (member5.Rank <= Enums.GuildMemberRank.LilySupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member5.Rank] > 0)
                            {
                                ushort[] expr_928_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_928_cp_1 = member5.Rank;
                                expr_928_cp_0[(int)expr_928_cp_1] = (ushort)(expr_928_cp_0[(int)expr_928_cp_1] - 1);
                            }
                            member5.Rank = Enums.GuildMemberRank.LilySupervisor;
                            ushort[] expr_954_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_954_cp_1 = member5.Rank;
                            expr_954_cp_0[(int)expr_954_cp_1] = (ushort)(expr_954_cp_0[(int)expr_954_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member5.Rank <= Enums.GuildMemberRank.LilyAgent)
                                {
                                    if (this.RanksCounts[(int)member5.Rank] > 0)
                                    {
                                        ushort[] expr_9A4_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_9A4_cp_1 = member5.Rank;
                                        expr_9A4_cp_0[(int)expr_9A4_cp_1] = (ushort)(expr_9A4_cp_0[(int)expr_9A4_cp_1] - 1);
                                    }
                                    member5.Rank = Enums.GuildMemberRank.LilyAgent;
                                    ushort[] expr_9D0_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_9D0_cp_1 = member5.Rank;
                                    expr_9D0_cp_0[(int)expr_9D0_cp_1] = (ushort)(expr_9D0_cp_0[(int)expr_9D0_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member5.Rank <= Enums.GuildMemberRank.LilyFollower)
                                {
                                    if (this.RanksCounts[(int)member5.Rank] > 0)
                                    {
                                        ushort[] expr_A1D_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_A1D_cp_1 = member5.Rank;
                                        expr_A1D_cp_0[(int)expr_A1D_cp_1] = (ushort)(expr_A1D_cp_0[(int)expr_A1D_cp_1] - 1);
                                    }
                                    member5.Rank = Enums.GuildMemberRank.LilyFollower;
                                    ushort[] expr_A49_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_A49_cp_1 = member5.Rank;
                                    expr_A49_cp_0[(int)expr_A49_cp_1] = (ushort)(expr_A49_cp_0[(int)expr_A49_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b6 += 1;
                }
                this.RankLiliesDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.TulipDonation descending
                    select memb).ToArray<Guild.Member>();
                byte b7 = 0;
                while ((int)b7 < array.Length)
                {
                    Guild.Member member6 = array[(int)b7];
                    if (member6.Rank <= Enums.GuildMemberRank.TSupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member6.Rank] > 0)
                            {
                                ushort[] expr_AF4_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_AF4_cp_1 = member6.Rank;
                                expr_AF4_cp_0[(int)expr_AF4_cp_1] = (ushort)(expr_AF4_cp_0[(int)expr_AF4_cp_1] - 1);
                            }
                            member6.Rank = Enums.GuildMemberRank.TSupervisor;
                            ushort[] expr_B20_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_B20_cp_1 = member6.Rank;
                            expr_B20_cp_0[(int)expr_B20_cp_1] = (ushort)(expr_B20_cp_0[(int)expr_B20_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member6.Rank <= Enums.GuildMemberRank.TulipAgent)
                                {
                                    if (this.RanksCounts[(int)member6.Rank] > 0)
                                    {
                                        ushort[] expr_B70_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_B70_cp_1 = member6.Rank;
                                        expr_B70_cp_0[(int)expr_B70_cp_1] = (ushort)(expr_B70_cp_0[(int)expr_B70_cp_1] - 1);
                                    }
                                    member6.Rank = Enums.GuildMemberRank.TulipAgent;
                                    ushort[] expr_B9C_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_B9C_cp_1 = member6.Rank;
                                    expr_B9C_cp_0[(int)expr_B9C_cp_1] = (ushort)(expr_B9C_cp_0[(int)expr_B9C_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member6.Rank <= Enums.GuildMemberRank.TulipFollower)
                                {
                                    if (this.RanksCounts[(int)member6.Rank] > 0)
                                    {
                                        ushort[] expr_BE9_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_BE9_cp_1 = member6.Rank;
                                        expr_BE9_cp_0[(int)expr_BE9_cp_1] = (ushort)(expr_BE9_cp_0[(int)expr_BE9_cp_1] - 1);
                                    }
                                    member6.Rank = Enums.GuildMemberRank.TulipFollower;
                                    ushort[] expr_C15_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_C15_cp_1 = member6.Rank;
                                    expr_C15_cp_0[(int)expr_C15_cp_1] = (ushort)(expr_C15_cp_0[(int)expr_C15_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b7 += 1;
                }
                this.RankTulipsDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.OrchidesDonation descending
                    select memb).ToArray<Guild.Member>();
                byte b8 = 0;
                while ((int)b8 < array.Length)
                {
                    Guild.Member member7 = array[(int)b8];
                    if (member7.Rank <= Enums.GuildMemberRank.OSupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member7.Rank] > 0)
                            {
                                ushort[] expr_CC0_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_CC0_cp_1 = member7.Rank;
                                expr_CC0_cp_0[(int)expr_CC0_cp_1] = (ushort)(expr_CC0_cp_0[(int)expr_CC0_cp_1] - 1);
                            }
                            member7.Rank = Enums.GuildMemberRank.OSupervisor;
                            ushort[] expr_CEC_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_CEC_cp_1 = member7.Rank;
                            expr_CEC_cp_0[(int)expr_CEC_cp_1] = (ushort)(expr_CEC_cp_0[(int)expr_CEC_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member7.Rank <= Enums.GuildMemberRank.OrchidAgent)
                                {
                                    if (this.RanksCounts[(int)member7.Rank] > 0)
                                    {
                                        ushort[] expr_D3C_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_D3C_cp_1 = member7.Rank;
                                        expr_D3C_cp_0[(int)expr_D3C_cp_1] = (ushort)(expr_D3C_cp_0[(int)expr_D3C_cp_1] - 1);
                                    }
                                    member7.Rank = Enums.GuildMemberRank.OrchidAgent;
                                    ushort[] expr_D68_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_D68_cp_1 = member7.Rank;
                                    expr_D68_cp_0[(int)expr_D68_cp_1] = (ushort)(expr_D68_cp_0[(int)expr_D68_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member7.Rank <= Enums.GuildMemberRank.OrchidFollower)
                                {
                                    if (this.RanksCounts[(int)member7.Rank] > 0)
                                    {
                                        ushort[] expr_DB5_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_DB5_cp_1 = member7.Rank;
                                        expr_DB5_cp_0[(int)expr_DB5_cp_1] = (ushort)(expr_DB5_cp_0[(int)expr_DB5_cp_1] - 1);
                                    }
                                    member7.Rank = Enums.GuildMemberRank.OrchidFollower;
                                    ushort[] expr_DE1_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_DE1_cp_1 = member7.Rank;
                                    expr_DE1_cp_0[(int)expr_DE1_cp_1] = (ushort)(expr_DE1_cp_0[(int)expr_DE1_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b8 += 1;
                }
                this.RankOrchidsDonations = array.ToArray<Guild.Member>();
                array = (
                    from memb in this.Members.Values
                    orderby memb.TotalDoantion descending
                    select memb).ToArray<Guild.Member>();
                b = 0;
                byte b9 = 0;
                while ((int)b9 < array.Length)
                {
                    Guild.Member member8 = array[(int)b9];
                    if (member8.Rank <= Enums.GuildMemberRank.HDeputyLeader)
                    {
                        if (b < 2)
                        {
                            if (this.RanksCounts[(int)member8.Rank] > 0)
                            {
                                ushort[] expr_E8C_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_E8C_cp_1 = member8.Rank;
                                expr_E8C_cp_0[(int)expr_E8C_cp_1] = (ushort)(expr_E8C_cp_0[(int)expr_E8C_cp_1] - 1);
                            }
                            member8.Rank = Enums.GuildMemberRank.HDeputyLeader;
                            ushort[] expr_EB8_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_EB8_cp_1 = member8.Rank;
                            expr_EB8_cp_0[(int)expr_EB8_cp_1] = (ushort)(expr_EB8_cp_0[(int)expr_EB8_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b >= 4)
                            {
                                break;
                            }
                            if (member8.Rank <= Enums.GuildMemberRank.HonorarySteward)
                            {
                                if (this.RanksCounts[(int)member8.Rank] > 0)
                                {
                                    ushort[] expr_F05_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_F05_cp_1 = member8.Rank;
                                    expr_F05_cp_0[(int)expr_F05_cp_1] = (ushort)(expr_F05_cp_0[(int)expr_F05_cp_1] - 1);
                                }
                                member8.Rank = Enums.GuildMemberRank.HonorarySteward;
                                ushort[] expr_F31_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_F31_cp_1 = member8.Rank;
                                expr_F31_cp_0[(int)expr_F31_cp_1] = (ushort)(expr_F31_cp_0[(int)expr_F31_cp_1] + 1);
                                b += 1;
                            }
                        }
                    }
                    b9 += 1;
                }
                this.RankTotalDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.SilverDonation descending
                    select memb).ToArray<Guild.Member>();
                byte b10 = 0;
                while ((int)b10 < array.Length)
                {
                    Guild.Member member9 = array[(int)b10];
                    if (member9.Rank <= Enums.GuildMemberRank.SSupervisor)
                    {
                        if (b < 5)
                        {
                            if (this.RanksCounts[(int)member9.Rank] > 0)
                            {
                                ushort[] expr_FDC_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_FDC_cp_1 = member9.Rank;
                                expr_FDC_cp_0[(int)expr_FDC_cp_1] = (ushort)(expr_FDC_cp_0[(int)expr_FDC_cp_1] - 1);
                            }
                            member9.Rank = Enums.GuildMemberRank.SSupervisor;
                            ushort[] expr_1008_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_1008_cp_1 = member9.Rank;
                            expr_1008_cp_0[(int)expr_1008_cp_1] = (ushort)(expr_1008_cp_0[(int)expr_1008_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 7)
                            {
                                if (member9.Rank <= Enums.GuildMemberRank.SilverAgent)
                                {
                                    if (this.RanksCounts[(int)member9.Rank] > 0)
                                    {
                                        ushort[] expr_1058_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_1058_cp_1 = member9.Rank;
                                        expr_1058_cp_0[(int)expr_1058_cp_1] = (ushort)(expr_1058_cp_0[(int)expr_1058_cp_1] - 1);
                                    }
                                    member9.Rank = Enums.GuildMemberRank.SilverAgent;
                                    ushort[] expr_1084_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_1084_cp_1 = member9.Rank;
                                    expr_1084_cp_0[(int)expr_1084_cp_1] = (ushort)(expr_1084_cp_0[(int)expr_1084_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 9)
                                {
                                    break;
                                }
                                if (member9.Rank <= Enums.GuildMemberRank.SilverFollower)
                                {
                                    if (this.RanksCounts[(int)member9.Rank] > 0)
                                    {
                                        ushort[] expr_10D2_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_10D2_cp_1 = member9.Rank;
                                        expr_10D2_cp_0[(int)expr_10D2_cp_1] = (ushort)(expr_10D2_cp_0[(int)expr_10D2_cp_1] - 1);
                                    }
                                    member9.Rank = Enums.GuildMemberRank.SilverFollower;
                                    ushort[] expr_10FE_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_10FE_cp_1 = member9.Rank;
                                    expr_10FE_cp_0[(int)expr_10FE_cp_1] = (ushort)(expr_10FE_cp_0[(int)expr_10FE_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b10 += 1;
                }
                this.RankSilversDonations = array.ToArray<Guild.Member>();
                b = 0;
                array = (
                    from memb in this.Members.Values
                    orderby memb.GuideDoantion descending
                    select memb).ToArray<Guild.Member>();
                byte b11 = 0;
                while ((int)b11 < array.Length)
                {
                    Guild.Member member10 = array[(int)b11];
                    if (member10.Rank <= Enums.GuildMemberRank.GSupervisor)
                    {
                        if (b < 3)
                        {
                            if (this.RanksCounts[(int)member10.Rank] > 0)
                            {
                                ushort[] expr_11A9_cp_0 = this.RanksCounts;
                                Enums.GuildMemberRank expr_11A9_cp_1 = member10.Rank;
                                expr_11A9_cp_0[(int)expr_11A9_cp_1] = (ushort)(expr_11A9_cp_0[(int)expr_11A9_cp_1] - 1);
                            }
                            member10.Rank = Enums.GuildMemberRank.GSupervisor;
                            ushort[] expr_11D5_cp_0 = this.RanksCounts;
                            Enums.GuildMemberRank expr_11D5_cp_1 = member10.Rank;
                            expr_11D5_cp_0[(int)expr_11D5_cp_1] = (ushort)(expr_11D5_cp_0[(int)expr_11D5_cp_1] + 1);
                            b += 1;
                        }
                        else
                        {
                            if (b < 5)
                            {
                                if (member10.Rank <= Enums.GuildMemberRank.GuideAgent)
                                {
                                    if (this.RanksCounts[(int)member10.Rank] > 0)
                                    {
                                        ushort[] expr_1225_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_1225_cp_1 = member10.Rank;
                                        expr_1225_cp_0[(int)expr_1225_cp_1] = (ushort)(expr_1225_cp_0[(int)expr_1225_cp_1] - 1);
                                    }
                                    member10.Rank = Enums.GuildMemberRank.GuideAgent;
                                    ushort[] expr_1251_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_1251_cp_1 = member10.Rank;
                                    expr_1251_cp_0[(int)expr_1251_cp_1] = (ushort)(expr_1251_cp_0[(int)expr_1251_cp_1] + 1);
                                    b += 1;
                                }
                            }
                            else
                            {
                                if (b >= 7)
                                {
                                    break;
                                }
                                if (member10.Rank <= Enums.GuildMemberRank.GuideFollower)
                                {
                                    if (this.RanksCounts[(int)member10.Rank] > 0)
                                    {
                                        ushort[] expr_129E_cp_0 = this.RanksCounts;
                                        Enums.GuildMemberRank expr_129E_cp_1 = member10.Rank;
                                        expr_129E_cp_0[(int)expr_129E_cp_1] = (ushort)(expr_129E_cp_0[(int)expr_129E_cp_1] - 1);
                                    }
                                    member10.Rank = Enums.GuildMemberRank.GuideFollower;
                                    ushort[] expr_12CA_cp_0 = this.RanksCounts;
                                    Enums.GuildMemberRank expr_12CA_cp_1 = member10.Rank;
                                    expr_12CA_cp_0[(int)expr_12CA_cp_1] = (ushort)(expr_12CA_cp_0[(int)expr_12CA_cp_1] + 1);
                                    b += 1;
                                }
                            }
                        }
                    }
                    b11 += 1;
                }
                this.RankGuideDonations = array.ToArray<Guild.Member>();
            }
            finally
            {
                if (flag)
                {
                    System.Threading.Monitor.Exit(this);
                }
            }
        }

        public uint ID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }

        public ulong SilverFund
        {
            get { return BitConverter.ToUInt64(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }

        public uint ConquerPointFund
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Writer.Write(value, 20, Buffer); }
        }

        public uint MemberCount
        {
            get { return BitConverter.ToUInt32(Buffer, 24); }
            set { Writer.Write(value, 24, Buffer); }
        }

        public uint LevelRequirement
        {
            get { return BitConverter.ToUInt32(Buffer, 48); }
            set { Writer.Write(value, 48, Buffer); }
        }

        public uint RebornRequirement
        {
            get { return BitConverter.ToUInt32(Buffer, 52); }
            set { Writer.Write(value, 52, Buffer); }
        }

        public uint ClassRequirement
        {
            get { return BitConverter.ToUInt32(Buffer, 56); }
            set { Writer.Write(value, 56, Buffer); }
        }

        public bool AllowTrojans
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Trojan) != ClassRequirements.Trojan);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Trojan;
            }
        }
        public bool AllowWarriors
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Warrior) != ClassRequirements.Warrior);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Warrior;
            }
        }
        public bool AllowTaoists
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Taoist) != ClassRequirements.Taoist);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Taoist;
            }
        }
        public bool AllowArchers
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Archer) != ClassRequirements.Archer);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Archer;
            }
        }
        public bool AllowNinjas
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Ninja) != ClassRequirements.Ninja);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Ninja;
            }
        }
        public bool AllowMonks
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Monk) != ClassRequirements.Monk);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Monk;
            }
        }
        public bool AllowPirates
        {
            get
            {
                return ((ClassRequirement & ClassRequirements.Pirate) != ClassRequirements.Pirate);
            }
            set
            {
                ClassRequirement ^= ClassRequirements.Pirate;
            }
        }


        public uint DeputyLeaderCount;

        public byte Level
        {
            get
            {
                if (Losts == 0)
                    return Buffer[60];
                else
                    return 0;
            }
            set
            {
                Buffer[60] = 0;
                if (Losts == 0)
                    Buffer[60] = (byte)(Math.Min(Wins, 100));
            }
        }

        public string Name;

        public SafeDictionary<uint, Member> Members;
        public SafeDictionary<uint, Guild> Ally, Enemy;
        public uint Wins;
        public uint Losts;
        public uint cp_donaion = 0;
        public uint money_donation = 0;
        public uint honor_donation = 0;
        public uint pkp_donation = 0;
        public uint rose_donation = 0;
        public uint tuil_donation = 0;
        public uint orchid_donation = 0;
        public uint lilies_donation = 0;

        public string Bulletin;

        public Member Leader;
        private string leaderName;
        public uint EWarScore;
        public uint WarofKings;
        public uint CTFPoints;
        public uint CTFReward = 0;
        public uint CTFFlagScore;
        public string LeaderName
        {
            get
            {
                return leaderName;
            }
            set
            {
                leaderName = value;
                Writer.Write(value, 32, Buffer);
            }
        }
        public static Boolean CheckNameExist(String Name)
        {
            foreach (Guild guilds in Kernel.Guilds.Values)
            {
                if (guilds.Name == Name)
                    return true;
            }
            return false;
        }
        public bool Create(string name)
        {
            if (name.Length < 16)
            {
                Name = name;
                SilverFund = 500000;
                Level = 1;

                EnrollmentDate = Albetros.Core.Common.TimeGet(Albetros.Core.TimeType.Day);
                Members.Add(Leader.ID, Leader);
                try
                {
                    Database.GuildTable.Create(this);
                }
                catch { return false; }
                Kernel.Guilds.Add(ID, this);
                Message message = null;
                message = new Message("Congratulations, " + leaderName + " has created guild " + name + " Succesfully!", System.Drawing.Color.White, Message.World);
                foreach (Client.GameClient client in Kernel.GamePool.Values)
                {
                    client.Send(message);
                }
                return true;
            }
            return false;
        }

        public void AddMember(Client.GameClient client)
        {
            if (client.AsMember == null && client.Guild == null)
            {
                client.AsMember = new Member(ID)
                {
                    ID = client.Entity.UID,
                    Level = client.Entity.Level,
                    Name = client.Entity.Name,
                    Rank = Nyx.Server.Game.Enums.GuildMemberRank.Member,
                    Class = client.Entity.Class
                };
                if (Nobility.Board.ContainsKey(client.Entity.UID))
                {
                    client.AsMember.NobilityRank = Nobility.Board[client.Entity.UID].Rank;
                    client.AsMember.Gender = Nobility.Board[client.Entity.UID].Gender;
                }
                MemberCount++;
                client.Guild = this;
                client.Entity.GuildID = (ushort)client.Guild.ID;

                client.Entity.GuildRank = (ushort)client.AsMember.Rank;

                client.Entity.GuildBattlePower = GetSharedBattlepower(client.AsMember.Rank);
                for (int i = 0; i < client.ArsenalDonations.Length; i++)
                    client.ArsenalDonations[i] = 0;
                Database.EntityTable.UpdateGuildID(client);
                Database.EntityTable.UpdateGuildRank(client);
                Members.Add(client.Entity.UID, client.AsMember);
                SendGuild(client);
                client.Screen.FullWipe();
                client.Screen.Reload(null);
                SendGuildMessage(new Message(client.AsMember.Name + " has joined our guild.", System.Drawing.Color.Black, Message.Guild));
                GuildMinDonations guildMinDonations = new GuildMinDonations(31);
                guildMinDonations.AprendGuild(this);
                client.Send(guildMinDonations.Encode());
            }
        }

        public void SendMembers(Client.GameClient client, ushort page)
        {
            MemoryStream strm = new MemoryStream();
            BinaryWriter wtr = new BinaryWriter(strm);
            wtr.Write((ushort)0);
            wtr.Write((ushort)2102);
            wtr.Write((uint)0);
            wtr.Write((uint)page);
            int left = (int)MemberCount - page;
            if (left > 12)
                left = 12;
            if (left < 0)
                left = 0;
            wtr.Write((uint)left);
            int count = 0;
            int maxmem = page + 12;
            int minmem = page;
            List<Member> online = new List<Member>(250);
            List<Member> offline = new List<Member>(250);
            foreach (Member member in Members.Values)
            {
                if (member.IsOnline)
                    online.Add(member);
                else
                    offline.Add(member);
            }
            var unite = online.Union<Member>(offline);
            foreach (Member member in unite)
            {
                if (count >= minmem && count < maxmem)
                {
                    wtr.Write((uint)0);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i < member.Name.Length)
                        {
                            wtr.Write((byte)member.Name[i]);
                        }
                        else
                            wtr.Write((byte)0);
                    }
                    wtr.Write((uint)(member.IsOnline ? 1 : 0));
                    wtr.Write((uint)member.NobilityRank);
                    if (member.Gender == 3) wtr.Write((uint)(member.Gender - 2));
                    else wtr.Write((uint)(member.Gender + 1));
                    wtr.Write((uint)member.Level);
                    wtr.Write((uint)member.Rank);
                    wtr.Write((uint)0);
                    wtr.Write((uint)member.TotalDoantion);
                    wtr.Write((uint)0);
                    if (member.IsOnline) wtr.Write((uint)Client.GameClient.GetClientFromID(member.ID).Entity.TotalPerfectionScore);
                    else
                    {
                        if (MsgUserAbilityScore.RankingList.Where(i => i.UID == member.ID).Count() != 0)
                        {
                            wtr.Write((uint)MsgUserAbilityScore.RankingList.Where(i => i.UID == member.ID).FirstOrDefault().TotalPerfectionScore);
                        }
                        else
                        {
                            wtr.Write((uint)0);
                        }
                    }
                    wtr.Write((uint)member.Class);
                    wtr.Write((uint)(client.MemberLastLogin2 - member.LastLogin));
                    wtr.Write((uint)0);
                }
                count++;
            }
            int packetlength = (int)strm.Length;
            strm.Position = 0;
            wtr.Write((ushort)packetlength);
            strm.Position = strm.Length;
            wtr.Write(ASCIIEncoding.ASCII.GetBytes("TQServer"));
            strm.Position = 0;
            byte[] buf = new byte[strm.Length];
            strm.Read(buf, 0, buf.Length);
            wtr.Close();
            strm.Close();
            client.Send(buf);
        }

        public void SendGuildMessage(Interfaces.IPacket message)
        {
            foreach (Member member in Members.Values)
            {
                if (member.IsOnline)
                {
                    member.Client.Send(message);
                }
            }
        }
        public Member GetMemberByName(string membername)
        {
            foreach (Member member in Members.Values)
            {
                if (member.Name == membername)
                {
                    return member;
                }
            }
            return null;
        }
        public bool GetMember(string name, out Guild.Member getmem)
        {
            foreach (Guild.Member current in this.Members.Values)
            {
                if (current.Name == name)
                {
                    getmem = current;
                    return true;
                }
            }
            getmem = null;
            return false;
        }
        public void ExpelMemberOld(string membername, bool ownquit)
        {
            Member member = GetMemberByName(membername);
            if (member != null)
            {
                if (member.IsOnline)
                    PacketHandler.UninscribeAllItems(member.Client);
                else
                    foreach (var arsenal in Arsenals)
                        arsenal.RemoveInscribedItemsBy(member.ID);

                if (ownquit)
                    SendGuildMessage(new Message(member.Name + " has quit our guild.", System.Drawing.Color.Black, Message.Guild));
                else
                    SendGuildMessage(new Message(member.Name + " have been expelled from our guild.", System.Drawing.Color.Black, Message.Guild));
                uint uid = member.ID;
                if (member.Rank == Enums.GuildMemberRank.DeputyLeader)
                {
                    DeputyLeaderCount--;
                    ushort[] expr_C3_cp_0 = this.RanksCounts;
                    int expr_C3_cp_1 = 990;
                    expr_C3_cp_0[expr_C3_cp_1] -= 1;
                }
                else if (member.Rank == Enums.GuildMemberRank.Manager)
                    ManagerCount--;
                else if (member.Rank == Enums.GuildMemberRank.LeaderSpouse)
                    LeaderSpouseCount--;
                else if (member.Rank == Enums.GuildMemberRank.HonorarySuperv)
                    HonorarySupervCount--;
                else if (member.Rank == Enums.GuildMemberRank.Aide)
                    AideCount--;
                else if (member.Rank == Enums.GuildMemberRank.HonorarySteward)
                    HonoraryStewardCount--;
                else if (member.Rank == Enums.GuildMemberRank.HonoraryManager)
                    HonoraryManagerCount--;
                else if (member.Rank == Enums.GuildMemberRank.PKSupervisor)
                    PKSupervisorCount--;
                else if (member.Rank == Enums.GuildMemberRank.ArsenalAgent)
                    ArsenalAgentCount--;
                else if (member.Rank == Enums.GuildMemberRank.DeputySteward)
                    DeputyStewardCount--;
                else if (member.Rank == Enums.GuildMemberRank.HDeputyLeader)
                    HDeputyLeaderCount--;
                if (member.IsOnline)
                {
                    GuildCommand command = new GuildCommand(true);
                    command.Type = GuildCommand.Disband;
                    command.dwParam = ID;
                    member.Client.Send(command);
                    member.Client.AsMember = null;
                    member.Client.Guild = null;
                    member.Client.Entity.GuildID = (ushort)0;
                    member.Client.Entity.GuildRank = (ushort)0;
                    member.Client.Screen.FullWipe();
                    member.Client.Screen.Reload(null);
                    member.Client.Entity.GuildBattlePower = 0;
                }
                else
                {
                    member.GuildID = 0;
                    Database.EntityTable.UpdateData(member.ID, "GuildID", 0);
                }
                MemberCount--;
                Members.Remove(uid);
            }
        }
        public void ExpelMember(string membername, bool ownquit)
        {
            Member member = GetMemberByName(membername);
            if (member != null)
            {
                if (ownquit)
                    SendGuildMessage(new Message(member.Name + " has quit our guild.", System.Drawing.Color.Black, Message.Guild));

                else
                    SendGuildMessage(new Message(member.Name + " have been expelled from our guild.", System.Drawing.Color.Black, Message.Guild));
                uint uid = member.ID;
                    if (member.Rank == Game.Enums.GuildMemberRank.DeputyLeader)
                            RanksCounts[990]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.HDeputyLeader)
                            RanksCounts[980]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.HonoraryManager)
                            RanksCounts[880]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.HonorarySuperv)
                            RanksCounts[840]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.HonorarySteward)
                            RanksCounts[680]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.Aide)
                            RanksCounts[602]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.Steward)
                            RanksCounts[690]--;
                         if (member.Rank == Game.Enums.GuildMemberRank.Follower)
                            RanksCounts[490]--;
                if (member.IsOnline)
                {
                    GuildCommand command = new GuildCommand(true);
                    command.Type = GuildCommand.Disband;
                    command.dwParam = ID;
                    member.Client.Send(command);
                    member.Client.AsMember = null;
                    member.Client.Guild = null;
                    member.Client.Entity.GuildID = (ushort)0;
                    member.Client.Entity.GuildRank = (ushort)0;
                    member.Client.Entity.GuildBattlePower = 0;
                    member.Client.Screen.FullWipe();
                    member.Client.Screen.Reload(null);
                }
                else
                {
                    member.GuildID = 0;//معلش جراب بي اكونت تاني كده مهو المشكلة انو بيعلق مع اكونتات محددة 
                }
                MemberCount--;
                Members.Remove(uid);
            }
        }
        public void Disband()
        {
            var members = Members.Values.ToArray();
            foreach (Member member in members)
            {
                uint uid = member.ID;
                if (member.IsOnline)
                {
                    PacketHandler.UninscribeAllItems(member.Client);
                    member.Client.Entity.GuildBattlePower = 0;
                    GuildCommand command = new GuildCommand(true);
                    command.Type = GuildCommand.Disband;
                    command.dwParam = ID;
                    member.Client.Entity.GuildID = 0;
                    member.Client.Entity.GuildRank = 0;
                    member.Client.Send(command);
                    member.Client.Screen.FullWipe();
                    member.Client.Screen.Reload(null);
                    member.Client.AsMember = null;
                    member.Client.Guild = null;
                    Message message = null;
                    message = new Message("guild " + Name + " has been Disbanded!", System.Drawing.Color.White, Message.World);
                    foreach (Client.GameClient client in Kernel.GamePool.Values)
                    {
                        client.Send(message);
                    }
                }
                else
                {
                    foreach (var arsenal in Arsenals)
                        arsenal.RemoveInscribedItemsBy(member.ID);
                    member.GuildID = 0;
                    Database.EntityTable.UpdateData(member.ID, "GuildID", 0);
                }
                MemberCount--;
                Members.Remove(uid);
            }
            var ally_ = Ally.Values.ToArray();
            foreach (Guild ally in ally_)
            {
                RemoveAlly(ally.Name);
                ally.RemoveAlly(Name);
            }
            Database.GuildTable.Disband(this);
            Kernel.GamePool.Remove(ID);
            this.Name = null;
        }

        public void AddAlly(string name)
        {
            foreach (Guild guild in Kernel.Guilds.Values)
            {
                if (guild.Name == name)
                {
                    
                    if (this.Enemy.ContainsKey(guild.ID))
                    {
                        this.RemoveEnemy(guild.Name);
                    }
                    this.Ally.Add(guild.ID, guild);
                    _String message = new _String(true)
                    {
                        UID = guild.ID,
                        Type = 0x15
                    };
                    message.Texts.Add(string.Concat(new object[] { guild.Name, " ", guild.LeaderName, " 0 ", guild.MemberCount }));
                    this.SendGuildMessage(message);
                    this.SendGuildMessage(message);
                    Database.GuildTable.AddAlly(this, guild.ID);
                    return;
                }
            }
        }

        public void RemoveAlly(string name)
        {
            foreach (Guild guild in Ally.Values)
            {
                if (guild.Name == name)
                {
                    GuildCommand cmd = new GuildCommand(true);
                    cmd.Type = GuildCommand.Neutral1;
                    cmd.dwParam = guild.ID;
                    SendGuildMessage(cmd);
                    Database.GuildTable.RemoveAlly(this, guild.ID);
                    Ally.Remove(guild.ID);
                    return;
                }
            }
        }

        public void AddEnemy(string name)
        {
            foreach (Guild guild in Kernel.Guilds.Values)
            {
                if (guild.Name == name)
                {
                   
                    if (this.Ally.ContainsKey(guild.ID))
                    {
                        this.RemoveAlly(guild.Name);
                        guild.RemoveAlly(this.Name);
                    }
                    this.Enemy.Add(guild.ID, guild);
                    _String message = new _String(true)
                    {
                        UID = guild.ID,
                        Type = 0x16
                    };
                    message.Texts.Add(string.Concat(new object[] { guild.Name, " ", guild.LeaderName, " 0 ", guild.MemberCount }));
                    this.SendGuildMessage(message);
                    this.SendGuildMessage(message);
                    Database.GuildTable.AddEnemy(this, guild.ID);
                    break;
                }
            }
        }

        public void RemoveEnemy(string name)
        {
            foreach (Guild guild in Enemy.Values)
            {
                if (guild.Name == name)
                {
                    GuildCommand cmd = new GuildCommand(true);
                    cmd.Type = GuildCommand.Neutral2;
                    cmd.dwParam = guild.ID;
                    SendGuildMessage(cmd);
                    SendGuildMessage(cmd);
                    Database.GuildTable.RemoveEnemy(this, guild.ID);
                    Enemy.Remove(guild.ID);
                    return;
                }
            }
        }

        public void SendName(Client.GameClient client)
        {
            _String stringPacket = new _String(true);
            stringPacket.UID = ID;
            stringPacket.Type = _String.GuildName;
            //stringPacket.Texts.Add(Name + " " + LeaderName + " " + MemberCount);
            stringPacket.Texts.Add(string.Concat(new object[] { Name, " ", LeaderName, " ", GuildLevel, " ", MemberCount }));
            client.Send(stringPacket);
        }

        public void SendGuild(Client.GameClient client)
        {
            if (Members.ContainsKey(client.Entity.UID))
            {
                if (Bulletin == null)
                    Bulletin = "This is a new guild!";
                client.Send(new Network.GamePackets.GuildCommand((uint)Bulletin.Length) { Type = Network.GamePackets.GuildCommand.Bulletin, dwParam = (uint)AnnouncementDate, _str = Bulletin });
                client.AsMember.LoadArsenalDonation(client);
                Writer.Write((uint)client.AsMember.Arsenal_Donation, 8, this.Buffer);
                Writer.Write(0xfffe795f, 88, this.Buffer);
                Writer.Write((uint)client.AsMember.Rank, 28, this.Buffer);
                Writer.Write(GuildLevel, 60, Buffer);
                Writer.Write(LeaderName, 32, Buffer);
                Writer.Write((uint)EnrollmentDate, 67, this.Buffer);
                Writer.Write(LevelRequirement, 48, this.Buffer);
                Writer.Write(RebornRequirement, 52, this.Buffer);
                Writer.Write(ClassRequirement, 56, this.Buffer);
                client.Send(Buffer);
            }
        }

        public ulong ArsTotalDonation()
        {
            ulong total = 0;
            foreach (var arsenal in Arsenals)
                total += arsenal.Donation;
            return total;
        }

        public void SendAllyAndEnemy(Client.GameClient client)
        {
            foreach (Guild guild in Enemy.Values)
            {
                _String stringPacket = new _String(true);
                stringPacket.UID = guild.ID;
                stringPacket.Type = _String.GuildEnemies;
                stringPacket.Texts.Add(guild.Name + " " + guild.LeaderName + " 0 " + guild.MemberCount);
                client.Send(stringPacket);
                client.Send(stringPacket);
            }
            foreach (Guild guild in Ally.Values)
            {
                _String stringPacket = new _String(true);
                stringPacket.UID = guild.ID;
                stringPacket.Type = _String.GuildAllies;
                stringPacket.Texts.Add(guild.Name + " " + guild.LeaderName + " 0 " + guild.MemberCount);
                client.Send(stringPacket);
                client.Send(stringPacket);
            }
        }

        public static bool ValidName(string Name)
        {
            if (Name.Length < 4 && Name.Length > 15) return false;
            else if (Name.IndexOfAny(new char[20] { ' ', '#', '%', '^', '&', '*', '(', ')', ';', ':', '\'', '\"', '/', '\\', ',', '.', '{', '}', '[', ']' }) > 0) return false;
            else return true;
        }

        public Guild.Member[] RankSilversDonations = new Guild.Member[0];
        public Guild.Member[] RankArsenalDonations = new Guild.Member[0];
        public Guild.Member[] RankCPDonations = new Guild.Member[0];
        public Guild.Member[] RankPkDonations = new Guild.Member[0];
        public Guild.Member[] RankLiliesDonations = new Guild.Member[0];
        public Guild.Member[] RankOrchidsDonations = new Guild.Member[0];
        public Guild.Member[] RankRosseDonations = new Guild.Member[0];
        public Guild.Member[] RankTulipsDonations = new Guild.Member[0];
        public Guild.Member[] RankGuideDonations = new Guild.Member[0];
        public Guild.Member[] RankTotalDonations = new Guild.Member[0];
        public ushort[] RanksCounts = new ushort[1001];

        public List<Member> ListMember = new List<Member>();
        public List<RankingList> SilversList = new List<RankingList>();
        public List<RankingList> CPSList = new List<RankingList>();
        public List<RankingList> TotalDonationsList = new List<RankingList>();
        public List<RankingList> ArsenalsList = new List<RankingList>();
        public List<RankingList> PKSList = new List<RankingList>();
        public List<RankingList> GuidesList = new List<RankingList>();
        public List<RankingList> TulipsList = new List<RankingList>();
        public List<RankingList> OrchidsList = new List<RankingList>();
        public List<RankingList> RosesList = new List<RankingList>();
        public List<RankingList> LiliesList = new List<RankingList>();

    }
}
