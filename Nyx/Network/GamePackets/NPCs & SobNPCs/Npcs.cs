using System;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class NpcRequest : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            throw new NotImplementedException();
        }

        public uint NpcID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public byte OptionID
        {
            get { return Buffer[14]; }
            set { Buffer[14] = value; }
        }

        public byte InteractType
        {
            get { return Buffer[15]; }
        }

        public string Input
        {
            get { return Encoding.Default.GetString(Buffer, 18, Buffer[17]); }
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
    public class NpcReply : Writer, Interfaces.IPacket
    {
        public const byte
            Dialog = 1,
            Option = 2,
            Input = 3,
            Avatar = 4,
            MessageBox = 6,
            Finish = 100;

        private byte[] Buffer;

        public NpcReply()
        {
            Buffer = new byte[28];
            Writer.Write((ushort)20, 0, Buffer);
            Writer.Write(2032, 2, Buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
        }
        public NpcReply(byte interactType, string text)
        {
            Buffer = new byte[29];
            Writer.Write((ushort)(21 + text.Length), 0, Buffer);
            Writer.Write(2032, 2, Buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            InteractType = interactType;
            OptionID = 255;
            DontDisplay = true;
            Text = text;
        }
        public void Reset()
        {
            OptionID = 255;
            DontDisplay = true;
            Text = "";
        }

        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        /// <summary>
        /// This should be the max length of the input string if the interact type is
        /// `Input`. Otherwise, if it is neither of these two, it should be 0.
        /// </summary>
        public ushort InputMaxLength
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }
        public byte OptionID
        {
            get { return Buffer[14]; }
            set { Buffer[14] = value; }
        }
        public byte InteractType
        {
            get { return Buffer[15]; }
            set { Buffer[15] = value; }
        }
        /// <summary>
        /// This should be set to false when your sending the packet with the
        /// interaction type `Finish`, otherwise true
        /// </summary>
        public bool DontDisplay
        {
            get { return (Buffer[16] == 1); }
            set { Buffer[16] = (byte)(value ? 1 : 0); }
        }
        public string Text
        {
            get { return Encoding.Default.GetString(Buffer, 18, Buffer[17]); }
            set
            {
                int realloc = value.Length + 12 + 17;
                if (realloc != Buffer.Length)
                {
                    byte[] new_Packet = new byte[realloc];
                    System.Buffer.BlockCopy(Buffer, 0, new_Packet, 0, 28);
                    Buffer = new_Packet;
                }
                Writer.Write((ushort)(value.Length + 21), 0, Buffer);
                Writer.WriteWithLength(value, 17, Buffer);
            }
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
    public class NpcSpawn : Writer, Interfaces.IPacket, Interfaces.INpc, Interfaces.IMapObject
    {
        private byte[] Buffer;
        private ushort _MapID;
        private string _Name;
        public NpcSpawn(bool Created = true)
        {
            if (Created)
            {
                Buffer = new byte[36];
                Writer.Write(28, 0, Buffer);
                Writer.Write(2030, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            }

        }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public ushort X
        {
            get { return BitConverter.ToUInt16(Buffer, 16); }
            set { Writer.Write(value, 16, Buffer); }
        }

        public ushort Y
        {
            get { return BitConverter.ToUInt16(Buffer, 18); }
            set { Writer.Write(value, 18, Buffer); }
        }

        public ushort Mesh
        {
            get { return BitConverter.ToUInt16(Buffer, 20); }
            set { Writer.Write(value, 20, Buffer); }
        }

        public Nyx.Server.Game.Enums.NpcType Type
        {
            get { return (Nyx.Server.Game.Enums.NpcType)Buffer[22]; }
            set { Buffer[22] = (byte)value; }
        }

        public ushort MapID { get { return _MapID; } set { _MapID = value; } }

        public Nyx.Server.Game.MapObjectType MapObjType { get { return Nyx.Server.Game.MapObjectType.Npc; } }

        public Client.GameClient Owner { get { return null; } }

        public void SendSpawn(Client.GameClient client, bool checkScreen)
        {
            if (client.Screen.Add(this) || !checkScreen)
            {
                client.Send(Buffer);
            }
        }
        public void SendSpawn(Client.GameClient client)
        {
            SendSpawn(client, false);
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;

                byte[] buffer = new byte[90];
                Buffer.CopyTo(buffer, 0);
                Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
                buffer[32] = 1;
                Writer.WriteWithLength(value, 33, buffer);
                Buffer = buffer;
            }
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
            SendSpawn(client, false);
        }
    }
}
