using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Client;
using Nyx.Server.Game;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network;

namespace Nyx.Server
{
    class mdf31
    {
        public static void travel(ushort x, ushort y, Client.GameClient client)
        {

            Map.UpdateCoordonatesForAngle(ref x, ref y, Kernel.GetAngle(41, 417, client.Entity.X, client.Entity.Y));
            client.Entity.Shift(x, y);
            Attack attack = new Attack(true);
            attack.X = x;
            attack.Y = y;
            attack.AttackType = Attack.FatalStrike;
            client.Entity.Owner.SendScreen(attack, true);

            client.Entity.Owner.Screen.Reload(attack);
        }
        public static void line(Client.GameClient client, byte time, string mam)
        {
            byte[] Buf = new byte[150 + 8];
            Writer.WriteUInt16(150, 0, Buf);
            Writer.WriteUInt16(10010, 2, Buf);
            Writer.WriteUInt32(client.Entity.UID, 4 + 4, Buf);
            Writer.WriteUInt32(227, 4 + 8, Buf);// 
            Writer.WriteUInt32(164, 20 + 4, Buf);
            Writer.WriteUInt32(1, 22 + 4, Buf);
            Writer.WriteUInt32(time, 32 + 4, Buf);//Time duration for the effect 
            Writer.WriteUInt32(1, 37 + 4, Buf);
            Writer.WriteUInt32(30, 38 + 4, Buf);
            Writer.WriteString(mam, 39 + 4, Buf);// name 
            client.Send(Buf);
        }
    }
}