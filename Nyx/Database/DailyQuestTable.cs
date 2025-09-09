using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Database
{
    public class DailyQuestTable
    {
        public static void Load(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("dailyquests").Where("UID", client.Entity.UID))
            using (var reader = cmd.CreateReader())
            {
                while (reader.Read())
                {
                    client.Entity.ETHAPItemID = reader.ReadUInt32("ETHAPItemID");
                    client.Entity.DidTOM = reader.ReadBoolean("DidTOM");
                    client.Entity.ETHAPItemAmount = reader.ReadByte("ETHAPItemAmount");
                    client.Entity.SpiritBeadsCount = reader.ReadUInt16("SpiritBeadsCount");
                    client.Entity.TOM = reader.ReadByte("TOM");
                    client.Entity.TOMQ = reader.ReadByte("TOMQ");
                    client.Entity.TOMRC = reader.ReadByte("TOMRC");
                    client.Entity.SpiritBeadsType = reader.ReadByte("SpiritBeadsCount");
                    client.Entity.RareMaterialsAmount = reader.ReadByte("RareMaterialsAmount");
                    client.Entity.RareMaterialsItemID = reader.ReadByte("RareMaterialsItemID");
                    client.Entity.DidFirstHeavenTreasury = reader.ReadBoolean("DidFirstHeavenTreasury");
                    return;
                }
                Insert(client);
            }
        }
        private static bool Exists(uint UID)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("dailyquests").Where("UID", UID))
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
        public static void Insert(Client.GameClient client)
        {
            if (!Exists(client.Entity.UID))
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("dailyquests"))
                    cmd.Insert("UID", client.Entity.UID)
                        .Execute();
            }
        }
        public static void Save(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("dailyquests"))
                cmd
                    .Set("ETHAPItemID", client.Entity.ETHAPItemID)
                    .Set("ETHAPItemAmount", client.Entity.ETHAPItemAmount)
                    .Set("SpiritBeadsCount", client.Entity.SpiritBeadsCount)
                    .Set("SpiritBeadsType", client.Entity.SpiritBeadsType)
                    .Set("DidTOM", client.Entity.DidTOM)
                    .Set("RareMaterialsAmount", client.Entity.RareMaterialsAmount)
                    .Set("RareMaterialsItemID", client.Entity.RareMaterialsItemID)
                    .Set("DidFirstHeavenTreasury", client.Entity.DidFirstHeavenTreasury)
                    .Set("TOM", client.Entity.TOM)
                    .Set("TOMQ", client.Entity.TOMQ)
                    .Set("TOMRC", client.Entity.TOMRC)
                    .Where("UID", client.Entity.UID)
            .Execute();
        }
        public static void Reset(Client.GameClient client)
        {
            client.Entity.ETHAPItemID = 0;
            client.Entity.ETHAPItemAmount = 0;
            client.Entity.SpiritBeadsCount = 0;
            client.Entity.SpiritBeadsType = 0;
            client.Entity.RareMaterialsAmount = 0;
            client.Entity.DidTOM = false;
            client.Entity.TOMRC = 3;
            client.Entity.TOM = 0;
            client.Entity.TOMQ = 255;
            client.Entity.RareMaterialsItemID = 0;
            client.Entity.DidFirstHeavenTreasury = false;
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("dailyquests"))
                cmd
                    .Set("ETHAPItemID", client.Entity.ETHAPItemID)
                    .Set("ETHAPItemAmount", client.Entity.ETHAPItemAmount)
                    .Set("SpiritBeadsCount", client.Entity.SpiritBeadsCount)
                    .Set("SpiritBeadsType", client.Entity.SpiritBeadsType)
                    .Set("RareMaterialsAmount", client.Entity.RareMaterialsAmount)
                    .Set("RareMaterialsItemID", client.Entity.RareMaterialsItemID)
                    .Set("DidFirstHeavenTreasury", client.Entity.DidFirstHeavenTreasury)
                    .Set("TOM", client.Entity.TOM)
                    .Set("TOMQ", client.Entity.TOMQ)
                    .Set("TOMRC", client.Entity.TOMRC)
                    .Where("UID", client.Entity.UID)
            .Execute();
        }
    }
}