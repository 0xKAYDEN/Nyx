using System;
using System.Collections.Generic;
using Nyx.Server.Network;
using Nyx.Server.Client;
using System.Linq;
using Nyx.Server.Network.GamePackets;
using System.IO;
using Nyx.Server.Database;

namespace Nyx.Server
{
    public enum QuestType : uint
    {
        RecruitQuest = 1,
        TutorialQuest = 2,
        DailyQuest = 3,
        EquipmentBonus = 4,
        Event = 5,
        Nezha_Feud = 6,
        RegionQuests = 7,
        Festival = 8,
        Cross_server = 9,
        KingdomWar = 10,
        ScrambleRealm = 11,
    }
    public enum QuestID : uint
    {
        SorrowofDesolation = 3644,
        FireForce = 3647,
        GloryOfThePast= 3637,
        SecretOfBright = 3638,
        HealingTheDying = 3642,
        PathToFlameTemple = 3643,
        TheWayofTommorow = 3631,
        UnexceptedDiscovery = 3639,
        BloodVengeance = 3634,
        MetalForce = 3646,
        MysteriousMetal = 3640,
        EvilRoot = 3635,
        ForgeFurance = 3636,
        EndHomelessness = 3649,
        SacrificetotheDead = 3641,
        WaitingForMiracle = 3648,
        UnknownDangers = 3632,
        WheelofNature = 3645,
        ScrambleforJustice = 35034,
        CrystalBounty = 35028,
        AshesOfAnger = 35024,
        BeastsOfLegend = 35025,
        DisCity1 = 6472,
        MonsterSage = 3633,
        Discity2 = 6473,
        DisCity3 = 6474,
        DisCity4 = 6475,
        ThunderStrike = 35007,
        WorshipLeaders = 6329,
        Spirit_Beads = 2375,
        TowerOfMystery = 6126,
        EvilLabyrinth = 6467,
        HeavenTreasury = 6390,
        Magnolias = 6014,
        TempestWing = 200,
        EveryThingHasAPrice = 6245,
        Release_the_souls = 6049,
        RareMaterials = 6366,
        SecondQuestStageOne = 2414,
        SkyPass = 6350,
        SecondQuestStageTwo = 2416,
        SecondQuestStageThree = 2418,
        SecondQuestStageFour = 2419,
        Secret_in_the_Chest = 3679,
        Weird_Formation = 3680,
        Mind_of_Evil = 3682,  
    }

    public class QuestPacket : Writer, Interfaces.IPacket
    {
        public enum QuestAction : ushort
        {
            Begin = 1,
            QuitQuest = 2,
            List = 3,
            Complete = 4
        }

        public struct QuestData
        {
            public enum QuestStatus : uint
            {
                Accepted = 0,
                Finished = 1,
                Available = 2
            }

            public QuestID UID;
            public QuestStatus Status;
            public uint Time;

            public static QuestData Create(QuestID _uid, QuestStatus _status, uint _time)
            {
                QuestData qItem = new QuestData();
                qItem.UID = _uid;
                qItem.Status = _status;
                qItem.Time = _time;
                return qItem;
            }
        }

        byte[] Buffer;
        public QuestPacket(bool Create, int count = 0)
        {
            if (Create)
            {
                Buffer = new byte[8 + 12 + 8 * count];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(1134, 2, Buffer);
                Amount = (ushort)count;
            }
        }
        ushort offset = 8;
        public void Apend(QuestData quest)
        {
            Write((uint)quest.UID, offset, Buffer);//uid 
            offset += 4;
            Write((uint)quest.Status, offset, Buffer);//sender
            offset += 4;
            Write(quest.Time, offset, Buffer);//Subject
            offset += 4;
        }

        public QuestAction Action
        {
            get { return (QuestAction)BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write((ushort)value, 4, Buffer); }
        }
        public ushort Amount
        {
            get { return BitConverter.ToUInt16(Buffer, 6); }
            set { Writer.Write(value, 6, Buffer); }
        }

        public QuestData this[int index]
        {
            get
            {
                QuestData data = new QuestData();
                data.UID = (QuestID)BitConverter.ToUInt32(Buffer, 8 + 12 * index);
                data.Status = (QuestData.QuestStatus)BitConverter.ToUInt32(Buffer, 8 + 12 * index + 4);
                data.Time = BitConverter.ToUInt32(Buffer, 8 + 12 * index + 8);
                return data;
            }
            set
            {
                Write((uint)value.UID, 8 + 12 * index, Buffer);
                Write((uint)value.Status, 8 + 12 * index + 4, Buffer);
                Write(value.Time, 8 + 12 * index + 8, Buffer);
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
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
    public class Quests
    {
        public class QuestItem
        {
            public QuestPacket.QuestData QItem;
            public uint Kills = 0;
            public uint Kills1 = 0;
            public uint Kills2 = 0;
            public string Mob = "";

            public static QuestItem Create(QuestPacket.QuestData Q_Item, uint Kills, string Mob = "")
            {
                QuestItem retn = new QuestItem();
                retn.QItem = Q_Item;
                retn.Mob = Mob;
                retn.Kills = Kills;
                return retn;
            }

            public void WriteItem(BinaryWriter writer)
            {
                writer.Write((uint)QItem.UID); //= reader.ReadUInt32();
                writer.Write((uint)QItem.Status);
                writer.Write(QItem.Time);
                writer.Write(Kills);
                writer.Write(Mob);
            }
            public QuestItem ReadItem(BinaryReader reader)
            {
                QItem = new QuestPacket.QuestData();
                QItem.UID = (QuestID)reader.ReadUInt32();//4
                QItem.Status = (QuestPacket.QuestData.QuestStatus)reader.ReadUInt32();//8
                QItem.Time = reader.ReadUInt32();//10
                Kills = reader.ReadUInt32();//12
                Mob = reader.ReadString();//14                         
                return this;
            }

        }
        public GameClient Player;
        public SafeDictionary<QuestID, QuestItem> src;
        public Quests(GameClient _owner)
        {
            Player = _owner;
            src = new SafeDictionary<QuestID, QuestItem>();
        }
        public void Reset(Client.GameClient client, bool OnlyDaiilyQuest)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT))
            {
                cmd.Select("quests").Where("UID", client.Entity.UID);
                using (MySqlReader rdr = new MySqlReader(cmd))
                {
                    if (rdr.Read())
                    {
                        if (!OnlyDaiilyQuest)
                        {
                            var array = src.Values.Where(p => QuestInfo.CheckType((QuestID)p.QItem.UID) == QuestType.DailyQuest && p.QItem.Status == QuestPacket.QuestData.QuestStatus.Finished).ToArray();
                            var array4 = src.Values.Where(p => QuestInfo.CheckType((QuestID)p.QItem.UID) == QuestType.Cross_server && p.QItem.Status == QuestPacket.QuestData.QuestStatus.Finished).ToArray();
                          
                            for (int i = 0; i < array4.Length; i++)
                            {
                                src.Remove(array4[i].QItem.UID);
                            }
                            for (int i = 0; i < array.Length; i++)
                            {
                                src.Remove(array[i].QItem.UID);
                            }
                            
                            array4 = null;
                            array = null;
                            //Load();
                        }
                        else
                        {
                            var array = src.Values.Where(p => QuestInfo.CheckType((QuestID)p.QItem.UID) == QuestType.DailyQuest && p.QItem.Status == QuestPacket.QuestData.QuestStatus.Finished).ToArray();

                            for (int i = 0; i < array.Length; i++)
                            {
                                src.Remove(array[i].QItem.UID);
                            }
                            array = null;
                          //  Load();
                        }
                    }
                    else
                    {
                        using (var command = new MySqlCommand(MySqlCommandType.INSERT).Insert("quests"))
                        {
                            command.Insert("UID", client.Entity.UID).Insert("Name", client.Entity.Name);
                            command.Execute();
                        }
                    }
                }
            }
        }
        public void Load()
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT))
            {
                cmd.Select("quests").Where("UID", Player.Entity.UID);
                using (MySqlReader rdr = new MySqlReader(cmd))
                {
                    if (rdr.Read())
                    {
                        byte[] data = rdr.ReadBlob("quests");
                        if (data.Length > 0)
                        {
                            using (var stream = new MemoryStream(data))
                            using (var reader = new BinaryReader(stream))
                            {
                                int count = reader.ReadByte();
                                for (uint x = 0; x < count; x++)
                                {
                                    QuestItem item = new QuestItem();
                                    item = item.ReadItem(reader);
                                    src.Add(item.QItem.UID, item);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (var command = new MySqlCommand(MySqlCommandType.INSERT).Insert("quests"))
                        {
                            command.Insert("UID", Player.Entity.UID).Insert("Name", Player.Entity.Name);
                            command.Execute();
                        }
                    }
                }
            }
        }
        public void Save()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((byte)src.Count);
            foreach (var quest in src.Values)
                quest.WriteItem(writer);
            string SQL = "UPDATE `quests` SET quests=@quests where UID = " + Player.Entity.UID + " ;";
            byte[] rawData = stream.ToArray();
            using (var conn = DataHolder.MySqlConnection)
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@quests", rawData);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void IncreaseQuestKills(QuestID UID, uint Kills)
        {
            if (src.ContainsKey(UID) && IsActiveQuest(UID))
            {
                var item = src[UID];
                item.Kills += Kills;
                src[UID] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq[1] = item.Kills;
                dataq[2] = item.Kills;
                Player.Send(dataq);
            }
        }
        public void IncreaseQuestKills5(QuestID UID, uint Kills)
        {
            if (src.ContainsKey(UID) && IsActiveQuest(UID))
            {
                var item = src[UID];
                item.Kills += Kills;
                src[UID] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq[1] = item.Kills;
                if (item.Kills == 15)
                {
                    dataq.Bright = 1;
                }
                Player.Send(dataq);
            }
        }
        #region Realm
        public void ThunderStrike(QuestID UID, uint Kills)
        {
            if (src.ContainsKey(UID) && IsActiveQuest(UID))
            {
                var item = src[UID];
                item.Kills += Kills;
                src[UID] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq.ThunderStrike = item.Kills;
                Player.Send(dataq);
            }
        }
        public void CrystalBounty(QuestID UID, uint Kills)
        {
            if (src.ContainsKey(UID) && IsActiveQuest(UID))
            {
                var item = src[UID];
                item.Kills += Kills;
                src[UID] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq.CrystalBounty = item.Kills;
                Player.Send(dataq);
            }
        }
        public void AshesOfAnger/*Realm*/(QuestID UID,  uint Kills1)
        {
            if (src.ContainsKey(UID))
            {
                var item = src[UID];
                item.Kills += Kills1;
                src[UID] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq.AshesOfAnger = item.Kills;
                Player.Send(dataq);
            }
        }
        #endregion
        public void IncreaseQuestDones(QuestID UID, uint item1, uint item2 = 0, uint item3 = 0, uint item4 = 0)
        {
            if (src.ContainsKey(UID) && IsActiveQuest(UID))
            {
                var item = src[UID];
                item.Kills += item1 + item2 + item3 + item4;
                src[UID] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq[1] = item1;
                dataq[2] = item2;
                dataq[3] = item3;
                dataq[4] = item4;
                Player.Send(dataq);
            }
        }
        public void DoneTowerMystery()
        {
            if (src.ContainsKey(QuestID.TowerOfMystery) && IsActiveQuest(QuestID.TowerOfMystery))
            {
                var item = src[QuestID.TowerOfMystery];
                item.Kills = 1;
                src[QuestID.TowerOfMystery] = item;

                QuestData dataq = new QuestData(true);
                dataq.UID = QuestID.TowerOfMystery;
                dataq[1] = 1;
                dataq[2] = 1;
                Player.Send(dataq);
            }
        }
        public void IncreaseQuestKills2/*SecondQuestStage3*/(QuestID UID, uint item1)
        {
            if (src.ContainsKey(UID) && IsActiveQuest(UID))
            {

                var item = src[UID];
                item.Kills += item1;
                src[UID] = item;
                uint kills2 = item.Kills;
                QuestData dataq = new QuestData(true);
                dataq.UID = UID;
                dataq[2] = dataq[2] + item1;
                while (kills2 >= 8)
                {
                    dataq[1] += 1;
                    dataq[2] = 0;
                    kills2 -= 8;
                }
                Player.Send(dataq);
            }
        }
        private bool IsActiveQuest(QuestID UID)
        {
            return CheckQuest(UID, QuestPacket.QuestData.QuestStatus.Accepted);
        }
        private bool CheckQuest(QuestID UID, QuestPacket.QuestData.QuestStatus status)
        {
            return src.Values.Where((p) => p.QItem.UID == UID && p.QItem.Status == status).Count() == 1;
        }
        public bool HasQuest(QuestID UID)
        {
            if (src.ContainsKey(UID))
                return true;
            return false;
        }
        public QuestPacket.QuestData.QuestStatus CheckQuest(QuestID UID)
        {
            if (!src.ContainsKey(UID))
                src.Add(UID, new QuestItem());
            return src[UID].QItem.Status;
        }
        public QuestItem GetQuest(QuestID UID)
        {
            return src[UID];
        }
        public bool FinishQuest(QuestID UID)
        {
            if (src[UID] != null && src.ContainsKey(UID))
            {
                var item = src[UID];
                if (item.QItem.Status != QuestPacket.QuestData.QuestStatus.Finished)
                {
                    item.QItem.Status = QuestPacket.QuestData.QuestStatus.Finished;
                    src[UID] = item;
                    SendSinglePacket(item.QItem, QuestPacket.QuestAction.Complete);
                    return true;
                }
            }
           
            return false;
        }
        public QuestItem Accept(QuestID UID, uint time = 0)
        {
            if (!src.ContainsKey(UID))
            {
                var n_quest = QuestItem.Create(QuestPacket.QuestData.Create(UID, QuestPacket.QuestData.QuestStatus.Accepted, time), 0);
                src.Add(n_quest.QItem.UID, n_quest);
                SendSinglePacket(n_quest.QItem, QuestPacket.QuestAction.Begin);
                return n_quest;
            }
            return new QuestItem();
        }

        private void SendSinglePacket(QuestPacket.QuestData data, QuestPacket.QuestAction mode)
        {
            QuestPacket Packet = new QuestPacket(true, 1);
            Packet.Action = mode;
            Packet.Apend(data);
            Player.Send(Packet);
        }
        int AcceptQuestsCount()
        {
            return src.Values.Where((p) => p.QItem.Status == QuestPacket.QuestData.QuestStatus.Accepted).Count();
        }
        public bool AllowAccept()
        {
            return AcceptQuestsCount() < 20;
        }
        private void SendAutoPacket(string Text, ushort map, ushort x, ushort y, uint NpcUid)
        {
            Player.MessageBox(Text, p =>
            {
                Data datapacket = new Data(true);
                datapacket.UID = p.Entity.UID;
                datapacket.ID = 162;
                datapacket.TimeStamp2 = NpcUid;
                datapacket.wParam1 = x;
                datapacket.wParam2 = y;
                p.Send(datapacket);

            }, null, 0);
        }
        public void SendFullGUI()
        {
            Dictionary<int, Queue<QuestPacket.QuestData>> Collection = new Dictionary<int, Queue<QuestPacket.QuestData>>();
            Collection.Add(0, new Queue<QuestPacket.QuestData>());

            int count = 0;
            var Array = QuestInfo.AllQuests.Values.ToArray();

            for (uint x = 0; x < Array.Length; x++)
            {
                if (x % 80 == 0)
                {
                    count++;
                    Collection.Add(count, new Queue<QuestPacket.QuestData>());
                }
                if (src.ContainsKey((QuestID)Array[x].MissionId))
                    Collection[count].Enqueue(src[Array[x].MissionId].QItem);
                else
                {
                    var quest = QuestPacket.QuestData.Create(Array[x].MissionId,
                        QuestPacket.QuestData.QuestStatus.Available, 0);
                    Collection[count].Enqueue(quest);
                }
            }
            foreach (var aray in Collection.Values)
            {
                Queue<QuestPacket.QuestData> ItemArray = aray;

                QuestPacket Packet = new QuestPacket(true, ItemArray.Count);
                Packet.Action = QuestPacket.QuestAction.List;
                for (byte x = 0; x < Packet.Amount; x++)
                {
                    var item = ItemArray.Dequeue();
                    if (QuestInfo.CheckType(item.UID) == QuestType.DailyQuest)
                        item.Status = QuestPacket.QuestData.QuestStatus.Available;
                    Packet.Apend(item);
                }
                Player.Send(Packet);
            }
        }

        public bool QuitQuest(QuestID UID)
        {
            if (src.ContainsKey(UID))
            {
                var item = src[UID];
                if (item.QItem.Status == QuestPacket.QuestData.QuestStatus.Accepted)
                {
                    item.QItem.Status = QuestPacket.QuestData.QuestStatus.Available;
                    src[UID] = item;
                    SendSinglePacket(item.QItem, QuestPacket.QuestAction.QuitQuest);
                    src.Remove(UID);
                    return true;
                }
            }
            return false;
        }
    }
    public class QuestInfo
    {
        public struct Info
        {
            public QuestID MissionId;
            public QuestType TypeId;
        }
        public static uint ActionBase;
        public static SafeDictionary<QuestID, Info> AllQuests = new SafeDictionary<QuestID, Info>();
        public static void Load()
        {
            string[] text = File.ReadAllLines("database\\Questinfo.ini");
            Info info = new Info();
            for (int x = 0; x < text.Length; x++)
            {
                string line = text[x];
                string[] split = line.Split('=');
                if (split[0] == "ActionBase")
                    ActionBase = uint.Parse(split[1]);
                else if (split[0] == "TotalMission")
                    AllQuests = new SafeDictionary<QuestID, Info>(int.Parse(split[1]));
                else if (split[0] == "MissionId")
                {
                    var id = (QuestID)uint.Parse(split[1]);

                    if (!AllQuests.ContainsKey(id))
                        AllQuests.Add(id, info);
                    else
                        info = AllQuests[id];

                    info.MissionId = id;

                }
                else if (split[0] == "TypeId")
                    info.TypeId = (QuestType)byte.Parse(split[1]);
            }
        }
        public static QuestType CheckType(QuestID UID)
        {
            return AllQuests[UID].TypeId;
        }
    }
    public class QuestData : Writer, Interfaces.IPacket
    {
        byte[] Buffer;
        public QuestData(bool Create)
        {
            if (true)
            {
                Buffer = new byte[60];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(1135, 2, Buffer);
            }
        }

        public QuestID UID
        {
            get { return (QuestID)BitConverter.ToUInt32(Buffer, 8); }
            set { Write((uint)value, 8, Buffer); }
        }

        public uint this[int index]
        {
            get { return BitConverter.ToUInt32(Buffer, 8 + 4 * index); }
            set { Write(value, 8 + 4 * index, Buffer); }
        }
        public uint Bright
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }
        public uint AshesOfAnger
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }
        public uint CrystalBounty
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public uint ThunderStrike
        {
            get { return BitConverter.ToUInt32(Buffer, 32); }
            set { Write(value, 32, Buffer); }
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
            client.Send(Buffer);
        }
    }
}
