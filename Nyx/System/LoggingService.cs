using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Buffers;
using System.Threading;
using Nyx.Server.Client;
using Nyx.Server.Network;

namespace Nyx.Server
{
    public static class LoggingService
    {
        private static ILogger _logger;

        public static void Initialize()
        {
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build())
                .CreateLogger();

            Log.Logger = _logger;
        }

        public static void Shutdown()
        {
            Log.CloseAndFlush();
        }

        #region Server Logging
        public static void ServerStart(string serverName, string version)
        {
            Log.Information("Starting {ServerName} v{Version}", serverName, version);
        }

        public static void ServerStarted(string serverName, ushort port)
        {
            Log.Information("{ServerName} started successfully on port {Port}", serverName, port);
        }

        public static void ServerShutdown(string serverName)
        {
            Log.Information("Shutting down {ServerName}", serverName);
        }

        public static void ServerError(string serverName, Exception ex)
        {
            Log.Error(ex, "Critical error in {ServerName}", serverName);
        }
        #endregion

        #region Database Logging
        public static void DatabaseConnected(string databaseName)
        {
            Log.Information("Connected to database: {DatabaseName}", databaseName);
        }

        public static void DatabaseConnectionFailed(string databaseName, Exception ex)
        {
            Log.Error(ex, "Failed to connect to database: {DatabaseName}", databaseName);
        }

        public static void DatabaseQueryExecuted(string query, TimeSpan duration)
        {
            Log.Debug("Database query executed in {Duration}ms: {Query}", duration.TotalMilliseconds, query);
        }

        public static void DatabaseError(string operation, Exception ex)
        {
            Log.Error(ex, "Database error during {Operation}", operation);
        }
        #endregion

        #region Client Logging
        public static void ClientConnected(string ipAddress, string username = null)
        {
            if (username != null)
                Log.Information("Client connected from {IPAddress} - User: {Username}", ipAddress, username);
            else
                Log.Information("Client connected from {IPAddress}", ipAddress);
        }

        public static void ClientDisconnected(string ipAddress, string username = null)
        {
            if (username != null)
                Log.Information("Client disconnected from {IPAddress} - User: {Username}", ipAddress, username);
            else
                Log.Information("Client disconnected from {IPAddress}", ipAddress);
        }

        public static void ClientLoginSuccess(string username, string ipAddress)
        {
            Log.Information("User {Username} logged in successfully from {IPAddress}", username, ipAddress);
        }

        public static void ClientLoginFailed(string username, string ipAddress, string reason)
        {
            Log.Warning("Login failed for user {Username} from {IPAddress}: {Reason}", username, ipAddress, reason);
        }

        public static void ClientBruteForceDetected(string ipAddress, int attempts)
        {
            Log.Warning("Brute force attack detected from {IPAddress} - {Attempts} attempts", ipAddress, attempts);
        }

        public static void ClientPacketReceived(string username, ushort packetId, int size)
        {
            Log.Debug("Packet received from {Username}: ID={PacketId}, Size={Size} bytes", username, packetId, size);
        }

        public static void ClientPacketSent(string username, ushort packetId, int size)
        {
            Log.Debug("Packet sent to {Username}: ID={PacketId}, Size={Size} bytes", username, packetId, size);
        }
        #endregion

        #region Game Logging
        public static void PlayerAction(string playerName, string action, string details = null)
        {
            if (details != null)
                Log.Information("Player {PlayerName} performed {Action}: {Details}", playerName, action, details);
            else
                Log.Information("Player {PlayerName} performed {Action}", playerName, action);
        }

        public static void PlayerLevelUp(string playerName, int newLevel)
        {
            Log.Information("Player {PlayerName} reached level {Level}", playerName, newLevel);
        }

        public static void PlayerDeath(string playerName, string killerName = null)
        {
            if (killerName != null)
                Log.Information("Player {PlayerName} was killed by {KillerName}", playerName, killerName);
            else
                Log.Information("Player {PlayerName} died", playerName);
        }

        public static void ItemTransaction(string playerName, string itemName, string transactionType, int quantity = 1)
        {
            Log.Information("Item transaction: {PlayerName} {TransactionType} {Quantity}x {ItemName}", 
                playerName, transactionType, quantity, itemName);
        }

        public static void GuildAction(string guildName, string action, string details = null)
        {
            if (details != null)
                Log.Information("Guild {GuildName} {Action}: {Details}", guildName, action, details);
            else
                Log.Information("Guild {GuildName} {Action}", guildName, action);
        }

        public static void TournamentEvent(string tournamentName, string eventType, string details = null)
        {
            if (details != null)
                Log.Information("Tournament {TournamentName} {EventType}: {Details}", tournamentName, eventType, details);
            else
                Log.Information("Tournament {TournamentName} {EventType}", tournamentName, eventType);
        }
        #endregion

        #region System Logging
        public static void SystemInfo(string component, string message)
        {
            Log.Information("[{Component}] {Message}", component, message);
        }

        public static void SystemWarning(string component, string message)
        {
            Log.Warning("[{Component}] {Message}", component, message);
        }

        public static void SystemError(string component, string message, Exception ex = null)
        {
            if (ex != null)
                Log.Error(ex, "[{Component}] {Message}", component, message);
            else
                Log.Error("[{Component}] {Message}", component, message);
        }

        public static void SystemDebug(string component, string message)
        {
            Log.Debug("[{Component}] {Message}", component, message);
        }

        public static void PerformanceMetric(string operation, TimeSpan duration)
        {
            Log.Debug("Performance: {Operation} completed in {Duration}ms", operation, duration.TotalMilliseconds);
        }
        #endregion

        #region Security Logging
        public static void SecurityEvent(string eventType, string details, string ipAddress = null)
        {
            if (ipAddress != null)
                Log.Warning("Security event: {EventType} - {Details} from {IPAddress}", eventType, details, ipAddress);
            else
                Log.Warning("Security event: {EventType} - {Details}", eventType, details);
        }

        public static void IPBanned(string ipAddress, string reason)
        {
            Log.Warning("IP {IPAddress} banned: {Reason}", ipAddress, reason);
        }

        public static void SuspiciousActivity(string playerName, string activity, string ipAddress)
        {
            Log.Warning("Suspicious activity detected: Player {PlayerName} from {IPAddress} - {Activity}", 
                playerName, ipAddress, activity);
        }
        #endregion

        #region Utility Methods
        public static IDisposable CreateLogContext(string propertyName, object value)
        {
            return LogContext.PushProperty(propertyName, value);
        }

        public static IDisposable CreateLogContext(Dictionary<string, object> properties)
        {
            return LogContext.PushProperty("Context", properties);
        }
        #endregion

        // Add to existing PacketHandler without changing packet processing
        public static class PacketPerformanceTracker
        {
            private static readonly ConcurrentDictionary<ushort, PacketMetrics> _metrics = new();
            
            public static void TrackPacket(ushort packetId, TimeSpan processingTime)
            {
                var metrics = _metrics.GetOrAdd(packetId, _ => new PacketMetrics());
                metrics.RecordProcessingTime(processingTime);
            }
            
            public static void LogSlowPackets()
            {
                foreach (var kvp in _metrics)
                {
                    if (kvp.Value.AverageProcessingTime > TimeSpan.FromMilliseconds(100))
                    {
                        LoggingService.SystemWarning("PacketPerformance", 
                            $"Slow packet {kvp.Key}: {kvp.Value.AverageProcessingTime}ms avg");
                    }
                }
            }
        }

        // Add to existing packet processing without changing protocol
        public static class PacketErrorHandler
        {
            public static void HandlePacketError(Exception ex, GameClient client, byte[] packet)
            {
                LoggingService.SystemError("PacketHandler", 
                    $"Error processing packet from {client.Entity?.Name}: {ex.Message}", ex);
                
                // Don't disconnect client, just log and continue
                // This prevents server crashes from malformed packets
            }
        }
    }
} 