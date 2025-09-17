using Microsoft.Extensions.Hosting;
using Nyx.Server.Client;
using Nyx.Server.Interfaces;
using Nyx.Server.Network.GamePackets;
using Serilog;

namespace Nyx.Server.Threding
{
    public class CharacterService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(5);

        public CharacterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Character Service started");

            using var timer = new PeriodicTimer(_checkInterval);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await PerformCharacterChecksAsync();
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Security Service stopped gracefully");
            }
        }

        private async Task PerformCharacterChecksAsync()
        {
            try
            {
                var onlineClients = GetOnlineClients().ToList();

                foreach (var client in onlineClients)
                {
                    try
                    {
                        await CheckCharacterPromotion(client);
                        await CheckCharacterSpells(client);
                        // Add more security checks here as needed
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error performing Character checks for client: {ClientId}", client.Account.EntityID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in Character check cycle");
            }
        }

        private async Task CheckCharacterPromotion(GameClient client)
        {
            #region Trojan Promotion  
            byte trojancurrentclass = 10;
            byte trojanMaxClass = 15;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == trojancurrentclass && client.Entity.Class < trojanMaxClass)
            {
                client.Entity.Class = 11;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= trojancurrentclass && client.Entity.Class <= trojanMaxClass)
            {
                client.Entity.Class = 12;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= trojancurrentclass && client.Entity.Class <= trojanMaxClass)
            {
                client.Entity.Class = 13;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= trojancurrentclass && client.Entity.Class <= trojanMaxClass)
            {
                client.Entity.Class = 14;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= trojancurrentclass && client.Entity.Class < trojanMaxClass)
            {
                client.Entity.Class = 15;
            }
            #endregion
            #region Warrior Promotion  
            byte Warriorcurrentclass = 20;
            byte WarriorMaxClass = 25;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == Warriorcurrentclass && client.Entity.Class < WarriorMaxClass)
            {
                client.Entity.Class = 21;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= Warriorcurrentclass && client.Entity.Class <= WarriorMaxClass)
            {
                client.Entity.Class = 22;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= Warriorcurrentclass && client.Entity.Class <= WarriorMaxClass)
            {
                client.Entity.Class = 23;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= Warriorcurrentclass && client.Entity.Class <= WarriorMaxClass)
            {
                client.Entity.Class = 24;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= Warriorcurrentclass && client.Entity.Class < WarriorMaxClass)
            {
                client.Entity.Class = 25;
            }
            #endregion
            #region Archer Promotion
            byte Archercurrentclass = 40;
            byte ArcherMaxClass = 45;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == Archercurrentclass && client.Entity.Class < ArcherMaxClass)
            {
                client.Entity.Class = 41;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= Archercurrentclass && client.Entity.Class <= ArcherMaxClass)
            {
                client.Entity.Class = 42;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= Archercurrentclass && client.Entity.Class <= ArcherMaxClass)
            {
                client.Entity.Class = 43;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= Archercurrentclass && client.Entity.Class <= ArcherMaxClass)
            {
                client.Entity.Class = 44;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= Archercurrentclass && client.Entity.Class < ArcherMaxClass)
            {
                client.Entity.Class = 45;
            }
            #endregion
            #region Ninja Promotion
            byte NinjacurrentClass = 50;
            byte NinjaMaxClass = 55;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == NinjacurrentClass && client.Entity.Class < NinjaMaxClass)
            {
                client.Entity.Class = 51;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= NinjacurrentClass && client.Entity.Class <= NinjaMaxClass)
            {
                client.Entity.Class = 52;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= NinjacurrentClass && client.Entity.Class <= NinjaMaxClass)
            {
                client.Entity.Class = 53;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= NinjacurrentClass && client.Entity.Class <= NinjaMaxClass)
            {
                client.Entity.Class = 54;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= NinjacurrentClass && client.Entity.Class < NinjaMaxClass)
            {
                client.Entity.Class = 55;
            }

            #endregion
            #region Monk Promotion  
            byte MonkcurrentClass = 60;
            byte MonkMaxClass = 65;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == MonkcurrentClass && client.Entity.Class < MonkMaxClass)
            {
                client.Entity.Class = 61;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= MonkcurrentClass && client.Entity.Class <= MonkMaxClass)
            {
                client.Entity.Class = 62;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= MonkcurrentClass && client.Entity.Class <= MonkMaxClass)
            {
                client.Entity.Class = 63;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= MonkcurrentClass && client.Entity.Class <= MonkMaxClass)
            {
                client.Entity.Class = 64;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= MonkcurrentClass && client.Entity.Class < MonkMaxClass)
            {
                client.Entity.Class = 65;
            }


            #endregion
            #region Pirate Promotion  
            byte PiratecurrentClass = 70;
            byte PirateMaxClass = 75;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == PiratecurrentClass && client.Entity.Class < PirateMaxClass)
            {
                client.Entity.Class = 71;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= PiratecurrentClass && client.Entity.Class <= PirateMaxClass)
            {
                client.Entity.Class = 72;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= PiratecurrentClass && client.Entity.Class <= PirateMaxClass)
            {
                client.Entity.Class = 73;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= PiratecurrentClass && client.Entity.Class <= PirateMaxClass)
            {
                client.Entity.Class = 74;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= PiratecurrentClass && client.Entity.Class < PirateMaxClass)
            {
                client.Entity.Class = 75;
            }

            #endregion
            #region Water And Fire Promotion  
            #region Water  
            if ((client.Entity.Level >= 40 && client.Entity.Level < 70) && (client.Entity.Class == 100))
            {
                client.Entity.Class = 101;
            }
            if ((client.Entity.Level >= 70 && client.Entity.Level < 100) && (client.Entity.Class == 101))
            {
                client.Entity.Class = 102;
            }
            if ((client.Entity.Level >= 100 && client.Entity.Level < 110) && (client.Entity.Class == 132))
            {
                client.Entity.Class = 133;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class == 133)
            {
                client.Entity.Class = 135;
            }
            #endregion
            #region Fire  
            if ((client.Entity.Level >= 40 && client.Entity.Level <= 70) && (client.Entity.Class == 100))
            {
                client.Entity.Class = 101;
            }
            if ((client.Entity.Level >= 100 && client.Entity.Level <= 110) && (client.Entity.Class == 142))
            {
                client.Entity.Class = 143;
            }
            if ((client.Entity.Level >= 110 && client.Entity.Level <= 120) && (client.Entity.Class == 143))
            {
                client.Entity.Class = 145;
            }
            #endregion
            #endregion
            #region Dragon-Warrior Promotion  
            byte DragonWarriorcurrentClass = 80;
            byte DragonWarriorMaxClass = 85;
            if (client.Entity.Level >= 15 && client.Entity.Level < 40 && client.Entity.Class == DragonWarriorcurrentClass && client.Entity.Class < DragonWarriorMaxClass)
            {
                client.Entity.Class = 81;
            }
            if (client.Entity.Level >= 40 && client.Entity.Level < 70 && client.Entity.Class >= DragonWarriorcurrentClass && client.Entity.Class <= DragonWarriorMaxClass)
            {
                client.Entity.Class = 82;
            }
            if (client.Entity.Level >= 70 && client.Entity.Level < 100 && client.Entity.Class >= DragonWarriorcurrentClass && client.Entity.Class <= DragonWarriorMaxClass)
            {
                client.Entity.Class = 83;
            }
            if (client.Entity.Level >= 100 && client.Entity.Level < 110 && client.Entity.Class >= DragonWarriorcurrentClass && client.Entity.Class <= DragonWarriorMaxClass)
            {
                client.Entity.Class = 84;
            }
            if (client.Entity.Level >= 110 && client.Entity.Class >= DragonWarriorcurrentClass && client.Entity.Class < DragonWarriorMaxClass)
            {
                client.Entity.Class = 85;
            }

            #endregion
        }

        private async Task CheckCharacterSpells(GameClient client)
        {
            try
            {
                #region Trojan  
                HashSet<ushort> TrojanSpells = new HashSet<ushort>() { 1110, 1015, 11970, 11990, 11980, 1115, 1270, 1190, 11960 };
                if (client.Entity.Class >= 10 && client.Entity.Class <= 15 && client.Entity.Reborn == 0)
                {
                    if (client.Entity.Level >= 40)
                    {
                        if (client.Entity.Class >= 11)
                            // Use the HashSet for cleaner code
                            foreach (var spellId in TrojanSpells.Where(id => id != 11960)) // Exclude level 90 spell
                            {
                                if (!client.Spells.ContainsKey(spellId))
                                {
                                    client.AddSpell(LearnableMagic(spellId));
                                    effx = true;
                                }
                            }
                    }
                    if (client.Entity.Level >= 90 && client.Entity.Reborn == 2 && client.Entity.FirstRebornClass == 15 && client.Entity.SecondRebornClass == 15)
                    {
                        if (!client.Spells.ContainsKey(11960))
                        {
                            client.AddSpell(LearnableMagic(11960));
                            effx = true;
                        }
                    }
                }
                #endregion
                #region Warrior  
                if (client.Entity.Class >= 20 && client.Entity.Class <= 25)
                {
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(11200)) { client.AddSpell(LearnableMagic(11200)); effx = true; }
                        if (!client.Spells.ContainsKey(10470)) { client.AddSpell(LearnableMagic(10470)); effx = true; }
                        if (!client.Spells.ContainsKey(1025)) { client.AddSpell(LearnableMagic(1025)); effx = true; }
                        if (!client.Spells.ContainsKey(1020)) { client.AddSpell(LearnableMagic(1020)); effx = true; }
                        if (!client.Spells.ContainsKey(1015)) { client.AddSpell(LearnableMagic(1015)); effx = true; }
                        if (!client.Spells.ContainsKey(12770)) { client.AddSpell(LearnableMagic(12770)); effx = true; }
                        if (!client.Spells.ContainsKey(12670)) { client.AddSpell(LearnableMagic(12670)); effx = true; }
                        if (!client.Spells.ContainsKey(12700)) { client.AddSpell(LearnableMagic(12700)); effx = true; }
                        if (!client.Spells.ContainsKey(10311)) { client.AddSpell(LearnableMagic(10311)); effx = true; }
                        if (!client.Spells.ContainsKey(1045)) { client.AddSpell(LearnableMagic(1045)); effx = true; }
                        if (!client.Spells.ContainsKey(1046)) { client.AddSpell(LearnableMagic(1046)); effx = true; }
                    }
                    if (client.Entity.Level >= 61)
                    {
                        if (!client.Spells.ContainsKey(12680)) { client.AddSpell(LearnableMagic(12680)); effx = true; }
                        if (!client.Spells.ContainsKey(1051)) { client.AddSpell(LearnableMagic(1051)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(12660)) { client.AddSpell(LearnableMagic(12660)); effx = true; }
                        if (!client.Spells.ContainsKey(12690)) { client.AddSpell(LearnableMagic(12690)); effx = true; }
                        if (!client.Spells.ContainsKey(11160)) { client.AddSpell(LearnableMagic(11160)); effx = true; }
                    }
                }
                #endregion
                #region Archer  
                if (client.Entity.Class >= 40 && client.Entity.Class <= 45)
                {
                    if (client.Entity.Level >= 1)
                    {
                        if (!client.Spells.ContainsKey(8002)) { client.AddSpell(LearnableMagic(8002)); effx = true; }
                    }
                    if (client.Entity.Level >= 23)
                    {
                        if (!client.Spells.ContainsKey(8001)) { client.AddSpell(LearnableMagic(8001)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(8000)) { client.AddSpell(LearnableMagic(8000)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(8003)) { client.AddSpell(LearnableMagic(8003)); effx = true; }
                        if (!client.Spells.ContainsKey(9000)) { client.AddSpell(LearnableMagic(9000)); effx = true; }
                        if (!client.Spells.ContainsKey(8030)) { client.AddSpell(LearnableMagic(8030)); effx = true; }
                    }
                    if (client.Entity.Level >= 100)
                    {

                        if (client.Spells[8003].Level < 1)
                        {
                            ISkill spell = new Spell(true);
                            spell.ID = 8003;
                            spell.Level = 1;
                            client.AddSpell(spell);
                            effx = true;
                        }

                    }
                }
                if (client.Entity.Class >= 41 && client.Entity.Class <= 45)
                {
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(11620)) { client.Send(new Network.GamePackets.Message("Congratulations! You now hold the knowledge of this path!.", Network.GamePackets.Message.System)); client.AddSpell(LearnableMagic(11620)); effx = true; }
                        if (!client.Spells.ContainsKey(11610)) { client.AddSpell(LearnableMagic(11610)); effx = true; }
                        if (!client.Spells.ContainsKey(11660)) { client.AddSpell(LearnableMagic(11660)); effx = true; }
                    }
                    if (client.Entity.Level >= 50)
                    {
                        if (!client.Spells.ContainsKey(11590)) { client.AddSpell(LearnableMagic(11590)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(11650)) { client.AddSpell(LearnableMagic(11650)); effx = true; }
                    }
                    if (client.Entity.Level >= 90)
                    {
                        if (!client.Spells.ContainsKey(11670)) { client.AddSpell(LearnableMagic(11670)); effx = true; }
                    }
                    if (client.Entity.Level >= 100)
                    {
                        if (!client.Spells.ContainsKey(11600)) { client.AddSpell(LearnableMagic(11600)); effx = true; }
                    }
                }
                #endregion

                #region Ninja  
                if (client.Entity.Class >= 50 && client.Entity.Class <= 55)
                {
                    if (client.Entity.Level >= 15)
                    {
                        if (!client.Spells.ContainsKey(6011)) { client.AddSpell(LearnableMagic(6011)); effx = true; }
                        if (!client.Spells.ContainsKey(12070)) { client.AddSpell(LearnableMagic(12070)); effx = true; }
                        if (!client.Spells.ContainsKey(12110)) { client.AddSpell(LearnableMagic(12110)); effx = true; }
                    }
                    if (client.Entity.Level >= 20)
                    {
                        if (!client.Spells.ContainsKey(11170)) { client.AddSpell(LearnableMagic(11170)); effx = true; }
                        if (!client.Spells.ContainsKey(11180)) { client.AddSpell(LearnableMagic(11180)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(6011)) { client.AddSpell(LearnableMagic(6011)); effx = true; }
                        if (!client.Spells.ContainsKey(6000)) { client.AddSpell(LearnableMagic(6000)); effx = true; }
                        if (!client.Spells.ContainsKey(12090)) { client.AddSpell(LearnableMagic(12090)); effx = true; }
                        if (!client.Spells.ContainsKey(12080)) { client.AddSpell(LearnableMagic(12080)); effx = true; }
                        if (!client.Spells.ContainsKey(11230)) { client.AddSpell(LearnableMagic(11230)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(6001)) { client.AddSpell(LearnableMagic(6001)); effx = true; }
                        if (!client.Spells.ContainsKey(6002)) { client.AddSpell(LearnableMagic(6002)); effx = true; }
                        if (!client.Spells.ContainsKey(12090)) { client.AddSpell(LearnableMagic(12090)); effx = true; }
                        if (!client.Spells.ContainsKey(6010)) { client.AddSpell(LearnableMagic(6010)); effx = true; }
                    }
                    if (client.Entity.Level >= 110)
                    {
                        if (!client.Spells.ContainsKey(6004)) { client.AddSpell(LearnableMagic(6004)); effx = true; }
                        if (client.Entity.Reborn == 2 && client.Entity.Class == 55 && client.Entity.SecondRebornClass == 55 && client.Entity.FirstRebornClass == 55)
                        {
                            if (!client.Spells.ContainsKey(6003)) { client.AddSpell(LearnableMagic(6003)); effx = true; }
                        }
                    }
                }
                #endregion
                #region Monk  
                if (client.Entity.Class >= 60 && client.Entity.Class <= 65)
                {
                    if (client.Entity.Level >= 5)
                    {
                        if (!client.Spells.ContainsKey(10490)) { client.AddSpell(LearnableMagic(10490)); effx = true; }
                    }
                    if (client.Entity.Level >= 15)
                    {
                        if (!client.Spells.ContainsKey(10390)) { client.AddSpell(LearnableMagic(10390)); effx = true; }
                        if (!client.Spells.ContainsKey(10415)) { client.AddSpell(LearnableMagic(10415)); effx = true; }
                    }
                    if (client.Entity.Level >= 20)
                    {
                        if (!client.Spells.ContainsKey(10395)) { client.AddSpell(LearnableMagic(10395)); effx = true; }
                        if (!client.Spells.ContainsKey(10410)) { client.AddSpell(LearnableMagic(10410)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(10381)) { client.AddSpell(LearnableMagic(10381)); effx = true; }
                        if (!client.Spells.ContainsKey(10400)) { client.AddSpell(LearnableMagic(10400)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(10425)) { client.AddSpell(LearnableMagic(10425)); effx = true; }
                    }
                    if (client.Entity.Level >= 100)
                    {
                        if (!client.Spells.ContainsKey(10420)) { client.AddSpell(LearnableMagic(10420)); effx = true; }
                        if (!client.Spells.ContainsKey(10421)) { client.AddSpell(LearnableMagic(10421)); effx = true; }
                        if (!client.Spells.ContainsKey(10422)) { client.AddSpell(LearnableMagic(10422)); effx = true; }
                        if (!client.Spells.ContainsKey(10423)) { client.AddSpell(LearnableMagic(10423)); effx = true; }
                        if (!client.Spells.ContainsKey(10424)) { client.AddSpell(LearnableMagic(10424)); effx = true; }
                        if (!client.Spells.ContainsKey(10430)) { client.AddSpell(LearnableMagic(10430)); effx = true; }
                    }
                    if (client.Entity.Level >= 140)
                    {
                        if (!client.Spells.ContainsKey(12570)) { client.AddSpell(LearnableMagic(12570)); effx = true; }
                        if (!client.Spells.ContainsKey(12550)) { client.AddSpell(LearnableMagic(12550)); effx = true; }
                        if (!client.Spells.ContainsKey(12560)) { client.AddSpell(LearnableMagic(12560)); effx = true; }
                    }
                }
                #endregion

                #region Pirate  
                if (client.Entity.Class >= 70 && client.Entity.Class <= 75)
                {
                    if (client.Entity.Level >= 1)
                    {
                        if (!client.Spells.ContainsKey(11030)) { client.AddSpell(LearnableMagic(11030)); effx = true; }
                        if (!client.Spells.ContainsKey(11050)) { client.AddSpell(LearnableMagic(11050)); effx = true; }
                    }
                    if (client.Entity.Level >= 15)
                    {
                        if (!client.Spells.ContainsKey(11140)) { client.AddSpell(LearnableMagic(11140)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(11060)) { client.AddSpell(LearnableMagic(11060)); effx = true; }
                        if (!client.Spells.ContainsKey(11110)) { client.AddSpell(LearnableMagic(11110)); effx = true; }
                        if (!client.Spells.ContainsKey(11130)) { client.AddSpell(LearnableMagic(11130)); effx = true; }
                        if (!client.Spells.ContainsKey(11070)) { client.AddSpell(LearnableMagic(11070)); effx = true; }
                        if (!client.Spells.ContainsKey(11120)) { client.AddSpell(LearnableMagic(11120)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (client.Entity.Reborn >= 1 && client.Entity.FirstRebornClass == 75)
                        {
                            if (!client.Spells.ContainsKey(11100)) { client.AddSpell(LearnableMagic(11100)); effx = true; }
                        }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (client.Entity.Class >= 70 && client.Entity.FirstRebornClass >= 72 && client.Entity.FirstRebornClass <= 75 && client.Entity.SecondRebornClass >= 72 && client.Entity.SecondRebornClass <= 75)
                        {
                            if (!client.Spells.ContainsKey(11040)) { client.AddSpell(LearnableMagic(11040)); effx = true; }
                        }
                    }
                }
                #endregion

                #region Taoist  
                if (client.Entity.Class >= 100 && client.Entity.Class <= 101)
                {
                    if (!client.Spells.ContainsKey(1000)) { client.AddSpell(LearnableMagic(1000)); effx = true; }
                    if (!client.Spells.ContainsKey(1005)) { client.AddSpell(LearnableMagic(1005)); effx = true; }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(1195)) { client.AddSpell(LearnableMagic(1195)); effx = true; }
                    }
                }
                #region Water  
                if (client.Entity.Class >= 132 && client.Entity.Class <= 135)
                {
                    if (!client.Spells.ContainsKey(1000)) { client.AddSpell(LearnableMagic(1000)); effx = true; }
                    if (!client.Spells.ContainsKey(1005)) { client.AddSpell(LearnableMagic(1005)); effx = true; }
                    if (!client.Spells.ContainsKey(12390)) { client.AddSpell(LearnableMagic(12390)); client.Send(new Network.GamePackets.Message("Congratulations! You have learned BlessingTouch Epic Skill For Water.", Network.GamePackets.Message.System)); effx = true; }
                    if (!client.Spells.ContainsKey(12370)) { client.AddSpell(LearnableMagic(12370)); client.Send(new Network.GamePackets.Message("Congratulations! You have learned AuroraLotus Epic Skill For Water.", Network.GamePackets.Message.System)); effx = true; }
                    if (client.Entity.Class >= 132 && client.Entity.FirstRebornClass >= 132 && client.Entity.SecondRebornClass >= 132)
                    {
                        if (!client.Spells.ContainsKey(30000)) { client.AddSpell(LearnableMagic(30000)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(10309)) { client.AddSpell(LearnableMagic(10309)); effx = true; }
                        if (!client.Spells.ContainsKey(1195)) { client.AddSpell(LearnableMagic(1195)); effx = true; }
                        if (!client.Spells.ContainsKey(1055)) { client.AddSpell(LearnableMagic(1055)); effx = true; }
                        if (!client.Spells.ContainsKey(1085)) { client.AddSpell(LearnableMagic(1085)); effx = true; }
                        if (!client.Spells.ContainsKey(1090)) { client.AddSpell(LearnableMagic(1090)); effx = true; }
                        if (!client.Spells.ContainsKey(1095)) { client.AddSpell(LearnableMagic(1095)); effx = true; }
                        if (!client.Spells.ContainsKey(1125)) { client.AddSpell(LearnableMagic(1125)); effx = true; }
                        if (!client.Spells.ContainsKey(1010)) { client.AddSpell(LearnableMagic(1010)); effx = true; }
                        if (!client.Spells.ContainsKey(1050)) { client.AddSpell(LearnableMagic(1050)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(1075)) { client.AddSpell(LearnableMagic(1075)); effx = true; }
                        if (!client.Spells.ContainsKey(1100)) { client.AddSpell(LearnableMagic(1100)); effx = true; }
                    }
                    if (client.Entity.Level >= 80)
                    {
                        if (!client.Spells.ContainsKey(1175)) { client.AddSpell(LearnableMagic(1175)); effx = true; }
                        if (!client.Spells.ContainsKey(10309)) { client.AddSpell(LearnableMagic(10309)); effx = true; }
                    }
                    if (client.Entity.Level >= 94)
                    {
                        if (!client.Spells.ContainsKey(1170)) { client.AddSpell(LearnableMagic(1170)); effx = true; }
                    }
                }
                else
                {
                    if (client.Spells.ContainsKey(12390)) { client.RemoveSpell(LearnableMagic(12390)); }
                    if (client.Spells.ContainsKey(12370)) { client.RemoveSpell(LearnableMagic(12370)); }
                }
                #endregion
                #region Fire  
                if (client.Entity.Class >= 142 && client.Entity.Class <= 145)
                {
                    if (!client.Spells.ContainsKey(1125)) { client.AddSpell(LearnableMagic(1125)); effx = true; }
                    if (!client.Spells.ContainsKey(1010)) { client.AddSpell(LearnableMagic(1010)); effx = true; }
                    if (!client.Spells.ContainsKey(5001)) { client.AddSpell(LearnableMagic(5001)); effx = true; }
                    if (!client.Spells.ContainsKey(1005)) { client.AddSpell(LearnableMagic(1005)); effx = true; }
                    if (!client.Spells.ContainsKey(10310)) { client.AddSpell(LearnableMagic(10310)); effx = true; }
                    if (!client.Spells.ContainsKey(12380)) { client.AddSpell(LearnableMagic(12380)); client.Send(new Network.GamePackets.Message("Congratulations! You have learned FlameLotus Epic Skill For Fire.", Network.GamePackets.Message.System)); effx = true; }
                    if (!client.Spells.ContainsKey(12400)) { client.AddSpell(LearnableMagic(12400)); client.Send(new Network.GamePackets.Message("Congratulations! You have learned BreakingTouch Epic Skill For Fire.", Network.GamePackets.Message.System)); effx = true; }
                    if (client.Entity.Class >= 142 && client.Entity.FirstRebornClass >= 142 && client.Entity.SecondRebornClass >= 142)
                    {
                        if (!client.Spells.ContainsKey(10310)) { client.AddSpell(LearnableMagic(10310)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {
                        if (!client.Spells.ContainsKey(1195)) { client.AddSpell(LearnableMagic(1195)); effx = true; }
                        if (!client.Spells.ContainsKey(10309)) { client.AddSpell(LearnableMagic(10309)); effx = true; }
                    }
                    if (client.Entity.Level >= 52)
                    {
                        if (!client.Spells.ContainsKey(1150)) { client.AddSpell(LearnableMagic(1150)); effx = true; }
                    }
                    if (client.Entity.Level >= 55)
                    {
                        if (!client.Spells.ContainsKey(1180)) { client.AddSpell(LearnableMagic(1180)); effx = true; }
                    }
                    if (client.Entity.Level >= 48)
                    {
                        if (!client.Spells.ContainsKey(1120)) { client.AddSpell(LearnableMagic(1120)); effx = true; }
                        if (!client.Spells.ContainsKey(1165)) { client.AddSpell(LearnableMagic(1165)); effx = true; }
                    }
                    if (client.Entity.Level >= 43)
                    {
                        if (!client.Spells.ContainsKey(1160)) { client.AddSpell(LearnableMagic(1160)); effx = true; }
                    }
                    if (client.Entity.Level >= 81)
                    {
                        if (client.Spells.ContainsKey(1001) && client.Spells[1001] != null && client.Spells[1001].Level == 3)
                        {
                            if (!client.Spells.ContainsKey(1002)) { client.AddSpell(LearnableMagic(1002)); effx = true; }
                        }
                    }
                }
                else
                {
                    if (client.Spells.ContainsKey(12380)) { client.RemoveSpell(LearnableMagic(12380)); }
                    if (client.Spells.ContainsKey(12400)) { client.RemoveSpell(LearnableMagic(12400)); }
                }
                #endregion
                #endregion
                #region Dragon-Warrior   
                if (client.Entity.Class >= 80 && client.Entity.Class <= 85)
                {
                    if (client.Entity.Class >= 80 && client.Entity.FirstRebornClass >= 85 && client.Entity.SecondRebornClass >= 85)
                    {
                        if (!client.Spells.ContainsKey(12300)) { client.AddSpell(LearnableMagic(12300)); effx = true; }
                    }
                    if (client.Entity.Level >= 3)
                    {
                        if (!client.Spells.ContainsKey(12290)) { client.AddSpell(LearnableMagic(12290)); effx = true; }
                    }
                    if (client.Entity.Level >= 15)
                    {
                        if (!client.Spells.ContainsKey(12320)) { client.AddSpell(LearnableMagic(12320)); effx = true; }
                        if (!client.Spells.ContainsKey(12330)) { client.AddSpell(LearnableMagic(12330)); effx = true; }
                        if (!client.Spells.ContainsKey(12340)) { client.AddSpell(LearnableMagic(12340)); effx = true; }
                        if (!client.Spells.ContainsKey(12270)) { client.AddSpell(LearnableMagic(12270)); effx = true; }
                    }
                    if (client.Entity.Level >= 40)
                    {//  12280   12200   
                        if (!client.Spells.ContainsKey(12240)) { client.AddSpell(LearnableMagic(12240)); effx = true; }
                        if (!client.Spells.ContainsKey(12220)) { client.AddSpell(LearnableMagic(12220)); effx = true; }
                        if (!client.Spells.ContainsKey(12210)) { client.AddSpell(LearnableMagic(12210)); effx = true; }
                        if (!client.Spells.ContainsKey(12290)) { client.AddSpell(LearnableMagic(12290)); effx = true; }
                        if (!client.Spells.ContainsKey(12120)) { client.AddSpell(LearnableMagic(12240)); effx = true; }
                        if (!client.Spells.ContainsKey(12130)) { client.AddSpell(LearnableMagic(12220)); effx = true; }
                        if (!client.Spells.ContainsKey(12140)) { client.AddSpell(LearnableMagic(12210)); effx = true; }
                    }
                    if (client.Entity.Level >= 70)
                    {
                        if (!client.Spells.ContainsKey(12160)) { client.AddSpell(LearnableMagic(12160)); effx = true; }
                        if (!client.Spells.ContainsKey(12280)) { client.AddSpell(LearnableMagic(12280)); effx = true; }
                        if (!client.Spells.ContainsKey(12200)) { client.AddSpell(LearnableMagic(12200)); effx = true; }
                    }
                    if (client.Entity.Level >= 100)
                    {
                        if (!client.Spells.ContainsKey(12170)) { client.AddSpell(LearnableMagic(12170)); effx = true; }
                        if (!client.Spells.ContainsKey(12350)) { client.AddSpell(LearnableMagic(12350)); effx = true; }

                    }
                }
                #endregion


            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
           
        }

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

        public static bool effx = false;
        public static ISkill LearnableMagic(ushort MagicID)
        {
            ISkill Magic = new Spell(true);
            Magic.ID = MagicID;
            return Magic;
        }
        public override void Dispose()
        {
            Log.Information("Security Service disposed");
            base.Dispose();
        }
    }
}
