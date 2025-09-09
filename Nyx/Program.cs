using Nyx.Server.Client;
using Nyx.Server.Database;
using Nyx.Server.Game;
using Nyx.Server.Game.Npc;
using Nyx.Server.Network;
using Nyx.Server.Network.AuthPackets;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network.GamePackets.Union;
using Nyx.Server.Network.Sockets;
using Org.BouncyCastle.Bcpg;
using Serilog;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
namespace Nyx.Server
{
    public class Program
    {

        //[DllImport("kernel32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool SetProcessWorkingSetSize(IntPtr process,
        //    UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
        //[DllImport("user32.dll")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32.dll")]
        //public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static DateTime LastRandomReset = DateTime.Now;
        public static Encoding Encoding = ASCIIEncoding.Default;
        public static CachedAttributeInvocation<Func<Client.GameClient, byte[], Task>, PacketAttribute, ushort> MsgInvoker;
        public static CachedAttributeInvocation<Func<Client.GameClient, byte[], Task>, Game.Npc.NpcAttribute, NpcID> MsgNpc;
        //private static Native.ConsoleEventHandler Nyx.ServerHandler;
        public static Client.GameClient[] GamePool = new Client.GameClient[0];
        public static Client.GameClient[] Values = new Client.GameClient[0];

        public static void UpdateConsoleTitle()
        {
            if (Kernel.GamePool.Count > Program.MaxOn)
                Program.MaxOn = Kernel.GamePool.Count;
            if (Kernel.GamePool.Count != 0)
            {
                Console.Title = ">>>Online Players: [ " + Kernel.GamePool.Count + " ] Max Online: [ " + Program.MaxOn + " ]<<<";
            }
            else if (Kernel.GamePool.Count == 0)
            {
                Console.Title = ">>>No Online Players Now!! But Max Online: [ " + Program.MaxOn + " ]<<<";
            }
        }
        public static void Save()
        {
            Log.Information("Starting server save process...");
            try
            {
                using (var conn = Database.DataHolder.MySqlConnection)
                {
                    conn.Open();
                    foreach (Client.GameClient client in Kernel.GamePool.Values)
                    {
                        client.Account.Save(client);
                        Database.EntityTable.SaveEntity(client, conn);
                        Database.DailyQuestTable.Save(client);
                        Database.SkillTable.SaveProficiencies(client, conn);
                        Database.ActivenessTable.Save(client);
                        Database.ChiTable.Save(client);
                        Database.SkillTable.SaveSpells(client, conn);
                        Database.MailboxTable.Save(client);
                        Database.ArenaTable.SaveArenaStatistics(client.ArenaStatistic, client.CP, conn);
                        Database.TeamArenaTable.SaveArenaStatistics(client.TeamArenaStatistic, conn);
                    }
                }
                Nyx.Server.Database.JiangHu.SaveJiangHu();
                AuctionBase.Save();
                Database.Flowers.SaveFlowers();
                Database.InnerPowerTable.Save();
                Database.EntityVariableTable.Save(0, Vars);
                using (MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT))
                {
                    cmd.Select("configuration");
                    using (MySqlReader r = new MySqlReader(cmd))
                    {
                        if (r.Read())
                        {
                            new Database.MySqlCommand(Database.MySqlCommandType.UPDATE).Update("configuration").Set("ServerKingdom", Kernel.ServerKingdom).Set("ItemUID", Network.GamePackets.ConquerItem.ItemUID.Now).Set("GuildID", Game.ConquerStructures.Society.Guild.GuildCounter.Now).Set("UnionID", Union.UnionCounter.Now).Execute();
                            if (r.ReadByte("LastDailySignReset") != DateTime.Now.Month) MsgSignIn.Reset();
                        }
                    }
                }
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("configuration"))
                    cmd.Set("LastDailySignReset", DateTime.Now.Month).Execute();
            }
            catch (Exception e)
            {
                LoggingService.SystemError("Save", "Error during server save", e);
                Console.WriteLine(e);
            }
        }
        static void GameServer_OnClientReceive(byte[] buffer, int length, ClientWrapper obj)
        {
            if (obj.Connector == null)
            {
                obj.Disconnect();
                return;
            }

            Client.GameClient Client = obj.Connector as Client.GameClient;

            if (Client.Exchange)
            {
                Client.Exchange = false;
                Client.Action = 1;
                var crypto = new Network.Cryptography.GameCryptography(global::System.Text.Encoding.Default.GetBytes(Constants.GameCryptographyKey));
                byte[] otherData = new byte[length];
                Array.Copy(buffer, otherData, length);
                crypto.Decrypt(otherData, length);

                bool extra = false;
                int pos = 0;
                if (BitConverter.ToInt32(otherData, length - 140) == 128)//no extra packet
                {
                    pos = length - 140;
                    Client.Cryptography.Decrypt(buffer, length);
                }
                else if (BitConverter.ToInt32(otherData, length - 176) == 128)//extra packet
                {
                    pos = length - 176;
                    extra = true;
                    Client.Cryptography.Decrypt(buffer, length - 36);
                }
                int len = BitConverter.ToInt32(buffer, pos); pos += 4;
                if (len != 128)
                {
                    Client.Disconnect();
                    return;
                }
                byte[] pubKey = new byte[128];
                for (int x = 0; x < len; x++, pos++) pubKey[x] = buffer[pos];

                string PubKey = global::System.Text.Encoding.Default.GetString(pubKey);
                Client.Cryptography = Client.DHKeyExchange.HandleClientKeyPacket(PubKey, Client.Cryptography);

                if (extra)
                {
                    byte[] data = new byte[36];
                    Buffer.BlockCopy(buffer, length - 36, data, 0, 36);
                    processData(data, 36, Client);
                }
            }
            else
            {
                processData(buffer, length, Client);
            }
        }
        private static void processData(byte[] buffer, int length, Client.GameClient Client)
        {
            try
            {
                Client.Cryptography.Decrypt(buffer, length);
                Client.Queue.Enqueue(buffer, length);
                if (Client.Queue.CurrentLength > 1224)
                {
                    LoggingService.SystemWarning("PacketHandler", $"Packet size too large: {Client.Queue.CurrentLength} bytes from {Client.Entity?.Name ?? "Unknown"}");
                    Console.WriteLine("[Disconnect]Reason:The packet size is too big. " + Client.Queue.CurrentLength);
                    Client.Disconnect();
                    return;
                }
                while (Client.Queue.CanDequeue())
                {
                    byte[] data = Client.Queue.Dequeue();
                    if (data.Length >= 2)
                    {
                        ushort packetId = BitConverter.ToUInt16(data, 2); // Packet ID is at offset 2
                        LoggingService.ClientPacketReceived(Client.Entity?.Name ?? "Unknown", packetId, data.Length);

                        // Track packet performance
                        PacketPerformanceTracker.TrackPacketProcessing(packetId, () =>
                        {
                            try
                            {
                                // Use packet attribute system for automatic handler discovery
                                var handler = MsgInvoker[packetId];
                                if (handler != null)
                                {
                                    handler.Invoke(Client, data);
                                }
                                else
                                {
                                    LoggingService.SystemWarning("PacketHandler", $"No packet handler found for ID {packetId} from {Client.Entity?.Name ?? "Unknown"}");
                                }

                                // Handle NPC interactions using NpcAttribute system
                                if (packetId == 2031 || packetId == 2032)
                                {
                                    try
                                    {
                                        var npcRequest = new Network.GamePackets.NpcRequest();
                                        npcRequest.Deserialize(data);
                                        
                                        var npcHandler = MsgNpc[(NpcID)npcRequest.NpcID];
                                        if (npcHandler != null)
                                        {
                                            npcHandler.Invoke(Client, data);
                                        }
                                    }
                                    catch (Exception npcEx)
                                    {
                                        LoggingService.SystemError("NpcHandler", $"Error processing NPC interaction from {Client.Entity?.Name ?? "Unknown"}", npcEx);
                                    }
                                }

                                Network.PacketHandler.HandlePacket(data, Client);
                            }
                            catch (Exception ex)
                            {
                                PacketErrorHandler.HandlePacketError(ex, Client, data, packetId);
                            }
                        });
                    }
                    else
                    {
                        LoggingService.SystemWarning("PacketHandler", $"Invalid packet size: {data.Length} bytes from {Client.Entity?.Name ?? "Unknown"}");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("PacketProcessing", $"Critical error in packet processing for {Client.Entity?.Name ?? "Unknown"}: {ex.Message}", ex);
                Client.Disconnect();
            }
        }
        static void GameServer_OnClientConnect(ClientWrapper obj)
        {
            LoggingService.ClientConnected(obj.IP);
            Client.GameClient client = new Client.GameClient(obj);
            client.Send(client.DHKeyExchange.CreateServerKeyPacket());
            obj.Connector = client;
        }
        static void GameServer_OnClientDisconnect(ClientWrapper obj)
        {
            if (obj.Connector != null)
            {
                var client = obj.Connector as Client.GameClient;
                LoggingService.ClientDisconnected(obj.IP, client?.Entity?.Name);
                client.Disconnect();
            }
            else
            {
                LoggingService.ClientDisconnected(obj.IP);
                obj.Disconnect();
            }
        }
        static void AuthServer_OnClientReceive(byte[] buffer, int length, ClientWrapper arg3)
        {
            var player = arg3.Connector as Client.AuthClient;
            AuthClient authClient = arg3.Connector as AuthClient;
            player.Cryptographer.Decrypt(buffer, length);
            player.Queue.Enqueue(buffer, length);
            while (player.Queue.CanDequeue())
            {
                byte[] packet = player.Queue.Dequeue();

                ushort len = BitConverter.ToUInt16(packet, 0);
                ushort id = BitConverter.ToUInt16(packet, 2);

                if (len == 312)
                {
                    player.Info = new Authentication();
                    player.Info.Deserialize(packet);
                    player.Account = new AccountTable(player.Info.Username);
                    if (!BruteForceProtection.AcceptJoin(arg3.IP))
                    {
                        LoggingService.ClientBruteForceDetected(arg3.IP, 5);
                        Console.WriteLine(string.Concat(new string[] { "Client > ", player.Info.Username, "was blocked address", arg3.IP, "!" }));
                        arg3.Disconnect();
                        break;
                    }
                    Forward Fw = new Forward();
                    if (player.Account.Password == player.Info.Password && player.Account.exists)
                    {
                        LoggingService.ClientLoginSuccess(player.Info.Username, arg3.IP);
                        Fw.Type = Forward.ForwardType.Ready;
                    }
                    else
                    {
                        LoggingService.ClientLoginFailed(player.Info.Username, arg3.IP, "Invalid credentials");
                        BruteForceProtection.ClientRegistred(arg3.IP);
                        Fw.Type = Forward.ForwardType.InvalidInfo;
                    }
                    if (IPBan.IsBanned(arg3.IP))
                    {
                        LoggingService.IPBanned(arg3.IP, "IP is banned");
                        Fw.Type = Forward.ForwardType.Banned;
                        player.Send(Fw);
                        return;
                    }
                    if (Fw.Type == Network.AuthPackets.Forward.ForwardType.Ready)
                    {
                        Fw.Identifier = player.Account.GenerateKey();
                        Kernel.AwaitingPool[Fw.Identifier] = player.Account;
                        Fw.IP = GameIP;
                        Fw.Port = GamePort;
                    }
                    player.Send(Fw);
                }
            }
        }
        public static void LoadServer(bool KnowConfig)
        {
            try
            {
                // Initialize logging system
                LoggingService.Initialize();
                LoggingService.ServerStart("Nyx.Server Game Server", "1.0.0");

                RandomSeed = Convert.ToInt32(DateTime.Now.Ticks.ToString().Remove(DateTime.Now.Ticks.ToString().Length / 2));
                Log.Information("Initializing server with random seed: {RandomSeed}", RandomSeed);
                Kernel.Random = new FastRandom(RandomSeed);
                #region Configuration

                ServerSettings settings = new ServerSettings();
                AbstractDbContext.Configuration = settings.Database;
                Log.Information("Database settings: {0} {1} **** {2}", settings.Database.Hostname, settings.Database.Username, settings.Database.Schema);
                Log.Information("Loading server configuration...");
                if (!KnowConfig)
                {
                    DatabaseName = settings.Database.Schema;
                    DatabasePass = settings.Database.Password;
                    Log.Information("Using default database configuration");
                }

                try
                {
                    Database.DataHolder.CreateConnection(DatabaseName, DatabasePass);
                    LoggingService.DatabaseConnected(DatabaseName);
                }
                catch (Exception ex)
                {
                    LoggingService.DatabaseConnectionFailed(DatabaseName, ex);
                    throw;
                }

                Database.EntityTable.EntityUID = new Counter(0);
                new MySqlCommand(MySqlCommandType.UPDATE).Update("entities").Set("Online", 0).Execute();
                using (MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT))
                {
                    cmd.Select("configuration");
                    using (MySqlReader r = new MySqlReader(cmd))
                    {
                        if (r.Read())
                        {
                            if (!KnowConfig)
                            {
                                GameIP = r.ReadString("ServerIP");
                                GamePort = 5816;
                                AuthPort = r.ReadUInt16("ServerPort");
                            }
                            Database.EntityTable.EntityUID = new Counter(r.ReadUInt32("EntityID"));
                            if (Database.EntityTable.EntityUID.Now == 0)
                                Database.EntityTable.EntityUID.Now = 1;
                            Union.UnionCounter = new Counter(r.ReadUInt32("UnionID"));
                            Kernel.ServerKingdom = (r.ReadUInt32("ServerKingdom"));
                            if (r.ReadByte("LastDailySignReset") != DateTime.Now.Month) MsgSignIn.Reset();
                            Game.ConquerStructures.Society.Guild.GuildCounter = new Nyx.Server.Counter(r.ReadUInt32("GuildID"));
                            Network.GamePackets.ConquerItem.ItemUID = new Nyx.Server.Counter(r.ReadUInt32("ItemUID"));
                            Constants.ExtraExperienceRate = r.ReadUInt32("ExperienceRate");
                            Constants.ExtraSpellRate = r.ReadUInt32("ProficiencyExperienceRate");
                            Constants.ExtraProficiencyRate = r.ReadUInt32("SpellExperienceRate");
                            Constants.MoneyDropRate = r.ReadUInt32("MoneyDropRate");
                            Constants.ConquerPointsDropRate = r.ReadUInt32("ConquerPointsDropRate");
                            Constants.ItemDropRate = r.ReadUInt32("ItemDropRate");
                            Constants.ItemDropQualityRates = r.ReadString("ItemDropQualityString").Split('~');
                            Database.EntityVariableTable.Load(0, out Vars);
                        }
                    }
                }
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("configuration"))
                    cmd.Set("LastDailySignReset", DateTime.Now.Month).Execute();
                #endregion
                Database.JiangHu.LoadStatus();
                Database.JiangHu.LoadJiangHu();
                Way2Heroes.Load();
                QuestInfo.Load();
                AuctionBase.Load();
                Database.DataHolder.ReadStats();
                Nyx.Server.Soul.SoulProtection.Load();
                Database.PerfectionTable.Load();
                Database.LotteryTable.Load();
                Database.ConquerItemTable.ClearNulledItems();
                Database.ConquerItemInformation.Load();
                Database.MonsterInformation.Load();
                Database.IPBan.Load();
                Database.SpellTable.Load();
                Database.ShopFile.Load();
                Database.HonorShop.Load();
                Database.RacePointShop.Load();
                Database.ChampionShop.Load();
                Database.EShopFile.Load();
                Database.EShopV2File.Load();
                Database.MapsTable.Load();
                Database.Flowers.LoadFlowers();
                Database.NobilityTable.Load();
                Database.ArenaTable.Load();
                Database.TeamArenaTable.Load();
                Database.GuildTable.Load();
                Database.ChiTable.LoadAllChi();
                Refinery.LoadItems();
                StorageManager.Load();
                Database.StorageItem.Load();
                UnionTable.Load();
                // Use improved threading and world management
                var threadingManager = ImprovedThreadingManager.Instance;
                World = new ImprovedWorld();
                World.Init();
                Database.InnerPowerTable.LoadDBInformation();
                Database.InnerPowerTable.Load();
                Map.CreateTimerFactories();
                Database.SignInTable.Load();
                Database.DMaps.Load();
                Game.Screen.CreateTimerFactories();
                World.CreateTournaments();
                Game.GuildWar.Initiate();
                Game.ClanWar.Initiate();
                Game.Tournaments.SkillTournament.LoadSkillTop8();
                Game.Tournaments.TeamTournament.LoadTeamTop8();
                Clan.LoadClans();
                Booths.Load();
                Database.FloorItemTable.Load();
                Database.ReincarnationTable.Load();
                new MsgUserAbilityScore().GetRankingList();
                new MsgEquipRefineRank().UpdateRanking();
                BruteForceProtection.CreatePoll();
                {
                    Client.GameClient gc = new Client.GameClient(new ClientWrapper());
                    gc.Account = new AccountTable("NONE");
                    gc.Socket.Alive = false;
                    gc.Entity = new Entity(EntityFlag.Player, false) { Name = "NONE" };
                    Npcs.GetDialog(new NpcRequest(), gc, true);
                }
                #region OpenSocket
                Network.Cryptography.AuthCryptography.PrepareAuthCryptography();

                // Initialize network manager
                var networkManager = NetworkManager.Instance;

                // Initialize packet attribute system
                MsgInvoker = new CachedAttributeInvocation<Func<Client.GameClient, byte[], Task>, PacketAttribute, ushort>(PacketAttribute.Translator);
                MsgNpc = new CachedAttributeInvocation<Func<Client.GameClient, byte[], Task>, Game.Npc.NpcAttribute, NpcID>(Game.Npc.NpcAttribute.Translator);

                // Use improved server sockets for better client handling
                AuthServer = new Network.Sockets.ImprovedServerSocket();
                AuthServer.OnClientConnect += (client) =>
                {
                    // Register with network manager
                    networkManager.RegisterConnection(client);
                    AuthServer_OnClientConnect(client);
                };
                AuthServer.OnClientReceive += AuthServer_OnClientReceive;
                AuthServer.OnClientDisconnect += (client) =>
                {
                    // Unregister from network manager
                    networkManager.UnregisterConnection(client);
                    AuthServer_OnClientDisconnect(client);
                };
                AuthServer.Enable(AuthPort, "0.0.0.0");

                GameServer = new Network.Sockets.ImprovedServerSocket();
                GameServer.OnClientConnect += (client) =>
                {
                    // Register with network manager
                    networkManager.RegisterConnection(client);
                    GameServer_OnClientConnect(client);
                };
                GameServer.OnClientReceive += GameServer_OnClientReceive;
                GameServer.OnClientDisconnect += (client) =>
                {
                    // Unregister from network manager
                    networkManager.UnregisterConnection(client);
                    GameServer_OnClientDisconnect(client);
                };
                GameServer.Enable(GamePort, "0.0.0.0");
                #endregion
                Console.Clear();
                Console.Title = "Server Running Normally!";
                Console.WriteLine("Server Loaded Successfully!!");
                Console.WriteLine("Nyx.Server!!");

                LoggingService.ServerStarted("Nyx.Server Game Server", GamePort);
                Log.Information("Server initialization completed successfully");

                // Start periodic performance monitoring
                StartPerformanceMonitoring();

                //Nyx.ServerHandler += Nyx.ServerConsole_CloseEvent;
                //Native.SetConsoleCtrlHandler(Nyx.ServerHandler, true);

                StartConsoleCommandHandler();
            }
            catch (Exception ex)
            {
                LoggingService.ServerError("Nyx.Server Game Server", ex);
                Console.WriteLine($"Critical error during server startup: {ex.Message}");
                throw;
            }
        }
        static void AuthServer_OnClientDisconnect(ClientWrapper obj)
        {
            obj.Disconnect();
        }
        static void AuthServer_OnClientConnect(ClientWrapper obj)
        {
            Client.AuthClient authState;
            obj.Connector = (authState = new Client.AuthClient(obj));
            authState.Cryptographer = new Network.Cryptography.AuthCryptography();
            Network.AuthPackets.PasswordCryptographySeed pcs = new PasswordCryptographySeed();
            pcs.Seed = Kernel.Random.Next();
            authState.PasswordSeed = pcs.Seed;
            authState.Send(pcs);
        }
        public static Network.Sockets.ImprovedServerSocket AuthServer;
        public static Network.Sockets.ImprovedServerSocket GameServer;
        public static string GameIP;
        public static ushort GamePort;
        public static string DatabaseName;
        public static string DatabasePass;
        public static ushort AuthPort;
        public static ImprovedWorld World;
        public static VariableVault Vars;
        public static int MaxOn = 0;
        public static int RandomSeed = 0;
        static void Main()
        {
            try
            {
                LoadServer(false);
            }
            catch (Exception ex)
            {
                LoggingService.ServerError("Nyx.Server Game Server", ex);
                Console.WriteLine($"Fatal error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        internal static void CommandsAI(string p)
        {
            throw new NotImplementedException();
        }

        private static void StartPerformanceMonitoring()
        {
            // Start a background task to log performance statistics every 5 minutes
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(5));
                    try
                    {
                        PacketPerformanceTracker.LogPacketStatistics();
                        PacketPerformanceTracker.LogSlowPackets();
                        PacketErrorHandler.LogErrorStatistics();
                    }
                    catch (Exception ex)
                    {
                        LoggingService.SystemError("PerformanceMonitoring", "Error in performance monitoring", ex);
                    }
                }
            });
        }

        private static void StartConsoleCommandHandler()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                        continue;

                    var args = input.Trim().Split(' ');
                    var command = args[0].ToLower();

                    switch (command)
                    {
                        case "exit":
                        case "quit":
                            Console.WriteLine("Shutting down server...");
                            Environment.Exit(0);
                            break;
                        case "save":
                            Save();
                            Console.WriteLine("Server state saved.");
                            break;
                        case "players":
                            Console.WriteLine($"Online Players: {Kernel.GamePool.Count}");
                            break;
                        case "cpanal":
                            AdminTools.ControlPanel cp = new AdminTools.ControlPanel();
                            cp.ShowDialog();
                            break;
                        default:
                            Console.WriteLine($"Unknown command: {command}");
                            break;
                    }
                }
            });
        }
    }
}