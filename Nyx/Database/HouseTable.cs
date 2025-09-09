using System;
using System.IO;
using System.Linq;
using System.Text;
using Nyx.Server.Game;
using Nyx.Server.Client;
using Nyx.Server.Database;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Database
{
    public class HouseTable
    {
        public static void Load(Client.GameClient client)
        {
                using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.SELECT).Select("house").Where("id", (long)client.Account.EntityID))
                using (var reader = new Database.MySqlReader(cmd))
                {
                    if (reader.Read())
                    {
                        MapsTable.MapInformation information = new MapsTable.MapInformation
                        {
                            ID = ((ushort)client.Entity.UID),
                            BaseID = reader.ReadUInt16("Map"),
                            HouseLevel = reader.ReadUInt32("HouseLevel"),
                            Status = 7,
                            Weather = 0,
                            Owner = client.Entity.UID,
                        };
                        if (information.HouseLevel == 0)
                            information.HouseLevel = 1;
                        MapsTable.MapInformations.Add(information.ID, information);
                        Kernel.Maps.Remove(information.ID);
                        if (!Kernel.Maps.ContainsKey(information.ID))
                            new Map(information.ID, information.BaseID, DMaps.MapPaths[information.BaseID]);
                    }
                    using (var cmd1 = new Database.MySqlCommand(Database.MySqlCommandType.SELECT).Select("house").Where("id", client.Entity.UID))
                    {
                        if (reader.Read())
                        {
                            ushort id = (ushort)client.Entity.UID;
                            if (Kernel.Maps.ContainsKey(id))
                                Kernel.Maps.Remove(id);
                            if (MapsTable.MapInformations.ContainsKey(id))
                                MapsTable.MapInformations.Remove(id);

                        }
                    }
                }
           
        }
        public static void Create(Client.GameClient client)
        {
            using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.INSERT))
                cmd.Insert("house")
                    .Insert("id", client.Entity.UID)
                    .Insert("Map", "1098")
                    .Insert("owner", client.Entity.UID)
                    .Insert("HouseLevel", "1")
                    .Insert("Name", client.Entity.Name)
                    .Execute();
            MapsTable.MapInformation information = new MapsTable.MapInformation
            {
                ID = (ushort)client.Entity.UID,
                BaseID = 1098,
                Status = 7,
                Weather = 0,
                Owner = client.Entity.UID,
                HouseLevel = 1
            };
            MapsTable.MapInformations.Add(information.ID, information);
            if (!Kernel.Maps.ContainsKey(information.ID))
            {
                new Map(information.ID, information.BaseID, DMaps.MapPaths[information.BaseID]);
            }
        }
        public static void SaveFurniture(byte[] packet, Client.GameClient client)
        {
                NpcSpawn npcs = new NpcSpawn(false);
                npcs.Deserialize(packet);
                npcs.MapID = client.Entity.MapID;
                using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.INSERT))
                    cmd.Insert("Furniture")
                        .Insert("NameOwner", client.Entity.Name)
                        .Insert("id", npcs.UID) 
                        .Insert("mapid", npcs.MapID)
                        .Insert("lookface", npcs.Mesh)
                        .Insert("type", (long)npcs.Type)
                        .Insert("cellx", npcs.X)
                        .Insert("celly", npcs.Y)
                        .Execute();
                if (!Kernel.Furnitures.ContainsKey(npcs.UID))
                {
                    Kernel.Furnitures.Add(npcs.UID, client.Entity.Name);
                }
        }
        public static void Upgrade(Client.GameClient client, ushort _base, byte level)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("Furniture").Where("NameOwner", client.Entity.Name))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    if (Kernel.Furnitures[reader.ReadUInt32("id")] != null)
                        Kernel.Furnitures.Remove(reader.ReadUInt32("id"));
                }
            }
            Nyx.Server.Database.MySqlCommand command = new Nyx.Server.Database.MySqlCommand(MySqlCommandType.DELETE);
            command.Delete("Furniture", "NameOwner", client.Entity.Name).Execute();
            MapsTable.MapInformations.Remove((ushort)client.Entity.UID);
            Kernel.Maps.Remove(((ushort)client.Entity.UID));
            MapsTable.MapInformation information = new MapsTable.MapInformation
            {
                ID = (ushort)client.Entity.UID,
                BaseID = _base,
                Status = 7,
                Weather = 0,
                Owner = client.Entity.UID,
                HouseLevel = level
            };
            MapsTable.MapInformations.Add(information.ID, information);

            using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE))
                cmd.Update("house").Set("HouseLevel", level)
                .Set("Map", _base).Where("id", client.Entity.UID).Execute();

            if (!Kernel.Maps.ContainsKey(information.ID))
            {
                new Map(information.ID, information.BaseID, DMaps.MapPaths[information.BaseID]);
            }
        }
    }
}