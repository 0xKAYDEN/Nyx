
using System;
using Nyx.Server.Network.GamePackets;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server
{
    public class Constants
    {
        public static readonly Message FullInventory = new Message("There is not enough room in your inventory!", System.Drawing.Color.Red, Message.TopLeft),
            OneKissADay = new Message("You may only send 1 kiss a day", System.Drawing.Color.Red, Message.TopLeft),
            th30FlowerADay = new Message("You may only send 30 free flower a day", System.Drawing.Color.Red, Message.TopLeft),
            OneFlowerADay = new Message("You may only send 1 flower a day", System.Drawing.Color.Red, Message.TopLeft),
            TradeRequest = new Message("Trade request sent.", System.Drawing.Color.Red, Message.TopLeft),
            TradeInventoryFull = new Message("There is not enough room in your partner inventory.", System.Drawing.Color.Red, Message.TopLeft),
            TradeInProgress = new Message("An trade is already in progress. Try again later.", System.Drawing.Color.Red, Message.TopLeft),
            FloorItemNotAvailable = new Message("You need to wait until you will be able to pick this item up!", System.Drawing.Color.Red, Message.TopLeft),
            JailItemUnusable = new Message("You can't use this item in here!", System.Drawing.Color.Red, Message.TopLeft),
            PKForbidden = new Message("PK is forbidden in this region.", System.Drawing.Color.Red, Message.Talk),
            ExpBallsUsed = new Message("You can use only ten exp balls a day. Try tomorrow.", System.Drawing.Color.Red, Message.TopLeft),
            SpellLeveled = new Message("Congratulation, you have just leveled your spell.", System.Drawing.Color.Red, Message.TopLeft),
            ProficiencyLeveled = new Message("Congratulation, you have just leveled your proficiency.", System.Drawing.Color.Red, Message.TopLeft),
            AutoHunt = new Message("You can't use this item in here!", System.Drawing.Color.Red, Message.TopLeft),
            ArrowsReloaded = new Message("Arrows Reloaded.", System.Drawing.Color.Red, Message.TopLeft),
            Warrent = new Message("The guards are looking for you!", System.Drawing.Color.Red, Message.TopLeft),
            VIPExpired = new Message("Your VIP has expired. Please reactivate your VIP if you wish to keep VIP services.", System.Drawing.Color.Red, Message.World),
            VIPLifetime = new Message("Your VIP service is unlimited.", System.Drawing.Color.Red, Message.World),
            WrongAccessory = new Message("You cannot wear this accessory and this item at the same time.", System.Drawing.Color.Red, Message.World),
            NoAccessory = new Message("You cannot wear an accessory without a support item.", System.Drawing.Color.Red, Message.World),
            vipteleport = new Message("You can't teleport in this map.", System.Drawing.Color.Red, Message.World);

        public static Message VIPRemaining(string days, string hours)
        {
            return new Message("You have " + days + " day(s) and " + hours + " hour(s) of VIP service remaining.", System.Drawing.Color.Red, Message.World);
        }
        public static Message NoArrows(string name)
        {
            return new Message("Can't reload arrows, you are out of " + name + "s!", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Stigma(float percent, int time)
        {
            return new Message("Stigma activated. Your attack will be increased with " + percent + " for " + time + ".", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Accuracy(int time)
        {
            return new Message("Accuracy activated. Your agility will be increased a bit for " + time + ".", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Invisibility(int time)
        {
            return new Message("Invisibility activated. You will be invisible for monsters as long as you don't attack for " + time + ".", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Shield(float percent, int time)
        {
            return new Message("Shield activated. Your defence will be increased with " + percent + " for " + time + ".", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Shackled(int time)
        {
            return new Message("You have been shackled and can not move for " + time + " Seconds.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message ShackleTime(DateTime time)
        {
            return new Message("SoulShackle already activated. You need to wait for " + time + " seconds more", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Auras(string Aura, string Power, ushort Att, short time)
        {
            return new Message("You Activated " + Aura + " and got " + Power + " " + Att + "% for " + time + " seconds.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message TyrantAura(string Aura, string Power, ushort Att, string Power2, ushort Att2, short time)
        {
            return new Message("You Activated " + Aura + " and got " + Power + " " + Att + "% + " + Power2 + " " + Att2 + "% for " + time + " seconds.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Dodge(float percent, int time)
        {
            return new Message("Dodge activated. Your dodge will be increased with " + percent + " for " + time + ".", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message NoDrugs(int time)
        {
            return new Message("Poison star activated. You will not be able to use drugs for " + time + " seconds.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message ExtraExperience(uint experience)
        {
            return new Message("You Have Gained Extra " + experience + " Experience for Killing the Monster.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message TeamExperience(uint experience)
        {
            return new Message("One of your teammates killed a monster so you gained " + experience + " experience.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message NoobTeamExperience(uint experience)
        {
            return new Message("One of your teammates killed a monster and because you have a noob inside your team, you gained " + experience + " experience.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message PickupGold(uint amount)
        {
            return new Message("You received " + amount + " silver.", System.Drawing.Color.Red, Message.Talk);
        }
        public static Message AzureShield(float percent, int time)
        {
            return new Message("AzureShield activated. Your defence will be increased with " + percent + " for " + time + " Seconds.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message PickupConquerPoints(uint amount)
        {
            return new Message("You received " + amount + " Conquer Points.", System.Drawing.Color.Red, Message.Talk);
        }
        public static Message PickupItem(ConquerItem item)
        {
            return new Message("You obtained " + item.StackSize + " " + Database.ConquerItemInformation.BaseInformations[item.ID].Name + ".", System.Drawing.Color.Red, Message.Talk);
        }
        public static Message DummyLevelTooHigh()
        {
            return new Message("You can't attack this dummy because your level is not high enough.", System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message BoothItemSell(string buyername, string itemname, bool conquerpoints, uint cost)
        {
            return new Message("Congratulations. You just have just sold " + itemname + " to " + buyername + " for " + cost + (conquerpoints ? " ConquerPoints." : " Gold."), System.Drawing.Color.Red, Message.TopLeft);
        }
        public static Message Enchant(int origEnch, int newEnch)
        {
            if (newEnch <= origEnch)
                return new Message("You were unlucky. You didn't gain any more enchantment in your item. Your generated enchant is " + newEnch + ".", System.Drawing.Color.Red, Message.TopLeft);
            else
                return new Message("You were lucky. You gained more enchantment in your item. Your generated enchant is " + newEnch + ".", System.Drawing.Color.Red, Message.TopLeft);
        }
        public const string DataHolderPath = "database\\",
        ShopsPath = "database\\shops\\Shop.dat",
        EShopsPath = "database\\shops\\emoneyshop.ini",
        EShopsV2Path = "database\\shops\\emoneyshopV2.ini",
        HonorShopPath = "database\\shops\\HonorShop.ini",
        RaceShopPath = "database\\shops\\RacePointShop.ini",
        ChampionShopPath = "database\\shops\\GoldenLeagueShop.ini",
        PortalsPath = "database\\Portals.ini",
        RevivePoints = "database\\RevivePoints.ini",
        MonstersPath = "database\\Monsters.txt",
        ItemBaseInfosPath = "database\\Items.txt",
        QuizShow = "database\\QuizShow.txt",
        ItemPlusInfosPath = "database\\ItemsPlus.ini",
        SoulGearInformation = "database\\soulgear.txt",
        ServerKey = "TQServer",
        BugList = "database\\BugList.txt",
        GameCryptographyKey = "C238xs65pjy7HU9Q";
        public const int MaxBroadcasts = 50;
        public static uint ExtraExperienceRate, ExtraSpellRate, ExtraProficiencyRate, MoneyDropRate, ConquerPointsDropRate, ItemDropRate;
        public static string[] ItemDropQualityRates;
        public const sbyte pScreenDistance = 19;
        public const sbyte nScreenDistance = 19;
        public const sbyte remScreenDistance = 19;
        public const ushort DisconnectTimeOutSeconds = 10,
            FloorItemSeconds = 25,
            FloorItemAvailableAfter = 20;

        public static readonly System.Collections.Generic.List<ushort> SkillSoul_Allowed = new System.Collections.Generic.List<ushort>() 
        { 
            11005, 
            1165, 
            6001, 
            6000,
            11650, 
            1002, 
            10415, 
            11110,
            11600, 
            10381, 
            1046, 
            11190,         
            11000,  
            5010, 
            1420, 
            1045,
            11070,
            11170,
            1115,
        };
        public static readonly System.Collections.Generic.List<ushort> PKForbiddenMaps = new System.Collections.Generic.List<ushort>()
            {
                1036,
                1002,
                5550,
                6000,
                700,
                3825,
                4020,
                3055,
                1738,
                1039,
                3852,
                1004,
                1006,
                8880,
                8881,
                1950,
                8800,
                8801,
                8802,
                8803,
                601
            };

        public static readonly System.Collections.Generic.List<uint> NoDropItems = new System.Collections.Generic.List<uint>() 
        { 
            1100006,1100009,2100055,2100065,2100075,2100085,2100095,1100003,750000, 
            729304,729611,729612,729613,729614,729703,711609,711610,711611,750000 
        };

        public static readonly System.Collections.Generic.List<ushort> PKFreeMaps = new System.Collections.Generic.List<ushort>()
            {
                8080,
                3955,
                1700,
                3957,
                3956,
3856,
                3958,
                3959,
                2527,
                1505,
                3998,
                1042,
                1043,
                1044,
                1045,
                1047,
                1048,
                1046,
                2526,
                2522,
                2065,
                1038,
                2072,
                2073,
                2071,
                2074,
                2075,
                1005,
                6000,
                6001,
                1734,
                1733,
                1732,
                1737,
                1731,
                1730,
                7001,
                2057,
                1734,
                1733,
                1732,
                1731,
                1730,
                1508,
                1518,
                8877,
                3333,
                3355,
                4562,
                3935,
                4662,
                5560,
                1509
            };

        public static readonly System.Collections.Generic.List<int> AvaibleSpells = new System.Collections.Generic.List<int>()
            {
                1380,
                1385, 
                1390,
                1395,
                1400, 
                1405,
                1410,
                1046,
                1045,
                5030,
                7001,
                7002,
                7003,
                7020,
                1415,
    1418,
    1416,
    1417,
    1419,
                1360
        
            };
    }
}
