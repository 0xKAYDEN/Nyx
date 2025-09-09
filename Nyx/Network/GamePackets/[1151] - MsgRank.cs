using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class GenericRanking : Writer, Interfaces.IPacket
    {
        public const byte
        Ranking = 1,
        QueryCount = 2,
        InformationRequest = 5;
        public const uint
        RoseFairy = 30000002,
        LilyFairy = 30000102,
        OrchidFairy = 30000202,
        InnerPower = 70000000,
        TulipFairy = 30000302,
        Kiss = 30000402,
        Love = 30000502,
        Tins = 30000602,
        Jade = 30000702,
        Chi = 60000000,
        DragonChi = 60000001,
        PhoenixChi = 60000002,
        TigerChi = 60000003,
        TurtleChi = 60000004;

        private byte[] Buffer;
        private int current;

        public GenericRanking(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[744 + 8];
                Writer.Write(Buffer.Length - 8, 0, Buffer);
                Writer.Write(1151, 2, Buffer);
            }
        }

        public uint Mode
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public uint RankingType
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public ushort RegisteredCount
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set {Writer.Write(value, 12, Buffer); }
        }

        public ushort Page
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set { Writer.Write(value, 14, Buffer); }
        }

        public uint Count
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public void Append(uint rank, uint amount, uint uid, string name)
        {
            int offset = current * 72 + 24;
            if (offset + 72 <= Buffer.Length)
            {
                current++;
                Count = (uint)current;
                Write(rank, offset, Buffer);
                offset += 8;
                Write(amount, offset, Buffer);
                offset += 8;
                Write(uid, offset, Buffer);
                offset += 4;
                Write(uid, offset, Buffer);
                offset += 4;
                Writer.Write(name, offset, Buffer);
                offset += 16;
                Writer.Write(name, offset, Buffer);
                offset += 32;
            }
        }
        public void Append2(uint rank, uint amount, uint uid, string name, byte level, ushort Class, uint mesh)
        {
            int offset = current * 72 + 24;
            if (offset + 72 <= Buffer.Length)
            {
                current++;
                Count = (uint)current;
                Write(rank, offset, Buffer);
                offset += 8;
                Write(amount, offset, Buffer);
                offset += 8;
                Write(uid, offset, Buffer);
                offset += 4;
                Write(uid, offset, Buffer);
                offset += 4;
                Writer.Write(name, offset, Buffer);
                offset += 16;
                Writer.Write(name, offset, Buffer);
                offset += 16;
                Writer.Write(level, offset, Buffer);
                offset += 4;
                Writer.Write(Class, offset, Buffer);
                offset += 4;
                Writer.Write(mesh, offset, Buffer);
                offset += 4;
            }
        }
        public void Reset()
        {
            current = 0;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] _buffer)
        {
            Buffer = _buffer;
            if (Count == 0)
            {
                byte[] buffer = new byte[744 + 8];
                Buffer.CopyTo(buffer, 0);
                Writer.Write(744, 0, buffer);
                Writer.Write(1151, 2, buffer);
                Buffer = buffer;
            }
        }
    }
}