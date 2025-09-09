using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Game.ConquerStructures;

namespace Nyx.Server.Database
{
    public class NobilityTable
    {
        public static void Load()
        {
            using(var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("nobility"))
            using (var reader = cmd.CreateReader())
            {
                while (reader.Read())
                {
                    NobilityInformation nobilityinfo = new NobilityInformation();
                    nobilityinfo.EntityUID = reader.ReadUInt32("EntityUID");
                    nobilityinfo.Name = reader.ReadString("EntityName");
                    nobilityinfo.Donation = reader.ReadUInt64("Donation");
                    nobilityinfo.Gender = reader.ReadByte("Gender");
                    nobilityinfo.Mesh = reader.ReadUInt32("Mesh");
                    Game.ConquerStructures.Nobility.Board.Add(nobilityinfo.EntityUID, nobilityinfo);
                }
                Game.ConquerStructures.Nobility.Sort(0);
            }
            
        }

        public static void InsertNobilityInformation(NobilityInformation information)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                cmd.Insert("nobility").Insert("entityname", information.Name)
                    .Insert("entityuid", information.EntityUID).Insert("donation", information.Donation)
                    .Insert("gender", information.Gender).Insert("mesh", information.Mesh)
                    .Execute();
        }
        public static void UpdateNobilityInformation(Game.ConquerStructures.NobilityInformation information)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("nobility"))
                cmd.Set("donation", information.Donation).Where("entityuid", information.EntityUID)
                    .Execute();
        }
    }
}