using Nyx.Server.Client;
using Nyx.Server.Game;
using Nyx.Server.Interfaces;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Database
{
    public class SubClassTable
    {
        public static void Load(Entity Entity)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(MySqlCommandType.SELECT).Select("subclasses").Where("id", (long)Entity.UID))
            {
                using (MySqlReader reader = mySqlCommand.CreateReader())
                {
                    while (reader.Read())
                    {
                        Nyx.Server.Game.SubClass subClass = new Nyx.Server.Game.SubClass();
                        subClass.ID = reader.ReadByte("Uid");
                        subClass.Level = reader.ReadByte("Level");
                        subClass.Phase = reader.ReadByte("Phase");
                        Entity.SubClasses.Classes.Add(subClass.ID, subClass);
                        Game_SubClass gameSubClass = new Game_SubClass();
                        gameSubClass.ClassId = (Game_SubClass.ID)subClass.ID;
                        gameSubClass.Phase = subClass.Phase;
                        gameSubClass.Type = Game_SubClass.Types.Learn;
                        Entity.Owner.Send((IPacket)gameSubClass);
                        gameSubClass.Type = Game_SubClass.Types.MartialPromoted;
                        Entity.Owner.Send((IPacket)gameSubClass);
                    }
                }
            }
        }

        public static bool Contains(Entity Entity, byte id)
        {
            bool flag = false;
            using (MySqlCommand mySqlCommand = new MySqlCommand(MySqlCommandType.SELECT))
            {
                mySqlCommand.Select("subclasses").Where("id", (long)Entity.UID).And("uid", (long)id);
                using (MySqlReader reader = mySqlCommand.CreateReader())
                {
                    if (reader.Read())
                    {
                        if ((int)reader.ReadByte("uid") == (int)id)
                            flag = true;
                    }
                }
            }
            return flag;
        }

        public static void Insert(Entity Entity, byte id)
        {
            using (var Command = new MySqlCommand(MySqlCommandType.INSERT))
                Command.Insert("subclasses")
                    .Insert("uid", id)
                    .Insert("id", Entity.UID)
                    .Execute();
        }
        public static void Update(Game.Entity Entity, Game.SubClass SubClass)
        {
            MySqlCommand Command = new MySqlCommand(MySqlCommandType.UPDATE);
            Command.Update("subclasses")
                .Set("phase", SubClass.Phase)
                .Set("level", SubClass.Level)
                .Where("id", Entity.UID)
                .And("uid", SubClass.ID)
                .Execute();
        }

        public static void Update(Client.GameClient client)
        {
            EntityTable.UpdateData(client, "StudyPoints", client.Entity.SubClasses.StudyPoints);
        }
    }
}
