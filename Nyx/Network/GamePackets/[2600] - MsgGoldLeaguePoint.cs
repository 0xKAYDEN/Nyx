using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Client;

namespace Nyx.Server.Network.GamePackets
{
    public class ChampionPoints : Writer, Interfaces.IPacket
    {
        byte[] Buffer;
        public ChampionPoints()
        {
            Buffer = new byte[7 + 8];
            Writer.Write(7, 0, Buffer);
            Writer.Write(2600, 2, Buffer);
        }
        public uint TodayPoints;
        public uint AllChampionPoints;
        public DateTime LastChampionPointFill
        {
            get;
            set;
        }
        public uint Points;
        public void Send(Client.GameClient client)
        {
            client.Send(Encode());
        }
        public byte[] Encode()
        {
            byte[] ptr = CreateProtocolBuffer(Points, AllChampionPoints);
            byte[] Buffer = new byte[11 + ptr.Length];
            Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
            Writer.Write(2600, 2, Buffer);
            Write(8, 4, Buffer);
            Array.Copy(ptr, 0, Buffer, 4, ptr.Length);
            return Buffer;
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
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public uint EntityID
        {
            get;
            set;
        }
    }
}
