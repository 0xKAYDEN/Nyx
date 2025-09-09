using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nyx.Server;
using Nyx.Server.Game;
using Nyx.Server.GamePackets;

namespace Nyx.Server.Database
{
    public class PerfectionTable
    {
        #region ItemRefineCost
        public static Dictionary<uint, ItemRefineCost> CostList = new Dictionary<uint, ItemRefineCost>();
        public class ItemRefineCost
        {
            public uint ItemID;
            public ushort Progress;
        }
        public static void LoadItemRefineCost()
        {
            if (File.Exists(Constants.DataHolderPath + "item_refine_cost.txt"))
            {
                string[] Lines = File.ReadAllLines(Constants.DataHolderPath + "item_refine_cost.txt");
                foreach (var line in Lines)
                {
                    var spilitline = line.Split(new string[] { "@@", " " }, StringSplitOptions.RemoveEmptyEntries);
                    ItemRefineCost irc = new ItemRefineCost();
                    irc.ItemID = Convert.ToUInt32(spilitline[0]);
                    irc.Progress = Convert.ToUInt16(spilitline[1]);
                    CostList.Add(irc.ItemID, irc);
                }
            }
        }
        #endregion
        #region ItemRefineUpgrade
        public static List<ItemRefineUpgrade> UpgradeList = new List<ItemRefineUpgrade>();
        public class ItemRefineUpgrade
        {
            public byte Position;
            public ushort Stars;
            public ushort Progress;
        }
        public static void LoadItemRefineUpgrade()
        {
            if (File.Exists(Constants.DataHolderPath + "item_refine_upgrade.txt"))
            {
                string[] Lines = File.ReadAllLines(Constants.DataHolderPath + "item_refine_upgrade.txt");
                foreach (var line in Lines)
                {
                    var spilitline = line.Split(new string[] { "@@", " " }, StringSplitOptions.RemoveEmptyEntries);
                    ItemRefineUpgrade iru = new ItemRefineUpgrade();
                    iru.Position = Convert.ToByte(spilitline[0]);
                    iru.Stars = Convert.ToUInt16(spilitline[1]);
                    iru.Progress = Convert.ToUInt16(spilitline[2]);
                    UpgradeList.Add(iru);
                }
            }
        }
        #endregion
        #region ItemRefineAttribute
        public static Dictionary<ushort, ItemRefineAttribute> AttributeList = new Dictionary<ushort, ItemRefineAttribute>();
        public class ItemRefineAttribute
        {
            public ushort ID;
            public Game.Enums.PerfectionEffect Type;
            public byte Level;
            public uint DoWhat;
            public uint DoWhat2;
            public ushort Chance;
        }
        public static void LoadItemRefineAttribute()
        {
            if (File.Exists(Constants.DataHolderPath + "item_refine_attr.txt"))
            {
                string[] Lines = File.ReadAllLines(Constants.DataHolderPath + "item_refine_attr.txt");
                foreach (var line in Lines)
                {
                    var spilitline = line.Split(new string[] { "@@", " " }, StringSplitOptions.RemoveEmptyEntries);
                    ItemRefineAttribute iru = new ItemRefineAttribute();
                    iru.ID = Convert.ToUInt16(spilitline[0]);
                    iru.Type = (Game.Enums.PerfectionEffect)Convert.ToByte(spilitline[1]);
                    iru.Level = Convert.ToByte(spilitline[2]);
                    iru.DoWhat = Convert.ToUInt16(spilitline[3]);
                    iru.DoWhat2 = Convert.ToUInt16(spilitline[4]);
                    iru.Chance = Convert.ToUInt16(spilitline[5]);
                    AttributeList.Add(iru.ID, iru);
                }
            }
        }
        #endregion
        #region AbilityScore
        public static Dictionary<ushort, ItemAbilityScore> AbilityScore = new Dictionary<ushort, ItemAbilityScore>();
        public const byte Jiang = 1,
            Chi = 2,
            Enchant = 4,
            Level = 5,
            Stars = 6,
            Socket = 10,
            Gems = 11,
            Plus = 12,
            Soul = 13,
            Bless = 15;
        public class ItemAbilityScore
        {
            public ushort ID = 0;
            public byte Type = 0;
            public uint Min = 0;
            public uint Max = 0;
            public uint Points = 0;
        }
        public static void LoadItemAbilityScore()
        {
            if (File.Exists(Constants.DataHolderPath + "ability_score.txt"))
            {
                string[] Lines = File.ReadAllLines(Constants.DataHolderPath + "ability_score.txt");
                foreach (var line in Lines)
                {
                    var spilitline = line.Split(new string[] { "@@", " " }, StringSplitOptions.RemoveEmptyEntries);
                    ItemAbilityScore iru = new ItemAbilityScore();
                    iru.ID = Convert.ToUInt16(spilitline[0]);
                    iru.Type = Convert.ToByte(spilitline[1]);
                    iru.Min = Convert.ToUInt32(spilitline[2]);
                    iru.Max = Convert.ToUInt32(spilitline[3]);
                    iru.Points = Convert.ToUInt32(spilitline[4]);
                    AbilityScore.Add(iru.ID, iru);
                }
            }
        }
        #endregion
        public static void Load()
        {
            LoadItemRefineCost();
            LoadItemRefineUpgrade();
            LoadItemAbilityScore();
            LoadItemRefineAttribute();
        }
    }
}
