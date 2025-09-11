using Nyx.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Packets
{
    public class MsgReloadScreen
    {
        [Network.Packet(1037)]
        public static async Task Process(GameClient client, byte[] packet)
        {
            if (client.Action != 2) return;
            client.SendScreen(packet, true);
            client.Screen.Reload(null);
        }
    }
}
