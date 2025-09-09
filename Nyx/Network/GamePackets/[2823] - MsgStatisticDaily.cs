using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using Nyx.Server.Client; 
using Nyx.Server.Network; 
using System.IO; 
using Nyx.Server.Database; 

namespace Nyx.Server
{ 
    public class Way2Heroes : Network.Writer, Interfaces.IPacket 
    { 
        public static void SendDone(GameClient client) 
        { 
            byte[] Buffer = new byte[106 + 8]; 
            Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer); 
            Writer.Write(2832, 2, Buffer);
            Write(10, 4, Buffer);//count 
            int offset = 6; 
            for (uint i = 0; i < 10; i++) 
            {
                Write(i + 1, offset, Buffer); offset += 5;//id 
                Buffer[offset++] = 0;//has something to claim 
                Write(4, offset, Buffer); offset += 4;//items done 
            } 
            client.Send(Buffer); 
        } 
        public struct Stage 
        { 
            public uint UID; 
            public uint Progress; 
            public uint[] Prize; 
        } 
        public static Dictionary<uint, Stage> Stages; 

        public class StageInfo 
        { 
            public uint UID; 
            public bool Done; 
            public SafeDictionary<uint, SubStageInfo> SubStages = new SafeDictionary<uint, SubStageInfo>(); 
            public void WriteItem(BinaryWriter writer) 
            { 
                writer.Write(UID); //= reader.ReadUInt32(); 
                writer.Write(Done); 
                writer.Write((byte)SubStages.Count); 
                foreach (var prize in SubStages.Values) 
                    prize.WriteItem(writer); 
            } 
            public StageInfo ReadItem(BinaryReader reader) 
            { 
                UID = reader.ReadUInt32();//4 
                Done = reader.ReadBoolean();//8 
                int count = reader.ReadByte(); 
                for (uint x = 0; x < count; x++) 
                { 
                    SubStageInfo item = new SubStageInfo(); 
                    item = item.ReadItem(reader); 
                    SubStages.Add(item.UID, item); 
                }       
                return this; 
            } 
        } 
        public class SubStageInfo 
        { 
            public uint UID; 
            public bool Done; 
            public uint points; 
            public void WriteItem(BinaryWriter writer) 
            { 
                writer.Write(UID); //= reader.ReadUInt32(); 
                writer.Write(Done); 
                writer.Write(points); 
            } 
            public SubStageInfo ReadItem(BinaryReader reader) 
            { 
                UID = reader.ReadUInt32();//4 
                Done = reader.ReadBoolean();//8 
                points = reader.ReadUInt32();//10         
                return this; 
            } 
        } 
        public static void Load() 
        { 
            Stages = new Dictionary<uint, Stage>(); 
            string[] text = File.ReadAllLines("database/stagegoal.txt"); 
            Stage Stage = new Stage(); 
            for (int x = 0; x < text.Length; x++) 
            { 
                string line = text[x]; 
                string[] split = line.Split('='); 
                if (line.StartsWith("[") && line.EndsWith("]") && !line.Contains("StageGoal")) 
                { 
                    string[] numbers = line.Replace("[", "").Replace("]", "").Split('-'); 
                    uint id = uint.Parse(numbers[0]) * 100; 
                    if (numbers.Length > 1) 
                        id += uint.Parse(numbers[1]); 
                    if (Stages.ContainsKey(id)) 
                        Stage = Stages[uint.Parse(split[1])]; 
                    else 
                    { 
                        Stage = new Stage(); 
                        Stage.UID = id; 
                        Stage.Prize = new uint[3]; 
                        Stages.Add(id, Stage); 
                    } 
                } 
                else 
                { 
                    if (split[0] == "Progress") 
                        Stage.Progress = uint.Parse(split[1]); 
                    else if (split[0] == "Prize1") 
                    { 
                        string[] PrizeLine = split[1].Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries); 
                        for (int i = 0; i < PrizeLine.Length; i++) 
                        { 
                            Stage.Prize[i] = uint.Parse(PrizeLine[i]); 
                        } 

                    } 
                } 
            } 
            
        } 
        /// <summary> 
        /// 
        /// </summary> 
        /////////////////////////////////////////////////// 
        /////////////////////////////////////////////////// 
        public static void Load(Client.GameClient client) 
        { 
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT)) 
            { 
                cmd.Select("Way2Heroes").Where("UID", client.Entity.UID); 
                using (MySqlReader rdr = new MySqlReader(cmd)) 
                { 
                    if (rdr.Read()) 
                    { 
                        byte[] data = rdr.ReadBlob("Way2Heroes"); 
                        if (data.Length > 0) 
                        { 
                            using (var stream = new MemoryStream(data)) 
                            using (var reader = new BinaryReader(stream)) 
                            { 
                                int count = reader.ReadByte(); 
                                for (uint x = 0; x < count; x++) 
                                { 
                                    StageInfo item = new StageInfo(); 
                                    item = item.ReadItem(reader);
                                    client.Way2Heroes.Add(item.UID, item); 
                                } 
                            } 
                        } 
                    } 
                    else 
                    { 
                        using (var command = new MySqlCommand(MySqlCommandType.INSERT)) 
                        { 
                            command.Insert("Way2Heroes").Insert("UID", client.Entity.UID).Insert("Name", client.Entity.Name); 
                            command.Execute(); 
                        } 
                    } 
                } 
            }            
        } 
        /////////////////////////////////////////////////// 
        /////////////////////////////////////////////////// 
        public static void Save(Client.GameClient client) 
        { 
            MemoryStream stream = new MemoryStream(); 
            BinaryWriter writer = new BinaryWriter(stream); 
            writer.Write((byte)client.Way2Heroes.Count); 
            foreach (var prize in client.Way2Heroes.Values) 
                prize.WriteItem(writer); 
            string SQL = "UPDATE `Way2Heroes` SET Way2Heroes=@Way2Heroes where UID = " + client.Entity.UID + " ;"; 
            byte[] rawData = stream.ToArray(); 
            using (var conn = DataHolder.MySqlConnection) 
            { 
                conn.Open(); 
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand()) 
                { 
                    cmd.Connection = conn; 
                    cmd.CommandText = SQL; 
                    cmd.Parameters.AddWithValue("@Way2Heroes", rawData); 
                    cmd.ExecuteNonQuery(); 
                } 
            } 
        } 
        /////////////////////////////////////////////////// 
        ///////////////////////////////////////////////////        

        byte[] buffer; 
        public Way2Heroes(uint page) 
        { 
            buffer = new byte[11 + 8]; 
            Writer.Write((ushort)(buffer.Length - 8), 0, buffer); 
            Writer.Write(2831, 2, buffer); 
            Page = page; 
        } 

        private uint Page 
        { 
            get { return BitConverter.ToUInt32(buffer, 4); } 
            set {Writer.Write(value, 4, buffer); } 
        } 
        private bool AllDone 
        { 
            get { return buffer[8] == 1; } 
            set { buffer[8] = value == true ? (byte)1 : (byte)0; } 
        } 
        private ushort Count 
        { 
            get { return BitConverter.ToUInt16(buffer, 9); }
            set { Write(value, 9, buffer); } 
        } 

        int offset = 11; 
        public void check(StageInfo stages) 
        { 
            if (stages.SubStages.Count > 0) 
            { 
                byte[] bytes = new byte[11 + 8 + stages.SubStages.Count * 9]; 
                buffer.CopyTo(bytes, 0); 
                Writer.Write((ushort)(bytes.Length - 8), 0, bytes); 
                buffer = bytes; 


                Count = (ushort)stages.SubStages.Count; 
                AllDone = stages.Done; 

                var items = stages.SubStages.Values.ToArray(); 
                for (uint i = 0; i < items.Length; i++) 
                {
                    Write(items[i].UID, offset, buffer); offset += 8;//id                     
                    buffer[offset++] = items[i].Done == true ? (byte)1 : (byte)0; // //has something to claim     

                } 
            } 
        } 
        public void Send(GameClient client) 
        { 
            client.Send(this.Encode()); 
        } 
        public void Deserialize(byte[] Data) 
        { 
            buffer = Data; 
        } 
        public byte[] Encode() 
        { 
            return buffer; 
        } 
    } 
}  