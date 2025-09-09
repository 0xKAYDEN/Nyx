using System.Collections.Generic;
using System;


namespace Nyx.Server.Database
{
    public class MapsTable
    {
        public struct MapInformation
        {
            public ushort ID;
            public ushort BaseID;
            public uint Status;
            public uint Weather;
            public uint RaceRecord;
            public uint Owner;
            public uint HouseLevel;
            public uint Box;
            public uint BoxX;
            public uint BoxY;
        }
        public static CareDictionary<long, MapInformation> MapInformations = new CareDictionary<long, MapInformation>(10000);
        private static IniFile RaceRecords;
        public static void Load()
        {
            RaceRecords = new IniFile("database\\racerecords.ini", "record");
            using(var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("maps"))
            using (var reader = cmd.CreateReader())
            {
                while (reader.Read())
                {
                    MapInformation info = new MapInformation();
                    info.ID = reader.ReadUInt16("Id");
                    info.BaseID = reader.ReadUInt16("Mapdoc");
                    info.Status = reader.ReadUInt32("Type");
                    info.Weather = reader.ReadUInt32("Weather");
                    info.RaceRecord = RaceRecords[info.ID, 0].Cast<uint>();
                    MapInformations.Add(info.ID, info);
                }
            }
            
        }

        internal static void SaveRecord(MapInformation key)
        {
            RaceRecords[key.ID] = key.RaceRecord;
        }
    }
}
