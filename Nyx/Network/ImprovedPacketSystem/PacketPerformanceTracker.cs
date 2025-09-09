using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Nyx.Server.Network.ImprovedPacketSystem
{
    /// <summary>
    /// Tracks packet processing performance and statistics
    /// </summary>
    public static class PacketPerformanceTracker
    {
        private static readonly ConcurrentDictionary<ushort, PacketStats> _packetStats;
        private static readonly ConcurrentDictionary<ushort, Queue<long>> _processingTimes;
        private static readonly System.Threading.Timer _cleanupTimer;
        private static readonly object _lockObject = new object();

        static PacketPerformanceTracker()
        {
            _packetStats = new ConcurrentDictionary<ushort, PacketStats>();
            _processingTimes = new ConcurrentDictionary<ushort, Queue<long>>();
            
            // Cleanup old data every 5 minutes
            _cleanupTimer = new System.Threading.Timer(CleanupOldData, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
        }

        /// <summary>
        /// Track packet processing time
        /// </summary>
        public static void TrackPacketProcessing(ushort packetId, Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                action();
            }
            finally
            {
                stopwatch.Stop();
                RecordProcessingTime(packetId, stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Track packet processing time with custom timing
        /// </summary>
        public static void TrackPacketProcessing(ushort packetId, long processingTimeMs)
        {
            RecordProcessingTime(packetId, processingTimeMs);
        }

        /// <summary>
        /// Record processing time for a packet
        /// </summary>
        private static void RecordProcessingTime(ushort packetId, long processingTimeMs)
        {
            var stats = _packetStats.GetOrAdd(packetId, _ => new PacketStats { PacketId = packetId });
            
            lock (stats)
            {
                stats.TotalProcessed++;
                stats.TotalProcessingTime += processingTimeMs;
                stats.AverageProcessingTime = stats.TotalProcessingTime / stats.TotalProcessed;
                
                if (processingTimeMs > stats.MaxProcessingTime)
                    stats.MaxProcessingTime = processingTimeMs;
                
                if (processingTimeMs < stats.MinProcessingTime || stats.MinProcessingTime == 0)
                    stats.MinProcessingTime = processingTimeMs;
                
                stats.LastProcessed = DateTime.UtcNow;
            }

            // Store recent processing times for percentile calculations
            var queue = _processingTimes.GetOrAdd(packetId, _ => new Queue<long>());
            lock (queue)
            {
                queue.Enqueue(processingTimeMs);
                
                // Keep only last 1000 processing times
                while (queue.Count > 1000)
                {
                    queue.Dequeue();
                }
            }
        }

        /// <summary>
        /// Get statistics for a specific packet
        /// </summary>
        public static PacketStats GetPacketStats(ushort packetId)
        {
            return _packetStats.TryGetValue(packetId, out var stats) ? stats.Clone() : new PacketStats { PacketId = packetId };
        }

        /// <summary>
        /// Get statistics for all packets
        /// </summary>
        public static List<PacketStats> GetAllPacketStats()
        {
            return _packetStats.Values.Select(s => s.Clone()).ToList();
        }

        /// <summary>
        /// Get top performing packets (fastest average)
        /// </summary>
        public static List<PacketStats> GetTopPerformingPackets(int count = 10)
        {
            return _packetStats.Values
                .Where(s => s.TotalProcessed > 0)
                .OrderBy(s => s.AverageProcessingTime)
                .Take(count)
                .Select(s => s.Clone())
                .ToList();
        }

        /// <summary>
        /// Get slowest packets
        /// </summary>
        public static List<PacketStats> GetSlowestPackets(int count = 10)
        {
            return _packetStats.Values
                .Where(s => s.TotalProcessed > 0)
                .OrderByDescending(s => s.AverageProcessingTime)
                .Take(count)
                .Select(s => s.Clone())
                .ToList();
        }

        /// <summary>
        /// Get most processed packets
        /// </summary>
        public static List<PacketStats> GetMostProcessedPackets(int count = 10)
        {
            return _packetStats.Values
                .Where(s => s.TotalProcessed > 0)
                .OrderByDescending(s => s.TotalProcessed)
                .Take(count)
                .Select(s => s.Clone())
                .ToList();
        }

        /// <summary>
        /// Get percentile processing time for a packet
        /// </summary>
        public static long GetPercentileProcessingTime(ushort packetId, int percentile)
        {
            if (!_processingTimes.TryGetValue(packetId, out var queue))
                return 0;

            lock (queue)
            {
                if (queue.Count == 0)
                    return 0;

                var times = queue.ToArray();
                Array.Sort(times);
                
                var index = (int)Math.Ceiling((percentile / 100.0) * times.Length) - 1;
                return times[Math.Max(0, index)];
            }
        }

        /// <summary>
        /// Get overall performance summary
        /// </summary>
        public static PerformanceSummary GetPerformanceSummary()
        {
            var allStats = _packetStats.Values.Where(s => s.TotalProcessed > 0).ToList();
            
            if (!allStats.Any())
                return new PerformanceSummary();

            var totalProcessed = allStats.Sum(s => s.TotalProcessed);
            var totalTime = allStats.Sum(s => s.TotalProcessingTime);
            var avgTime = totalTime / totalProcessed;
            var maxTime = allStats.Max(s => s.MaxProcessingTime);
            var minTime = allStats.Min(s => s.MinProcessingTime);

            return new PerformanceSummary
            {
                TotalPacketsProcessed = totalProcessed,
                AverageProcessingTime = avgTime,
                MaxProcessingTime = maxTime,
                MinProcessingTime = minTime,
                TotalProcessingTime = totalTime,
                UniquePacketTypes = allStats.Count,
                LastUpdated = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Reset statistics for a specific packet
        /// </summary>
        public static void ResetPacketStats(ushort packetId)
        {
            _packetStats.TryRemove(packetId, out _);
            _processingTimes.TryRemove(packetId, out _);
        }

        /// <summary>
        /// Reset all statistics
        /// </summary>
        public static void ResetAllStats()
        {
            _packetStats.Clear();
            _processingTimes.Clear();
        }

        /// <summary>
        /// Cleanup old data
        /// </summary>
        private static void CleanupOldData(object state)
        {
            var cutoffTime = DateTime.UtcNow.AddHours(-1); // Keep last hour of data
            
            var packetsToRemove = _packetStats.Values
                .Where(s => s.LastProcessed < cutoffTime)
                .Select(s => s.PacketId)
                .ToList();

            foreach (var packetId in packetsToRemove)
            {
                ResetPacketStats(packetId);
            }
        }

        /// <summary>
        /// Dispose the tracker
        /// </summary>
        public static void Dispose()
        {
            _cleanupTimer?.Dispose();
        }
    }

    /// <summary>
    /// Statistics for a specific packet type
    /// </summary>
    public class PacketStats
    {
        public ushort PacketId { get; set; }
        public long TotalProcessed { get; set; }
        public long TotalProcessingTime { get; set; }
        public double AverageProcessingTime { get; set; }
        public long MaxProcessingTime { get; set; }
        public long MinProcessingTime { get; set; }
        public DateTime LastProcessed { get; set; }

        public PacketStats Clone()
        {
            return new PacketStats
            {
                PacketId = PacketId,
                TotalProcessed = TotalProcessed,
                TotalProcessingTime = TotalProcessingTime,
                AverageProcessingTime = AverageProcessingTime,
                MaxProcessingTime = MaxProcessingTime,
                MinProcessingTime = MinProcessingTime,
                LastProcessed = LastProcessed
            };
        }

        public override string ToString()
        {
            return $"Packet {PacketId}: {TotalProcessed} processed, Avg: {AverageProcessingTime:F2}ms, Max: {MaxProcessingTime}ms";
        }
    }

    /// <summary>
    /// Overall performance summary
    /// </summary>
    public class PerformanceSummary
    {
        public long TotalPacketsProcessed { get; set; }
        public double AverageProcessingTime { get; set; }
        public long MaxProcessingTime { get; set; }
        public long MinProcessingTime { get; set; }
        public long TotalProcessingTime { get; set; }
        public int UniquePacketTypes { get; set; }
        public DateTime LastUpdated { get; set; }

        public override string ToString()
        {
            return $"Performance: {TotalPacketsProcessed} packets, {UniquePacketTypes} types, Avg: {AverageProcessingTime:F2}ms";
        }
    }
} 