using System;

namespace Nyx.Server.Network.GamePackets
{
    public class Activeness : Writer, Interfaces.IPacket
    {
        byte[] Buffer;
        public enum Types : byte
        {
            NumberOfTasks = 14,
            LoginTask = 25,
            VIPTask = 26,
            HoursTask = 27,
            LotteryTask = 28,
            DailyQuestTask = 29,
            FlowerTask = 30,
            QualiferTask = 31,
            ChampionPTask = 32,
            HorseRacingTask = 33,
            TIBTask = 34,
            Enlightenment = 35,
            TeamQualiferTask = 36,
            ChiTask = 37,
            JiangTask = 38,
        }
        public Activeness(Client.GameClient client, bool Ok = true)
        {
            if (Ok)
            {
                Buffer = new byte[90 + 8];
                Buffer[0] = 90;
                Writer.Write(2820, 2, Buffer);
                Buffer[4] = 3;
                Buffer[5] = (byte)Types.NumberOfTasks;
                Buffer[6] = (byte)Types.LoginTask;
                Buffer[12] = (byte)Types.VIPTask;
                Buffer[18] = (byte)Types.HoursTask;
                Buffer[24] = (byte)Types.LotteryTask;
                Buffer[30] = (byte)Types.DailyQuestTask;
                Buffer[36] = (byte)Types.FlowerTask;
                Buffer[42] = (byte)Types.QualiferTask;
                Buffer[48] = (byte)Types.ChampionPTask;
                Buffer[72] = (byte)Types.TeamQualiferTask;
                Buffer[54] = (byte)Types.HorseRacingTask;
                Buffer[60] = (byte)Types.TIBTask;
                Buffer[66] = (byte)Types.Enlightenment;
                Buffer[78] = (byte)Types.ChiTask;
                Buffer[84] = (byte)Types.JiangTask;
            }
        }
        public byte GetMaxTimesOfTask(Types type)
        {
            byte Count = 0;
            if (type == (Types)25)
                Count = 1;
            if (type == (Types)26)
                Count = 1;
            if (type == (Types)27)
                Count = 5;
            if (type == (Types)28)
                Count = 10;
            if (type == (Types)29)
                Count = 8;
            if (type == (Types)30)
                Count = 1;
            if (type == (Types)31)
                Count = 5;
            if (type == (Types)32)
                Count = 1;
            if (type == (Types)33)
                Count = 1;
            if (type == (Types)34)
                Count = 1;
            if (type == (Types)35)
                Count = 3;
            if (type == (Types)36)
                Count = 5;
            if (type == (Types)37)
                Count = 12;
            if (type == (Types)38)
                Count = 12;
            return Count;
        }
        public void SendSinglePacket(Client.GameClient client, Types type, byte value)
        {
            if (value > GetMaxTimesOfTask(type))
            {
                value = GetMaxTimesOfTask(type);
            }
            byte[] ary = new byte[12+ 8];
            Writer.Write((byte)12, 0, ary);
            Writer.Write((ushort)2820, 2, ary);
            ary[4] = 3;
            ary[5] = 1;
            ary[6] =(byte)type;
            if (value >= GetMaxTimesOfTask(type))
            {
                ary[10] = 1;
            }
            ary[11] = value;
            client.Send(ary);
            if (type == (Types)25)
            {
                client.Activenes.LoginTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteLoginTask = 1;
                }
            }
            if (type == (Types)26)
            {
                client.Activenes.VIPActivenessTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteVIPActiveness = 1;
                }
            }
            if (type == (Types)27)
            {
                client.Activenes.HalfAnHourOnlineTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteHalfAnHourOnline = 1;
                }
            }
            if (type == (Types)28)
            {
                client.Activenes.LotteryTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteLottery = 1;
                }
            }
            if (type == (Types)29)
            {
                client.Activenes.DailyQuestTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteDailyQuest = 1;
                }
            }
            if (type == (Types)30)
            {
                client.Activenes.FlowerAndGiftTime = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteFlowerAndGift = 1;
                }
            }
            if (type == (Types)31)
            {
                client.Activenes.QualiferTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteQualifer = 1;
                }
            }
            if (type == (Types)32)
            {
                client.Activenes.ChampionPointsTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteChampionPoints = 1;
                }
            }
            if (type == (Types)33)
            {
                client.Activenes.HorseRacingTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.HorseRacingComplete = 1;
                }
            }
            if (type == (Types)34)
            {
                client.Activenes.TIBTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteTIB = 1;
                }
            }
            if (type == (Types)35)
            {
                client.Activenes.EnlightenmentTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteEnlightenment = 1;
                }
            }
            if (type == (Types)36)
            {
                client.Activenes.TeamQualiferTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteTeamQualifer = 1;
                }
            }
            if (type == (Types)37)
            {
                client.Activenes.ChiStudyTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteChiStudy = 1;
                }
            }
            if (type == (Types)38)
            {
                client.Activenes.JiangStudyTimes = value;
                if (value >= GetMaxTimesOfTask(type))
                {
                    client.Activenes.CompleteJiangStudy = 1;
                }
            }
            ActivenessPoints AP = new ActivenessPoints();
            AP.Points = AP.GetActivenessPoints(client.Activenes.Encode());
            client.ActivenessPoints = AP.Points;
            AP.Send(client);
        }
        public static void Login(Client.GameClient client, bool JustLoggingNow = false)
        {
            Activeness AN = new Activeness(client);
            AN.CompleteLoginTask = 1;
            AN.LoginTimes = 1;
            if (client.Entity.VIPLevel > 0)
            {
                AN.CompleteVIPActiveness = 1;
                AN.VIPActivenessTimes = 1;
            }
            else
            {
                AN.CompleteVIPActiveness = 0;
                AN.VIPActivenessTimes = 0;
            }
            if (JustLoggingNow)
            {
                AN.HalfHourTask = Time32.Now;
            }
            AN.HalfAnHourOnlineTimes = Math.Min((byte)5, client.Entity.HoursTimes);
            if (Math.Min((byte)5, client.Entity.HoursTimes) >= 5)
            {
                AN.CompleteHalfAnHourOnline = 1;
            }
            if (client.LotteryEntries >= 10)
            {
                AN.CompleteLottery = 1;
                AN.LotteryTimes = 10;
            }
            else
            {
                AN.CompleteLottery = 0;
                AN.LotteryTimes = client.LotteryEntries;
            }
            if (AN.DailyQuestCompletedTimes(client) >= 8)
            {
                AN.CompleteDailyQuest = 1;
                AN.DailyQuestTimes = AN.DailyQuestCompletedTimes(client);
            }
            else
            {
                AN.CompleteDailyQuest = 0;
                AN.DailyQuestTimes = AN.DailyQuestCompletedTimes(client);
            }
            if (client.Entity.MyFlowers.aFlower >= 1)
            {
                AN.CompleteFlowerAndGift = 0;
                AN.FlowerAndGiftTime = 0;
            }
            else
            {
                AN.CompleteFlowerAndGift = 1;
                AN.FlowerAndGiftTime = 1;
            }
            if (client.ArenaStatistic != null && client.ArenaStatistic.TodayBattles >= 5)
            {
                AN.CompleteQualifer = 1;
                AN.QualiferTimes = 5;
            }
            else
            {
                AN.CompleteQualifer = 0;
                AN.QualiferTimes = (byte)client.ArenaStatistic.TodayBattles;
            }
            AN.ChiStudyTimes = Math.Min((byte)client.Entity.ChiStudyTimes, (byte)12);
            AN.JiangStudyTimes = Math.Min((byte)client.Entity.JiangStudyTimes, (byte)12);
            if (AN.ChiStudyTimes >= 12)
            {
                AN.CompleteChiStudy = 1;
            }
            if (AN.JiangStudyTimes >= 12)
            {
                AN.CompleteJiangStudy = 1;
            }
            if (client.CP != null && client.CP.TodayPoints != 0)
            {
                AN.CompleteChampionPoints = 1;
                AN.ChampionPointsTimes = 1;
            }
            else
            {
                AN.CompleteChampionPoints = 0;
                AN.ChampionPointsTimes = 0;
            }
            if (client.RacePoints != 0)
            {
                AN.HorseRacingComplete = 1;
                AN.HorseRacingTimes = 1;
            }
            else
            {
                AN.HorseRacingComplete = 0;
                AN.HorseRacingTimes = 0;
            }
            if (client.Entity.DidTIB == true)
            {
                AN.TIBTimes = 1;
                AN.CompleteTIB = 1;
            }
            else
            {
                AN.CompleteTIB = 0;
                AN.TIBTimes = 0;
            }
            if (client.Entity.EnlightenPoints != 0)
            {
                byte EnlightenP = (byte)Math.Min((byte)3, client.Entity.EnlightmentTimes);
                AN.EnlightenmentTimes = EnlightenP;
                if (AN.EnlightenmentTimes >= 3)
                    AN.CompleteEnlightenment = 1;
            }
            if (client.TeamArenaStatistic != null && client.TeamArenaStatistic.TodayBattles >= 2)
            {
                AN.CompleteTeamQualifer = 1;
                AN.TeamQualiferTimes = 2;
            }
            else
            {
                AN.CompleteTeamQualifer = 0;
                AN.TeamQualiferTimes = (byte)client.TeamArenaStatistic.TodayBattles;
            }
            AN.Send(client);
            ActivenessPoints AP = new ActivenessPoints();
            AP.Points = AP.GetActivenessPoints(AN.Encode());
            client.ActivenessPoints = AP.Points;
            AP.Send(client);
            if (client.Entity.ClaimedActivenessGift1 == 1)
            {
                ClaimActivenessGift CAG = new ClaimActivenessGift();
                CAG.Type = 1;
                client.Send(CAG.Encode());
            }
            if (client.Entity.ClaimedActivenessGift2 == 1)
            {
                ClaimActivenessGift CAG = new ClaimActivenessGift();
                CAG.Type = 2;
                client.Send(CAG.Encode());
            }
            if (client.Entity.ClaimedActivenessGift3 == 1)
            {
                ClaimActivenessGift CAG = new ClaimActivenessGift();
                CAG.Type = 3;
                client.Send(CAG.Encode());
            }
            client.Activenes = AN;
            client.Entity.LoadedActivenessSystem = true;
            if (client.ActivenessPoints >= 320)
            {
                new TitleStorage().AddTitle(client, 2001, 2, false);
            }
        }
        public byte DailyQuestCompletedTimes(Client.GameClient client)
        {
            byte count = 0;
            if (client.Quests.HasQuest(QuestID.WorshipLeaders))
                if (client.Quests.CheckQuest(QuestID.WorshipLeaders) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.EveryThingHasAPrice))
                if (client.Quests.CheckQuest(QuestID.EveryThingHasAPrice) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.TowerOfMystery))
                if (client.Quests.CheckQuest(QuestID.TowerOfMystery) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.HeavenTreasury))
                if (client.Quests.CheckQuest(QuestID.HeavenTreasury) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.Magnolias))
                if (client.Quests.CheckQuest(QuestID.Magnolias) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.RareMaterials))
                if (client.Quests.CheckQuest(QuestID.RareMaterials) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.Release_the_souls))
                if (client.Quests.CheckQuest(QuestID.Release_the_souls) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            if (client.Quests.HasQuest(QuestID.Spirit_Beads))
                if (client.Quests.CheckQuest(QuestID.Spirit_Beads) == QuestPacket.QuestData.QuestStatus.Finished)
                    count++;
            return count;
        }
         public byte CompleteLoginTask
         {
             get { return Buffer[10]; }
             set { Buffer[10] = value; }
         }
         public byte LoginTimes
         {
             get { return Buffer[11]; }
             set { Buffer[11] = value; }
         }
         public byte CompleteVIPActiveness 
         {
             get { return Buffer[16]; }
             set { Buffer[16] = value; }
         }
         public byte VIPActivenessTimes 
         {
             get { return Buffer[17]; }
             set { Buffer[17] = value; }
         }
         public byte CompleteHalfAnHourOnline 
         {
             get { return Buffer[22]; }
             set { Buffer[22] = value; }
         }
         public Time32 HalfHourTask = Time32.Now;
         public byte HalfAnHourOnlineTimes 
         {
             get { return Buffer[23]; }
             set { Buffer[23] = value; }
         }
         public byte CompleteLottery 
         {
             get { return Buffer[28]; }
             set { Buffer[28] = value; }
         }
         public byte LotteryTimes 
         {
             get { return Buffer[29]; }
             set { Buffer[29] = value; }
         }
         public byte CompleteDailyQuest 
         {
             get { return Buffer[34]; }
             set { Buffer[34] = value; }
         }
         public byte DailyQuestTimes 
         {
             get { return Buffer[35]; }
             set { Buffer[35] = value; }
         }
         public byte CompleteFlowerAndGift 
         {
             get { return Buffer[40]; }
             set { Buffer[40] = value; }
         }
         public byte FlowerAndGiftTime 
         {
             get { return Buffer[41]; }
             set { Buffer[41] = value; }
         }
         public byte CompleteQualifer 
         {
             get { return Buffer[46]; }
             set { Buffer[46] = value; }
         }
         public byte QualiferTimes 
         {
             get { return Buffer[47]; }
             set { Buffer[47] = value; }
         }
         public byte CompleteChampionPoints 
         {
             get { return Buffer[52]; }
             set { Buffer[52] = value; }
         }
         public byte ChampionPointsTimes 
         {
             get { return Buffer[53]; }
             set { Buffer[53] = value; }
         }
         public byte HorseRacingComplete 
         {
             get { return Buffer[58]; }
             set { Buffer[58] = value; }
         }
         public byte HorseRacingTimes 
         {
             get { return Buffer[59]; }
             set { Buffer[59] = value; }
         }
         public byte CompleteTIB 
         {
             get { return Buffer[64]; }
             set { Buffer[64] = value; }
         }
         public byte TIBTimes 
         {
             get { return Buffer[65]; }
             set { Buffer[65] = value; }
         }
         public byte CompleteEnlightenment 
         {
             get { return Buffer[70]; }
             set { Buffer[70] = value; }
         }
         public byte EnlightenmentTimes 
         {
             get { return Buffer[71]; }
             set { Buffer[71] = value; }
         }
         public byte CompleteTeamQualifer 
         {
             get { return Buffer[76]; }
             set { Buffer[76] = value; }
         }
         public byte TeamQualiferTimes 
         {
             get { return Buffer[77]; }
             set { Buffer[77] = value; }
         }
         public byte CompleteChiStudy 
         {
             get { return Buffer[82]; }
             set { Buffer[82] = value; }
         }
         public byte ChiStudyTimes 
        {
            get { return Buffer[83]; }
            set { Buffer[83] = value; }
        }
         public byte CompleteJiangStudy 
        {
            get { return Buffer[88]; }
            set { Buffer[88] = value; }
        }
         public byte JiangStudyTimes 
        {
            get { return Buffer[89]; }
            set { Buffer[89] = value; }
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            if (Buffer != null)
                client.Send(Buffer);
        }
    }
    public class ClaimActivenessGift : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public ClaimActivenessGift()
        {
            Buffer = new byte[30 + 8];
            Buffer[0] = 30;//Length
            Writer.Write(2822, 2, Buffer);
        }
        public byte Type//1 To Complete
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            if (Buffer != null)
                client.Send(Buffer);
        }
    }
    public class ActivenessPoints : Writer, Interfaces.IPacket
    {
        byte[] Buffer;

        public ActivenessPoints()
        {
            Buffer = new byte[30 + 8];
            Buffer[0] = 30;
            Writer.Write(2823, 2, Buffer);
            Buffer[4] = 1;
            Buffer[5] = 2;
            Buffer[9] = 1;
        }
        public ushort GetActivenessPoints(byte[] buffer)
        {
            ushort points = 0;
            if (buffer != null)
            {
                points += ((byte)(buffer[11] * 5));
                points += ((byte)(buffer[17] * 5));
                points += ((byte)(buffer[23] * 5));
                points += ((byte)(buffer[29] * 5));
                points += ((byte)(buffer[35] * 5));
                points += ((byte)(buffer[41] * 5));
                points += ((byte)(buffer[47] * 5));
                points += ((byte)(buffer[53] * 10));
                points += ((byte)(buffer[59] * 10));
                points += ((byte)(buffer[65] * 10));
                points += ((byte)(buffer[71] * 5));
                points += ((byte)(buffer[77] * 10));
                points += ((byte)(buffer[83] * 5));
                points += ((byte)(buffer[89] * 5));
                return points;
            }
            return 0;
        }
        public byte Type
        {
            get { return Buffer[4]; }
            set { Buffer[4] = value; }
        }
        public ushort Points
        {
            get { return BitConverter.ToUInt16(Buffer, 13); }
            set {Writer.Write(value, 13, Buffer); }
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            if (Buffer != null)
                client.Send(Buffer);
        }
    }
}
