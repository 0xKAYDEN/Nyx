using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Nyx.Server.Database;
using System.Collections.Concurrent;
using Nyx.Server.Game;
using Nyx.Server.Client;

using System.IO;
using System.Linq;

namespace Nyx.Server
{
    public static class Kernel
    {
        #region 7bit
        public static byte[] FinalizeProtoBuf(object proto, ushort packetid)
        {
            byte[] protobuff;
            using (var ms = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, proto);
                protobuff = ms.ToArray();
                byte[] buffer;
                buffer = new byte[12 + protobuff.Length];
                System.Buffer.BlockCopy(protobuff, 0, buffer, 4, protobuff.Length);
                Network.SafeWriter.Write(buffer.Length - 8, 0, buffer);
                Network.SafeWriter.Write(packetid, 2, buffer);
               /* if (packetid == 3257)
                {
                    var packet2 = new byte[buffer.Length - 4];
                    Array.Copy(buffer, 4, packet2, 0, packet2.Length);
                    var variable = Kernel.Read7BitEncodedInt(packet2).SelectMany(BitConverter.GetBytes).Encode();
                    Network.PacketHandler.PrintPacket(variable);
                }*/
                return buffer;
            }
        }
        public static byte[] CreateProtocolBuffer(params uint[] values)
        {
            List<byte> ptr = new List<byte>();
            ptr.Add(8);
            for (int x = 0; x < values.Length; x++)
            {
                uint value = values[x];
                while (value > 0x7F)
                {
                    ptr.Add((byte)((value & 0x7F) | 0x80));
                    value >>= 7;
                }
                ptr.Add((byte)(value & 0x7F));
                ptr.Add((byte)(8 * (x + 2)));
                if (x + 1 == values.Length)
                    break;
            }
            return ptr.ToArray();
        }
        public static uint[] Read7BitEncodedInt(byte[] buffer)
        {
            List<uint> ptr2 = new List<uint>();

            for (int i = 0; i < buffer.Length; )
            {
                if (i + 2 <= buffer.Length)
                {
                    int tmp = buffer[i++];

                    if (tmp % 8 == 0)
                        while (true)
                        {
                            if (i + 1 > buffer.Length) break;
                            tmp = buffer[i++];
                            if (tmp < 128)
                            {
                                ptr2.Add((uint)tmp);
                                break;
                            }
                            else
                            {
                                int result = tmp & 0x7f;
                                if ((tmp = buffer[i++]) < 128)
                                {
                                    result |= tmp << 7;
                                    ptr2.Add((uint)result);
                                    break;
                                }
                                else
                                {
                                    result |= (tmp & 0x7f) << 7;
                                    if ((tmp = buffer[i++]) < 128)
                                    {
                                        result |= tmp << 14;
                                        ptr2.Add((uint)result);
                                        break;
                                    }
                                    else
                                    {
                                        result |= (tmp & 0x7f) << 14;
                                        if ((tmp = buffer[i++]) < 128)
                                        {
                                            result |= tmp << 21;
                                            ptr2.Add((uint)result);
                                            break;
                                        }
                                        else
                                        {
                                            result |= (tmp & 0x7f) << 21;
                                            result |= (tmp = buffer[i++]) << 28;
                                            ptr2.Add((uint)result);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                }
                else break;
            }
            return ptr2.ToArray();
        }
        #endregion
        public static bool
        AlluringWitchHisCrystals = false,
        AlluringWitchHisCrystals2 = false,
        Ganoderma = false,
        Ganoderma2 = false,
        Titan = false,
        Titan2 = false,
        Spawn1 = false,
        SpawnNemesis = false,
        SpawnBanshee = false,
        SpawnNemesis2 = false,
        SpawnBanshee2 = false,
        SpawnBansheeSoul = false,
        SpawnBansheeSoulKeep = false;
        public static Time32 SpawnBansheeSoulStamp;
        public static bool PlunderWar = false;
        public static uint ServerKingdom = 0;
        public static ConcurrentDictionary<uint, Network.GamePackets.ConquerItem> AuctionItems = new ConcurrentDictionary<uint, Network.GamePackets.ConquerItem>();
        public static Game.ConquerStructures.QuizShow QuizShow = new Game.ConquerStructures.QuizShow();
        public static ConcurrentDictionary<uint, Game.Entity> BlackSpoted = new ConcurrentDictionary<uint, Game.Entity>();
        public static Dictionary<uint, Clan> Clans = new Dictionary<uint, Clan>();
        public static Dictionary<uint, Game.Features.Reincarnation.ReincarnateInfo> ReincarnatedCharacters = new Dictionary<uint, Game.Features.Reincarnation.ReincarnateInfo>();
        public static ConcurrentDictionary<uint, Database.AccountTable> AwaitingPool = new ConcurrentDictionary<uint, Database.AccountTable>();
        public static ConcurrentDictionary<uint, Client.GameClient> GamePool = new ConcurrentDictionary<uint, Client.GameClient>();
        public static ConcurrentDictionary<uint, Client.GameClient> DisconnectPool = new ConcurrentDictionary<uint, Client.GameClient>();
        public static SafeDictionary<uint, Game.ConquerStructures.Society.Guild> Guilds_adv = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Guild>();
        public static SafeDictionary<ushort, Game.Map> Maps = new SafeDictionary<ushort, Game.Map>();
        public static SafeDictionary<uint, Game.ConquerStructures.Society.Guild> Guilds = new SafeDictionary<uint, Nyx.Server.Game.ConquerStructures.Society.Guild>();
        public static Dictionary<uint, Nyx.Server.Network.GamePackets.Union.UnionClass> Unions = new Dictionary<uint, Nyx.Server.Network.GamePackets.Union.UnionClass>();
        public static SafeDictionary<uint, string> Furnitures = new SafeDictionary<uint, string>();
        public static List<char> InvalidCharacters = new List<char>() { ' ', '[', ']', '#', '\\', '/', '"', '=', '', };
        public static List<string> Insults = new List<string>() { "k o s", "Dick", "head", "mother", "fucker", "Kick", "ass", "Fuck ur self", "5od yad", "abok", "Dick", "cock", "M3rsen", "pussy", "son of bitch", "kos", "omk", " k o s", "mtnak", "sharmot", "5owl", "5awl", "zanya", "3rs", "hanekak", "Dana hanekak", "Den", "Sharmota", "Kosomen omak", "Kosomen", "Mayten", "a7a", "a7eh", "fuck", "a 7 a", "a7 a" };
        public static FastRandom Random = new FastRandom();
        public static int boundID = 45;
        public static int boundIDEnd = 46;
        public static bool SendUpdates = false, ClanCityApply = false, Attackable = false, canpk = false, TreasureInTheBlue = false;
        public static Dictionary<UInt32, Refinery.RefineryBoxes> DatabaseRefineryBoxes =
                                                    new Dictionary<UInt32, Refinery.RefineryBoxes>();
        public static Dictionary<UInt32, Refinery.RefineryItem> DatabaseRefinery =
                                                    new Dictionary<UInt32, Refinery.RefineryItem>();

        public static SafeDictionary<uint, Client.GameClient> WasInGamePool = new SafeDictionary<uint, Client.GameClient>(1000000);
        public static uint MaxLilies1 = 999, MaxOrchads1 = 500, MaxRoses1 = 5000, MaxTulips1 = 50, MaxRoses = 100, MaxLilies = 999, MaxOrchads = 500, MaxTulips = 50;
        private static Int64 RandSeed = 3721;
        [DllImport("winmm", EntryPoint = "timeGetTime", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        private static extern long timeGetTime();
        public static int ClansOnMap(GameClient[] clients)
        {
            List<uint> list = new List<uint>();
            for (int i = 0; i < clients.Length; i++)
            {
                GameClient GameClient = clients[i];
                if (GameClient.Entity.ClanId != 0u && !list.Contains(GameClient.Entity.ClanId))
                {
                    list.Add(GameClient.Entity.ClanId);
                }
            }
            return list.Count;
        }
        public static string TimeStamp()
        {
            return "[" + DateTime.Now.AddMilliseconds((Time32.Now - Time32.Now).GetHashCode()).ToString("hh:mm:ss") + "]";
        }
        public static int RandGet(int nMax, bool bReset = false)
        {
            if (bReset)
                RandSeed = timeGetTime();

            Int64 x = 0xffffffff;
            double i;
            ulong final;

            RandSeed *= ((Int64)134775813);
            RandSeed += 1;
            RandSeed = RandSeed % x;
            i = ((double)RandSeed) / (double)0xffffffff;
            final = (ulong)(nMax * i);

            return (int)final;
        }
        public static ulong Gettime()
        {
            ulong Year = (ulong)DateTime.Now.Year;
            Year = Year - 1900;
            ulong Month = (ulong)DateTime.Now.Month;
            Month = Month - 1;
            ulong DayOfYear = (ulong)DateTime.Now.DayOfYear;
            ulong Day = (ulong)DateTime.Now.Day;
            ulong Hour = (ulong)DateTime.Now.Hour;
            ulong Minute = (ulong)DateTime.Now.Minute;
            ulong Second = (ulong)DateTime.Now.Second;
            Second += 8;
            ulong value = Year;
            value = value * 100 + Month;
            value = value * 1000 + DayOfYear;
            value = value * 100 + Day;
            value = value * 100 + Hour;
            value = value * 100 + Minute;
            value = value * 100 + Second;
            return value;
        }
        public static short GetDistance(ushort X, ushort Y, ushort X2, ushort Y2)
        {
            return (short)Math.Sqrt((X - X2) * (X - X2) + (Y - Y2) * (Y - Y2));
        }
        public static short GetAngle2(ushort X, ushort Y, ushort x2, ushort y2)
        {
            double r = Math.Atan2(y2 - Y, x2 - X);
            if (r < 0)
                r += Math.PI * 2;
            return (short)Math.Round(r * 180 / Math.PI);
        }
        public static double GetDDistance(ushort X, ushort Y, ushort X2, ushort Y2)
        {
            return Math.Sqrt((X - X2) * (X - X2) + (Y - Y2) * (Y - Y2));
        }
        public static int GetDegree(int X, int X2, int Y, int Y2)
        {
            int direction = 0;

            double AddX = X2 - X;
            double AddY = Y2 - Y;
            double r = (double)Math.Atan2(AddY, AddX);
            if (r < 0) r += (double)Math.PI * 2;

            direction = (int)(360 - (r * 180 / Math.PI));

            return direction;
        }
        public static UInt32 ToDateTimeInt2(DateTime dt)
        {
            return UInt32.Parse(dt.ToString("yyyyMMddHH"));
        }
        public static UInt64 ToDateTimeInt(DateTime dt)
        {
            return UInt64.Parse(dt.ToString("yyyyMMddHHmmss"));
        }
        public static DateTime FromDateTimeInt(UInt64 val)
        {
            return new DateTime(
                (Int32)(val / 10000000000),
                (Int32)((val % 10000000000) / 100000000),
                (Int32)((val % 100000000) / 1000000),
                (Int32)((val % 1000000) / 10000),
                (Int32)((val % 10000) / 100),
                (Int32)(val % 100));
        }
        public static DateTime FromDateTimeInt(Int64 val)
        {
            return new DateTime(
                (Int32)(val / 10000000000),
                (Int32)((val % 10000000000) / 100000000),
                (Int32)((val % 100000000) / 1000000),
                (Int32)((val % 1000000) / 10000),
                (Int32)((val % 10000) / 100),
                (Int32)(val % 100));
        }
        public static UInt64 RecordTime(DateTime dt)
        {
            return UInt64.Parse(dt.ToString("yyyyMMdd"));
        }
        public static Boolean ValidClanName(String name)
        {
            lock (Clans)
            {
                foreach (Clan clans in Clans.Values)
                {
                    if (clans.Name == name)
                        return false;
                }
            }
            return true;
        }
        public static Game.Enums.ConquerAngle GetAngle(ushort X, ushort Y, ushort X2, ushort Y2)
        {
            double direction = 0;

            double AddX = X2 - X;
            double AddY = Y2 - Y;
            double r = (double)Math.Atan2(AddY, AddX);

            if (r < 0) r += (double)Math.PI * 2;

            direction = 360 - (r * 180 / (double)Math.PI);

            byte Dir = (byte)((7 - (Math.Floor(direction) / 45 % 8)) - 1 % 8);
            return (Game.Enums.ConquerAngle)(byte)((int)Dir % 8);
        }
        #region World
        public static void SendWorldMessage(Interfaces.IPacket packet)
        {
            foreach (Client.GameClient client in Program.Values)
            {
                if (client != null)
                {
                    client.Send(packet);
                }
            }
        }
        public static void SendWorldMessage(Interfaces.IPacket message, Client.GameClient[] to)
        {
            foreach (Client.GameClient client in to)
            {
                if (client != null)
                {
                    client.Send(message);
                }
            }
        }
        public static void Execute(Action<Client.GameClient> action)
        {
            foreach (Client.GameClient client in Program.Values)
            {
                if (client != null)
                {
                    action(client);
                }
            }
        }
        public static void SendWorldMessage(Interfaces.IPacket message, Client.GameClient[] to, uint exceptuid)
        {
            foreach (Client.GameClient client in to)
            {
                if (client != null)
                {
                    if (client.Entity.UID != exceptuid)
                    {
                        client.Send(message);
                    }
                }
            }
        }

        public static void SendWorldMessage(Interfaces.IPacket message, Client.GameClient[] to, ushort mapid)
        {
            foreach (Client.GameClient client in to)
            {
                if (client != null)
                {
                    if (client.Map.ID == mapid)
                    {
                        client.Send(message);
                    }
                }
            }
        }

        public static void SendWorldMessage(Interfaces.IPacket message, Client.GameClient[] to, ushort mapid, uint exceptuid)
        {
            foreach (Client.GameClient client in to)
            {
                if (client != null)
                {
                    if (client.Map.ID == mapid)
                    {
                        if (client.Entity.UID != exceptuid)
                        {
                            client.Send(message);
                        }
                    }
                }
            }
        }
        #endregion
        public static void SendScreen(Interfaces.IMapObject obj, Interfaces.IPacket packet)
        {
            var Values = Kernel.GamePool.Values;
            foreach (var pClient in Values)
            {
                if (pClient == null) continue;
                if (!pClient.Socket.Alive) continue;
                if (pClient.Entity.MapID != obj.MapID) continue;
                if (Kernel.GetDistance(pClient.Entity.X, pClient.Entity.Y, obj.X, obj.Y) > Constants.pScreenDistance) continue;
                pClient.Send(packet);
            }
        }
        public static uint maxJumpTime(short distance)
        {
            uint x = 0;
            x = 400 * (uint)distance / 10;
            return x;
        }
        public static bool Rate(int value)
        {
            return value > Random.Next() % 100;
        }
        public static bool Rate(double percent)
        {
            if (percent == 0) return false;
            while ((int)percent > 0) percent /= 10f;
            int discriminant = 1;
            percent = Math.Round(percent, 4);
            while (percent != Math.Ceiling(percent))
            {
                percent *= 10;
                discriminant *= 10;
                percent = Math.Round(percent, 4);
            }
            return Kernel.Rate((int)percent, discriminant);
        }
        public static bool Rate(int value, int discriminant)
        {
            return value > Random.Next() % discriminant;
        }
        public static bool Rate(ulong value)
        {
            return Rate((int)value);
        }
        public static int RandFromGivingNums(params int[] nums)
        {
            return nums[Random.Next(0, nums.Length)];
        }
        public static bool Rate2(int value)
        {
            return value > Kernel.RandGet(100, false);
        }
        internal static void SendSpawn(Game.StaticEntity item)
        {
            foreach (Client.GameClient client in Kernel.GamePool.Values)
                if (client != null)
                    if (client.Map.ID == item.MapID)
                        if (GetDistance(item.X, item.Y, client.Entity.X, client.Entity.Y) <= Constants.pScreenDistance)
                            item.SendSpawn(client);
        }
        public static bool ChanceSuccess(int percent)
        {
            if (percent == 0)
                return false;

            return (Random.Next(0, 100) < percent);
        }
        public static Enums.ConquerAngle GetFacing(short angle)
        {
            sbyte c_angle = (sbyte)((angle / 46) - 1);
            return (c_angle == -1) ? Enums.ConquerAngle.South : (Enums.ConquerAngle)c_angle;
        }
        public static short GetAnglex(ushort X, ushort Y, ushort x2, ushort y2)
        {
            double r = Math.Atan2(y2 - Y, x2 - X);
            if (r < 0)
                r += Math.PI * 2;
            return (short)Math.Round(r * 180 / Math.PI);
        }
        public static bool ChanceSuccess(double Chance)
        {
            double num2 = (((double)Random.Next(0x989680)) / 10000000.0) * 100.0;
            return (Chance >= num2);
        }
    }
}
