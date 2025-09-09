using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class OfflineTGStats : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        public OfflineTGStats(bool create)
        {
            if (create)
            {
                Buffer = new byte[8 + 20];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(2043, 2, Buffer);
            }
        }

        public ushort TrainedMinutes
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set {Writer.Write(value, 4, Buffer); }
        }

        public ushort TotalTrainingMinutesLeft
        {
            get { return BitConverter.ToUInt16(Buffer, 6); }
            set {Writer.Write(value, 6, Buffer); }
        }

        public byte Character_AcquiredLevel
        {
            get { return Buffer[8]; }
            set { Buffer[8] = value; }
        }

        public ulong Character_NewExp
        {
            get { return BitConverter.ToUInt64(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] source)
        {
            Buffer = source;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
    public class OfflineTGRequest : Writer, Interfaces.IPacket
    {
        public const byte
            OnTrainingTimeRequested = 0,
            OnConfirmation = 1,
            ReplyToConfirmation = 3,
            ClaimExperience = 4;

        private byte[] Buffer;
        public OfflineTGRequest(bool create)
        {
            if (create)
            {
                Buffer = new byte[8 + 12];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(2044, 2, Buffer);
            }
        }

        public byte ID
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public ushort Minutes
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] source)
        {
            Buffer = source;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}