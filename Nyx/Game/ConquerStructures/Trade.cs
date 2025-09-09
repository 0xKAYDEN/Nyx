using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.ConquerStructures
{
    public class Trade
    {
        public uint Money, ConquerPoints, TraderUID;
        public List<ConquerItem> Items;
        public bool Accepted, InTrade;
        public Trade()
        {
            InTrade = Accepted = false;
            ConquerPoints = Money = TraderUID = 0;
            Items = new List<ConquerItem>();
        }
    }
}
