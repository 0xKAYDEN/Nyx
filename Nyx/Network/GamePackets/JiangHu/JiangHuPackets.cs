using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class JiangHuStatus : Writer
    {
        byte[] packet;
        public JiangHuStatus(uint count = 1)
        {
            packet = new byte[56 + count * 18];

            Writer.Write((ushort)(48 + count * 18), 0, packet);
            Writer.Write((ushort)(2701), 2, packet);//2701
            Write(9999999, 39, packet);
        }

        public string Name
        {
            set { Writer.Write(value, 4, packet); }
        }
        public byte Stage { get { return packet[20]; } set { Write(value, 20, packet); } }
        public byte Talent { get { return packet[21]; } set { Write(value, 21, packet); } }
        public uint Timer { get { return BitConverter.ToUInt32(packet, 22); } set { Write(value, 22, packet); } }
        public ulong StudyPoints { get { return BitConverter.ToUInt64(packet, 27); } set { Writer.Write(value, 27, packet); } }
        public uint FreeTimeTodey { get { return BitConverter.ToUInt32(packet, 35); } set { Write(value, 35, packet); } }

        public byte FreeTimeTodeyUsed { get { return packet[43]; } set { Write(value, 43, packet); } }

        public uint RoundBuyPoints { get { return BitConverter.ToUInt32(packet, 44); } set { Write(value, 44, packet); } }
        private ushort Position = 48;
        public void Apprend(ICollection<Game.JiangHu.JiangStages> val)
        {
            foreach (var obj in val)
            {
                if (!obj.Activate)
                    break;
                for (byte x = 0; x < obj.Stars.Length; x++)
                {
                    if (packet.Length < Position + 2)
                        break;
                    var star = obj.Stars[x];
                    if (!star.Activate)
                        break;
                    Writer.Write(star.UID, Position, packet);
                    Position += 2;
                }
            }
            FreeTimeTodeyUsed = 0;

        }
        public byte[] Encode()
        {
            return packet;
        }
    }
    public class JiangHuRank : Writer
    {
        byte[] packet;
        public JiangHuRank(byte entry = 1)
        {
            packet = new byte[16 + entry * 41];
            Writer.Write((ushort)(packet.Length - 8), 0, packet);
            Writer.Write(2703, 2, packet);

            Write(entry, 6, packet);//count on page
        }
        public byte Page { get { return packet[4]; } set { packet[4] = value; } }
        public byte MyRank { get { return packet[5]; } set { packet[5] = value; } }

        public byte RegisteredCount { get { return packet[7]; } set { Write(value, 7, packet); } }

        private ushort Position = 8;
        public void Appren(byte Rank, uint Inner_Strength, uint Level, string Name, string CustomizedName)
        {
            Write(Rank, Position, packet); Position++;
            Write(Inner_Strength, Position, packet); Position += 4;
            Write(Level, Position, packet); Position += 4;
            Writer.Write(Name, Position, packet); Position += 16;
            Writer.Write(CustomizedName, Position, packet); Position += 16;
        }
        public byte[] Encode()
        {
            return packet;
        }
    }
    public class JiangHuUpdate : Writer
    {
        byte[] packet;
        public JiangHuUpdate()
        {
            packet = new byte[27];
            Writer.Write(19, 0, packet);
            Writer.Write(2702, 2, packet);

        }
        public uint FreeCourse { get { return BitConverter.ToUInt32(packet, 4); } set { Write(value, 4, packet); } }
        public byte Star { get { return packet[10]; } set { Write(value, 10, packet); } }
        public byte Stage { get { return packet[11]; } set { Write(value, 11, packet); } }
        //public byte Talent { get { return ReadByte(14); } set { Write(value, 14, packet); } }
        public ushort Atribute { get { return BitConverter.ToUInt16(packet, 12); } set { Writer.Write(value, 12, packet); } }
        public byte FreeTimeTodeyUsed { get { return packet[14]; } set { Write(value, 14, packet); } }
        public uint RoundBuyPoints { get { return BitConverter.ToUInt32(packet, 15); } set { Write(value, 15, packet); } }

        public byte[] Encode()
        {
            return packet;
        }
    }
    public class JiangHu : Writer
    {
        public const byte IconBar = 0, InfoStauts = 7, UpdateTalent = 5, UpdateStar = 11, OpenStage = 12, UpdateTime = 13, SetName = 14;

        private byte[] packet;

        private uint Leng(string[] dat)
        {
            uint len = 0;
            foreach (string line in dat)
                len += (byte)line.Length;
            return (uint)(len + dat.Length);
        }

        public JiangHu()
        {
            Texts = new List<string>();

        }
        public List<string> Texts;
        public byte Mode = 0;

        public void CreateArray()
        {
            packet = new byte[Leng(Texts.ToArray()) + 7 + 8];//6
            Writer.Write((ushort)(packet.Length - 8), 0, packet);
            Writer.Write(2700, 2, packet);
            Write(Mode, 4, packet);
            Write((byte)Texts.Count, 5, packet);
            ushort position = 6;
            for (ushort x = 0; x < Texts.Count; x++)
            {
                string text = Texts[x];
                Write((byte)text.Length, position, packet);
                Writer.Write(text, (ushort)(position + 1), packet);
                position += (ushort)(text.Length + 1);
            }
        }
        public void Clear()
        {
            Texts.Clear();
        }
        public void Send(Client.GameClient client)
        {
            if (packet != null)
            {
                client.Send(packet.ToArray());
            }
        }
    }
}
