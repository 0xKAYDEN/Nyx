using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game
{
    public unsafe class Clone
    {
        public const ushort
          Uint32_Stamp = 4,
        Uint32_Mesh = 8,
        Uint32_UID = 12,
        Uint32_GuildId = 16,
        Uint32_GuildRank = 20,
        Uint16_Unknown = 24,
        BitVector32 = 26,
        Uint32_Apparence = 50,
        Uint32_Head = 52,
        Uint32_Garment = 56,
        Uint32_Armor = 60,
        Utin32_LeftWeapon = 64,
        Uint32_RightWeapon = 68,
        Uint32_LeftWeaponAccessory = 72,
        Uint32_RightWeaponAccessory = 76,
        Uint32_Steed = 80,
        Uint32_MountArmor = 84,
        Uint32_EpicMonk = 254 + 4,
        SteedWing = 88,
        WingPlus = 92,
        WingColor = 96,
        Uint16_HitPoints = 103 + 4,
        Uint16_X = 111 + 4,
        Uint16_Y = 113 + 4,
        Uint16_Haire = 115 + 4,
        Byte_Tester = 107 + 4,
        Byte_Fascing = 117 + 4,
        Uint32_Action = 118 + 4,
        Uint16_Action2 = 122 + 4,
        Byte_Reborn = 125 + 4,
        Byte_Level = 126 + 4,
        Byte_WindowsView = 128 + 4,
        Byte_Away = 129 + 4,
        Uint32_BonusBattlePower = 130 + 4,
        Uint32_FlowerRank = 146 + 4,
        Byte_Nobility = 150 + 4,
        Uint16_ColorArmor = 154 + 4,
        Uint16_ColorShield = 156 + 4,
        Uint16_ColorHelment = 158 + 4,
        Uint32_QuizPoints = 160 + 4,
        Uint32_SteedPlus = 164 + 4,
        Uint32_SteedColor = 170 + 4,
        Uint16_Enlighten = 174 + 4,
        Uint32_ClanUID = 186 + 4,
        Uint32_ClanRank = 190 + 4,
        Byte_Title = 198 + 4,
        Uint32_HeadSoul = 213 + 4,
        Uint32_ArmorSoul = 217 + 4,
        Uint32_LeftWeapsonSoul = 221 + 4,
        Uint32_RightWeapsonSoul = 225 + 4,
        Byte_SubClassActive = 229 + 4,
        Uint32_SubClassHash = 230 + 4,
        Uint16_FirstRebornClass = 238 + 4,
        Uint16_SecondRebornClass = 240 + 4,
        Uint16_Class = 242 + 4,
        Uint32_BattlePower = 250 + 4,
        Byte_JiangHuTalent = 254 + 4,
        Byte_JiangHuActive = 255 + 4,
        Byte_CloneCount = 263 + 4,
        Uint16_CloneModel = 264 + 4,
        Uint32_OwnerUID = 266 + 4,
        Str_Count = 284 + 12 + 4,
        Str_NameLenght = 285 + 12 + 4,
        Uint16_PLenght = 287 + 12 + 4;


        public void RemoveThat()
        {
            Data Data = new Data(true);
            Data.UID = UID;
            Data.ID = Data.RemoveEntity;
            Owner.SendScreen(Data.Encode(), true);

        }
        private byte[] Packet;
        public uint UID = 0;
        public Client.GameClient Owner;
        public static Counter CounterUID = new Counter(700100);
        private BitVector32 BitVector;
        public void AddFlag(int Flag, bool UpdateSpawn = false)
        {
            if (!BitVector.Contain((int)Flag))
            {
                BitVector.Add((int)Flag);
                UpdateSpawnPacket();
            }
        }
        public void RemoveFlag(int Flag, bool UpdateSpawn = false)
        {
            if (BitVector.Contain((int)Flag))
            {
                BitVector.Remove((int)Flag);
                UpdateSpawnPacket();
            }
        }
        public void UpdateSpawnPacket(bool UpdateSpawn = false)
        {
            if (UpdateSpawn)
            {
                fixed (byte* ptr = Packet)
                {
                    for (int x = 0; x < BitVector.bits.Length; x++)
                        *(uint*)(ptr + BitVector32 + x * 4) = BitVector.bits[x];
                }
            }
            SendUpdate(BitVector.bits, (byte)Update.StatusFlag);
        }
        public unsafe void SendUpdate(uint[] Value, int datatype)
        {
            Update Update = new Update(true);
            Update.UID = UID;
            Update.Append((byte)datatype, Value);
            Update.Send(Owner);
        }
        private void CreateArray(string Name, string CloneName)
        {
            int Size = Name.Length + Uint16_PLenght + 8 + CloneName.Length + 2;
            Packet = new byte[Size];
            fixed (byte* ptr = Packet)
            {
                *(ushort*)(ptr + 0) = (ushort)(Size - 8);
                *(ushort*)(ptr + 2) = 10014;
                *(int*)(ptr + Uint32_Stamp) = Time32.Now.Value;
                *(byte*)(ptr + Str_Count) = 4;
                *(byte*)(ptr + Str_NameLenght) = (byte)Name.Length;
                ushort offset = Str_NameLenght + 1;
                for (int x = 0; x < Name.Length; x++)
                    *(byte*)(ptr + offset + x) = (byte)Name[x];
                offset += (ushort)(Name.Length + 2);
                *(byte*)(ptr + offset) = (byte)CloneName.Length;
                offset += 1;
                for (int x = 0; x < CloneName.Length; x++)
                    *(byte*)(ptr + offset + x) = (byte)CloneName[x];
            }
        }
        public static void CreateShadowClone1(Entity client)
        {
            client.MyClones.Add(new Clone(client, "ShadowClone", 10003));
        }
        public static void CreateShadowClone2(Entity client)
        {
            client.MyClones.Add(new Clone(client, "ShadowClone", 3));
            client.MyClones.Add(new Clone(client, "ShadowClone", 10003));
        }
        public Clone(Entity role, string CloneName, int flag)
        {
            BitVector = new BitVector32(160);
            Owner = role.Owner;
            CreateArray(CloneName, role.Name);
            UID = CounterUID.Next;
            fixed (byte* ptr = Packet)
            {
                AppendEquip(role.Owner);
                *(ushort*)(ptr + Uint32_Apparence) = (ushort)role.Appearance;
                *(ushort*)(ptr + Uint16_X) = role.X;
                *(ushort*)(ptr + Uint16_Y) = role.Y;
                *(ushort*)(ptr + Uint16_Haire) = role.HairStyle;
                *(ushort*)(ptr + Byte_Fascing) = (ushort)role.Facing;
                for (int x = Byte_Reborn; x < Uint16_ColorArmor; x++)
                {
                    *(byte*)(ptr + x) = role.SpawnPacket[x];
                }
                *(int*)(ptr + Uint32_BattlePower) = role.BattlePower;
                *(uint*)(ptr + Uint32_Mesh) = role.Mesh;
                *(uint*)(ptr + Uint32_UID) = UID;
                *(uint*)(ptr + Uint16_HitPoints) = 1;
                *(ushort*)(ptr + Uint16_CloneModel) = (ushort)flag;
                *(byte*)(ptr + Byte_CloneCount) = 2;
                *(uint*)(ptr + Uint32_OwnerUID) = role.UID;

                SendView(role.Owner);
                AddFlag((int)22);

                _String str = new _String(true);
                str.UID = UID;
                str.Type = _String.Effect;
                str.Texts.Add("replaceappear");
                Owner.SendScreen(str);
            }
        }
        public void AppendEquip(Client.GameClient client)
        {
            fixed (byte* user_pointer = Owner.Entity.SpawnPacket)
            {
                fixed (byte* ptr = Packet)
                {
                    for (ushort x = Uint32_Head; x < Uint32_MountArmor + 4; x += 4)
                        *(uint*)(ptr + x) = *(uint*)(user_pointer + x);
                    for (ushort x = Uint16_ColorArmor; x < Uint16_ColorHelment + 2; x += 2)
                        *(ushort*)(ptr + x) = *(ushort*)(user_pointer + x);
                    for (ushort x = Uint32_HeadSoul; x < Uint32_RightWeapsonSoul + 4; x += 4)
                        *(uint*)(ptr + x) = *(uint*)(user_pointer + x);
                }
            }
        }
        public void SendView(Client.GameClient client)
        {
            fixed (byte* ptr = Packet)
            {
                *(ushort*)(ptr + Uint16_X) = Owner.Entity.X;
                *(ushort*)(ptr + Uint16_Y) = Owner.Entity.Y;
                client.SendScreen(Packet, true);
            }
        }
        public void Send(Client.GameClient client)
        {
            fixed (byte* ptr = Packet)
            {
                *(ushort*)(ptr + Uint16_X) = Owner.Entity.X;
                *(ushort*)(ptr + Uint16_Y) = Owner.Entity.Y;
                client.Send(Packet);
            }
        }
    }
    public unsafe class CloneAttack
    {
        public static void Process(byte[] packet, Client.GameClient client)
        {
            foreach (var clone in client.Entity.MyClones)
            {
                var attack = new Attack(true);
                attack.Attacker = clone.UID;
                attack.AttackType = Attack.Melee;
                foreach (var obj1 in client.Screen.Objects)
                {
                    if (Kernel.GetDistance(obj1.X, obj1.Y, client.Entity.X, client.Entity.Y) <= 3)
                    {
                        if (obj1.MapObjType == MapObjectType.Monster || obj1.MapObjType == MapObjectType.Player)
                        {
                            uint damage = 0;
                            var attacked = obj1 as Entity;
                            if (Game.Attacking.Handle.CanAttack(client.Entity, attacked, null, false))
                            {
                                damage = (uint)Kernel.Random.Next(500, 5000);
                                attack.Damage = damage;
                                attack.Attacked = attacked.UID;
                                attack.X = attacked.X;
                                attack.Y = attacked.Y;
                                Game.Attacking.Handle.ReceiveAttack(client.Entity, attacked, attack, ref damage, null);
                            }
                        }
                        else if (obj1.MapObjType == MapObjectType.SobNpc)
                        {
                            uint damage = 0;
                            var attacked = obj1 as SobNpcSpawn;
                            if (Game.Attacking.Handle.CanAttack(client.Entity, attacked, null))
                            {
                                damage = Game.Attacking.Calculate.Melee(client.Entity, attacked, ref attack);
                                damage /= 3;
                                attack.Damage = damage;
                                attack.Attacked = attacked.UID;
                                attack.X = attacked.X;
                                attack.Y = attacked.Y;
                                Game.Attacking.Handle.ReceiveAttack(client.Entity, attacked, attack, damage, null);
                            }
                        }
                    }
                }
            }
        }
    }

}
