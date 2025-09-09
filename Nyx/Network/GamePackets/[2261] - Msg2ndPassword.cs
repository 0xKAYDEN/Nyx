using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class WareHousePassword : Writer, Interfaces.IPacket
    {
        public const byte Terminate = 0,
            VerifiedPassword = 1,
            ForgetPassword = 2,
            SetNewPass = 3,
            SendInformation = 4,
            PasswordCorrect = 5,
            PasswordWrong = 6,
            Termination = 8;

        public enum Nextaction
        {
            Nothing,
            OpenWarehouse,
            OwnBooth
        }

        private byte[] Buffer;

        public WareHousePassword(bool create)
        {
            if (create)
            {
                Buffer = new byte[16 + 8];
                Writer.Write(16, 0, Buffer);
                Writer.Write(2261, 2, Buffer);
            }
        }

        public byte type
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }

        public uint OldPassword
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public uint NewPassword
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}