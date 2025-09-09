using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Timers;
namespace Nyx.Server.Network.GamePackets
{
    public class MentorPremio
    {
        public const byte
        Show = 0,
        Training = 1,
        Hunting = 2;

        byte[] Packet;
        public MentorPremio()
        {
            Packet = new byte[28];
            Writer.Write(Packet.Length - 8, 0, Packet);
            Writer.Write(1036, 2, Packet);
        }
        public byte Action
        {
            get { return (byte)Packet[8]; }
            set { Packet[8] = value; }
        }
        public uint OnlineTraining
        {
            get { return BitConverter.ToUInt16(Packet, 14); }
            set { Writer.Write(value,14, Packet); }
        }
        public uint BlessedHunting
        {
            get { return BitConverter.ToUInt16(Packet, 18); }
            set { Writer.Write(value, 18, Packet); }
        }
        public byte[] Encode()
        {
            return Packet;
        }
        public void Deserialize(byte[] buffer)
        {
            Packet = buffer;
        }
        public static void Process(byte[] packet, Client.GameClient client)
        {
            MentorPremio Premio = new MentorPremio();
            Premio.Deserialize(packet);
            switch (Premio.Action)
            {
                case MentorPremio.Show:
                    {
                        Premio.OnlineTraining = client.Entity.OnlineTraining;
                        Premio.BlessedHunting = client.Entity.BlessedHunting;
                        client.Send(Premio.Encode());
                        break;
                    }
                case MentorPremio.Training:
                    {
                        client.IncreaseExperience((ulong)(((double)client.Entity.OnlineTraining / 606) * client.ExpBall), false);
                        client.Entity.OnlineTraining = 0;
                        Premio.Action = MentorPremio.Show;
                        Premio.OnlineTraining = client.Entity.OnlineTraining;
                        Premio.BlessedHunting = client.Entity.BlessedHunting;
                        client.Send(Premio.Encode());
                        break;
                    }
                case MentorPremio.Hunting:
                    {
                        client.IncreaseExperience((ulong)(client.Entity.BlessedHunting / 600.0 * client.ExpBall), false);
                        client.Entity.BlessedHunting = 0;
                        Premio.Action = MentorPremio.Show;
                        Premio.OnlineTraining = client.Entity.OnlineTraining;
                        Premio.BlessedHunting = client.Entity.BlessedHunting;
                        client.Send(Premio.Encode());
                        break;
                    }
                default:  break;
            }
        }
    }
    public class MentorPrize : Writer, Interfaces.IPacket
    {
        public const byte
        Show = 0,
        ClaimExperience = 1,
        ClaimSomeExperience = 2,
        ClaimHeavenBlessing = 3,
        ClaimPlus = 4,
        Unknown = 5;

        private byte[] Buffer;
        public MentorPrize(bool create)
        {
            if (!create)
            {
                Buffer = new byte[48];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(2067, 2, Buffer);
            }
        }
        public uint Prize_Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint Mentor_ID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public ulong Prize_Experience
        {
            get { return BitConverter.ToUInt64(Buffer, 24); }
            set { Writer.Write(value, 24, Buffer); }
        }
        public ushort Prize_HeavensBlessing
        {
            get { return BitConverter.ToUInt16(Buffer, 32); }
            set { Writer.Write(value, 32, Buffer); }
        }
        public ushort Prize_PlusStone
        {
            get { return BitConverter.ToUInt16(Buffer, 34); }
            set { Writer.Write(value, 34, Buffer); }
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
        public static void Process(byte[] packet, Client.GameClient client)
        {
            MentorPrize prize = new MentorPrize(false);
            prize.Deserialize(packet);
            switch (prize.Prize_Type)
            {
                case MentorPrize.ClaimExperience:
                case MentorPrize.ClaimSomeExperience:
                    {
                        client.IncreaseExperience((ulong)(((double)client.PrizeExperience / 600) * client.ExpBall), false);
                        client.PrizeExperience = 0;
                        foreach (var appr in client.Apprentices.Values)
                        {
                            appr.Actual_Experience = 0;
                            Database.KnownPersons.SaveApprenticeInfo(appr);
                        }
                        prize.Mentor_ID = client.Entity.UID;
                        prize.Prize_Type = MentorPrize.Show;
                        prize.Prize_Experience = client.PrizeExperience;
                        prize.Prize_HeavensBlessing = client.PrizeHeavenBlessing;
                        prize.Prize_PlusStone = client.PrizePlusStone;
                        client.Send(prize);
                        break;
                    }
                case MentorPrize.ClaimHeavenBlessing:
                    {
                        client.AddBless(client.PrizeHeavenBlessing);
                        client.Entity.Update((byte)Network.GamePackets.Update.OnlineTraining, client.OnlineTrainingPoints, false);
                        client.PrizeHeavenBlessing = 0;
                        foreach (var appr in client.Apprentices.Values)
                        {
                            appr.Actual_HeavenBlessing = 0;
                            Database.KnownPersons.SaveApprenticeInfo(appr);
                        }
                        prize.Mentor_ID = client.Entity.UID;
                        prize.Prize_Type = MentorPrize.Show;
                        prize.Prize_Experience = client.PrizeExperience;
                        prize.Prize_HeavensBlessing = client.PrizeHeavenBlessing;
                        prize.Prize_PlusStone = client.PrizePlusStone;
                        client.Send(prize);
                        break;
                    }
                case MentorPrize.ClaimPlus:
                    {
                        int stones = client.PrizePlusStone / 100;
                        int totake = stones;
                        if (stones > 0)
                        {
                            for (; stones > 0; stones--)
                            {
                                client.PrizePlusStone -= 100;
                                if (!client.Inventory.Add(730001, 1, 1))
                                    break;
                            }
                        }
                        foreach (var appr in client.Apprentices.Values)
                        {
                            if (appr.Actual_Plus >= totake)
                            {
                                appr.Actual_Plus -= (ushort)totake;
                                totake = 0;
                            }
                            else
                            {
                                totake -= appr.Actual_Plus;
                                appr.Actual_Plus = 0;
                            }
                            Database.KnownPersons.SaveApprenticeInfo(appr);
                        }
                        prize.Mentor_ID = client.Entity.UID;
                        prize.Prize_Type = MentorPrize.Show;
                        prize.Prize_Experience = client.PrizeExperience;
                        prize.Prize_HeavensBlessing = client.PrizeHeavenBlessing;
                        prize.Prize_PlusStone = client.PrizePlusStone;
                        client.Send(prize);
                        break;
                    }
                default:
                    {
                        prize.Mentor_ID = client.Entity.UID;
                        prize.Prize_Experience = client.PrizeExperience;
                        prize.Prize_HeavensBlessing = client.PrizeHeavenBlessing;
                        prize.Prize_PlusStone = client.PrizePlusStone;
                        client.Send(prize);
                        break;
                    }
            }
        }
    }
    public unsafe class MentorInformation : Writer, Interfaces.IPacket
    {
        private byte[] Buffer;
        private string m_MentorName = "", m_MentorSpouse = "", m_ApprenticeName = "";
        public MentorInformation(bool create)
        {
            if (create)
            {
                Buffer = new byte[158];
                Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
                Writer.Write(2066, 2, Buffer);
                Write(999999, 24, Buffer);
            }
        }
        public uint Mentor_Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint Mentor_ID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint Apprentice_ID
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public uint Mentor_Mesh
        {
            get { return BitConverter.ToUInt32(Buffer, 16); }
            set { Write(value, 16, Buffer); }
        }
        public uint Shared_Battle_Power
        {
            get { return BitConverter.ToUInt32(Buffer, 20); }
            set
            {
                if (value > 200)
                    value = 0;
                if (value < 0)
                    value = 0;
                Write(value, 20, Buffer);
            }
        }
        public uint Enrole_Date
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Write(value, 28, Buffer); }
        }
        public uint Mentor_Level
        {
            get { return BitConverter.ToUInt32(Buffer, 32); }
            set { Write(value, 32, Buffer); }
        }
        public byte Mentor_Class
        {
            get { return Buffer[33]; }
            set { Buffer[33] = value; }
        }
        public ushort Mentor_PkPoints
        {
            get { return BitConverter.ToUInt16(Buffer, 34); }
            set { Writer.Write(value, 34, Buffer); }
        }
        public bool Mentor_Online
        {
            get { return (Buffer[56] == 1); }
            set { Buffer[56] = (byte)(value ? 1 : 0); }
        }
        public uint Apprentice_Experience
        {
            get { return BitConverter.ToUInt32(Buffer, 64); }
            set { Write(value, 64, Buffer); }
        }
        public ushort Apprentice_Blessing
        {
            get { return BitConverter.ToUInt16(Buffer, 72); }
            set { Writer.Write(value, 72, Buffer); }
        }
        public ushort Apprentice_Composing
        {
            get { return BitConverter.ToUInt16(Buffer, 74); }
            set { Writer.Write(value, 74, Buffer); }
        }
        public byte String_Count
        {
            get { return Buffer[76]; }
            set { Buffer[76] = value; }
        }
        public string Mentor_Name
        {
            get { return m_MentorName; }
            set
            {
                m_MentorName = value;
                Writer.WriteWithLength(value, 77, Buffer);
            }
        }
        public string Mentor_Spouse_Name
        {
            get { return m_MentorSpouse; }
            set
            {
                m_MentorSpouse = value;
                Writer.WriteWithLength(value, 79 + Mentor_Name.Length, Buffer);
            }
        }
        public string Apprentice_Name
        {
            get { return m_ApprenticeName; }
            set
            {
                m_ApprenticeName = value;
                Writer.WriteWithLength(value, 81 + Mentor_Name.Length + Mentor_Spouse_Name.Length, Buffer);
            }
        }
        public void Null(int Offset)
        {
            Buffer[Offset] = 0;
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
            // client.Send(Buffer);
        }
    }
    public unsafe class MentorApprentice : Writer, Interfaces.IPacket
    {
        public const byte
        RequestApprentice = 1,
        RequestMentor = 2,
        LeaveMentor = 3,
        ExpellApprentice = 4,
        AcceptRequestApprentice = 8,
        AcceptRequestMentor = 9,
        DumpApprentice = 18,
        DumpMentor = 19;

        byte[] Buffer;
        public MentorApprentice(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[32];
                Writer.Write(24, 0, Buffer);
                Writer.Write(2065, 2, Buffer);
            }
        }
        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set { Write(value, 4, Buffer); }
        }
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Write(value, 8, Buffer); }
        }
        public uint dwParam
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }
        public byte Dynamic
        {
            get { return Buffer[16]; }
            set { Buffer[16] = value; }
        }
        public bool Online
        {
            get { return Buffer[20] == 1; }
            set { Buffer[20] = value == true ? (byte)1 : (byte)0; }
        }
        public string Name
        {
            get
            {
                return System.Text.Encoding.Default.GetString(Buffer, 22, Buffer[21]);
            }
            set
            {
                byte[] newBuffer = new byte[24 + value.Length + 8];
                Buffer.CopyTo(newBuffer, 0);
                Writer.Write((ushort)(24 + value.Length), 0, newBuffer);
                Writer.WriteWithLength(value, 21, newBuffer);
                Buffer = newBuffer;
            }
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
            // client.Send(Buffer);
        }
        public static void MentorInformation(byte[] packet, Nyx.Server.Client.GameClient client)
        {
            MentorInformation info = new MentorInformation(false);
            info.Deserialize(packet);
            if (info.Mentor_Type == 1)
            {
                client.ReviewMentor();
            }
        }
        public static void MentorApprentices(byte[] packet, Nyx.Server.Client.GameClient client)
        {
            MentorApprentice ma = new MentorApprentice(false);
            ma.Deserialize(packet);
            switch (ma.Type)
            {
                case MentorApprentice.LeaveMentor:
                    {
                        LeaveMentors(ma, client);
                        break;
                    }
                case MentorApprentice.ExpellApprentice:
                    {
                        ExpelApprentice(ma, client);
                        break;
                    }
                case MentorApprentice.RequestApprentice:
                    {
                        AddApprentice(ma, client);
                        break;
                    }
                case MentorApprentice.RequestMentor:
                    {
                        AddMentor(ma, client);
                        break;
                    }
                case MentorApprentice.AcceptRequestApprentice:
                    {
                        AcceptRequestApprentices(ma, client);
                        break;
                    }
                case MentorApprentice.AcceptRequestMentor:
                    {
                        AcceptRequestMentors(ma, client);
                        break;
                    }
            }
        }
        static void ExpelApprentice(MentorApprentice ma, Client.GameClient client)
        {
            if (client.Apprentices.ContainsKey(ma.dwParam))
            {
                var appr = client.Apprentices[ma.dwParam];
                if (appr.IsOnline)
                {
                    ma.Type = MentorApprentice.DumpMentor;
                    ma.Online = false;
                    appr.Client.Send(ma);
                    appr.Client.Mentor = null;
                    appr.Client.ReviewMentor();
                }
                Database.KnownPersons.RemoveMentor(appr.ID);
                client.Apprentices.Remove(appr.ID);
            }
        }
        static void LeaveMentors(MentorApprentice ma, Client.GameClient client)
        {
            if (client.Mentor != null)
            {
                ma.Type = MentorApprentice.DumpMentor;
                client.Send(ma);
                if (client.Mentor.IsOnline)
                {
                    ma.Type = MentorApprentice.DumpApprentice;
                    client.Mentor.Client.Send(ma);
                    ApprenticeInformation AppInfo = new ApprenticeInformation();
                    AppInfo.Apprentice_ID = client.Entity.UID;
                    AppInfo.Apprentice_Level = 0;
                    AppInfo.Apprentice_Class = 0;
                    AppInfo.Apprentice_PkPoints = 0;
                    AppInfo.Apprentice_Name = client.Entity.Name;
                    AppInfo.Apprentice_Online = false;
                    AppInfo.Apprentice_Spouse_Name = "";
                    AppInfo.Enrole_date = 0;
                    AppInfo.Mentor_ID = client.Mentor.Client.Entity.UID;
                    AppInfo.Mentor_Mesh = client.Mentor.Client.Entity.Mesh;
                    AppInfo.Mentor_Name = client.Mentor.Client.Entity.Name;
                    AppInfo.Type = 2;
                    client.Mentor.Client.Send(AppInfo);
                    client.Mentor.Client.Apprentices.Remove(client.Entity.UID);
                }
                Database.KnownPersons.RemoveMentor(client.Entity.UID);

                client.Mentor = null;
                client.ReviewMentor();
            }
        }
        static void AddMentor(MentorApprentice ma, Client.GameClient client)
        {
            Client.GameClient Target = null;
            if (Kernel.GamePool.TryGetValue(ma.dwParam, out Target))
            {
                if (!client.Screen.Contains(Target.Entity.UID)) return;
                RequestInformation request = new RequestInformation();
                request.Requester = client.Entity.UID;
                request.Receiver = Target.Entity.UID;
                request.Level = client.Entity.Level;
                request.BattlePower = (uint)client.Entity.BattlePower;
                if (client.Entity.Spouse == Target.Entity.Name)//Spouse
                {
                    request.Spouse = RequestInformation.Request;
                }
                if (client.Friends.ContainsKey(Target.Entity.UID))//Friends
                {
                    request.Friends = RequestInformation.Request;
                }
                if (client.Partners.ContainsKey(Target.Entity.UID))//TradePartner
                {
                    request.TradePartner = RequestInformation.Request;
                }
                if (client.Apprentices.ContainsKey(Target.Entity.UID))//Mentor
                {
                    request.Mentor = RequestInformation.Request;
                }
                if (client.Mentor != null)//Apprentice/
                {
                    if (client.Mentor.ID == Target.Entity.UID)
                        request.Apprentice = RequestInformation.Request;
                }
                if (Target.Mentor != null)
                    if (Target.Mentor.ID == client.Entity.UID)
                        request.Apprentice = RequestInformation.Request;
                if (client.Team != null && Target.Team != null)//Team
                {
                    if (client.Team.Contain(Target.Entity.UID))
                        if (Target.Team.Contain(client.Entity.UID))
                            request.Team = RequestInformation.Request;
                }
                if (client.Entity.GuildID == Target.Entity.GuildID)//Guild
                {
                    request.GuildRank = RequestInformation.Request;
                }
                if (Target.Enemy.ContainsKey(client.Entity.UID))//Enemy
                {
                    request.Enemy = RequestInformation.Request;
                }
                Target.Send(request);
                MentorApprentice Mentor = new MentorApprentice(true);
                Mentor.Type = MentorApprentice.AcceptRequestMentor;
                Mentor.dwParam = Target.Entity.UID;
                Mentor.UID = client.Entity.UID;
                Mentor.Dynamic = (byte)client.Entity.BattlePower;
                Mentor.Online = true;
                Mentor.Name = client.Entity.Name;
                Target.Send(Mentor);
            }
        }
        static void AddApprentice(MentorApprentice ma, Client.GameClient client)
        {
            Client.GameClient Target = null;
            if (Kernel.GamePool.TryGetValue(ma.dwParam, out Target))
            {
                if (!client.Screen.Contains(Target.Entity.UID)) return;
                if (client.apprtnum == 5) return;
                RequestInformation request = new RequestInformation();
                request.Requester = client.Entity.UID;
                request.Receiver = Target.Entity.UID;
                request.Level = client.Entity.Level;
                request.BattlePower = (uint)client.Entity.BattlePower;
                if (client.Entity.Spouse == Target.Entity.Name)//Spouse
                {
                    request.Spouse = RequestInformation.Request;
                }
                if (client.Friends.ContainsKey(Target.Entity.UID))//Friends
                {
                    request.Friends = RequestInformation.Request;
                }
                if (client.Partners.ContainsKey(Target.Entity.UID))//TradePartner
                {
                    request.TradePartner = RequestInformation.Request;
                }
                if (client.Apprentices.ContainsKey(Target.Entity.UID))//Mentor
                {
                    request.Mentor = RequestInformation.Request;
                }
                if (client.Mentor != null)//Apprentice/
                {
                    if (client.Mentor.ID == Target.Entity.UID)
                        request.Apprentice = RequestInformation.Request;
                }
                if (Target.Mentor != null)
                    if (Target.Mentor.ID == client.Entity.UID)
                        request.Apprentice = RequestInformation.Request;
                if (client.Team != null && Target.Team != null)//Team
                {
                    if (client.Team.Contain(Target.Entity.UID))
                        if (Target.Team.Contain(client.Entity.UID))
                            request.Team = RequestInformation.Request;
                }
                if (client.Entity.GuildID == Target.Entity.GuildID)//Guild
                {
                    request.GuildRank = RequestInformation.Request;
                }
                if (Target.Enemy.ContainsKey(client.Entity.UID))//Enemy
                {
                    request.Enemy = RequestInformation.Request;
                }
                Target.Send(request);
                MentorApprentice Mentor = new MentorApprentice(true);
                Mentor.Type = MentorApprentice.AcceptRequestApprentice;
                Mentor.dwParam = Target.Entity.UID;
                Mentor.UID = client.Entity.UID;
                Mentor.Dynamic = (byte)client.Entity.BattlePower;
                Mentor.Online = true;
                Mentor.Name = client.Entity.Name;
                Target.Send(Mentor);
            }
        }
        static void AcceptRequestMentors(MentorApprentice ma, Client.GameClient client)
        {
            Client.GameClient Target = null;
            if (Kernel.GamePool.TryGetValue(ma.UID, out Target))
            {
                if (ma.Dynamic == 1)
                {
                    uint EnroleDate = (uint)(DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day);

                    Target.Mentor = new Nyx.Server.Game.ConquerStructures.Society.Mentor();
                    Target.Mentor.ID = client.Entity.UID;
                    Target.Mentor.Name = client.Entity.Name;
                    Target.Mentor.EnroleDate = EnroleDate;

                    MentorInformation Information = new MentorInformation(true);
                    Information.Mentor_Type = 1;
                    Information.Mentor_ID = Target.Mentor.Client.Entity.UID;
                    Information.Apprentice_ID = Target.Entity.UID;
                    Information.Enrole_Date = EnroleDate;
                    Information.Mentor_Level = Target.Mentor.Client.Entity.Level;
                    Information.Mentor_Class = Target.Mentor.Client.Entity.Class;
                    Information.Mentor_PkPoints = Target.Mentor.Client.Entity.PKPoints;
                    Information.Mentor_Mesh = Target.Mentor.Client.Entity.Mesh;
                    Information.Mentor_Online = true;
                    Information.Shared_Battle_Power = Target.Entity.BattlePowerFrom(Target.Mentor.Client.Entity);
                    Information.String_Count = 3;
                    Information.Mentor_Name = Target.Mentor.Client.Entity.Name;
                    Information.Apprentice_Name = Target.Entity.Name;
                    Information.Mentor_Spouse_Name = Target.Mentor.Client.Entity.Spouse;

                    Target.Send(Information);
                    Target.ReviewMentor();
                    ApprenticeInformation AppInfo = new ApprenticeInformation();
                    AppInfo.Apprentice_ID = Target.Entity.UID;
                    AppInfo.Apprentice_Level = Target.Entity.Level;
                    AppInfo.Apprentice_Name = Target.Entity.Name;
                    AppInfo.Apprentice_Class = Target.Entity.Class;
                    AppInfo.Apprentice_PkPoints = Target.Entity.PKPoints;
                    AppInfo.Apprentice_Online = true;
                    AppInfo.Apprentice_Spouse_Name = Target.Entity.Spouse;
                    AppInfo.Enrole_date = EnroleDate;
                    AppInfo.Mentor_ID = client.Entity.UID;
                    AppInfo.Mentor_Mesh = client.Entity.Mesh;
                    AppInfo.Mentor_Name = client.Entity.Name;
                    AppInfo.Type = 2;
                    client.Send(AppInfo);
                    client.Apprentices.Add(Target.Entity.UID, new Nyx.Server.Game.ConquerStructures.Society.Apprentice()
                    {
                        ID = Target.Entity.UID,
                        Name = Target.Entity.Name,
                        EnroleDate = EnroleDate
                    });
                    Database.KnownPersons.AddMentor(Target.Mentor, client.Apprentices[Target.Entity.UID]);
                }
                else
                {
                    Target.Send(new Message(client.Entity.Name + " declined your request.", System.Drawing.Color.Beige, Message.Talk));
                }
            }
        }
        static void AcceptRequestApprentices(MentorApprentice ma, Client.GameClient client)
        {
            Client.GameClient Target = null;
            if (Kernel.GamePool.TryGetValue(ma.UID, out Target))
            {
                if (ma.Dynamic == 1)
                {
                    uint EnroleDate = (uint)(DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day);
                    MentorInformation Information = new MentorInformation(true);
                    Information.Mentor_Type = 1;
                    Information.Mentor_ID = Target.Entity.UID;
                    Information.Apprentice_ID = client.Entity.UID;
                    Information.Enrole_Date = EnroleDate;
                    Information.Mentor_Level = Target.Entity.Level;
                    Information.Mentor_Class = Target.Entity.Class;
                    Information.Mentor_PkPoints = Target.Entity.PKPoints;
                    Information.Mentor_Mesh = Target.Entity.Mesh;
                    Information.Mentor_Online = true;
                    Information.Shared_Battle_Power = client.Entity.BattlePowerFrom(Target.Entity);
                    Information.String_Count = 3;
                    Information.Mentor_Name = Target.Entity.Name;
                    Information.Apprentice_Name = client.Entity.Name;
                    Information.Mentor_Spouse_Name = Target.Entity.Spouse;

                    client.Send(Information);
                    client.Mentor = new Nyx.Server.Game.ConquerStructures.Society.Mentor();
                    client.Mentor.ID = Target.Entity.UID;
                    client.Mentor.Name = Target.Entity.Name;
                    client.Mentor.EnroleDate = EnroleDate;
                    client.ReviewMentor();

                    ApprenticeInformation AppInfo = new ApprenticeInformation();
                    AppInfo.Apprentice_ID = client.Entity.UID;
                    AppInfo.Apprentice_Level = client.Entity.Level;
                    AppInfo.Apprentice_Name = client.Entity.Name;
                    AppInfo.Apprentice_Online = true;
                    AppInfo.Apprentice_Class = client.Entity.Class;
                    AppInfo.Apprentice_PkPoints = client.Entity.PKPoints;
                    AppInfo.Apprentice_Spouse_Name = client.Entity.Spouse;
                    AppInfo.Enrole_date = EnroleDate;
                    AppInfo.Mentor_ID = Target.Entity.UID;
                    AppInfo.Mentor_Mesh = Target.Entity.Mesh;
                    AppInfo.Mentor_Name = Target.Entity.Name;
                    AppInfo.Type = 2;
                    Target.Send(AppInfo);
                    Target.Apprentices.Add(client.Entity.UID, new Nyx.Server.Game.ConquerStructures.Society.Apprentice()
                    {
                        ID = client.Entity.UID,
                        Name = client.Entity.Name,
                        EnroleDate = EnroleDate
                    });
                    Database.KnownPersons.AddMentor(client.Mentor, Target.Apprentices[client.Entity.UID]);
                }
                else
                {
                    Target.Send(new Message(client.Entity.Name + " declined your request.", System.Drawing.Color.Beige, Message.Talk));
                }
            }
        }
    }
}