using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class TitlePacket : Writer, Interfaces.IPacket
    {
        private Byte[] mData;
        private Int32 Offset = 11;

        public TitlePacket(Boolean create)
        {
            if (create)
            {
                mData = new Byte[24 + 8];
                Writer.Write((UInt16)(mData.Length - 8), 0, mData);
                Writer.Write((UInt16)1130, 2, mData);
            }
        }
        public UInt32 Identifier
        {
            get { return BitConverter.ToUInt32(mData, 4); }
            set { Writer.Write((UInt32)value, 4, mData); }
        }
        public Titles Title
        {
            get { return (Titles)mData[8]; }
            set { mData[8] = (Byte)value; }
        }
        public Types Type
        {
            get { return (Types)mData[9]; }
            set { Writer.Write((UInt16)value, 9, mData); }
        }
        public Byte Count
        {
            get { return mData[10]; }
            set { mData[10] = value; }
        }
        public void Add(Byte id)
        {
            Byte[] tmp = new Byte[mData.Length];
            mData.CopyTo(tmp, 0);

            mData = new Byte[tmp.Length + 1 + 8];
            Buffer.BlockCopy(tmp, 0, mData, 0, tmp.Length);

            Writer.Write((UInt16)(mData.Length - 8), 0, mData);

            Writer.Write(id, Offset, mData);

            Offset++;
        }
        public void Deserialize(byte[] buffer)
        {
            this.mData = buffer;
        }
        public byte[] Encode()
        {
            return mData;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(mData);
        }
        public enum Types : ushort
        {
            Switch = 3,
            List = 4
        }
        public enum Titles : ushort
        {
            None = 0,

            GoldenRacer = 11,

            ElitePKChamption_Low = 12,
            ElitePK2ndPlace_Low = 13,
            ElitePK3ndPlace_Low = 14,
            ElitePKTopEight_Low = 15,

            ElitePKChamption_High = 16,
            ElitePK2ndPlace_High = 17,
            ElitePK3ndPlace_High = 18,
            ElitePKTopEight_High = 19,

            Team_PK_Champion__Low_ = 20,
            Team_PK_2nd_Place_Low_ = 21,
            Team_PK_3rd_Place_Low_ = 22,
            Team_PK_Top_8__Low_ = 23,

            Team_PK_Champion_High_ = 24,
            Team_PK_2nd_Place_High_ = 25,
            Team_PK_3rd_Place__High_ = 26,
            Team_PK_Top_8_High_ = 27,

            Skill_PK_Champion__Low_ = 28,
            Skill_PK_2nd_Place_Low_ = 29,
            Skill_PK_3rd_Place_Low_ = 30,
            Skill_PK_Top_8__Low_ = 31,

            Skill_PK_Champion_High_ = 32,
            Skill_PK_2nd_Place_High_ = 33,
            Skill_PK_3rd_Place__High_ = 34,
            Skill_PK_Top_8_High_ = 35,
        }
    }
}