using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;
using System.Collections;
using System.Windows;

using Nyx.Server.Client;

namespace Nyx.Server.Game
{
    public class StaticEntity : Writer, Interfaces.IMapObject
    {
        public const uint FlagMesh = 513;

        public object SyncRoot = new object();
        public byte[] SpawnPacket;
        private uint mesh, uid;
        private ushort x, y;
        private ushort level;
        private string name;

        public bool Viable { get; set; }
        public Time32 NotViableStamp;
        public uint SetBy = 0;

        public StaticEntity(uint uid, ushort x, ushort y,ushort map)
        {
            this.uid = uid;
            this.x = x;
            this.y = y;
            this.MapID = map;

            SpawnPacket = new byte[8 + Entity._Names + 40];
            Writer.Write((ushort)(Entity._Names + 40), 0, SpawnPacket);
            Writer.Write(10014, 2, SpawnPacket);
            Write(uid, Entity._UID, SpawnPacket);
         Writer.Write(Update.Flags.Dead | Update.Flags.Ghost, Entity._StatusFlag, SpawnPacket);
            Writer.Write(0, Entity._Hitpoints, SpawnPacket);
            Writer.Write(1, Entity._RaceItem, SpawnPacket);
            Writer.Write(0, Entity._Facing, SpawnPacket);
            Writer.Write(x, Entity._X, SpawnPacket);
            Writer.Write(y, Entity._Y, SpawnPacket);
        }
        public string Name { get { return name; } set { name = value; } }
        public ushort Level { get { return level; } set { level = value; } }
        public ushort X { get { return x; } set { x = value; } }
        public ushort Y { get { return y; } set { y = value; } }
        public ushort MapID { get; set; }
        public uint UID { get { return uid; } set { uid = value; } }
        public GameClient Owner { get { return null; } }
        public MapObjectType MapObjType { get { return MapObjectType.StaticEntity; } }

        public void SendSpawn(Client.GameClient client)
        {
            SendSpawn(client, true);
        }

        public void SendSpawn(Client.GameClient client, bool checkScreen)
        {
            if (client.Screen.Add(this) || !checkScreen)
                client.Send(SpawnPacket);
        }
        public bool QuestionMark { get { return mesh == 767; } }
        public Enums.RaceItemType Type
        {
            get
            {
                switch (mesh)
                {
                    case 760: return Enums.RaceItemType.GuardPotion;
                    case 761: return Enums.RaceItemType.FrozenTrap;
                    case 762: return Enums.RaceItemType.SluggishPotion;
                    case 763: return Enums.RaceItemType.DizzyHammer;
                    case 764: return Enums.RaceItemType.RestorePotion;
                    case 765: return Enums.RaceItemType.ScreamBomb;
                    case 766: return Enums.RaceItemType.TransformItem;
                    case 768: return Enums.RaceItemType.SpiritPotion;
                    case 769: return Enums.RaceItemType.ExcitementPotion;
                    case 770: return Enums.RaceItemType.ChaosBomb;
                    default:
                        {

                            int val = (int)Enums.RaceItemType.TransformItem;
                            while (val == (int)Enums.RaceItemType.TransformItem || val == (int)Enums.RaceItemType.FrozenTrap)
                                val = Kernel.Random.Next((int)Enums.RaceItemType.ChaosBomb, (int)Enums.RaceItemType.SuperExcitementPotion);
                            return (Enums.RaceItemType)val;
                        }
                }
            }
        }

        private static Tuple<uint, string, ushort>[] raceItems = new[]
            {
                new  Tuple<uint, string, ushort>(760,"GuardPotion",1),
//                new  Tuple<uint, string, ushort>(761,"FrozenTrap",1),
                new  Tuple<uint, string, ushort>(762,"SluggishPotion",3),
                new  Tuple<uint, string, ushort>(763,"DizzyHammer",1),
                new  Tuple<uint, string, ushort>(764,"RestorePotion",3 ),
                new  Tuple<uint, string, ushort>(765,"ScreamBomb",1 ),
                new  Tuple<uint, string, ushort>(766,"SuperExclamationMark",1),
                new  Tuple<uint, string, ushort>(767,"SuperQuestionMark",1),
                new  Tuple<uint, string, ushort>(768,"SpiritPotion",1),
                new  Tuple<uint, string, ushort>(769,"ExcitementPotion",1),
                new  Tuple<uint, string, ushort>(770,"ChaosBomb",1)
            };


        public void Pick()
        {
            Viable = true;

            var itemData = raceItems[Kernel.Random.Next(raceItems.Length)];    
            this.mesh = itemData.Item1;
            this.name = itemData.Item2;
            this.level = itemData.Item3;

            Write(mesh, Entity._Mesh, SpawnPacket);
            Writer.Write(level, Entity._MonsterLevel, SpawnPacket);
            Writer.Write(new List<string>() { name, string.Empty }, Entity._Names, SpawnPacket);
        }
        public void DoFlag()
        {
            Viable = true;
            this.mesh = FlagMesh;
            this.name = "Flag";
            this.level = 1;
            
            Write(mesh, Entity._Mesh, SpawnPacket);
            Writer.Write(level, Entity._MonsterLevel, SpawnPacket);
            Writer.Write(new List<string>() { name, string.Empty }, Entity._Names, SpawnPacket);
        }
        public void DoFrozenTrap(uint setter)
        {
            SetBy = setter;
            Viable = true;

            this.mesh = 761;
            this.name = "Frozen Trap";
            this.level = 1;

            Write(mesh, Entity._Mesh, SpawnPacket);
            Writer.Write(level, Entity._MonsterLevel, SpawnPacket);
            Writer.Write(new List<string>() { name, string.Empty }, Entity._Names, SpawnPacket);
        }

        public void SendScreen(Interfaces.IPacket packet)
        {
            var Values = Kernel.GamePool.Values;
            foreach (var pClient in Values)
            {
                if (pClient == null) continue;
                if (!pClient.Socket.Alive) continue;
                if (pClient.Entity.MapID != MapID) continue;
                if (Kernel.GetDistance(pClient.Entity.X, pClient.Entity.Y, X, Y) > Constants.pScreenDistance) continue;
                pClient.Send(packet);
            }
        }
    }
}