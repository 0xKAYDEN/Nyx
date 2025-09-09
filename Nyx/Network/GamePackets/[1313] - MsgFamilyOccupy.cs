using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class ClanArena : Interfaces.IPacket
    {
        private byte[] Buffer;
        public enum Modes : byte
        {
            ShowGUI = 6,
            Apply = 0,
            Cancel = 1,
            Join = 8,
        }
        public ClanArena()
        {
            Buffer = new byte[8 + 140];
            Writer.Write(140, 0, Buffer);
            Writer.Write(1313, 2, Buffer);
            Writer.Write(398, 13, Buffer);
            Buffer[93] = 1;
            Buffer[94] = 1;
        }
        public byte Mode
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }
        public uint NpcID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }
        public byte Mode_Param
        {
            get { return Buffer[12]; }
            set { Buffer[12] = value; }
        }
        public byte Param
        {
            get { return Buffer[16]; }
            set { Buffer[16] = value; }
        }
        public string _WinningClanName = "Unknown";
        public string WinningClanName
        {
            get { return _WinningClanName; }
            set {
                _WinningClanName = value;
                Writer.Write(value, 20, Buffer); }
        }
        public string _ClanCityName = "TwinCityClan";
        public string ClanCityName
        {
            get { return _ClanCityName; }
            set
            {
                ClanCityName = value;
                Writer.Write(value, 56, Buffer);
            }
        }
        public uint OccuPrevYDays
        {
            get { return BitConverter.ToUInt32(Buffer, 96); }
            set { Writer.Write(value, 96, Buffer); }
        }

        public uint ItemID1//729536
        {
            get { return BitConverter.ToUInt32(Buffer, 100); }
            set { Writer.Write(value, 100, Buffer); }
        }

        public uint ItemID2//729536
        {
            get { return BitConverter.ToUInt32(Buffer, 104); }
            set { Writer.Write(value, 104, Buffer); }
        }

        public uint Fee
        {
            get { return BitConverter.ToUInt32(Buffer, 120); }
            set { Writer.Write(value, 120, Buffer); }
        }
        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}