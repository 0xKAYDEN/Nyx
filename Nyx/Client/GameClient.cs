using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;
using Nyx.Server.Network.Cryptography;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network.Sockets;
using Nyx.Server.Network;
using Nyx.Server.Interfaces;
using Nyx.Server.Game.ConquerStructures;
using Nyx.Server.Game;
using Nyx.Server.Client;
using System.Threading.Tasks;
using Albetros.Core;
using System.Text;


namespace Nyx.Server.Client
{
    public class GameClient
    {
        public GameClient(ClientWrapper socket)
        {
            Fake = socket == null;
            if (Fake) socket = new ClientWrapper() { Alive = true };
            Queue = new ConcurrentPacketQueue();
            PacketFilter = new PacketFilter() { { 10010, 10 }, { 10005, 7 }, { 2064, 4 }, { 2032, 3 }, { 1027, 2 } };
            Attackable = false;
            Action = 0;
            _socket = socket;
            Cryptography = new GameCryptography(Encoding.Default.GetBytes(Constants.GameCryptographyKey));
            DHKeyExchange = new Packets.DHKeyExchange.ServerKeyExchange();
            ChiPowers = new List<ChiPowerStructure>();
        }

        public static GameClient CharacterFromName(string p)
        {
            foreach (var x in Kernel.GamePool.Values)
            {
                if (p == x.Entity.Name)
                    return x;
            }
            return null;
        }

        #region Values
        #region Int32
        public int TopGlClaim = 0;
        public int TopDlClaim = 0;
        public int apprtnum = 0;
        public int speedHackSuspiction = 0;
        public int LastJumpTime = 0;
        public int StatHP;
        public int TeamCheerFor;
        public int SMSpinCount;
        public int ArenaState = 0;
        public int DailyEnergy = 0;
        public int MaxEnergy = 120;
        #endregion
        #region UShort
        public ushort OnlineTrainingPoints = 0;
        public ushort PrizePlusStone;
        public ushort PrizeHeavenBlessing;
        public ushort UplevelProficiency;
        uint _Offical;
        uint _Harem;
        uint _Guards;
        public ushort MentorApprenticeRequest;
        #endregion
        #region Time32
        public Time32 DecelerateStamp;
        public Time32 ChaosStamp;
        public Time32 DizzyStamp;
        public Time32 GuardStamp;
        public Time32 RaceExcitementStamp;
        public Time32 FrightenStamp;
        public Time32 CTFUpdateStamp;
        public Time32 LastClientJump;
        public Time32 LastVIPTeamTeleport;
        public Time32 ReviveStamp = Time32.Now;
        public Time32 lastJumpTime = Time32.Now;
        public Time32 lastClientJumpTime = Time32.Now;
        public Time32 XPListStamp = Time32.Now;
        public Time32 XPCountStamp = Time32.Now;
        public Time32 LastVIPTeleport;
        public Time32 CantAttack = Time32.Now;
        public Time32 LastTrainingPointsUp, LastTreasurePoints = Time32.Now.AddMinutes(1);
        public Time32 CoolStamp;
        public Time32 LastMove;
        public Time32 LastAttack;
        public Time32 MiningStamp;
        #endregion
        #region Byte
        public byte Action = 0;
        public byte SelectedGem = 0;
        public byte RoulettePlace { get; set; }
        public byte ExpBalls = 0;
        public byte TinterItemSelect = 0;
        public byte JewelarLauKind;
        public byte JewelarLauGems;
        public byte LotteryEntries;
        public byte SelectedItem;
        public byte UpdateType;
        public byte xpCount;
        public byte KylinUpgradeCount = 0;
        #endregion
        #region Byte Array
        public byte[] SMPacket;
        #endregion
        #region Uint Array
        public uint[] ArsenalDonations;
        #endregion
        #region String
        public string SMCaptcha;
        #endregion
        #region Boolean (false)
        public bool InArenaMatch = false;
        public bool SpiltStack = false;
        public bool IsFairy = false;
        public bool SignedUpForEPK;
        public bool FakeLoaded;
        public bool JustCreated = false;
        public bool JustOpenedDetain;
        public bool WaitingTradePassword;
        public bool WaitingItemUnlockPassword;
        public bool LoggedIn;
        public bool TeamAura;
        public bool Fake;
        public bool AlternateEquipment;
        public bool Attackable;
        public bool WarehouseOpen;
        public bool InLottery;
        public bool RaceExcitement;
        public bool RaceDecelerated;
        public bool RaceGuard;
        public bool RaceDizzy;
        public bool RaceFrightened;
        public bool Disconnected = false;
        public bool ForgetWHPass = false;
        public bool Filtering = false;
        public bool ItemGive = false;
        public bool endteam = false;
        public bool Mining = false;
        public bool DoubleExpToday = false;
        public bool endarena = false;
        public bool WentToComplete = false;
        #endregion
        #region Objects
        public object TimerSyncRoot, ItemSyncRoot;

        #endregion
        #region Boolean (true)
        public bool Exchange = true;
        public bool JustLoggedOn = true;
        public bool DoSetOffline = true;
        #endregion
        #region Short With Value "0"
        public short lastJumpDistance = 0;
        #endregion
        #region Ulong
        public ulong TeamAuraStatusFlag;
        public ulong PrizeExperience;
        #endregion
        #region SmallByte
        public sbyte WarehouseOpenTries;
        #endregion
        #region Method
        public IDisposable[] TimerSubscriptions;
        public Game.ConquerStructures.Team Team;
        public Network.GamePackets.Union.UnionClass Union;
        public List<RouletteStructure> RoulleteBet = new List<RouletteStructure>();
        public RoulettePlayerStructure Roullete = new RoulettePlayerStructure();
        public Quests Quests;
        public Game.ProgressBar ProgressBar;
        public IEnumerable<GameClient> GamePool { get; set; }
        public Database.AccountTable Account;
        public QuizShow.QuizClient Quiz;
        public GameCryptography Cryptography;
        public Network.GamePackets.Interaction Interaction;
        public ActivenessPoints ActivenessPoint;
        public Activeness Activenes;
        public Action<GameClient> OnDisconnect;
        public Game.Enums.Color staticArmorColor;
        public System.Threading.Timer Timer;
        public ClientWrapper Socket { get { return _socket; } }
        public Database.ChiTable.ChiData ChiData;
        public List<ChiPowerStructure> ChiPowers;
        public Nyx.Server.Game.Entity BodyGuard;
        public GameClient PrayLead;
        public Packets.DHKeyExchange.ServerKeyExchange DHKeyExchange;
        public ConcurrentPacketQueue Queue;
        public PacketFilter PacketFilter;
        private ClientWrapper _socket;
        public GameClient LobbyPlayWith;
        public Game.ConquerStructures.Society.Guild Guild;
        public Game.ConquerStructures.Society.Guild.Member AsMember;
        public Game.ConquerStructures.Booth Booth;
        public GameCharacterUpdates SpeedChange;
        public Action<GameClient> MessageOK;
        public Action<GameClient> MessageCancel;
        public Game.Arena.QualifierList.QualifierGroup WatchingGroup;
        public Game.Arena.QualifierList.QualifierGroup QualifierGroup;
        public Network.GamePackets.ArenaStatistic ArenaStatistic;
        public Network.GamePackets.ChampionPoints CP;
        public Game.TeamArena.QualifierList.QualifierGroup TeamWatchingGroup;
        public Game.TeamArena.QualifierList.QualifierGroup TeamQualifierGroup;
        public Network.GamePackets.TeamArenaStatistic TeamArenaStatistic;
        public Nyx.Server.Game.ConquerStructures.Trade Trade;
        public Tuple<ConquerItem, ConquerItem> Weapons;
        public Game.Enums.PKMode PrevPK;
        public Game.UsableRacePotion[] Potions;
        public GameClient TeamAuraOwner;
        public Game.Map map;
        public Game.JiangHu.JiangStages.AtributesType HuTyp { get; set; }
        public Game.ConquerStructures.Inventory Inventory;
        public Game.ConquerStructures.Equipment Equipment;
        public Nyx.Server.Game.ConquerStructures.Society.Mentor Mentor;
        public Nyx.Server.Game.ConquerStructures.Society.Apprentice AsApprentice;
        public Game.ConquerStructures.NobilityInformation NobilityInformation;
        public Game.Entity Entity;
        public Game.Screen Screen;
        public Game.Entity Companion;
        public Interfaces.IPacket ReloadWith;
        public VariableVault Variables;
        public SlotMachine SlotMachine;
        public Network.GamePackets.Trade TradePacket;
        public ItemLock ItemUnlockPacket;
        public ElitePK.FighterStats ElitePKStats;
        public ElitePK.Match ElitePKMatch, WatchingElitePKMatch;
        public Nyx.Server.Database.LotteryTable.LotteryItem LotoItem;
        #endregion
        #region Dictionaries And Lists
        public List<string> GuildNamesSpawned = new List<string>();
        public SafeDictionary<uint, Way2Heroes.StageInfo> Way2Heroes = new SafeDictionary<uint, Way2Heroes.StageInfo>();
        public SafeDictionary<uint, uint> ClamiedReward = new SafeDictionary<uint, uint>(0x3e8);
        public SafeDictionary<uint, Network.GamePackets.Mailbox.PrizeInfo> Prizes = new SafeDictionary<uint, Network.GamePackets.Mailbox.PrizeInfo>(1000);
        public List<GameClient> Prayers;
        public SafeDictionary<ushort, Interfaces.IProf> Proficiencies;
        public SafeDictionary<ushort, Interfaces.ISkill> Spells;
        public SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend> Friends;
        public SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Enemy> Enemy;
        public SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.TradePartner> Partners;
        public SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Apprentice> Apprentices;
        public SafeDictionary<Game.ConquerStructures.Warehouse.WarehouseID, Game.ConquerStructures.Warehouse> Warehouses;
        public static Dictionary<uint, GameClient> BoothingAI = new Dictionary<uint, GameClient>();

        public SafeDictionary<uint, DetainedItem> ClaimableItem = new SafeDictionary<uint, DetainedItem>(1000),
                                                  DeatinedItem = new SafeDictionary<uint, DetainedItem>(1000);
        #endregion
        #region DateTime
        public DateTime timerattack = new DateTime();
        public DateTime VIPDate;
        public DateTime LastDragonBallUse, LastResetTime;
        public DateTime OfflineTGEnterTime;
        public DateTime LastVote;
        #endregion
        #region Object Array
        public object[] OnMessageBoxEventParams;
        #endregion
        #region Uint32
        public uint TeamAuraPower;
        uint _UnionExploits = 0;
        public uint NpcCpsInput;
        public uint InteractionEffect;
        public uint VIPDays;
        public uint WarehousePW;
        public uint RoulletBetAmount { get; set; }
        public uint RoulletWinnigAmount { get; set; }
        public uint Arsenal_Donation = 0;
        public uint ActiveNpc = 0;
        public uint MoneySave = 0;
        public uint _ChiPoints = 0;
        public uint SType = 0;
        public uint FairyType = 0;
        public uint PlayRouletteUID = 0;
        public uint WatchRoulette = 0;
        public uint ForgetWHPassDate = 0;
        public uint RouletteID { get; set; }
        public uint RouletteWatchID { get; set; }
        public uint TempPassword;
        public uint TradePartnerRequest;
        uint _UnionID;
        public uint BlessTime;
        public uint VirtuePoints;
        public uint GuildJoinTarget;
        public uint RaceExcitementAmount;
        public uint RaceExtraVigor;
        #endregion
        #endregion
        public void BreakTouch(GameClient client)
        {
            if (!client.Spells.ContainsKey(12400))
                return;

            if (client.Weapons != null)
                if (client.Weapons.Item2 != null)
                    if (client.Weapons.Item2.ID / 1000 != 619)
                        return;

            var spell = Database.SpellTable.GetSpell(client.Spells[12400].ID, client.Spells[12400].Level);
            if (MyMath.Success((double)30))
            {
                if (Entity.ContainsFlag3(Update.Flags3.lianhuaran04))
                {
                    SpellUse suse = new SpellUse(true);
                    suse.Attacker = Entity.UID;
                    suse.SpellID = spell.ID;
                    suse.SpellLevel = spell.Level;

                    var array = Game.Attacking.Handle.PlayerinRange(Entity, Entity).ToArray();
                    foreach (var target in array)
                    {
                        var attacked = target.Entity;
                        if (attacked.UID == client.Entity.UID)
                            continue;
                        if (Game.Attacking.Handle.CanAttack(client.Entity, attacked, spell, true))
                        {
                            var attack = new Attack(true);
                            attack.Attacker = client.Entity.UID;
                            attack.Attacked = attacked.UID;

                            uint damage = Game.Attacking.Calculate.Magic(client.Entity, attacked, ref attack);

                            attack.Damage = damage;
                            suse.Effect1 = attack.Effect1;
                            suse.Effect1 = attack.Effect1;

                            Game.Attacking.Handle.ReceiveAttack(client.Entity, attacked, attack, ref damage, spell);
                            suse.AddTarget(attacked, damage, attack);
                        }
                    }
                    client.SendScreen(suse, true);

                    Entity.RemoveFlag3(Update.Flags3.lianhuaran01);
                    Entity.RemoveFlag3(Update.Flags3.lianhuaran02);
                    Entity.RemoveFlag3(Update.Flags3.lianhuaran03);
                    Entity.RemoveFlag3(Update.Flags3.lianhuaran04);
                }
                else if (Entity.ContainsFlag3(Update.Flags3.lianhuaran03))
                {
                    Entity.AddFlag3(Update.Flags3.lianhuaran04);
                    Entity.RemoveFlag3(Update.Flags3.lianhuaran03);
                    Entity.lianhuaranPercent = 0.5f;
                }
                else if (Entity.ContainsFlag3(Update.Flags3.lianhuaran02))
                {
                    Entity.AddFlag3(Update.Flags3.lianhuaran03);
                    Entity.RemoveFlag3(Update.Flags3.lianhuaran02);
                    Entity.lianhuaranPercent = 0.4f;
                }
                else if (Entity.ContainsFlag3(Update.Flags3.lianhuaran01))
                {
                    Entity.AddFlag3(Update.Flags3.lianhuaran02);
                    Entity.RemoveFlag3(Update.Flags3.lianhuaran01);
                    Entity.lianhuaranPercent = 0.3f;
                }
                else if (!Entity.ContainsFlag3(Update.Flags3.lianhuaran01))
                {
                    Entity.AddFlag3(Update.Flags3.lianhuaran01);
                    Entity.lianhuaranPercent = 0.1f;
                }
                Entity.lianhuaranStamp = Time32.Now;
                Entity.lianhuaranLeft = 20;

                client.IncreaseSpellExperience(100, 12400);
            }
        }
        public byte UnionType
        {
            set
            {
                Entity.SpawnPacket[287] = value;
                SendScreen(Entity.SpawnPacket, false);
            }
        }
        public uint UnionTitle
        {
            get
            {

                if (Union != null)
                {
                    if (Union.IsKingdom())
                    {
                        return (uint)(Offical + Harem + Guards);
                    }
                }
                return 0;
            }
        }
        public static GameClient GetClientFromName(string name)
        {
            foreach (var clientq in Kernel.GamePool.Values)
            {
                if (clientq.Entity.Name == name)
                {
                    return clientq;
                }
            }
            return null;
        }
        public uint Offical
        {
            get
            {
                return _Offical;
            }
            set
            {

                _Offical = value;
                Entity.Write((ushort)(Offical + Harem + Guards), 278, Entity.SpawnPacket);
                if (Entity.EntityFlag == EntityFlag.Player)
                {
                    Entity.Update((byte)Update.UnionRank, (ushort)(Offical + Harem + Guards), true);
                }
                SendScreen(Entity.SpawnPacket, false);
            }
        }
        public uint Harem
        {
            get
            {

                return _Harem;
            }
            set
            {

                _Harem = value;
                Entity.Write((ushort)(Offical + Harem + Guards), 278, Entity.SpawnPacket);
                if (Entity.EntityFlag == EntityFlag.Player)
                {
                    Entity.Update((byte)Update.UnionRank, (ushort)(Offical + Harem + Guards), true);
                }
                SendScreen(Entity.SpawnPacket, false);
            }
        }
        public uint Guards
        {
            get
            {

                return _Guards;
            }
            set
            {

                _Guards = value;
                Entity.Write((ushort)(Offical + Harem + Guards), 278, Entity.SpawnPacket);
                if (Entity.EntityFlag == EntityFlag.Player)
                {
                    Entity.Update((byte)Update.UnionRank, (ushort)(Offical + Harem + Guards), true);
                }
                SendScreen(Entity.SpawnPacket, false);
            }
        }
        public static GameClient GetClientFromID(uint id)
        {
            foreach (var clientt in Kernel.GamePool.Values)
            {
                if (clientt.Entity.UID == id)
                {
                    return clientt;
                }
            }
            return null;
        }
        public uint UnionID
        {
            get
            {
                return _UnionID;
            }
            set
            {

                _UnionID = value;
                Entity.SpawnPacket[274] = (byte)_UnionID;
                SendScreen(Entity.SpawnPacket, false);
            }
        }
        public byte UnionRank
        {
            set
            {

                Entity.SpawnPacket[286] = value;
                SendScreen(Entity.SpawnPacket, false);
            }
        }
        public uint UnionExploits
        {
            get
            {
                return _UnionExploits;
            }
            set
            {

                _UnionExploits = value;
                byte i = 0;
                if (_UnionExploits >= 200)
                    i += 1;
                if (_UnionExploits >= 500)
                    i += 1;
                if (_UnionExploits >= 1000)
                    i += 1;
                if (_UnionExploits >= 1500)
                    i += 1;
                if (_UnionExploits >= 2100)
                    i += 1;
                if (_UnionExploits >= 2800)
                    i += 1;
                if (_UnionExploits >= 3700)
                    i += 1;
                if (_UnionExploits >= 4700)
                    i += 1;
                if (_UnionExploits >= 6000)
                    i += 1;
                if (_UnionExploits >= 7500)
                    i += 1;
                if (_UnionExploits >= 10000)
                    i += 1;
                if (_UnionExploits >= 15000)
                    i += 1;
                if (_UnionExploits >= 23000)
                    i += 1;
                Entity.SpawnPacket[278] = i;
                if (Entity.EntityFlag == EntityFlag.Player)
                {
                    Entity.Update((byte)Update.MilitaryRank, i, true);
                    Entity.Update((byte)Update.MilitaryExploits, _UnionExploits, true);
                }
            }
        }
        public static bool IsVaildForTeamPk(GameClient Client)
        {
            if (Client.Team != null)
            {
                if (Client.Team.EliteFighterStats != null)
                    if (Client.Team.EliteFighterStats.Flag == Game.Features.Tournaments.TeamElitePk.FighterStats.StatusFlag.Fighting)
                        return true;
            }
            return false;
        }
        public bool LobbySignup
        {
            get { return this["LobbySignup"]; }
            set
            {
                this["LobbySignup"] = value;
            }
        }
        public uint WeaponLook
        {
            get { return this["weaponlook"]; }
            set { this["weaponlook"] = value; }
        }
        public uint WeaponLook2
        {
            get { return this["weaponlook2"]; }
            set { this["weaponlook2"] = value; }
        }
        public void ReshareClan()
        {
            lock (ItemSyncRoot)
            {
                var clan = Entity.GetClan;
            }
        }
        public byte ClanWarPoints
        {
            get { return this["ClanWarPoints"]; }
            set
            {
                this["ClanWarPoints"] = value;
            }
        }
        public uint Appearance
        {
            get { return this["Appearance"]; }
            set
            {
                this["Appearance"] = value;
            }
        }
        public void ReadyToPlay()
        {
            Weapons = new Tuple<ConquerItem, ConquerItem>(null, null);
            ItemSyncRoot = new object();
            Inventory = new Game.ConquerStructures.Inventory(this);
            Equipment = new Game.ConquerStructures.Equipment(this);
            Screen = new Game.Screen(this);
            WarehouseOpen = false;
            WarehouseOpenTries = 0;
            TempPassword = 0;
            ArsenalDonations = new uint[10];
            if (Account != null)
            {

                Warehouses = new SafeDictionary<Game.ConquerStructures.Warehouse.WarehouseID, Game.ConquerStructures.Warehouse>(20);
                Warehouses.Add((Game.ConquerStructures.Warehouse.WarehouseID)this.Account.EntityID, new Game.ConquerStructures.Warehouse(this, (Game.ConquerStructures.Warehouse.WarehouseID)this.Account.EntityID, 300));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.TwinCity, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.TwinCity));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.PhoenixCity, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.PhoenixCity));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.ApeCity, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.ApeCity));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.DesertCity, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.DesertCity));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.BirdCity, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.BirdCity));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.StoneCity, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.StoneCity));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.Market, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.Market));
                Warehouses.Add(Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.Poker, new Game.ConquerStructures.Warehouse(this, Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID.Poker));
                if (Kernel.Furnitures[(uint)(this.Account.EntityID + 1000000)] != null)
                {
                    Warehouses.Add((Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID)(uint)(this.Account.EntityID + 1000000), new Game.ConquerStructures.Warehouse(this, (Nyx.Server.Game.ConquerStructures.Warehouse.WarehouseID)(uint)(this.Account.EntityID + 1000000)));
                }
               
            }
            Trade = new Game.ConquerStructures.Trade();
            ArenaStatistic = new ArenaStatistic(true);
            CP = new ChampionPoints();
            ActivenessPoint = new ActivenessPoints();
            Activenes = new Activeness(this);
            Prayers = new List<GameClient>();
            map = null;
            Quests = new Quests(this);
        }
        public void Send(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return;
            if (Fake) return;
            // Use pooled buffer to reduce allocations and GC pressure
            var _buffer = System.Buffers.ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                Buffer.BlockCopy(buffer, 0, _buffer, 0, buffer.Length);
            var id = BitConverter.ToUInt16(_buffer, 2);
            Network.SafeWriter.Write(Constants.ServerKey, buffer.Length - 8, _buffer);
            try
            {
                lock (_socket)
                {
                    if (!_socket.Alive) return;
                    lock (Cryptography)
                    {
                        Cryptography.Encrypt(_buffer, buffer.Length);
                        var sendArr = new byte[buffer.Length];
                        Buffer.BlockCopy(_buffer, 0, sendArr, 0, buffer.Length);
                        _socket.Send(sendArr);
                    }
                }
            }
            catch (Exception)
            {
                _socket.Alive = false;
                Disconnect();
            }
            }
            finally
            {
                System.Buffers.ArrayPool<byte>.Shared.Return(_buffer);
            }
        }
        private void EndSend(IAsyncResult res)
        {
            try
            {
                _socket.Socket.EndSend(res);
            }
            catch
            {
                _socket.Alive = false;
                Disconnect();
            }
        }
        public void Send(Interfaces.IPacket buffer)
        {
            Send(buffer.Encode());
        }
        public void SendScreenSpawn(Interfaces.IMapObject obj, bool self)
        {
            foreach (Interfaces.IMapObject _obj in Screen.Objects)
            {
                if (_obj == null)
                    continue;
                if (_obj.UID != Entity.UID)
                {
                    if (_obj.MapObjType == Game.MapObjectType.Player)
                    {
                        GameClient client = _obj.Owner as GameClient;
                        obj.SendSpawn(client, false);
                    }
                }
            }
            if (self)
                obj.SendSpawn(this);

        }
        public void RemoveScreenSpawn(Interfaces.IMapObject obj, bool self)
        {
            if (Screen == null) return;
            if (Screen.Objects == null) return;
            foreach (Interfaces.IMapObject _obj in Screen.Objects)
            {
                if (_obj == null) continue;
                if (obj == null) continue;
                if (_obj.UID != Entity.UID)
                {
                    if (_obj.MapObjType == Game.MapObjectType.Player)
                    {
                        GameClient client = _obj.Owner as GameClient;
                        client.Screen.Remove(obj);
                    }
                }
            }
            if (self)
                Screen.Remove(obj);

        }
        public void SendScreen(byte[] buffer, bool self = true)
        {
            foreach (Interfaces.IMapObject obj in Screen.Objects)
            {
                if (obj == null) continue;
                if (obj.UID != Entity.UID)
                {
                    if (obj.MapObjType == Game.MapObjectType.Player)
                    {
                        GameClient client = obj.Owner as GameClient;
                        if (WatchingGroup != null && client.WatchingGroup == null)
                            continue;
                        client.Send(buffer);
                    }
                }
            }
            if (self)
                Send(buffer);

        }
        public readonly DateTime MemberLastLogin = new DateTime();
        public uint MemberLastLogin2
        {
            get { return (uint)(DateTime.UtcNow - MemberLastLogin).TotalSeconds; }
        }
        public void SendScreen(Interfaces.IPacket buffer, bool self = true, bool message = false)
        {
            if (Screen == null) return;
            if (Screen.Objects == null) return;
            foreach (Interfaces.IMapObject obj in Screen.Objects)
            {
                if (obj == null)
                    continue;
                if (obj.MapObjType == Game.MapObjectType.Player)
                {
                    GameClient client = obj.Owner as GameClient;
                    if (message && client.Entity.BlackList.Contains(Entity.Name)) continue;
                    if (client.Entity.UID != Entity.UID)
                        client.Send(buffer);
                }
            }
            if (self)
                Send(buffer);
        }
        public void Disconnect(bool save = true)
        {
            if (Fake) return;
            if (this == null)
                return;
            if (this.Entity == null)
                return;
            if (Screen != null) Screen.DisposeTimers();

            Program.World.Unregister(this);
            if (OnDisconnect != null) OnDisconnect(this);
            if (_socket.Connector != null)
            {
                _socket.Disconnect();
                if (save)
                {
                    ShutDown();
                }
            }
        }
        private void ShutDown()
        {
            if (Socket.Connector == null) return;
            Socket.Connector = null;
            if (this.Entity != null)
            {
                try
                {
                    if (Entity.MyClones.Count != 0)
                    {
                        foreach (var clone in Entity.MyClones)
                            clone.RemoveThat();
                        Entity.MyClones.Clear();
                    }
                    if (Fake) return;
                    if (this.JustCreated) return;
                    Time32 now = Time32.Now;
                    Kernel.DisconnectPool.Add(this.Entity.UID, this);
                    RemoveScreenSpawn(this.Entity, false);
                    using (var conn = Database.DataHolder.MySqlConnection)
                    {
                        Database.JiangHu.SaveJiangHu();
                        conn.Open();
                        Database.EntityTable.UpdateOnlineStatus(this, false);
                        Database.EntityTable.SaveEntity(this, conn);
                        Database.ActivenessTable.Save(this);
                        Database.EntityVariableTable.Save(this, conn);
                        Database.Flowers.SaveFlowers();
                        Database.MailboxTable.Save(this);
                        Database.SkillTable.SaveProficiencies(this, conn);
                        Database.SkillTable.SaveSpells(this, conn);
                        Database.DailyQuestTable.Save(this);
                        Database.ChiTable.Save(this);
                        Database.ArenaTable.SaveArenaStatistics(this.ArenaStatistic,this.CP, conn);
                        Database.TeamArenaTable.SaveArenaStatistics(this.TeamArenaStatistic, conn);
                        Database.KingdomMissionTable.Save(this, conn);

                    }
                    Kernel.GamePool.Remove(this.Entity.UID);
                    if (this.RouletteID != 0)
                    {
                        byte[] packett = new byte[]
                        {
                            0x05, 0x00, 0xf4, 0x0a, 0x02, 0x54,
                            0x51, 0x43, 0x6c, 0x69, 0x65, 0x6e, 0x74
                        };
                        PacketHandler.HandlePacket(packett, this);
                    }
                    if (Booth != null)
                        Booth.Remove();
                    if (Quests != null)
                        Quests.Save();
                    if (Companion != null)
                    {
                        Map.RemoveEntity(Companion);
                        Data data = new Data(true);
                        data.UID = Companion.UID;
                        data.ID = Data.RemoveEntity;
                        Companion.MonsterInfo.SendScreen(data);
                    }
                    if (QualifierGroup != null)
                        QualifierGroup.End(this);
                    if (TeamQualifierGroup != null)
                        TeamQualifierGroup.CheckEnd(this, true);



                    Game.Arena.Clear(this);
                    Game.TeamArena.Clear(this);

                    RemoveScreenSpawn(this.Entity, false);
                    #region ChangeName
                    string name200 = Entity.Name;
                    string name300 = Entity.NewName;
                    if (Entity.NewName != "")
                    {
                        if (Entity.NewName != "")
                        {
                            Database.MySqlCommand cmdupdate = null;
                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("apprentice").Set("MentorName", Entity.NewName).Where("MentorID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("apprentice").Set("ApprenticeName", Entity.NewName).Where("ApprenticeID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("arena").Set("EntityName", Entity.NewName).Where("EntityID", Entity.UID).Execute();



                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("enemy").Set("EnemyName", Entity.NewName).Where("EnemyID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("friends").Set("FriendName", Entity.NewName).Where("FriendID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("guilds").Set("Name", Entity.NewName).Where("Name", Entity.Name).Execute();


                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("guilds").Set("LeaderName", Entity.NewName).Where("LeaderName", Entity.Name).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("skillteampk").Set("Name", Entity.NewName).Where("UID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("teampk").Set("Name", Entity.NewName).Where("UID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("nobility").Set("EntityName", Entity.NewName).Where("EntityUID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("partners").Set("PartnerName", Entity.NewName).Where("PartnerID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("chi").Set("name", Entity.NewName).Where("uid", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("teamarena").Set("EntityName", Entity.NewName).Where("EntityID", Entity.UID).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("entities").Set("Spouse", Entity.NewName).Where("Spouse", Entity.Name).Execute();

                            cmdupdate = new Database.MySqlCommand(Database.MySqlCommandType.UPDATE);
                            cmdupdate.Update("entities").Set("Name", Entity.NewName).Where("Name", Entity.Name).Execute();

                            if (Game.ConquerStructures.Nobility.Board.ContainsKey(Entity.UID))
                            {
                                Game.ConquerStructures.Nobility.Board[Entity.UID].Name = Entity.NewName;
                            }
                            if (Arena.ArenaStatistics.ContainsKey(Entity.UID))
                            {
                                Arena.ArenaStatistics[Entity.UID].Name = Entity.NewName;
                            }
                            if (Guild != null)
                            {
                                if (Guild.LeaderName == name200)
                                {
                                    Kernel.Guilds[Guild.ID].LeaderName = Entity.NewName;
                                    Kernel.Guilds[Guild.ID].Members[Entity.UID].Name = Entity.NewName;
                                }
                            }
                            if (Entity.ClanId != 0 && Entity.Myclan != null)
                            {
                                if (Entity.Myclan.LeaderName == name200)
                                {
                                    Kernel.Clans[Entity.ClanId].LeaderName = Entity.NewName;
                                    Kernel.Clans[Entity.ClanId].Members[Entity.UID].Name = Entity.NewName;
                                }
                            }
                        }
                    }
                    #endregion

                    #region Friend/TradePartner/Apprentice
                    Network.GamePackets.Message msg = new Network.GamePackets.Message("Your friend, " + Entity.Name + ", has logged off.", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft);
                    if (Friends == null)
                    {
                        if (Entity.VIPLevel == 1)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(55);
                        else if (Entity.VIPLevel == 2)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(60);
                        else if (Entity.VIPLevel == 3)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(70);
                        else if (Entity.VIPLevel == 4)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(80);
                        else if (Entity.VIPLevel == 5)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(90);
                        else if (Entity.VIPLevel == 6)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(100);
                        else if (Entity.VIPLevel == 0)
                            Friends = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Friend>(50);
                    }
                    MentorInformation Information = new MentorInformation(true);
                    Information.Mentor_Type = 1;
                    Information.Mentor_ID = Entity.UID;
                    Information.Mentor_Level = Entity.Level;
                    Information.Mentor_Class = Entity.Class;
                    Information.Mentor_PkPoints = Entity.PKPoints;
                    Information.Mentor_Mesh = Entity.Mesh;
                    Information.Mentor_Online = false;
                    Information.String_Count = 3;
                    Information.Mentor_Name = Entity.Name;
                    Information.Mentor_Spouse_Name = Entity.Spouse;
                    foreach (Game.ConquerStructures.Society.Friend friend in Friends.Values)
                    {
                        if (friend.IsOnline)
                        {
                            var packet = new Nyx.Server.Network.GamePackets.KnownPersons(true)
                            {
                                UID = Entity.UID,
                                Type = KnownPersons.RemovePerson,
                                Name = Entity.Name,
                                Online = false
                            };
                            friend.Client.Send(packet);
                            if (Entity.NewName != "")
                            {
                                if (friend.Client.Friends.ContainsKey(Entity.UID))
                                {
                                    friend.Client.Friends[Entity.UID].Name = Entity.NewName;
                                }
                            }
                            packet.Type = KnownPersons.AddFriend;
                            if (friend != null)
                            {
                                if (friend.Client != null)
                                {
                                    friend.Client.Send(packet);
                                    friend.Client.Send(msg);
                                }
                            }
                        }
                    }
                    Network.GamePackets.Message msg2 = new Network.GamePackets.Message("Your partner, " + Entity.Name + ", has logged off.", System.Drawing.Color.Red, Network.GamePackets.Message.TopLeft);

                    if (Partners != null)
                    {
                        foreach (Game.ConquerStructures.Society.TradePartner partner in Partners.Values)
                        {
                            if (partner.IsOnline)
                            {
                                var packet = new TradePartner(true)
                                {
                                    UID = Entity.UID,
                                    Type = TradePartner.BreakPartnership,
                                    Name = Entity.Name,
                                    HoursLeft = (int)(new TimeSpan(partner.ProbationStartedOn.AddDays(3).Ticks).TotalHours - new TimeSpan(DateTime.Now.Ticks).TotalHours),
                                    Online = false
                                };
                                partner.Client.Send(packet);
                                if (Entity.NewName != "")
                                {
                                    if (partner.Client.Partners.ContainsKey(Entity.UID))
                                    {
                                        partner.Client.Partners[Entity.UID].Name = Entity.NewName;
                                    }
                                }
                                packet.Type = TradePartner.AddPartner;
                                if (partner != null)
                                {
                                    if (partner.Client != null)
                                    {
                                        partner.Client.Send(packet);
                                        partner.Client.Send(msg2);
                                    }
                                }
                            }
                        }
                    }

                    foreach (var appr in Apprentices.Values)
                    {
                        if (appr.IsOnline)
                        {
                            Information.Apprentice_ID = appr.ID;
                            Information.Enrole_Date = appr.EnroleDate;
                            Information.Apprentice_Name = appr.Name;
                            appr.Client.Send(Information);
                            appr.Client.ReviewMentor();
                        }
                    }

                    if (Mentor != null)
                    {
                        if (Mentor.IsOnline)
                        {
                            ApprenticeInformation AppInfo = new ApprenticeInformation();
                            AppInfo.Apprentice_ID = Entity.UID;
                            AppInfo.Apprentice_Level = Entity.Level;
                            AppInfo.Apprentice_Name = Entity.Name;
                            AppInfo.Apprentice_Online = false;
                            AppInfo.Apprentice_Spouse_Name = Entity.Spouse;
                            AppInfo.Enrole_date = Mentor.EnroleDate;
                            AppInfo.Mentor_ID = Mentor.Client.Entity.UID;
                            AppInfo.Mentor_Mesh = Mentor.Client.Entity.Mesh;
                            AppInfo.Mentor_Name = Mentor.Client.Entity.Name;
                            AppInfo.Type = 2;
                            Mentor.Client.Send(AppInfo);
                        }
                    }

                    #endregion
                    if (Team != null)
                    {
                        Team.Remove(this, true);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    Program.UpdateConsoleTitle();
                    Kernel.DisconnectPool.Remove(this.Entity.UID);
                }
            }
        }
        public string IP
        {
            get
            {
                return _socket.IP;
            }
        }
        public uint ChiPoints
        {
            get { return _ChiPoints; }
            set
            {
                if (ChiData != null)
                {
                    ChiData.ChiPoints = value;

                }
                _ChiPoints = value;
            }
        }
        public uint BackupArmorLook
        {
            get { return this["bkparmorlook"]; }
            set { this["bkparmorlook"] = value; }
        }
        public uint ArmorLook
        {
            get { return this["armorlook"]; }
            set { this["armorlook"] = value; }
        }
        public uint HeadgearLook
        {
            get { return this["headgearlook"]; }
            set { this["headgearlook"] = value; }
        }
        public bool ValidArmorLook(uint id)
        {
            if (id == 0) return false;
            var soulInfo = Database.AddingInformationTable.SoulGearItems[id];
            if (id >= 800000 && id < 900000)
            {
                if (soulInfo.ItemIdentifier < 100)
                    if (soulInfo.ItemIdentifier != ConquerItem.Armor)
                        return false;
                    else { }
                else
                    if (Network.PacketHandler.ItemPosition((uint)(soulInfo.ItemIdentifier * 1000)) != ConquerItem.Armor)
                        return false;
            }
            else
                if (Network.PacketHandler.ItemPosition(id) != ConquerItem.Armor)
                    return false;
            return true;
        }
        public bool ValidHeadgearLook(uint id)
        {
            if (id == 0) return false;
            var soulInfo = Database.AddingInformationTable.SoulGearItems[id];
            if (id >= 800000 && id < 900000)
            {
                if (soulInfo.ItemIdentifier < 100)
                    if (soulInfo.ItemIdentifier != ConquerItem.Head)
                        return false;
                    else { }
                else
                    if (Network.PacketHandler.ItemPosition((uint)(soulInfo.ItemIdentifier * 1000)) != ConquerItem.Head)
                        return false;
            }
            else
                if (Network.PacketHandler.ItemPosition(id) != ConquerItem.Head)
                    return false;
            return true;
        }
        public void SetNewArmorLook(uint id)
        {
            ArmorLook = id;
            if (!ValidArmorLook(id)) return;

            var iu = new Network.GamePackets.ItemUsage(true);
            iu.UID = uint.MaxValue - 1;
            iu.dwParam = 13;
            iu.ID = Network.GamePackets.ItemUsage.UnequipItem;
            Send(iu);
            iu = new Network.GamePackets.ItemUsage(true);
            iu.UID = uint.MaxValue - 1;
            iu.ID = Network.GamePackets.ItemUsage.RemoveInventory;
            Send(iu);

            ConquerItem fakeItem = new Network.GamePackets.ConquerItem(true);
            fakeItem.ID = id;
            fakeItem.Durability = 1;
            fakeItem.MaximDurability = 1;
            fakeItem.Color = (Game.Enums.Color)Kernel.Random.Next(4, 8);
            fakeItem.UID = uint.MaxValue - 1;
            fakeItem.Position = 13;
            Send(fakeItem);
            fakeItem.Mode = Enums.ItemMode.Update;
            Send(fakeItem);
            ClientEquip eqs = new ClientEquip();
            eqs.DoEquips(this);
            Send(eqs);
            Equipment.UpdateEntityPacket();
        }
        public void SetNewHeadgearLook(uint id)
        {
            HeadgearLook = id;
            if (!ValidHeadgearLook(id)) return;

            var iu = new Network.GamePackets.ItemUsage(true);
            iu.UID = uint.MaxValue - 2;
            iu.dwParam = 14;
            iu.ID = Network.GamePackets.ItemUsage.UnequipItem;
            Send(iu);
            iu = new Network.GamePackets.ItemUsage(true);
            iu.UID = uint.MaxValue - 2;
            iu.ID = Network.GamePackets.ItemUsage.RemoveInventory;
            Send(iu);

            ConquerItem fakeItem = new Network.GamePackets.ConquerItem(true);
            fakeItem.ID = id;
            fakeItem.Durability = 1;
            fakeItem.MaximDurability = 1;
            fakeItem.Color = (Game.Enums.Color)Kernel.Random.Next(4, 8);
            fakeItem.UID = uint.MaxValue - 2;
            fakeItem.Position = 14;
            Send(fakeItem);
            fakeItem.Mode = Enums.ItemMode.Update;
            Send(fakeItem);
            ClientEquip eqs = new ClientEquip();
            eqs.DoEquips(this);
            Send(eqs);
            Equipment.UpdateEntityPacket();
        }
        public string PromoteItemNameNeed
        {
            get
            {
                if (Entity.Class % 10 == 0)
                    return " nothing but";
                if (Entity.Class % 10 == 1)
                    if (Entity.Class / 10 == 4)
                        return " five Euxenite Ores and";
                    else
                        return " nothing but";
                if (Entity.Class % 10 == 2)
                    return " one Emerald and";
                if (Entity.Class % 10 == 3)
                    return " one Meteor and";
                if (Entity.Class % 10 == 4)
                    return " one MoonBox and";
                return " nothing but";
            }
        }
        public byte PromoteItemCountNeed
        {
            get
            {
                if (Entity.Class % 10 == 0)
                    return 0;
                if (Entity.Class % 10 == 1)
                    if (Entity.Class / 10 == 4)
                        return 5;
                    else
                        return 0;
                if (Entity.Class % 10 == 2)
                    return 1;
                if (Entity.Class % 10 == 3)
                    return 1;
                if (Entity.Class % 10 == 4)
                    return 1;
                return 0;
            }
        }
        public uint PromoteItemNeed
        {
            get
            {
                if (Entity.Class % 10 == 0)
                    return 0;
                if (Entity.Class % 10 == 1)
                    if (Entity.Class / 10 == 4)
                        return 1072031;
                    else
                        return 0;
                if (Entity.Class % 10 == 2)
                    return 1080001;
                if (Entity.Class % 10 == 3)
                    return 1088001;
                if (Entity.Class % 10 == 4)
                    return 721020;
                return 0;
            }
        }
        public uint PromoteItemGain
        {
            get
            {
                if (Entity.Class % 10 == 0)
                    return 0;
                if (Entity.Class % 10 == 1)
                    if (Entity.Class / 10 == 4)
                        return 500067;
                    else
                        return 0;
                if (Entity.Class % 10 == 2)
                    return 0;
                if (Entity.Class % 10 == 4)
                    return 1088000;
                return 0;
            }
        }
        public uint PromoteLevelNeed
        {
            get
            {
                if (Entity.Class % 10 == 0)
                    return 15;
                if (Entity.Class % 10 == 1)
                    return 40;
                if (Entity.Class % 10 == 2)
                    return 70;
                if (Entity.Class % 10 == 3)
                    return 100;
                if (Entity.Class % 10 == 4)
                    return 110;
                return 0;
            }
        }
        public void ApplyRacePotion(Enums.RaceItemType type, uint target)
        {
            switch (type)
            {
                case Enums.RaceItemType.FrozenTrap:
                    {
                        if (target != uint.MaxValue)
                        {
                            if (Map.Floor[Entity.X, Entity.Y, MapObjectType.StaticEntity])
                            {
                                StaticEntity item = new StaticEntity((uint)(Entity.X * 1000 + Entity.Y), Entity.X, Entity.Y, (ushort)Map.ID);
                                item.DoFrozenTrap(Entity.UID);
                                Map.AddStaticEntity(item);
                                Kernel.SendSpawn(item);
                            }
                        }
                        else
                        {
                            Entity.FrozenStamp = Time32.Now;
                            Entity.FrozenTime = 5;
                            GameCharacterUpdates update = new GameCharacterUpdates(true);
                            update.UID = Entity.UID;
                            update.Add(GameCharacterUpdates.Freeze, 0, 4);
                            SendScreen(update, true);
                            Entity.AddFlag(Update.Flags.Freeze);
                        }
                        break;
                    }
                case Enums.RaceItemType.RestorePotion:
                    {
                        this.Entity.Vigor += 2000;
                        if (this.Entity.Vigor > (ushort)this.Entity.MaxVigor)
                            this.Entity.Vigor = (ushort)this.Entity.MaxVigor;
                        Send(new Vigor(true) { Amount = this.Entity.Vigor });
                        break;
                    }
                case Enums.RaceItemType.ExcitementPotion:
                    {
                        if (RaceExcitement && RaceExcitementAmount > 50)
                            return;

                        if (RaceDecelerated)
                        {
                            RaceDecelerated = false;

                            var upd = new GameCharacterUpdates(true);
                            upd.UID = Entity.UID;
                            upd.Remove(GameCharacterUpdates.Decelerated);
                            SendScreen(upd, true);
                        }
                        RaceExcitementStamp = Time32.Now;
                        RaceExcitement = true;
                        {
                            var upd = new GameCharacterUpdates(true);
                            upd.UID = Entity.UID;
                            upd.Add(GameCharacterUpdates.Accelerated, 50, 15, 25);
                            SendScreen(upd, true);
                            SpeedChange = upd;
                        }
                        RaceExcitementAmount = 50;
                        Entity.AddFlag(Update.Flags.OrangeSparkles);
                        break;
                    }
                case Enums.RaceItemType.SuperExcitementPotion:
                    {
                        if (RaceDecelerated)
                        {
                            RaceDecelerated = false;

                            var upd = new GameCharacterUpdates(true);
                            upd.UID = Entity.UID;
                            upd.Remove(GameCharacterUpdates.Decelerated);
                            SendScreen(upd, true);
                        }
                        RaceExcitementAmount = 200;
                        RaceExcitementStamp = Time32.Now;
                        RaceExcitement = true;
                        this.Entity.AddFlag(Update.Flags.SpeedIncreased);
                        {
                            var upd = new GameCharacterUpdates(true);
                            upd.UID = Entity.UID;
                            upd.Add(GameCharacterUpdates.Accelerated, 200, 15, 100);
                            SendScreen(upd, true);
                            SpeedChange = upd;
                        }
                        Entity.AddFlag(Update.Flags.OrangeSparkles);
                        break;
                    }
                case Enums.RaceItemType.GuardPotion:
                    {
                        RaceGuard = true;
                        GuardStamp = Time32.Now;
                        Entity.AddFlag(Update.Flags.DivineShield);
                        DizzyStamp = DizzyStamp.AddSeconds(-100);
                        FrightenStamp = FrightenStamp.AddSeconds(-100);
                        var upd = new GameCharacterUpdates(true);
                        upd.UID = Entity.UID;
                        upd.Add(GameCharacterUpdates.DivineShield, 0, 10);
                        SendScreen(upd, true);
                        break;
                    }
                case Enums.RaceItemType.DizzyHammer:
                    {
                        Entity Target;
                        if (Screen.TryGetValue(target, out Target))
                        {
                            var Owner = Target.Owner;
                            if (Owner != null)
                            {
                                if (!Owner.RaceGuard && !Owner.RaceFrightened)
                                {
                                    Owner.DizzyStamp = Time32.Now;
                                    Owner.RaceDizzy = true;
                                }
                            }
                        }
                        break;
                    }
                case Enums.RaceItemType.ScreamBomb:
                    {
                        SendScreen(new SpellUse(true)
                        {
                            Attacker = Entity.UID,
                            SpellID = 9989,
                            SpellLevel = 0,
                            X = Entity.X,
                            Y = Entity.Y
                        }.AddTarget(Entity, 0, null), true);
                        foreach (var obj in Screen.SelectWhere<Entity>(MapObjectType.Player,
                            (o) => Kernel.GetDistance(o.X, o.Y, Entity.X, Entity.Y) <= 10))
                        {
                            var Owner = obj.Owner;
                            if (!Owner.RaceGuard && !Owner.RaceDizzy)
                            {
                                Owner.RaceFrightened = true;
                                Owner.FrightenStamp = Time32.Now;
                            }
                        }
                        break;
                    }
                case Enums.RaceItemType.SpiritPotion:
                    {
                        RaceExtraVigor = 2000;
                        break;
                    }
                case Enums.RaceItemType.ChaosBomb:
                    {
                        SendScreen(new SpellUse(true)
                        {
                            Attacker = Entity.UID,
                            SpellID = 9989,
                            SpellLevel = 0,
                            X = Entity.X,
                            Y = Entity.Y
                        }.AddTarget(Entity, 0, null), true);
                        foreach (var obj in this.Screen.SelectWhere<Entity>(MapObjectType.Player,
                               (o) => Kernel.GetDistance(o.X, o.Y, Entity.X, Entity.Y) <= 10))
                        {
                            var Owner = obj.Owner;
                            if (!Owner.RaceGuard)
                            {
                                Owner.FrightenStamp = Time32.Now;
                                Owner.DizzyStamp = Owner.DizzyStamp.AddSeconds(-1000);
                            }
                        }
                        break;
                    }
                case Enums.RaceItemType.SluggishPotion:
                    {
                        SendScreen(new SpellUse(true)
                        {
                            Attacker = Entity.UID,
                            SpellID = 9989,
                            SpellLevel = 0,
                            X = Entity.X,
                            Y = Entity.Y
                        }.AddTarget(Entity, 0, null), true);
                        foreach (var obj in this.Screen.SelectWhere<Entity>(MapObjectType.Player,
                              o => Kernel.GetDistance(o.X, o.Y, Entity.X, Entity.Y) <= 10))
                        {
                            var Owner = obj.Owner;
                            if (!Owner.RaceGuard)
                            {
                                Owner.RaceDecelerated = true;
                                Owner.DecelerateStamp = Time32.Now;
                                if (Owner.RaceExcitement)
                                {
                                    Owner.RaceExcitement = false;

                                    var upd = new GameCharacterUpdates(true);
                                    upd.UID = Owner.Entity.UID;
                                    upd.Remove(GameCharacterUpdates.Accelerated);
                                    Owner.SendScreen(upd, true);
                                }
                                Owner.Entity.AddFlag(Update.Flags.PurpleSparkles);
                                {
                                    var upd = new GameCharacterUpdates(true);
                                    upd.UID = Owner.Entity.UID;
                                    unchecked { upd.Add(GameCharacterUpdates.Decelerated, 50, 10, (uint)(0 - 25)); }
                                    Owner.SendScreen(upd, true);
                                    Owner.SpeedChange = upd;
                                }
                            }
                        }
                        break;
                    }
                case Enums.RaceItemType.TransformItem:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (Potions[i] != null)
                            {
                                if (Potions[i].Type != Enums.RaceItemType.TransformItem)
                                {
                                    Send(new RacePotion(true)
                                    {
                                        Amount = 0,
                                        Location = i + 1,
                                        PotionType = Potions[i].Type
                                    });
                                    Potions[i] = null;
                                }
                            }
                        }
                        //for (int i = 0; i < 5; i++)
                        {
                            int i = 0;
                            if (Potions[i] == null)
                            {
                                int val = (int)Enums.RaceItemType.TransformItem;
                                while (val == (int)Enums.RaceItemType.TransformItem)
                                    val = Kernel.Random.Next((int)Enums.RaceItemType.ChaosBomb, (int)Enums.RaceItemType.SuperExcitementPotion);
                                Potions[i] = new UsableRacePotion();
                                Potions[i].Count = 1;
                                Potions[i].Type = (Enums.RaceItemType)val;
                                Send(new RacePotion(true)
                                {
                                    Amount = 1,
                                    Location = i + 1,
                                    PotionType = Potions[i].Type
                                });
                            }
                        }
                        break;
                    }
            }
        }
        public void ReviewMentor()
        {
            #region NotMentor
            uint nowBP = 0;
            if (Mentor != null)
            {
                if (Mentor.IsOnline)
                {
                    nowBP = Entity.BattlePowerFrom(Mentor.Client.Entity);
                }
            }
            if (nowBP > 200) nowBP = 0;
            if (nowBP < 0) nowBP = 0;
            if (Entity.MentorBattlePower != nowBP)
            {
                Entity.MentorBattlePower = nowBP;
                if (Mentor != null)
                {
                    if (Mentor.IsOnline)
                    {
                        MentorInformation Information = new MentorInformation(true);
                        Information.Mentor_Type = 1;
                        Information.Mentor_ID = Mentor.Client.Entity.UID;
                        Information.Apprentice_ID = Entity.UID;
                        Information.Enrole_Date = Mentor.EnroleDate;
                        Information.Mentor_Level = Mentor.Client.Entity.Level;
                        Information.Mentor_Class = Mentor.Client.Entity.Class;
                        Information.Mentor_PkPoints = Mentor.Client.Entity.PKPoints;
                        Information.Mentor_Mesh = Mentor.Client.Entity.Mesh;
                        Information.Mentor_Online = true;
                        Information.Shared_Battle_Power = nowBP;
                        Information.String_Count = 3;
                        Information.Mentor_Name = Mentor.Client.Entity.Name;
                        Information.Apprentice_Name = Entity.Name;
                        Information.Mentor_Spouse_Name = Mentor.Client.Entity.Spouse;
                        Send(Information);
                    }
                }
            }
            #endregion
            #region Mentor
            if (Apprentices == null) Apprentices = new SafeDictionary<uint, Game.ConquerStructures.Society.Apprentice>();
            foreach (var appr in Apprentices.Values)
            {
                if (appr.IsOnline)
                {
                    uint nowBPs = 0;
                    nowBPs = appr.Client.Entity.BattlePowerFrom(Entity);
                    if (appr.Client.Entity.MentorBattlePower != nowBPs)
                    {
                        appr.Client.Entity.MentorBattlePower = nowBPs;
                        MentorInformation Information = new MentorInformation(true);
                        Information.Mentor_Type = 1;
                        Information.Mentor_ID = Entity.UID;
                        Information.Apprentice_ID = appr.Client.Entity.UID;
                        Information.Enrole_Date = appr.EnroleDate;
                        Information.Mentor_Level = Entity.Level;
                        Information.Mentor_Class = Entity.Class;
                        Information.Mentor_PkPoints = Entity.PKPoints;
                        Information.Mentor_Mesh = Entity.Mesh;
                        Information.Mentor_Online = true;
                        Information.Shared_Battle_Power = nowBPs;
                        Information.String_Count = 3;
                        Information.Mentor_Name = Entity.Name;
                        Information.Apprentice_Name = appr.Client.Entity.Name;
                        Information.Mentor_Spouse_Name = Entity.Spouse;
                        appr.Client.Send(Information);
                    }
                }
            }
            #endregion

        }
        public void AddGl()
        {
            TopGlClaim++;
            return;
        }
        public void AddDl()
        {
            TopDlClaim++;
            return;
        }
        public void AddBless(uint value)
        {
            Entity.HeavenBlessing += value;
            Entity.Update(Network.GamePackets._String.Effect, "bless", true);
            if (Mentor != null)
            {
                if (Mentor.IsOnline)
                {
                    Mentor.Client.PrizeHeavenBlessing += (ushort)(value / 10 / 60 / 60);
                    AsApprentice = Mentor.Client.Apprentices[Entity.UID];
                }
                if (AsApprentice != null)
                {
                    AsApprentice.Actual_HeavenBlessing += (ushort)(value / 10 / 60 / 60);
                    AsApprentice.Total_HeavenBlessing += (ushort)(value / 10 / 60 / 60);
                }
            }
        }
        public uint ArenaPoints
        {
            get
            {
                return ArenaStatistic.ArenaPoints;
            }
            set
            {
                ArenaStatistic.ArenaPoints =
                    TeamArenaStatistic.ArenaPoints =
                    value;
            }
        }
        public uint ChampionPoints
        {
            get
            {
                return CP.Points;
            }
            set
            {
                uint old = CP.Points;
                if (CP.TodayPoints < value)
                    CP.TodayPoints = value;
                if (CP.TodayPoints > 650)
                {
                    CP.TodayPoints = 650;
                    if (Entity.FullyLoaded)
                        Send(new Network.GamePackets.Message("Your Champion Points have reached the max amount of 650 points. You can collect more points tommorrow.", Color.Red, Network.GamePackets.Message.System));
                }
                else
                {
                    CP.Points =
                   value;
                    if (old < ChampionPoints)
                        CP.AllChampionPoints += (ChampionPoints - old);
                    if (Entity.FullyLoaded && old < ChampionPoints)
                        Send(new Network.GamePackets.Message("You received " + (ChampionPoints - old) + " Champion Points!", Color.Red, Network.GamePackets.Message.System));
                }
                if (Entity.LoadedActivenessSystem)
                {
                    Activenes.SendSinglePacket(this, Activeness.Types.ChampionPTask, 1);
                }
            }
        }
        public ushort ActivenessPoints
        {
            get
            {
                return ActivenessPoint.Points;
            }
            set
            {
                ActivenessPoint.Points =
                    value;
            }
        }
        public byte XPCount
        {
            get { return xpCount; }
            set
            {
                xpCount = value;
                if (xpCount >= 100) xpCount = 100;

                Update update = new Update(true);
                update.UID = Entity.UID;
                update.Append(Update.XPCircle, xpCount);
                update.Send(this);
            }
        }
        public Game.Map Map
        {
            get
            {
                if (map == null)
                {
                    if (Entity == null) return null;
                    Kernel.Maps.TryGetValue(Entity.MapID, out map);

                    if (map == null)
                        return (map = new Game.Map(Entity.MapID, Database.MapsTable.MapInformations[Entity.MapID].BaseID, Database.DMaps.MapPaths[Database.MapsTable.MapInformations[Entity.MapID].BaseID]));
                }
                else
                {
                    if (map.ID != Entity.MapID)
                    {
                        Kernel.Maps.TryGetValue(Entity.MapID, out map);

                        if (map == null)
                            return (map = new Game.Map(Entity.MapID, Database.MapsTable.MapInformations[Entity.MapID].BaseID, Database.DMaps.MapPaths[Database.MapsTable.MapInformations[Entity.MapID].BaseID]));
                    }
                }
                return map;
            }
        }
        public uint ExpBall
        {
            get
            {
                ulong exp = Database.DataHolder.LevelExperience(Entity.Level);
                return (uint)(exp * 13000 / (ulong)((Entity.Level * Entity.Level * Entity.Level / 12) + 1));
            }
        }
        public bool AddProficiency(IProf proficiency)
        {
            if (Proficiencies.ContainsKey(proficiency.ID))
            {
                Proficiencies[proficiency.ID].Level = proficiency.Level;
                Proficiencies[proficiency.ID].Experience = proficiency.Experience;
                proficiency.Send(this);
                return false;
            }
            else
            {
                Proficiencies.Add(proficiency.ID, proficiency);
                proficiency.NeededExperience = Database.DataHolder.ProficiencyLevelExperience(proficiency.Level);
                proficiency.Send(this);
                Database.SkillTable.SaveProficiencies(this);
                return true;
            }
        }
        public bool AddSpell(ISkill spell)
        {
            if (Spells.ContainsKey(spell.ID))
            {
                if (Spells[spell.ID].Level < spell.Level)
                {
                    Spells[spell.ID].Level = spell.Level;
                    Spells[spell.ID].Experience = spell.Experience;
                    if (spell.ID != 3060)
                    {
                        spell.Send(this);
                    }
                }
                return false;
            }
            else
            {
                if (spell.ID == 1045 || spell.ID == 1046)
                {

                }
                Spells.Add(spell.ID, spell);
                if (spell.ID != 3060)
                {
                    spell.Send(this);
                }
                Database.SkillTable.SaveSpells(this);

                return true;
            }
        }
        public bool RemoveSpell(Interfaces.ISkill spell)
        {
            if (Spells.ContainsKey(spell.ID))
            {
                Spells.Remove(spell.ID);
                Network.GamePackets.Data data = new Data(true);
                data.UID = Entity.UID;
                data.dwParam = spell.ID;
                data.ID = 109;
                Send(data);
                Database.SkillTable.DeleteSpell(this, spell.ID);
                return true;
            }
            return false;
        }
        public void IncreaseExperience(ulong experience, bool addMultiple)
        {
            if (Entity.ContainsFlag(Update.Flags.Cursed))
                return;
            if (Entity.Dead) return;
            byte level = Entity.Level;
            ulong _experience = Entity.Experience;
            ulong prExperienece = experience;
            if (addMultiple)
            {
                if (Entity.VIPLevel > 0)
                    experience *= Entity.VIPLevel;
                experience *= Constants.ExtraExperienceRate;
                if (Entity.HeavenBlessing > 0)
                    experience += (uint)(experience * 20 / 100);
                if (Entity.Reborn >= 2)
                    experience /= 3;

                if (Guild != null)
                {
                    if (Guild.Level > 0)
                    {
                        experience += (ushort)(experience * Guild.Level / 100);
                    }
                }
                prExperienece = experience + (ulong)(experience * ((float)Entity.BattlePower / 100));

                _experience += prExperienece;
            }
            else
                _experience += experience;

            if (Entity.Level < 140 && Entity.InAutoHunt == true)
            {
                Entity.AutoHuntEXP += experience;
                return;
            }
            else if (Entity.Level == 140 && Entity.InAutoHunt == true)
            {
                Entity.AutoHuntEXP = 0;
                return;
            }
            if (Entity.Level < 140)
            {
                while (_experience >= Database.DataHolder.LevelExperience(level) && level < 140)
                {
                    _experience -= Database.DataHolder.LevelExperience(level);
                    level++;
                    if (Entity.Reborn == 1)
                    {
                        if (level >= 130 && Entity.FirstRebornLevel > 130 && level < Entity.FirstRebornLevel)
                            level = Entity.FirstRebornLevel;
                    }
                    else if (Entity.Reborn == 2)
                    {
                        if (level >= 130 && Entity.SecondRebornLevel > 130 && level < Entity.SecondRebornLevel)
                            level = Entity.SecondRebornLevel;
                    }
                    if (Entity.Class >= 10 && Entity.Class <= 15)
                        if (!Spells.ContainsKey(1110))
                            AddSpell(new Network.GamePackets.Spell(true) { ID = 1110 });
                    if (Entity.Class >= 20 && Entity.Class <= 25)
                        if (!Spells.ContainsKey(1020))
                            AddSpell(new Network.GamePackets.Spell(true) { ID = 1020 });
                    if (Entity.Class >= 40 && Entity.Class <= 45)
                        if (!Spells.ContainsKey(8002))
                            AddSpell(new Network.GamePackets.Spell(true) { ID = 8002 });
                    if (Entity.Class >= 50 && Entity.Class <= 55)
                        if (!Spells.ContainsKey(6011))
                            AddSpell(new Network.GamePackets.Spell(true) { ID = 6011 });
                    if (Entity.Class >= 60 && Entity.Class <= 65)
                        if (!Spells.ContainsKey(10490))
                            AddSpell(new Network.GamePackets.Spell(true) { ID = 10490 });
                    if (Mentor != null)
                    {
                        if (Mentor.IsOnline)
                        {
                            uint exExp = (uint)(level * 2);
                            Mentor.Client.PrizeExperience += exExp;
                            AsApprentice = Mentor.Client.Apprentices[Entity.UID];
                            if (AsApprentice != null)
                            {
                                AsApprentice.Actual_Experience += exExp;
                                AsApprentice.Total_Experience += exExp;
                            }
                            if (Mentor.Client.PrizeExperience > 50 * 606)
                                Mentor.Client.PrizeExperience = 50 * 606;
                        }
                    }
                    if (level == 70)
                    {
                        if (ArenaStatistic == null || ArenaStatistic.EntityID == 0)
                        {
                            ArenaStatistic = new Nyx.Server.Network.GamePackets.ArenaStatistic(true);
                            ArenaStatistic.EntityID = Entity.UID;
                            ArenaStatistic.Name = Entity.Name;
                            ArenaStatistic.Level = Entity.Level;
                            ArenaStatistic.Class = Entity.Class;
                            ArenaStatistic.Model = Entity.Mesh;
                            ArenaPoints = Database.ArenaTable.ArenaPointFill(Entity.Level);
                            ArenaStatistic.LastArenaPointFill = DateTime.Now;
                            Database.ArenaTable.InsertArenaStatistic(this);
                            ArenaStatistic.Status = Network.GamePackets.ArenaStatistic.NotSignedUp;
                            Game.Arena.ArenaStatistics.Add(Entity.UID, ArenaStatistic);
                        }
                    }
                    if (Entity.Reborn == 0)
                    {
                        if (level <= 120)
                        {
                            Database.DataHolder.GetStats(Entity.Class, level, this);
                            CalculateStatBonus();
                            CalculateHPBonus();
                            GemAlgorithm();
                        }
                        else
                            Entity.Atributes += 3;
                    }
                    else
                    {
                        Entity.Atributes += 3;
                    }
                }
                if (Entity.Level != level)
                {
                    if (Team != null)
                    {
                        if (Team.LowestLevelsUID == Entity.UID)
                        {
                            Team.LowestLevel = 0;
                            Team.LowestLevelsUID = 0;
                            Team.SearchForLowest();
                        }
                    }
                    Entity.Level = level;
                    Entity.Hitpoints = Entity.MaxHitpoints;
                    Entity.Mana = Entity.MaxMana;
                    if (Entity.Level > 130)
                        Database.EntityTable.UpdateLevel(Entity.Owner);
                    if (Entity.Reborn == 2)
                        Network.PacketHandler.ReincarnationHash(Entity.Owner);
                }
                if (Entity.Experience != _experience)
                    Entity.Experience = _experience;
            }
            #region Trojan
            if (Entity.Class >= 10 && Entity.Class <= 15)
            {
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(1110)) { AddSpell(LearnableSpell(1110));  }
                    if (!Spells.ContainsKey(1015)) { AddSpell(LearnableSpell(1015));  }
                    if (!Spells.ContainsKey(1115)) { AddSpell(LearnableSpell(1115));  }
                    if (!Spells.ContainsKey(1270)) { AddSpell(LearnableSpell(1270));  }
                    if (!Spells.ContainsKey(1190)) { AddSpell(LearnableSpell(1190));  }
                }
            }
            #endregion
            #region Warrior
            if (Entity.Class >= 20 && Entity.Class <= 25)
            {
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(11200)) { AddSpell(LearnableSpell(11200));  }
                    if (!Spells.ContainsKey(10470)) { AddSpell(LearnableSpell(10470));  }
                    if (!Spells.ContainsKey(1025)) { AddSpell(LearnableSpell(1025));  }
                    if (!Spells.ContainsKey(1020)) { AddSpell(LearnableSpell(1020));  }
                    if (!Spells.ContainsKey(1015)) { AddSpell(LearnableSpell(1015));  }
                    if (!Spells.ContainsKey(12670)) { AddSpell(LearnableSpell(12670));  }
                    if (!Spells.ContainsKey(12700)) { AddSpell(LearnableSpell(12700));  }
                }
                if (Entity.Level >= 61)
                {
                    if (!Spells.ContainsKey(1051)) { AddSpell(LearnableSpell(1051));  }
                    if (!Spells.ContainsKey(12680)) { AddSpell(LearnableSpell(12680));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(11160)) { AddSpell(LearnableSpell(11160));  }
                    if (!Spells.ContainsKey(12660)) { AddSpell(LearnableSpell(12660));  }
                    if (!Spells.ContainsKey(12690)) { AddSpell(LearnableSpell(12690));  }
                }
            }
            #endregion
            #region Archer
            if (Entity.Class >= 40 && Entity.Class <= 45)
            {
                if (Entity.Level >= 1)
                {
                    if (!Spells.ContainsKey(8002)) { AddSpell(LearnableSpell(8002));  }
                }
                if (Entity.Level >= 23)
                {
                    if (!Spells.ContainsKey(8001)) { AddSpell(LearnableSpell(8001));  }
                }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(8000)) { AddSpell(LearnableSpell(8000));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(8003)) { AddSpell(LearnableSpell(8003));  }
                    if (!Spells.ContainsKey(9000)) { AddSpell(LearnableSpell(9000));  }
                    if (!Spells.ContainsKey(8030)) { AddSpell(LearnableSpell(8030));  }
                }
                if (Entity.Level >= 100)
                {

                    if (Spells[8003].Level < 1)
                    {
                        ISkill spell = new Spell(true);
                        spell.ID = 8003;
                        spell.Level = 1;
                        AddSpell(spell);
                        
                    }

                }
            }
            if (Entity.Class >= 41 && Entity.Class <= 45)
            {
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(11620)) {  AddSpell(LearnableSpell(11620));  }
                    if (!Spells.ContainsKey(11610)) { AddSpell(LearnableSpell(11610));  }
                    if (!Spells.ContainsKey(11660)) { AddSpell(LearnableSpell(11660));  }
                }
                if (Entity.Level >= 50)
                {
                    if (!Spells.ContainsKey(11590)) { AddSpell(LearnableSpell(11590));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(11650)) { AddSpell(LearnableSpell(11650));  }
                }
                if (Entity.Level >= 90)
                {
                    if (!Spells.ContainsKey(11670)) { AddSpell(LearnableSpell(11670));  }
                }
                if (Entity.Level >= 100)
                {
                    if (!Spells.ContainsKey(11600)) { AddSpell(LearnableSpell(11600));  }
                }
            }
            #endregion
            #region Ninja
            if (Entity.Class >= 50 && Entity.Class <= 55)
            {
                if (Entity.Level >= 15)
                {
                    if (!Spells.ContainsKey(6011)) { AddSpell(LearnableSpell(6011));  }
                }
                if (Entity.Level >= 20)
                {
                    if (!Spells.ContainsKey(11170)) { AddSpell(LearnableSpell(11170));  }
                    if (!Spells.ContainsKey(11180)) { AddSpell(LearnableSpell(11180));  }
                }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(12080)) { AddSpell(LearnableSpell(12080));  }
                    if (!Spells.ContainsKey(11230)) { AddSpell(LearnableSpell(11230));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(6001)) { AddSpell(LearnableSpell(6001));  }
                    if (!Spells.ContainsKey(6010)) { AddSpell(LearnableSpell(6010));  }
                }
                if (Entity.Level >= 110)
                {
                    if (!Spells.ContainsKey(6004)) { AddSpell(LearnableSpell(6004));  }
                }
            }
            #endregion
            #region Monk
            if (Entity.Class >= 60 && Entity.Class <= 65)
            {
                if (Entity.Level >= 5)
                {
                    if (!Spells.ContainsKey(10490)) { AddSpell(LearnableSpell(10490));  }
                }
                if (Entity.Level >= 15)
                {
                    if (!Spells.ContainsKey(10390)) { AddSpell(LearnableSpell(10390));  }
                    if (!Spells.ContainsKey(10415)) { AddSpell(LearnableSpell(10415));  }
                }
                if (Entity.Level >= 20)
                {
                    if (!Spells.ContainsKey(10395)) { AddSpell(LearnableSpell(10395));  }
                    if (!Spells.ContainsKey(10410)) { AddSpell(LearnableSpell(10410));  }
                }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(10381)) { AddSpell(LearnableSpell(10381));  }
                    if (!Spells.ContainsKey(10400)) { AddSpell(LearnableSpell(10400));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(10425)) { AddSpell(LearnableSpell(10425));  }
                }
                if (Entity.Level >= 100)
                {
                    if (!Spells.ContainsKey(10420)) { AddSpell(LearnableSpell(10420));  }
                    if (!Spells.ContainsKey(10421)) { AddSpell(LearnableSpell(10421));  }
                    if (!Spells.ContainsKey(10422)) { AddSpell(LearnableSpell(10422));  }
                    if (!Spells.ContainsKey(10423)) { AddSpell(LearnableSpell(10423));  }
                    if (!Spells.ContainsKey(10424)) { AddSpell(LearnableSpell(10424));  }
                    if (!Spells.ContainsKey(10430)) { AddSpell(LearnableSpell(10430));  }
                }
            }
            #endregion
            #region Pirate
            if (Entity.Class >= 70 && Entity.Class <= 75)
            {
                if (!Spells.ContainsKey(11030)) { AddSpell(LearnableSpell(11030));  }
                if (!Spells.ContainsKey(11050)) { AddSpell(LearnableSpell(11050));  }
                if (Entity.Level >= 15)
                {
                    if (!Spells.ContainsKey(11140)) { AddSpell(LearnableSpell(11140));  }
                }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(11060)) { AddSpell(LearnableSpell(11060));  }
                    if (!Spells.ContainsKey(11110)) { AddSpell(LearnableSpell(11110));  }
                    if (!Spells.ContainsKey(11130)) { AddSpell(LearnableSpell(11130));  }
                    if (!Spells.ContainsKey(11070)) { AddSpell(LearnableSpell(11070));  }
                    if (!Spells.ContainsKey(11120)) { AddSpell(LearnableSpell(11120));  }
                }
                if (Entity.Level >= 70)
                {
                    if (Entity.Reborn >= 1 && Entity.FirstRebornClass == 75)
                    {
                        if (!Spells.ContainsKey(11100)) { AddSpell(LearnableSpell(11100));  }
                    }
                }
            }
            #endregion
            #region Taoist
            if (Entity.Class >= 100 && Entity.Class <= 101)
            {
                if (!Spells.ContainsKey(1000)) { AddSpell(LearnableSpell(1000));  }
                if (!Spells.ContainsKey(1005)) { AddSpell(LearnableSpell(1005));  }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(1195)) { AddSpell(LearnableSpell(1195));  }
                }
            }
            #region Water
            if (Entity.Class >= 132 && Entity.Class <= 135)
            {
                if (!Spells.ContainsKey(1000)) { AddSpell(LearnableSpell(1000));  }
                if (!Spells.ContainsKey(1005)) { AddSpell(LearnableSpell(1005));  }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(10309)) { AddSpell(LearnableSpell(10309));  }
                    if (!Spells.ContainsKey(1195)) { AddSpell(LearnableSpell(1195));  }
                    if (!Spells.ContainsKey(1055)) { AddSpell(LearnableSpell(1055));  }
                    if (!Spells.ContainsKey(1085)) { AddSpell(LearnableSpell(1085));  }
                    if (!Spells.ContainsKey(1090)) { AddSpell(LearnableSpell(1090));  }
                    if (!Spells.ContainsKey(1095)) { AddSpell(LearnableSpell(1095));  }
                    if (!Spells.ContainsKey(1125)) { AddSpell(LearnableSpell(1125));  }
                    if (!Spells.ContainsKey(1010)) { AddSpell(LearnableSpell(1010));  }
                    if (!Spells.ContainsKey(1050)) { AddSpell(LearnableSpell(1050));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(1075)) { AddSpell(LearnableSpell(1075));  }
                    if (!Spells.ContainsKey(1100)) { AddSpell(LearnableSpell(1100));  }
                }
                if (Entity.Level >= 80)
                {
                    if (!Spells.ContainsKey(1175)) { AddSpell(LearnableSpell(1175));  }
                    if (!Spells.ContainsKey(10309)) { AddSpell(LearnableSpell(10309));  }
                }
                if (Entity.Level >= 94)
                {
                    if (!Spells.ContainsKey(1170)) { AddSpell(LearnableSpell(1170));  }
                }
            }
            #endregion
            #region Fire
            if (Entity.Class >= 142 && Entity.Class <= 145)
            {
                if (!Spells.ContainsKey(1125)) { AddSpell(LearnableSpell(1125));  }
                if (!Spells.ContainsKey(1010)) { AddSpell(LearnableSpell(1010));  }
                if (!Spells.ContainsKey(5001)) { AddSpell(LearnableSpell(5001));  }
                if (!Spells.ContainsKey(1005)) { AddSpell(LearnableSpell(1005));  }
                if (!Spells.ContainsKey(10310)) { AddSpell(LearnableSpell(10310));  }
  if (Entity.Class >= 142 && Entity.FirstRebornClass >= 142 && Entity.SecondRebornClass >= 142)
                {
                    if (!Spells.ContainsKey(10310)) { AddSpell(LearnableSpell(10310));  }
                }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(1195)) { AddSpell(LearnableSpell(1195));  }
                    if (!Spells.ContainsKey(10309)) { AddSpell(LearnableSpell(10309));  }
                }
                if (Entity.Level >= 52)
                {
                    if (!Spells.ContainsKey(1150)) { AddSpell(LearnableSpell(1150));  }
                }
                if (Entity.Level >= 55)
                {
                    if (!Spells.ContainsKey(1180)) { AddSpell(LearnableSpell(1180));  }
                }
                if (Entity.Level >= 48)
                {
                    if (!Spells.ContainsKey(1120)) { AddSpell(LearnableSpell(1120));  }
                    if (!Spells.ContainsKey(1165)) { AddSpell(LearnableSpell(1165));  }
                }
                if (Entity.Level >= 43)
                {
                    if (!Spells.ContainsKey(1160)) { AddSpell(LearnableSpell(1160));  }
                }
                if (Entity.Level >= 81)
                {
                    if (Spells.ContainsKey(1001) && Spells[1001] != null && Spells[1001].Level == 3)
                    {
                        if (!Spells.ContainsKey(1002)) { AddSpell(LearnableSpell(1002));  }
                    }
                }
            }
            #endregion
            #endregion
            #region Dragon-Warrior
            if (Entity.Class >= 80 && Entity.Class <= 85)
            {
                if (Entity.Class >= 85 && Entity.FirstRebornClass >= 85 && Entity.SecondRebornClass >= 85)
                {
                    if (!Spells.ContainsKey(12300)) { AddSpell(LearnableSpell(12300));  }
                }
                if (Entity.Level >= 1)
                {
                    if (!Spells.ContainsKey(12240)) { AddSpell(LearnableSpell(12240));  }
                    if (!Spells.ContainsKey(12220)) { AddSpell(LearnableSpell(12220));  }
                    if (!Spells.ContainsKey(12210)) { AddSpell(LearnableSpell(12210));  }
                }
                if (Entity.Level >= 3)
                {
                    if (!Spells.ContainsKey(12290)) { AddSpell(LearnableSpell(12290));  }
                }
                if (Entity.Level >= 15)
                {
                    if (!Spells.ContainsKey(12320)) { AddSpell(LearnableSpell(12320));  }
                    if (!Spells.ContainsKey(12330)) { AddSpell(LearnableSpell(12330));  }
                    if (!Spells.ContainsKey(12340)) { AddSpell(LearnableSpell(12340));  }
                    if (!Spells.ContainsKey(12270)) { AddSpell(LearnableSpell(12270));  }
                }
                if (Entity.Level >= 40)
                {
                    if (!Spells.ContainsKey(12120)) { AddSpell(LearnableSpell(12120));  }
                    if (!Spells.ContainsKey(12130)) { AddSpell(LearnableSpell(12130));  }
                    if (!Spells.ContainsKey(12140)) { AddSpell(LearnableSpell(12140));  }
                    if (!Spells.ContainsKey(11960)) { AddSpell(LearnableSpell(11960));  }
                }
                if (Entity.Level >= 70)
                {
                    if (!Spells.ContainsKey(12200)) { AddSpell(LearnableSpell(12200));  }
                    if (!Spells.ContainsKey(12280)) { AddSpell(LearnableSpell(12280));  }
                    if (!Spells.ContainsKey(12160)) { AddSpell(LearnableSpell(12160));  }
                }
                if (Entity.Level >= 100)
                {
                    if (!Spells.ContainsKey(12350)) { AddSpell(LearnableSpell(12350));  }
                    if (!Spells.ContainsKey(12170)) { AddSpell(LearnableSpell(12170));  }
                }
            }
            #endregion
        }
        public void IncreaseSpellExperience(uint experience, ushort id)
        {
            if (Spells.ContainsKey(id))
            {
                switch (id)
                {
                    case 1290:
                    case 5030:
                    case 7030:
                        experience = 100; break;
                }
                experience *= Constants.ExtraSpellRate;
                experience += (uint)(experience * Entity.Gems[6] / 100);
                Interfaces.ISkill spell = Spells[id];
                if (spell == null)
                    return;
                if (Entity.VIPLevel > 0)
                {
                    experience *= 5;
                }
                Database.SpellInformation spellInfo = Database.SpellTable.SpellInformations[spell.ID][spell.Level];
                if (spellInfo != null)
                {
                    if (spellInfo.NeedExperience != 0 && Entity.Level >= spellInfo.NeedLevel)
                    {
                        spell.Experience += experience;
                        bool leveled = false;
                        if (spell.Experience >= spellInfo.NeedExperience)
                        {
                            spell.Experience = 0;
                            spell.Level++;
                            leveled = true;
                            Send(Constants.SpellLeveled);
                        }
                        if (leveled)
                        {
                            spell.Send(this);
                            Database.SkillTable.SaveSpells(this);
                        }
                        else
                        {
                            Network.GamePackets.SkillExperience update = new SkillExperience(true);
                            update.AppendSpell(spell.ID, spell.Experience);
                            update.Send(this);
                            Database.EntityTable.UpdateSkillExp(this, spell.ID, experience);
                        }
                    }
                }
            }
        }
        public void IncreaseProficiencyExperience(uint experience, ushort id)
        {
            if (Proficiencies.ContainsKey(id))
            {
                Interfaces.IProf proficiency = Proficiencies[id];
                experience *= Constants.ExtraProficiencyRate;
                experience += (uint)(experience * Entity.Gems[5] / 100);
                if (Entity.VIPLevel > 0)
                {
                    experience *= 5;
                }
                proficiency.Experience += experience;
                if (proficiency.Level < 20)
                {
                    bool leveled = false;
                    while (proficiency.Experience >= Database.DataHolder.ProficiencyLevelExperience(proficiency.Level))
                    {
                        proficiency.Experience -= Database.DataHolder.ProficiencyLevelExperience(proficiency.Level);
                        proficiency.Level++;
                        if (proficiency.Level == 20)
                        {
                            proficiency.Experience = 0;
                            proficiency.Send(this);
                            Send(Constants.ProficiencyLeveled);
                            return;
                        }
                        proficiency.NeededExperience = Database.DataHolder.ProficiencyLevelExperience(proficiency.Level);
                        leveled = true;
                        Send(Constants.ProficiencyLeveled);
                    }
                    if (leveled)
                    {
                        proficiency.Send(this);
                    }
                    else
                    {
                        Network.GamePackets.SkillExperience update = new SkillExperience(true);
                        update.AppendProficiency(proficiency.ID, proficiency.Experience, Database.DataHolder.ProficiencyLevelExperience(proficiency.Level));
                        update.Send(this);
                    }
                }

            }
            else
            {
                AddProficiency(new Network.GamePackets.Proficiency(true) { ID = id });
            }
        }
        public byte ExtraAtributePoints(byte level, byte mClass)
        {
            if (mClass == 135)
            {
                if (level <= 110)
                    return 0;
                switch (level)
                {
                    case 112: return 1;
                    case 114: return 3;
                    case 116: return 6;
                    case 118: return 10;
                    case 120: return 15;
                    case 121: return 15;
                    case 122: return 21;
                    case 123: return 21;
                    case 124: return 28;
                    case 125: return 28;
                    case 126: return 36;
                    case 127: return 36;
                    case 128: return 45;
                    case 129: return 45;
                    default:
                        return 55;
                }
            }
            else
            {
                if (level <= 120)
                    return 0;
                switch (level)
                {
                    case 121: return 1;
                    case 122: return 3;
                    case 123: return 6;
                    case 124: return 10;
                    case 125: return 15;
                    case 126: return 21;
                    case 127: return 28;
                    case 128: return 36;
                    case 129: return 45;
                    default:
                        return 55;
                }
            }
        }
        public static ISkill LearnableSpell(ushort spellid)
        {
            ISkill spell = new Spell(true);
            spell.ID = spellid;
            return spell;
        }
        public bool Reborn(byte toClass)
        {
            if (Inventory.Contains(3000550, 1)) Inventory.Remove(3000550, 1);
            #region Items
            if (Inventory.Count > 37) return false;
            switch (toClass)
            {
                case 11:
                case 21:
                case 51:
                case 61:
                case 71:
                    {
                        Inventory.Add(410077, Game.Enums.ItemEffect.Poison);
                        break;
                    }
                case 41:
                    {
                        Inventory.Add(500057, Game.Enums.ItemEffect.Shield);
                        break;
                    }
                case 132:
                case 142:
                    {
                        if (toClass == 132)
                            Inventory.Add(421077, Game.Enums.ItemEffect.MP);
                        else
                            Inventory.Add(421077, Game.Enums.ItemEffect.HP);
                        break;
                    }
            }
            #region Low level items
            for (byte i = 1; i < 9; i++)
            {
                if (i != 7)
                {
                    ConquerItem item = Equipment.TryGetItem(i);
                    if (item != null && item.ID != 0)
                    {
                        try
                        {
                            //UnloadItemStats(item, false);
                            Database.ConquerItemInformation cii = new Nyx.Server.Database.ConquerItemInformation(item.ID, item.Plus);
                            item.ID = cii.LowestID(Network.PacketHandler.ItemMinLevel(Network.PacketHandler.ItemPosition(item.ID)));
                            item.Mode = Nyx.Server.Game.Enums.ItemMode.Update;
                            item.Send(this);
                            LoadItemStats();
                            Database.ConquerItemTable.UpdateItemID(item);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            ConquerItem hand = Equipment.TryGetItem(5);
            if (hand != null)
            {
                Equipment.Remove(5);
                CalculateStatBonus();
                CalculateHPBonus();
            }
            hand = Equipment.TryGetItem(25);
            if (hand != null)
            {
                Equipment.Remove(25);
                CalculateStatBonus();
                CalculateHPBonus();
            }
            LoadItemStats();
            SendScreen(Entity.SpawnPacket, false);
            #endregion
            #endregion
            if (Entity.Reborn == 0)
            {
                Entity.FirstRebornClass = Entity.Class;
                Entity.FirstRebornLevel = Entity.Level;
                Entity.Atributes =
                    (ushort)(ExtraAtributePoints(Entity.FirstRebornClass, Entity.FirstRebornLevel) + 52);
            }
            else
            {
                Entity.SecondRebornClass = Entity.Class;
                Entity.SecondRebornLevel = Entity.Level;
                Entity.Atributes =
                    (ushort)(ExtraAtributePoints(Entity.FirstRebornClass, Entity.FirstRebornLevel) +
                    ExtraAtributePoints(Entity.SecondRebornClass, Entity.SecondRebornLevel) + 62);
            }
            byte PreviousClass = Entity.Class;
            Entity.Reborn++;
            Entity.Class = toClass;
            Entity.Level = 15;
            Entity.Experience = 0;
            #region Spells
            Interfaces.ISkill[] spells = Spells.Values.ToArray();
            foreach (Interfaces.ISkill spell in spells)
            {
                spell.PreviousLevel = spell.Level;
                spell.Level = 0;
                spell.Experience = 0;
                #region KungFuKing
                if (PreviousClass == 85)
                {
                    if (Entity.Class != 81)
                    {
                        switch (spell.ID)
                        {
                            case 12120:
                            case 12130:
                            case 12140:
                            case 12160:
                            case 12170:
                            case 12200:
                            case 12240:
                            case 12350:
                            case 12270:
                            case 12280:
                            case 12290:
                            case 12300:
                            case 12320:
                            case 12330:
                            case 12340:
                                RemoveSpell(spell);
                                break;
                        }
                    }
                }
                #endregion
                #region Pirate

                if (PreviousClass == 75)
                {
                    if (Entity.Class != 71)
                    {
                        switch (spell.ID)
                        {
                            case 11110:
                            case 11040:
                            case 11050:
                            case 11060:
                            case 11100:
                            case 11120:
                            case 11130:
                            case 11030:
                                RemoveSpell(spell);
                                break;
                        }
                    }
                }

                #endregion
                #region Monk

                if (PreviousClass == 65)
                {
                    if (Entity.Class != 61)
                    {
                        switch (spell.ID)
                        {
                            case 10490:
                            case 10415:
                            case 10381:
                                RemoveSpell(spell);
                                break;
                        }
                    }
                }

                #endregion
                #region Warrior

                if (PreviousClass == 25)
                {
                    if (Entity.Class != 21)
                    {
                        switch (spell.ID)
                        {
                            case 1025:
                                if (Entity.Class != 21 && Entity.Class != 132)
                                    RemoveSpell(spell);
                                break;
                        }
                    }
                }

                #endregion
                #region Ninja

                if (toClass != 51)
                {
                    switch (spell.ID)
                    {
                        case 6010:
                        case 6000:
                        case 6011:
                            RemoveSpell(spell);
                            break;
                    }
                }

                #endregion
                #region Trojan

                if (toClass != 11)
                {
                    switch (spell.ID)
                    {
                        case 1115:
                            RemoveSpell(spell);
                            break;
                    }
                }

                #endregion
                #region Archer

                if (toClass != 41)
                {
                    switch (spell.ID)
                    {
                        case 8001:
                        case 8000:
                        case 8003:
                        case 9000:
                        case 8002:
                        case 8030:
                            RemoveSpell(spell);
                            break;
                    }
                }

                #endregion
                #region WaterTaoist

                if (PreviousClass == 135)
                {
                    if (toClass != 132)
                    {
                        switch (spell.ID)
                        {
                            case 1000:
                            case 1001:
                            case 1010:
                            case 1125:
                            case 1100:
                            case 8030:
                                RemoveSpell(spell);
                                break;
                            case 1050:
                            case 1175:
                            case 1170:
                                if (toClass != 142)
                                    RemoveSpell(spell);
                                break;
                        }
                    }
                }

                #endregion
                #region FireTaoist

                if (PreviousClass == 145)
                {
                    if (toClass != 142)
                    {
                        switch (spell.ID)
                        {
                            case 1000:
                            case 1001:
                            case 1150:
                            case 1180:
                            case 1120:
                            case 1002:
                            case 1160:
                            case 1165:
                                RemoveSpell(spell);
                                break;
                        }
                    }
                }

                #endregion
                if (Spells.ContainsKey(spell.ID))
                    if (spell.ID != (ushort)Game.Enums.SkillIDs.Reflect)
                        spell.Send(this);
            }
            #endregion
            #region Proficiencies
            foreach (Interfaces.IProf proficiency in Proficiencies.Values)
            {
                proficiency.PreviousLevel = proficiency.Level;
                proficiency.Level = 0;
                proficiency.Experience = 0;
                proficiency.Send(this);
            }
            #endregion
            #region Adding earned skills
            if (Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 9876 });
            if (toClass == 51 && PreviousClass == 55 && Entity.Reborn == 1)
                AddSpell(new Spell(true) { ID = 6002 });
            if (toClass == 81 && PreviousClass == 85 && Entity.Reborn == 1)
                AddSpell(new Spell(true) { ID = 12280 });
            if (Entity.FirstRebornClass == 85 && Entity.SecondRebornClass == 85 && Entity.Class == 81 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 12300 });
            if (Entity.FirstRebornClass == 15 && Entity.SecondRebornClass == 15 && Entity.Class == 11 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 10315 });
            if (Entity.FirstRebornClass == 25 && Entity.SecondRebornClass == 25 && Entity.Class == 21 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 10311 });
            if (Entity.FirstRebornClass == 45 && Entity.SecondRebornClass == 45 && Entity.Class == 41 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 10313 });
            if (Entity.FirstRebornClass == 55 && Entity.SecondRebornClass == 55 && Entity.Class == 51 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 6003 });
            if (Entity.FirstRebornClass == 65 && Entity.SecondRebornClass == 65 && Entity.Class == 61 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 10405 });
            if (Entity.FirstRebornClass == 135 && Entity.SecondRebornClass == 135 && Entity.Class == 132 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 30000 });
            if (Entity.FirstRebornClass == 145 && Entity.SecondRebornClass == 145 && Entity.Class == 142 &&
                Entity.Reborn == 2)
                AddSpell(new Spell(true) { ID = 10310 });
            if (Entity.Reborn == 1)
            {
                if (Entity.FirstRebornClass == 75 && Entity.Class == 71)
                {
                    AddSpell(new Spell(true) { ID = 3050 });
                }
                if (Entity.FirstRebornClass == 15 && Entity.Class == 11)
                {
                    AddSpell(new Spell(true) { ID = 3050 });
                }
                else if (Entity.FirstRebornClass == 25 && Entity.Class == 21)
                {
                    AddSpell(new Spell(true) { ID = 3060 });
                }
                else if (Entity.FirstRebornClass == 145 && Entity.Class == 142)
                {
                    AddSpell(new Spell(true) { ID = 3080 });
                }
                else if (Entity.FirstRebornClass == 135 && Entity.Class == 132)
                {
                    AddSpell(new Spell(true) { ID = 3090 });
                }
            }
            if (Entity.Reborn == 2)
            {
                if (Entity.SecondRebornClass == 75 && Entity.Class == 71)
                {
                    AddSpell(new Spell(true) { ID = 3050 });
                }
                if (Entity.SecondRebornClass == 15 && Entity.Class == 11)
                {
                    AddSpell(new Spell(true) { ID = 3050 });
                }
                else if (Entity.SecondRebornClass == 25)
                {
                    AddSpell(new Spell(true) { ID = 3060 });
                }
                else if (Entity.SecondRebornClass == 145 && Entity.Class == 142)
                {
                    AddSpell(new Spell(true) { ID = 3080 });
                }
                else if (Entity.SecondRebornClass == 135 && Entity.Class == 132)
                {
                    AddSpell(new Spell(true) { ID = 3090 });
                }
            }

            #endregion
            #region Remove extra skills

            if (Entity.Reborn == 2)
            {
                #region Pison Star Del
                if (Entity.SecondRebornClass == 55 && Entity.Class == 41)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                if (Entity.SecondRebornClass == 55 && Entity.Class == 81)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                if (Entity.SecondRebornClass == 55 && Entity.Class == 11)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                if (Entity.SecondRebornClass == 55 && Entity.Class == 71)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                if (Entity.SecondRebornClass == 55 && Entity.Class == 61)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                if (Entity.SecondRebornClass == 55 && Entity.Class == 21)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                else if (Entity.SecondRebornClass == 55 && Entity.Class == 142)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }
                else if (Entity.SecondRebornClass == 55 && Entity.Class == 132)
                {
                    RemoveSpell(new Spell(false) { ID = 6002 });
                }

                #endregion
            }

            #endregion
            PacketHandler.RemoveBadSkills(this);
            Database.DataHolder.GetStats(Entity.Class, Entity.Level, this);
            CalculateStatBonus();
            CalculateHPBonus();
            GemAlgorithm();
            #region The View Of Wepeon
            ClientEquip eqs = new ClientEquip();
            eqs.DoEquips(this);
            Send(eqs);
            Equipment.UpdateEntityPacket();
            #endregion
            using (var conn = Database.DataHolder.MySqlConnection)
            {
                conn.Open();
                Database.EntityTable.SaveEntity(this, conn);

                Database.SkillTable.SaveSpells(this, conn);
                Database.SkillTable.SaveProficiencies(this, conn);
                Database.SkillTable.SaveSpells(this);
                Database.SkillTable.SaveProficiencies(this);
            }
            if (this.Quests.HasQuest(QuestID.SecondQuestStageOne) || this.Quests.HasQuest(QuestID.SecondQuestStageTwo) || this.Quests.HasQuest(QuestID.SecondQuestStageThree) || this.Quests.HasQuest(QuestID.SecondQuestStageFour))
            {
                this.Quests.QuitQuest(QuestID.SecondQuestStageOne);
                this.Quests.QuitQuest(QuestID.SecondQuestStageTwo);
                this.Quests.QuitQuest(QuestID.SecondQuestStageThree);
                this.Quests.QuitQuest(QuestID.SecondQuestStageFour);
            }
            var array = this.Quests.src.Values.Where(p => QuestInfo.CheckType((QuestID)p.QItem.UID) == QuestType.EquipmentBonus && p.QItem.Status == QuestPacket.QuestData.QuestStatus.Finished).ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                this.Quests.src.Remove(array[i].QItem.UID);
            }
            array = null;
            if (Entity.Reborn == 1)
            {
                foreach (var client in Kernel.GamePool.Values)
                    client.Send(new Network.GamePackets.Message("Congratulations! " + Entity.Name + " got reborn!", System.Drawing.Color.WhiteSmoke, 2005));
            }
            else
            {
                foreach (var client in Kernel.GamePool.Values)
                    client.Send(new Network.GamePackets.Message("Congratulations! " + Entity.Name + " got 2 reborn!", System.Drawing.Color.WhiteSmoke, 2005));

            }
            return true;
        }
        public uint GetArsenalDonation()
        {
            uint val = 0;
            foreach (var Uint in ArsenalDonations)
                val += Uint;
            using (var cmd = new Nyx.Server.Database.MySqlCommand(Nyx.Server.Database.MySqlCommandType.UPDATE))
                cmd.Update("entities").Set("GuildArsenalDonation", (uint)val).Where("UID", this.Entity.UID)
                    .Execute();
            return val;
        }
        public void CalculateHPBonus()
        {
            switch (Entity.Class)
            {
                case 11: Entity.MaxHitpoints = (uint)(StatHP * 1.05F); break;
                case 12: Entity.MaxHitpoints = (uint)(StatHP * 1.08F); break;
                case 13: Entity.MaxHitpoints = (uint)(StatHP * 1.10F); break;
                case 14: Entity.MaxHitpoints = (uint)(StatHP * 1.12F); break;
                case 15: Entity.MaxHitpoints = (uint)(StatHP * 1.15F); break;
                default: Entity.MaxHitpoints = (uint)StatHP; break;
            }

            Entity.MaxHitpoints += Entity.ItemHP;
            Entity.MaxHitpoints += Entity.Intensification;
            Entity.Hitpoints = Math.Min(Entity.Hitpoints, Entity.MaxHitpoints);
        }
        public void CalculateStatBonus()
        {
            byte ManaBoost = 5;
            const byte HitpointBoost = 24;
            sbyte Class = (sbyte)(Entity.Class / 10);
            if (Class == 13 || Class == 14)
                ManaBoost += (byte)(5 * (Entity.Class - (Class * 10)));
            StatHP = (ushort)((Entity.Strength * 3) +
                                     (Entity.Agility * 3) +
                                     (Entity.Spirit * 3) +
                                     (Entity.Vitality * HitpointBoost));
            Entity.MaxMana = (ushort)((Entity.Spirit * ManaBoost) + Entity.ItemMP);
            Entity.Mana = Math.Min(Entity.Mana, Entity.MaxMana);
        }
        public void SendStatMessage()
        {
            this.ReviewMentor();
            Network.GamePackets.Message Msg = new Nyx.Server.Network.GamePackets.Message(" Your status has been changed", System.Drawing.Color.DarkGoldenrod
                , Network.GamePackets.Message.TopLeft);
            Msg.__Message = string.Format(Msg.__Message,
                new object[] { Entity.MinAttack, Entity.MaxAttack, Entity.MagicAttack, Entity.Defence, (Entity.MagicDefence + Entity.MagicDefence), Entity.Dodge, Entity.PhysicalDamageDecrease, Entity.MagicDamageDecrease, Entity.PhysicalDamageIncrease, Entity.MagicDamageIncrease, Entity.Hitpoints, Entity.MaxHitpoints, Entity.Mana, Entity.MaxMana, Entity.BattlePower });
            this.Send(Msg);
        }
        private bool AreStatsLoadable(ConquerItem item)
        {
            if (!AlternateEquipment)
                if (item.Position > 20)
                    return false;
            if (AlternateEquipment)
                if (item.Position < 20)
                    if (!Equipment.Free((byte)(20 + item.Position)))
                        return false;

            int Position = item.Position;
            if (item.Position > 20) Position -= 20;

            if (Position == ConquerItem.LeftWeapon || Position == ConquerItem.RightWeapon)
                return false;

            return true;
        }
        private Tuple<ConquerItem, ConquerItem> ComputeWeapons()
        {
            if (!AlternateEquipment)
            {
                return new Tuple<ConquerItem, ConquerItem>(
                    Equipment.TryGetItem(ConquerItem.RightWeapon),
                    Equipment.TryGetItem(ConquerItem.LeftWeapon));
            }
            else
            {
                if (Equipment.Free(ConquerItem.AlternateRightWeapon))
                {
                    return new Tuple<ConquerItem, ConquerItem>(
                        Equipment.TryGetItem(ConquerItem.RightWeapon),
                        Equipment.TryGetItem(ConquerItem.LeftWeapon));
                }
                else
                {
                    if (Equipment.Free(ConquerItem.RightWeapon))
                    {
                        return new Tuple<ConquerItem, ConquerItem>(
                            Equipment.TryGetItem(ConquerItem.AlternateRightWeapon),
                            Equipment.TryGetItem(ConquerItem.AlternateLeftWeapon));
                    }
                    else
                    {
                        if (!Equipment.Free(ConquerItem.AlternateLeftWeapon))
                        {
                            return new Tuple<ConquerItem, ConquerItem>(
                                Equipment.TryGetItem(ConquerItem.AlternateRightWeapon),
                                Equipment.TryGetItem(ConquerItem.AlternateLeftWeapon));
                        }
                        else
                        {
                            if (Equipment.Free(ConquerItem.LeftWeapon))
                            {
                                return new Tuple<ConquerItem, ConquerItem>(
                                    Equipment.TryGetItem(ConquerItem.AlternateRightWeapon),
                                    null);
                            }
                            else
                            {
                                ConquerItem aRight = Equipment.TryGetItem(ConquerItem.AlternateRightWeapon),
                                             nLeft = Equipment.TryGetItem(ConquerItem.LeftWeapon);
                                if (PacketHandler.IsTwoHand(aRight.ID))
                                {
                                    if (PacketHandler.IsArrow(nLeft.ID))
                                    {
                                        if (PacketHandler.IsBow(aRight.ID))
                                        {
                                            return new Tuple<ConquerItem,
                                                ConquerItem>(aRight, nLeft);
                                        }
                                        else
                                        {
                                            return new Tuple<ConquerItem,
                                                ConquerItem>(aRight, null);
                                        }
                                    }
                                    else
                                    {
                                        if (PacketHandler.IsShield(nLeft.ID))
                                        {
                                            if (!Spells.ContainsKey(10311))
                                            {
                                                Send(new Network.GamePackets.Message("You need to know Perseverance (Pure Warrior skill) to be able to wear 2-handed weapon and shield.", System.Drawing.Color.Red, Network.GamePackets.Message.Talk));
                                                return new Tuple<ConquerItem,
                                                    ConquerItem>(aRight, null);
                                            }
                                            else
                                            {
                                                return new Tuple<ConquerItem,
                                                    ConquerItem>(aRight, nLeft);
                                            }
                                        }
                                        else
                                        {
                                            return new Tuple<ConquerItem,
                                                ConquerItem>(aRight, null);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!PacketHandler.IsTwoHand(nLeft.ID))
                                    {
                                        return new Tuple<ConquerItem,
                                            ConquerItem>(aRight, nLeft);
                                    }
                                    else
                                    {
                                        return new Tuple<ConquerItem,
                                            ConquerItem>(aRight, null);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        static bool EquipPassLvlReq(Database.ConquerItemBaseInformation baseInformation, Client.GameClient client)
        {
            if (client.Entity.Level < baseInformation.Level)
                return false;
            else
                return true;
        }
        static bool EquipPassRbReq(Database.ConquerItemBaseInformation baseInformation, Client.GameClient client)
        {
            if (baseInformation.Level <= 70 && client.Entity.Reborn >= 1 && client.Entity.Level >= 70)
                return true;
            else
                return false;
        }
        static bool EquipPassJobReq(Database.ConquerItemBaseInformation baseInformation, Client.GameClient client)
        {
            switch (baseInformation.Class)
            {
                #region Trojan
                case 10: if (client.Entity.Class <= 15 && client.Entity.Class >= 10) return true; break;
                case 11: if (client.Entity.Class <= 15 && client.Entity.Class >= 11) return true; break;
                case 12: if (client.Entity.Class <= 15 && client.Entity.Class >= 12) return true; break;
                case 13: if (client.Entity.Class <= 15 && client.Entity.Class >= 13) return true; break;
                case 14: if (client.Entity.Class <= 15 && client.Entity.Class >= 14) return true; break;
                case 15: if (client.Entity.Class == 15) return true; break;
                #endregion
                #region Warrior
                case 20: if (client.Entity.Class <= 25 && client.Entity.Class >= 20) return true; break;
                case 21: if (client.Entity.Class <= 25 && client.Entity.Class >= 21) return true; break;
                case 22: if (client.Entity.Class <= 25 && client.Entity.Class >= 22) return true; break;
                case 23: if (client.Entity.Class <= 25 && client.Entity.Class >= 23) return true; break;
                case 24: if (client.Entity.Class <= 25 && client.Entity.Class >= 24) return true; break;
                case 25: if (client.Entity.Class == 25) return true; break;
                #endregion
                #region Archer
                case 40: if (client.Entity.Class <= 45 && client.Entity.Class >= 40) return true; break;
                case 41: if (client.Entity.Class <= 45 && client.Entity.Class >= 41) return true; break;
                case 42: if (client.Entity.Class <= 45 && client.Entity.Class >= 42) return true; break;
                case 43: if (client.Entity.Class <= 45 && client.Entity.Class >= 43) return true; break;
                case 44: if (client.Entity.Class <= 45 && client.Entity.Class >= 44) return true; break;
                case 45: if (client.Entity.Class == 45) return true; break;
                #endregion
                #region Ninja
                case 50: if (client.Entity.Class <= 55 && client.Entity.Class >= 50) return true; break;
                case 51: if (client.Entity.Class <= 55 && client.Entity.Class >= 51) return true; break;
                case 52: if (client.Entity.Class <= 55 && client.Entity.Class >= 52) return true; break;
                case 53: if (client.Entity.Class <= 55 && client.Entity.Class >= 53) return true; break;
                case 54: if (client.Entity.Class <= 55 && client.Entity.Class >= 54) return true; break;
                case 55: if (client.Entity.Class == 55) return true; break;
                #endregion
                #region Monk
                case 60: if (client.Entity.Class <= 65 && client.Entity.Class >= 60) return true; break;
                case 61: if (client.Entity.Class <= 65 && client.Entity.Class >= 61) return true; break;
                case 62: if (client.Entity.Class <= 65 && client.Entity.Class >= 62) return true; break;
                case 63: if (client.Entity.Class <= 65 && client.Entity.Class >= 63) return true; break;
                case 64: if (client.Entity.Class <= 65 && client.Entity.Class >= 64) return true; break;
                case 65: if (client.Entity.Class == 65) return true; break;
                #endregion
                #region Pirate
                case 70: if (client.Entity.Class <= 75 && client.Entity.Class >= 70) return true; break;
                case 71: if (client.Entity.Class <= 75 && client.Entity.Class >= 71) return true; break;
                case 72: if (client.Entity.Class <= 75 && client.Entity.Class >= 72) return true; break;
                case 73: if (client.Entity.Class <= 75 && client.Entity.Class >= 73) return true; break;
                case 74: if (client.Entity.Class <= 75 && client.Entity.Class >= 74) return true; break;
                case 75: if (client.Entity.Class == 75) return true; break;
                #endregion
                #region Taoist
                case 190: if (client.Entity.Class >= 100) return true; break;
                #endregion
                case 0: return true;
                default: return false;
            }
            return false;
        }
        public void LoadItemStats()
        {
            uint bStats = Entity.Hitpoints;
            for (int i = 0; i < 29; i++)
                if (Equipment.Objects[i] != null)
                    Equipment.Objects[i].IsWorn = false;
            #region Set Every Variable to Zero
            Entity.Defence = 0;
            Entity.MagicDefence = 0;
            Entity.MagicDefencePercent = 0;
            Entity.BaseMagicAttack = 0;
            Entity.BaseMagicDefence =
            Entity.BaseMaxAttack =
            Entity.BaseMinAttack =
            Entity.PhysicalDamageDecrease =
            Entity.PhysicalDamageIncrease =
            Entity.MagicDamageDecrease =
            Entity.MagicDamageIncrease = 0;
            Entity.ItemHP = 0;
            Entity.ItemHP =
            Entity.ItemMP =
            Entity.AttackRange = (byte)0;
            Entity.Dodge = 0;
            Entity.MinAttack = 0;
            Entity.MaxAttack = 0;
            Entity.Defence = 0;
            Entity.SuperItemBless = 0;
            Entity.MagicDefence = 0;
            Entity.Dodge = 0;
            Entity.BaseMagicAttack = 0;
            Entity.WoodResistance = 0;
            Entity.FireResistance = 0;
            Entity.WaterResistance = 0;
            Entity.EarthResistance = 0;
            Entity.MetalResistance = 0;
            Entity.Breaktrough = 0;
            Entity.WearsGoldPrize = false;
            Entity.CriticalStrike = 0;
            Entity.Immunity = 0;
            Entity.Penetration = 0;
            Entity.Counteraction = 0;
            Entity.Block = 0;
            Entity.Detoxication = 0;
            Entity.Intensification = 0;
            Entity.Penetration = 0;
            Entity.SkillCStrike = 0;
            Entity.MaxAttack = 0;
            Entity.MinAttack = 0;
            Entity.PhysicalDamageDecrease = 0;
            Entity.MagicDamageDecrease = 0;
            Entity.MagicDamageIncrease = 0;
            Entity.PhysicalDamageIncrease = 0;
            Entity.MagicDefencePercent = 0;
            Entity.ItemHP = 0;
            Entity.ItemMP = 0;
            Entity.ItemBless = 1.0;
            Entity.AttackRange = 1;
            Entity.BaseMinAttack = 0;
            Entity.BaseMaxAttack = 0;
            Entity.BaseMagicDefence = 0;
            Entity.BaseDefence = 0;
            Entity.MagicDamageIncrease = 0;
            Entity.Gems = new int[GemTypes.Last];
            Entity.Weight = 0;
            Entity.Accuracy = 0;
            #endregion
            foreach (ConquerItem i in Equipment.Objects)
            {
                if (i == null) continue;
                if (i.Durability == 0) continue;
                if (!AreStatsLoadable(i)) continue;
                loadItemStats(i);
            }
            Weapons = ComputeWeapons();
            if (Weapons == null) Weapons = new Tuple<ConquerItem, ConquerItem>(null, null);
            if (Weapons.Item1 != null)
            {
                loadItemStats(Weapons.Item1);
                if (Weapons.Item2 != null)
                {
                    if (!Weapons.Item1.IsTwoHander())
                        loadItemStats(Weapons.Item2);
                    else
                        if (PacketHandler.IsArrow(Weapons.Item2.ID) || (Entity.Class >= 20 && Entity.Class <= 25))
                            loadItemStats(Weapons.Item2);
                }
            }
            #region SubClass
            Parallel.ForEach(this.Entity.SubClasses.Classes.Values, clases =>
            {
                #region MartialArtist

                if (clases.ID == 1)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.CriticalStrike += 100;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.CriticalStrike += 200;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.CriticalStrike += 300;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.CriticalStrike += 400;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.CriticalStrike += 600;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.CriticalStrike += 800;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.CriticalStrike += 1000;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.CriticalStrike += 1200;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.CriticalStrike += 1500;
                    }
                }

                #endregion

                #region Warlock

                if (clases.ID == 2)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.SkillCStrike += 100;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.SkillCStrike += 200;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.SkillCStrike += 300;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.SkillCStrike += 400;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.SkillCStrike += 600;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.SkillCStrike += 800;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.SkillCStrike += 1000;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.SkillCStrike += 1200;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.SkillCStrike += 1500;
                    }
                }

                #endregion

                #region ChiMaster

                if (clases.ID == 3)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.Immunity += 100;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.Immunity += 200;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.Immunity += 300;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.Immunity += 400;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.Immunity += 600;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.Immunity += 800;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.Immunity += 1000;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.Immunity += 1200;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.Immunity += 1500;
                    }
                }

                #endregion

                #region Sage

                if (clases.ID == 4)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.Penetration += 100;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.Penetration += 200;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.Penetration += 300;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.Penetration += 400;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.Penetration += 600;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.Penetration += 800;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.Penetration += 1000;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.Penetration += 1200;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.Penetration += 1500;
                    }
                }

                #endregion

                #region Apothecary

                if (clases.ID == 5)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.Detoxication += 8;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.Detoxication += 16;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.Detoxication += 24;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.Detoxication += 32;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.Detoxication += 40;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.Detoxication += 48;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.Detoxication += 56;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.Detoxication += 64;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.Detoxication += 72;
                    }
                }

                #endregion

                #region Performer

                if (clases.ID == 6)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.BaseMaxAttack += 100;
                        this.Entity.BaseMinAttack += 100;
                        this.Entity.BaseMagicAttack += 100;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.BaseMaxAttack += 200;
                        this.Entity.BaseMinAttack += 200;
                        this.Entity.BaseMagicAttack += 200;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.BaseMaxAttack += 300;
                        this.Entity.BaseMinAttack += 300;
                        this.Entity.BaseMagicAttack += 300;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.BaseMaxAttack += 400;
                        this.Entity.BaseMinAttack += 400;
                        this.Entity.BaseMagicAttack += 400;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.BaseMaxAttack += 500;
                        this.Entity.BaseMinAttack += 500;
                        this.Entity.BaseMagicAttack += 500;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.BaseMaxAttack += 600;
                        this.Entity.BaseMinAttack += 600;
                        this.Entity.BaseMagicAttack += 600;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.BaseMaxAttack += 700;
                        this.Entity.BaseMinAttack += 700;
                        this.Entity.BaseMagicAttack += 700;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.BaseMaxAttack += 800;
                        this.Entity.BaseMinAttack += 800;
                        this.Entity.BaseMagicAttack += 800;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.BaseMaxAttack += 1000;
                        this.Entity.BaseMinAttack += 1000;
                        this.Entity.BaseMagicAttack += 1000;
                    }
                }

                #endregion

                #region Wrangler

                if (clases.ID == 9)
                {
                    if (clases.Phase == 1)
                    {
                        this.Entity.ItemHP += 100;
                    }
                    if (clases.Phase == 2)
                    {
                        this.Entity.ItemHP += 200;
                    }
                    if (clases.Phase == 3)
                    {
                        this.Entity.ItemHP += 300;
                    }
                    if (clases.Phase == 4)
                    {
                        this.Entity.ItemHP += 400;
                    }
                    if (clases.Phase == 5)
                    {
                        this.Entity.ItemHP += 500;
                    }
                    if (clases.Phase == 6)
                    {
                        this.Entity.ItemHP += 600;
                    }
                    if (clases.Phase == 7)
                    {
                        this.Entity.ItemHP += 800;
                    }
                    if (clases.Phase == 8)
                    {
                        this.Entity.ItemHP += 1000;
                    }
                    if (clases.Phase == 9)
                    {
                        this.Entity.ItemHP += 1200;
                    }
                }

                #endregion
            });

            #endregion
            #region Chi
            uint percentage = 100;


            foreach (var chiPower in ChiPowers)
            {
                foreach (var attribute in chiPower.Attributes)
                {
                    switch (attribute.Type)
                    {
                        case Game.Enums.ChiAttribute.CriticalStrike:
                            Entity.CriticalStrike += (int)((ushort)(attribute.Value * 10) * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.Counteraction:
                            Entity.Counteraction += (ushort)(attribute.Value * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.AddAttack:
                            Entity.BaseMinAttack += attribute.Value * percentage / 100;
                            Entity.BaseMaxAttack += attribute.Value * percentage / 100;
                            break;
                        case Game.Enums.ChiAttribute.AddMagicAttack:
                            Entity.BaseMagicAttack += attribute.Value * percentage / 100;
                            break;
                        case Game.Enums.ChiAttribute.AddMagicDefense:
                            Entity.BaseMagicDefence += attribute.Value * percentage / 100;
                            break;
                        case Game.Enums.ChiAttribute.Breakthrough:
                            Entity.Breaktrough += (ushort)(attribute.Value * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.HPAdd:
                            Entity.ItemHP += attribute.Value * percentage / 100;
                            break;
                        case Game.Enums.ChiAttribute.Immunity:
                            Entity.Immunity += (int)((ushort)(attribute.Value * 10) * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.MagicDamageDecrease:
                            Entity.MagicDamageDecrease += (ushort)(attribute.Value * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.MagicDamageIncrease:
                            Entity.MagicDamageIncrease += (ushort)(attribute.Value * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.PhysicalDamageDecrease:
                            Entity.PhysicalDamageDecrease += (ushort)(attribute.Value * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.PhysicalDamageIncrease:
                            Entity.PhysicalDamageIncrease += (ushort)(attribute.Value * percentage / 100);
                            break;
                        case Game.Enums.ChiAttribute.SkillCriticalStrike:
                            Entity.SkillCStrike += (int)((ushort)(attribute.Value * 10) * percentage / 100);
                            break;
                    }
                }
            }
            #region Dragon Ranking
            if ((ChiData.DragonRank <= 50 || ChiData.DragonPoints == 400) && ChiPowers.Count > 0)
            {
                if (ChiData.DragonRank <= 3 || ChiData.DragonPoints == 400)
                {
                    Entity.ItemHP += 5000;
                    Entity.BaseMagicDefence += 300;
                    Entity.PhysicalDamageDecrease += 1000;
                    Entity.MagicDamageDecrease += 300;
                }
                else if (ChiData.DragonRank <= 15)
                {
                    var hp = (uint)Math.Round((3000 - (ChiData.DragonRank - 4) * 62.5));
                    Entity.ItemHP += hp;
                    Entity.BaseMagicDefence += (uint)(250 - (ChiData.DragonRank - 4) * 9);
                    Entity.PhysicalDamageDecrease += (ushort)(600 - (ChiData.DragonRank - 4) * 18);
                    Entity.MagicDamageDecrease += (ushort)(200 - (ChiData.DragonRank - 4) * 4);
                }
                else if (ChiData.DragonRank <= 50)
                {
                    Entity.ItemHP += 1500;
                    Entity.BaseMagicDefence += 100;
                    Entity.PhysicalDamageDecrease += 300;
                    Entity.MagicDamageDecrease += 100;
                }
            }
            #endregion
            #region Phoenix Ranking
            if ((ChiData.PhoenixRank <= 50 || ChiData.PhoenixPoints == 400) && ChiPowers.Count > 1)
            {
                if (ChiData.PhoenixRank <= 3 || ChiData.PhoenixPoints == 400)
                {
                    Entity.BaseMinAttack += 3000;
                    Entity.BaseMaxAttack += 3000;
                    Entity.BaseMagicAttack += 3000;
                    Entity.PhysicalDamageIncrease += 1000;
                    Entity.MagicDamageIncrease += 300;
                }
                else if (ChiData.PhoenixRank <= 15)
                {
                    Entity.BaseMinAttack += (uint)(2000 - (ChiData.PhoenixRank - 4) * 45);
                    Entity.BaseMaxAttack += (uint)(2000 - (ChiData.PhoenixRank - 4) * 45);
                    Entity.BaseMagicAttack += (uint)(2000 - (ChiData.PhoenixRank - 4) * 45);
                    Entity.PhysicalDamageIncrease += (ushort)(600 - (ChiData.PhoenixRank - 4) * 18);
                    Entity.MagicDamageIncrease += (ushort)(200 - (ChiData.PhoenixRank - 4) * 4);
                }
                else if (ChiData.PhoenixRank <= 50)
                {
                    Entity.BaseMinAttack += 1000;
                    Entity.BaseMaxAttack += 1000;
                    Entity.BaseMagicAttack += 1000;
                    Entity.PhysicalDamageIncrease += 300;
                    Entity.MagicDamageIncrease += 100;
                }
            }
            #endregion
            #region Tiger Ranking
            if ((ChiData.TigerRank <= 50 || ChiData.TigerPoints == 400) && ChiPowers.Count > 2)
            {
                if (ChiData.TigerRank <= 3 || ChiData.TigerPoints == 400)
                {
                    Entity.CriticalStrike += 1500;
                    Entity.SkillCStrike += 1500;
                    Entity.Immunity += 800;
                }
                else if (ChiData.TigerRank <= 15)
                {
                    Entity.CriticalStrike += (ushort)(1100 - (ChiData.TigerRank - 4) * 10);
                    Entity.SkillCStrike += (ushort)(1100 - (ChiData.TigerRank - 4) * 10);
                    Entity.Immunity += 500;
                }
                else if (ChiData.TigerRank <= 50)
                {
                    Entity.CriticalStrike += 500;
                    Entity.SkillCStrike += 500;
                    Entity.Immunity += 200;
                }
            }
            #endregion
            #region Turtle Ranking
            if ((ChiData.TurtleRank <= 50 || ChiData.TurtlePoints == 400) && ChiPowers.Count > 3)
            {
                if (ChiData.TurtleRank <= 3 || ChiData.TurtlePoints == 400)
                {
                    Entity.Breaktrough += 150;
                    Entity.Counteraction += 150;
                    Entity.Immunity += 800;
                }
                else if (ChiData.TurtleRank <= 15)
                {
                    Entity.Breaktrough += (ushort)(110 - (ChiData.TurtleRank - 4) * 1);
                    Entity.Counteraction += (ushort)(110 - (ChiData.TurtleRank - 4) * 1);
                    Entity.Immunity += 500;
                }
                else if (ChiData.TurtleRank <= 50)
                {
                    Entity.Breaktrough += 50;
                    Entity.Counteraction += 50;
                    Entity.Immunity += 200;
                }
            }
            #endregion
            #endregion
            if (Entity.Aura_isActive)
                doAuraBonuses(Entity.Aura_actType, Entity.Aura_actPower, 1);
            else
                removeAuraBonuses(Entity.Aura_actType, Entity.Aura_actPower, 1);
            if (TeamAura)
                doAuraBonuses(TeamAuraStatusFlag, TeamAuraPower, 1);
            else
                removeAuraBonuses(TeamAuraStatusFlag, TeamAuraPower, 1);
            if (this.Entity.MyJiang != null)
            {
                this.Entity.MyJiang.CreateStatusAtributes(this.Entity);
            }
            #region InnerPower
            if (Entity.InnerPower != null)
            {
                Entity.InnerPower.UpdateStatus();
                Entity.Defence += (ushort)Entity.InnerPower.Defence;
                Entity.CriticalStrike += (int)Entity.InnerPower.CriticalStrike;
                Entity.SkillCStrike += (int)Entity.InnerPower.SkillCriticalStrike;
                Entity.Immunity += (int)Entity.InnerPower.Immunity;
                Entity.Breaktrough += (ushort)Entity.InnerPower.Breakthrough;
                Entity.Counteraction += (ushort)Entity.InnerPower.Counteraction;
                Entity.ItemHP += Entity.InnerPower.MaxLife;
                Entity.BaseMaxAttack += Entity.InnerPower.AddAttack;
                Entity.BaseMinAttack += Entity.InnerPower.AddAttack;
                Entity.BaseMagicAttack += Entity.InnerPower.AddMagicAttack;
                Entity.BaseMagicDefence += Entity.InnerPower.AddMagicDefense;
                Entity.PhysicalDamageIncrease += (ushort)Entity.InnerPower.FinalAttack;
                Entity.PhysicalDamageDecrease += (ushort)Entity.InnerPower.FinalDefense;
                Entity.MagicDamageIncrease += (ushort)Entity.InnerPower.FinalMagicAttack;
                Entity.MagicDamageDecrease += (ushort)Entity.InnerPower.FinalMagicDefense;
            }
            #endregion
         #region Format Points
            if ((int)Entity.Spirit >= 540 || (int)Entity.Strength >= 540 || (int)Entity.Vitality >= 540 || (int)Entity.Agility >= 540)
            {
                Entity.Strength = 100;
                Entity.Vitality = 70;
                Entity.Agility = 250;
                Entity.Spirit = 170;
                Entity.Atributes = 118;
                GemAlgorithm();
                CalculateStatBonus();
                CalculateHPBonus();
            }
            #endregion
            Entity.Hitpoints = bStats;
            Entity.BP = Entity.BattlePower;
            CalculateStatBonus();
            CalculateHPBonus();
            ReviewMentor();
            GemAlgorithm();
            new MsgUserTotalRefineLev().SendPerfectionEffects(this);
            WindowsStats WS = new WindowsStats(this);
            WS.Send(this);
        }
        public void doAuraBonuses(ulong type, uint power, int i)
        {
            switch (type)
            {
                case Update.Flags2.EarthAura:
                    {
                        Entity.EarthResistance += (int)power * i;
                        break;
                    }
                case Update.Flags2.FireAura:
                    {
                        Entity.FireResistance += (int)power * i;
                        break;
                    }
                case Update.Flags2.MetalAura:
                    {
                        Entity.MetalResistance += (int)power * i;
                        break;
                    }
                case Update.Flags2.WoodAura:
                    {
                        Entity.WoodResistance += (int)power * i;
                        break;
                    }
                case Update.Flags2.WaterAura:
                    {
                        Entity.WaterResistance += (int)power * i;
                        break;
                    }
                case Update.Flags2.TyrantAura:
                    {
                        Entity.CriticalStrike += (int)power * i * 10;
                        Entity.SkillCStrike += (int)power * i * 10;
                        if (Entity.CriticalStrike > 120000) Entity.CriticalStrike = 120000;
                        if (Entity.SkillCStrike > 120000) Entity.SkillCStrike = 120000;
                        if (Entity.CriticalStrike < 0) Entity.CriticalStrike = 0;
                        if (Entity.SkillCStrike < 0) Entity.SkillCStrike = 0;
                        break;
                    }
                case Update.Flags2.FendAura:
                    {
                        Entity.Immunity += (int)power * i * 100;
                        break;
                    }
            }
        }
        public void removeAuraBonuses(ulong type, uint power, int i)
        {
            switch (type)
            {
                case Update.Flags2.EarthAura:
                    {
                        Entity.EarthResistance -= (int)power * i;
                        break;
                    }
                case Update.Flags2.FireAura:
                    {
                        Entity.FireResistance -= (int)power * i;
                        break;
                    }
                case Update.Flags2.MetalAura:
                    {
                        Entity.MetalResistance -= (int)power * i;
                        break;
                    }
                case Update.Flags2.WoodAura:
                    {
                        Entity.WoodResistance -= (int)power * i;
                        break;
                    }
                case Update.Flags2.WaterAura:
                    {
                        Entity.WaterResistance -= (int)power * i;
                        break;
                    }
                case Update.Flags2.TyrantAura:
                    {
                        Entity.CriticalStrike -= (int)power * i * 10;
                        Entity.SkillCStrike -= (int)power * i * 10;
                        if (Entity.CriticalStrike > 120000) Entity.CriticalStrike = 120000;
                        if (Entity.SkillCStrike > 120000) Entity.SkillCStrike = 120000;
                        if (Entity.CriticalStrike < 0) Entity.CriticalStrike = 0;
                        if (Entity.SkillCStrike < 0) Entity.SkillCStrike = 0;
                        break;
                    }
                case Update.Flags2.FendAura:
                    {
                        Entity.Immunity -= (int)power * i * 100;
                        break;
                    }
            }
        }
        private void loadItemStats(ConquerItem item)
        {
            Entity.BP = Entity.BattlePower;
            if (item.ID == ConquerItem.GoldPrize)
                Entity.WearsGoldPrize = true;
            int position = item.Position;
            bool isOver = false;

            if (isOver = (position > 20))
                position -= 20;
            item.IsWorn = true;
            int plus = item.Plus;
            Database.ConquerItemInformation dbi = new Database.ConquerItemInformation(item.ID, item.Plus);
            if (dbi != null)
            {
                #region Give Stats.

                #region soul stats
                if (item.Purification.PurificationItemID != 0)
                {
                    Database.ConquerItemInformation soulDB = new Database.ConquerItemInformation(item.Purification.PurificationItemID, 0);
                    if (position == ConquerItem.LeftWeapon)
                    {
                        Entity.BaseMinAttack += (uint)(soulDB.BaseInformation.MinAttack / 2);
                        Entity.BaseMaxAttack += (uint)(soulDB.BaseInformation.MaxAttack / 2);
                    }
                    else
                    {
                        Entity.BaseMinAttack += soulDB.BaseInformation.MinAttack;
                        Entity.BaseMaxAttack += soulDB.BaseInformation.MaxAttack;
                    }
                    Entity.ItemHP += soulDB.BaseInformation.ItemHP;
                    Entity.BaseDefence += soulDB.BaseInformation.PhysicalDefence;
                    Entity.MagicDefence += soulDB.BaseInformation.MagicDefence;
                    Entity.Dodge += soulDB.BaseInformation.Dodge;
                    Entity.Accuracy += soulDB.BaseInformation.Accuracy;
                    Entity.BaseMagicAttack += soulDB.BaseInformation.MagicAttack;
                    Entity.MetalResistance += soulDB.BaseInformation.MetalResist;
                    Entity.WoodResistance += soulDB.BaseInformation.WoodResist;
                    Entity.FireResistance += soulDB.BaseInformation.FireResist;
                    Entity.WaterResistance += soulDB.BaseInformation.WaterResist;
                    Entity.EarthResistance += soulDB.BaseInformation.EarthResist;
                    Entity.Breaktrough += soulDB.BaseInformation.BreakThrough;
                    Entity.CriticalStrike += soulDB.BaseInformation.CriticalStrike;
                    Entity.Immunity += soulDB.BaseInformation.Immunity;
                    Entity.Penetration += soulDB.BaseInformation.Penetration;
                    Entity.Counteraction += soulDB.BaseInformation.CounterAction;
                    Entity.Block += soulDB.BaseInformation.Block;
                    Entity.Weight += soulDB.BaseInformation.Weight;
                }
                #endregion
                #region Refinery stats
                Refinery.RefineryItem refine = null;
                if (item.ExtraEffect.Available)
                {
                    if (Kernel.DatabaseRefinery.TryGetValue(item.ExtraEffect.EffectID, out refine))
                    {
                        if (refine != null)
                        {
                            switch (refine.Type)
                            {
                                case Refinery.RefineryItem.RefineryType.Block:
                                    Entity.Block += (UInt16)(refine.Percent * 100);
                                    break;
                                case Refinery.RefineryItem.RefineryType.BreakThrough:
                                    Entity.Breaktrough += (UInt16)((refine.Percent * 10));
                                    break;
                                case Refinery.RefineryItem.RefineryType.Counteraction:
                                    Entity.Counteraction += (UInt16)(refine.Percent * 10);
                                    break;
                                case Refinery.RefineryItem.RefineryType.Critical:
                                    Entity.CriticalStrike += (UInt16)((refine.Percent * 100));
                                    break;
                                case Refinery.RefineryItem.RefineryType.Detoxication:
                                    Entity.Detoxication += (UInt16)(refine.Percent);
                                    break;
                                case Refinery.RefineryItem.RefineryType.Immunity:
                                    Entity.Immunity += (UInt16)(refine.Percent * 100);
                                    break;
                                case Refinery.RefineryItem.RefineryType.Intensification:
                                    Entity.Intensification += (UInt16)(refine.Percent);
                                    break;
                                case Refinery.RefineryItem.RefineryType.Penetration:
                                    Entity.Penetration += (UInt16)(refine.Percent * 100);
                                    break;
                                case Refinery.RefineryItem.RefineryType.SCritical:
                                    Entity.SkillCStrike += (UInt16)(refine.Percent * 100);
                                    break;
                            }
                        }
                    }
                }
                #endregion
                if (item.ID == 2100075 && item.Plus == 12)
                {//egycoo
                    Entity.BaseMinAttack += 8800;
                    Entity.BaseMaxAttack += 9000;
                    Entity.BaseMagicAttack += 9000;
                    Entity.Breaktrough += 30;
                    Entity.Counteraction += 30;
                    Entity.CriticalStrike += 1500;
                    Entity.SkillCStrike += 300;
                    Entity.Immunity += 400;
                }
                if (position == ConquerItem.Tower)
                {
                    Entity.PhysicalDamageDecrease += dbi.BaseInformation.PhysicalDefence;
                    Entity.MagicDamageDecrease += dbi.BaseInformation.MagicDefence;
                }
                else if (position == ConquerItem.Fan)
                {
                    Entity.PhysicalDamageIncrease += dbi.BaseInformation.MaxAttack;
                    Entity.MagicDamageIncrease += dbi.BaseInformation.MagicAttack;
                }

                else if (position == ConquerItem.Wing)
                {
                    Entity.PhysicalDamageIncrease += dbi.BaseInformation.MaxAttack;
                    Entity.MagicDamageIncrease += dbi.BaseInformation.MagicAttack;
                }
                else
                {
                    if (position == ConquerItem.LeftWeapon)
                    {
                        Entity.BaseMagicAttack += (uint)dbi.BaseInformation.MagicAttack / 2;
                        Entity.BaseMinAttack += (uint)dbi.BaseInformation.MinAttack / 2;
                        Entity.BaseMaxAttack += (uint)dbi.BaseInformation.MaxAttack / 2;

                    }
                    else
                    {
                        if (position == ConquerItem.RightWeapon)
                        {
                            Entity.BaseMagicAttack += (uint)(dbi.BaseInformation.MagicAttack);
                            Entity.AttackRange += dbi.BaseInformation.AttackRange;
                            if (Network.PacketHandler.IsTwoHand(dbi.BaseInformation.ID))
                                Entity.AttackRange += 4;
                            else
                                Entity.AttackRange += 3;
                        }
                        Entity.BaseMinAttack += dbi.BaseInformation.MinAttack;
                        Entity.BaseMaxAttack += dbi.BaseInformation.MaxAttack;
                    }

                    Entity.BaseDefence += dbi.BaseInformation.PhysicalDefence;
                    Entity.BaseMagicAttack += dbi.BaseInformation.MagicAttack;
                }
                Entity.ItemHP += dbi.BaseInformation.ItemHP;
                Entity.ItemMP += dbi.BaseInformation.ItemMP;
                Entity.Dodge += dbi.BaseInformation.Dodge;
                Entity.Dexterity += dbi.BaseInformation.Frequency;
                Entity.Weight += dbi.BaseInformation.Weight;
                if (item.Position != ConquerItem.Steed)
                {

                    Entity.ItemBless -= ((double)item.Bless / 100);
                }


                var gem = (int)item.SocketOne;
                if (gem != 0 && gem != 255)
                    Entity.Gems[gem / 10] += GemTypes.Effects[gem / 10][gem % 10];

                gem = (int)item.SocketTwo;
                if (gem != 0 && gem != 255)
                    Entity.Gems[gem / 10] += GemTypes.Effects[gem / 10][gem % 10];

                if (item.Plus > 0)
                {
                    var add = dbi.PlusInformation;
                    Entity.BaseMinAttack += add.MinAttack;
                    Entity.BaseMaxAttack += add.MaxAttack;
                    Entity.BaseMagicAttack += add.MagicAttack;
                    Entity.BaseDefence += add.PhysicalDefence;
                    Entity.Dodge += add.Dodge;
                    Entity.Dexterity += add.Agility;
                    Entity.MagicDefence += add.MagicDefence;
                    Entity.ItemHP += add.ItemHP;
                }
                Entity.ItemHP += item.Enchant;
                var per = 1;
                var per2 = 1;
                Entity.CriticalStrike += (int)dbi.BaseInformation.CriticalStrike / per;
                Entity.SkillCStrike += (int)dbi.BaseInformation.SkillCriticalStrike / per;
                Entity.Immunity += (int)dbi.BaseInformation.Immunity / per;
                Entity.Penetration += (int)dbi.BaseInformation.Penetration / per;
                Entity.Block += (int)dbi.BaseInformation.Block / per;
                Entity.Breaktrough += (int)dbi.BaseInformation.BreakThrough / per2;
                Entity.Counteraction += (int)dbi.BaseInformation.CounterAction / per2;
                Entity.MetalResistance += dbi.BaseInformation.MetalResist;
                Entity.WoodResistance += dbi.BaseInformation.WoodResist;
                Entity.WaterResistance += dbi.BaseInformation.WaterResist;
                Entity.FireResistance += dbi.BaseInformation.FireResist;
                Entity.EarthResistance += dbi.BaseInformation.EarthResist;

                #endregion
            }
        }
        public void GemAlgorithm()
        {

            Entity.MaxAttack = Entity.BaseMaxAttack + Entity.Strength;
            Entity.MinAttack = Entity.BaseMinAttack + Entity.Strength;
            Entity.MagicAttack = Entity.BaseMagicAttack;
        }
        public bool InTournaments()
        {
            return QualifierGroup != null || TeamQualifierGroup != null || ElitePKMatch != null;
        }
        public DynamicVariable this[string variable]
        {
            get { return Variables[variable]; }
            set { Variables[variable] = value; }
        }
        public bool IsWatching()
        {
            return WatchingGroup != null || TeamWatchingGroup != null;
        }
        public bool InQualifier()
        {
            bool inteam = false;
            if (Team != null)
            {
                if (Team.EliteFighterStats != null)
                    inteam = true;
            }
            return QualifierGroup != null || TeamQualifierGroup != null || inteam;
        }
        public bool InTeamQualifier()
        {
            bool inteam = false;
            if (Team != null)
            {
                if (Team.EliteMatch != null)
                    if (Team.EliteMatch.Map != null)
                        if (Team.EliteMatch.Map.ID == Entity.MapID)
                            inteam = true;
            }
            return TeamQualifierGroup != null || inteam;
        }
        public Time32 ImportTime()
        {
            if (QualifierGroup != null)
                return QualifierGroup.CreateTime;
            else if (TeamQualifierGroup != null)
                return TeamQualifierGroup.ImportTime;
            if (Team != null)
            {
                if (Team.EliteMatch != null)
                    return Team.EliteMatch.ImportTime;
            }
            return Time32.Now;
        }
        public void UpdateQualifier(long damage, bool toxicfog = false)
        {

            if (QualifierGroup != null)
                QualifierGroup.UpdateDamage(QualifierGroup.OppositeClient(this), (uint)damage);
            else if (TeamQualifierGroup != null)
            {
                if (TeamQualifierGroup.Player1 != null)
                {
                    TeamQualifierGroup.UpdateDamage(TeamQualifierGroup.Player1, (uint)damage, true);
                }
                if (TeamQualifierGroup.Player2 != null)
                {
                    TeamQualifierGroup.UpdateDamage(TeamQualifierGroup.Player2, (uint)damage, true);
                }
                if (TeamQualifierGroup.Loser != null)
                {
                    TeamQualifierGroup.UpdateDamage(TeamQualifierGroup.Loser, (uint)damage, true);
                }
                if (TeamQualifierGroup.Winner != null)
                {
                    TeamQualifierGroup.UpdateDamage(TeamQualifierGroup.Winner, (uint)damage, true);
                }
            }
            else if (toxicfog)
            {
                if (ElitePKMatch != null)
                {
                    var opponent = ElitePKMatch.targetOf(this);
                    if (opponent != null)
                        opponent.ElitePKStats.Points += (uint)damage;
                    ElitePKMatch.Update();
                }
                else if (Team != null)
                {
                    if (Team.EliteMatch != null)
                    {
                        var opponent = Team.EliteMatch.targetOfWin(this.Team);
                        if (opponent != null)
                        {
                            opponent.Points += (uint)damage;
                            opponent.Team.SendMesageTeam(opponent.Team.EliteMatch.CreateUpdate().Encode(), 0);
                        }
                        Team.SendMesageTeam(Team.EliteMatch.CreateUpdate().Encode(), 0);
                    }
                }
            }
        }
        public uint CurrentHonor
        {
            get
            {
                return ArenaStatistic.CurrentHonor;
            }
            set
            {
                ArenaStatistic.CurrentHonor =
                    TeamArenaStatistic.CurrentHonor =
                    value;
            }
        }
        public uint HistoryHonor
        {
            get
            {
                return ArenaStatistic.HistoryHonor;
            }
            set
            {
                ArenaStatistic.HistoryHonor =
                    TeamArenaStatistic.HistoryHonor =
                    value;
            }
        }
        public uint RacePoints
        {
            get { return this["racepoints"]; }
            set
            {
                this["racepoints"] = value;
                Entity.Update(Update.RaceShopPoints, value, false);
            }
        }
        internal void EndQualifier()
        {


            if (QualifierGroup != null)
                QualifierGroup.End(this);

            if (TeamQualifierGroup != null)
                TeamQualifierGroup.CheckEnd(this);
        }
        internal void Send(string msg, uint type = Network.GamePackets.Message.System)
        {
            Send(new Network.GamePackets.Message(msg, type));
        }
        public string GenerateCaptcha(int len)
        {
            string str = "";
            while (len-- > 0)
            {
                int type = Kernel.Random.Next(0, 3);
                if (type == 0) str += (char)Kernel.Random.Next('0', '9');
                else if (type == 1) str += (char)Kernel.Random.Next('a', 'z');
                else str += (char)Kernel.Random.Next('A', 'Z');
            }
            return str;
        }
        public void MessageBox(string text, Action<GameClient> msg_ok = null, Action<GameClient> msg_cancel = null, uint time = 0)
        {
            if (Entity.MapID == 6000 || Entity.MapID == 6001 || Entity.MapID == 6002 ||
                Entity.MapID == 6003 || Entity.MapID == 6004 || Entity.MapID == 1038 ||
                 PlayRouletteUID > 0) return;
            if (InQualifier())
                return;
            MessageOK = msg_ok;
            MessageCancel = msg_cancel;
            NpcReply msg = new NpcReply(NpcReply.MessageBox, text);
            Send(msg);
            if (time != 0)
                Time(time);
        }
        public void Time(uint time)
        {
            Send(new Data(true) { UID = Entity.UID, dwParam = time, ID = Data.CountDown });
        }
        public bool Online
        {
            get
            {
                return Socket.Connector != null;
            }
        }
        internal void LoadData(bool loadFake = false)
        {
            if (Quests != null)
                Quests.Load();
            else 
            {
                Quests = new Quests(this);
                Quests.Load();
            }
            Database.MailboxTable.Load(this);
            Entity.LoadTopStatus();
            Database.DailyQuestTable.Load(this);
            Database.ActivenessTable.Load(this);
            Database.HouseTable.Load(this);
            Achievement.Load(this);
            Database.KingdomMissionTable.Load(this);
            Database.ChiRetreatTable.Load(this);
            Nyx.Server.Way2Heroes.Load(this);
            Database.ConquerItemTable.LoadItems(this);
            Database.SubClassTable.Load(this.Entity);
            if (!loadFake)
            {
                using (var conn = Database.DataHolder.MySqlConnection)
                {
                    conn.Open();
                    Database.SkillTable.LoadProficiencies(this, conn);
                    Database.SkillTable.LoadSpells(this, conn);
                }
                Database.ClaimItemTable.LoadClaimableItems(this);
                Database.DetainedItemTable.LoadDetainedItems(this);
                Database.KnownPersons.LoadPartner(this);
                Database.KnownPersons.LoadEnemy(this);
                Database.KnownPersons.LoaderFriends(this);
                Database.KnownPersons.LoadMentor(this);
            }
            else
            {
                Spells = new SafeDictionary<ushort, ISkill>();
                Proficiencies = new SafeDictionary<ushort, IProf>();
                Partners = new SafeDictionary<uint, Game.ConquerStructures.Society.TradePartner>();
                Enemy = new SafeDictionary<uint, Game.ConquerStructures.Society.Enemy>();
                ClaimableItem = new SafeDictionary<uint, DetainedItem>();
                DeatinedItem = new SafeDictionary<uint, DetainedItem>();
                Friends = new SafeDictionary<uint, Game.ConquerStructures.Society.Friend>();
                Apprentices = new SafeDictionary<uint, Game.ConquerStructures.Society.Apprentice>();
            }
            Database.ChiTable.Load(this);
        }
        //public void FakeLoad(uint UID)
        //{
        //    if (!Kernel.GamePool.ContainsKey(UID))
        //    {
        //        ReadyToPlay();
        //        this.Account = new Database.AccountTable(null);
        //        this.Account.EntityID = UID;
        //        if (Database.EntityTable.LoadEntity(this))
        //        {
        //            if (this.Entity.FullyLoaded)
        //            {
        //                VariableVault variables;
        //                Database.EntityVariableTable.Load(this.Account.EntityID, out variables);
        //                this.Variables = variables;


        //                if (this.BackupArmorLook != 0)
        //                    this.SetNewArmorLook(this.BackupArmorLook);
        //                else
        //                    this.SetNewArmorLook(this.ArmorLook);
        //                this.SetNewHeadgearLook(this.HeadgearLook);
        //                this.BackupArmorLook = 0;

        //                this.LoadData(true);

        //                if (this.Entity.GuildID != 0)
        //                    this.Entity.GuildBattlePower = this.Guild.GetSharedBattlepower(this.Entity.GuildRank);

        //                this.ReviewMentor();

        //                Network.PacketHandler.LoadEntity(this);

        //                Program.World.Register(this);
        //                Kernel.GamePool.Add(Entity.UID, this);
        //                FakeLoaded = true;
        //                Entity.NobilityRank = NobilityInformation.Rank;
        //            }
        //        }
        //    }
        //}
        public void FakeLoad2(uint UID, string Name = "")
        {
            if (Name == "")
                Name = "Booth[" + UID + "]";
            if (!Kernel.GamePool.ContainsKey(UID))
            {
                this.ReadyToPlay();
                this.Account = new Database.AccountTable(null);
                this.Account.EntityID = UID;
                this.Entity = new Entity(EntityFlag.Player, false);
                this.Entity.Owner = this;
                this.Entity.Name = Name;
                this.Entity.UID = UID;
                this.Entity.Vitality = 537;
                this.Entity.Face = 37;
                this.Entity.Body = 1003;
                this.Entity.HairStyle = 630;
                this.Entity.Level = 140;
                this.Entity.Class = 15;
                this.Entity.Reborn = 2;
                this.Entity.MaxHitpoints = 60000;
                this.Entity.Hitpoints = this.Entity.MaxHitpoints;
                this.Entity.Mana = 800;

                this.Variables = new VariableVault();
                this.Friends = new SafeDictionary<uint, Game.ConquerStructures.Society.Friend>();
                this.Enemy = new SafeDictionary<uint, Game.ConquerStructures.Society.Enemy>();
                this.ChiData = new Database.ChiTable.ChiData();
                this.ChiPowers = new List<ChiPowerStructure>();


                this.NobilityInformation = new Nyx.Server.Game.ConquerStructures.NobilityInformation();
                this.NobilityInformation.EntityUID = this.Entity.UID;
                this.NobilityInformation.Name = this.Entity.Name;
                this.NobilityInformation.Donation = 0;
                this.NobilityInformation.Rank = Game.ConquerStructures.NobilityRank.Serf;
                this.NobilityInformation.Position = -1;
                this.NobilityInformation.Gender = 1;
                this.NobilityInformation.Mesh = this.Entity.Mesh;
                if (this.Entity.Body % 10 >= 3)
                    this.NobilityInformation.Gender = 0;

                this.TeamArenaStatistic = new Nyx.Server.Network.GamePackets.TeamArenaStatistic(true);
                this.TeamArenaStatistic.EntityID = this.Entity.UID;
                this.TeamArenaStatistic.Name = this.Entity.Name;
                this.TeamArenaStatistic.Level = this.Entity.Level;
                this.TeamArenaStatistic.Class = this.Entity.Class;
                this.TeamArenaStatistic.Model = this.Entity.Mesh;
                this.TeamArenaStatistic.Status = Network.GamePackets.TeamArenaStatistic.NotSignedUp;

                this.ArenaStatistic = new Nyx.Server.Network.GamePackets.ArenaStatistic(true);
                this.ArenaStatistic.EntityID = this.Entity.UID;
                this.ArenaStatistic.Name = this.Entity.Name;
                this.ArenaStatistic.Level = this.Entity.Level;
                this.ArenaStatistic.Class = this.Entity.Class;
                this.ArenaStatistic.Model = this.Entity.Mesh;
                this.ArenaPoints = Database.ArenaTable.ArenaPointFill(this.Entity.Level);
                this.ArenaStatistic.LastArenaPointFill = DateTime.Now;
                this.ArenaStatistic.Status = Network.GamePackets.ArenaStatistic.NotSignedUp;

                this.ChiPowers = new List<ChiPowerStructure>();
                this.ChiData = new Database.ChiTable.ChiData() { Name = this.Entity.Name, UID = this.Entity.UID, Powers = this.ChiPowers };

                this.Entity.Stamina = 150;

                this.Spells = new SafeDictionary<ushort, Interfaces.ISkill>();
                this.Proficiencies = new SafeDictionary<ushort, Interfaces.IProf>();

                PacketHandler.LoadEntity(this);
            }
        }  
    }
}