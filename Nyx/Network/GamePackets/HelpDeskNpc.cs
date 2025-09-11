using Nyx.Server.Client;
using Nyx.Server.Game.Npc;
using Nyx.Server.Network;

namespace Nyx.Server.Network.GamePackets
{
	public static class HelpDeskNpc
	{
		[Game.Npc.NpcAttribute((uint)NpcID.HelpDesk)]
		public static void Handle(GameClient client, NpcRequest req, NpcDialog dialog)
		{
			if (client == null || req == null)
				return;


			switch (req.OptionID)
			{
				case 0:
					{
						dialog.Text($"Hello, {client.Entity.Name} tell me how can i help you?");
						dialog.Option("My Nulification", 1);
						dialog.Option("My Daily Energy", 2);
                        dialog.Option("Exit", 255);
						dialog.Finish();
                        break;
					}
				case 1:
					{
						Nullifications.Callculations.Calculate(client);
						dialog.Text($"You have nulification {client.Entity.Nullifications} Points");
                        dialog.Option("Back to main menu", 0); // Add option to go back
                        dialog.Finish();
                        break;
                    }
				case 2:
					{
						dialog.Text($"You have {client.DailyEnergy} Points");
                        dialog.Option("Back to main menu", 0); // Add option to go back
                        dialog.Finish();
                        break;
                    }
                case 255: // Exit
                    {
                        dialog.Finish(); // Only call Finish() when actually exiting
                        return;
                    }
            }
		}
	}
}

