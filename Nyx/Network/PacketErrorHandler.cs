using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Nyx.Server.Client;

namespace Nyx.Server.Network
{
    public class PacketErrorInfo
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime FirstOccurrence { get; set; }
        public DateTime LastOccurrence { get; set; }
        public int OccurrenceCount { get; set; }
        public string PlayerName { get; set; }
        public string IPAddress { get; set; }
    }

    public static class PacketErrorHandler
    {
        private static readonly ConcurrentDictionary<string, PacketErrorInfo> _errorHistory = new();
        private static readonly object _lock = new object();

        public static void HandlePacketError(Exception ex, GameClient client, byte[] packet, ushort packetId)
        {
            var errorKey = $"{ex.GetType().Name}_{packetId}";
            var errorInfo = _errorHistory.GetOrAdd(errorKey, _ => new PacketErrorInfo
            {
                ErrorType = ex.GetType().Name,
                ErrorMessage = ex.Message,
                FirstOccurrence = DateTime.Now,
                PlayerName = client?.Entity?.Name ?? "Unknown",
                IPAddress = client?.IP ?? "Unknown"
            });

            lock (_lock)
            {
                errorInfo.LastOccurrence = DateTime.Now;
                errorInfo.OccurrenceCount++;
                errorInfo.PlayerName = client?.Entity?.Name ?? "Unknown";
                errorInfo.IPAddress = client?.IP ?? "Unknown";
            }

            // Log the error with appropriate level based on frequency
            if (errorInfo.OccurrenceCount <= 3)
            {
                LoggingService.SystemError("PacketHandler", 
                    $"Error processing packet {packetId} from {client?.Entity?.Name ?? "Unknown"}: {ex.Message}", ex);
            }
            else if (errorInfo.OccurrenceCount <= 10)
            {
                LoggingService.SystemWarning("PacketHandler", 
                    $"Repeated error processing packet {packetId} from {client?.Entity?.Name ?? "Unknown"}: {ex.Message} (Count: {errorInfo.OccurrenceCount})");
            }
            else
            {
                // Don't spam logs for repeated errors
                if (errorInfo.OccurrenceCount % 50 == 0)
                {
                    LoggingService.SystemWarning("PacketHandler", 
                        $"Frequent error processing packet {packetId}: {ex.Message} (Total occurrences: {errorInfo.OccurrenceCount})");
                }
            }

            // Don't disconnect client for most errors - just log and continue
            // Only disconnect for critical errors that could cause security issues
            if (IsCriticalError(ex, packetId))
            {
                LoggingService.SecurityEvent("CriticalPacketError", 
                    $"Critical packet error from {client?.Entity?.Name ?? "Unknown"} ({client?.IP ?? "Unknown"}): {ex.Message}");
                
                // Only disconnect for security-critical errors
                if (IsSecurityCriticalError(ex, packetId))
                {
                    client?.Disconnect();
                }
            }
        }

        private static bool IsCriticalError(Exception ex, ushort packetId)
        {
            // Define what constitutes a critical error
            return ex is OutOfMemoryException || 
                   ex is StackOverflowException ||
                   ex is AccessViolationException ||
                   IsSecurityCriticalError(ex, packetId);
        }

        private static bool IsSecurityCriticalError(Exception ex, ushort packetId)
        {
            // Define security-critical errors that should result in disconnection
            return ex is ArgumentOutOfRangeException ||
                   ex is IndexOutOfRangeException ||
                   ex.Message.Contains("buffer") && ex.Message.Contains("overflow") ||
                   packetId == 0 || packetId > 65535; // Invalid packet ID
        }

        public static void LogErrorStatistics()
        {
            var totalErrors = _errorHistory.Values.Sum(e => e.OccurrenceCount);
            var uniqueErrors = _errorHistory.Count;

            LoggingService.SystemInfo("ErrorStats", 
                $"Total packet errors: {totalErrors}, Unique error types: {uniqueErrors}");

            // Log top 5 most frequent errors
            var topErrors = _errorHistory.Values
                .OrderByDescending(e => e.OccurrenceCount)
                .Take(5);

            foreach (var error in topErrors)
            {
                LoggingService.SystemWarning("TopErrors", 
                    $"Error: {error.ErrorType} - {error.ErrorMessage} (Count: {error.OccurrenceCount}, " +
                    $"Last: {error.LastOccurrence:HH:mm:ss})");
            }
        }

        public static void ClearErrorHistory()
        {
            _errorHistory.Clear();
        }

        public static PacketErrorInfo GetErrorInfo(string errorKey)
        {
            return _errorHistory.TryGetValue(errorKey, out var info) ? info : null;
        }

        public static IEnumerable<PacketErrorInfo> GetAllErrors()
        {
            return _errorHistory.Values;
        }

        public static void ResetErrorCount(string errorKey)
        {
            if (_errorHistory.TryGetValue(errorKey, out var errorInfo))
            {
                lock (_lock)
                {
                    errorInfo.OccurrenceCount = 0;
                    errorInfo.FirstOccurrence = DateTime.Now;
                }
            }
        }
    }
} 