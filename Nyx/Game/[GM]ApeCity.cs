using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Interfaces;

namespace Nyx.Server.Game
{
    class _GM_ApeCity
    {
        public static ushort X = 556, Y = 565;
        public static ushort X1 = 567, Y1 = 573;
        public static ushort X2 = 567, Y2 = 586;
        public static ushort X3 = 568, Y3 = 600;
        public static ushort X4 = 569, Y4 = 617;
        public static ushort X5 = 572, Y5 = 631;
        public static ushort X6 = 573, Y6 = 640;
        public static ushort X7 = 580, Y7 = 632;
        public static ushort X8 = 591, Y8 = 630;
        public static ushort X9 = 600, Y9 = 628;
        public static ushort X10 = 603, Y10 = 619;
        public static ushort X11 = 606, Y11 = 610;
        public static ushort X12 = 607, Y12 = 602;
        public static ushort X13 = 606, Y13 = 594;
        public static ushort X14 = 587, Y14 = 586;
        public static ushort X15 = 572, Y15 = 584;
        public static ushort X16 = 567, Y16 = 572;
        public static void Load()
        {
            if (Kernel.Maps.ContainsKey(1020))
            {
                uint ItemID = 720159;
                #region CPBag

                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                    {
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    }
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }
                }

                #endregion
            }
        }
        public static void Load2()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X1, Y = Y1;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);

                        }
                    }

                #endregion

                }
            }
        }
        public static void Load3()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X2, Y = Y2;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;


                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }
                #endregion
                }
            }
        }
        public static void Load4()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X3, Y = Y3;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }
                #endregion
                    Load5();
                }
            }
        }
        public static void Load5()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X4, Y = Y4;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }
                #endregion
                }
            }
        }
        public static void Load6()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X5, Y = Y5;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }
                #endregion
                }
            }
        }
        public static void Load7()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X6, Y = Y6;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }


                }
                #endregion
            }
        }
        public static void Load8()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X7, Y = Y16;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load9()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                ushort X = 9, Y = 9;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;
                foreach (Client.GameClient C in Kernel.GamePool.Values)
                {
                    if (C.Entity.MapID == 1020)
                    {

                        npc.SendSpawn(C);
                    }
                }

            }


        }
        public static void Load10()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X10, Y = Y10;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load11()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X11, Y = Y11;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load12()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X7, Y = Y16;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load13()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X12, Y = Y12;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load14()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X13, Y = Y13;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load15()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X14, Y = Y14;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }
        public static void Load16()
        {
            if (Kernel.Maps.ContainsKey(1002))
            {
                #region CPBag

                uint ItemID = 720159;
                ushort X = X15, Y = Y15;
                INpc npc = new Network.GamePackets.NpcSpawn();
                npc.UID = 1305;
                npc.Mesh = 13050;
                npc.Type = Enums.NpcType.Talker;
                npc.X = (ushort)(X - 1);
                npc.Y = (ushort)(Y - 1);
                npc.MapID = 1020;

                Game.Map Map = Kernel.Maps[1020];
                if (Map.SelectCoordonates(ref X, ref Y))
                {
                    Network.GamePackets.FloorItem floorItem = new Network.GamePackets.FloorItem(true);
                    floorItem.ValueType = Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
                    floorItem.Value = 100;
                    floorItem.ItemID = ItemID;
                    floorItem.MapID = 1020;
                    floorItem.MapObjType = Game.MapObjectType.Item;
                    floorItem.X = X;
                    floorItem.Y = Y;
                    floorItem.Type = Network.GamePackets.FloorItem.Drop;
                    floorItem.OnFloor = Time32.Now;
                    floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    while (Map.Npcs.ContainsKey(floorItem.UID))
                        floorItem.UID = Network.GamePackets.FloorItem.FloorUID.Next;
                    Map.AddFloorItem(floorItem);
                    foreach (Client.GameClient C in Kernel.GamePool.Values)
                    {
                        if (C.Entity.MapID == 1020)
                        {
                            C.SendScreenSpawn(floorItem, true);
                            npc.SendSpawn(C);
                        }
                    }

                #endregion
                }
            }
        }


    }

}