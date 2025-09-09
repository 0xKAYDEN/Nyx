using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Database
{
    public class StorageItem
    {
        public uint ID;
        public uint Points;
        public uint Stars;
        public byte Type;
        public static Dictionary<uint, StorageItem> Garments = new Dictionary<uint, StorageItem>();
        public static Dictionary<uint, StorageItem> Mounts = new Dictionary<uint, StorageItem>();
        public static void Load()
        {
            string[] baseText = System.IO.File.ReadAllLines("database\\coat_storage_type.txt");
            foreach (var bas_line in baseText)
            {
                var line = bas_line.Split(new string[] { "@@"}, StringSplitOptions.RemoveEmptyEntries);
                StorageItem item = new StorageItem();
                item.ID = uint.Parse(line[0]);
                item.Points = uint.Parse(line[1]);
                item.Stars = uint.Parse(line[7]);
                item.Type = byte.Parse(line[2]);
                if (item.Type == 1)
                    Garments.Add(item.ID, item);
                else
                    Mounts.Add(item.ID, item);
            }
        }
        public static uint AmountStarGarments(Nyx.Server.Client.GameClient client, byte Star)
        {
            uint Count = 0;
            foreach (var Garment in client.Entity.StorageItems.Values)
            {
                StorageItem item;
                if (Garments.TryGetValue(Garment.ID, out item))
                {
                    if (item.Type != 1)
                        continue;
                    if (item.Stars >= Star)
                        Count++;
                }
            }
            return Count;
        }
        public static uint PerfectionPoints(Nyx.Server.Client.GameClient client, bool Garment)
        {
            uint Count = 0;
            foreach (var item2 in client.Entity.StorageItems.Values)
            {
                StorageItem item;
                if (Garment)
                {
                    if (Garments.TryGetValue(item2.ID, out item))
                    {
                        if (item.Type != 1)
                            continue;
                        if (item.Points == 1)
                            Count += 50;
                        else if (item.Points >= 2 && item.Points <= 10)
                            Count += 100;
                        else if (item.Points >= 11 && item.Points <= 50)
                            Count += 200;
                        else if (item.Points >= 51 && item.Points <= 100)
                            Count += 300;
                        else if (item.Points >= 100 && item.Points <= 200)
                            Count += 500;
                    }
                }
                else
                {
                    if (Mounts.TryGetValue(item2.ID, out item))
                    {
                        if (item.Type != 2)
                            continue;
                        if (item.Points == 1)
                            Count += 50;
                        else if (item.Points >= 2 && item.Points <= 10)
                            Count += 100;
                        else if (item.Points >= 11 && item.Points <= 50)
                            Count += 200;
                        else if (item.Points >= 51 && item.Points <= 100)
                            Count += 300;
                        else if (item.Points >= 100 && item.Points <= 200)
                            Count += 500;
                    }
                }
            }
            return Count;
        }
        public static uint AmountStarMounts(Nyx.Server.Client.GameClient client, byte Star)
        {
            uint Count = 0;
            foreach (var Mount in client.Entity.StorageItems.Values)
            {
                StorageItem item;
                if (Mounts.TryGetValue(Mount.ID, out item))
                {
                    if (item.Type != 2)
                        continue;
                    if (item.Stars >= Star)
                        Count++;
                }
            }
            return Count;
        }
    }
}
