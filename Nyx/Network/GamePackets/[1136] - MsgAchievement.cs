using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Game;
using Nyx.Server.Network;
using Nyx.Server.Database;

namespace Nyx.Server.Game
{
    public class BitVector32
    {
        public uint[] bits;

        public int Size { get { return 32 * bits.Length; } }

        public BitVector32(int BitCount)
        {
            int sections = BitCount / 32;
            if (BitCount % 32 != 0)
                sections += 1;
            bits = new uint[sections];
        }

        public void Add(int index)
        {
            if (index < Size)
            {
                int idx = index / 32;
                uint bites = (uint)(1 << (index % 32));
                bits[idx] |= bites;
            }
        }
        public void Remove(int index)
        {
            if (index < Size)
            {
                int idx = index / 32;
                uint bites = (uint)(1 << (index % 32));
                bits[idx] &= ~bites;
            }
        }
        public bool Contain(int index)
        {
            if (index > Size) return false;
            int idx = index / 32;
            uint bites = (uint)(1 << (index % 32));
            return ((bits[idx] & bites) == bites);
        }
        public void Clear()
        {
            ushort siz = (byte)(Size / 32);
            for (byte x = 0; x < siz; x++)
            {
                bits[x] = 0;
            }
        }
    }
    public class Achievement : Nyx.Server.Network.Writer
    {
        public const byte File_Size = 13;

        private Byte[] _buffer;
        private Entity Object;
        private BitVector32 BitVector32;
        public Achievement(Entity _obj)
        {
            Object = _obj;
            _buffer = new byte[76];
         Writer.Write(68, 0, _buffer);
         Writer.Write(1136, 2, _buffer);
            Write(Object.UID, 8, _buffer);
            BitVector32 = new BitVector32(32 * File_Size);
            Write(File_Size, 12, _buffer);
        }
        public void CreateFlaID(int id)
        {
            int flagid = (int)((id / 100) % 100 - 1) * 32 + (int)(id % 100 - 1);
            AddFlag(flagid);
        }
        public void AddFlag(int flag)
        {
            if (!BitVector32.Contain(flag))
            {
                BitVector32.Add(flag);

                ShowScreen(flag);
                Send();
            }
        }
        public void Send()
        {
            for (byte x = 0; x < BitVector32.bits.Length; x++)
                Write(BitVector32.bits[x], (ushort)(16 + 4 * x), _buffer);
            Object.Owner.Send(_buffer);
        }
        public int Count(uint UID)
        {
            var c = 0;
            try
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("achievement").Where("UID", UID))
                using (var reader = new MySqlReader(cmd))
                {
                    if (reader.Read())
                    {
                        var line = reader.ReadString("Achievement");
                        c = line.Split('#').Length;
                    }
                }
            }
            catch { }
            return c;
        }
        private void ShowScreen(int flag)
        {
            uint FRAG_ID = (uint)(10100 + (uint)(100 * (byte)(flag / 32)) + (byte)(flag % 32) + 1);
            byte[] data = new byte[28];
            Writer.Write(20, 0, data);
            Writer.Write(1136, 2, data);
            Write(2, 4, data);
            Write(Object.UID, 8, data);
            Write(FRAG_ID, 12, data);
            Object.Owner.Send(data);
            using (var cmd = new Database.MySqlCommand(MySqlCommandType.SELECT).Select("achievement").Where("UID", Object.Owner.Account.EntityID))
            using (var Reader = new MySqlReader(cmd))
            {
                while (Reader.Read())
                {
                    using (var cmd1 = new Database.MySqlCommand(MySqlCommandType.UPDATE))
                        cmd1.Update("achievement").Set("Achievement", Object.MyAchievement.ToString()).Where("UID", Object.UID).Execute();
                }
            }
            string Mesajje = "" + Object.Name + " received [Achievement " + FRAG_ID + "]! ";
            Network.GamePackets.Message mesaj = new Network.GamePackets.Message(Mesajje, Object.Name, global::System.Drawing.Color.Red, Network.GamePackets.Message.System);
            Object.Owner.Send(mesaj.Encode());
        }
        public override string ToString()
        {
            string line = "";
            foreach (uint bits in BitVector32.bits)
                line += bits.ToString() + "#";
            return line;
        }
        public byte[] Encode()
        {
            return _buffer;
        }
        public byte[] ViewOpen()
        {
            byte[] packet = _buffer.ToArray();
            packet[4] = 1;
            return packet;
        }
        public void Load(string bas_line)
        {
            if (bas_line.Length == 0) return;
            string[] line = bas_line.Split('#');
            for (byte x = 0; x < 13; x++)
                BitVector32.bits[x] = uint.Parse(line[x]);
        }
        public static bool Load(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("achievement").Where("UID", client.Account.EntityID))
            using (var reader = new MySqlReader(cmd))
            {
                if (reader.Read())
                {
                    client.Entity.MyAchievement = new Achievement(client.Entity);
                    client.Entity.MyAchievement.Load(reader.ReadString("Achievement"));
                }
                else
                {
                    using (var com = new Database.MySqlCommand(MySqlCommandType.INSERT))
                        com.Insert("Achievement").Insert("UID", (long)client.Entity.UID)
                            .Insert("Owner", client.Account.Username)
                            .Insert("Name", client.Entity.Name).Execute();
                    client.Entity.MyAchievement = new Achievement(client.Entity);
                }
                return true;
            }
        }
    }
}
