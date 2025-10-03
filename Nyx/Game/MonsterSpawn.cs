using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Game
{
    public unsafe class MonsterSpawn
    {
        public static void Titan(GameClient client)
        {
            if (!Kernel.Maps[1020].Npcs.ContainsKey(29996))
            {
                Kernel.Titan = true;
                Kernel.Titan2 = true;
                NpcSpawn npc = new NpcSpawn();
                npc.UID = 29996;
                npc.Mesh = 7040;
                npc.X = 396;
                npc.Y = 592;
                npc.MapID = 1020;
                npc.Type = Enums.NpcType.Talker;
                Kernel.Maps[1020].Npcs.Add(29996, npc);
                npc.SendSpawn(client);
                client.Send(new Network.GamePackets.Message("Warning! Titan has appeared at Love Canyon!", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
            }
        }
        public static void Ganoderma(GameClient client)
        {
            if (!Kernel.Maps[1011].Npcs.ContainsKey(29995))
            {
                Kernel.Ganoderma = true;
                Kernel.Ganoderma2= true;
                NpcSpawn npc = new NpcSpawn();
                npc.UID = 29995;
                npc.Mesh = 7040;
                npc.X = 674;
                npc.Y = 790;
                npc.MapID = 1011;
                npc.Type = Enums.NpcType.Talker;
                Kernel.Maps[1011].Npcs.Add(29995, npc);
                npc.SendSpawn(client);
                client.Send(new Network.GamePackets.Message("Warning! Ganoderma has appeared at the forest.", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
            }
        }
        public static void AlluringWitchHisCrystals(GameClient client)
        {
            if (!Kernel.Maps[3935].Npcs.ContainsKey(29994))
            {
                Kernel.AlluringWitchHisCrystals = true;
                Kernel.AlluringWitchHisCrystals2 = true;
                NpcSpawn npc = new NpcSpawn();
                npc.UID = 29994;
                npc.Mesh = 7040;
                npc.X = 386;
                npc.Y = 385;
                npc.MapID = 3935;
                npc.Type = Enums.NpcType.Talker;
                Kernel.Maps[3935].Npcs.Add(29994, npc);
                npc.SendSpawn(client);
                client.Send(new Message("The Alluring Witch has appeared in the Deserted Village in the Realm. Hurry and go kill her to get Senior Dragon Souls!", Color.White, Message.Talk));
            }
        }
        public static void StartSnowBanshee(GameClient client)
        {
            if (!Kernel.Maps[1927].Npcs.ContainsKey(2999))
            {
                Kernel.SpawnBanshee = true;
                Kernel.SpawnBanshee2 = true;
                NpcSpawn npc = new NpcSpawn();
                npc.UID = 2999;
                npc.Mesh = 16537;
                npc.X = 540;
                npc.Y = 430;
                npc.MapID = 1927;
                npc.Type = Enums.NpcType.Talker;
                Kernel.Maps[1927].Npcs.Add(2999, npc);
                npc.SendSpawn(client);
                client.Send(new Network.GamePackets.Message("The Snow Banshee appeared in Frozen Grotto 2 (540,430)! Defeat it!", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
            }
        }
        public static void StartNemesisTyrant(GameClient client)
        {
            uint UID = 3080;
            ushort MapID = 3856;
            ushort Mesh = 30796;
            Random Random = new Random();
            int random = Random.Next(1, 5);
            if (!Kernel.Maps[MapID].Npcs.ContainsKey(UID))
            {
                if (random == 1)
                {
                    Kernel.SpawnNemesis = true;
                    Kernel.SpawnNemesis2 = true;
                    NpcSpawn npc = new NpcSpawn();
                    npc.UID = UID;
                    npc.Mesh = Mesh;
                    npc.X = 291;
                    npc.Y = 349;
                    npc.MapID = MapID;
                    npc.Type = Enums.NpcType.Talker;
                    Kernel.Maps[MapID].Npcs.Add(UID, npc);
                    npc.SendSpawn(client);
                    client.Send(new Network.GamePackets.Message("The NemesisTyrant has shown up in the Bloodshed Sea in (291, 349)", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                }
                else if (random == 2)
                {
                    Kernel.SpawnNemesis = true;
                    Kernel.SpawnNemesis2 = true;
                    NpcSpawn npc = new NpcSpawn();
                    npc.UID = UID;
                    npc.Mesh = Mesh;
                    npc.X = 164;
                    npc.Y = 270;
                    npc.MapID = MapID;
                    npc.Type = Enums.NpcType.Talker;
                    Kernel.Maps[MapID].Npcs.Add(UID, npc);
                    npc.SendSpawn(client);
                    client.Send(new Network.GamePackets.Message("The NemesisTyrant has shown up in the Bloodshed Sea in (164, 270)", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                }
                else if (random == 3)
                {
                    Kernel.SpawnNemesis = true;
                    Kernel.SpawnNemesis2 = true;
                    NpcSpawn npc = new NpcSpawn();
                    npc.UID = UID;
                    npc.Mesh = Mesh;
                    npc.X = 118;
                    npc.Y = 181;
                    npc.MapID = MapID;
                    npc.Type = Enums.NpcType.Talker;
                    Kernel.Maps[MapID].Npcs.Add(UID, npc);
                    npc.SendSpawn(client);
                    client.Send(new Network.GamePackets.Message("The NemesisTyrant has shown up in the Bloodshed Sea in (118, 181)", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                }
                else if (random == 4)
                {
                    Kernel.SpawnNemesis = true;
                    Kernel.SpawnNemesis2 = true;
                    NpcSpawn npc = new NpcSpawn();
                    npc.UID = UID;
                    npc.Mesh = Mesh;
                    npc.X = 137;
                    npc.Y = 88;
                    npc.MapID = MapID;
                    npc.Type = Enums.NpcType.Talker;
                    Kernel.Maps[MapID].Npcs.Add(UID, npc);
                    npc.SendSpawn(client);
                    client.Send(new Network.GamePackets.Message("The NemesisTyrant has shown up in the Bloodshed Sea in (137, 088)", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                }
                else if (random == 5)
                {
                    Kernel.SpawnNemesis = true;
                    Kernel.SpawnNemesis2 = true;
                    NpcSpawn npc = new NpcSpawn();
                    npc.UID = UID;
                    npc.Mesh = Mesh;
                    npc.X = 229;
                    npc.Y = 349;
                    npc.MapID = MapID;
                    npc.Type = Enums.NpcType.Talker;
                    Kernel.Maps[MapID].Npcs.Add(UID, npc);
                    npc.SendSpawn(client);
                    client.Send(new Network.GamePackets.Message("The NemesisTyrant has shown up in the Bloodshed Sea in (229, 106)!", System.Drawing.Color.WhiteSmoke, Network.GamePackets.Message.System));
                }
            }
        }
    }
}
