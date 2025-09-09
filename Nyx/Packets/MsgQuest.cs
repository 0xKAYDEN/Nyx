using Nyx.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Packets
{
    public class MsgQuest
    {
        public static ulong ClientSeal = BitConverter.ToUInt64(Encoding.Default.GetBytes("TQClient"), 0);

        [Network.Packet((ushort)Network.PacketType.MsgQuest)]
        [Network.Packet(1135)]
        public static async Task Process(GameClient client, byte[] packet)
        {
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
            switch(ID)
            {
                case 1135:
                    {
                        QuestData quest = new QuestData(true);
                        quest.Deserialize(packet);
                        break;
                    }
                case 1134:
                    {
                        QuestPacket quest = new QuestPacket(false);
                        quest.Deserialize(packet);
                        switch (quest.Action)
                        {
                            case QuestPacket.QuestAction.Begin:
                                {
                                    /* if (client.Quests.AllowAccept())
                                         client.Quests.Accept((QuestID)quest[0].UID, 0);
                                     client.Send(quest);*/
                                    break;
                                }
                            case QuestPacket.QuestAction.List:
                                {
                                    client.Quests.SendFullGUI();
                                    break;
                                }
                        }
                        break;
                    }
            }

        }
    }
}
