using Microsoft.Extensions.Hosting;
using Nyx.Server.Database;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network.GamePackets.Union;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Threding
{
    public class WorldStateService : BackgroundService
    {
        private readonly TimeSpan _saveInterval = TimeSpan.FromMinutes(2);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("World Save Service started. Auto-saving every {Interval}", _saveInterval);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await SaveWorldStateAsync();
                    await Task.Delay(_saveInterval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Perform final save before shutdown
                    await SaveWorldStateAsync();
                    break;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error during world save");
                    await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
                }
            }
        }
        private async Task SaveWorldStateAsync()
        {
            Log.Information("Starting server save process...");
            try
            {
                using (var conn = Database.DataHolder.MySqlConnection)
                {
                    await conn.OpenAsync();
                    foreach (Client.GameClient client in Kernel.GamePool.Values)
                    {
                        client.Account.Save(client);
                        Database.EntityTable.SaveEntity(client, conn);
                        Database.DailyQuestTable.Save(client);
                        Database.SkillTable.SaveProficiencies(client, conn);
                        Database.ActivenessTable.Save(client);
                        Database.ChiTable.Save(client);
                        Database.SkillTable.SaveSpells(client, conn);
                        Database.MailboxTable.Save(client);
                        Database.ArenaTable.SaveArenaStatistics(client.ArenaStatistic, client.CP, conn);
                        Database.TeamArenaTable.SaveArenaStatistics(client.TeamArenaStatistic, conn);
                    }
                }
                Nyx.Server.Database.JiangHu.SaveJiangHu();
                AuctionBase.Save();
                Database.Flowers.SaveFlowers();
                Database.InnerPowerTable.Save();
                Database.EntityVariableTable.Save(0, Program.Vars);
                using (MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT))
                {
                    cmd.Select("configuration");
                    using (MySqlReader r = new MySqlReader(cmd))
                    {
                        if (r.Read())
                        {
                            new Database.MySqlCommand(Database.MySqlCommandType.UPDATE).Update("configuration").Set("ServerKingdom", Kernel.ServerKingdom).Set("ItemUID", Network.GamePackets.ConquerItem.ItemUID.Now).Set("GuildID", Game.ConquerStructures.Society.Guild.GuildCounter.Now).Set("UnionID", Union.UnionCounter.Now).Execute();
                            if (r.ReadByte("LastDailySignReset") != DateTime.Now.Month) MsgSignIn.Reset();
                        }
                    }
                }
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE).Update("configuration"))
                    cmd.Set("LastDailySignReset", DateTime.Now.Month).Execute();
            }
            catch (Exception e)
            {
                LoggingService.SystemError("Save", "Error during server save", e);
            }

            // Simulate save operation
            await Task.Delay(1000); // Simulate I/O delay
            //_gameWorld.LastSaveTime = DateTime.UtcNow;

            //Log.Information("World saved successfully at {Time}", _gameWorld.LastSaveTime);
        }

    }
}
