using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;

namespace Nyx.Server.Network
{
    public class PacketMetrics
    {
        private readonly ConcurrentQueue<TimeSpan> _processingTimes = new();
        private readonly object _lock = new object();
        private const int MaxSamples = 100;

        public void RecordProcessingTime(TimeSpan processingTime)
        {
            _processingTimes.Enqueue(processingTime);
            
            // Keep only the last MaxSamples
            while (_processingTimes.Count > MaxSamples)
            {
                _processingTimes.TryDequeue(out _);
            }
        }

        public TimeSpan AverageProcessingTime
        {
            get
            {
                var times = _processingTimes.ToArray();
                if (times.Length == 0) return TimeSpan.Zero;
                
                var totalTicks = times.Sum(t => t.Ticks);
                return TimeSpan.FromTicks(totalTicks / times.Length);
            }
        }

        public long TotalPackets => _processingTimes.Count;
        public TimeSpan MaxProcessingTime => _processingTimes.Count > 0 ? _processingTimes.Max() : TimeSpan.Zero;
        public TimeSpan MinProcessingTime => _processingTimes.Count > 0 ? _processingTimes.Min() : TimeSpan.Zero;
    }

    public static class PacketPerformanceTracker
    {
        private static readonly ConcurrentDictionary<ushort, PacketMetrics> _metrics = new();
        private static readonly Stopwatch _stopwatch = new Stopwatch();

        public static void TrackPacketProcessing(ushort packetId, Action processingAction)
        {
            _stopwatch.Restart();
            
            try
            {
                processingAction();
            }
            finally
            {
                _stopwatch.Stop();
                var metrics = _metrics.GetOrAdd(packetId, _ => new PacketMetrics());
                metrics.RecordProcessingTime(_stopwatch.Elapsed);
            }
        }

        public static void LogSlowPackets()
        {
            foreach (var kvp in _metrics)
            {
                var packetId = kvp.Key;
                var metrics = kvp.Value;
                
                if (metrics.AverageProcessingTime > TimeSpan.FromMilliseconds(50))
                {
                    LoggingService.SystemWarning("PacketPerformance", 
                        $"Slow packet {packetId}: {metrics.AverageProcessingTime.TotalMilliseconds:F2}ms avg, " +
                        $"max: {metrics.MaxProcessingTime.TotalMilliseconds:F2}ms, " +
                        $"total: {metrics.TotalPackets}");
                }
            }
        }

        public static void LogPacketStatistics()
        {
            var totalPackets = _metrics.Values.Sum(m => m.TotalPackets);
            var avgProcessingTime = _metrics.Values.Any() 
                ? TimeSpan.FromTicks((long)_metrics.Values.Average(m => m.AverageProcessingTime.Ticks))
                : TimeSpan.Zero;

            LoggingService.SystemInfo("PacketStats", 
                $"Total packets processed: {totalPackets}, " +
                $"Average processing time: {avgProcessingTime.TotalMilliseconds:F2}ms, " +
                $"Unique packet types: {_metrics.Count}");
        }

        public static PacketMetrics GetPacketMetrics(ushort packetId)
        {
            return _metrics.GetOrAdd(packetId, _ => new PacketMetrics());
        }

        public static void ClearMetrics()
        {
            _metrics.Clear();
        }
    }
} 