using System;
using System.Collections.Generic;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class _String : Writer, Interfaces.IPacket
    {
        public const byte GuildName = 3,
        Spouse = 6,
        Effect = 10,
        Effect2 = 30,
        StartGamble = 17,
        EndGamble = 18,
        GuildList = 11,
        Unknown = 13,
        ViewEquipSpouse = 16,
        Sound = 20,
        GuildAllies = 21,
        GuildEnemies = 22,
        WhisperDetails = 26;

        byte[] Buffer;

        public const byte MapEffect = 9;
        public ushort PositionX
        {
            get { return (ushort)UID; }
            set { UID = (uint)((PositionY << 16) | value); }
        }

        public ushort PositionY
        {
            get { return (ushort)(UID >> 16); }
            set { UID = (uint)((value << 16) | PositionX); }
        }


        public _String(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[23];
                Writer.Write(15, 0, Buffer);
                Writer.Write(1015, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
                Texts = new List<string>();
            }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public ushort X
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public ushort Y
        {
            get { return BitConverter.ToUInt16(Buffer, 10); }
            set { Write(value, 8, Buffer); }
        }
        public byte Type
        {
            get { return Buffer[12]; }
            set { Buffer[12] = value; }
        }
        public byte TextsCount
        {
            get { return Buffer[13]; }
            set { Buffer[13] = value; }
        }
        public List<string> Texts;

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            ushort entirelength = 23;
            foreach (string list in Texts)
                entirelength += (ushort)list.Length;
            byte[] buffer = new byte[entirelength];
            Writer.Write((ushort)(entirelength - 8), 0, buffer);
            Writer.Write(1015, 2, buffer);
            Write(UID, 8, buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, buffer);
            buffer[12] = Type;
            Buffer = buffer;
            Writer.Write(Texts, 13, Buffer);
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
            Texts = new List<string>(buffer[13]);
            ushort offset = 14;
            byte count = 0;
            while (count != TextsCount)
            {
                ushort textlength = buffer[offset]; offset++;
                string text = Program.Encoding.GetString(buffer, offset, textlength); offset += textlength;
                Texts.Add(text);
                count++;
            }
        }
    }
}
