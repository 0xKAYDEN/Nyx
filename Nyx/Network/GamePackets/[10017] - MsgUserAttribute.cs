using System;
using System.Collections.Generic;
using Nyx.Server.Game;
using Nyx.Server.Client;

namespace Nyx.Server.Network.GamePackets
{
    public class OnlineTraining
    {
        public const byte
        Show = 0,
        InTraining = 1,
        Review = 2,
        IncreasePoints = 3,
        ReceiveExperience = 4,
        Remove = 5;
    }
    public class Update : Writer, Interfaces.IPacket
    {
        public struct UpdateStruct
        {
            public uint Type;
            public ulong Value1;
            public ulong Value2;
        }
        public class Flags
        {
            public const ulong
           FlashingName = 1UL << 0,
           Poisoned = 1UL << 1,
           DivineShield = 1UL << 57,
           Invisible = 1UL << 2,
           Normal = 1UL << 3,
           XPList = 1UL << 4,
           Dead = 1UL << 5,
           TeamLeader = 1UL << 6,
           StarOfAccuracy = 1UL << 7,
           MagicShield = 1UL << 8,
           Stigma = 1UL << 9,
           Ghost = 1UL << 10,
           FadeAway = 1UL << 11,
           RedName = 1UL << 14,
           BlackName = 1UL << 15,
           ReflectMelee = 1UL << 17,
           Superman = 1UL << 18,
           Ball = 1UL << 19,
           Ball2 = 1UL << 20,
           Invisibility = 1UL << 22,
           Cyclone = 1UL << 23,
           Dodge = 1UL << 26,
           Fly = 1UL << 27,
           Intensify = 1UL << 28,
           CastPray = 1UL << 30,
           Praying = 1UL << 31,
           Cursed = 1UL << 32,
           HeavenBlessing = 1UL << 33,
           TopGuildLeader = 1UL << 34,
           TopDeputyLeader = 1UL << 35,
           MonthlyPKChampion = 1UL << 36,
           WeeklyPKChampion = 1UL << 37,
           TopWarrior = 1UL << 38,
           TopTrojan = 1UL << 39,
           TopArcher = 1UL << 40,
           TopWaterTaoist = 1UL << 41,
           TopFireTaoist = 1UL << 42,
           TopNinja = 1UL << 43,
           ShurikenVortex = 1UL << 46,
           FatalStrike = 1UL << 47,
           Flashy = 1UL << 48,
           Ride = 1UL << 50,
           TopSpouse = 1UL << 51,
           OrangeSparkles = 1UL << 52,
           PurpleSparkles = 1UL << 53,
           FreezeSmall = 1UL << 54,
           SpeedIncreased = 1UL << 55,
           IncMoveSpeed = 1UL << 56,
           GodlyShield = 1UL << 57,
           Stun = 1UL << 58,
           Freeze = 1UL << 59,
           ChaosCycle = 1UL << 60;

        }

        public class Flags2
        {
            public const ulong
           Top2Weekly = 1UL << 65,
           Top2Monthly = 1UL << 68,
            DragonWarriorTop = 481UL << 26,
           Congelado = 0x400000000000000,
           Fatigue = 1UL << 126,
           Top2Fire = 1UL << 71,
           Top2Water = 1UL << 74,
           Top2Ninja = 1UL << 77,
           Top2Warrior = 1UL << 80,
           Top2Trojan = 1UL << 83,
           Top2Archer = 1UL << 86,
           Contestant = 1UL << 91,
           ChainBoltActive = 1UL << 92,
           AzureShield = 1UL << 93,
           ScurvyBomb = 1UL << 96,
           TyrantAura = 1UL << 98,
           FendAura = 1UL << 100,
           MetalAura = 1UL << 102,
           WoodAura = 1UL << 104,
           WaterAura = 1UL << 106,
           FireAura = 1UL << 108,
           EffectBall = 1UL << 119,
           EarthAura = 1UL << 110,
           SoulShackle = 1UL << 111,
           TyrantAura2 = 0x600000000,
FendAura2 = 0x1800000000,
MetalAura2 = 0x6000000000,
WoodAura2 = 0x18000000000,
WaterAura2 = 0x60000000000,
FireAura2 = 0x180000000000,
EarthAura2 = 0x600000000000,
           Oblivion = 1UL << 112,
           ShieldBlock = 1UL << 113,
           TopMonk = 1UL << 114,
           Top2Monk = 1UL << 116,
           CarryingFlag = 1UL << 118,
           PoisonStar = 1UL << 119,
           CannonBarrage = 1UL << 120,
           BlackbeardsRage = 1UL << 121,
           TopPirate = 1UL << 122,
           TopPirate2 = 1UL << 124,
           DefensiveStance = 1UL << 126;
        }

        public class Flags3
        {
            public const ulong
            MagicDefender = 1UL << 128,
            PurpleBall = 1UL << 131,
            Assassin = 0x20000,
            rygh_hglx = 1UL << 174,
            rygh_syzs = 1UL << 175,
            CarryGold = 1UL << 165,
            BlueBall = 1UL << 132,
            PathOfShadow = 1UL << 145,
            BladeFlurry = 1UL << 146,
            KineticSpark = 1UL << 147,
            lianhuaran01 = 1UL << 168,
            lianhuaran02 = 1UL << 169,
            lianhuaran03 = 1UL << 170,
                lianhuaran04 = 1UL << 171,
            DragonFlow = 1UL << 148,
            SuperCyclone = 0x400000,
            ConuqerSuperYellow = 1UL << 151,
            ConuqerSuperBlue = 1UL << 152,
            ConuqerSuperUnderBlue = 1UL << 153,
            DragonWarriorTop = 1UL << 154,
            AutoHunting = 1UL << 155,
            DragonFury = 1UL << 158,
            DragonCyclone = 1UL << 159,
            DragonSwing = 1UL << 160,
            GoldBrickNormal = 1UL << 161,
            GoldBrickRefined = 1UL << 162,
            GoldBrickUnique = 1UL << 163,
            GoldBrickElite = 1UL << 164,
            GoldBrickSuper = 1UL << 165,
            MrConquer = 1UL << 166,
            MsConquerHostess = 1UL << 167,
            Flam1 = 1UL << 168,
            Flam2 = 1UL << 169,
            Flam3 = 1UL << 170,
            Flam4 = 1UL << 171,
            AuroraLotus = 1UL << 172,
            FlameLotus = 1UL << 173,
            ShieldBreak = 1UL << 176,
            WarriorEpicShield = 1UL << 55,
            DivineGuard = 1UL << 177;
        }

        public class nFlags
        {
            public const
                int Normal = 0x0,
                Poisoned = 1,
                Invisible = 2,
                XPList = 4,
                Dead = 5,
                TeamLeader = 6,
                StarOfAccuracy = 7,
                MagicShield = 8,
                Stigma = 9,
                Ghost = 10,
                FadeAway = 11,
                RedName = 14,
                BlackName = 15,
                ReflectMelee = 17,
                Superman = 18,
                Ball = 19,
                Ball2 = 20,
                Invisibility = 22,
                Cyclone = 23,
                Dodge = 26,
                Fly = 27,
                Intensify = 28,
                CastPray = 30,
                Praying = 31,
                HeavenBlessing = 33,
                TopGuildLeader = 34,
                TopDeputyLeader = 35,
                MonthlyPKChampion = 36,
                WeeklyPKChampion = 37,
                ShurikenVortex = 46,
                FatalStrike = 47,
                Flashy = 48,
                Ride = 50,
                AzureShield = 92,
                SoulShackle = 110,
                Oblivion = 111,
                Shield = 128,
                RemoveName = 129,
                PurpleBall = 131,
                BlueBall = 132,
                PathOfShadow = 145,
                AssasinExpSkill = 146,
                KineticSpark = 147;
        }



        public const byte
            Hitpoints = 0,
            DragonSwing = 75,
            FirstCreditGift = 71,
            MaxHitpoints = 1,
            Mana = 2,
            MaxMana = 3,
            InnerPowerPotency = 77,
            SoulShackle = 54,
            Fatigue = 55,
            Money = 4,
            Experience = 5,
            PKPoints = 6,
            Class = 7,
            Stamina = 8,
            WHMoney = 9,
            Atributes = 10,
            Mesh = 11,
            Level = 12,
            Spirit = 13,
            Vitality = 14,
            Strength = 15,
            Agility = 16,
            HeavensBlessing = 17,
            DoubleExpTimer = 18,
            CursedTimer = 20,
            Reborn = 22,
            StatusFlag = 25,
            HairStyle = 26,
            RaceShopPoints = 47,
            XPCircle = 27,
            LuckyTimeTimer = 28,
            ConquerPoints = 29,
            OnlineTraining = 31,
            MentorBattlePower = 36,
            ExtraBattlePower = 36,
            Merchant = 38,
            VIPLevel = 39,
            QuizPoints = 40,
            EnlightPoints = 41,
            GuildShareBP = 44,
            BoundConquerPoints = 45,
            FirsRebornClass = 51,
            SecondRebornClass = 50,
            IncreasePStrike = 59,
            IncreaseMStrike = 60,
            IncreaseImunity = 61,
            IncreaseBreack = 62,
            IncreaseAntiBreack = 63,
            IncreaseMaxHp = 64,
            IncreasePAttack = 65,
            IncreaseMAttack = 66,
            IncreaseFinalPDamage = 67,
            IncreaseFinalMDamage = 68,
            IncreaseFinalPAttack = 69,
            IncreaseFinalMAttack = 70,
            WoodResist = 60,
            WaterResist = 61,
            FireResist = 62,
            GuildBattlepower = 44,
            MetalResist = 63,
            EarthResist = 64,
            MagicDefenderIcone = 49,
            DefensiveStance = 56,
            ExtraInventory = 79,
            AvailableSlots = 80,
            Blessed = 66,
            gMagicAtk = 67,
            gAttack = 68,
            Assassin = 57,
            UnionMember = 81,
            MilitaryRank = 82,
            UnionRank = 83,
            MilitaryExploits = 84;
        byte[] Buffer;
        const byte minBufferSize = 44 + 4;
        public Update(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[minBufferSize + 8];
                Writer.Write(minBufferSize, 0, Buffer);
                Writer.Write(10017, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint self
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public uint UpdateCount
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set
            {
                byte[] buffer = new byte[minBufferSize + 8 + 20 * value];
                Buffer.CopyTo(buffer, 0);
                Writer.Write((ushort)(minBufferSize + 20 * value), 0, buffer);
                Buffer = buffer;
                Write(value, 12, Buffer);
            }
        }
        public void Append(byte type, byte value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 2));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 4, Buffer);
        }
        public void Append(byte type, ushort value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 4, Buffer);
        }

        public void AppendFull(byte type, ulong val1, ulong val2, ulong val3)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(val1, offset + 4, Buffer);
            Writer.Write(val2, offset + 12, Buffer);
            Writer.Write(val3, offset + 20, Buffer);

        }

        public static void UpdateActivenessTask(Client.GameClient client, byte Task, byte value)
        {
            byte[] Buffer = new byte[12 + 8];
            Writer.Write(12, 0, Buffer);
            Writer.Write(2820, 2, Buffer);//Length
            Writer.Write(3, 4, Buffer);//Unknown
            Writer.Write(1, 5, Buffer);//AmountTasksWillUpdate
            Writer.Write(Task, 6, Buffer);//type
            Writer.Write(0, 10, Buffer);//Completed
            Writer.Write(value, 11, Buffer);//value
            client.Send(Buffer);
        }
        public void PoPAppend(byte type, ulong val1, ulong val2)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(val1, offset + 8, Buffer);
            Writer.Write(val2, offset + 16, Buffer);
        }
        public void Append(byte type, uint value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 28)
                );
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 4, Buffer);

        }

        public void Append(byte type, double value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 28));
            Writer.Write(type, offset, Buffer);
            Writer.Write((ulong)(value * 100), offset + 4, Buffer);

        }
        public void Append3(byte type, uint value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 8, Buffer);
        }
        public void Append4(byte type, uint value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 24));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 4, Buffer);
        }

        public void Append(byte type, ulong value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 4, Buffer);
        }
        public void Append2(byte type, byte value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 12, Buffer);
        }
        public void Append2(byte type, ushort value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 12, Buffer);
        }
        public void Append2(byte type, uint value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 12, Buffer);
        }

        public void Append2(byte type, ulong value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + ((UpdateCount - 1) * 20));
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 12, Buffer);
        }

        public void Clear()
        {
            byte[] buffer = new byte[minBufferSize + 8];
            Writer.Write(minBufferSize, 0, Buffer);
            Writer.Write(10017, 2, Buffer);
            Write(UID, 8, buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            Buffer = buffer;
        }

        public List<UpdateStruct> Updates
        {
            get
            {
                List<UpdateStruct> structs = new List<UpdateStruct>();
                ushort offset = 16;
                if (UpdateCount > 0)
                {
                    for (int c = 0; c < UpdateCount; c++)
                    {
                        UpdateStruct st = new UpdateStruct();
                        st.Type = BitConverter.ToUInt32(Buffer, offset); offset += 4;
                        st.Value1 = BitConverter.ToUInt64(Buffer, offset); offset += 8;
                        st.Value2 = BitConverter.ToUInt64(Buffer, offset); offset += 8;
                        structs.Add(st);
                    }
                }
                return structs;
            }
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
        public void Append(byte type, byte value, byte second, byte second2, byte second3, byte second4, byte second5, byte second6, byte second7)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(UpdateCount * 16);//12
            Write(type, offset, Buffer);
            Writer.Write(value, offset + 4, Buffer);
            Write(second, offset + 12, Buffer);
            Write(second2, offset + 13, Buffer);
            Write(second3, offset + 14, Buffer);
            Write(second4, offset + 15, Buffer);
            Write(second5, offset + 16, Buffer);
            Write(second6, offset + 17, Buffer);
            Write(second7, offset + 18, Buffer);
        }
        public void AppendClanBPIcon(Client.GameClient client, byte value)
        {
            UID = client.Entity.UID;
            UpdateCount = 2;
            Write(42, 16, Buffer);
            if (client.Team != null)
            {
                Write(client.Team.Lider.Entity.UID, 16, Buffer);
            }
            Writer.Write(42, 44, Buffer);
            Writer.Write(value, 48, Buffer);
        }
        public void Append(ulong val1, ulong val2, ulong val3, uint val4, uint val5, uint val6, uint val7)
        {
            UpdateCount = 2;
            Write(0x19, 16, Buffer);
            Writer.Write(val1, 20, Buffer);
            Writer.Write(val2, 28, Buffer);
            Writer.Write(val3, 36, Buffer);
            Write(val4, 40 + 4, Buffer);
            Write(val5, 44 + 4, Buffer);
            Write(val6, 48 + 4, Buffer);
            Write(val7, 52 + 4, Buffer);
        }

        public void Append(byte type, uint[] value)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + (UpdateCount - 1) * 20);
            Write(type, offset, Buffer);
            for (byte x = 0; x < value.Length; x++)
                Write(value[x], (ushort)((offset + 4) + x * 4), Buffer);
        }
        public void Append(byte type, uint Flag, uint Time, uint Dmg, uint Level)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(16 + (UpdateCount - 1) * 24);
            Write(type, offset, Buffer);//12
            Write(Flag, (ushort)(offset + 4), Buffer);//16
            Write(Time, (ushort)(offset + 8), Buffer);
            Write(Dmg, (ushort)(offset + 12), Buffer);
            Write(Level, (ushort)(offset + 16), Buffer);
        }
        public enum AuraDataTypes
        {
            None = 0,
            Unknown = 1,
            Remove = 2,
            Add = 3
        }
        public enum AuraType
        {
            None = 0,
            TyrantAura = 1,
            FendAura = 2,
            MetalAura = 3,
            WoodAura = 4,
            WaterAura = 5,
            FireAura = 6,
            EarthAura = 7
        }
        public void Aura(Entity Entity, AuraDataTypes state, AuraType AuraType, Database.SpellInformation spell)
        {
            byte[] buffer = new byte[32 + 8];
            Writer.Write(32, 0, buffer);
            Writer.Write(2410, 2, buffer);
            Writer.Write((uint)Time32.Now.GetHashCode(), 4, buffer);
            Writer.Write((uint)state, 8, buffer);
            Writer.Write((uint)Entity.UID, 12, buffer);
            Writer.Write((uint)AuraType, 16, buffer);
            if (spell != null)
            {
                Writer.Write(spell.Level, 20, buffer);
                Writer.Write(30, 24, buffer);
                Writer.Write(spell.Power, 28, buffer);
            }
            if (Kernel.GamePool.ContainsKey(Entity.UID))
            {
                Kernel.GamePool[Entity.UID].Send(buffer);
            }
            Buffer = new byte[96];

            Writer.Write(96 - 8, 0, Buffer);
            Writer.Write(10017, 2, Buffer);
            Writer.Write(Entity.UID, 8, Buffer);
            Writer.Write(2, 12, Buffer);
            Writer.Write(0xffffffff, 16, Buffer);
            Writer.Write(8, 40, Buffer);
            Writer.Write(36, 44, Buffer);
            if (Kernel.GamePool.ContainsKey(Entity.UID))
            {
                Kernel.GamePool[Entity.UID].Send(Buffer);
            }

        }
    }

}