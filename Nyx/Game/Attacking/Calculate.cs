
using System;
using Nyx.Server.Network.GamePackets;
using System.Runtime.InteropServices;
using Nyx.Server;
using Nyx.Server.Interfaces;
using System.Collections.Generic;

namespace Nyx.Server.Game.Attacking
{
    public class Calculate
    {
        public static uint Percent(Entity attacked, float percent)
        {
            return (uint)(attacked.Hitpoints * percent);
        }
        public static uint Percent(SobNpcSpawn attacked, float percent)
        {
            return (uint)(attacked.Hitpoints * percent);
        }
        public static uint Percent(int target, float percent)
        {
            return (uint)(target * percent);
        }
        public static bool RateStatus(byte value)
        {
            return (Kernel.Random.Next() % 100) < value;
        }
        public static bool GetRefinery()
        {
            return RateStatus(30);
          
        }
        public static ulong CalculateExpBonus(ushort Level, ushort MonsterLevel, ulong Experience)
        {
            int leveldiff = (2 + Level - MonsterLevel);
            if (leveldiff < -5)
                Experience = (ulong)(Experience * 1.3);
            else if (leveldiff < -1)
                Experience = (ulong)(Experience * 1.2);
            else if (leveldiff == 4)
                Experience = (ulong)(Experience * 0.8);
            else if (leveldiff == 5)
                Experience = (ulong)(Experience * 0.3);
            else if (leveldiff > 5)
                Experience = (ulong)(Experience * 0.1);
            return Experience;
        }

        internal static uint CalculateBossDamage(Entity attacker, Entity attacked, ushort SpellID)
        {
            int Damage = 0;
            Damage = MyMath.GetDamageMonster2Entity(attacker, attacked, SpellID, 0);
            Attack a = new Attack(true);
            CheckDamage(attacker, attacked, ref Damage, ref a, true);
            if (attacked.Action == Enums.ConquerAction.Sit)
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Stamina >= 20)
                        attacked.Stamina -= 20;
                }
            return (uint)Damage;
        }

        internal static uint Magic(Entity attacker, Entity attacked, Database.SpellInformation spell, ref Attack attack, byte p = 0)
        {
            int Damage = 0;

            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, spell.ID, spell.Level, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, spell.ID, spell.Level, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, true);
            if (attacked.Action == Enums.ConquerAction.Sit)
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Stamina >= 20)
                        attacked.Stamina -= 20;
                }
            return (uint)Damage;
        }

        internal static uint Melee(Entity attacker, SobNpcSpawn attackedSobNpc, ref Attack attack)
        {
            return (uint)MyMath.GetDamageEntity2Environment(attacker, attackedSobNpc, (byte)Attack.Melee, ref attack);
        }

        internal static uint Melee(Entity attacker, Entity attacked, Database.SpellInformation spell, ref Attack attack, byte p = 0)
        {
            int Damage = 0;
            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, spell.ID, spell.Level, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, spell.ID, spell.Level, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, false);
            if (attacked.Action == Enums.ConquerAction.Sit)
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Stamina >= 20)
                        attacked.Stamina -= 20;
                }
            return (uint)Damage;
        }

        internal static uint MonsterDamage(Entity attacker, Entity attacked, ref Attack attack, bool p)
        {
            if (attacker.Name == "Guard2")
                return 1;
            int Damage = 0;
            Damage = MyMath.GetDamageMonster2Entity(attacker, attacked, !p ? Attack.Melee : Attack.Magic);
            Attack a = new Attack(true);
            CheckDamage(attacker, attacked, ref Damage, ref a, p);
            if (attacked.Action == Enums.ConquerAction.Sit)
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Stamina >= 20)
                        attacked.Stamina -= 20;
                }
            return (uint)Damage;
        }

        internal static uint MonsterDamage(Entity attacker, Entity attacked, ushort spellid)
        {
            int Damage = 0;
            Damage = MyMath.GetDamageMonster2Entity(attacker, attacked, spellid, 0);
            Attack a = new Attack(true);
            CheckDamage(attacker, attacked, ref Damage, ref a, false);
            return (uint)Damage;
        }

        internal static uint Ranged(Entity attacker, Entity attacked, ref Attack attack, byte p = 0)
        {
            int Damage = 0;

            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, Attack.Ranged, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, Attack.Ranged, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, false);
            return (uint)Damage;
        }

        internal static uint Melee(Entity attacker, Entity attacked, ref Attack attack, byte p = 0)
        {
            if ((attacked.Name == "GoldenOctopus" || attacked.Name == "SilverOctopus") && attacked.EntityFlag == EntityFlag.Monster)
                return 1;
            if (attacked.EntityFlag == EntityFlag.Player && attacked.ContainsFlag3((ulong)1UL << 53))
                return 1;
            int Damage = 0;
            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, Attack.Melee, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, Attack.Melee, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, false);
            if (attacked.Action == Enums.ConquerAction.Sit)
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Stamina >= 20)
                        attacked.Stamina -= 20;
                }
            return (uint)Damage;
        }

        internal static uint Magic(Entity attacker, Entity attacked, ref Attack attack, byte p = 0)
        {
            if (attacked.Name == "GoldenOctopus" || attacked.Name == "SilverOctopus" || attacked.Name.Contains("Crystal") && attacked.EntityFlag == EntityFlag.Monster)
                return 1;
            int Damage = 0;

            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, Attack.Magic, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, Attack.Magic, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, false);
            if (attacked.Action == Enums.ConquerAction.Sit)
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Stamina >= 20)
                        attacked.Stamina -= 20;
                }
            return (uint)Damage;
        }

        internal static uint Ranged(Entity attacker, SobNpcSpawn attackedsob, ref Attack attack)
        {
            return (uint)MyMath.GetDamageEntity2Environment(attacker, attackedsob, Attack.Ranged, ref attack);
        }

        internal static uint Magic(Entity attacker, SobNpcSpawn attackedsob, Database.SpellInformation spell, ref Attack attack)
        {
            return (uint)MyMath.GetDamageEntity2Environment(attacker, attackedsob, spell.ID, spell.Level,ref attack);
        }

        internal static uint Ranged(Entity attacker, Entity attacked, Database.SpellInformation spell, ref Attack attack, byte p = 0)
        {
            if ((attacked.Name == "GoldenOctopus" || attacked.Name == "SilverOctopus") && attacked.EntityFlag == EntityFlag.Monster)
                return 1;
            int Damage = 0;
            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, spell.ID, spell.Level, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, spell.ID, spell.Level, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, false);
            return (uint)Damage;
        }

        private static void AutoRespone(Entity attacker, Entity attacked, ref int Damage)
        {
                if (attacker.EntityFlag == EntityFlag.Player)
                {
                    if (attacker.Owner.Spells.ContainsKey(11120))
                    {
                        var s = attacker.Owner.Spells[11120];
                        var spell = Database.SpellTable.SpellInformations[s.ID][s.Level];
                        if (spell != null)
                        {
                            if (Kernel.Rate(spell.Percent))
                            {
                                var ent = attacked as Entity;
                                if (!ent.IsBlackSpotted)
                                {
                                    ent.IsBlackSpotted = true;
                                    ent.BlackSpotStamp = Time32.Now;
                                    ent.BlackSpotStepSecs = spell.Duration;
                                    Kernel.BlackSpoted.TryAdd(ent.UID, ent);
                                    BlackSpotPacket bsp = new BlackSpotPacket();
                                    foreach (var h in Kernel.GamePool.Values)
                                    {
                                        h.Send(bsp.ToArray(true, ent.UID));
                                    }
                                }
                            }
                        }
                    }
                }
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacked.Owner.Spells.ContainsKey(11130) && attacked.Owner.Entity.IsEagleEyeShooted)
                    {
                        var s = attacked.Owner.Spells[11130];
                        var spell = Database.SpellTable.SpellInformations[s.ID][s.Level];
                        if (spell != null)
                        {
                            if (Kernel.Rate(spell.Percent))
                            {
                                attacked.Owner.Entity.IsEagleEyeShooted = false;
                                SpellUse ssuse = new SpellUse(true);
                                ssuse.Attacker = attacked.UID;
                                ssuse.SpellID = spell.ID;
                                ssuse.SpellLevel = spell.Level;
                                ssuse.AddTarget(attacked.Owner.Entity, new SpellUse.DamageClass().Damage = 11030, null);
                                if (attacked.EntityFlag == EntityFlag.Player)
                                {
                                    attacked.Owner.SendScreen(ssuse, true);
                                }
                            }
                        }
                    }
                    if (attacked.CounterKillSwitch && Kernel.Rate(30) && !attacker.ContainsFlag(Update.Flags.Fly) && Time32.Now > attacked.CounterKillStamp.AddSeconds(15))
                    {
                        attacked.CounterKillStamp = Time32.Now;
                        Network.GamePackets.Attack attack = new Nyx.Server.Network.GamePackets.Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = Melee(attacked, attacker, ref attack);
                        //Database.SpellInformation information = Database.SpellTable.SpellInformations[6003][attacked.Owner.Spells[6003].Level];
                        damage = damage / 3;
                        attack.Attacked = attacker.UID;
                        attack.Attacker = attacked.UID;
                        attack.AttackType = Network.GamePackets.Attack.Scapegoat;
                        attack.Damage = 0;
                        attack.ResponseDamage = damage;
                        attack.X = attacked.X;
                        attack.Y = attacked.Y;

                        if (attacker.Hitpoints <= damage)
                        {
                            if (attacker.EntityFlag == EntityFlag.Player)
                            {
                                attacked.Owner.UpdateQualifier(attacker.Hitpoints);

                                attacker.Owner.SendScreen(attack, true);
                                attacked.AttackPacket = null;
                            }
                            else
                            {
                                attacker.MonsterInfo.SendScreen(attack);
                            }
                            attacker.Die(attacked);
                        }
                        else
                        {
                            attacker.Hitpoints -= damage;
                            if (attacker.EntityFlag == EntityFlag.Player)
                            {
                                attacked.Owner.UpdateQualifier(damage);

                                attacker.Owner.SendScreen(attack, true);
                            }
                            else
                            {
                                attacker.MonsterInfo.SendScreen(attack);
                            }
                        }
                        Damage = 0;
                    }
                    else if (attacked.Owner.Spells.ContainsKey(3060) && Kernel.Rate(15))
                    {     
                        uint damage = (uint)(Damage / 10);
                        if (damage <= 0)
                            damage = 1;
                        if (damage > 10000) damage = 10000;
                        Network.GamePackets.Attack attack = new Nyx.Server.Network.GamePackets.Attack(true);
                        attack.Attacked = attacker.UID;
                        attack.Attacker = attacked.UID;
                        attack.AttackType = Network.GamePackets.Attack.Reflect;
                        attack.Damage = damage;
                        attack.ResponseDamage = damage;
                        attack.X = attacked.X;
                        attack.Y = attacked.Y;

                        if (attacker.Hitpoints <= damage)
                        {
                            if (attacker.EntityFlag == EntityFlag.Player)
                            {
                                attacked.Owner.UpdateQualifier(attacker.Hitpoints);

                                attacker.Owner.SendScreen(attack, true);
                                attacked.AttackPacket = null;
                            }
                            else
                            {
                                attacker.MonsterInfo.SendScreen(attack);
                            }
                            attacker.Die(attacked);
                        }
                        else
                        {
                            attacker.Hitpoints -= damage;
                            if (attacker.EntityFlag == EntityFlag.Player)
                            {
                                attacked.Owner.UpdateQualifier(damage);
                                attacker.Owner.SendScreen(attack, true);
                            }
                            else
                            {
                                attacker.MonsterInfo.SendScreen(attack);
                            }
                        }
                        Damage = 0;
                    }
                    else if (attacked.Owner.Spells.ContainsKey(12680) && attacked.ContainsFlag3((ulong)1UL << 51) && attacked.EntityFlag == EntityFlag.Player)
                    {
                        uint Percent = 0;
                        if (attacked.Owner.Spells[12680].Level == 0)
                        {
                            Percent = attacked.Hitpoints * 55 / 100;

                        }
                        if (attacked.Owner.Spells[12680].Level == 1)
                        {
                            Percent = attacked.Hitpoints * 60 / 100;

                        }
                        if (attacked.Owner.Spells[12680].Level == 2)
                        {
                            Percent = attacked.Hitpoints * 65 / 100;

                        }
                        if (attacked.Owner.Spells[12680].Level == 3)
                        {
                            Percent = attacked.Hitpoints * 70 / 100;

                        }
                        if (attacked.Owner.Spells[12680].Level == 4)
                        {
                            Percent = attacked.Hitpoints * 80 / 100;

                        }
                        if (attacked.Owner.Spells[12680].Level == 5)
                        {
                            Percent = attacked.Hitpoints * 85 / 100;

                        }
                        if (attacked.Owner.Spells[12680].Level == 6)
                        {
                            Percent = attacked.Hitpoints * 90 / 100;

                        }

                        uint damage = Percent;
                        if (damage <= 0)
                            damage = 1;
                        if (damage > 50000) damage = 50000;
                        Network.GamePackets.Attack attack = new Nyx.Server.Network.GamePackets.Attack(true);
                        attack.Attacked = attacker.UID;
                        attack.Attacker = attacked.UID;
                        attack.AttackType = Network.GamePackets.Attack.Reflect;
                        attack.Damage = damage;
                        attack.ResponseDamage = damage;
                        attack.X = attacked.X;
                        attack.Y = attacked.Y;

                        if (attacker.Hitpoints <= damage)
                        {
                            if (attacker.EntityFlag == EntityFlag.Player)
                            {
                                attacked.Owner.UpdateQualifier(attacker.Hitpoints);

                                attacker.Owner.SendScreen(attack, true);
                                attacked.AttackPacket = null;
                            }
                            else
                            {
                                attacker.MonsterInfo.SendScreen(attack);
                            }
                            attacker.Die(attacked);
                        }
                        else
                        {
                            attacker.Hitpoints -= damage;
                            if (attacker.EntityFlag == EntityFlag.Player)
                            {
                                attacked.Owner.UpdateQualifier(damage);
                                attacker.Owner.SendScreen(attack, true);
                            }
                            else
                            {
                                attacker.MonsterInfo.SendScreen(attack);
                            }
                        }
                        attacked.Stamina += 30;
                        attacked.RemoveFlag3((ulong)1UL << 51);
                    }
                }
            
        }
        public static void CheckDamage(Entity attacker, Entity attacked, ref int Damage, ref Attack Packet, bool magic = false)
        {
            if (attacked.EntityFlag == EntityFlag.Monster)
            {
                if (!attacker.Assassin())
                {
                    if (attacked.MonsterInfo.Boss || attacked.Boss == 1)
                    {
                        if (magic)
                            Damage = (int)Damage * 10;
                        else
                            Damage = (int)Damage * 4;
                        return;
                    }
                }
            }
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                GemEffect.Effect(attacker);
            }
            if (attacked.EntityFlag == EntityFlag.Player)
            {
                BlessEffect.Effect(attacked);
            }
            Calculate.AutoRespone(attacker, attacked, ref Damage);
            if (attacked.ContainsFlag2(Network.GamePackets.Update.Flags2.AzureShield))
            {

                if (Damage > attacked.AzureShieldDefence)
                {
                    Damage -= attacked.AzureShieldDefence;
                    CreateAzureDMG(attacked.AzureShieldDefence, attacker, attacked);
                    attacked.RemoveFlag2(Network.GamePackets.Update.Flags2.AzureShield);
                }
                else
                {
                    CreateAzureDMG((uint)Damage, attacker, attacked);
                    attacked.AzureShieldDefence -= (ushort)Damage;
                    attacked.AzureShieldPacket();
                    Damage = 1;
                }
            }
            #region DragonSwing
            if (Kernel.Rate(20))
            {
                if (attacked.ContainsFlag3(Update.Flags3.DragonSwing))
                {
                    _String str = new _String(true);
                    str.UID = attacked.UID;
                    str.TextsCount = 1;
                    str.Type = _String.Effect;
                    str.Texts.Add("poisonmiss");
                    if (attacked.EntityFlag == EntityFlag.Player)
                        attacked.Owner.SendScreen(str, true);
                    Damage = 0;
                }
            }
            #endregion
            if (attacked.ContainsFlag(Network.GamePackets.Update.Flags.ShurikenVortex))
                Damage = 1;
            
        }
        public static void CreateAzureDMG(uint dmg, Entity attacker, Entity attacked)
        {
            Network.GamePackets.Attack attac = new Attack(true);
            attac.Attacker = attacker.UID;
            attac.Attacked = attacked.UID;
            attac.X = attacked.X;
            attac.Y = attacked.Y;
            attac.AttackType = 55;
            attac.Damage = dmg;
            attacked.Owner.SendScreen(attac, true);
        }

        //public static void Refinary(Entity attacker, Entity attacked, ref double Damage, ref Attack Packet, bool magic = false)
        //{
        //    if (attacker.EntityFlag == EntityFlag.Player)
        //    {
        //        if (GetRefinery())
        //        {                
        //            {
        //                if (Kernel.Rate((double)attacker.Breaktrough / 100))
        //                {
        //                    if (attacker.Breaktrough > 0)
        //                    {
        //                        if (attacker.Breaktrough > attacked.Counteraction)
        //                        {
        //                            double Power = (double)(attacker.Breaktrough - attacked.Counteraction);
        //                            Power = (double)(Power / 100);
        //                            if (MyMath.Success(Power))
        //                                Damage += Damage * (Power / 100);
        //                        }

        //                        Packet.Effect1 |= Attack.AttackEffects1.Break;
        //                    }
        //                }
        //                else
        //                {

        //                    if (!magic)
        //                    {
        //                        if (attacker.CriticalStrike > 0)
        //                        {
        //                            if (attacker.CriticalStrike > attacked.Immunity)
        //                            {
        //                                double Power = (double)(attacker.CriticalStrike - attacked.Immunity);
        //                                Power = (double)(Power / 100);
        //                                if (MyMath.Success(Power))
        //                                    Damage += Damage * 50 / 100;
        //                                Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
        //                            }
        //                            else
        //                                Packet.Effect1 |= Attack.AttackEffects1.Immu;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (attacker.Penetration > 0)
        //                        {
        //                            double Power = (double)(attacker.Penetration / 100);
        //                            if (MyMath.Success(Power))
        //                                Damage = Damage * 50 / 100;
        //                            Packet.Effect1 |= Attack.AttackEffects1.Penetration;
        //                        }
        //                        if (attacker.SkillCStrike > 0)
        //                        {
        //                            if (attacker.SkillCStrike >= attacked.Immunity)
        //                            {
        //                                double Power = (double)(attacker.SkillCStrike - attacker.Immunity);
        //                                Power = (double)(Power / 100);
        //                                if (MyMath.Success(Power))
        //                                    Damage += Damage * 50 / 100;
        //                                Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
        //                            }

        //                        }
        //                        else
        //                            Packet.Effect1 |= Attack.AttackEffects1.Immu;
        //                    }
        //                }
        //            }                    
        //        }
        //    }
        //    if (attacked.EntityFlag == EntityFlag.Player)
        //    {
        //        if (RateStatus(25))
        //        {
        //            if (attacked.Block > 0)
        //            {
        //                double Power = (double)(attacked.Block / 100);
        //                if (MyMath.Success(Power))
        //                {
        //                    Damage = Damage / 2;
        //                    Packet.Effect1 |= Attack.AttackEffects1.Block;
        //                }
        //            }
        //            if (attacked.IsShieldBlock)
        //            {
        //                if (MyMath.Success(attacked.ShieldBlockPercent))
        //                {
        //                    Damage = Damage / 2;
        //                    Packet.Effect1 |= Attack.AttackEffects1.Block;
        //                }
        //            }
        //        }
        //    }
        //}
        public static void Immu(Entity Attacked)
        {
            if (Attacked.EntityFlag == EntityFlag.Player)
            {
                _String str = new _String(true);
                str.UID = Attacked.UID;
                str.TextsCount = 1;
                str.Type = _String.Effect;
                str.Texts.Add("bossimmunity");

                Attacked.Owner.SendScreen(str, true);
            }

        }
        public static void Refinary(Entity attacker, Entity attacked, ref double Damage, ref Attack Packet, bool magic = false)
        {
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                //if (GetRefinery())
                {
                    {
                        //if (attacked.BattlePower > attacker.BattlePower)
                        //{
                        //    if (attacker.Breaktrough > 0)
                        //    {
                        //        if (attacker.Breaktrough > attacked.Counteraction)
                        //        {
                        //            double Power = (double)(attacker.Breaktrough - attacked.Counteraction);
                        //            Power = (double)(Power / 10);
                        //            if (MyMath.Success(Power))
                        //            {
                        //                Damage += Damage * (Power / 100);
                        //                Packet.Effect1 |= Attack.AttackEffects1.Break;
                        //            }
                        //            else
                        //            {
                        //                Immu(attacked);
                        //                Damage = 0;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            Immu(attacked);
                        //            Damage = 0;
                        //        }

                        //    }
                        //}
                        if (RateStatus(100))
                        {

                            if (!magic)
                            {
                                if (attacker.CriticalStrike > 0)
                                {
                                    if (attacker.CriticalStrike > attacked.Immunity)
                                    {
                                        double Power = (double)(attacker.CriticalStrike - attacked.Immunity);
                                        if (Kernel.ChanceSuccess(((float)attacker.CriticalStrike / 400f) -
                                            ((float)attacked.Immunity / 400f)))
                                        {
                                            Damage += Damage * 50 / 100;
                                            Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
                                        }

                                        else
                                            Immu(attacked);

                                    }
                                    else
                                        Immu(attacked);
                                }
                            }
                            else
                            {
                                if (attacker.Penetration > 0)
                                {
                                    double Power = (double)(attacker.Penetration / 100);
                                    if (MyMath.Success(Power))
                                    {
                                        Damage += Damage * 50 / 100;
                                        Packet.Effect1 |= Attack.AttackEffects1.Penetration;
                                    }
                                    else if (attacker.SkillCStrike > 0)
                                    {
                                        if (attacker.SkillCStrike >= attacked.Immunity)
                                        {
                                            Power = (double)(attacker.SkillCStrike - attacked.Immunity);
                                            Power = (double)(Power / 100);
                                            if (MyMath.Success(Power))
                                            {
                                                Damage += Damage * 50 / 100;
                                                Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
                                            }
                                            else
                                                Immu(attacked);
                                        }

                                    }
                                    else
                                        Immu(attacked);
                                }
                                else if (attacker.SkillCStrike > 0)
                                {
                                    if (attacker.SkillCStrike >= attacked.Immunity)
                                    {
                                        double Power = (double)(attacker.SkillCStrike - attacked.Immunity);
                                        Power = (double)(Power / 100);
                                        if (MyMath.Success(Power))
                                        {
                                            Damage += Damage * 50 / 100;
                                            Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
                                        }
                                        else
                                            Immu(attacked);
                                    }

                                }
                                else
                                    Immu(attacked);
                            }
                        }
                    }
                }
            }
            if (attacked.EntityFlag == EntityFlag.Player)
            {
                if (RateStatus(100))
                {
                    if (attacked.Block > 0)
                    {
                        double Power = (double)(attacked.Block / 100);
                        if (MyMath.Success(Power))
                        {
                            Damage = Damage / 2;
                            Packet.Effect1 |= Attack.AttackEffects1.Block;
                        }
                    }
                    if (attacked.IsShieldBlock)
                    {
                        if (MyMath.Success(attacked.ShieldBlockPercent))
                        {
                            Damage = Damage / 2;
                            Packet.Effect1 |= Attack.AttackEffects1.Block;
                        }
                    }

                }
            }
        }
        public static void Refinary(Entity attacker, SobNpcSpawn attacked, ref double Damage, ref Attack Packet, bool magic = false)
        {
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                if (RateStatus(50))
                {

                    if (!magic)
                    {
                        if (attacker.CriticalStrike > 0)
                        {

                            double Power = (double)(attacker.CriticalStrike);
                            Power = (double)(Power / 100);
                            if (MyMath.Success(Power))
                            {
                                Damage += Damage * 50 / 100;
                                Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
                            }
                        }
                    }
                    else
                    {
                        if (attacker.Penetration > 0)
                        {
                            double Power = (double)(attacker.Penetration / 100);
                            if (MyMath.Success(Power))
                            {
                                Damage = Damage * 50 / 100;
                                Packet.Effect1 |= Attack.AttackEffects1.Penetration;
                            }

                        }
                        if (attacker.SkillCStrike > 0)
                        {
                            double Power = (double)(attacker.SkillCStrike);
                            Power = (double)(Power / 100);
                            if (MyMath.Success(Power))
                            {
                                Damage += Damage * 50 / 100;
                                Packet.Effect1 |= Attack.AttackEffects1.CriticalStrike;
                            }
                        }

                    }
                }
            }
        }

        internal static uint CloneMelee(Entity attacker, Entity attacked, ref Attack attack)
        {
            int Damage = 0;

            if (attacked.EntityFlag == EntityFlag.Monster)
                Damage = MyMath.GetDamageEntity2Monster(attacker, attacked, 2, ref attack);
            else
                Damage = MyMath.GetDamageEntity2Entity(attacker, attacked, 2, ref attack);

            CheckDamage(attacker, attacked, ref Damage, ref attack, false);
            return (uint)Damage;
        }
    }

}
namespace Nyx.Server
{
    public class StatusConstants
    {
        public const int AdjustSet = -30000,
                         AdjustFull = -32768,
                         AdjustPercent = 30000;
    }
    public class MathHelper
    {
        public static int BitFold32(int lower16, int higher16)
        {
            return (lower16) | (higher16 << 16);
        }
        public static void BitUnfold32(int bits32, out int lower16, out int upper16)
        {
            lower16 = (int)(bits32 & UInt16.MaxValue);
            upper16 = (int)(bits32 >> 16);
        }
        public static void BitUnfold64(ulong bits64, out int lower32, out int upper32)
        {
            lower32 = (int)(bits64 & UInt32.MaxValue);
            upper32 = (int)(bits64 >> 32);
        }
        public static int RoughDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }
        public static int ConquerDirection(int x1, int y1, int x2, int y2)
        {
            double angle = Math.Atan2(y2 - y1, x2 - x1);
            angle -= Math.PI / 2;

            if (angle < 0) angle += 2 * Math.PI;

            angle *= 8 / (2 * Math.PI);
            return (int)angle;
        }
        public static int MulDiv(int number, int numerator, int denominator)
        {
            return (number * numerator + denominator / 2) / denominator;
        }
        public static bool OverflowAdd(ref int acc, int add)
        {
            if (int.MaxValue - acc < add)
                return true;
            acc = Math.Max(acc + add, 0);
            return false;
        }
        public static int AdjustDataEx(int data, int adjust, int maxData = 0)
        {
            if (adjust >= StatusConstants.AdjustPercent)
                return MulDiv(data, adjust - StatusConstants.AdjustPercent, 100);

            if (adjust <= StatusConstants.AdjustSet)
                return -1 * adjust + StatusConstants.AdjustSet;

            if (adjust == StatusConstants.AdjustFull)
                return maxData;

            return data + adjust;
        }
    }
    public class GemTypes
    {
        public const int
            Phoenix = 0,
            Dragon = 1,
            Fury = 2,
            Rainbow = 3,
            Kylin = 4,
            Violet = 5,
            Moon = 6,
            Tortoise = 7,
            Thunder = 10,
            Glory = 12,

            First = Phoenix,
            Last = Glory + 1;

        public static readonly string[] Animation = new string[]
                {
                    "phoenix",
                    "goldendragon",
                    "fastflash",
                    "rainbow",
                    "goldenkylin",
                    "purpleray",
                    "moon",
                    "recovery"
                };

        public static readonly int[][] Effects = new[]
                {
                    new[] { 0, 5, 10, 15 },
                    new[] { 0, 5, 10, 15 },
                    new[] { 0, 5, 10, 15 },
                    new[] { 0, 10, 15, 25 },
                    new[] { 0, 50, 100, 200 },
                    new[] { 0, 30, 50, 100 },
                    new[] { 0, 15, 30, 50 },
                    new[] { 0, 2, 4, 6 },
                    new[] { 0, 0, 0, 0 },
                    new[] { 0, 0, 0, 0 },
                    new[] { 0, 100, 300, 500 },
                    new[] { 0, 0, 0, 0 },
                    new[] { 0, 100, 300, 500 }
                };
    }
}

