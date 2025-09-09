using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class SlotMachineResponse : Writer, Interfaces.IPacket
    {

        byte[] Buffer;

        public SlotMachineResponse(bool Create = false)
        {
            Buffer = new byte[28];
            Writer.Write(20, 0, Buffer);
            Writer.Write(1352, 2, Buffer);
        }

        public Game.Enums.SlotMachineSubType Mode
        {
            get { return (Game.Enums.SlotMachineSubType)Buffer[4]; }
            set { Buffer[4] = (byte)value; }
        }
        public byte WheelOne
        {
            get { return Buffer[5]; }
            set { Buffer[5] = value; }
        }
        public byte WheelTwo
        {
            get { return Buffer[6]; }
            set { Buffer[6] = value; }
        }
        public byte WheelThree
        {
            get { return Buffer[7]; }
            set { Buffer[7] = value; }
        }
        public uint NpcID
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
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
