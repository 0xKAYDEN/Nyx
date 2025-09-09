using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;
using Nyx.Server.Network.Sockets;

namespace Nyx.Server.Network
{
    public class NetworkManager
    {
        private static NetworkManager _instance;
        private static readonly object _lock = new object();
        
        private readonly ConcurrentDictionary<string, ClientWrapper> _activeConnections;
        private readonly ConcurrentDictionary<string, ConnectionStats> _connectionStats;
        private readonly Timer _statsTimer;
        private readonly Timer _cleanupTimer;
        
        // Configuration
        private const int StatsIntervalMs = 60000; // 1 minute
        private const int CleanupIntervalMs = 300000; // 5 minutes
        private const int MaxConnectionsPerIP = 10;
        private const int MaxTotalConnections = 5000;

        public int TotalConnections => _activeConnections.Count;
        public int HealthyConnections => _activeConnections.Values.Count(c => c.Alive);
        public Dictionary<string, int> ConnectionsPerIP => _activeConnections.Values
            .GroupBy(c => c.IP)
            .ToDictionary(g => g.Key, g => g.Count());

        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new NetworkManager();
                    }
                }
                return _instance;
            }
        }

        private NetworkManager()
        {
            _activeConnections = new ConcurrentDictionary<string, ClientWrapper>();
            _connectionStats = new ConcurrentDictionary<string, ConnectionStats>();
            
            _statsTimer = new Timer(LogStats, null, StatsIntervalMs, StatsIntervalMs);
            _cleanupTimer = new Timer(CleanupDeadConnections, null, CleanupIntervalMs, CleanupIntervalMs);
        }

        public bool CanAcceptConnection(string clientIP)
        {
            // Check total connection limit
            if (TotalConnections >= MaxTotalConnections)
            {
                LoggingService.SystemWarning("Network", $"Total connection limit reached: {TotalConnections}");
                return false;
            }

            // Check per-IP connection limit
            var connectionsFromIP = _activeConnections.Values.Count(c => c.IP == clientIP);
            if (connectionsFromIP >= MaxConnectionsPerIP)
            {
                LoggingService.SecurityEvent("ConnectionLimit", $"Too many connections from {clientIP}: {connectionsFromIP}");
                return false;
            }

            return true;
        }

        public void RegisterConnection(ClientWrapper client)
        {
            if (client == null || string.IsNullOrEmpty(client.IP))
                return;

            var connectionId = $"{client.IP}_{DateTime.Now.Ticks}";
            _activeConnections.TryAdd(connectionId, client);
            
            LoggingService.SystemDebug("Network", $"Connection registered: {client.IP} (Total: {TotalConnections})");
        }

        public void UnregisterConnection(ClientWrapper client)
        {
            if (client == null || string.IsNullOrEmpty(client.IP))
                return;

            var connectionId = _activeConnections.FirstOrDefault(kvp => kvp.Value == client).Key;
            if (!string.IsNullOrEmpty(connectionId))
            {
                _activeConnections.TryRemove(connectionId, out _);
                
                // Store connection stats
                if (client is ImprovedClientWrapper improvedClient)
                {
                    _connectionStats.TryAdd(connectionId, improvedClient.GetConnectionStats());
                }
                
                LoggingService.SystemDebug("Network", $"Connection unregistered: {client.IP} (Total: {TotalConnections})");
            }
        }

        public void DisconnectAllConnections()
        {
            LoggingService.SystemInfo("Network", $"Disconnecting all {TotalConnections} connections");
            
            var connections = _activeConnections.Values.ToList();
            foreach (var connection in connections)
            {
                try
                {
                    connection.Disconnect();
                }
                catch (Exception ex)
                {
                    LoggingService.SystemError("Network", $"Error disconnecting {connection.IP}", ex);
                }
            }
            
            _activeConnections.Clear();
        }

        public void DisconnectConnectionsFromIP(string ipAddress)
        {
            var connectionsToDisconnect = _activeConnections.Values
                .Where(c => c.IP == ipAddress)
                .ToList();

            LoggingService.SecurityEvent("Network", $"Disconnecting {connectionsToDisconnect.Count} connections from {ipAddress}");
            
            foreach (var connection in connectionsToDisconnect)
            {
                try
                {
                    connection.Disconnect();
                }
                catch (Exception ex)
                {
                    LoggingService.SystemError("Network", $"Error disconnecting {connection.IP}", ex);
                }
            }
        }

        public List<ConnectionStats> GetConnectionStats()
        {
            return _connectionStats.Values.ToList();
        }

        public ConnectionStats GetConnectionStats(string ipAddress)
        {
            return _connectionStats.Values
                .Where(s => s.IP == ipAddress)
                .OrderByDescending(s => s.LastActivity)
                .FirstOrDefault();
        }

        private void LogStats(object state)
        {
            try
            {
                var totalConnections = TotalConnections;
                var healthyConnections = HealthyConnections;
                var connectionsPerIP = ConnectionsPerIP;
                
                var topIPs = connectionsPerIP
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(5)
                    .ToList();

                LoggingService.SystemInfo("Network", 
                    $"Network Stats - Total: {totalConnections}, Healthy: {healthyConnections}, " +
                    $"Top IPs: {string.Join(", ", topIPs.Select(kvp => $"{kvp.Key}({kvp.Value})"))}");

                // Log warnings for high connection counts
                foreach (var kvp in connectionsPerIP.Where(kvp => kvp.Value > 5))
                {
                    LoggingService.SystemWarning("Network", $"High connection count from {kvp.Key}: {kvp.Value}");
                }
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", "Error logging network stats", ex);
            }
        }

        private void CleanupDeadConnections(object state)
        {
            try
            {
                var deadConnections = _activeConnections.Values
                    .Where(c => !c.Alive)
                    .ToList();

                foreach (var connection in deadConnections)
                {
                    UnregisterConnection(connection);
                }

                if (deadConnections.Count > 0)
                {
                    LoggingService.SystemDebug("Network", $"Cleaned up {deadConnections.Count} dead connections");
                }

                // Clean up old stats
                var cutoff = DateTime.Now.AddHours(-1);
                var oldStats = _connectionStats
                    .Where(kvp => kvp.Value.LastActivity < cutoff)
                    .Select(kvp => kvp.Key)
                    .ToList();

                foreach (var key in oldStats)
                {
                    _connectionStats.TryRemove(key, out _);
                }
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", "Error cleaning up dead connections", ex);
            }
        }

        public void Shutdown()
        {
            try
            {
                _statsTimer?.Dispose();
                _cleanupTimer?.Dispose();
                
                DisconnectAllConnections();
                
                LoggingService.SystemInfo("Network", "Network manager shutdown complete");
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", "Error during network manager shutdown", ex);
            }
        }

        public NetworkHealthReport GetHealthReport()
        {
            return new NetworkHealthReport
            {
                TotalConnections = TotalConnections,
                HealthyConnections = HealthyConnections,
                UnhealthyConnections = TotalConnections - HealthyConnections,
                ConnectionsPerIP = ConnectionsPerIP,
                TopConnectionsByIP = ConnectionsPerIP
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(10)
                    .ToList(),
                RecentConnectionStats = _connectionStats.Values
                    .Where(s => s.LastActivity > DateTime.Now.AddMinutes(-5))
                    .ToList()
            };
        }
    }

    public class NetworkHealthReport
    {
        public int TotalConnections { get; set; }
        public int HealthyConnections { get; set; }
        public int UnhealthyConnections { get; set; }
        public Dictionary<string, int> ConnectionsPerIP { get; set; }
        public List<KeyValuePair<string, int>> TopConnectionsByIP { get; set; }
        public List<ConnectionStats> RecentConnectionStats { get; set; }
    }
} 