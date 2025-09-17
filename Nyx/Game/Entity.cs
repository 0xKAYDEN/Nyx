using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;
using System.Collections;
using Nyx.Server.Interfaces;
using Nyx.Server.Database;
using System.Collections.Concurrent;
using Nyx.Server.Client;
using Nyx.Server.GamePackets;
using Message = Nyx.Server.Network.GamePackets.Message;


namespace Nyx.Server.Game
{
    public class Entity : Writer, Interfaces.IBaseEntity, Interfaces.IMapObject
    {

        public Dictionary<uint, FloorItem> FloorItems = new Dictionary<uint, FloorItem>();
        public Dictionary<uint, uint> Wings = new Dictionary<uint, uint>();
        public WardrobeTitles WTitles;
        public List<string> NowEquippedWing = new List<string>();
        public List<string> NowEquippedTitle = new List<string>();
        public byte[] DailySignDays;
        public byte DailySignRewards;
        public byte DailySignVIPChances;
        public List<string> BlackList;
        public int SelectedStage;
        public uint mdf31;
        public uint Nullifications;
        public int SelectedAttribute;
        public bool SwordSoul = false;
        public bool BansheeSpirit = false;
        public byte XPCountTwist = 0;
        public Time32 ManiacDance;
        public Time32 XpBlueStamp;
        public Time32 BackfireStamp;
        public Time32 WaveofBlood;
        public Time32 ScarofEarth;
        public byte WaveofBloodXp = 8;
        public HashSet<int> CompletedPromotions = new HashSet<int>();
        public bool EpicWarrior()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null && weapons.Item2 != null)
                    if (weapons.Item1.ID / 1000 == 624 && weapons.Item2.ID / 1000 == 624)
                        return true;
            }
            return false;
        }

        #region ScurvyBomb
        public byte ScurbyBomb;
        public Time32  ScurbyBombStamp, ScurbyBomb2Stamp;
        #endregion
        #region Values
        #region Method
        public InnerPower InnerPower;
        public Entity SpookDetails;
        public Map TOMM;
        private Enums.PKMode _PKMode;
        public Game.Features.Flowers MyFlowers;
        private EntityFlag _EntityFlag;
        private MapObjectType _MapObjectType;
        public Enums.Mode Mode;
        public Database.MonsterInformation MonsterInfo;
        private BitVector32 BitVector32 = new BitVector32(32 * 5);
        public Enums.AppearanceType AppearanceBkp;
        public Achievement MyAchievement;
        public Features.Flowers Flowers;
        public Enums.PKMode PrevPKMode;
        public Features.Tournaments.TeamElitePk.Match SkillTeamWatchingElitePKMatch;
        public Action<Entity> OnDeath;
        public Network.GamePackets.Attack AttackPacket;
        public Network.GamePackets.Attack VortexPacket;
        private Client.GameClient _Owner;
        public ConcurrentDictionary<int, DateTime> Halos;
        public Tournaments.Elite_client Elite;
        private WareHousePassword.Nextaction _NextAction = WareHousePassword.Nextaction.Nothing;
        public Game.JiangHu.AttackFlag AttackJiang = JiangHu.AttackFlag.None;
        public Network.GamePackets.Gambleing Gambleing;
        public ConquerItem LottoItem = null;
        public ConquerItem LotteryPrize;
        public Nyx.Server.Game.JiangHu MyJiang;
        public Game.Subclasses SubClasses = new Game.Subclasses();
        public Game.Tournaments.Team_client Team;
        public Game.Tournaments.Skill_client Skill;
        private Network.GamePackets.Update update;
        #endregion
        #region Lists And Dictionaries
        public Dictionary<uint, Game.PkExpeliate> PkExplorerValues = new Dictionary<uint, PkExpeliate>();
        public List<Game.ConquerStructures.ChiRetreatStructure> RetreatChiPowers = new List<Game.ConquerStructures.ChiRetreatStructure>(); 
        public ConcurrentDictionary<TitlePacket.Titles, DateTime> Titles;
        public List<ushort> MonstersSpells = null;
        public List<Clone> MyClones = new List<Clone>();
        public Time32 ClonePacket;
        public Dictionary<uint, ConquerItem> StorageItems;
        #endregion
        #region Bytes & SB
        public byte AzureShieldLevel = 0;
        public byte TOMQ = 255;
        public byte TOM = 0;
        public byte TOMRC = 3;
        public byte EditNameTimes = 5;
        public byte GLCTF;
        public byte AutoRev = 0;
        public byte ClaimedActivenessGift1 = 0;
        public byte ClaimedActivenessGift2= 0;
        public byte ETHAPItemAmount = 0;
        public byte StarterPackLevel = 0;
        public byte SpiritBeadsType = 0;
        public byte RareMaterialsAmount;
        public byte ClaimedActivenessGift3 = 0;
        private byte _Dodge;
        public byte CountKilling;
        private byte _vipLevel;
        public static byte ScreenDistance = 0;
        byte cls, secls, seclss;
        private byte _stamina, _class, _reborn = 0, _level;
        public static sbyte[] XDir = new sbyte[] { 0, -1, -1, -1, 0, 1, 1, 1 };
        public static sbyte[] YDir = new sbyte[] { 1, 1, 0, -1, -1, -1, 0, 1 };
        public static sbyte[] XDir2 = new sbyte[] { 0, -2, -2, -2, 0, 2, 2, 2, -1, -2, -2, -1, 1, 2, 2, 1, -1, -2, -2, -1, 1, 2, 2, 1 };
        public static sbyte[] YDir2 = new sbyte[] { 2, 2, 0, -2, -2, -2, 0, 2, 2, 1, -1, -2, -2, -1, 1, 2, 2, 1, -1, -2, -2, -1, 1, 2 };
        public byte FirstRebornLevel, SecondRebornLevel;
        public byte[] SpawnPacket;
        public byte ToxicFogLeft, FlashingNameTime, FlyTime, InvisibilityTime, StarOfAccuracyTime, DodgeTime, AccuracyTime, ShieldTime, MagicDefenderSecs;
        private byte _receivedEnlighenPoints;
        public ushort MagicShieldTime = 0;
        public ushort StigmaTime = 0;
        public byte LotteryJadeAdd;
        #endregion
        #region Uints
        public uint Aura_actLevel;
        public uint OnlineTraining, BlessedHunting;
        public uint RareMaterialsItemID = 0;
        public uint KingdomDeed = 0;
        public uint StrikePoints = 0;
        public uint TodayStrikePoints = 0;
        public uint totalperfectionscore_ = 0;
        public uint TotalPerfectionScore
        {
            get
            {
                if (Owner == null) return totalperfectionscore_;
                uint points = 0;
                points += Owner.Equipment.GetFullEquipmentEnumPoints;
                points += Owner.Equipment.GetFullEquipmentSocketPoints;
                points += Owner.Equipment.GetFullEquipmentGemPoints;
                points += Owner.Equipment.GetFullEquipmentPlusPoints;
                points += Owner.Equipment.GetFullEquipmentBlessPoints;
                points += Owner.Equipment.GetFullEquipmentRefinePoints;
                points += Owner.Equipment.GetFullEquipmentSoulPoints;
                points += Owner.Equipment.GetFullEquipmentEnchantPoints;
                points += Owner.Equipment.GetFullEquipmentPerfecetionLevelPoints;
                points += Owner.Equipment.GetFullEquipmentLevelPoints;
                points += (uint)MsgUserAbilityScore.CalculatePerfectionChiPoints(Owner);
                points += (uint)MsgUserAbilityScore.CalculatePerfectionJiangPoints(Owner);
                points += (uint)((Vitality + Spirit + Strength + Agility + Atributes) * 5);
                points += (uint)(Level < 140 ? Level * 20 : Level * 25);
                if (InnerPower != null) points += InnerPower.TotalScore * 2;
                points += (uint)((uint)NobilityRank * 1000);
                points += (uint)(Reborn * 1000);
                points += (Database.StorageItem.PerfectionPoints(Owner, true));
                points += (Database.StorageItem.PerfectionPoints(Owner, false));
                points += (uint)(MsgUserAbilityScore.CalculateSubClassPoints(Owner));
                return points;
            }
            set
            {
                totalperfectionscore_ = value;
            }
        }
        public uint NormalGems;
        public uint ChiStudyTimes = 0;
        public uint StrResID = 0;
        public uint JiangStudyTimes = 0;
        public byte EnlightmentTimes = 0;
        public byte HoursTimes = 0;
        public bool DidTIB = false;
        public uint ETHAPItemID = 0;
        uint _ExtraInventory;
        public uint KillArena1 = 0, KillArena2 = 0, KillArena3 = 0, KillArena4 = 0, KillArena5 = 0, KillArena6 = 0, KillArena7 = 0;
        public uint TransformationMaxHP = 0;
        private uint _heavenblessing, _money, _uid, _hitpoints, _maxhitpoints;
        private uint _conquerpoints, _boundCps;
        public uint TransformationAttackRange = 0;
        public uint LotteryItemID = 0;
        public uint InteractionType = 0;
        public uint InteractionWith = 0;
        public uint ItemHP = 0;
        public uint Weight;
        private uint _MinAttack, _MaxAttack, _MagicAttack;
        public uint BaseMinAttack, BaseMaxAttack, BaseMagicAttack, BaseMagicDefence;
        private uint _TransMinAttack, _TransMaxAttack, _TransDodge, _TransPhysicalDefence, _TransMagicDefence;
        public uint Accurity = 0;
        public uint MAttack;
        public uint MDefense;
        private uint guildBP = 0;
        public uint Aura_actPower;
        public uint LotteryItemPlus;
        public uint LotteryItemSocket1;
        public uint LotteryItemSoc2;
        public uint LotteryItemColor;
        public uint LotteryItemSoc1;
        #endregion
        #region Ushorts
        public ushort Detoxication;
        private ushort _doubleexp, _superpotion, _body, _transformationid, _face, _strength, _agility, _spirit, _vitality, _atributes, _mana, _maxmana, _hairstyle, _mapid, _previousmapid, _x, _y, _pkpoints;
        public ushort _Flag;
        public bool KilledPluto = false;
        public ushort Intensification;
        public ushort InteractionX = 0;
        public ushort SpiritBeadsCount = 0;
        public ushort InteractionY = 0;
        public ushort FinalMagicDmgPlus;
        public ushort FinalMagicDmgReduct;
        public ushort FinalDmgPlus;
        public ushort FinalDmgReduct;
        public ushort DragonSwingPower;
        public ushort ShieldBlockPercent;
        private ushort _actionX, _actionY;
        public ushort KOSpell = 0;
        private double _enlightenPoints;
        private ushort _enlightmenttime;
        public ushort KOCount = 0;
        private ushort _MDefence, _MDefencePercent;
        public ushort BaseDefence;
        public ushort ItemMP = 0, PhysicalDamageDecrease = 0, PhysicalDamageIncrease = 0, MagicDamageDecrease = 0, MagicDamageIncrease = 0, AttackRange = 1, Vigor = 0;

        public ushort HitRate { get; set; }
        public ushort AzureShieldDefence = 0;
        public ushort EquipAgility = 0;
        #endregion
        #region Shorts
        public short CycloneTime = 0, SuperCycloneTime = 0, SupermanTime = 0, NoDrugsTime = 0, FatalStrikeTime = 0, ShurikenVortexTime = 0, OblivionTime = 0, AuraTime = 0, ShackleTime = 0, ChaosTime = 0, AzureTime;
        #endregion
        #region Ints
        public int lianhuaranLeft;
        public int Shock;
        public int FrozenTime;
        public int SuperItemBless;
        int _Energy = 0;
        public int TransformationTime = 0;
        public int Accuracy;
        public int AzureDamage = 0;
        public int ChainboltTime;
        public int EarthResistance;
        public int Penetration;
        public int Counteraction;
        public int MetalResistance;
        public int WoodResistance;
        public int WaterResistance;
        public int FireResistance;
        public int Immunity;
        public int FatigueSecs;
        public int Breaktrough;
        public int CriticalStrike;
        public int SkillCStrike;
        public int Block;
        public int[] Gems = new int[GemTypes.Last];
        public int BlackSpotStepSecs;
        public int Fright;
        public int DragonFuryTime;
        public const int DefaultDefense2 = 10000;
        #endregion
        #region Ulongs
        public ulong Aura_actType2;
        public ulong AutoHuntEXP { get; set; }
        private ulong _experience;
        public ulong Aura_actType;
        public ulong Merchant;
        #endregion
        #region Longs
        #endregion
        #region Strings
        public string LoweredName;
        private string _Name, _Spouse;
        public string NewName = "";
        #endregion
        #region Times
        public Time32 DragonFlowStamp, AutoRevStamp;
        public Time32 DragonCyclone;
        public Time32 DeathStamp, AttackStamp, StaminaStamp, VortexAttackStamp, CycloneStamp, SupermanStamp, FatigueStamp, CannonBarrageStamp,
                              StigmaStamp, StarOfAccuracyStamp, DragonFlow, InvisibilityStamp, MagicShieldStamp, DodgeStamp, EnlightmentStamp, BlackSpotStamp, BlackbeardsRageStamp, DefensiveStanceStamp,
                              AccuracyStamp, ScurvyBombStamp, ShieldStamp, FlyStamp, NoDrugsStamp, ToxicFogStamp, FatalStrikeStamp, DoubleExpStamp, BladeTempest, MagicDefenderStamp,
                              ShurikenVortexStamp, IntensifyStamp, SpiritFocusStamp, TransformationStamp, CounterKillStamp, PKPointDecreaseStamp,
                              HeavenBlessingStamp, OblivionStamp, AuraStamp, ShackleStamp, AzureStamp, StunStamp, GuildRequest = Time32.Now,
                              BladeFlurryStamp, SuperCycloneStamp, IncreaseFinalMDamageStamp, IncreaseFinalPDamageStamp, IncreaseFinalMAttackStamp,
        IncreaseFinalPAttackStamp, IncreaseImunityStamp, IncreaseAntiBreackStamp, IncreasePStrikeStamp, IncreaseBreackStamp, GodlyShieldStamp,
        IncreaseAttributeStamp;
        public Time32 DivineGuardStamp, ShieldBreakStamp;
        public Time32 DragonSwingStamp;
        public Time32 DragonFuryStamp;
        public Time32 lianhuaranStamp;
        public DateTime LastGetEnergy;
        public DateTime PledgeStamp;
        public DateTime FlashingNameStamp;
        public DateTime AzureShieldStamp = DateTime.Now;
        private Time32 spiritFocusStamp;
        public Time32 EagleEyeStamp;
        public Time32 MortalWoundStamp;
        public Time32 SpellStamp;
        public Time32 ChainboltStamp;
        public Time32 FrozenStamp;
        public Time32 Cursed;
        public Time32 SkillCoolDown;
        public Time32 DragonCycloneStamp { get; set; }
        public Time32 DragonFury { get; set; }
        public DateTime CongeladoTimeStamp;
        public Time32 GaleBombStamp { get; set; }
        public Time32 BladeTempestStamp { get; set; }
        public Time32 ShockStamp;
        #endregion
        #region Booleans
        public bool CauseOfDeathIsMagic = false;
        public bool LoadedActivenessSystem = false;
        public bool AnsweredKingdomQuiz;
        public bool DidTOM = false;
        public bool DidBrightFortune = false;
        public bool IncreaseFinalMDamage = false,
        IncreaseFinalPDamage = false,
        IncreaseFinalMAttack = false,
        IncreaseFinalPAttack = false,
        IncreaseImunity = false,
        IncreaseAntiBreack = false,
        IncreasePStrike = false,
        IncreaseBreack = false,
        IncreaseAttribute = false,
        GodlyShield = false;
        public bool ClaimedTOM = false;
        public bool InTOM = false;
        public bool CounterKillSwitch = false;
        public byte FirstCredit;
        public bool DidFirstHeavenTreasury = false;
        public bool Companion;
        public bool KilledThirllingSpook = false;
        public bool Stunned = false, Confused = false;
        public bool InteractionSet = false;
        public bool Aura_isActive;
        private bool spiritFocus;
        public bool DragonSwing = false;
        public bool FrozenD;
        public bool IsDropped = false;
        public bool HasMagicDefender = false;
        public bool IsDefensiveStance = false;
        public bool MagicDefenderOwner = false;
        public bool Tournament_Signed = false;
        public bool InteractionInProgress = false;
        public bool InSkillPk = false;
        public bool Invisable, IsBlackSpotted, IsEagleEyeShooted = false;
        public bool InTeamArenaorArena;
        public bool InTeamArena;
        public bool InTeamArenaMap;
        public bool OnDragonSwing;
        public bool IsShieldBlock;
        public bool Killed = false;
        public bool awayTeleported = false;
        public bool WearsGoldPrize = false;
        public bool InAutoHunt;
        public bool FullyLoaded = false, HandleTiming = false;
        public Boolean IsGreen(Entity Entity) { return (Entity.Level - Level) >= 3; }
        public Boolean IsWhite(Entity Entity) { return (Entity.Level - Level) >= 0 && (Entity.Level - Level) < 3; }
        public Boolean IsRed(Entity Entity) { return (Entity.Level - Level) >= -4 && (Entity.Level - Level) < 0; }
        public Boolean IsBlack(Entity Entity) { return (Entity.Level - Level) < -4; }
        public bool SendUpdates = false;
        public bool UseItem = false;
        #endregion
        #region Objects
        public object SwitchJiang { get; set; }
        public static object[] name;
        public object CBpSyncRoot = new object();
        public object FlusterTime;
#endregion
        #region Double And Floats
        public float ToxicFogPercent, StigmaIncrease, MagicShieldIncrease, DodgeIncrease, ShieldIncrease;
public double ItemBless = 1.0;
public float lianhuaranPercent;
public double DragonGems;
public double PhoenixGems;
public float IntensifyPercent;
#endregion
        #endregion
        public int EquippedTitle
        {
            get
            {
                return BitConverter.ToInt32(SpawnPacket, 288);
            }
            set
            {
               Writer.Write(value, 288, SpawnPacket);
                foreach (var player in Owner.Screen.Objects.Where(p => p.MapObjType == MapObjectType.Player))
                    if (player != null)
                        if ((player as Entity) != null)
                            (player as Entity).Owner.Send(SpawnPacket);
            }
        }
        public int TitlePoints
        {
            get
            {
                return WTitles != null ? WTitles.Points : BitConverter.ToInt32(SpawnPacket, 292);
            }
            set
            {

                if (value < WTitles.Points)
                    return;
                WriteInt32(value, 292, SpawnPacket);
                Update(89, (uint)value, false);
                foreach (var player in Owner.Screen.Objects.Where(p => p.MapObjType == MapObjectType.Player))
                    if (player != null)
                        if ((player as Entity) != null)
                            (player as Entity).Owner.Send(SpawnPacket);
                if (WTitles != null && WTitles.Points != value)
                {
                    WTitles.Points = value;
                    WTitles.Update();
                }
            }
        }
        private uint Flor;
        public uint FlowerRank
        {
            get { return Flor; }
            set
            {
                Flor = value;
                Writer.Write(value + 10000, 137 + 9 + 4, SpawnPacket);
            }
        }
        public int UTitlePoints
        {
            get
            {
                return BitConverter.ToInt32(SpawnPacket, 292);
            }
            set
            {
                Writer.Write(value, 292, SpawnPacket);
                Update(89, (uint)value, false);
                foreach (var player in Owner.Screen.Objects.Where(p => p.MapObjType == MapObjectType.Player))
                    if (player != null)
                        if ((player as Entity) != null)
                            (player as Entity).Owner.Send(SpawnPacket);
            }
        }
        public int EquippedWing
        {
            get
            {
                return BitConverter.ToInt32(SpawnPacket, 296);
            }
            set
            {
               Writer.Write(value, 296, SpawnPacket);
                foreach (var player in Owner.Screen.Objects.Where(p => p.MapObjType == MapObjectType.Player))
                    if (player != null)
                        if ((player as Entity) != null)
                            (player as Entity).Owner.Send(SpawnPacket);
            }
        }
        public uint QuestKO
        {
            get
            {
                if (EntityFlag == Game.EntityFlag.Player)
                    return Owner["QuestKO"];
                return 0;
            }
            set
            {
                if (EntityFlag == Game.EntityFlag.Player)
                    Owner["QuestKO"] = value;
            }
        }
        public bool IsBowEquipped
        {
            get
            {
                if (EntityFlag == Game.EntityFlag.Player)
                {
                    var right = Owner.Equipment.TryGetItem(ConquerItem.RightWeapon);
                    if (right != null)
                    {
                        return PacketHandler.IsBow(right.ID);
                    }
                }
                return false;
                // return Equipment.Select(ItemPosition.WeaponRight, (x) => x.IsBow); 
            }
        }
        public Time32 ShieldBlockStamp;
        public int AdjustWeaponDamage(Entity target, int damage)
        {
            return MathHelper.MulDiv((int)damage, GetDefense2(target), DefaultDefense2);
        }
        public int GetDefense2(Entity target)
        {
            if (Reborn == 0) return DefaultDefense2;

            var defense2 = (FirstRebornClass % 10) >= 3 ? 7000 : DefaultDefense2;
            if (Reborn < 2)
            {
                return defense2;
            }

            if (target.EntityFlag == Game.EntityFlag.Monster)
            {
                return DefaultDefense2;
            }

            var targetHero = target as Entity;
            if (targetHero != null)
            {
                return targetHero.Reborn < 2 ? 5000 : 7000;
            }

            return defense2;
        }
        public int AdjustMagicDamage(Entity target, int damage)
        {
            return MathHelper.MulDiv(damage, GetDefense2(target), DefaultDefense2);
        }
        public bool RebornSpell(ushort skillid)
        {
            if (skillid == 9876 || skillid == 6002 || skillid == 10315 || skillid == 10311 || skillid == 10313 ||
                skillid == 6003 || skillid == 10405 || skillid == 30000 || skillid == 10310 || skillid == 3050 ||
                skillid == 3060 || skillid == 3080 || skillid == 3090)
                return true;
            else
                return false;
        } 
        public int AdjustData(int data, int adjust, int maxData = 0)
        {
            return MathHelper.AdjustDataEx(data, adjust, maxData);
        }
        public bool OnIntensify()
        {
            return ContainsFlag(Network.GamePackets.Update.Flags.Intensify);
        }
        public int AdjustAttack(int attack)
        {
          //  var addAttack = 0;
            if (OnIntensify())
                //attack = (int)((double)attack * IntensifyPercent); //PvP Reduction!
                attack = (int)((double)attack * 1.5); //PvP Reduction!
            else if (ContainsFlag(Network.GamePackets.Update.Flags.Stigma))
                attack += (int)((double)attack * 0.3); 
            //{
            //    addAttack += Math.Max(0, AdjustData((int)attack, 30)) - (int)attack;
            //    attack = (attack + (addAttack * attack / 100));
            //}             
            if (OnSuperman())
                attack += (int)((double)attack * 0.2); //PvP Reduction!
            if (OnSuperCyclone())
                attack += (int)((double)attack * 0.2); //PvP Reduction!
            
            if (OnOblivion())
                attack += (int)((double)attack * 0.2); //PvP Reduction!
            if (OnFatalStrike())
                attack += (int)((double)attack * 0.2); //PvP Reduction!
            
            //if (TryGetStatus(StatusType.KeepBow, out status))
            //{
            //    addAttack += Math.Max(0, AdjustData(attack, status.Power)) - attack;
            //}

            return attack;
        }
        public int AdjustDefense(int defense)
        {
           // var addDefense = 0;


            if (ContainsFlag(Network.GamePackets.Update.Flags.MagicShield))
                defense += (int)((double)defense * 0.3); //PvP Reduction!
            if (ContainsFlag3(Network.GamePackets.Update.Flags3.WarriorEpicShield))
                defense += (int)((double)defense * 0.4); //PvP Reduction!

            return defense;
        }
        public int AdjustBowDefense(int defense)
        {
            return defense;
        }
        public int AdjustHitrate(int hitrate)
        {
            var addHitrate = 0;

          
            if (ContainsFlag(Network.GamePackets.Update.Flags.StarOfAccuracy))
            {
                addHitrate += Math.Max(0, AdjustData(hitrate,  30)) - hitrate;
            }

            return hitrate + addHitrate;
        }
        public uint ArmorId
        { 
            get
            {
                if (EntityFlag == Game.EntityFlag.Player)
                {
                    var item = Owner.Equipment.TryGetItem(ConquerItem.Armor);
                    if (item !=null)
                        return item.ID;
                }
                return 0;
            } 
        }
        public int ReduceDamage { get { return (int)ItemBless; } }
        public void AzureShieldPacket()
        {
            var Remain = AzureShieldStamp.AddSeconds(MagicShieldTime) - DateTime.Now;
            Network.GamePackets.Update aupgrade = new Network.GamePackets.Update(true);
            aupgrade.UID = UID;
            aupgrade.Append(49
           , 93
           , (uint)Remain.TotalSeconds, AzureShieldDefence, AzureShieldLevel);
            Owner.Send(aupgrade);
        }
        public uint ExtraInventory
        {
            get { return _ExtraInventory; }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.AvailableSlots, 300, false);
                    Update(Network.GamePackets.Update.ExtraInventory, value, false);
                    _ExtraInventory = value;
                }
            }
        }
        public ushort actionX
        {
            get { return _actionX; }
            set { _actionX = value; }
        }
        public ushort actionY
        {
            get { return _actionY; }
            set { _actionY = value; }
        }
        public WareHousePassword.Nextaction NextAction
        {
            get { return _NextAction; }
            set { _NextAction = value; }
        }
        public byte JiangTalent
        {
            get
            {
                return this.SpawnPacket[_JingHu_Talen];
            }
            set
            {
                Network.SafeWriter.Write(value, (ushort)_JingHu_Talen, this.SpawnPacket);
            }
        }
        public bool JiangActive
        {
            get
            {
                return (this.SpawnPacket[_JiangHuActive] == 1);
            }
            set
            {
                Network.SafeWriter.Write(value ? ((byte)1) : ((byte)0), (ushort)_JiangHuActive, this.SpawnPacket);
            }
        }
        public byte _SubClass;
        private uint _SubClassLevel;
        public byte SubClass
        {
            get
            {
                return this._SubClass;
            }
            set
            {
                this._SubClass = value;
                this.SpawnPacket[233] = (this.EntityFlag != Nyx.Server.Game.EntityFlag.Monster) ? this._SubClass : ((byte)0);
               
            }
        }
        public uint SubClassLevel
        {
            get { return _SubClassLevel; }
            set
            {
                _SubClassLevel = value;

            }
        }
        public byte SubClassesActive
        {
            get { return SpawnPacket[_ActiveSubclass]; }
            set { SpawnPacket[_ActiveSubclass] = value; }
        }
        #region Offsets
        public static int
            TimeStamp = 4,
            _Mesh = 8,
            _UID = 12,
            _QuizPoints = 164,  
            _GuildID = 16,
            _GuildRank = 20,
            _StatusFlag = 26,
            _StatusFlag2 = 34,
            _StatusFlag3 = 42,
            _AppearanceType = 50,
            _Hitpoints = 107,
            _MonsterLevel = 113,
            _X = 115,
            _Y = 117,
            _HairStyle = 119,
            _Facing = 121,
            _Action = 122,
            _Reborn = 129,
            _Level = 130,
            _WindowSpawn = 132,
            _Away = 133,
            _ExtraBattlepower = 134,
            _FlowerIcon = 150,
            _NobilityRank = 154,
            _ClanShareBp = 198,
            _ClanUID = 190,
            _ClanRank = 194,
            _Title = 202,
            _ShowArenaGlow = 213,
            _Boss = 216,
            _RaceItem = 218,
            _ActiveSubclass = 234,
            _FirstRebornClass = 242,
            _SecondRebornClass = 244,
            _Class = 246,
            _CountryCode = 248,
            _BattlePower = 254,
            _JingHu_Talen = 258,
            _JiangHuActive = 259,
            _CUID = 270,
            _Names = 300;
        #endregion
        
        public bool IsWarTop(ulong Title)
        {
            return Title >= 11 && Title <= 35;
        }
        public void AddTopStatus3(UInt64 Title, DateTime EndsOn, Boolean Db = true)
        {

            Boolean HasFlag = false;
            int T = (int)Title;
            HasFlag = Halos.ContainsKey(T);
            Halos[T] = EndsOn;
            AddFlag3(Title);
            if (Db)
            {
                if (HasFlag)
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
                    cmd.Update("status").Set("time", Kernel.ToDateTimeInt(EndsOn))
                        .Where("status", Title).And("entityid", (UInt32)UID);
                    cmd.Execute();
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
                    cmd.Insert("status")
                        .Insert("entityid", (UInt32)UID)
                        .Insert("status", Title)
                        .Insert("time", Kernel.ToDateTimeInt(EndsOn));
                    cmd.Execute();
                }
            }
        }
        public void AddTopStatus(UInt64 Title, byte flagtype, DateTime EndsOn, Boolean Db = true)
        {
            Boolean HasFlag = false;
            if (IsWarTop(Title))
            {
                HasFlag = Titles.ContainsKey((TitlePacket.Titles)Title);
                Titles.TryAdd((TitlePacket.Titles)Title, EndsOn);
            }
            else
            {
                switch (flagtype)
                {
                    case 1:
                        HasFlag = ContainsFlag(Title);
                        AddFlag(Title);
                        break;
                    case 2:
                        HasFlag = ContainsFlag2(Title);
                        AddFlag2(Title);
                        break;
                    case 3:
                        HasFlag = ContainsFlag3(Title);
                        AddFlag3(Title);
                        break;
                }
            }
            if (Db)
            {
                if (HasFlag)
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
                    cmd.Update("status").Set("time", Kernel.ToDateTimeInt(EndsOn))
                        .Where("status", Title).And("flagtype", flagtype).And("entityid", (UInt32)UID);
                    cmd.Execute();
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
                    cmd.Insert("status").Insert("entityid", (UInt32)UID).Insert("status", Title).Insert("flagtype", flagtype).Insert("time", Kernel.ToDateTimeInt(EndsOn));
                    cmd.Execute();
                }
            }
        }
        public void AddTopStatus(UInt64 Title, DateTime EndsOn, Boolean Db = true)
        {
            Title = TopStatusToInt(Title);
            Boolean HasFlag = false;
            if (IsWarTop(Title))
            {
                HasFlag = Titles.ContainsKey((TitlePacket.Titles)Title);
                Titles.TryAdd((TitlePacket.Titles)Title, EndsOn);
            }
            else
            {
                int T = (int)Title;
                HasFlag = Halos.ContainsKey(T);
                Halos[T] = EndsOn;
                if (Title == 7 || Title == 8
                    || Title == 22 || Title == 41) AddFlag2(IntToTopStatus(Title));
                else AddFlag(IntToTopStatus(Title));
            }
            if (Db)
            {
                if (HasFlag)
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
                    cmd.Update("status").Set("time", Kernel.ToDateTimeInt(EndsOn))
                        .Where("status", Title).And("entityid", (UInt32)UID);
                    cmd.Execute();
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
                    cmd.Insert("status")
                        .Insert("entityid", (UInt32)UID)
                        .Insert("status", Title)
                        .Insert("time", Kernel.ToDateTimeInt(EndsOn));
                    cmd.Execute();
                }
            }
        }
        public void RemoveTopStatus(UInt64 Title)
        {
            ulong baseFlag = TopStatusToInt(Title);
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.DELETE);
            cmd.Delete("status", "entityid", UID).And("status", baseFlag).Execute();
            if (baseFlag == 31 || baseFlag == 32 || baseFlag == 33 || baseFlag == 34)
                RemoveFlag3((Title));
            else if (baseFlag == 7 || baseFlag == 22)
                RemoveFlag2((Title));
            else if (Title == (ulong)1UL << 166 || Title == (ulong)1UL << 167 || Title == (ulong)1UL << 174 || Title == (ulong)1UL << 175)
                RemoveFlag3((ulong)Title);
            else
                RemoveFlag(Title);
        }
        public void LoadTopStatus()
        {
            using (MySqlCommand Command = new MySqlCommand(MySqlCommandType.SELECT))
            {
                Command.Select("status").Where("entityid", UID).Execute();
                using (MySqlReader Reader = new MySqlReader(Command))
                {
                    while (Reader.Read())
                    {
                        UInt64 Title = Reader.ReadUInt64("status");
                        DateTime Time = Kernel.FromDateTimeInt(Reader.ReadUInt64("time"));
                        if (DateTime.Now > Time)
                            RemoveTopStatus(Title);
                        else
                        {
                            //if (!ContainsFlag(IntToTopStatus(Title)))

                            if (Title == (ulong)1UL << 167 || Title == (ulong)1UL << 166 || Title == (ulong)1UL << 174 || Title == (ulong)1UL << 175)
                            {
                                AddTopStatus3((ulong)Title, Time, false);
                            }
                            else
                            {
                                AddTopStatus(Title, Time, false);
                            }
                        }
                    }
                }
            }
        }
        public uint CUID
        {
            get
            {
                if (SpawnPacket != null)
                    return BitConverter.ToUInt32(SpawnPacket, _CUID);
                else
                    return _uid;
            }
            set
            {
                _uid = value;
                Write(value, _CUID, SpawnPacket);
            }
        }
        public TitlePacket.Titles MyTitle
        {
            get { return (TitlePacket.Titles)SpawnPacket[_Title]; }
            set
            {
                SpawnPacket[_Title] = (Byte)value;
                if (FullyLoaded)
                {
                    MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
                    cmd.Update("entities").Set("My_Title", (Byte)value).Where("uid", UID).Execute();
                }
            }
        }
        public short KOSpellTime
        {
            get
            {
                if (KOSpell == 1110)
                {
                    if (ContainsFlag(Network.GamePackets.Update.Flags.Cyclone))
                    {
                        return CycloneTime;
                    }
                }
                else if (KOSpell == 1025)
                {
                    if (ContainsFlag(Network.GamePackets.Update.Flags.Superman))
                    {
                        return SupermanTime;
                    }
                }
                return 0;
            }
            set
            {
                if (KOSpell == 1110)
                {
                    if (ContainsFlag(Network.GamePackets.Update.Flags.Cyclone))
                    {
                        int Seconds = CycloneStamp.AddSeconds(value).AllSeconds() - Time32.Now.AllSeconds();
                        if (Seconds >= 20)
                        {
                            CycloneTime = 20;
                            CycloneStamp = Time32.Now;
                        }
                        else
                        {
                            CycloneTime = (short)Seconds;
                            CycloneStamp = Time32.Now;
                        }
                    }
                }
                if (KOSpell == 1025)
                {
                    if (ContainsFlag(Network.GamePackets.Update.Flags.Superman))
                    {
                        int Seconds = SupermanStamp.AddSeconds(value).AllSeconds() - Time32.Now.AllSeconds();
                        if (Seconds >= 20)
                        {
                            SupermanTime = 20;
                            SupermanStamp = Time32.Now;
                        }
                        else
                        {
                            SupermanTime = (short)Seconds;
                            SupermanStamp = Time32.Now;
                        }
                    }
                }
            }
        }
        public bool DragonWarrior()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null)
                    if (weapons.Item1.ID / 1000 == 617)
                        return true;
                    else if (weapons.Item2 != null)
                        if (weapons.Item2.ID / 1000 == 617)
                            return true;
            }
            return false;
        }
        public bool Transformed
        {
            get
            {
                return TransformationID != 98 && TransformationID != 99 && TransformationID != 0;
            }
        }
        public byte FirstRebornClass
        {
            get
            {
                return cls;
            }
            set
            {
                cls = value;
                SpawnPacket[_FirstRebornClass] = value;
                Update(Network.GamePackets.Update.FirsRebornClass, value, false);
            }
        }
        public byte SecondRebornClass
        {
            get
            {
                return secls;
            }
            set
            {
                secls = value;
                SpawnPacket[_SecondRebornClass] = value;
                Update(Network.GamePackets.Update.SecondRebornClass, value, false);
            }
        }
        public byte ThirdRebornClass
        {
            get
            {
                return seclss;
            }
            set
            {
                seclss = value;
                SpawnPacket[_Class] = value;
            }
        }
        public Time32 LastTimeUseSlide = Time32.Now;
        public uint MaxVigor
        {
            get
            {
                uint MaxVigor2 = 0;
                if (!Owner.Equipment.Free(12))
                {
                    ConquerItem dbi = Owner.Equipment.TryGetItem(12);
                    if (dbi.Plus == 1)
                        MaxVigor2 += 50;
                    if (dbi.Plus == 2)
                        MaxVigor2 += 120;
                    if (dbi.Plus == 3)
                        MaxVigor2 += 200;
                    if (dbi.Plus == 4)
                        MaxVigor2 += 350;
                    if (dbi.Plus == 5)
                        MaxVigor2 += 650;
                    if (dbi.Plus == 6)
                        MaxVigor2 += 1000;
                    if (dbi.Plus == 7)
                        MaxVigor2 += 1400;
                    if (dbi.Plus == 8)
                        MaxVigor2 += 2000;
                    if (dbi.Plus == 9)
                        MaxVigor2 += 2800;
                    if (dbi.Plus == 10)
                        MaxVigor2 += 3100;
                    if (dbi.Plus == 11)
                        MaxVigor2 += 3500;
                    if (dbi.Plus == 12)
                        MaxVigor2 += 4000;
                    MaxVigor2 += 30;
                    if (!Owner.Equipment.Free(ConquerItem.SteedCrop))
                    {
                        if (Owner.Equipment.Objects[18] != null)
                        {
                            if (Owner.Equipment.Objects[18].ID % 10 == 9)
                            {
                                MaxVigor2 += 1000;
                            }
                            else if (Owner.Equipment.Objects[18].ID % 10 == 8)
                            {
                                MaxVigor2 += 700;
                            }
                            else if (Owner.Equipment.Objects[18].ID % 10 == 7)
                            {
                                MaxVigor2 += 500;
                            }
                            else if (Owner.Equipment.Objects[18].ID % 10 == 6)
                            {
                                MaxVigor2 += 300;
                            }
                            else if (Owner.Equipment.Objects[18].ID % 10 == 5)
                            {
                                MaxVigor2 += 100;
                            }
                        }
                    }
                }
                return MaxVigor2;
            }
        }
        public bool IsFire()
        {
            if (this.EntityFlag == Game.EntityFlag.Player)
            {
                if (this.Class >= 140 && this.Class <= 145)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool IsWater()
        {
            if (this.EntityFlag == Game.EntityFlag.Player)
            {
                if (this.Class >= 130 && this.Class <= 135)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool EpicTaoist()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null && weapons.Item2 != null)
                    if (weapons.Item1.ID / 1000 == 620 && weapons.Item2.ID / 1000 == 619)//Backsword,Hossu
                        return true;

            }
            return false;
        }
        public int Energy
        {
            get { return _Energy; }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    if (Class >= 100 && Class <= 135 && EpicTaoist())
                    {
                        RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.FlameLotus);
                        if (value > 220)
                            value = 220;
                        Update upgrade = new Update(true);
                        upgrade.UID = UID;
                        upgrade.Append(78, 172, 5, (uint)value, 0);
                        this.Owner.SendScreen(upgrade.Encode(), true);
                        if (value >= 200)
                        {
                            if (!ContainsFlag3((ulong)Network.GamePackets.Update.Flags3.AuroraLotus))
                            {
                                AddFlag3((ulong)Network.GamePackets.Update.Flags3.AuroraLotus);
                            }
                        }
                    }
                    else if (Class >= 140 && Class <= 145 && EpicTaoist())
                    {
                        RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.AuroraLotus);
                        if (value > 330)
                            value = 330;
                        Update upgrade = new Update(true);
                        upgrade.UID = UID;
                        upgrade.Append(78, 173, 5, (uint)value, 0);
                        this.Owner.SendScreen(upgrade.Encode(), true);
                        if (value >= 300)
                        {
                            if (!ContainsFlag3((ulong)Network.GamePackets.Update.Flags3.FlameLotus))
                            {
                                AddFlag3((ulong)Network.GamePackets.Update.Flags3.FlameLotus);
                            }
                        }
                    }
                    else
                    {
                        RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.AuroraLotus);
                        RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.FlameLotus);
                    }
                }
                _Energy = value;
            }

        }
      /*  public UInt16 BattlePowerCalc(Entity e)
        {
            UInt16 BP = (ushort)(e.Level + ExtraBattlePower);

            if (e == null) return 0;
            if (e.Owner == null) return 0;
            var weapons = e.Owner.Weapons;
            foreach (ConquerItem i in e.Owner.Equipment.Objects)
            {
                if (i == null) continue;
                int pos = i.Position; if (pos > 20) pos -= 20;
                if (pos != ConquerItem.Bottle &&
                    pos != ConquerItem.Garment && pos != ConquerItem.RightWeaponAccessory && pos != ConquerItem.LeftWeaponAccessory && pos != ConquerItem.SteedArmor)
                {
                    if (!i.IsWorn) continue;
                    if (pos == ConquerItem.RightWeapon || pos == ConquerItem.LeftWeapon)
                        continue;
                    BP += ItemBatlePower(i);
                }
            }
            if (weapons.Item1 != null)
            {
                var i = weapons.Item1;
                Byte Multiplier = 1;
                if (i.IsTwoHander())
                    Multiplier = weapons.Item2 == null ? (Byte)2 : (Byte)1;
                BP += (ushort)(ItemBatlePower(i) * Multiplier);
            }
            if (weapons.Item2 != null)
                BP += ItemBatlePower(weapons.Item2);
            if (EntityFlag == Game.EntityFlag.Player)
            {
               BP += (Byte)e.NobilityRank;
            }
               BP += (Byte)(e.Reborn * 5);
               BP += (Byte)GuildBattlePower;
            

            return BP;
        }*/
        public ushort BattlePowerCalc(Entity e)
        {
            UInt16 BP = (ushort)(e.Level + ExtraBattlePower);

            if (e == null) return 0;
            if (e.Owner == null) return 0;
            var weapons = e.Owner.Weapons;
            foreach (ConquerItem i in e.Owner.Equipment.Objects)
            {
                if (i == null) continue;
                int pos = i.Position;
                if (pos > 20) pos -= 20;
                if (pos != ConquerItem.Bottle &&
                    pos != ConquerItem.Garment && pos != ConquerItem.RightWeaponAccessory &&
                    pos != ConquerItem.LeftWeaponAccessory && pos != ConquerItem.SteedArmor)
                {
                    if (!i.IsWorn) continue;
                    if (pos == ConquerItem.RightWeapon || pos == ConquerItem.LeftWeapon)
                        continue;
                    BP += ItemBatlePower(i);
                }
            }
            if (weapons.Item1 != null)
            {
                var i = weapons.Item1;
                Byte Multiplier = 1;
                if (i.IsTwoHander())
                    Multiplier = weapons.Item2 == null ? (Byte)2 : (Byte)1;
                BP += (ushort)(ItemBatlePower(i) * Multiplier);
            }
            if (weapons.Item2 != null)
            {
                BP += ItemBatlePower(weapons.Item2);
            }
            if (EntityFlag == Game.EntityFlag.Player)
            {
                BP += (Byte)e.NobilityRank;
            }
            BP += (Byte)(e.Reborn * 5);
            BP += (Byte)GuildBattlePower;
            BP += (ushort)ExtraBattlePower;
            //  EquipmentColor = BP;
            return BP;
        }
        private ushort ItemBatlePower(ConquerItem i)
        {
            Byte Multiplier = 1;
            Byte quality = (Byte)(i.ID % 10);
            int BP = 0;
            if (quality >= 6)
            {
                BP += (Byte)((quality - 5) * Multiplier);
            }
            if (i.SocketOne != 0)
            {
                BP += (Byte)(1 * Multiplier);
                if ((Byte)i.SocketOne % 10 == 3)
                    BP += (Byte)(1 * Multiplier);
                if (i.SocketTwo != 0)
                {
                    BP += (Byte)(1 * Multiplier);
                    if ((Byte)i.SocketTwo % 10 == 3)
                        BP += (Byte)(1 * Multiplier);
                }
            }
            BP += (Byte)(i.Plus * Multiplier);
            return (ushort)BP;
        }
        public Entity(EntityFlag Flag, bool companion)
        {
            Companion = companion;
            this.EntityFlag = Flag;
            Mode = Enums.Mode.None;
            update = new Nyx.Server.Network.GamePackets.Update(true);
            update.UID = UID;
            switch (Flag)
            {
                case EntityFlag.Player:
                    MapObjType = Game.MapObjectType.Player;
                    Halos = new ConcurrentDictionary<int, DateTime>();
                    break;
                case EntityFlag.Monster: MapObjType = Game.MapObjectType.Monster; break;
            }
            SpawnPacket = new byte[0];
        }
        public void Ressurect()
        {
            if (EntityFlag == EntityFlag.Player)
                Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });
        }
        public void RemoveFlags()
        {
            RemoveFlag(Network.GamePackets.Update.Flags.Fly);
            RemoveFlag(Network.GamePackets.Update.Flags.Ride);
            RemoveFlag(Network.GamePackets.Update.Flags.Cyclone);
            RemoveFlag(Network.GamePackets.Update.Flags.Superman);
            RemoveFlag2(Network.GamePackets.Update.Flags2.CannonBarrage);
            RemoveFlag(Network.GamePackets.Update.Flags.XPList);
            RemoveFlag3((uint)(Network.GamePackets.Update.Flags3.SuperCyclone));
            RemoveFlag3(Network.GamePackets.Update.Flags3.BladeFlurry);
            RemoveFlag(Network.GamePackets.Update.Flags.FatalStrike);
            RemoveFlag(Network.GamePackets.Update.Flags.FlashingName);
            RemoveFlag(Network.GamePackets.Update.Flags.ShurikenVortex);
            RemoveFlag2(Network.GamePackets.Update.Flags2.Oblivion);
            RemoveFlag2(Network.GamePackets.Update.Flags2.AzureShield);
            RemoveFlag2(Network.GamePackets.Update.Flags2.CarryingFlag);
            RemoveFlag(Network.GamePackets.Update.Flags.CastPray);
            RemoveFlag(Network.GamePackets.Update.Flags.Praying);
            RemoveFlag2(Network.GamePackets.Update.Flags2.SoulShackle);
            RemoveFlag3((uint)Network.GamePackets.Update.Flags3.DragonCyclone);
            ToxicFogLeft = 0;
            RemoveFlag2(Network.GamePackets.Update.Flags2.SoulShackle);
            NoDrugsTime = 0;
            RemoveFlag3(Network.GamePackets.Update.Flags3.GoldBrickNormal);
            RemoveFlag3(Network.GamePackets.Update.Flags3.GoldBrickRefined);
            RemoveFlag3(Network.GamePackets.Update.Flags3.GoldBrickUnique);
            RemoveFlag3(Network.GamePackets.Update.Flags3.GoldBrickElite);
            RemoveFlag3(Network.GamePackets.Update.Flags3.GoldBrickSuper);
            RemoveFlag(Network.GamePackets.Update.Flags.Stigma);
            RemoveFlag(Network.GamePackets.Update.Flags.MagicShield);
            RemoveFlag3(Network.GamePackets.Update.Flags3.WarriorEpicShield);
            RemoveFlag(Network.GamePackets.Update.Flags2.FendAura);
            RemoveFlag(Network.GamePackets.Update.Flags2.TyrantAura);
            RemoveFlag(Network.GamePackets.Update.Flags3.KineticSpark);
            RemoveFlag(Network.GamePackets.Update.Flags2.EarthAura);
            RemoveFlag(Network.GamePackets.Update.Flags2.EarthAura2);
            RemoveFlag(Network.GamePackets.Update.Flags2.FendAura2);
            RemoveFlag(Network.GamePackets.Update.Flags2.FireAura);
            RemoveFlag(Network.GamePackets.Update.Flags2.FireAura2);
            RemoveFlag(Network.GamePackets.Update.Flags2.MetalAura);
            RemoveFlag(Network.GamePackets.Update.Flags2.MetalAura2);
            RemoveFlag(Network.GamePackets.Update.Flags2.TyrantAura2);
            RemoveFlag(Network.GamePackets.Update.Flags2.WaterAura);
            RemoveFlag(Network.GamePackets.Update.Flags2.WaterAura2);
            RemoveFlag(Network.GamePackets.Update.Flags2.WoodAura);
            RemoveFlag(Network.GamePackets.Update.Flags2.WoodAura2);
            RemoveFlag2(Network.GamePackets.Update.Flags2.ShieldBlock);
            RemoveFlag3(Network.GamePackets.Update.Flags3.DivineGuard);
        }
        public void BringToLife()
        {
            Hitpoints = MaxHitpoints;
            TransformationID = 0;
            Stamina = 100;
            FlashingNameTime = 0;
            FlashingNameStamp = DateTime.Now;
            RemoveFlag(Network.GamePackets.Update.Flags.FlashingName);
            RemoveFlag(Network.GamePackets.Update.Flags.Dead | Network.GamePackets.Update.Flags.Ghost);
            if (EntityFlag == EntityFlag.Player)
                Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status });
            if (EntityFlag == Game.EntityFlag.Player)
            {
                Owner.Entity.AutoRev = 0;
                Owner.ReviveStamp = Time32.Now;
                Owner.Attackable = false;
            }
        }
        public void DropRandomStuff(Entity KillerName)
        {
            if (Owner.Inventory.Count > 0)
            {
                var array = Owner.Inventory.Objects.ToArray();
                uint count = (uint)(array.Length / 4);
                byte startfrom = (byte)Kernel.Random.Next((int)count);
                for (int c = 0; c < count; c++)
                {
                    int index = c + startfrom;
                    if (array[index] != null)
                    {
                        if (PKPoints > 99)
                        {
                            if (array[index].Lock == 0)
                            {
                                if (array[index].UnlockEnd > DateTime.Now.AddDays(1))
                                {
                                    if (!array[index].Bound && !array[index].Inscribed && array[index].ID != 723753)
                                    {
                                        if (!array[index].Suspicious && array[index].ID != 3000550)
                                        {
                                            var Item = array[index];
                                            if (Item.ID >= 729960 && Item.ID <= 729970) return;
                                            Item.Lock = 0;
                                            var infos = Database.ConquerItemInformation.BaseInformations[(uint)Item.ID];
                                            ushort x = X, y = Y;
                                            Game.Map Map = Kernel.Maps[MapID];
                                            if (Map.SelectCoordonates(ref x, ref y))
                                            {
                                                FloorItem floorItem = new FloorItem(true);
                                                Owner.Inventory.Remove(Item, Enums.ItemUse.Remove);
                                                floorItem.Item = Item;
                                                floorItem.ValueType = FloorItem.FloorValueType.Item;
                                                floorItem.ItemID = (uint)Item.ID;
                                                floorItem.MapID = MapID;
                                                floorItem.MapObjType = MapObjectType.Item;
                                                floorItem.X = x;
                                                floorItem.Y = y;
                                                floorItem.Type = FloorItem.Drop;
                                                floorItem.OnFloor = Time32.Now;
                                                floorItem.ItemColor = floorItem.Item.Color;
                                                floorItem.UID = FloorItem.FloorUID.Next;
                                                while (Map.Npcs.ContainsKey(floorItem.UID))
                                                    floorItem.UID = FloorItem.FloorUID.Next;
                                                Map.AddFloorItem(floorItem);
                                                Owner.SendScreenSpawn(floorItem, true);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (PKPoints >= 30 && Killer != null && Killer.Owner != null)
            {
                foreach (var Item in Owner.Equipment.Objects)
                {
                    if (Item != null)
                    {
                        if (Item.Position == 9 || Item.Position == 12 || Item.Position == 17) return;
                        if (Item.Position == 5)
                            if (Item.ID.ToString().StartsWith("105")) return;
                        if (Kernel.Rate(35 + (int)(PKPoints > 30 ? 75 : 0)))
                        {
                            ushort x = X, y = Y;
                            Game.Map Map = Kernel.Maps[MapID];
                            if (Map.SelectCoordonates(ref x, ref y))
                            {
                                Owner.Equipment.RemoveToGround(Item.Position);
                                var infos = Database.ConquerItemInformation.BaseInformations[(uint)Item.ID];
                                FloorItem floorItem = new FloorItem(true);
                                floorItem.Item = Item;
                                floorItem.ValueType = FloorItem.FloorValueType.Item;
                                floorItem.ItemID = (uint)Item.ID;
                                floorItem.MapID = MapID;
                                floorItem.MapObjType = MapObjectType.Item;
                                floorItem.X = x;
                                floorItem.Y = y;
                                floorItem.Type = FloorItem.DropDetain;
                                floorItem.OnFloor = Time32.Now;
                                floorItem.ItemColor = floorItem.Item.Color;
                                floorItem.UID = FloorItem.FloorUID.Next;
                                while (Map.Npcs.ContainsKey(floorItem.UID))
                                    floorItem.UID = FloorItem.FloorUID.Next;
                                Owner.SendScreenSpawn(floorItem, true);
                                Database.DetainedItemTable.DetainItem(Item, Owner, Killer.Owner);
                                Owner.Equipment.UpdateEntityPacket();
                                ClientEquip eq = new ClientEquip(Owner);
                                eq.DoEquips(Owner);
                                Owner.Send(eq);
                                break;
                            }
                        }
                    }
                }
            }
            if (PKPoints > 99)
            {
                if (KillerName.EntityFlag == EntityFlag.Player)
                {
                    foreach (var client in Kernel.GamePool.Values)
                    client.Send(new Network.GamePackets.Message(Name + " has been captured by " + KillerName.Name + " and sent in jail! The world is now safer!", Message.System));
                    Teleport(6000, 32, 73);
                }
                else
                {
                    foreach (var client in Kernel.GamePool.Values)
                    client.Send(new Network.GamePackets.Message(Name + " has been captured and sent in jail! The world is now safer!", Message.System));
                    Teleport(6000, 32, 73);
                }
            }
        }
        public void Die(UInt32 killer)
        {
            if (MyClones.Count != 0)
            {
                foreach (var clone in MyClones)
                    clone.RemoveThat();
                MyClones.Clear();
            }
            this.RemoveFlag2(18014398509481984uL);
            if (EntityFlag == EntityFlag.Player)
            {
                Owner.XPCount = 0;
                if (OnDeath != null) OnDeath(this);
                if (Owner.Booth != null)
                {
                    Owner.Booth.Remove();
                    Owner.Booth = null;
                }
            }
            Killed = true;
            Hitpoints = 0;
            DeathStamp = Time32.Now;
            ToxicFogLeft = 0;
            if (Companion)
            {
                AddFlag(Network.GamePackets.Update.Flags.Ghost | Network.GamePackets.Update.Flags.Dead | Network.GamePackets.Update.Flags.FadeAway);
                Network.GamePackets.Attack attack = new Network.GamePackets.Attack(true);
                attack.Attacked = UID;
                attack.AttackType = Network.GamePackets.Attack.Kill;
                attack.X = X;
                attack.Y = Y;
                MonsterInfo.SendScreen(attack);
                Owner.Map.RemoveEntity(this);

                Owner.Companion = null;
            }
            if (EntityFlag == EntityFlag.Player)
            {
                if (Constants.PKFreeMaps.Contains(MapID))
                    goto Over;
            Over:
                AddFlag(Network.GamePackets.Update.Flags.Ghost);
                AddFlag((ulong)Network.GamePackets.Update.Flags.Dead);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.Fly);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.Ride);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.Cyclone);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.Superman);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.FatalStrike);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.FlashingName);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.ShurikenVortex);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.CastPray);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.Praying);
            RemoveFlag2((ulong)Network.GamePackets.Update.Flags2.Oblivion);
            RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.SuperCyclone);
            RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.DragonCyclone);
            RemoveFlag2((ulong)Network.GamePackets.Update.Flags2.AzureShield);
            RemoveFlag((ulong)Network.GamePackets.Update.Flags.GodlyShield);
            RemoveFlag2((ulong)Network.GamePackets.Update.Flags2.CarryingFlag);
                Attack attack = new Attack(true);
                attack.AttackType = Network.GamePackets.Attack.Kill;
                attack.X = X;
                attack.Y = Y;
                attack.Attacked = UID;
                attack.Attacker = killer;
                attack.Damage = 0;
                Owner.SendScreen(attack, true);
                if (Body % 10 < 3)
                    TransformationID = 99;
                else
                    TransformationID = 98;

                Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });

                Owner.EndQualifier();
            }
            else
            {
                Kernel.Maps[MapID].Floor[X, Y, MapObjType, this] = true;
            }
            if (EntityFlag == EntityFlag.Player)
                if (OnDeath != null) OnDeath(this);
        }
        public Entity Killer;
        public void Die(Entity killer)
        {
            if (EntityFlag == EntityFlag.Player && killer.EntityFlag == EntityFlag.Player)
            {
                if (Owner.Spells.ContainsKey(12660) && Owner.Entity.EpicWarrior())
                {
                    XPCountTwist = Owner.XPCount;
                }
                if (ContainsFlag3((ulong)1UL << 53))
                    RemoveFlag3((ulong)1UL << 53);
                #region Perfection
                Game.Enums.PerfectionEffect effect = Enums.PerfectionEffect.StraightLife;
                byte chance = 0;
                new MsgRefineEffect().HandleStraightLife(this, ref chance);
                if (Kernel.Rate(chance))
                {
                    new MsgRefineEffect().SendEffect(this, killer, effect);
                }
                #endregion
                #region PlunderWar
                if ((DateTime.Now.DayOfWeek >= DayOfWeek.Wednesday && DateTime.Now.DayOfWeek <= DayOfWeek.Friday) && (DateTime.Now.Hour == 21) && (DateTime.Now.Minute <= 30))
                {
                    if (MapID == 3935 && killer.MapID == 3935)
                    {
                        killer.KingdomDeed += 3;
                        if (killer.Owner.Team != null)
                        {
                            foreach (var tm in killer.Owner.Team.Teammates)
                            {
                                tm.Entity.KingdomDeed += 3;
                            }
                        }
                    }
                }
                #endregion
                if (!Constants.PKFreeMaps.Contains(MapID))
                {
                    if (Level < 140 && Experience != 0)
                    {
                        ulong amount = 0;
                        if (VIPLevel < 4)
                        {
                            Experience -= Math.Min(Hitpoints * MaxHitpoints * Level, Experience);
                            amount = Math.Min(Hitpoints * MaxHitpoints * Level, Experience);
                            killer.Owner.IncreaseExperience(amount, false);
                        }
                        else
                        {
                            Experience -= Math.Min(Hitpoints * MaxHitpoints * Level, Experience) / 2;
                            amount = Math.Min(Hitpoints * MaxHitpoints * Level, Experience) / 2;
                            killer.Owner.IncreaseExperience(amount, false);
                        }
                        Owner.Send(new Message("You lost " + amount + " EXP!", System.Drawing.Color.Red, Message.System));
                    }
                }
                Owner.Send(new Message("You are dead.", System.Drawing.Color.Red, Message.System));

            }
            if (MyClones.Count != 0)
            {
                foreach (var clone in MyClones)
                    clone.RemoveThat();
                MyClones.Clear();
            }
            #region Dura
            if (this.MapID < 10000 || killer.MapID < 10000)
            {
                if (killer.EntityFlag == EntityFlag.Player && this.EntityFlag == EntityFlag.Player)//Player2Player!!!TheDeadPlayerWillLoseDura
                {
                    if (PKPoints > 29)
                    {
                        Game.Attacking.DurabilityItems.LessDura(this, 10);
                    }
                    else
                    {
                        Game.Attacking.DurabilityItems.LessDura(this, 1);
                    }
                }
                if (killer.EntityFlag == EntityFlag.Player && this.EntityFlag != EntityFlag.Player)//Player2Monster!!!TheKillerWillLoseDura
                {
                    if (Kernel.Rate(1))
                    {
                        Game.Attacking.DurabilityItems.LessDura(killer, 1);
                    }
                }
                if (killer.EntityFlag != EntityFlag.Player && this.EntityFlag == EntityFlag.Player)//Monster2Player!!!TheDeadPlayerWillLoseDura
                {
                    if (killer.Boss != 0)
                        Game.Attacking.DurabilityItems.LessDura(this, (byte)Kernel.Random.Next(0, 3));
                    else
                    {
                        if (Kernel.Rate(3))
                        {
                            Game.Attacking.DurabilityItems.LessDura(this, 1);
                        }
                    }
                }
            }
            #endregion
            killer.CountKilling++;
            #region CaptureTheFlag
            if (killer.GuildID != 0 && killer.MapID == CaptureTheFlag.MapID && CaptureTheFlag.IsWar)
            {
                if (GuildID != 0)
                {
                    if (killer.GuildID != GuildID) { CaptureTheFlag.AddScore(1, killer.Owner.Guild); CaptureTheFlag.AddExploits(1, killer.Owner.AsMember); }
                }
                if (ContainsFlag2((ulong)Network.GamePackets.Update.Flags2.CarryingFlag))
                {
                    RemoveFlag2((ulong)Network.GamePackets.Update.Flags2.CarryingFlag);
                    killer.AddFlag2((ulong)Network.GamePackets.Update.Flags2.CarryingFlag);
                }
                if (Program.World.CTF.Bases.ContainsKey(UID))
                {
                    var _base = Program.World.CTF.Bases[UID];
                    _base.Capture();
                }
            }
            #endregion
            if (killer.CountKilling >= 100 && killer.HeavenBlessing > 0 && killer.BlessedHunting < 4578)
            {
                killer.CountKilling = 0;
                killer.BlessedHunting += 1;
            }
            else if (killer.BlessedHunting >= 4578)
            {
                killer.Owner.MessageBox("You have reached the maximum amount of EXP available from bonus hunting. Would you like to claim it, now?",
                (p) =>
                {
                    killer.Owner.IncreaseExperience((ulong)(killer.BlessedHunting / 600.0 * killer.Owner.ExpBall), false);
                    killer.BlessedHunting = 0;
                    MentorPremio Premio = new MentorPremio();
                    Premio.Action = MentorPremio.Show;
                    Premio.OnlineTraining = killer.OnlineTraining;
                    Premio.BlessedHunting = killer.BlessedHunting;
                    killer.Owner.Send(Premio.Encode());
                });
            }
            if (InAutoHunt)
            {
                AutoHunt AutoHunt = new AutoHunt();
                AutoHunt.Action = AutoHunt.Mode.KilledBy;
                AutoHunt.Unknown = 3329;
                AutoHunt.KilledName = killer.Name;
                AutoHunt.EXPGained = AutoHuntEXP;
                Owner.Send(AutoHunt.Encode());
                Owner.IncreaseExperience(Owner.Entity.AutoHuntEXP, false);
                Owner.Entity.AutoHuntEXP = 0;
                AutoRevStamp = Time32.Now;
                AutoRev = 20;
                if (HeavenBlessing == 0)
                    Owner.Entity.RemoveFlag(Network.GamePackets.Update.Flags3.AutoHunting);
            }
            #region Die Guild System
            if (killer.EntityFlag == EntityFlag.Player && EntityFlag == EntityFlag.Player)
            {

                if (Owner.Guild != null && killer.Owner.Guild != null && Owner.Map.ID == 1015)
                {
                    Owner.Guild.pkp_donation += 2;
                    Owner.Guild.pkp_donation -= 2;
                    foreach (var client in Kernel.GamePool.Values)
                    {
                        client.Send(new Message("The " + killer.Owner.AsMember.Rank + " " + killer.Name + " of the Guild " + killer.Owner.Guild.Name + " has killed the " + killer.Owner.AsMember.Rank + " " + Name + " of the Guild " + Owner.Guild.Name + " at BirdIsland!", System.Drawing.Color.Yellow, Network.GamePackets.Message.Guild));
                    }
                }
                if (Owner.Guild != null && killer.Owner.Guild != null && Owner.Map.ID == 1020)
                {
                    Owner.Guild.pkp_donation += 2;
                    Owner.Guild.pkp_donation -= 2;
                    foreach (var client in Kernel.GamePool.Values)
                        client.Send(new Message("The " + killer.Owner.AsMember.Rank + " " + killer.Name + " of the Guild " + killer.Owner.Guild.Name + " has killed the " + killer.Owner.AsMember.Rank + " " + Name + " of the Guild " + Owner.Guild.Name + " at ApeCity!", System.Drawing.Color.Yellow, Network.GamePackets.Message.Guild));
                }
                if (Owner.Guild != null && killer.Owner.Guild != null && Owner.Map.ID == 1011)
                {
                    Owner.Guild.pkp_donation += 2;
                    Owner.Guild.pkp_donation -= 2;
                    foreach (var client in Kernel.GamePool.Values)
                        client.Send(new Message("The " + killer.Owner.AsMember.Rank + " " + killer.Name + " of the Guild " + killer.Owner.Guild.Name + " has killed the " + killer.Owner.AsMember.Rank + " " + Name + " of the Guild " + Owner.Guild.Name + " at PhoenixCastle!", System.Drawing.Color.Yellow, Network.GamePackets.Message.Guild));
                }
                if (Owner.Guild != null && killer.Owner.Guild != null && Owner.Map.ID == 1000)
                {
                    Owner.Guild.pkp_donation += 2;
                    Owner.Guild.pkp_donation -= 2;
                    foreach (var client in Kernel.GamePool.Values)
                        client.Send(new Message("The " + killer.Owner.AsMember.Rank + " " + killer.Name + " of the Guild " + killer.Owner.Guild.Name + " has killed the " + killer.Owner.AsMember.Rank + " " + Name + " of the Guild " + Owner.Guild.Name + " at DesertCity!", System.Drawing.Color.Yellow, Network.GamePackets.Message.Guild));
                }
            }
            #endregion
            RemoveFlags();
            if (EntityFlag == EntityFlag.Player)
            {
                Owner.XPCount = 0;
                if (OnDeath != null) OnDeath(this);
                if (Owner.Booth != null)
                {
                    Owner.Booth.Remove();
                    Owner.Booth = null;
                }
            }
            Killer = killer;
            Hitpoints = 0;
            DeathStamp = Time32.Now;
            ToxicFogLeft = 0;
            if (Companion)
            {
                AddFlag(Network.GamePackets.Update.Flags.Ghost | Network.GamePackets.Update.Flags.Dead | Network.GamePackets.Update.Flags.FadeAway);
                Network.GamePackets.Attack zattack = new Network.GamePackets.Attack(true);
                zattack.Attacked = UID;
                zattack.AttackType = Network.GamePackets.Attack.Kill;
                zattack.X = X;
                zattack.Y = Y;
                MonsterInfo.SendScreen(zattack);
                Owner.Map.RemoveEntity(this);
                Owner.Companion = null;
            }

            if (EntityFlag == EntityFlag.Player)
            {
                if (killer.EntityFlag == EntityFlag.Player)
                {
                    if (Constants.PKFreeMaps.Contains(killer.MapID))
                        goto Over;
                    if (killer.Owner.Map.BaseID == 700)
                        goto Over;

                    if (((killer.PKMode != Nyx.Server.Game.Enums.PKMode.Jiang) && (killer.PKMode != Nyx.Server.Game.Enums.PKMode.Guild) && (killer.PKMode != Nyx.Server.Game.Enums.PKMode.Revenge) && (!ContainsFlag(Network.GamePackets.Update.Flags.FlashingName) && !ContainsFlag(Network.GamePackets.Update.Flags.BlackName)) && !this.ContainsFlag(1L)) && !this.ContainsFlag(0x8000L))
                    {
                        killer.AddFlag(Network.GamePackets.Update.Flags.FlashingName);
                        killer.FlashingNameStamp = DateTime.Now;
                        killer.FlashingNameTime = 60;
                        if (killer.GuildID != 0)
                        {
                            if (killer.Owner.Guild.Enemy.ContainsKey(GuildID))
                            {
                                killer.PKPoints += 3;
                                killer.Cursed = Time32.Now;
                                Update update = new Update(true) { UID = killer.UID };
                                update.Append(Network.GamePackets.Update.CursedTimer, UID);
                                killer.AddFlag(Network.GamePackets.Update.Flags.Cursed);
                                killer.Owner.Send(update.Encode());
                            }
                            else
                            {
                                if (!killer.Owner.Enemy.ContainsKey(UID))
                                    killer.PKPoints += 10;
                                else
                                    killer.PKPoints += 5;
                                killer.PKPoints += 3;
                                killer.Cursed = Time32.Now;
                                Update update = new Update(true) { UID = killer.UID };
                                update.Append(Network.GamePackets.Update.CursedTimer, UID);
                                killer.AddFlag(Network.GamePackets.Update.Flags.Cursed);
                                killer.Owner.Send(update.Encode());
                            }
                        }
                        else
                        {
                            if (!killer.Owner.Enemy.ContainsKey(UID))
                            {
                                killer.PKPoints += 10;
                            }
                            else
                            {
                                killer.PKPoints += 5;
                            }
                            killer.Cursed = Time32.Now;
                            Update update = new Update(true) { UID = killer.UID };
                            update.Append(Network.GamePackets.Update.CursedTimer, UID);
                            killer.AddFlag(Network.GamePackets.Update.Flags.Cursed);
                            killer.Owner.Send(update.Encode());
                        }

                        if (killer.EntityFlag == Game.EntityFlag.Player)
                        {
                            if (EntityFlag == Game.EntityFlag.Player)
                            {
                                Game.PkExpeliate pk = new Game.PkExpeliate();
                                if (!killer.PkExplorerValues.ContainsKey(UID))
                                {
                                    pk.UID = killer.UID;
                                    pk.killedUID = UID;
                                    pk.Name = Name;
                                    pk.KilledAt = Map.GetMapName(MapID);
                                    pk.LostExp = 0;
                                    pk.Times = 1;
                                    pk.Potency = (uint)BattlePower;
                                    pk.Level = Level;
                                    PkExpelTable.PkExploitAdd(killer.Owner, pk);
                                }
                                else
                                {
                                    pk.UID = killer.UID;
                                    pk.killedUID = UID;
                                    pk.Name = Name;
                                    pk.KilledAt = Map.GetMapName(MapID);
                                    pk.LostExp = 0;
                                    killer.PkExplorerValues[UID].Times += 1;
                                    pk.Times = killer.PkExplorerValues[UID].Times;
                                    pk.Potency = (uint)BattlePower;
                                    pk.Level = Level;
                                    PkExpelTable.Update(killer.Owner, pk);
                                }
                            }
                        }
                        Network.PacketHandler.AddEnemy(this.Owner, killer.Owner);
                    }
                    if (killer.PKMode != Nyx.Server.Game.Enums.PKMode.Jiang)
                    {
                        if (killer.EntityFlag == Nyx.Server.Game.EntityFlag.Player)
                        {
                            if (Kernel.Rate(50))
                            {
                                DropRandomStuff(Killer);
                            }
                        }
                    }
                }
            }
        Over:

            Network.GamePackets.Attack attack = new Attack(true);
            attack.Attacker = killer.UID;
            attack.Attacked = UID;
            attack.AttackType = Network.GamePackets.Attack.Kill;
            attack.X = X;
            attack.Y = Y;

            if (EntityFlag == EntityFlag.Player)
            {
                AddFlag(Network.GamePackets.Update.Flags.Ghost);
                AddFlag(Network.GamePackets.Update.Flags.Dead);
                RemoveFlags();
                SpiritFocus = false;
                #region Aura
                if (Aura_isActive)
                {
                    RemoveFlag2(Aura_actType);
                    Owner.removeAuraBonuses(Aura_actType, Aura_actPower, 1);
                    Aura_isActive = false;
                    AuraTime = 0;
                    Aura_actType = 0;
                    Aura_actPower = 0;
                }
                #endregion
                if (Body % 10 < 3)
                    TransformationID = 99;
                else
                    TransformationID = 98;
                Owner.SendScreen(attack, true);
                Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });

                Owner.EndQualifier();
            }
            else
            {

                if (!Companion && !IsDropped)
                    MonsterInfo.Drop(killer);
                Kernel.Maps[MapID].Floor[X, Y, MapObjType, this] = true;
                if (killer.EntityFlag == EntityFlag.Player)
                {
                    #region Team
                    if (killer.Owner.Team != null)
                    {
                        foreach (Client.GameClient teammate in killer.Owner.Team.Teammates)
                        {
                            if (Kernel.GetDistance(killer.X, killer.Y, teammate.Entity.X, teammate.Entity.Y) <= Constants.pScreenDistance)
                            {
                                if (killer.UID != teammate.Entity.UID)
                                {
                                    uint extraExperience = MaxHitpoints / 2;
                                    if (killer.Spouse == teammate.Entity.Name)
                                        extraExperience = MaxHitpoints * 2;
                                    byte TLevelN = teammate.Entity.Level;
                                    if (killer.Owner.Team.CanGetNoobExperience(teammate))
                                    {
                                        if (teammate.Entity.Level < 137)
                                        {
                                            extraExperience *= 2;
                                            teammate.IncreaseExperience(extraExperience, false);
                                            teammate.Send(Constants.NoobTeamExperience(extraExperience));
                                        }
                                    }
                                    else
                                    {
                                        if (teammate.Entity.Level < 137)
                                        {
                                            teammate.IncreaseExperience(extraExperience, false);
                                            teammate.Send(Constants.TeamExperience(extraExperience));
                                        }
                                    }
                                    byte TLevelNn = teammate.Entity.Level;
                                    byte newLevel = (byte)(TLevelNn - TLevelN);
                                    if (newLevel != 0)
                                    {
                                        if (TLevelN < 70)
                                        {
                                            for (int i = TLevelN; i < TLevelNn; i++)
                                            {
                                                teammate.Team.Teammates[0].VirtuePoints += (uint)(i * 3.83F);
                                                teammate.Team.SendMessage(new Message("The leader, " + teammate.Team.Teammates[0].Entity.Name + ", has gained " + (uint)(i * 7.7F) + " virtue points for power leveling the rookies.", System.Drawing.Color.Red, Message.Team));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    killer.Owner.XPCount++;
                    if (killer.OnKOSpell())
                        killer.KOSpellTime++;
                }
            }
            if (EntityFlag == EntityFlag.Player)
                if (OnDeath != null) OnDeath(this);

        }
        public void RemoveMagicDefender()
        {
            if (MagicDefenderOwner && HasMagicDefender)
            {
                if (Owner.Team != null && HasMagicDefender && MagicDefenderOwner)
                {
                    foreach (var mate in Owner.Team.Teammates)
                    {
                        mate.Entity.HasMagicDefender = false;
                        mate.Entity.MagicDefenderSecs = 0;
                        mate.Entity.RemoveFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.MagicDefender);
                        mate.Entity.Update(mate.Entity.StatusFlag, mate.Entity.StatusFlag2, mate.Entity.StatusFlag3, Nyx.Server.Network.GamePackets.Update.MagicDefenderIcone, 0x80, 0, 0, false);
                    }
                }
                MagicDefenderOwner = false;
            }
            RemoveFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.MagicDefender);
            Update(StatusFlag, StatusFlag2, StatusFlag3, Nyx.Server.Network.GamePackets.Update.MagicDefenderIcone, 0x80, 0, 0, false);
            HasMagicDefender = false;
        }
        public void Update(ulong val1, ulong val2, ulong val3, uint val4, uint val5, uint val6, uint val7, bool screen)
        {
            
            if (this.Owner == null)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(val1, val2, val3, val4, val5, val6, val7);

            if (!screen)
                update.Send(Owner);
            else
                Owner.SendScreen(update, true);
        }
        public void Update(byte type, uint value, uint secondvalue)
        {
            Network.GamePackets.Update upd = new Update(true);
            upd.Append(type, value);
            upd.Append(type, secondvalue);
            upd.UID = UID;
            Owner.Send(upd);
        }
        public void Update(byte type, byte value, bool screen)
        {
            if (!SendUpdates)
                return;
            if (this.Owner == null)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(type, value, 0, 0, 0, 0, 0, 0, 0);
            if (!screen)
                update.Send(Owner);
            else
                Owner.SendScreen(update, true);
        }
        public void UpdateClan(byte value)
        {
            if (!SendUpdates)
                return;
            if (this.Owner == null)
                return;
            update = new Update(true);
            update.AppendClanBPIcon(Owner,value);
            update.Send(Owner);
        }
        public void Update(byte type, ushort value, bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(type, value);
            if (!screen)
                update.Send(Owner as Client.GameClient);
            else
                (Owner as Client.GameClient).SendScreen(update, true);
        }
        public void Update(byte type, double value, bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(type, value);
            if (!screen)
                update.Send(Owner as Client.GameClient);
            else
                (Owner as Client.GameClient).SendScreen(update, true);
        }
        public void Update(byte type, uint value, bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(type, value);
            if (!screen)
                update.Send(Owner as Client.GameClient);
            else
                (Owner as Client.GameClient).SendScreen(update, true);
        }
        public void Update(byte type, ulong value, bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(type, value);
            if (EntityFlag == EntityFlag.Player)
            {
                if (!screen)
                    update.Send(Owner as Client.GameClient);
                else
                    (Owner as Client.GameClient).SendScreen(update, true);
            }
            else
            {
                MonsterInfo.SendScreen(update);
            }
        }
        public void Update(byte type, ulong value, ulong value2, bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.PoPAppend(type, value, value2);
            if (!screen)
                update.Send(Owner as Client.GameClient);
            else
                (Owner as Client.GameClient).SendScreen(update, true);
        }
        public void UpdateEffects(bool screen)
        {

            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.AppendFull(0x19, StatusFlag, StatusFlag2, StatusFlag3);
            if (EntityFlag == EntityFlag.Player)
            {
                if (!screen)
                    update.Send(Owner as Client.GameClient);
                else
                    (Owner as Client.GameClient).SendScreen(update, true);
            }
            else
            {
                MonsterInfo.SendScreen(update);
            }
        }
        public void Update2(byte type, ulong value, bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append2(type, value);
            if (EntityFlag == EntityFlag.Player)
            {
                if (!screen)
                    update.Send(Owner as Client.GameClient);
                else
                    (Owner as Client.GameClient).SendScreen(update, true);
            }
            else
            {
                MonsterInfo.SendScreen(update);
            }
        }
        public void Update(byte type, string value, bool screen)
        {
            if (!SendUpdates)
                return;
            Network.GamePackets._String update = new _String(true);
            update.UID = this.UID;
            update.Type = type;
            update.TextsCount = 1;
            update.Texts.Add(value);
            if (EntityFlag == EntityFlag.Player)
            {
                if (!screen)
                    update.Send(Owner as Client.GameClient);
                else
                    (Owner as Client.GameClient).SendScreen(update, true);
            }
            else
            {
                MonsterInfo.SendScreen(update);
            }
        }
        public void UpdatePass(string column, long value)
        {
            if (EntityFlag == Game.EntityFlag.Player)
                if (FullyLoaded)
                    new Database.MySqlCommand(Nyx.Server.Database.MySqlCommandType.UPDATE).Update("entities").Set(column, value).Where("UID", UID).Execute();
        }
        public bool UnionActive
        {
            get { return (SpawnPacket[270] == 1); }
            set { Write(value ? ((byte)1) : ((byte)0), 270, SpawnPacket); }
        }
        public byte ServerID
        {
            get { return SpawnPacket[261]; }
            set { SpawnPacket[261] = value; }
        }
        public bool Move(Enums.ConquerAngle Direction, int teleport = 1)
        {
            ushort _X = X, _Y = Y;
            Facing = Direction;
            int dir = ((int)Direction) % XDir.Length;
            sbyte xi = XDir[dir], yi = YDir[dir];
            _X = (ushort)(X + xi);
            _Y = (ushort)(Y + yi);
            Game.Map Map = null;
            if (Kernel.Maps.TryGetValue(MapID, out Map))
            {
                var objType = MapObjType;
                if (Map.Floor[_X, _Y, objType])
                {
                    if (objType == MapObjectType.Monster)
                    {
                        Map.Floor[_X, _Y, MapObjType] = false;
                        Map.Floor[X, Y, MapObjType] = true;
                    }
                    X = _X;
                    Y = _Y;
                    return true;
                }
                else
                {
                    if (Mode == Enums.Mode.None)
                        if (EntityFlag != EntityFlag.Monster)
                            if (teleport == 1)
                                Teleport(MapID, X, Y);
                    return false;
                }
            }
            else
            {
                if (EntityFlag != EntityFlag.Monster)
                    Teleport(MapID, X, Y);
                else
                    return false;
            }
            return true;
        }
        public bool Move(Enums.ConquerAngle Direction, bool slide)
        {
            ushort _X = X, _Y = Y;
            if (!slide)
                return Move((Enums.ConquerAngle)((byte)Direction % 8));

            int dir = ((int)Direction) % XDir2.Length;
            Facing = Direction;
            sbyte xi = XDir2[dir], yi = YDir2[dir];
            _X = (ushort)(X + xi);
            _Y = (ushort)(Y + yi);

            Game.Map Map = null;

            if (Kernel.Maps.TryGetValue(MapID, out Map))
            {
                if (Map.Floor[_X, _Y, MapObjType])
                {
                    if (MapObjType == MapObjectType.Monster)
                    {
                        Map.Floor[_X, _Y, MapObjType] = false;
                        Map.Floor[X, Y, MapObjType] = true;
                    }
                    X = _X;
                    Y = _Y;
                    return true;
                }
                else
                {
                    if (Mode == Enums.Mode.None)
                    {
                        if (EntityFlag != EntityFlag.Monster)
                            Teleport(MapID, X, Y);
                        else
                            return false;
                    }
                }
            }
            else
            {
                if (EntityFlag != EntityFlag.Monster)
                    Teleport(MapID, X, Y);
                else
                    return false;
            }
            return true;
        }
        public void SendSpawn(Client.GameClient client)
        {
            SendSpawn(client, true);
        }
        public void SendSpawn(Client.GameClient client, bool checkScreen = true)
        {
            if (!this.Invisable)
            {
                if (client.Screen.Add(this) || !checkScreen)
                {
                    if (EntityFlag == Game.EntityFlag.Player)
                    {
                        if (client.InQualifier() && this.Owner.IsWatching() || (this.SkillTeamWatchingElitePKMatch != null || this.Owner.WatchingElitePKMatch != null) || this.Invisable)
                            return;
                        if (this.Owner.IsFairy)
                        {
                            FairySpawn FS = new FairySpawn(true);
                            FS.SType = this.Owner.SType;
                            FS.FairyType = this.Owner.FairyType;
                            FS.UID = this.UID;
                            client.Send(FS);
                        }
                        if (Owner.WatchingElitePKMatch != null)
                            return;
                        client.Send(SpawnPacket);
                    }
                    else
                        client.Send(SpawnPacket);
                    if (EntityFlag == EntityFlag.Player)
                    {
                        if (this.Owner.IsFairy)
                        {
                            FairySpawn FS = new FairySpawn(true);
                            FS.SType = this.Owner.SType;
                            FS.FairyType = this.Owner.FairyType;
                            FS.UID = this.UID;
                            client.Send(FS);
                        }
                        if (Owner.Booth != null)
                        {
                            client.Send(Owner.Booth);
                            if (Owner.Booth.HawkMessage != null)
                                client.Send(Owner.Booth.HawkMessage);
                        }
                    }
                }
            }
        }
      
public void Shift(ushort X, ushort Y)
{
if (EntityFlag == EntityFlag.Player)
{
if (!Database.MapsTable.MapInformations.ContainsKey(MapID))
return;
this.X = X;
this.Y = Y;
Network.GamePackets.Data Data = new Network.GamePackets.Data(true);
Data.UID = UID;
Data.ID = Network.GamePackets.Data.FlashStep;
Data.dwParam = MapID;
Data.wParam1 = X;
Data.wParam2 = Y;
Owner.SendScreen(Data, true);
Owner.Screen.Reload(null);
}
}
        public void nAddFlag(int flag)
        {
            BitVector32.Add(flag);

            for (byte x = 0; x < BitVector32.bits.Length; x++)
                Write(BitVector32.bits[x], (ushort)((byte)26 + x * 4), SpawnPacket);
            nUpdateEffects(true);
        }
        public bool nContainsFlag(int flag)
        {
            return BitVector32.Contain(flag);
        }
        public void nRemoveFlag(int flag)
        {
            if (nContainsFlag(flag))
            {
                BitVector32.Remove(flag);

                for (byte x = 0; x < BitVector32.bits.Length; x++)
                    Write(BitVector32.bits[x], (ushort)((byte)26 + x * 4), SpawnPacket);

                nUpdateEffects(true);
            }
        }
        public void nUpdateEffects(bool screen)
        {
            if (!SendUpdates)
                return;
            update = new Update(true);
            update.UID = UID;
            update.Append(Network.GamePackets.Update.StatusFlag, BitVector32.bits);
            if (EntityFlag == EntityFlag.Player)
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    if (!screen)
                        update.Send(Owner as Client.GameClient);
                    else
                        (Owner as Client.GameClient).SendScreen(update, true);
                }
                else
                {
                    MonsterInfo.SendScreen(update);
                }
            }
        }
        public void AddFlag(ulong flag)
        {
            StatusFlag |= flag;
        }
        public bool ContainsFlag(ulong flag)
        {
            ulong aux = StatusFlag;
            aux &= ~flag;
            return !(aux == StatusFlag);
        }
        public void RemoveFlag(ulong flag)
        {
            if (ContainsFlag(flag))
            {
                StatusFlag &= ~flag;
            }
        }
        public void AddFlag2(ulong flag)
        {
            if (flag == Network.GamePackets.Update.Flags2.SoulShackle) { StatusFlag2 |= flag; return; }
            if (!ContainsFlag(Network.GamePackets.Update.Flags.Dead) && !ContainsFlag(Network.GamePackets.Update.Flags.Ghost))
                StatusFlag2 |= flag;
        }
        public UInt64 TopStatusToInt(UInt64 top)
        {
            switch (top)
            {
                case Network.GamePackets.Update.Flags.TopWaterTaoist:
                    return 1;
                case Network.GamePackets.Update.Flags.TopWarrior:
                    return 2;
                case Network.GamePackets.Update.Flags.TopTrojan:
                    return 3;
                case Network.GamePackets.Update.Flags.TopArcher:
                    return 4;
                case Network.GamePackets.Update.Flags.TopNinja:
                    return 5;
                case Network.GamePackets.Update.Flags.TopFireTaoist:
                    return 6;
                case Network.GamePackets.Update.Flags2.TopMonk:
                    return 7;
                case Network.GamePackets.Update.Flags.TopSpouse:
                    return 8;
                case Network.GamePackets.Update.Flags.TopGuildLeader:
                    return 9;
                case Network.GamePackets.Update.Flags.TopDeputyLeader:
                    return 10;
                case Network.GamePackets.Update.Flags.MonthlyPKChampion:
                    return 20;
                case Network.GamePackets.Update.Flags.WeeklyPKChampion:
                    return 21;
                case Network.GamePackets.Update.Flags2.TopPirate:
                    return 22;
            }
                    
            return top;
        }
        public UInt64 IntToTopStatus(UInt64 top)
        {
            switch (top)
            {
                case 1:
                    return Network.GamePackets.Update.Flags.TopWaterTaoist;
                case 2:
                    return Network.GamePackets.Update.Flags.TopWarrior;
                case 3:
                    return Network.GamePackets.Update.Flags.TopTrojan;
                case 4:
                    return Network.GamePackets.Update.Flags.TopArcher;
                case 5:
                    return Network.GamePackets.Update.Flags.TopNinja;
                case 6:
                    return Network.GamePackets.Update.Flags.TopFireTaoist;
                case 7:
                    return Network.GamePackets.Update.Flags2.TopMonk;
                case 8:
                    return Network.GamePackets.Update.Flags.TopSpouse;
                case 9:
                    return Network.GamePackets.Update.Flags.TopGuildLeader;
                case 10:
                    return Network.GamePackets.Update.Flags.TopDeputyLeader;
                case 20:
                    return Network.GamePackets.Update.Flags.MonthlyPKChampion;
                case 21:
                    return Network.GamePackets.Update.Flags.WeeklyPKChampion;
                case 22:
                    return Network.GamePackets.Update.Flags2.TopPirate;
            }
            return top;
        }
        public bool ContainsFlag2(ulong flag)
        {
            ulong aux = StatusFlag2;
            aux &= ~flag;
            return !(aux == StatusFlag2);
        }
        public void RemoveFlag2(ulong flag)
        {
            if (ContainsFlag2(flag))
            {
                StatusFlag2 &= ~flag;
            }
        }
        public void AddFlag3(ulong flag)
        {
            StatusFlag3 |= flag;
        }
        public bool ContainsFlag3(ulong flag)
        {
            ulong aux = StatusFlag3;
            aux &= ~flag;
            return !(aux == StatusFlag3);
        }
        public void RemoveFlag3(ulong flag)
        {
            if (ContainsFlag3(flag))
            {
                StatusFlag3 &= ~flag;
            }
        }
        public Enums.AppearanceType Appearance
        {
            get { return (Enums.AppearanceType)BitConverter.ToUInt16(SpawnPacket, _AppearanceType); }
            set {Writer.Write((ushort)value, _AppearanceType, SpawnPacket); }
        }
        public bool fMove(Enums.ConquerAngle Direction, ref ushort _X, ref ushort _Y)
        {
            Facing = Direction;
            sbyte xi = 0, yi = 0;
            switch (Direction)
            {
                case Enums.ConquerAngle.North: xi = -1; yi = -1; break;
                case Enums.ConquerAngle.South: xi = 1; yi = 1; break;
                case Enums.ConquerAngle.East: xi = 1; yi = -1; break;
                case Enums.ConquerAngle.West: xi = -1; yi = 1; break;
                case Enums.ConquerAngle.NorthWest: xi = -1; break;
                case Enums.ConquerAngle.SouthWest: yi = 1; break;
                case Enums.ConquerAngle.NorthEast: yi = -1; break;
                case Enums.ConquerAngle.SouthEast: xi = 1; break;
            }
            _X = (ushort)(_X + xi);
            _Y = (ushort)(_Y + yi);
            if (EntityFlag == Game.EntityFlag.Player)
            {
                if (Owner.Map.Floor[_X, _Y, MapObjType, null])
                    return true;
                else
                    return false;
            }
            else
            {
                Game.Map Map = null;
                if (Kernel.Maps.TryGetValue(MapID, out Map))
                {
                    if (Map.Floor[_X, _Y, MapObjType, null])
                        return true;
                    else
                        return false;
                }
                return true;
            }
        }
        public void SetLocation(ushort MapID, ushort X, ushort Y)
        {
            if (EntityFlag == EntityFlag.Player)
            {
                this.X = X;
                this.Y = Y;
                this.MapID = MapID;
            }
        }
        public void TeleportTeam(ushort MapID, ushort X, ushort Y)
        {
            if (Owner.Team != null)
            {
                foreach (var someone in Owner.Team.Teammates)
                {
                    #region AutoHunt
                    if (someone.Entity.InAutoHunt)
                    {
                        AutoHunt AutoHunt = new AutoHunt();
                        AutoHunt.Action = AutoHunt.Mode.ChangedMap;
                        AutoHunt.EXPGained = someone.Entity.AutoHuntEXP;
                        someone.Send(AutoHunt.Encode());

                        AutoHunt.Action = AutoHunt.Mode.EXPGained;
                        AutoHunt.EXPGained = someone.Entity.AutoHuntEXP;
                        someone.Send(AutoHunt.Encode());
                        someone.IncreaseExperience(someone.Entity.AutoHuntEXP, false);
                        someone.Entity.RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.AutoHunting);
                        someone.Entity.AutoHuntEXP = 0;
                        someone.Entity.InAutoHunt = false;
                    }
                    #endregion
                    if (MyClones.Count != 0)
                    {
                        foreach (var clone in MyClones)
                            clone.RemoveThat();
                        MyClones.Clear();
                    }
                    #region Pets
                    if (someone.Companion != null)
                    {
                        someone.Map.RemoveEntity(someone.Companion);
                        Data data = new Data(true);
                        data.UID = someone.Companion.UID;
                        data.ID = Network.GamePackets.Data.RemoveEntity;
                        someone.Companion.MonsterInfo.SendScreen(data);
                        someone.Companion = null;
                    }
                    #endregion
                    if (EntityFlag == EntityFlag.Player)
                    {
                        ushort baseID = 0;
                        if (!Kernel.Maps.ContainsKey(MapID)) return;
                        else
                        {
                            baseID = Kernel.Maps[MapID].BaseID;
                        }
                        if (someone.InQualifier())
                            if (MapID != 700 && MapID < 11000)
                                someone.EndQualifier();
                        someone.Entity.PrevX = someone.Entity.X;
                        someone.Entity.PrevY = someone.Entity.Y;
                        someone.Entity.X = X;
                        someone.Entity.Y = Y;
                        someone.Entity.PreviousMapID = someone.Entity.MapID;
                        someone.Entity.MapID = MapID;
                        Network.GamePackets.Data Data = new Network.GamePackets.Data(true);
                        Data.UID = UID;
                        Data.ID = Network.GamePackets.Data.Teleport;
                        Data.dwParam = baseID;
                        Data.wParam1 = X;
                        Data.wParam2 = Y;
                        someone.Send(Data);
                        someone.Send(new MapStatus() { BaseID = someone.Map.BaseID, ID = someone.Map.ID, Status = Database.MapsTable.MapInformations[someone.Map.ID].Status, Weather = Database.MapsTable.MapInformations[someone.Map.ID].Weather });
                        someone.Entity.Action = Nyx.Server.Game.Enums.ConquerAction.None;
                        someone.ReviveStamp = Time32.Now;
                        someone.Attackable = false;
                        someone.Screen.Reload(null);
                        someone.ReshareClan();
                        if (!someone.Equipment.Free(12))
                            if (someone.Map.ID == 1036 && someone.Equipment.TryGetItem((byte)12).Plus < 6)
                                RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                    }
                }
            }
            else
            {
                if (this != null && Owner != null)
                {
                    if (this.MapID == 3935 && MapID != 3935)
                    {
                        byte[] buf = new byte[28 + 8];
                        buf[0] = 28;
                        Writer.Write(2501, 2, buf);
                        buf[4] = 1;
                        Writer.Write(this.UID, 8, buf);
                        Writer.Write(uint.MaxValue - this.UID, 12, buf);
                        Owner.Send(buf);
                        PKMode = Game.Enums.PKMode.Capture;
                        Owner.Send(new Data(true) { UID = UID, ID = Data.ChangePKMode, dwParam = (uint)PKMode });
                    }
                    #region AutoHunt
                    if (Owner.Entity.InAutoHunt)
                    {
                        AutoHunt AutoHunt = new AutoHunt();
                        AutoHunt.Action = AutoHunt.Mode.ChangedMap;
                        AutoHunt.EXPGained = Owner.Entity.AutoHuntEXP;
                        Owner.Send(AutoHunt.Encode());

                        AutoHunt.Action = AutoHunt.Mode.EXPGained;
                        AutoHunt.EXPGained = Owner.Entity.AutoHuntEXP;
                        Owner.Send(AutoHunt.Encode());
                        Owner.IncreaseExperience(Owner.Entity.AutoHuntEXP, false);
                        Owner.Entity.RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.AutoHunting);
                        Owner.Entity.AutoHuntEXP = 0;
                        Owner.Entity.InAutoHunt = false;
                    }
                    #endregion
                    if (MyClones.Count != 0)
                    {
                        foreach (var clone in MyClones)
                            clone.RemoveThat();
                        MyClones.Clear();
                    }
                    #region Pets
                    if (Owner.Companion != null)
                    {
                        Owner.Map.RemoveEntity(Owner.Companion);
                        Data data = new Data(true);
                        data.UID = Owner.Companion.UID;
                        data.ID = Network.GamePackets.Data.RemoveEntity;
                        Owner.Companion.MonsterInfo.SendScreen(data);
                        Owner.Companion = null;
                    }
                    #endregion
                    if (EntityFlag == EntityFlag.Player)
                    {
                        #region IfThePlayerInSameMapThatWillTeleportToIt
                        if (this.MapID == MapID)
                        {
                            if (!Database.MapsTable.MapInformations.ContainsKey(MapID) && !Owner.InQualifier())
                            {
                                MapID = 1002;
                                X = 300;
                                Y = 278;
                            }
                            this.X = X;
                            this.Y = Y;
                            Network.GamePackets.Data Data = new Network.GamePackets.Data(true);
                            Data.UID = UID;
                            Data.ID = Network.GamePackets.Data.Teleport;
                            Data.dwParam = Database.MapsTable.MapInformations[MapID].BaseID;
                            Data.wParam1 = X;
                            Data.wParam2 = Y;
                            Owner.Send(Data);
                            Owner.Screen.FullWipe();
                            Owner.Screen.Reload(null);
                            Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });
                            return;
                        }
                        #endregion
                        ushort baseID = 0;
                        if (!Kernel.Maps.ContainsKey(MapID) && !MapsTable.MapInformations.ContainsKey(MapID)) return;
                        else
                        {
                            baseID = Kernel.Maps[MapID].BaseID;
                        }
                        if (Owner.InQualifier())
                            if (MapID != 700 && MapID < 11000)
                                Owner.EndQualifier();
                        this.PrevX = this.X;
                        this.PrevY = this.Y;
                        this.X = X;
                        this.Y = Y;
                        this.PreviousMapID = this.MapID;
                        this.MapID = MapID;
                        Network.GamePackets.Data data = new Network.GamePackets.Data(true);
                        data.UID = UID;
                        data.ID = Network.GamePackets.Data.Teleport;
                        data.dwParam = baseID;
                        data.wParam1 = X;
                        data.wParam2 = Y;
                        Owner.Send(data);
                        Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });
                        Owner.Entity.Action = Nyx.Server.Game.Enums.ConquerAction.None;
                        Owner.ReviveStamp = Time32.Now;
                        Owner.Attackable = false;
                        Owner.Screen.Reload(null);
                        Owner.ReshareClan();
                        if (!Owner.Equipment.Free(12))
                            if (Owner.Map.ID == 1036 && Owner.Equipment.TryGetItem((byte)12).Plus < 6)
                                RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                        if (!Owner.Equipment.Free(12))
                            if (Owner.Equipment.TryGetItem((byte)12).Plus < 4)
                                RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                    }
                }
            }

        }
        public void Teleport(ushort MapID, ushort X, ushort Y)
        {
            if (this != null && Owner != null)
            {
               /* if (this.MapID == 3935 && MapID != 3935)
                {
                    byte[] buf = new byte[28 + 8];
                    buf[0] = 28;
                    Writer.Write(2501, 2, buf);
                    buf[4] = 1;
                    Writer.Write(this.UID, 8, buf);
                    Writer.Write(uint.MaxValue - this.UID, 12, buf);
                    Owner.Send(buf);
                    PKMode = Game.Enums.PKMode.Capture;
                    Owner.Send(new Data(true) { UID = UID, ID = Data.ChangePKMode, dwParam = (uint)PKMode });
                }*/
                if (this.MapID == 3856 && MapID != 3856)
                {
                    Data data = new Data(true);
                    data.UID = UID;
                    data.ID = 104;
                    data.TimeStamp = Time32.Now;
                    data.dwParam = 0;
                    Owner.Send(data);
                    Owner.SendScreen(data, true);
                }  
                #region AutoHunt
                if (Owner.Entity.InAutoHunt)
                {
                    AutoHunt AutoHunt = new AutoHunt();
                    AutoHunt.Action = AutoHunt.Mode.ChangedMap;
                    AutoHunt.EXPGained = Owner.Entity.AutoHuntEXP;
                    Owner.Send(AutoHunt.Encode());

                    AutoHunt.Action = AutoHunt.Mode.EXPGained;
                    AutoHunt.EXPGained = Owner.Entity.AutoHuntEXP;
                    Owner.Send(AutoHunt.Encode());
                    Owner.IncreaseExperience(Owner.Entity.AutoHuntEXP, false);
                    Owner.Entity.RemoveFlag3((ulong)Network.GamePackets.Update.Flags3.AutoHunting);
                    Owner.Entity.AutoHuntEXP = 0;
                    Owner.Entity.InAutoHunt = false;
                }
                #endregion
                if (MyClones.Count != 0)
                {
                    foreach (var clone in MyClones)
                        clone.RemoveThat();
                    MyClones.Clear();
                }
                #region Pets
                if (Owner.Companion != null)
                {
                    Owner.Map.RemoveEntity(Owner.Companion);
                    Data data = new Data(true);
                    data.UID = Owner.Companion.UID;
                    data.ID = Network.GamePackets.Data.RemoveEntity;
                    Owner.Companion.MonsterInfo.SendScreen(data);
                    Owner.Companion = null;
                }
                #endregion
                if (EntityFlag == EntityFlag.Player)
                {
                    #region IfThePlayerInSameMapThatWillTeleportToIt
                    if (this.MapID == MapID)
                    {
                        if (!Database.MapsTable.MapInformations.ContainsKey(MapID) && !Owner.InQualifier())
                        {
                            MapID = 1002;
                            X = 300;
                            Y = 278;
                        }
                        this.X = X;
                        this.Y = Y;
                        Network.GamePackets.Data Data = new Network.GamePackets.Data(true);
                        Data.UID = UID;
                        Data.ID = Network.GamePackets.Data.Teleport;
                        Data.dwParam = Database.MapsTable.MapInformations[MapID].BaseID;
                        Data.wParam1 = X;
                        Data.wParam2 = Y;
                        Owner.Send(Data);
                        Owner.Screen.FullWipe();
                        Owner.Screen.Reload(null);
                        Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });
                        return;
                    }
                    #endregion
                    ushort baseID = 0;
                    if (!Kernel.Maps.ContainsKey(MapID) && !MapsTable.MapInformations.ContainsKey(MapID)) return;
                    else
                    {
                        baseID = Kernel.Maps[MapID].BaseID;
                    }
                    if (Owner.InQualifier())
                        if (MapID != 700 && MapID < 11000)
                            Owner.EndQualifier();
                    this.PrevX = this.X;
                    this.PrevY = this.Y;
                    this.X = X;
                    this.Y = Y;
                    this.PreviousMapID = this.MapID;
                    this.MapID = MapID;
                    Network.GamePackets.Data data = new Network.GamePackets.Data(true);
                    data.UID = UID;
                    data.ID = Network.GamePackets.Data.Teleport;
                    data.dwParam = baseID;
                    data.wParam1 = X;
                    data.wParam2 = Y;
                    Owner.Send(data);
                    Owner.Send(new MapStatus() { BaseID = Owner.Map.BaseID, ID = Owner.Map.ID, Status = Database.MapsTable.MapInformations[Owner.Map.ID].Status, Weather = Database.MapsTable.MapInformations[Owner.Map.ID].Weather });
                    Owner.Entity.Action = Nyx.Server.Game.Enums.ConquerAction.None;
                    Owner.ReviveStamp = Time32.Now;
                    Owner.Attackable = false;
                    Owner.Screen.Reload(null);
                    Owner.ReshareClan();
                    if (!Owner.Equipment.Free(12))
                        if (Owner.Map.ID == 1036 && Owner.Equipment.TryGetItem((byte)12).Plus < 6)
                            RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                    if (!Owner.Equipment.Free(12))
                        if (Owner.Equipment.TryGetItem((byte)12).Plus < 4)
                            RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                }
            }

        }
        public void SoulShackleRemover()
        {
            ShackleTime = 0;
            RemoveFlag2(Network.GamePackets.Update.Flags2.SoulShackle);
        }
        public ushort PrevX, PrevY;
        public void Teleport(ushort BaseID, ushort DynamicID, ushort X, ushort Y)
        {
            if (MyClones.Count != 0)
            {
                foreach (var clone in MyClones)
                    clone.RemoveThat();
                MyClones.Clear();
            }
            if (EntityFlag == EntityFlag.Player)
            {
                if (!Database.DMaps.MapPaths.ContainsKey(BaseID))
                    return;

                if (Owner.InQualifier())
                    if (BaseID != 700 && BaseID < 11000)
                        Owner.EndQualifier();
                if (!Kernel.Maps.ContainsKey(DynamicID)) new Map(DynamicID, BaseID, Database.DMaps.MapPaths[BaseID]);
                this.PrevX = this.X;
                this.PrevY = this.Y;
                this.X = X;
                this.Y = Y;
                this.PreviousMapID = this.MapID;
                this.MapID = DynamicID;
                Network.GamePackets.Data Data = new Network.GamePackets.Data(true);
                Data.UID = UID;
                Data.ID = Network.GamePackets.Data.Teleport;
                Data.dwParam = BaseID;
                Data.wParam1 = X;
                Data.wParam2 = Y;
                Owner.Send(Data);
                Owner.Entity.Action = Nyx.Server.Game.Enums.ConquerAction.None;
                Owner.ReviveStamp = Time32.Now;
                Owner.Attackable = false;
                Owner.Screen.Reload(null);
                
                if (!Owner.Equipment.Free(12))
                    if (Owner.Map.ID == 1036 && Owner.Equipment.TryGetItem((byte)12).Plus < 6)
                        RemoveFlag(Network.GamePackets.Update.Flags.Ride);
            }
        }
        public bool OnKOSpell()
        {
            return OnCyclone() || OnSuperman() || OnSuperCyclone();
        }
        public bool OnSuperCyclone()
        {
            return ContainsFlag3(Network.GamePackets.Update.Flags3.SuperCyclone) || ContainsFlag2(Network.GamePackets.Update.Flags3.SuperCyclone) || ContainsFlag(Network.GamePackets.Update.Flags3.SuperCyclone);
        }  
        public bool OnOblivion()
        {
            return ContainsFlag2(Network.GamePackets.Update.Flags2.Oblivion);
        }
        public bool OnCyclone()
        {
            return ContainsFlag(Network.GamePackets.Update.Flags.Cyclone);
        }
        public bool OnSuperman()
        {
            return ContainsFlag(Network.GamePackets.Update.Flags.Superman);
        }
        public bool OnScurvyBomb()
        {
            return ContainsFlag2(Network.GamePackets.Update.Flags2.ScurvyBomb);
        }
        public bool OnFatalStrike()
        {
            return ContainsFlag(Network.GamePackets.Update.Flags.FatalStrike);
        }
        public String ToHex(byte[] buf)
        {
            var builder = new StringBuilder();
            foreach (var b in buf)
                builder.Append(b.ToString("X2") + " ");
            return builder.ToString();
        }
        public void Untransform()
        {
            if (MapID == 1036 && TransformationTime == 3600)
                return;
            this.TransformationID = 0;

            double maxHP = TransformationMaxHP;
            double HP = Hitpoints;
            double point = HP / maxHP;

            Hitpoints = (uint)(MaxHitpoints * point);
            Update(Network.GamePackets.Update.MaxHitpoints, MaxHitpoints, false);
        }
        public byte[] WindowSpawn()
        {
            byte[] buffer = new byte[SpawnPacket.Length];
            SpawnPacket.CopyTo(buffer, 0);
            buffer[_WindowSpawn] = 1;
            return buffer;
        }
        public int getFan(bool Magic)
        {
            if (Owner.Equipment.Free(10))
                return 0;

            ushort magic = 0;
            ushort physical = 0;
            ushort gemVal = 0;

            #region Get
            ConquerItem Item = this.Owner.Equipment.TryGetItem(10);

            if (Item != null)
            {
                if (Item.ID > 0)
                {
                    switch (Item.ID % 10)
                    {
                        case 3:
                        case 4:
                        case 5: physical += 300; magic += 150; break;
                        case 6: physical += 500; magic += 200; break;
                        case 7: physical += 700; magic += 300; break;
                        case 8: physical += 900; magic += 450; break;
                        case 9: physical += 1200; magic += 750; break;
                    }

                    switch (Item.Plus)
                    {
                        case 0: break;
                        case 1: physical += 200; magic += 100; break;
                        case 2: physical += 400; magic += 200; break;
                        case 3: physical += 600; magic += 300; break;
                        case 4: physical += 800; magic += 400; break;
                        case 5: physical += 1000; magic += 500; break;
                        case 6: physical += 1200; magic += 600; break;
                        case 7: physical += 1300; magic += 700; break;
                        case 8: physical += 1400; magic += 800; break;
                        case 9: physical += 1500; magic += 900; break;
                        case 10: physical += 1600; magic += 950; break;
                        case 11: physical += 1700; magic += 1000; break;
                        case 12: physical += 1800; magic += 1050; break;
                    }
                    switch (Item.SocketOne)
                    {
                        case Enums.Gem.NormalThunderGem: gemVal += 100; break;
                        case Enums.Gem.RefinedThunderGem: gemVal += 300; break;
                        case Enums.Gem.SuperThunderGem: gemVal += 500; break;
                    }
                    switch (Item.SocketTwo)
                    {
                        case Enums.Gem.NormalThunderGem: gemVal += 100; break;
                        case Enums.Gem.RefinedThunderGem: gemVal += 300; break;
                        case Enums.Gem.SuperThunderGem: gemVal += 500; break;
                    }
                }
            }
            #endregion

            magic += gemVal;
            physical += gemVal;

            if (Magic)
                return (int)magic;
            else
                return (int)physical;
        }
        public int getTower(bool Magic)
        {
            if (Owner.Equipment.Free(11))
                return 0;

            ushort magic = 0;
            ushort physical = 0;
            ushort gemVal = 0;

            #region Get
            ConquerItem Item = this.Owner.Equipment.TryGetItem(11);

            if (Item != null)
            {
                if (Item.ID > 0)
                {
                    switch (Item.ID % 10)
                    {
                        case 3:
                        case 4:
                        case 5: physical += 250; magic += 100; break;
                        case 6: physical += 400; magic += 150; break;
                        case 7: physical += 550; magic += 200; break;
                        case 8: physical += 700; magic += 300; break;
                        case 9: physical += 1100; magic += 600; break;
                    }

                    switch (Item.Plus)
                    {
                        case 0: break;
                        case 1: physical += 150; magic += 50; break;
                        case 2: physical += 350; magic += 150; break;
                        case 3: physical += 550; magic += 250; break;
                        case 4: physical += 750; magic += 350; break;
                        case 5: physical += 950; magic += 450; break;
                        case 6: physical += 1100; magic += 550; break;
                        case 7: physical += 1200; magic += 625; break;
                        case 8: physical += 1300; magic += 700; break;
                        case 9: physical += 1400; magic += 750; break;
                        case 10: physical += 1500; magic += 800; break;
                        case 11: physical += 1600; magic += 850; break;
                        case 12: physical += 1700; magic += 900; break;
                    }
                    switch (Item.SocketOne)
                    {
                        case Enums.Gem.NormalGloryGem: gemVal += 100; break;
                        case Enums.Gem.RefinedGloryGem: gemVal += 300; break;
                        case Enums.Gem.SuperGloryGem: gemVal += 500; break;
                    }
                    switch (Item.SocketTwo)
                    {
                        case Enums.Gem.NormalGloryGem: gemVal += 100; break;
                        case Enums.Gem.RefinedGloryGem: gemVal += 300; break;
                        case Enums.Gem.SuperGloryGem: gemVal += 500; break;
                    }
                }
            }
            #endregion

            magic += gemVal;
            physical += gemVal;

            if (Magic)
                return (int)magic;
            else
                return (int)physical;
        }
        public Double GemBonus(Byte type)
        {
            Double bonus = 0;
            foreach (ConquerItem i in Owner.Equipment.Objects)
                if (i != null)
                    if (i.IsWorn)
                        bonus += i.GemBonus(type);
            if (Class >= 130 && Class <= 135)
                if (type == ItemSocket.Tortoise)
                    bonus = Math.Min(0.12, bonus);
            return bonus;
        }
        public int BattlePower
        {
            get
            {
                return BattlePowerCalc(this);
            }
        }
        public int NMBattlePower
        {
            get
            {
                return (int)(BattlePowerCalc(this) - MentorBattlePower);
            }
        }
        public uint BattlePowerFrom(Entity mentor)
        {
            if (mentor.NMBattlePower < NMBattlePower) return 0;
            uint bp = (uint)((mentor.NMBattlePower - NMBattlePower) / 3.3F);
            if (Level >= 125) bp = (uint)((bp * (135 - Level)) / 10);
            if (bp < 0) bp = 0;
            return bp;
        }
        byte _ClanSharedBp;
        public byte ClanSharedBp
        {
            get { return _ClanSharedBp; }
            set
            {
                _ClanSharedBp = value;
                Update(36, value, false);
                Owner.CalculateStatBonus();
                Owner.CalculateHPBonus();
                Owner.LoadItemStats();
            }
        }
       public string Name 
        { 
            get { return _Name; } 
            set 
            { 
                _Name = value; 
                LoweredName = value.ToLower(); 
                if (EntityFlag == Game.EntityFlag.Player) 
                { 
                    SpawnPacket = new byte[8 + _Names + _Name.Length + 36]; 
                    Write((ushort)(SpawnPacket.Length - 8), 0, SpawnPacket);
                    Write(10014, 2, SpawnPacket);
                    Write((uint)Time32.timeGetTime().GetHashCode(), 4, SpawnPacket);
                    Write(new List<string>() { _Name, "", clan, "", "", "", _unionname }, _Names, SpawnPacket); 
                } 
                else 
                { 
                    if (ClanName != "") 
                    { 
                        SpawnPacket = new byte[8 + _Names + _Name.Length + ClanName.Length + 6];
                        Write((ushort)(SpawnPacket.Length - 8), 0, SpawnPacket);
                        Write(10014, 2, SpawnPacket);
                        Write((uint)Time32.timeGetTime().GetHashCode(), 4, SpawnPacket);
                        Write(new List<string>() { _Name, "", "", ClanName }, _Names, SpawnPacket); 
                    } 
                    else 
                    { 
                        SpawnPacket = new byte[8 + _Names + _Name.Length + 20];
                        Write((ushort)(SpawnPacket.Length - 8), 0, SpawnPacket);
                        Write(10014, 2, SpawnPacket);
                        Write((uint)Time32.timeGetTime().GetHashCode(), 4, SpawnPacket);
                        Write(new List<string>() { _Name, "", "", "" }, _Names, SpawnPacket); 
                    } 
                } 
            } 
        }
        public string Spouse
        {
            get
            {
                return _Spouse;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets._String.Spouse, value, false);
                }
                _Spouse = value;
            }
        }
        public uint Money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Money, value, false);


            }
        }
        public byte VIPLevel
        {
            get
            {
                return _vipLevel;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.VIPLevel, value, false);
                }
                _vipLevel = value;
            }
        }
        public uint BoundCps
        {
            get { return _boundCps; }
            set
            {
                Update(Network.GamePackets.Update.BoundConquerPoints, value, false);
                _boundCps = value;
            }
        }
        public uint ConquerPoints
        {
            get
            {
                return _conquerpoints;
            }
            set
            {
                _conquerpoints = value;
                Update(Network.GamePackets.Update.ConquerPoints, value, false);
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
                if (EntityFlag == EntityFlag.Player)
                {
                    if (Owner != null)
                    {
                        if (Owner.ArenaStatistic != null)
                            Owner.ArenaStatistic.Model = (uint)(Face * 10000 + value);
                        Update(Network.GamePackets.Update.Mesh, Mesh, true);
                    }
                }
            }
        }
        public ushort DoubleExperienceTime
        {
            get
            {
                return _doubleexp;
            }
            set
            {
                ushort oldVal = DoubleExperienceTime;
                _doubleexp = value;
                if (FullyLoaded)
                    if (oldVal <= _doubleexp)
                        if (EntityFlag == EntityFlag.Player)
                        {
                            if (Owner != null)
                            {
                                Update(Network.GamePackets.Update.DoubleExpTimer, DoubleExperienceTime, 200, false);
                            }
                        }
            }
        }
        public ushort CalculateHitPoint()
        {
            ushort valor = 0;
            switch (Class)
            {
                case 11:
                    valor += (ushort)(Agility * 3.15 + Spirit * 3.15 + Strength * 3.15 + Vitality * 25.2);
                    break;
                case 12:
                    valor += (ushort)(Agility * 3.24 + Spirit * 3.24 + Strength * 3.24 + Vitality * 25.9);
                    break;
                case 13:
                    valor += (ushort)(Agility * 3.30 + Spirit * 3.30 + Strength * 3.30 + Vitality * 26.4);
                    break;
                case 14:
                    valor += (ushort)(Agility * 3.36 + Spirit * 3.36 + Strength * 3.36 + Vitality * 26.8);
                    break;
                case 15:
                    valor += (ushort)(Agility * 3.45 + Spirit * 3.45 + Strength * 3.45 + Vitality * 27.6);
                    break;
                default:
                    valor += (ushort)(Agility * 3 + Spirit * 3 + Strength * 3 + Vitality * 24);
                    break;
            }
            return valor;

        }
        public ushort SuperPotion
        {
            get
            {
                return _superpotion;
            }
            set
            {
                uint oldVal = SuperPotion;
                _superpotion = value;
            }
        }
        public uint HeavenBlessing
        {
            get
            {
                return _heavenblessing;
            }
            set
            {
                uint oldVal = HeavenBlessing;
                _heavenblessing = value;
                if (FullyLoaded)
                    if (value > 0)
                        if (!ContainsFlag(Network.GamePackets.Update.Flags.HeavenBlessing) || oldVal <= _heavenblessing)
                        {
                            AddFlag(Network.GamePackets.Update.Flags.HeavenBlessing);
                            Update(Network.GamePackets.Update.HeavensBlessing, HeavenBlessing, false);
                            Update(Network.GamePackets._String.Effect, "bless", true);
                        }
            }
        }
        public byte Stamina
        {
            get
            {
                return _stamina;
            }
            set
            {
                _stamina = value;
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Stamina, value, false);
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
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Mesh, Mesh, true);
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
                if (EntityFlag == EntityFlag.Player)
                {
                    if (Owner != null)
                    {
                        if (Owner.ArenaStatistic != null)
                            Owner.ArenaStatistic.Model = (uint)(value * 10000 + Body);
                        Update(Network.GamePackets.Update.Mesh, Mesh, true);
                    }
                }
            }
        }
        public uint Mesh
        {
            get
            {
                return BitConverter.ToUInt32(SpawnPacket, _Mesh);
            }
        }
        public byte Class
        {
            get
            {
                return _class;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    if (Owner != null)
                    {
                        if (Owner.ArenaStatistic != null)
                            Owner.ArenaStatistic.Class = value;
                        Update(Network.GamePackets.Update.Class, value, false);
                    }
                }
                _class = value;
                SpawnPacket[_Class] = value;
            }
        }
        public byte Reborn
        {
            get
            {
                return _reborn;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.Reborn, value, true);
                }
                _reborn = value;
                SpawnPacket[_Reborn] = value;
            }
        }
        public string QuestFrom
        {
            get
            {
                if (EntityFlag == Game.EntityFlag.Player)
                    return Owner["QuestFrom"];
                return "";
            }
            set
            {
                if (EntityFlag == Game.EntityFlag.Player)
                    Owner["QuestFrom"] = value;
            }
        }
        public string QuestMob
        {
            get
            {
                if (EntityFlag == Game.EntityFlag.Player)
                    return Owner["QuestMob"];
                return "";
            }
            set
            {
                if (EntityFlag == Game.EntityFlag.Player)
                    Owner["QuestMob"] = value;
            }
        }
        public byte Level
        {
            get
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    SpawnPacket[_Level] = _level;
                    return _level;
                }
                else
                {
                    SpawnPacket[_MonsterLevel] = _level;
                    return _level;
                }
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.Level, value, true);
                    Data _Data = new Data(true);
                    _Data.UID = UID;
                    _Data.ID = Data.Leveled;
                    // _Data.TimeStamp = Time32.Now;
                    //   _Data.Unknown2 = 1;
                    _Data.wParam1 = value;

                    if (Owner != null)
                    {
                        (Owner as Client.GameClient).SendScreen(_Data, true);
                        if (Owner.ArenaStatistic != null)
                        {
                            Owner.ArenaStatistic.Level = value;
                            Owner.ArenaStatistic.ArenaPoints = Database.ArenaTable.ArenaPointFill(Owner.ArenaStatistic.Level);
                        }
                    }
                    if (Owner != null)
                    {
                        if (Owner.AsMember != null)
                        {
                            Owner.AsMember.Level = value;
                        }
                    }
                    SpawnPacket[_Level] = value;
                }
                else
                {
                    SpawnPacket[_MonsterLevel] = value;
                }
                _level = value;
            }
        }
        private uint mentorBP;
        public uint MentorBattlePower
        {
            get
            {
                return mentorBP;
            }
            set
            {
                if ((int)value < 0)
                    value = 0;
                if (Owner.Mentor != null)
                {
                    if (Owner.Mentor.IsOnline)
                    {
                        ExtraBattlePower -= mentorBP;
                        mentorBP = value;
                        ExtraBattlePower += value;
                        int val = Owner.Mentor.Client.Entity.BattlePower;
                        Update(Network.GamePackets.Update.MentorBattlePower, (uint)Math.Min(val, value), (uint)val);
                    }
                    else
                    {
                        ExtraBattlePower -= mentorBP;
                        mentorBP = 0;
                        Update(Network.GamePackets.Update.MentorBattlePower, (uint)0, (uint)0);
                    }
                }
                else
                {
                    ExtraBattlePower -= mentorBP;
                    mentorBP = 0;
                    Update(Network.GamePackets.Update.MentorBattlePower, (uint)0, (uint)0);
                }
            }
        }
        public uint ExtraBattlePower
        {
            get
            {
                return BitConverter.ToUInt32(SpawnPacket, _ExtraBattlepower);
            }
            set
            {
                if (value > 200) value = 0;
                Write(value, _ExtraBattlepower, SpawnPacket);
            }
        }
        public void SendSysMessage(string mesaj)
        {
            byte[] buffer = new Nyx.Server.Network.GamePackets.Message(mesaj, System.Drawing.Color.Yellow, 0x7dc).Encode();
            this.Owner.Send(buffer);
        }
        public void SetAway(bool isAway)
        {
            if (!isAway && Away == 1)
            {
                Away = 0;
                Owner.SendScreen(SpawnPacket, false);

                if (awayTeleported)
                {
                    awayTeleported = false;
                    Teleport(PreviousMapID, PrevX, PrevY);
                }
            }
            else if (isAway && Away == 0)
            {
                if (!Constants.PKFreeMaps.Contains(MapID))
                {
                    if (!(MapID == 1036 || MapID == 1002 || Owner.Mining))
                    {
                        PreviousMapID = MapID;
                        PrevX = X;
                        PrevY = Y;
                        Teleport(1036, 100, 100);
                        awayTeleported = true;
                    }
                }
            }
            Away = isAway ? (byte)1 : (byte)0;
        }
        public byte Away
        {
            get
            {
                return SpawnPacket[_Away];
            }
            set
            {
                SpawnPacket[_Away] = value;
            }
        }
        public uint UID
        {
            get
            {
                if (SpawnPacket != null && SpawnPacket.Length >= _UID)
                    return BitConverter.ToUInt32(SpawnPacket, _UID);
                else
                    return _uid;
            }
            set
            {
                _uid = value;
                Write(value, _UID, SpawnPacket);
            }
        }
        public ushort GuildID
        {
            get
            {
                return BitConverter.ToUInt16(SpawnPacket, _GuildID);
            }
            set
            {
                Write(value, _GuildID, SpawnPacket);
            }
        }
        public ushort GuildRank
        {
            get
            {
                return BitConverter.ToUInt16(SpawnPacket, _GuildRank);
            }
            set
            {
                Writer.Write(value, _GuildRank, SpawnPacket);
            }
        }
        public ushort Strength
        {
            get
            {
                return _strength;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.Strength, value, false);
                }
                _strength = value;
            }
        }
        public ushort Agility
        {
            get
            {
                if (OnCyclone())
                    return (ushort)(_agility);
                return _agility;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Agility, value, false);
                _agility = value;
            }
        }
        public ushort Spirit
        {
            get
            {
                return _spirit;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Spirit, value, false);
                _spirit = value;
            }
        }
        public ushort Vitality
        {
            get
            {
                return _vitality;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Vitality, value, false);
                _vitality = value;
            }
        }
        public ushort Atributes
        {
            get
            {
                return _atributes;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Atributes, value, false);
                _atributes = value;
            }
        }
        public byte Boss
        {
            get
            {
                return SpawnPacket[_Boss];
            }
            set
            {
                SpawnPacket[_Boss] = 1;
            }
        }
        public uint Hitpoints
        {
            get
            {
                return _hitpoints;
            }
            set
            {
                value = (uint)Math.Max(0, (int)value);
                if (EntityFlag == EntityFlag.Player)
                    Update((byte)Network.GamePackets.Update.Hitpoints, value, true);
                else if (EntityFlag == EntityFlag.Monster)
                {
                    var update = new Update(true);
                    update.UID = UID;
                    update.Append(Network.GamePackets.Update.Hitpoints, value);
                    MonsterInfo.SendScreen(update);
                }
                _hitpoints = value;
                if (Boss > 0)
                {
                    uint key = (uint)(MaxHitpoints / 10000);
                    if (key != 0)
                        Writer.Write((ushort)(value / key), _Hitpoints, SpawnPacket);
                    else
                        Writer.Write((ushort)(value * MaxHitpoints / 1000 / 1.09), _Hitpoints, SpawnPacket);
                }
                else
                    Writer.Write((ushort)value, _Hitpoints, SpawnPacket);
                if (EntityFlag == EntityFlag.Player)
                {
                    if (Owner != null)
                    {
                        if (Owner.Team != null)
                        {
                            foreach (var Team in Owner.Team.Temates)
                            {
                                TeamMember addme = new TeamMember();
                                addme.UID = Owner.Entity.UID;
                                addme.Hitpoints = (ushort)Owner.Entity.Hitpoints;
                                addme.Mesh = Owner.Entity.Mesh;
                                addme.Name = Owner.Entity.Name;
                                addme.MaxHitpoints = (ushort)Owner.Entity.MaxHitpoints;
                                Team.entry.Send(addme.Encode());
                            }
                        }
                    }
                }
            }
        }
        public ushort Mana
        {
            get
            {
                return _mana;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Mana, value, false);
                _mana = value;
            }
        }
        public ushort MaxMana
        {
            get
            {
                return _maxmana;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.MaxMana, value, false);
                _maxmana = value;
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
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.HairStyle, value, true);
                }
                _hairstyle = value;
                Writer.Write(value, _HairStyle, SpawnPacket);
            }
        }
        public ConquerStructures.NobilityRank NobilityRank
        {
            get
            {
                return (Nyx.Server.Game.ConquerStructures.NobilityRank)SpawnPacket[_NobilityRank];
            }
            set
            {
                SpawnPacket[_NobilityRank] = (byte)value;
                if (Owner != null)
                {
                    if (Owner.AsMember != null)
                    {
                        Owner.AsMember.NobilityRank = value;
                    }
                }
            }
        }
        public byte HairColor
        {
            get
            {
                return (byte)(HairStyle / 100);
            }
            set
            {
                HairStyle = (ushort)((value * 100) + (HairStyle % 100));
            }
        }
        public ushort MapID
        {
            get
            {
                return _mapid;
            }
            set
            {
                _mapid = value;
            }
        }
        public ushort PreviousMapID
        {
            get
            {
                return _previousmapid;
            }
            set
            {
                _previousmapid = value;
            }
        }
        public ushort X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                Writer.Write(value, _X, SpawnPacket);
            }
        }
        public ushort Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                Writer.Write(value, _Y, SpawnPacket);
            }
        }
        public bool Dead
        {
            get
            {
                return Hitpoints < 1;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public ushort Defence
        {
            get
            {
                if (Time32.Now < ShieldStamp.AddSeconds(ShieldTime) && ContainsFlag(Network.GamePackets.Update.Flags.MagicShield))
                    if (ShieldIncrease > 0)
                        return (ushort)Math.Min(65535, (int)(BaseDefence * ShieldIncrease));
                if (Time32.Now < ShieldStamp.AddSeconds(ShieldTime) && ContainsFlag3(Network.GamePackets.Update.Flags3.WarriorEpicShield))
                    if (ShieldIncrease > 0)
                        return (ushort)Math.Min(65535, (int)(BaseDefence * ShieldIncrease));
                if (SuperItemBless > 0)
                    return (ushort)(BaseDefence + (float)BaseDefence / 100 * SuperItemBless);
                return BaseDefence;
            }
            set { BaseDefence = value; }
        }
        public ushort TransformationDefence
        {
            get
            {
                if (ContainsFlag(Network.GamePackets.Update.Flags.MagicShield) || ContainsFlag3(Network.GamePackets.Update.Flags3.WarriorEpicShield))
                {
                    if (ShieldTime > 0)
                        return (ushort)(_TransPhysicalDefence * ShieldIncrease);
                    else
                        return (ushort)(_TransPhysicalDefence * MagicShieldIncrease);
                }
                return (ushort)_TransPhysicalDefence;
            }
            set { _TransPhysicalDefence = value; }
        }
        public ushort MagicDefencePercent
        {
            get { return _MDefencePercent; }
            set { _MDefencePercent = value; }
        }
        public ushort TransformationMagicDefence
        {
            get { return (ushort)_TransMagicDefence; }
            set { _TransMagicDefence = value; }
        }
        public ushort MagicDefence
        {
            get { return _MDefence; }
            set { _MDefence = value; }
        }
        public Client.GameClient Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        public uint TransformationMinAttack
        {
            get
            {
                if (ContainsFlag(Network.GamePackets.Update.Flags.Stigma))
                    return (uint)(_TransMinAttack * StigmaIncrease);
                return _TransMinAttack;
            }
            set { _TransMinAttack = value; }
        }
        public uint TransformationMaxAttack
        {
            get
            {
                if (ContainsFlag(Network.GamePackets.Update.Flags.Stigma))
                    return (uint)(_TransMaxAttack * StigmaIncrease);
                return _TransMaxAttack;
            }
            set { _TransMaxAttack = value; }
        }
        public uint MinAttack
        {
            get
            {
                return _MinAttack;
            }
            set { _MinAttack = value; }
        }
        public uint MaxAttack
        {
            get
            {
                return _MaxAttack;
            }
            set { _MaxAttack = value; }
        }
        public uint MaxHitpoints
        {
            get
            {
                return _maxhitpoints;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    if (TransformationID != 98)
                        Update(Network.GamePackets.Update.MaxHitpoints, value, true);
                }
                _maxhitpoints = value;
            }
        }
        public uint MagicAttack
        {
            get
            {
                return _MagicAttack;
            }
            set { _MagicAttack = value; }
        }
        public byte Dodge
        {
            get
            {
                if (ContainsFlag(Network.GamePackets.Update.Flags.Dodge))
                {
                    return (byte)(_Dodge * DodgeIncrease);
                }
                return _Dodge;
            }
            set { _Dodge = value; }
        }
        public ushort Dexterity;
        public byte TransformationDodge
        {
            get
            {
                if (ContainsFlag(Network.GamePackets.Update.Flags.Dodge))
                    return (byte)(_TransDodge * DodgeIncrease);
                return (byte)_TransDodge;
            }
            set { _TransDodge = value; }
        }
        public MapObjectType MapObjType
        {
            get { return _MapObjectType; }
            set { _MapObjectType = value; }
        }
        public EntityFlag EntityFlag
        {
            get { return _EntityFlag; }
            set { _EntityFlag = value; }
        }
        public ulong Experience
        {
            get
            {
                return _experience;
            }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                    Update(Network.GamePackets.Update.Experience, value, false);
                _experience = value;
            }
        }
        public double EnlightenPoints
        {
            get
            {
                return _enlightenPoints;
            }
            set
            {
                _enlightenPoints = value;
            }
        }
        public byte ReceivedEnlightenPoints
        {
            get
            {
                return _receivedEnlighenPoints;
            }
            set
            {
                _receivedEnlighenPoints = value;
            }
        }
        public ushort EnlightmentTime
        {
            get
            {
                return _enlightmenttime;
            }
            set
            {
                _enlightmenttime = value;
            }
        }
        public ushort PKPoints
        {
            get
            {
                return _pkpoints;
            }
            set
            {
                _pkpoints = value;
                if (EntityFlag == EntityFlag.Player)
                {
                    Update(Network.GamePackets.Update.PKPoints, value, false);
                    if (PKPoints > 99)
                    {
                        RemoveFlag(Network.GamePackets.Update.Flags.RedName);
                        AddFlag(Network.GamePackets.Update.Flags.BlackName);
                    }
                    else if (PKPoints > 29)
                    {
                        AddFlag(Network.GamePackets.Update.Flags.RedName);
                        RemoveFlag(Network.GamePackets.Update.Flags.BlackName);
                    }
                    else if (PKPoints < 30)
                    {
                        RemoveFlag(Network.GamePackets.Update.Flags.RedName);
                        RemoveFlag(Network.GamePackets.Update.Flags.BlackName);
                    }
                }
            }
        }
        public bool Hercule()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null && weapons.Item2 != null)
                    if (weapons.Item1.ID / 1000 == 480 && weapons.Item2.ID / 1000 == 480 || weapons.Item1.ID / 1000 == 410 && weapons.Item2.ID / 1000 == 410 || weapons.Item1.ID / 1000 == 420 && weapons.Item2.ID / 1000 == 420 || weapons.Item1.ID / 1000 == 614 && weapons.Item2.ID / 1000 == 614)
                        return true;
            }
            return false;
        }
        public uint QuizPoints
        {
            get { return BitConverter.ToUInt32(SpawnPacket, _QuizPoints); }
            set
            {
                if (EntityFlag == EntityFlag.Player)
                {
                    Update((byte)Network.GamePackets.Update.QuizPoints, value, true);
                }
                Write(value, _QuizPoints, SpawnPacket);
            }
        }
        public UInt32 ClanId
        {
            get { return BitConverter.ToUInt32(SpawnPacket, _ClanUID); }
            set { Write((UInt32)value, _ClanUID, SpawnPacket); }
        }
        public Clan Myclan;
        public Clan.Ranks ClanRank
        {
            get { return (Clan.Ranks)SpawnPacket[_ClanRank]; }
            set { SpawnPacket[_ClanRank] = (Byte)value; }
        }
        public Clan GetClan
        {
            get
            {
                Clan cl;
                Kernel.Clans.TryGetValue(ClanId, out cl);
                return cl;
            }
        }
        string clan = "";
        public string ClanName
        {
            get { return clan; }
            set
            {
                clan = value;
                if (clan.Length > 15)
                    clan = clan.Substring(0, 15);
                Writer.Write(new List<string>() { _Name, "", clan, "" }, _Names, SpawnPacket);
            }
        }
        private UInt32 mClanJoinTarget;
        public UInt32 ClanJoinTarget
        {
            get { return this.mClanJoinTarget; }
            set { this.mClanJoinTarget = value; }
        }
        public Enums.PKMode PKMode
        {
            get { return _PKMode; }
            set { _PKMode = value; }
        }
        public ushort Action
        {
            get { return BitConverter.ToUInt16(SpawnPacket, _Action); }
            set
            {
                Writer.Write(value, _Action, SpawnPacket);
            }
        }
        private string _unionname = "";
        public string UnionName
        {
            get { return _unionname; }
            set
            {
                _unionname = value;
                Write(new List<string>() { _Name, "", clan, "", "", "", _unionname }, _Names, SpawnPacket);
                Owner.SendScreen(SpawnPacket, false);
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
        public ulong StatusFlag
        {
            get
            {
                return BitConverter.ToUInt64(SpawnPacket, _StatusFlag);
            }
            set
            {
                ulong OldV = StatusFlag;
                if (value != OldV)
                {
                    Writer.Write(value, _StatusFlag, SpawnPacket);
                    UpdateEffects(true);
                }
            }
        }
        private ulong _Stateff2 = 0;
        public ulong StatusFlag2
        {
            get { return _Stateff2; }
            set
            {
                ulong OldV = StatusFlag2;
                if (value != OldV)
                {
                    _Stateff2 = value;
                 Writer.Write(value, _StatusFlag2, SpawnPacket);

                    UpdateEffects(true);
                    // Update2(Network.GamePackets.Update.StatusFlag, value, true);// !ContainsFlag(Network.GamePackets.Update.Flags.XPList));//you need to update the SECOND value of stateff
                }
            }
        }
        ulong _Stateff3 = 0;
        public ulong StatusFlag3
        {
            get { return _Stateff3; }
            set
            {
                ulong OldV = StatusFlag3;
                if (value != OldV)
                {
                    _Stateff3 = value;
                 Writer.Write(value, _StatusFlag3, SpawnPacket);

                    UpdateEffects(true);
                    // Update2(Network.GamePackets.Update.StatusFlag, value, true);// !ContainsFlag(Network.GamePackets.Update.Flags.XPList));//you need to update the SECOND value of stateff
                }
            }
        }
        public void SendScreen(Interfaces.IPacket Data)
        {
            Client.GameClient[] Chars = new Client.GameClient[Kernel.GamePool.Count];
            Kernel.GamePool.Values.CopyTo(Chars, 0);
            foreach (Client.GameClient C in Chars)
                if (C != null)
                    if (C.Entity != null)
                        if (Game.Calculations.PointDistance(X, Y, C.Entity.X, C.Entity.Y) <= 20)
                            C.Send(Data);
            Chars = null;

        }
        public void DieString()
        {
            _String str = new _String(true);
            str.UID = this.UID;
            str.Type = _String.Effect;
            str.Texts.Add("ghost");
            str.Texts.Add("1ghost");
            str.TextsCount = 1;
            if (EntityFlag == Game.EntityFlag.Player)
            {
                this.SendScreen(str);
            }
        }
        public ushort CountryID
        {
            get
            {
                return BitConverter.ToUInt16(SpawnPacket, _CountryCode);
            }
            set
            {
                Writer.Write((ushort)value, _CountryCode, SpawnPacket);
            }
        }
        public bool Assassin()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null)
                    if (weapons.Item1.ID / 1000 == 613)
                        return true;
                    else if (weapons.Item2 != null)
                        if (weapons.Item2.ID / 1000 == 613)
                            return true;
            }
            return false;
        }
        public bool EpicTrojan()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null)
                    if (weapons.Item1.ID / 1000 == 614)
                        return true;
                    else if (weapons.Item2 != null)
                        if (weapons.Item2.ID / 1000 == 614)
                            return true;
            }
            return false;
        }
        public bool SpiritFocus
        {
            get
            {
                if (!spiritFocus) return false;
                if (Time32.Now > spiritFocusStamp.AddSeconds(5))
                    return true;
                return false;
            }
            set { spiritFocus = value; if (value) spiritFocusStamp = Time32.Now; }
        }
        public uint GuildBattlePower
        {
            get
            {
                return guildBP;
            }
            set
            {
              //  ExtraBattlePower -= guildBP;
                guildBP = value;
                Update(Network.GamePackets.Update.GuildBattlepower, guildBP, false);
                Write(Math.Min(value, 15), 209, SpawnPacket);
             //   ExtraBattlePower += guildBP;
            }
        }
        internal void PreviousTeleport()
        {
            Teleport(PreviousMapID, PrevX, PrevY);
            BringToLife();
        }
        public bool IsThisLeftGate(int X, int Y)
        {
            if (Game.GuildWar.RightGate == null)
                return false;
            if (MapID == 1038)
            {
                if ((X == 223 || X == 222) && (Y >= 175 && Y <= 185))
                {
                    if (Game.GuildWar.RightGate.Mesh / 10 == 27)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool InJail()
        {
            if (MapID == 6000 || MapID == 6001)
                return true;
            return false;
        }
        public bool IsThisRightGate(int X, int Y)
        {
            if (Game.GuildWar.LeftGate == null)
                return false;
            if (MapID == 1038)
            {
                if ((Y == 210 || Y == 209) && (X >= 154 && X <= 166))
                {
                    if (Game.GuildWar.LeftGate.Mesh / 10 == 24)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool ThroughGate(int X, int Y)
        {
            return IsThisLeftGate(X, Y) || IsThisRightGate(X, Y);
        }
        public int BP
        {
            get
            {
                return BitConverter.ToInt32(this.SpawnPacket, _BattlePower);
            }
            set
            {
                Writer.Write(value, _BattlePower, this.SpawnPacket);
            }
        }
        public bool EpicNinja()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null)
                    if (weapons.Item1.ID / 1000 == 616)
                        return true;
                    else if (weapons.Item2 != null)
                        if (weapons.Item2.ID / 1000 == 616)
                            return true;
            }
            return false;
        }
        public bool EpicMonk()
        {
            if (EntityFlag == Game.EntityFlag.Player)
            {
                var weapons = Owner.Weapons;
                if (weapons.Item1 != null && weapons.Item2 != null)
                    if (weapons.Item1.ID / 1000 == 622 && weapons.Item2.ID / 1000 == 622)
                            return true;
            }
            return false;
        }  
    }
}