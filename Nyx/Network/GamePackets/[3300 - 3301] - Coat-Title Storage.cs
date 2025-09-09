using ProtoBuf;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Network.GamePackets
{
    public sealed class CoatStorage
    {
        public CoatStorage() { }
        public bool Read(byte[] packet)
        {
            using (var memoryStream = new MemoryStream(packet))
            {
                Info = Serializer.DeserializeWithLengthPrefix<CoatStorageProto>(memoryStream, PrefixStyle.Fixed32);
            }
            return true;
        }
        public void Handle(Client.GameClient client)
        {
            switch (Info.ActionId)
            {
                case Action.Combine:
                    {

                        break;
                    }
                case Action.Equipcheck:

                    if (Illegal(Info, client))
                        return;

                    ConquerItem myItem;
                    if (client.Inventory.TryGetItem(Info.ItemGuid, out myItem))
                    {
                        var packet = FinalizeProtoBuf(new CoatStorageProto()
                        {
                            ActionId = Info.ActionId,
                            ItemGuid = Info.ItemGuid,
                        });
                        client.Send(packet);
                    }
                    break;

                case Action.Addtowardrobe:
                    {
                        if (Illegal(Info, client))
                            return;

                        if (client.Inventory.TryGetItem(Info.ItemGuid, out myItem) ||
                            client.Entity.StorageItems.TryGetValue(Info.ItemGuid, out myItem))
                        {
                            if (!client.Entity.StorageItems.ContainsKey(myItem.UID))
                            {
                                client.Entity.StorageItems.Add(myItem.UID, myItem);
                                myItem.InWardrobe = true;
                                Database.ConquerItemTable.UpdateWardrobe(myItem.InWardrobe, myItem.UID);
                                client.Inventory.Remove(myItem, Game.Enums.ItemUse.None);
                            }
                            var dbInfo = Database.ConquerItemInformation.BaseInformations[myItem.ID];
                            var pos = (byte)PacketHandler.GetPositionFromID(myItem.ID);
                            if (pos == 17)
                            {
                                if (client.Entity.SubClasses.Classes.ContainsKey(9))
                                    if (client.Entity.SubClasses.Classes[9].Phase >= dbInfo.Proficiency)
                                    {
                                        client.Send(FinalizeProtoBuf(new CoatStorageProto()
                                        {
                                            ActionId = Info.ActionId,
                                            ItemGuid = Info.ItemGuid,
                                            ItemId = (int)myItem.ID
                                        }));
                                        foreach (var i in client.Entity.StorageItems.Values)
                                            if (i.Position != 0 && i.Position == (byte)PacketHandler.GetPositionFromID(myItem.ID))
                                            {
                                                i.Position = 0;
                                                Database.ConquerItemTable.UpdateLocation(i, client);
                                            }
                                        myItem.Position = (byte)PacketHandler.GetPositionFromID(myItem.ID);

                                        if (!client.Equipment.Add(myItem))
                                        {
                                            client.Equipment.Remove((byte)myItem.Position, true);
                                        }
                                        var iu = new ItemUsage(true);
                                        iu.ID = ItemUsage.Unknown5;
                                        iu.UID = myItem.UID;
                                        iu.dwParam = myItem.Position;
                                        client.Send(iu.Encode());

                                        ClientEquip equips = new ClientEquip();
                                        equips.DoEquips(client);
                                        client.Send(equips.Encode());

                                        Database.ConquerItemTable.UpdateLocation(myItem, client);
                                        client.Equipment.UpdateEntityPacket();
                                    }
                                    else
                                        break;
                                if (Database.StorageItem.AmountStarMounts(client, 5) >= 1)
                                {
                                    new TitleStorage().AddTitle(client, 6004, 23, false);
                                }
                                if (Database.StorageItem.AmountStarMounts(client, 4) >= 5)
                                {
                                    new TitleStorage().AddTitle(client, 2006, 7, false);
                                }
                            }
                            if (pos == 9)
                            {
                                client.Send(FinalizeProtoBuf(new CoatStorageProto()
                                {
                                    ActionId = Info.ActionId,
                                    ItemGuid = Info.ItemGuid,
                                    ItemId = (int)myItem.ID
                                }));
                                foreach (var i in client.Entity.StorageItems.Values)
                                    if (i.Position != 0 && i.Position == (byte)PacketHandler.GetPositionFromID(myItem.ID))
                                    {
                                        i.Position = 0;
                                        Database.ConquerItemTable.UpdateLocation(i, client);
                                    }
                                myItem.Position = (byte)PacketHandler.GetPositionFromID(myItem.ID);

                                if (!client.Equipment.Add(myItem))
                                {
                                    client.Equipment.Remove((byte)myItem.Position, true);

                                }
                                var iu = new ItemUsage(true);
                                iu.ID = ItemUsage.Unknown5;
                                iu.UID = myItem.UID;
                                iu.dwParam = myItem.Position;
                                client.Send(iu.Encode());

                                ClientEquip equips = new ClientEquip();
                                equips.DoEquips(client);

                                if ((equips.Garment == 0 || equips.Garment > 0 && equips.Garment != myItem.UID) && myItem.Position != 17)
                                    equips.Garment = myItem.UID;

                                client.Send(equips.Encode());

                                Database.ConquerItemTable.UpdateLocation(myItem, client);
                                client.Equipment.UpdateEntityPacket();
                                if (Database.StorageItem.AmountStarGarments(client, 5) >= 1)
                                {
                                    new TitleStorage().AddTitle(client, 6003, 22, false);
                                }
                                if (Database.StorageItem.AmountStarGarments(client, 4) >= 5)
                                {
                                    new TitleStorage().AddTitle(client, 2005, 6, false);
                                }
                            }
                            break;
                        }
                        if (Database.StorageItem.AmountStarGarments(client, 5) >= 1)
                        {
                            new TitleStorage().AddTitle(client, 6003, 22, false);
                        }
                        if (Database.StorageItem.AmountStarMounts(client, 5) >= 1)
                        {
                            new TitleStorage().AddTitle(client, 6004, 23, false);
                        }
                        if (Database.StorageItem.AmountStarGarments(client, 4) >= 5)
                        {
                            new TitleStorage().AddTitle(client, 2005, 6, false);
                        }
                        if (Database.StorageItem.AmountStarMounts(client, 4) >= 5)
                        {
                            new TitleStorage().AddTitle(client, 2006, 7, false);
                        }
                        break;
                    }
                case Action.Takeoff:
                    if (client.Inventory.TryGetItem(Info.ItemGuid, out myItem) ||
                        client.Entity.StorageItems.TryGetValue(Info.ItemGuid, out myItem) || client.Equipment.TryGetItem(Info.ItemGuid, out myItem))
                    {
                        client.Send(FinalizeProtoBuf(new CoatStorageProto()
                        {
                            ActionId = Info.ActionId,
                            ItemGuid = Info.ItemGuid,
                            ItemId = (int)myItem.ID
                        }));

                        var pos = (byte)PacketHandler.GetPositionFromID(myItem.ID);


                        ClientEquip equips = new ClientEquip();
                        equips.DoEquips(client);
                        if (pos == 17)
                            equips.SteedArmor = 0;
                        else equips.Garment = 0;
                        client.Send(equips.Encode());

                        if (myItem != null && !client.Entity.StorageItems.ContainsKey(myItem.UID))
                            client.Entity.StorageItems.Add(myItem.UID, myItem);
                        client.Equipment.UpdateEntityPacket();

                    }
                    break;
                case Action.Retrieve:
                    if (client.Entity.StorageItems.TryGetValue(Info.ItemGuid, out myItem))
                    {
                        client.Send(FinalizeProtoBuf(new CoatStorageProto()
                        {
                            ActionId = Info.ActionId,
                            ItemGuid = Info.ItemGuid,
                            ItemId = (int)myItem.ID
                        }));

                        client.Entity.StorageItems.Remove(myItem.UID);
                        myItem.InWardrobe = false;
                        var pos = (byte)PacketHandler.GetPositionFromID(myItem.ID);

                        if (client.Inventory.ContainsUID(myItem.UID))
                        {
                            client.Inventory.Remove(myItem, Game.Enums.ItemUse.Move);
                            if (client.Equipment.TryGetItem(pos) != null && client.Equipment.TryGetItem(pos).UID == myItem.UID)
                                client.Equipment.Remove(pos);
                            else client.Inventory.Add(myItem, Game.Enums.ItemUse.Move);
                        }
                        else
                        {
                            if (client.Equipment.TryGetItem(pos) != null && client.Equipment.TryGetItem(pos).UID == myItem.UID)
                                client.Equipment.Remove(pos);
                            else client.Inventory.Add(myItem, Game.Enums.ItemUse.Move);
                        }


                        Database.ConquerItemTable.UpdateWardrobe(myItem.InWardrobe, myItem.UID);
                        ClientEquip equips = new ClientEquip();
                        equips.DoEquips(client);
                        if (pos == 17)
                            equips.SteedArmor = 0;
                        else equips.Garment = 0;
                        client.Send(equips.Encode());
                        client.Equipment.UpdateEntityPacket();
                    }
                    break;
            }
        }

        private bool Illegal(CoatStorageProto Info, Client.GameClient client)
        {
            ConquerItem myItem;
            if (client.Inventory.TryGetItem(Info.ItemGuid, out myItem) ||
            client.Entity.StorageItems.TryGetValue(Info.ItemGuid, out myItem) ||
            client.Equipment.TryGetItem(Info.ItemGuid, out myItem))
            {
                var dbInfo = Database.ConquerItemInformation.BaseInformations.ContainsKey(myItem.ID) ?
                Database.ConquerItemInformation.BaseInformations[myItem.ID] : null;
                if (dbInfo == null)
                    return true;
                var charSex = (client.Entity.Body == 1003 || client.Entity.Body == 1004) ? "Male" : "Female";
                if (dbInfo.Gender == 1 ? charSex != "Male" : dbInfo.Gender == 0 ? false : charSex != "Female")
                    return true;
            }
            else return true;
            return false;
        }

        private byte[] FinalizeProtoBuf(CoatStorageProto coatStorageProto)
        {
            using (var memoryStream = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(memoryStream, coatStorageProto, PrefixStyle.Fixed32);
                var pkt = new byte[8 + memoryStream.Length];
                memoryStream.ToArray().CopyTo(pkt, 0);
                Writer.Write((ushort)memoryStream.Length, 0, pkt);
                Writer.Write((ushort)3300, 2, pkt);

                return pkt;
            }
        }
        public void Login(Client.GameClient client)
        {
            var pkt = new CoatStorageProto();
            foreach (var item in client.Entity.StorageItems.Values)
            {
                pkt.AddItem(item,
                    client.Entity.StorageItems.Values.Where(i => i.ID == item.ID).Count());
                client.Send(FinalizeProtoBuf(pkt));

                if (item.Position != 0)
                {
                    client.Equipment.Add(item);

                    var iu = new ItemUsage(true);
                    iu.ID = ItemUsage.Unknown5;
                    iu.UID = item.UID;
                    iu.dwParam = item.Position;
                    client.Send(iu.Encode());

                    ClientEquip equips = new ClientEquip();
                    equips.DoEquips(client);
                    client.Send(equips.Encode());

                    Database.ConquerItemTable.UpdateLocation(item, client);
                    client.Equipment.UpdateEntityPacket();
                }

            }
            var currentGarment = client.Equipment.TryGetItem((byte)PacketHandler.Positions.Garment);
            if (currentGarment != null && !client.Entity.StorageItems.ContainsKey(currentGarment.UID))
            {
                client.Entity.StorageItems.Add(currentGarment.UID, currentGarment);
                pkt.AddItem(currentGarment,
                    client.Entity.StorageItems.Values.Where(i => i.ID == currentGarment.ID).Count());
                pkt.Item.Equipped = true;
                client.Send(FinalizeProtoBuf(pkt));
            }
            var currentMountArmor = client.Equipment.TryGetItem((byte)PacketHandler.Positions.SteedArmor);
            if (currentMountArmor != null && !client.Entity.StorageItems.ContainsKey(currentMountArmor.UID))
            {
                client.Entity.StorageItems.Add(currentMountArmor.UID, currentMountArmor);
                pkt.AddItem(currentMountArmor,
                    client.Entity.StorageItems.Values.Where(i => i.ID == currentMountArmor.ID).Count());
                pkt.Item.Equipped = true;
                client.Send(FinalizeProtoBuf(pkt));
            }

        }


        public CoatStorageProto Info;

        public enum Action : int
        {
            /// <summary>
            /// Load items in storage ...
            /// </summary>
            Login = 0,
            Equipcheck = 1,
            Retrieve = 2,
            Combine = 4,
            Addtowardrobe = 5,
            Takeoff = 6,
        }
    }
    [ProtoContract]
    public class CoatStorageProto
    {
        [ProtoMember(1, IsRequired = true)]
        public CoatStorage.Action ActionId;
        [ProtoMember(2, IsRequired = true)]
        public uint ItemGuid;
        [ProtoMember(3, IsRequired = true)]
        public int ItemId;
        [ProtoMember(4, IsRequired = true)]
        public int Junk;
        [ProtoMember(5, IsRequired = true)]
        public ItemStorage Item;
        public void AddItem(ConquerItem item, int stack)
        {
            Item = new ItemStorage();
            Item.ItemUID = item.UID;
            Item.ItemID = (int)item.ID;
            Item.Maximumdurability = Item.Minimumdurability = item.MaximDurability;
            Item.Stack = stack;
            Item.FirstSocket = (int)item.SocketOne;
            Item.SecondSocket = (int)item.SocketTwo;
            Item.Plus = item.Plus;
            Item.Protection = item.Bless;
            Item.Bound = item.Bound;
            Item.Health = item.Enchant;
            Item.SocketProgress = (int)item.SocketProgress;
            Item.Effect = item.Effect;
            Item.Color = item.Color;
            Item.CraftProgress = (int)item.PlusProgress;
            Item.Locked = item.Lock == 1 ? true : false;
            Item.Suspicious = false;
            Item.Inscribed = false;
            Item.dwParam7 = 0;
            Item.Equipped = item.Position != 0;
            Item.dwParam15 = 0;
            Item.Time = 0;
            Item.SubTime = 0;
        }

    }
    [ProtoContract]
    public class ItemStorage
    {
        [ProtoMember(1, IsRequired = true)]
        public uint ItemUID;
        [ProtoMember(2, IsRequired = true)]
        public int ItemID;
        [ProtoMember(3, IsRequired = true)]
        public int SocketProgress;
        [ProtoMember(4, IsRequired = true)]
        public int FirstSocket;
        [ProtoMember(5, IsRequired = true)]
        public int SecondSocket;
        [ProtoMember(6, IsRequired = true)]
        public Nyx.Server.Game.Enums.ItemEffect Effect;
        [ProtoMember(7, IsRequired = true)]
        public int dwParam7;
        [ProtoMember(8, IsRequired = true)]
        public int Plus;
        [ProtoMember(9, IsRequired = true)]
        public int Protection;
        [ProtoMember(10, IsRequired = true)]
        public bool Bound;
        [ProtoMember(11, IsRequired = true)]
        public int Health;
        [ProtoMember(12, IsRequired = true)]
        public bool Equipped;
        [ProtoMember(13, IsRequired = true)]
        public bool Suspicious;
        [ProtoMember(14, IsRequired = true)]
        public bool Locked;
        [ProtoMember(15, IsRequired = true)]
        public int dwParam15;
        [ProtoMember(16, IsRequired = true)]
        public Nyx.Server.Game.Enums.Color Color;
        [ProtoMember(17, IsRequired = true)]
        public int CraftProgress;
        /// <summary>
        /// Inscribed in guild arsenal 
        /// This class is for wardrobe items which are garments or mount armors so this filed is always false
        /// </summary>
        [ProtoMember(18, IsRequired = true)]
        public bool Inscribed;
        /// <summary>
        /// Time left in seconds !
        /// </summary>
        [ProtoMember(19, IsRequired = true)]
        public int Time;
        /// <summary>
        /// Time left in minutes (if item not activated only)
        /// </summary>
        [ProtoMember(20, IsRequired = true)]
        public int SubTime;
        [ProtoMember(21, IsRequired = true)]
        public int Stack;
        [ProtoMember(22, IsRequired = true)]
        public int Minimumdurability;
        [ProtoMember(23, IsRequired = true)]
        public int Maximumdurability;
    }
    public static class Storage
    {
        private const string Path = "database\\Storage.ini";

        public static void Read(out StorageInfo storageInfo)
        {
            storageInfo = new StorageInfo();
            var reader = new Nyx.Server.IniFile(Path);
            {
                storageInfo.Count = reader.ReadInt32("Storage", "StorageTypeCount", 0);
                storageInfo.Storages = new StorageInfo.Storage[storageInfo.Count];
                for (int i = 0; i < storageInfo.Count; i++)
                {
                    var mySection = (i + 1).ToString();
                    storageInfo.Storages[i] = new StorageInfo.Storage();
                    storageInfo.Storages[i].Type = reader.ReadInt32(mySection, "Type", 0);
                    storageInfo.Storages[i].UnitCount = reader.ReadInt32(mySection, "UnitCount", 0);
                    storageInfo.Storages[i].ViewType = reader.ReadInt32(mySection, "ViewType", 0);
                    storageInfo.Storages[i].Units = new StorageInfo.Storage.Unit[storageInfo.Storages[i].UnitCount];
                    for (int u = 0; u < storageInfo.Storages[i].UnitCount; u++)
                    {
                        var unitSection = mySection + "-" + (u + 1).ToString();
                        storageInfo.Storages[i].Units[u] = new StorageInfo.Storage.Unit();
                        storageInfo.Storages[i].Units[u].ID = reader.ReadInt32(unitSection, "ID", 0);
                        storageInfo.Storages[i].Units[u].AniSection = reader.ReadString(unitSection, "AniSection");
                        storageInfo.Storages[i].Units[u].GetWayType3 = reader.ReadInt32(unitSection, "GetWayType3", 0);
                        storageInfo.Storages[i].Units[u].GetWayText3 = reader.ReadString(unitSection, "GetWayText3");
                        storageInfo.Storages[i].Units[u].Param = reader.ReadString(unitSection, "Param");
                        storageInfo.Storages[i].Units[u].Intro = reader.ReadString(unitSection, "Intro");
                    }

                }
            }
            //Console.WriteLine("Storages loaded");
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public class StorageInfo
    {
        public int Count;
        public Storage[] Storages;
        public class Storage
        {
            public int Type;
            public int UnitCount;
            public int ViewType;
            public Unit[] Units;
            public class Unit
            {
                public int ID;
                public string AniSection;
                public int GetWayType3;
                public string GetWayText3;
                public string Param;
                public string Intro;
            }
        }
        public Storage GetStorageByType(int Type)
        {
            foreach (var storage in Storages)
                if (storage != null && storage.Type == Type)
                    return storage;
            return null;
        }
        public Storage.Unit GetUnitByID(int ID, Storage _storage = null)
        {
            if (_storage != null)
            {
                foreach (var unit in _storage.Units)
                    if (unit.ID == ID)
                        return unit;
            }
            else
            {
                foreach (var storage in Storages)
                    if (storage != null)
                        foreach (var unit in storage.Units)
                            if (unit.ID == ID)
                                return unit;
            }
            return null;
        }
    }
    public sealed class StorageManager
    {
        private static StorageInfo Info;
        public static Dictionary<uint, Network.GamePackets.WardrobeTitles> Data;

        public static void Load()
        {
            Storage.Read(out Info);
            Data = new Dictionary<uint, Network.GamePackets.WardrobeTitles>();
            using (var cmd = new Database.MySqlCommand(Database.MySqlCommandType.SELECT))
            {
                cmd.Select("Titles");
                var reader = cmd.CreateReader();
                while (reader.Read())
                {
                    var title = new Network.GamePackets.WardrobeTitles();
                    title.Id = reader.ReadUInt32("Id");
                    title.Points = reader.ReadInt32("Points");
                    title.Data = reader.ReadBlob("Data");
                    Data.Add(title.Id, title);
                }
            }
        }

        public static T Wing<T>(int _type, int _id)
        {
            object value = null;
            int trash = 0;
            if (typeof(T) == typeof(bool))
            {
                value = int.TryParse(Info.GetUnitByID(_id, Info.GetStorageByType(_type)).Param, out trash);
            }
            else if (typeof(T) == typeof(int))
            {
                var myType = _type.ToString();
                var myID = _id.ToString();


                while (myID.Length < 4)
                    myID = "0" + myID;
                value = int.Parse(myType + myID);
            }
            else
                throw new Exception("Unknow type : " + typeof(T).Name);
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static T Title<T>(int _type, int _id)
        {
            object value = null;
            int trash = 0;
            if (typeof(T) == typeof(bool))
            {
                value = !int.TryParse(Info.GetUnitByID(_id, Info.GetStorageByType(_type)).Param, out trash);
            }
            else if (typeof(T) == typeof(int))
            {
                var myType = _type.ToString();
                var myID = _id.ToString();

                while (myID.Length < 4)
                    myID = "0" + myID;
                value = int.Parse(myType + myID);
            }
            else
                throw new Exception("Unknow type : " + typeof(T).Name);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static Network.GamePackets.WardrobeTitles Find(Func<Network.GamePackets.WardrobeTitles, bool> predicate)
        {
            foreach (var title in Data.Values)
            {
                if (predicate(title))
                    return title;
            }
            return null;
        }
    }
    public class WardrobeTitles
    {
        public uint Id;
        public byte[] Data;
        public int Points;

        public void Create()
        {
            string SQL = "INSERT INTO `Titles` (Id, Points, Data) VALUES (@Id, @Points ,@Data)";
            using (var conn = Database.DataHolder.MySqlConnection)
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(SQL, conn))
                {
                    var reader = new Database.MySqlCommand(Database.MySqlCommandType.SELECT).Select("Titles").Where("Id", Id).CreateReader();
                    if (reader.Read())
                    {
                        Update();
                        return;
                    }
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Points", Points);
                    cmd.Parameters.AddWithValue("@Data", Data);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update()
        {
            string SQL = "UPDATE `Titles` SET Data=@Data where ID = " + Id + " ;";
            using (var conn = Database.DataHolder.MySqlConnection)
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@Data", Data);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddTitle(short _type, short _id, bool equipped = false)
        {
            using (var writer = new BinaryWriter(new MemoryStream()))
            {
                Data[0]++;
                writer.Write(Data);
                writer.Write(_type);
                writer.Write(_id);
                writer.Write(equipped);


                Data = (writer.BaseStream as MemoryStream).ToArray();
            }

            Update();
        }

        public void Update(short _type, short _id, bool equipped)
        {
            using (var reader = new BinaryReader(new MemoryStream(Data)))
            {
                var count = reader.ReadByte();
                for (var i = 0; i < count; i++)
                {
                    var type = reader.ReadInt16();
                    var id = reader.ReadInt16();
                    if (type == _type && id == _id)
                    {
                        Data[reader.BaseStream.Position++] = equipped ? (byte)1 : (byte)0;
                    }
                    else if (StorageManager.Wing<bool>(_type, _id) && StorageManager.Wing<bool>(type, id))
                        Data[reader.BaseStream.Position++] = 0;
                    else
                        reader.BaseStream.Position++;
                }
            }
            Update();
        }
    }
    public sealed class TitleStorage
    {
        public TitleStorage() { }
        public bool Read(byte[] packet)
        {
            using (var memoryStream = new MemoryStream(packet))
            {
                Info = Serializer.DeserializeWithLengthPrefix<TitleStorageProto>(memoryStream, PrefixStyle.Fixed32);
            }
            return true;
        }
        public void Login(Client.GameClient client)
        {
            if (client.Entity.WTitles == null)
            {
                client.Entity.WTitles = StorageManager.Find(t => t.Id == client.Entity.UID);
            }
            if (client.Entity.WTitles == null || client.Entity.WTitles.Data == null || client.Entity.WTitles.Data.Length == 0) return;
            var myPacketReader = new BinaryReader(new MemoryStream(client.Entity.WTitles.Data));
            var _count = myPacketReader.ReadByte();
            for (var i = 0; i < _count; i++)
            {
                var _type = myPacketReader.ReadInt16();
                var _id = myPacketReader.ReadInt16();
                var _equipped = myPacketReader.ReadBoolean();
                var pkt = new TitleStorageProto()
                {
                    ActionId = Action.Update,
                    Points = client.Entity.WTitles.Points,
                    Type = _type,
                    Id = _id,
                    Value = new TitleValue()
                    {
                        Type = _type,
                        Id = _id,
                        Equipped = _equipped
                    }
                };

                if (_equipped)
                    if (StorageManager.Wing<bool>(_type, _id))
                        client.Entity.EquippedWing = StorageManager.Wing<int>(_type, _id);
                    else if (StorageManager.Title<bool>(_type, _id))
                        client.Entity.EquippedTitle = StorageManager.Title<int>(_type, _id);

                client.Send(FinalizeProtoBuf(pkt));

                client.Entity.TitlePoints = client.Entity.WTitles.Points;
            }
        }
        public void Handle(Client.GameClient client)
        {
            switch (Info.ActionId)
            {
                case Action.Unequip:
                    if (StorageManager.Wing<bool>(Info.Type, Info.Id))
                    {
                        client.Entity.EquippedWing = 0;
                        client.Entity.WTitles.Update((short)Info.Type, (short)Info.Id, false);
                    }
                    else if (StorageManager.Title<bool>(Info.Type, Info.Id))
                    {
                        client.Entity.EquippedTitle = 0;
                        client.Entity.WTitles.Update((short)Info.Type, (short)Info.Id, false);
                    }
                    client.Send(FinalizeProtoBuf(Info));
                    break;
                case Action.Equip:
                    if (StorageManager.Wing<bool>(Info.Type, Info.Id))
                    {
                        client.Entity.EquippedWing = StorageManager.Wing<int>(Info.Type, Info.Id);
                        client.Entity.WTitles.Update((short)Info.Type, (short)Info.Id, true);
                    }
                    else if (StorageManager.Title<bool>(Info.Type, Info.Id))
                    {
                        client.Entity.EquippedTitle = StorageManager.Title<int>(Info.Type, Info.Id);
                        client.Entity.WTitles.Update((short)Info.Type, (short)Info.Id, true);
                    }

                    Info = new TitleStorageProto()
                    {
                        ActionId = Action.Equip,
                        Points = client.Entity.WTitles.Points,
                        Type = Info.Type,
                        Id = Info.Id,
                        Value = new TitleValue()
                        {
                            Type = Info.Type,
                            Id = Info.Id,
                            Equipped = false
                        }
                    };

                    client.Send(FinalizeProtoBuf(Info));
                    break;
            }
        }
        public void AddTitle(Client.GameClient client, short _type, short _id, bool equipped = false)
        {

            if (client.Entity.WTitles == null)
            {
                client.Entity.WTitles = new WardrobeTitles();
                client.Entity.WTitles.Id = client.Entity.UID;
                client.Entity.WTitles.Data = new byte[1];
                client.Entity.WTitles.Create();
            }

            var pkt = new TitleStorageProto()
            {
                ActionId = Action.Update,
                Points = client.Entity.WTitles.Points,
                Type = _type,
                Id = _id,
                Value = new TitleValue()
                {
                    Type = _type,
                    Id = _id,
                    Equipped = equipped
                }
            };

            client.Send(FinalizeProtoBuf(pkt));


            client.Entity.WTitles.AddTitle(_type, _id, equipped);

            if (!StorageManager.Data.ContainsKey(client.Entity.WTitles.Id))
                StorageManager.Data.Add(client.Entity.WTitles.Id, client.Entity.WTitles);
            else StorageManager.Data[client.Entity.WTitles.Id] = client.Entity.WTitles;

            if (equipped)
                if (StorageManager.Wing<bool>(_type, _id))
                    client.Entity.EquippedWing = StorageManager.Wing<int>(_type, _id);
                else if (StorageManager.Title<bool>(_type, _id))
                    client.Entity.EquippedTitle = StorageManager.Title<int>(_type, _id);

        }
        public byte[] FinalizeProtoBuf(TitleStorageProto titleStorageProto)
        {
            using (var memoryStream = new MemoryStream())
            {
                Serializer.SerializeWithLengthPrefix(memoryStream, titleStorageProto, PrefixStyle.Fixed32);
                var pkt = new byte[8 + memoryStream.Length];
                memoryStream.ToArray().CopyTo(pkt, 0);
                Writer.Write((ushort)memoryStream.Length, 0, pkt);
                Writer.Write((ushort)3301, 2, pkt);

                return pkt;
            }
        }


        public TitleStorageProto Info;
        [Flags]
        public enum Action : int
        {
            Update = 0,
            UseTitle = 1,
            RemoveTitle = 3,
            Equip = 4,
            Unequip = 5,
        }
    }
    [ProtoContract]
    public class TitleStorageProto
    {
        [ProtoMember(1, IsRequired = true)]
        public TitleStorage.Action ActionId;
        [ProtoMember(2, IsRequired = true)]
        public int Points;
        [ProtoMember(3, IsRequired = true)]
        public int Type;
        [ProtoMember(4, IsRequired = true)]
        public int Id;
        [ProtoMember(5, IsRequired = true)]
        public TitleValue Value;
    }
    [ProtoContract]
    public class TitleValue
    {
        [ProtoMember(1, IsRequired = true)]
        public int Type;
        [ProtoMember(2, IsRequired = true)]
        public int Id;
        [ProtoMember(3, IsRequired = true)]
        public bool Equipped;
        [ProtoMember(4, IsRequired = true)]
        public int dwParam4;
    }

}

