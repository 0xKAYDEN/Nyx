using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class CurrentLocationPacket : Writer, Interfaces.IPacket
    {
        byte[] buffer;

        public CurrentLocationPacket(byte[] buffer)
        {
            this.buffer = buffer;
        }
        public CurrentLocationPacket(uint heroID, Nyx.Server.Game.Enums.CountryID countryID)
        {
            buffer = new byte[18];
            Writer.Write(10, 0, buffer);
            Writer.Write(0x97E, 2, buffer);
            Write(heroID, 4, buffer);
            Writer.Write((ushort)countryID, 8, buffer);
        }

        public uint HeroID
        {
            get
            {
                return BitConverter.ToUInt32(buffer, 4);
            }
            set
            {
                Write(value, 4, buffer);
            }
        }

        public ushort CountryID
        {
            get
            {
                return BitConverter.ToUInt16(buffer, 8);
            }
            set
            {
                Writer.Write((ushort)value, 8, buffer);
            }
        }

        public byte[] Encode()
        {
            return buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(buffer);
        }
    }
}
