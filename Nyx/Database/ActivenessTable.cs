using System;
using Nyx.Server.Client;

namespace Nyx.Server.Database
{
    public class ActivenessTable
    {
        public static void Load(GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("Activeness").Where("UID", client.Entity.UID))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    client.Entity.ClaimedActivenessGift1 = reader.ReadByte("ClaimedGift1");
                    client.Entity.ClaimedActivenessGift2 = reader.ReadByte("ClaimedGift2");
                    client.Entity.ClaimedActivenessGift3 = reader.ReadByte("ClaimedGift3");
                    client.Entity.ChiStudyTimes = reader.ReadByte("ChiStudy");
                    client.Entity.JiangStudyTimes = reader.ReadByte("JiangStudy");
                    client.Entity.DidTIB = reader.ReadBoolean("DidTIB");
                    client.Entity.EnlightmentTimes = reader.ReadByte("EnlightmentTimes");
                    client.Entity.HoursTimes = reader.ReadByte("HoursTimes");
                    return;
                }
                ActivenessTable.Insert(client);
            }
        }
        public static void Save(GameClient client)
        {
            if (client.Entity == null) return;
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("Activeness"))
                cmd.Set("ClaimedGift1", client.Entity.ClaimedActivenessGift1)
                    .Set("ClaimedGift2", client.Entity.ClaimedActivenessGift2)
                    .Set("ClaimedGift3", client.Entity.ClaimedActivenessGift3)
                    .Set("JiangStudy", client.Entity.JiangStudyTimes)
                    .Set("ChiStudy", client.Entity.ChiStudyTimes)
                    .Set("EnlightmentTimes", client.Entity.EnlightmentTimes)
                    .Set("DidTIB", client.Entity.DidTIB)
                    .Set("HoursTimes", client.Entity.HoursTimes)
                    .Where("UID", client.Entity.UID)
                    .Execute();
        }
        public static void Reset(GameClient client)
        {
            if (client.Entity == null) return;
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("Activeness"))
                cmd.Set("ClaimedGift1", 0)
                    .Set("ClaimedGift2", 0)
                    .Set("ClaimedGift3", 0)
                    .Set("ChiStudy", 0)
                    .Set("JiangStudy", 0)
                    .Set("DidTIB", false)
                    .Set("EnlightmentTimes", 0)
                    .Set("HoursTimes", 0)
                   .Where("UID", client.Entity.UID)
                   .Execute();
            client.Entity.ChiStudyTimes = 0;
            client.Entity.JiangStudyTimes = 0;
            client.Entity.EnlightmentTimes = 0;
            client.Entity.DidTIB = false;
            client.Entity.HoursTimes = 0;
            client.Entity.ClaimedActivenessGift1 = 0;
            client.Entity.ClaimedActivenessGift2 = 0;
            client.Entity.ClaimedActivenessGift3 = 0;
        }
        private static void Insert(GameClient client)
        {
            if (!ActivenessTable.Exists(client.Entity.UID))
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("Activeness"))
                    cmd.Insert("UID", client.Entity.UID).Execute();
            }
        }
        private static bool Exists(uint UID)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("Activeness").Where("UID", UID))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    reader.Close();
                    reader.Dispose();
                    return true;
                }
                return false;
            }
        }
    }
}