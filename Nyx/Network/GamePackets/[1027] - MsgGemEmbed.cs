using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class EmbedSocket : Writer, Interfaces.IPacket
    {
        public const ushort Add = 0,
            Remove = 1,
            SlotOne = 1,
            SlotTwo = 2;

        private byte[] Buffer;
        private int num = 4;

        public EmbedSocket(bool Create)
        {
            if (Create)
            {
                Buffer = null;
            }
        }

        public uint ItemUID
        {
            get { return BitConverter.ToUInt32(Buffer, 8 + num); }
            set { Write(value, 8 + num, Buffer); }
        }

        public uint GemUID
        {
            get { return BitConverter.ToUInt32(Buffer, 12 + num); }
            set { Write(value, 12 + num, Buffer); }
        }

        public ushort Slot
        {
            get { return BitConverter.ToUInt16(Buffer, 16 + num); }
            set {Writer.Write(value, 16 + num, Buffer); }
        }

        public ushort Mode
        {
            get { return BitConverter.ToUInt16(Buffer, 18 + num); }
            set {Writer.Write(value, 18 + num, Buffer); }
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
}