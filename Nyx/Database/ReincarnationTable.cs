using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Database
{
    public class ReincarnationTable
    {
        public static void Load()
        {
            using(var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("reincarnation"))
            using (var reader = cmd.CreateReader())
            {
                while (reader.Read())
                {
                    Nyx.Server.Game.Features.Reincarnation.ReincarnateInfo info = new Nyx.Server.Game.Features.Reincarnation.ReincarnateInfo();
                    info.UID = reader.ReadUInt32("Uid");
                    info.Level = reader.ReadByte("Level");
                    info.Experience = reader.ReadUInt32("Experience");
                    if (!Kernel.ReincarnatedCharacters.ContainsKey(info.UID))
                        Kernel.ReincarnatedCharacters.Add(info.UID, info);
                }
            }
        }

        public static void NewReincarnated(Game.Entity entity)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                cmd.Insert("reincarnation").Insert("uid", entity.UID).Insert("level", entity.Level).Insert("experience", 0)
                    .Execute();
        }

        public static void RemoveReincarnated(Game.Entity entity)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.DELETE))
                cmd.Delete("reincarnation", "uid", entity.UID)
                    .Execute();
        }
    }
}
