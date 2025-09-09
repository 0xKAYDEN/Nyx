using System;

namespace Nyx.Server.Client
{
    public static class GameClientExtensions
    {
        public static void LogPlayerAction(this GameClient client, string action, string details = null)
        {
            if (client?.Entity?.Name != null)
            {
                LoggingService.PlayerAction(client.Entity.Name, action, details);
            }
        }

        public static void LogPlayerLevelUp(this GameClient client, int newLevel)
        {
            if (client?.Entity?.Name != null)
            {
                LoggingService.PlayerLevelUp(client.Entity.Name, newLevel);
            }
        }

        public static void LogPlayerDeath(this GameClient client, string killerName = null)
        {
            if (client?.Entity?.Name != null)
            {
                LoggingService.PlayerDeath(client.Entity.Name, killerName);
            }
        }

        public static void LogItemTransaction(this GameClient client, string itemName, string transactionType, int quantity = 1)
        {
            if (client?.Entity?.Name != null)
            {
                LoggingService.ItemTransaction(client.Entity.Name, itemName, transactionType, quantity);
            }
        }

        public static void LogGuildAction(this GameClient client, string action, string details = null)
        {
            if (client?.Entity?.Name != null && client.Entity.GuildID > 0)
            {
                // You might want to get the actual guild name here
                string guildName = $"Guild_{client.Entity.GuildID}";
                LoggingService.GuildAction(guildName, action, details);
            }
        }

        public static void LogSuspiciousActivity(this GameClient client, string activity)
        {
            if (client?.Entity?.Name != null)
            {
                LoggingService.SuspiciousActivity(client.Entity.Name, activity, client.IP);
            }
        }
    }
} 