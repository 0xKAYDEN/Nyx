using System;
using System.Text;
namespace Nyx.Server.Network.AuthPackets
{
    public class PasswordCryptographySeed : Interfaces.IPacket
    {
        byte[] Buffer;
        public PasswordCryptographySeed()
        {
            Buffer = new byte[8];
            Network.SafeWriter.Write(8, 0, Buffer);
            Network.SafeWriter.Write(1059, 2, Buffer);
        }
        public int Seed
        {
            get
            {
                return BitConverter.ToInt32(Buffer, 4);
            }
            set
            {
                Network.SafeWriter.Write(value, 4, Buffer);
            }
        }

        public void Deserialize(byte[] buffer)
        {
            //no implementation
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}
