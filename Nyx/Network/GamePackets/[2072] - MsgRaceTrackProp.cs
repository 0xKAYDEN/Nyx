using Nyx.Server.Client;
using Nyx.Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class RacePotion : Writer, Interfaces.IPacket
    {
        private byte[] buffer;
        public RacePotion(bool create)
        {
            if (create)
            {
                buffer = new byte[24];
                Writer.Write(16, 0, buffer);
                Writer.Write(2072, 2, buffer);
            }
        }

        public ushort Amount
        {
            get { return BitConverter.ToUInt16(buffer, 4); }
            set {Writer.Write(value, 4, buffer); }
        }

        public Enums.RaceItemType PotionType
        {
            get { return (Enums.RaceItemType)BitConverter.ToUInt16(buffer, 6); }
            set {Writer.Write((ushort)value, 6, buffer); }
        }

        public int Location
        {
            get { return BitConverter.ToInt32(buffer, 8); }
            set {Writer.Write(value, 8, buffer); }
        }
        
        public uint dwParam
        {
            get { return BitConverter.ToUInt32(buffer, 12); }
            set { Write(value, 12, buffer); }
        }
        
        public void Deserialize(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public void Send(GameClient client)
        {
            client.Send(buffer);
        }

        public byte[] Encode()
        {
            return buffer;
        }
    }
}
