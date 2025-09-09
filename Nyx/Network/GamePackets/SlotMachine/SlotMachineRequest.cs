using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class SlotMachineRequest : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public SlotMachineRequest(bool Create = false)
        {
        }

        public Game.Enums.SlotMachineSubType Mode
        {
            get { return (Game.Enums.SlotMachineSubType)Buffer[4]; }
            set { Buffer[4] = (byte)value; }
        }
        public byte BetMultiplier
        {
            get { return Buffer[5]; }
            set { Buffer[5] = value; }
        }

        public uint NpcID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }


        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
}
