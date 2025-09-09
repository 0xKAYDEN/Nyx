using System;
using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Network.ImprovedPacketSystem
{
    /// <summary>
    /// Integration example showing how to use the improved packet system
    /// </summary>
    public static class PacketSystemIntegration
    {
        /// <summary>
        /// Initialize the improved packet system
        /// </summary>
        public static void Initialize()
        {
            LoggingService.SystemDebug("PacketSystem", "Initializing improved packet system...");
            
            // Register additional packet handlers
            RegisterAdditionalHandlers();
            
            LoggingService.SystemDebug("PacketSystem", "Improved packet system initialized successfully");
        }

        /// <summary>
        /// Handle packets using the improved system
        /// </summary>
        public static void HandlePacket(byte[] packet, GameClient client)
        {
            // Use the improved packet handler
            ImprovedPacketHandler.HandlePacket(packet, client);
        }

        /// <summary>
        /// Register additional packet handlers
        /// </summary>
        private static void RegisterAdditionalHandlers()
        {
            // Register more packet handlers as needed
            ImprovedPacketHandler.RegisterHandler(1006, HandleUserInfo, new ImprovedPacketHandler.PacketInfo 
            { 
                Name = "UserInfo", 
                Description = "User information packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });

            ImprovedPacketHandler.RegisterHandler(1008, HandleItemInfo, new ImprovedPacketHandler.PacketInfo 
            { 
                Name = "ItemInfo", 
                Description = "Item information packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });

            ImprovedPacketHandler.RegisterHandler(1009, HandleItemEquip, new ImprovedPacketHandler.PacketInfo 
            { 
                Name = "ItemEquip", 
                Description = "Item equipment packet",
                RequiresAuthentication = true,
                MinDataLength = 8
            });

            ImprovedPacketHandler.RegisterHandler(1015, HandleName, new ImprovedPacketHandler.PacketInfo 
            { 
                Name = "Name", 
                Description = "Name packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });

            ImprovedPacketHandler.RegisterHandler(1019, HandleFriend, new ImprovedPacketHandler.PacketInfo 
            { 
                Name = "Friend", 
                Description = "Friend system packet",
                RequiresAuthentication = true,
                MinDataLength = 4
            });
        }

        #region Additional Packet Handlers

        private static void HandleUserInfo(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read user info data
            var userId = reader.ReadUInt32();
            
            // Process user info
            LoggingService.SystemDebug("PacketHandler", $"UserInfo: UserID={userId}");
            
            // Delegate to existing handler if needed
            // MsgUserInfo.Handle(client, packet.Data.Encode());
        }

        private static void HandleItemInfo(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read item info data
            var itemId = reader.ReadUInt32();
            var itemType = reader.ReadUInt16();
            
            // Process item info
            LoggingService.SystemDebug("PacketHandler", $"ItemInfo: ItemID={itemId}, Type={itemType}");
            
            // Delegate to existing handler if needed
            // MsgItemInfo.Handle(client, packet.Data.Encode());
        }

        private static void HandleItemEquip(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read item equip data
            var itemId = reader.ReadUInt32();
            var equipSlot = reader.ReadUInt32();
            
            // Process item equip
            LoggingService.SystemDebug("PacketHandler", $"ItemEquip: ItemID={itemId}, Slot={equipSlot}");
            
            // Delegate to existing handler if needed
            // MsgItemEquip.Handle(client, packet.Data.Encode());
        }

        private static void HandleName(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read name data
            var nameLength = reader.ReadUInt16();
            var name = reader.ReadString(nameLength);
            
            // Process name
            LoggingService.SystemDebug("PacketHandler", $"Name: {name}");
            
            // Delegate to existing handler if needed
            // MsgName.Handle(client, packet.Data.Encode());
        }

        private static void HandleFriend(PacketStructure packet, GameClient client)
        {
            var reader = new PacketReader(packet.Data.Span);
            
            // Read friend data
            var friendAction = reader.ReadUInt32();
            var friendId = reader.ReadUInt32();
            
            // Process friend action
            LoggingService.SystemDebug("PacketHandler", $"Friend: Action={friendAction}, FriendID={friendId}");
            
            // Delegate to existing handler if needed
            // MsgFriend.Handle(client, packet.Data.Encode());
        }

        #endregion

        /// <summary>
        /// Get packet system statistics
        /// </summary>
        public static void LogPacketStatistics()
        {
            var handlerStats = ImprovedPacketHandler.GetStats();
            var performanceStats = PacketPerformanceTracker.GetPerformanceSummary();
            
            LoggingService.SystemDebug("PacketSystem", $"Handler Stats: {handlerStats.TotalHandlers} handlers, {handlerStats.RegisteredPackets} packets");
            LoggingService.SystemDebug("PacketSystem", $"Performance: {performanceStats}");
            
            // Log top performing packets
            var topPackets = PacketPerformanceTracker.GetTopPerformingPackets(5);
            LoggingService.SystemDebug("PacketSystem", "Top performing packets:");
            foreach (var packet in topPackets)
            {
                LoggingService.SystemDebug("PacketSystem", $"  {packet}");
            }
            
            // Log slowest packets
            var slowestPackets = PacketPerformanceTracker.GetSlowestPackets(5);
            LoggingService.SystemDebug("PacketSystem", "Slowest packets:");
            foreach (var packet in slowestPackets)
            {
                LoggingService.SystemDebug("PacketSystem", $"  {packet}");
            }
        }

        /// <summary>
        /// Example of creating and sending a packet using the improved system
        /// </summary>
        public static void SendExamplePacket(GameClient client)
        {
            // Create a walk packet using the improved system
            var packetData = PacketFactory.CreateProtocolPacket(10005, 
                (uint)Nyx.Server.Game.Enums.ConquerAngle.North,
                client.Entity?.UID ?? 0,
                0, // Walk type
                (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                client.Entity?.MapID ?? 0
            );
            
            // Send the packet
            client.Send(packetData);
            
            LoggingService.SystemDebug("PacketSystem", "Sent example walk packet");
        }

        /// <summary>
        /// Example of creating a custom packet
        /// </summary>
        public static void SendCustomPacket(GameClient client, string message)
        {
            // Create a custom packet using the packet factory
            var packetData = PacketFactory.CreatePacket(9999, writer =>
            {
                writer.WriteString(message);
                writer.WriteUInt32((uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            });
            
            // Send the packet
            client.Send(packetData);
            
            LoggingService.SystemDebug("PacketSystem", $"Sent custom packet with message: {message}");
        }

        /// <summary>
        /// Cleanup the packet system
        /// </summary>
        public static void Cleanup()
        {
            LoggingService.SystemDebug("PacketSystem", "Cleaning up improved packet system...");
            
            // Dispose performance tracker
            PacketPerformanceTracker.Dispose();
            
            LoggingService.SystemDebug("PacketSystem", "Improved packet system cleaned up successfully");
        }
    }
} 