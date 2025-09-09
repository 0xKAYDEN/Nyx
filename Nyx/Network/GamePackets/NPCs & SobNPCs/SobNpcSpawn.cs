using Nyx.Server.Database;
using System;
using Nyx.Server.Game;

namespace Nyx.Server.Network.GamePackets
{
    public class SobNpcSpawn : Writer, Interfaces.IPacket, Interfaces.INpc, Interfaces.ISobNpc, Interfaces.IMapObject
    {
        private byte[] Buffer;
        public SobNpcSpawn()
        {
            Buffer = new byte[90];
            Writer.Write(82, 0, Buffer);
            Writer.Write(1109, 2, Buffer);
            ShowName = false;
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint MaxHitpoints
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint Hitpoints
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set
            {
                Write(value, 16, Buffer);
            }
        }

        public ushort X
        {
            get { return BitConverter.ToUInt16(Buffer, 20); }
            set { Writer.Write(value, 20, Buffer); }
        }
        public string effect
        {
            get;
            set;
        }
        public ushort Y
        {
            get { return BitConverter.ToUInt16(Buffer, 22); }
            set { Writer.Write(value, 22, Buffer); }
        }

        public ushort Mesh
        {
            get { return BitConverter.ToUInt16(Buffer, 24); }
            set { Writer.Write(value, 24, Buffer); }
        }

        public Nyx.Server.Game.Enums.NpcType Type
        {
            get { return (Nyx.Server.Game.Enums.NpcType)Buffer[26]; }
            set { Buffer[26] = (byte)value; }
        }

        public ushort Sort
        {
            get { return BitConverter.ToUInt16(Buffer, 28); }
            set { Writer.Write(value, 28, Buffer); }
        }

        public bool SpawnOnMinutes
        {
            get;
            set;
        }
        public byte BeforeHour
        {
            get;
            set;
        }

        public bool ShowName
        {
            get { return Buffer[40] == 1; }
            set { Buffer[40] = value == true ? (byte)1 : (byte)0; }
        }

        public string LoweredName;
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                LoweredName = value.ToLower();
                byte[] buffer = new byte[90];
                Buffer.CopyTo(buffer, 0);
                Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                Writer.WriteWithLength(value, 41, buffer);
                Buffer = buffer;
            }
        }

        public ushort MapID { get; set; }

        public Nyx.Server.Game.MapObjectType MapObjType
        {
            get
            {
                if (MaxHitpoints == 0)
                    return Nyx.Server.Game.MapObjectType.Npc;
                return Nyx.Server.Game.MapObjectType.SobNpc;
            }
        }

        public void Die(Game.Entity killer)
        {

            if (MapID == 1038)
            {
                if (UID != 810)
                {
                    if (Hitpoints != 0 || Mesh != 251 && Mesh != 281)
                    {
                        if (Mesh == 241)
                            Mesh = (ushort)(250 + Mesh % 10);
                        else
                            Mesh = (ushort)(280 + Mesh % 10);
                        Hitpoints = 0;
                        Update upd = new Update(true);
                        upd.UID = UID;
                        upd.Append(Update.Mesh, Mesh);
                        killer.Owner.SendScreen(upd, true);
                    }
                    Attack attack = new Attack(true);
                    attack.Attacker = killer.UID;
                    attack.Attacked = UID;
                    attack.AttackType = Network.GamePackets.Attack.Kill;
                    attack.X = X;
                    attack.Y = Y;
                    killer.Owner.Send(attack);
                    killer.KOCount++;
                }
            }
            else if (MapID == 8175)
            {
                if (UID != 810)
                {
                    if (Hitpoints != 0 || Mesh != 251 && Mesh != 281)
                    {
                        if (Mesh == 241)
                            Mesh = (ushort)(250 + Mesh % 10);
                        else
                            Mesh = (ushort)(280 + Mesh % 10);

                        Update upd = new Update(true);
                        upd.UID = UID;
                        upd.Append(Update.Mesh, Mesh);
                        killer.Owner.SendScreen(upd, true);
                        Hitpoints = 0;
                    }
                    Attack attack = new Attack(true);
                    attack.Attacker = killer.UID;
                    attack.Attacked = UID;
                    attack.AttackType = Network.GamePackets.Attack.Kill;
                    attack.X = X;
                    attack.Y = Y;
                    killer.Owner.Send(attack);
                    killer.KOCount++;
                }
            }

            else
            {
                Attack attack = new Attack(true);
                attack.Attacker = killer.UID;
                attack.Attacked = UID;
                attack.AttackType = Network.GamePackets.Attack.Kill;
                attack.X = X;
                attack.Y = Y;
                killer.Owner.Send(attack);
                Hitpoints = MaxHitpoints;
                Update upd = new Update(true);
                upd.UID = UID;
                upd.Append(Update.Hitpoints, MaxHitpoints);
                killer.Owner.SendScreen(upd, true);
            }
        }

        private Client.GameClient owner_null = null;
        public Client.GameClient Owner
        {
            get
            {
                return owner_null;
            }
            set
            {
                owner_null = value;
            }
        }
        public void SendSpawn(Client.GameClient client, bool checkScreen)
        {
            if (client.Screen.Add(this) || !checkScreen)
            {
                client.Send(Buffer);
                if (effect != "" && effect != null)
                {
                    client.SendScreen(new _String(true) { UID = UID, TextsCount = 22, Type = 10, Texts = { effect } }, true);
                }
            }
        }
        public void SendSpawn(Client.GameClient client)
        {
            SendSpawn(client, false);
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
            SendSpawn(client, false);
        }
    }
}
