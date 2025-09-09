using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
   public class AutoInvite
    {
        byte[] Packet;
        public AutoInvite()
        {
            Packet = new byte[28];
            Writer.Write(Packet.Length - 8, 0, Packet);
            Writer.Write(1126, 2, Packet);
        }
        public uint StrResID
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { Writer.Write(value, 8, Packet); }
        }
        public byte Action
        {
            get { return Packet[12]; }
            set { Packet[12] = value; }
        }
        public uint Countdown
        {
            get { return BitConverter.ToUInt32(Packet, 16); }
            set { Writer.Write(value, 16, Packet); }
        }
        public byte[] Encode()
        {
            return Packet;
        }
        public void Deserialize(byte[] Pack)
        {
            Packet = Pack;
        }
        public static void Process(byte[] Packet, Client.GameClient client)
        {
            if (client != null && client.Entity != null && !client.Entity.InJail() && !client.InQualifier() && !client.InTeamQualifier() && !client.IsWatching() && client.Booth == null && !Constants.PKFreeMaps.Contains(client.Entity.MapID) && client.Entity.MapID < 10000)
            {
                AutoInvite Auto = new AutoInvite();
                Auto.Deserialize(Packet);
                switch (client.Entity.StrResID)
                {
                    case 10531://PowerArena
                        client.Entity.Teleport(1002, 336, 137);
                        client.MessageBox("You`ve been teleported to Arena Manager Wang. Talk to her to sign up.");
                        break;
                    case 10519://ClassPk
                        client.Entity.Teleport(1002, 289, 147);
                        break;
                    case 10521://WeeklyPk
                    case 10527://Monthly
                        client.Entity.Teleport(1002, 327, 196);
                        client.MessageBox("You`ve been teleported to General Bravery. Talk to him to sign up.");
                        break;
                    case 10535://CTF
                        client.Entity.Teleport(1002, 225, 238);
                        break;
                    case 10525:
                        if (!client.Spells.ContainsKey(7001))
                        {
                            client.Send("You need learn the riding skill!");
                        }
                        else
                        {
                            if (!client.Equipment.Free(ConquerItem.Steed))
                                 Program.World.SteedRace.Join(client);
                            else
                                client.Send("You need to wear a horse first!");
                        }
                        break;
                    case 10543://TeamPk
                        client.Entity.Teleport(1002, 304, 143);
                        break;
                    case 10541://SkilTeamPk
                        client.Entity.Teleport(1002, 314, 147);
                        break;
                    case 10515://GuildWar
                        client.Entity.Teleport(1002, 225, 237);
                        break;
                    case 10533://ElitePk
                        client.Entity.Teleport(1002, 298, 144);
                        break;
                }
                client.Entity.StrResID = 0;
            }
        }
    }
}
