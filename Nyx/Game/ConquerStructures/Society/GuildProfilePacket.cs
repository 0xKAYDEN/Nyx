using System;
namespace Nyx.Server.Network.GamePackets
{
    using Nyx.Server;
    using Nyx.Server.Client;
    using Nyx.Server.Interfaces;
    using Nyx.Server.Network;
    using System;

    public class GuildProfilePacket : Writer, Interfaces.IPacket
    {
        private byte[] Packet;
        public GuildProfilePacket() { }
        public uint Silver
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set { Write(value, 8, Packet); }
        }

        public uint Pk
        {
            get { return BitConverter.ToUInt32(Packet, 20); }
            set { Write(value, 20, Packet); }
        }

        public uint Cps
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { Write(value, 12, Packet); }
        }

        public uint Guide
        {
            get { return BitConverter.ToUInt32(Packet, 16); }
            set { Write(value, 16, Packet); }
        }

        public uint Arsenal
        {
            get { return BitConverter.ToUInt32(Packet, 24); }
            set { Write(value, 24, Packet); }
        }

        public uint Rose
        {
            get { return BitConverter.ToUInt32(Packet, 28); }
            set { Write(value, 28, Packet); }
        }

        public uint Orchid
        {
            get { return BitConverter.ToUInt32(Packet, 32); }
            set { Write(value, 32, Packet); }
        }

        public uint Lily
        {
            get { return BitConverter.ToUInt32(Packet, 36); }
            set { Write(value, 36, Packet); }
        }

        public uint Tulip
        {
            get { return BitConverter.ToUInt32(Packet, 40); }
            set { Write(value, 40, Packet); }
        }

        public uint HistorySilvers
        {
            get { return BitConverter.ToUInt32(Packet, 44); }
            set { Write(value, 44, Packet); }
        }

        public uint HistoryCps
        {
            get { return BitConverter.ToUInt32(Packet, 48); }
            set { Write(value, 48, Packet); }
        }
        public uint Exploits
        {
            get;
            set;
        }
        public uint HistoryGuide
        {
            get { return BitConverter.ToUInt32(Packet, 52); }
            set { Write(value, 52, Packet); }
        }

        public uint HistoryPk
        {
            get { return BitConverter.ToUInt32(Packet, 56); }
            set { Write(value, 56, Packet); }
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Packet);
        }
        public void Deserialize(byte[] Data)
        {
            this.Packet = Data;
        }

        public byte[] Encode()
        {
            return Packet;
        }
    }
}