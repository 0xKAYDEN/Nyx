using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace Nyx.Server.Network.Sockets
{
    public class ImprovedClientWrapper : ClientWrapper
    {
        private readonly SemaphoreSlim _sendThrottle;
        private readonly ConcurrentQueue<byte[]> _sendQueue;
        private readonly object _sendSyncRoot = new object();
        private readonly Timer _healthCheckTimer;
        private readonly Timer _cleanupTimer;
        
        // Configuration
        private const int MaxSendQueueSize = 1000;
        private const int SendThrottleMs = 0; // Minimum time between sends
        private const int HealthCheckIntervalMs = 30000; // 30 seconds
        private const int CleanupIntervalMs = 60000; // 1 minute
        private const int MaxIdleTimeMs = 300000; // 5 minutes

        public DateTime LastActivity { get; private set; }
        public int SendQueueSize => _sendQueue.Count;
        public bool IsHealthy { get; private set; } = true;
        public int TotalBytesSent { get; private set; }
        public int TotalBytesReceived { get; private set; }

        public ImprovedClientWrapper()
        {
            _sendThrottle = new SemaphoreSlim(1, 1);
            _sendQueue = new ConcurrentQueue<byte[]>();
            LastActivity = DateTime.Now;
            
            // Start health check timer
            _healthCheckTimer = new Timer(HealthCheckCallback, null, HealthCheckIntervalMs, HealthCheckIntervalMs);
            _cleanupTimer = new Timer(CleanupCallback, null, CleanupIntervalMs, CleanupIntervalMs);
        }

        public new void Create(Socket socket, ServerSocket server, Action<byte[], int, ClientWrapper> callBack)
        {
            base.Create(socket, server, callBack);
            LastActivity = DateTime.Now;
            
            // Set socket options for better performance
            try
            {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                socket.ReceiveBufferSize = 8192;
                socket.SendBufferSize = 8192;
                socket.ReceiveTimeout = 30000; // 30 seconds
                socket.SendTimeout = 30000; // 30 seconds
            }
            catch (Exception ex)
            {
                LoggingService.SystemWarning("Network", $"Failed to set socket options for {IP}: {ex.Message}");
            }
        }

        public new void Send(byte[] data)
        {
            if (!Alive || data == null || data.Length == 0)
                return;

            try
            {
                // Check send queue size
                if (_sendQueue.Count >= MaxSendQueueSize)
                {
                    LoggingService.SystemWarning("Network", $"Send queue full for {IP}, dropping packet");
                    return;
                }

                // Add to send queue
                _sendQueue.Enqueue(data);
                LastActivity = DateTime.Now;

                // Trigger async send
                _ = SendQueuedDataAsync();
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error queuing send for {IP}", ex);
            }
        }

        private async Task SendQueuedDataAsync()
        {
            if (!await _sendThrottle.WaitAsync(100)) // Wait up to 100ms
                return;

            try
            {
                while (_sendQueue.TryDequeue(out var data))
                {
                    if (!Alive) break;

                    try
                    {
                        var bytesSent = Socket.Send(data);
                        TotalBytesSent += bytesSent;
                        LastActivity = DateTime.Now;

                        // Log large packets for monitoring
                        if (data.Length > 1024)
                        {
                            LoggingService.SystemDebug("Network", $"Large packet sent to {IP}: {data.Length} bytes");
                        }
                    }
                    catch (SocketException ex)
                    {
                        LoggingService.SystemWarning("Network", $"Socket error sending to {IP}: {ex.Message}");
                        Server.InvokeDisconnect(this);
                        break;
                    }
                    catch (Exception ex)
                    {
                        LoggingService.SystemError("Network", $"Error sending to {IP}", ex);
                        Server.InvokeDisconnect(this);
                        break;
                    }

                    // Removed delay to reduce latency and jitter during consecutive sends
                }
            }
            finally
            {
                _sendThrottle.Release();
            }
        }

        public new void Disconnect()
        {
            if (!Alive) return;

            try
            {
                Alive = false;
                IsHealthy = false;

                // Stop timers
                _healthCheckTimer?.Dispose();
                _cleanupTimer?.Dispose();

                // Clear send queue
                while (_sendQueue.TryDequeue(out _)) { }

                // Call base disconnect
                base.Disconnect();

                LoggingService.ClientDisconnected(IP);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error during disconnect for {IP}", ex);
            }
        }

        private void HealthCheckCallback(object state)
        {
            if (!Alive) return;

            try
            {
                var idleTime = DateTime.Now - LastActivity;
                
                // Check if connection is idle too long
                if (idleTime.TotalMilliseconds > MaxIdleTimeMs)
                {
                    LoggingService.SystemWarning("Network", $"Connection idle too long for {IP}: {idleTime.TotalMinutes:F1} minutes");
                    Server.InvokeDisconnect(this);
                    return;
                }

                // Check socket health
                if (Socket != null && Socket.Connected)
                {
                    try
                    {
                        // Quick poll to check if socket is still responsive
                        if (Socket.Poll(0, SelectMode.SelectRead) && Socket.Available == 0)
                        {
                            // Socket is closed
                            LoggingService.SystemWarning("Network", $"Socket closed for {IP}");
                            Server.InvokeDisconnect(this);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggingService.SystemWarning("Network", $"Socket health check failed for {IP}: {ex.Message}");
                        Server.InvokeDisconnect(this);
                        return;
                    }
                }

                IsHealthy = true;
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error in health check for {IP}", ex);
            }
        }

        private void CleanupCallback(object state)
        {
            if (!Alive) return;

            try
            {
                // Log connection statistics
                if (TotalBytesSent > 0 || TotalBytesReceived > 0)
                {
                    LoggingService.SystemDebug("Network", 
                        $"Connection stats for {IP}: Sent={TotalBytesSent}, Received={TotalBytesReceived}, Queue={SendQueueSize}");
                }

                // Reset counters
                TotalBytesSent = 0;
                TotalBytesReceived = 0;
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error in cleanup for {IP}", ex);
            }
        }

        public new static void TryReceive(ClientWrapper wrapper)
        {
            if (wrapper is ImprovedClientWrapper improvedWrapper)
            {
                improvedWrapper.LastActivity = DateTime.Now;
            }

            try
            {
                ClientWrapper.TryReceive(wrapper);
                
                if (wrapper is ImprovedClientWrapper improved)
                {
                    improved.TotalBytesReceived += wrapper.Buffer?.Length ?? 0;
                }
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error in TryReceive for {wrapper.IP}", ex);
            }
        }

        public ConnectionStats GetConnectionStats()
        {
            return new ConnectionStats
            {
                IP = IP,
                IsAlive = Alive,
                IsHealthy = IsHealthy,
                LastActivity = LastActivity,
                SendQueueSize = SendQueueSize,
                TotalBytesSent = TotalBytesSent,
                TotalBytesReceived = TotalBytesReceived,
                IdleTime = DateTime.Now - LastActivity
            };
        }
    }

    public class ConnectionStats
    {
        public string IP { get; set; }
        public bool IsAlive { get; set; }
        public bool IsHealthy { get; set; }
        public DateTime LastActivity { get; set; }
        public int SendQueueSize { get; set; }
        public int TotalBytesSent { get; set; }
        public int TotalBytesReceived { get; set; }
        public TimeSpan IdleTime { get; set; }
    }
} 