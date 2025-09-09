using MySqlX.XDevAPI;
using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Packets
{
    public class MsgPrestigeRanking
    {
        [Network.Packet((ushort)Network.PacketType.MsgPrestigeRanking)]
        private static async Task Process(GameClient client, byte[] packet)
        {
            var pkt = new MsgRankMemberShow();
            var myPacket = new byte[packet.Length - 8];
            for (int i = 0; i < myPacket.Length; i++)
            {
                myPacket[i] = packet[i];
            }
            if (pkt.Read(myPacket))
                pkt.Handle(client);
            
        }
    }
}
