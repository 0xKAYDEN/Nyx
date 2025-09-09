using System;
using Nyx.Server.Network.AuthPackets;
namespace Nyx.Server.Network.GamePackets
{
    public class Connect : Interfaces.IPacket
    {
        byte[] Buffer;
        public uint Identifier
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }
        public Forward.ForwardType Type
        {
            get
            {
                return (Forward.ForwardType)(byte)BitConverter.ToUInt32(Buffer, 8);
            }
            set
            {
                Network.SafeWriter.Write((byte)value, 8, Buffer);
            }
        }
        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
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
