using Nyx.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets.Union
{
    public class UnionTable
    {
        public static List<UnionClass.Member> LoadMembers(uint UnionID)
        {
            List<UnionClass.Member> Members = new List<UnionClass.Member>();
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities").Where("UnionID", UnionID))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    var member = new UnionClass.Member(UnionID);
                    member.ID = reader.ReadUInt32("uid");
                    member.Name = reader.ReadString("name");
                    member.Level = reader.ReadByte("Level");
                    if (Nyx.Server.Game.ConquerStructures.Nobility.Board.ContainsKey(member.ID))
                    {
                        member.NobilityRank = Nyx.Server.Game.ConquerStructures.Nobility.Board[member.ID].Rank;
                    }
                    member.Class = reader.ReadByte("Class");
                    member.UnionExploits = reader.ReadUInt32("UnionExploits");
                    member.Offical = reader.ReadUInt16("Offical");
                    member.Harem = reader.ReadUInt16("Harem");
                    member.Guards = reader.ReadUInt16("Guards");
                    member.GuildID = reader.ReadUInt32("guildid");
                    member.Mesh = uint.Parse(reader.ReadUInt16("Face").ToString() + reader.ReadUInt16("Body").ToString());
                    Members.Add(member);
                }
            }
            return Members;
        }
        public static void Load()
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("Unions"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    var Union = new UnionClass();
                    Union.ID = reader.ReadUInt32("ID");
                    Union.GoldBricks = reader.ReadUInt32("GoldBricks");
                    Union.UnionFund = reader.ReadUInt64("UnionFund");
                    Union.Name = reader.ReadString("Name");
                    Union.Title = reader.ReadString("Title");
                    Union.LeaderName = reader.ReadString("LeaderName");
                    Union.LeaderUID = reader.ReadUInt32("LeaderUID");
                    Union.Bulletin = reader.ReadString("Bulletin");
                    Union.Declaration = reader.ReadString("Declaration");
                    foreach (var guild in Nyx.Server.Kernel.Guilds.Values)
                    {
                        if (guild.UnionID == Union.ID)
                        {
                            Union.Guilds.Add(guild.ID);
                        }
                    }
                    string[] Officals = reader.ReadString("CoreOfficals").Split('#');
                    Union.Coreofficals[0] = uint.Parse(Officals[0]);
                    Union.Coreofficals[1] = uint.Parse(Officals[1]);
                    Union.Coreofficals[2] = uint.Parse(Officals[2]);
                    Union.Coreofficals[3] = uint.Parse(Officals[3]);
                    Union.Coreofficals[4] = uint.Parse(Officals[4]);
                    Union.Coreofficals[5] = uint.Parse(Officals[5]);
                    string[] Harem = reader.ReadString("Harem").Split('#');
                    Union.Harem[0] = uint.Parse(Harem[0]);
                    Union.Harem[1] = uint.Parse(Harem[1]);
                    Union.Harem[2] = uint.Parse(Harem[2]);
                    Union.Harem[3] = uint.Parse(Harem[3]);
                    Union.Harem[4] = uint.Parse(Harem[4]);
                    Union.Harem[5] = uint.Parse(Harem[5]);
                    Union.Harem[6] = uint.Parse(Harem[6]);
                    Union.Harem[7] = uint.Parse(Harem[7]);
                    Union.Harem[8] = uint.Parse(Harem[8]);
                    Union.Harem[9] = uint.Parse(Harem[9]);
                    string[] Guards = reader.ReadString("Guards").Split('#');
                    Union.Guards[0] = uint.Parse(Guards[0]);
                    Union.Guards[1] = uint.Parse(Guards[1]);
                    Union.Guards[2] = uint.Parse(Guards[2]);
                    Union.Guards[3] = uint.Parse(Guards[3]);
                    Union.Guards[4] = uint.Parse(Guards[4]);
                    Union.Guards[5] = uint.Parse(Guards[5]);
                    Union.Guards[6] = uint.Parse(Guards[6]);
                    Union.Guards[7] = uint.Parse(Guards[7]);
                    Union.Guards[8] = uint.Parse(Guards[8]);
                    Union.Guards[9] = uint.Parse(Guards[9]);
                    Union.Guards[10] = uint.Parse(Guards[10]);
                    Union.Guards[11] = uint.Parse(Guards[11]);
                    Union.Guards[12] = uint.Parse(Guards[12]);
                    Union.Guards[13] = uint.Parse(Guards[13]);
                    Union.Guards[14] = uint.Parse(Guards[14]);
                    Union.Guards[15] = uint.Parse(Guards[15]);
                    Union.Guards[16] = uint.Parse(Guards[16]);
                    Union.Guards[17] = uint.Parse(Guards[17]);
                    Union.Guards[18] = uint.Parse(Guards[18]);
                    Union.Guards[19] = uint.Parse(Guards[19]);
                    Nyx.Server.Kernel.Unions.Add(Union.ID, Union);
                }
            }
        }
        public static void CreateUnion(UnionClass union)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("Unions").
            Insert("ID", union.ID).
            Insert("GoldBricks", union.GoldBricks).
            Insert("UnionFund", union.UnionFund).
            Insert("Name", union.Name).
            Insert("Title", union.Title).
            Insert("LeaderName", union.LeaderName).
            Insert("LeaderUID", union.LeaderUID).
            Insert("Bulletin", "").
            Insert("Declaration", ""))
                cmd.Execute();
        }
        public static void UpdateHarem(UnionClass union)
        {
            var builder = new StringBuilder();
            builder.Append(union.Harem[0] + "#" + union.Harem[1] + "#" + union.Harem[2] + "#" + union.Harem[3] + "#" + union.Harem[4] + "#" + union.Harem[5] + "#" + union.Harem[6] + "#" + union.Harem[7] + "#" + union.Harem[8] + "#" + union.Harem[9]);
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions")
            .Set("Harem", builder.ToString()).Where("ID", union.ID))
                cmd.Execute();
        }
        public static void UpdateCoreOfficals(UnionClass union)
        {
            var builder = new StringBuilder();
            builder.Append(union.Coreofficals[0] + "#" + union.Coreofficals[1] + "#" + union.Coreofficals[2] + "#" + union.Coreofficals[3] + "#" + union.Coreofficals[4] + "#" + union.Coreofficals[5] + "#" + union.Coreofficals[6] + "#" + union.Coreofficals[7]);
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions")
            .Set("CoreOfficals", builder.ToString()).Where("ID", union.ID))
                cmd.Execute();
        }
        public static void UpdateGuards(UnionClass union)
        {
            var builder = new StringBuilder();
            builder.Append(union.Guards[0] + "#" + union.Guards[1] + "#" + union.Guards[2] + "#" + union.Guards[3] + "#" + union.Guards[4] + "#" + union.Guards[5] + "#" + union.Guards[6] + "#" + union.Guards[7] + "#" + union.Guards[8] + "#" + union.Guards[9] + "#" + union.Guards[10] + "#" + union.Guards[11] + "#" + union.Guards[12] + "#" + union.Guards[13] + "#" + union.Guards[14] + "#" + union.Guards[15] + "#" + union.Guards[16] + "#" + union.Guards[17] + "#" + union.Guards[18] + "#" + union.Guards[19]);
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions")
            .Set("Guards", builder.ToString()).Where("ID", union.ID))
                cmd.Execute();
        }
        public static void UpdateLeader(UnionClass union)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("Unions")
             .Set("LeaderName", union.LeaderName).Set("LeaderUID", union.LeaderUID).Where("ID", union.ID))
                cmd.Execute();
        }
        public static void UpdateData(uint UID, string column, object value)
        {
            if (value is Boolean)
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                    cmd.Update("entities").Set(column, (Boolean)value).Where("UID", UID)
                        .Execute();
            }
            else
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                    cmd.Update("entities").Set(column, value.ToString()).Where("UID", UID)
                        .Execute();
            }
        }
        public static void DeleteUnion(uint UnionID)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.DELETE))
                cmd.Delete("unions", "UnionID", UnionID).Execute();
        }
    }
}
