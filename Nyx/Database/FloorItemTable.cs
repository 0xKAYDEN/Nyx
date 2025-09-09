using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server;

namespace Nyx.Server.Database
{
    public class FloorItemTable
    {
        public static List<Network.GamePackets.FloorItem> FloorItems = new List<Network.GamePackets.FloorItem>();
        public static void Load()
        {
            using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.SELECT).Select("flooritem"))
            using (var reader = new Database.MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    Network.GamePackets.FloorItem fl = new Network.GamePackets.FloorItem(true);
                    fl.ItemID = reader.ReadUInt32("ID");
                    fl.Type = reader.ReadUInt16("Type");
                    fl.MapID = reader.ReadUInt16("MapID");
                    fl.X = reader.ReadUInt16("X");
                    fl.Y = reader.ReadUInt16("Y");
                    fl.mColor = reader.ReadByte("Color");
                    fl.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Kernel.Maps[fl.MapID].Npcs.ContainsKey(fl.UID))
                        fl.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    FloorItems.Add(fl);
                }
            }
        }
    }
}