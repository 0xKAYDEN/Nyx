using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Nyx.Server.Network.Sockets
{
    public class ImprovedServerSocket : ServerSocket
    {
        public event Action<ClientWrapper> OnClientConnect, OnClientDisconnect;
        public event Action<byte[], int, ClientWrapper> OnClientReceive;

        private Socket _listener;
        private readonly SemaphoreSlim _connectionThrottle;
        private readonly ConcurrentDictionary<string, DateTime> _recentConnections;
        private readonly object _syncRoot = new object();
        
        private ushort _port;
        private string _ipString;
        private bool _enabled;
        private CancellationTokenSource _cancellationTokenSource;
        
        // Configuration
        private const int MaxConcurrentConnections = 1000;
        private const int ConnectionThrottleMs = 100; // Minimum time between connections from same IP
        private const int MaxConnectionsPerIP = 5; // Maximum connections per IP

        public ImprovedServerSocket()
        {
            _connectionThrottle = new SemaphoreSlim(MaxConcurrentConnections);
            _recentConnections = new ConcurrentDictionary<string, DateTime>();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public new void Enable(ushort port, string ip)
        {
            lock (_syncRoot)
            {
                if (_enabled) return;

                _port = port;
                _ipString = ip;
                _enabled = true;
                _cancellationTokenSource = new CancellationTokenSource();

                try
                {
                    _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    
                    // Set socket options for better performance
                    _listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    _listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    _listener.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    
                    _listener.Bind(new IPEndPoint(IPAddress.Parse(_ipString), _port));
                    _listener.Listen(MaxConcurrentConnections);
                    
                    LoggingService.SystemInfo("Network", $"Improved server socket enabled on {ip}:{port}");
                    
                    // Start accepting connections
                    _ = AcceptConnectionsAsync();
                }
                catch (Exception ex)
                {
                    LoggingService.SystemError("Network", $"Failed to enable server socket: {ex.Message}", ex);
                    throw;
                }
            }
        }

        private async Task AcceptConnectionsAsync()
        {
            while (_enabled && !_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    // Wait for connection throttle
                    await _connectionThrottle.WaitAsync(_cancellationTokenSource.Token);
                    
                    var clientSocket = await _listener.AcceptAsync();
                    
                    // Process connection in background
                    _ = ProcessClientConnectionAsync(clientSocket);
                }
                catch (OperationCanceledException)
                {
                    // Server is shutting down
                    break;
                }
                catch (Exception ex)
                {
                    LoggingService.SystemError("Network", "Error accepting connection", ex);
                    await Task.Delay(1000); // Wait before retrying
                }
            }
        }

        private async Task ProcessClientConnectionAsync(Socket clientSocket)
        {
            string clientIP = "Unknown";
            
            try
            {
                // Get client IP
                if (clientSocket.RemoteEndPoint is IPEndPoint remoteEndPoint)
                {
                    clientIP = remoteEndPoint.Address.ToString();
                }

                // Check connection throttling
                if (!CheckConnectionThrottling(clientIP))
                {
                    LoggingService.SecurityEvent("ConnectionThrottle", $"Connection throttled from {clientIP}");
                    clientSocket.Close();
                    return;
                }

                // Set socket options for better performance
                clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                clientSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                clientSocket.ReceiveBufferSize = 8192;
                clientSocket.SendBufferSize = 8192;

                // Create client wrapper
                var wrapper = new ClientWrapper();
                wrapper.Create(clientSocket, this, OnClientReceive);
                wrapper.Alive = true;
                wrapper.IP = clientIP;

                LoggingService.ClientConnected(clientIP);
                
                // Notify connection event
                OnClientConnect?.Invoke(wrapper);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error processing client connection from {clientIP}", ex);
                try
                {
                    clientSocket?.Close();
                }
                catch { }
            }
            finally
            {
                // Release connection throttle
                _connectionThrottle.Release();
            }
        }

        private bool CheckConnectionThrottling(string clientIP)
        {
            var now = DateTime.Now;
            
            // Check if IP has too many recent connections
            var recentConnections = _recentConnections.Count(kvp => 
                kvp.Key == clientIP && 
                (now - kvp.Value).TotalMilliseconds < ConnectionThrottleMs);

            if (recentConnections >= MaxConnectionsPerIP)
            {
                return false;
            }

            // Add to recent connections
            _recentConnections.TryAdd($"{clientIP}_{now.Ticks}", now);
            
            // Clean up old entries
            CleanupRecentConnections();
            
            return true;
        }

        private void CleanupRecentConnections()
        {
            var cutoff = DateTime.Now.AddMilliseconds(-ConnectionThrottleMs);
            var keysToRemove = _recentConnections
                .Where(kvp => kvp.Value < cutoff)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                _recentConnections.TryRemove(key, out _);
            }
        }

        public new void Disable()
        {
            lock (_syncRoot)
            {
                if (!_enabled) return;

                _enabled = false;
                _cancellationTokenSource?.Cancel();

                try
                {
                    _listener?.Close();
                    _listener?.Dispose();
                }
                catch (Exception ex)
                {
                    LoggingService.SystemError("Network", "Error disabling server socket", ex);
                }

                LoggingService.SystemInfo("Network", "Improved server socket disabled");
            }
        }

        public new void InvokeDisconnect(ClientWrapper client)
        {
            try
            {
                OnClientDisconnect?.Invoke(client);
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Network", $"Error invoking disconnect for {client.IP}", ex);
            }
        }

        public new bool Enabled => _enabled;

        public int ActiveConnections => MaxConcurrentConnections - _connectionThrottle.CurrentCount;

        public void Reset()
        {
            Disable();
            Enable(_port, _ipString);
        }
    }
} 