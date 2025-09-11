using Nyx.Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Network.GamePackets;
using System.Threading;
using System.Threading.Generic;
using Nyx.Server.Network.Sockets;
using Nyx.Server.Game.ConquerStructures;
using Nyx.Server.Game.ConquerStructures.Society;
using Nyx.Server.Client;
using System.Drawing;
using Nyx.Server.Database;
using Nyx.Server.Interfaces;
using Nyx.Server.Network;
using Message = Nyx.Server.Network.GamePackets.Message;


namespace Nyx.Server
{
    public class World
    {
        public Joseph.DelayedTask DelayedTask;
        public SteedRace SteedRace;
        public static Time32 ClanWarArenaStampScore;
        public Entity attacked;
        public CaptureTheFlag CTF;
        public Auction Auction;
        public static StaticPool GenericThreadPool;
        public static StaticPool ReceivePool, SendPool;
        public TimerRule<GameClient> Characters, AutoAttack, Companions, Prayer;
        public TimerRule<ClientWrapper> ConnectionReceive, ConnectionReview, ConnectionSend;
        public World()
        {
            GenericThreadPool = new StaticPool(32).Run();
            ReceivePool = new StaticPool(128).Run();
            SendPool = new StaticPool(32).Run();
        }
        public void Init()
        {
            Characters = new TimerRule<GameClient>(CharactersCallback, 1000, ThreadPriority.BelowNormal);
            AutoAttack = new TimerRule<GameClient>(AutoAttackCallback, 1000, ThreadPriority.BelowNormal);
            Companions = new TimerRule<GameClient>(CompanionsCallback, 1000, ThreadPriority.BelowNormal);
            Prayer = new TimerRule<GameClient>(PrayerCallback, 1000, ThreadPriority.BelowNormal);
            /////////////////////////////
            ConnectionReview = new TimerRule<ClientWrapper>(connectionReview, 60000, ThreadPriority.Lowest);
            ConnectionReceive = new TimerRule<ClientWrapper>(connectionReceive, 1);
            ConnectionSend = new TimerRule<ClientWrapper>(connectionSend, 1);
            ////////////////////////////
            Subscribe(ServerFunctions, 5000);
            Subscribe(WorldTournaments, 1000);
            Subscribe(ArenaFunctions, 1000, ThreadPriority.AboveNormal);
            Subscribe(TeamArenaFunctions, 1000, ThreadPriority.AboveNormal);

        }
        public void CreateTournaments()
        {
            SteedRace = new SteedRace();
            ElitePKTournament.Create();
            Game.Features.Tournaments.TeamElitePk.TeamTournament.Create();
            Game.Features.Tournaments.TeamElitePk.SkillTeamTournament.Create();
            CTF = new CaptureTheFlag();
            DelayedTask = new Joseph.DelayedTask();
            Auction = new Auction();
        }
        private void connectionReview(ClientWrapper wrapper, int time)
        {
            ClientWrapper.TryReview(wrapper);
        }
        private void connectionReceive(ClientWrapper wrapper, int time)
        {
            ClientWrapper.TryReceive(wrapper);
        }
        private void connectionSend(ClientWrapper wrapper, int time)
        {
            ClientWrapper.TrySend(wrapper);
        }
        public bool Register(GameClient client)
        {
            if (client.TimerSubscriptions == null)
            {
                client.TimerSyncRoot = new object();
                client.TimerSubscriptions = new IDisposable[]
                {
                    Characters.Add(client),
                    AutoAttack.Add(client),
                    Companions.Add(client),
                    Prayer.Add(client),
                };
                return true;
            }
            return false;
        }
        public void Unregister(GameClient client)
        {
            if (client.TimerSubscriptions == null) return;
            lock (client.TimerSyncRoot)
            {
                if (client.TimerSubscriptions != null)
                {
                    foreach (var timer in client.TimerSubscriptions)
                        timer.Dispose();
                    client.TimerSubscriptions = null;
                }
            }
        }
        public static bool Valid(GameClient client)
        {
            if (!client.Socket.Alive || client.Entity == null || client == null)
            {
                client.Disconnect();
                return false;
            }
            return true;
        }
        private void CharactersCallback(GameClient client, int time)
        {
            if (!Valid(client)) return;
            Time32 Now = new Time32(time);
            #region FloorItems
            /* foreach (var flooritem in Database.FloorItemTable.FloorItemms)
            {
             //   if (Kernel.GetDistance(flooritem.X, flooritem.Y, client.Entity.X, client.Entity.Y) < 17)
                  //  client.SendScreenSpawn(flooritem, true);
            }*/
            #endregion
            #region Bloodshed~Sea item's
            if (client.Entity.IncreaseFinalMDamage)
            {
                if (Time32.Now > client.Entity.IncreaseFinalMDamageStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseFinalMDamage = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreaseFinalPDamage)
            {
                if (Time32.Now > client.Entity.IncreaseFinalPDamageStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseFinalPDamage = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreaseFinalMAttack)
            {
                if (Time32.Now > client.Entity.IncreaseFinalMAttackStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseFinalMAttack = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreaseFinalPAttack)
            {
                if (Time32.Now > client.Entity.IncreaseFinalPAttackStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseFinalPAttack = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreaseImunity)
            {
                if (Time32.Now > client.Entity.IncreaseImunityStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseImunity = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreaseAntiBreack)
            {
                if (Time32.Now > client.Entity.IncreaseAntiBreackStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseAntiBreack = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreasePStrike)
            {
                if (Time32.Now > client.Entity.IncreasePStrikeStamp.AddSeconds(80))
                {
                    client.Entity.IncreasePStrike = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.IncreaseBreack)
            {
                if (Time32.Now > client.Entity.IncreaseBreackStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseBreack = false;
                    client.LoadItemStats();
                }
            }
            if (client.Entity.GodlyShield)
            {
                if (client.Entity.ContainsFlag((ulong)Network.GamePackets.Update.Flags.GodlyShield))
                {
                    if (Time32.Now > client.Entity.GodlyShieldStamp.AddSeconds(80))
                    {
                        client.Entity.RemoveFlag((ulong)Network.GamePackets.Update.Flags.GodlyShield);
                        client.Entity.GodlyShield = false;
                    }
                }
            }
            if (client.Entity.IncreaseAttribute)
            {
                if (Time32.Now > client.Entity.IncreaseAttributeStamp.AddSeconds(80))
                {
                    client.Entity.IncreaseAttribute = false;
                    client.LoadItemStats();
                }
            }
            #endregion
            #region PowerArena
            if (((DateTime.Now.Hour == 12 && DateTime.Now.Minute == 55) || (DateTime.Now.Hour == 19 && DateTime.Now.Minute == 55)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 5 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
            if (((DateTime.Now.Hour == 12 && DateTime.Now.Minute == 56) || (DateTime.Now.Hour == 19 && DateTime.Now.Minute == 56)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 4 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
            if (((DateTime.Now.Hour == 12 && DateTime.Now.Minute == 57) || (DateTime.Now.Hour == 20 && DateTime.Now.Minute == 57)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 3 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
            if (((DateTime.Now.Hour == 12 && DateTime.Now.Minute == 58) || (DateTime.Now.Hour == 20 && DateTime.Now.Minute == 58)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 2 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
            if (((DateTime.Now.Hour == 12 && DateTime.Now.Minute == 59) || (DateTime.Now.Hour == 20 && DateTime.Now.Minute == 59)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be opened in 1 minutes. Please get ready for that!", Color.White, Message.Talk));
            }
            if (((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 00) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 00)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("The Power Arena is open! Find Arena Manager Wang in Twin City (465,234) to sign up for the Arena.", Color.Red, Message.TopLeft));

            }
            if (((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 55) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 55)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be closed in 5 minutes. Go and claim your reward now!", Color.White, Message.Talk));

            }
            if (((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 56) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 56)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be closed in 4 minutes. Go and claim your reward now!", Color.White, Message.Talk));

            }
            if (((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 57) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 57)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be closed in 3 minutes. Go and claim your reward now!", Color.White, Message.Talk));

            }
            if (((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 58) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 58)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be closed in 2 minutes. Go and claim your reward now!", Color.White, Message.Talk));

            }
            if (((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 59) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 59)) && DateTime.Now.Second == 1)
            {
                client.Send(new Message("Power Arena will be closed in 1 minutes. Go and claim your reward now!", Color.White, Message.Talk));

            }
            #endregion
            #region BroadCast
            if (DateTime.Now > Game.ConquerStructures.Broadcast.LastBroadcast.AddMinutes(2))
            {
                if (Game.ConquerStructures.Broadcast.Broadcasts.Count > 0)
                {
                    Game.ConquerStructures.Broadcast.CurrentBroadcast = Game.ConquerStructures.Broadcast.Broadcasts[0];
                    Game.ConquerStructures.Broadcast.Broadcasts.Remove(Game.ConquerStructures.Broadcast.CurrentBroadcast);
                    Game.ConquerStructures.Broadcast.LastBroadcast = DateTime.Now;
                    client.Send(new Network.GamePackets.Message(Game.ConquerStructures.Broadcast.CurrentBroadcast.Message, "ALLUSERS", Game.ConquerStructures.Broadcast.CurrentBroadcast.EntityName, System.Drawing.Color.Red, Network.GamePackets.Message.BroadcastMessage));
                }
                else
                    Game.ConquerStructures.Broadcast.CurrentBroadcast.EntityID = 1;
            }
            #endregion
            //#region CTF
            //if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            //{
            //    if (DateTime.Now.Hour == 20 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
            //    {
            //        if (!client.Entity.InJail())
            //        {
            //            Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
            //            {
            //                StrResID = 10535,
            //                Countdown = 60,
            //                Action = 1
            //            };
            //            client.Entity.StrResID = 10535;
            //            client.Send(alert.Encode());
            //        }
            //        if (client.Entity.GLCTF == 1)
            //        {
            //            client.Entity.GLCTF = 0;
            //        }
            //        using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
            //        {
            //            cmd.Update("entities").Set("GLCTF", 0).Execute();
            //        }

            //    }
            //}
            //if (CaptureTheFlag.IsWar)
            //{
            //    if (client.Entity.MapID == CaptureTheFlag.MapID)
            //    {
            //        CaptureTheFlag.SortScoresJoining(client, out client.Guild);
            //        CaptureTheFlag.SendScores();

            //    }
            //}
            //#endregion
            #region Activeness
            if (client.Activenes != null)
            {
                if (Time32.Now >= client.Activenes.HalfHourTask.AddMinutes(30))
                {
                    client.Activenes.HalfHourTask = Time32.Now;
                    client.Entity.HoursTimes++;
                    client.Activenes.SendSinglePacket(client, Activeness.Types.HoursTask, (byte)(client.Entity.HoursTimes));
                }
            }
            #endregion
            //#region Team Pk
            //if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && DateTime.Now.Hour == 18 && DateTime.Now.Minute == 55 && DateTime.Now.Second == 00)
            //{
            //    if (!client.Entity.InJail())
            //    {
            //        Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
            //        {
            //            StrResID = 10543,
            //            Countdown = 60,
            //            Action = 1
            //        };
            //        client.Entity.StrResID = 10543;
            //        client.Send(alert.Encode());
            //    }
            //}
            //#endregion
            //#region SkillTeamPk
            //if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && DateTime.Now.Hour == 19 && DateTime.Now.Minute == 40 && DateTime.Now.Second == 00)
            //{
            //    if (!client.Entity.InJail())
            //    {
            //        Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
            //        {
            //            StrResID = 10541,
            //            Countdown = 60,
            //            Action = 1
            //        };
            //        client.Entity.StrResID = 10541;
            //        client.Send(alert.Encode());
            //    }
            //}
            //#endregion
            #region Roullet PlayerTimer
            Map map = Kernel.Maps[2807];
            if (map != null)
            {
                foreach (Interfaces.IRoulette Table in map.Tables.Values)
                {
                    if (Table == null) return;
                    Roulette.RoulettePacket.GetTablePlayerNumber(Table);
                    if (Table.PlayerNumber > 0)
                    {
                        if (Table.Time.AddSeconds(Table.Stamp) < Time32.Now)
                        {
                            Table.Recored.Clear();
                            FastRandom Rand = new FastRandom();
                            byte num = (byte)Rand.Next(0, 38);
                            Table.LotteryNumber = num;
                            if (client.RouletteID == Table.UID || client.RouletteWatchID == Table.UID)
                            {
                                byte[] buffer = new byte[5 + 8];
                                Network.SafeWriter.Write(5, 0, buffer);
                                Network.SafeWriter.Write(2801, 2, buffer);
                                if (num == 38)
                                    num = 37;
                                Network.SafeWriter.Write((byte)num, 4, buffer);
                                client.Send(buffer);
                                client.RoulletWinnigAmount = 0;
                                if (client.RouletteWatchID == 0)
                                {
                                    foreach (var item in client.RoulleteBet)
                                    {
                                        if (item.BetAttribute.Values.Contains(num))
                                        {
                                            if (Table.StackType == 1)
                                            {
                                                client.RoulletWinnigAmount += item.BetAmount * item.BetAttribute.Profitability;
                                            }
                                            else if (Table.StackType == 2)
                                            {
                                                client.RoulletWinnigAmount += item.BetAmount * item.BetAttribute.Profitability;
                                            }
                                        }
                                    }
                                    Table.Recored.Add(client);
                                    if (Table.StackType == 1)
                                    {
                                        client.Entity.Money += client.RoulletWinnigAmount;
                                    }
                                    else if (Table.StackType == 2)
                                    {
                                        client.Entity.ConquerPoints += client.RoulletWinnigAmount;
                                    }
                                    client.RoulleteBet.Clear();
                                }

                            }
                            Table.Time = Time32.Now;
                            Table.Stamp = 35;
                        }
                    }
                }
            }
            #endregion
            #region Elite PK Tournament
            if (((DateTime.Now.Hour == ElitePK.EventTime) && DateTime.Now.Minute >= 55) && !ElitePKTournament.TimersRegistered)
            {
                ElitePKTournament.RegisterTimers();
                ElitePKBrackets brackets = new ElitePKBrackets(true, 0);
                brackets.Type = ElitePKBrackets.EPK_State;
                brackets.OnGoing = true;
                client.Send(brackets);
                if (!client.Entity.InJail())
                {
                    Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
                    {
                        StrResID = 10533,
                        Countdown = 60,
                        Action = 1
                    };
                    client.Entity.StrResID = 10533;
                    client.Send(alert.Encode());
                }
                #region RemoveTopElite
                var EliteChampion = Network.GamePackets.TitlePacket.Titles.ElitePKChamption_High;
                var EliteSecond = Network.GamePackets.TitlePacket.Titles.ElitePK2ndPlace_High;
                var EliteThird = Network.GamePackets.TitlePacket.Titles.ElitePK3ndPlace_High;
                var EliteEightChampion = Network.GamePackets.TitlePacket.Titles.ElitePKChamption_Low;
                var EliteEightSecond = Network.GamePackets.TitlePacket.Titles.ElitePK2ndPlace_Low;
                var EliteEightThird = Network.GamePackets.TitlePacket.Titles.ElitePK3ndPlace_Low;
                var EliteEight = Network.GamePackets.TitlePacket.Titles.ElitePKTopEight_Low;
                if (client.Entity.Titles.ContainsKey(EliteChampion))
                    client.Entity.RemoveTopStatus((ulong)EliteChampion);
                if (client.Entity.Titles.ContainsKey(EliteSecond))
                    client.Entity.RemoveTopStatus((ulong)EliteSecond);
                if (client.Entity.Titles.ContainsKey(EliteThird))
                    client.Entity.RemoveTopStatus((ulong)EliteThird);
                if (client.Entity.Titles.ContainsKey(EliteEightChampion))
                    client.Entity.RemoveTopStatus((ulong)EliteEightChampion);
                if (client.Entity.Titles.ContainsKey(EliteEightSecond))
                    client.Entity.RemoveTopStatus((ulong)EliteEightSecond);
                if (client.Entity.Titles.ContainsKey(EliteEightThird))
                    client.Entity.RemoveTopStatus((ulong)EliteEightThird);
                if (client.Entity.Titles.ContainsKey(EliteEight))
                    client.Entity.RemoveTopStatus((ulong)EliteEight);
                #endregion
            }
            if ((((DateTime.Now.Hour == ElitePK.EventTime + 1)) && DateTime.Now.Minute >= 10) && ElitePKTournament.TimersRegistered)
            {
                bool done = true;
                foreach (var epk in ElitePKTournament.Tournaments)
                    if (epk.Players.Count != 0)
                        done = false;
                if (done)
                {
                    ElitePKTournament.TimersRegistered = false;
                    ElitePKBrackets brackets = new ElitePKBrackets(true, 0);
                    brackets.Type = ElitePKBrackets.EPK_State;
                    brackets.OnGoing = false;
                    client.Send(brackets);
                }
            }
            #endregion
            #region FlameLit
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && DateTime.Now.Hour == 14 && DateTime.Now.Minute == 30 && DateTime.Now.Second == 00)
                client.Send(new Message("Let`s light up the flame to celebrate the Olympic Games! Find the Flame Taoist (353,325) to learn more!", Color.WhiteSmoke, 2007));
            #endregion
            #region MonthlyPk
            if (DateTime.Now.Day == 1 && DateTime.Now.Hour == 20 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
            {
                if (!client.Entity.InJail())
                {
                    Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
                    {
                        StrResID = 10523,
                        Countdown = 60,
                        Action = 1
                    };
                    client.Entity.StrResID = 10523;
                    client.Send(alert.Encode());
                    client.Send(new Message("It's time for Pk War. Go to talk to General Bravely in Twin City (324,194) before 20:19.", Color.Red, Message.TopLeft));
                }
            }
            #endregion
            #region BlackSpot
            if (Kernel.BlackSpoted.Values.Count > 0)
            {
                foreach (var spot in Kernel.BlackSpoted.Values)
                {
                    if (Time32.Now >= spot.BlackSpotStamp.AddSeconds(spot.BlackSpotStepSecs))
                    {
                        if (spot.Dead && spot.EntityFlag == EntityFlag.Player)
                        {
                            foreach (var h in Kernel.GamePool.Values)
                            {
                                Network.GamePackets.BlackSpotPacket BlackSpotPacket = new Network.GamePackets.BlackSpotPacket();
                                h.Send(BlackSpotPacket.ToArray(false, spot.UID));
                            }
                            Kernel.BlackSpoted.Remove(spot.UID);
                            continue;
                        }
                        foreach (var h in Kernel.GamePool.Values)
                        {
                            Network.GamePackets.BlackSpotPacket BlackSpotPacket = new Network.GamePackets.BlackSpotPacket();
                            h.Send(BlackSpotPacket.ToArray(false, spot.UID));
                        }
                        spot.IsBlackSpotted = false;
                        Kernel.BlackSpoted.Remove(spot.UID);
                    }
                }
            }
            #endregion
            #region Jiang
            if (client.Entity.MyJiang != null)
            {
                client.Entity.MyJiang.TheadTime(client);
            }
            #endregion
            #region Energy
            Game.ConquerStructures.Society.Energy.RechargeEnergy();
            #endregion
            #region Flags
            if (client.Entity.ContainsFlag3(Network.GamePackets.Update.Flags3.DivineGuard))
            {
                if (Time32.Now >= client.Entity.DivineGuardStamp.AddSeconds(10))
                {
                    client.Entity.RemoveFlag3(Network.GamePackets.Update.Flags3.DivineGuard);
                    client.LoadItemStats();
                }
            }
            if (client.Entity.ContainsFlag3(Network.GamePackets.Update.Flags3.ShieldBreak))
            {
                if (Time32.Now >= client.Entity.ShieldBreakStamp.AddSeconds(10))
                {
                    client.Entity.RemoveFlag3(Network.GamePackets.Update.Flags3.ShieldBreak);
                    client.LoadItemStats();
                }
            }
            if (client.Entity.HeavenBlessing > 0)
            {
                if (Now > client.LastTrainingPointsUp.AddMinutes(1))
                {
                    client.OnlineTrainingPoints += 3;
                    if (client.OnlineTrainingPoints >= 30)
                    {
                        client.OnlineTrainingPoints -= 30;
                        client.Entity.OnlineTraining += 2;
                        client.Entity.Update((byte)Update.OnlineTraining, OnlineTraining.ReceiveExperience, 0);
                    }
                    client.LastTrainingPointsUp = Now;
                    client.Entity.Update((byte)Update.OnlineTraining, OnlineTraining.IncreasePoints, 0);
                }
            }
            if (client.Entity.HeavenBlessing > 0)
            {
                if (Now > client.Entity.HeavenBlessingStamp.AddMilliseconds(1000))
                {
                    client.Entity.HeavenBlessingStamp = Now;
                    client.Entity.HeavenBlessing--;
                }
            }
            if (client.Entity.DoubleExperienceTime > 0)
            {
                if (Now > client.Entity.DoubleExpStamp.AddMilliseconds(1000))
                {
                    client.Entity.DoubleExpStamp = Now;
                    client.Entity.DoubleExperienceTime--;
                }
            }
            if (client.Entity.EnlightmentTime > 0)
            {
                if (Now >= client.Entity.EnlightmentStamp.AddMinutes(1))
                {
                    client.Entity.EnlightmentStamp = Now;
                    client.Entity.EnlightmentTime--;
                    if (client.Entity.EnlightmentTime % 10 == 0 && client.Entity.EnlightmentTime > 0)
                        client.IncreaseExperience(Game.Attacking.Calculate.Percent((int)client.ExpBall, .10F), false);
                }
            }
            if (Now >= client.Entity.PKPointDecreaseStamp.AddMinutes(5))
            {
                client.Entity.PKPointDecreaseStamp = Now;
                if (client.Entity.PKPoints > 0)
                {
                    client.Entity.PKPoints--;
                }
                else
                    client.Entity.PKPoints = 0;
            }
            if (client.Entity.Titles.Count > 0)
            {
                foreach (var titles in client.Entity.Titles)
                {
                    if (DateTime.Now > titles.Value)
                    {
                        client.Entity.Titles.Remove(titles.Key);
                        if (client.Entity.MyTitle == titles.Key)
                            client.Entity.MyTitle = Network.GamePackets.TitlePacket.Titles.None;

                        client.Entity.RemoveTopStatus((UInt64)titles.Key);
                    }
                }
            }
            if (!client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.XPList))
            {
                if (Now > client.XPCountStamp.AddSeconds(3))
                {
                    client.XPCountStamp = Now;
                    client.XPCount++;
                    if (client.XPCount >= 100)
                    {
                        client.Entity.AddFlag(Network.GamePackets.Update.Flags.XPList);
                        client.XPCount = 0;
                        client.XPListStamp = Now;
                    }
                }
            }
            else
            {
                if (Now > client.XPListStamp.AddSeconds(20))
                {
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.XPList);
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.FreezeSmall))
            {
                if (client.RaceFrightened)
                {
                    if (Time32.Now > client.FrightenStamp.AddSeconds(20))
                    {
                        client.RaceFrightened = false;
                        {
                            GameCharacterUpdates update = new GameCharacterUpdates(true);
                            update.UID = client.Entity.UID;
                            update.Remove(GameCharacterUpdates.Flustered);
                            client.SendScreen(update, true);
                        }
                        client.Entity.RemoveFlag((ulong)Update.Flags.FreezeSmall);
                    }
                    else
                    {
                        int rand;
                        ushort x, y;
                        do
                        {
                            rand = Kernel.Random.Next(Game.Map.XDir.Length);
                            x = (ushort)(client.Entity.X + Game.Map.XDir[rand]);
                            y = (ushort)(client.Entity.Y + Game.Map.YDir[rand]);
                        }
                        while (!client.Map.Floor[x, y, MapObjectType.Player]);
                        client.Entity.Facing = Kernel.GetAngle(client.Entity.X, client.Entity.Y, x, y);
                        client.Entity.X = x;
                        client.Entity.Y = y;
                        client.SendScreen(new TwoMovements()
                        {
                            EntityCount = 1,
                            Facing = client.Entity.Facing,
                            FirstEntity = client.Entity.UID,
                            WalkType = 9,
                            X = client.Entity.X,
                            Y = client.Entity.Y,
                            MovementType = TwoMovements.Walk
                        }, true);
                    }
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.Freeze))
            {
                if (Now > client.Entity.FrozenStamp.AddSeconds(client.Entity.FrozenTime))
                {
                    client.Entity.FrozenTime = 0;
                    client.Entity.RemoveFlag((ulong)Update.Flags.Freeze);
                    GameCharacterUpdates update = new GameCharacterUpdates(true);
                    update.UID = client.Entity.UID;
                    update.Remove(GameCharacterUpdates.Freeze);
                    client.SendScreen(update, true);
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.ChaosCycle))
            {
                if (Time32.Now > client.FrightenStamp.AddSeconds(5))
                {
                    client.RaceFrightened = false;
                    {
                        GameCharacterUpdates update = new GameCharacterUpdates(true);
                        update.UID = client.Entity.UID;
                        update.Remove(GameCharacterUpdates.Flustered);
                        client.SendScreen(update);
                    }
                    client.Entity.RemoveFlag((ulong)Update.Flags.ChaosCycle);
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.FreezeSmall))
            {
                if (Time32.Now > client.FrightenStamp.AddSeconds(client.Entity.Fright))
                {
                    GameCharacterUpdates update = new GameCharacterUpdates(true);
                    update.UID = client.Entity.UID;
                    update.Remove(GameCharacterUpdates.Dizzy);
                    client.SendScreen(update, true);
                    client.Entity.RemoveFlag((ulong)Update.Flags.FreezeSmall);
                }
                else
                {
                    int rand;
                    ushort x, y;
                    do
                    {
                        rand = Kernel.Random.Next(Game.Map.XDir.Length);
                        x = (ushort)(client.Entity.X + Game.Map.XDir[rand]);
                        y = (ushort)(client.Entity.Y + Game.Map.YDir[rand]);
                    }
                    while (!client.Map.Floor[x, y, MapObjectType.Player]);
                    client.Entity.Facing = Kernel.GetAngle(client.Entity.X, client.Entity.Y, x, y);
                    client.Entity.X = x;
                    client.Entity.Y = y;
                    client.SendScreen(new TwoMovements()
                    {
                        EntityCount = 1,
                        Facing = client.Entity.Facing,
                        FirstEntity = client.Entity.UID,
                        WalkType = 9,
                        X = client.Entity.X,
                        Y = client.Entity.Y,
                        MovementType = TwoMovements.Walk
                    }, true);
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.FlashingName))
            {
                if (DateTime.Now > client.Entity.FlashingNameStamp.AddSeconds(client.Entity.FlashingNameTime))
                {
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.FlashingName);
                }
            }
            if (client.Entity.Aura_isActive)
            {
                if (client.Entity.Aura_isActive)
                {
                    if (Time32.Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                    {
                        client.Entity.RemoveFlag2(client.Entity.Aura_actType);
                        client.removeAuraBonuses(client.Entity.Aura_actType, client.Entity.Aura_actPower, 1);
                        client.Entity.Aura_isActive = false;
                        client.Entity.AuraTime = 0;
                        client.Entity.Aura_actType = 0;
                        client.Entity.Aura_actPower = 0;
                    }
                }
            }
            if (client.Entity.OnKOSpell())
            {
                if (client.Entity.OnCyclone())
                {
                    int Seconds = Now.AllSeconds() - client.Entity.CycloneStamp.AddSeconds(client.Entity.CycloneTime).AllSeconds();
                    if (Seconds >= 1)
                    {
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Cyclone);
                    }
                }
                if (client.Entity.OnSuperman())
                {
                    int Seconds = Now.AllSeconds() - client.Entity.SupermanStamp.AddSeconds(client.Entity.SupermanTime).AllSeconds();
                    if (Seconds >= 1)
                    {
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Superman);
                    }
                }
                if (client.Entity.OnSuperCyclone())
                {
                    int Seconds = Now.AllSeconds() - client.Entity.SuperCycloneStamp.AddSeconds(client.Entity.SuperCycloneTime).AllSeconds();
                    if (Seconds >= 1)
                    {
                        client.Entity.RemoveFlag3(Network.GamePackets.Update.Flags3.SuperCyclone);
                    }
                }
                if (!client.Entity.OnKOSpell())
                {
                    client.Entity.KOCount = 0;
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Fly))
            {
                if (Now >= client.Entity.FlyStamp.AddSeconds(client.Entity.FlyTime))
                {
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Fly);
                    client.Entity.FlyTime = 0;
                }
            }
            if (client.Entity.NoDrugsTime > 0)
            {
                if (Now > client.Entity.NoDrugsStamp.AddSeconds(client.Entity.NoDrugsTime))
                {
                    client.Entity.NoDrugsTime = 0;
                }
            }
            if (client.Entity.ToxicFogLeft > 0)
            {
                if (Now >= client.Entity.ToxicFogStamp.AddSeconds(2))
                {
                    float Percent = client.Entity.ToxicFogPercent;
                    Percent = Percent / 100 * (client.Entity.Immunity / 100F);
                    //Remove this line if you want it normal
                    //Percent = Math.Min(0.1F, client.Entity.ToxicFogPercent);
                    client.Entity.ToxicFogLeft--;
                    if (client.Entity.ToxicFogLeft == 0)
                    {
                        client.Entity.RemoveFlag(Update.Flags.Poisoned);
                        return;
                    }
                    client.Entity.ToxicFogStamp = Now;
                    if (client.Entity.Hitpoints > 1)
                    {
                        uint damage = Game.Attacking.Calculate.Percent(client.Entity, Percent);
                        uint value = 100;
                        if (client.Equipment.TotalPerfectionLevel >= 1) value -= 30;
                        if (client.Equipment.TotalPerfectionLevel >= 45) value -= 5;
                        if (client.Equipment.TotalPerfectionLevel >= 85) value -= 5;
                        if (client.Equipment.TotalPerfectionLevel >= 110) value -= 5;
                        if (client.Equipment.TotalPerfectionLevel >= 145) value -= 5;
                        if (client.Equipment.TotalPerfectionLevel >= 185) value -= 10;
                        if (client.Equipment.TotalPerfectionLevel >= 200) value -= 10;
                        if (client.Equipment.TotalPerfectionLevel >= 230) value -= 10;
                        if (client.Equipment.TotalPerfectionLevel >= 260) value -= 10;
                        if (client.Equipment.TotalPerfectionLevel >= 300) value -= 10;
                        damage = (damage / 100) * value;
                        client.Entity.Hitpoints -= damage;
                        Network.GamePackets.SpellUse suse = new Network.GamePackets.SpellUse(true);
                        suse.Attacker = client.Entity.UID;
                        suse.SpellID = 10010;
                        suse.AddTarget(client.Entity, damage, null);
                        client.SendScreen(suse, true);

                        if (client != null)
                        {
                            client.UpdateQualifier(damage);

                        }

                    }
                }
            }
            else
            {
                if (client.Entity.ContainsFlag(Update.Flags.Poisoned))
                    client.Entity.RemoveFlag(Update.Flags.Poisoned);
            }
            if (client.Entity.OnFatalStrike())
            {
                if (Now > client.Entity.FatalStrikeStamp.AddSeconds(client.Entity.FatalStrikeTime))
                {
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.FatalStrike);
                }
            }
            if (client.Entity.OnOblivion())
            {
                if (Now > client.Entity.OblivionStamp.AddSeconds(client.Entity.OblivionTime))
                {
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.Oblivion);
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.ShurikenVortex))
            {
                if (Now > client.Entity.ShurikenVortexStamp.AddSeconds(client.Entity.ShurikenVortexTime))
                {
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.ShurikenVortex);
                }
            }
            if (client.Entity.Transformed)
            {
                if (Now > client.Entity.TransformationStamp.AddSeconds(client.Entity.TransformationTime))
                {
                    client.Entity.Untransform();
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.SoulShackle))
            {
                if (Now > client.Entity.ShackleStamp.AddSeconds(client.Entity.ShackleTime))
                {
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.SoulShackle);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.AzureShield))
            {
                if (Now > client.Entity.MagicShieldStamp.AddSeconds(client.Entity.MagicShieldTime))
                {
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.AzureShield);
                }
            }
            if (client.Entity.ContainsFlag3(Update.Flags3.BladeFlurry))
            {
                if (Time32.Now > client.Entity.BladeFlurryStamp.AddSeconds(45))
                {
                    client.Entity.RemoveFlag3(Update.Flags3.BladeFlurry);
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.GodlyShield) && client.Entity.MapID != 3846)
            {
                if (Time32.Now > client.GuardStamp.AddSeconds(10))
                {
                    client.RaceGuard = false;
                    {
                        GameCharacterUpdates update = new GameCharacterUpdates(true);
                        update.UID = client.Entity.UID;
                        update.Remove(GameCharacterUpdates.DivineShield);
                        client.SendScreen(update);
                    }
                    client.Entity.RemoveFlag((ulong)Update.Flags.GodlyShield);
                }
            }
            if (client.Entity.ContainsFlag3(Update.Flags3.SuperCyclone))
            {
                if (Time32.Now > client.Entity.SuperCycloneStamp.AddSeconds(40))
                {
                    client.Entity.RemoveFlag3(Update.Flags3.SuperCyclone);
                }
            }
            if (client.Entity.ContainsFlag(Update.Flags.DivineShield) && client.Entity.MapID == 1950)
            {
                if (Now > client.GuardStamp.AddSeconds(10))
                {
                    client.RaceGuard = false;
                    {
                        GameCharacterUpdates update = new GameCharacterUpdates(true);
                        update.UID = client.Entity.UID;
                        update.Remove(GameCharacterUpdates.DivineShield);
                        client.SendScreen(update);
                    }
                    client.Entity.RemoveFlag(Update.Flags.DivineShield);
                }
            }
            if (client.Entity.ContainsFlag(Update.Flags.OrangeSparkles) && !client.InQualifier() && client.Entity.MapID == 1950)
            {
                if (Time32.Now > client.RaceExcitementStamp.AddSeconds(15))
                {
                    var upd = new GameCharacterUpdates(true)
                    {
                        UID = client.Entity.UID
                    };
                    upd.Remove(GameCharacterUpdates.Accelerated);
                    client.SendScreen(upd);
                    client.SpeedChange = null;
                    client.Entity.RemoveFlag(Update.Flags.OrangeSparkles);
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.OrangeSparkles))
            {
                if (Time32.Now > client.RaceExcitementStamp.AddSeconds(15))
                {
                    var upd = new GameCharacterUpdates(true)
                    {
                        UID = client.Entity.UID
                    };
                    upd.Remove(GameCharacterUpdates.Accelerated);
                    client.SendScreen(upd);
                    client.SpeedChange = null;
                    client.Entity.RemoveFlag((ulong)Update.Flags.SpeedIncreased);
                    client.Entity.RemoveFlag((ulong)Update.Flags.OrangeSparkles);
                }
            }
            if (client.Entity.ContainsFlag((ulong)Update.Flags.PurpleSparkles))
            {
                if (Time32.Now > client.DecelerateStamp.AddSeconds(10))
                {
                    {
                        client.RaceDecelerated = false;
                        var upd = new GameCharacterUpdates(true)
                        {
                            UID = client.Entity.UID
                        };
                        upd.Remove(GameCharacterUpdates.Decelerated);
                        client.SendScreen(upd);
                        client.SpeedChange = null;
                    }
                    client.Entity.RemoveFlag((ulong)Update.Flags.PurpleSparkles);
                }
            }
            if (client.Entity.ContainsFlag(Update.Flags.PurpleSparkles) && !client.InQualifier())
            {
                if (Time32.Now > client.DecelerateStamp.AddSeconds(10))
                {
                    {
                        client.RaceDecelerated = false;
                        var upd = new GameCharacterUpdates(true)
                        {
                            UID = client.Entity.UID
                        };
                        upd.Remove(GameCharacterUpdates.Decelerated);
                        client.SendScreen(upd);
                        client.SpeedChange = null;
                    }
                    client.Entity.RemoveFlag(Update.Flags.PurpleSparkles);
                }
            }
            if (client.Entity.ContainsFlag(Update.Flags.Cursed))
            {
                if (Time32.Now > client.Entity.Cursed.AddSeconds(300))
                {
                    client.Entity.RemoveFlag(Update.Flags.Cursed);
                }
            }
            if (!client.TeamAura)
            {
                if (client.Team != null && !client.Entity.Dead && client.Team.Teammates != null)
                {
                    foreach (Client.GameClient pClient in client.Team.Teammates)
                    {
                        if (client.Entity.UID != pClient.Entity.UID && Kernel.GetDistance(client.Entity.X, client.Entity.Y, pClient.Entity.X, pClient.Entity.Y) <= Constants.pScreenDistance)
                        {
                            if (pClient.Entity.Aura_isActive && pClient.Socket.Alive && pClient.Entity.UID != client.Entity.UID && pClient.Entity.MapID == client.Entity.MapID)
                            {
                                if (pClient.Entity.Aura_actType == Update.Flags2.FendAura || pClient.Entity.Aura_actType == Update.Flags2.TyrantAura)
                                {
                                    client.TeamAura = true;
                                    client.TeamAuraOwner = pClient;
                                    client.TeamAuraStatusFlag = pClient.Entity.Aura_actType;
                                    client.TeamAuraPower = pClient.Entity.Aura_actPower;
                                    client.Entity.AddFlag2(client.TeamAuraStatusFlag);
                                    string type = "Critial Strikes";
                                    if (client.Entity.Aura_actType == 100) type = "Immunity";
                                    client.Send(new Message(type + " increased By " + client.TeamAuraPower + " percent!", System.Drawing.Color.Red, Message.Agate));
                                    client.doAuraBonuses(client.TeamAuraStatusFlag, client.TeamAuraPower, 1);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var pClient = client.TeamAuraOwner;
                string type = "Critial Strikes";
                if (client.Entity.Aura_actType == 100) type = "Immunity";
                if (pClient == null)
                {
                    client.TeamAura = false;
                    client.removeAuraBonuses(client.TeamAuraStatusFlag, client.TeamAuraPower, 1);
                    client.Entity.RemoveFlag2(client.TeamAuraStatusFlag);
                    client.Send(new Message(type + " decreased by " + client.TeamAuraPower + " percent!", System.Drawing.Color.Red, Message.Agate));
                    client.TeamAuraStatusFlag = 0;
                    client.TeamAuraPower = 0;
                }
                else
                {
                    if (!pClient.Entity.Aura_isActive || !pClient.Socket.Alive || pClient.Entity.Dead || pClient.Entity.MapID != client.Entity.MapID)
                    {
                        client.TeamAura = false;
                        client.removeAuraBonuses(client.TeamAuraStatusFlag, client.TeamAuraPower, 1);
                        client.Entity.RemoveFlag2(client.TeamAuraStatusFlag);
                        client.Send(new Message(type + " decreased by " + client.TeamAuraPower + " percent!", System.Drawing.Color.Red, Message.Agate));
                        client.TeamAuraStatusFlag = 0;
                        client.TeamAuraPower = 0;
                    }
                    else
                    {
                        if (client.Team == null || (pClient.Team == null || (pClient.Team != null && !pClient.Team.IsTeammate(client.Entity.UID))) || client.Entity.Dead || Kernel.GetDistance(client.Entity.X, client.Entity.Y, pClient.Entity.X, pClient.Entity.Y) > Constants.pScreenDistance)
                        {
                            client.TeamAura = false;
                            client.removeAuraBonuses(client.TeamAuraStatusFlag, client.TeamAuraPower, 1);
                            client.Entity.RemoveFlag2(client.TeamAuraStatusFlag);
                            client.Send(new Message(type + " decreased by " + client.TeamAuraPower + " percent!", System.Drawing.Color.Red, Message.Agate));
                            client.TeamAuraStatusFlag = 0;
                            client.TeamAuraPower = 0;
                        }
                    }
                }
            }
            if (client.Entity.ContainsFlag(Update.Flags2.Congelado))
            {
                if (DateTime.Now > client.Entity.CongeladoTimeStamp)
                {
                    client.Entity.RemoveFlag(Update.Flags2.Congelado);
                }
            }
            if (client.Entity.ContainsFlag(18014398509481984uL) && client.Entity.MapID == 1950 && client.RaceFrightened)
            {
                if (Time32.Now > client.FrightenStamp.AddSeconds(60))
                {
                    client.RaceFrightened = false;
                    GameCharacterUpdates gameCharacterUpdates = new GameCharacterUpdates(true);
                    gameCharacterUpdates.UID = client.Entity.UID;
                    gameCharacterUpdates.Remove(54u);
                    client.SendScreen(gameCharacterUpdates, true);
                    client.Entity.RemoveFlag(18014398509481984uL);
                }
            }
            if (client.Entity.ContainsFlag3(Update.Flags3.DragonCyclone))
            {
                if (Time32.Now > client.Entity.DragonCycloneStamp.AddSeconds(45))
                {
                    client.Entity.RemoveFlag3(Update.Flags3.DragonCyclone);
                }
            }
            if (client.Entity.ContainsFlag3(Update.Flags3.DragonFury))
            {
                if (Time32.Now > client.Entity.DragonFuryStamp.AddSeconds(client.Entity.DragonFuryTime))
                {
                    client.Entity.RemoveFlag3(Update.Flags3.DragonFury);

                    Network.GamePackets.Update upgrade = new Network.GamePackets.Update(true);
                    upgrade.UID = client.Entity.UID;
                    upgrade.Append(74
                        , 0
                        , 0, 0, 0);
                    client.Entity.Owner.Send(upgrade.Encode());
                }
            }
            if (client.Entity.ContainsFlag3(Update.Flags3.DragonFlow) && !client.Entity.ContainsFlag3(Update.Flags3.DragonCyclone))
            {
                if (Time32.Now > client.Entity.DragonFlowStamp.AddSeconds(8))
                {
                    if (client.Spells.ContainsKey(12270))
                    {
                        var spell = Database.SpellTable.GetSpell(client.Spells[12270].ID, client.Spells[12270].Level);
                        if (spell != null)
                        {
                            int stamina = 100;
                            if (client.Entity.HeavenBlessing > 0)
                                stamina += 50;
                            if (client.Entity.Stamina != stamina)
                            {
                                client.Entity.Stamina += (byte)spell.Power;
                                if (client.Entity.ContainsFlag3(Update.Flags3.DragonCyclone))
                                    if (client.Entity.Stamina != stamina)
                                        client.Entity.Stamina += (byte)spell.Power;
                                _String str = new _String(true);
                                str.UID = client.Entity.UID;
                                str.TextsCount = 1;
                                str.Type = _String.Effect;
                                str.Texts.Add("leedragonblood");
                                client.SendScreen(str, true);
                            }
                        }
                    }
                    client.Entity.DragonFlowStamp = Time32.Now;
                }
            }
            if (client.Entity.ContainsFlag3(Update.Flags3.DragonSwing))
            {
                if (Time32.Now > client.Entity.DragonSwingStamp.AddSeconds(160))
                {
                    client.Entity.RemoveFlag3(Update.Flags3.DragonSwing);
                    client.Entity.OnDragonSwing = false;
                    Update upgrade = new Update(true);
                    upgrade.UID = client.Entity.UID;
                    upgrade.Append(Update.DragonSwing, 0, 0, 0, 0);
                    client.Entity.Owner.Send(upgrade.Encode());
                }
            }
            if (client.Entity.AutoRev > 0)
            {
                if (client.Entity.HeavenBlessing > 0)
                {
                    if (Time32.Now >= client.Entity.AutoRevStamp.AddSeconds(client.Entity.AutoRev))
                    {
                        client.Entity.Action = Game.Enums.ConquerAction.None;
                        client.ReviveStamp = Time32.Now;
                        client.Attackable = false;
                        client.Entity.TransformationID = 0;
                        client.Entity.RemoveFlag(Update.Flags.Dead);
                        client.Entity.RemoveFlag(Update.Flags.Ghost);
                        client.Entity.Hitpoints = client.Entity.MaxHitpoints;
                        client.Entity.Mana = client.Entity.MaxMana;
                        client.Entity.AutoRev = 0;
                        AutoHunt AutoHunt = new AutoHunt();
                        AutoHunt.Action = AutoHunt.Mode.Start;
                        client.Entity.InAutoHunt = true;
                        // PacketHandler.HandlePacket(AutoHunt.Encode(), client);  

                    }
                }
                else
                {
                    client.Entity.RemoveFlag(Update.Flags3.AutoHunting);
                    client.Entity.InAutoHunt = false;
                }
            }
            if (client.Entity.Hitpoints == 0 && client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Dead) && !client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Ghost))
            {
                if (Now > client.Entity.DeathStamp.AddSeconds(2))
                {
                    client.Entity.AddFlag(Network.GamePackets.Update.Flags.Ghost);
                    if (client.Entity.Body % 10 < 3)
                        client.Entity.TransformationID = 99;
                    else
                        client.Entity.TransformationID = 98;

                    client.SendScreenSpawn(client.Entity, true);
                }
            }
            if (client.Entity.ContainsFlag2(Update.Flags2.ChainBoltActive))
                if (Now > client.Entity.ChainboltStamp.AddSeconds(client.Entity.ChainboltTime))
                    client.Entity.RemoveFlag2(Update.Flags2.ChainBoltActive);
            if (client.Entity.HasMagicDefender && Now >= client.Entity.MagicDefenderStamp.AddSeconds(client.Entity.MagicDefenderSecs))
            {
                client.Entity.RemoveMagicDefender();
            }
            if (Now >= client.Entity.BlackbeardsRageStamp.AddSeconds(60))
            {
                client.Entity.RemoveFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.BlackbeardsRage);
            }
            if (Now >= client.Entity.CannonBarrageStamp.AddSeconds(60))
            {
                client.Entity.RemoveFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.CannonBarrage);
            }
            if (Now >= client.Entity.SuperCycloneStamp.AddSeconds(40))
            {
                client.Entity.RemoveFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.SuperCyclone);
            }
            if (Now >= client.Entity.FatigueStamp.AddSeconds(client.Entity.FatigueSecs))
            {
                client.Entity.RemoveFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.Fatigue);
                client.Entity.IsDefensiveStance = false;
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.TyrantAura) && !client.TeamAura)
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.TyrantAura);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.FendAura) && !client.TeamAura)
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.FendAura);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.MetalAura))
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.MetalAura);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.WoodAura))
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.WoodAura);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.WaterAura))
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.WaterAura);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.EarthAura))
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.EarthAura);
                }
            }
            if (client.Entity.ContainsFlag2(Network.GamePackets.Update.Flags2.FireAura))
            {
                if (Now >= client.Entity.AuraStamp.AddSeconds(client.Entity.AuraTime))
                {
                    client.Entity.AuraTime = 0;
                    client.Entity.Aura_isActive = false;
                    //client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag2(Network.GamePackets.Update.Flags2.FireAura);
                }
            }

            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Stigma))
            {
                if (Now >= client.Entity.StigmaStamp.AddSeconds(client.Entity.StigmaTime))
                {
                    client.Entity.StigmaTime = 0;
                    client.Entity.StigmaIncrease = 0;
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Stigma);
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Dodge))
            {
                if (Now >= client.Entity.DodgeStamp.AddSeconds(client.Entity.DodgeTime))
                {
                    client.Entity.DodgeTime = 0;
                    client.Entity.DodgeIncrease = 0;
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Dodge);
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Invisibility))
            {
                if (Now >= client.Entity.InvisibilityStamp.AddSeconds(client.Entity.InvisibilityTime))
                {
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Invisibility);
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.StarOfAccuracy))
            {
                if (client.Entity.StarOfAccuracyTime != 0)
                {
                    if (Now >= client.Entity.StarOfAccuracyStamp.AddSeconds(client.Entity.StarOfAccuracyTime))
                    {
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.StarOfAccuracy);
                    }
                }
                else
                {
                    if (Now >= client.Entity.AccuracyStamp.AddSeconds(client.Entity.AccuracyTime))
                    {
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.StarOfAccuracy);
                    }
                }
            }
            if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.MagicShield))
            {
                if (client.Entity.MagicShieldTime != 0)
                {
                    if (Now >= client.Entity.MagicShieldStamp.AddSeconds(client.Entity.MagicShieldTime))
                    {
                        client.Entity.MagicShieldIncrease = 0;
                        client.Entity.MagicShieldTime = 0;
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.MagicShield);
                    }
                }
                else
                {
                    if (Now >= client.Entity.ShieldStamp.AddSeconds(client.Entity.ShieldTime))
                    {
                        client.Entity.ShieldIncrease = 0;
                        client.Entity.ShieldTime = 0;
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.MagicShield);
                    }
                }
            }
            if (client.Entity.EnlightenPoints >= 0.9)
            {
                client.Entity.Update((byte)Update.EnlightPoints, client.Entity.EnlightenPoints, true);
            }
            else if ((client.Entity.EnlightenPoints < 1.0) && client.Entity.ContainsFlag((byte)Update.EnlightPoints))
            {
                client.Entity.RemoveFlag((byte)Update.EnlightPoints);
            }
            if (client.Entity.ContainsFlag(Update.Flags.CastPray))
            {
                if (client.BlessTime <= 7198500)
                    client.BlessTime += 1000;
                else
                    client.BlessTime = 7200000;
                client.Entity.Update(Update.LuckyTimeTimer, client.BlessTime, false);
            }
            if (client.Entity.ContainsFlag(Update.Flags.Praying))
            {
                if (client.PrayLead != null)
                {
                    if (client.PrayLead.Socket.Alive)
                    {
                        if (client.BlessTime <= 7199000)
                            client.BlessTime += 500;
                        else
                            client.BlessTime = 7200000;
                        client.Entity.Update(Update.LuckyTimeTimer, client.BlessTime, false);
                    }
                    else
                        client.Entity.RemoveFlag(Update.Flags.Praying);
                }
                else
                    client.Entity.RemoveFlag(Update.Flags.Praying);
            }
            if (!client.Entity.ContainsFlag(Update.Flags.Praying) && !client.Entity.ContainsFlag(Update.Flags.CastPray))
            {
                if (client.BlessTime > 0)
                {
                    if (client.BlessTime >= 500)
                        client.BlessTime -= 500;
                    else
                        client.BlessTime = 0;
                    client.Entity.Update(Update.LuckyTimeTimer, client.BlessTime, false);
                }
            }
            #endregion
            #region Mentor
            client.ReviewMentor();
            #endregion
            #region XpBlueStamp
            if (client.Entity.ContainsFlag3(Update.Flags3.WarriorEpicShield))
            {
                if (Time32.Now > client.Entity.XpBlueStamp.AddSeconds(33))
                {
                    client.Entity.ShieldIncrease = 0;
                    client.Entity.ShieldTime = 0;
                    client.Entity.MagicShieldIncrease = 0;
                    client.Entity.MagicShieldTime = 0;
                    client.Entity.RemoveFlag3(Update.Flags3.WarriorEpicShield);
                }
            }
            #endregion
            #region ManiacDance
            if (client.Entity.ContainsFlag3((ulong)1UL << 53))
            {
                if (Time32.Now > client.Entity.ManiacDance.AddSeconds(15))
                {
                    client.Entity.RemoveFlag3((ulong)1UL << 53);
                }
            }
            #endregion
            #region Backfire
            if (client.Entity.ContainsFlag3((ulong)1UL << 51))
            {
                if (Time32.Now > client.Entity.BackfireStamp.AddSeconds(8))
                {
                    if (client.Spells.ContainsKey(12680))
                    {
                        if (client.Entity.ContainsFlag3((ulong)1UL << 51))
                            client.Entity.RemoveFlag3((ulong)1UL << 51);
                    }
                    client.Entity.BackfireStamp = Time32.Now;
                }
            }
            #endregion
            //#region Bp Checks
            //if (client.Account.State != AccountTable.AccountState.GameMaster)
            //{
            //    if (client.Entity.NobilityRank == NobilityRank.King && client.Entity.BattlePower > 405)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.BattlePower > 405)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.NobilityRank == NobilityRank.Prince && client.Entity.BattlePower > 402)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.NobilityRank == NobilityRank.Duke && client.Entity.BattlePower > 400)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.NobilityRank == NobilityRank.Earl && client.Entity.BattlePower > 398)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.NobilityRank == NobilityRank.Knight && client.Entity.BattlePower > 394)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.NobilityRank == NobilityRank.Baron && client.Entity.BattlePower > 396)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //    if (client.Entity.NobilityRank == NobilityRank.Serf && client.Entity.BattlePower > 390)
            //    {
            //        client.Account.Cheat();
            //        client.Account.State = Database.AccountTable.AccountState.Cheated;
            //        client.Disconnect();
            //    }
            //}

            //#endregion
            //#region GMRobe Checker
            //if (client.Account.State == 0 && client.Inventory.Contains(137010, 1) || client.Account.State == 0 && client.Equipment.IsHavingGMRobe())
            //{
            //    client.Equipment.Remove(1);
            //    client.Equipment.Remove(2);
            //    client.Equipment.Remove(3);
            //    client.Equipment.Remove(4);
            //    client.Equipment.Remove(5);
            //    client.Equipment.Remove(6);
            //    client.Equipment.Remove(7);
            //    client.Equipment.Remove(8);
            //    client.Equipment.Remove(9);
            //    client.Equipment.Remove(10);
            //    client.Equipment.Remove(11);
            //    client.Equipment.Remove(12);
            //    client.Equipment.Remove(18);
            //    client.Equipment.Remove(19);
            //    ConquerItem[] inventory = new ConquerItem[client.Inventory.Objects.Length];
            //    client.Inventory.Objects.CopyTo(inventory, 0);
            //    foreach (ConquerItem item in inventory)
            //    {
            //        client.Inventory.Remove(item, Nyx.Server.Game.Enums.ItemUse.Remove);
            //    }
            //    client.Account.Cheat();
            //    client.Account.State = Database.AccountTable.AccountState.Cheated;
            //    client.Disconnect();
            //}
            //#endregion
            #region Gambling
            if (client.Entity.Gambleing != null)
            {
                if (DateTime.Now >= client.Entity.Gambleing.StartTime.AddSeconds(50))
                {
                    var Random = new Random();
                    client.Entity.Gambleing.Seconds = 0;
                    client.Entity.Gambleing.Type = Gambleing.Gambl.EndGamble;
                    client.Send(client.Entity.Gambleing);
                    client.Entity.Gambleing.Seconds = 1;
                    client.Entity.Gambleing.Type = Gambleing.Gambl.ResultGamble;
                    client.Entity.Gambleing.Dice1 = (byte)Random.Next(1, 7);
                    client.Entity.Gambleing.Dice2 = (byte)Random.Next(1, 7);
                    client.Entity.Gambleing.Dice3 = (byte)Random.Next(1, 7);
                    client.Entity.Gambleing.Unknowen = (byte)Random.Next(1, 7);
                    client.Send(client.Entity.Gambleing);
                    byte sum = 0;
                    sum += client.Entity.Gambleing.Dice1;
                    sum += client.Entity.Gambleing.Dice2;
                    sum += client.Entity.Gambleing.Dice3;
                    if (sum <= 10)
                    {
                        if (client.Entity.Gambleing.Bet.ContainsKey(0))
                        {
                            client.Entity.Money += client.Entity.Gambleing.Bet[0].Amount *
                                                   client.Entity.Gambleing.Bet[0].Precent;
                        }
                    }
                    else
                    {
                        if (client.Entity.Gambleing.Bet.ContainsKey(1))
                        {
                            client.Entity.Money += client.Entity.Gambleing.Bet[1].Amount *
                                                   client.Entity.Gambleing.Bet[1].Precent;
                        }
                    }
                    if (client.Entity.Gambleing.Bet.ContainsKey(sum))
                    {
                        client.Entity.Money += client.Entity.Gambleing.Bet[sum].Amount *
                                               client.Entity.Gambleing.Bet[sum].Precent;
                    }
                    client.Entity.Gambleing.Seconds = 50;
                    client.Entity.Gambleing.StartTime = DateTime.Now;
                    client.Entity.Gambleing.Type = Gambleing.Gambl.BeginGamble;
                    client.Entity.Gambleing.Dice1 = 0;
                    client.Entity.Gambleing.Dice2 = 0;
                    client.Entity.Gambleing.Dice3 = 0;
                    client.Send(client.Entity.Gambleing);
                }
            }
            #endregion
            #region Minning
            if (client.Mining && !client.Entity.Dead)
            {
                if (Now >= client.MiningStamp.AddSeconds(2))
                {
                    client.MiningStamp = Now;
                    Game.ConquerStructures.Mining.Mine(client);
                }
            }
            #endregion
            #region Attackable
            if (client.JustLoggedOn)
            {
                client.JustLoggedOn = false;
                client.ReviveStamp = Now;
            }
            if (!client.Attackable)
            {
                if (Now > client.ReviveStamp.AddSeconds(5))
                {
                    client.Attackable = true;
                }
            }
            #endregion
            //#region OverHP
            //if (client.Entity.FullyLoaded)
            //{
            //    if (client.Entity.Hitpoints > client.Entity.MaxHitpoints && client.Entity.MaxHitpoints > 1 && !client.Entity.Transformed)
            //    {
            //        client.Entity.Hitpoints = client.Entity.MaxHitpoints;
            //    }
            //}
            //#endregion
            #region Auto Restore HP
            if (DateTime.Now.Second == 00 || DateTime.Now.Second == 10 || DateTime.Now.Second == 20 || DateTime.Now.Second == 30 || DateTime.Now.Second == 40 || DateTime.Now.Second == 50)
            {
                if (client.Entity.Hitpoints < client.Entity.MaxHitpoints && !Constants.PKFreeMaps.Contains(client.Entity.MapID) && !client.Entity.Dead && client.Entity.MaxHitpoints > 1)
                {
                    client.Entity.Hitpoints += (uint)Math.Min((uint)(client.Entity.MaxHitpoints - client.Entity.Hitpoints), (uint)6);
                }
            }
            #endregion
            #region TreasureInTheBlue
            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                if (!Kernel.TreasureInTheBlue)
                {
                    if ((DateTime.Now.Hour == 12 && DateTime.Now.Minute >= 30 || DateTime.Now.Hour == 13 && DateTime.Now.Minute < 30) || (DateTime.Now.Hour == 20 && DateTime.Now.Minute >= 30 || DateTime.Now.Hour == 21 && DateTime.Now.Minute < 29) && DateTime.Now.Second == 0)
                    {
                        if (!client.Entity.InJail())
                        {
                            Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
                            {
                                StrResID = 10552,
                                Countdown = 60,
                                Action = 1
                            };
                            client.Entity.StrResID = 10552;
                            client.Send(alert.Encode());
                            Kernel.TreasureInTheBlue = true;
                        }
                    }
                }
                if (Kernel.TreasureInTheBlue && ((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 19) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 19)) && DateTime.Now.Second == 0)
                {
                    client.Send(new Message("The ship of `Treasure In The Blue` event will return home after 10 minutes. Hurry to exchange your coins!", Color.Red, Message.TopLeft));
                }
                if (Kernel.TreasureInTheBlue && ((DateTime.Now.Hour == 13 && DateTime.Now.Minute == 29) || (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 29)) && DateTime.Now.Second == 0)
                {
                    Kernel.TreasureInTheBlue = false;
                    client.Send(new Message("This round of 'Treasure In The Blue' event has ended. Let's look forward to the next round!", Color.White, Message.Talk));
                    if (client.Entity.MapID == 3071 || client.Entity.MapID == 1068)
                    {
                        client.Entity.Teleport(1002, 300, 278);
                    }
                }
            }
            #endregion
            #region GuildRequest
            if (Now > client.Entity.GuildRequest.AddSeconds(30))
            {
                client.GuildJoinTarget = 0;
            }
            #endregion
            #region ClassPk
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour == 19 && DateTime.Now.Minute == 30 && DateTime.Now.Second <= 10)
            {
                if (!client.Entity.InJail())
                {
                    Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
                    {
                        StrResID = 10519,
                        Countdown = 60,
                        Action = 1
                    };
                    client.Entity.StrResID = 10519;
                    client.Send(alert.Encode());
                }
            }
            #endregion
            #region QuizShow
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 13 || DateTime.Now.Hour == 20)
                {
                    if (DateTime.Now.Minute == 55 && DateTime.Now.Second == 00)
                    {
                        if (!client.Entity.InJail())
                            client.Send(new Message("Quiz show will start in 5 minutes!", System.Drawing.Color.White, Message.TopLeft));
                    }
                }
                if (DateTime.Now.Hour == 4 || DateTime.Now.Hour == 14 || DateTime.Now.Hour == 21)
                {
                    if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
                    {
                        if (!client.Entity.InJail())
                        {

                            client.Send(new Message("Quiz show has started!", System.Drawing.Color.White, Message.TopLeft));
                        }
                    }
                }
            }
            #endregion
            #region QuizShow
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday || DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 13 || DateTime.Now.Hour == 20)
                {
                    if (DateTime.Now.Minute == 55 && DateTime.Now.Second == 00)
                    {
                        if (!client.Entity.InJail())
                            client.Send(new Message("Quiz show will start in 5 minutes!", System.Drawing.Color.White, Message.TopLeft));
                    }
                }
                if (DateTime.Now.Hour == 4 || DateTime.Now.Hour == 14 || DateTime.Now.Hour == 21)
                {
                    if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
                    {
                        if (!client.Entity.InJail())
                        {

                            client.Send(new Message("Quiz show has started!", System.Drawing.Color.White, Message.TopLeft));
                        }
                    }
                }
            }
            #endregion
            #region Life~Pk//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 13 && DateTime.Now.Second <= 02)
            {

                client.Send(new Message("Life~Pk event began!!", System.Drawing.Color.White, Message.Center));
                client.MessageBox("Life~PK Start Wanna Join?",
                      (p) => { p.Entity.Teleport(10002, 309, 266); }, null, 60);
            }
            #endregion
            #region GentleWar//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 18 && DateTime.Now.Second <= 02)
            {

                client.Send(new Message("GentleWar event began!!!", System.Drawing.Color.White, Message.Center));
                client.MessageBox("GentleWar Start Wanna Join?",
                      (p) => { p.Entity.Teleport(10002, 313, 266); }, null, 60);
            }
            #endregion
            #region ButchersWar//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 45 && DateTime.Now.Second <= 02)
            {

                client.Send(new Message("ButchersWar event began!!", System.Drawing.Color.White, Message.Center));
                client.MessageBox("ButchersWar Start Wanna Join?",
                      (p) => { p.Entity.Teleport(10002, 317, 266); }, null, 60);
            }
            #endregion
            #region CrazyWar//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 35 && DateTime.Now.Second <= 02)
            {
                client.Send(new Message("CrazyWar event began", System.Drawing.Color.White, Message.Center));
                client.MessageBox("CrazyWar Start Wanna Join?",
                      (p) => { p.Entity.Teleport(10002, 315, 266); }, null, 60);
            }
            #endregion
            #region BigBoss
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 54 && DateTime.Now.Second == 00)
            {//Nyx.Server
                client.Send(new Message("BigBoss event began", System.Drawing.Color.White, Message.Center));
                client.MessageBox("BigBoss Start Wanna Join?",
                      (p) => { p.Entity.Teleport(10002, 311, 266); }, null, 60);
            }
            #endregion
            #region OneHit//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            {
                if (DateTime.Now.Minute == 40 && DateTime.Now.Second == 02)
                {
                    client.Send(new Message("OneHit event began", System.Drawing.Color.White, Message.Center));
                    client.MessageBox("OneHit Has Started! Would you like to join?",
                          (p) => { p.Entity.Teleport(10002, 319, 266); }, null, 60);//Nyx.Server
                }
            }
            #endregion  //Nyx.Server
            #region WeeklyPk
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && DateTime.Now.Hour == 20 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
            {
                if (!client.Entity.InJail())
                {
                    Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
                    {
                        StrResID = 10521,
                        Countdown = 60,
                        Action = 1
                    };
                    client.Entity.StrResID = 10521;
                    client.Send(alert.Encode());
                    client.Send(new Message("It's time for Pk War. Go to talk to General Bravely in Twin City (324,194) before 20:19.", Color.Red, Message.TopLeft));
                }
            }
            #endregion
            #region [Top] Conquer//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 01 && DateTime.Now.Second <= 02)
            {
                Kernel.SendWorldMessage(new Message(" the [Top] Conquer Pk War its began.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    GameClient.MessageBox("[Top] Conquer Start Wanna Join?",
                          (p) => { p.Entity.Teleport(10002, 309, 289); }, null, 60);
            }
            #endregion
            #region BlackName PK//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 38 && DateTime.Now.Second <= 01)
            {
                Kernel.SendWorldMessage(new Message(" the [Top] BlackName PK War its began.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    GameClient.MessageBox("BlackName PK Start Wanna Join?",
                          (p) => { p.Entity.Teleport(10002, 311, 289); }, null, 60);
            }
            #endregion
            #region ChampionsPkWar //Nyx.Server
            if (DateTime.Now.Minute == 5 && DateTime.Now.Second == 10)
            {
                Kernel.SendWorldMessage(new Message(" the [Top] Champions PK War its began.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    GameClient.MessageBox("ChampionsPkWar Quest began! Would you like to join ?",
                   (p) => { p.Entity.Teleport(10002, 313, 289); }, null, 60);
            }
            #endregion
            #region RedName//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 13 && DateTime.Now.Second <= 01)
            {
                Kernel.SendWorldMessage(new Message(" the [Top] RedName PK War its began.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    GameClient.MessageBox("RedName Start Wanna Join?",
                         (p) => { p.Entity.Teleport(10002, 315, 289); }, null, 60);
            }
            #endregion
            #region DeadWorld//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 18 && DateTime.Now.Second <= 01)
            {
                Kernel.SendWorldMessage(new Message(" the [Top] DeadWorld PK War its began.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    GameClient.MessageBox("DeadWorld Start Wanna Join?",
                          (p) => { p.Entity.Teleport(10002, 317, 289); }, null, 60);
            }
            #endregion
            #region Revenger//Nyx.Server
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if (DateTime.Now.Minute == 25 && DateTime.Now.Second <= 02)
            {
                Kernel.SendWorldMessage(new Message(" the [Top] Revenger PK War its began.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    GameClient.MessageBox("Revenger Start Wanna Join?",
                         (p) => { p.Entity.Teleport(10002, 319, 289); }, null, 60);
            }
            #endregion
            #region InnerPwoer [06]
            if (DateTime.Now.Minute == 06 && DateTime.Now.Second == 2)
            {
                Kernel.SendWorldMessage(new Message("InnerPowerQuest War began [Prize: [500] Point(InnerPower)] ?!", Color.White, Message.TopLeft), Program.GamePool);
                foreach (Client.GameClient GameClient in Kernel.GamePool.Values)
                    client.MessageBox("InnerPowerQuest began! Would you like to join [Prize: [500] Point(InnerPower)]?",
                    p => { p.Entity.Teleport(1002, 296, 359); }, null, 60);
            }
            #endregion
            #region Lucky Quest
            if (client.Entity.MapID == 6000 || client.Entity.MapID == 6001 || client.Entity.MapID == 102 || client.Entity.MapID == 6002 || client.Entity.MapID == 6003 || client.Entity.MapID == 6004)
                return;
            if ((DateTime.Now.Minute == 00) && (DateTime.Now.Second == 10))
            {
                Kernel.SendWorldMessage(new Message("Quest Lucky Box Start and finsh 00:02 go all Twin City to get Some Gifet", System.Drawing.Color.Yellow, Network.GamePackets.Message.Guild), Program.Values);
                client.MessageBox("Lucky Quest Has Start! Would you like to go? go all Twin City to get Some Gifet",
                 p => { p.Entity.Teleport(3339, 43, 50); }, null, 60);
            }

            #endregion
            #region Quest HealTh Wine
            if ((DateTime.Now.Minute == 30) && (DateTime.Now.Second == 00))
            {
                client.MessageBox("Quest Monster Healthwine began! Would you like to join For get 5000 Cps",
                    (p) => { p.Entity.Teleport(1000, 294, 460); }, null, 60);
            }
            #endregion  
            


        }
        private void AutoAttackCallback(GameClient client, int time)
        {
            if (!Valid(client)) return;
            Time32 Now = new Time32(time);
            #region ShurikenVortex
            if (client.Entity.AttackPacket != null || client.Entity.VortexAttackStamp != null)
            {
                try
                {
                    if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.ShurikenVortex))
                    {
                        if (client.Entity.VortexPacket != null && client.Entity.VortexPacket.Encode() != null)
                        {
                            if (Now > client.Entity.VortexAttackStamp.AddMilliseconds(1000))
                            {
                                client.Entity.VortexAttackStamp = Now;
                                new Game.Attacking.Handle(client.Entity.VortexPacket, client.Entity, null);
                            }
                        }
                    }
                    else
                    {
                        client.Entity.VortexPacket = null;
                        var AttackPacket = client.Entity.AttackPacket;
                        if (AttackPacket != null && AttackPacket.Encode() != null)
                        {
                            uint AttackType = AttackPacket.AttackType;
                            if (AttackType == Network.GamePackets.Attack.Melee ||
                                AttackType == Network.GamePackets.Attack.Ranged ||
                                AttackType == Network.GamePackets.Attack.Magic)
                            {
                                if (AttackType == Network.GamePackets.Attack.Magic)
                                {
                                    if (Now > client.Entity.AttackStamp.AddSeconds(1))
                                    {
                                        if (AttackPacket.Damage != 12160 &&
                                            AttackPacket.Damage != 12170 &&
                                            AttackPacket.Damage != 12120 &&
                                            AttackPacket.Damage != 12130 &&
                                            AttackPacket.Damage != 12140 &&
                                            AttackPacket.Damage != 12320 &&
                                            AttackPacket.Damage != 12330 &&
                                            AttackPacket.Damage != 12340 &&
                                            AttackPacket.Damage != 12210)
                                        {
                                            new Game.Attacking.Handle(AttackPacket, client.Entity, null);
                                        }
                                    }
                                }
                                else
                                {

                                    int decrease = 0;
                                    if (client.Entity.OnCyclone())
                                        decrease = 700;
                                    if (client.Entity.OnSuperman())
                                        decrease = 200;
                                    if (Now >
                                        client.Entity.AttackStamp.AddMilliseconds((1000 - client.Entity.Agility - decrease) * (int)(AttackType ==
                                                                                       Network.GamePackets.Attack.Ranged
                                                                                          ? 1
                                                                                          : 1)))
                                    {
                                        new Game.Attacking.Handle(AttackPacket, client.Entity, null);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    client.Entity.AttackPacket = null;
                    client.Entity.VortexPacket = null;
                }
            }
            #endregion
        }
        private void PrayerCallback(GameClient client, int time)
        {
            if (!Valid(client)) return;
            Time32 Now = new Time32(time);

            if (client.Entity.Reborn > 1)
                return;

            if (!client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Praying))
            {
                foreach (Interfaces.IMapObject ClientObj in client.Screen.Objects)
                {
                    if (ClientObj != null)
                    {
                        if (ClientObj.MapObjType == Game.MapObjectType.Player)
                        {
                            var Client = ClientObj.Owner;
                            if (Client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.CastPray))
                            {
                                if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, ClientObj.X, ClientObj.Y) <= 3)
                                {
                                    client.Entity.AddFlag(Network.GamePackets.Update.Flags.Praying);
                                    client.PrayLead = Client;
                                    client.Entity.Action = Client.Entity.Action;
                                    Client.Prayers.Add(client);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (client.PrayLead != null)
                {
                    if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, client.PrayLead.Entity.X, client.PrayLead.Entity.Y) > 4)
                    {
                        client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Praying);
                        client.PrayLead.Prayers.Remove(client);
                        client.PrayLead = null;
                    }
                }
            }
        }
        private void CompanionsCallback(GameClient client, int time)
        {
            if (!Valid(client)) return;
            Time32 Now = new Time32(time);

            if (client.Companion != null)
            {
                short distance = Kernel.GetDistance(client.Companion.X, client.Companion.Y, client.Entity.X, client.Entity.Y);
                if (distance >= 8)
                {
                    ushort X = (ushort)(client.Entity.X + Kernel.Random.Next(2));
                    ushort Y = (ushort)(client.Entity.Y + Kernel.Random.Next(2));
                    if (!client.Map.SelectCoordonates(ref X, ref Y))
                    {
                        X = client.Entity.X;
                        Y = client.Entity.Y;
                    }
                    client.Companion.X = X;
                    client.Companion.Y = Y;
                    Network.GamePackets.Data data = new Nyx.Server.Network.GamePackets.Data(true);
                    data.ID = Network.GamePackets.Data.Jump;
                    data.dwParam = (uint)((Y << 16) | X);
                    data.wParam1 = X;
                    data.wParam2 = Y;
                    data.UID = client.Companion.UID;
                    client.Companion.MonsterInfo.SendScreen(data);
                }
                else if (distance > 4)
                {
                    Enums.ConquerAngle facing = Kernel.GetAngle(client.Companion.X, client.Companion.Y, client.Companion.Owner.Entity.X, client.Companion.Owner.Entity.Y);
                    if (!client.Companion.Move(facing))
                    {
                        facing = (Enums.ConquerAngle)Kernel.Random.Next(7);
                        if (client.Companion.Move(facing))
                        {
                            client.Companion.Facing = facing;
                            Network.GamePackets.GroundMovement move = new Nyx.Server.Network.GamePackets.GroundMovement(true);
                            move.Direction = facing;
                            move.UID = client.Companion.UID;
                            move.GroundMovementType = Network.GamePackets.GroundMovement.Run;
                            client.Companion.MonsterInfo.SendScreen(move);
                        }
                    }
                    else
                    {
                        client.Companion.Facing = facing;
                        Network.GamePackets.GroundMovement move = new Nyx.Server.Network.GamePackets.GroundMovement(true);
                        move.Direction = facing;
                        move.UID = client.Companion.UID;
                        move.GroundMovementType = Network.GamePackets.GroundMovement.Run;
                        client.Companion.MonsterInfo.SendScreen(move);
                    }
                }
                else
                {
                    var monster = client.Companion;
                    if (monster.MonsterInfo.InSight == 0)
                    {
                        if (client.Entity.AttackPacket != null)
                        {
                            if (client.Entity.AttackPacket.AttackType == Network.GamePackets.Attack.Magic)
                            {
                                if (client.Entity.AttackPacket.Decoded)
                                {
                                    if (Database.SpellTable.SpellInformations.ContainsKey((ushort)client.Entity.AttackPacket.Damage))
                                    {
                                        var info = Database.SpellTable.SpellInformations[(ushort)client.Entity.AttackPacket.Damage].Values.ToArray()[client.Spells[(ushort)client.Entity.AttackPacket.Damage].Level];
                                        if (info.CanKill)
                                        {
                                            monster.MonsterInfo.InSight = client.Entity.AttackPacket.Attacked;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                monster.MonsterInfo.InSight = client.Entity.AttackPacket.Attacked;
                            }
                        }
                    }
                    else
                    {
                        if (monster.MonsterInfo.InSight > 400000 && monster.MonsterInfo.InSight < 600000 || monster.MonsterInfo.InSight > 800000 && monster.MonsterInfo.InSight != monster.UID)
                        {
                            Entity attacked = null;

                            if (client.Screen.TryGetValue(monster.MonsterInfo.InSight, out attacked))
                            {
                                if (Now > monster.AttackStamp.AddMilliseconds(monster.MonsterInfo.AttackSpeed))
                                {
                                    monster.AttackStamp = Now;
                                    if (attacked.Dead)
                                    {
                                        monster.MonsterInfo.InSight = 0;
                                    }
                                    else
                                        new Game.Attacking.Handle(null, monster, attacked);
                                }
                            }
                            else
                                monster.MonsterInfo.InSight = 0;
                        }
                    }
                }
            }
        }
        private void WorldTournaments(int time)
        {
            try
            {
                //#region PlunderWar
                //if (DateTime.Now.Hour == 18 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00) Kernel.PlunderWar = true;
                //if (DateTime.Now.Hour == 20 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
                //{ Kernel.PlunderWar = false; Network.GamePackets.Union.UnionClass.UpGradeUnion(); }
                //#endregion
                #region Spawns
                if (!Kernel.Maps.ContainsKey(1927) || Kernel.Maps.ContainsKey(3935) || Kernel.Maps.ContainsKey(3856) || Kernel.Maps.ContainsKey(1002)) return;
                #region SnowBanshee
                if ((DateTime.Now.Minute == 27 && DateTime.Now.Second == 00) || (DateTime.Now.Minute == 57 && DateTime.Now.Second == 00))
                {
                    ushort MapID = 1927;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.Name == "SnowBanshee").Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(4171))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[4171];
                                mt.BoundX = 540;
                                mt.RespawnTime = 2000000000;
                                mt.BoundY = 430;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.Level = mt.Level;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.Boss = 1;
                                entity.MapID = MapID;
                                entity.X = 540;
                                entity.Y = 430;
                                if (entity.X == 0 || entity.Y == 0)
                                {
                                    do
                                    {
                                        entity.X = 540;
                                        entity.Y = 430;
                                    }
                                    while (!Map.Floor[entity.X, entity.Y, MapObjectType.Monster]);
                                }

                                Map.AddEntity(entity);
                                Network.GamePackets._String stringPacket = new Nyx.Server.Network.GamePackets._String(true);
                                stringPacket.UID = entity.UID;
                                stringPacket.Type = Network.GamePackets._String.Effect;
                                stringPacket.Texts.Add("MBStandard");
                                Data data = new Data(true);
                                data.UID = entity.UID;
                                data.ID = Network.GamePackets.Data.AddEntity;
                                data.wParam1 = entity.X;
                                data.wParam2 = entity.Y;
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            clllient.Send(stringPacket);
                                            clllient.Send(data);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }

                                        }
                                    }
                                }
                                foreach (var client in Kernel.GamePool.Values)
                                    client.Send(new Network.GamePackets.Message("The Snow Banshee appeared in Frozen Grotto 2 (" + 540 + ", " + 430 + ")! Defeat it!", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                            }
                        }
                    }
                }
                #endregion
                #region NemesisTyrant
                if ((DateTime.Now.Minute == 45 && DateTime.Now.Second == 00) || (DateTime.Now.Minute == 15 && DateTime.Now.Second == 00))
                {
                    ushort x = 0, y = 0;
                    int R = Kernel.Random.Next(1, 7);
                    if (R == 1)
                    {
                        x = 94;
                        y = 126;
                    }
                    if (R == 2)
                    {
                        x = 173;
                        y = 82;
                    }
                    if (R == 3)
                    {
                        x = 264;
                        y = 209;
                    }
                    if (R == 4)
                    {
                        x = 181;
                        y = 169;
                    }
                    if (R == 5)
                    {
                        x = 104;
                        y = 226;
                    }
                    if (R == 6)
                    {
                        x = 180;
                        y = 237;
                    }
                    if (R == 7)
                    {
                        x = 283;
                        y = 160;
                    }
                    if (R == 8)
                    {
                        x = 218;
                        y = 339;
                    }
                    ushort MapID = 3856;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.Name == "NemesisTyrant").Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(4220))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[4220];
                                mt.BoundX = x;
                                mt.RespawnTime = 2000000000;
                                mt.BoundY = y;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.Level = mt.Level;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.MapID = MapID;
                                entity.X = x;
                                entity.Boss = 1;
                                entity.Y = y;
                                if (x == 0 || y == 0)
                                {
                                    var cord = Map.RandomCoordinates();
                                    entity.X = cord.Item1;
                                    entity.Y = cord.Item2;
                                    do
                                    {
                                        cord = Map.RandomCoordinates();
                                        entity.X = cord.Item1;
                                        entity.Y = cord.Item2;
                                    }
                                    while (!Map.Floor[entity.X, entity.Y, MapObjectType.Monster]);
                                }

                                Map.AddEntity(entity);
                                Network.GamePackets._String stringPacket =
                new Nyx.Server.Network.GamePackets._String(true);
                                stringPacket.UID = entity.UID;
                                stringPacket.Type = Network.GamePackets._String.Effect;
                                stringPacket.Texts.Add("MBStandard");
                                Data data = new Data(true);
                                data.UID = entity.UID;
                                data.ID = Network.GamePackets.Data.AddEntity;
                                data.wParam1 = entity.X;
                                data.wParam2 = entity.Y;
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            clllient.Send(stringPacket);
                                            clllient.Send(data);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }

                                        }
                                    }
                                }
                                foreach (var client in Kernel.GamePool.Values)
                                    client.Entity.SendSysMessage("The NemesisTyrant has shown up in Bloodshed Sea (" + x + ", " + y + ")!");
                            }
                        }
                    }
                }
                #endregion
                #region AlluringWitch&HisCrystals
                if ((DateTime.Now.Minute == 00 && DateTime.Now.Second == 00))
                {
                    ushort MapID = 3935;
                    #region AlluringWitch
                    ushort ID = 7883;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.Name == "AlluringWitch").Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(ID))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[ID];
                                mt.BoundX = 386;
                                mt.RespawnTime = 2000000000;
                                mt.BoundY = 385;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.Boss = 1;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.Level = mt.Level;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.MapID = MapID;
                                entity.X = 386;
                                entity.Y = 385;
                                entity.SendUpdates = true;
                                Map.AddEntity(entity);
                                Network.GamePackets._String stringPacket =
                new Nyx.Server.Network.GamePackets._String(true);
                                stringPacket.UID = entity.UID;
                                stringPacket.Type = Network.GamePackets._String.Effect;
                                stringPacket.Texts.Add("MBStandard");
                                Data data = new Data(true);
                                data.UID = entity.UID;
                                data.ID = Network.GamePackets.Data.AddEntity;
                                data.wParam1 = entity.X;
                                data.wParam2 = entity.Y;
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            clllient.Send(stringPacket);
                                            clllient.Send(data);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }

                                        }
                                    }
                                }
                                foreach (var client in Kernel.GamePool.Values)
                                {
                                    client.Send(new Message("The Alluring Witch has appeared in the Deserted Village in the Realm. Hurry and go kill her to get Senior Dragon Souls!", Color.White, Message.Talk));
                                }
                            }
                        }
                    }
                    #endregion
                    #region DarkCrystalOfWind
                    ushort ID2 = 7971;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.MonsterInfo.ID == 7971).Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(ID2))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[ID2];
                                mt.BoundX = 380;
                                mt.RespawnTime = 2000000000;
                                mt.BoundY = 393;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.Boss = 1;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.Level = mt.Level;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.MapID = MapID;
                                entity.SendUpdates = true;
                                entity.X = 380;
                                entity.Y = 393;
                                Map.AddEntity(entity);
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region DarkCrystalOfFire
                    ushort ID3 = 7973;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.MonsterInfo.ID == 7973).Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(ID3))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[ID3];
                                mt.BoundX = 395;
                                mt.RespawnTime = 2000000000;
                                mt.BoundY = 395;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.Boss = 1;
                                entity.SendUpdates = true;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.Level = mt.Level;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.MapID = MapID;
                                entity.X = 395;
                                entity.Y = 395;
                                Map.AddEntity(entity);
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region ThunderCrystal
                    ushort ID4 = 7972;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.MonsterInfo.ID == 7972).Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(ID4))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[ID4];
                                mt.BoundX = 380;
                                mt.BoundY = 378;
                                mt.RespawnTime = 2000000000;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.Boss = 1;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.SendUpdates = true;
                                entity.Level = mt.Level;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.MapID = MapID;
                                entity.X = 380;
                                entity.Y = 378;
                                Map.AddEntity(entity);
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region DarkCrystalOfSky
                    ushort ID5 = 7884;
                    if (Kernel.Maps[MapID].Entities.Where(i => i.Value.MonsterInfo.ID == 7884).Count() == 0)
                    {
                        if (Kernel.Maps.ContainsKey(MapID))
                        {
                            var Map = Kernel.Maps[MapID];
                            if (Database.MonsterInformation.MonsterInformations.ContainsKey(ID5))
                            {
                                Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[ID5];
                                mt.BoundX = 396;
                                mt.BoundY = 379;
                                mt.RespawnTime = 2000000000;
                                Entity entity = new Entity(EntityFlag.Monster, false);
                                entity.MapObjType = MapObjectType.Monster;
                                entity.MonsterInfo = mt.Copy();
                                entity.MonsterInfo.Owner = entity;
                                entity.Name = mt.Name;
                                entity.Boss = 1;
                                entity.MinAttack = mt.MinAttack;
                                entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                                entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                                entity.Defence = mt.Defence;
                                entity.Body = mt.Mesh;
                                entity.Level = mt.Level;
                                entity.SendUpdates = true;
                                entity.UID = Map.EntityUIDCounter.Next;
                                entity.MapID = MapID;
                                entity.X = 396;
                                entity.Y = 379;
                                Map.AddEntity(entity);
                                foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                                {
                                    if (clllient.Map.ID == entity.MapID)
                                    {
                                        if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                            Constants.nScreenDistance)
                                        {
                                            entity.SendSpawn(clllient, false);
                                            if (entity.MaxHitpoints > 65535)
                                            {
                                                Update upd = new Update(true) { UID = entity.UID };
                                                upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                                upd.Append(Update.Hitpoints, entity.Hitpoints);
                                                clllient.Send(upd);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                #region Ganoderma&Titan
                if (DateTime.Now.Minute == 11 && DateTime.Now.Second == 00)
                {
                    uint monsterid = (uint)Kernel.Random.Next(3130, 3134);
                    ushort MapID = 1011;
                    var Map = Kernel.Maps[MapID];
                    if (Database.MonsterInformation.MonsterInformations.ContainsKey(monsterid))
                    {
                        Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[monsterid];
                        mt.BoundX = 674;
                        mt.BoundY = 790;
                        mt.RespawnTime = 2000000000;
                        Entity entity = new Entity(EntityFlag.Monster, false);
                        entity.MapObjType = MapObjectType.Monster;
                        entity.MonsterInfo = mt.Copy();
                        entity.MonsterInfo.Owner = entity;
                        entity.Name = mt.Name;
                        entity.MinAttack = mt.MinAttack;
                        entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                        entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                        entity.Defence = mt.Defence;
                        entity.Body = mt.Mesh;
                        entity.Level = mt.Level;
                        entity.UID = Map.EntityUIDCounter.Next;
                        entity.Boss = 1;
                        entity.MapID = MapID;
                        entity.X = 674;
                        entity.Y = 790;
                        if (entity.X == 0 || entity.Y == 0)
                        {
                            do
                            {
                                entity.X = 674;
                                entity.Y = 790;
                            }
                            while (!Map.Floor[entity.X, entity.Y, MapObjectType.Monster]);
                        }

                        Map.AddEntity(entity);
                        Network.GamePackets._String stringPacket =
        new Nyx.Server.Network.GamePackets._String(true);
                        stringPacket.UID = entity.UID;
                        stringPacket.Type = Network.GamePackets._String.Effect;
                        stringPacket.Texts.Add("MBStandard");
                        Data data = new Data(true);
                        data.UID = entity.UID;
                        data.ID = Network.GamePackets.Data.AddEntity;
                        data.wParam1 = entity.X;
                        data.wParam2 = entity.Y;
                        foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                        {
                            if (clllient.Map.ID == entity.MapID)
                            {
                                if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                    Constants.nScreenDistance)
                                {
                                    entity.SendSpawn(clllient, false);
                                    clllient.Send(stringPacket);
                                    clllient.Send(data);
                                    if (entity.MaxHitpoints > 65535)
                                    {
                                        Update upd = new Update(true) { UID = entity.UID };
                                        upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                        upd.Append(Update.Hitpoints, entity.Hitpoints);
                                        clllient.Send(upd);
                                    }

                                }
                            }
                        }
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(new Network.GamePackets.Message("Warning! Ganoderma has appeared at the forest.", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                    }
                }
                if (DateTime.Now.Minute == 15 && DateTime.Now.Second == 00)
                {
                    uint monsterid = (uint)Kernel.Random.Next(3134, 3137);
                    ushort MapID = 1020;
                    var Map = Kernel.Maps[MapID];
                    if (Database.MonsterInformation.MonsterInformations.ContainsKey(monsterid))
                    {
                        Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[monsterid];
                        mt.BoundX = 396;
                        mt.BoundY = 592;
                        Entity entity = new Entity(EntityFlag.Monster, false);
                        entity.MapObjType = MapObjectType.Monster;
                        entity.MonsterInfo = mt.Copy();
                        entity.MonsterInfo.Owner = entity;
                        entity.Name = mt.Name;
                        entity.MinAttack = mt.MinAttack;
                        entity.MaxAttack = entity.MagicAttack = mt.MaxAttack;
                        entity.Hitpoints = entity.MaxHitpoints = mt.Hitpoints;
                        entity.Defence = mt.Defence;
                        entity.Body = mt.Mesh;
                        entity.Level = mt.Level;
                        entity.UID = Map.EntityUIDCounter.Next;
                        entity.Boss = 1;
                        entity.MapID = MapID;
                        entity.X = 396;
                        entity.Y = 592;
                        if (entity.X == 0 || entity.Y == 0)
                        {
                            do
                            {
                                entity.X = 396;
                                entity.Y = 592;
                            }
                            while (!Map.Floor[entity.X, entity.Y, MapObjectType.Monster]);
                        }

                        Map.AddEntity(entity);
                        Network.GamePackets._String stringPacket =
        new Nyx.Server.Network.GamePackets._String(true);
                        stringPacket.UID = entity.UID;
                        stringPacket.Type = Network.GamePackets._String.Effect;
                        stringPacket.Texts.Add("MBStandard");
                        Data data = new Data(true);
                        data.UID = entity.UID;
                        data.ID = Network.GamePackets.Data.AddEntity;
                        data.wParam1 = entity.X;
                        data.wParam2 = entity.Y;
                        foreach (Client.GameClient clllient in Kernel.GamePool.Values)
                        {
                            if (clllient.Map.ID == entity.MapID)
                            {
                                if (Kernel.GetDistance(clllient.Entity.X, clllient.Entity.Y, entity.X, entity.Y) <
                                    Constants.nScreenDistance)
                                {
                                    entity.SendSpawn(clllient, false);
                                    clllient.Send(stringPacket);
                                    clllient.Send(data);
                                    if (entity.MaxHitpoints > 65535)
                                    {
                                        Update upd = new Update(true) { UID = entity.UID };
                                        upd.Append(Update.MaxHitpoints, entity.MaxHitpoints);
                                        upd.Append(Update.Hitpoints, entity.Hitpoints);
                                        clllient.Send(upd);
                                    }

                                }
                            }
                        }
                        foreach (var client in Kernel.GamePool.Values)
                            client.Send(new Network.GamePackets.Message("Warning! Titan has appeared at Love Canyon!", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                    }
                }
                #endregion
                #endregion
                #region Quiz Show
                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                    if (DateTime.Now.Hour == 4 || DateTime.Now.Hour == 14 || DateTime.Now.Hour == 21)
                        if (DateTime.Now.Minute == 0 && DateTime.Now.Second <= 2)
                            Kernel.QuizShow.Start();
                #endregion
                //#region CaptureTheFlag
                //if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                //{
                //    if (DateTime.Now.Hour == 20 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 0 && !CaptureTheFlag.IsWar)
                //    {
                //        CaptureTheFlag.IsWar = true;
                //        CaptureTheFlag.StartTime = DateTime.Now;
                //        CaptureTheFlag.ClearHistory();
                //        foreach (var current in Kernel.Guilds.Values)
                //        {
                //            current.CTFFlagScore = 0;
                //            current.Points = 0;
                //            current.CTFdonationCPs = 0;
                //            current.CTFdonationSilver = 0;
                //            current.CalculateCTFRank(true);
                //            foreach (var current2 in current.Members.Values)
                //            {
                //                current2.Exploits = 0;
                //                current2.ExploitsRank = 0;
                //                current2.CTFCpsReward = 0;
                //                current2.CTFSilverReward = 0;
                //            }
                //            current.CalculateCTFRank(false);
                //        }



                //    }
                //}
                //if (CaptureTheFlag.IsWar)
                //{
                //    Program.World.CTF.SendUpdates();
                //    if (DateTime.Now >= CaptureTheFlag.StartTime.AddHours(1))
                //    {
                //        CaptureTheFlag.IsWar = false;
                //        CaptureTheFlag.Close();
                //    }
                //}
                //if (CTF != null)
                //    CTF.SpawnFlags();
                //#endregion
                //#region TeamPk
                //if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && DateTime.Now.Hour == 18 && DateTime.Now.Minute == 55 && DateTime.Now.Second == 00)
                //    Game.Features.Tournaments.TeamElitePk.TeamTournament.Open();
                //#endregion
                //#region SkillTeamPk
                //if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && DateTime.Now.Hour == 19 && DateTime.Now.Minute == 40 && DateTime.Now.Second == 00)
                //    Game.Features.Tournaments.TeamElitePk.SkillTeamTournament.Open();
                //#endregion
                #region ClanWarArena
                {
                    if (Time32.Now > ClanWarArenaStampScore.AddSeconds(5))
                    {
                        ClanWarArenaStampScore = Time32.Now;
                        GameClient[] array = (
                        from p in Kernel.GamePool.Values
                        where p.Entity.MapID == 1505 && !p.Entity.Dead
                        select p).ToArray<GameClient>();
                        int num = Kernel.ClansOnMap(array);
                        byte[] buffer = new Message(string.Concat(new object[]
                {
                    "Alive Clans In ",
                    "TwinCityClanArena",
                    " : ",
                    num
                }), Color.Yellow, 2108u).Encode();
                        GameClient[] array2 = array;
                        for (int i = 0; i < array2.Length; i++)
                        {
                            GameClient GameClient = array2[i];
                            GameClient.Send(buffer);
                        }
                    }
                }
                #endregion
                #region GuildWar Monday
                if (!Game.GuildWar.IsWar)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour == 18 && DateTime.Now.Minute >= 00)
                    {
                        Game.GuildWar.Start();
                        foreach (var client in Program.GamePool)
                            if (client.Entity.GuildID != 0)
                                client.MessageBox("GuildWar Has Started Now Will Finish 9:00 PM",
                                    p => { p.Entity.Teleport(1002, 311, 263); }, null);
                    }
                }
                if (Game.GuildWar.IsWar)
                {
                    if (Time32.Now > Game.GuildWar.ScoreSendStamp.AddSeconds(3))
                    {
                        Game.GuildWar.ScoreSendStamp = Time32.Now;
                        Game.GuildWar.SendScores();
                    }
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour == 20 && DateTime.Now.Minute == 50 && DateTime.Now.Second <= 2)
                    {
                        Kernel.SendWorldMessage(new Network.GamePackets.Message("GuildWar Has will closed 10 Minutes go all Pola and ready to finish",
                                System.Drawing.Color.White, Network.GamePackets.Message.Center), Program.GamePool);
                    }
                    else if (GuildWar.IsWar)
                    {
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && DateTime.Now.Hour == 21 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
                        {
                            GuildWar.End();
                        }
                    }
                }
                #endregion
                #region GuildWar Friday
                if (!Game.GuildWar.IsWar)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DateTime.Now.Hour == 18 && DateTime.Now.Minute >= 00)
                    {
                        Game.GuildWar.Start();
                        foreach (var client in Program.GamePool)
                            if (client.Entity.GuildID != 0)
                                client.MessageBox("GuildWar Has Started Now Will Finish 9:00 PM",
                                    p => { p.Entity.Teleport(1002, 311, 263); }, null);
                    }
                }
                if (Game.GuildWar.IsWar)
                {
                    if (Time32.Now > Game.GuildWar.ScoreSendStamp.AddSeconds(3))
                    {
                        Game.GuildWar.ScoreSendStamp = Time32.Now;
                        Game.GuildWar.SendScores();
                    }
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DateTime.Now.Hour == 20 && DateTime.Now.Minute == 50 && DateTime.Now.Second <= 2)
                    {
                        Kernel.SendWorldMessage(new Network.GamePackets.Message("GuildWar Has will closed 10 Minutes go all Pola and ready to finish",
                                System.Drawing.Color.White, Network.GamePackets.Message.Center), Program.GamePool);
                    }
                    else if (GuildWar.IsWar)
                    {
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && DateTime.Now.Hour == 21 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 00)
                        {
                            GuildWar.End();
                        }
                    }
                }
                #endregion
                #region  Restat server 13 [PM]
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 55 && DateTime.Now.Second <= 00)
                {

                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 5 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                    Console.WriteLine("Warrning !!! Server Will Restart Aftr 5 Minutes get Ready !!");
                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 56 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 4 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 57 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 3 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);

                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 58 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 2 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);

                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 59 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 1 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);

                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 59 && DateTime.Now.Second <= 30)
                {

                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart After 30 Seconds get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 59 && DateTime.Now.Second <= 55)
                {
                    Program.CommandsAI("@save");
                }
                if (DateTime.Now.Hour == 13 && DateTime.Now.Minute == 00 && DateTime.Now.Second <= 00)
                {
                    Program.CommandsAI("@restart");
                }
                #endregion
                #region  Restat server 1 [AM]
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute == 55 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 5 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                    Console.WriteLine("Warrning !!! Server Will Restart Aftr 5 Minutes get Ready !!");
                }
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute == 56 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 4 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                }
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute == 57 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 3 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);

                }
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute == 58 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 2 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);

                }
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute == 59 && DateTime.Now.Second <= 00)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart Aftr 1 Minutes get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);

                }
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute == 59 && DateTime.Now.Second <= 30)
                {
                    Kernel.SendWorldMessage(new Message(" Warrning !!! Server Will Restart After 30 Seconds get Ready !!.", System.Drawing.Color.Black, Message.Talk), Program.GamePool);
                }
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 59 && DateTime.Now.Second <= 55)
                {
                    Program.CommandsAI("@save");
                }
                if (DateTime.Now.Hour == 01 && DateTime.Now.Minute == 00 && DateTime.Now.Second <= 00)
                {
                    Program.CommandsAI("@restart");
                }
                #endregion
                #region Elite PK Tournament
                if (((DateTime.Now.Hour == ElitePK.EventTime) && DateTime.Now.Minute >= 55) && !ElitePKTournament.TimersRegistered)
                {
                    ElitePK.EventTime = DateTime.Now.Hour;
                    ElitePKTournament.RegisterTimers();
                }
                #endregion
                #region ApeCity--DropBarty
                if (DateTime.Now.Minute == 29 && DateTime.Now.Second == 55)
                {
                    Kernel.SendWorldMessage(new Message("[GM] Envoy will apear in ApeCity after 1 Minute [Prize: [100] ConquerPoints] ?!", Color.White, Message.TopLeft), Program.Values);
                    foreach (var Pclient in Program.Values)
                        Pclient.MessageBox("[GM] Envoy will apear in ApeCity after 1 Minute [Prize: [100] ConquerPoints] ?!",
                        p => { p.Entity.Teleport(1020, 567, 566); }, null, 60);
                
                    Kernel.SendWorldMessage(new Message("[GM] Envoy will apear in ApeCity after 1 Minute and DropParty will Start Hurry go to ApeCity to Get 100 CPS.", System.Drawing.Color.Black, Message.Center), Program.Values);
                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 03)
                {

                    Game._GM_ApeCity.Load2();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 06)
                {

                    Game._GM_ApeCity.Load3();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 09)
                {

                    Game._GM_ApeCity.Load4();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 12)
                {

                    Game._GM_ApeCity.Load5();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 15)
                {

                    Game._GM_ApeCity.Load6();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 18)
                {

                    Game._GM_ApeCity.Load7();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 21)
                {

                    Game._GM_ApeCity.Load8();

                }
                if (DateTime.Now.Minute == 00 && DateTime.Now.Second == 24)
                {

                    Game._GM_ApeCity.Load9();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 28)
                {

                    Game._GM_ApeCity.Load10();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 31)
                {

                    Game._GM_ApeCity.Load11();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 34)
                {

                    Game._GM_ApeCity.Load12();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 37)
                {

                    Game._GM_ApeCity.Load13();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 40)
                {

                    Game._GM_ApeCity.Load14();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 43)
                {

                    Game._GM_ApeCity.Load15();

                }
                if (DateTime.Now.Minute == 30 && DateTime.Now.Second == 46)
                {

                    Game._GM_ApeCity.Load16();
                    Kernel.SendWorldMessage(new Message("[GM] Envoy Drop Event ended come back next hour , it apear every hour at 1:00 Goodluck.", System.Drawing.Color.Black, Message.Center), Program.Values);

                
                }
                #endregion

            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }
        private void ServerFunctions(int time)
        {
            //if (DateTime.Now.Second == 00)
            //{
            //    //Program.Save();
            //}
            if (DateTime.Now > Program.LastRandomReset.AddMinutes(30))
            {
                Program.LastRandomReset = DateTime.Now;
                Kernel.Random = new FastRandom(Program.RandomSeed);
            }
            if (DateTime.Now >= LastPerfectionSort.AddMinutes(1))
            {
                LastPerfectionSort = DateTime.Now;
                new MsgUserAbilityScore().GetRankingList();
                new MsgEquipRefineRank().UpdateRanking();
            }
        }
        DateTime LastPerfectionSort = DateTime.Now;
        private void ArenaFunctions(int time)
        {
            Game.Arena.EngagePlayers();
            Game.Arena.CheckGroups();
            Game.Arena.VerifyAwaitingPeople();
            Game.Arena.Reset();
        }
        private void TeamArenaFunctions(int time)
        {
            Game.TeamArena.PickUpTeams();
            Game.TeamArena.EngagePlayers();
            Game.TeamArena.CheckGroups();
            Game.TeamArena.VerifyAwaitingPeople();
            Game.TeamArena.Reset();
        }
        #region Funcs
        public static void Execute(Action<int> action, int timeOut = 0, ThreadPriority priority = ThreadPriority.Normal)
        {
            GenericThreadPool.Subscribe(new LazyDelegate(action, timeOut, priority));
        }
        public static void Execute<T>(Action<T, int> action, T param, int timeOut = 0, ThreadPriority priority = ThreadPriority.Normal)
        {
            GenericThreadPool.Subscribe<T>(new LazyDelegate<T>(action, timeOut, priority), param);
        }
        public static IDisposable Subscribe(Action<int> action, int period = 1, ThreadPriority priority = ThreadPriority.Normal)
        {
            return GenericThreadPool.Subscribe(new TimerRule(action, period, priority));
        }
        public static IDisposable Subscribe<T>(Action<T, int> action, T param, int timeOut = 0, ThreadPriority priority = ThreadPriority.Normal)
        {
            return GenericThreadPool.Subscribe<T>(new TimerRule<T>(action, timeOut, priority), param);
        }
        public static IDisposable Subscribe<T>(TimerRule<T> rule, T param, StandalonePool pool)
        {
            return pool.Subscribe<T>(rule, param);
        }
        public static IDisposable Subscribe<T>(TimerRule<T> rule, T param, StaticPool pool)
        {
            return pool.Subscribe<T>(rule, param);
        }
        public static IDisposable Subscribe<T>(TimerRule<T> rule, T param)
        {
            return GenericThreadPool.Subscribe<T>(rule, param);
        }
        #endregion
    }
}