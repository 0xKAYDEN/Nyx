using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nyx.Server.Client;
using Nyx.Server.Game;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Threding
{
    public class TournamentsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private DateTime _lastPlunderWarStartCheck = DateTime.MinValue;
        private DateTime _lastPlunderWarEndCheck = DateTime.MinValue;
        private DateTime _lastCTFStartCheck = DateTime.MinValue;
        private DateTime _lastTeamPKCheck = DateTime.MinValue;
        private DateTime _lastSkillTeamPkCheck = DateTime.MinValue;

        public TournamentsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Tournament Service started");

            // Use a periodic timer for more precise timing
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await CheckTournamentTimesAsync();
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Tournament Service stopped gracefully");
            }
        }
        private async Task CheckTournamentTimesAsync()
        {
            var now = DateTime.Now;

            // Check PlunderWar start (6:00 PM)
            await CheckPlunderWarStart(now);

            // Check PlunderWar end (8:00 PM)
            await CheckPlunderWarEnd(now);

            // Check CaptureTheFlag start (Saturday 8:00 PM)
            await CheckCaptureTheFlagStart(now);

            // Check CaptureTheFlag ongoing events
            await CheckCaptureTheFlagOngoing(now);

            // Spawn CTF flags if CTF is active
            await SpawnCTFFlags();

            // Check TeamPK start (Saturday 6:55 PM)
            await CheckTeamPKStart(now);

            // Check SkillTeamPk start (Wednesday 7:40 PM)
            await CheckSkillTeamPkStart(now);

        }

        #region PlunderWar Methods
        private async Task CheckPlunderWarStart(DateTime now)
        {
            if (now.Hour == 18 && now.Minute == 0 && now.Second == 0)
            {
                if ((now - _lastPlunderWarStartCheck).TotalSeconds >= 1)
                {
                    _lastPlunderWarStartCheck = now;
                    await StartPlunderWarAsync();
                }
            }
        }
        private async Task CheckPlunderWarEnd(DateTime now)
        {
            if (now.Hour == 20 && now.Minute == 0 && now.Second == 0)
            {
                if ((now - _lastPlunderWarEndCheck).TotalSeconds >= 1)
                {
                    _lastPlunderWarEndCheck = now;
                    await EndPlunderWarAsync();
                }
            }
        }
        private async Task StartPlunderWarAsync()
        {
            try
            {
                Log.Information("Starting PlunderWar tournament at {Time}", DateTime.Now);
                Kernel.PlunderWar = true;
                Log.Information("PlunderWar started successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error starting PlunderWar tournament");
            }
        }

        private async Task EndPlunderWarAsync()
        {
            try
            {
                Log.Information("Ending PlunderWar tournament at {Time}", DateTime.Now);
                Kernel.PlunderWar = false;
                Network.GamePackets.Union.UnionClass.UpGradeUnion();
                Log.Information("PlunderWar ended successfully, union upgraded");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error ending PlunderWar tournament");
            }
        }
        #endregion

        #region CaptureTheFlag Methods
        private async Task CheckCaptureTheFlagStart(DateTime now)
        {
            if (now.DayOfWeek == DayOfWeek.Saturday &&
                now.Hour == 20 && now.Minute == 0 && now.Second == 0)
            {
                if ((now - _lastCTFStartCheck).TotalSeconds >= 1)
                {
                    _lastCTFStartCheck = now;
                    await StartCaptureTheFlagAsync();
                }
            }
        }

        private async Task CheckCaptureTheFlagOngoing(DateTime now)
        {
            if (CaptureTheFlag.IsWar)
            {
                try
                {
                    // Send CTF updates
                    Program.World.CTF.SendUpdates();

                    // Check if CTF should end (1 hour duration)
                    if (now >= CaptureTheFlag.StartTime.AddHours(1))
                    {
                        await EndCaptureTheFlagAsync();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error during CTF ongoing checks");
                }
            }
        }

        private async Task SpawnCTFFlags()
        {
            try
            {
                if (Program.World.CTF != null)
                {
                    Program.World.CTF.SpawnFlags();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error spawning CTF flags");
            }
        }

        private async Task StartCaptureTheFlagAsync()
        {
            try
            {
                if (!CaptureTheFlag.IsWar)
                {
                    Log.Information("Starting CaptureTheFlag tournament at {Time}", DateTime.Now);

                    CaptureTheFlag.IsWar = true;
                    CaptureTheFlag.StartTime = DateTime.Now;
                    CaptureTheFlag.ClearHistory();

                    // Reset all guilds and members
                    await ResetAllGuildsForCTF();

                    Log.Information("CaptureTheFlag started successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error starting CaptureTheFlag tournament");
            }
        }

        private async Task ResetAllGuildsForCTF()
        {
            foreach (var guild in Kernel.Guilds.Values)
            {
                try
                {
                    // Reset guild CTF properties
                    guild.CTFFlagScore = 0;
                    guild.Points = 0;
                    guild.CTFdonationCPs = 0;
                    guild.CTFdonationSilver = 0;

                    // Calculate initial rank
                    guild.CalculateCTFRank(true);

                    // Reset all members in the guild
                    foreach (var member in guild.Members.Values)
                    {
                        member.Exploits = 0;
                        member.ExploitsRank = 0;
                        member.CTFCpsReward = 0;
                        member.CTFSilverReward = 0;
                    }

                    // Recalculate ranks after member reset
                    guild.CalculateCTFRank(false);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error resetting guild {GuildName} for CTF", guild.Name);
                }
            }
        }

        private async Task EndCaptureTheFlagAsync()
        {
            try
            {
                Log.Information("Ending CaptureTheFlag tournament at {Time}", DateTime.Now);
                CaptureTheFlag.IsWar = false;
                CaptureTheFlag.Close();
                Log.Information("CaptureTheFlag ended successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error ending CaptureTheFlag tournament");
            }
        }
        #endregion

        #region Team PK Methods
        private async Task CheckTeamPKStart(DateTime now)
        {
            // Saturday at 6:55:00 PM
            if (now.DayOfWeek == DayOfWeek.Saturday &&
                now.Hour == 18 && now.Minute == 55 && now.Second == 0)
            {
                if ((now - _lastTeamPKCheck).TotalSeconds >= 1)
                {
                    _lastTeamPKCheck = now;
                    await StartTeamPKInvitesAsync();
                }
            }
        }

        private async Task StartTeamPKInvitesAsync()
        {
            try
            {
                Log.Information("Sending Team PK invitations at {Time}", DateTime.Now);

                // Send invitations to all online players who are not in jail
                foreach (var client in GetOnlineClients())
                {
                    try
                    {
                        if (!client.Entity.InJail())
                        {
                            var alert = new Network.GamePackets.AutoInvite
                            {
                                StrResID = 10543,
                                Countdown = 60,
                                Action = 1
                            };

                            client.Entity.StrResID = 10543;
                            client.Send(alert.Encode());

                            Log.Debug("Team PK invitation sent to player: {PlayerName}", client.Entity.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error sending Team PK invite to player: {PlayerName}",client.Entity?.Name ?? "Unknown");
                    }
                }

                Log.Information("Team PK invitations sent successfully to all eligible players");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending Team PK invitations");
            }
        }

        #endregion

        #region SkillTeamPk Methods
        private async Task CheckSkillTeamPkStart(DateTime now)
        {
            // Saturday at 6:55:00 PM
            if (now.DayOfWeek == DayOfWeek.Wednesday &&
                now.Hour == 19 && now.Minute == 40 && now.Second == 0)
            {
                if ((now - _lastSkillTeamPkCheck).TotalSeconds >= 1)
                {
                    _lastSkillTeamPkCheck = now;
                    await StartSkillTeamPkInvitesAsync();
                }
            }
        }

        private async Task StartSkillTeamPkInvitesAsync()
        {
            try
            {
                Log.Information("Sending Team PK invitations at {Time}", DateTime.Now);

                // Send invitations to all online players who are not in jail
                foreach (var client in GetOnlineClients())
                {
                    try
                    {
                        if (!client.Entity.InJail())
                        {
                            var alert = new Network.GamePackets.AutoInvite
                            {
                                StrResID = 10541,
                                Countdown = 60,
                                Action = 1
                            };

                            client.Entity.StrResID = 10541;
                            client.Send(alert.Encode());

                            Log.Debug("Team PK invitation sent to player: {PlayerName}", client.Entity.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error sending Team PK invite to player: {PlayerName}", client.Entity?.Name ?? "Unknown");
                    }
                }

                Log.Information("Team PK invitations sent successfully to all eligible players");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending Team PK invitations");
            }
        }
        #endregion

        private IEnumerable<GameClient> GetOnlineClients()
        {
            // This method should return all currently online clients
            // Replace this with your actual method to get online players
            return Kernel.GamePool.Values.Where(client => client != null && client.Entity != null);
        }

        public override void Dispose()
        {
            Log.Information("Tournament Service disposed");
            base.Dispose();
        }


    }
}
