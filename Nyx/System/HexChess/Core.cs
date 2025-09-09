using Nyx.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.HexChess
{
    public class Core
    {
        //1. get all the maps for the event from the database , Set the Nullification Amount for each Map
        public static Dictionary<uint, int> HexChessMaps = new Dictionary<uint, int>();
        public Core()
        {
            HexChessMaps.Clear();
            //MapID , Nullification to stay alive in the map
            HexChessMaps.Add(3956, 500);
            HexChessMaps.Add(3957, 1000);
            HexChessMaps.Add(3958, 2000);
            HexChessMaps.Add(3959, 3000);
        }

        //2. Spawn The Monster When ever the player enter the map
        public static void HexChessSolo(GameClient client)
        {
            if (!client.Entity.Dead)
            {
                client.Entity.Teleport(3956, 40, 50);

            }
        }

        public static void HexChessStory(List<GameClient> clients)
        {
            foreach (GameClient client in clients)
            {
                if(!client.Entity.Dead)
                {
                    client.Entity.Teleport(3956, 40, 50);
                }
            }
        }

        public static void HexChessElite(List<GameClient> clients)
        {
            foreach (GameClient client in clients)
            {
                client.Entity.Teleport(3956, 40, 50);

            }
        }
    }
}
