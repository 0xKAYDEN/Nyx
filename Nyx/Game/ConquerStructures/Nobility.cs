using System;
using System.Linq;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;
using System.IO;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Game.ConquerStructures
{
    public class Nobility : Nyx.Server.Network.Writer
    {
        public static SafeDictionary<uint, NobilityInformation> Board = new SafeDictionary<uint, NobilityInformation>(10000);
        public static List<NobilityInformation> BoardList = new List<NobilityInformation>(10000);
        public static void Handle(NobilityInfo information, Client.GameClient client)
        {
            switch (information.Type)
            {
                case NobilityInfo.Donate:
                    {
                        if (client.Trade.InTrade)
                            return;

                        ulong silvers = information.dwParam;
                        bool newDonator = false;
                        client.NobilityInformation.Gender = (byte)(client.Entity.Body % 10);
                        if (client.NobilityInformation.Donation == 0)
                            newDonator = true;
                        if (information.MoneyType == 1)
                        {
                            ulong cps = silvers / 50000;
                            if (client.Entity.ConquerPoints >= cps)
                            {
                                client.Entity.ConquerPoints -= (uint)cps;
                                client.NobilityInformation.Donation += silvers;
                            }
                        }
                        else
                        {
                            if (client.Entity.Money >= silvers)
                            {
                                client.Entity.Money -= (uint)silvers;
                                client.NobilityInformation.Donation += silvers;
                            }
                        }

                        if (!Board.ContainsKey(client.Entity.UID) && client.NobilityInformation.Donation == silvers && newDonator)
                        {
                            Board.Add(client.Entity.UID, client.NobilityInformation);
                            try
                            {
                                Database.NobilityTable.InsertNobilityInformation(client.NobilityInformation);
                            }
                            catch
                            {
                                Database.NobilityTable.UpdateNobilityInformation(client.NobilityInformation);
                            }
                        }
                        else
                        {
                            Database.NobilityTable.UpdateNobilityInformation(client.NobilityInformation);
                        }
                        Sort(client.Entity.UID);
                        break;
                    }
                    /*   case NobilityInfo.List:
                    {
                        byte[] packet = new byte[600 + 8];
                        Write(600, 0, packet);
                        Write(2064, 2, packet);
                        Write(2, 4, packet);
                        Write(5, 10, packet);
                        Write(10, 12, packet);
                        int offset = 120;
                        for (int i = (int)(information.wParam1 * 10); i < information.wParam1 * 10 + 10 && i < BoardList.Count; i++)
                        {
                            var nob = BoardList[i];
                            Write(nob.EntityUID, offset, packet);
                            offset += 4;
                            if (Kernel.GamePool.ContainsKey(nob.EntityUID))
                            {
                                Write(1, offset, packet);
                                offset += 4;
                                Write(nob.Mesh, offset, packet);
                                offset += 4;
                            }
                            else
                            {
                                offset += 8;
                            }
                            Write(nob.Name, offset, packet);
                            offset += 20;
                            Write(nob.Donation, offset, packet);
                            offset += 8;
                            Write((uint)nob.Rank, offset, packet);
                            offset += 4;
                            Write((uint)i, offset, packet);
                            offset += 4;
                        }
                        client.Send(packet);
 
                        information.Type = NobilityInfo.NextRank;
                        ulong value = 0;
                        information.dwParam2 = 0;
                        lock (BoardList)
                        {
                            if (client.NobilityInformation.Rank == NobilityRank.Prince)
                                value = (ulong)(BoardList[02].Donation - client.NobilityInformation.Donation + 1);
                            if (client.NobilityInformation.Rank == NobilityRank.Duke)
                                value = (ulong)(BoardList[14].Donation - client.NobilityInformation.Donation + 1);
                            if (client.NobilityInformation.Rank == NobilityRank.Earl)
                                value = (ulong)(BoardList[49].Donation - client.NobilityInformation.Donation + 1);
                        }
                        Network.SafeWriter.Write(value, 8, information.Encode());
                        information.dwParam3 = 60;
                        information.dwParam4 = uint.MaxValue;
                        client.Send(information);
                        break;
                    }*/
                case NobilityInfo.List:
                    {
                        bool Again = false;
                        byte Count = 0;
                        MemoryStream strm = new MemoryStream();
                        BinaryWriter wtr = new BinaryWriter(strm);
                        wtr.Write((ushort)0);
                        wtr.Write((ushort)2064);
                        wtr.Write((uint)NobilityInfo.List);
                        wtr.Write((ushort)information.wParam1);
                        wtr.Write((ushort)5);
                        wtr.Write((uint)0);
                        wtr.Write((uint)0);
                        wtr.Write((uint)0);
                        for (int i = (int)(information.wParam1 * 10); i < information.wParam1 * 10 + 10; i++)
                        {
                            if (BoardList.Count > i)
                            {
                                Count++;
                                wtr.Write((uint)BoardList[i].EntityUID);
                                wtr.Write((uint)BoardList[i].Gender);
                                wtr.Write((uint)BoardList[i].Mesh);
                                for (int c = 0; c < 20; c++)
                                {
                                    if (BoardList[i].Name.Length > c)
                                    {
                                        wtr.Write((byte)(BoardList[i].Name[c]));
                                    }
                                    else
                                    {
                                        wtr.Write((byte)(0));
                                    }
                                }
                                wtr.Write((ulong)BoardList[i].Donation);
                                wtr.Write((uint)BoardList[i].Rank);
                                wtr.Write((uint)BoardList[i].Position);
                                if (i == 1 && Again == false || i == 11 && Again == false || i == 21 && Again == false || i == 31 && Again == false || i == 41 && Again == false)
                                {
                                    i -= 2;
                                    Count = 0;
                                    Again = true;
                                }
                            }
                        }
                        Again = false;
                        int packetlength = (int)strm.Length;
                        strm.Position = 0;
                        wtr.Write((ushort)packetlength);
                        strm.Position = strm.Length;
                        wtr.Write(System.Text.Encoding.UTF7.GetBytes("TQServer"));
                        strm.Position = 0;
                        byte[] buf = new byte[strm.Length];
                        strm.Read(buf, 0, buf.Length);
                        Network.SafeWriter.Write(Count, 12, buf);
                        client.Send(buf);
                        break;
                    }
                case NobilityInfo.NextRank:
                    {
                        NobilityIcon icon = new NobilityIcon(0xa8)
                        {
                            Type = NobilityIcon.NobilityTypes.Minimum
                        };
                        for (int i = 1; i < 12; i += 2)
                        {
                            if (i == 11)
                            {
                                i++;
                            }
                            NobilityIcon.NobilityRanks rank = (NobilityIcon.NobilityRanks)i;
                            icon.AddMinimum(rank, Nobility.GetMinimumDonation(rank));
                        }
                        client.Send((byte[])icon);
                        break;
                    }
            }
        }
        public static ulong GetMinimumDonation(Network.GamePackets.NobilityIcon.NobilityRanks Rank)
        {
            switch (Rank)
            {
                case Network.GamePackets.NobilityIcon.NobilityRanks.Knight:
                case Network.GamePackets.NobilityIcon.NobilityRanks.Lady:
                    return 0x1c9c380L;

                case Network.GamePackets.NobilityIcon.NobilityRanks.Baron:
                case Network.GamePackets.NobilityIcon.NobilityRanks.Baroness:
                    return 0x5f5e100L;

                case Network.GamePackets.NobilityIcon.NobilityRanks.Earl:
                case Network.GamePackets.NobilityIcon.NobilityRanks.Countess:
                    return 0xbebc200L;
            }
            return MinimumDonationFor(Rank);
        }
        public static ulong MinimumDonationFor(Network.GamePackets.NobilityIcon.NobilityRanks Rank)
        {
            ulong num = 0L;
            for (int c = 0; c < BoardList.Count; c++)
            {


                sbyte place = 0;
                Network.GamePackets.NobilityIcon.NobilityRanks commoner = Network.GamePackets.NobilityIcon.NobilityRanks.Commoner;
                uint identifier = BoardList[c].EntityUID;
                ulong donation = BoardList[c].Donation;
                GetNobilityStats(identifier, donation, ref place, ref commoner);
                if ((commoner == Rank) && ((donation < num) || (num == 0L)))
                {
                    num = donation;
                }

            }
            return (num + ((ulong)1L));
        }
        public static void GetNobilityStats(uint Identifier, ulong Donation, ref sbyte Place, ref Network.GamePackets.NobilityIcon.NobilityRanks Rank)
        {
            sbyte place = 0;
            Network.GamePackets.NobilityIcon.NobilityRanks commoner = Network.GamePackets.NobilityIcon.NobilityRanks.Commoner;
            for (int c = 0; c < 50; c++)
            {

                if (BoardList[c].EntityUID == Identifier)
                {
                    break;
                }
                if (place < 50)
                {
                    place = (sbyte)(place + 1);
                }
            }

            Place = (place < 50) ? place : ((sbyte)(-1));
            if (Donation == 0L)
            {
                Place = -1;
            }
            commoner = GetRanking(Donation, place);
            Rank = commoner;
        }
        public static Network.GamePackets.NobilityIcon.NobilityRanks GetRanking(ulong Donation, sbyte Place)
        {
            if ((Donation != 0L) && (Place >= 0))
            {
                if ((Place < 5) && (Donation > 0L))
                {
                    return Network.GamePackets.NobilityIcon.NobilityRanks.King;
                }
                if ((Place < 25) && (Donation > 0L))
                {
                    return Network.GamePackets.NobilityIcon.NobilityRanks.Prince;
                }
                if ((Place < 20) && (Donation > 0L))
                {
                    return Network.GamePackets.NobilityIcon.NobilityRanks.Duke;
                }
                if (Donation >= 0xbebc200L)
                {
                    return Network.GamePackets.NobilityIcon.NobilityRanks.Earl;
                }
                if (Donation >= 0x5f5e100L)
                {
                    return Network.GamePackets.NobilityIcon.NobilityRanks.Baron;
                }
                if (Donation >= 0x1c9c380L)
                {
                    return Network.GamePackets.NobilityIcon.NobilityRanks.Knight;
                }
            }
            return Network.GamePackets.NobilityIcon.NobilityRanks.Commoner;
        }
        public static void Donate(ulong silvers, Client.GameClient client)
        {
            bool newDonator = false;
            client.NobilityInformation.Gender = (byte)(client.Entity.Body % 10);
            if (client.NobilityInformation.Donation == 0)
                newDonator = true;

            client.NobilityInformation.Donation += silvers;

            if (!Board.ContainsKey(client.Entity.UID) && client.NobilityInformation.Donation == silvers && newDonator)
            {
                Board.Add(client.Entity.UID, client.NobilityInformation);
                try
                {
                    Database.NobilityTable.InsertNobilityInformation(client.NobilityInformation);
                }
                catch
                {
                    Database.NobilityTable.UpdateNobilityInformation(client.NobilityInformation);
                }
            }
            else
            {
                Database.NobilityTable.UpdateNobilityInformation(client.NobilityInformation);
            }
            Sort(client.Entity.UID);
        }
        public static void Sort(uint updateUID)
        {
            SafeDictionary<uint, NobilityInformation> sortedBoard = new SafeDictionary<uint, NobilityInformation>();

            int Place = 0;
            foreach (NobilityInformation entry in Board.Values.OrderByDescending((p) => p.Donation))
            {
                Client.GameClient client = null;
                try
                {
                    int previousPlace = entry.Position;
                    entry.Position = Place;
                    NobilityRank Rank = NobilityRank.Serf;

                    if (Place > 50)
                    {
                        if (entry.Donation >= 200000000)
                        {
                            Rank = NobilityRank.Earl;
                        }
                        else if (entry.Donation >= 100000000)
                        {
                            Rank = NobilityRank.Baron;
                        }
                        else if (entry.Donation >= 30000000)
                        {
                            Rank = NobilityRank.Knight;
                        }
                    }
                    else
                    {
                        if (Place < 9)
                        {
                            Rank = NobilityRank.King;
                        }
                        else if (Place < 15)
                        {
                            Rank = NobilityRank.Prince;
                        }
                        else
                        {
                            Rank = NobilityRank.Duke;
                        }
                    }
                    var oldRank = entry.Rank;
                    entry.Rank = Rank;
                    if (Kernel.GamePool.TryGetValue(entry.EntityUID, out client))
                    {
                        bool updateTheClient = false;
                        if (oldRank != Rank)
                        {
                            if (oldRank == NobilityRank.Serf)
                            {
                                foreach (var player in Kernel.GamePool.Values)
                                    player.Send(new Message("Hear ye, hear ye! Greetings to our new "+Rank+", " + client.Entity.Name + " !", System.Drawing.Color.Red, Message.TopLeft));
                            }
                            else
                            {
                                foreach (var player in Kernel.GamePool.Values)
                                    player.Send(new Message(client.Entity.Name + " has been promoted to " + Rank + "!", System.Drawing.Color.Red, Message.TopLeft));
                            }
                            updateTheClient = true;
                        }
                        else
                        {
                            if (previousPlace != Place)
                            {
                                updateTheClient = true;
                            }
                        }
                        if (updateTheClient || client.Entity.UID == updateUID)
                        {
                            NobilityInfo update = new NobilityInfo(true);
                            update.Type = NobilityInfo.Icon;
                            update.dwParam = entry.EntityUID;
                            update.UpdateString(entry);
                            client.SendScreen(update, true);
                            client.Entity.NobilityRank = entry.Rank;
                        }
                    }
                    sortedBoard.Add(entry.EntityUID, entry);
                    Place++;
                }
                catch { }
            }
            Board = sortedBoard;
            lock (BoardList)
                BoardList = Board.Values.ToList();
        }
    }
    public class NobilityInformation
    {
        public string Name;
        public uint EntityUID;
        public uint Mesh;
        public ulong Donation;
        public byte Gender;
        public int Position;
        public NobilityRank Rank;
    }

    public enum NobilityRank : byte
    {
        Serf = 0,
        Knight = 1,
        Baron = 3,
        Earl = 5,
        Duke = 7,
        Prince = 9,
        King = 12
    }
}
