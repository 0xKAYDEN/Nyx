using Nyx.Server.Client;
using Nyx.Server.Network;
using Nyx.Server.Network.GamePackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Packets
{
    public class MsgLogin
    {
        [Network.Packet((ushort)Network.PacketType.MsgLogin)]
        public static async Task Process(GameClient client, byte[] packet)
        {
            if (client.Action == 1)
            {
                Connect connect = new Connect();
                connect.Deserialize(packet);
                PacketHandler.AppendConnect(connect, client);
            }
            else
                client.Disconnect();
        }
    }
}
