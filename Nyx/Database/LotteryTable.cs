using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Database
{
    public class LotteryTable
    {
        public class LotteryItem
        {
            public uint ID;
            public byte Sockets;
            public byte Plus;
            public uint NPCID;
        }
        public static List<LotteryItem> LotteryItems = new List<LotteryItem>();
        public static byte GetItemPlus(uint itemid)
        {
            if (itemid == 730001)
                return 1;
            if (itemid == 730002)
                return 2;
            if (itemid == 730003)
                return 3;
            if (itemid == 730004)
                return 4;
            if (itemid == 730005)
                return 5;
            if (itemid == 730006)
                return 6;
            if (itemid == 730007)
                return 7;
            if (itemid == 730008)
                return 8;
            if (itemid == 730009)
                return 9;
         /*   if (itemid % 10 == 8)
                return GetItemPluus(itemid);*/
            return 0;
        }
        public static List<uint> WillGetSoc = new List<uint>();
        public static byte GetItemPluus(uint g)
        {
            if (Kernel.Rate(50))
                return 6;
            else WillGetSoc.Add(g);
            return 0;
        }
        public static void Load()
        {
            try
            {
                string[] file = System.IO.File.ReadAllLines("database\\LotteryItems.txt");
                foreach (var item2 in file)
                {
                    if (item2 == " " || item2 == "")
                        continue;
                    string[] data = item2.Split(' ');
                    LotteryItem item = new LotteryItem();
                    item.ID = Convert.ToUInt32(data[0]);
                    if (data.Length >= 3)
                    {
                        item.NPCID = Convert.ToUInt32(data[2]);
                    }
                    else
                    {
                        item.NPCID = 925;
                    }
                    item.Plus = GetItemPlus(item.ID)/* GetItemPlus(item.ID)*/;
                        item.Sockets = 0;
                    LotteryItems.Add(item);
                }

            }
            catch (Exception)
            {
            }
        }
    }
}
