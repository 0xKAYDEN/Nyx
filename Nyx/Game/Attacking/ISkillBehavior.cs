using Nyx.Server.Client;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.Attacking
{
    public interface ISkillBehavior
    {
        void ExecuteOnPlayers(GameClient caster, GameClient Target, Database.SpellInformation spell);
        void ExecuteOnMonsters(GameClient caster, Entity Target, Database.SpellInformation spell);
        void ExecuteOnNpc(GameClient caster, SobNpcSpawn Target, Database.SpellInformation spell);

        bool ValidAttackOnPlayers(GameClient caster, GameClient Target, Database.SpellInformation spell)
        {
            if (caster.Entity.Mana < spell.UseMana || caster.Entity.Stamina < spell.UseStamina
                || spell.Range < Kernel.GetDDistance(caster.Entity.X, caster.Entity.Y, Target.Entity.X, Target.Entity.Y) || Target.Entity.Dead || caster.Entity.Dead ||
                caster.Entity.MapID != Target.Entity.MapID || Time32.Now > caster.Entity.SkillCoolDown || caster.Entity.SkillTeamWatchingElitePKMatch != null || caster.WatchingElitePKMatch != null
                || caster.WatchingGroup != null || spell == null || spell.NeedXP == 1 && !caster.Entity.ContainsFlag(Update.Flags.XPList))
            {
                if (spell.UseArrows > 0 && isArcherSkill(spell.ID))
                {
                    var weapons = caster.Weapons;
                    if (weapons.Item2 != null)
                    if (!caster.Entity.ContainsFlag3(Update.Flags3.Assassin))
                    if (!PacketHandler.IsArrow(weapons.Item2.ID))
                     return false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        bool ValidAttackOnMonsters(GameClient caster, Entity Target, Database.SpellInformation spell)
        {
            if (caster.Entity.Mana < spell.UseMana || caster.Entity.Stamina < spell.UseStamina || Target.EntityFlag != EntityFlag.Monster
                || spell.Range < Kernel.GetDDistance(caster.Entity.X, caster.Entity.Y, Target.X, Target.Y) || Target.Dead || caster.Entity.Dead ||
                caster.Entity.MapID != Target.MapID || Time32.Now > caster.Entity.SkillCoolDown || caster.Entity.SkillTeamWatchingElitePKMatch != null || caster.WatchingElitePKMatch != null
                || caster.WatchingGroup != null || spell == null || spell.NeedXP == 1 && !caster.Entity.ContainsFlag(Update.Flags.XPList))
            {
                if (spell.UseArrows > 0 && isArcherSkill(spell.ID))
                {
                    var weapons = caster.Weapons;
                    if (weapons.Item2 != null)
                        if (!caster.Entity.ContainsFlag3(Update.Flags3.Assassin))
                            if (!PacketHandler.IsArrow(weapons.Item2.ID))
                                return false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        bool ValidAttackOnNpc(GameClient caster, SobNpcSpawn Target, Database.SpellInformation spell)
        {
            if (caster.Entity.Mana < spell.UseMana || caster.Entity.Stamina < spell.UseStamina
                || spell.Range < Kernel.GetDDistance(caster.Entity.X, caster.Entity.Y, Target.X, Target.Y) || Target.MaxHitpoints > 0 || caster.Entity.Dead ||
                caster.Entity.MapID != Target.MapID || Time32.Now > caster.Entity.SkillCoolDown || caster.Entity.SkillTeamWatchingElitePKMatch != null || caster.WatchingElitePKMatch != null
                || caster.WatchingGroup != null || spell == null || spell.NeedXP == 1 && !caster.Entity.ContainsFlag(Update.Flags.XPList))
            {
                if (spell.UseArrows > 0 && isArcherSkill(spell.ID))
                {
                    var weapons = caster.Weapons;
                    if (weapons.Item2 != null)
                        if (!caster.Entity.ContainsFlag3(Update.Flags3.Assassin))
                            if (!PacketHandler.IsArrow(weapons.Item2.ID))
                                return false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        bool isArcherSkill(uint ID)
        {
            if (ID >= 8000 && ID <= 9875)
                return true;
            return false;
        }

        // Concrete implementation for a basic targeted attack
        //public class BasicAttackBehavior : ISkillBehavior
        //{
        //    public void Execute(GameClient caster, SkillData skillData, Vector3 targetPosition, uint targetId)
        //    {
        //        // 1. Find the target entity on the server
        //        GameClient target = ServerWorld.FindPlayer(targetId); // Or monster

        //        // 2. Validate: Is target in range? Alive?
        //        if (target == null || !target.Entity.Dead || caster.Entity.SkillTeamWatchingElitePKMatch != null ||
        //            caster.WatchingElitePKMatch != null || caster.WatchingGroup != null || caster.Entity.Mana < spell.UseMana ||
        //            caster.Entity.Stamina < spell.UseStamina || Vector3.Distance(caster.Entity, target.Entity) > skillData.Range)
        //        {
        //            caster.SendSkillFailedPacket();
        //            return;
        //        }
        //        // 3. Calculate damage (Server-authoritative algorithm!)
        //        int finalDamage = CalculateDamage(caster, target, skillData);

        //        // 4. Apply the damage
        //        target.TakeDamage(finalDamage, caster);

        //        // 5. Create effect (blood splatter, etc.)
        //        Server.BroadcastSkillEffect(caster, target.Position, skillData.Id);

        //        // 6. Set cooldown and consume mana on the caster
        //        caster.Character.StartSkillCooldown(skillData.Id);
        //        caster.Character.ConsumeMana(skillData.ManaCost);
        //    }

        //}
    }
}
