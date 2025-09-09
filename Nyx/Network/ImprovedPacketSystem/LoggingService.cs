using System;

namespace Nyx.Server.Network.ImprovedPacketSystem
{
    /// <summary>
    /// Simple logging service for the improved packet system
    /// </summary>
    public static class LoggingService
    {
        /// <summary>
        /// Log a system debug message
        /// </summary>
        public static void SystemDebug(string component, string message)
        {
            Console.WriteLine($"[DEBUG][{component}] {message}");
        }

        /// <summary>
        /// Log a system warning message
        /// </summary>
        public static void SystemWarning(string component, string message)
        {
            Console.WriteLine($"[WARN][{component}] {message}");
        }

        /// <summary>
        /// Log a system error message
        /// </summary>
        public static void SystemError(string component, string message, Exception ex = null)
        {
            Console.WriteLine($"[ERROR][{component}] {message}");
            if (ex != null)
            {
                Console.WriteLine($"[ERROR][{component}] Exception: {ex.Message}");
                Console.WriteLine($"[ERROR][{component}] StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Log a security event
        /// </summary>
        public static void SecurityEvent(string component, string message)
        {
            Console.WriteLine($"[SECURITY][{component}] {message}");
        }

        /// <summary>
        /// Log a client packet received
        /// </summary>
        public static void ClientPacketReceived(string clientName, ushort packetId, int packetSize)
        {
            Console.WriteLine($"[PACKET][RECV] {clientName} -> Packet {packetId} ({packetSize} bytes)");
        }

        /// <summary>
        /// Log a client packet sent
        /// </summary>
        public static void ClientPacketSent(string clientName, ushort packetId, int packetSize)
        {
            Console.WriteLine($"[PACKET][SEND] {clientName} <- Packet {packetId} ({packetSize} bytes)");
        }
    }
} 