using MySqlX.XDevAPI;
using Nyx.Server.Interfaces;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.Npc
{
    public class NpcHandler
    {
        public static IniFile AvatarLinker = null;

        public static bool CheckValidation(Client.GameClient client, NpcRequest npcRequest)
        {
            if (AvatarLinker == null)
                AvatarLinker = new IniFile("\\database\\npc.ini");
            Npcs dialog = new Npcs(client);
            if (!client.Map.Npcs.ContainsKey(client.ActiveNpc) || npcRequest == null || client == null || client.Entity == null || (npcRequest.NpcID == 0 && npcRequest.OptionID == 255))
                return false;
            if (client.Trade != null)
                if (client.Trade.InTrade)
                    return false;
            return true;
        }

        [NpcAttribute(NpcID.HelpDesk)]
        public static void HelpDesk(Client.GameClient client, NpcRequest npcRequest, bool bypass = false)
        {
            if (!bypass && CheckValidation(client, npcRequest))
            {
                Npcs dialog = new Npcs(client);
                INpc npcs = null;
                if (client.Map.Npcs.TryGetValue(client.ActiveNpc, out npcs))
                {
                    ushort avatar = (ushort)AvatarLinker.ReadInt16("NpcType" + (npcs.Mesh / 10), "SimpleObjID", 1);

                    dialog.Avatar(avatar);
                }
                switch (npcRequest.OptionID)
                {
                    case 0:
                        {
                            dialog.Text($"Hello, {client.Entity.Name} tell me how can i help you?");
                            dialog.Option("My Nulification", 1);
                            dialog.Option("My Daily Energy", 2);
                            dialog.Send();
                            break;
                        }
                    case 1:
                        {
                            Nullifications.Callculations.Calculate(client);
                            dialog.Text($"You have nulification {client.Entity.Nullifications} Points");
                            dialog.Send();
                            break;
                        }
                    case 2:
                        {
                            dialog.Text($"You have {client.DailyEnergy} Points");
                            dialog.Send();
                            break;
                        }
                }
            }
        }
    }
}
