﻿using System;

namespace Nyx.Server.Network.GamePackets
{
    public class CharacterInfo : Writer, Interfaces.IPacket
    {
        Client.GameClient client;
        public CharacterInfo(Client.GameClient _client)
        {
            client = _client;
        }
        public void Deserialize(byte[] buffer)
        {
            throw new NotImplementedException();
        }
        public byte[] Encode()
        {
            byte[] Packet = new byte[144 + client.Entity.Spouse.Length + client.Entity.Name.Length];
            Writer.Write((ushort)(Packet.Length - 8), 0, Packet);
            Writer.Write(1006, 2, Packet);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Packet);
            Write(client.Entity.UID, 8, Packet);
            Write(client.Entity.Mesh, 14, Packet);
            Writer.Write(client.Entity.HairStyle, 18, Packet);
            Write(client.Entity.Money, 20, Packet);
            Write((uint)client.Entity.ConquerPoints, 28, Packet);
         Writer.Write(client.Entity.Experience, 32, Packet);
            Writer.Write(client.Entity.Strength, 60, Packet);
            Writer.Write(client.Entity.Agility, 62, Packet);
            Writer.Write(client.Entity.Vitality, 64, Packet);
            Writer.Write(client.Entity.Spirit, 66, Packet);
            Writer.Write(client.Entity.Atributes, 68, Packet);
            Write(client.Entity.Hitpoints, 70, Packet);
            Writer.Write(client.Entity.Mana, 74, Packet);
            Writer.Write(client.Entity.PKPoints, 72, Packet);
            Packet[78] = client.Entity.Level;
            Packet[79] = client.Entity.Class;
            Packet[80] = client.Entity.FirstRebornClass;
            Packet[81] = client.Entity.SecondRebornClass;
            Packet[83] = client.Entity.Reborn;
            if (client.Entity.FirstCredit == 1)
            {
                Packet[89] = 1;
            }
            if (client.Entity.FirstCredit == 2)
            {
                Packet[89] = 7;
            }
            else
            {
                Packet[89] = 0;
            }
            Writer.Write((ushort)(client.Entity.EnlightenPoints * 100), 93, Packet);
            Write(client.Entity.BoundCps, 107, Packet);
            Write(client.Entity.SubClasses.Active, 111, Packet);
            Write(client.Entity.SubClasses.GetHashPoint(), 112, Packet);
            Writer.Write((UInt16)client.Entity.MyTitle, 103, Packet);
            Writer.Write((ushort)client.Entity.CountryID, 124, Packet);
            Writer.Write(3, 130, Packet);
            Packet[131] = (byte)client.Entity.Name.Length;
            Writer.Write(client.Entity.Name, 132, Packet);
            Writer.Write((byte)client.Entity.Spouse.Length, 133 + client.Entity.Name.Length, Packet);
            Writer.Write(client.Entity.Spouse, 134 + client.Entity.Name.Length, Packet);
            return Packet;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Encode());
        }
    }
}
