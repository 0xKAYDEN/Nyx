﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Game.ConquerStructures;
using Nyx.Server.Network;
using Nyx.Server.Interfaces;
using System.Drawing;
using Nyx.Server.Database;
using Nyx.Server.Joseph;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Game.Attacking
{
    public class Handle
    {

        private Attack attack;
        private Entity attacker, attacked;
        public Handle(Attack attack, Entity attacker, Entity attacked)
        {
            this.attack = attack;
            this.attacker = attacker;
            this.attacked = attacked;
            this.Execute();
        }
        #region Interations
        public class InteractionRequest
        {
            public InteractionRequest(Network.GamePackets.Attack attack, Game.Entity a_client)
            {
                Client.GameClient client = a_client.Owner;

                client.Entity.InteractionInProgress = false;
                client.Entity.InteractionWith = attack.Attacked;
                client.Entity.InteractionType = 0;
                client.InteractionEffect = attack.ResponseDamage;

                if (Kernel.GamePool.ContainsKey(attack.Attacked))
                {
                    Client.GameClient clienttarget = Kernel.GamePool[attack.Attacked];
                    clienttarget.Entity.InteractionInProgress = false;
                    clienttarget.Entity.InteractionWith = client.Entity.UID;
                    clienttarget.Entity.InteractionType = 0;
                    clienttarget.InteractionEffect = attack.ResponseDamage;
                    attack.Attacker = client.Entity.UID;
                    attack.X = clienttarget.Entity.X;
                    attack.Y = clienttarget.Entity.Y;
                    attack.AttackType = 46;
                    clienttarget.Send(attack);
                    clienttarget.Send(attack);
                }
            }
        }
        public class InteractionEffect
        {
            public InteractionEffect(Network.GamePackets.Attack attack, Game.Entity a_client)
            {
                Client.GameClient client = a_client.Owner;

                if (Kernel.GamePool.ContainsKey(client.Entity.InteractionWith))
                {
                    Client.GameClient clienttarget = Kernel.GamePool[client.Entity.InteractionWith];

                    if (clienttarget.Entity.X == client.Entity.X && clienttarget.Entity.Y == client.Entity.Y)
                    {
                        attack.Damage = client.Entity.InteractionType;
                        attack.ResponseDamage = client.InteractionEffect;
                        clienttarget.Entity.InteractionSet = true;
                        client.Entity.InteractionSet = true;
                        attack.Attacker = clienttarget.Entity.UID;
                        attack.Attacked = client.Entity.UID;
                        attack.AttackType = 47;
                        attack.X = clienttarget.Entity.X;
                        attack.Y = clienttarget.Entity.Y;

                        clienttarget.Send(attack);
                        attack.AttackType = 49;

                        attack.Attacker = client.Entity.UID;
                        attack.Attacked = clienttarget.Entity.UID;
                        client.SendScreen(attack, true);

                        attack.Attacker = clienttarget.Entity.UID;
                        attack.Attacked = client.Entity.UID;
                        client.SendScreen(attack, true);
                    }
                }
            }
        }
        public class InteractionAccept
        {
            public InteractionAccept(Network.GamePackets.Attack attack, Game.Entity a_client)
            {

                Client.GameClient client = a_client.Owner;
                if (client.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Ride))
                    client.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                if (client.Entity.InteractionWith != attack.Attacked)
                    return;
                attack.ResponseDamage = client.InteractionEffect;
                client.Entity.InteractionSet = false;
                if (Kernel.GamePool.ContainsKey(attack.Attacked))
                {
                    Client.GameClient clienttarget = Kernel.GamePool[attack.Attacked];
                    if (clienttarget.Entity.ContainsFlag(Network.GamePackets.Update.Flags.Ride))
                        clienttarget.Entity.RemoveFlag(Network.GamePackets.Update.Flags.Ride);
                    clienttarget.Entity.InteractionSet = false;
                    if (clienttarget.Entity.InteractionWith != client.Entity.UID)
                        return;
                    if (clienttarget.Entity.Body == 1003 || clienttarget.Entity.Body == 1004)
                    {
                        attack.Attacker = client.Entity.UID;
                        attack.X = client.Entity.X;
                        attack.Y = client.Entity.Y;
                        clienttarget.Send(attack);
                        clienttarget.Entity.InteractionInProgress = true;
                        client.Entity.InteractionInProgress = true;
                        clienttarget.Entity.InteractionType = attack.Damage;
                        clienttarget.Entity.InteractionX = client.Entity.X;
                        clienttarget.Entity.InteractionY = client.Entity.Y;
                        client.Entity.InteractionType = attack.Damage;
                        client.Entity.InteractionX = client.Entity.X;
                        client.Entity.InteractionY = client.Entity.Y;
                        if (clienttarget.Entity.X == client.Entity.X && clienttarget.Entity.Y == client.Entity.Y)
                        {
                            attack.Damage = client.Entity.InteractionType;
                            clienttarget.Entity.InteractionSet = true;
                            client.Entity.InteractionSet = true;
                            attack.Attacker = clienttarget.Entity.UID;
                            attack.Attacked = client.Entity.UID;
                            attack.AttackType = 47;
                            attack.X = clienttarget.Entity.X;
                            attack.Y = clienttarget.Entity.Y;
                            attack.ResponseDamage = clienttarget.InteractionEffect;
                            clienttarget.Send(attack);
                            attack.AttackType = 49;
                            attack.Attacker = client.Entity.UID;
                            attack.Attacked = clienttarget.Entity.UID;
                            client.SendScreen(attack, true);
                            attack.Attacker = clienttarget.Entity.UID;
                            attack.Attacked = client.Entity.UID;
                            client.SendScreen(attack, true);
                        }
                    }
                    else
                    {
                        attack.AttackType = 47;
                        attack.Attacker = client.Entity.UID;
                        attack.X = client.Entity.X;
                        attack.Y = client.Entity.Y;
                        clienttarget.Send(attack);
                        clienttarget.Entity.InteractionInProgress = true;
                        client.Entity.InteractionInProgress = true;
                        clienttarget.Entity.InteractionType = attack.Damage;
                        clienttarget.Entity.InteractionX = clienttarget.Entity.X;
                        clienttarget.Entity.InteractionY = clienttarget.Entity.Y;
                        client.Entity.InteractionType = attack.Damage;
                        client.Entity.InteractionX = clienttarget.Entity.X;
                        client.Entity.InteractionY = clienttarget.Entity.Y;
                        if (clienttarget.Entity.X == client.Entity.X && clienttarget.Entity.Y == client.Entity.Y)
                        {
                            clienttarget.Entity.InteractionSet = true;
                            client.Entity.InteractionSet = true;
                            attack.Attacker = clienttarget.Entity.UID;
                            attack.Attacked = client.Entity.UID;
                            attack.X = clienttarget.Entity.X;
                            attack.Y = clienttarget.Entity.Y;
                            attack.ResponseDamage = clienttarget.InteractionEffect;
                            clienttarget.Send(attack);
                            attack.AttackType = 49;
                            client.SendScreen(attack, true);
                            attack.Attacker = client.Entity.UID;
                            attack.Attacked = clienttarget.Entity.UID;
                            client.SendScreen(attack, true);
                        }
                    }
                }
            }
        }
        public class InteractionStopEffect
        {
            public InteractionStopEffect(Network.GamePackets.Attack attack, Game.Entity a_client)
            {
                Client.GameClient client = a_client.Owner;

                if (Kernel.GamePool.ContainsKey(attack.Attacked))
                {
                    Client.GameClient clienttarget = Kernel.GamePool[attack.Attacked];
                    attack.Attacker = client.Entity.UID;
                    attack.Attacked = clienttarget.Entity.UID;
                    attack.Damage = client.Entity.InteractionType;
                    attack.X = client.Entity.X;
                    attack.Y = client.Entity.Y;
                    attack.AttackType = 50;
                    client.SendScreen(attack, true);
                    attack.Attacker = clienttarget.Entity.UID;
                    attack.Attacked = client.Entity.UID;
                    clienttarget.SendScreen(attack, true);
                    client.Entity.Teleport(client.Entity.MapID, client.Entity.X, client.Entity.Y);
                    clienttarget.Entity.Teleport(clienttarget.Entity.MapID, clienttarget.Entity.X, clienttarget.Entity.Y);
                    client.Entity.InteractionType = 0;
                    client.Entity.InteractionWith = 0;
                    client.Entity.InteractionInProgress = false;
                    clienttarget.Entity.InteractionType = 0;
                    clienttarget.Entity.InteractionWith = 0;
                    clienttarget.Entity.InteractionInProgress = false;
                }
            }
        }
        public class InteractionRefuse
        {
            public InteractionRefuse(Network.GamePackets.Attack attack, Game.Entity a_client)
            {
                Client.GameClient client = a_client.Owner;

                client.Entity.InteractionType = 0;
                client.Entity.InteractionWith = 0;
                client.Entity.InteractionInProgress = false;

                if (Kernel.GamePool.ContainsKey(attack.Attacked))
                {
                    Client.GameClient clienttarget = Kernel.GamePool[attack.Attacked];
                    clienttarget.Entity.InteractionType = 0;
                    clienttarget.Entity.InteractionWith = 0;
                    clienttarget.Entity.InteractionInProgress = false;
                }
            }
        }
        #endregion
        public Entity findClosestTarget(Entity attacker, ushort X, ushort Y, IEnumerable<Interfaces.IMapObject> Array)
        {
            Entity closest = attacker;
            int dPrev = 10000, dist = 0;
            foreach (var _obj in Array)
            {
                if (_obj == null) continue;
                if (_obj.MapObjType != MapObjectType.Player && _obj.MapObjType != MapObjectType.Monster) continue;
                dist = Kernel.GetDistance(X, Y, _obj.X, _obj.Y);
                if (dist < dPrev)
                {
                    dPrev = dist;
                    closest = (Entity)_obj;
                }
            }
            return closest;
        }
        public static IEnumerable<Client.GameClient> PlayerinRange(Entity attacker, Entity attacked)
        {
            var dictionary = Kernel.GamePool.Values.ToArray();
            return dictionary.Where((player) => player.Entity.MapID == attacked.MapID && Kernel.GetDistance(player.Entity.X, player.Entity.Y, attacker.X, attacker.Y) <= 7);
        }
        public static void QuitSteedRace(Entity attacker)
        {
            attacker.Owner.MessageBox("Do you want to quit the steed race?", (pClient) =>
            {
                pClient.Entity.Teleport(1002, 301, 279);
                pClient.Entity.RemoveFlag(Update.Flags.Ride);
            });
        }
        public static List<ushort> GetWeaponSpell(SpellInformation spell)
        {
            return Database.SpellTable.WeaponSpells.Values.Where(p => p.Contains(spell.ID)).FirstOrDefault();

        }
        public static IEnumerable<Client.GameClient> PlayerinRange(FloorItem item, int dist)
        {
            var dictionary = Kernel.GamePool.Values.ToArray();
            return dictionary.Where((player) => player.Entity.MapID == item.MapID && Kernel.GetDistance(player.Entity.X, player.Entity.Y, item.X, item.Y) <= dist).OrderBy(player => Kernel.GetDistance(player.Entity.X, player.Entity.Y, item.X, item.Y));
        }
        public static void InfroEcho(FloorItem item)
        {
            var client = item.Owner;
            var X = item.X;
            var Y = item.Y;
            if (!client.Spells.ContainsKey(12550))
                return;
            var spell = SpellTable.GetSpell(client.Spells[12550].ID, client.Spells[12550].Level);

            var attack = new Attack(true);
            attack.Attacker = client.Entity.UID;
            attack.X = X;
            attack.Y = Y;
            attack.Damage = spell.ID;
            attack.AttackType = Attack.Magic;


            SpellUse suse = new SpellUse(true);
            suse.Attacker = client.Entity.UID;
            suse.SpellID = spell.ID;
            suse.SpellLevel = spell.Level;
            suse.X = X;
            suse.Y = Y;
            suse.SpellEffect = 1;
            for (int c = 0; c < client.Screen.Objects.Length; c++)
            {
                if (c >= client.Screen.Objects.Length)
                    break;
                Interfaces.IMapObject _obj = client.Screen.Objects[c];
                if (_obj == null)
                    continue;
                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                {
                    var attacked = _obj as Entity;
                    if (Kernel.GetDistance(X, Y, attacked.X, attacked.Y) <= spell.Range)
                    {
                        if (CanAttack(client.Entity, attacked, spell, attack.AttackType == Attack.Ranged))
                        {
                            uint damage = Game.Attacking.Calculate.Melee(client.Entity, attacked, ref attack);
                            damage = damage / 100 * 90;
                            suse.Effect1 = attack.Effect1;
                            ReceiveAttack(client.Entity, attacked, attack, ref damage, spell);
                            suse.AddTarget(attacked, damage, attack);
                            attacked.Update(_String.Effect, "twz_xldy_gj", true);
                        }
                    }
                }
            }
            client.Entity.AttackPacket = null;
            client.SendScreen(suse, true);
        }
        private void Execute()
        {
            #region interactions
            if (attack != null)
            {
                switch (attack.AttackType)
                {
                    case (uint)Network.GamePackets.Attack.InteractionRequest:
                        new InteractionRequest(attack, attacker);
                        return;
                    case (uint)Network.GamePackets.Attack.InteractionEffect:
                        new InteractionEffect(attack, attacker);
                        return;
                    case (uint)Network.GamePackets.Attack.InteractionAccept:
                        new InteractionAccept(attack, attacker);
                        return;
                    case (uint)Network.GamePackets.Attack.InteractionRefuse:
                        new InteractionRefuse(attack, attacker);
                        return;
                    case (uint)Network.GamePackets.Attack.InteractionStopEffect:
                        new InteractionStopEffect(attack, attacker);
                        return;
                }
            }
            #endregion
            #region Monster -> Player \ Monster
            if (attack == null)
            {
                if (attacker.EntityFlag != EntityFlag.Monster) return;
                if (attacker.Companion)
                {
                    if (Constants.PKForbiddenMaps.Contains(attacker.MapID)) return;
                }
                if (attacked.EntityFlag == EntityFlag.Player)
                {
                    if (!attacked.Owner.Attackable) return;
                    if (attacked.Dead) return;
                    if (attacked.Owner.ProgressBar != null)
                    {
                        attacked.Owner.ProgressBar.End(attacked.Owner);
                    }
                    #region SnowBanshee
                    if (attacker.Mesh == 951)
                    {
                        uint rand = (uint)Kernel.Random.Next(1, 4);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 30010;//IceBolt
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 30010;//IceBolt
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 30014;//FrostClaw
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 30014;//FrostClaw
                                break;
                        }
                        if (Kernel.Rate(5))
                        {
                            attacker.MonsterInfo.SpellID = 30011;//IceShock
                        }
                        if (Kernel.Rate(5))
                        {
                            attacker.MonsterInfo.SpellID = 30012;//Avalanche
                        }
                        if (Kernel.Rate(3))
                        {
                            attacker.MonsterInfo.SpellID = 30013;//RangAttack
                        }
                        #region Ice Shock
                        if (attacker.MonsterInfo.SpellID == 30013)
                        {
                            SpellUse suse = new SpellUse(true);
                            attack = new Attack(true);
                            attack.Effect1 = Attack.AttackEffects1.None;
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(7000, 15000);
                            suse.Effect1 = attack.Effect1;
                            suse.Attacker = attacker.UID;
                            suse.SpellID = 30013;
                            ushort X, Y;
                            X = Y = 0;
                            X = (ushort)(attacker.X);
                            Y = attacker.Y;
                            suse.X = X;
                            suse.Y = Y;
                            attacked.AttackPacket = null;
                            Attack m_attack = new Attack(true);
                            m_attack.Attacker = attacker.UID;
                            m_attack.Attacked = attacker.MonsterInfo.ID;
                            m_attack.X = X;
                            m_attack.Y = Y;
                            m_attack.AttackType = 52;
                            m_attack.SpellID = attacker.MonsterInfo.SpellID;
                            foreach (Client.GameClient client in Kernel.GamePool.Values)
                            {
                                if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, X, Y) < 12)
                                {
                                    if (client.Entity.ContainsFlag(Update.Flags.Dead))
                                        continue;
                                    if (attacked.Hitpoints <= damage)
                                    {
                                        attacked.Die(attacker);
                                    }
                                    else
                                    {
                                        attacked.Hitpoints -= damage;
                                    }
                                    suse.AddTarget(attacked, damage, attack);
                                }
                            }
                            attacked.Owner.SendScreen(suse, true);
                            attacker.MonsterInfo.SendScreen(m_attack);
                        }
                        #endregion
                        #region normal
                        if (attacker.MonsterInfo.SpellID == 30010 || attacker.MonsterInfo.SpellID == 30014)
                        {
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(1500, 3000);
                            if (attacked.Hitpoints <= damage)
                            {
                                attacked.Die(attacker);
                            }
                            else
                            {
                                attacked.Hitpoints -= damage;
                            }
                            if (attacker.Companion)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                            SpellUse suse = new SpellUse(true);
                            suse.Attacker = attacker.UID;
                            suse.SpellID = attacker.MonsterInfo.SpellID;
                            suse.X = attacked.X;
                            suse.Y = attacked.Y;
                            suse.AddTarget(attacked, damage, attack);
                            attacked.Owner.SendScreen(suse, true);
                        }
                        #endregion
                        #region Ice Shock
                        if (attacker.MonsterInfo.SpellID == 30011)
                        {
                            SpellUse suse = new SpellUse(true);
                            attack = new Attack(true);
                            attack.Effect1 = Attack.AttackEffects1.None;
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(2700, 5000);
                            suse.Effect1 = attack.Effect1;
                            if (attacked.Hitpoints <= damage)
                            {
                                attacked.Die(attacker);
                            }
                            else
                            {
                                attacked.Hitpoints -= damage;
                                attacked.Owner.Entity.FrozenStamp = Time32.Now;
                                attacked.Owner.Entity.FrozenTime = 3;
                                var upd = new GameCharacterUpdates(true);
                                upd.UID = attacked.UID;
                                upd.Add(GameCharacterUpdates.Flustered, 0, 3);
                                attacked.Owner.SendScreen(upd, true);
                                attacked.Owner.Entity.AddFlag(Update.Flags.Freeze);

                            }
                            if (attacker.Companion)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                            suse.Attacker = attacker.UID;
                            suse.SpellID = attacker.MonsterInfo.SpellID;
                            suse.X = attacked.X;
                            attacked.AttackPacket = null;
                            suse.Y = attacked.Y;
                            suse.AddTarget(attacked, damage, attack);
                            foreach (var obj in attacked.Owner.Screen.Objects)
                            {
                                if (Calculations.InBox(obj.X, obj.Y, attacker.X, attacker.Y, 14))
                                {
                                    if (obj.MapObjType == MapObjectType.Player)
                                    {
                                        if (obj.Owner.Entity.ContainsFlag(Update.Flags.Dead))
                                            continue;
                                        attacked = obj as Entity;
                                        if (attacked.Hitpoints <= damage)
                                        {
                                            attacked.Die(attacker);
                                        }
                                        else
                                        {
                                            attacked.Hitpoints -= damage;
                                            attacked.Owner.Entity.FrozenStamp = Time32.Now;
                                            attacked.Owner.Entity.FrozenTime = 3;
                                            var upd = new GameCharacterUpdates(true);
                                            upd.UID = attacked.UID;
                                            upd.Add(GameCharacterUpdates.Freeze, 0, 3);
                                            attacked.Owner.SendScreen(upd, true);
                                            attacked.Owner.Entity.AddFlag(Update.Flags.Freeze);
                                        }

                                        suse.AddTarget(attacked, damage, attack);
                                        attacked.Owner.SendScreen(suse, true);
                                    }
                                }
                            }
                            attacked.Owner.SendScreen(suse, true);
                        }
                        #endregion
                        #region Avalanche
                        if (attacker.MonsterInfo.SpellID == 30012)
                        {
                            SpellUse suse = new SpellUse(true);
                            attack = new Attack(true);
                            attack.Effect1 = Attack.AttackEffects1.None;
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(2700, 5000);
                            suse.Effect1 = attack.Effect1;
                            if (attacked.Hitpoints <= damage)
                            {
                                attacked.Die(attacker);
                            }
                            else
                            {
                                attacked.Hitpoints -= damage;
                                attacked.Owner.Entity.FrozenStamp = Time32.Now;
                                attacked.Owner.Entity.FrozenTime = 1;
                                GameCharacterUpdates update = new GameCharacterUpdates(true);
                                update.UID = attacked.UID;
                                update.Add(GameCharacterUpdates.Freeze, 0, 1);
                                attacked.Owner.SendScreen(update, true);
                                attacked.AddFlag(Update.Flags.Freeze);
                            }
                            if (attacker.Companion)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                            suse.Attacker = attacker.UID;
                            suse.SpellID = attacker.MonsterInfo.SpellID;
                            suse.X = attacked.X;
                            attacked.AttackPacket = null;
                            suse.Y = attacked.Y;
                            suse.AddTarget(attacked, damage, attack);
                            foreach (var obj in attacked.Owner.Screen.Objects)
                            {
                                if (Calculations.InBox(obj.X, obj.Y, attacker.X, attacker.Y, 10))
                                {
                                    if (obj.MapObjType == MapObjectType.Player)
                                    {
                                        if (obj.Owner.Entity.ContainsFlag(Update.Flags.Dead))
                                            continue;
                                        attacked = obj as Entity;
                                        if (attacked.Hitpoints <= damage)
                                        {
                                            attacked.Die(attacker);
                                        }
                                        else
                                        {
                                            attacked.Hitpoints -= damage;
                                            attacked.Owner.Entity.FrozenStamp = Time32.Now;
                                            attacked.Owner.Entity.FrozenTime = 1;
                                            GameCharacterUpdates update = new GameCharacterUpdates(true);
                                            update.UID = attacked.UID;
                                            update.Add(GameCharacterUpdates.Freeze, 0, 1);
                                            attacked.Owner.SendScreen(update, true);
                                            attacked.AddFlag(Update.Flags.Freeze);
                                        }

                                        suse.AddTarget(attacked, damage, attack);
                                    }
                                }
                            }
                            attacked.Owner.SendScreen(suse, true);
                        }
                        #endregion
                    }
                    #endregion
                    #region WaterMonsters
                    if (attacker.Mesh == 273)
                    {
                        uint rand = (uint)Nyx.Server.Kernel.Random.Next(1, 5);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 11313;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 30012;
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 11313;
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 11313;
                                break;
                            case 5:
                                attacker.MonsterInfo.SpellID = 11313;
                                break;
                        }
                        #region AngerCrop
                        if (attacker.MonsterInfo.SpellID == 30012 && Kernel.Rate(30))
                        {
                            SpellUse suse = new SpellUse(true);
                            attack = new Attack(true);
                            attack.Effect1 = Attack.AttackEffects1.None;
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(5000, 5000);
                            suse.Effect1 = attack.Effect1;
                            if (attacked.Hitpoints <= damage)
                            {
                                attacked.Die(attacker);
                            }
                            else
                            {
                                attacked.Hitpoints -= damage;
                                attacked.Owner.Entity.FrozenStamp = Time32.Now;
                                attacked.Owner.Entity.FrozenTime = 5;
                                GameCharacterUpdates update = new GameCharacterUpdates(true);
                                update.UID = attacked.UID;
                                update.Add(GameCharacterUpdates.Freeze, 0, 5);
                                attacked.Owner.SendScreen(update, true);
                                attacked.AddFlag((ulong)Update.Flags.Freeze);
                            }
                            if (attacker.Companion)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                            suse.Attacker = attacker.UID;
                            suse.SpellID = attacker.MonsterInfo.SpellID;
                            suse.X = attacked.X;
                            attacked.AttackPacket = null;
                            suse.Y = attacked.Y;
                            suse.AddTarget(attacked, damage, attack);
                            foreach (var obj in attacked.Owner.Screen.Objects)
                            {
                                if (Calculations.InBox(obj.X, obj.Y, attacker.X, attacker.Y, 10))
                                {
                                    if (obj.MapObjType == MapObjectType.Player)
                                    {
                                        if (obj.Owner.Entity.ContainsFlag((ulong)Update.Flags.Dead))
                                            continue;
                                        attacked = obj as Entity;
                                        if (attacked.Hitpoints <= damage)
                                        {
                                            attacked.Die(attacker);
                                        }
                                        else
                                        {
                                            attacked.Hitpoints -= damage;
                                            attacked.Owner.Entity.FrozenStamp = Time32.Now;
                                            attacked.Owner.Entity.FrozenTime = 5;
                                            GameCharacterUpdates update = new GameCharacterUpdates(true);
                                            update.UID = attacked.UID;
                                            update.Add(GameCharacterUpdates.Freeze, 0, 5);
                                            attacked.Owner.SendScreen(update, true);
                                            attacked.AddFlag((ulong)Update.Flags.Freeze);
                                        }

                                        suse.AddTarget(attacked, damage, attack);
                                    }
                                }
                            }
                            attacked.Owner.SendScreen(suse, true);
                        }
                        else
                        {
                            SpellUse suse = new SpellUse(true);
                            attack = new Attack(true);
                            attack.Effect1 = Attack.AttackEffects1.None;
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(347, 347);
                            suse.Effect1 = attack.Effect1;
                            if (attacked.Hitpoints <= damage)
                            {
                                attacked.Die(attacker);
                            }
                            if (attacker.Companion)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                            suse.Attacker = attacker.UID;
                            suse.SpellID = attacker.MonsterInfo.SpellID;
                            suse.X = attacked.X;
                            attacked.AttackPacket = null;
                            suse.Y = attacked.Y;
                            suse.AddTarget(attacked, damage, attack);
                            attacked.Owner.SendScreen(suse, true);
                        }
                        #endregion
                    }
                    #endregion
                    #region NemesisTyrant
                    if (attacker.Mesh == 984)
                    {
                        uint damage = 0;
                        uint rand = (uint)Kernel.Random.Next(1, 5);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 12061;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 12062;;
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 12064;
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 12066;
                                break;
                             case 5:
                                attacker.MonsterInfo.SpellID = 12066;
                                break;
                        }
                        if (Kernel.Rate(5))
                        {
                            attacker.MonsterInfo.SpellID = 12063;
                            damage = (uint)Kernel.Random.Next(40000, 50000);
                        }
                        if (damage == 0)
                            damage += (uint)Kernel.Random.Next(5000, 20000);
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        SpellUse suse = new SpellUse(true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    #region Kylin(Realm)
                    if (attacker.Mesh == 788)
                    {
                        uint rand = (uint)Kernel.Random.Next(1, 2);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 1150;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 1150;
                                break;
                        }
                    }
                    #endregion
                    #region LavaBeast
                    if (attacker.Mesh == 251 && attacker.Name != "HeadlessSoldier")
                    {
                        uint rand = (uint)Nyx.Server.Kernel.Random.Next(1, 7);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 10000;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 10001;
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 10003;
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 10000;
                                break;
                            case 5:
                                attacker.MonsterInfo.SpellID = 10000;
                                break;
                            case 6:
                                {
                                    attacker.MonsterInfo.SpellID = 7015;
                                    attacked.Owner.FrightenStamp = Time32.Now;
                                    var upd = new GameCharacterUpdates(true);
                                    upd.UID = attacked.UID;
                                    upd.Add(GameCharacterUpdates.Flustered, 0, 5);
                                    attacked.Owner.SendScreen(upd, true);
                                    attacked.Owner.Entity.AddFlag((ulong)Update.Flags.ChaosCycle);
                                    break;
                                }
                            case 7:
                                attacker.MonsterInfo.SpellID = 10000;
                                break;
                        }
                        uint damage = 0;
                        damage += (uint)Kernel.Random.Next(2400, 5500);
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        SpellUse suse = new SpellUse(true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                            
                    }
                    #endregion
                    #region TeratoDragon
                    if (attacker.Mesh == 950)
                    {
                        uint rand = (uint)Nyx.Server.Kernel.Random.Next(1, 7);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 7014;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 7017;
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 7017;
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 7012;
                                break;
                            case 5:
                                attacker.MonsterInfo.SpellID = 7013;
                                break;
                            case 6:
                                attacker.MonsterInfo.SpellID = 7015;
                                break;
                            case 7:
                                attacker.MonsterInfo.SpellID = 7016;
                                break;
                        }
                        #region TD Area
                        if (attacker.MonsterInfo.SpellID == 7014 || attacker.MonsterInfo.SpellID == 7017)
                        {
                            SpellUse suse = new SpellUse(true);
                            attack = new Attack(true);
                            attack.Effect1 = Attack.AttackEffects1.None;
                            uint damage = 0;
                            damage += (uint)Kernel.Random.Next(2500, 6000);
                            suse.Effect1 = attack.Effect1;
                            if (attacked.Hitpoints <= damage)
                            {
                                attacked.Die(attacker);
                            }
                            else
                            {
                                attacked.Hitpoints -= damage;
                            }
                            if (attacker.Companion)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                            suse.Attacker = attacker.UID;
                            suse.SpellID = attacker.MonsterInfo.SpellID;
                            suse.X = attacked.X;
                            suse.Y = attacked.Y;
                            attacked.AttackPacket = null;
                            suse.AddTarget(attacked, damage, attack);
                            foreach (var obj in attacked.Owner.Screen.Objects)
                            {
                                if (Calculations.InBox(obj.X, obj.Y, attacker.X, attacker.Y, 14))
                                {
                                    if (obj.MapObjType == MapObjectType.Player)
                                    {
                                        if (obj.Owner.Entity.ContainsFlag((ulong)Update.Flags.Dead))
                                            continue;
                                        attacked = obj as Entity;
                                        if (attacked.Hitpoints <= damage)
                                        {
                                            attacked.Die(attacker);
                                        }
                                        else
                                        {
                                            attacked.Hitpoints -= damage;
                                        }

                                        suse.AddTarget(attacked, damage, attack);
                                    }
                                }
                            }
                            attacked.Owner.SendScreen(suse, true);
                        }
                    }
                        #endregion
                    #region 2nd skill
                    if (attacker.MonsterInfo.SpellID == 7013)
                    {
                        SpellUse suse = new SpellUse(true);
                        attack = new Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = 0;
                        damage += (uint)Kernel.Random.Next(2500, 6000);
                        suse.Effect1 = attack.Effect1;
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                            attacked.Owner.FrightenStamp = Time32.Now;
                            attacked.Owner.Entity.Fright = 5;
                            var upd = new GameCharacterUpdates(true);
                            upd.UID = attacked.UID;
                            upd.Add(GameCharacterUpdates.Dizzy, 0, 5);
                            attacked.Owner.SendScreen(upd, true);
                            attacked.Owner.Entity.AddFlag((ulong)Update.Flags.FreezeSmall);
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        attacked.AttackPacket = null;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        foreach (var obj in attacked.Owner.Screen.Objects)
                        {
                            if (Calculations.InBox(obj.X, obj.Y, attacker.X, attacker.Y, 14))
                            {
                                if (obj.MapObjType == MapObjectType.Player)
                                {
                                    if (obj.Owner.Entity.ContainsFlag((ulong)Update.Flags.Dead))
                                        continue;
                                    attacked = obj as Entity;
                                    if (attacked.Hitpoints <= damage)
                                    {
                                        attacked.Die(attacker);
                                    }
                                    else
                                    {
                                        attacked.Hitpoints -= damage;
                                        attacked.Owner.FrightenStamp = Time32.Now;
                                        attacked.Owner.Entity.Fright = 5;
                                        var upd = new GameCharacterUpdates(true);
                                        upd.UID = attacked.UID;
                                        upd.Add(GameCharacterUpdates.Dizzy, 0, 5);
                                        attacked.Owner.SendScreen(upd, true);
                                        attacked.Owner.Entity.AddFlag((ulong)Update.Flags.FreezeSmall);
                                    }

                                    suse.AddTarget(attacked, damage, attack);
                                }
                            }
                        }
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    #region Chill
                    if (attacker.MonsterInfo.SpellID == 7015)
                    {
                        SpellUse suse = new SpellUse(true);
                        attack = new Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = 0;
                        damage += (uint)Kernel.Random.Next(2500, 6000);
                        suse.Effect1 = attack.Effect1;
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                            attacked.Owner.FrightenStamp = Time32.Now;
                            var upd = new GameCharacterUpdates(true);
                            upd.UID = attacked.UID;
                            upd.Add(GameCharacterUpdates.Flustered, 0, 5);
                            attacked.Owner.SendScreen(upd, true);
                            attacked.Owner.Entity.AddFlag((ulong)Update.Flags.ChaosCycle);

                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);

                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        attacked.AttackPacket = null;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        foreach (var obj in attacked.Owner.Screen.Objects)
                        {
                            if (Calculations.InBox(obj.X, obj.Y, attacker.X, attacker.Y, 14))
                            {
                                if (obj.MapObjType == MapObjectType.Player)
                                {
                                    if (obj.Owner.Entity.ContainsFlag((ulong)Update.Flags.Dead))
                                        continue;
                                    attacked = obj as Entity;
                                    if (attacked.Hitpoints <= damage)
                                    {
                                        attacked.Die(attacker);
                                    }
                                    else
                                    {
                                        attacked.Hitpoints -= damage;
                                        attacked.Owner.FrightenStamp = Time32.Now;
                                        var upd = new GameCharacterUpdates(true);
                                        upd.UID = attacked.UID;
                                        upd.Add(GameCharacterUpdates.Flustered, 0, 5);
                                        attacked.Owner.SendScreen(upd, true);
                                        attacked.Owner.Entity.AddFlag((ulong)Update.Flags.ChaosCycle);
                                    }

                                    suse.AddTarget(attacked, damage, attack);
                                }
                            }
                        }
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    #endregion
                    #region SwordMaster
                    if (attacker.Mesh == 953)
                    {
                        uint rand = (uint)Nyx.Server.Kernel.Random.Next(1, 5);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 10502;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 10504;
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 10506;
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 10503;
                                break;
                            case 5:
                                attacker.MonsterInfo.SpellID = 10369;
                                break;
                        }
                        uint damage = 0;
                        damage += (uint)Kernel.Random.Next(2000, 4500);

                        if (attacked.Hitpoints <= damage)
                            attacked.Die(attacker);
                        else
                            attacked.Hitpoints -= damage;
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        SpellUse suse = new SpellUse(true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    #region ThrillingSpook
                    if (attacker.Mesh == 952)
                    {
                        uint rand = (uint)Kernel.Random.Next(1,9);
                        switch (rand)
                        {
                            case 1:
                                attacker.MonsterInfo.SpellID = 11313;
                                break;
                            case 2:
                                attacker.MonsterInfo.SpellID = 10360;
                                break;
                            case 3:
                                attacker.MonsterInfo.SpellID = 12640;
                                break;
                            case 4:
                                attacker.MonsterInfo.SpellID = 12638;
                                break;
                            case 5:
                                attacker.MonsterInfo.SpellID = 12638;
                                break;
                            case 6:
                                attacker.MonsterInfo.SpellID = 12638;
                                break;
                            case 7:
                                attacker.MonsterInfo.SpellID = 11313;
                                break;
                            case 8:
                            case 9:
                                attacker.MonsterInfo.SpellID = 10362;
                                break;
                        }
                        uint damage = 0;
                        damage += (uint)Kernel.Random.Next(2400, 5500);
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        SpellUse suse = new SpellUse(true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    #region Guard1
                    if (attacker.Name == "Guard1")
                    {
                        uint damage = 0;
                        damage = (uint)Kernel.Random.Next(50000, 60000);
                        if (attacked == null)
                            return;
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        SpellUse suse = new SpellUse(true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = 0;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    #region Guard2
                    if (attacker.Name == "Guard2")
                    {
                        uint damage = 0;
                        damage = 1;
                        if (attacked == null)
                            return;
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        SpellUse suse = new SpellUse(true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = 0;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                    }
                    #endregion
                    attack = new Attack(true);
                    attack.Effect1 = Attack.AttackEffects1.None;
                    attack = new Attack(true);
                    attack.Attacker = attacker.UID;
                    attack.Attacked = attacker.MonsterInfo.ID;
                    attack.X = attacked.X;
                    attack.Y = attacked.Y;
                    attack.AttackType = 52;
                    attack.SpellID = attacker.MonsterInfo.SpellID;
                    attacker.MonsterInfo.SendScreen(attack);
                    if (attacker.MonsterInfo.SpellID == 0)
                    {
                        attack = new Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = Calculate.MonsterDamage(attacker, attacked, ref attack, false);
                        attack.Attacker = attacker.UID;
                        attack.Attacked = attacked.UID;
                        attack.AttackType = Attack.Melee;
                        attack.Damage = damage;
                        attack.X = attacked.X;
                        attack.Y = attacked.Y;
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Owner.SendScreen(attack, true);
                            attacked.Die(attacker.UID);
                            if (attacked.InAutoHunt)
                            {
                                AutoHunt AutoHunt = new AutoHunt();
                                AutoHunt.Action = AutoHunt.Mode.KilledBy;
                                AutoHunt.Unknown = 3329;
                                AutoHunt.KilledName = attacker.MonsterInfo.Name;
                                AutoHunt.EXPGained = attacked.AutoHuntEXP;
                                attacked.Owner.Send(AutoHunt.Encode());
                                attacked.Owner.IncreaseExperience(attacked.AutoHuntEXP, false);
                                attacked.AutoHuntEXP = 0;
                                attacked.AutoRevStamp = Time32.Now;
                                attacked.AutoRev = 20;
                            }
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                            attacked.Owner.SendScreen(attack, true);
                        }
                    }
                    else
                    {
                        SpellUse suse = new SpellUse(true);
                        attack = new Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = Calculate.MonsterDamage(attacker, attacked, attacker.MonsterInfo.SpellID);
                        suse.Effect1 = attack.Effect1;
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker.UID);
                            if (attacked.InAutoHunt)
                            {
                                AutoHunt AutoHunt = new AutoHunt();
                                AutoHunt.Action = AutoHunt.Mode.KilledBy;
                                AutoHunt.Unknown = 3329;
                                AutoHunt.KilledName = attacker.MonsterInfo.Name;
                                AutoHunt.EXPGained = attacked.AutoHuntEXP;
                                attacked.Owner.Send(AutoHunt.Encode());
                                attacked.Owner.IncreaseExperience(attacked.AutoHuntEXP, false);
                                attacked.AutoHuntEXP = 0;
                                attacked.AutoRevStamp = Time32.Now;
                                attacked.AutoRev = 20;
                            }
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                        if (attacker.Companion)
                            attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.Owner.SendScreen(suse, true);
                    }
                }
                else
                {
                    if (attacker.MonsterInfo.SpellID == 0)
                    {
                        attack = new Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = Calculate.MonsterDamage(attacker, attacked, ref attack, false);
                        attack.Attacker = attacker.UID;
                        attack.Attacked = attacked.UID;
                        attack.AttackType = Attack.Melee;
                        attack.Damage = damage;
                        attack.X = attacked.X;
                        attack.Y = attacked.Y;
                        attacked.MonsterInfo.SendScreen(attack);
                        if (attacker.Companion)
                            if (damage > attacked.Hitpoints)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                            else attacker.Owner.IncreaseExperience(damage, true);
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                            attack = new Attack(true);
                            attack.Attacker = attacker.UID;
                            attack.Attacked = attacked.UID;
                            attack.AttackType = Network.GamePackets.Attack.Kill;
                            attack.X = attacked.X;
                            attack.Y = attacked.Y;
                            attacked.MonsterInfo.SendScreen(attack);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                    }
                    else
                    {
                        SpellUse suse = new SpellUse(true);
                        attack = new Attack(true);
                        attack.Effect1 = Attack.AttackEffects1.None;
                        uint damage = Calculate.MonsterDamage(attacker, attacked, attacker.MonsterInfo.SpellID);
                        suse.Effect1 = attack.Effect1;
                        suse.Attacker = attacker.UID;
                        suse.SpellID = attacker.MonsterInfo.SpellID;
                        suse.X = attacked.X;
                        suse.Y = attacked.Y;
                        suse.AddTarget(attacked, damage, attack);
                        attacked.MonsterInfo.SendScreen(suse);
                        if (attacker.Companion)
                            if (damage > attacked.Hitpoints)
                                attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                            else attacker.Owner.IncreaseExperience(damage, true);
                        if (attacked.Hitpoints <= damage)
                        {
                            attacked.Die(attacker);
                            attack = new Attack(true);
                            attack.Attacker = attacker.UID;
                            attack.Attacked = attacked.UID;
                            attack.AttackType = Attack.Kill;
                            attack.X = attacked.X;
                            attack.Y = attacked.Y;
                            attacked.MonsterInfo.SendScreen(attack);
                        }
                        else
                        {
                            attacked.Hitpoints -= damage;
                        }
                    }
                }
            }
            #endregion
            #region Player -> Player \ Monster \ Sob Npc
            else
            {
                #region Merchant
                if (attack.AttackType == Attack.MerchantAccept || attack.AttackType == Attack.MerchantRefuse)
                {

                    attacker.AttackPacket = null;
                    return;
                }
                #endregion
                #region Marriage
                if (attack.AttackType == Attack.MarriageAccept || attack.AttackType == Attack.MarriageRequest)
                {
                    if (attack.AttackType == Attack.MarriageRequest)
                    {
                        Client.GameClient Spouse = null;
                        uint takeout = attack.Attacked;
                        if (takeout == attacker.UID)
                            takeout = attack.Attacker;
                        if (Kernel.GamePool.TryGetValue(takeout, out Spouse))
                        {
                            if (attacker.Spouse != "None" || Spouse.Entity.Spouse != "None")
                            {
                                attacker.Owner.Send(new Message("You cannot marry someone that is already married with someone else!", System.Drawing.Color.Black, Message.TopLeft));
                            }
                            else
                            {
                                uint id1 = attacker.Mesh % 10, id2 = Spouse.Entity.Mesh % 10;

                                if (id1 <= 2 && id2 >= 3 || id1 >= 2 && id2 <= 3)
                                {

                                    attack.X = Spouse.Entity.X;
                                    attack.Y = Spouse.Entity.Y;

                                    Spouse.Send(attack);
                                }
                                else
                                {
                                    attacker.Owner.Send(new Message("You cannot marry someone of your gender!", System.Drawing.Color.Black, Message.TopLeft));
                                }
                            }
                        }
                    }
                    else
                    {
                        Client.GameClient Spouse = null;
                        if (Kernel.GamePool.TryGetValue(attack.Attacked, out Spouse))
                        {
                            if (attacker.Spouse != "None" || Spouse.Entity.Spouse != "None")
                            {
                                attacker.Owner.Send(new Message("You cannot marry someone that is already married with someone else!", System.Drawing.Color.Black, Message.TopLeft));
                            }
                            else
                            {
                                if (attacker.Mesh % 10 <= 2 && Spouse.Entity.Mesh % 10 >= 3 || attacker.Mesh % 10 >= 3 && Spouse.Entity.Mesh % 10 <= 2)
                                {
                                    Spouse.Entity.Spouse = attacker.Name;
                                    attacker.Spouse = Spouse.Entity.Name;
                                    Message message = null;
                                    if (Spouse.Entity.Mesh % 10 >= 3)
                                        message = new Message("Joy and happiness! " + Spouse.Entity.Name + " and " + attacker.Name + " have joined together in the holy marriage. We wish them a stone house.", System.Drawing.Color.BurlyWood, Message.Center);
                                    else
                                        message = new Message("Joy and happiness! " + attacker.Name + " and " + attacker.Spouse + " have joined together in the holy marriage. We wish them a stone house.", System.Drawing.Color.BurlyWood, Message.Center);

                                    foreach (Client.GameClient client in Kernel.GamePool.Values)
                                    {
                                        client.Send(message);
                                    }

                                    Spouse.Entity.Update(_String.Effect, "firework-2love", true);
                                    attacker.Update(_String.Effect, "firework-2love", true);
                                }
                                else
                                {
                                    attacker.Owner.Send(new Message("You cannot marry someone of your gender!", System.Drawing.Color.Black, Message.TopLeft));
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Attacking
                else
                {
                    attacker.Owner.Attackable = true;
                    Entity attacked = null;

                    SobNpcSpawn attackedsob = null;
                    FloorItem attackedItem = null;
                    #region Checks
                    if (attack.Attacker != attacker.UID)
                        return;
                    if (attacker.EntityFlag != EntityFlag.Player)
                        return;
                    attacker.RemoveFlag(Update.Flags.Invisibility);

                    bool pass = false;
                    if (attack.AttackType == Attack.Melee)
                    {
                        if (attacker.OnFatalStrike())
                        {
                            if (attack.Attacked < 600000)
                            {
                                pass = true;
                            }
                        }
                    }
                    ushort decrease = 0;
                    if (attacker.OnCyclone())
                        decrease = 1;

                    if (attacker.OnSuperman())
                        decrease = 300;
                    if (attacker.Dead)
                    {
                        if (attacker.AttackPacket != null)
                            attacker.AttackPacket = null;
                        return;
                    }
                    if (attacker.Owner.InQualifier())
                    {
                        if (Time32.Now < attacker.Owner.ImportTime().AddSeconds(12))
                        {
                            return;
                        }
                    }

                    bool doWep1Spell = false, doWep2Spell = false;
                restart:

                    #region Extract attack information
                    ushort SpellID = 0, X = 0, Y = 0;
                    uint Target = 0;
                    if (attack.AttackType == Attack.Magic)
                    {
                        if (!attack.Decoded)
                        {
                            #region GetSkillID
                            SpellID = Convert.ToUInt16(((long)attack.Encode()[28] & 0xFF) | (((long)attack.Encode()[29] & 0xFF) << 8));
                            SpellID ^= (ushort)0x915d;
                            SpellID ^= (ushort)attacker.UID;
                            SpellID = (ushort)(SpellID << 0x3 | SpellID >> 0xd);
                            SpellID -= 0xeb42;
                            #endregion
                            #region GetCoords
                            X = (ushort)((attack.Encode()[20] & 0xFF) | ((attack.Encode()[21] & 0xFF) << 8));
                            X = (ushort)(X ^ (uint)(attacker.UID & 0xffff) ^ 0x2ed6);
                            X = (ushort)(((X << 1) | ((X & 0x8000) >> 15)) & 0xffff);
                            X = (ushort)((X | 0xffff0000) - 0xffff22ee);

                            Y = (ushort)((attack.Encode()[22] & 0xFF) | ((attack.Encode()[23] & 0xFF) << 8));
                            Y = (ushort)(Y ^ (uint)(attacker.UID & 0xffff) ^ 0xb99b);
                            Y = (ushort)(((Y << 5) | ((Y & 0xF800) >> 11)) & 0xffff);
                            Y = (ushort)((Y | 0xffff0000) - 0xffff8922);
                            #endregion
                            #region GetTarget
                            Target = ((uint)attack.Encode()[16] & 0xFF) | (((uint)attack.Encode()[17] & 0xFF) << 8) | (((uint)attack.Encode()[18] & 0xFF) << 16) | (((uint)attack.Encode()[19] & 0xFF) << 24);
                            Target = ((((Target & 0xffffe000) >> 13) | ((Target & 0x1fff) << 19)) ^ 0x5F2D2463 ^ attacker.UID) - 0x746F4AE6;
                            #endregion

                            attack.X = X;
                            attack.Y = Y;
                            attack.Damage = SpellID;
                            attack.Attacked = Target;
                            attack.Decoded = true;
                        }
                        else
                        {
                            X = attack.X;
                            Y = attack.Y;
                            SpellID = (ushort)attack.Damage;
                            Target = attack.Attacked;
                        }
                    }
                    #endregion

                    if (!(doWep1Spell || doWep2Spell))
                    {
                        if (SpellID == 1045 || SpellID == 1046 || SpellID == 11005 || SpellID == 11000 || SpellID == 1100) // FB and SS
                        {
                            //do checks
                        }
                        else
                        {
                            int milliSeconds = 1000 - attacker.Agility - decrease;
                            if (milliSeconds < 0 || milliSeconds > 5000)
                                milliSeconds = 0;
                            if (Time32.Now < attacker.AttackStamp.AddMilliseconds(milliSeconds))
                                return;
                        }

                        attacker.AttackStamp = Time32.Now;

                    }
                    #endregion


                    if (attacker.ContainsFlag(Update.Flags.Ride) && attacker.Owner.Equipment.TryGetItem(18) == null)
                    {
                        if (attack.AttackType != Attack.Magic)
                            attacker.RemoveFlag(Update.Flags.Ride);
                        else
                            if (!(SpellID == 7003 || SpellID == 7002))
                                attacker.RemoveFlag(Update.Flags.Ride);
                    }
                    if (attacker.ContainsFlag(Update.Flags.CastPray))
                        attacker.RemoveFlag(Update.Flags.CastPray);
                    if (attacker.ContainsFlag(Update.Flags.Praying))
                        attacker.RemoveFlag(Update.Flags.Praying);

                    #region Dash
                    if (SpellID == 1051)
                    {

                        if (Kernel.GetDistance(attack.X, attack.Y, attacker.X, attacker.Y) > 4)
                        {
                            attacker.Owner.Disconnect();
                            return;
                        }
                        attacker.X = attack.X; attacker.Y = attack.Y;
                        ushort x = attacker.X, y = attacker.Y;
                        Game.Map.UpdateCoordonatesForAngle(ref x, ref y, (Enums.ConquerAngle)Target);
                        foreach (Interfaces.IMapObject obj in attacker.Owner.Screen.Objects)
                        {
                            if (obj == null)
                                continue;
                            if (obj.X == x && obj.Y == y && (obj.MapObjType == MapObjectType.Monster || obj.MapObjType == MapObjectType.Player))
                            {
                                Entity entity = obj as Entity;
                                if (!entity.Dead)
                                {
                                    Target = obj.UID;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                    #region CounterKill
                    if (attack.AttackType == Attack.CounterKillSwitch)
                    {

                        if (attacked != null)
                            if (attacked.ContainsFlag(Update.Flags.Fly)) { attacker.AttackPacket = null; return; }
                        if (attacker != null)
                            if (attacker.ContainsFlag(Update.Flags.Fly)) { attacker.AttackPacket = null; return; }
                        if (attacker.Owner.Spells.ContainsKey(6003))
                        {
                            if (!attacker.CounterKillSwitch)
                            {
                                if (Time32.Now >= attacker.CounterKillStamp.AddSeconds(30))
                                {
                                    attacker.CounterKillStamp = Time32.Now;
                                    attacker.CounterKillSwitch = true;
                                    Attack m_attack = new Attack(true);
                                    m_attack.Attacked = attacker.UID;
                                    m_attack.Attacker = attacker.UID;
                                    m_attack.AttackType = Attack.CounterKillSwitch;
                                    m_attack.Damage = 1;
                                    m_attack.X = attacker.X;
                                    m_attack.Y = attacker.Y;
                                    m_attack.Send(attacker.Owner);
                                }
                            }
                            else
                            {
                                attacker.CounterKillSwitch = false;
                                Attack m_attack = new Attack(true);
                                m_attack.Attacked = attacker.UID;
                                m_attack.Attacker = attacker.UID;
                                m_attack.AttackType = Attack.CounterKillSwitch;
                                m_attack.Damage = 0;
                                m_attack.X = attacker.X;
                                m_attack.Y = attacker.Y;
                                m_attack.Send(attacker.Owner);
                            }

                            attacker.Owner.IncreaseSpellExperience(100, 6003);
                            attacker.AttackPacket = null;
                        }
                    }
                    #endregion
                    #region Melee

                    else if (attack.AttackType == Attack.Melee)
                    {
                        if (attacker.Assassin() || attacker.IsBowEquipped)
                        {
                            attack.AttackType = Attack.Ranged;
                            new Game.Attacking.Handle(attack, attacker, attacked);
                            return;
                        }
                        if (attacker.Owner.Screen.TryGetValue(attack.Attacked, out attacked))
                        {
                            if (attacked.Owner != null && attacked.Owner.ProgressBar != null)
                            {
                                attacked.Owner.ProgressBar.End(attacked.Owner);
                            }
                            #region EarthSweep
                            if (attack.SpellID == 12220 || attack.SpellID == 12210)
                            {
                                SpellUse suse = new SpellUse(true);
                                suse.Attacker = attacker.UID;
                                suse.SpellID = attack.SpellID;
                                suse.SpellLevel = 0;
                                suse.X = attacker.X;
                                suse.Y = attacker.Y;
                                Fan fan = new Fan(attacker.X, attacker.Y, attacked.X, attacked.Y, 7, 180);
                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                {
                                    if (_obj == null)
                                        continue;
                                    if (_obj.MapObjType == MapObjectType.Monster ||
                                        _obj.MapObjType == MapObjectType.Player)
                                    {
                                        attacked = _obj as Entity;
                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= 7)
                                        {
                                            if (CanAttack(attacker, attacked, null, attack.AttackType == Attack.Melee))
                                            {
                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                uint damage = Game.Attacking.Calculate.Melee(attacker,
                                                    attacked, ref attack);
                                                attack.Attacked = 0;
                                                attack.Damage = damage;
                                                suse.Effect1 = attack.Effect1;
                                                ReceiveAttack(attacker, attacked, attack, ref damage, null);
                                                suse.AddTarget(attacked, damage, attack);
                                            }
                                        }
                                    }
                                }
                                attacker.Owner.SendScreen(suse, true);
                                attacker.AttackPacket = null;
                                attack = null;
                                return;
                            }
                            #endregion
                            CheckForExtraWeaponPowers(attacker.Owner, attacked);
                            if (!CanAttack(attacker, attacked, null, attack.AttackType == Attack.Melee)) return;
                            pass = false;
                            ushort range = attacker.AttackRange;
                            if (attacker.Transformed)
                                range = (ushort)attacker.TransformationAttackRange;
                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= range || pass)
                            {
                                attack.Effect1 = Attack.AttackEffects1.None;
                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                attack.Damage = damage;
                                if (attacker.OnFatalStrike())
                                {
                                    if (attacked.EntityFlag == EntityFlag.Monster)
                                    {
                                        var weaps = attacker.Owner.Weapons;
                                        bool can = false;
                                        if (weaps.Item1 != null)
                                            if (weaps.Item1 != null) if (weaps.Item1.ID / 1000 == 601 || weaps.Item1.ID / 1000 == 616) can = true; if (weaps.Item2 != null) if (weaps.Item2.ID / 1000 == 601 || weaps.Item2.ID / 1000 == 616) can = true;
                                        can = true;
                                        if (weaps.Item2 != null)
                                            if (weaps.Item1 != null) if (weaps.Item1.ID / 1000 == 601 || weaps.Item1.ID / 1000 == 616) can = true; if (weaps.Item2 != null) if (weaps.Item2.ID / 1000 == 601 || weaps.Item2.ID / 1000 == 616) can = true;
                                        can = true;
                                        if (!can)
                                            return;
                                        ushort x = attacked.X;
                                        ushort y = attacked.Y;
                                        Map.UpdateCoordonatesForAngle(ref x, ref y, Kernel.GetAngle(attacked.X, attacked.Y, attacker.X, attacker.Y));
                                        attacker.Shift(x, y);
                                        attack.X = x;
                                        attack.Y = y;
                                        attack.Damage = damage;
                                        attack.AttackType = Attack.FatalStrike;
                                    }
                                }
                                var weapons = attacker.Owner.Weapons;
                                if (weapons.Item1 != null)
                                {
                                    ConquerItem rightweapon = weapons.Item1;
                                    ushort wep1subyte = (ushort)(rightweapon.ID / 1000), wep2subyte = 0;
                                    bool wep1bs = false, wep2bs = false;
                                    if (wep1subyte == 421 || wep1subyte == 620)
                                    {
                                        wep1bs = true;
                                        wep1subyte = 420;
                                    }
                                    ushort wep1spellid = 0, wep2spellid = 0;
                                    Database.SpellInformation wep1spell = null, wep2spell = null;
                                    if (Database.SpellTable.WeaponSpells.ContainsKey(wep1subyte))
                                    {
                                        var weaponskill = Database.SpellTable.WeaponSpells[wep1subyte];
                                        for (int i = 0; i < weaponskill.Count; i++)
                                        {
                                            if (!doWep1Spell || (Kernel.Rate(50) && weaponskill.Count > 1))
                                            {
                                                wep1spellid = weaponskill[i];
                                                if (attacker.Owner.Spells.ContainsKey(wep1spellid) && Database.SpellTable.SpellInformations.ContainsKey(wep1spellid))
                                                {
                                                    wep1spell = Database.SpellTable.SpellInformations[wep1spellid][attacker.Owner.Spells[wep1spellid].Level];
                                                    doWep1Spell = Kernel.Rate(wep1spell.Percent);
                                                    //if (attacked.EntityFlag == EntityFlag.Player && wep1spellid == 10490)
                                                    //    doWep1Spell = Kernel.Rate(5);
                                                }
                                            }
                                        }
                                    }
                                    if (!doWep1Spell)
                                    {
                                        if (weapons.Item2 != null)
                                        {
                                            ConquerItem leftweapon = weapons.Item2;
                                            wep2subyte = (ushort)(leftweapon.ID / 1000);
                                            if (wep2subyte == 421 || wep2subyte == 620)
                                            {
                                                wep2bs = true;
                                                wep2subyte = 420;
                                            }
                                            if (Database.SpellTable.WeaponSpells.ContainsKey(wep2subyte))
                                            {
                                                var weaponskill2 = Database.SpellTable.WeaponSpells[wep2subyte];
                                                for (int i = 0; i < weaponskill2.Count; i++)
                                                {
                                                    if (!doWep2Spell || (Kernel.Rate(50) && weaponskill2.Count > 1))
                                                    {
                                                        wep2spellid = weaponskill2[i];
                                                        if (attacker.Owner.Spells.ContainsKey(wep2spellid) && Database.SpellTable.SpellInformations.ContainsKey(wep2spellid))
                                                        {
                                                            wep2spell = Database.SpellTable.SpellInformations[wep2spellid][attacker.Owner.Spells[wep2spellid].Level];
                                                            doWep2Spell = Kernel.Rate(wep2spell.Percent);
                                                            //if (attacked.EntityFlag == EntityFlag.Player && wep2spellid == 10490)
                                                            //    doWep2Spell = Kernel.Rate(5);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (!attacker.Transformed)
                                    {
                                        if (doWep1Spell)
                                        {
                                            attack.AttackType = Attack.Magic;
                                            attack.Decoded = true;
                                            attack.weaponspell = true;
                                            attack.X = attacked.X;
                                            attack.Y = attacked.Y;
                                            attack.Attacked = attacked.UID;
                                            attack.Damage = wep1spell.ID;
                                            goto restart;
                                        }
                                        if (doWep2Spell)
                                        {
                                            attack.AttackType = Attack.Magic;
                                            attack.Decoded = true;
                                            attack.weaponspell = true;
                                            attack.X = attacked.X;
                                            attack.Y = attacked.Y;
                                            attack.Attacked = attacked.UID;
                                            attack.Damage = wep2spell.ID;
                                            goto restart;
                                        }
                                        if (wep1bs)
                                            wep1subyte++;
                                        if (attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag != EntityFlag.Player)
                                            if (damage > attacked.Hitpoints)
                                            {
                                                attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attacked.Hitpoints), wep1subyte);
                                                if (wep2subyte != 0)
                                                {
                                                    if (wep2bs)
                                                        wep2subyte++;
                                                    attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attacked.Hitpoints), wep2subyte);
                                                }
                                            }
                                            else
                                            {
                                                attacker.Owner.IncreaseProficiencyExperience(damage, wep1subyte);
                                                if (wep2subyte != 0)
                                                {
                                                    if (wep2bs)
                                                        wep2subyte++;
                                                    attacker.Owner.IncreaseProficiencyExperience(damage, wep2subyte);
                                                }
                                            }
                                    }
                                }
                                else
                                {
                                    if (!attacker.Transformed)
                                    {
                                        if (attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag != EntityFlag.Player)
                                            if (damage > attacked.Hitpoints)
                                            {
                                                attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attacked.Hitpoints), 0);
                                            }
                                            else
                                            {
                                                attacker.Owner.IncreaseProficiencyExperience(damage, 0);
                                            }
                                    }
                                }
                                ReceiveAttack(attacker, attacked, attack, ref damage, null);
                                attack.AttackType = Attack.Melee;
                            }
                            else
                            {
                                attacker.AttackPacket = null;
                            }
                        }
                        else if (attacker.Owner.Screen.TryGetSob(attack.Attacked, out attackedsob))
                        {
                            CheckForExtraWeaponPowers(attacker.Owner, null);
                            if (CanAttack(attacker, attackedsob, null))
                            {
                                ushort range = attacker.AttackRange;
                                if (attacker.Transformed)
                                    range = (ushort)attacker.TransformationAttackRange;
                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= range)
                                {
                                    attack.Effect1 = Attack.AttackEffects1.None;
                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                    var weapons = attacker.Owner.Weapons;
                                    if (weapons.Item1 != null)
                                    {
                                        ConquerItem rightweapon = weapons.Item1;
                                        ushort wep1subyte = (ushort)(rightweapon.ID / 1000), wep2subyte = 0;
                                        bool wep1bs = false, wep2bs = false;
                                        if (wep1subyte == 421 || wep1subyte == 620)
                                        {
                                            wep1bs = true;
                                            wep1subyte = 420;
                                        }

                                        ushort wep1spellid = 0, wep2spellid = 0;
                                        Database.SpellInformation wep1spell = null, wep2spell = null;
                                        if (Database.SpellTable.WeaponSpells.ContainsKey(wep1subyte))
                                        {
                                            var wep1 = Database.SpellTable.WeaponSpells[wep1subyte];
                                            for (int i = 0; i < wep1.Count; i++)
                                            {
                                                if (!doWep1Spell)
                                                {
                                                    wep1spellid = wep1[i];
                                                    if (attacker.Owner.Spells.ContainsKey(wep1spellid) && Database.SpellTable.SpellInformations.ContainsKey(wep1spellid))
                                                    {
                                                        wep1spell = Database.SpellTable.SpellInformations[wep1spellid][attacker.Owner.Spells[wep1spellid].Level];
                                                        doWep1Spell = Kernel.Rate(wep1spell.Percent);
                                                    }
                                                }
                                            }
                                        }
                                        if (!doWep1Spell)
                                        {
                                            if (weapons.Item2 != null)
                                            {
                                                ConquerItem leftweapon = weapons.Item2;
                                                wep2subyte = (ushort)(leftweapon.ID / 1000);
                                                if (wep2subyte == 421 || wep2subyte == 620)
                                                {
                                                    wep2bs = true;
                                                    wep2subyte = 420;
                                                }
                                                if (Database.SpellTable.WeaponSpells.ContainsKey(wep2subyte))
                                                {
                                                    var weaponskill2 = Database.SpellTable.WeaponSpells[wep2subyte];
                                                    for (int i = 0; i < weaponskill2.Count; i++)
                                                    {
                                                        if (!doWep2Spell || (Kernel.Rate(50) && weaponskill2.Count > 1))
                                                        {
                                                            wep2spellid = weaponskill2[i];
                                                            if (attacker.Owner.Spells.ContainsKey(wep2spellid) && Database.SpellTable.SpellInformations.ContainsKey(wep2spellid))
                                                            {
                                                                wep2spell = Database.SpellTable.SpellInformations[wep2spellid][attacker.Owner.Spells[wep2spellid].Level];
                                                                doWep2Spell = Kernel.Rate(wep2spell.Percent);
                                                                //if (attacked.PlayerFlag == PlayerFlag.Player && wep2spellid == 10490)
                                                                //    doWep2Spell = Kernel.Rate(5);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (!attacker.Transformed)
                                        {
                                            if (doWep1Spell)
                                            {
                                                attack.AttackType = Attack.Magic;
                                                attack.Decoded = true;
                                                attack.weaponspell = true;
                                                attack.X = attackedsob.X;
                                                attack.Y = attackedsob.Y;
                                                attack.Attacked = attackedsob.UID;
                                                attack.Damage = wep1spell.ID;
                                                goto restart;
                                            }
                                            if (doWep2Spell)
                                            {
                                                attack.AttackType = Attack.Magic;
                                                attack.Decoded = true;
                                                attack.weaponspell = true;
                                                attack.X = attackedsob.X;
                                                attack.Y = attackedsob.Y;
                                                attack.Attacked = attackedsob.UID;
                                                attack.Damage = wep2spell.ID;
                                                goto restart;
                                            }
                                            if (attacker.MapID == 1039)
                                            {
                                                if (wep1bs)
                                                    wep1subyte++;
                                                if (attacker.EntityFlag == EntityFlag.Player)
                                                    if (damage > attackedsob.Hitpoints)
                                                    {
                                                        attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attackedsob.Hitpoints), wep1subyte);
                                                        if (wep2subyte != 0)
                                                        {
                                                            if (wep2bs)
                                                                wep2subyte++;
                                                            attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attackedsob.Hitpoints), wep2subyte);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        attacker.Owner.IncreaseProficiencyExperience(damage, wep1subyte);
                                                        if (wep2subyte != 0)
                                                        {
                                                            if (wep2bs)
                                                                wep2subyte++;
                                                            attacker.Owner.IncreaseProficiencyExperience(damage, wep2subyte);
                                                        }
                                                    }
                                            }
                                        }
                                    }
                                    attack.Damage = damage;
                                    ReceiveAttack(attacker, attackedsob, attack, damage, null);
                                }
                                else
                                {
                                    attacker.AttackPacket = null;
                                }
                            }
                        }
                        else
                        {
                            attacker.AttackPacket = null;
                        }
                    }
                    #endregion
                    #region Ranged
                    else if (attack.AttackType == Attack.Ranged)
                    {

                        if (attacker.Owner.Screen.TryGetValue(attack.Attacked, out attacked))
                        {
                            CheckForExtraWeaponPowers(attacker.Owner, attacked);
                            if (!CanAttack(attacker, attacked, null, false))
                                return;
                            var weapons = attacker.Owner.Weapons;
                            if (weapons.Item1 == null) return;
                            if (weapons.Item1.ID / 1000 != 500 && weapons.Item1.ID / 1000 != 613)
                                return;

                            if (weapons.Item1.ID / 1000 == 500)
                                if (weapons.Item2 != null)
                                    if (!PacketHandler.IsArrow(weapons.Item2.ID))
                                        return;

                            #region Kinetic Spark
                            if (attacker.ContainsFlag3(Update.Flags3.KineticSpark))
                            {
                                var spell = Database.SpellTable.GetSpell(11590, attacker.Owner);
                                if (spell != null)
                                {
                                    spell.CanKill = true;
                                    if (Kernel.Rate(spell.Percent))
                                    {
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = attacker.X;
                                        suse.Y = attacker.Y;
                                        IMapObject lastAttacked = attacker;
                                        uint p = 0;
                                        if (Handle.CanAttack(attacker, attacked, spell, false))
                                        {
                                            lastAttacked = attacked;
                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                            suse.Effect1 = attack.Effect1;
                                            damage = damage - damage * (p += 20) / 100;
                                            Handle.ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                            suse.AddTarget(attacked, damage, attack);
                                        }
                                        foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                        {
                                            if (_obj == null) continue;
                                            if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                            {
                                                if (_obj.UID == attacked.UID) continue;
                                                var attacked1 = _obj as Entity;
                                                if (Kernel.GetDistance(lastAttacked.X, lastAttacked.Y, attacked1.X, attacked1.Y) <= 5)
                                                {
                                                    if (Handle.CanAttack(attacker, attacked1, spell, false))
                                                    {
                                                        lastAttacked = attacked1;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked1, spell, ref attack);
                                                        suse.Effect1 = attack.Effect1;

                                                        damage = damage - damage * (p += 20) / 100;
                                                        // damage += (damage * 36) / 100;
                                                        if (damage == 0) break;
                                                        Handle.ReceiveAttack(attacker, attacked1, attack, ref damage, spell);
                                                        suse.AddTarget(attacked1, damage, attack);
                                                    }
                                                }
                                            }
                                            else if (_obj.MapObjType == MapObjectType.SobNpc)
                                            {
                                                attackedsob = _obj as SobNpcSpawn;
                                                if (Kernel.GetDistance(lastAttacked.X, lastAttacked.Y, attackedsob.X, attackedsob.Y) <= 5)
                                                {
                                                    if (Handle.CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        lastAttacked = attackedsob;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        suse.Effect1 = attack.Effect1;

                                                        damage = damage - damage * (p += 20) / 100;
                                                        // damage += (damage * 36) / 100;
                                                        if (damage == 0) break;
                                                        Handle.ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                        }
                                        attacker.Owner.SendScreen(suse, true);
                                        return;
                                    }
                                }
                            }
                            #endregion

                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= Constants.pScreenDistance)
                            {
                                attack.Effect1 = Attack.AttackEffects1.None;
                                uint damage = 0;

                                if (attacker.Assassin() || attacker.IsBowEquipped)
                                {
                                    damage = Game.Attacking.Calculate.Ranged(attacker, attacked, ref attack) / 2;
                                }
                                else
                                {
                                    damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                }

                                attack.Damage = damage;
                                if (attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag != EntityFlag.Player)
                                    if (damage > attacked.Hitpoints)
                                    {
                                        attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attacked.Hitpoints), (ushort)(weapons.Item1.ID / 1000));
                                    }
                                    else
                                    {
                                        attacker.Owner.IncreaseProficiencyExperience(damage, (ushort)(weapons.Item1.ID / 1000));
                                    }
                                ReceiveAttack(attacker, attacked, attack, ref damage, null);
                            }
                        }
                        else if (attacker.Owner.Screen.TryGetSob(attack.Attacked, out attackedsob))
                        {
                            if (CanAttack(attacker, attackedsob, null))
                            {
                                if (attacker.Owner.Equipment.TryGetItem(ConquerItem.LeftWeapon) == null)
                                    return;

                                var weapons = attacker.Owner.Weapons;
                                if (weapons.Item1 == null) return;
                                if (weapons.Item1.ID / 1000 != 500 && weapons.Item1.ID / 1000 != 613)
                                    return;

                                if (attacker.MapID != 1039)
                                    if (weapons.Item1.ID / 1000 == 500)
                                        if (weapons.Item2 != null)
                                            if (!PacketHandler.IsArrow(weapons.Item2.ID))
                                                return;

                                #region Kinetic Spark
                                if (attacker.ContainsFlag3(Update.Flags3.KineticSpark))
                                {
                                    var spell = Database.SpellTable.GetSpell(11590, attacker.Owner);
                                    if (spell != null)
                                    {
                                        spell.CanKill = true;
                                        if (Kernel.Rate(spell.Percent))
                                        {
                                            attacker.RemoveFlag3(Update.Flags3.KineticSpark);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = attacker.X;
                                            suse.Y = attacker.Y;

                                            IMapObject lastAttacked = attacker;
                                            uint p = 0;
                                            if (Handle.CanAttack(attacker, attackedsob, spell))
                                            {
                                                lastAttacked = attackedsob;
                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                suse.Effect1 = attack.Effect1;

                                                damage = damage - damage * (p += 20) / 100;
                                                //damage += (damage * 36) / 100;
                                                Handle.ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                suse.AddTarget(attackedsob, damage, attack);
                                            }
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null) continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    var attacked1 = _obj as Entity;
                                                    if (Kernel.GetDistance(lastAttacked.X, lastAttacked.Y, attacked1.X, attacked1.Y) <= 5)
                                                    {
                                                        if (Handle.CanAttack(attacker, attacked1, spell, false))
                                                        {
                                                            lastAttacked = attacked1;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attacked1, spell, ref attack);
                                                            suse.Effect1 = attack.Effect1;

                                                            damage = damage - damage * (p += 20) / 100;
                                                            //  damage += (damage * 36) / 100;
                                                            if (damage == 0) break;
                                                            Handle.ReceiveAttack(attacker, attacked1, attack, ref damage, spell);
                                                            suse.AddTarget(attacked1, damage, attack);
                                                        }
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    if (_obj.UID == Target) continue;
                                                    var attackedsob1 = _obj as SobNpcSpawn;
                                                    if (Kernel.GetDistance(lastAttacked.X, lastAttacked.Y, attackedsob1.X, attackedsob1.Y) <= 5)
                                                    {
                                                        if (Handle.CanAttack(attacker, attackedsob1, spell))
                                                        {
                                                            lastAttacked = attackedsob1;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob1, ref attack);
                                                            suse.Effect1 = attack.Effect1;

                                                            damage = damage - damage * (p += 20) / 100;
                                                            //damage += (damage * 36) / 100;
                                                            if (damage == 0) break;
                                                            Handle.ReceiveAttack(attacker, attackedsob1, attack, damage, spell);
                                                            suse.AddTarget(attackedsob1, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                            return;
                                        }
                                    }
                                }
                                #endregion

                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= Constants.pScreenDistance)
                                {
                                    attack.Effect1 = Attack.AttackEffects1.None;
                                    uint damage = 0;
                                    //   if (!attacker.Assassin())
                                    damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);
                                    //     else
                                    //       damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                    attack.Damage = damage;
                                    ReceiveAttack(attacker, attackedsob, attack, damage, null);

                                    if (damage > attackedsob.Hitpoints)
                                    {
                                        attacker.Owner.IncreaseProficiencyExperience(Math.Min(damage, attackedsob.Hitpoints), (ushort)(weapons.Item1.ID / 1000));
                                    }
                                    else
                                    {
                                        attacker.Owner.IncreaseProficiencyExperience(damage, (ushort)(weapons.Item1.ID / 1000));
                                    }
                                }
                            }
                        }
                        else
                        {
                            attacker.AttackPacket = null;
                        }
                    }
                    #endregion
                    #region Magic
                    else if (attack.AttackType == Attack.Magic)
                    {
                        CheckForExtraWeaponPowers(attacker.Owner, attacked);
                        uint Experience = 100;
                        bool shuriken = false;
                        ushort spellID = SpellID;
                        if (SpellID >= 3090 && SpellID <= 3306)
                            spellID = 3090;
                        if (spellID == 6012)
                            shuriken = true;



                        if (attacker == null)
                            return;
                        if (attacker.Owner == null)
                        {
                            attacker.AttackPacket = null;
                            return;
                        }
                        if (attacker.Owner.Spells == null)
                        {
                            attacker.Owner.Spells = new SafeDictionary<ushort, Interfaces.ISkill>();
                            attacker.AttackPacket = null;
                            return;
                        }
                        if (attacker.Owner.Spells[spellID] == null && spellID != 6012)
                        {
                            attacker.AttackPacket = null;
                            return;
                        }

                        Database.SpellInformation spell = null;
                        if (shuriken)
                            spell = Database.SpellTable.SpellInformations[6010][0];
                        else
                        {
                            byte choselevel = 0;
                            if (spellID == SpellID)
                                choselevel = attacker.Owner.Spells[spellID].Level;
                            if (Database.SpellTable.SpellInformations[SpellID] != null && !Database.SpellTable.SpellInformations[SpellID].ContainsKey(choselevel))
                                choselevel = (byte)(Database.SpellTable.SpellInformations[SpellID].Count - 1);

                            spell = Database.SpellTable.SpellInformations[SpellID][choselevel];
                        }
                        if (spell == null)
                        {
                            attacker.AttackPacket = null;
                            return;
                        }
                        attacked = null;
                        attackedsob = null;
                        if (attacker.Owner.Screen.TryGetValue(Target, out attacked) || attacker.Owner.Screen.TryGetSob(Target, out attackedsob) || Target == attacker.UID || spell.Sort != 1)
                        {
                            if (Target == attacker.UID)
                                attacked = attacker;
                            if (attacked != null)
                            {
                                if (attacked.Dead && spell.Sort != Database.SpellSort.Revive && spell.ID != 10405 && spell.ID != 10425)
                                {
                                    attacker.AttackPacket = null;
                                    return;
                                }
                            }
                            if (!attacker.RebornSpell(spell.ID))
                            {
                                if (attacker.Owner.Spells.ContainsKey(11960))
                                {
                                    if (attacked == null)
                                    {
                                        if (attacker.Owner.Spells[11960].Level == 0 && Kernel.Rate(spell.Percent))
                                        {
                                            attacker.Stamina += 5;
                                            attacker.Owner.Send(new Message("You stamina has increased by % 5 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 1 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 6;
                                            attacker.Owner.Send(new Message("You stamina has increased by 6 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 2 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 7;
                                            attacker.Owner.Send(new Message("You stamina has increased by 7 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 3 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 8;
                                            attacker.Owner.Send(new Message("You stamina has increased by 8 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 4 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 9;
                                            attacker.Owner.Send(new Message("You stamina has increased by 9 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 5 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 10;
                                            attacker.Owner.Send(new Message("You stamina has increased by 10 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 6 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 11;
                                            attacker.Owner.Send(new Message("You stamina has increased by % 11 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 7 && Kernel.Rate(35))
                                        {
                                            if (attacker.Stamina <= 88)
                                            {
                                                attacker.Stamina += 12;
                                                attacker.Owner.Send(new Message("You stamina has increased by 12 points!", System.Drawing.Color.Red, Message.TopLeft));
                                            }
                                        }
                                        if (attacker.Owner.Spells[11960].Level == 8 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 14;
                                            attacker.Owner.Send(new Message("You stamina has increased by 14 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Owner.Spells[11960].Level == 9 && Kernel.Rate(35))
                                        {
                                            attacker.Stamina += 20;
                                            attacker.Owner.Send(new Message("You stamina has increased by 20 points!", System.Drawing.Color.Red, Message.TopLeft));

                                        }
                                        if (attacker.Stamina > 100)
                                            attacker.Stamina = 100;
                                    }
                                }
                            }
                            if (Target >= 400000 && Target <= 600000 || Target >= 800000)
                            {
                                if (attacked == null && attackedsob == null)
                                    return;
                            }
                            else if (Target != 0 && attacked == null && attackedsob == null && attackedItem == null)
                                return;
                            if (attacked != null)
                            {
                                if (attacked.EntityFlag == EntityFlag.Monster)
                                {
                                    if (spell.CanKill)
                                    {
                                        if (attacked.MonsterInfo.InSight == 0)
                                        {
                                            attacked.MonsterInfo.InSight = attacker.UID;
                                        }
                                    }
                                }
                            }
                            if (!attacker.Owner.Spells.ContainsKey(spellID))
                            {
                                if (spellID != 6012)
                                    return;
                            }

                            var weapons = attacker.Owner.Weapons;
                            if (spell != null)
                            {
                                if (spell.OnlyWithThisWeaponSubtype.Count != 0)
                                {
                                    uint firstwepsubtype, secondwepsubtype;
                                    if (weapons.Item1 != null)
                                    {
                                        firstwepsubtype = weapons.Item1.ID / 1000;
                                        if (firstwepsubtype == 421) firstwepsubtype = 420;
                                        if (weapons.Item2 != null)
                                        {
                                            secondwepsubtype = weapons.Item2.ID / 1000;
                                            if (!spell.OnlyWithThisWeaponSubtype.Contains((ushort)firstwepsubtype) && !spell.OnlyWithThisWeaponSubtype.Contains((ushort)(attacker.Owner.WeaponLook / 1000)))
                                            {
                                                if (!spell.OnlyWithThisWeaponSubtype.Contains((ushort)secondwepsubtype) && !spell.OnlyWithThisWeaponSubtype.Contains((ushort)(attacker.Owner.WeaponLook2 / 1000)))
                                                {
                                                    attacker.AttackPacket = null;
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!spell.OnlyWithThisWeaponSubtype.Contains((ushort)firstwepsubtype) && !spell.OnlyWithThisWeaponSubtype.Contains((ushort)(attacker.Owner.WeaponLook / 1000)))
                                            {
                                                attacker.AttackPacket = null;
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        attacker.AttackPacket = null;
                                        return;
                                    }
                                }
                            }



                            Interfaces.ISkill client_Spell;
                            if (!attacker.Owner.Spells.TryGetValue(spell.ID, out client_Spell))
                                if (!attacker.Owner.Spells.TryGetValue(spellID, out client_Spell))
                                    return;
                            #region Allowed Skills
                            if (!attacker.Owner.Fake)
                            {
                                var xspell = GetWeaponSpell(spell);
                                //  var xspell = Database.SpellTable.WeaponSpells.Values.Where(p => p.Contains(spell.ID)).FirstOrDefault();
                                if (xspell != null)
                                {
                                    if (!attack.weaponspell && spell.Sort == SpellSort.SingleWeaponSkill)
                                    {
                                        attacker.AttackPacket = null;
                                        return;
                                    }
                                }



                                if (spellID == 1045 || spellID == 1046)
                                {
                                    if (attack.Attacked != 0)
                                    {
                                        attacker.AttackPacket = null;
                                        return;
                                    }
                                }
                            }
                            #endregion
                            switch (spellID)
                            {

                                #region Classes
                                #region Trojan
                                #region New Trojan
                                #region BreathFocus
                                case 11960:
                                    {
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.X = attacker.X;
                                        suse.Y = attacker.Y;
                                        suse.Targets.Add(attacker.UID, 1);
                                        attacker.Owner.SendScreen(suse, true);
                                        attacker.Stamina += (byte)spell.Power;
                                        attacker.Owner.Send(new Message("Your Stamina has increased by " + spell.Power, Message.TopLeft));
                                        break;
                                    }
                                #endregion
                                #region SuperCyclone
                                case 11970:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (!attacked.OnKOSpell())
                                                attacked.KOCount = 0;
                                            attacked.KOSpell = spell.ID;
                                            suse.AddTarget(attacked, 0, attack);
                                            attacked.SuperCycloneStamp = Time32.Now;
                                            attacked.SuperCycloneTime = 40;
                                            attacked.AddFlag3(Update.Flags3.SuperCyclone);
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region FatalCross
                                case 11980:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            ushort _X = attacker.X, _Y = attacker.Y;
                                            ushort _tX = X, _tY = Y;
                                            byte dist = (byte)spell.Distance;
                                            var Array = attacker.Owner.Screen.Objects;
                                            InLineAlgorithm algo = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, dist,
                                                                               InLineAlgorithm.Algorithm.DDA);

                                            int i = algo.lcoords.Count;

                                            double disth = 1.5;

                                            suse.X = X;
                                            suse.Y = Y;
                                            foreach (Interfaces.IMapObject _obj in Array)
                                            {
                                                bool hit = false;
                                                for (int j = 0; j < i; j++)
                                                    if (Kernel.GetDDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[j].X, (ushort)algo.lcoords[j].Y) <= disth)
                                                        hit = true;
                                                if (hit)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);

                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                            damage = (uint)(damage * 0.8);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);

                                        }
                                        break;
                                    }
                                #endregion
                                #region MortalStrike
                                case 11990:
                                    {
                                        if (Kernel.Rate(spell.Percent))
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                                sector.Arrange(spell.Sector, spell.Range);
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range + 1)
                                                {
                                                    foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                    {
                                                        if (_obj == null)
                                                            continue;
                                                        if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                        {
                                                            attacked = _obj as Entity;

                                                            if (sector.Inside(attacked.X, attacked.Y))
                                                            {
                                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                {
                                                                    uint damage = 0;
                                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                                    if (_obj.MapObjType == MapObjectType.Player)
                                                                    {
                                                                        damage = (uint)(Game.Attacking.Calculate.Melee(attacker, attacked, ref attack) * spell.HumanPower / 100);
                                                                    }
                                                                    else
                                                                    {
                                                                        damage = (uint)(Game.Attacking.Calculate.Melee(attacker, attacked, ref attack) * ((spell.Power - 30000) / 100));
                                                                    }
                                                                    suse.Effect1 = attack.Effect1;
                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                    suse.AddTarget(attacked, damage, attack);
                                                                }
                                                            }
                                                        }
                                                        else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                        {
                                                            attackedsob = _obj as SobNpcSpawn;

                                                            if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                            {
                                                                if (CanAttack(attacker, attackedsob, spell))
                                                                {
                                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                    ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                    suse.AddTarget(attackedsob, damage, attack);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                                attacker.Owner.SendScreen(suse, true);
                                            }
                                        }
                                        break;
                                    }

                                #endregion
                                #endregion
                                #endregion
                                #region Warrior
                                #region MagicDefender
                                case 11200:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.Attacker = attacker.UID;
                                            spellUse.SpellID = spell.ID;
                                            spellUse.SpellLevel = spell.Level;
                                            spellUse.X = X;
                                            spellUse.Y = Y;
                                            if (attacker.Owner.Team != null)
                                            {
                                                foreach (var mate in attacker.Owner.Team.Teammates)
                                                {
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, mate.Entity.X, mate.Entity.Y) <= 4)
                                                    {
                                                        spellUse.AddTarget(mate.Entity, 0, attack);
                                                        if (attacker.UID == mate.Entity.UID)
                                                            mate.Entity.MagicDefenderOwner = true;
                                                        mate.Entity.HasMagicDefender = true;
                                                        mate.Entity.MagicDefenderSecs = (byte)spell.Duration;
                                                        attacker.RemoveFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.MagicDefender);
                                                        mate.Entity.AddFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.MagicDefender);
                                                        mate.Entity.Update(mate.Entity.StatusFlag, mate.Entity.StatusFlag2, mate.Entity.StatusFlag3, Update.MagicDefenderIcone, 0x80, mate.Entity.MagicDefenderSecs, 0, false);
                                                        mate.Entity.MagicDefenderStamp = Time32.Now;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                spellUse.AddTarget(attacker, 0, null);
                                                attacker.Owner.Entity.HasMagicDefender = true;
                                                attacker.Owner.Entity.MagicDefenderOwner = true;
                                                attacker.Owner.Entity.MagicDefenderSecs = (byte)spell.Duration;
                                                attacker.RemoveFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.MagicDefender);
                                                attacker.AddFlag3(Nyx.Server.Network.GamePackets.Update.Flags3.MagicDefender);
                                                attacker.Owner.Entity.Update(attacker.Owner.Entity.StatusFlag, attacker.Owner.Entity.StatusFlag2, attacker.Owner.Entity.StatusFlag3, Update.MagicDefenderIcone, 0x80, attacker.Owner.Entity.MagicDefenderSecs, 0, false);
                                                attacker.Owner.Entity.MagicDefenderStamp = Time32.Now;
                                            }
                                            attacker.Owner.SendScreen(spellUse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region DefensiveStance
                                case 11160:
                                    {
                                        if (Time32.Now >= attacker.DefensiveStanceStamp.AddSeconds(10))
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);
                                                SpellUse spellUse = new SpellUse(true);
                                                spellUse.Attacker = attacker.UID;
                                                spellUse.SpellID = spell.ID;
                                                spellUse.SpellLevel = spell.Level;
                                                spellUse.X = X;
                                                spellUse.Y = Y;
                                                attacker.Owner.SendScreen(spellUse, true);
                                                if (attacker.IsDefensiveStance)
                                                {
                                                    attacker.RemoveFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.Fatigue);
                                                    attacker.IsDefensiveStance = false;
                                                }
                                                else
                                                {
                                                    attacker.FatigueSecs = spell.Duration;
                                                    attacker.FatigueStamp = Time32.Now;
                                                    attacker.AddFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.Fatigue);
                                                    attacker.Update(attacker.Owner.Entity.StatusFlag, attacker.Owner.Entity.StatusFlag2, attacker.Owner.Entity.StatusFlag3, Update.DefensiveStance, 0x7E, (uint)spell.Duration, (uint)(spell.Level + 1), false);
                                                    attacker.IsDefensiveStance = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            attacker.Owner.Send(new Message("You need to wait 10 seconds in order to cast the spell again!", Color.Red, Message.TopLeft));
                                        }
                                        break;
                                    }
                                #endregion
                                #region ShieldBlock
                                case 10470: // Shield Block
                                    {

                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);
                                                SpellUse spellUse = new SpellUse(true);
                                                spellUse.Attacker = attacker.UID;
                                                spellUse.SpellID = spell.ID;
                                                spellUse.SpellLevel = spell.Level;
                                                spellUse.X = X;
                                                spellUse.Y = Y;
                                                attacker.Owner.SendScreen(spellUse, true);
                                                if (attacker.IsShieldBlock)
                                                {
                                                    Network.GamePackets.Update aupgrade = new Network.GamePackets.Update(true);
                                                    aupgrade.UID = attacker.UID;
                                                    aupgrade.Append(49
                                                   , 113
                                                   , (uint)0, 0, 0);
                                                    attacker.Owner.Send(aupgrade);
                                                    attacker.IsShieldBlock = false;
                                                }
                                                else
                                                {
                                                    attacker.ShieldBlockStamp = Time32.Now;
                                                    attacker.ShieldBlockPercent = (ushort)spell.Power;
                                                    Network.GamePackets.Update aupgrade = new Network.GamePackets.Update(true);
                                                    aupgrade.UID = attacker.UID;
                                                    aupgrade.Append(49
                                                   , 113
                                                   , (uint)spell.Duration, spell.Power, spell.Level);
                                                    attacker.Owner.Send(aupgrade);
                                                    attacker.IsShieldBlock = true;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Dash
                                case 1051:
                                    {
                                        if (attacked != null)
                                        {
                                            if (!attacked.Dead)
                                            {
                                                var direction = Kernel.GetAngle(attacker.X, attacker.Y, attacked.X, attacked.Y);
                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                {
                                                    attack = new Attack(true);
                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                    uint damage = Calculate.Melee(attacker, attacked, ref attack);
                                                    attack.AttackType = Attack.Dash;
                                                    attack.X = attacked.X;
                                                    attack.Y = attacked.Y;
                                                    attack.Attacker = attacker.UID;
                                                    attack.Attacked = attacked.UID;
                                                    attack.Damage = damage;
                                                    attack.Encode()[27] = (byte)direction;
                                                    attacked.Move(direction);
                                                    attacker.Move(direction);

                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                    attacker.Owner.SendScreen(attack, true);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Vortex
                                case 6010:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;

                                            attacker.AddFlag(Update.Flags.ShurikenVortex);
                                            attacker.RemoveFlag(Update.Flags.Ride);
                                            attacker.ShurikenVortexStamp = Time32.Now;
                                            attacker.ShurikenVortexTime = 20;

                                            attacker.Owner.SendScreen(suse, true);

                                            attacker.VortexPacket = new Attack(true);
                                            attacker.VortexPacket.Decoded = true;
                                            attacker.VortexPacket.Damage = 6012;
                                            attacker.VortexPacket.AttackType = Attack.Magic;
                                            attacker.VortexPacket.Attacker = attacker.UID;
                                        }
                                        break;
                                    }

                                case 6012://VortexRespone
                                    {
                                        if (!attacker.ContainsFlag(Update.Flags.ShurikenVortex))
                                        {
                                            attacker.AttackPacket = null;
                                            break;
                                        }
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = attacker.X;
                                        suse.Y = attacker.Y;
                                        //suse.SpellLevelHu = client_Spell.LevelHu2;
                                        foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                        {
                                            if (_obj == null)
                                                continue;
                                            if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                            {
                                                attacked = _obj as Entity;
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                {
                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                        suse.Effect1 = attack.Effect1;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                            }
                                            else if (_obj.MapObjType == MapObjectType.SobNpc)
                                            {
                                                attackedsob = _obj as SobNpcSpawn;
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                {
                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        suse.Effect1 = attack.Effect1;
                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                        }
                                        attacker.Owner.SendScreen(suse, true);
                                        break;
                                    }
                                #endregion
                                #region Epic Warrior
                                #region WaveofBlood
                                case 12690:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            Fan sector = new Fan(attacker.X, attacker.Y, X, Y, spell.Range, spell.Sector);
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Distance + 1)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj == null)
                                                        continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (sector.IsInFan(attacked.X, attacked.Y))
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                                                suse.Effect1 = attack.Effect1;
                                                                ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                suse.AddTarget(attacked, damage, attack);
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (sector.IsInFan(attackedsob.X, attackedsob.Y))
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Calculate.Melee(attacker, attackedsob, ref attack);
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            if (attacker.WaveofBloodXp > 1)
                                            {
                                              //  attacker.WaveofBloodXp -= 1;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }

                                #endregion
                                #region LeftHook || RightHook || StraightFist
                                case 12740:
                                case 12750:
                                case 12760:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            Game.Attacking.InLineAlgorithm ila = new Nyx.Server.Game.Attacking.InLineAlgorithm(attacker.X,
                                        X, attacker.Y, Y, (byte)spell.Range, InLineAlgorithm.Algorithm.DDA);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (ila.InLine(attacked.X, attacked.Y))
                                                    {
                                                        if (!CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                        suse.Effect1 = attack.Effect1;

                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (ila.InLine(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (!CanAttack(attacker, attackedsob, spell))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }

                                #endregion
                                #region ManiacDance
                                case 12700:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            attacker.AddFlag3((ulong)1UL << 53);
                                            attacker.ManiacDance = Time32.Now;
                                            attacker.RemoveFlag(Update.Flags.Ride);
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Backfire
                                case 12680:
                                    {
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = attacker.UID;
                                        spellUse.Attacker1 = attacker.UID;
                                        spellUse.SpellID = spell.ID;
                                        spellUse.SpellLevel = spell.Level;
                                        spellUse.X = X;
                                        spellUse.Y = Y;
                                        spellUse.AddTarget(attacker, 1, attack);
                                        attacker.Owner.SendScreen(spellUse, true);
                                        if (attacker.ContainsFlag3((ulong)1UL << 51))
                                            attacker.RemoveFlag3((ulong)1UL << 51);
                                        else
                                            attacker.AddFlag3((ulong)1UL << 51);
                                        attacker.BackfireStamp = Time32.Now;
                                        break;
                                    }
                                #endregion
                                #region ScarofEarth
                                case 12670:
                                    {
                                        if (!attacker.EpicWarrior())
                                        {
                                            attacker.AttackPacket = null; return;
                                        }
                                        if (Kernel.Rate(spell.Percent))
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                                sector.Arrange(spell.Sector, spell.Range);
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range + 1)
                                                {
                                                    foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                    {
                                                        if (_obj == null)
                                                            continue;
                                                        if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                        {
                                                            attacked = _obj as Entity;

                                                            if (sector.Inside(attacked.X, attacked.Y))
                                                            {
                                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                {
                                                                    uint damage = 0;
                                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                                    if (_obj.MapObjType == MapObjectType.Player)
                                                                    {
                                                                        damage = (uint)(Game.Attacking.Calculate.Melee(attacker, attacked, ref attack));
                                                                    }
                                                                    else
                                                                    {
                                                                        damage = (uint)(Game.Attacking.Calculate.Melee(attacker, attacked, ref attack) * ((spell.Power - 30000) / 100));
                                                                    }
                                                                    suse.Effect1 = attack.Effect1;
                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                    suse.AddTarget(attacked, damage, attack);
                                                                }
                                                            }
                                                        }
                                                        else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                        {
                                                            attackedsob = _obj as SobNpcSpawn;

                                                            if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                            {
                                                                if (CanAttack(attacker, attackedsob, spell))
                                                                {
                                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                    ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                    suse.AddTarget(attackedsob, damage, attack);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                                attacker.Owner.SendScreen(suse, true);
                                            }
                                        }
                                        break;
                                    }

                                #endregion
                                #region Pounce
                                case 12770:
                                    {
                                        if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= 5)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= 5)
                                                {
                                                    for (int c = 0; c < attacker.Owner.Screen.Objects.Length; c++)
                                                    {
                                                        if (c >= attacker.Owner.Screen.Objects.Length)
                                                            break;
                                                        Interfaces.IMapObject _obj = attacker.Owner.Screen.Objects[c];
                                                        if (_obj == null)
                                                            continue;
                                                        if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                        {
                                                            attacked = _obj as Entity;
                                                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                            {
                                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Ranged))
                                                                {
                                                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                                    suse.Effect1 = attack.Effect1;
                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                    suse.AddTarget(attacked, damage, attack);

                                                                }
                                                            }
                                                        }
                                                    }
                                                    attacker.AttackPacket = null;
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null; return;
                                                }
                                                attacker.Owner.SendScreen(suse, true);
                                                suse.Targets = new SafeDictionary<uint, SpellUse.DamageClass>();
                                                attacker.AttackPacket = null; return;
                                            }
                                            attacker.AttackPacket = null;
                                        }
                                        attacker.AttackPacket = null; return;
                                    }
                                #endregion
                                #endregion
                                #endregion
                                #region Archer
                                #region Scatter
                                case 8001:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                            sector.Arrange(spell.Sector, spell.Distance);
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;

                                                    if (sector.Inside(attacked.X, attacked.Y))
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Ranged(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);
                                                            suse.Effect1 = attack.Effect1;
                                                            if (damage == 0)
                                                                damage = 1;
                                                            damage = Game.Attacking.Calculate.Percent((int)damage, spell.PowerPercent);

                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region StarArrow
                                case 10313:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            if (attacked != null)
                                            {
                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                {
                                                    ushort _X = attacked.X, _Y = attacked.Y;
                                                    byte dist = 5;
                                                    var angle = Kernel.GetAngle(attacker.X, attacker.Y, attacked.X, attacked.Y);
                                                    while (dist != 0)
                                                    {
                                                        if (attacked.fMove(angle, ref _X, ref _Y))
                                                        {
                                                            X = _X;
                                                            Y = _Y;
                                                        }
                                                        else break;
                                                        dist--;
                                                    }
                                                    suse.X = attacked.X = X;
                                                    suse.Y = attacked.Y = Y;
                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                    suse.AddTarget(attacked, damage, attack);
                                                }
                                            }
                                            else if (attackedsob != null)
                                            {
                                                if (CanAttack(attacker, attackedsob, spell))
                                                {
                                                    suse.X = attackedsob.X;
                                                    suse.Y = attackedsob.Y;
                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                    suse.Effect1 = attack.Effect1;
                                                    if (damage == 0)
                                                        damage = 1;
                                                    damage = Game.Attacking.Calculate.Percent((int)damage, spell.PowerPercent);

                                                    ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                    suse.AddTarget(attackedsob, damage, attack);
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Assassin
                                #region PathOfJoseph
                                case 11620:
                                    {
                                        var weps = attacker.Owner.Weapons;
                                        if ((weps.Item1 != null && weps.Item1.ID / 1000 != 613) || (weps.Item2 != null && weps.Item2.ID / 1000 != 613))
                                        {
                                            attacker.Owner.Send(new Message("You need to wear only knifes!", Color.Red, Message.Talk));
                                            return;
                                        }
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = attacker.UID;
                                        spellUse.SpellID = spell.ID;
                                        spellUse.SpellLevel = spell.Level;
                                        spellUse.X = X;
                                        spellUse.Y = Y;
                                        attacker.Owner.SendScreen(spellUse, true);
                                        attacker.BP = attacker.BattlePower;
                                        if (attacker.ContainsFlag3(Update.Flags3.Assassin))
                                        {
                                            attacker.RemoveFlag3(Update.Flags3.Assassin);
                                            if (attacker.ContainsFlag3(Update.Flags3.BladeFlurry))
                                                attacker.RemoveFlag3(Update.Flags3.BladeFlurry);
                                            if (attacker.ContainsFlag3(Update.Flags3.KineticSpark))
                                                attacker.RemoveFlag3(Update.Flags3.KineticSpark);
                                        }
                                        else
                                            attacker.AddFlag3(Update.Flags3.Assassin);
                                        break;
                                    }
                                #endregion
                                #region Blade Furry
                                case 11610:
                                    {
                                        if (!attacker.ContainsFlag3(Update.Flags3.Assassin))
                                            return;
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = X;
                                        //suse.SpellLevelHu = client_Spell.LevelHu2;
                                        suse.Y = Y;
                                        if (attacker.ContainsFlag(Update.Flags.XPList))
                                        {
                                            attacker.AddFlag3(Update.Flags3.BladeFlurry);
                                            attacker.BladeFlurryStamp = Time32.Now;
                                            attacker.RemoveFlag(Update.Flags.XPList);
                                        }
                                        else
                                        {
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj == null) continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                                suse.Effect1 = attack.Effect1;
                                                                ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                suse.AddTarget(attacked, damage, attack);
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                suse.Effect1 = attack.Effect1;

                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        attacker.Owner.SendScreen(suse, true);
                                        break;
                                    }
                                #endregion
                                #region Mortal Wound
                                case 11660:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (!attacker.ContainsFlag3(Update.Flags3.Assassin))
                                                return;
                                            if (Time32.Now > attacker.MortalWoundStamp.AddMilliseconds(750))
                                            {
                                                attacker.MortalWoundStamp = Time32.Now;
                                                PrepareSpell(spell, attacker.Owner);
                                                attacker.AttackPacket = null;
                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                if (attackedsob == null)
                                                {
                                                    if (CanAttack(attacker, attacked, spell, false))
                                                    {
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Ranged(attacker, attacked, ref attack);
                                                        damage = (uint)(damage * 0.7);
                                                        suse.Effect1 = attack.Effect1;
                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                        suse.AddTarget(attacked, damage, attack);
                                                        attacker.Owner.SendScreen(suse, true);
                                                    }
                                                }
                                                else
                                                {
                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);

                                                        suse.Effect1 = attack.Effect1;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                        suse.AddTarget(attackedsob, damage, attack);
                                                        attacker.Owner.SendScreen(suse, true);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Blistering Wave
                                case 11650:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (!attacker.ContainsFlag3(Update.Flags3.Assassin))
                                                return;
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            suse.LevelHu = client_Spell.LevelHu2;
                                            Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                            sector.Arrange(120, spell.Range);
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;

                                                    if (sector.Inside(attacked.X, attacked.Y))
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Spirit Focus
                                case 9000:
                                case 11670:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (spell.ID == 11670)
                                                if (!attacker.ContainsFlag3(Update.Flags3.Assassin))
                                                    return;

                                            PrepareSpell(spell, attacker.Owner);

                                            attack = new Attack(true);
                                            attack.Attacker = attacker.UID;
                                            attack.Attacked = attacker.UID;
                                            attack.AttackType = Attack.Magic;
                                            attack.X = attacker.X;
                                            attack.Y = attacker.Y;
                                            attack.Damage = spell.ID;
                                            attack.KOCount = spell.Level;
                                            attacker.Owner.SendScreen(attack, true);

                                            attacker.IntensifyPercent = spell.PowerPercent;
                                            attacker.IntensifyStamp = Time32.Now;
                                        }
                                        break;
                                    }
                                #endregion
                                #region Kinetic Spark
                                case 11590:
                                    {
                                        if (!attacker.ContainsFlag3(Update.Flags3.Assassin))
                                            return;

                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = attacker.UID;
                                        spellUse.SpellID = spell.ID;
                                        spellUse.SpellLevel = spell.Level;
                                        spellUse.X = X;
                                        spellUse.Y = Y;
                                        attacker.Owner.SendScreen(spellUse, true);

                                        if (attacker.ContainsFlag3(Update.Flags3.KineticSpark))
                                            attacker.RemoveFlag3(Update.Flags3.KineticSpark);
                                        else
                                            attacker.AddFlag3(Update.Flags3.KineticSpark);
                                        break;
                                    }
                                #endregion
                                #region Dagger Storm
                                case 11600:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (!attacker.ContainsFlag3(Update.Flags3.Assassin))
                                                return;

                                            var map = attacker.Owner.Map;
                                            if (!map.Floor[X, Y, MapObjectType.Item, null]) return;
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.Attacker1 = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.AddTarget(attacker, 0, null);
                                            attacker.Owner.SendScreen(suse, true);

                                            FloorItem floorItem = new FloorItem(true);
                                            if (attacker.Owner.Spells[spellID].LevelHu2 == 1)
                                                floorItem.ItemID = FloorItem.FuryofEgg;
                                            else if (attacker.Owner.Spells[spellID].LevelHu2 == 2)
                                                floorItem.ItemID = FloorItem.ShacklingIce;
                                            else
                                                floorItem.ItemID = FloorItem.DaggerStorm;
                                            floorItem.MapID = attacker.MapID;
                                            floorItem.ItemColor = Enums.Color.Black;
                                            floorItem.Type = FloorItem.Effect;
                                            floorItem.X = X;
                                            floorItem.Y = Y;
                                            floorItem.OnFloor = Time32.Now;
                                            floorItem.Owner = attacker.Owner;
                                            while (map.Npcs.ContainsKey(floorItem.UID))
                                                floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                                            map.AddFloorItem(floorItem);
                                            attacker.Owner.SendScreenSpawn(floorItem, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #region RapidFire
                                case 8000:
                                    {
                                        if (attackedsob != null)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                if (CanAttack(attacker, attackedsob, spell))
                                                {
                                                    PrepareSpell(spell, attacker.Owner);
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    //suse.SpellLevelHu = client_Spell.LevelHu2;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = attackedsob.X;
                                                    suse.Y = attackedsob.Y;
                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                    uint damage = Calculate.Ranged(attacker, attackedsob, ref attack);
                                                    suse.Effect1 = attack.Effect1;
                                                    suse.AddTarget(attackedsob, damage, attack);

                                                    ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                    attacker.Owner.SendScreen(suse, true);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!attacked.Dead)
                                            {
                                                if (CanUseSpell(spell, attacker.Owner))
                                                {
                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        SpellUse suse = new SpellUse(true);
                                                        suse.Attacker = attacker.UID;
                                                        suse.SpellID = spell.ID;
                                                        suse.SpellLevel = spell.Level;
                                                        suse.X = attacked.X;
                                                        suse.Y = attacked.Y;
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Calculate.Ranged(attacker, attacked, ref attack);
                                                        suse.AddTarget(attacked, damage, attack);

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        attacker.Owner.SendScreen(suse, true);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Fly
                                case 8002:
                                case 8003:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (attacker.MapID == 1950)
                                                return;
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            attacked.FlyStamp = Time32.Now;
                                            attacked.FlyTime = (byte)spell.Duration;

                                            suse.AddTarget(attacker, attacker.FlyTime, null);

                                            attacker.AddFlag(Update.Flags.Fly);
                                            attacker.RemoveFlag(Update.Flags.Ride);
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #region Ninja
                                #region SuperTwofoldBlade
                                case 12080:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            //var dis = spell.Distance;
                                            //if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= dis)
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Distance)
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                //suse.SpellLevelHu = client_Spell.UseSpell;
                                                Game.Attacking.InLineAlgorithm ila = new Nyx.Server.Game.Attacking.InLineAlgorithm(attacker.X,
                              X, attacker.Y, Y, (byte)spell.Range, InLineAlgorithm.Algorithm.DDA);
                                                bool aaAttack = false;
                                                var Array = attacker.Owner.Screen.Objects;
                                                foreach (Interfaces.IMapObject _obj in Array)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= 4)
                                                        {
                                                            if (attacked.UID == Target)
                                                            {
                                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                {
                                                                    aaAttack = true;
                                                                    var damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                    suse.AddTarget(attacked, damage, attack);
                                                                }
                                                            }
                                                            else if (ila.InLine(attacked.X, attacked.Y))
                                                            {
                                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                {
                                                                    aaAttack = true;
                                                                    var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                    suse.AddTarget(attacked, damage, attack);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (attackedsob.UID == Target)
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                aaAttack = true;
                                                                // if (!moveIn.InRange(attackedsob.X, attackedsob.Y, 4, ranger))
                                                                //  continue;
                                                                var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                //damage += damage * 15 / 100;
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                                if (aaAttack)
                                                    attacker.Owner.SendScreen(suse, true);
                                                //attacker.RadiantStamp = DateTime.Now;
                                            }

                                        }
                                        break;
                                    }


                                #endregion
                                #region ShadowClone
                                case 12090:
                                    {
                                        if (attacker.MyClones.Count != 0)
                                        {
                                            foreach (var clone in attacker.MyClones)
                                                clone.RemoveThat();
                                            attacker.MyClones.Clear();
                                            break;
                                        }
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.Attacker1 = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            switch (attacker.Owner.Spells[SpellID].Level)
                                            {
                                                case 0:
                                                case 1:
                                                case 2:
                                                    {
                                                        Clone.CreateShadowClone1(attacker);
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        Clone.CreateShadowClone2(attacker);
                                                        break;
                                                    }
                                            }
                                            foreach (var clone in attacker.MyClones)
                                            {
                                                Data Data = new Data(true);
                                                Data.ID = Data.Revive;
                                                Data.UID = clone.UID;
                                                Data.wParam1 = attacker.X;
                                                Data.wParam2 = attacker.Y;
                                                attacker.Owner.SendScreen(Data, true);
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region FatalSpin
                                case 12110:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            Game.Attacking.InLineAlgorithm ila = new Nyx.Server.Game.Attacking.InLineAlgorithm(attacker.X,
                                        X, attacker.Y, Y, (byte)spell.Distance, InLineAlgorithm.Algorithm.DDA);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (ila.InLine(attacked.X, attacked.Y))
                                                    {
                                                        if (!CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                        suse.Effect1 = attack.Effect1;

                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (ila.InLine(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (!CanAttack(attacker, attackedsob, spell))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Twilight Dance
                                case 12070:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.Attacker1 = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;

                                            TwilightAction(attacker, suse, spell, X, Y);

                                            attacker.Owner.SendScreen(suse, true);
                                            attacker.AttackPacket = null;

                                        }
                                        break;
                                    }
                                #endregion
                                #region ToxicFog
                                case 6001:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Distance)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Player || _obj.MapObjType == MapObjectType.Monster)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (attacked.MapObjType == MapObjectType.Monster)
                                                            if (attacked.MonsterInfo.Boss)
                                                                continue;
                                                        if (Kernel.GetDistance(X, Y, attacked.X, attacked.Y) <= spell.Range)
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                int potDifference = attacker.BattlePower - attacked.BattlePower;

                                                                int rate = spell.Percent + potDifference - 20;

                                                                if (Kernel.Rate(rate))
                                                                {
                                                                    attacked.ToxicFogStamp = Time32.Now;
                                                                    attacked.ToxicFogLeft = 20;
                                                                    attacked.ToxicFogPercent = spell.PowerPercent;
                                                                    attacked.AddFlag(Update.Flags.Poisoned);
                                                                    suse.AddTarget(attacked, 1, null);
                                                                }
                                                                else
                                                                {
                                                                    suse.AddTarget(attacked, 0, null);
                                                                    suse.Targets[attacked.UID].Hit = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region TwofoldBlades
                                case 6000:
                                    {
                                        if (Time32.Now >= attacker.SpellStamp.AddMilliseconds(500))
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                ushort Xx, Yx;
                                                if (attacked != null)
                                                {
                                                    Xx = attacked.X;
                                                    Yx = attacked.Y;
                                                }
                                                else
                                                {
                                                    Xx = attackedsob.X;
                                                    Yx = attackedsob.Y;
                                                }
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, Xx, Yx) <= spell.Range)
                                                {
                                                    if (attackedsob == null)
                                                        if (attacked.ContainsFlag(Network.GamePackets.Update.Flags.Fly))
                                                            return;
                                                    //if (attacked.ContainsFlag(Network.GamePackets.Update.Flags.Fly))
                                                    //  return;
                                                    if (attacker.ContainsFlag(Network.GamePackets.Update.Flags.Fly))
                                                        return;
                                                    PrepareSpell(spell, attacker.Owner);

                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;
                                                    //suse.SpellLevelHu = client_Spell.LevelHu2;
                                                    bool send = false;

                                                    if (attackedsob == null)
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                            suse.AddTarget(attacked, damage, attack);
                                                            send = true;

                                                            if (attacker.Owner.Spells.ContainsKey(11230) && !attacked.Dead)
                                                            {
                                                                var s = attacker.Owner.Spells[11230];
                                                                var spellz = Database.SpellTable.SpellInformations[s.ID][s.Level];
                                                                if (spellz != null)
                                                                {
                                                                    if (Nyx.Server.Kernel.Rate(spellz.Percent))
                                                                    {
                                                                        SpellUse ssuse = new SpellUse(true);
                                                                        ssuse.Attacker = attacker.UID;
                                                                        ssuse.SpellID = spellz.ID;

                                                                        ssuse.SpellLevel = spellz.Level;
                                                                        damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack) / 2;
                                                                        ssuse.AddTarget(attacked, new SpellUse.DamageClass().Damage = damage, attack);
                                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spellz);
                                                                        attacker.Owner.SendScreen(ssuse, true);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Calculate.Melee(attacker, attackedsob, ref attack);
                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                            suse.Effect1 = attack.Effect1;

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                            send = true;
                                                        }
                                                    }
                                                    if (send)
                                                        attacker.Owner.SendScreen(suse, true);
                                                    attacker.SpellStamp = Time32.Now;
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                            }
                                        }
                                        break;
                                    }

                                #endregion
                                #region PoisonStar
                                case 6002:
                                    {
                                        if (attackedsob != null)
                                            return;
                                        if (attacked.EntityFlag == EntityFlag.Monster)
                                            return;
                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                int potDifference = attacker.BattlePower - attacked.BattlePower;

                                                int rate = spell.Percent + potDifference;

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;

                                                if (CanAttack(attacker, attacked, spell, false))
                                                {
                                                    suse.AddTarget(attacked, 1, attack);
                                                    if (Calculate.RateStatus(35))//Kernel.Rate(rate))
                                                    {
                                                        attacked.AddFlag2(Update.Flags2.EffectBall);
                                                        attacked.NoDrugsStamp = Time32.Now;
                                                        attacked.NoDrugsTime = (short)spell.Duration;
                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                            attacked.Owner.Send(Constants.NoDrugs(spell.Duration));
                                                    }
                                                    else
                                                    {
                                                        suse.Targets[attacked.UID].Hit = false;
                                                        suse.Targets[attacked.UID].Damage = spell.ID;
                                                    }
                                                    attacked.Owner.SendScreen(suse, true);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region ArcherBane
                                case 6004:
                                    {
                                        if (attackedsob != null)
                                            return;
                                        if (attacked.EntityFlag == EntityFlag.Monster)
                                            return;
                                        if (!attacked.ContainsFlag(Update.Flags.Fly))
                                            return;
                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                int potDifference = attacker.BattlePower - attacked.BattlePower;

                                                int rate = spell.Percent + potDifference;

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                //suse.SpellLevelHu = client_Spell.LevelHu2;
                                                if (CanAttack(attacker, attacked, spell, false))
                                                {
                                                    uint dmg = Calculate.Percent(attacked, 0.1F);
                                                    suse.AddTarget(attacked, dmg, attack);

                                                    if (Kernel.Rate(rate))
                                                    {
                                                        attacked.Hitpoints -= dmg;
                                                        attacked.RemoveFlag(Update.Flags.Fly);
                                                    }
                                                    else
                                                    {
                                                        suse.Targets[attacked.UID].Hit = false;
                                                    }

                                                    attacked.Owner.SendScreen(suse, true);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region MortalDrag
                                case 11180:
                                    {
                                        if (attacked != null)
                                        {
                                            if (attacked.ContainsFlag(Update.Flags.Fly))
                                                return;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                            {
                                                if (CanUseSpell(spell, attacker.Owner))
                                                {
                                                    if (!Kernel.Rate(Math.Max(5, 100 - (attacked.BattlePower - attacker.BattlePower) / 5))) return;
                                                    PrepareSpell(spell, attacker.Owner);
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    //suse.SpellLevelHu = client_Spell.LevelHu2;
                                                    suse.X = attacked.X;
                                                    suse.Y = attacked.Y;
                                                    ushort newx = attacker.X;
                                                    ushort newy = attacker.Y;
                                                    Map.Pushback(ref newx, ref newy, attacked.Facing, 5);
                                                    if (attacker.Owner.Map.Floor[newx, newy, MapObjectType.Player, attacked])
                                                    {
                                                        suse.X = attacked.X = newx;
                                                        suse.Y = attacked.Y = newy;
                                                    }
                                                    Nyx.Server.Network.GamePackets.SpellUse.DamageClass tar = new SpellUse.DamageClass();
                                                    if (CanAttack(attacker, attacked, spell, false))
                                                    {
                                                        tar.Damage = Calculate.Melee(attacker, attacked, ref attack);
                                                        suse.AddTarget(attacked, tar, attack);
                                                        ReceiveAttack(attacker, attacked, attack, ref tar.Damage, spell);
                                                    }
                                                    if (attacker.EntityFlag == EntityFlag.Player)
                                                        attacker.Owner.SendScreen(suse, true);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region BloodyScythe
                                case 11170:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {

                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            Nyx.Server.Network.GamePackets.SpellUse.DamageClass tar = new SpellUse.DamageClass();
                                            foreach (var t in attacker.Owner.Screen.Objects)
                                            {
                                                if (t == null)
                                                    continue;
                                                if (t.MapObjType == MapObjectType.Player || t.MapObjType == MapObjectType.Monster)
                                                {
                                                    var target = t as Entity;
                                                    if (Kernel.GetDistance(X, Y, target.X, target.Y) <= spell.Range)
                                                    {
                                                        if (CanAttack(attacker, target, spell, false))
                                                        {
                                                            tar.Damage = Calculate.Melee(attacker, target, spell, ref attack, client_Spell.LevelHu2) / 2;
                                                            tar.Hit = true;
                                                            suse.AddTarget(target, tar, attack);
                                                            ReceiveAttack(attacker, target, attack, ref tar.Damage, spell);
                                                        }
                                                    }
                                                }
                                            }

                                            if (attacker.EntityFlag == EntityFlag.Player)
                                                attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #region Monk
                                #region RadiantPalm
                                case 10381:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            attacker.SpellStamp = Time32.Now;
                                            ushort Xx, Yx;
                                            if (attacked != null)
                                            {
                                                Xx = attacked.X;
                                                Yx = attacked.Y;
                                            }
                                            else
                                            {
                                                Xx = attackedsob.X;
                                                Yx = attackedsob.Y;
                                            }
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, Xx, Yx) <= spell.Range)
                                            {
                                                if (attackedsob == null)
                                                    if (attacked.ContainsFlag(Network.GamePackets.Update.Flags.Fly))
                                                        return;
                                                if (attacker.ContainsFlag(Network.GamePackets.Update.Flags.Fly))
                                                    return;
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;

                                                bool send = false;

                                                if (attackedsob == null)
                                                {
                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        var damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                        damage = (uint)(damage * 1.1);
                                                        suse.Effect1 = attack.Effect1;
                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                        suse.AddTarget(attacked, damage, attack);
                                                        send = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                        suse.Effect1 = attack.Effect1;
                                                        suse.AddTarget(attackedsob, damage, attack);
                                                        send = true;
                                                    }
                                                }
                                                if (send)
                                                    attacker.Owner.SendScreen(suse, true);
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region MonkSpells
                                #region EpicMonk
                                #region Tranquility
                                case 10425:
                                    {
                                      /*  if (attacker.EpicMonk())
                                        {
                                            attack.AttackType = Attack.Magic;
                                            attack.Decoded = true;
                                            attack.X = attacked.X;
                                            attack.Y = attacked.Y;
                                            attack.Attacked = attacked.UID;
                                            attack.Attacker = attacker.UID;
                                            attack.Damage = 12560;
                                            goto restart;
                                        }*/
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            if (attacked == null) return;
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            suse.AddTarget(attacked, 0, attack);
                                            if (attacked != null && attacked.EntityFlag == EntityFlag.Player)
                                            {
                                                if (attacked.ContainsFlag2((ulong)Update.Flags2.SoulShackle))
                                                {
                                                    Update upgrade = new Update(true);
                                                    upgrade.UID = attacked.UID;
                                                    upgrade.Append(Network.GamePackets.Update.SoulShackle, 111, 0, 0, 0);
                                                    attacked.Owner.Send(upgrade.Encode());
                                                }
                                                attacked.RemoveFlag2(Update.Flags2.Congelado);
                                                attacked.RemoveFlag2(Update.Flags2.EffectBall);
                                                attacked.lianhuaranLeft = 0;
                                                attacked.NoDrugsTime = 0;// Nyx.Server
                                                if (CaptureTheFlag.IsWar && attacker.GuildID == attacked.GuildID)
                                                {
                                                    CaptureTheFlag.AddExploits(1, attacker.Owner.AsMember);
                                                }
                                                attacked.ToxicFogLeft = 0;
                                                attacked.ScurbyBomb = 0;
                                                attacked.NoDrugsTime = 0;
                                                attacked.DragonFuryTime = 0;
                                                if (attacked.Dead)
                                                {
                                                    attacked.SoulShackleRemover();
                                                }
                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                    attacker.Owner.SendScreen(suse, true);
                                                else attacked.MonsterInfo.SendScreen(suse);
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Tranqulity By Joseph
                           /*     case 10425:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (!attacker.EpicMonk())
                                            {
                                                PrepareSpell(spell, attacker.Owner);
                                                if (attacked == null) return;
                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID; // Nyx.Server
                                                suse.SpellID = SpellID;
                                                suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                suse.AddTarget(attacked, 0, attack);
                                                attacked.ToxicFogLeft = 0;
                                                attacked.RemoveFlag2(Update.Flags2.SoulShackle);
                                                Network.GamePackets.Update upgrade = new Network.GamePackets.Update(true);
                                                upgrade.UID = attacked.UID;
                                                upgrade.Append(Network.GamePackets.Update.SoulShackle
                                                    , 111// Nyx.Server
                                                    , 0, 0, spell.Level);
                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                    attacked.Owner.Send(upgrade.Encode());

                                                attacked.RemoveFlag2(Update.Flags2.Congelado);
                                                attacked.RemoveFlag2(Update.Flags2.EffectBall);
                                                attacked.lianhuaranLeft = 0;
                                                attacked.NoDrugsTime = 0;// Nyx.Server
                                                if (CaptureTheFlag.IsWar && attacker.GuildID == attacked.GuildID)
                                                {
                                                    CaptureTheFlag.AddExploits(1, attacker.Owner.AsMember);
                                                }
                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                    attacked.Owner.SendScreen(suse, true);
                                                else
                                                    attacker.Owner.SendScreen(suse, true);// Nyx.Server
                                            }
                                            else
                                            {
                                                goto case 12560;
                                            }

                                        }
                                        break;
                                    }*/
                                #endregion
                                #region GraceOfHeaven
                                case 12560:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            SpellID = 10425;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            attacker.AttackPacket = null;
                                            if (attacker.Owner.Team != null)
                                            {

                                                foreach (Client.GameClient teammate in attacker.Owner.Team.Teammates)
                                                {
                                                    // if (Kernel.GetDistance(attacker.X, attacker.Y, teammate.Entity.X, teammate.Entity.Y) <= spell.Range)
                                                    {
                                                        if (teammate.Entity.ContainsFlag2((ulong)Update.Flags2.SoulShackle))
                                                        {
                                                            teammate.Entity.RemoveFlag2((ulong)Update.Flags2.SoulShackle);
                                                            suse.AddTarget(teammate.Entity, 0, null);
                                                            Update upgrade = new Update(true);
                                                            upgrade.UID = teammate.Entity.UID;
                                                            upgrade.Append(Network.GamePackets.Update.SoulShackle
                                                            , 111
                                                            , 0, 0, spell.Level);
                                                            if (teammate.Entity.EntityFlag == EntityFlag.Player)
                                                                teammate.Entity.Owner.Send(upgrade.Encode());
                                                        }
                                                    }
                                                }
                                                attacker.Owner.SendScreen(suse, true);

                                            }

                                            {
                                                suse.AddTarget(attacked, 0, attack);

                                                attacked.ToxicFogLeft = 0;
                                                attacked.RemoveFlag2((ulong)Update.Flags2.SoulShackle);

                                                Update upgrade = new Update(true);
                                                upgrade.UID = attacked.UID;
                                                upgrade.Append(Network.GamePackets.Update.SoulShackle
                                                    , 111
                                                    , 0, 0, spell.Level);
                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                    attacked.Owner.Send(upgrade.Encode());

                                                attacked.ToxicFogLeft = 0;
                                                attacked.ScurbyBomb = 0;
                                                attacked.NoDrugsTime = 0;
                                                attacked.DragonFuryTime = 0;
                                                //attacked.Owner.Send(new GameCharacterUpdates(true) { UID = attacked.UID, }
                                                //            .Remove(GameCharacterUpdates.SoulShacle));

                                                attacked.NoDrugsTime = 0;
                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                    attacked.Owner.SendScreen(suse, true);
                                                else
                                                    attacker.Owner.SendScreen(suse, true);
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Grace~Of~Heaven By Nyx.Server(Joseph) ;)

                                /*case 12560:
                                    if (CanUseSpell(spell, attacker.Owner))
                                    {
                                        if (attacker.EpicMonk())
                                        {
                                            if (attacked == null) return;
                                            SpellUse suse = new SpellUse(true);// Nyx.Server
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            suse.AddTarget(attacked, 0, attack);// Nyx.Server
                                            if (attacker.Owner.Team != null)
                                            {
                                                foreach (Client.GameClient teammate in attacker.Owner.Team.Teammates)
                                                {
                                                    if (teammate.Entity.UID != attacker.UID)
                                                    {

                                                        suse.AddTarget(teammate.Entity, spell.Power, null);
                                                        teammate.Entity.ToxicFogLeft = 0;
                                                        teammate.Entity.RemoveFlag2(Update.Flags2.SoulShackle);

                                                        Network.GamePackets.Update upgrade = new Network.GamePackets.Update(true);
                                                        upgrade.UID = teammate.Entity.UID;
                                                        upgrade.Append(Network.GamePackets.Update.SoulShackle
                                                            , 111
                                                            , 0, 0, spell.Level);
                                                        if (teammate.Entity.EntityFlag == EntityFlag.Player)
                                                            teammate.Entity.Owner.Send(upgrade.Encode());

                                                        teammate.Entity.RemoveFlag2(Update.Flags2.Congelado);
                                                        teammate.Entity.RemoveFlag2(Update.Flags2.EffectBall);
                                                        teammate.Entity.lianhuaranLeft = 0;


                                                        teammate.Entity.NoDrugsTime = 0;



                                                        if (teammate.Entity.EntityFlag == EntityFlag.Player)
                                                            teammate.Entity.Owner.SendScreen(suse, true);
                                                        else
                                                            teammate.Entity.Owner.SendScreen(suse, true);


                                                    }
                                                }
                                            }
                                        }

                                    }
                                    break;
                                    */

                                #endregion
                                #endregion
                                #region Auras

                                case 10424:
                                case 10423:
                                case 10422:
                                case 10421:
                                case 10420:
                                //Tyrant Aura
                                case 10395:
                                //Fend Aura
                                case 10410:
                                    {
                                        HandleAura(attacker, spell);
                                    }
                                    break;
                                #endregion
                                #region Compassion
                                case 10430:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;

                                            if (attacker.Owner.Team != null)
                                            {
                                                PrepareSpell(spell, attacker.Owner);
                                                foreach (Client.GameClient teammate in attacker.Owner.Team.Teammates)
                                                {
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, teammate.Entity.X, teammate.Entity.Y) <= spell.Distance)
                                                    {
                                                        teammate.Entity.RemoveFlag(Update.Flags.Poisoned);

                                                        suse.AddTarget(teammate.Entity, 0, attack);
                                                    }
                                                }
                                                attacker.Owner.SendScreen(suse, true);
                                            }
                                            else
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                attacker.RemoveFlag(Update.Flags.Poisoned);

                                                suse.AddTarget(attacker, 0, attack);

                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                    attacked.Owner.SendScreen(suse, true);
                                                else
                                                    attacker.Owner.SendScreen(suse, true);
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Serenity
                                case 10400:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            if (attacker == null) return;

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;

                                            suse.AddTarget(attacker, 0, attack);
                                            attacked.ToxicFogLeft = 0;
                                            attacked.NoDrugsTime = 0;
                                            attacked.RemoveFlag2(Update.Flags2.SoulShackle);
                                            attacked.RemoveFlag2(Update.Flags2.EffectBall);
                                            attacked.lianhuaranLeft = 0;
                                            //                                            attacked.Owner.Send(new GameCharacterUpdates(true) { UID = attacked.UID, }
                                            //                                                        .Remove(GameCharacterUpdates.SoulShacle));
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region //WhirlwindKick
                                case 10415:
                                    {
                                        if (Time32.Now < attacker.SpellStamp.AddMilliseconds(300))
                                        {
                                            attacker.AttackPacket = null; return;
                                        }
                                        if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= 3)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = 0;
                                                suse.X = (ushort)Kernel.Random.Next(3, 10);
                                                suse.Y = 0;
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= 3)
                                                {
                                                    for (int c = 0; c < attacker.Owner.Screen.Objects.Length; c++)
                                                    {
                                                        if (c >= attacker.Owner.Screen.Objects.Length)
                                                            break;
                                                        Interfaces.IMapObject _obj = attacker.Owner.Screen.Objects[c];
                                                        if (_obj == null)
                                                            continue;
                                                        if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                        {
                                                            attacked = _obj as Entity;
                                                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                            {
                                                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Ranged))
                                                                {
                                                                    uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                                    damage = (uint)(damage * 0.5);
                                                                    suse.Effect1 = attack.Effect1;
                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                    attacked.Stunned = true;
                                                                    attacked.StunStamp = Time32.Now;
                                                                    suse.AddTarget(attacked, damage, attack);

                                                                }
                                                            }
                                                        }
                                                    }
                                                    attacker.AttackPacket = null;
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null; return;
                                                }
                                                attacker.Owner.SendScreen(suse, true);
                                                attacker.SpellStamp = Time32.Now;
                                                suse.Targets = new SafeDictionary<uint, SpellUse.DamageClass>();
                                                attacker.AttackPacket = null; return;
                                            }
                                            attacker.AttackPacket = null;
                                        }
                                        attacker.AttackPacket = null; return;
                                    }
                                #endregion
                                #endregion
                                #region UPSweep-DownSweep-Strike
                                case 12580:
                                case 12590:
                                case 12600:
                                    {
                                        SpellUse suse = new SpellUse(true);

                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = attack.SpellID;
                                        suse.SpellLevel = 0;
                                        suse.X = attacker.X;
                                        suse.Y = attacker.Y;
                                        Fan fan = new Fan(attacker.X, attacker.Y, attacked.X, attacked.Y, 7, 240);

                                        foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                        {
                                            if (_obj == null) continue;
                                            if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                            {
                                                attacked = _obj as Entity;
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= 7)
                                                {
                                                    if (CanAttack(attacker, attacked, null, attack.AttackType == Attack.Melee))
                                                    {
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damages = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                        attack.Attacked = attacked.UID;
                                                        attack.Damage = damages;
                                                        suse.Effect1 = attack.Effect1;
                                                        ReceiveAttack(attacker, attacked, attack, ref damages, null);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region WrathOfTheEmperor
                                case 12570:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            suse.SpellEffect = 1;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null) continue;
                                                if (_obj.MapObjType == MapObjectType.Monster)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= 2)
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {

                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                            damage = (uint)(damage * (spell.Power - 30000) / 100);
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= 2)
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {

                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                            damage = damage * 110 / 100;
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= 2)
                                                    {
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {

                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region InfroEcho
                                case 12550:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            suse.SpellEffect = 0;
                                            int count = 3;
                                            List<System.Drawing.Point> Area = new List<Point>();
                                            for (int i = 0; i < 360; i += spell.Sector)
                                            {
                                                if (Area.Count >= count)
                                                {
                                                    break;
                                                }
                                                int r = i;
                                                var distance = Kernel.Random.Next(spell.Range, spell.Distance);
                                                var x2 = (ushort)(X + (distance * Math.Cos(r)));
                                                var y2 = (ushort)(Y + (distance * Math.Sin(r)));
                                                System.Drawing.Point point = new System.Drawing.Point((int)x2, (int)y2);
                                                if (!Area.Contains(point))
                                                {
                                                    Area.Add(point);
                                                }
                                                else
                                                {
                                                    i--;
                                                }
                                            }
                                            foreach (var a in Area)
                                            {
                                                FloorItem item = new FloorItem(true);
                                                item.ItemID = 1397;
                                                item.UID = FloorItem.FloorUID.Next;
                                                item.MapID = attacker.MapID;
                                                item.mColor = 14;
                                                item.ItemColor = Enums.Color.Black;
                                                item.Type = (byte)FloorItem.Effect;
                                                item.Owner = attacker.Owner;
                                                item.OwnerUID = attacker.UID;
                                                item.FlowerType = 1;
                                                item.X = (ushort)a.X;
                                                item.Y = (ushort)a.Y;
                                                item.Name = "InfernalEcho";
                                                item.OnFloor = Time32.Now;
                                                while (attacker.Owner.Map.FloorItems.ContainsKey(item.UID))
                                                    item.UID = FloorItem.FloorUID.Next;
                                                attacker.Owner.Map.AddFloorItem(item);
                                                attacker.Owner.SendScreen(item, true);
                                                attacker.Owner.SendScreenSpawn(item, true);
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #region Pirate
                                #region PirateSpells
                                #region GaleBomb
                                case 11070:

                                    if (CanUseSpell(spell, attacker.Owner))
                                    {

                                        PrepareSpell(spell, attacker.Owner);
                                        Map map;
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        //suse.SpellLevelHu = attacker.Owner.Spells[spell.ID].LevelHu;
                                        suse.X = X;
                                        suse.Y = Y;

                                        Nyx.Server.Network.GamePackets.SpellUse.DamageClass tar = new SpellUse.DamageClass();
                                        int num = 0;

                                        switch (spell.Level)
                                        {
                                            case 0:
                                            case 1:
                                                num = 3;
                                                break;
                                            case 2:
                                            case 3:
                                                num = 4;
                                                break;
                                            default:
                                                num = 5;
                                                break;
                                        }
                                        int i = 0;
                                        Kernel.Maps.TryGetValue(attacker.Owner.Map.BaseID, out map);
                                        foreach (var t in attacker.Owner.Screen.Objects)
                                        {
                                            if (t == null)
                                                continue;
                                            if (t.MapObjType == MapObjectType.Player || t.MapObjType == MapObjectType.Monster)
                                            {
                                                var target = t as Entity;
                                                if (Kernel.GetDistance(X, Y, target.X, target.Y) <= spell.Range)
                                                {
                                                    if (CanAttack(attacker, target, spell, false))
                                                    {
                                                        tar.Damage = Calculate.Melee(attacker, target, spell, ref attack, client_Spell.LevelHu2);
                                                        tar.Hit = true;
                                                        tar.newX = target.X;
                                                        tar.newY = target.Y;
                                                        Map.Pushback(ref tar.newX, ref tar.newY, attacker.Facing, 5);

                                                        if (map != null)
                                                        {
                                                            if (map.Floor[tar.newX, tar.newY, MapObjectType.Player, attacker])
                                                            {
                                                                target.X = tar.newX;
                                                                target.Y = tar.newY;
                                                            }
                                                            else
                                                            {
                                                                tar.newX = target.X;
                                                                tar.newY = target.Y;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (attacker.Owner.Map.Floor[tar.newX, tar.newY, MapObjectType.Player, attacker])
                                                            {
                                                                target.X = tar.newX;
                                                                target.Y = tar.newY;
                                                            }
                                                            else
                                                            {
                                                                target.X = tar.newX;
                                                                target.Y = tar.newY;
                                                            }
                                                        }

                                                        suse.AddTarget(target, tar, attack);
                                                        ReceiveAttack(attacker, target, attack, ref tar.Damage, spell);
                                                        i++;
                                                        if (i > num) break;
                                                    }
                                                }
                                            }
                                        }

                                        if (attacker.EntityFlag == EntityFlag.Player)
                                            attacker.Owner.SendScreen(suse, true);
                                    }
                                    break;
                                #endregion
                                #region BladeTempest
                                case 11110:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            suse.SpellLevel = spell.Level;
                                            ushort _X = attacker.X, _Y = attacker.Y;
                                            ushort _tX = X, _tY = Y;
                                            byte dist = (byte)spell.Distance;
                                            var Array = attacker.Owner.Screen.Objects;
                                            InLineAlgorithm algo = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, dist,
                                                                               InLineAlgorithm.Algorithm.DDA);
                                            X = attacker.X;
                                            Y = attacker.Y;
                                            int i = 0;
                                            for (i = 0; i < algo.lcoords.Count; i++)
                                            {
                                                if (attacker.Owner.Map.Floor[algo.lcoords[i].X, algo.lcoords[i].Y, MapObjectType.Player]
                                                    && !attacker.ThroughGate(algo.lcoords[i].X, algo.lcoords[i].Y))
                                                {
                                                    X = (ushort)algo.lcoords[i].X;
                                                    Y = (ushort)algo.lcoords[i].Y;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (!attacker.Owner.Map.Floor[X, Y, MapObjectType.Player, null])
                                                return;
                                            double disth = 1.5;

                                            foreach (Interfaces.IMapObject _obj in Array)
                                            {
                                                bool hit = false;
                                                for (int j = 0; j < i; j++)
                                                    if (Kernel.GetDDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[j].X, (ushort)algo.lcoords[j].Y) <= disth)
                                                        hit = true;
                                                if (hit)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.X = X;
                                            attacker.Y = Y;
                                            attacker.Owner.SendScreen(suse, true);
                                            attacker.Owner.Screen.Reload(suse);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Kraken`sRevenge
                                case 11100:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {

                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            int num = 0;

                                            switch (spell.Level)
                                            {
                                                case 0:
                                                case 1:
                                                    num = 3;
                                                    break;
                                                case 2:
                                                case 3:
                                                    num = 4;
                                                    break;
                                                default:
                                                    num = 5;
                                                    break;
                                            }

                                            int i = 0;
                                            BlackSpotPacket bsp = new BlackSpotPacket();
                                            foreach (var t in attacker.Owner.Screen.Objects)
                                            {
                                                if (t == null)
                                                    continue;
                                                if (t.MapObjType == MapObjectType.Player || t.MapObjType == MapObjectType.Monster)
                                                {
                                                    var target = t as Entity;
                                                    if (CanAttack(attacker, target, spell, false))
                                                    {
                                                        target.IsBlackSpotted = true;
                                                        target.BlackSpotStamp = Time32.Now;
                                                        target.BlackSpotStepSecs = spell.Duration;
                                                        Kernel.BlackSpoted.TryAdd(target.UID, target);
                                                        suse.AddTarget(target, new SpellUse.DamageClass(), attack);
                                                        i++;
                                                        if (i == num) break;
                                                    }
                                                }
                                            }
                                            if (attacker.EntityFlag == EntityFlag.Player)
                                                attacker.Owner.SendScreen(suse, true);

                                            foreach (var h in Kernel.GamePool.Values)
                                            {
                                                foreach (var t in suse.Targets.Keys)
                                                {
                                                    h.Send(bsp.ToArray(true, t));
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region ScurvyBomb
                                case 11040:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Distance)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (Kernel.GetDistance(X, Y, attacked.X, attacked.Y) <= spell.Range)
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                int potDifference = attacker.BattlePower - attacked.BattlePower;
                                                                int rate = spell.Percent + potDifference;
                                                                if (Kernel.Rate(rate))
                                                                {
                                                                    if (_obj.MapObjType == MapObjectType.Player)
                                                                    {
                                                                        attacked.ScurbyBombStamp = Time32.Now;
                                                                        attacked.ScurbyBomb2Stamp = Time32.Now;
                                                                        attacked.ScurbyBomb = 15;
                                                                        attacked.AddFlag2((ulong)Update.Flags2.ScurvyBomb);
                                                                        Update upgrade = new Update(true);
                                                                        upgrade.UID = attacked.UID;
                                                                        upgrade.Append(Network.GamePackets.Update.Fatigue, 15, 8, 0, 0);
                                                                        attacked.Owner.Send(upgrade.Encode());
                                                                    }
                                                                    attack.Effect1 = Attack.AttackEffects1.None;
                                                                    uint damage = Calculate.Ranged(attacker, attacked, ref attack);
                                                                    suse.Effect1 = attack.Effect1;
                                                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                    suse.Targets.Add(attacked.UID, damage);
                                                                }
                                                                else
                                                                {
                                                                    suse.Targets.Add(attacked.UID, 0);
                                                                    suse.Targets[attacked.UID].Hit = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Cannon Barrage
                                case 11050:
                                    {
                                        if (attacker.Owner.Entity.ContainsFlag(Nyx.Server.Network.GamePackets.Update.Flags.XPList))
                                        {
                                            attacker.Owner.Entity.RemoveFlag(Nyx.Server.Network.GamePackets.Update.Flags.XPList);
                                            attacker.Owner.Entity.AddFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.CannonBarrage);
                                            attacker.Owner.Entity.CannonBarrageStamp = Time32.Now;
                                            return;
                                        }


                                        PrepareSpell(spell, attacker.Owner);

                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = X;
                                        suse.Y = Y;
                                        //suse.SpellLevelHu = client_Spell.LevelHu2;
                                        Nyx.Server.Network.GamePackets.SpellUse.DamageClass tar = new SpellUse.DamageClass();
                                        foreach (var t in attacker.Owner.Screen.Objects)
                                        {
                                            if (t == null)
                                                continue;
                                            if (t.MapObjType == MapObjectType.Player || t.MapObjType == MapObjectType.Monster)
                                            {
                                                var target = t as Entity;
                                                if (Kernel.GetDistance(attacker.Owner.Entity.X, attacker.Owner.Entity.Y, target.X, target.Y) <= spell.Range)
                                                {
                                                    if (CanAttack(attacker, target, spell, false))
                                                    {
                                                        tar.Damage = Calculate.Ranged(attacker, target, ref attack, client_Spell.LevelHu2);
                                                        suse.AddTarget(target, tar, attack);
                                                        ReceiveAttack(attacker, target, attack, ref tar.Damage, spell);
                                                    }
                                                }
                                            }
                                        }

                                        if (attacker.EntityFlag == EntityFlag.Player)
                                            attacker.Owner.SendScreen(suse, true);

                                        break;
                                    }
                                #endregion
                                #region Blackbeard`sRage
                                case 11060:
                                    {
                                        if (attacker.Owner.Entity.ContainsFlag(Nyx.Server.Network.GamePackets.Update.Flags.XPList))
                                        {
                                            attacker.Owner.Entity.RemoveFlag(Nyx.Server.Network.GamePackets.Update.Flags.XPList);
                                            attacker.Owner.Entity.AddFlag2(Nyx.Server.Network.GamePackets.Update.Flags2.BlackbeardsRage);
                                            attacker.Owner.Entity.BlackbeardsRageStamp = Time32.Now;
                                            return;
                                        }

                                        int num = 0;
                                        switch (spell.Level)
                                        {
                                            case 0:
                                            case 1:
                                                num = 3;
                                                break;
                                            case 2:
                                            case 3:
                                                num = 4;
                                                break;
                                            default:
                                                num = 5;
                                                break;
                                        }
                                        int i = 0;
                                        PrepareSpell(spell, attacker.Owner);

                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = X;
                                        suse.Y = Y;
                                        //suse.SpellLevelHu = client_Spell.LevelHu2;
                                        Nyx.Server.Network.GamePackets.SpellUse.DamageClass tar = new SpellUse.DamageClass();
                                        foreach (var t in attacker.Owner.Screen.Objects)
                                        {
                                            if (t == null)
                                                continue;
                                            if (t.MapObjType == MapObjectType.Player || t.MapObjType == MapObjectType.Monster)
                                            {
                                                var target = t as Entity;
                                                if (Kernel.GetDistance(attacker.Owner.Entity.X, attacker.Owner.Entity.Y, target.X, target.Y) <= spell.Range)
                                                {
                                                    if (CanAttack(attacker, target, spell, false))
                                                    {
                                                        tar.Damage = Calculate.Ranged(attacker, target, ref attack, client_Spell.LevelHu2);
                                                        suse.AddTarget(target, tar, attack);
                                                        ReceiveAttack(attacker, target, attack, ref tar.Damage, spell);
                                                        i++;
                                                        if (i == num) break;
                                                    }
                                                }
                                            }
                                        }

                                        if (attacker.EntityFlag == EntityFlag.Player)
                                            attacker.Owner.SendScreen(suse, true);

                                        break;
                                    }
                                #endregion
                                #endregion
                                #endregion
                                #region BruceLee
                                #region LeeLong
                                #region DragonFlow
                                case 12270:
                                    {

                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = attacker.UID;
                                        spellUse.Attacker1 = attacker.UID;
                                        spellUse.SpellID = spell.ID;
                                        spellUse.SpellLevel = spell.Level;
                                        spellUse.X = X;
                                        spellUse.Y = Y;
                                        spellUse.AddTarget(attacker, 1, attack);
                                        attacker.Owner.SendScreen(spellUse, true);
                                        if (attacker.ContainsFlag3((uint)Update.Flags3.DragonFlow))
                                            attacker.RemoveFlag3((uint)Update.Flags3.DragonFlow);
                                        else
                                            attacker.AddFlag3((uint)Update.Flags3.DragonFlow);
                                        attacker.DragonFlowStamp = Time32.Now;
                                        break;
                                    }
                                #endregion
                                #region DragonRoar
                                case 12280:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            PrepareSpell(spell, attacker.Owner);
                                            if (attacker.Owner.Team != null)
                                            {
                                                foreach (Client.GameClient teammate in attacker.Owner.Team.Teammates)
                                                {
                                                    if (teammate.Entity.UID != attacker.UID)
                                                    {
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, teammate.Entity.X, teammate.Entity.Y) <= spell.Range)
                                                        {
                                                            teammate.Entity.Stamina += (byte)spell.Power;
                                                            suse.AddTarget(teammate.Entity, spell.Power, null);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region DragonCyclone
                                case 12290:
                                    {
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = X;
                                        suse.Y = Y;
                                        if (attacker.ContainsFlag(Update.Flags.XPList))
                                        {
                                            attacker.RemoveFlag(Update.Flags.XPList);
                                            attacker.AddFlag3(Update.Flags3.DragonCyclone);
                                            attacker.DragonCycloneStamp = Time32.Now;
                                            attacker.Owner.SendScreen(suse, true);

                                        }
                                        else
                                        {
                                            ushort _X = attacker.X, _Y = attacker.Y;
                                            ushort _tX = X, _tY = Y;
                                            byte dist = (byte)spell.Distance;
                                            var Array = attacker.Owner.Screen.Objects;
                                            InLineAlgorithm algo = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, dist,
                                                                               InLineAlgorithm.Algorithm.DDA);
                                            X = attacker.X;
                                            Y = attacker.Y;
                                            int i = 0;
                                            for (i = 0; i < algo.lcoords.Count; i++)
                                            {
                                                if (attacker.Owner.Map.Floor[algo.lcoords[i].X, algo.lcoords[i].Y, MapObjectType.Player]
                                                    && !attacker.ThroughGate(algo.lcoords[i].X, algo.lcoords[i].Y))
                                                {
                                                    X = (ushort)algo.lcoords[i].X;
                                                    Y = (ushort)algo.lcoords[i].Y;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (!attacker.Owner.Map.Floor[X, Y, MapObjectType.Player, null])
                                                return;
                                            double disth = 1.5;

                                            foreach (Interfaces.IMapObject _obj in Array)
                                            {
                                                bool hit = false;
                                                for (int j = 0; j < i; j++)
                                                    if (Kernel.GetDDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[j].X, (ushort)algo.lcoords[j].Y) <= disth)
                                                        hit = true;
                                                if (hit)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                            damage = (uint)(damage * 0.5);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.X = X;
                                            attacker.Y = Y;
                                            attacker.Owner.SendScreen(suse, true);
                                            attacker.Owner.Screen.Reload(suse);

                                        }
                                        break;
                                    }
                                #endregion
                                #region DragonSlash
                                case 12350:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            Game.Attacking.InLineAlgorithm ila = new Nyx.Server.Game.Attacking.InLineAlgorithm(attacker.X,
                                        X, attacker.Y, Y, (byte)spell.Range, InLineAlgorithm.Algorithm.DDA);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.Attacker1 = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (ila.InLine(attacked.X, attacked.Y))
                                                    {
                                                        if (!CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                        suse.Effect1 = attack.Effect1;

                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (ila.InLine(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (!CanAttack(attacker, attackedsob, spell))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region AirKick
                                case 12320:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            suse.SpellLevel = spell.Level;
                                            ushort _X = attacker.X, _Y = attacker.Y;
                                            ushort _tX = X, _tY = Y;
                                            byte dist = (byte)spell.Distance;
                                            var Array = attacker.Owner.Screen.Objects;
                                            InLineAlgorithm algo = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, dist,
                                                                               InLineAlgorithm.Algorithm.DDA);
                                            X = attacker.X;
                                            Y = attacker.Y;
                                            int i = 0;
                                            for (i = 0; i < algo.lcoords.Count; i++)
                                            {
                                                if (attacker.Owner.Map.Floor[algo.lcoords[i].X, algo.lcoords[i].Y, MapObjectType.Player]
                                                    && !attacker.ThroughGate(algo.lcoords[i].X, algo.lcoords[i].Y))
                                                {
                                                    X = (ushort)algo.lcoords[i].X;
                                                    Y = (ushort)algo.lcoords[i].Y;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (!attacker.Owner.Map.Floor[X, Y, MapObjectType.Player, null])
                                                return;
                                            double disth = 1.5;

                                            foreach (Interfaces.IMapObject _obj in Array)
                                            {
                                                bool hit = false;
                                                for (int j = 0; j < i; j++)
                                                    if (Kernel.GetDDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[j].X, (ushort)algo.lcoords[j].Y) <= disth)
                                                        hit = true;
                                                if (hit)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Ranged(attacker, attacked, ref attack);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.X = X;
                                            attacker.Y = Y;
                                            attacker.Owner.SendScreen(suse, true);
                                            attacker.Owner.Screen.Reload(suse);
                                        }
                                        break;
                                    }
                                #endregion
                                #region AirSweep and AirRaid
                                case 12330:
                                case 12210:
                                case 12340:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                            sector.Arrange(spell.Sector, spell.Range);
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj == null)
                                                        continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;

                                                        if (sector.Inside(attacked.X, attacked.Y))
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                                                suse.Effect1 = attack.Effect1;

                                                                ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                suse.AddTarget(attacked, damage, attack);
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;

                                                        if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Speed Kick
                                case 12120:
                                case 12130:
                                case 12140:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            suse.SpellLevel = spell.Level;
                                            ushort _X = attacker.X, _Y = attacker.Y;
                                            ushort _tX = X, _tY = Y;
                                            byte dist = (byte)spell.Distance;
                                            var Array = attacker.Owner.Screen.Objects;
                                            InLineAlgorithm algo = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, dist,
                                                                               InLineAlgorithm.Algorithm.DDA);
                                            X = attacker.X;
                                            Y = attacker.Y;
                                            int i = 0;
                                            for (i = 0; i < algo.lcoords.Count; i++)
                                            {
                                                if (attacker.Owner.Map.Floor[algo.lcoords[i].X, algo.lcoords[i].Y, MapObjectType.Player]
                                                    && !attacker.ThroughGate(algo.lcoords[i].X, algo.lcoords[i].Y))
                                                {
                                                    X = (ushort)algo.lcoords[i].X;
                                                    Y = (ushort)algo.lcoords[i].Y;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (!attacker.Owner.Map.Floor[X, Y, MapObjectType.Player, null])
                                                return;
                                            double disth = 1.5;

                                            foreach (Interfaces.IMapObject _obj in Array)
                                            {
                                                bool hit = false;
                                                for (int j = 0; j < i; j++)
                                                    if (Kernel.GetDDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[j].X, (ushort)algo.lcoords[j].Y) <= disth)
                                                        hit = true;
                                                if (hit)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                                            damage = (uint)(damage * 2);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.X = X;
                                            attacker.Y = Y;
                                            attacker.Owner.SendScreen(suse, true);
                                            attacker.Owner.Screen.Reload(suse);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Cracking swipe
                                case 12170:
                                case 12160:
                                case 12220:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            Fan sector = new Fan(attacker.X, attacker.Y, X, Y, spell.Range, spell.Sector);
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Distance + 1)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj == null)
                                                        continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;

                                                        if (sector.IsInFan(attacked.X, attacked.Y))
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                                                damage = (uint)(damage * 0.5);
                                                                suse.Effect1 = attack.Effect1;
                                                                ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                suse.AddTarget(attacked, damage, attack);
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;

                                                        if (sector.IsInFan(attackedsob.X, attackedsob.Y))
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Dragon Punch
                                case 12240:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            Game.Attacking.InLineAlgorithm ila = new Nyx.Server.Game.Attacking.InLineAlgorithm(attacker.X,
                                        X, attacker.Y, Y, (byte)spell.Range, InLineAlgorithm.Algorithm.DDA);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (ila.InLine(attacked.X, attacked.Y))
                                                    {
                                                        if (!CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                        suse.Effect1 = attack.Effect1;

                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (ila.InLine(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (!CanAttack(attacker, attackedsob, spell))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }

                                #endregion
                                #region DragonFury
                                case 12300:
                                    {
                                        if (attacked != null)
                                        {
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                            {
                                                if (CanUseSpell(spell, attacker.Owner))
                                                {
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;

                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                        suse.Effect1 = attack.Effect1;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                        attacker.Owner.Entity.IsEagleEyeShooted = true;

                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                            attacked.Owner.SendScreen(suse, true);
                                                        else
                                                            attacked.MonsterInfo.SendScreen(suse);
                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                        {
                                                            int potDifference = attacker.BattlePower - attacked.BattlePower;

                                                            int rate = spell.Percent + potDifference - 20;
                                                            if (Kernel.Rate(rate))
                                                            {

                                                                attacked.AddFlag3(Update.Flags3.DragonFury);
                                                                attacked.DragonFuryStamp = Time32.Now;
                                                                attacked.DragonFuryTime = spell.Duration;

                                                                Network.GamePackets.Update upgrade = new Network.GamePackets.Update(true);
                                                                upgrade.UID = attacked.UID;
                                                                upgrade.Append(74
                                                                    , (uint)spell.Status
                                                                    , (uint)spell.Duration, spell.Power, spell.Level);
                                                                attacker.Owner.Send(upgrade.Encode());

                                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                                    attacked.Owner.Send(upgrade);
                                                                else
                                                                    attacked.MonsterInfo.SendScreen(upgrade);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region DragonSwing
                                case 12200:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.Attacker = attacker.UID;
                                            spellUse.SpellID = spell.ID;
                                            spellUse.SpellLevel = spell.Level;
                                            spellUse.X = X;
                                            spellUse.Y = Y;
                                            attacker.Owner.SendScreen(spellUse, true);
                                            if (attacker.OnDragonSwing)
                                            {
                                                attacker.OnDragonSwing = false;
                                                attacker.RemoveFlag3(Update.Flags3.DragonSwing);
                                                Update upgrade = new Update(true);
                                                upgrade.UID = attacker.UID;
                                                upgrade.Append(Update.DragonSwing, (uint)spell.Status, 0, 0, 0);
                                                attacker.Owner.Send(upgrade.Encode());
                                            }
                                            else
                                            {
                                                attacker.OnDragonSwing = true;
                                                attacker.DragonSwingPower = (ushort)spell.Power;
                                                attacker.AddFlag3(Update.Flags3.DragonSwing);
                                                Update upgrade = new Update(true);
                                                upgrade.UID = attacker.UID;
                                                upgrade.Append(Update.DragonSwing, (uint)spell.Duration, (uint)spell.Status, spell.Power, spell.Level);
                                                attacker.Owner.Send(upgrade.Encode());
                                                attacker.DragonSwingStamp = Time32.Now;
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #endregion
                                #region Toaist
                                #region Single heal/meditation spells
                                case 1190:
                                case 1195:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;

                                            uint damage = spell.Power;
                                            if (spell.ID == 1190)
                                            {
                                                Experience = damage = Math.Min(damage, attacker.MaxHitpoints - attacker.Hitpoints);
                                                attacker.Hitpoints += damage;
                                            }
                                            else
                                            {
                                                Experience = damage = Math.Min(damage, (uint)(attacker.MaxMana - attacker.Mana));
                                                attacker.Mana += (ushort)damage;
                                            }

                                            suse.AddTarget(attacker, spell.Power, attack);

                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region AuroraLotus
                                case 12370:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {

                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            attacker.Owner.SendScreen(suse, true);
                                            FloorItem item = new FloorItem(true);
                                            item.UID = FloorItem.FloorUID.Next;
                                            item.ItemID = FloorItem.AuroraLotus;
                                            item.MapID = attacker.MapID;
                                            item.X = X;
                                            item.Y = Y;
                                            item.time = 0;
                                            item.ItemColor = (Game.Enums.Color)25;
                                            item.Type = FloorItem.Effect;
                                            item.mColor = 13;
                                            item.OwnerUID = attacker.UID;
                                            item.OwnerGuildUID = attacker.GuildID;
                                            item.Name = spell.Name;
                                            item.Owner = attacker.Owner;
                                            item.OnFloor = Time32.Now;
                                            item.FlowerType = 3;
                                            item.time = Kernel.Gettime();
                                            attacker.Owner.Map.AddFloorItem(item);
                                            attacker.Owner.SendScreenSpawn(item, true);
                                            attacker.Energy = 0;
                                        }
                                        break;
                                    }
                                #endregion
                                #region FlameLotus
                                case 12380:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {

                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            attacker.Owner.SendScreen(suse, true);
                                            FloorItem item = new FloorItem(true);
                                            item.UID = FloorItem.FloorUID.Next;
                                            item.ItemID = FloorItem.FlameLotus;
                                            item.MapID = attacker.MapID;
                                            item.X = X;
                                            item.Y = Y;
                                            item.ItemColor = (Game.Enums.Color)25;
                                            item.Type = FloorItem.Effect;
                                            item.mColor = 13;
                                            item.OwnerUID = attacker.UID;
                                            item.OwnerGuildUID = attacker.GuildID;
                                            item.Name = spell.Name;
                                            item.Owner = attacker.Owner;
                                            item.OnFloor = Time32.Now;
                                            item.FlowerType = 3;
                                            item.time = Kernel.Gettime();
                                            attacker.Owner.Map.AddFloorItem(item);
                                            attacker.Owner.SendScreenSpawn(item, true);
                                            attacker.Energy = 0;
                                        }
                                        break;
                                    }
                                #endregion
                                #region Revive
                                case 1050:
                                case 1100:
                                    {
                                        if (attackedsob != null)
                                            return;
                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <=
                                        spell.Distance)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);
                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;
                                                // suse.SoulLevel = client_Spell.SoulLevel; 
                                                suse.AddTarget(attacked, 0, attack);
                                                attacked.Owner.Entity.Action =
                                                Game.Enums.ConquerAction.None;
                                                attacked.Owner.ReviveStamp = Time32.Now;
                                                attacked.Owner.Attackable = false;
                                                attacked.Owner.Entity.AutoRev = 0;
                                                attacked.Owner.Entity.TransformationID = 0;
                                                attacked.Owner.Entity.RemoveFlag((ulong)Update.Flags.Dead);
                                                attacked.Owner.Entity.RemoveFlag((ulong)Update.Flags.Ghost);
                                                attacked.Owner.Entity.Hitpoints = attacked.Owner.Entity.MaxHitpoints;
                                                attacked.Ressurect();
                                                attacked.Owner.SendScreen(suse, true);
                                                if (attacked.Owner.Spells.ContainsKey(12660) && attacked.EpicWarrior())
                                                {
                                                    attacked.Owner.XPCount = attacked.Owner.Entity.XPCountTwist;
                                                }
                                                if (CaptureTheFlag.IsWar && attacker.GuildID == attacked.GuildID)
                                                {
                                                    CaptureTheFlag.AddExploits(2, attacker.Owner.AsMember);
                                                }
                                                if (attacker.Owner.Spells.ContainsKey(12390))
                                                {
                                                    if (Kernel.Rate(50))
                                                    {
                                                        SpellUse susee = new SpellUse(true);
                                                        susee.Attacker = attacker.UID;
                                                        susee.SpellID = 12390;
                                                        susee.SpellLevel = 6;
                                                        susee.X = attacked.X;
                                                        susee.Y = attacked.Y;
                                                        attacked.Owner.SendScreen(susee, true);
                                                        attacked.AddFlag((ulong)Update.Flags.Stigma);
                                                        attacked.StigmaStamp = Time32.Now;
                                                        attacked.StigmaIncrease = 15;
                                                        attacked.StigmaTime = 45;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #endregion
                                #region Single magic damage spells
                                case 11030:
                                case 1000:
                                case 1001:
                                case 1002:
                                case 1150:
                                case 1160:
                                case 1180:
                                case 1320:
                                    //case 11040:
                                    //case 10381:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (spell.ID == 10310)
                                            {
                                                if (Time32.Now > attacker.EagleEyeStamp.AddSeconds(20))
                                                    return;
                                                attacker.EagleEyeStamp = Time32.Now;
                                            }
                                            if (attacked != null)
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                                {
                                                    if (spell.ID == 1002)
                                                    {
                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                        {
                                                            if (attacker.Owner.Spells.ContainsKey(12400))
                                                            {
                                                                if (Kernel.Rate(10))
                                                                {
                                                                    var skill = attacker.Owner.Spells[12400];
                                                                    SpellUse susee = new SpellUse(true);
                                                                    susee.Attacker = attacker.UID;
                                                                    susee.SpellID = skill.ID;
                                                                    susee.SpellLevel = skill.Level;
                                                                    susee.X = X;
                                                                    susee.Y = Y;
                                                                    if (attacked.EntityFlag == EntityFlag.Player)
                                                                        attacked.Owner.SendScreen(susee, true);
                                                                    attacker.Owner.IncreaseSpellExperience(100, skill.ID);

                                                                }
                                                            }
                                                        }
                                                    }
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack);
                                                        if (spell.ID == 11030)
                                                            damage = (uint)((damage) / 1.1);
                                                        if (spell.ID == 1002)
                                                            damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                        if (spell.ID == 1002)
                                                            damage = (uint)((damage) / 1.5);
                                                        if (spell.ID == 1001)
                                                            damage = (uint)((damage) / 1.5);
                                                        if (spell.ID == 1000)
                                                            damage = (uint)((damage) / 1.5);
                                                        suse.Effect1 = attack.Effect1;

                                                        suse.Effect1 = attack.Effect1;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                        attacker.Owner.Entity.IsEagleEyeShooted = true;

                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                            attacked.Owner.SendScreen(suse, true);
                                                        else
                                                            attacked.MonsterInfo.SendScreen(suse);

                                                        var attackd = attacked as Entity;
                                                        if (Kernel.BlackSpoted.ContainsKey(attackd.UID) && spell.ID == 11030)
                                                        {
                                                            attacker.Owner.Entity.IsEagleEyeShooted = false;
                                                            if (attacker.Owner.Spells.ContainsKey(11130))
                                                            {
                                                                var s = attacker.Owner.Spells[11130];
                                                                var sspell = Database.SpellTable.SpellInformations[s.ID][s.Level];
                                                                if (spell != null)
                                                                {
                                                                    attacker.EagleEyeStamp = Time32.Now;
                                                                    attacker.Owner.Entity.IsEagleEyeShooted = false;
                                                                    SpellUse ssuse = new SpellUse(true);
                                                                    ssuse.Attacker = attacker.UID;
                                                                    ssuse.SpellID = sspell.ID;
                                                                    ssuse.SpellLevel = sspell.Level;
                                                                    ssuse.AddTarget(attacker.Owner.Entity, new SpellUse.DamageClass().Damage = 11030, attack);
                                                                    if (attacker.EntityFlag == EntityFlag.Player)
                                                                    {
                                                                        attacker.Owner.SendScreen(ssuse, true);
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                            }
                                            else
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Distance)
                                                {
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                        suse.Effect1 = attack.Effect1;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);

                                                        attacker.Owner.SendScreen(suse, true);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            attacker.AttackPacket = null;
                                        }
                                        break;
                                    }
                                #endregion
                                #region Multi heal spells
                                case 1005:
                                case 1055:
                                case 1170:
                                case 1175:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (attackedsob != null)
                                            {
                                                if (attacker.MapID == 1038)
                                                    break;
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Distance)
                                                {
                                                    PrepareSpell(spell, attacker.Owner);

                                                    uint damage = spell.Power;
                                                    damage = Math.Min(damage, attackedsob.MaxHitpoints - attackedsob.Hitpoints);
                                                    attackedsob.Hitpoints += damage;
                                                    Experience += damage;
                                                    suse.AddTarget(attackedsob, damage, attack);

                                                    attacker.Owner.SendScreen(suse, true);
                                                }
                                            }
                                            else
                                            {
                                                if (spell.Multi)
                                                {
                                                    if (attacker.Owner.Team != null)
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        foreach (Client.GameClient teammate in attacker.Owner.Team.Teammates)
                                                        {
                                                            if (Kernel.GetDistance(attacker.X, attacker.Y, teammate.Entity.X, teammate.Entity.Y) <= spell.Distance)
                                                            {
                                                                uint damage = spell.Power;
                                                                damage = Math.Min(damage, teammate.Entity.MaxHitpoints - teammate.Entity.Hitpoints);
                                                                teammate.Entity.Hitpoints += damage;
                                                                Experience += damage;
                                                                suse.AddTarget(teammate.Entity, damage, attack);

                                                                if (spell.NextSpellID != 0)
                                                                {
                                                                    attack.Damage = spell.NextSpellID;
                                                                    attacker.AttackPacket = attack;
                                                                }
                                                                else
                                                                {
                                                                    attacker.AttackPacket = null;
                                                                }
                                                            }
                                                        }
                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                            attacked.Owner.SendScreen(suse, true);
                                                        else
                                                            attacked.MonsterInfo.SendScreen(suse);
                                                    }
                                                    else
                                                    {
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                                        {
                                                            PrepareSpell(spell, attacker.Owner);

                                                            uint damage = spell.Power;
                                                            damage = Math.Min(damage, attacked.MaxHitpoints - attacked.Hitpoints);
                                                            attacked.Hitpoints += damage;
                                                            Experience += damage;
                                                            suse.AddTarget(attacked, damage, attack);

                                                            if (spell.NextSpellID != 0)
                                                            {
                                                                attack.Damage = spell.NextSpellID;
                                                                attacker.AttackPacket = attack;
                                                            }
                                                            else
                                                            {
                                                                attacker.AttackPacket = null;
                                                            }
                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                attacked.Owner.SendScreen(suse, true);
                                                            else
                                                                attacked.MonsterInfo.SendScreen(suse);
                                                        }
                                                        else
                                                        {
                                                            attacker.AttackPacket = null;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);

                                                        uint damage = spell.Power;
                                                        damage = Math.Min(damage, attacked.MaxHitpoints - attacked.Hitpoints);
                                                        attacked.Hitpoints += damage;
                                                        Experience += damage;
                                                        suse.AddTarget(attacked, damage, attack);

                                                        if (spell.NextSpellID != 0)
                                                        {
                                                            attack.Damage = spell.NextSpellID;
                                                            attacker.AttackPacket = attack;
                                                        }
                                                        else
                                                        {
                                                            attacker.AttackPacket = null;
                                                        }
                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                            attacked.Owner.SendScreen(suse, true);
                                                        else
                                                            attacked.MonsterInfo.SendScreen(suse);
                                                    }
                                                    else
                                                    {
                                                        attacker.AttackPacket = null;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            attacker.AttackPacket = null;
                                        }
                                        break;
                                    }
                                #endregion
                                #region Linear spells

                                case 1045:
                                case 1046:
                                case 11000:
                                case 11005:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            Game.Attacking.InLineAlgorithm ila = new Nyx.Server.Game.Attacking.InLineAlgorithm(attacker.X,
                                        X, attacker.Y, Y, (byte)spell.Range, InLineAlgorithm.Algorithm.DDA);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (ila.InLine(attacked.X, attacked.Y))
                                                    {
                                                        if (!CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                        suse.Effect1 = attack.Effect1;

                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (ila.InLine(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (!CanAttack(attacker, attackedsob, spell))
                                                            continue;

                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        attack.Damage = damage;

                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region XPSpells inofensive
                                case 1015:
                                case 1020:
                                case 1025:
                                case 1110:
                                case 6011:
                                case 10390:
                                    {
                                        //case 11060: {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;

                                            suse.AddTarget(attacked, 0, attack);

                                            if (spell.ID == 6011)
                                            {
                                                attacked.FatalStrikeStamp = Time32.Now;
                                                attacked.FatalStrikeTime = 60;
                                                attacked.AddFlag(Update.Flags.FatalStrike);
                                                attacker.RemoveFlag(Update.Flags.Ride);
                                            }
                                            else
                                            {
                                                if (spell.ID == 1110 || spell.ID == 1025 || spell.ID == 10390)
                                                {

                                                    if (!attacked.OnKOSpell())
                                                        attacked.KOCount = 0;
                                                    attacked.KOSpell = spell.ID;
                                                    if (spell.ID == 1110)
                                                    {
                                                        attacked.CycloneStamp = Time32.Now;
                                                        attacked.CycloneTime = 20;
                                                        attacked.AddFlag((ulong)Update.Flags.Cyclone);
                                                    }
                                                    else if (spell.ID == 10390)
                                                    {
                                                        attacked.OblivionStamp = Time32.Now;
                                                        attacked.OblivionTime = 20;
                                                        attacked.AddFlag2((ulong)Update.Flags2.Oblivion);
                                                    }
                                                    else
                                                    {
                                                        attacked.SupermanStamp = Time32.Now;
                                                        attacked.SupermanTime = 20;
                                                        attacked.AddFlag((ulong)Update.Flags.Superman);
                                                    }
                                                }
                                                else
                                                {

                                                    if (spell.ID == 1020)
                                                    {
                                                        if (attacked.EpicWarrior())
                                                        {
                                                            attacked.ShieldTime = 0;
                                                            attacked.ShieldStamp = Time32.Now;
                                                            attacked.MagicShieldStamp = Time32.Now;
                                                            attacked.MagicShieldTime = 0;
                                                            attacked.AddFlag3(Update.Flags3.WarriorEpicShield);
                                                            attacked.ShieldStamp = Time32.Now;
                                                            attacked.ShieldIncrease = 1.1f;
                                                            attacked.ShieldTime = 35;
                                                            attacker.XpBlueStamp = Time32.Now;
                                                        }
                                                        else
                                                        {
                                                            attacked.ShieldTime = 0;
                                                            attacked.ShieldStamp = Time32.Now;
                                                            attacked.MagicShieldStamp = Time32.Now;
                                                            attacked.MagicShieldTime = 0;
                                                            attacked.AddFlag(Update.Flags.MagicShield);
                                                            attacked.ShieldStamp = Time32.Now;
                                                            attacked.ShieldIncrease = 1.1f;
                                                            attacked.ShieldTime = 60;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        attacked.AccuracyStamp = Time32.Now;
                                                        attacked.StarOfAccuracyStamp = Time32.Now;
                                                        attacked.StarOfAccuracyTime = 0;
                                                        attacked.AccuracyTime = 0;

                                                        attacked.AddFlag(Update.Flags.StarOfAccuracy);
                                                        attacked.AccuracyStamp = Time32.Now;
                                                        attacked.AccuracyTime = (byte)spell.Duration;
                                                    }
                                                }
                                            }
                                            attacked.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Circle spells
                                case 1010:
                                case 1120:
                                case 1125:
                                case 3090:
                                case 5001:
                                case 8030:
                                case 1115:
                                    {
                                        if (spell.ID == 10315)
                                        {
                                            if (attacker.Owner.Weapons.Item1 == null) return;
                                            if (attacker.Owner.Weapons.Item1.IsTwoHander()) return;
                                        }
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            UInt16 ox, oy;
                                            ox = attacker.X;
                                            oy = attacker.Y;
                                            if (spellID == 10315)
                                            {
                                                Attack npacket = new Attack(true);
                                                npacket.Attacker = attacker.UID;
                                                npacket.AttackType = 53;
                                                npacket.X = X;
                                                npacket.Y = Y;
                                                SafeWriter.Write(spell.ID, 28, npacket.Encode());
                                                SafeWriter.Write(spell.Level, 30, npacket.Encode());
                                                attacker.Owner.SendScreen(npacket, true);
                                                attacker.X = X;
                                                attacker.Y = Y;
                                                attacker.SendSpawn(attacker.Owner);
                                            }

                                            List<IMapObject> objects = new List<IMapObject>();
                                            if (attacker.Owner.Screen.Objects.Count() > 0)
                                                objects = GetObjects(ox, oy, attacker.Owner);
                                            if (objects != null)
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range)
                                                {
                                                    if (spellID == 10315)
                                                    {
                                                        foreach (IMapObject objs in objects.ToArray())
                                                        {
                                                            if (objs == null)
                                                                continue;

                                                            if (objs.MapObjType == MapObjectType.Monster || objs.MapObjType == MapObjectType.Player)
                                                            {
                                                                attacked = objs as Entity;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);

                                                                        suse.Effect1 = attack.Effect1;
                                                                        if (spell.Power > 0)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                                            suse.Effect1 = attack.Effect1;
                                                                        }
                                                                        if (spell.ID == 8030)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attacked, ref attack, client_Spell.LevelHu2);
                                                                        }

                                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                        suse.AddTarget(attacked, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                            else if (objs.MapObjType == MapObjectType.SobNpc)
                                                            {
                                                                attackedsob = objs as SobNpcSpawn;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attackedsob, spell))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                        if (spell.Power > 0)
                                                                        {
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                                        }
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        if (spell.ID == 8030)
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);
                                                                        suse.Effect1 = attack.Effect1;
                                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                        suse.AddTarget(attackedsob, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                        {
                                                            if (_obj == null)
                                                                continue;
                                                            if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                            {
                                                                attacked = _obj as Entity;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                                        if (spell.ID == 1115)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                                            //damage = (uint)(damage);
                                                                        }
                                                                        else if (spell.Power > 0)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                                        }
                                                                        suse.Effect1 = attack.Effect1;
                                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                        suse.AddTarget(attacked, damage, attack);

                                                                        if (spell.ID == 1120)
                                                                        {
                                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                                attacked.Owner.BreakTouch(attacker.Owner);



                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                            {
                                                                attackedsob = _obj as SobNpcSpawn;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attackedsob, spell))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                        if (spell.Power > 0)
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        if (spell.ID == 8030)
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);

                                                                        suse.Effect1 = attack.Effect1;
                                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                        suse.AddTarget(attackedsob, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                            Nyx.Server.Game.Calculations.IsBreaking(attacker.Owner, ox, oy);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Buffers
                                case 1075:
                                case 1085:
                                case 1090:
                                case 1095:
                                case 3080:
                                case 10405:
                                case 30000:
                                    {
                                        if (attackedsob != null)
                                        {
                                            if (CanUseSpell(spell, attacker.Owner))
                                            {
                                                PrepareSpell(spell, attacker.Owner);

                                                SpellUse suse = new SpellUse(true);
                                                suse.Attacker = attacker.UID;
                                                suse.SpellID = spell.ID;
                                                suse.SpellLevel = spell.Level;
                                                suse.X = X;
                                                suse.Y = Y;

                                                suse.AddTarget(attackedsob, 0, null);

                                                attacker.Owner.SendScreen(suse, true);
                                            }
                                        }
                                        else
                                        {
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                            {
                                                if (CanUseSpell(spell, attacker.Owner))
                                                {
                                                    PrepareSpell(spell, attacker.Owner);

                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    suse.AddTarget(attacked, 0, null);

                                                    if (spell.ID == 1075 || spell.ID == 1085)
                                                    {
                                                        if (spell.ID == 1075)
                                                        {
                                                            attacked.AddFlag(Update.Flags.Invisibility);
                                                            attacked.InvisibilityStamp = Time32.Now;
                                                            attacked.InvisibilityTime = (byte)spell.Duration;
                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                attacked.Owner.Send(Constants.Invisibility(spell.Duration));
                                                        }
                                                        else
                                                        {
                                                            attacked.AccuracyStamp = Time32.Now;
                                                            attacked.StarOfAccuracyStamp = Time32.Now;
                                                            attacked.StarOfAccuracyTime = 0;
                                                            attacked.AccuracyTime = 0;

                                                            attacked.AddFlag(Update.Flags.StarOfAccuracy);
                                                            attacked.StarOfAccuracyStamp = Time32.Now;
                                                            attacked.StarOfAccuracyTime = (byte)spell.Duration;
                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                attacked.Owner.Send(Constants.Accuracy(spell.Duration));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (spell.ID == 1090)
                                                        {
                                                            attacked.ShieldTime = 0;
                                                            attacked.ShieldStamp = Time32.Now;
                                                            attacked.MagicShieldStamp = Time32.Now;
                                                            attacked.MagicShieldTime = 0;

                                                            attacked.AddFlag(Update.Flags.MagicShield);
                                                            attacked.MagicShieldStamp = Time32.Now;
                                                            attacked.MagicShieldIncrease = 1.1f;//spell.PowerPercent;
                                                            attacked.MagicShieldTime = (byte)spell.Duration;
                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                attacked.Owner.Send(Constants.Shield(spell.PowerPercent, spell.Duration));
                                                        }
                                                        else if (spell.ID == 1095)
                                                        {
                                                            attacked.AddFlag(Update.Flags.Stigma);
                                                            attacked.StigmaStamp = Time32.Now;
                                                            attacked.StigmaIncrease = spell.PowerPercent;
                                                            attacked.StigmaTime = (byte)spell.Duration;
                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                attacked.Owner.Send(Constants.Stigma(spell.PowerPercent, spell.Duration));
                                                            if (CaptureTheFlag.IsWar && attacker.GuildID == attacked.GuildID)
                                                            {
                                                                CaptureTheFlag.AddExploits(1, attacker.Owner.AsMember);
                                                            }
                                                        }
                                                        else if (spell.ID == 30000)
                                                        {
                                                            if (attacked.ContainsFlag2(Update.Flags2.AzureShield))
                                                            {
                                                                return;
                                                            }

                                                            if (spell.Level == 0)
                                                                attacked.AzureShieldDefence = 3000;
                                                            else
                                                                attacked.AzureShieldDefence = (ushort)(3000 * spell.Level);
                                                            attacked.AzureShieldLevel = spell.Level;
                                                            attacked.MagicShieldStamp = Time32.Now;

                                                            attacked.AzureShieldStamp = DateTime.Now;
                                                            attacked.AddFlag2(Update.Flags2.AzureShield);
                                                            attacked.MagicShieldTime = spell.Percent;
                                                            attacked.AzureShieldPacket();

                                                            if (attacked.EntityFlag == EntityFlag.Player)
                                                                attacked.Owner.Send(Constants.Shield(12000, attacked.MagicShieldTime));
                                                            if (CaptureTheFlag.IsWar && attacker.GuildID == attacked.GuildID)
                                                            {
                                                                CaptureTheFlag.AddExploits(1, attacker.Owner.AsMember);
                                                            }
                                                        }
                                                        if (spell.ID == 10405 && attacked.Dead)
                                                        {
                                                            if (attacked.EntityFlag != EntityFlag.Player)
                                                                break;
                                                            if (!attacked.ContainsFlag2(Update.Flags2.SoulShackle))
                                                            {
                                                                if ((attacked.BattlePower - attacker.BattlePower) > 5)
                                                                    break;
                                                                if (Time32.Now > attacker.SpellStamp.AddSeconds(2))
                                                                {
                                                                    #region Perfection
                                                                    Game.Enums.PerfectionEffect effect = Enums.PerfectionEffect.FreeSoul;
                                                                    byte chance = 0;
                                                                    new MsgRefineEffect().HandleFreeSoul(attacker, ref chance);
                                                                    if (Kernel.Rate(chance))
                                                                    {
                                                                        new MsgRefineEffect().SendEffect(attacked, attacker, effect);
                                                                        break;
                                                                    }
                                                                    #endregion
                                                                    attacker.SpellStamp = Time32.Now;
                                                                    attacked.AddFlag(Update.Flags.Dead);//Flag them as dead... should not be needed. This is no movement
                                                                    attacked.AddFlag2(Update.Flags2.SoulShackle);//Give them shackeld effect                                                         
                                                                    attacked.ShackleStamp = Time32.Now;//Set stamp so source can remove the flag after X seconds
                                                                    attacked.ShackleTime = 90;//(short)(30 + 15 * spell.Level);//double checking here. Could be db has this wrong.
                                                                    Network.GamePackets.Update upgrade = new Network.GamePackets.Update(true);
                                                                    upgrade.UID = attacked.UID;
                                                                    upgrade.Append(Network.GamePackets.Update.SoulShackle
                                                                        , 111
                                                                        , 90, 0, spell.Level);
                                                                    attacked.Owner.Send(upgrade.Encode());
                                                                    if (CaptureTheFlag.IsWar && attacker.GuildID != attacked.GuildID)
                                                                    {
                                                                        CaptureTheFlag.AddExploits(1, attacker.Owner.AsMember);
                                                                    }
                                                                    if (attacked.EntityFlag == EntityFlag.Player)
                                                                        attacked.Owner.Send(Constants.Shackled(attacked.ShackleTime));
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (attacked.EntityFlag == EntityFlag.Player)
                                                        attacked.Owner.SendScreen(suse, true);
                                                    else
                                                        attacked.MonsterInfo.SendScreen(suse);

                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Percent
                                case 3050:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (attackedsob != null)
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Distance)
                                                {
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        uint damage = Game.Attacking.Calculate.Percent(attackedsob, spell.PowerPercent);
                                                        attackedsob.Hitpoints -= damage;

                                                        suse.AddTarget(attackedsob, damage, attack);

                                                        attacker.Owner.SendScreen(suse, true);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                                {
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        uint damage = Game.Attacking.Calculate.Percent(attacked, spell.PowerPercent);
                                                        if (attacked.Owner != null)
                                                        {
                                                            attacked.Owner.UpdateQualifier(damage);
                                                        }
                                                        attacked.Hitpoints -= damage;

                                                        suse.AddTarget(attacked, damage, attack);

                                                        if (attacked.EntityFlag == EntityFlag.Player)
                                                            attacked.Owner.SendScreen(suse, true);
                                                        else
                                                            attacked.MonsterInfo.SendScreen(suse);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region ExtraXP
                                case 1040:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            PrepareSpell(spell, attacker.Owner);
                                            if (attacker.Owner.Team != null)
                                            {
                                                foreach (Client.GameClient teammate in attacker.Owner.Team.Teammates)
                                                {
                                                    if (teammate.Entity.UID != attacker.UID)
                                                    {
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, teammate.Entity.X, teammate.Entity.Y) <= 18)
                                                        {
                                                            teammate.XPCount += 20;
                                                            suse.AddTarget(teammate.Entity, 20, null);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region WeaponSpells
                                #region Circle
                                case 5010:
                                case 7020:
                                    {

                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= attacker.AttackRange + 1)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj == null)
                                                        continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                        {
                                                            if (attacked.ContainsFlag(Update.Flags.Fly))
                                                                return;
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                PrepareSpell(spell, attacker.Owner);

                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                                suse.Effect1 = attack.Effect1;

                                                                ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                suse.AddTarget(attacked, damage, attack);
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                PrepareSpell(spell, attacker.Owner);
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }

                                        break;
                                    }
                                #endregion
                                #region Single target
                                case 11140:
                                case 10490:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            TryTrip suse = new TryTrip(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= attacker.AttackRange + 1)
                                            {
                                                if (attackedsob != null)
                                                {
                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Calculate.Melee(attacker, attackedsob, ref attack);
                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                        suse.Damage = damage;
                                                        suse.Attacked = attackedsob.UID;
                                                        suse.Effect1 = attack.Effect1;
                                                        suse.AddTarget(attackedsob.UID, damage, attack);
                                                    }
                                                }
                                                else
                                                {
                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Calculate.Melee(attacker, attacked, spell, ref attack, 0);
                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                        suse.Damage = damage;
                                                        suse.Attacked = attacked.UID;
                                                        suse.Effect1 = attack.Effect1;
                                                        suse.AddTarget(attacked.UID, damage, attack);
                                                    }
                                                }
                                                attacker.AttackPacket = null;
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        attacker.AttackPacket = null;
                                        break;
                                    }
                                case 1290:
                                case 5030:
                                case 5040:
                                case 7000:
                                case 7010:
                                case 7030:
                                case 7040:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            //suse.SoulLevel = client_Spell.SoulLevel;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= attacker.AttackRange + 1)
                                            {
                                                if (attackedsob != null)
                                                {
                                                    if (CanAttack(attacker, attackedsob, spell))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                                else
                                                {
                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker,
                                                       attacked, ref attack);
                                                        suse.Effect1 = attack.Effect1;
                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }

                                        break;
                                    }

                                #endregion
                                #region Sector
                                case 1250:
                                case 5050:
                                case 5020:
                                case 1300:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            //suse.SpellLevelHu = client_Spell.LevelHu2;
                                            suse.Y = Y;
                                            Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                            sector.Arrange(spell.Sector, spell.Range);
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Distance + 1)
                                            {
                                                foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                {
                                                    if (_obj == null)
                                                        continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;

                                                        if (sector.Inside(attacked.X, attacked.Y))
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                                suse.Effect1 = attack.Effect1;
                                                                ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                suse.AddTarget(attacked, damage, attack);
                                                            }
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;

                                                        if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                attack.Effect1 = Attack.AttackEffects1.None;
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #region Riding
                                case 7001:
                                    {
                                        if (attacker.MapID == 1950)
                                            return;
                                        if (attacker.ContainsFlag(Update.Flags.ShurikenVortex))
                                            return;
                                        if (!attacker.Owner.Equipment.Free(12))
                                        {
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;

                                            if (attacker.ContainsFlag(Update.Flags.Ride))
                                            {
                                                attacker.RemoveFlag(Update.Flags.Ride);
                                            }
                                            else
                                            {
                                                if (attacker.Owner.Map.ID == 1036 && attacker.Owner.Equipment.TryGetItem((byte)12).Plus < 6)
                                                    break;
                                                if (attacker.Owner.Map.BaseID == 700)
                                                    break;
                                                if (attacker.Stamina >= 20)
                                                {
                                                    attacker.AddFlag(Update.Flags.Ride);
                                                    attacker.Stamina -= 20;
                                                    attacker.Vigor = (ushort)(attacker.MaxVigor);
                                                    Network.GamePackets.Vigor vigor = new Network.GamePackets.Vigor(true);
                                                    vigor.Amount = (uint)(attacker.MaxVigor);
                                                    vigor.Send(attacker.Owner);

                                                }
                                            }
                                            suse.AddTarget(attacker, 0, attack);
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                case 7002:
                                    {//Spook
                                        if (attacker.MapID == 1950)
                                            return;
                                        if (attacked.ContainsFlag(Update.Flags.Ride) && attacker.ContainsFlag(Update.Flags.Ride))
                                        {
                                            ConquerItem attackedSteed = null, attackerSteed = null;
                                            if ((attackedSteed = attacked.Owner.Equipment.TryGetItem(ConquerItem.Steed)) != null)
                                            {
                                                if ((attackerSteed = attacker.Owner.Equipment.TryGetItem(ConquerItem.Steed)) != null)
                                                {
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;
                                                    suse.AddTarget(attacked, 0, attack);
                                                    if (attackedSteed.Plus < attackerSteed.Plus)
                                                        attacked.RemoveFlag(Update.Flags.Ride);
                                                    else if (attackedSteed.Plus == attackerSteed.Plus && attackedSteed.PlusProgress <= attackerSteed.PlusProgress)
                                                        attacked.RemoveFlag(Update.Flags.Ride);
                                                    else
                                                        suse.Targets[attacked.UID].Hit = false;
                                                    attacker.Owner.SendScreen(suse, true);
                                                    attacked.Owner.Send(new Message(attacker.Name + " use Threat Skill on you your steed. You are dismounted!", Color.Red, Message.System));
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case 7003:
                                    {//WarCry
                                        if (attacker.MapID == 1950)
                                            return;
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = X;
                                        suse.Y = Y;
                                        ConquerItem attackedSteed = null, attackerSteed = null;
                                        foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                        {
                                            if (_obj == null)
                                                continue;
                                            if (_obj.MapObjType == MapObjectType.Player && _obj.UID != attacker.UID)
                                            {
                                                attacked = _obj as Entity;
                                                if ((attackedSteed = attacked.Owner.Equipment.TryGetItem(ConquerItem.Steed)) != null)
                                                {
                                                    if ((attackerSteed = attacker.Owner.Equipment.TryGetItem(ConquerItem.Steed)) != null)
                                                    {
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= attackedSteed.Plus)
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                suse.AddTarget(attacked, 0, attack);
                                                                if (attackedSteed.Plus < attackerSteed.Plus)
                                                                    attacked.RemoveFlag(Update.Flags.Ride);
                                                                else if (attackedSteed.Plus == attackerSteed.Plus && attackedSteed.PlusProgress <= attackerSteed.PlusProgress)
                                                                    attacked.RemoveFlag(Update.Flags.Ride);
                                                                else
                                                                    suse.Targets[attacked.UID].Hit = false;
                                                                attacked.Owner.Send(new Message(attacker.Name + " use Threat Skill on you your steed. You are dismounted!", Color.Red, Message.System));
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        attacker.Owner.SendScreen(suse, true);
                                        break;
                                    }
                                #endregion
                                #region FireOfHell
                                case 1165:
                                case 7014:
                                case 7017:
                                case 7015:
                                case 7011:
                                case 7012:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);

                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            Sector sector = new Sector(attacker.X, attacker.Y, X, Y);
                                            sector.Arrange(spell.Sector, spell.Distance);
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null)
                                                    continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;

                                                    if (sector.Inside(attacked.X, attacked.Y))
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                            suse.Effect1 = attack.Effect1;

                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);

                                                            if (spell.ID == 1165)
                                                            {
                                                                if (attacked.EntityFlag == EntityFlag.Player)
                                                                    attacked.Owner.BreakTouch(attacker.Owner);



                                                            }
                                                        }
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;

                                                    if (sector.Inside(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                            uint damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                            suse.Effect1 = attack.Effect1;
                                                            ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                            suse.AddTarget(attackedsob, damage, attack);
                                                        }
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Trasnformations
                                case 1270:
                                case 1280:
                                case 1350:
                                case 1360:
                                case 3321:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (attacker.MapID == 1036)
                                                return;
                                            if (attacker.MapID == 1950)
                                                return;
                                            bool wasTransformated = attacker.Transformed;
                                            PrepareSpell(spell, attacker.Owner);

                                            #region Atributes
                                            switch (spell.ID)
                                            {
                                                case 3321://GM skill
                                                    {
                                                        attacker.TransformationMaxAttack = 2000000;
                                                        attacker.TransformationMinAttack = 2000000;
                                                        attacker.TransformationDefence = 65355;
                                                        attacker.TransformationMagicDefence = 65355;
                                                        attacker.TransformationDodge = 35;
                                                        attacker.TransformationTime = 65355;
                                                        attacker.TransformationID = 223;
                                                        attacker.Hitpoints = attacker.MaxHitpoints;
                                                        attacker.Mana = attacker.MaxMana;
                                                        break;
                                                    }
                                                case 1350:
                                                    switch (spell.Level)
                                                    {
                                                        case 0:
                                                            {
                                                                attacker.TransformationMaxAttack = 182;
                                                                attacker.TransformationMinAttack = 122;
                                                                attacker.TransformationDefence = 1300;
                                                                attacker.TransformationMagicDefence = 94;
                                                                attacker.TransformationDodge = 35;
                                                                attacker.TransformationTime = 39;
                                                                attacker.TransformationID = 207;
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                attacker.TransformationMaxAttack = 200;
                                                                attacker.TransformationMinAttack = 134;
                                                                attacker.TransformationDefence = 1400;
                                                                attacker.TransformationMagicDefence = 96;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 49;
                                                                attacker.TransformationID = 207;
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                attacker.TransformationMaxAttack = 240;
                                                                attacker.TransformationMinAttack = 160;
                                                                attacker.TransformationDefence = 1500;
                                                                attacker.TransformationMagicDefence = 97;
                                                                attacker.TransformationDodge = 45;
                                                                attacker.TransformationTime = 59;
                                                                attacker.TransformationID = 207;
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                attacker.TransformationMaxAttack = 258;
                                                                attacker.TransformationMinAttack = 172;
                                                                attacker.TransformationDefence = 1600;
                                                                attacker.TransformationMagicDefence = 98;
                                                                attacker.TransformationDodge = 50;
                                                                attacker.TransformationTime = 69;
                                                                attacker.TransformationID = 267;
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                attacker.TransformationMaxAttack = 300;
                                                                attacker.TransformationMinAttack = 200;
                                                                attacker.TransformationDefence = 1900;
                                                                attacker.TransformationMagicDefence = 99;
                                                                attacker.TransformationDodge = 55;
                                                                attacker.TransformationTime = 79;
                                                                attacker.TransformationID = 267;
                                                                break;
                                                            }
                                                    }
                                                    break;
                                                case 1270:
                                                    switch (spell.Level)
                                                    {
                                                        case 0:
                                                            {
                                                                attacker.TransformationMaxAttack = 282;
                                                                attacker.TransformationMinAttack = 179;
                                                                attacker.TransformationDefence = 73;
                                                                attacker.TransformationMagicDefence = 34;
                                                                attacker.TransformationDodge = 9;
                                                                attacker.TransformationTime = 34;
                                                                attacker.TransformationID = 214;
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                attacker.TransformationMaxAttack = 395;
                                                                attacker.TransformationMinAttack = 245;
                                                                attacker.TransformationDefence = 126;
                                                                attacker.TransformationMagicDefence = 45;
                                                                attacker.TransformationDodge = 12;
                                                                attacker.TransformationTime = 39;
                                                                attacker.TransformationID = 214;
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                attacker.TransformationMaxAttack = 616;
                                                                attacker.TransformationMinAttack = 367;
                                                                attacker.TransformationDefence = 180;
                                                                attacker.TransformationMagicDefence = 53;
                                                                attacker.TransformationDodge = 15;
                                                                attacker.TransformationTime = 44;
                                                                attacker.TransformationID = 214;
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                attacker.TransformationMaxAttack = 724;
                                                                attacker.TransformationMinAttack = 429;
                                                                attacker.TransformationDefence = 247;
                                                                attacker.TransformationMagicDefence = 53;
                                                                attacker.TransformationDodge = 15;
                                                                attacker.TransformationTime = 49;
                                                                attacker.TransformationID = 214;
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                attacker.TransformationMaxAttack = 1231;
                                                                attacker.TransformationMinAttack = 704;
                                                                attacker.TransformationDefence = 499;
                                                                attacker.TransformationMagicDefence = 50;
                                                                attacker.TransformationDodge = 20;
                                                                attacker.TransformationTime = 54;
                                                                attacker.TransformationID = 274;
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                attacker.TransformationMaxAttack = 1573;
                                                                attacker.TransformationMinAttack = 941;
                                                                attacker.TransformationDefence = 601;
                                                                attacker.TransformationMagicDefence = 53;
                                                                attacker.TransformationDodge = 25;
                                                                attacker.TransformationTime = 59;
                                                                attacker.TransformationID = 274;
                                                                break;
                                                            }
                                                        case 6:
                                                            {
                                                                attacker.TransformationMaxAttack = 1991;
                                                                attacker.TransformationMinAttack = 1107;
                                                                attacker.TransformationDefence = 1029;
                                                                attacker.TransformationMagicDefence = 55;
                                                                attacker.TransformationDodge = 30;
                                                                attacker.TransformationTime = 64;
                                                                attacker.TransformationID = 274;
                                                                break;
                                                            }
                                                        case 7:
                                                            {
                                                                attacker.TransformationMaxAttack = 2226;
                                                                attacker.TransformationMinAttack = 1235;
                                                                attacker.TransformationDefence = 1029;
                                                                attacker.TransformationMagicDefence = 55;
                                                                attacker.TransformationDodge = 35;
                                                                attacker.TransformationTime = 69;
                                                                attacker.TransformationID = 274;
                                                                break;
                                                            }
                                                    }
                                                    break;
                                                case 1360:
                                                    switch (spell.Level)
                                                    {
                                                        case 0:
                                                            {
                                                                attacker.TransformationMaxAttack = 1215;
                                                                attacker.TransformationMinAttack = 610;
                                                                attacker.TransformationDefence = 100;
                                                                attacker.TransformationMagicDefence = 96;
                                                                attacker.TransformationDodge = 30;
                                                                attacker.TransformationTime = 59;
                                                                attacker.TransformationID = 217;
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                attacker.TransformationMaxAttack = 1310;
                                                                attacker.TransformationMinAttack = 650;
                                                                attacker.TransformationDefence = 400;
                                                                attacker.TransformationMagicDefence = 97;
                                                                attacker.TransformationDodge = 30;
                                                                attacker.TransformationTime = 79;
                                                                attacker.TransformationID = 217;
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                attacker.TransformationMaxAttack = 1420;
                                                                attacker.TransformationMinAttack = 710;
                                                                attacker.TransformationDefence = 650;
                                                                attacker.TransformationMagicDefence = 98;
                                                                attacker.TransformationDodge = 30;
                                                                attacker.TransformationTime = 89;
                                                                attacker.TransformationID = 217;
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                attacker.TransformationMaxAttack = 1555;
                                                                attacker.TransformationMinAttack = 780;
                                                                attacker.TransformationDefence = 720;
                                                                attacker.TransformationMagicDefence = 98;
                                                                attacker.TransformationDodge = 30;
                                                                attacker.TransformationTime = 99;
                                                                attacker.TransformationID = 277;
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                attacker.TransformationMaxAttack = 1660;
                                                                attacker.TransformationMinAttack = 840;
                                                                attacker.TransformationDefence = 1200;
                                                                attacker.TransformationMagicDefence = 99;
                                                                attacker.TransformationDodge = 30;
                                                                attacker.TransformationTime = 109;
                                                                attacker.TransformationID = 277;
                                                                break;
                                                            }
                                                    }
                                                    break;
                                                case 1280:
                                                    switch (spell.Level)
                                                    {
                                                        case 0:
                                                            {
                                                                attacker.TransformationMaxAttack = 930;
                                                                attacker.TransformationMinAttack = 656;
                                                                attacker.TransformationDefence = 290;
                                                                attacker.TransformationMagicDefence = 45;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 29;
                                                                attacker.TransformationID = 213;
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                attacker.TransformationMaxAttack = 1062;
                                                                attacker.TransformationMinAttack = 750;
                                                                attacker.TransformationDefence = 320;
                                                                attacker.TransformationMagicDefence = 46;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 34;
                                                                attacker.TransformationID = 213;
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                attacker.TransformationMaxAttack = 1292;
                                                                attacker.TransformationMinAttack = 910;
                                                                attacker.TransformationDefence = 510;
                                                                attacker.TransformationMagicDefence = 50;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 39;
                                                                attacker.TransformationID = 213;
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                attacker.TransformationMaxAttack = 1428;
                                                                attacker.TransformationMinAttack = 1000;
                                                                attacker.TransformationDefence = 600;
                                                                attacker.TransformationMagicDefence = 53;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 44;
                                                                attacker.TransformationID = 213;
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                attacker.TransformationMaxAttack = 1570;
                                                                attacker.TransformationMinAttack = 1100;
                                                                attacker.TransformationDefence = 700;
                                                                attacker.TransformationMagicDefence = 55;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 49;
                                                                attacker.TransformationID = 213;
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                attacker.TransformationMaxAttack = 1700;
                                                                attacker.TransformationMinAttack = 1200;
                                                                attacker.TransformationDefence = 880;
                                                                attacker.TransformationMagicDefence = 57;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 54;
                                                                attacker.TransformationID = 273;
                                                                break;
                                                            }
                                                        case 6:
                                                            {
                                                                attacker.TransformationMaxAttack = 1900;
                                                                attacker.TransformationMinAttack = 1300;
                                                                attacker.TransformationDefence = 1540;
                                                                attacker.TransformationMagicDefence = 59;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 59;
                                                                attacker.TransformationID = 273;
                                                                break;
                                                            }
                                                        case 7:
                                                            {
                                                                attacker.TransformationMaxAttack = 2100;
                                                                attacker.TransformationMinAttack = 1500;
                                                                attacker.TransformationDefence = 1880;
                                                                attacker.TransformationMagicDefence = 61;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 59;
                                                                attacker.TransformationID = 273;
                                                                break;
                                                            }
                                                        case 8:
                                                            {
                                                                attacker.TransformationMaxAttack = 2300;
                                                                attacker.TransformationMinAttack = 1600;
                                                                attacker.TransformationDefence = 1970;
                                                                attacker.TransformationMagicDefence = 63;
                                                                attacker.TransformationDodge = 40;
                                                                attacker.TransformationTime = 59;
                                                                attacker.TransformationID = 273;
                                                                break;
                                                            }
                                                    }
                                                    break;

                                            }
                                            #endregion

                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.Attacker = attacker.UID;
                                            spellUse.SpellID = spell.ID;
                                            spellUse.SpellLevel = spell.Level;
                                            spellUse.X = X;
                                            spellUse.Y = Y;
                                            spellUse.AddTarget(attacker, (uint)0, attack);
                                            attacker.Owner.SendScreen(spellUse, true);
                                            attacker.TransformationStamp = Time32.Now;
                                            attacker.TransformationMaxHP = 3000;
                                            if (spell.ID == 1270)
                                                attacker.TransformationMaxHP = 50000;
                                            attacker.TransformationAttackRange = 3;
                                            if (spell.ID == 1360)
                                                attacker.TransformationAttackRange = 10;
                                            if (!wasTransformated)
                                            {
                                                double maxHP = attacker.MaxHitpoints;
                                                double HP = attacker.Hitpoints;
                                                double point = HP / maxHP;

                                                attacker.Hitpoints = (uint)(attacker.TransformationMaxHP * point);
                                            }
                                            attacker.Update(Update.MaxHitpoints, attacker.TransformationMaxHP, false);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Bless
                                case 9876:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            attacker.AddFlag(Update.Flags.CastPray);
                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.Attacker = attacker.UID;
                                            spellUse.SpellID = spell.ID;
                                            spellUse.SpellLevel = spell.Level;
                                            spellUse.X = X;
                                            spellUse.Y = Y;
                                            spellUse.AddTarget(attacker, 0, attack);
                                            attacker.Owner.SendScreen(spellUse, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region Companions
                                case 4000:
                                case 4010:
                                case 4020:
                                case 4050:
                                case 4060:
                                case 4070:
                                case 12020:
                                case 12030:
                                case 12040:
                                case 12050:
                                case 12610:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (attacker.Owner.Companion != null)
                                            {
                                                if (attacker.Owner.Companion.MonsterInfo != null)
                                                {
                                                    attacker.Owner.Map.RemoveEntity(attacker.Owner.Companion);
                                                    Data data = new Data(true);
                                                    data.UID = attacker.Owner.Companion.UID;
                                                    data.ID = Data.RemoveEntity;
                                                    attacker.Owner.Companion.MonsterInfo.SendScreen(data);
                                                    attacker.Owner.Companion = null;
                                                }
                                            }
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.Attacker = attacker.UID;
                                            spellUse.SpellID = spell.ID;
                                            spellUse.SpellLevel = spell.Level;
                                            spellUse.X = X;
                                            spellUse.Y = Y;
                                            spellUse.AddTarget(attacker, 0, attack);
                                            attacker.Owner.SendScreen(spellUse, true);
                                            attacker.Owner.Companion = new Entity(EntityFlag.Monster, true);
                                            attacker.Owner.Companion.MonsterInfo = new Nyx.Server.Database.MonsterInformation();
                                            Database.MonsterInformation mt = Database.MonsterInformation.MonsterInformations[spell.Power];
                                            attacker.Owner.Companion.Owner = attacker.Owner;
                                            attacker.Owner.Companion.MapObjType = MapObjectType.Monster;
                                            attacker.Owner.Companion.MonsterInfo = mt.Copy();
                                            attacker.Owner.Companion.MonsterInfo.Owner = attacker.Owner.Companion;
                                            attacker.Owner.Companion.Name = mt.Name;
                                            attacker.Owner.Companion.MinAttack = mt.MinAttack;
                                            attacker.Owner.Companion.MaxAttack = attacker.Owner.Companion.MagicAttack = mt.MaxAttack;
                                            attacker.Owner.Companion.Hitpoints = attacker.Owner.Companion.MaxHitpoints = mt.Hitpoints;
                                            attacker.Owner.Companion.Body = mt.Mesh;
                                            attacker.Owner.Companion.Level = mt.Level;
                                            attacker.Owner.Companion.UID = (uint)(attacker.UID - 200000);
                                            attacker.Owner.Companion.MapID = attacker.Owner.Map.ID;
                                            attacker.Owner.Companion.SendUpdates = true;
                                            attacker.Owner.Companion.X = attacker.X;
                                            attacker.Owner.Companion.Y = attacker.Y;
                                            attacker.Owner.Map.AddEntity(attacker.Owner.Companion);
                                            attacker.Owner.SendScreenSpawn(attacker.Owner.Companion, true);
                                        }
                                        break;
                                    }
                                #endregion
                                #region ChainBolt
                                case 10309:
                                    {
                                        SpellUse suse = new SpellUse(true);
                                        suse.Attacker = attacker.UID;
                                        suse.SpellID = spell.ID;
                                        suse.SpellLevel = spell.Level;
                                        suse.X = attacker.X;
                                        suse.Y = attacker.Y;
                                        if (attacked != null)
                                        {
                                            if (attacker.ContainsFlag2(Update.Flags2.ChainBoltActive))
                                            {
                                                PrepareSpell(spell, attacker.Owner);
                                                suse.X = X;
                                                suse.Y = Y;
                                                int maxR = spell.Distance;
                                                if (attacked != null)
                                                {
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= maxR)
                                                    {
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack);
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                }
                                                var Array = attacker.Owner.Screen.Objects;
                                                var closestTarget = findClosestTarget(attacked, attacked.X, attacked.Y, Array);
                                                ushort x = closestTarget.X, y = closestTarget.Y;
                                                int targets = Math.Max((int)spell.Level, 1);

                                                foreach (Interfaces.IMapObject _obj in Array)
                                                {
                                                    if (targets == 0) continue;
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;

                                                        if (Kernel.GetDistance(x, y, attacked.X, attacked.Y) <= maxR)
                                                        {
                                                            if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                            {
                                                                maxR = 6;

                                                                var damage2 = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack);
                                                                damage2 = (uint)(damage2 / 2);
                                                                ReceiveAttack(attacker, attacked, attack, ref damage2, spell);

                                                                suse.AddTarget(attacked, damage2, attack);
                                                                x = attacked.X;
                                                                y = attacked.Y;
                                                                targets--;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (suse.Targets.Count == 0) return;
                                                attacker.Owner.SendScreen(suse, true);
                                            }
                                            else
                                            {
                                                if (CanUseSpell(spell, attacker.Owner))
                                                {
                                                    PrepareSpell(spell, attacker.Owner);
                                                    attacker.ChainboltStamp = Time32.Now;
                                                    attacker.ChainboltTime = spell.Duration;
                                                    attacker.AddFlag2(Update.Flags2.ChainBoltActive);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                #endregion
                                #region Linear spells
                                case 1260:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            InLineAlgorithm ila = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, (byte)spell.Range, InLineAlgorithm.Algorithm.DDA);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = SpellID;
                                            suse.SpellLevel = attacker.Owner.Spells[SpellID].Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                            {
                                                if (_obj == null) continue;
                                                if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                {
                                                    attacked = _obj as Entity;
                                                    if (ila.InLine(attacked.X, attacked.Y))
                                                    {
                                                        if (!CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee)) continue;
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                                        suse.Effect1 = attack.Effect1;
                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                        suse.AddTarget(attacked, damage, attack);
                                                    }
                                                }
                                                else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                {
                                                    attackedsob = _obj as SobNpcSpawn;
                                                    if (ila.InLine(attackedsob.X, attackedsob.Y))
                                                    {
                                                        if (!CanAttack(attacker, attackedsob, spell)) continue;
                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                        suse.AddTarget(attackedsob, damage, attack);
                                                    }
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                        }
                                        break;
                                    }

                                case 10315:
                                    {
                                        if (spell.ID == 10315)
                                        {
                                            if (attacker.Owner.Weapons.Item1 == null) return;
                                            if (attacker.Owner.Weapons.Item1.IsTwoHander()) return;
                                        }
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            UInt16 ox, oy;
                                            ox = attacker.X;
                                            oy = attacker.Y;
                                            if (spellID == 10315)
                                            {
                                                Attack npacket = new Attack(true);
                                                npacket.Attacker = attacker.UID;
                                                npacket.AttackType = 53;
                                                npacket.X = X;
                                                npacket.Y = Y;
                                                SafeWriter.Write(spell.ID, 28, npacket.Encode());
                                                SafeWriter.Write(spell.Level, 30, npacket.Encode());
                                                attacker.Owner.SendScreen(npacket, true);
                                                attacker.X = X;
                                                attacker.Y = Y;
                                                attacker.SendSpawn(attacker.Owner);
                                                attacker.Owner.Screen.Reload(npacket);
                                            }
                                            List<IMapObject> objects = new List<IMapObject>();
                                            if (attacker.Owner.Screen.Objects.Count() > 0)
                                                objects = GetObjects(ox, oy, attacker.Owner);
                                            if (objects != null)
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range)
                                                {
                                                    if (spellID == 10315)
                                                    {
                                                        foreach (IMapObject objs in objects.ToArray())
                                                        {
                                                            if (objs == null) continue;
                                                            if (objs.MapObjType == MapObjectType.Monster || objs.MapObjType == MapObjectType.Player)
                                                            {
                                                                attacked = objs as Entity;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                                        damage = (uint)(damage * 0.8);
                                                                        suse.Effect1 = attack.Effect1;
                                                                        if (spell.Power > 0)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack);
                                                                            suse.Effect1 = attack.Effect1;
                                                                        }
                                                                        if (spell.ID == 8030)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attacked, ref attack);
                                                                        }
                                                                        suse.Effect1 = attack.Effect1;
                                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                                        suse.AddTarget(attacked, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                            else if (objs.MapObjType == MapObjectType.SobNpc)
                                                            {
                                                                attackedsob = objs as SobNpcSpawn;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attackedsob, spell))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                        if (spell.Power > 0)
                                                                        {
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                                        }
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        if (spell.ID == 8030)
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);
                                                                        suse.Effect1 = attack.Effect1;
                                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                                        suse.AddTarget(attackedsob, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                                                        {
                                                            if (_obj == null) continue;
                                                            if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                            {
                                                                attacked = _obj as Entity;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attacked, ref attack);
                                                                        suse.Effect1 = attack.Effect1;
                                                                        if (spell.Power > 0)
                                                                        {
                                                                            attack.Effect1 = Attack.AttackEffects1.None;
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack);
                                                                            suse.Effect1 = attack.Effect1;
                                                                        }
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        if (spell.ID == 8030)
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attacked, ref attack);
                                                                        if (spell.ID == 1115)
                                                                            damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                                                        ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                                        suse.AddTarget(attacked, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                            else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                            {
                                                                attackedsob = _obj as SobNpcSpawn;
                                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                                {
                                                                    if (CanAttack(attacker, attackedsob, spell))
                                                                    {
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                        if (spell.Power > 0)
                                                                            damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                                        attack.Effect1 = Attack.AttackEffects1.None;
                                                                        if (spell.ID == 8030)
                                                                            damage = Game.Attacking.Calculate.Ranged(attacker, attackedsob, ref attack);
                                                                        suse.Effect1 = attack.Effect1;
                                                                        ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                                        suse.AddTarget(attackedsob, damage, attack);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                            }
                                            attacker.Owner.SendScreen(suse, true);
                                            Calculations.IsBreaking(attacker.Owner, ox, oy);
                                        }
                                        break;
                                    }
                                #region ChargingVortex
                                case 11190:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (attacker.Owner.Map.Floor[X, Y, MapObjectType.InvalidCast, null])
                                                break;
                                            spell.UseStamina = 20;
                                            PrepareSpell(spell, attacker.Owner);
                                            SpellUse suse = new SpellUse(true);
                                            suse.Attacker = attacker.UID;
                                            suse.SpellID = spell.ID;
                                            suse.SpellLevel = spell.Level;
                                            suse.X = X;
                                            suse.Y = Y;
                                            UInt16 ox, oy;
                                            ox = attacker.X;
                                            oy = attacker.Y;
                                            attack.X = X;
                                            attack.Y = Y;
                                            attack.Attacker = attacker.UID;
                                            attack.AttackType = 53;
                                            attack.X = X;
                                            attack.Y = Y;
                                            attacker.Owner.SendScreen(attack, true);
                                            attacker.X = X;
                                            attacker.Y = Y;
                                            if (Kernel.GetDistance(attacker.X, attacker.Y, X, Y) <= spell.Range)
                                            {

                                                var Array = attacker.Owner.Screen.Objects;

                                                foreach (Interfaces.IMapObject _obj in Array)
                                                {
                                                    if (_obj.MapObjType == MapObjectType.Monster || _obj.MapObjType == MapObjectType.Player)
                                                    {
                                                        attacked = _obj as Entity;
                                                        if (Kernel.GetDistance(X, Y, attacked.X, attacked.Y) > spell.Range)
                                                            continue;
                                                        if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                                        {
                                                            var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);

                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                    }
                                                    else if (_obj.MapObjType == MapObjectType.SobNpc)
                                                    {
                                                        attackedsob = _obj as SobNpcSpawn;
                                                        if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Range)
                                                        {
                                                            if (CanAttack(attacker, attackedsob, spell))
                                                            {
                                                                uint damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);
                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                attacker.AttackPacket = null;
                                            }
                                            Calculations.IsBreaking(attacker.Owner, ox, oy);
                                            attacker.Owner.SendScreen(suse, true);
                                            attacker.Owner.Screen.Reload(suse);
                                        }
                                        break;
                                    }
                                #endregion
                                #endregion
                                #region Heaven Blade
                                //HeavenBlade
                                case 10310:
                                    {
                                        if (CanUseSpell(spell, attacker.Owner))
                                        {
                                            if (attacked != null)
                                            {
                                                if (Kernel.GetDistance(attacker.X, attacker.Y, attacked.X, attacked.Y) <= spell.Distance)
                                                {
                                                    SpellUse suse = new SpellUse(true);
                                                    suse.Attacker = attacker.UID;
                                                    suse.SpellID = spell.ID;
                                                    suse.SpellLevel = spell.Level;
                                                    suse.X = X;
                                                    suse.Y = Y;

                                                    if (CanAttack(attacker, attacked, spell, false))
                                                    {
                                                        PrepareSpell(spell, attacker.Owner);
                                                        var damage = Game.Attacking.Calculate.Magic(attacker, attacked, spell, ref attack, client_Spell.LevelHu2);
                                                        if (Kernel.Rate(spell.Percent))
                                                        {
                                                            ReceiveAttack(attacker, attacked, attack, ref damage, spell);
                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                        else
                                                        {
                                                            damage = 0;
                                                            suse.AddTarget(attacked, damage, attack);
                                                        }
                                                        attacker.Owner.SendScreen(suse, true);
                                                    }
                                                }
                                                else
                                                {
                                                    attacker.AttackPacket = null;
                                                }
                                            }
                                            else
                                            {
                                                if (attackedsob != null)
                                                {
                                                    if (Kernel.GetDistance(attacker.X, attacker.Y, attackedsob.X, attackedsob.Y) <= spell.Distance)
                                                    {
                                                        SpellUse suse = new SpellUse(true);
                                                        suse.Attacker = attacker.UID;
                                                        suse.SpellID = spell.ID;
                                                        suse.SpellLevel = spell.Level;
                                                        suse.X = X;
                                                        suse.Y = Y;

                                                        if (CanAttack(attacker, attackedsob, spell))
                                                        {
                                                            PrepareSpell(spell, attacker.Owner);
                                                            var damage = Game.Attacking.Calculate.Magic(attacker, attackedsob, spell, ref attack);
                                                            if (Kernel.Rate(spell.Percent))
                                                            {
                                                                ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                            else
                                                            {
                                                                damage = 0;
                                                                suse.AddTarget(attackedsob, damage, attack);
                                                            }
                                                            attacker.Owner.SendScreen(suse, true);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            attacker.AttackPacket = null;
                                        }
                                        break;
                                    }
                                #endregion

                                default:
                                    {
                                        if (attacker.Owner.Account.State == Nyx.Server.Database.AccountTable.AccountState.GameMaster)
                                            attacker.Owner.Send(new Message("Unknown Skill : " + spellID, System.Drawing.Color.Red, Message.System));
                                        break;
                                    }
                            }
                            attacker.Owner.IncreaseSpellExperience(Experience, spellID);
                            if (attacker.MapID == 1039)
                            {
                                if (spell.ID == 7001 || spell.ID == 9876)
                                {
                                    attacker.AttackPacket = null;
                                    return;
                                }
                                if (attacker.AttackPacket != null)
                                {
                                    attack.Damage = spell.ID;
                                    attacker.AttackPacket = attack;
                                    var xspell = GetWeaponSpell(spell);
                                    //  var xspell = Database.SpellTable.WeaponSpells.Values.Where(p => p.Contains(spell.ID));
                                    if (xspell != null)
                                    {
                                        if (attacker.AttackPacket == null)
                                        {
                                            attack.AttackType = Attack.Melee;
                                            attacker.AttackPacket = attack;
                                        }
                                        else
                                        {
                                            attacker.AttackPacket.AttackType = Attack.Melee;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (spell.NextSpellID != 0)
                                {
                                    if (spell.NextSpellID >= 1000 && spell.NextSpellID <= 1002)
                                        if (Target >= 1000000)
                                        {
                                            attacker.AttackPacket = null;
                                            return;
                                        }
                                    attack.Damage = spell.NextSpellID;
                                    attacker.AttackPacket = attack;
                                }
                                else
                                {
                                    var xspell = GetWeaponSpell(spell);//Database.SpellTable.WeaponSpells.Values.Where(p => p.Contains(spell.ID));
                                    if (xspell == null || spell.ID == 9876)
                                        attacker.AttackPacket = null;
                                    else
                                    {
                                        if (attacker.AttackPacket == null)
                                        {
                                            attack.AttackType = Attack.Melee;
                                            attacker.AttackPacket = attack;
                                        }
                                        else
                                        {
                                            attacker.AttackPacket.AttackType = Attack.Melee;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            attacker.AttackPacket = null;
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

        }
        public EntityFlag EntityFlag
        {
            get { return _EntityFlag; }
            set { _EntityFlag = value; }
        }
        private EntityFlag _EntityFlag;
        public static void HandleAura(Game.Entity attacker, Database.SpellInformation spell)
        {
            ulong statusFlag = 0;
            ulong statusFlag2 = 0;
            Update.AuraType aura = Update.AuraType.TyrantAura;
            switch (spell.ID)
            {
                case 10424: statusFlag = Update.Flags2.EarthAura; break;
                case 10423: statusFlag = Update.Flags2.FireAura; break;
                case 10422: statusFlag = Update.Flags2.WaterAura; break;
                case 10421: statusFlag = Update.Flags2.WoodAura; break;
                case 10420: statusFlag = Update.Flags2.MetalAura; break;
                case 10410: statusFlag = Update.Flags2.FendAura; break;
                case 10395: statusFlag = Update.Flags2.TyrantAura; break;
            }
            switch (spell.ID)
            {
                case 10424: statusFlag2 = Update.Flags2.EarthAura2; break;
                case 10423: statusFlag2 = Update.Flags2.FireAura2; break;
                case 10422: statusFlag2 = Update.Flags2.WaterAura2; break;
                case 10421: statusFlag2 = Update.Flags2.WoodAura2; break;
                case 10420: statusFlag2 = Update.Flags2.MetalAura2; break;
                case 10410: statusFlag2 = Update.Flags2.FendAura2; break;
                case 10395: statusFlag2 = Update.Flags2.TyrantAura2; break;
            }

            if (attacker.Dead) return;
            if (attacker.Aura_isActive)
            {
                switch (attacker.Aura_actType)
                {
                    case 10424: aura = Update.AuraType.EarthAura; break;
                    case 10423: aura = Update.AuraType.FireAura; break;
                    case 10422: aura = Update.AuraType.WaterAura; break;
                    case 10421: aura = Update.AuraType.WoodAura; break;
                    case 10420: aura = Update.AuraType.MetalAura; break;
                    case 10410: aura = Update.AuraType.FendAura; break;
                    case 10395: aura = Update.AuraType.TyrantAura; break;
                }
                new Update(true).Aura(attacker, Update.AuraDataTypes.Remove, aura, spell);

                attacker.RemoveFlag2(attacker.Aura_actType);
                attacker.RemoveFlag2(attacker.Aura_actType2);
                //attacker.Owner.removeAuraBonuses(statusFlag, spell.Power, 1);
                attacker.Owner.removeAuraBonuses(attacker.Aura_actType, attacker.Aura_actPower, 1);
                attacker.Aura_isActive = false;
                attacker.AuraTime = 0;
                if (statusFlag == attacker.Aura_actType)
                {
                    attacker.Aura_actType = 0;
                    attacker.Aura_actType2 = 0;
                    attacker.Aura_actPower = 0;
                    attacker.Aura_actLevel = 0;
                    return;
                }



            }
            if (CanUseSpell(spell, attacker.Owner))
            {
                if (statusFlag != 0)
                {
                    switch (attacker.Aura_actType)
                    {
                        case 10424: aura = Update.AuraType.EarthAura; break;
                        case 10423: aura = Update.AuraType.FireAura; break;
                        case 10422: aura = Update.AuraType.WaterAura; break;
                        case 10421: aura = Update.AuraType.WoodAura; break;
                        case 10420: aura = Update.AuraType.MetalAura; break;
                        case 10410: aura = Update.AuraType.FendAura; break;
                        case 10395: aura = Update.AuraType.TyrantAura; break;
                    }
                    new Update(true).Aura(attacker, Update.AuraDataTypes.Remove, aura, spell);

                    attacker.RemoveFlag2(attacker.Aura_actType);
                    attacker.RemoveFlag2(attacker.Aura_actType2);
                    attacker.Owner.removeAuraBonuses(attacker.Aura_actType, attacker.Aura_actPower, 1);
                    attacker.Aura_isActive = false;
                    attacker.AuraTime = 0;
                    if (statusFlag == attacker.Aura_actType)
                    {
                        attacker.Aura_actType2 = 0;
                        attacker.Aura_actType = 0;
                        attacker.Aura_actPower = 0;
                        attacker.Aura_actLevel = 0;
                    }



                }
                attacker.AuraStamp = Time32.Now;
                if (spell.Power == 0) spell.Power = 45;
                attacker.AuraTime = (short)(spell.Power * 20);

                PrepareSpell(spell, attacker.Owner);

                SpellUse suse = new SpellUse(true);
                suse.Attacker = attacker.UID;
                suse.SpellID = spell.ID;
                suse.SpellLevel = spell.Level;
                suse.X = attacker.X;
                suse.Y = attacker.Y;

                suse.AddTarget(attacker, 0, null);
                attacker.Owner.SendScreen(suse, true);
                attacker.AddFlag2(statusFlag);
                attacker.AddFlag2(statusFlag2);
                attacker.Aura_isActive = true;
                attacker.Aura_actType = statusFlag;
                attacker.Aura_actType2 = statusFlag2;
                attacker.Aura_actPower = spell.Power;
                attacker.Aura_actLevel = spell.Level;
                attacker.Owner.doAuraBonuses(statusFlag, spell.Power, 1);

                switch (spell.ID)
                {
                    case 10424: aura = Update.AuraType.EarthAura; break;
                    case 10423: aura = Update.AuraType.FireAura; break;
                    case 10422: aura = Update.AuraType.WaterAura; break;
                    case 10421: aura = Update.AuraType.WoodAura; break;
                    case 10420: aura = Update.AuraType.MetalAura; break;
                    case 10410: aura = Update.AuraType.FendAura; break;
                    case 10395: aura = Update.AuraType.TyrantAura; break;
                }
                new Update(true).Aura(attacker, Update.AuraDataTypes.Add, aura, spell);

            }

        }
        public static List<IMapObject> GetObjects(UInt16 ox, UInt16 oy, Client.GameClient c)
        {
            UInt16 x, y;
            x = c.Entity.X;
            y = c.Entity.Y;

            var list = new List<IMapObject>();
            c.Entity.X = ox;
            c.Entity.Y = oy;
            foreach (IMapObject objects in c.Screen.Objects)
            {
                if (objects != null)
                    if (objects.UID != c.Entity.UID)
                        if (!list.Contains(objects))
                            list.Add(objects);
            }
            c.Entity.X = x;
            c.Entity.Y = y;
            foreach (IMapObject objects in c.Screen.Objects)
            {
                if (objects != null)
                    if (objects.UID != c.Entity.UID)
                        if (!list.Contains(objects))
                            list.Add(objects);
            }
            if (list.Count > 0)
                return list;
            return null;
        }
        public static void ReceiveAttack(Game.Entity attacker, Game.Entity attacked, Attack attack, ref uint damage, Database.SpellInformation spell)
        {
            #region [#SendHP]
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                //شريط الاخضر 
                foreach (Client.GameClient Attacker in Program.Values)
                    //   { 
                    //    mdf3.line(Attacker, 5, "" + attacked.Name + " >> " + attacked.Hitpoints + "/" + attacked.MaxHitpoints + "");//كلمة 
                    //   } 


                    ///////////////////////// 

                    //الكلام البنفسجى 
                    attacker.Owner.Send(new Message("" + attacked.Name + " >> " + attacked.Hitpoints + "/" + attacked.MaxHitpoints + "",
         System.Drawing.Color.White,
          Message.Agate));
                ////////////////// 

            }
            #endregion
            #region Perfection
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                #region Perfection
                Game.Enums.PerfectionEffect effect = Enums.PerfectionEffect.CalmWind;
                byte chance = 0;
                new MsgRefineEffect().GenerateEffectandChance(attacker.Owner, ref effect, ref chance);
                if (Kernel.Rate(chance))
                {
                    new MsgRefineEffect().SendEffect(attacker, attacked, effect);
                }
                #endregion
            }
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                #region Perfection
                byte chance = 0;
                new MsgRefineEffect().HandleLuckyStrike(attacker, ref chance);
                if (Kernel.Rate(chance))
                {
                    new MsgRefineEffect().SendEffect(attacker, attacked, Game.Enums.PerfectionEffect.LuckyStrike);
                    damage *= 2;
                }
                #endregion
            }
            if (attacked.EntityFlag == EntityFlag.Player)
            {
                #region Perfection
                Game.Enums.PerfectionEffect effect = Enums.PerfectionEffect.Stricklock;
                byte chance = 0;
                new MsgRefineEffect().HandleStrickeLock(attacker, ref chance);
                if (Kernel.Rate(chance))
                {
                    new MsgRefineEffect().SendEffect(attacked, attacker, effect);
                    return;
                }
                #endregion
            }
            if (attacked.EntityFlag == EntityFlag.Player)
            {
                #region Perfection
                Game.Enums.PerfectionEffect effect = Enums.PerfectionEffect.Stricklock;
                byte chance = 0;
                new MsgRefineEffect().HandleBloodSpawn(attacked, ref chance);
                if (Kernel.Rate(chance))
                {
                    new MsgRefineEffect().SendEffect(attacked, attacker, effect);
                    attacked.Hitpoints = attacked.MaxHitpoints;
                    attacked.Mana = attacked.MaxMana;
                }
                #endregion
            }
            if (attacked.EntityFlag == EntityFlag.Player)
            {
                #region Perfection
                Game.Enums.PerfectionEffect effect = Enums.PerfectionEffect.MirrorofSin;
                byte chance = 0;
                new MsgRefineEffect().HandleMirror(attacked, ref chance);
                if (Kernel.Rate(chance))
                {
                    new MsgRefineEffect().SendEffect(attacked, attacker, effect);
                    attacked.AddFlag(Network.GamePackets.Update.Flags.XPList);
                }
                #endregion
            }
            #endregion
            if (!(attacked.Name.Contains("Guard") && attacked.EntityFlag == EntityFlag.Monster))
                if (attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag != EntityFlag.Player && !attacked.Name.Contains("Guard"))
                {
                    if (damage > attacked.Hitpoints)
                    {
                        attacker.Owner.IncreaseExperience(Calculate.CalculateExpBonus(attacker.Level, attacked.Level, Math.Min(damage, attacked.Hitpoints)), true);
                        if (spell != null)
                            attacker.Owner.IncreaseSpellExperience((uint)Calculate.CalculateExpBonus(attacker.Level, attacked.Level, Math.Min(damage, attacked.Hitpoints)), spell.ID);

                    }
                    else
                    {
                        attacker.Owner.IncreaseExperience(Calculate.CalculateExpBonus(attacker.Level, attacked.Level, damage), true);
                        if (spell != null)
                            attacker.Owner.IncreaseSpellExperience((uint)Calculate.CalculateExpBonus(attacker.Level, attacked.Level, damage), spell.ID);
                    }
                }
            if (attacker.EntityFlag == EntityFlag.Monster && attacked.EntityFlag == EntityFlag.Player)
            {
                if (attacked.Action == Enums.ConquerAction.Sit)
                    if (attacked.Stamina > 20)
                        attacked.Stamina -= 20;
                    else
                        attacked.Stamina = 0;
                attacked.Action = Enums.ConquerAction.None;
            }

            if (attack.AttackType == Attack.Magic)
            {
                if (attacked.Hitpoints <= damage)
                {
                    if (attacked.Owner != null)
                    {
                        attacked.Owner.UpdateQualifier(attacked.Hitpoints);
                    }
                    attacked.CauseOfDeathIsMagic = true;
                    attacked.Die(attacker);
                    attacked.IsDropped = false;

                    if (attacker.PKMode == Enums.PKMode.Jiang)
                    {
                        if (attacked.JiangActive)
                        {
                            if (attacker.MyJiang != null && attacker.MyJiang != null)
                                attacker.MyJiang.GetKill(attacker.Owner, attacked.MyJiang);
                        }
                    }
                    if (attacked.Owner != null && attacker.Owner != null)
                    {
                        if (attacked.Owner.Team != null && attacker.Owner.Team != null)
                        {
                            if (attacker.Owner.Team.EliteFighterStats != null && attacked.Owner.Team.EliteFighterStats != null)
                            {
                                if (attacker.Owner.Team.EliteMatch != null)
                                {
                                    if (!attacked.Owner.Team.Alive)
                                    {
                                        attacker.Owner.Team.EliteFighterStats.Points += damage;
                                        attacker.Owner.Team.EliteMatch.End(attacked.Owner.Team);
                                    }
                                    else
                                    {
                                        attacker.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                        attacked.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (attacked.Owner != null && attacker.Owner != null)
                    {
                        if (attacked.Owner.Team != null && attacker.Owner.Team != null)
                        {
                            if (attacker.Owner.Team.EliteFighterStats != null && attacked.Owner.Team.EliteFighterStats != null)
                            {
                                if (attacker.Owner.Team.EliteMatch != null)
                                {
                                    //if (attacker.MapID == attacked.Owner.Team.EliteMatch.Map.ID
                                    attacker.Owner.Team.EliteFighterStats.Points += damage;
                                    attacker.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                    attacked.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                }
                            }
                        }
                    }
                    if (attacked.Owner != null)
                    {
                        attacked.Owner.UpdateQualifier(damage);
                    }

                    attacked.Hitpoints -= damage;
                }
            }
            else
            {
                if (attacked.Hitpoints <= damage)
                {
                    if (attacked.EntityFlag == EntityFlag.Player)
                    {

                        attacked.Owner.UpdateQualifier(attacked.Hitpoints);
                        attacked.Owner.SendScreen(attack, true);
                        attacker.AttackPacket = null;
                    }
                    else
                    {
                        attacked.MonsterInfo.SendScreen(attack);
                    }
                    attacked.Die(attacker);
                    if (attacker.PKMode == Enums.PKMode.Jiang)
                    {
                        if (attacked.JiangActive)
                        {
                            if (attacker.MyJiang != null && attacker.MyJiang != null)
                                attacker.MyJiang.GetKill(attacker.Owner, attacked.MyJiang);
                        }
                    }
                    if (attacked.Owner != null && attacker.Owner != null)
                    {
                        if (attacked.Owner.Team != null && attacker.Owner.Team != null)
                        {
                            if (attacker.Owner.Team.EliteFighterStats != null && attacked.Owner.Team.EliteFighterStats != null)
                            {
                                if (attacker.Owner.Team.EliteMatch != null)
                                {
                                    if (attacker.MapID == attacked.Owner.Team.EliteMatch.Map.ID)
                                    {
                                        if (!attacked.Owner.Team.Alive)
                                        {
                                            attacker.Owner.Team.EliteFighterStats.Points += damage;
                                            attacker.Owner.Team.EliteMatch.End(attacked.Owner.Team);
                                        }
                                        else
                                        {
                                            attacker.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                            attacked.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (attacked.Owner != null && attacker.Owner != null)
                    {
                        if (attacked.Owner.Team != null && attacker.Owner.Team != null)
                        {
                            if (attacker.Owner.Team.EliteFighterStats != null && attacked.Owner.Team.EliteFighterStats != null)
                            {
                                if (attacker.Owner.Team.EliteMatch != null)
                                {
                                    if (attacker.MapID == attacked.Owner.Team.EliteMatch.Map.ID)
                                    {
                                        attacker.Owner.Team.EliteFighterStats.Points += damage;
                                        attacker.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                        attacked.Owner.Team.SendMesageTeam(attacker.Owner.Team.EliteMatch.CreateUpdate().Encode(), 0);
                                    }
                                }
                            }
                        }
                    }
                    attacked.Hitpoints -= damage;
                    if (attacked.EntityFlag == EntityFlag.Player)
                    {
                        attacked.Owner.UpdateQualifier(damage);
                        attacked.Owner.SendScreen(attack, true);
                    }
                    else
                        attacked.MonsterInfo.SendScreen(attack);
                    attacker.AttackPacket = attack;
                    attacker.AttackStamp = Time32.Now;
                }
            }
        }
        public static void ReceiveAttack(Game.Entity attacker, SobNpcSpawn attacked, Attack attack, uint damage, Database.SpellInformation spell)
        {
            if (attacker.EntityFlag == EntityFlag.Player)
                if (damage > attacked.Hitpoints)
                {
                    if (attacker.MapID == 1039)
                        attacker.Owner.IncreaseExperience(Math.Min(damage, attacked.Hitpoints), true);
                    if (spell != null)
                        attacker.Owner.IncreaseSpellExperience(Math.Min(damage, attacked.Hitpoints), spell.ID);
                }
                else
                {
                    if (attacker.MapID == 1039)
                        attacker.Owner.IncreaseExperience(damage, true);
                    if (spell != null)
                        attacker.Owner.IncreaseSpellExperience(damage, spell.ID);
                }
            if (attacker.MapID == CaptureTheFlag.MapID)
            {
                if (attacker.GuildID != 0 && Program.World.CTF.Bases[attacked.UID].CapturerID != attacker.GuildID)
                {
                    Program.World.CTF.AddScore(damage, attacker.Owner.Guild, attacked);
                }
            }
            if (attacker.MapID == 1509)
            {
                if (attacked.UID == 812)
                {
                    Clan clan = attacker.GetClan;
                    if (Game.ClanWar.PoleKeeper == clan)
                        return;
                    if (attacked.Hitpoints <= damage)
                        attacked.Hitpoints = 0;
                    Game.ClanWar.AddScore(damage, clan);
                }
            }
            #region CaptureTheFlag
            if (attacker.MapID == CaptureTheFlag.MapID)
            {
                if (attacker.GuildID != 0 && Program.World.CTF.Bases[attacked.UID].CapturerID != attacker.GuildID)
                {
                    if (attacked.Hitpoints <= damage)
                    {
                        Program.World.CTF.FlagOwned(attacked);
                        foreach (var player in Kernel.GamePool.Values)
                        {
                            player.Send(new Message("Guild " + attacker.Owner.Guild.Name + " occupied Flag Base No. " + (attacked.UID - 101990).ToString() + "!", Color.Red, Message.TopLeft));
                        }
                    }
                    Program.World.CTF.AddScore(damage, attacker.Owner.Guild, attacked);
                }
            }
            #endregion
            if (attacker.MapID == 1038)
            {
                if (attacked.UID == 810)
                {
                    if (Game.GuildWar.PoleKeeper == attacker.Owner.Guild)
                        return;
                    if (attacked.Hitpoints <= damage)
                        attacked.Hitpoints = 0;
                    Game.GuildWar.AddScore(damage, attacker.Owner.Guild);
                }
            }
            if (attack.AttackType == Attack.Magic)
            {
                if (attacked.Hitpoints <= damage)
                {
                    attacked.Die(attacker);
                }
                else
                {
                    attacked.Hitpoints -= damage;
                }
            }
            else
            {
                attacker.Owner.SendScreen(attack, true);
                if (attacked.Hitpoints <= damage)
                {
                    attacked.Die(attacker);
                }
                else
                {
                    attacked.Hitpoints -= damage;
                    attacker.AttackPacket = attack;
                    attacker.AttackStamp = Time32.Now;
                }
            }
        }
        public static bool isArcherSkill(uint ID)
        {
            if (ID >= 8000 && ID <= 9875)
                return true;
            return false;
        }
        public static bool CanUseSpell(Database.SpellInformation spell, Client.GameClient client)
        {
            if (client.Entity.SkillTeamWatchingElitePKMatch != null)
                return false;
            if (client.WatchingElitePKMatch != null)
                return false;
            if (client.WatchingGroup != null)
                return false;
            if (spell == null)
                return false;
            if (client.Entity.Mana < spell.UseMana)
                return false;
            if (client.Entity.Stamina < spell.UseStamina)
                return false;
            if (spell.UseArrows > 0 && isArcherSkill(spell.ID))
            {
                var weapons = client.Weapons;
                if (weapons.Item2 != null)
                    if (!client.Entity.ContainsFlag3(Update.Flags3.Assassin))
                        if (!PacketHandler.IsArrow(weapons.Item2.ID))
                            return false;

                return true;
            }
            if (spell.NeedXP == 1 && !client.Entity.ContainsFlag(Update.Flags.XPList))
                return false;
            return true;
        }
        public static void PrepareSpell(Database.SpellInformation spell, Client.GameClient client)
        {
            if (spell.NeedXP == 1)
                client.Entity.RemoveFlag(Update.Flags.XPList);
            if (client.Map.ID != 1039)
            {
                if (spell.UseMana > 0)
                    if (client.Entity.Mana >= spell.UseMana)
                        client.Entity.Mana -= spell.UseMana;
                if (spell.UseStamina > 0)
                    if (client.Entity.Stamina >= spell.UseStamina)
                        client.Entity.Stamina -= spell.UseStamina;
            }
        }
        public static bool CanAttack(Game.Entity attacker, SobNpcSpawn attacked, Database.SpellInformation spell)
        {
            if (attacked == null)
                return false;

            #region CaptureTheFlag
            if (attacker.MapID == CaptureTheFlag.MapID)
            {
                if (Program.World.CTF.Bases.ContainsKey(attacked.UID))
                {
                    var _base = Program.World.CTF.Bases[attacked.UID];
                    if (_base.CapturerID == attacker.GuildID)
                        return false;
                }
                return true;
            }
            #endregion
            if (attacker.MapID == 1509)
            {
                if (attacker.ClanId == 0 || !Game.ClanWar.IsWar)
                {
                    if (attacked.UID == 812)
                    {
                        return false;
                    }
                }
                if (Game.ClanWar.PoleKeeper != null)
                {
                    if (Game.ClanWar.PoleKeeper == attacker.GetClan)
                    {
                        if (attacked.UID == 812)
                        {
                            return false;
                        }
                    }
                }
            }

            if (attacker.MapID == 1038)
            {
                if (attacker.GuildID == 0 || !Game.GuildWar.IsWar)
                {
                    if (attacked.UID == 810)
                    {
                        return false;
                    }
                }
                if (Game.GuildWar.PoleKeeper != null)
                {
                    if (Game.GuildWar.PoleKeeper == attacker.Owner.Guild)
                    {
                        if (attacked.UID == 810)
                        {
                            return false;
                        }
                    }
                    else if (attacked.UID == 516075 || attacked.UID == 516074)
                    {
                        if (Game.GuildWar.PoleKeeper == attacker.Owner.Guild)
                        {
                            if (attacker.PKMode == Enums.PKMode.Team)
                                return false;
                        }
                    }
                }
            }
            if (attacker.MapID == 1039)
            {
                bool stake = true;
                if (attacked.LoweredName.Contains("crow"))
                    stake = false;

                ushort levelbase = (ushort)(attacked.Mesh / 10);
                if (stake)
                    levelbase -= 42;
                else
                    levelbase -= 43;

                byte level = (byte)(20 + (levelbase / 3) * 5);
                if (levelbase == 108 || levelbase == 109)
                    level = 125;
                if (attacker.Level >= level)
                    return true;
                else
                {
                    attacker.AttackPacket = null;
                    attacker.Owner.Send(Constants.DummyLevelTooHigh());
                    return false;
                }
            }
            return true;
        }
        public static bool CanAttack(Game.Entity attacker, Game.Entity attacked, Database.SpellInformation spell, bool melee)
        {
            if (DateTime.Now < attacker.Owner.timerattack.AddSeconds(6))
                return false;
            if (attacked.EntityFlag == EntityFlag.Monster)
            {
                if (attacked.Companion)
                {
                    if (attacked.Owner == attacker.Owner)
                        return false;
                }
            }
            #region Clone Section
         //Amjad
            #endregion
            if (attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag == EntityFlag.Player && attacker.MapID == 3935 && attacked.MapID == 3935)
            {
                if (Kernel.GetDistance(attacker.X, attacker.Y, 336, 313) <= 4)
                {
                    attacker.Owner.Send(new Message("You can't PK in the safe zone.", Color.Red, Message.System));
                    return false;
                }
                if (attacker.Owner.Union != null && attacked.Owner.Union != null && attacker.PKMode == Game.Enums.PKMode.Union && attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag == EntityFlag.Player)
                {
                    if (attacker.Owner.Union.ID == attacked.Owner.Union.ID)
                        return false;
                }
            }
            if (attacker.EntityFlag == EntityFlag.Player)
            {
                if (attacker.Owner.Map.BaseID == (ushort)ElitePKTournament.WaitingAreaID)
                {
                    if (attacker.Owner.Team != null)
                        if (attacker.Owner.Team.EliteFighterStats != null)
                            return false;
                    if (attacker.Owner.ElitePKStats != null)
                        return false;
                }
            }


            if (attacker.UID == attacked.UID)
                return false;
            if (attacker.SkillTeamWatchingElitePKMatch != null)
                return false;
            if (attacked.SkillTeamWatchingElitePKMatch != null)
                return false;
            if (spell != null)
            {
                if (spell.ID != 8001)
                {
                    if (spell.OnlyGround)
                        if (attacked.ContainsFlag(Update.Flags.Fly))
                            return false;
                    if (melee && attacked.ContainsFlag(Update.Flags.Fly))
                        return false;
                }
            }
            if (spell != null)
            {
                if (spell.ID == 10381 || spell.ID == 6000 || spell.ID == 5030 || spell.ID == 6010)
                {
                    if (attacked.ContainsFlag(Update.Flags.Fly))
                        return false;
                }
            }
            if (spell == null)
            {
                if (attacked.ContainsFlag(Update.Flags.Fly))
                    return false;
            }
            if (attacker.PKMode == Enums.PKMode.Jiang)
            {
                if (attacked.JiangActive)
                {
                    if (!attacked.Owner.Attackable)
                        return false;
                    if (attacked.Dead)
                        return false;
                    if (attacker.MapID == 1002 || attacker.MapID == 1000
                        || attacker.MapID == 1015 || attacker.MapID == 1020
                        || attacker.MapID == 1011)
                        if (attacked.EntityFlag == EntityFlag.Player)
                            if (!attacked.Owner.Attackable)
                                return false;
                    {
                        try
                        {
                            if (attacker.AttackJiang != JiangHu.AttackFlag.None)
                            {
                                if ((attacker.AttackJiang & JiangHu.AttackFlag.NotHitFriends) == JiangHu.AttackFlag.NotHitFriends)
                                {
                                    if (attacker.Owner.Friends.ContainsKey(attacked.UID))
                                        return false;
                                }
                                if ((attacker.AttackJiang & JiangHu.AttackFlag.NoHitAlliesClan) == JiangHu.AttackFlag.NoHitAlliesClan)
                                {
                                    var attacker_clan = attacker.GetClan;
                                    if (attacker_clan != null)
                                    {
                                        if (attacker_clan.Allies.ContainsKey(attacked.ClanId))
                                            return false;
                                    }
                                }
                                if ((attacker.AttackJiang & JiangHu.AttackFlag.NotHitAlliedGuild) == JiangHu.AttackFlag.NotHitAlliedGuild)
                                {
                                    if (attacker.Owner.Guild != null)
                                    {
                                        if (attacker.Owner.Guild.Ally.ContainsKey(attacked.GuildID))
                                            return false;
                                    }
                                }
                                if ((attacker.AttackJiang & JiangHu.AttackFlag.NotHitClanMembers) == JiangHu.AttackFlag.NotHitClanMembers)
                                {
                                    if (attacker.ClanId == attacked.ClanId)
                                        return false;

                                }
                                if ((attacker.AttackJiang & JiangHu.AttackFlag.NotHitGuildMembers) == JiangHu.AttackFlag.NotHitGuildMembers)
                                {
                                    if (attacker.GuildID == attacked.GuildID)
                                        return false;

                                }
                            }
                        }
                        catch (Exception) {  }
                        return true;
                    }
                }
            }
            if (attacked.Dead) return false;
            if (attacker.EntityFlag == EntityFlag.Player)
                if (attacker.Owner.WatchingElitePKMatch != null)
                    return false;
            if (attacked.EntityFlag == EntityFlag.Player)
                if (attacked.Owner.WatchingElitePKMatch != null)
                    return false;
            if (attacker.EntityFlag == EntityFlag.Player)
                if (attacked != null && attacked.EntityFlag == EntityFlag.Player)
                    if (attacker.Owner.InTeamQualifier() && attacked.Owner.InTeamQualifier())
                        return !attacker.Owner.Team.IsTeammate(attacked.UID);

            if (attacker.MapID == CaptureTheFlag.MapID)
                if (!CaptureTheFlag.Attackable(attacker) || !CaptureTheFlag.Attackable(attacked))
                    return false;

            if (spell != null)
                if (spell.CanKill && attacker.EntityFlag == EntityFlag.Player && Constants.PKForbiddenMaps.Contains(attacker.Owner.Map.ID) && attacked.EntityFlag == EntityFlag.Player)
                    return false;
            if (attacker.EntityFlag == EntityFlag.Player)
                if (attacker.Owner.WatchingGroup != null)
                    return false;
            if (attacked == null)
                return false;
            if (attacked.Dead)
            {
                attacker.AttackPacket = null;
                return false;
            }
            if (attacker.EntityFlag == EntityFlag.Player && attacked.EntityFlag == EntityFlag.Player)
                if ((attacker.Owner.InQualifier() && attacked.Owner.IsWatching()) || (attacked.Owner.InQualifier() && attacker.Owner.IsWatching()))
                    return false;
            if (attacker.EntityFlag == EntityFlag.Player)
                if (Time32.Now < attacker.Owner.CantAttack)
                    return false;
            if (attacked.EntityFlag == EntityFlag.Monster)
            {
                if (attacked.Companion)
                {
                    if (Constants.PKForbiddenMaps.Contains(attacker.Owner.Map.ID))
                    {
                        if (attacked.Owner == attacker.Owner)
                            return false;
                        if (attacker.PKMode != Nyx.Server.Game.Enums.PKMode.PK &&
                         attacker.PKMode != Nyx.Server.Game.Enums.PKMode.Team)
                            return false;
                        else
                        {
                            attacker.AddFlag(Network.GamePackets.Update.Flags.FlashingName);
                            attacker.FlashingNameStamp = DateTime.Now;
                            attacker.FlashingNameTime = 20;

                            return true;
                        }
                    }
                }
                if (attacked.Name.Contains("Guard"))
                {
                    if (attacker.PKMode != Nyx.Server.Game.Enums.PKMode.PK &&
                    attacker.PKMode != Nyx.Server.Game.Enums.PKMode.Team)
                        return false;
                    else
                    {
                        attacker.AddFlag(Network.GamePackets.Update.Flags.FlashingName);
                        attacker.FlashingNameStamp = DateTime.Now;
                        attacker.FlashingNameTime = 20;
                        return true;
                    }
                }
                else
                    return true;
            }
            else
            {
                if (attacked.EntityFlag == EntityFlag.Player)
                    if (!attacked.Owner.Attackable)
                        return false;
                if (attacker.EntityFlag == EntityFlag.Player)
                    if (attacker.Owner.WatchingGroup == null)
                        if (attacked.EntityFlag == EntityFlag.Player)
                            if (attacked.Owner.WatchingGroup != null)
                                return false;


                if (Constants.PKForbiddenMaps.Contains(attacker.Owner.Map.ID))
                {
                    if (attacker.PKMode == Nyx.Server.Game.Enums.PKMode.PK ||
                        attacker.PKMode == Nyx.Server.Game.Enums.PKMode.Team || (spell != null && spell.CanKill))
                    {
                        attacker.Owner.Send(Constants.PKForbidden);
                        attacker.AttackPacket = null;
                    }
                    return false;
                }
                if (attacker.PKMode == Nyx.Server.Game.Enums.PKMode.Capture)
                {
                    if (attacked.ContainsFlag(Update.Flags.FlashingName) || attacked.PKPoints > 99)
                    {
                        return true;
                    }
                }
                if (attacker.PKMode == Nyx.Server.Game.Enums.PKMode.Peace)
                {
                    return false;
                }
                if (attacker.UID == attacked.UID)
                    return false;
                if (attacker.PKMode == Nyx.Server.Game.Enums.PKMode.Team)
                {
                    if (attacker.Owner.Team != null)
                    {
                        if (attacker.Owner.Team.IsTeammate(attacked.UID))
                        {
                            attacker.AttackPacket = null;
                            return false;
                        }
                    }

                    if (attacker.GuildID == attacked.GuildID && attacker.GuildID != 0)
                    {
                        attacker.AttackPacket = null;
                        return false;
                    }
                    if (attacker.ClanId == attacked.ClanId && attacker.ClanId != 0)
                    {
                        attacker.AttackPacket = null;
                        return false;
                    }
                    if (attacker.Owner.Friends.ContainsKey(attacked.UID))
                    {
                        attacker.AttackPacket = null;
                        return false;
                    }
                    if (attacker.Owner.Guild != null)
                    {

                        if (attacker.Owner.Guild.Ally != null)
                            if (attacker.Owner.Guild.Ally.ContainsKey(attacked.GuildID))
                            {
                                attacker.AttackPacket = null;
                                return false;
                            }
                    }
                    if (attacker.ClanId != 0)
                    {
                        var clan = attacker.GetClan;
                        if (clan != null)
                            if (clan.Allies.ContainsKey(attacked.ClanId))
                            {
                                attacker.AttackPacket = null;
                                return false;
                            }
                    }
                }

                if (spell != null)
                    if (spell.OnlyGround)
                        if (attacked.ContainsFlag(Update.Flags.Fly))
                            return false;

                if (spell != null)
                    if (!spell.CanKill)
                        return true;

                if (attacker.PKMode != Nyx.Server.Game.Enums.PKMode.PK &&
                    attacker.PKMode != Nyx.Server.Game.Enums.PKMode.Team && attacked.PKPoints < 99)
                {
                    attacker.AttackPacket = null;
                    return false;
                }
                else
                {
                    if (!attacked.ContainsFlag(Update.Flags.FlashingName))
                    {
                        if (!attacked.ContainsFlag(Update.Flags.BlackName))
                        {
                            if (Constants.PKFreeMaps.Contains(attacker.MapID))
                                return true;

                            if (attacker.Owner.Map.BaseID == 700)
                                return true;
                            attacker.AddFlag(Network.GamePackets.Update.Flags.FlashingName);
                            attacker.FlashingNameStamp = DateTime.Now;
                            attacker.FlashingNameTime = 20;
                        }
                    }
                }
                return true;
            }
        }
        private void TwilightAction(Entity attacker, SpellUse suse, SpellInformation spell, ushort X, ushort Y)
        {
            byte dist = 18;
            var map = attacker.Owner.Map;

            var algo = new InLineAlgorithm(attacker.X, X, attacker.Y, Y, dist);

            var count = (double)algo.lcoords.Count / 3;
            int i = 1;
            Program.World.DelayedTask.StartDelayedTask(() =>
            {
                var selected = i * (int)count;
                selected = Math.Min(algo.lcoords.Count - 1, selected);
                X = (ushort)algo.lcoords[selected].X;
                Y = (ushort)algo.lcoords[selected].Y;


                FloorItem floorItem = new FloorItem(true);
                floorItem.ItemID = FloorItem.Twilight;
                floorItem.ItemColor = (Enums.Color)(i + 1);
                floorItem.MapID = attacker.MapID;
                floorItem.Type = FloorItem.Effect;
                floorItem.X = X;
                floorItem.Y = Y;
                floorItem.OnFloor = Time32.Now;
                floorItem.Owner = attacker.Owner;
                while (map.Npcs.ContainsKey(floorItem.UID))
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                map.AddFloorItem(floorItem);

                attacker.Owner.SendScreenSpawn(floorItem, true);

                if (i != 0)
                {
                    Data data = new Network.GamePackets.Data(true);
                    data.UID = attacker.UID;
                    data.X = X;
                    data.Y = Y;
                    data.ID = 434;
                    data.wParam1 = attacker.X;
                    data.wParam2 = attacker.Y;
                    attacker.Owner.SendScreen(data, true);

                    //double percent = 1;
                    //switch (i)
                    //{
                    //    case 1:
                    //        percent = 0.92;
                    //        break;
                    //    case 2:
                    //        percent = 1.02;
                    //        break;
                    //    case 3:
                    //        percent = 1.2;
                    //        break;
                    //}


                    foreach (Interfaces.IMapObject _obj in attacker.Owner.Screen.Objects)
                    {
                        bool hit = false;
                        var selected2 = Math.Max(0, i - 1) * (int)count;
                        selected2 = Math.Min(algo.lcoords.Count - 1, selected2);
                        if (Kernel.GetDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[selected].X, (ushort)algo.lcoords[selected].Y) <= spell.Range)
                            hit = true;
                        //for (int j = selected2; j < selected; j++)
                        //    if (Kernel.GetDistance(_obj.X, _obj.Y, (ushort)algo.lcoords[j].X, (ushort)algo.lcoords[j].Y) <= spell.Range)
                        //        hit = true;
                        if (hit)
                        {
                            if (_obj.MapObjType == MapObjectType.Monster)
                            {
                                attacked = _obj as Entity;
                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                {
                                    var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                    suse.AddTarget(attacked, damage, attack);
                                }
                            }
                            else if (_obj.MapObjType == MapObjectType.Player)
                            {
                                attacked = _obj as Entity;
                                if (CanAttack(attacker, attacked, spell, attack.AttackType == Attack.Melee))
                                {
                                    var damage = Game.Attacking.Calculate.Melee(attacker, attacked, spell, ref attack);
                                    damage = (uint)(damage * 0.5);
                                    //damage = (uint)((double)(damage * percent));
                                    ReceiveAttack(attacker, attacked, attack, ref damage, spell);

                                    suse.AddTarget(attacked, damage, attack);
                                }
                            }
                            else if (_obj.MapObjType == MapObjectType.SobNpc)
                            {
                                var attackedsob = _obj as SobNpcSpawn;
                                if (CanAttack(attacker, attackedsob, spell))
                                {
                                    var damage = Game.Attacking.Calculate.Melee(attacker, attackedsob, ref attack);

                                    ReceiveAttack(attacker, attackedsob, attack, damage, spell);

                                    suse.AddTarget(attackedsob, damage, attack);
                                }
                            }
                        }
                    }
                    if (suse.Targets.Count > 0)
                        attacker.Owner.SendScreen(suse, true);
                    suse.Targets.Clear();
                }
                i++;
            }, 0, 2, 500);
        }
        public static void CheckForExtraWeaponPowers(Client.GameClient client, Entity attacked)
        {
            #region Right Hand
            var weapons = client.Weapons;
            if (weapons.Item1 != null)
            {
                if (weapons.Item1.ID != 0)
                {
                    var Item = weapons.Item1;
                    if (Item.Effect != Enums.ItemEffect.None)
                    {
                        if (Kernel.Rate(30))
                        {
                            switch (Item.Effect)
                            {
                                case Enums.ItemEffect.HP:
                                    {
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = 1;
                                        spellUse.SpellID = 1175;
                                        spellUse.SpellLevel = 4;
                                        spellUse.X = client.Entity.X;
                                        spellUse.Y = client.Entity.Y;
                                        spellUse.AddTarget(client.Entity, 300, null);
                                        uint damage = Math.Min(300, client.Entity.MaxHitpoints - client.Entity.Hitpoints);
                                        client.Entity.Hitpoints += damage;
                                        client.SendScreen(spellUse, true);
                                        break;
                                    }
                                case Enums.ItemEffect.MP:
                                    {
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = 1;
                                        spellUse.SpellID = 1175;
                                        spellUse.SpellLevel = 2;
                                        spellUse.X = client.Entity.X;
                                        spellUse.Y = client.Entity.Y;
                                        spellUse.AddTarget(client.Entity, 300, null);
                                        ushort damage = (ushort)Math.Min(300, client.Entity.MaxMana - client.Entity.Mana);
                                        client.Entity.Mana += damage;
                                        client.SendScreen(spellUse, true);
                                        break;
                                    }
                                case Enums.ItemEffect.Shield:
                                    {
                                        if (client.Entity.ContainsFlag(Update.Flags.MagicShield))
                                            return;
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = 1;
                                        spellUse.SpellID = 1020;
                                        spellUse.SpellLevel = 0;
                                        spellUse.X = client.Entity.X;
                                        spellUse.Y = client.Entity.Y;
                                        spellUse.AddTarget(client.Entity, 120, null);
                                        client.Entity.ShieldTime = 0;
                                        client.Entity.ShieldStamp = Time32.Now;
                                        client.Entity.MagicShieldStamp = Time32.Now;
                                        client.Entity.MagicShieldTime = 0;

                                        client.Entity.AddFlag(Update.Flags.MagicShield);
                                        client.Entity.MagicShieldStamp = Time32.Now;
                                        client.Entity.MagicShieldIncrease = 1.1f;
                                        client.Entity.MagicShieldTime = 120;
                                        if (client.Entity.EntityFlag == EntityFlag.Player)
                                            client.Send(Constants.Shield(2, 120));
                                        client.SendScreen(spellUse, true);
                                        break;
                                    }
                                case Enums.ItemEffect.Poison:
                                    {
                                        if (attacked != null)
                                        {
                                            if (Constants.PKForbiddenMaps.Contains(client.Entity.MapID))
                                                return;
                                            if (client.Map.BaseID == 700)
                                                return;
                                            if (attacked.UID == client.Entity.UID)
                                                return;
                                            if (attacked.ToxicFogLeft > 0)
                                                return;
                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.SpellID = 5040;
                                            spellUse.Attacker = attacked.UID;
                                            spellUse.SpellLevel = 9;
                                            spellUse.X = attacked.X;
                                            spellUse.Y = attacked.Y;
                                            spellUse.AddTarget(attacked, 0, null);
                                            spellUse.Targets[attacked.UID].Hit = true;
                                            attacked.ToxicFogStamp = Time32.Now;
                                            attacked.ToxicFogLeft = 10;
                                            attacked.ToxicFogPercent = 0.05F;
                                            client.SendScreen(spellUse, true);
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            #endregion
            #region Left Hand
            if (weapons.Item2 != null)
            {
                if (weapons.Item2.ID != 0)
                {
                    var Item = weapons.Item2;
                    if (Item.Effect != Enums.ItemEffect.None)
                    {
                        if (Kernel.Rate(30))
                        {
                            switch (Item.Effect)
                            {
                                case Enums.ItemEffect.HP:
                                    {
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = 1;
                                        spellUse.SpellID = 1175;
                                        spellUse.SpellLevel = 4;
                                        spellUse.X = client.Entity.X;
                                        spellUse.Y = client.Entity.Y;
                                        spellUse.AddTarget(client.Entity, 300, null);
                                        uint damage = Math.Min(300, client.Entity.MaxHitpoints - client.Entity.Hitpoints);
                                        client.Entity.Hitpoints += damage;
                                        client.SendScreen(spellUse, true);
                                        break;
                                    }
                                case Enums.ItemEffect.MP:
                                    {
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = 1;
                                        spellUse.SpellID = 1175;
                                        spellUse.SpellLevel = 2;
                                        spellUse.X = client.Entity.X;
                                        spellUse.Y = client.Entity.Y;
                                        spellUse.AddTarget(client.Entity, 300, null);
                                        ushort damage = (ushort)Math.Min(300, client.Entity.MaxMana - client.Entity.Mana);
                                        client.Entity.Mana += damage;
                                        client.SendScreen(spellUse, true);
                                        break;
                                    }
                                case Enums.ItemEffect.Shield:
                                    {
                                        if (client.Entity.ContainsFlag(Update.Flags.MagicShield))
                                            return;
                                        SpellUse spellUse = new SpellUse(true);
                                        spellUse.Attacker = 1;
                                        spellUse.SpellID = 1020;
                                        spellUse.SpellLevel = 0;
                                        spellUse.X = client.Entity.X;
                                        spellUse.Y = client.Entity.Y;
                                        spellUse.AddTarget(client.Entity, 120, null);
                                        client.Entity.ShieldTime = 0;
                                        client.Entity.ShieldStamp = Time32.Now;
                                        client.Entity.MagicShieldStamp = Time32.Now;
                                        client.Entity.MagicShieldTime = 0;

                                        client.Entity.AddFlag(Update.Flags.MagicShield);
                                        client.Entity.MagicShieldStamp = Time32.Now;
                                        client.Entity.MagicShieldIncrease = 1.1f;
                                        client.Entity.MagicShieldTime = 120;
                                        if (client.Entity.EntityFlag == EntityFlag.Player)
                                            client.Send(Constants.Shield(2, 120));
                                        client.SendScreen(spellUse, true);
                                        break;
                                    }
                                case Enums.ItemEffect.Poison:
                                    {
                                        if (attacked != null)
                                        {
                                            if (attacked.UID == client.Entity.UID)
                                                return;
                                            if (Constants.PKForbiddenMaps.Contains(client.Entity.MapID))
                                                return;
                                            if (client.Map.BaseID == 700)
                                                return;
                                            if (attacked.ToxicFogLeft > 0)
                                                return;
                                            SpellUse spellUse = new SpellUse(true);
                                            spellUse.SpellID = 5040;
                                            spellUse.Attacker = attacked.UID;
                                            spellUse.SpellLevel = 9;
                                            spellUse.X = attacked.X;
                                            spellUse.Y = attacked.Y;
                                            spellUse.AddTarget(attacked, 0, null);
                                            spellUse.Targets[attacked.UID].Hit = true;
                                            attacked.ToxicFogStamp = Time32.Now;
                                            attacked.ToxicFogLeft = 10;
                                            attacked.ToxicFogPercent = 0.05F;
                                            client.SendScreen(spellUse, true);
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            #endregion
        }
        public void CheckForSuperGems(Client.GameClient client)
        {
            for (uint i = 1; i < 12; i++)
            {
                if (i != 7)
                {
                    ConquerItem item = client.Equipment.TryGetItem(i);
                    if (item != null && item.ID != 0)
                    {
                        if (item.SocketOne != 0)
                        {
                            if (item.SocketOne == Enums.Gem.SuperPhoenixGem)
                            {
                                if (Kernel.Rate(3)) //this is where your chances when to display the phoenix gem effect
                                {
                                    _String str = new _String(true);
                                    str.UID = attacker.UID;
                                    str.TextsCount = 1;
                                    str.Type = _String.Effect;
                                    str.Texts.Add("phoenix");
                                    attacker.Owner.SendScreen(str, true);
                                }
                            }
                            if (item.SocketOne == Enums.Gem.SuperDragonGem)
                            {
                                if (Kernel.Rate(3)) //this is where your chances when to display the dragon gem effect
                                {
                                    _String str = new _String(true);
                                    str.UID = attacker.UID;
                                    str.TextsCount = 1;
                                    str.Type = _String.Effect;
                                    str.Texts.Add("dragon");
                                    attacker.Owner.SendScreen(str, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}