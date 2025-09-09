using System;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class TwoMovements : Writer, Interfaces.IPacket
    {
        public const byte
            Direction = 0,
            Walk = 1,
            Jump = 2;

        private byte[] Buffer;

        public TwoMovements()
        {
            Buffer = new byte[32];
            Writer.Write(24, 0, Buffer);
            Writer.Write(1114, 2, Buffer);
        }

        public ushort MovementType
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set {Writer.Write(value, 4, Buffer); }
        }

        public Game.Enums.ConquerAngle Facing
        {
            get { return (Game.Enums.ConquerAngle)BitConverter.ToUInt16(Buffer, 6); }
            set {Writer.Write((ushort)value, 6, Buffer); }
        }

        public ushort X
        {
            get { return BitConverter.ToUInt16(Buffer, 6); }
            set {Writer.Write(value, 6, Buffer); }
        }

        public ushort WalkType
        {
            get { return BitConverter.ToUInt16(Buffer, 7); }
            set {Writer.Write(value, 7, Buffer); }
        }

        public bool IsRunning
        {
            get { return BitConverter.ToUInt16(Buffer, 8) == 1; }
            set {Writer.Write(value == true ? (ushort)1 : (ushort)0, 8, Buffer); }
        }

        public ushort Y
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set {Writer.Write(value, 8, Buffer); }
        }

        public uint EntityCount
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint FirstEntity
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public uint SecondEntity
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set { Write(value, 20, Buffer); }
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
