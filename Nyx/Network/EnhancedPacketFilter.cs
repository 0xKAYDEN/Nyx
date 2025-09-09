using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nyx.Server.Network
{
    public class RateLimitInfo
    {
        public DateTime LastReset { get; set; } = DateTime.Now;
        public int PacketCount { get; set; } = 0;
        public readonly object Lock = new object();
        
        private const int MaxPacketsPerMinute = 1000;
        private const int ResetIntervalMinutes = 1;

        public bool IsRateLimited()
        {
            lock (Lock)
            {
                var now = DateTime.Now;
                
                // Reset counter if interval has passed
                if (now > LastReset.AddMinutes(ResetIntervalMinutes))
                {
                    PacketCount = 0;
                    LastReset = now;
                }
                
                PacketCount++;
                return PacketCount > MaxPacketsPerMinute;
            }
        }

        public void Reset()
        {
            lock (Lock)
            {
                PacketCount = 0;
                LastReset = DateTime.Now;
            }
        }
    }

    public class EnhancedPacketFilter
    {
        private readonly PacketFilter _baseFilter;
        private readonly ConcurrentDictionary<string, RateLimitInfo> _ipLimits = new();
        private readonly ConcurrentDictionary<string, DateTime> _bannedIPs = new();
        
        private const int BanDurationMinutes = 15;
        private const int MaxFailedAttempts = 10;

        public EnhancedPacketFilter()
        {
            _baseFilter = new PacketFilter();
        }

        public void AddPacketLimit(int packetId, int limit)
        {
            _baseFilter.Add(packetId, limit);
        }

        public bool Filter(int packetId, string ipAddress)
        {
            // Check if IP is banned
            if (_bannedIPs.TryGetValue(ipAddress, out var banTime))
            {
                if (DateTime.Now < banTime.AddMinutes(BanDurationMinutes))
                {
                    LoggingService.SecurityEvent("RateLimit", $"Banned IP {ipAddress} attempted to send packet {packetId}");
                    return true; // Block the packet
                }
                else
                {
                    // Ban expired, remove from banned list
                    _bannedIPs.TryRemove(ipAddress, out _);
                }
            }

            // Check base packet filter (existing logic)
            if (_baseFilter.Filter(packetId))
            {
                LoggingService.SecurityEvent("PacketFilter", $"Packet {packetId} from {ipAddress} blocked by base filter");
                return true;
            }

            // Check IP-based rate limiting
            var rateLimit = _ipLimits.GetOrAdd(ipAddress, _ => new RateLimitInfo());
            if (rateLimit.IsRateLimited())
            {
                LoggingService.SecurityEvent("RateLimit", $"IP {ipAddress} rate limited for packet {packetId}");
                
                // Increment failed attempts
                var failedAttempts = GetFailedAttempts(ipAddress);
                if (failedAttempts >= MaxFailedAttempts)
                {
                    _bannedIPs.TryAdd(ipAddress, DateTime.Now);
                    LoggingService.IPBanned(ipAddress, "Exceeded rate limit multiple times");
                }
                
                return true; // Block the packet
            }

            return false; // Allow the packet
        }

        private int GetFailedAttempts(string ipAddress)
        {
            // Simple implementation - you might want to store this in a more persistent way
            return 0; // Placeholder
        }

        public void ResetIPLimit(string ipAddress)
        {
            if (_ipLimits.TryGetValue(ipAddress, out var rateLimit))
            {
                rateLimit.Reset();
            }
        }

        public void UnbanIP(string ipAddress)
        {
            _bannedIPs.TryRemove(ipAddress, out _);
        }

        public IEnumerable<string> GetBannedIPs()
        {
            return _bannedIPs.Keys;
        }

        public RateLimitInfo GetIPRateLimit(string ipAddress)
        {
            return _ipLimits.GetOrAdd(ipAddress, _ => new RateLimitInfo());
        }

        public void ClearAllLimits()
        {
            _ipLimits.Clear();
            _bannedIPs.Clear();
        }
    }
} 