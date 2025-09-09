using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class Gambleing : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        public DateTime StartTime;

        public enum Gambl
        {
            PlaceChip = 0,
            ConfirmPlaceChip = 1,
            CancelChip = 2,
            BeginGamble = 4,
            EndGamble = 5,
            ResultGamble = 6,
        }

        public struct Bett
        {
            public uint Amount;
            public byte Precent;
        }

        public Gambleing(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[20 + 8];
                Writer.Write(20, 0, Buffer);
                Writer.Write(1113, 2, Buffer);
            }
            else
            {
                Buffer = new byte[28 + 8];
                Writer.Write(28, 0, Buffer);
                Writer.Write(1113, 2, Buffer);
            }
        }

        public Gambl Type
        {
            get { return (Gambl)Buffer[4]; }
            set { Buffer[4] = (byte)value; }
        }

        public byte Seconds
        {
            get { return (byte)Buffer[5]; }
            set { Buffer[5] = (byte)value; }
        }

        public ushort ID
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set {Writer.Write((ushort)value, 8, Buffer); }
        }

        public byte Number
        {
            get { return (byte)Buffer[12]; }
            set { Buffer[12] = (byte)value; }
        }

        public ushort Amount
        {
            get { return BitConverter.ToUInt16(Buffer, 16); }
            set {Writer.Write((ushort)value, 16, Buffer); }
        }

        public byte Dice1
        {
            get { return (byte)Buffer[16]; }
            set { Buffer[16] = (byte)value; }
        }

        public byte Dice2
        {
            get { return (byte)Buffer[17]; }
            set { Buffer[17] = (byte)value; }
        }

        public byte Dice3
        {
            get { return (byte)Buffer[18]; }
            set { Buffer[18] = (byte)value; }
        }

        public byte Unknowen
        {
            get { return (byte)Buffer[19]; }
            set { Buffer[19] = (byte)value; }
        }

        public Dictionary<byte, Bett> Bet = new SafeDictionary<byte, Bett>(1000);

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
        }
    }
}