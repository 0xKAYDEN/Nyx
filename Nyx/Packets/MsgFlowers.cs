using Nyx.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Packets
{
    public class MsgFlowers
    {
        public static ulong ClientSeal = BitConverter.ToUInt64(Encoding.Default.GetBytes("TQClient"), 0);

        [Network.Packet(1150)]
        [Network.Packet(1151)]
        public static async Task Process(GameClient client, byte[] packet)
        {
            //using (MemoryStream ms = new MemoryStream)
            //{

            //}
                ushort Length = BitConverter.ToUInt16(packet, 0);
            ushort ID = BitConverter.ToUInt16(packet, 2);
            ushort TypeP4 = BitConverter.ToUInt16(packet, 4);
            ushort Offest6 = BitConverter.ToUInt16(packet, 6);
            if (client.Filtering)
                if (client.PacketFilter.Filter(ID))
                    return;
            if (ClientSeal != BitConverter.ToUInt64(packet, Length))
            {
                client.Disconnect();
                return;
            }
            switch (ID)
            {
                case 1150:
                    {
                        Game.Features.Flowers.SendFlower(client, packet);
                        return;
                    }
                case 1151:
                    {
                        Game.Features.Flowers.FlowersPacket(packet, client);
                        break;
                    }
            }
        }
    }
}
