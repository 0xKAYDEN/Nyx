using Nyx.Server.Client;
using Nyx.Server.Network.GamePackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Nullifications
{
    public class Callculations
    {
        //0. Checker for any Event 
        public static bool IsQualified(GameClient client)
        {
            Calculate(client);
            if(client.Entity.Nullifications >= 3000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //1. get all the equped gear of the player and add 100 point for each epic peace of gear
        public static void Calculate(GameClient client)
        {
            // Reset Nullifications before calculation if needed
            client.Entity.Nullifications = 0;

            foreach (var item in client.Equipment.Objects)
            {
                if (item == null) continue;
                if (IsEpic((ConquerItem)item)) // Replace with your actual epic check
                {
                    client.Entity.Nullifications += 500;
                }
            }

            // Example epic check (replace with your actual logic)
            bool IsEpic(ConquerItem item)
            {
                // Example: check for a special effect, ID range, or property
                return item.ID % 10 == (byte)Game.Enums.ItemQuality.Super; /*&& item.ExtraEffect.Available && item.ExtraEffect.EffectLevel >= 5;*/
                // Or: return item.ID == 123456; // for specific epic item IDs
            }
        }
        //2. Update the database 



    }
}
