using Humanizer;
using Nyx.Server.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Game.ConquerStructures.Society
{
    public class Energy
    {
        static Time32 currentTime = Time32.Now;
        public static bool HaveEnergy(GameClient client, int EventCost)
        {
            if (client.DailyEnergy >= EventCost)
                return true;
            else
                return false;
        }
        public static void RechargeEnergy()
        {
            // Store the current time
            //DateTime currentTime = DateTime.Now;
            if (currentTime.Value % 1 == 0)
            {
                foreach (GameClient client in Kernel.GamePool.Values)
                {
                    if (client != null)
                    {
                        // Add your energy recharge logic here
                        // client.CurrentEnergy = Math.Min(client.MaxEnergy, client.CurrentEnergy + energyToAdd);
                        if (client.DailyEnergy >= client.MaxEnergy)
                        {
                            // if the client have the max energy don't add anything
                        }
                        else
                        {
                            // Recharge energy for each client
                            // Example: client.Energy += 1; or your specific energy recharge logic
                            client.DailyEnergy += 10;
                            Log.Information("All players energy have been updated");
                        }
                    }
                }
            }
        }
    }
}
