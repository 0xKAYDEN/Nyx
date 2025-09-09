using System;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Database
{
    public class KingdomMissionTable
    {
        public static void Load(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("KingdomMission").Where("UID", client.Entity.UID))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    client.Entity.KingdomDeed = reader.ReadUInt32("KingdomDeeds");
                    client.Entity.StrikePoints = reader.ReadUInt32("StrikePoints");
                    client.Entity.TodayStrikePoints = reader.ReadUInt32("TodayStrikePoints");
                    return;
                }
                Insert(client);
            }
        }
        public static void Insert(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("KingdomMission")
              .Insert("UID", client.Entity.UID))
                cmd.Execute();
        }
        public static void Save(Client.GameClient client, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("KingdomMission"))
                cmd.Set("KingdomDeeds", client.Entity.KingdomDeed)
                    .Set("StrikePoints", client.Entity.StrikePoints).Set("TodayStrikePoints", client.Entity.TodayStrikePoints)
                .Where("UID", client.Entity.UID)
                .Execute(conn);
        }

    }
}
