using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class NobilityInfo : Writer, Interfaces.IPacket
    {
        public const uint
            Donate = 1,
            List = 2,
            Icon = 3,
            NextRank = 4;

        byte[] Buffer;

        public NobilityInfo(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[183 + 8];
                Writer.Write(33, 0, Buffer);
                Writer.Write(2064, 2, Buffer);
            }
            Strings = new List<string>();
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public ushort MoneyType
        {
            get { return BitConverter.ToUInt16(Buffer, 104); }
            set {Writer.Write(value, 104, Buffer); }
        }

        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public ushort wParam1
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set {Writer.Write(value, 8, Buffer); }
        }

        public ushort wParam2
        {
            get { return BitConverter.ToUInt16(Buffer, 10); }
            set {Writer.Write(value, 10, Buffer); }
        }

        public uint dwParam2
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public uint dwParam3
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public uint dwParam4
        {
            get { return BitConverter.ToUInt32(Buffer, 24); }
            set { Write(value, 24, Buffer); }
        }
        //Thanks to ImmuneOne, who fixed the strings offsets, I managed to get nobility done.
        public byte StringCount
        {
            get { return Buffer[120]; }
            set { Buffer[120] = value; }
        }

        public List<string> DecodedStrings
        {
            get
            {
                List<string> list = new List<string>(StringCount);
                int offset = 120;
                for (int count = 0; count < StringCount; count++)
                {
                    byte stringLength = Buffer[offset]; offset++;
                    string String = Encoding.Default.GetString(Buffer, offset, stringLength);
                    offset += stringLength;
                    list.Add(String);
                }
                return list;
            }
        }

        public List<string> Strings;

        public void UpdateString(Game.ConquerStructures.NobilityInformation info)
        {
            string buildString = info.EntityUID + " " + info.Donation + " " + (byte)info.Rank + " " + info.Position;
            buildString = (char)buildString.Length + buildString;
            Strings.Add(buildString);
        }

        public void ListString(Game.ConquerStructures.NobilityInformation info)
        {
            string buildString = info.EntityUID + " " + info.Gender + " 0 " + info.Name + " " + info.Donation + " " + (byte)info.Rank + " " + info.Position;
            buildString = (char)buildString.Length + buildString;
            Strings.Add(buildString);
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Encode());
        }

        public byte[] Encode()
        {
            if (Strings.Count == 0)
                return Buffer;
            string theString = "";
            for (int count = 0; count < Strings.Count; count++)
            {
                theString += Strings[count];
            }
            byte[] newBuffer = new byte[183 + 8 + theString.Length];
            Buffer.CopyTo(newBuffer, 0);
            Writer.Write((ushort)(newBuffer.Length - 8), 0, newBuffer);
            newBuffer[120] = (byte)Strings.Count;
            Writer.Write(theString, 121, newBuffer);
            return newBuffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public class NobilityIcon
    {
        private byte[] mData;
        private int Offset;
        private int rankOffset;

        public NobilityIcon(int length = 120)
        {
            this.Offset = 120;
            this.rankOffset = 8;
            this.Init(length);
        }

        public NobilityIcon(string s)
        {
            this.Offset = 120;
            this.rankOffset = 8;
            this.Init(0xa8 + s.Length);
            Nyx.Server.Network.SafeWriter.Write(new List<string> { s }, 120, this.mData);
        }

        public NobilityIcon(byte[] d)
        {
            this.Offset = 120;
            this.rankOffset = 8;
            this.mData = new byte[d.Length];
            d.CopyTo(this.mData, 0);
        }
        public enum NobilityRanks
        {
            Commoner,
            Knight,
            Lady,
            Baron,
            Baroness,
            Earl,
            Countess,
            Duke,
            Dutchess,
            Prince,
            Princess,
            Queen,
            King
        }
        public void AddMinimum(NobilityRanks Rank, ulong amount)
        {
            Nyx.Server.Network.SafeWriter.Write(amount, this.rankOffset, this.mData);
            Nyx.Server.Network.SafeWriter.Write(uint.MaxValue, this.rankOffset + 8, this.mData);
            Nyx.Server.Network.SafeWriter.Write((uint)Rank, this.rankOffset + 12, this.mData);
            this.rankOffset += 0x10;
        }
        public enum Gender : byte
        {
            Female = 2,
            Male = 1
        }
        public static Gender GetGender(uint body)
        {
            if ((body == 0x3eb) || (body == 0x3ec))
            {
                return Gender.Male;
            }
            return Gender.Female;
        }
        public void AddRank(uint Identifier, uint body, NobilityRanks Rank, string Name, ulong Donation, byte Pos)
        {
            byte[] array = new byte[this.mData.Length];
            this.mData.CopyTo(array, 0);
            this.mData = new byte[array.Length + 0x30];
            array.CopyTo(this.mData, 0);
            Nyx.Server.Network.SafeWriter.Write((ushort)this.mData.Length, 0, this.mData);
            Nyx.Server.Network.SafeWriter.Write(Identifier, this.Offset, this.mData);
            Nyx.Server.Network.SafeWriter.Write((uint)GetGender(body), this.Offset + 4, this.mData);
            Nyx.Server.Network.SafeWriter.Write(body, this.Offset + 8, this.mData);
            Nyx.Server.Network.SafeWriter.Write(Name, this.Offset + 12, this.mData);
            Nyx.Server.Network.SafeWriter.Write(Donation, this.Offset + 0x20, this.mData);
            Nyx.Server.Network.SafeWriter.Write((uint)Rank, this.Offset + 40, this.mData);
            Nyx.Server.Network.SafeWriter.Write(Pos, this.Offset + 0x2c, this.mData);
            this.Offset += 0x30;
        }

        private void Init(int len)
        {
            this.mData = new byte[len + 8];
            Nyx.Server.Network.SafeWriter.Write((ushort)len, 0, this.mData);
            Nyx.Server.Network.SafeWriter.Write((ushort)0x810, 2, this.mData);
        }

        public static implicit operator byte[](NobilityIcon d)
        {
            return d.mData;
        }

        public uint dwParam
        {
            get
            {
                return BitConverter.ToUInt32(this.mData, 8);
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 8, this.mData);
            }
        }

        public ulong MinimumDonation
        {
            get
            {
                return BitConverter.ToUInt64(this.mData, 8);
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 8, this.mData);
                this.mData[20] = 60;
                Nyx.Server.Network.SafeWriter.Write(uint.MaxValue, 0x18, this.mData);
            }
        }

        public byte ShownMin
        {
            get
            {
                return this.mData[10];
            }
            set
            {
                this.mData[10] = value;
            }
        }

        public string String
        {
            get
            {
                return Encoding.ASCII.GetString(this.mData, 0x22, this.mData[0x21]);
            }
        }
        public enum NobilityTypes : uint
        {
            Donate = 1,
            Icon = 3,
            Minimum = 4,
            Rankings = 2
        }

        public NobilityTypes Type
        {
            get
            {
                return (NobilityTypes)BitConverter.ToUInt32(this.mData, 4);
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write((uint)value, 4, this.mData);
            }
        }

        public uint wParam1
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

        public uint wParam2
        {
            get
            {
                return BitConverter.ToUInt32(this.mData, 0x10);
            }
            set
            {
                Nyx.Server.Network.SafeWriter.Write(value, 0x10, this.mData);
            }
        }
    }
}
