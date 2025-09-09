using System;


namespace Nyx.Server.Network.GamePackets
{
    public class RouletteSpawn : Writer, Interfaces.IPacket, Interfaces.IRoulette, Interfaces.IMapObject
    {
        private byte[] Buffer;
        private ushort _MapID;


        public RouletteSpawn(bool Created = true)
        {
            Buffer = new byte[31];
            Write(23, 0, Buffer);
            Write(2807, 2, Buffer);
        }

        public System.Collections.Generic.List<Client.GameClient> Recored { get; set; }
        public System.Collections.Generic.Dictionary<uint, Game.RoulettePlayerStructure> Players { get; set; }
        public ushort LotteryNumber { get; set; }
        public Time32 Time { get; set; }
        public int Stamp { get; set; }
        public bool Place0 { get; set; }
        public bool Place1 { get; set; }
        public bool Place2 { get; set; }
        public bool Place3 { get; set; }
        public bool Place4 { get; set; }

        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }

        public ushort X
        {
            get { return BitConverter.ToUInt16(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }

        public ushort Y
        {
            get { return BitConverter.ToUInt16(Buffer, 18); }
            set { Write(value, 18, Buffer); }
        }

        public ushort Mesh
        {
            get { return BitConverter.ToUInt16(Buffer, 20); }
            set { Write(value, 20, Buffer); }
        }

        public ushort TableNumber
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }

        public ushort StackType
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public ushort PlayerNumber
        {
            get { return BitConverter.ToUInt16(Buffer, 22); }
            set { Write(value, 22, Buffer); }
        }

        public Nyx.Server.Game.MapObjectType MapObjType
        {
            get { return Nyx.Server.Game.MapObjectType.Npc; }
        }

        public Client.GameClient Owner
        {
            get { return null; }
        }

        public void SendSpawn(Client.GameClient client, bool checkScreen)
        {
            if (client.Screen.Add(this) || !checkScreen)
            {
                client.Send(Buffer);
            }
        }

        public void SendSpawn(Client.GameClient client)
        {
            SendSpawn(client, false);
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            SendSpawn(client, false);
        }


        public ushort MapID
        {
            get { return _MapID; }
            set { _MapID = value; }
        }
    }
}