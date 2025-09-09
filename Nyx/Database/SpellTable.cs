using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Database
{
    public enum LeftToAdd : ushort
    {
        Bless = 9876,
        //----------
        SummonGuard = 4000,
        SummonBat = 4010,
        SummonBatBoss = 4020,
        BloodyBat = 4050,
        FireEvil = 4060,
        Skeleton = 4070
    }
    public class SpellSort
    {
        public const byte
            Damage = 1,
            Heal = 2,
            MultiWeaponSkill = 4,
            Circle = 5,
            XP = 6,
            Revive = 7,
            XPIncrease = 11,
            Dash = 12,
            Linear = 14,
            SingleWeaponSkill = 16,
            Specials = 19,
            ManaAdd = 20,
            Summon = 23,
            HPPercentDecrease = 26,
            Spook = 30,
            WarCry = 31,
            Ride = 32;
    }
    public class SpellTarget
    {
        public const byte
            Magic = 0,
            EntitiesOnly = 1,
            Self = 2,
            AroundCoordonates = 4,
            Sector = 8,//power % 1000 = sector angle
            AutoAttack = 16,
            PlayersOnly = 32;
    }
    public class SpellInformation
    {
        public ushort ID;
        public byte Level;
        public bool CanKill;
        public byte Sort;
        public bool OnlyGround;
        public bool Multi;
        public byte Target;
        public ushort UseMana;
        public byte UseStamina;
        public byte UseArrows;
        public byte Percent;
        public int Sector;
        public int Duration;
        public ushort Range;
        public ushort Distance;
        public uint Power;
        public float PowerPercent;
        public ulong Status;
        public uint NeedExperience;
        public byte NeedLevel;
        public byte NeedXP;
        public ushort HumanPower;
        public ushort CPCost = 0;
        public List<ushort> WeaponSubtype;
        public List<ushort> OnlyWithThisWeaponSubtype;
        public ushort NextSpellID;
        public string Name;
    }
    public class SpellTable
    {
        public static SafeDictionary<ushort, SafeDictionary<byte, SpellInformation>> SpellInformations = new SafeDictionary<ushort, SafeDictionary<byte, SpellInformation>>();
        public static SafeDictionary<ushort, List<ushort>> WeaponSpells = new SafeDictionary<ushort, List<ushort>>();
        public static void Load()
        {
            foreach (string str in System.IO.File.ReadAllLines(@"database\magictype.txt"))
            {
                Parse(str.Trim());
            }
        }
        public static void Parse(string Line)
        {
            string[] strArray2 = Line.Split(new string[] { "@@", " " }, StringSplitOptions.RemoveEmptyEntries);
            SpellInformation information = new SpellInformation();
            information.WeaponSubtype = new List<ushort>();
            information.OnlyWithThisWeaponSubtype = new List<ushort>();
            information.ID = Convert.ToUInt16(strArray2[1]);
            information.Name = strArray2[3];
            information.Level = Convert.ToByte(strArray2[8]);

            if (Convert.ToByte(strArray2[4]) == 1)
                information.CanKill = true;
            else
                information.CanKill = false;

            information.Sort = Convert.ToByte(strArray2[2]);
            if (Convert.ToByte(strArray2[5]) == 1)
                information.OnlyGround = true;
            else
                information.OnlyGround = false;
            if (Convert.ToByte(strArray2[6]) == 1)
                information.Multi = true;
            else
                information.Multi = false;

            information.Target = Convert.ToByte(strArray2[7]);
            information.Level = Convert.ToByte(strArray2[8]);
            information.UseMana = Convert.ToUInt16(strArray2[9]);
            if (Convert.ToInt32(strArray2[10]) > 0)
            {
                information.Power = Convert.ToUInt32(strArray2[10]);
            }
            else
            {
                information.Power = 0;
            }
            information.PowerPercent = ((float)information.Power % 1000) / 100;

            information.Percent = Convert.ToByte(strArray2[12]);
            information.Duration = Convert.ToInt32(strArray2[13]);
            information.Range = Convert.ToUInt16(strArray2[14]);
            information.Sector = information.Range * 20;
            information.Distance = Convert.ToUInt16(strArray2[15]);
            if (information.Distance >= 4)
            {
                information.Distance = (ushort)(information.Distance - 1);
            }
            information.Status = (ulong)Convert.ToInt64(strArray2[16]);
            information.NeedExperience = Convert.ToUInt32(strArray2[18]);
            information.NeedLevel = Convert.ToByte(strArray2[20]);
            uint WeaponSubtype = Convert.ToUInt32(strArray2[22]);
            var subtype1 = (ushort)(WeaponSubtype % 1000);
            var subtype2 = (ushort)((WeaponSubtype / 1000) % 1000);
            if (WeaponSubtype == 60000)
                subtype1 = 614;
            if (subtype1 != 0)
            {
                information.WeaponSubtype.Add(subtype1);
                information.OnlyWithThisWeaponSubtype.Add(subtype1);
            }
            if (subtype2 != 0)
            {
                information.WeaponSubtype.Add(subtype2);
                information.OnlyWithThisWeaponSubtype.Add(subtype2);
            }

            if (WeaponSubtype == 50000)
                information.WeaponSubtype = information.OnlyWithThisWeaponSubtype = new List<ushort>();
            information.NextSpellID = Convert.ToUInt16(strArray2[32]);
            information.NeedXP = Convert.ToByte(strArray2[21]);
            information.UseStamina = Convert.ToByte(strArray2[29]);
            information.UseArrows = Convert.ToByte(strArray2[34]);
            information.HumanPower = (ushort)(Convert.ToUInt16(strArray2[35]) - 30000);
            if (information.CPCost == 0 && information.Level == 0)
                information.CPCost = 27;
            if (information.CPCost == 0 && information.Level == 1)
                information.CPCost = 81;
            if (information.CPCost == 0 && information.Level == 2)
                information.CPCost = 122;
            if (information.CPCost == 0 && information.Level == 3)
                information.CPCost = 181;
            if (information.CPCost == 0 && information.Level == 4)
                information.CPCost = 274;
            if (information.CPCost == 0 && information.Level == 5)
                information.CPCost = 411;
            if (information.CPCost == 0 && information.Level == 6)
                information.CPCost = 617;
            if (information.CPCost == 0 && information.Level == 7)
                information.CPCost = 926;
            if (information.CPCost == 0 && information.Level == 8)
                information.CPCost = 1386;
            if (information.CPCost == 0 && information.Level == 9)
                information.CPCost = 2084;
            if (SpellInformations.ContainsKey(information.ID))
            {
                SpellInformations[information.ID].Add(information.Level, information);
            }
            else
            {
                SpellInformations.Add(information.ID, new SafeDictionary<byte, SpellInformation>(10));
                SpellInformations[information.ID].Add(information.Level, information);
            }
            if (information.Distance > 17)
            {
                information.Distance = 17;
            }

            if (information.WeaponSubtype.Count != 0)
            {
                switch (information.ID)
                {
                    case 1290:
                    case 1300:
                    case 1250:
                    case 1260:
                    case 5010:
                    case 5020:
                    case 5030:
                    case 5040:
                    case 5050:
                    case 7020:
                    case 7030:
                    case 7000:
                    case 7010:
                    case 11120:
                    case 11140:
                    case 11230:
                    case 12110:
                    case 7040:
                    case 10490:
                    case 12240:
                    case 12570:
                    case 11990:
                    case 12230:
                    case 12220:
                    case 12210:
                    case 12670:
                        for (int i = 0; i < information.WeaponSubtype.Count; i++)
                        {
                            var subtype = information.WeaponSubtype[i];
                            if (!WeaponSpells.ContainsKey(subtype))
                                WeaponSpells.Add(subtype, new List<ushort>());
                            if (!WeaponSpells[subtype].Contains(information.ID))
                                WeaponSpells[subtype].Add(information.ID);
                        }
                        break;
                }
            }
        }
        public static SpellInformation GetSpell(ushort ID, Client.GameClient client)
        {
            if (client != null)
                if (client.Spells.ContainsKey(ID))
                    return GetSpell(ID, client.Spells[ID].Level);
            return GetSpell(ID, 0);
        }
        public static SpellInformation GetSpell(ushort ID, byte level)
        {
            if (SpellInformations.ContainsKey(ID))
            {
                var dict = SpellInformations[ID];
                if (dict.ContainsKey(level))
                    return dict[level];
                else if (dict.Count != 0) return dict.Values.First(p => p.ID != 0);
                else return null;

            }
            else return null;
        }
    }
}