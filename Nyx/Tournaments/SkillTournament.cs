using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Database;

namespace Nyx.Server.Game.Tournaments
{

    public class Team_client
    {
        public uint Points = 0;
        public uint UID = 0;
        public ushort Avatar = 0;
        public ushort Mesh = 0;
        public string Name = "";
        public ushort Postion = 0;
        public byte MyTitle = 0;

        public Team_client(Client.GameClient client)
        {
            this.UID = client.Entity.UID;
            this.Avatar = client.Entity.Face;
            this.Mesh = client.Entity.Body;
            this.Name = client.Entity.Name;

        }
        public Team_client(uint _uid, ushort _avatar, ushort _mesh, string _name, uint _points, ushort Position, byte Tytle)
        {
            this.MyTitle = Tytle;
            this.Postion = Position;
            this.Points = _points;
            this.UID = _uid;
            this.Avatar = _avatar;
            this.Mesh = _mesh;
            this.Name = _name;
        }
    }
    public class TeamTournament
    {
        public static Dictionary<uint, Team_client> Team_PK_Tournament = new Dictionary<uint, Team_client>(500);
        public static Dictionary<uint, Team_client> TeamTop8 = new Dictionary<uint, Team_client>(10);
        public static long TeamRank = 0;
        public static void LoginClient(Client.GameClient client)
        {
            if (TeamTop8.ContainsKey(client.Entity.UID))
            {
                if (TeamTop8.ContainsKey(client.Entity.UID))
                {
                    client.Entity.Team = TeamTop8[client.Entity.UID];
                }
            }
        }
        public static void DeleteTabelInstances()
        {
            foreach (Team_client client in TeamTop8.Values)
            {
                MySqlCommand cmd = new MySqlCommand(MySqlCommandType.DELETE);
                cmd.Delete("teampk", "UID", client.UID).Execute();
            }
        }
        public static void LoadTeamTop8()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("teampk");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                Team_client client = new Team_client(r.ReadUInt32("UID"), r.ReadUInt16("Avatar"), r.ReadUInt16("Mesh"), r.ReadString("Name"), r.ReadUInt32("Points"), r.ReadUInt16("Postion"), r.ReadByte("MyTitle"));
                if (!TeamTop8.ContainsKey(client.UID))
                    TeamTop8.Add(client.UID, client);
            }
        }
        public static void SaveTeamTop8(Client.GameClient client)
        {
            Team_client clients = new Team_client(
              (uint)client.Entity.UID
              , (ushort)client.Entity.Face
              , (ushort)client.Entity.Body
              , (string)client.Entity.Name
              , 0
              , (ushort)TeamRank
              , (byte)client.Entity.MyTitle
                  );
            if (!TeamTop8.ContainsKey(clients.UID))
                TeamTop8.Add(clients.UID, clients);
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
            cmd.Insert("teampk")
                .Insert("UID", clients.UID).Insert("Avatar", clients.Avatar)
                .Insert("Mesh", clients.Mesh).Insert("Name", clients.Name)
                .Insert("Points", clients.Points).Insert("Postion", TeamRank)
          .Insert("MyTitle", clients.MyTitle);
            cmd.Execute();
        }

        public static void Open()
        {
            if (!Start)
            {
                DeleteTabelInstances();
                Start = true;
                Team_PK_Tournament.Clear();
                TeamTop8.Clear();
            }
        }
        public static void TeamPkMero()
        {

            DeleteTabelInstances();
            Team_PK_Tournament.Clear();
            TeamTop8.Clear();

        }
        public void Open(int hour, int minute)
        {
            if (DateTime.Now.Minute == minute && DateTime.Now.Hour == hour)
            {
                if (!Start)
                {
                    DeleteTabelInstances();
                    Start = true;
                    Team_PK_Tournament.Clear();
                    TeamTop8.Clear();
                }
            }
        }
        
        public static DateTime StartTimer;
        public static bool Start = false;


        public static void AddMap(Client.GameClient client)
        {

            client.Entity.Teleport(4562, 170, 187);
            client.Send(new Network.GamePackets.Message("Hurry up! A new team has joined at TeamPkChampion event. Don't let them win!", System.Drawing.Color.White, 2011));
        }


    }
    public class SkillTournament
    {
        public static Dictionary<uint, Skill_client> Skill_PK_Tournament = new Dictionary<uint, Skill_client>(500);
        public static Dictionary<uint, Skill_client> SkillTop8 = new Dictionary<uint, Skill_client>(10);
        public static int SkillRank { get; set; }
        public static void LoginClient(Client.GameClient client)
        {
            if (SkillTop8.ContainsKey(client.Entity.UID))
            {
                if (SkillTop8.ContainsKey(client.Entity.UID))
                {
                    client.Entity.Skill = SkillTop8[client.Entity.UID];
                }
            }

        }


        public static void DeleteTabelInstances()
        {
            foreach (Skill_client client in SkillTop8.Values)
            {
                MySqlCommand cmd = new MySqlCommand(MySqlCommandType.DELETE);
                cmd.Delete("skillteampk", "UID", client.UID).Execute();
            }
        }
        public static void LoadSkillTop8()
        {
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.SELECT);
            cmd.Select("skillteampk");
            MySqlReader r = new MySqlReader(cmd);
            while (r.Read())
            {
                Skill_client client = new Skill_client(r.ReadUInt32("UID"), r.ReadUInt16("Avatar"), r.ReadUInt16("Mesh"), r.ReadString("Name"), r.ReadUInt32("Points"), r.ReadUInt16("Postion"), r.ReadByte("MyTitle"));
                if (!SkillTop8.ContainsKey(client.UID))
                    SkillTop8.Add(client.UID, client);
            }
        }
        public static void SaveSkillTop8(Client.GameClient client)
        {
            Skill_client clients = new Skill_client(
              (uint)client.Entity.UID
              , (ushort)client.Entity.Face
              , (ushort)client.Entity.Body
              , (string)client.Entity.Name
              , 0
              , (ushort)SkillRank
              , (byte)client.Entity.MyTitle
                  );
            if (!SkillTop8.ContainsKey(clients.UID))
                SkillTop8.Add(clients.UID, clients);
            MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
            cmd.Insert("skillteampk")
                .Insert("UID", clients.UID).Insert("Avatar", clients.Avatar)
                .Insert("Mesh", clients.Mesh).Insert("Name", clients.Name)
                .Insert("Points", clients.Points).Insert("Postion", SkillRank)
          .Insert("MyTitle", clients.MyTitle);
            cmd.Execute();
        }

        public static void Open()
        {
            if (!Start)
            {
                DeleteTabelInstances();
                Start = true;
                Skill_PK_Tournament.Clear();
                SkillTop8.Clear();
            }
        }
        public static void SkillPkMero()
        {

            DeleteTabelInstances();
            Skill_PK_Tournament.Clear();
            SkillTop8.Clear();

        }
        public void Open(int hour, int minute)
        {
            if (DateTime.Now.Minute == minute && DateTime.Now.Hour == hour)
            {
                if (!Start)
                {
                    DeleteTabelInstances();
                    Start = true;
                    Skill_PK_Tournament.Clear();
                    SkillTop8.Clear();
                }
            }
        }
        public void Meroscore(Client.GameClient client)
        {
            if (Start)
            {
                for (uint x = 1; x < 9; x++)
                {
                    string Mesage = "";
                    foreach (Skill_client clients in SkillTop8.Values)
                    {
                        if (clients.Postion == x)
                        {
                            Mesage = "No." + x + " " + clients.Name + ": Score: " + clients.Points + "";
                        }
                    }
                    Network.GamePackets.Message msg = new Network.GamePackets.Message(Mesage, System.Drawing.Color.Red, x == 1 ? Network.GamePackets.Message.FirstRightCorner : Network.GamePackets.Message.ContinueRightCorner);
                    client.Send(msg);
                }
            }
        }
        
        public static DateTime StartTimer;
        public static bool Start = false;
        public static uint MapID = 4662;

        public static void AddMap(Client.GameClient client)
        {

            client.Entity.Teleport(4662, 170, 187);
            client.Send(new Network.GamePackets.Message("Hurry up! A new player has joined at SkillPk event. Don't let them win!", System.Drawing.Color.White, 2011));
        }


    }
    public class Skill_client
    {
        public uint Points = 0;
        public uint UID = 0;
        public ushort Avatar = 0;
        public ushort Mesh = 0;
        public string Name = "";
        public ushort Postion = 0;
        public byte MyTitle = 0;

        public Skill_client(Client.GameClient client)
        {
            this.UID = client.Entity.UID;
            this.Avatar = client.Entity.Face;
            this.Mesh = client.Entity.Body;
            this.Name = client.Entity.Name;

        }
        public Skill_client(uint _uid, ushort _avatar, ushort _mesh, string _name, uint _points, ushort Position, byte Tytle)
        {
            this.MyTitle = Tytle;
            this.Postion = Position;
            this.Points = _points;
            this.UID = _uid;
            this.Avatar = _avatar;
            this.Mesh = _mesh;
            this.Name = _name;
        }
    }
    public class Elite_client
    {
        public uint Points = 0;
        public uint UID = 0;
        public ushort Avatar = 0;
        public ushort Mesh = 0;
        public string Name = "";
        public ushort Postion = 0;

        public Elite_client(Client.GameClient client)
        {
            this.UID = client.Entity.UID;
            this.Avatar = client.Entity.Face;
            this.Mesh = client.Entity.Body;
            this.Name = client.Entity.Name;
        }

        public Elite_client(uint _uid, ushort _avatar, ushort _mesh, string _name, uint _points, ushort Position,
            byte Tytle)
        {
            this.Postion = Position;
            this.Points = _points;
            this.UID = _uid;
            this.Avatar = _avatar;
            this.Mesh = _mesh;
            this.Name = _name;
        }
    }

    public class EliteTournament
    {
        public static Dictionary<uint, Elite_client> Elite_PK_Tournament = new Dictionary<uint, Elite_client>(500);
        public static Dictionary<uint, Elite_client> Top8 = new Dictionary<uint, Elite_client>(10);

        public static void LoginClient(Client.GameClient client)
        {
            if (!Start)
            {
                if (Top8.ContainsKey(client.Entity.UID))
                {
                    client.Entity.Elite = Top8[client.Entity.UID];
                }
            }
        }

        public static void DeleteTabelInstances()
        {
            foreach (Elite_client client in Top8.Values)
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.DELETE))
                    cmd.Delete("elitepk", "UID", client.UID).Execute();
            }
        }


        //public EliteTournament() { LoadTop8(); }
        public static void Open()
        {
            if (!Start)
            {
                DeleteTabelInstances();
                Start = true;
                //CalculateTime = DateTime.Now;
                //StartTimer = DateTime.Now;
                //SendInvitation();
                Elite_PK_Tournament.Clear();
                Top8.Clear();
            }
        }

        public void Open(int hour, int minute)
        {
            if (DateTime.Now.Minute == minute && DateTime.Now.Hour == hour)
            {
                if (!Start)
                {
                    DeleteTabelInstances();
                    Start = true;
                    //CalculateTime = DateTime.Now;
                    //StartTimer = DateTime.Now;
                    //SendInvitation();
                    Elite_PK_Tournament.Clear();
                    Top8.Clear();
                }
            }
        }

        public static DateTime StartTimer;
        public static bool Start = false;


        public static void AddMap(Client.GameClient client)
        {
            if (Start)
            {

                client.Entity.Teleport(7777, 150, 162);
            }
        }

        public static void SendScore(Client.GameClient client)
        {
            if (Kernel.GamePool.ContainsKey(client.Entity.UID))
            {
                for (uint x = 1; x < 9; x++)
                {
                    string Mesage = "";
                    foreach (Elite_client clients in Top8.Values)
                    {
                        if (clients.Postion == x)
                        {
                            Mesage = "No." + x + " " + clients.Name + ": Score: " + clients.Points + "";
                            //Console.WriteLine("a7a9");
                        }
                    }
                    Network.GamePackets.Message msg = new Network.GamePackets.Message(Mesage, System.Drawing.Color.Red,
                        x == 1
                            ? Network.GamePackets.Message.FirstRightCorner
                            : Network.GamePackets.Message.ContinueRightCorner);
                    client.Send(msg);
                }
            }
        }

        public static void CalculateRank()
        {
            if (Start)
            {
                Dictionary<uint, ulong> ToIndex = new Dictionary<uint, ulong>();
                uint CurKey = 0;
                int Rank = 1;
                for (short x = 0; x < Elite_PK_Tournament.Count; x++)
                {
                    if (Rank == 9)
                        break;
                    ulong Value = 0;
                    foreach (uint K in Elite_PK_Tournament.Keys)
                    {
                        if (Elite_PK_Tournament[K].Points >= Value && !ToIndex.ContainsKey(K))
                        {
                            Value = Elite_PK_Tournament[K].Points;
                            CurKey = K;
                        }
                    }
                    if (!ToIndex.ContainsKey(CurKey))
                        ToIndex.Add(CurKey, Value);
                    if (Elite_PK_Tournament.ContainsKey(CurKey))
                    {
                        Elite_PK_Tournament[CurKey].Postion = (ushort)Rank;
                    }
                    Rank++;
                }
                lock (Top8)
                {
                    Top8.Clear();
                    for (byte x = 1; x < 10; x++)
                    {
                        foreach (Elite_client client in Elite_PK_Tournament.Values)
                        {
                            if (client.Postion == x)
                            {
                                Top8.Add(client.UID, client);
                            }
                        }
                    }
                }
            }
        }
    }

}
namespace Nyx.Server.Network.GamePackets
{
    public class Skill_Pk : Writer
    {
        private byte[] byte_0;

        public Skill_Pk(uint clientID)
        {
            this.byte_0 = new byte[428];
            Writer.Write(420, 0, this.byte_0);
            Writer.Write(2253, 2, this.byte_0);
            this.byte_0[8] = 3;
            this.byte_0[16] = 8;
            Writer.Write(clientID, 20, this.byte_0);
        }

        public void Send(Client.GameClient client)
        {
            client.Send(this.Encode());
        }

        public byte[] Encode()
        {
            ushort num = 24;
            Nyx.Server.Game.Tournaments.Skill_client[] array = Nyx.Server.Game.Tournaments.SkillTournament.SkillTop8.Values.ToArray<Nyx.Server.Game.Tournaments.Skill_client>();
            for (byte i = 1; i < 9; i = (byte)(i + 1))
            {
                Nyx.Server.Game.Tournaments.Skill_client[] skillClientArray = array;
                for (int j = 0; j < (int)skillClientArray.Length; j++)
                {
                    Nyx.Server.Game.Tournaments.Skill_client skillClient = skillClientArray[j];
                    if (i == skillClient.Postion)
                    {
                        this.byte_0[num] = (byte)skillClient.Postion;
                        num = (ushort)(num + 4);
                        Writer.Write(skillClient.Name, (int)num, this.byte_0);
                        num = (ushort)(num + 32);
                        Writer.Write(uint.Parse(string.Concat(skillClient.Avatar.ToString(), skillClient.Mesh.ToString())), (int)num, this.byte_0);
                        num = (ushort)(num + 4);
                        Writer.Write(skillClient.UID, (int)num, this.byte_0);
                        num = (ushort)(num + 4);
                    }
                }
            }
            byte[] byte0 = this.byte_0;
            return byte0;
        }
    }
}