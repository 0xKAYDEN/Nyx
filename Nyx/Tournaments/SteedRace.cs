using Nyx.Server.Game;
using Nyx.Server.Network.GamePackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Game
{
    using Colors = System.Drawing.Color;
    using Nyx.Server.Client;
    public class UsableRacePotion
    {
        public Enums.RaceItemType Type;
        public int Count;
    }
    public class SteedRace
    {
        public uint[][] RaceSettings = new[]
{
new uint[]{ (uint)Enums.Maps.MarketRace, 88, 149, 
420, 431, 4, 
65, 174, 621,
123, 243, 60, 
214, 334, 70, 
346, 459, 100},
new uint[]{ (uint)Enums.Maps.IceRace, 175, 250, 
200, 153, 6, //255, 535*
154, 267, 621,
146, 392, 70,
283, 351, 100,
295, 079, 100},
new uint[]{ (uint)Enums.Maps.IslandRace, 60, 400, 
899, 816, 10,
96, 392, 621,
220, 234, 200,
472, 160, 200,
777, 464, 300},
new uint[]{ (uint)Enums.Maps.DungeonRace, 450, 520,
682, 484 , 10,
435, 559, 621,
471, 759, 200,
714, 598, 250,
489, 679, 20},
new uint[]{ (uint)Enums.Maps.LavaRace, 150, 350,
330, 170, 6,
101, 397, 623,
327, 553, 100,
526, 477, 200,
283, 275, 100}
};
        public static ushort MAPID = 1950;
        public static uint[] Settings;
        public static uint RaceRecord;

        private Map Map;

        private SobNpcSpawn Gate;
        private ushort GateSetX, GateSetY;
        public ushort GateX { get { return GateSetX; } }
        public ushort GateY { get { return GateSetY; } }

        private volatile int Records;
        private IDisposable Subscriber;

        public SteedRace()
        {
            Subscriber = World.Subscribe(work, 1000);
        }

        public void Create()
        {
            while (true)
            {
                int rand = Kernel.Random.Next(RaceSettings.Length);
                if (Database.MapsTable.MapInformations.ContainsKey((ushort)RaceSettings[rand][0]))
                {
                    if (!Kernel.Maps.ContainsKey((ushort)RaceSettings[rand][0]))
                        new Map((ushort)RaceSettings[rand][0], "");
                    if (Kernel.Maps.ContainsKey((ushort)RaceSettings[rand][0]))
                        Create(RaceSettings[rand][0]);
                    break;
                }
            }
        }
        public void Create(uint mapId)
        {
            int index = -1;
            for (int i = 0; i < RaceSettings.Length; i++)
            {
                if (RaceSettings[i][0] == mapId)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1) return;
            Settings = RaceSettings[index];
            MAPID = (ushort)Settings[0];
            var info = Database.MapsTable.MapInformations[MAPID];

            RaceRecord = info.RaceRecord;
            Map = Kernel.Maps[MAPID];
            foreach (var item in Map.StaticEntities.Values)
                Map.Floor[item.X, item.Y, MapObjectType.StaticEntity, null] = true;
            Map.StaticEntities.Clear();
            Map.Npcs.Clear();
            Map.Entities.Clear();

            Gate = new SobNpcSpawn();
            Gate.UID = 19501;
            Gate.MapID = MAPID;
            Gate.X = (ushort)Settings[6];
            Gate.Y = (ushort)Settings[7];
            Gate.Mesh = (ushort)Settings[8];
            Gate.ShowName = false;
            Gate.Name = " ";
            Gate.Type = Enums.NpcType.Furniture;
            GateSetX = Gate.X; GateSetY = Gate.Y;
            Map.AddNpc(Gate);//, false);

            Init();
        }

        private void Init()
        {
            GeneratePotions();
            Records = 0;
            DataSend = false;
        }

        private void GeneratePotions()
        {
            uint count = 100;
            ushort x, y;
            Tuple<ushort, ushort, int>[] limits = new[]
{
new Tuple<ushort,ushort,int>( (ushort)Settings[9], (ushort)Settings[10], (int)Settings[11] ),
new Tuple<ushort,ushort,int>( (ushort)Settings[12], (ushort)Settings[13], (int)Settings[14] ),
new Tuple<ushort,ushort,int>( (ushort)Settings[15], (ushort)Settings[16], (int)Settings[17] )
};

            while (count > 0)
            {
                x = (ushort)Kernel.Random.Next(0, Map.Floor.Bounds.Width);
                y = (ushort)Kernel.Random.Next(0, Map.Floor.Bounds.Height);
                bool valid = false;
                foreach (var range in limits)
                    valid |= (Kernel.GetDistance(x, y, range.Item1, range.Item2) < range.Item3);
                if (valid)
                {
                    if (Map.Floor[x, y, MapObjectType.StaticEntity, null] && Map.Floor[x, y, MapObjectType.Player, null])
                    {
                        bool v = true;
                        for (int i = 0; i < Game.Map.XDir.Length; i++)
                            if ((!Map.Floor[x + Game.Map.XDir[i], y + Game.Map.YDir[i], MapObjectType.Player, null] ||
                            !Map.Floor[x + Game.Map.XDir[i], y + Game.Map.YDir[i], MapObjectType.StaticEntity, null]) && v)
                                v = false;
                        if (!v) continue;

                        StaticEntity item = new StaticEntity((uint)(x * 1000 + y), x, y, MAPID);
                        item.Pick();

                        item.MapID = MAPID;
                        Map.AddStaticEntity(item);
                        count--;
                    }
                }
            }
        }
        bool DataSend = false;
        Time32 Last5Sec;
        DateTime Last5Sec2;
        public void work(int time)
        {
            DateTime now = DateTime.Now;
            if (DateTime.Now.DayOfWeek >= DayOfWeek.Monday && DateTime.Now.Hour == 14 && DateTime.Now.Minute == 59 && DateTime.Now.Second == 0 && DateTime.Now.DayOfWeek <= DayOfWeek.Friday)
            {
                Create();
                foreach (var client in Kernel.GamePool.Values)
                {
                    if (client.Entity.InJail()) continue;
                    Network.GamePackets.AutoInvite alert = new Network.GamePackets.AutoInvite
                    {
                        StrResID = 10525,
                        Countdown = 60,
                        Action = 1
                    };
                    client.Entity.StrResID = 10525;
                    client.Send(alert.Encode());
                    client.Send(new Data(true) { UID = client.Entity.UID, ID = Data.CountDown, dwParam = 60 });
                }
                foreach (var client in Kernel.GamePool.Values)
                {
                    if (client.Entity.InJail()) continue;
                    client.Send(new Network.GamePackets.Message("SteedRace has started You have 1 minute to signup go to TC HorseRaceManager!.", System.Drawing.Color.White, Network.GamePackets.Message.Center));
                }
                Last5Sec = Time32.Now.AddSeconds(60);
                Last5Sec2 = DateTime.Now.AddSeconds(60);
            }
            else if (Time32.Now >= Last5Sec && !DataSend)
            {
                SendData(Data.BeginSteedRace, uid: 1);
                OpenGate();
                DataSend = true;
            }
            else if (DateTime.Now.DayOfWeek >= DayOfWeek.Monday && DateTime.Now.DayOfWeek <= DayOfWeek.Friday && DateTime.Now.Hour == 15 && DateTime.Now.Minute == 25 && DateTime.Now.Second == 00)
            {
                End();
            }
        }
        public void Join(Client.GameClient client)
        {
            client.Send(new Data(true) { UID = client.Entity.UID, ID = Data.CountDown, dwParam = (uint)(Time32.Now.AllSeconds() - Last5Sec.AllSeconds()) });
            client.Entity.AddFlag(Update.Flags.Ride);
            client.Entity.Vigor = (ushort)(client.Entity.MaxVigor);
            Network.GamePackets.Vigor vigor = new Network.GamePackets.Vigor(true);
            vigor.Amount = (uint)(client.Entity.MaxVigor);
            vigor.Send(client.Entity.Owner);
            client.Entity.Teleport(MAPID, (ushort)Settings[1], (ushort)Settings[2]);
            client.Send(new RaceRecord()
            {
                Type = RaceRecordTypes.BestTime,
                Rank = (int)Database.MapsTable.MapInformations[MAPID].RaceRecord, // best time in milliseconds
                dwParam = 1800000 //a constant? 
            });
            client.MessageBox("You`re in the Horse Racing map now!");
            client.Send(new RacePotion(true) { PotionType = Enums.RaceItemType.Null, Amount = 1 });
            client.Send(new RacePotion(true) { PotionType = Enums.RaceItemType.Null, Amount = 0 });
            client.Potions = new UsableRacePotion[5];
        }
        private void OpenGate()
        {
            if (Gate == null)
                return;
            Gate.X = 0;
            Gate.Y = 0;
            Send(new Data(true) { UID = Gate.UID, ID = Data.RemoveEntity });
        }

        private void Send(Interfaces.IPacket packet)
        {
            foreach (var client in Kernel.GamePool.Values)
            {
                if (client.Entity.MapID != MAPID)
                    continue;
                client.Send(packet);
            }
        }
        private void SendData(ushort ID, uint value = 0, uint uid = 0)
        {
            Data data = null;
            if (uid != 0)
                data = new Data(true) { UID = uid, ID = ID, dwParam = value };

            foreach (var player in Kernel.GamePool.Values)
            {
                if (player.Entity.MapID == MAPID)
                {
                    if (uid == 0)
                    {
                        data = new Data(true) { UID = player.Entity.UID, ID = ID, dwParam = value };
                        player.Send(data);
                    }
                    else
                    {
                        player.Send(data);
                    }
                }
            }
        }

        private void Status(Client.GameClient client, int rank, int time, int award)
        {
            var packet = new RaceRecord()
            {
                Type = RaceRecordTypes.AddRecord,
                Rank = rank,
                Name = client.Entity.Name,
                Time = time,
                Prize = award
            };
            Send(packet);
        }

        private void End()
        {
            if (Gate != null)
            {
                Gate.X = GateSetX;
                Gate.Y = GateSetY;
            }
            foreach (var player in Kernel.GamePool.Values)
                if (player.Entity.MapID == MAPID)
                    Exit(player);
            Init();
        }

        public void FinishRace(Client.GameClient client)
        {
            if (Kernel.GetDistance(client.Entity.X, client.Entity.Y, (ushort)Settings[3], (ushort)Settings[4]) > 22)
            {
                return;
            }
            else
            {
                if (Records < 5)
                {
                    Records++;
                    int rank = Records;
                    if (rank == 1)
                    {
                        client.Entity.AddTopStatus((ulong)(((int)TitlePacket.Titles.GoldenRacer)), DateTime.Now.AddDays(1));
                    }
                    TimeSpan span = DateTime.Now - Last5Sec2;
                    var key = Database.MapsTable.MapInformations[MAPID];
                    if (key.RaceRecord > span.TotalMilliseconds)
                    { // new best record
                        key.RaceRecord = (uint)span.TotalMilliseconds;
                        Database.MapsTable.SaveRecord(key);
                    }
                    int award = AwardPlayer(client, (int)span.TotalMilliseconds, rank);
                    client.RacePoints += (uint)award;
                    Status(client, rank, (int)span.TotalMilliseconds, award);
                    client.Send(new RaceRecord()
                    {
                        Type = RaceRecordTypes.EndTime,
                        Rank = rank,
                        dwParam = (int)span.TotalMilliseconds,
                        dwParam2 = award,
                        Time = (int)span.TotalMilliseconds,
                        Prize = award
                    });
                }
                client.ChampionPoints += 100;
                client.Activenes.SendSinglePacket(client, Activeness.Types.HorseRacingTask, 1);
                Exit(client);
            }
        }

        private int AwardPlayer(Client.GameClient client, int time, int rank)
        {
            return Math.Max(10000, 100000 / rank - time * 2);
        }

        public void Exit(Client.GameClient client)
        {
            switch (client.Entity.PreviousMapID)
            {
                default:
                    {
                        client.Entity.Teleport(1002, 300, 278);
                        break;
                    }
                case 1000:
                    {
                        client.Entity.Teleport(1000, 500, 650);
                        break;
                    }
                case 1020:
                    {
                        client.Entity.Teleport(1020, 565, 562);
                        break;
                    }
                case 1011:
                    {
                        client.Entity.Teleport(1011, 188, 264);
                        break;
                    }
                case 1015:
                    {
                        client.Entity.Teleport(1015, 717, 571);
                        break;
                    }
            }
        }
    }
}