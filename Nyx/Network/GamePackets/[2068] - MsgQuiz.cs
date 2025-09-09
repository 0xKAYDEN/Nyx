using System;
using Nyx.Server.Client;
using Nyx.Server.Network;
using Nyx.Server.Interfaces;

namespace Nyx.Server.Network.GamePackets
{
    public unsafe class QuizShowTypes
    {
        public const ushort
        Open = 1,
        SendQuestion = 2,
        ReceiveToping = 3,
        SendTop = 4,
        History = 5;
    }
    public unsafe class OpenQuiz : Writer, IPacket
    {
        byte[] Buffer;
        public OpenQuiz()
        {
            Buffer = new byte[52];
            Writer.Write(44, 0, Buffer);
            Writer.Write(2068, 2, Buffer);
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }
        public ushort StartInTimeSecouds
        {
            get { return BitConverter.ToUInt16(Buffer, 6); }
            set { Writer.Write(value, 6, Buffer); }
        }
        public ushort AllQuestions
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }
        public ushort FullTimeLimit
        {
            get { return BitConverter.ToUInt16(Buffer, 10); }
            set { Writer.Write(value, 10, Buffer); }
        }
        public ushort ExpBallFirst
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }
        public ushort ExpBall2nd
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set { Writer.Write(value, 14, Buffer); }
        }
        public ushort ExpBall3rd
        {
            get { return BitConverter.ToUInt16(Buffer, 16); }
            set { Writer.Write(value, 16, Buffer); }
        }
        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
        public static void Process(byte[] packet, Client.GameClient client)
        {
            const byte GiveAwaser = 3, Remove = 8;
            byte Typ = packet[4];
            var quizShow = Kernel.QuizShow;
            switch (Typ)
            {
                case GiveAwaser:
                    {
                        if (quizShow.Open)
                        {
                            if (client.Quiz == null)
                                client.Quiz = quizShow.RegisteredUsers[client.Entity.UID];

                            if (client.Quiz == null) break;

                            client.Quiz.Timer += (ushort)quizShow.NewQuestionTime;
                            byte answer = packet[8];
                            if (answer > 4) break;
                            bool right = answer == quizShow.CurrentQuestion.AnswerRight;

                            client.Quiz.Answered = true;
                            byte pts = 0;
                            if (right)
                            {
                                pts = (byte)(quizShow.RightAnswerReward + quizShow.TimeLimit - quizShow.NewQuestionTime);

                                client.Quiz.RightQuestion = 1;
                                client.Quiz.Points += pts;
                                client.Entity.QuizPoints += pts;
                            }
                            else
                            {
                                pts = 1;

                                client.Quiz.RightQuestion = 2;
                                client.Quiz.Points += pts;
                                client.Entity.QuizPoints += pts;
                            }
                            client.IncreaseExperience(client.ExpBall / 300 * pts, false);

                            if (quizShow.FirstQuestion)
                            {
                                QuizRank rnk = new QuizRank();
                                rnk.GiveRight = client.Quiz.RightQuestion;
                                rnk.MyPoints = (ushort)client.Quiz.Points;
                                rnk.MyRank = 0;
                                rnk.MyTime = client.Quiz.Timer;
                                rnk.Type = QuizShowTypes.SendTop;
                                client.Send(rnk.Encode());
                            }
                        }
                        break;
                    }
                case Remove:
                    {
                        if (quizShow.Open)
                            quizShow.RemovePlayer(client);
                        break;
                    }
            }
        }
    }
    public unsafe class QuizQuestions : Writer, IPacket
    {
        byte[] Buffer;
        private ushort GetLeng(string[] ques)
        {
            ushort set = 0;
            foreach (string add in ques)
                set += (ushort)add.Length;

            return set;
        }
        public QuizQuestions(string[] Questions)
        {
            Buffer = new byte[52 + 4 + GetLeng(Questions)];
            Writer.Write((ushort)(44 + 4 + GetLeng(Questions)), 0, Buffer);
            Writer.Write(2068, 2, Buffer);

            Buffer[24] = (byte)Questions.Length;

            int position = 25;
            for (ushort i = 0; i < (ushort)Questions.Length; i++)
            {
                Writer.WriteWithLength(Questions[i], position, Buffer);
                position += 1 + Questions[i].Length;
            }
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }
        public ushort NoQuestion
        {
            get { return BitConverter.ToUInt16(Buffer, 6); }
            set { Writer.Write(value, 6, Buffer); }
        }
        public ushort Right
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }
        public ushort AllQuestions
        {
            get { return BitConverter.ToUInt16(Buffer, 10); }
            set { Writer.Write(value, 10, Buffer); }
        }
        public ushort FullTimeLimit
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }
        public ushort MyPoints
        {
            get { return BitConverter.ToUInt16(Buffer, 14); }
            set { Writer.Write(value, 14, Buffer); }
        }
        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public unsafe class QuizRank : Writer, IPacket
    {
        byte[] Buffer;
        public QuizRank()
        {
            Buffer = new byte[92];
            Writer.Write(84, 0, Buffer);
            Writer.Write(2068, 2, Buffer);
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }
        public ushort MyPoints
        {
            get { return BitConverter.ToUInt16(Buffer, 6); }
            set { Writer.Write(value, 6, Buffer); }
        }
        public ushort MyTime
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }
        public byte MyRank
        {
            get { return Buffer[10]; }
            set { Buffer[10] = value; }
        }
        public byte GiveRight
        {
            get { return Buffer[11]; }
            set { Buffer[11] = value; }
        }
        public byte Count
        {
            get { return Buffer[20]; }
            set { Buffer[20] = value; }
        }
        ushort Position = 24;
        public void Aprend(string name, ushort BestScore, ushort BestTime)
        {
            if ((ushort)(Position + 20) <= (ushort)(Buffer.Length - 8))
            {
                Count++;
                Writer.Write(name, Position, Buffer);
                Position += 16;
                Writer.Write(BestScore, Position, Buffer);
                Position += 2;
                Writer.Write(BestTime, Position, Buffer);
                Position += 2;
            }
        }
        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
    public unsafe class QuizHistory : Writer, IPacket
    {
        byte[] Buffer;
        public QuizHistory()
        {
            Buffer = new byte[68];
            Writer.Write((ushort)(68 - 8), 0, Buffer);
            Writer.Write((ushort)2068, 2, Buffer);
        }
        public ushort Type
        {
            get { return BitConverter.ToUInt16(Buffer, 4); }
            set { Writer.Write(value, 4, Buffer); }
        }
        public byte MyRank
        {
            get { return Buffer[6]; }
            set { Buffer[6] = value; }
        }
        public ushort ExpBallsReceives
        {
            get { return BitConverter.ToUInt16(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }
        public ushort MyTime
        {
            get { return BitConverter.ToUInt16(Buffer, 10); }
            set { Writer.Write(value, 10, Buffer); }
        }
        public ushort MyPoints
        {
            get { return BitConverter.ToUInt16(Buffer, 12); }
            set { Writer.Write(value, 12, Buffer); }
        }
        ushort Position = 24;
        public void Append(string name, ushort BestScore, ushort BestTime)
        {
            if ((ushort)(Position + 20) <= (ushort)(Buffer.Length - 8))
            {
                Writer.Write(name, Position, Buffer);
                Position += 16;
                Writer.Write(BestScore, Position, Buffer);
                Position += 2;
                Writer.Write(BestTime, Position, Buffer);
                Position += 2;
            }
        }
        public void Send(GameClient client)
        {
            client.Send(Buffer);
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }
    }
}