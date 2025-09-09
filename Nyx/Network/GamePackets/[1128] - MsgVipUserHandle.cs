using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum VIPTeleportLocations
{
    TwinCity = 1,
    PhoenixCastle = 2,
    ApeCity = 3,
    DesertCity = 4,
    BirdIland = 5,
    TCSquare = 6,
    WPAltar = 7,
    WPApparation = 8,
    WPPoltergiest = 9,
    WPTurtledove = 10,
    PCSqaure = 11,
    MFWaterCave = 12,
    MFVillage = 13,
    MFLake = 14,
    MFMineCave = 15,
    MFBridge = 16,
    MFToApeCity = 17,
    ACSquare = 18,
    ACSouth = 19,
    ACEast = 20,
    ACNorth = 21,
    ACWest = 22,
    BISquare = 23,
    BICenter = 24,
    BISouthWest = 25,
    BINorthWest = 26,
    BINorthEast = 27,
    DCSquare = 28,
    DCSouth = 29,
    DCVillage = 30,
    DCMoonSpring = 31,
    DCAncientMaze = 32
}
public enum VIPTeleportTypes
{
    SelfTeleport = 0,
    TeamTeleport = 1,
    TeammateConfirmation = 2,
    TeammateTeleport = 3,
}

namespace Nyx.Server.Network.GamePackets
{
    public class VIPTeleport : Writer, Interfaces.IPacket
    {
        private Byte[] Buffer;
        public static implicit operator Byte[](VIPTeleport d) { return d.Buffer; }

        public VIPTeleport()
        {
            this.Buffer = new Byte[32 + 8];
            Writer.Write(32, 0, Buffer);
            Writer.Write(1128, 2, Buffer);
        }

        public VIPTeleportTypes TeleportType
        {
            get { return (VIPTeleportTypes)BitConverter.ToUInt32(this.Buffer, 4); }
            set { Write((UInt32)value, 4, Buffer); }
        }
        public VIPTeleportLocations Location
        {
            get { return (VIPTeleportLocations)BitConverter.ToUInt32(this.Buffer, 8); }
            set { Write((UInt32)value, 8, Buffer); }
        }
        public Byte Countdown
        {
            get { return Buffer[12]; }
            set { Buffer[12] = value; }
        }
        public String Name
        {
            get { return Encoding.ASCII.GetString(Buffer, 17, Buffer[16]); }
            set { Writer.Write(value, 16, Buffer); }
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public byte[] Encode()
        {
            return Buffer;
        }
        public static void Teleport(Client.GameClient client, VIPTeleportLocations Location)
        {
            if (client.Entity.InJail())
                return;
            switch (Location)
            {
                case VIPTeleportLocations.TwinCity:
                case VIPTeleportLocations.TCSquare: client.Entity.Teleport((ushort)Game.Enums.Maps.TwinCity, 300, 278); break;
                case VIPTeleportLocations.WPAltar: client.Entity.Teleport((ushort)Game.Enums.Maps.TwinCity, 513, 747); break;
                case VIPTeleportLocations.WPApparation: client.Entity.Teleport((ushort)Game.Enums.Maps.TwinCity, 252, 495); break;
                case VIPTeleportLocations.WPPoltergiest: client.Entity.Teleport((ushort)Game.Enums.Maps.TwinCity, 105, 349); break;
                case VIPTeleportLocations.WPTurtledove: client.Entity.Teleport((ushort)Game.Enums.Maps.TwinCity, 665, 396); break;

                case VIPTeleportLocations.PhoenixCastle:
                case VIPTeleportLocations.PCSqaure: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 188, 264); break;
                case VIPTeleportLocations.MFWaterCave: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 380, 31); break;
                case VIPTeleportLocations.MFVillage: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 785, 472); break;
                case VIPTeleportLocations.MFLake: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 369, 568); break;
                case VIPTeleportLocations.MFMineCave: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 924, 560); break;
                case VIPTeleportLocations.MFBridge: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 648, 567); break;
                case VIPTeleportLocations.MFToApeCity: client.Entity.Teleport((ushort)Game.Enums.Maps.PhoenixCastle, 475, 841); break;

                case VIPTeleportLocations.ApeCity:
                case VIPTeleportLocations.ACSquare: client.Entity.Teleport((ushort)Game.Enums.Maps.ApeMoutain, 565, 562); break;
                case VIPTeleportLocations.ACSouth: client.Entity.Teleport((ushort)Game.Enums.Maps.ApeMoutain, 699, 640); break;
                case VIPTeleportLocations.ACEast: client.Entity.Teleport((ushort)Game.Enums.Maps.ApeMoutain, 624, 337); break;
                case VIPTeleportLocations.ACNorth: client.Entity.Teleport((ushort)Game.Enums.Maps.ApeMoutain, 200, 224); break;
                case VIPTeleportLocations.ACWest: client.Entity.Teleport((ushort)Game.Enums.Maps.ApeMoutain, 322, 621); break;

                case VIPTeleportLocations.DesertCity:
                case VIPTeleportLocations.DCSquare: client.Entity.Teleport((ushort)Game.Enums.Maps.DesertCity, 500, 650); break;
                case VIPTeleportLocations.DCSouth: client.Entity.Teleport((ushort)Game.Enums.Maps.DesertCity, 758, 750); break;
                case VIPTeleportLocations.DCVillage: client.Entity.Teleport((ushort)Game.Enums.Maps.DesertCity, 480, 271); break;
                case VIPTeleportLocations.DCMoonSpring: client.Entity.Teleport((ushort)Game.Enums.Maps.DesertCity, 291, 450); break;
                case VIPTeleportLocations.DCAncientMaze: client.Entity.Teleport((ushort)Game.Enums.Maps.DesertCity, 87, 321); break;

                case VIPTeleportLocations.BirdIland:
                case VIPTeleportLocations.BISquare: client.Entity.Teleport((ushort)Game.Enums.Maps.BirdIsland, 717, 571); break;
                case VIPTeleportLocations.BICenter: client.Entity.Teleport((ushort)Game.Enums.Maps.BirdIsland, 585, 593); break;
                case VIPTeleportLocations.BISouthWest: client.Entity.Teleport((ushort)Game.Enums.Maps.BirdIsland, 562, 786); break;
                case VIPTeleportLocations.BINorthWest: client.Entity.Teleport((ushort)Game.Enums.Maps.BirdIsland, 125, 323); break;
                case VIPTeleportLocations.BINorthEast: client.Entity.Teleport((ushort)Game.Enums.Maps.BirdIsland, 125, 323); break;
            }
        }
    }
}