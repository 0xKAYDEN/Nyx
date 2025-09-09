using System;
using System.Linq;
using System.Text;
using Nyx.Server.Client;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.Features
{
    public class Flowers
    {
        public enum Effect : byte
        {
            None = 0,
            Rouse = 1,
            Lilies = 2,
            Orchids = 3,
            Tulips = 4,
            Kiss = 1,
            love = 2,
            Tins = 3,
            Jade = 4,
        }
        public enum FlowersT : byte
        {
            Rouse = 0,
            Lilies = 1,
            Orchids = 2,
            Tulips = 3,
            Kiss = 4,
            love = 5,
            Tins = 6,
            Jade = 7,
        }

        public Flowers()
        {
        }
        public Flowers(uint _UID, string name)
        {
            UID = _UID;
            Name = name;
            aFlower = GetCountFlowers();
        }
        public uint UID;
        public uint aFlower = 1;
        public uint SendDay = 0;
        public string Name = "";
        public byte GetCountFlowers()
        {
            if (Client.GameClient.GetClientFromID(UID) != null)
            {
                if (Client.GameClient.GetClientFromID(UID).Entity.VIPLevel == 1)
                    return 2;
                if (Client.GameClient.GetClientFromID(UID).Entity.VIPLevel == 2)
                    return 3;
                if (Client.GameClient.GetClientFromID(UID).Entity.VIPLevel == 3)
                    return 5;
                if (Client.GameClient.GetClientFromID(UID).Entity.VIPLevel == 4)
                    return 10;
                if (Client.GameClient.GetClientFromID(UID).Entity.VIPLevel == 5)
                    return 15;
                if (Kernel.GamePool[UID].Entity.VIPLevel == 6)
                    return 30;
                return 1;
            }
            return 1;
        }
        public int SendScreenValue(FlowersT typ, int rak)
        {
            if (rak == 0 || rak > 100)
                return 0;
            return (int)(30000002 + (uint)(100 * (byte)typ) + GetRank(rak));
        }
        public ushort GetRank(int rank)
        {
            if (rank == 1)
                return 0;
            if (rank == 2)
                return 10000;
            if (rank == 3)
                return 20000;
            if (rank > 3)
                return 30000;

            return 0;
        }
        public int BoySendScreenValue(FlowersT typ, int rak)
        {
            int ret = 0;
            if (rak == -1) return 0;
            if (rak > 100) return 0;

            ret = (int)(30000402 + (uint)(100 * (byte)typ));

            return ret;
        }

        public int RankRoses = 0;
        public int RankLilies = 0;//max 10 start with -1.
        public int RankOrchids = 0;//max 10 start with -1.
        public int RankTuilps = 0;//max 10 start with -1.

        public uint RedRoses;//kiss
        public uint RedRoses2day;
        public uint Lilies;//love
        public uint Lilies2day;
        public uint Orchads;//wine
        public uint Orchads2day;
        public uint Tulips;//jade
        public uint Tulips2day;

        public override string ToString()
        {
            return UID + "#"
                + Name + "#"
                + RedRoses + "#"
                + RedRoses2day + "#"
                + Lilies + "#"
                + Lilies2day + "#"
                + Orchads + "#"
                + Orchads2day + "#"
                + Tulips + "#"
                + Tulips2day + "#"
                + SendDay + "#"
                + aFlower + "#"
                + 0 + "#"
                + 0 + "#"
                + 0 + "#"
                + 0 + "#"
                + 0 + "#";
        }
        public void Read(string aLine)
        {
            if (aLine == "" || aLine == null) return;
            string[] line = aLine.Split('#');
            UID = uint.Parse(line[0]);
            Name = line[1];
            RedRoses = uint.Parse(line[2]);
            RedRoses2day = uint.Parse(line[3]);
            Lilies = uint.Parse(line[4]);
            Lilies2day = uint.Parse(line[5]);
            Orchads = uint.Parse(line[6]);
            Orchads2day = uint.Parse(line[7]);
            Tulips = uint.Parse(line[8]);
            Tulips2day = uint.Parse(line[9]);
            SendDay = uint.Parse(line[10]);
            aFlower = uint.Parse(line[11]);
            Reset();
        }
        public void Reset()
        {
            if (SendDay != DateTime.Now.Day)
            {
                RedRoses2day = Lilies2day = Orchads2day = Tulips2day = 0;
                aFlower = GetCountFlowers();
                SendDay = (uint)DateTime.Now.Day;
            }
        }

        public static System.Collections.Concurrent.ConcurrentDictionary<uint, Flowers> Flowers_Poll = new System.Collections.Concurrent.ConcurrentDictionary<uint, Flowers>();
        public static System.Collections.Concurrent.ConcurrentDictionary<uint, Flowers> BoyFlowers = new System.Collections.Concurrent.ConcurrentDictionary<uint, Flowers>();

        public static Flowers[] KissTop100 = new Flowers[0];
        public static Flowers[] LoveTop100 = new Flowers[0];
        public static Flowers[] TineTop100 = new Flowers[0];
        public static Flowers[] JadeTop100 = new Flowers[0];

        public static Flowers[] RedRousesTop100 = new Flowers[0];
        public static Flowers[] LiliesTop100 = new Flowers[0];
        public static Flowers[] OrchidsTop100 = new Flowers[0];
        public static Flowers[] TulipsTop100 = new Flowers[0];

        public static object RouseLock = new object();
        public static object LilisLock = new object();
        public static object OrchidsLock = new object();
        public static object TulipsLock = new object();

        public static object KissLock = new object();
        public static object LoveLock = new object();
        public static object TineLock = new object();
        public static object JadeLock = new object();

        public static List<Flowers> RankKiss = new List<Flowers>();
        public static List<Flowers> RankLove = new List<Flowers>();
        public static List<Flowers> RankTine = new List<Flowers>();
        public static List<Flowers> RankJade = new List<Flowers>();

        public static List<Flowers> Rankrose = new List<Flowers>();
        public static List<Flowers> Ranklili = new List<Flowers>();
        public static List<Flowers> Rankorchid = new List<Flowers>();
        public static List<Flowers> RankTulips = new List<Flowers>();

        public static void CulculateRankJade(Flowers afflow)
        {
            lock (JadeLock)
            {
                try
                {
                    if (!RankJade.Contains(afflow))
                        RankJade.Add(afflow);
                    var data = RankJade.ToArray();

                    Array.Sort(data, (c1, c2) => { return c2.Tulips.CompareTo(c1.Tulips); });

                    var room = data.ToArray();

                    List<Flowers> backUpd = new List<Flowers>();

                    int x = 1;
                    foreach (Flowers flow in room)
                    {
                        if (flow.Tulips == 0) continue;
                        if (x < 100)
                        {

                            flow.RankTuilps = x;
                            backUpd.Add(flow);
                        }
                        else
                            flow.RankTuilps = 0;
                        x++;
                    }
                    lock (JadeTop100)
                    {
                        RankJade = new List<Flowers>(backUpd);
                        JadeTop100 = backUpd.ToArray();
                    }
                }
                catch (Exception) {  }
            }
        }
        public static void CulculateRankTine(Flowers afflow)
        {
            lock (TineLock)
            {
                try
                {
                    if (!RankTine.Contains(afflow))
                        RankTine.Add(afflow);
                    var data = RankTine.ToArray();

                    Array.Sort(data, (c1, c2) => { return c2.Orchads.CompareTo(c1.Orchads); });

                    var room = data.ToArray();

                    List<Flowers> backUpd = new List<Flowers>();

                    int x = 1;
                    foreach (Flowers flow in room)
                    {
                        if (flow.Orchads == 0) continue;
                        if (x < 100)
                        {

                            flow.RankOrchids = x;
                            backUpd.Add(flow);
                        }
                        else
                            flow.RankOrchids = 0;
                        x++;
                    }
                    lock (TineTop100)
                    {
                        RankTine = new List<Flowers>(backUpd);
                        TineTop100 = backUpd.ToArray();
                    }
                }
                catch (Exception) {  }
            }
        }
        public static void CulculateRankLove(Flowers afflow)
        {
            lock (LoveLock)
            {
                try
                {
                    if (!RankLove.Contains(afflow))
                        RankLove.Add(afflow);
                    var data = RankLove.ToArray();

                    Array.Sort(data, (c1, c2) => { return c2.Lilies.CompareTo(c1.Lilies); });

                    var room = data.ToArray();

                    List<Flowers> backUpd = new List<Flowers>();

                    int x = 1;
                    foreach (Flowers flow in room)
                    {
                        if (flow.Lilies == 0) continue;
                        if (x < 100)
                        {
                            flow.RankLilies = x;
                            backUpd.Add(flow);
                        }
                        else
                            flow.RankLilies = 0;
                        x++;
                    }
                    lock (LoveTop100)
                    {
                        RankLove = new List<Flowers>(backUpd);
                        LoveTop100 = backUpd.ToArray();
                    }
                }
                catch (Exception) {  }
            }
        }
        public static void CulculateRankKiss(Flowers afflow)
        {
            lock (KissLock)
            {
                try
                {
                    if (!RankKiss.Contains(afflow))
                        RankKiss.Add(afflow);
                    var data = RankKiss.ToArray();

                    Array.Sort(data, (c1, c2) => { return c2.RedRoses.CompareTo(c1.RedRoses); });

                    var room = data.ToArray();

                    List<Flowers> backUpd = new List<Flowers>();

                    int x = 1;
                    foreach (Flowers flow in room)
                    {
                        if (flow.RedRoses == 0) continue;
                        if (x < 100)
                        {

                            flow.RankRoses = x;
                            backUpd.Add(flow);
                        }
                        else
                            flow.RankRoses = 0;
                        x++;
                    }
                    lock (KissTop100)
                    {
                        RankKiss = new List<Flowers>(backUpd);
                        KissTop100 = backUpd.ToArray();
                    }
                }
                catch (Exception) {  }
            }
        }

        public static void CulculateRankRouse(Flowers afflow)
        {
            lock (RouseLock)
            {
                try
                {
                    if (!Rankrose.Contains(afflow))
                        Rankrose.Add(afflow);
                    var data = Rankrose.ToArray();

                    Array.Sort(data, (c1, c2) => { return c2.RedRoses.CompareTo(c1.RedRoses); });

                    var room = data.ToArray();

                    List<Flowers> backUpd = new List<Flowers>();

                    int x = 1;
                    foreach (Flowers flow in room)
                    {
                        if (flow.RedRoses == 0) continue;
                        if (x < 100)
                        {
                            flow.RankRoses = x;
                            backUpd.Add(flow);
                        }
                        else
                        {
                            flow.RankRoses = 0;
                        }

                        x++;
                    }
                    lock (RedRousesTop100)
                    {
                        Rankrose = new List<Flowers>(backUpd);
                        RedRousesTop100 = backUpd.ToArray();
                    }
                }
                catch (Exception) {  }
            }
        }
        public static void CulculateRankLilies(Flowers afflow)
        {
            lock (LilisLock)
            {
                if (!Ranklili.Contains(afflow))
                    Ranklili.Add(afflow);
                var data = Ranklili.ToArray();

                Array.Sort(data, (c1, c2) => { return c2.Lilies.CompareTo(c1.Lilies); });

                //IEnumerable<Flowers> query = data.OrderBy(redrous => redrous.Tulips);

                //var room = query.Encode();
                var room = data.ToArray();
                List<Flowers> backUpd = new List<Flowers>();

                int x = 1;
                foreach (Flowers flow in room)
                {
                    if (flow.Lilies == 0) continue;
                    if (x < 100)
                    {

                        flow.RankLilies = x;
                        backUpd.Add(flow);
                    }
                    else
                    {
                        flow.RankLilies = 0;
                    }

                    x++;
                }
                lock (LiliesTop100)
                {
                    Ranklili = new List<Flowers>(backUpd);
                    LiliesTop100 = backUpd.ToArray();
                }
            }
        }
        public static void CulculateRankOrchids(Flowers afflow)
        {
            lock (OrchidsLock)
            {
                if (!Rankorchid.Contains(afflow))
                    Rankorchid.Add(afflow);
                var data = Rankorchid.ToArray();

                Array.Sort(data, (c1, c2) => { return c2.Orchads.CompareTo(c1.Orchads); });

                //IEnumerable<Flowers> query = data.OrderBy(redrous => redrous.Tulips);

                //var room = query.Encode();
                var room = data.ToArray();

                List<Flowers> backUpd = new List<Flowers>();

                int x = 1;
                foreach (Flowers flow in room)
                {
                    if (flow.Orchads == 0) continue;
                    if (x < 100)
                    {
                        flow.RankOrchids = x;
                        backUpd.Add(flow);
                    }
                    else
                    {
                        flow.RankOrchids = 0;

                    }

                    x++;
                }
                lock (OrchidsTop100)
                {
                    Rankorchid = new List<Flowers>(backUpd);
                    OrchidsTop100 = backUpd.ToArray();
                }
            }
        }
        public static void CulculateRankTulips(Flowers afflow)
        {
            lock (TulipsLock)
            {
                if (!RankTulips.Contains(afflow))
                    RankTulips.Add(afflow);
                var data = RankTulips.ToArray();

                Array.Sort(data, (c1, c2) => { return c2.Tulips.CompareTo(c1.Tulips); });

                var room = data.ToArray();

                List<Flowers> backUpd = new List<Flowers>();

                int x = 1;
                foreach (Flowers flow in room)
                {
                    if (flow.Tulips == 0) continue;
                    if (x < 100)
                    {
                        flow.RankTuilps = x;
                        backUpd.Add(flow);
                    }
                    else
                    {
                        flow.RankTuilps = 0;
                    }

                    x++;
                }
                lock (TulipsTop100)
                {
                    RankTulips = new List<Flowers>(backUpd);
                    TulipsTop100 = backUpd.ToArray();
                }
            }
        }

        public static void FlowersPacket(byte[] packet, GameClient client)
        {
            GenericRanking ranking = new GenericRanking(true);
            ranking.Deserialize(packet);
            switch (packet[4])
            {
                case 6://Prestige
                    {
                        var index = new byte[10] { 0, 15, 25, 45, 55, 65, 75, 85, 135, 145 };
                        Game.Entity Best = null;
                        Game.Entity Trojan = null;
                        Game.Entity Warrior = null;
                        Game.Entity Archer = null;
                        Game.Entity Ninja = null;
                        Game.Entity Monk = null;
                        Game.Entity Pirate = null;
                        Game.Entity Dragon = null;
                        Game.Entity Water = null;
                        Game.Entity Fire = null;
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0)
                                Best = MsgUserAbilityScore.RankingList.OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 1)
                                Trojan = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 2)
                                Warrior = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 3)
                                Archer = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 4)
                                Ninja = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 5)
                                Monk = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 6)
                                Pirate = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 7)
                                Dragon = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 8)
                                Water = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                            if (i == 9)
                                Fire = MsgUserAbilityScore.RankingList.Where(x => x.Class >= (index[i] - 5) && x.Class <= index[i]).OrderByDescending(x => x.TotalPerfectionScore).ThenByDescending(x => x.Level).ThenBy(x => x.UID).FirstOrDefault();
                        }
                        Entity[] array = new Entity[10] { Best, Trojan, Warrior, Archer, Ninja, Monk, Pirate, Dragon, Water, Fire };
                        byte count = 0;
                        if (Best != null) count++;
                        if (Trojan != null)count++;
                        if (Warrior != null) count++;
                        if (Fire != null) count++;
                        if (Water != null) count++;
                        if (Ninja != null) count++;
                        if (Monk != null) count++;
                        if (Pirate != null) count++;
                        if (Dragon != null) count++;
                        if (Archer != null) count++;
                        var nRanking = new GenericRanking(true);
                        nRanking.Mode = 6;
                        nRanking.Page = ranking.Page;
                         nRanking.Count = count;
                       for (byte z = 0; z < array.Length; z++)
                       {
                           var Entity = array[z];
                           if (Entity == null) continue;
                           nRanking.Append2((uint)(z + 1), Entity.TotalPerfectionScore, Entity.UID, Entity.Name, Entity.Level, Entity.Class, Entity.Mesh);
                       }
                        client.Send(nRanking);
                        break;
                    }
                case GenericRanking.Ranking:
                    {
                        if (ranking.RankingType == GenericRanking.InnerPower)
                        {
                            ushort page = ranking.Page;
                            InnerPower[] list = null;
                            Func<InnerPower, uint> select = null;
                            list = InnerPower.InnerPowerRank.GetRankingList();
                            select = (inner) => { return inner.TotalScore; };
                            if (list.Length < page * 10) return;
                            uint count = (uint)Math.Min(list.Length - page * 10, 10);
                            var nRanking = new Network.GamePackets.GenericRanking(true);
                            nRanking.Mode = Network.GamePackets.GenericRanking.Ranking;
                            nRanking.RankingType = ranking.RankingType;
                            nRanking.Page = page;
                            nRanking.RegisteredCount = (ushort)list.Length;
                            int rank = (int)page * 10;
                            for (int i = rank; i < rank + count; i++)
                            {
                                var current = list[i];
                                nRanking.Append((uint)(i + 1), (uint)select(current), current.UID, current.Name);
                            }
                            client.Send(nRanking);
                        }
                        else if (ranking.RankingType > 80000000 && ranking.RankingType <= 80000009)
                        {
                            byte realyclass = 0;
                            if (ranking.RankingType % 10 == 1) realyclass = 15;
                            if (ranking.RankingType % 10 == 2) realyclass = 25;
                            if (ranking.RankingType % 10 == 3) realyclass = 45;
                            if (ranking.RankingType % 10 == 4) realyclass = 55;
                            if (ranking.RankingType % 10 == 5) realyclass = 65;
                            if (ranking.RankingType % 10 == 6) realyclass = 75;
                            if (ranking.RankingType % 10 == 7) realyclass = 85;
                            if (ranking.RankingType % 10 == 8) realyclass = 135;
                            if (ranking.RankingType % 10 == 9) realyclass = 145;
                            var array = MsgUserAbilityScore.RankingList.Where(i => i.Class >= (realyclass - 5) && i.Class <= realyclass).OrderByDescending(i => i.TotalPerfectionScore).ThenByDescending(i => i.Level).ThenBy(i => i.UID).ToArray();

                            var nRanking = new GenericRanking(true);
                            nRanking.RegisteredCount = (ushort)Math.Min(array.Length, 100);
                            nRanking.Mode = 1;
                            nRanking.RankingType = ranking.RankingType;
                            nRanking.Page = ranking.Page;
                          //  nRanking.Count = (ushort)Math.Min(array.Length - (ranking.Page * 10), 10);
                            uint sss = (ushort)Math.Min(array.Length - (ranking.Page * 10), 10);

                            int rank = (int)nRanking.Page * 10;

                            for (int i = rank; i < rank + sss; i++)
                            {
                                var current = array[i]; if (current == null) break;
                                nRanking.Append2((uint)(i + 1), current.TotalPerfectionScore, current.UID, current.Name, current.Level, current.Class, current.Mesh);
                            }
                            client.Send(nRanking);
                        }
                        else if (ranking.RankingType == 80000000)
                        {
                            var array = MsgUserAbilityScore.RankingList.OrderByDescending(i => i.TotalPerfectionScore).ThenByDescending(i => i.Level).ThenBy(i => i.UID).ToArray();

                            var nRanking = new GenericRanking(true);
                            nRanking.RegisteredCount = (ushort)Math.Min(array.Length, 100);
                            nRanking.Mode = 1;
                            nRanking.RankingType = ranking.RankingType;
                            nRanking.Page = ranking.Page;
                            //  nRanking.Count = (ushort)Math.Min(array.Length - (ranking.Page * 10), 10);
                            uint sss = (ushort)Math.Min(array.Length - (ranking.Page * 10), 10);

                            int rank = (int)nRanking.Page * 10;

                            for (int i = rank; i < rank + sss; i++)
                            {
                                var current = array[i]; if (current == null) break;
                                nRanking.Append2((uint)(i + 1), current.TotalPerfectionScore, current.UID, current.Name, current.Level, current.Class, current.Mesh);
                            }
                            client.Send(nRanking);
                        }
                        else if (ranking.RankingType >= GenericRanking.DragonChi && ranking.RankingType <= GenericRanking.TurtleChi)
                            Network.PacketHandler.ShowGenericRanking(ranking, client);
                        else if (ranking.RankingType >= GenericRanking.RoseFairy && ranking.RankingType <= GenericRanking.TulipFairy)
                            Network.PacketHandler.ShowGenericRanking2(ranking, client);
                        else if (ranking.RankingType >= GenericRanking.Kiss && ranking.RankingType <= GenericRanking.Jade)
                            Network.PacketHandler.ShowGenericRanking2(ranking, client);
                        break;
                    }
                case GenericRanking.QueryCount:
                    {
                        if (ranking.RankingType == 80000000)
                        {
                            var array = MsgUserAbilityScore.RankingList.OrderByDescending(i => i.TotalPerfectionScore).ThenByDescending(i => i.Level).ThenBy(i => i.UID).ToArray();
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (array[i].UID == client.Entity.UID)
                                {
                                    Network.GamePackets.GenericRanking PacketRank = new Network.GamePackets.GenericRanking(true);
                                    PacketRank.Mode = Network.GamePackets.GenericRanking.QueryCount;
                                    PacketRank.RankingType = ranking.RankingType;
                                    PacketRank.Append2((uint)(i + 1), array[i].TotalPerfectionScore, array[i].UID, array[i].Name, array[i].Level, array[i].Class, array[i].Mesh);
                                    client.Send(PacketRank.Encode());
                                    break;
                                }
                            }
                            break;
                        }
                        if (ranking.RankingType >= 80000000 && ranking.RankingType <= 80000009)
                        {
                            byte realyclass = 0;
                            if (ranking.RankingType % 10 == 1) realyclass = 15;
                            if (ranking.RankingType % 10 == 2) realyclass = 25;
                            if (ranking.RankingType % 10 == 3) realyclass = 45;
                            if (ranking.RankingType % 10 == 4) realyclass = 55;
                            if (ranking.RankingType % 10 == 5) realyclass = 65;
                            if (ranking.RankingType % 10 == 6) realyclass = 75;
                            if (ranking.RankingType % 10 == 7) realyclass = 85;
                            if (ranking.RankingType % 10 == 8) realyclass = 135;
                            if (ranking.RankingType % 10 == 9) realyclass = 145;
                            var array = MsgUserAbilityScore.RankingList.Where(i => i.Class >= (realyclass - 5) && i.Class <= realyclass).OrderByDescending(i => i.TotalPerfectionScore).ThenByDescending(i => i.Level).ThenBy(i => i.UID).ToArray();
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (array[i].UID == client.Entity.UID)
                                {
                                    Network.GamePackets.GenericRanking PacketRank = new Network.GamePackets.GenericRanking(true);
                                    PacketRank.Mode = Network.GamePackets.GenericRanking.QueryCount;
                                    PacketRank.RankingType = ranking.RankingType;
                                    PacketRank.Append2((uint)(i + 1), array[i].TotalPerfectionScore, array[i].UID, array[i].Name, array[i].Level, array[i].Class, array[i].Mesh);
                                    client.Send(PacketRank.Encode());
                                    break;
                                }

                            }
                            break;
                        }
                        if (ranking.RankingType == 70000000)
                        {
                            if (client.Entity.InnerPower != null)
                            {
                                GenericRanking genericRanking3 = new GenericRanking(true);
                                genericRanking3.Mode = 2;
                                genericRanking3.RankingType = ranking.RankingType;
                                genericRanking3.Count = 1;
                                genericRanking3.Append((uint)client.Entity.InnerPower.Rank, client.Entity.InnerPower.TotalScore, client.Entity.UID, client.Entity.Name);
                                client.Send(genericRanking3.Encode());
                            }
                            return;
                        }
                        if (Flowers.IsGirl((uint)client.Entity.Body))
                        {
                            if (client.Entity.MyFlowers != null)
                            {
                                int num61 = -1;
                                uint num62 = Flowers.CreateMyRank(client, out num61);
                                packet[4] = 5;
                                client.Send(packet);
                                client.Entity.FlowerRank = (uint)client.Entity.MyFlowers.SendScreenValue((Game.Features.Flowers.FlowersT)num62, num61);
                                GenericRanking genericRanking2 = new GenericRanking(true);
                                genericRanking2.Mode = 2u;
                                genericRanking2.RankingType = client.Entity.FlowerRank;
                                genericRanking2.Count = 1u;
                                int rank = num61;
                                if (client.Entity.MyFlowers.RankRoses < 100 && client.Entity.MyFlowers.RankRoses > 0)
                                {
                                    genericRanking2.RankingType = GenericRanking.RoseFairy;
                                    genericRanking2.Append((uint)client.Entity.MyFlowers.RankRoses, client.Entity.MyFlowers.RedRoses, client.Entity.UID, client.Entity.Name);
                                    client.Send(genericRanking2.Encode());
                                }
                                if (client.Entity.MyFlowers.RankLilies < 100 && client.Entity.MyFlowers.RankLilies > 0)
                                {
                                    genericRanking2.Reset();
                                    genericRanking2.RankingType = GenericRanking.LilyFairy;
                                    genericRanking2.Append((uint)client.Entity.MyFlowers.RankLilies, client.Entity.MyFlowers.Lilies, client.Entity.UID, client.Entity.Name);
                                    client.Send(genericRanking2.Encode());
                                }
                                if (client.Entity.MyFlowers.RankOrchids < 100 && client.Entity.MyFlowers.RankOrchids > 0)
                                {
                                    genericRanking2.Reset();
                                    genericRanking2.RankingType = GenericRanking.OrchidFairy;
                                    genericRanking2.Append((uint)client.Entity.MyFlowers.RankOrchids, client.Entity.MyFlowers.Orchads, client.Entity.UID, client.Entity.Name);
                                    client.Send(genericRanking2.Encode());
                                }
                                if (client.Entity.MyFlowers.RankTuilps < 100 && client.Entity.MyFlowers.RankTuilps > 0)
                                {
                                    genericRanking2.Reset();
                                    genericRanking2.RankingType = GenericRanking.TulipFairy;
                                    genericRanking2.Append((uint)client.Entity.MyFlowers.RankTuilps, client.Entity.MyFlowers.Tulips, client.Entity.UID, client.Entity.Name);
                                    client.Send(genericRanking2.Encode());
                                }
                                packet[4] = 5;
                                client.Send(packet);
                                break;
                            }
                            break;
                        }
                        else
                        {
                            if (client.Entity.MyFlowers != null)
                            {
                                GenericRanking genericRanking3 = new GenericRanking(true);
                                genericRanking3.Mode = 2u;
                                genericRanking3.RankingType = 30000402u;
                                genericRanking3.Count = 1u;
                                if (client.Entity.MyFlowers.RankRoses < 100 && client.Entity.MyFlowers.RankRoses > 0)
                                {
                                    genericRanking3.Append((uint)client.Entity.MyFlowers.RankRoses, client.Entity.MyFlowers.RedRoses, client.Entity.UID, client.Entity.Name);
                                    client.Send(genericRanking3.Encode());
                                }
                                if (client.Entity.MyFlowers.RankLilies < 100 && client.Entity.MyFlowers.RankLilies > 0)
                                {
                                    genericRanking3.Reset();
                                    genericRanking3.Append((uint)client.Entity.MyFlowers.RankLilies, client.Entity.MyFlowers.Lilies, client.Entity.UID, client.Entity.Name);
                                    genericRanking3.RankingType = 30000502u;
                                    client.Send(genericRanking3.Encode());
                                }
                                if (client.Entity.MyFlowers.RankOrchids < 100 && client.Entity.MyFlowers.RankOrchids > 0)
                                {
                                    genericRanking3.Reset();
                                    genericRanking3.Append((uint)client.Entity.MyFlowers.RankOrchids, client.Entity.MyFlowers.Orchads, client.Entity.UID, client.Entity.Name);
                                    genericRanking3.RankingType = 30000602u;
                                    client.Send(genericRanking3.Encode());
                                }
                                if (client.Entity.MyFlowers.RankTuilps < 100 && client.Entity.MyFlowers.RankTuilps > 0)
                                {
                                    genericRanking3.Reset();
                                    genericRanking3.Append((uint)client.Entity.MyFlowers.RankTuilps, client.Entity.MyFlowers.Tulips, client.Entity.UID, client.Entity.Name);
                                    genericRanking3.RankingType = 30000702u;
                                    client.Send(genericRanking3.Encode());
                                }
                                packet[4] = 5;
                                client.Send(packet);
                                break;
                            }
                        }
                        break;
                    }
            }
        }
        public static void SendFlower(Client.GameClient client, byte[] packet)
        {
            byte typ1 = packet[4];
            uint Target = BitConverter.ToUInt32(packet, 8);
            uint ITEM_UID = BitConverter.ToUInt32(packet, 12);
            if (IsBoy(client.Entity.Body) && typ1 == 0)//boy send
            {
                switch (ITEM_UID)
                {
                    case 0://send my flower
                        {
                            if (client.Entity.MyFlowers.aFlower == 0) break;
                            Client.GameClient target_client;
                            if (Kernel.GamePool.TryGetValue(Target, out target_client))
                            {
                                if (!IsGirl(target_client.Entity.Body)) return;
                                client.Activenes.SendSinglePacket(client, Activeness.Types.FlowerTask, 1);
                                client.Entity.MyFlowers.aFlower -= 1;
                                client.Entity.MyFlowers.SendDay = (uint)DateTime.Now.Day;
                                target_client.Entity.MyFlowers.RedRoses2day += 1;
                                target_client.Entity.MyFlowers.RedRoses += 1;
                                SendFlower flow = new SendFlower();
                                flow.Typing = 0;
                                flow.Effect = (byte)Game.Features.Flowers.Effect.Tulips;
                                flow.Amount = 1;
                                flow.SenderName = client.Entity.Name;
                                flow.ReceiverName = target_client.Entity.Name;
                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Rouse;
                                client.SendScreen(flow.Encode(), true);
                            }
                            break;
                        }
                    default:
                        {

                            Network.GamePackets.ConquerItem Item = null;
                            if (client.Inventory.TryGetItem(ITEM_UID, out Item))
                            {
                                Client.GameClient target_client;
                                if (Kernel.GamePool.TryGetValue(Target, out target_client))
                                {
                                    if (!IsGirl(target_client.Entity.Body)) return;
                                    uint Amount = Item.ID % 1000;
                                    SendFlower flow = new SendFlower();
                                    flow.Typing = typ1;
                                    flow.Amount = Amount;
                                    flow.SenderName = client.Entity.Name;
                                    flow.ReceiverName = target_client.Entity.Name;
                                    switch (GetFlowerTyp(Item.ID))
                                    {
                                        case (byte)Game.Features.Flowers.FlowersT.Rouse:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Rouse;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Rouse;
                                                target_client.Entity.MyFlowers.RedRoses2day += Amount;
                                                target_client.Entity.MyFlowers.RedRoses += Amount;
                                                if (Game.Features.Flowers.RedRousesTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.RedRousesTop100[98].RedRoses <= target_client.Entity.MyFlowers.RedRoses)
                                                    {
                                                        Game.Features.Flowers.CulculateRankRouse(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankRouse(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                        case (byte)Game.Features.Flowers.FlowersT.Lilies:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Lilies;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Lilies;
                                                target_client.Entity.MyFlowers.Lilies2day += Amount;
                                                target_client.Entity.MyFlowers.Lilies += Amount;
                                                if (Game.Features.Flowers.LiliesTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.LiliesTop100[98].Lilies <= target_client.Entity.MyFlowers.Lilies)
                                                    {
                                                        Game.Features.Flowers.CulculateRankLilies(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankLilies(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                        case (byte)Game.Features.Flowers.FlowersT.Orchids:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Orchids;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Orchids;

                                                target_client.Entity.MyFlowers.Orchads2day += Amount;
                                                target_client.Entity.MyFlowers.Orchads += Amount;
                                                if (Game.Features.Flowers.OrchidsTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.OrchidsTop100[98].Orchads <= target_client.Entity.MyFlowers.Orchads)
                                                    {
                                                        Game.Features.Flowers.CulculateRankOrchids(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankOrchids(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                        case (byte)Game.Features.Flowers.FlowersT.Tulips:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Tulips;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Tulips;

                                                target_client.Entity.MyFlowers.Tulips2day += Amount;
                                                target_client.Entity.MyFlowers.Tulips += Amount;
                                                if (Game.Features.Flowers.TulipsTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.TulipsTop100[98].Tulips <= target_client.Entity.MyFlowers.Tulips)
                                                    {
                                                        Game.Features.Flowers.CulculateRankTulips(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankTulips(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                    }
                                    client.Inventory.Remove(Item, Enums.ItemUse.Remove);
                                    client.SendScreen(flow.Encode(), true);
                                }
                            }
                            break;
                        }
                }
            }
            else if (IsGirl(client.Entity.Body) && typ1 == 1)//girl send 
            {
                switch (ITEM_UID)
                {
                    case 0://curent flower
                        {
                            if (client.Entity.MyFlowers.aFlower == 0)
                                return;
                            Client.GameClient target_client;
                            if (Kernel.GamePool.TryGetValue(Target, out target_client))
                            {
                                if (!IsBoy(target_client.Entity.Body)) return;
                                client.Entity.MyFlowers.aFlower -= 1;
                                client.Entity.MyFlowers.SendDay = (uint)DateTime.Now.Day;
                                target_client.Entity.MyFlowers.RedRoses += 1;
                                target_client.Entity.MyFlowers.RedRoses2day += 1;
                                SendFlower flow = new SendFlower();
                                flow.Typing = 1;
                                flow.Effect = (byte)Game.Features.Flowers.Effect.Kiss;
                                flow.Amount = 1;
                                flow.SenderName = client.Entity.Name;
                                flow.ReceiverName = target_client.Entity.Name;
                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Kiss;
                                client.SendScreen(flow.Encode(), true);
                            }
                            break;
                        }
                    default:
                        {
                            ConquerItem Item = null;
                            if (client.Inventory.TryGetItem(ITEM_UID, out Item))
                            {
                                Client.GameClient target_client = null;
                                if (Kernel.GamePool.TryGetValue(Target, out target_client))
                                {
                                    if (!IsBoy(target_client.Entity.Body)) return;
                                    uint Amount = Item.ID % 1000;
                                    SendFlower flow = new SendFlower();
                                    flow.Typing = 1;
                                    flow.Amount = Amount;
                                    flow.SenderName = client.Entity.Name;
                                    flow.ReceiverName = target_client.Entity.Name;
                                    switch (GetFlowerTyp(Item.ID))
                                    {
                                        case (byte)Game.Features.Flowers.FlowersT.Rouse:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Kiss;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Kiss;

                                                target_client.Entity.MyFlowers.RedRoses2day += Amount;
                                                target_client.Entity.MyFlowers.RedRoses += Amount;
                                                if (Game.Features.Flowers.KissTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.KissTop100[98].RedRoses <= target_client.Entity.MyFlowers.RedRoses)
                                                    {
                                                        Game.Features.Flowers.CulculateRankKiss(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankKiss(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                        case (byte)Game.Features.Flowers.FlowersT.Lilies:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.love;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.love;

                                                target_client.Entity.MyFlowers.Lilies2day += Amount;
                                                target_client.Entity.MyFlowers.Lilies += Amount;
                                                if (Game.Features.Flowers.LoveTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.LoveTop100[98].Lilies <= target_client.Entity.MyFlowers.Lilies)
                                                    {
                                                        Game.Features.Flowers.CulculateRankLove(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankLove(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                        case (byte)Game.Features.Flowers.FlowersT.Orchids:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Tins;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Tins;

                                                target_client.Entity.MyFlowers.Orchads2day += Amount;
                                                target_client.Entity.MyFlowers.Orchads += Amount;
                                                if (Game.Features.Flowers.TineTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.TineTop100[98].Orchads <= target_client.Entity.MyFlowers.Orchads)
                                                    {
                                                        Game.Features.Flowers.CulculateRankTine(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankTine(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                        case (byte)Game.Features.Flowers.FlowersT.Tulips:
                                            {
                                                flow.Effect = (byte)Game.Features.Flowers.Effect.Jade;
                                                flow.FType = (byte)Game.Features.Flowers.FlowersT.Jade;

                                                target_client.Entity.MyFlowers.Tulips2day += Amount;
                                                target_client.Entity.MyFlowers.Tulips += Amount;
                                                if (Game.Features.Flowers.JadeTop100.Length > 98)
                                                {
                                                    if (Game.Features.Flowers.JadeTop100[98].Tulips <= target_client.Entity.MyFlowers.Tulips)
                                                    {
                                                        Game.Features.Flowers.CulculateRankJade(target_client.Entity.MyFlowers);
                                                    }
                                                }
                                                else Game.Features.Flowers.CulculateRankJade(target_client.Entity.MyFlowers);
                                                break;
                                            }
                                    }
                                    client.Inventory.Remove(Item, Enums.ItemUse.Remove);
                                    client.SendScreen(flow.Encode(), true);
                                }
                            }
                            break;
                        }
                }
            }
        }
        public static uint CreateMyRank(Client.GameClient client, out int rank)
        {
            List<ClientRank> FRanks = new List<ClientRank>()
            {
           new ClientRank(){Rank = (uint)client.Entity.MyFlowers.RankLilies, Amount = client.Entity.MyFlowers.Lilies}
           ,   new ClientRank(){Rank = (uint)client.Entity.MyFlowers.RankOrchids, Amount = client.Entity.MyFlowers.Orchads}
           ,   new ClientRank(){Rank = (uint)client.Entity.MyFlowers.RankRoses, Amount = client.Entity.MyFlowers.RedRoses}
           ,   new ClientRank(){Rank = (uint)client.Entity.MyFlowers.RankTuilps, Amount = client.Entity.MyFlowers.Tulips}
            };
            var array = FRanks.Where((f1) => f1.Rank != 0).ToArray();
            Array.Sort(array, (f1, f2) =>
            {
                int n_rank = f1.Rank.CompareTo(f2.Rank);

                if (f2.Rank == f1.Rank)
                    return f2.Amount.CompareTo(f1.Amount);
                return n_rank;
            });
            if (array != null && array.Length > 0)
            {
                ClientRank BestRank = array[0];
                if (BestRank.Rank != 0)
                {
                    rank = (int)BestRank.Rank;
                    if (client.Entity.MyFlowers.RankLilies == BestRank.Rank && client.Entity.MyFlowers.Lilies == BestRank.Amount)
                        return (byte)Game.Features.Flowers.FlowersT.Lilies;
                    if (client.Entity.MyFlowers.RankOrchids == BestRank.Rank && client.Entity.MyFlowers.Orchads == BestRank.Amount)
                        return (byte)Game.Features.Flowers.FlowersT.Orchids;
                    if (client.Entity.MyFlowers.RankRoses == BestRank.Rank && client.Entity.MyFlowers.RedRoses == BestRank.Amount)
                        return (byte)Game.Features.Flowers.FlowersT.Rouse;
                    if (client.Entity.MyFlowers.RankTuilps == BestRank.Rank && client.Entity.MyFlowers.Tulips == BestRank.Amount)
                        return (byte)Game.Features.Flowers.FlowersT.Tulips;
                }
            }
            rank = 0;
            return 0;
        }
        public static uint GetFlowerTyp(uint ID)
        {
            if (ID >= 751001 && ID <= 751999 || ID >= 755001 && ID <= 755999)
                return (uint)Game.Features.Flowers.FlowersT.Rouse;
            if (ID >= 752001 && ID <= 752999 || ID >= 756001 && ID <= 756999)
                return (uint)Game.Features.Flowers.FlowersT.Lilies;
            if (ID >= 753001 && ID <= 753999 || ID >= 757001 && ID <= 757999)
                return (uint)Game.Features.Flowers.FlowersT.Orchids;
            if (ID >= 754001 && ID <= 754999 || ID >= 758001 && ID <= 758999)
                return (uint)Game.Features.Flowers.FlowersT.Tulips;
            return 0;
        }
        public static bool IsBoy(uint mesh)
        {
            return mesh == 1003 || mesh == 1004;
        }
        public static bool IsGirl(uint mesh)
        {
            return mesh == 2001 || mesh == 2002;
        }
        public class ClientRank
        {
            public uint Rank;
            public uint Amount;
        }
    }
}
namespace Nyx.Server.Network.GamePackets
{

    public class FairySpawn : Writer, Interfaces.IPacket
    {
        private byte[] buf;
        public FairySpawn(bool create)
        {
            if (create)
            {
                buf = new byte[28];
                Writer.Write(20, 0, buf);
                Writer.Write(2070, 2, buf);
            }
        }
        public uint SType
        {
            get { return BitConverter.ToUInt32(buf, 4); }
            set { Write(value, 4, buf); }
        }
        public uint FairyType
        {
            get { return BitConverter.ToUInt32(buf, 12); }
            set { Write(value, 12, buf); }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(buf, 16); }
            set { Write(value, 16, buf); }
        }
        public byte[] Encode()
        {
            return buf;
        }
        public void Deserialize(byte[] buffer)
        {
            buf = buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(buf);
        }
        public static void Process(byte[] packet, Client.GameClient client)
        {
            FairySpawn FS = new FairySpawn(false);
            FS.Deserialize(packet);
            switch (FS.SType)
            {
                case 1:
                    {
                        if (client.IsFairy == false)
                        {
                            client.IsFairy = true;
                            client.FairyType = FS.FairyType;
                            client.SType = FS.SType;
                            FS.UID = client.Entity.UID;
                            client.SendScreen(FS, true);
                        }
                        else client.Send(new Message("Cancel The Other Transformer First", Message.System));
                        break;
                    }
                case 2:
                    {
                        if (client.IsFairy == false) return;
                        client.IsFairy = false;
                        client.FairyType = 0;
                        client.SType = 0;
                        FS.UID = client.Entity.UID;
                        client.SendScreen(FS, true);
                        break;
                    }
                default:  break;
            }
        }
    }
    public class SendFlower : Writer
    {
        public const uint FlowerSender = 2;
        public const uint Flower = 3;
        private byte[] Packet;
        public SendFlower()
        {
            Packet = new byte[68];
            Writer.Write(60, 0, Packet);
            Writer.Write(1150, 2, Packet);
        }
        public void Apprend(Game.Features.Flowers flowers)
        {
            Writer.Write(flowers.RedRoses, 16, Packet);
            Writer.Write(flowers.RedRoses2day, 20, Packet);
            Writer.Write(flowers.Lilies, 24, Packet);
            Writer.Write(flowers.Lilies2day, 28, Packet);
            Writer.Write(flowers.Orchads, 32, Packet);
            Writer.Write(flowers.Orchads2day, 36, Packet);
            Writer.Write(flowers.Tulips, 40, Packet);
            Writer.Write(flowers.Tulips2day, 44, Packet);
        }
        public uint Typing
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set { Writer.Write(value, 4, Packet); }
        }
        public string SenderName
        {
            get { return Encoding.ASCII.GetString(Packet, 16, 16); }
            set { Writer.Write(value, 16, Packet); }
        }
        public string ReceiverName
        {
            get { return Encoding.ASCII.GetString(Packet, 32, 16); }
            set { Writer.Write(value, 32, Packet); }
        }
        public uint Amount
        {
            get { return BitConverter.ToUInt32(Packet, 48); }
            set { Writer.Write(value, 48, Packet); }
        }
        public uint FType
        {
            get { return BitConverter.ToUInt32(Packet, 52); }
            set { Writer.Write(value, 52, Packet); }
        }
        public uint Effect
        {
            get { return BitConverter.ToUInt32(Packet, 56); }
            set { Writer.Write(value, 56, Packet); }
        }
        public byte[] Encode()
        {
            return Packet;
        }
    }
}