using Nyx.Server;
using Nyx.Server.Network;
using System;
using Nyx.Server.Client;
using Nyx.Server.Interfaces;

namespace Nyx.Server.Network.GamePackets
{
    public class SubClassShow : Writer
    {
        private ushort Position = (ushort)30;
        public const byte SwitchSubClass = (byte)0;
        public const byte ActivateSubClass = (byte)1;
        public const byte Upgrade = (byte)2;
        public const byte SendUpdate = (byte)3;
        public const byte LearnSubClass = (byte)4;
        public const byte MartialPromoted = (byte)5;
        public const byte Open = (byte)6;
        public const byte ShowGUI = (byte)7;
        public const byte Animation = (byte)8;
        public const byte Join = (byte)9;
        public const byte Pro = (byte)10;
        private byte[] packet;

        public ushort ID
        {
            get
            {
                return BitConverter.ToUInt16(this.packet, 8);
            }
            set
            {
                Writer.Write(value, 8, this.packet);
            }
        }

        public byte Class
        {
            get
            {
                return this.packet[10];
            }
            set
            {
                Writer.Write(value, 10, this.packet);
            }
        }

        public byte Level
        {
            get
            {
                return this.packet[11];
            }
            set
            {
                Writer.Write(value, 11, this.packet);
            }
        }

        public ushort Study
        {
            get
            {
                return BitConverter.ToUInt16(this.packet, 10);
            }
            set
            {
                Writer.Write(value, 10, this.packet);
            }
        }

        public ushort StudyReceive
        {
            get
            {
                return BitConverter.ToUInt16(this.packet, 18);
            }
            set
            {
                Writer.Write(value, 18, this.packet);
            }
        }

        public ushort Count
        {
            get
            {
                return BitConverter.ToUInt16(this.packet, 26);
            }
            set
            {
                Writer.Write(value, 26, this.packet);
            }
        }

        public SubClassShow(ushort entry = (ushort) 0)
        {
            this.packet = new byte[38 + (int)entry * 3];
            Writer.Write((ushort)(this.packet.Length - 8), 0, this.packet);
            Writer.Write((ushort)2320, 2, this.packet);
            Writer.Write((uint)Time32.timeGetTime().GetHashCode(), 4, this.packet);
            if ((int)entry == 0)
                return;
            this.Count = entry;
        }

        public void Apprend(byte ID, byte Pharse, byte Level)
        {
            if ((int)(ushort)(this.packet.Length - 8) < (int)this.Position + 3)
                return;
            Writer.Write(ID, (int)this.Position++, this.packet);
            Writer.Write(Pharse, (int)this.Position++, this.packet);
            Writer.Write(Level, (int)this.Position++, this.packet);
        }

        public byte[] Encode()
        {
            return this.packet;
        }
    }
    public class Game_SubClass : IPacket
    {
        private Byte[] mData;

        public Game_SubClass()
        {
            this.mData = new Byte[29 + 8];
            Writer.Write((UInt16)(mData.Length - 8), 0, mData);
            Writer.Write((UInt16)2320, 2, mData);
        }

        public Types Type
        {
            get { return (Types)BitConverter.ToUInt16(mData, 4); }
            set { Writer.Write((Byte)value, 4, mData); }
        }
        public ushort StudyReceive
        {
            get { return BitConverter.ToUInt16(mData, 18); }
            set { Writer.Write((Byte)value, 18, mData); }
        }
        public ID ClassId
        {
            get { return (ID)mData[6]; }
            set { mData[6] = (Byte)value; }
        }
        public Byte Phase
        {
            get { return mData[7]; }
            set { mData[7] = value; }
        }
        public void Deserialize(byte[] buffer)
        {
            this.mData = buffer;
        }

        public byte[] Encode()
        {
            return mData;
        }

        public void Send(Client.GameClient c)
        {
            c.Send(mData);
        }

        public enum Types : ushort
        {
            Switch = 0,
            Activate = 1,
            Upgrade = 2,
            Learn = 4,
            MartialPromoted = 5,
            Show = 6,
            Info = 7
        }
        public enum ID : byte
        {
            None = 0,
            MartialArtist = 1,
            Warlock = 2,
            ChiMaster = 3,
            Sage = 4,
            Apothecary = 5,
            Performer = 6,
            Wrangler = 9
        }
    }
    public class SubClassShowFull : Writer, IPacket
    {
        public const byte SwitchSubClass = (byte)0;
        public const byte ActivateSubClass = (byte)1;
        public const byte LearnSubClass = (byte)4;
        public const byte Effect = (byte)5;
        public const byte ShowGUI = (byte)7;
        private byte[] Buffer;

        public ushort ID
        {
            get
            {
                return BitConverter.ToUInt16(this.Buffer, 8);
            }
            set
            {
                Writer.Write(value, 8, this.Buffer);
            }
        }

        public byte Class
        {
            get
            {
                return this.Buffer[10];
            }
            set
            {
                this.Buffer[10] = value;
            }
        }

        public byte Level
        {
            get
            {
                return this.Buffer[11];
            }
            set
            {
                this.Buffer[11] = value;
            }
        }

        public SubClassShowFull(bool Create)
        {
            if (!Create)
                return;
            this.Buffer = new byte[38];
            Writer.Write((ushort)30, 0, this.Buffer);
            Writer.Write((ushort)2320, 2, this.Buffer);
            Writer.Write((uint)Time32.timeGetTime().GetHashCode(), 4, this.Buffer);
        }

        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
        }

        public byte[] Encode()
        {
            return this.Buffer;
        }

        public void Send(GameClient c)
        {
            c.Send(this.Buffer);
        }
        public ushort Study
        {
            get
            {
                return BitConverter.ToUInt16(this.Buffer, 10);
            }
            set
            {
                Network.SafeWriter.Write(value, 10, this.Buffer);
            }
        }
        public ushort StudyReceive
        {
            get
            {
                return BitConverter.ToUInt16(this.Buffer, 0x12);
            }
            set
            {
                Network.SafeWriter.Write(value, 0x12, this.Buffer);
            }
        }
    }
    public class Game_SubClassInfo : IPacket
    {
        private Byte[] mData;
        private Int32 Offset = 26 + 4;

        public Game_SubClassInfo(Game.Entity c, Game_SubClass.Types Type)
        {
            this.mData = new Byte[26 + 4 + (c.SubClasses.Classes.Count * 3) + 8];
            Writer.Write((UInt16)(mData.Length - 8), 0, mData);
            Writer.Write((UInt16)2320, 2, mData);

            Writer.Write((Byte)Type, 4 + 4, mData);
            Writer.Write((UInt16)c.SubClasses.StudyPoints, 6 + 4, mData);
            Writer.Write((UInt16)c.SubClasses.Classes.Count, 22 + 4, mData);


            foreach (Game.SubClass subc in c.SubClasses.Classes.Values)
            {
                Writer.Write((Byte)subc.ID, Offset, mData); Offset++;
                Writer.Write((Byte)subc.Phase, Offset, mData); Offset++;
                Writer.Write((Byte)subc.Level, Offset, mData); Offset++;
            }
        }
        public void Deserialize(byte[] buffer)
        {
            this.mData = buffer;
        }

        public byte[] Encode()
        {
            return mData;
        }

        public void Send(Client.GameClient c)
        {
            c.Send(mData);
        }

    }
}
