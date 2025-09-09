using Nyx.Server.Client;
using Nyx.Server.Game;
using Nyx.Server.Interfaces;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;
using System;

namespace Nyx.Server.Roulette
{
    public class RoulettePacket
    {
        public enum Action : byte
        {
            Join = 3,
            RemovePlayer = 2,
            viewwatcher = 1,
            Record = 0
        }
        public static void PorocesCheck(byte[] packet, GameClient client)
        {
            var len = packet[4];
            int offset = 5;
            var Table = GetTable(client.RouletteID);
            if (Table == null)
                return;
            if (client.Trade.InTrade) return;
            client.RoulletBetAmount = 0;
            for (int i = 1; i < len + 1; i++)
            {
                RouletteStructure Roullet = new RouletteStructure();
                Roullet.BetAttribute.Type = packet[offset];
                offset += 1;
                Roullet.BetAmount = BitConverter.ToUInt32(packet, offset);
                offset += 4;

                if (Table.StackType == 1)
                {
                    client.RoulletBetAmount += Roullet.BetAmount;
                    Roullet.GetValues();
                    client.RoulleteBet.Add(Roullet);
                }
                else if (Table.StackType == 2)
                {
                    client.RoulletBetAmount += Roullet.BetAmount;
                    Roullet.GetValues();
                    client.RoulleteBet.Add(Roullet);
                }
            }
            if (Table.StackType == 1)
            {
                if (client.Entity.Money >= client.RoulletBetAmount)
                {
                    client.Entity.Money -= client.RoulletBetAmount;
                }
                else
                {
                    client.RoulleteBet.Clear();
                }
            }
            else if (Table.StackType == 2)
            {
                if (client.Entity.ConquerPoints >= client.RoulletBetAmount)
                {
                    client.Entity.ConquerPoints -= client.RoulletBetAmount;
                }
                else
                {
                    client.RoulleteBet.Clear();
                }
            }
            if (client.RoulleteBet.Count > 0)
            {
                int lenn = client.RoulleteBet.Count;
                byte[] buff = new byte[5 + 8 + 5 * len];
                Writer.Write((ushort)(buff.Length - 8), 0, buff);
                Writer.Write(2810, 2, buff);
                Writer.Write((byte)(client.RoulettePlace - (byte)1), 4, buff);
                Writer.Write((byte)lenn, 5, buff);
                int Offset = 6;
                for (int i = 1; i < len + 1; i++)
                {
                    Writer.Write((byte)client.RoulleteBet[i - 1].BetAttribute.Type, Offset, buff);
                    Offset += 1;
                    Writer.Write(client.RoulleteBet[i - 1].BetAmount, Offset, buff);
                    Offset += 4;
                }
                foreach (var nclient in Kernel.GamePool.Values)
                {
                    if (nclient.Entity.UID != client.Entity.UID)
                    {
                        if (nclient.RouletteID != 0 && nclient.RouletteID == client.RouletteID ||
                            nclient.RouletteWatchID != 0 && nclient.RouletteWatchID == client.RouletteID)
                        {
                            nclient.Send(buff);
                        }
                    }
                }
            }
        }
        public static void PorocesAction(byte[] packet, GameClient client)
        {
            switch (packet[4])
            {
                case (byte)Action.viewwatcher:
                    {
                        int len = 0;
                        foreach (var nclient in Kernel.GamePool.Values)
                        {
                            if (nclient.RouletteWatchID > 0)
                            {
                                if (nclient.RouletteWatchID == client.RouletteWatchID ||
                                    nclient.RouletteWatchID == client.RouletteID)
                                {
                                    len += 1;
                                }
                            }
                        }
                        byte[] buffer = new byte[5 + 8 + 4 * len];
                        Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                        Writer.Write(2803, 2, buffer);
                        Writer.Write((byte)len, 4, buffer);
                        int offset = 5;
                        {
                            foreach (var nclient in Kernel.GamePool.Values)
                            {
                                if (nclient.RouletteWatchID > 0)
                                {
                                    if (nclient.RouletteWatchID == client.RouletteWatchID ||
                                        nclient.RouletteWatchID == client.RouletteID)
                                    {
                                        Writer.Write(nclient.Entity.UID, offset, buffer);
                                        offset += 4;
                                    }
                                }
                            }
                        }
                        client.Send(buffer);
                        break;
                    }
                case (byte)Action.Record:
                    {
                        IRoulette Table = null;
                        if (client.RouletteID != 0)
                            Table = GetTable(client.RouletteID);
                        else if (client.RouletteWatchID != 0)
                            Table = GetTable(client.RouletteWatchID);
                        if (Table == null)
                            return;
                        int len = Table.Recored.Count;
                        byte[] buff = new byte[6 + 8 + 24 * len];
                        Writer.Write((ushort)(buff.Length - 8), 0, buff);
                        Writer.Write(2802, 2, buff);
                        Writer.Write((byte)Table.LotteryNumber, 4, buff);
                        Writer.Write((byte)len, 5, buff);
                        int offset = 6;
                        for (int i = 1; i < len + 1; i++)
                        {
                            Writer.Write(Table.Recored[i - 1].RoulletBetAmount, offset, buff);
                            offset += 4;
                            Writer.Write(Table.Recored[i - 1].RoulletWinnigAmount, offset, buff);
                            offset += 4;
                            Writer.Write(Table.Recored[i - 1].Entity.Name, offset, buff);
                            offset += 16;
                        }
                        client.Send(buff);
                        break;
                    }
                case (byte)Action.RemovePlayer:
                    {
                        if (client.RouletteWatchID != 0)
                        {
                            client.RouletteWatchID = 0;
                            break;
                        }
                        byte[] buffer = new byte[9 + 8];
                        Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                        Writer.Write(2805, 2, buffer);
                        Writer.Write(7, 4, buffer);
                        Writer.Write(client.Entity.UID, 5, buffer);
                        foreach (var nclient in Kernel.GamePool.Values)
                        {
                            if (nclient.Entity.UID != client.Entity.UID)
                            {
                                if (nclient.RouletteID == client.RouletteID ||
                                    nclient.RouletteWatchID == client.RouletteID)
                                {
                                    nclient.Send(buffer);
                                }
                            }
                        }
                        var map = Kernel.Maps[2807];
                        foreach (var table in map.Tables.Values)
                        {
                            if (table.UID == client.RouletteID)
                            {
                                if (client.RoulettePlace == 1)
                                    table.Place0 = false;
                                if (client.RoulettePlace == 2)
                                    table.Place1 = false;
                                if (client.RoulettePlace == 3)
                                    table.Place2 = false;
                                if (client.RoulettePlace == 4)
                                    table.Place3 = false;
                                if (client.RoulettePlace == 5)
                                    table.Place4 = false;
                            }
                        }
                        client.RoulettePlace = 0;
                        client.RouletteID = 0;
                        client.RoulleteBet.Clear();
                        break;
                    }
                case (byte)Action.Join:
                    {
                        if (client.Entity.MapID != 2807)
                            return;
                        var table = GetTable(client.RouletteWatchID);
                        if (table == null)
                            return;
                        GetTablePlayerNumber(table);
                        client.RoulettePlace = GetTablePlace(table);
                        if (table.PlayerNumber < 5)
                        {
                            byte[] buffer = new byte[9 + 8];
                            Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                            Writer.Write(2805, 2, buffer);
                            Writer.Write(8, 4, buffer);
                            Writer.Write((byte)(client.RoulettePlace - 1), 5, buffer);
                            foreach (var nclient in Kernel.GamePool.Values)
                            {
                                if (nclient.RouletteID == table.UID)
                                {
                                    if (nclient.Entity.UID != client.Entity.UID)
                                    {
                                        byte[] bufffer = new byte[29 + 8];
                                        Writer.Write((ushort)(bufffer.Length - 8), 0, bufffer);
                                        Writer.Write(2809, 2, bufffer);
                                        Writer.Write(client.Entity.UID, 4, bufffer);
                                        Writer.Write(client.Entity.Mesh, 8, bufffer);
                                        Writer.Write((byte)(client.RoulettePlace - (byte)1), 12, bufffer);
                                        Writer.Write(client.Entity.Name, 13, bufffer);
                                        nclient.Send(bufffer);
                                    }
                                }
                            }
                            client.Send(buffer);
                            client.RouletteWatchID = 0;
                            client.RouletteID = table.UID;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public static void PorocesSignUp(byte[] packet, GameClient client)
        {
            RouletteActions RouletteRequest = new RouletteActions(false);
            RouletteRequest.Deserialize(packet);
            uint TableID = 0;
            TableID = RouletteRequest.TableID;

            switch (RouletteRequest.Type)
            {
                case RouletteActions.RoulettType.Join:
                    {
                        if (client.Trade.InTrade) return;
                        if (client.Entity.MapID != 2807)
                            return;
                        client.RouletteWatchID = 0;
                        var Table = client.Map.Tables[TableID];
                        if (Table == null)
                            return;
                        GetTablePlayerNumber(client, TableID);
                        if (Table.PlayerNumber < 5)
                        {
                            client.RoulettePlace = GetTablePlace(Table);
                            var len = Table.PlayerNumber;
                            byte[] buff = new byte[11 + 8 + 25 * len];
                            Writer.Write((ushort)(buff.Length - 8), 0, buff);
                            Writer.Write(2808, 2, buff);
                            Writer.Write((byte)Table.StackType, 4, buff);
                            Writer.Write((byte)(client.RoulettePlace - (byte)1), 8, buff);
                            if (Table.PlayerNumber == 0)
                            {
                                Table.Stamp = 30;
                                Table.Time = Time32.Now;
                                Writer.Write((byte)Table.Stamp, 9, buff);
                            }
                            else
                            {
                                Writer.Write((byte)(Table.Stamp - (Time32.Now - Table.Time).AllSeconds()), 9,
                                    buff);
                            }
                            Writer.Write((byte)len, 10, buff);
                            int Offset = 11;
                            // for (int i = 1; i < len + 1; i++)
                            {
                                foreach (var nclient in Kernel.GamePool.Values)
                                {
                                    if (nclient.RouletteID == Table.UID)
                                    {
                                        if (nclient.Entity.UID != client.Entity.UID)
                                        {
                                            Writer.Write(nclient.Entity.UID, Offset, buff);
                                            Offset += 4;
                                            Writer.Write(nclient.Entity.Mesh, Offset, buff);
                                            Offset += 4;
                                            Writer.Write((byte)(nclient.RoulettePlace - (byte)1), Offset, buff);
                                            Offset += 1;
                                            Writer.Write(nclient.Entity.Name, Offset, buff);
                                            Offset += 16;

                                            byte[] buffer = new byte[29 + 8];
                                            Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                                            Writer.Write(2809, 2, buffer);
                                            Writer.Write(client.Entity.UID, 4, buffer);
                                            Writer.Write(client.Entity.Mesh, 8, buffer);
                                            Writer.Write((byte)(client.RoulettePlace - (byte)1), 12, buffer);
                                            Writer.Write(client.Entity.Name, 13, buffer);
                                            nclient.Send(buffer);
                                        }
                                    }
                                }
                                foreach (var nclient in Kernel.GamePool.Values)
                                {
                                    if (nclient.Entity.UID != client.Entity.UID)
                                    {
                                        if (nclient.RouletteWatchID == Table.UID)
                                        {
                                            byte[] buffer = new byte[29 + 8];
                                            Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                                            Writer.Write(2809, 2, buffer);
                                            Writer.Write(client.Entity.UID, 4, buffer);
                                            Writer.Write(client.Entity.Mesh, 8, buffer);
                                            Writer.Write((byte)(client.RoulettePlace - (byte)1), 12, buffer);
                                            Writer.Write(client.Entity.Name, 13, buffer);
                                            nclient.Send(buffer);
                                        }
                                    }
                                }
                            }
                            client.RouletteID = TableID;
                            client.Send(buff);
                        }
                        break;
                    }
                case RouletteActions.RoulettType.Watch:
                    {
                        if (client.Trade.InTrade) return;
                        if (client.Entity.MapID != 2807)
                            return;
                        var Table = GetTable(TableID);
                        GetTablePlayerNumber(Table);
                        if (Table.PlayerNumber == 0)
                            return;
                        var len = Table.PlayerNumber;
                        byte[] buff = new byte[11 + 8 + 25 * len];
                        Writer.Write((ushort)(buff.Length - 8), 0, buff);
                        Writer.Write(2808, 2, buff);
                        Writer.Write((byte)Table.StackType, 4, buff);
                        Writer.Write((byte)99, 8, buff);
                        Writer.Write((byte)(Table.Stamp - (Time32.Now - Table.Time).AllSeconds()), 9, buff);
                        Writer.Write((byte)len, 10, buff);
                        int Offset = 11;
                        // for (int i = 1; i < len + 1; i++)
                        {
                            foreach (var nclient in Kernel.GamePool.Values)
                            {
                                if (nclient.RouletteID == Table.UID)
                                {
                                    if (nclient.Entity.UID != client.Entity.UID)
                                    {
                                        Writer.Write(nclient.Entity.UID, Offset, buff);
                                        Offset += 4;
                                        Writer.Write(nclient.Entity.Mesh, Offset, buff);
                                        Offset += 4;
                                        Writer.Write((byte)(nclient.RoulettePlace - (byte)1), Offset, buff);
                                        Offset += 1;
                                        Writer.Write(nclient.Entity.Name, Offset, buff);
                                        Offset += 16;
                                    }
                                }
                            }
                        }
                        client.RouletteWatchID = Table.UID;
                        client.Send(buff);
                        break;
                    }
                case RouletteActions.RoulettType.Info:
                    {
                        var Table = GetTable(TableID);
                        GetTablePlayerNumber(Table);
                        client.Map.Tables[TableID].SendSpawn(client);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public static byte GetTablePlace(IRoulette Table)
        {
            if (Table.PlayerNumber == 0 && Table.Place0 == false)
            {
                Table.Place0 = true; return 1;
            }
            if (Table.Place0 == false)
            {
                Table.Place0 = true; return 1;
            }
            if (Table.Place1 == false)
            {
                Table.Place1 = true; return 2;
            }
            if (Table.Place2 == false)
            {
                Table.Place2 = true; return 3;
            }
            if (Table.Place3 == false)
            {
                Table.Place3 = true; return 4;
            }
            if (Table.Place4 == false)
            {
                Table.Place4 = true; return 5;
            }
            return 1;
        }
        public static IRoulette GetTable(uint TableID)
        {
            Map map = Kernel.Maps[2807];
            foreach (var Table in map.Tables.Values)
            {
                if (Table.UID == TableID)
                {
                    return Table;
                }
            }
            return null;
        }
        public static void GetTablePlayerNumber(IRoulette Table)
        {
            Table.PlayerNumber = 0;
            foreach (var nclient in Kernel.GamePool.Values)
            {
                if (nclient.RouletteID == Table.UID)
                {
                    Table.PlayerNumber += 1;
                }
            }
        }
        public static void GetTablePlayerNumber(GameClient client, uint ID)
        {
            Map map = Kernel.Maps[2807];
            var Table = map.Tables[ID];
            Table.PlayerNumber = 0;
            foreach (var nclient in Kernel.GamePool.Values)
            {
                if (nclient.RouletteID == Table.UID)
                {
                    Table.PlayerNumber += 1;
                }
            }
        }
    }
}
