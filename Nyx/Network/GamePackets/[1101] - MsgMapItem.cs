using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class 
        FloorItem : Writer, Interfaces.IPacket, Interfaces.IMapObject
    {
        public const ushort Drop = 1,
            Remove = 2,
            Animation = 3,
            DropDetain = 4,
            Effect = 10,
            RemoveEffect = 12,
        AuroraLutos = 930,
           FlameLutos = 940;
        public const uint
            Twilight = 40,
            DaggerStorm = 50,
             AuroraLotus = 930,
              FlameLotus = 940,
            FuryofEgg = 41,
            ShacklingIce = 42;
        public static Counter FloorUID = new Counter(0);
        byte[] Buffer;
        Client.GameClient owner;
        ushort mapid;
        public Time32 OnFloor, UseTime;
        public bool PickedUpAlready = false;
        public uint Value;
        public FloorValueType ValueType;
        private ConquerItem item;
        public bool Shake = false;
        public bool Zoom = false;
        public bool Darkness = false;

        public void AppendFlags()
        {
           Writer.Write((Shake ? 1 : 0) | (Zoom ? 2 : 0) | (Darkness ? 4 : 0), 8, Buffer);
        }

        public FloorItem(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[77 + 8];
                Writer.Write(77, 0, Buffer);
                Writer.Write(1101, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
                Value = 0;
                ValueType = FloorValueType.Item;
            }
        }
        public ushort MaxLife
        {
            get { return BitConverter.ToUInt16(Buffer, 20); }
            set {Writer.Write((ushort)value, 20, Buffer); }
        }
        public ushort Life
        {
            get { return BitConverter.ToUInt16(Buffer, 23); }
            set {Writer.Write((ushort)value, 23, Buffer); }
        }
        public byte mColor
        {
            get { return Buffer[27]; }
            set { Buffer[27] = value; }
        }
        public uint OwnerUID
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Write(value, 28, Buffer); }
        }
        public uint OwnerGuildUID
        {
            get { return BitConverter.ToUInt32(Buffer, 32); }
            set { Write(value, 32, Buffer); }
        }
        public byte FlowerType
        {
            get { return Buffer[36]; }
            set { Buffer[36] = value; }
        }
        public ulong time
        {
            get { return BitConverter.ToUInt64(Buffer, 45); }
            set { Writer.Write(value, 45, Buffer); }
        }
        public string Name
        {
            get { return Program.Encoding.GetString(Buffer, 45, 16); }
            set { Writer.Write(value, 45, Buffer); }
        }
        public string Unknown
        {
            get { return Program.Encoding.GetString(Buffer, 61, 16); }
            set { Writer.Write(value, 61, Buffer); }
        }  
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint ItemID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public ushort X
        {
            get { return BitConverter.ToUInt16(Buffer, 16); }
            set {Writer.Write(value, 16, Buffer); }
        }
        public ushort Y
        {
            get { return BitConverter.ToUInt16(Buffer, 18); }
            set {Writer.Write(value, 18, Buffer); }
        }
        public Game.Enums.Color ItemColor
        {
            get { return (Game.Enums.Color)BitConverter.ToUInt16(Buffer, 20); }
            set {Writer.Write((ushort)value, 20, Buffer); }
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 22); }
            set { Write(value, 22, Buffer); }
        }
        public Nyx.Server.Game.MapObjectType MapObjType
        {
            get { return Nyx.Server.Game.MapObjectType.Item; }
            set { }
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
        public void SendSpawn(Client.GameClient client)
        {
            SendSpawn(client, false);
        }
        public Client.GameClient Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public ConquerItem Item
        {
            get { return item; }
            set { item = value; }
        }
        public ushort MapID
        {
            get { return mapid; }
            set { mapid = value; }
        }
        public void SendSpawn(Client.GameClient client, bool checkScreen)
        {
            if (client.Screen.Add(this) || !checkScreen)
            {
                client.Send(Buffer);
            }
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public enum FloorValueType { Item, Money, ConquerPoints }
        public IDisposable TimerSubscriptions = null;
    }
}
