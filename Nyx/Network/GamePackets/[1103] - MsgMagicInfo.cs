using System;
using System.Collections.Generic;
using System.Linq;

namespace Nyx.Server.Network.GamePackets
{
    public class Spell : Writer, Interfaces.IPacket, Interfaces.ISkill
    {
        byte[] Buffer;
        private byte _PreviousLevel;
        private bool _Available;
        public Spell(bool Create)
        {
            Buffer = new byte[32 + 8];
            Writer.Write(32, 0, Buffer);
            Writer.Write(1103, 2, Buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            Buffer[20] = 1;
            _Available = false;
        }
        public ushort ID
        {
            get { return (ushort)BitConverter.ToUInt16(Buffer, 12); }
            set {Writer.Write(value, 12, Buffer); }
        }

        public byte Level
        {
            get { return (byte)BitConverter.ToUInt32(Buffer, 16); }
            set
            {
                if (Database.SpellTable.SpellInformations != null)
                    if (Database.SpellTable.SpellInformations.ContainsKey(ID))
                        if (!Database.SpellTable.SpellInformations[ID].ContainsKey(value))
                            value = Database.SpellTable.SpellInformations[ID].Keys.LastOrDefault();
                Writer.Write(value, 16, Buffer);
            }
        }

        byte _Levelhu = 0;
        public byte LevelHu
        {
            get { return _Levelhu; }
            set
            {
                _Levelhu = value;
                if (value >= 1)
                    Buffer[24] = 3;
                if (value >= 2)
                    Buffer[24] = 7;
                if (value >= 3)
                    Buffer[24] = 15;
                if (value >= 4)
                    Buffer[24] = 31;
            }
        }

        public byte LevelHu2
        {
            get;
            set;
        }

        public byte TempLevel
        {
            get
            {
                return (byte)BitConverter.ToUInt16(this.Buffer, 0x10 + 4);
            }
            set
            {
                Writer.Write(value, 0x10 + 4, this.Buffer);
            }
        }
        public byte PreviousLevel
        {
            get { return _PreviousLevel; }
            set { _PreviousLevel = value; }
        }

        public uint Experience
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(this);
        }

        public bool Available
        {
            get
            {
                return _Available;
            }
            set
            {
                _Available = value;
            }
        }
    }
}