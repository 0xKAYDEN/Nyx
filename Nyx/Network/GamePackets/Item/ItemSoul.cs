using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Nyx.Server.Network.GamePackets
{
    public class Game_ItemSoul : Interfaces.IPacket
    {
        private Byte[] mData;
        public Game_ItemSoul()
        {
            this.mData = new Byte[40 + 8]; Writer.Write((UInt16)(this.mData.Length - 8), 0, mData);
            Writer.Write((UInt16)2077, 2, mData);
        }
        public UInt32 Type
        {
            get
            {
                return BitConverter.ToUInt32(mData, 4);
            }
            set
            {
                Writer.Write(value, 4, mData);
            }
        }
        public UInt32 Identifier
        {
            get
            {
                return BitConverter.ToUInt32(mData, 8);
            }
            set
            {
                Writer.Write(value, 8, mData);
            }
        }
        public Types Mode
        {
            get
            {
                return (Types)mData[12];
            }
            set
            {
                mData[12] = (Byte)value;
            }
        }
        public UInt32 ID
        {
            get
            {
                return BitConverter.ToUInt32(mData, 16);
            }
            set
            {
                Writer.Write(value, 16, mData);
            }
        }
        public UInt32 Level
        {
            get
            {
                return BitConverter.ToUInt32(mData, 20);
            }
            set
            {
                Writer.Write(value, 20, mData);
            }
        }
        public UInt32 Percent
        {
            get
            {
                return BitConverter.ToUInt32(mData, 24);
            }
            set
            {
                Writer.Write(value, 24, mData);
            }
        }
        public UInt32 Time
        {
            get
            {
                return BitConverter.ToUInt32(mData, 28);
            }
            set
            {
                Writer.Write(value, 32, mData);
            }
        }
        public void Expired(Client.GameClient c)
        {
            mData[12] = 7; c.Send(this);
        }
        public Byte[] Serialize
        {
            get
            {
                return this.mData;
            }
        }
        public void Deserialize(Byte[] d)
        {
            this.mData = new Byte[d.Length]; d.CopyTo(this.mData, 0);
        }
        public enum Types :
            byte
        {
            Refine = 0, DragonSoul = 5
        }
        public byte[] Encode()
        {
            return mData;
        }
        public void Send
            (Client.GameClient client)
        {
            client.Send(mData);
        }
    }
}