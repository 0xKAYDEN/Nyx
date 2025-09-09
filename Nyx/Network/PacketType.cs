using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Network
{
    public enum PacketType : ushort
    {
        MsgPrestigeRanking = 3257,
        MsgItem = 1009,
        MsgQuest = 1134,
        MsgLogin = 1052,
    }
}
