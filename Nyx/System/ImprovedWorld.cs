using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Nyx.Server.Game;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network.Sockets;
using Nyx.Server.Game.ConquerStructures;
using Nyx.Server.Game.ConquerStructures.Society;
using Nyx.Server.Client;
using System.Drawing;
using Nyx.Server.Database;
using Nyx.Server.Interfaces;
using Nyx.Server.Network;
using Message = Nyx.Server.Network.GamePackets.Message;
using System.Threading.Generic;

namespace Nyx.Server
{
    public class ImprovedWorld : World
    {
        private readonly ImprovedThreadingManager _threadingManager;
        private readonly Dictionary<string, IDisposable> _timerSubscriptions;
        
        // Performance tracking
        private DateTime _lastPerformanceLog;
        private int _totalProcessedClients;
        private readonly object _statsLock = new object();

        public ImprovedWorld() : base()
        {
            _threadingManager = ImprovedThreadingManager.Instance;
            _timerSubscriptions = new Dictionary<string, IDisposable>();
            _lastPerformanceLog = DateTime.Now;
            
            LoggingService.SystemInfo("World", "Improved World initialized");
        }

        public new void Init()
        {
            // Initialize threading manager first
            _ = ImprovedThreadingManager.Instance;
            
            // Use improved threading for timer rules
            InitializeImprovedTimers();
            
            // Initialize base functionality
            base.Init();
            
            LoggingService.SystemInfo("World", "Improved World initialization complete");
        }

        private void InitializeImprovedTimers()
        {
            try
            {
                // Game logic timers - use game logic pool
                Characters = new TimerRule<GameClient>(ImprovedCharactersCallback, 1000, ThreadPriority.BelowNormal);
                AutoAttack = new TimerRule<GameClient>(ImprovedAutoAttackCallback, 1000, ThreadPriority.BelowNormal);
                Companions = new TimerRule<GameClient>(ImprovedCompanionsCallback, 1000, ThreadPriority.BelowNormal);
                Prayer = new TimerRule<GameClient>(ImprovedPrayerCallback, 1000, ThreadPriority.BelowNormal);
                
                // Network timers - use network pool for better performance
                ConnectionReview = new TimerRule<ClientWrapper>(ImprovedConnectionReview, 60000, ThreadPriority.Lowest);
                ConnectionReceive = new TimerRule<ClientWrapper>(ImprovedConnectionReceive, 1);
                ConnectionSend = new TimerRule<ClientWrapper>(ImprovedConnectionSend, 1);
                
                // Server functions - use background pool
                _timerSubscriptions["ServerFunctions"] = _threadingManager.SubscribeBackground(ImprovedServerFunctions, 5000);
                _timerSubscriptions["WorldTournaments"] = _threadingManager.SubscribeGameLogic(ImprovedWorldTournaments, 1000);
                _timerSubscriptions["ArenaFunctions"] = _threadingManager.SubscribeHighPriority(ImprovedArenaFunctions, 1000);
                _timerSubscriptions["TeamArenaFunctions"] = _threadingManager.SubscribeHighPriority(ImprovedTeamArenaFunctions, 1000);
                
                LoggingService.SystemInfo("World", "Improved timers initialized");
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", "Error initializing improved timers", ex);
            }
        }

        #region Improved Callback Methods

        private void ImprovedCharactersCallback(GameClient client, int time)
        {
            try
            {
                if (!Valid(client)) return;
                
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                
                // Process character logic
                ProcessCharacterLogic(client, time);
                
                stopwatch.Stop();
                UpdateClientProcessingStats(stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved characters callback for {client?.Entity?.Name}", ex);
            }
        }

        private void ImprovedAutoAttackCallback(GameClient client, int time)
        {
            try
            {
                if (!Valid(client)) return;
                
                // Use high priority pool for combat
                _threadingManager.ExecuteHighPriority(t => ProcessAutoAttack(client, t), 100);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved auto attack callback for {client?.Entity?.Name}", ex);
            }
        }

        private void ImprovedCompanionsCallback(GameClient client, int time)
        {
            try
            {
                if (!Valid(client)) return;
                
                // Use background pool for companion logic
                _threadingManager.ExecuteBackground(t => ProcessCompanions(client, t), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved companions callback for {client?.Entity?.Name}", ex);
            }
        }

        private void ImprovedPrayerCallback(GameClient client, int time)
        {
            try
            {
                if (!Valid(client)) return;
                
                // Use game logic pool for prayer processing
                _threadingManager.ExecuteGameLogic(t => ProcessPrayer(client, t), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved prayer callback for {client?.Entity?.Name}", ex);
            }
        }

        private void ImprovedConnectionReview(ClientWrapper wrapper, int time)
        {
            try
            {
                // Use network pool for connection management
                _threadingManager.ExecuteNetwork(t => ClientWrapper.TryReview(wrapper), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved connection review for {wrapper?.IP}", ex);
            }
        }

        private void ImprovedConnectionReceive(ClientWrapper wrapper, int time)
        {
            try
            {
                // Use network pool for receive operations
                _threadingManager.ExecuteNetwork(t => ClientWrapper.TryReceive(wrapper), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved connection receive for {wrapper?.IP}", ex);
            }
        }

        private void ImprovedConnectionSend(ClientWrapper wrapper, int time)
        {
            try
            {
                // Use network pool for send operations
                _threadingManager.ExecuteNetwork(t => ClientWrapper.TrySend(wrapper), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", $"Error in improved connection send for {wrapper?.IP}", ex);
            }
        }

        private void ImprovedServerFunctions(int time)
        {
            try
            {
                // Use background pool for server maintenance
                _threadingManager.ExecuteBackground(t => ProcessServerFunctions(t), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", "Error in improved server functions", ex);
            }
        }

        private void ImprovedWorldTournaments(int time)
        {
            try
            {
                // Use game logic pool for tournament processing
                _threadingManager.ExecuteGameLogic(t => ProcessWorldTournaments(t), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", "Error in improved world tournaments", ex);
            }
        }

        private void ImprovedArenaFunctions(int time)
        {
            try
            {
                // Use high priority pool for arena operations
                _threadingManager.ExecuteHighPriority(t => ProcessArenaFunctions(t), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", "Error in improved arena functions", ex);
            }
        }

        private void ImprovedTeamArenaFunctions(int time)
        {
            try
            {
                // Use high priority pool for team arena operations
                _threadingManager.ExecuteHighPriority(t => ProcessTeamArenaFunctions(t), 0);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", "Error in improved team arena functions", ex);
            }
        }

        #endregion

        #region Processing Methods

        private void ProcessCharacterLogic(GameClient client, int time)
        {
            Time32 Now = new Time32(time);
            
            // Process character-specific logic
            ProcessCharacterBuffs(client, Now);
            ProcessCharacterEffects(client, Now);
            ProcessCharacterEvents(client, Now);
        }

        private void ProcessCharacterBuffs(GameClient client, Time32 now)
        {
            var entity = client.Entity;
            if (entity == null) return;

            // Process buff effects
            if (entity.IncreaseFinalMDamage && now > entity.IncreaseFinalMDamageStamp.AddSeconds(80))
            {
                entity.IncreaseFinalMDamage = false;
                client.LoadItemStats();
            }

            if (entity.IncreaseFinalPDamage && now > entity.IncreaseFinalPDamageStamp.AddSeconds(80))
            {
                entity.IncreaseFinalPDamage = false;
                client.LoadItemStats();
            }

            // Process other buffs...
            ProcessOtherBuffs(client, now);
        }

        private void ProcessCharacterEffects(GameClient client, Time32 now)
        {
            var entity = client.Entity;
            if (entity == null) return;

            // Process special effects
            if (entity.MapID == 1002 && entity.mdf31 > 0)
            {
                entity.mdf31 -= 1;
            }

            // Process Godly Shield
            if (entity.GodlyShield && entity.ContainsFlag((ulong)Network.GamePackets.Update.Flags.GodlyShield))
            {
                if (now > entity.GodlyShieldStamp.AddSeconds(80))
                {
                    entity.RemoveFlag((ulong)Network.GamePackets.Update.Flags.GodlyShield);
                    entity.GodlyShield = false;
                }
            }
        }

        private void ProcessCharacterEvents(GameClient client, Time32 now)
        {
            // Process scheduled events
            ProcessPowerArenaEvents(client);
        }

        private void ProcessPowerArenaEvents(GameClient client)
        {
            var now = DateTime.Now;
            
            if (((now.Hour == 12 && now.Minute == 55) || (now.Hour == 19 && now.Minute == 55)) && now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 5 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
            
            if (((now.Hour == 12 && now.Minute == 56) || (now.Hour == 19 && now.Minute == 56)) && now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 4 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
        }

        private void ProcessOtherBuffs(GameClient client, Time32 now)
        {
            var entity = client.Entity;
            if (entity == null) return;

            // Process all other buff effects
            var buffs = new[]
            {
                (entity.IncreaseFinalMAttack, entity.IncreaseFinalMAttackStamp),
                (entity.IncreaseFinalPAttack, entity.IncreaseFinalPAttackStamp),
                (entity.IncreaseImunity, entity.IncreaseImunityStamp),
                (entity.IncreaseAntiBreack, entity.IncreaseAntiBreackStamp),
                (entity.IncreasePStrike, entity.IncreasePStrikeStamp),
                (entity.IncreaseBreack, entity.IncreaseBreackStamp),
                (entity.IncreaseAttribute, entity.IncreaseAttributeStamp)
            };

            foreach (var (isActive, stamp) in buffs)
            {
                if (isActive && now > stamp.AddSeconds(80))
                {
                    // Reset the buff - this would need to be implemented per buff type
                    // For now, just log that we need to handle this
                    LoggingService.SystemDebug("World", $"Buff expired for {entity.Name}");
                }
            }
        }

        public void CreateTournaments()
        {
            SteedRace = new SteedRace();
            ElitePKTournament.Create();
            Game.Features.Tournaments.TeamElitePk.TeamTournament.Create();
            Game.Features.Tournaments.TeamElitePk.SkillTeamTournament.Create();
            CTF = new CaptureTheFlag();
            DelayedTask = new Joseph.DelayedTask();
            Auction = new Auction();
        }

        private void ProcessAutoAttack(GameClient client, int time)
        {
            // Implement auto attack processing
            // This would contain the logic from the original AutoAttackCallback
        }

        private void ProcessCompanions(GameClient client, int time)
        {
            // Implement companion processing
            // This would contain the logic from the original CompanionsCallback
        }

        private void ProcessPrayer(GameClient client, int time)
        {
            // Implement prayer processing
            // This would contain the logic from the original PrayerCallback
        }

        private void ProcessServerFunctions(int time)
        {
            // Implement server functions processing
            // This would contain the logic from the original ServerFunctions
        }

        private void ProcessWorldTournaments(int time)
        {
            // Implement world tournaments processing
            // This would contain the logic from the original WorldTournaments
        }

        private void ProcessArenaFunctions(int time)
        {
            // Implement arena functions processing
            // This would contain the logic from the original ArenaFunctions
        }

        private void ProcessTeamArenaFunctions(int time)
        {
            // Implement team arena functions processing
            // This would contain the logic from the original TeamArenaFunctions
        }

        #endregion

        #region Performance Monitoring

        private void UpdateClientProcessingStats(long processingTime)
        {
            lock (_statsLock)
            {
                _totalProcessedClients++;
                
                // Log performance every 1000 clients or every 5 minutes
                if (_totalProcessedClients % 1000 == 0 || DateTime.Now > _lastPerformanceLog.AddMinutes(5))
                {
                    LoggingService.SystemDebug("World", 
                        $"Processed {_totalProcessedClients} clients, last processing time: {processingTime}ms");
                    _lastPerformanceLog = DateTime.Now;
                }
            }
        }

        public void LogPerformanceStatistics()
        {
            lock (_statsLock)
            {
                LoggingService.SystemInfo("World", 
                    $"Performance Stats - Total Clients Processed: {_totalProcessedClients}");
            }
        }

        #endregion

        #region Workload Distribution

        public void DistributeClientProcessing(IEnumerable<GameClient> clients)
        {
            _threadingManager.DistributeWorkload(clients, client =>
            {
                try
                {
                    if (Valid(client))
                    {
                        ProcessCharacterLogic(client, Time32.Now.Value);
                    }
                }
                catch (Exception ex)
                {
                    LoggingService.SystemError("World", $"Error processing client {client?.Entity?.Name}", ex);
                }
            }, ThreadPoolType.GameLogic);
        }

        #endregion

        #region Cleanup

        public new void Dispose()
        {
            try
            {
                // Dispose timer subscriptions
                foreach (var subscription in _timerSubscriptions.Values)
                {
                    subscription?.Dispose();
                }
                _timerSubscriptions.Clear();
                
                LoggingService.SystemInfo("World", "Improved World disposed");
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("World", "Error disposing improved world", ex);
            }
        }

        #endregion
    }
} 