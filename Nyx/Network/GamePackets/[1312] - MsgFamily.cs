using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Interfaces;


namespace Nyx.Server.Network.GamePackets
{
    public class ClanMembers : Writer, IPacket
    {
        private Byte[] mData;
        private Int32 Offset = 16;

        public ClanMembers(Clan clan)
        {
            mData = new Byte[56 + (36 * clan.Members.Count) + 8];
            Writer.Write((UInt16)(mData.Length - 8), 0, mData);
            Writer.Write((UInt16)1312, 2, mData);

            UInt32 Count = (UInt32)clan.Members.Count();
            Write(Count, Offset, mData); Offset += 4;
            foreach (ClanMember member in clan.Members.Values)
            {

                Writer.Write(member.Name, Offset, mData); Offset += 16;
                Write((UInt32)member.Level, Offset, mData); Offset += 4;
                Writer.Write(Convert.ToUInt16(member.Rank), Offset, mData); Offset += 2;
                Writer.Write(Convert.ToUInt16(Kernel.GamePool.ContainsKey(member.Identifier)), Offset, mData); Offset += 2;
                Write((UInt32)member.Class, Offset, mData); Offset += 8;
                Write((UInt32)member.Donation, Offset, mData); Offset += 4;

                Count -= 1;
            }
            Offset = 16;
            Type = Clan.Types.Members;
        }
        public Clan.Types Type
        {
            get { return (Clan.Types)mData[4]; }
            set { Write((Byte)value, 4, mData); }
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
    }
    public class ClanAnnouncement : Writer, IPacket
    {
        private Byte[] mData;
        public ClanAnnouncement(Clan clan)
        {
            if (clan.Announcement == null) clan.Announcement = "";
            mData = new Byte[85 + clan.Announcement.Length + 8];
            Writer.Write((UInt16)(mData.Length - 8), 0, mData);
            Writer.Write(1312, 2, mData);

            Write((Byte)24, 4, mData);
            Writer.Write((UInt16)clan.ID, 8, mData);
            Write((Byte)1, 16, mData);
            Writer.WriteWithLength(clan.Announcement, 17, mData);
        }
        public void Send(Client.GameClient client)
        {
            client.Send(mData);
        }

        public byte[] Encode()
        {
            return mData;
        }

        public void Deserialize(byte[] buffer)
        {
            mData = buffer;
        }
    }
    public class ClanRelations : Writer, IPacket
    {
        private Byte[] mData;
        private Int32 Offset = 20;

        public ClanRelations(Clan clan, RelationTypes type)
        {
            switch (type)
            {
                case RelationTypes.Allies:
                    {
                        mData = new Byte[28 + (clan.Allies.Count * 56) + 8];
                        Writer.Write((UInt16)(mData.Length - 8), 0, mData);
                        Writer.Write(1312, 2, mData);
                        Write((Byte)type, 4, mData);

                        Write((Byte)clan.Allies.Count, 16, mData);

                        foreach (Clan clans in clan.Allies.Values)
                        {
                            Writer.Write((UInt16)clans.ID, Offset, mData); Offset += 4;
                            Writer.Write(clans.Name, Offset, mData); Offset += 36;
                            Writer.Write(clans.LeaderName, Offset, mData); Offset += 16;
                        }
                        Offset = 20;
                        break;
                    }
                case RelationTypes.Enemies:
                    {
                        mData = new Byte[28 + (clan.Enemies.Count * 56) + 8];
                        Writer.Write((UInt16)(mData.Length - 8), 0, mData);
                        Writer.Write(1312, 2, mData);
                        Write((Byte)type, 4, mData);

                        Write((Byte)clan.Enemies.Count, 16, mData);

                        foreach (Clan clans in clan.Enemies.Values)
                        {
                            Writer.Write((UInt16)clans.ID, Offset, mData); Offset += 4;
                            Writer.Write(clans.Name, Offset, mData); Offset += 36;
                            Writer.Write(clans.LeaderName, Offset, mData); Offset += 16;
                        }
                        Offset = 20;
                        break;
                    }
            }
        }

        public enum RelationTypes : byte
        {
            Enemies = 13,
            Allies = 16
        }

        public void Send(Client.GameClient client)
        {
            client.Send(mData);
        }

        public byte[] Encode()
        {
            return mData;
        }

        public void Deserialize(byte[] buffer)
        {
            mData = buffer;
        }
    }
}
