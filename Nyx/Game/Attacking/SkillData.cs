using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nyx.Server.Game.Attacking
{
    // Example Skill Data Class (Server-Side)
    public class SkillData
    {
        public ushort SpellID { get; set; }
        public string Name { get; set; }
        public SkillType Type { get; set; } // Enum: Passive, ActiveTargeted, ActiveAOE, Toggle, Buff
        public byte Level { get; set; }
        public ushort ManaCost { get; set; }
        public ushort StaminaCost { get; set; }
        public Time32 CooldownTime { get; set; }
        public ushort Range { get; set; }

        // Effect parameters (this is the "algorithm" part)
        public uint BaseDamage { get; set; }
        public float DamageMultiplier { get; set; } // Based on player's attack
        public int DurationTicks { get; set; } // For buffs/DoTs
        public int HealAmount { get; set; }
        public ushort AoERadius { get; set; }

        // Reference to the logic class name (e.g., "BasicAttackLogic", "FireballLogic", "HealAoeLogic")
        public string BehaviorLogicClass { get; set; }

        //public class SpellInformation
        //{
        //    public ushort ID;
        //    public byte Level;
        //    public bool CanKill;
        //    public byte Sort;
        //    public bool OnlyGround;
        //    public bool Multi;
        //    public byte Target;
        //    public ushort UseMana;
        //    public byte UseStamina;
        //    public byte UseArrows;
        //    public byte Percent;
        //    public int Sector;
        //    public int Duration;
        //    public ushort Range;
        //    public ushort Distance;
        //    public uint Power;
        //    public float PowerPercent;
        //    public ulong Status;
        //    public uint NeedExperience;
        //    public byte NeedLevel;
        //    public byte NeedXP;
        //    public ushort HumanPower;
        //    public ushort CPCost = 0;
        //    public List<ushort> WeaponSubtype;
        //    public List<ushort> OnlyWithThisWeaponSubtype;
        //    public ushort NextSpellID;
        //    public string Name;
        //}
    }
}
