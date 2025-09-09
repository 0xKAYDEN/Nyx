using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Game
{
    public class BotShop : Network.Writer
    {
        public const int
             TimeStamp = 4,
             _Mesh = TimeStamp + 4,
             _UID = _Mesh + 4,//8
             _GuildID = _UID + 4,//12
             _GuildRank = _GuildID + 4, //16,
             _StatusFlag = _GuildRank + 6, //22,
             _StatusFlag2 = _StatusFlag + 8,
             _StatusFlag3 = _StatusFlag2 + 8,
             _AppearanceType = 50, //42
             _Hitpoints = 94,//86
             _MonsterLevel = _Hitpoints + 6,//92,
             _X = _MonsterLevel + 2,// 94,
             _Y = _X + 2, //96
             _HairStyle = _Y + 2, //98,
             _Facing = _HairStyle + 2,// 100,
             _Action = _Facing + 1, //101,
             _Reborn = _Action + 7, //108,
             _Level = _Reborn + 1, //109,
             _WindowSpawn = _Level + 2, //111,
             _Away = _WindowSpawn + 1,// 112,
             _ExtraBattlepower = _Away + 1,// 113,
             _FlowerIcon = _ExtraBattlepower + 12, //125,
             Uint32_FlowerRank = 137,
             _NobilityRank = _FlowerIcon + 8, //133,
             _QuizPoints = _NobilityRank + 9, //142,
             _ClanUID = _QuizPoints + 27,// 169,
             _ClanRank = _ClanUID + 4, //173,
             _Title = _ClanRank + 8, //181,
             _EntitySize = _Title + 2, //183
             _ShowArenaGlow = _EntitySize + 9,// 192,
             _Boss = _ShowArenaGlow + 3, //195,
             _RaceItem = _Boss + 2, //197,
             _ActiveSubclass = _RaceItem + 15, //212,
             _FirstRebornClass = _ActiveSubclass + 9, //221,
             _SecondRebornClass = _FirstRebornClass + 2,// 223,
             _Class = _SecondRebornClass + 2, // 225, 
             _CountryCode = _Class + 2,// 227,
             _AssassinAsBattlePower = _CountryCode + 6, // 231
             _JingHu_Talen = _AssassinAsBattlePower + 4,
             _JiangHuActive = _JingHu_Talen + 1,
             _Names = 257,


        Head = 52,
            Garment = 56,
            Armor = 60,
            LeftWeapon = 64,
            RightWeapon = 68,
            LeftWeaponAccessory = 72,
            RightWeaponAccessory = 76,
            Steed = 80,
            MountArmor = 84,
            ArmorColor = 145,
            LeftWeaponColor = 147,
            HeadColor = 149,
            SteedPlus = 155,
            SteedColor = 161,
            HeadSoul = 204,
            ArmorSoul = 208,
            LeftWeaponSoul = 212,
            RightWeaponSoul = 216;




        public static bool operator >(BotShop bot, Client.GameClient client)
        {
            if (!client.Screen.BOTSTALL.ContainsKey(bot.UID))
            {
                if (Kernel.GetDistance(bot.X, bot.Y, client.Entity.X, client.Entity.Y) < 16 && client.Entity.MapID == bot.MapID)
                {
                    bot.Send(client);
                    return true;
                }
            }
            return false;
        }
        public static bool operator <(BotShop bot, Client.GameClient client)
        {
            if (Kernel.GetDistance(bot.X, bot.Y, client.Entity.X, client.Entity.Y) >= 16 && client.Entity.MapID == bot.MapID)
            {
                return true;
            }
            return false;
        }

        public SafeDictionary<uint, Game.ConquerStructures.BoothItem> ItemList;

        public static Counter Shopss = new Counter(20000);
        public static Counter COUNER_SHOPS = new Counter(110000000);
        public static Dictionary<uint, BotShop> STATICSTALLS = new Dictionary<uint, BotShop>();
        public static Counter ITEMS = new Counter(100);


        public void WriteItems(uint Item, ushort Offset)
        {
            Write(Item, Offset, SpawnPacket);
        }

        private byte[] SpawnPacket;
        private string _Name;
        public Network.GamePackets.SobNpcSpawn Base;
        public ushort MapID;
        private Network.GamePackets.Message HawkMessage;
        public BotShop(string name, ushort _MapID, ushort _xx, ushort _yy)
        {
            try
            {

                ItemList = new SafeDictionary<uint, ConquerStructures.BoothItem>();

                MapID = _MapID;
                Base = new Network.GamePackets.SobNpcSpawn();
                Base.UID = Shopss.Next;
                Name = name;
                Base.Mesh = 406;
                Base.Type = Game.Enums.NpcType.Booth;
                Base.ShowName = true;
                Base.Name = Name;
                Base.MapID = MapID;
                Base.X = (ushort)(_xx + 1); ;//(ushort)(Owner.Entity.X + 1);
                Base.Y = _yy;// Owner.Entity.Y;
                X = _xx;
                Y = _yy;

                UID = COUNER_SHOPS.Next;
                STATICSTALLS.Add(Base.UID, this);

            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
        public void AddItem(Game.ConquerStructures.BoothItem.CostType CostTyp, uint Amount, Network.GamePackets.ConquerItem item)
        {
            try
            {
                var Basitem = new ConquerStructures.BoothItem();
                Basitem.Cost_Type = CostTyp;
                Basitem.Cost = Amount;
                Basitem.Item = new Network.GamePackets.ConquerItem(true);
                Database.ConquerItemInformation infos = new Database.ConquerItemInformation(item.ID, item.Plus);
                Basitem.Item.Durability = Basitem.Item.MaximDurability = infos.BaseInformation.Durability;
                Basitem.Item.UID = ITEMS.Next;
                Basitem.Item.Plus = item.Plus;
                Basitem.Item.Bless = item.Bless;
                Basitem.Item.ID = item.ID;
                Basitem.Item.Color = item.Color;//(Enums.Color)Kernel.Random.Next(1, 8);
                Basitem.Item.Enchant = item.Enchant;
                Basitem.Item.Purification = item.Purification;
                Basitem.Item.ExtraEffect = item.ExtraEffect;
                Basitem.Item.SocketOne = item.SocketOne;
                Basitem.Item.SocketTwo = item.SocketTwo;

                if (item.Purification.PurificationItemID != 0)
                {
                    var purific = Basitem.Item.Purification;
                    purific.ItemUID = Basitem.Item.UID;
                    Basitem.Item.Purification = purific;
                }
                else
                {
                    Basitem.Item.Purification = new Network.GamePackets.ItemAdding.Purification_();
                }

                if (item.ExtraEffect.EffectID != 0)
                {
                    var ExtraEffect = Basitem.Item.ExtraEffect;
                    ExtraEffect.ItemUID = Basitem.Item.UID;
                    Basitem.Item.ExtraEffect = ExtraEffect;
                }
                else
                {
                    Basitem.Item.ExtraEffect = new Network.GamePackets.ItemAdding.Refinery_();
                }

                ItemList.Add(Basitem.Item.UID, Basitem);
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
        public string Hawk
        {
            set
            {
                if (value != "" && value != "0")
                    HawkMessage = new Network.GamePackets.Message(value, "ALL", Name, System.Drawing.Color.Yellow, Network.GamePackets.Message.HawkMessage);
            }
        }

        public ConquerStructures.NobilityRank NobilityRank
        {
            get
            {
                return (Game.ConquerStructures.NobilityRank)SpawnPacket[_NobilityRank];
            }
            set
            {
                SpawnPacket[_NobilityRank] = (byte)value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                SpawnPacket = new byte[8 + _Names + _Name.Length + 18];
                Write((ushort)(SpawnPacket.Length - 8), 0, SpawnPacket);
                Write(10014, 2, SpawnPacket);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, SpawnPacket);
                Write(new List<string>() { _Name, "" }, _Names, SpawnPacket);
            }
        }

        private ushort _body, _transformationid, _face, _x, _y, _hairstyle;
        private uint _uid;
        public uint UID
        {
            get
            {
                return _uid;
            }
            set
            {
                _uid = value;
                Write(value, _UID, SpawnPacket);
            }
        }
        public ushort Body
        {
            get
            {
                return _body;
            }
            set
            {
                Write((uint)(TransformationID * 10000000 + Face * 10000 + value), _Mesh, SpawnPacket);
                _body = value;
            }
        }
        public ushort TransformationID
        {
            get
            {
                return _transformationid;
            }
            set
            {
                _transformationid = value;
                Write((uint)(value * 10000000 + Face * 10000 + Body), _Mesh, SpawnPacket);
            }
        }
        public ushort Face
        {
            get
            {
                return _face;
            }
            set
            {
                Write((uint)(TransformationID * 10000000 + value * 10000 + Body), _Mesh, SpawnPacket);
                _face = value;
            }
        }
        private ushort X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                Write(value, _X, SpawnPacket);
            }
        }
        private ushort Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                Write(value, _Y, SpawnPacket);
            }
        }
        public ushort Action
        {
            get { return BitConverter.ToUInt16(SpawnPacket, _Action); }
            set
            {
                Write(value, _Action, SpawnPacket);
            }
        }
        public Enums.ConquerAngle Facing
        {
            get { return (Enums.ConquerAngle)SpawnPacket[_Facing]; }
            set
            {
                SpawnPacket[_Facing] = (byte)value;
            }
        }

        public ushort HairStyle
        {
            get
            {
                return _hairstyle;
            }
            set
            {

                _hairstyle = value;
                Write(value, _HairStyle, SpawnPacket);
            }
        }
        public void Send(Client.GameClient client)
        {
            client.Send(SpawnPacket);
            client.Send(Base.Encode());
            if (HawkMessage != null)
                client.Send(HawkMessage.Encode());
        }


    }
}
