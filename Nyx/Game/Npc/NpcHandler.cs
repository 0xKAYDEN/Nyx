using MySqlX.XDevAPI;
using Nyx.Server.Client;
using Nyx.Server.Interfaces;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.Npc
{
    public class NpcHandler
    {
        //[NpcAttribute((uint)NpcID.HelpDesk)]
        //public static void Handle(GameClient client, NpcRequest req, NpcDialog dialog)
        //{
        //    if (client == null || req == null)
        //        return;

        //    switch (req.OptionID)
        //    {
        //        case 0:
        //            {
        //                dialog.Text($"Hello, {client.Entity.Name} tell me how can i help you?");
        //                dialog.Option("My Nulification", 1);
        //                dialog.Option("My Daily Energy", 2);
        //                dialog.Finish();
        //                break;
        //            }
        //        case 1:
        //            {
        //                // Preserve existing behavior
        //                Nullifications.Callculations.Calculate(client);
        //                dialog.Text($"You have nulification {client.Entity.Nullifications} Points");
        //                dialog.Finish();
        //                client.Disconnect();
        //                break;
        //            }
        //        case 2:
        //            {
        //                dialog.Text($"You have {client.DailyEnergy} Points");
        //                dialog.Finish();
        //                break;
        //            }
        //    }
        //}
    }
}
