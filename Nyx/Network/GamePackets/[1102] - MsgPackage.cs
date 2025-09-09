using Nyx.Server.Database;
using Nyx.Server.Game;
using System;
namespace Nyx.Server.Network.GamePackets
{
    public class Warehouse : Writer, Interfaces.IPacket
    {
        public static Client.GameClient client;
        public const byte Entire = 0, AddItem = 1, RemoveItem = 2;
        private byte[] buffer;
        public Warehouse(bool Create)
        {
            if (Create)
            {
                buffer = new byte[148 + 8];
                Writer.Write(148, 0, buffer);
                Writer.Write(1102, 2, buffer);
            }
        }

        public uint NpcID
        {
            get { return BitConverter.ToUInt32(buffer, 4); }
            set { Write(value, 4, buffer); }
        }

        public byte Type
        {
            get
            {
                return buffer[12];
            }
            set
            {
                buffer[12] = value;
            }
        }

        public uint Count
        {
            get { return BitConverter.ToUInt32(buffer, 24); }
            set
            {
                if (value > 20)
                    throw new Exception("Invalid Count value.");
                byte[] Buffer = new byte[8 + 84 + (72 * value)];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(1102, 2, Buffer);
                Write(NpcID, 4, Buffer);
                Write(Type, 12, Buffer);
                Buffer[13] = buffer[13];
                Write(value, 24, Buffer);
                buffer = Buffer;
            }
        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(buffer, 20); }
            set { Write(value, 20, buffer); }
        }

        public void Append(Network.GamePackets.ConquerItem item)
        {
            Write(item.UID, 28, buffer);
            Write(item.ID, 32, buffer);
            Write((byte)item.SocketOne, 37, buffer);
            Write((byte)item.SocketTwo, 38, buffer);
            Write(item.Plus, 45, buffer);
            Write(item.Bless, 46, buffer);
            Write((byte)(item.Bound == true ? 1 : 0), 47, buffer);
            Writer.Write(item.Enchant, 48, buffer);
            Writer.Write((ushort)item.Effect, 50, buffer);
            Write((byte)(item.Suspicious == true ? 1 : 0), 52, buffer);
            Write(item.Lock, 54, buffer);
            Write((byte)item.Color, 55, buffer);
            Write(item.SocketProgress, 56, buffer);
            Write(item.PlusProgress, 60, buffer);
            Write((byte)(item.Inscribed == true ? 1 : 0), 64, buffer);
            Writer.Write((ushort)item.Mode, 76, buffer);
            Writer.Write((ushort)item.Durability, 78, buffer);
            Writer.Write((ushort)item.MaximDurability, 80, buffer);
            Write((uint)((item.Minutes * 24) * 60), 72, buffer);
            Write(item.TimeLeftInMinutes, 68, buffer);
            Write(item.Stars, 84, buffer);
            Write(item.PerfectionProgress, 88, buffer);
            Write(item.OwnerUID, 92, buffer);
            Write(item.Owner, 96, buffer);
        }

        public byte[] Encode()
        {
            return buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(buffer);
        }
    }
}