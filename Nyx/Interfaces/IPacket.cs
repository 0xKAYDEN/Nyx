using System;

namespace Nyx.Server.Interfaces
{
    public interface IPacket
    {
        byte[] Encode();
        void Deserialize(byte[] buffer);
        void Send(Client.GameClient client);
    }
}
