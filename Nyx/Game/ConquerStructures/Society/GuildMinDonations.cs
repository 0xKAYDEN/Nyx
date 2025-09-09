using Nyx.Server.Game;
using Nyx.Server.Game.ConquerStructures.Society;
using System;
namespace Nyx.Server.Network.GamePackets
{
    public class GuildMinDonations : Writer
    {
        private byte[] packet;
        private ushort Position = 8;
        public byte[] Encode()
        {
            return this.packet;
        }
        public GuildMinDonations(ushort counts = 0)
        {
            this.packet = new byte[(int)(16 + counts * 8)];
            Writer.Write((ushort)(this.packet.Length - 8), 0, this.packet);
            Writer.Write(1061, 2, this.packet);
            Writer.Write(counts, 6, this.packet);
        }
        public void Aprend(Enums.GuildMemberRank Rank, uint amount)
        {
            Writer.Write((uint)Rank, (int)this.Position, this.packet);
            this.Position += 4;
            Writer.Write(amount, (int)this.Position, this.packet);
            this.Position += 4;
        }
        public void AprendGuild(Guild guild)
        {
            if (guild.RankArsenalDonations.Length >= 5)
            {
                Guild.Member member = guild.RankArsenalDonations[4];
                this.Aprend(Enums.GuildMemberRank.Manager, (uint)member.Arsenal_Donation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.Manager, 0u);
            }
            if (guild.RankArsenalDonations.Length >= 7)
            {
                Guild.Member member2 = guild.RankArsenalDonations[6];
                this.Aprend(Enums.GuildMemberRank.HonoraryManager, (uint)member2.Arsenal_Donation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.HonoraryManager, 0u);
            }
            if (guild.RankArsenalDonations.Length >= 8)
            {
                Guild.Member member3 = guild.RankArsenalDonations[7];
                this.Aprend(Enums.GuildMemberRank.Supervisor, (uint)member3.Arsenal_Donation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.Supervisor, 0u);
            }
            if (guild.RankArsenalDonations.Length >= 13)
            {
                Guild.Member member4 = guild.RankArsenalDonations[12];
                this.Aprend(Enums.GuildMemberRank.Steward, (uint)member4.Arsenal_Donation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.Steward, 0u);
            }
            if (guild.RankArsenalDonations.Length >= 15)
            {
                Guild.Member member5 = guild.RankArsenalDonations[14];
                this.Aprend(Enums.GuildMemberRank.ArsFollower, (uint)member5.Arsenal_Donation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.ArsFollower, 0u);
            }
            if (guild.RankCPDonations.Length >= 3)
            {
                Guild.Member member6 = guild.RankCPDonations[2];
                this.Aprend(Enums.GuildMemberRank.CPSupervisor, (uint)member6.ConquerPointDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.CPSupervisor, 0u);
            }
            if (guild.RankCPDonations.Length >= 5)
            {
                Guild.Member member7 = guild.RankCPDonations[4];
                this.Aprend(Enums.GuildMemberRank.CPAgent, (uint)member7.ConquerPointDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.CPAgent, 0u);
            }
            if (guild.RankCPDonations.Length >= 7)
            {
                Guild.Member member8 = guild.RankCPDonations[6];
                this.Aprend(Enums.GuildMemberRank.CPFollower, (uint)member8.ConquerPointDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.CPFollower, 0u);
            }
            if (guild.RankPkDonations.Length >= 3)
            {
                Guild.Member member9 = guild.RankPkDonations[2];
                this.Aprend(Enums.GuildMemberRank.PKSupervisor, (uint)member9.PKDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.PKSupervisor, 0u);
            }
            if (guild.RankPkDonations.Length >= 5)
            {
                Guild.Member member10 = guild.RankPkDonations[4];
                this.Aprend(Enums.GuildMemberRank.PKAgent, (uint)member10.PKDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.PKAgent, 0u);
            }
            if (guild.RankPkDonations.Length >= 7)
            {
                Guild.Member member11 = guild.RankPkDonations[6];
                this.Aprend(Enums.GuildMemberRank.PKFollower, (uint)member11.PKDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.PKFollower, 0u);
            }
            if (guild.RankRosseDonations.Length >= 3)
            {
                Guild.Member member12 = guild.RankRosseDonations[2];
                this.Aprend(Enums.GuildMemberRank.RoseSupervisor, (uint)member12.RoseDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.RoseSupervisor, 0u);
            }
            if (guild.RankRosseDonations.Length >= 5)
            {
                Guild.Member member13 = guild.RankRosseDonations[4];
                this.Aprend(Enums.GuildMemberRank.RoseAgent, (uint)member13.RoseDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.RoseAgent, 0u);
            }
            if (guild.RankRosseDonations.Length >= 7)
            {
                Guild.Member member14 = guild.RankRosseDonations[6];
                this.Aprend(Enums.GuildMemberRank.RoseFollower, (uint)member14.RoseDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.RoseFollower, 0u);
            }
            if (guild.RankLiliesDonations.Length >= 3)
            {
                Guild.Member member15 = guild.RankLiliesDonations[2];
                this.Aprend(Enums.GuildMemberRank.LilySupervisor, (uint)member15.LiliesDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.LilySupervisor, 0u);
            }
            if (guild.RankLiliesDonations.Length >= 5)
            {
                Guild.Member member16 = guild.RankLiliesDonations[4];
                this.Aprend(Enums.GuildMemberRank.LilyAgent, (uint)member16.LiliesDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.LilyAgent, 0u);
            }
            if (guild.RankLiliesDonations.Length >= 7)
            {
                Guild.Member member17 = guild.RankLiliesDonations[6];
                this.Aprend(Enums.GuildMemberRank.LilyFollower, (uint)member17.LiliesDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.LilyFollower, 0u);
            }
            if (guild.RankTulipsDonations.Length >= 3)
            {
                Guild.Member member18 = guild.RankTulipsDonations[2];
                this.Aprend(Enums.GuildMemberRank.TSupervisor, (uint)member18.TulipDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.TSupervisor, 0u);
            }
            if (guild.RankTulipsDonations.Length >= 5)
            {
                Guild.Member member19 = guild.RankTulipsDonations[4];
                this.Aprend(Enums.GuildMemberRank.TulipAgent, (uint)member19.TulipDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.TulipAgent, 0u);
            }
            if (guild.RankTulipsDonations.Length >= 7)
            {
                Guild.Member member20 = guild.RankTulipsDonations[6];
                this.Aprend(Enums.GuildMemberRank.TulipFollower, (uint)member20.TulipDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.TulipFollower, 0u);
            }
            if (guild.RankOrchidsDonations.Length >= 3)
            {
                Guild.Member member21 = guild.RankOrchidsDonations[2];
                this.Aprend(Enums.GuildMemberRank.OSupervisor, (uint)member21.OrchidesDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.OSupervisor, 0u);
            }
            if (guild.RankOrchidsDonations.Length >= 5)
            {
                Guild.Member member22 = guild.RankOrchidsDonations[4];
                this.Aprend(Enums.GuildMemberRank.OrchidAgent, (uint)member22.OrchidesDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.OrchidAgent, 0u);
            }
            if (guild.RankOrchidsDonations.Length >= 7)
            {
                Guild.Member member23 = guild.RankOrchidsDonations[6];
                this.Aprend(Enums.GuildMemberRank.OrchidFollower, (uint)member23.OrchidesDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.OrchidFollower, 0u);
            }
            if (guild.RankTotalDonations.Length >= 2)
            {
                Guild.Member member24 = guild.RankTotalDonations[1];
                this.Aprend(Enums.GuildMemberRank.HDeputyLeader, (uint)member24.TotalDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.HDeputyLeader, 0u);
            }
            if (guild.RankTotalDonations.Length >= 4)
            {
                Guild.Member member25 = guild.RankTotalDonations[3];
                this.Aprend(Enums.GuildMemberRank.HonorarySteward, (uint)member25.TotalDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.HonorarySteward, 0u);
            }
            if (guild.RankSilversDonations.Length >= 4)
            {
                Guild.Member member26 = guild.RankSilversDonations[3];
                this.Aprend(Enums.GuildMemberRank.SSupervisor, (uint)member26.SilverDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.SSupervisor, 0u);
            }
            if (guild.RankSilversDonations.Length >= 6)
            {
                Guild.Member member27 = guild.RankSilversDonations[5];
                this.Aprend(Enums.GuildMemberRank.SilverAgent, (uint)member27.SilverDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.SilverAgent, 0u);
            }
            if (guild.RankSilversDonations.Length >= 8)
            {
                Guild.Member member28 = guild.RankSilversDonations[7];
                this.Aprend(Enums.GuildMemberRank.SilverFollower, (uint)member28.SilverDonation);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.SilverFollower, 0u);
            }
            if (guild.RankGuideDonations.Length >= 3)
            {
                Guild.Member member29 = guild.RankGuideDonations[2];
                this.Aprend(Enums.GuildMemberRank.GSupervisor, (uint)member29.GuideDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.GSupervisor, 0u);
            }
            if (guild.RankGuideDonations.Length >= 5)
            {
                Guild.Member member30 = guild.RankGuideDonations[4];
                this.Aprend(Enums.GuildMemberRank.GuideAgent, (uint)member30.GuideDoantion);
            }
            else
            {
                this.Aprend(Enums.GuildMemberRank.GuideAgent, 0u);
            }
            if (guild.RankGuideDonations.Length >= 7)
            {
                Guild.Member member31 = guild.RankGuideDonations[6];
                this.Aprend(Enums.GuildMemberRank.GuideFollower, (uint)member31.GuideDoantion);
                return;
            }
            this.Aprend(Enums.GuildMemberRank.GuideFollower, 0u);
        }
    }
}
