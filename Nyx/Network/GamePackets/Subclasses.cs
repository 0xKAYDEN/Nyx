using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;
using System.Collections.Generic;

namespace Nyx.Server.Game
{
    public class SubClass
    {
        public byte ID;
        public byte Phase;
        public byte Level;
    }
    public class Subclasses
    {
        public Dictionary<byte, SubClass> Classes;
        public ushort StudyPoints;
        public byte Active;
        public ushort Critical;
        public ushort SCritical;
        public ushort Imunity;
        public ushort Penetration;
        public ushort Detoxication;
        public ushort PhsicalAttack;
        public ushort MagiAttack;
        public ushort HitPoints;

        public Subclasses()
        {
            this.Classes = new Dictionary<byte, SubClass>();
            this.StudyPoints = (ushort)0;
            this.Active = (byte)0;
        }

        public void Activate(GameClient client, byte clas)
        {
            this.Active = clas;
            client.Entity.SubClass = clas;
        }

        public uint GetHashPoint()
        {
            uint num = 0U;
            if (this.Classes.ContainsKey((byte)1))
                num += (uint)this.Classes[(byte)1].Phase;
            if (this.Classes.ContainsKey((byte)2))
                num += (uint)this.Classes[(byte)2].Phase * 10U;
            if (this.Classes.ContainsKey((byte)3))
                num += (uint)this.Classes[(byte)3].Phase * 100U;
            if (this.Classes.ContainsKey((byte)4))
                num += (uint)this.Classes[(byte)4].Phase * 1000U;
            if (this.Classes.ContainsKey((byte)5))
                num += (uint)this.Classes[(byte)5].Phase * 10000U;
            if (this.Classes.ContainsKey((byte)6))
                num += (uint)this.Classes[(byte)6].Phase * 100000U;
            if (this.Classes.ContainsKey((byte)9))
                num += (uint)this.Classes[(byte)9].Phase * 100000000U;
            return num;
        }

        public void UpgradeStatus(GameClient client, bool removeold)
        {
            if (removeold)
                //this.RemoveStatus(client);
                this.ClearStatus();
            foreach (SubClass Sc in this.Classes.Values)
            {
                switch (Sc.ID)
                {
                    case (byte)1:
                        this.Critical = (ushort)((uint)Subclasses.GetDamage(Sc) * 100U);
                        continue;
                    case (byte)2:
                        this.SCritical = (ushort)((uint)Subclasses.GetDamage(Sc) * 100U);
                        continue;
                    case (byte)3:
                        this.Imunity = (ushort)((uint)Subclasses.GetDamage(Sc) * 100U);
                        continue;
                    case (byte)4:
                        this.Penetration = (ushort)((uint)Subclasses.GetDamage(Sc) * 100U);
                        continue;
                    case (byte)5:
                        this.Detoxication = Subclasses.GetDamage(Sc);
                        continue;
                    case (byte)6:
                        this.MagiAttack = this.PhsicalAttack = Subclasses.GetDamage(Sc);
                        continue;
                    case (byte)9:
                        this.HitPoints = Subclasses.GetDamage(Sc);
                        continue;
                    default:
                        continue;
                }
            }
            //this.AddStatus(client);
        } 

        public void ClearStatus()
        {
            Subclasses subclasses1 = this;
            Subclasses subclasses2 = this;
            Subclasses subclasses3 = this;
            Subclasses subclasses4 = this;
            Subclasses subclasses5 = this;
            Subclasses subclasses6 = this;
            Subclasses subclasses7 = this;
            this.MagiAttack = (ushort)0;
            int num1 = 0;
            subclasses7.PhsicalAttack = (ushort)num1;
            int num2 = 0;
            subclasses6.Detoxication = (ushort)num2;
            int num3 = 0;
            subclasses5.Penetration = (ushort)num3;
            int num4 = 0;
            subclasses4.Imunity = (ushort)num4;
            int num5 = 0;
            subclasses3.SCritical = (ushort)num5;
            int num6 = 0;
            subclasses2.Critical = (ushort)num6;
            int num7 = 0;
            subclasses1.HitPoints = (ushort)num7;
        }

        public void RemoveStatus(GameClient client)
        {
            client.Entity.Hitpoints -= (uint)this.HitPoints;
            client.Entity.MaxHitpoints -= (uint)this.HitPoints;
            client.Entity.CriticalStrike -= (int)this.Critical;
            client.Entity.SkillCStrike -= (int)this.SCritical;
            client.Entity.Immunity -= (int)this.Imunity;
            client.Entity.Penetration -= this.Penetration;
            client.Entity.Detoxication -= this.Detoxication;
            client.Entity.PhysicalDamageIncrease -= this.PhsicalAttack;
            client.Entity.MagicDamageIncrease -= this.MagiAttack;
        }

        public void AddStatus(GameClient client)
        {
            client.Entity.MaxHitpoints += (uint)this.HitPoints;
            client.Entity.CriticalStrike += (int)this.Critical;
            client.Entity.SkillCStrike += (int)this.SCritical;
            client.Entity.Immunity += (int)this.Imunity;
            client.Entity.Penetration += this.Penetration;
            client.Entity.Detoxication += this.Detoxication;
            client.Entity.PhysicalDamageIncrease += this.PhsicalAttack;
            client.Entity.MagicDamageIncrease += this.MagiAttack;
        }

        public void Send(GameClient client)
        {
            SubClassShow subClassShow = new SubClassShow((ushort)this.Classes.Count);
            subClassShow.ID = (ushort)7;
            subClassShow.Study = (ushort)this.StudyPoints;
            foreach (SubClass subClass in this.Classes.Values)
                subClassShow.Apprend(subClass.ID, subClass.Phase, subClass.Level);
            client.Send(subClassShow.Encode());
        }
        public void SendLearn(Subclasses.ClassID ID, byte Level, GameClient client)
        {
            client.Send(new SubClassShow((ushort)0)
            {
                ID = (ushort)4,
                Class = ((byte)ID),
                Level = Level
            }.Encode());
        }
        public void SendPromoted(Subclasses.ClassID ID, byte Phase, GameClient client)
        {
            client.Send(new SubClassShow((ushort)0)
            {
                ID = (ushort)5,
                Class = ((byte)ID),
                Level = Phase
            }.Encode());
        }

        public static ushort GetDamage(SubClass Sc)
        {
            ushort num = (ushort)0;
            switch (Sc.ID)
            {
                case (byte)1:
                case (byte)2:
                case (byte)3:
                case (byte)4:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)1;
                            break;
                        case (byte)2:
                            num = (ushort)2;
                            break;
                        case (byte)3:
                            num = (ushort)3;
                            break;
                        case (byte)4:
                            num = (ushort)4;
                            break;
                        case (byte)5:
                            num = (ushort)6;
                            break;
                        case (byte)6:
                            num = (ushort)8;
                            break;
                        case (byte)7:
                            num = (ushort)10;
                            break;
                        case (byte)8:
                            num = (ushort)12;
                            break;
                        case (byte)9:
                            num = (ushort)15;
                            break;
                    }
                    break;
                case (byte)5:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)8;
                            break;
                        case (byte)2:
                            num = (ushort)16;
                            break;
                        case (byte)3:
                            num = (ushort)24;
                            break;
                        case (byte)4:
                            num = (ushort)32;
                            break;
                        case (byte)5:
                            num = (ushort)40;
                            break;
                        case (byte)6:
                            num = (ushort)48;
                            break;
                        case (byte)7:
                            num = (ushort)56;
                            break;
                        case (byte)8:
                            num = (ushort)64;
                            break;
                        case (byte)9:
                            num = (ushort)72;
                            break;
                    }
                    break;
                case (byte)6:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)100;
                            break;
                        case (byte)2:
                            num = (ushort)200;
                            break;
                        case (byte)3:
                            num = (ushort)300;
                            break;
                        case (byte)4:
                            num = (ushort)400;
                            break;
                        case (byte)5:
                            num = (ushort)500;
                            break;
                        case (byte)6:
                            num = (ushort)600;
                            break;
                        case (byte)7:
                            num = (ushort)700;
                            break;
                        case (byte)8:
                            num = (ushort)800;
                            break;
                        case (byte)9:
                            num = (ushort)1000;
                            break;
                    }
                    break;
                case (byte)9:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)100;
                            break;
                        case (byte)2:
                            num = (ushort)200;
                            break;
                        case (byte)3:
                            num = (ushort)300;
                            break;
                        case (byte)4:
                            num = (ushort)400;
                            break;
                        case (byte)5:
                            num = (ushort)500;
                            break;
                        case (byte)6:
                            num = (ushort)600;
                            break;
                        case (byte)7:
                            num = (ushort)800;
                            break;
                        case (byte)8:
                            num = (ushort)1000;
                            break;
                        case (byte)9:
                            num = (ushort)1200;
                            break;
                    }
                    break;
            }
            return num;
        }

        public static ushort GetRequired(SubClass Sc)
        {
            ushort num = (ushort)0;
            switch (Sc.ID)
            {
                case (byte)1:
                case (byte)2:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)300;
                            break;
                        case (byte)2:
                            num = (ushort)900;
                            break;
                        case (byte)3:
                            num = (ushort)1800;
                            break;
                        case (byte)4:
                            num = (ushort)2700;
                            break;
                        case (byte)5:
                            num = (ushort)3600;
                            break;
                        case (byte)6:
                            num = (ushort)5100;
                            break;
                        case (byte)7:
                            num = (ushort)6900;
                            break;
                        case (byte)8:
                            num = (ushort)8700;
                            break;
                        case (byte)9:
                            num = ushort.MaxValue;
                            break;
                    }
                    break;
                case (byte)3:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)600;
                            break;
                        case (byte)2:
                            num = (ushort)1800;
                            break;
                        case (byte)3:
                            num = (ushort)3600;
                            break;
                        case (byte)4:
                            num = (ushort)5400;
                            break;
                        case (byte)5:
                            num = (ushort)7200;
                            break;
                        case (byte)6:
                            num = (ushort)10200;
                            break;
                        case (byte)7:
                            num = (ushort)13800;
                            break;
                        case (byte)8:
                            num = (ushort)17400;
                            break;
                        case (byte)9:
                            num = ushort.MaxValue;
                            break;
                    }
                    break;
                case (byte)4:
                case (byte)6:
                case (byte)9:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)400;
                            break;
                        case (byte)2:
                            num = (ushort)1200;
                            break;
                        case (byte)3:
                            num = (ushort)2400;
                            break;
                        case (byte)4:
                            num = (ushort)3600;
                            break;
                        case (byte)5:
                            num = (ushort)4800;
                            break;
                        case (byte)6:
                            num = (ushort)6800;
                            break;
                        case (byte)7:
                            num = (ushort)9200;
                            break;
                        case (byte)8:
                            num = (ushort)11600;
                            break;
                        case (byte)9:
                            num = ushort.MaxValue;
                            break;
                    }
                    break;
                case (byte)5:
                    switch (Sc.Level)
                    {
                        case (byte)1:
                            num = (ushort)100;
                            break;
                        case (byte)2:
                            num = (ushort)200;
                            break;
                        case (byte)3:
                            num = (ushort)300;
                            break;
                        case (byte)4:
                            num = (ushort)400;
                            break;
                        case (byte)5:
                            num = (ushort)500;
                            break;
                        case (byte)6:
                            num = (ushort)1000;
                            break;
                        case (byte)7:
                            num = (ushort)4000;
                            break;
                        case (byte)8:
                            num = (ushort)9000;
                            break;
                        case (byte)9:
                            num = ushort.MaxValue;
                            break;
                    }
                    break;
            }
            return num;
        }

        public enum ClassID : byte
        {
            MartialArtist = 1,
            Warlock = 2,
            ChiMaster = 3,
            Sage = 4,
            Apothecary = 5,
            Performer = 6,
            Wrangler = 9
        }
    }
}
