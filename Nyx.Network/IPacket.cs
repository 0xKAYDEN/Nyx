using Nyx.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Network
{
    public interface IPacket
    {
        public ushort Length { get; }
        public PacketType Type { get; }
        void Decode(byte[] bytes);
        byte[] Encode();
    }
}
