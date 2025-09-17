using System;
using System.IO;
using System.Text;


namespace Nyx.Server.Database
{
    public class AccountTable
    {
        public enum AccountState : byte
        {
            GameMaster = 4,
            Cheated = 3,
            Banned = 1,
            Player = 0
        }
        public string Username;
        public string Password;
        public string IP;
        public AccountState State;
        public uint EntityID;
        public int RandomKey;
        public bool exists = false;
        public AccountTable(string username)
        {
            if (username == null) return;
            this.Username = username;
            this.Password = "";
            this.IP = "";
            this.State = AccountState.Player;
            this.EntityID = 0;

            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("accounts").Where("Username", username))
            using (var reader = new MySqlReader(cmd))
            {
                if (reader.Read())
                {
                    exists = true;
                    this.Password = reader.ReadString("Password");
                    this.IP = reader.ReadString("Ip");
                    this.EntityID = reader.ReadUInt32("EntityID");
                    this.State = (AccountState)reader.ReadInt32("State");
                }
            }
        }

        public uint GenerateKey(int randomKey = 0)
        {
            if (randomKey == 0)
                RandomKey = Kernel.Random.Next(11, 253) % 100 + 1;
            return (uint)
                        (Username.GetHashCode() *
                        Password.GetHashCode() *
                        RandomKey);
        }

        public bool MatchKey(uint key)
        {
            return key == GenerateKey(RandomKey);
        }
        public void Save(Client.GameClient client)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                cmd.Update("accounts").Set("Password", Password).Set("Ip", client.IP).Set("EntityID", EntityID)
                    .Where("Username", Username).Execute();
        }
        public void Insert()
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                cmd.Insert("accounts").Insert("Username", Username).Insert("Ip", IP)
                    .Insert("Password", Password).Insert("State", (int)State)
                    .Execute();
            exists = true;
        }
        public void Cheat()
        {
            if (exists)
            {
                MySqlCommand cmd = new MySqlCommand(MySqlCommandType.UPDATE);
                cmd.Update("accounts").Set("State", 3).Where("Username", Username).Execute();
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand(MySqlCommandType.INSERT);
                cmd.Insert("accounts").Insert("Username", Username).Insert("Password", Password).Insert("State", (byte)State).Execute();

            }
        }
        public void SaveState()
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                cmd.Update("accounts").Set("State", (int)State)
                    .Where("Username", Username).Execute();
        }
    }
}
