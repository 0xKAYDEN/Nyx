using Nyx.Server.Client;
using Nyx.Server.Network.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Domain.MsgNpc
{
    public class NpcHandler
    {
        [NpcAttribute(NpcID.HuntersGuild)]
        public static void HuntersGuild(GameClient client, byte Option, string Input, uint id)
        {

        }
    }
}
