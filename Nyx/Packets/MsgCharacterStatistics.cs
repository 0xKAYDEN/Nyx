using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;


namespace Nyx.Server.Packets
{
    public class MsgCharacterStatistics
    {
        [Network.Packet(1040)]
        public static async Task Process(GameClient client, byte[] packet)
        {
            uint UID = BitConverter.ToUInt32(packet, 8);
            if (Kernel.GamePool.TryGetValue(UID, out client))
            {
                WindowsStats WS = new WindowsStats(client);
                WS.Send(client);
            }
        }
    }
}
