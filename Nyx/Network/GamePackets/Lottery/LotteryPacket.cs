using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    internal class Lottery
    {
        private byte[] mData;
        public enum LotteryTypes
        {
            Accept = 0,
            AddJade = 1,
            Continue = 2,
            SocketOne = 7,
            SocketTwo = 8,
            ShowGUI = 0x103
        }
        public Lottery()
        {
            this.mData = new byte[26 + 8];
            Nyx.Server.Network.SafeWriter.Write(26, 0, this.mData);
            Nyx.Server.Network.SafeWriter.Write((ushort)1314, 2, this.mData);
            Nyx.Server.Network.SafeWriter.Write((byte)1, 6, this.mData);
        }

        public Lottery(byte[] d)
        {
            this.mData = new byte[d.Length];
            d.CopyTo(this.mData, 0);
        }

        public static implicit operator byte[](Lottery d)
        {
            return d.mData;
        }

        public byte AddJadeChances
        {
            get
            {
                return this.mData[11];
            }
            set
            {
                this.mData[11] = value;
            }
        }

        public byte Chances
        {
            get
            {
                return this.mData[12];
            }
            set
            {
                this.mData[12] = value;
            }
        }

        public Game.Enums.Color Color
        {
            get
            {
                return (Game.Enums.Color)((byte)BitConverter.ToUInt16(this.mData, 10));
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write((ushort)value, 10, this.mData);
            }
        }

        public byte Plus
        {
            get
            {
                return this.mData[9];
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 9, this.mData);
            }
        }

        public uint Prize
        {
            get
            {
                return BitConverter.ToUInt32(this.mData, 12);
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 12, this.mData);
            }
        }

        public byte SocketOne
        {
            get
            {
                return this.mData[7];
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 7, this.mData);
            }
        }

        public byte SocketTwo
        {
            get
            {
                return this.mData[8];
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 8, this.mData);
            }
        }

        public LotteryTypes Type
        {
            get
            {
                return (LotteryTypes)BitConverter.ToUInt16(this.mData, 4);
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write((ushort)value, 4, this.mData);
            }
        }
    }
    class LotterySystem : Writer, Interfaces.IPacket
    {
        public const ushort Accept = 0, AddJade = 1, Continue = 2, Show = 3;

        byte[] Buffer;
        public LotterySystem(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[28];
                Writer.Write(20, 0, Buffer);
                Writer.Write(1314, 2, Buffer);
                Write(3, 4, Buffer);
                Write(3, 10, Buffer);
                Write(2, 5, Buffer);
                Write(2, 6, Buffer);
                //Write(3, 12, Buffer);
            }
        }

        public byte Type
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }
        public byte SocketGem1
        {
            get { return Buffer[7]; }
            set { Buffer[7] = value; }
        }
        public byte SocketGem2
        {
            get { return Buffer[8]; }
            set { Buffer[8] = value; }
        }
        public byte Plus
        {
            get { return Buffer[9]; }
            set { Buffer[9] = value; }
        }
        public byte ItemColor
        {
            get { return Buffer[10]; }
            set { Buffer[10] = value; }
        }
        public byte JadesAdded
        {
            get { return Buffer[11]; }
            set { Buffer[11] = value; }
        }
        public ushort LottoTimes1
        {
            get { return BitConverter.ToUInt16(Buffer, 13); }
            set {Writer.Write(value, 13, Buffer); }
        }
        public uint ItemID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}