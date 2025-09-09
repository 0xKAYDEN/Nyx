using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Network.ImprovedPacketSystem
{
    /// <summary>
    /// Improved packet handler with better organization and performance
    /// </summary>
    public static class ImprovedPacketHandler
    {
        private static readonly ConcurrentDictionary<ushort, PacketHandlerDelegate> _handlers;
        private static readonly ConcurrentDictionary<ushort, PacketInfo> _packetInfo;
        private static readonly ulong ClientSeal = BitConverter.ToUInt64(System.Text.Encoding.Default.GetBytes("TQClient"), 0);

        static ImprovedPacketHandler()
        {
            _handlers = new ConcurrentDictionary<ushort, PacketHandlerDelegate>();
            _packetInfo = new ConcurrentDictionary<ushort, PacketInfo>();
            
            // Register all packet handlers
            RegisterPacketHandlers();
        }

        /// <summary>
        /// Delegate for packet handlers
        /// </summary>
        public delegate void PacketHandlerDelegate(PacketStructure packet, GameClient client);

        /// <summary>
        /// Information about a packet type
        /// </summary>
        public class PacketInfo
        {
            public ushort ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool RequiresAuthentication { get; set; }
            public int MinDataLength { get; set; }
            public int MaxDataLength { get; set; }
        }

        /// <summary>
        /// Handle a packet with improved error handling and logging
        /// </summary>
        public static void HandlePacket(byte[] rawPacket, GameClient client)
        {
            if (rawPacket == null || client == null)
                return;

            try
            {
                // Parse packet structure
                var packet = PacketStructure.Parse(rawPacket);
                
                // Validate packet
                if (!packet.IsValid(ClientSeal))
                {
                    LoggingService.SecurityEvent("PacketHandler", $"Invalid packet seal from {client.Entity?.Name ?? "Unknown"}");
                    client.Disconnect();
                    return;
                }

                // Check packet filtering
                if (client.Filtering && client.PacketFilter.Filter(packet.ID))
                {
                    LoggingService.SystemDebug("PacketHandler", $"Packet {packet.ID} filtered for {client.Entity?.Name ?? "Unknown"}");
                    return;
                }

                // Get packet info
                var packetInfo = GetPacketInfo(packet.ID);
                if (packetInfo != null)
                {
                    // Validate data length
                    if (packet.Data.Length < packetInfo.MinDataLength || 
                        (packetInfo.MaxDataLength > 0 && packet.Data.Length > packetInfo.MaxDataLength))
                    {
                        LoggingService.SecurityEvent("PacketHandler", 
                            $"Invalid packet data length for {packetInfo.Name}: {packet.Data.Length} bytes from {client.Entity?.Name ?? "Unknown"}");
                        client.Disconnect();
                        return;
                    }

                    // Check authentication requirement
                    if (packetInfo.RequiresAuthentication && client.Entity == null)
                    {
                        LoggingService.SecurityEvent("PacketHandler", 
                            $"Unauthenticated client attempted to send {packetInfo.Name}");
                        client.Disconnect();
                        return;
                    }
                }

                // Log packet reception
                LoggingService.ClientPacketReceived(client.Entity?.Name ?? "Unknown", packet.ID, rawPacket.Length);

                // Handle packet
                if (_handlers.TryGetValue(packet.ID, out var handler))
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    
                    try
                    {
                        handler(packet, client);
                    }
                    catch (Exception ex)
                    {
                        LoggingService.SystemError("PacketHandler", 
                            $"Error handling packet {packet.ID} ({packetInfo?.Name ?? "Unknown"}) from {client.Entity?.Name ?? "Unknown"}", ex);
                        
                        // Don't disconnect for most errors, just log them
                        if (IsCriticalPacketError(ex))
                        {
                            client.Disconnect();
                        }
                    }
                    finally
                    {
                        stopwatch.Stop();
                        
                        // Track packet performance
                        PacketPerformanceTracker.TrackPacketProcessing(packet.ID, () => { });
                        
                        // Log slow packets
                        if (stopwatch.ElapsedMilliseconds > 50)
                        {
                            LoggingService.SystemWarning("PacketHandler", 
                                $"Slow packet {packet.ID} ({packetInfo?.Name ?? "Unknown"}): {stopwatch.ElapsedMilliseconds}ms");
                        }
                    }
                }
                else
                {
                    LoggingService.SystemWarning("PacketHandler", 
                        $"Unhandled packet {packet.ID} from {client.Entity?.Name ?? "Unknown"}");
                }
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("PacketHandler", 
                    $"Critical error handling packet from {client.Entity?.Name ?? "Unknown"}", ex);
                client.Disconnect();
            }
        }

        /// <summary>
        /// Register a packet handler
        /// </summary>
        public static void RegisterHandler(ushort packetId, PacketHandlerDelegate handler, PacketInfo info = null)
        {
            _handlers.TryAdd(packetId, handler);
            
            if (info != null)
            {
                info.ID = packetId;
                _packetInfo.TryAdd(packetId, info);
            }
            
            LoggingService.SystemDebug("PacketHandler", $"Registered handler for packet {packetId} ({info?.Name ?? "Unknown"})");
        }

        /// <summary>
        /// Get packet information
        /// </summary>
        public static PacketInfo GetPacketInfo(ushort packetId)
        {
            return _packetInfo.TryGetValue(packetId, out var info) ? info : null;
        }

        /// <summary>
        /// Get all registered packet handlers
        /// </summary>
        public static IEnumerable<KeyValuePair<ushort, PacketInfo>> GetRegisteredPackets()
        {
            return _packetInfo.ToList();
        }

        /// <summary>
        /// Check if a packet is critical (should disconnect client on error)
        /// </summary>
        private static bool IsCriticalPacketError(Exception ex)
        {
            return ex is ArgumentOutOfRangeException ||
                   ex is IndexOutOfRangeException ||
                   ex is OutOfMemoryException ||
                   ex.Message.Contains("buffer") && ex.Message.Contains("overflow");
        }

        /// <summary>
        /// Register all packet handlers
        /// </summary>
        private static void RegisterPacketHandlers()
        {
            // Register common packet handlers
            RegisterHandler(10005, HandleWalk, new PacketInfo 
            { 
                Name = "Walk", 
                Description = "Player movement packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });

            RegisterHandler(10010, HandleAction, new PacketInfo 
            { 
                Name = "Action", 
                Description = "Player action packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });

            RegisterHandler(1004, HandleTalk, new PacketInfo 
            { 
                Name = "Talk", 
                Description = "Chat message packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });

            RegisterHandler(1022, HandleAttack, new PacketInfo 
            { 
                Name = "Attack", 
                Description = "Combat attack packet",
                RequiresAuthentication = true,
                MinDataLength = 8
            });

            RegisterHandler(3200, HandleSignIn, new PacketInfo 
            { 
                Name = "SignIn", 
                Description = "Daily sign-in packet",
                RequiresAuthentication = true,
                MinDataLength = 0
            });

            // Register more handlers as needed...
            RegisterLegacyHandlers();
        }

        /// <summary>
        /// Register legacy packet handlers for compatibility
        /// </summary>
        private static void RegisterLegacyHandlers()
        {
            // Handle legacy packets by delegating to existing handlers
            RegisterHandler(3255, (packet, client) => 
            {
                var pkt = new MsgItemRefineRecord();
                if (pkt.Read(packet.Data.ToArray()))
                    pkt.Handle(client);
            }, new PacketInfo { Name = "ItemRefineRecord", Description = "Item refinement record" });

            RegisterHandler(3251, (packet, client) => 
            {
                var pkt = new MsgItemRefineOpt();
                if (pkt.Read(packet.Data.ToArray()))
                    pkt.Handle(client, packet.Data.ToArray());
            }, new PacketInfo { Name = "ItemRefineOpt", Description = "Item refinement option" });

            RegisterHandler(3253, (packet, client) => 
            {
                var pkt = new MsgUserAbilityScore();
                if (pkt.Read(packet.Data.ToArray()))
                    pkt.Handle(client);
            }, new PacketInfo { Name = "UserAbilityScore", Description = "User ability score" });

            RegisterHandler(3257, (packet, client) => 
            {
                var pkt = new MsgRankMemberShow();
                if (pkt.Read(packet.Data.ToArray()))
                    pkt.Handle(client);
            }, new PacketInfo { Name = "RankMemberShow", Description = "Rank member display" });

            RegisterHandler(3256, (packet, client) => 
            {
                var pkt = new MsgEquipRefineRank();
                if (pkt.Read(packet.Data.ToArray()))
                    pkt.Handle(client);
            }, new PacketInfo { Name = "EquipRefineRank", Description = "Equipment refinement ranking" });
        }

        #region Packet Handlers

        private static void HandleWalk(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read walk data using 7-bit encoding
            var direction = reader.Read7BitEncodedInt() % 24;
            var uid = reader.Read7BitEncodedInt();
            var movementType = reader.Read7BitEncodedInt();
            var timeStamp = reader.Read7BitEncodedInt();
            var mapId = reader.Read7BitEncodedInt();

            // Process walk movement
            // This would contain the logic from the original walk handler
            LoggingService.SystemDebug("PacketHandler", 
                $"Walk: Direction={direction}, UID={uid}, Type={movementType}, Map={mapId}");
        }

        private static void HandleAction(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read action data
            var actionType = reader.ReadUInt32();
            var targetId = reader.ReadUInt32();

            // Process action
            LoggingService.SystemDebug("PacketHandler", 
                $"Action: Type={actionType}, Target={targetId}");
        }

        private static void HandleTalk(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read talk data
            var talkType = reader.ReadUInt16();
            var messageLength = reader.ReadUInt16();
            var message = reader.ReadString(messageLength);

            // Process chat message
            LoggingService.SystemDebug("PacketHandler", 
                $"Talk: Type={talkType}, Message={message}");
        }

        private static void HandleAttack(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read attack data
            var attackType = reader.ReadUInt32();
            var targetId = reader.ReadUInt32();

            // Process attack
            LoggingService.SystemDebug("PacketHandler", 
                $"Attack: Type={attackType}, Target={targetId}");
        }

        private static void HandleSignIn(PacketStructure packet, GameClient client)
        {
            // Handle daily sign-in
            MsgSignIn.Handle(client, packet.Data.ToArray());
        }

        #endregion

        #region Statistics and Monitoring

        /// <summary>
        /// Get packet handling statistics
        /// </summary>
        public static PacketHandlerStats GetStats()
        {
            return new PacketHandlerStats
            {
                TotalHandlers = _handlers.Count,
                RegisteredPackets = _packetInfo.Count,
                Handlers = _handlers.Keys.ToList(),
                PacketInfo = _packetInfo.ToList()
            };
        }

        /// <summary>
        /// Packet handler statistics
        /// </summary>
        public class PacketHandlerStats
        {
            public int TotalHandlers { get; set; }
            public int RegisteredPackets { get; set; }
            public List<ushort> Handlers { get; set; }
            public List<KeyValuePair<ushort, PacketInfo>> PacketInfo { get; set; }
        }

        #endregion
    }
} 