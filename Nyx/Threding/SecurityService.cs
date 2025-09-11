using Microsoft.Extensions.Hosting;
using Nyx.Server.Client;
using Nyx.Server.Database;
using Nyx.Server.Game;
using Nyx.Server.Game.ConquerStructures;
using Serilog;
using static Nyx.Server.Network.GamePackets.NobilityIcon;

namespace Nyx.Server.Threding
{
    public class SecurityService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);

        public SecurityService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Security Service started");

            using var timer = new PeriodicTimer(_checkInterval);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await PerformSecurityChecksAsync();
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Security Service stopped gracefully");
            }
        }
        private async Task PerformSecurityChecksAsync()
        {
            try
            {
                var onlineClients = GetOnlineClients().ToList();

                foreach (var client in onlineClients)
                {
                    try
                    {
                        await CheckBattlePowerAsync(client);
                        await CheckGMRobeAsync(client);
                        await CheckMaxHp(client);
                        // Add more security checks here as needed
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error performing security checks for client: {ClientId}", client.Account.EntityID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in security check cycle");
            }
        }

        #region Check over Max Hp

        private async Task CheckMaxHp(GameClient client)
        {
            if (client?.Entity == null || client.Account == null)
                return;

            if (client.Entity.FullyLoaded)
            {
                if (client.Entity.Hitpoints > client.Entity.MaxHitpoints && client.Entity.MaxHitpoints > 1 && !client.Entity.Transformed)
                {
                    client.Entity.Hitpoints = client.Entity.MaxHitpoints;

                    Log.Warning("Max Hp violation detected: Player {PlayerName} (Username: {Username}, IPAddress: {BP})", client.Entity.Name, client.Account.Username, client.Account.IP);
                }
            }
        }

        #endregion

        #region Battle Power Checks
        private async Task CheckBattlePowerAsync(GameClient client)
        {
            if (client?.Entity == null || client.Account == null)
                return;

            // Skip Game Masters
            if (client.Account.State == AccountTable.AccountState.GameMaster)
                return;

            var entity = client.Entity;
            var battlePower = entity.BattlePower;
            var nobilityRank = entity.NobilityRank;

            bool shouldDisconnect = false;

            switch (nobilityRank)
            {
                case NobilityRank.King:
                    shouldDisconnect = battlePower > 405;
                    break;
                case NobilityRank.Prince:
                    shouldDisconnect = battlePower > 402;
                    break;
                case NobilityRank.Duke:
                    shouldDisconnect = battlePower > 400;
                    break;
                case NobilityRank.Earl:
                    shouldDisconnect = battlePower > 398;
                    break;
                case NobilityRank.Baron:
                    shouldDisconnect = battlePower > 396;
                    break;
                case NobilityRank.Knight:
                    shouldDisconnect = battlePower > 394;
                    break;
                case NobilityRank.Serf:
                    shouldDisconnect = battlePower > 390;
                    break;
                default:
                    // Default check for any rank
                    shouldDisconnect = battlePower > 405;
                    break;
            }

            if (shouldDisconnect)
            {
                Log.Warning("Battle power violation detected: Player {PlayerName} (Rank: {Rank}, BP: {BP})", entity.Name, nobilityRank, battlePower);

                await HandleCheatingClientAsync(client, "Battle power violation");
            }
        }
        #endregion
        #region GM Robe Check
        private async Task CheckGMRobeAsync(GameClient client)
        {
            if (client?.Account == null || client.Entity == null)
                return;

            // Only check non-cheated accounts
            if (client.Account.State != AccountTable.AccountState.GameMaster)
                return;

            bool hasGMRobe = false;

            // Check inventory for GM robe (Item ID 137010)
            if (client.Inventory.Contains(137010, 1))
            {
                hasGMRobe = true;
                Log.Warning("GM robe found in inventory: Player {PlayerName}", client.Entity.Name);
            }

            // Check if wearing GM robe
            if (client.Equipment.IsHavingGMRobe())
            {
                hasGMRobe = true;
                Log.Warning("GM robe equipped: Player {PlayerName}", client.Entity.Name);
            }

            if (hasGMRobe)
            {
                await HandleGMRobeViolationAsync(client);
            }
        }
        private async Task HandleGMRobeViolationAsync(GameClient client)
        {
            try
            {
                Log.Warning("GM robe violation detected: Player {PlayerName}", client.Entity.Name);

                // Remove all equipment
                RemoveAllEquipment(client);

                // Clear inventory
                ClearInventory(client);

                // Mark as cheated and disconnect
                await HandleCheatingClientAsync(client, "GM robe violation");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error handling GM robe violation for player: {PlayerName}", client.Entity.Name);
            }
        }
        private void RemoveAllEquipment(GameClient client)
        {
            try
            {
                // Remove equipment from all slots
                byte[] equipmentSlots = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 18, 19 };

                foreach (byte slot in equipmentSlots)
                {
                    try
                    {
                        client.Equipment.Remove(slot);
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex, "Error removing equipment from slot {Slot} for player: {PlayerName}",slot, client.Entity.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error removing equipment for player: {PlayerName}", client.Entity.Name);
            }
        }

        private void ClearInventory(GameClient client)
        {
            try
            {
                // Create a copy to avoid modification during iteration
                var inventoryItems = client.Inventory.Objects
                    .Where(item => item != null)
                    .ToArray();

                foreach (var item in inventoryItems)
                {
                    try
                    {
                        client.Inventory.Remove(item, Nyx.Server.Game.Enums.ItemUse.Remove);
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex, "Error removing item {ItemId} from inventory for player: {PlayerName}",item.ID, client.Entity.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error clearing inventory for player: {PlayerName}", client.Entity.Name);
            }
        }
        #endregion
        #region Cheating Handling
        private async Task HandleCheatingClientAsync(GameClient client, string reason)
        {
            try
            {
                Log.Warning("Handling cheating client: {PlayerName}, Reason: {Reason}",
                    client.Entity.Name, reason);

                // Mark account as cheated
                client.Account.Cheat();
                client.Account.State = Database.AccountTable.AccountState.Cheated;

                // Log the cheating incident
                LogCheatingIncident(client, reason);

                // Disconnect the client
                await DisconnectClientAsync(client);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error handling cheating client: {PlayerName}", client.Entity.Name);
            }
        }

        private void LogCheatingIncident(GameClient client, string reason)
        {
            try
            {
                // Log to database or file
                Log.Warning("CHEATING_INCIDENT: Player={PlayerName}, Account={AccountId}, IP={IP}, Reason={Reason}, Time={Time}",
                    client.Entity.Name,
                    client.Account.EntityID,
                    client.IP,
                    reason,
                    DateTime.UtcNow);

                // You could also save this to a database
                // await _cheatLogger.LogAsync(client, reason);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error logging cheating incident for player: {PlayerName}", client.Entity.Name);
            }
        }

        private async Task DisconnectClientAsync(GameClient client)
        {
            try
            {
                // Give a small delay to ensure cheating state is saved
                await Task.Delay(100);
                client.Disconnect();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error disconnecting client: {PlayerName}", client.Entity.Name);
            }
        }
        #endregion
        private IEnumerable<GameClient> GetOnlineClients()
        {
            // Replace with your actual online clients retrieval
            return Kernel.GamePool.Values
                .Where(client => client != null &&
                       client.Entity != null &&
                       client.Account != null &&
                       client.Socket != null)
                .ToList();
        }

        public override void Dispose()
        {
            Log.Information("Security Service disposed");
            base.Dispose();
        }

    }
}
