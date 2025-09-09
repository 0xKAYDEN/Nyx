using Nyx.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public enum RaceRecordTypes : int
    {
        BestTime = 0,
        EndTime = 1,
        AddRecord = 2,
        CloseRecords = 3,
    }
    public class RaceRecord : Writer, Interfaces.IPacket
    {
        private byte[] buffer;
        public RaceRecord()
        {
            buffer = new byte[36 + 8];
            Writer.Write(36, 0, buffer);
            Writer.Write(1071, 2, buffer);
        }

        public RaceRecordTypes Type
        {
            get { return (RaceRecordTypes)BitConverter.ToInt32(buffer, 4); }
            set {Writer.Write((int)value, 4, buffer); }
        }

        public int Rank
        {
            get { return BitConverter.ToInt32(buffer, 8); }
            set {Writer.Write(value, 8, buffer); }
        }

        public string Name
        {
            get { return Encoding.Default.GetString(buffer, 12, 16).Replace("\0", ""); }
            set { Writer.Write(value, 12, buffer); }
        }

        //For type EndTime
        public int dwParam
        {
            get { return BitConverter.ToInt32(buffer, 12); }
            set {Writer.Write(value, 12, buffer); }
        }
        
        //For type EndTime
        public int dwParam2
        {
            get { return BitConverter.ToInt32(buffer, 16); }
            set {Writer.Write(value, 16, buffer); }
        }

        public int Time
        {
            get { return BitConverter.ToInt32(buffer, 28); }
            set {Writer.Write(value, 28, buffer); }
        }

        public int Prize
        {
            get { return BitConverter.ToInt32(buffer, 32); }
            set {Writer.Write(value, 32, buffer); }
        }

        public void Deserialize(byte[] buffer)
        {
            throw new NotImplementedException();
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
