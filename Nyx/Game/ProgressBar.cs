using Nyx.Server.Client;
using Nyx.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Game
{
    public class ProgressBar
    {
        private uint actionuid;
        public ProgressBar(GameClient Client, string EffectName, Action<GameClient> Action = null, string doneeffect = "Done!", uint Time = 5)
        {
            if (Client.Entity.Dead) return;
            if (Client.ProgressBar != null)
            {
                Client.ProgressBar.End(Client);
                return;
            }
            byte[] Packet = new byte[47 + 4 + EffectName.Length];
            Writer.Write((ushort)(Packet.Length - 8), 0, Packet);
            Writer.Write(10010, 2, Packet);
            Writer.Write(Client.Entity.UID, 8, Packet);
            Writer.Write(220, 12, Packet);
            Writer.Write(Time32.Now.GetHashCode(), 20, Packet);
            Writer.Write(164, 24, Packet);
            Writer.Write(1, 26, Packet);
            Writer.Write(Time, 36, Packet);
            Writer.Write(1, 41, Packet);
            Writer.Write((byte)(EffectName.Length), 42, Packet);
            Writer.Write(EffectName, 43, Packet);
            Client.Send(Packet);
            actionuid = Program.World.DelayedTask.StartDelayedTask(() =>
            {
                if (Action != null)
                    Action.Invoke(Client);
                Action = null;

                Packet = new byte[47 + 4 + doneeffect.Length];
                Writer.Write((ushort)(Packet.Length - 8), 0, Packet);
                Writer.Write(10010, 2, Packet);
                Writer.Write(Client.Entity.UID, 8, Packet);
                Writer.Write(220, 12, Packet);
                Writer.Write(Time32.Now.GetHashCode(), 20, Packet);
                Writer.Write(164, 24, Packet);
                Writer.Write((ushort)Enums.ConquerAngle.West, 26, Packet);
                Writer.Write(0, 36, Packet);
                Writer.Write(1, 41, Packet);
                if (doneeffect != "")
                {
                    Writer.Write((byte)(doneeffect.Length), 42, Packet);
                    Writer.Write(doneeffect, 43, Packet);
                }
                Client.Send(Packet);
                Client.ProgressBar = null;

            }, (int)Time * 1000);

        }
        public void End(Client.GameClient Client)
        {
            var doneeffect = "Interrupted";
            Program.World.DelayedTask.Remove(actionuid);
            byte[] Packet = new byte[47 + 4 + doneeffect.Length];
            Writer.Write((ushort)(Packet.Length - 8), 0, Packet);
            Writer.Write(10010, 2, Packet);
            Writer.Write(Client.Entity.UID, 8, Packet);
            Writer.Write(164, 24, Packet);
            Writer.Write((ushort)Enums.ConquerAngle.West, 26, Packet);
            Writer.Write(0, 36, Packet);
            Writer.Write(1, 41, Packet);
            Writer.Write((byte)(doneeffect.Length), 42, Packet);
            Writer.Write(doneeffect, 43, Packet);
            Client.Send(Packet);
            Client.ProgressBar = null;
        }
    }
}
