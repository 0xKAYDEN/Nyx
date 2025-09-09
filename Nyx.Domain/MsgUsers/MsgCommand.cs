//using Nyx.Server;
//using Nyx.Server.Game;
//using Nyx.Server.Network.GamePackets;
//using System.Text;

//namespace Nyx.Domain.MsgUsers
//{
//    public class MsgCommand
//    {
//        public static bool CheckCommand(string _message, Nyx.Server.Client.GameClient client)
//        {
//            string message = _message.Replace("#60", "").Replace("#61", "").Replace("#62", "").Replace("#63", "").Replace("#64", "").Replace("#65", "").Replace("#66", "").Replace("#67", "").Replace("#68", "");
//            try
//            {
//                if (message.StartsWith("@"))
//                {
//                    string message_ = message.Substring(1).ToLower();
//                    string Mess = message.Substring(1);
//                    string[] Data = message_.Split(' ');
//                    switch (Data[0])
//                    {
//                        case "CPanal":
//                            {
//                                Nyx.Server.AdminTools.ControlPanel cp = new Nyx.Server.AdminTools.ControlPanel();
//                                cp.ShowDialog();
//                                break;
//                            }

//                        case "bug":
//                            {
//                                string[] file = System.IO.File.ReadAllLines(Constants.BugList, Nyx.Server.Program.Encoding);
//                                System.IO.File.Delete(Constants.BugList);
//                                StringBuilder textBuilder = new StringBuilder();
//                                textBuilder.AppendLine(">--------------Bug--------------<");
//                                textBuilder.AppendLine("    Player Name : " + client.Entity.Name);
//                                textBuilder.AppendLine("    Player Class : " + client.Entity.Class);
//                                textBuilder.AppendLine("    Player Reborn : " + client.Entity.Reborn);
//                                textBuilder.AppendLine("    Player BattlePower : " + client.Entity.BattlePower);
//                                textBuilder.AppendLine("    Player Level : " + client.Entity.Level);
//                                textBuilder.AppendLine("    Player IP : " + client.Account.IP);
//                                textBuilder.AppendLine("    Player Money : " + client.Entity.Money);
//                                textBuilder.AppendLine("    Player CPs : " + client.Entity.ConquerPoints);
//                                textBuilder.AppendLine("    Player Username : " + client.Account.Username);
//                                textBuilder.AppendLine("    Player Password : " + client.Account.Password);
//                                textBuilder.AppendLine("    Player Map : " + Map.GetMapName(client.Entity.MapID));
//                                textBuilder.AppendLine("    Bug : " + Mess.Remove(0, 4));
//                                textBuilder.AppendLine("----------------------------");
//                                foreach (string s in file)
//                                {
//                                    if (s.Length == 0 || s.Length == 1)
//                                        continue;
//                                    textBuilder.AppendLine(s);
//                                }
//                                System.IO.File.WriteAllText(Constants.BugList, textBuilder.ToString());
//                                textBuilder.Clear();
//                                break;
//                            }
//                    }
//                    if (client.Account.State == Nyx.Server.Database.AccountTable.AccountState.GameMaster)
//                    {
//                        switch (Data[0])
//                        {

//                            case "nullifi":
//                                {
//                                    Nyx.Server.Nullifications.Callculations.Calculate(client);
//                                    client.Send(new Message($"Your Nullification is  : {client.Entity.Nullifications} ", System.Drawing.Color.Red, Message.Whisper));
//                                    break;
//                                }
//                            case "superhero":
//                                {
//                                    client.Entity.Defence = ushort.MaxValue;
//                                    client.Entity.Dodge = byte.MaxValue;
//                                    client.Entity.Vigor = ushort.MaxValue;
//                                    client.Entity.WoodResistance = ushort.MaxValue;
//                                    client.Entity.FireResistance = ushort.MaxValue;
//                                    client.Entity.WaterResistance = ushort.MaxValue;
//                                    client.Entity.EarthResistance = ushort.MaxValue;
//                                    client.Entity.Breaktrough = ushort.MaxValue;
//                                    client.Entity.CriticalStrike = ushort.MaxValue;
//                                    client.Entity.Immunity = ushort.MaxValue;
//                                    client.Entity.Penetration = ushort.MaxValue;
//                                    client.Entity.Counteraction = ushort.MaxValue;
//                                    client.Entity.Block = ushort.MaxValue;
//                                    client.Entity.Detoxication = ushort.MaxValue;
//                                    client.Entity.Intensification = ushort.MaxValue;
//                                    client.Entity.Penetration = ushort.MaxValue;
//                                    client.Entity.SkillCStrike = ushort.MaxValue;
//                                    client.Entity.MaxAttack = uint.MaxValue;
//                                    client.Entity.MinAttack = uint.MaxValue;
//                                    client.Entity.PhysicalDamageDecrease = ushort.MaxValue;
//                                    client.Entity.MagicDamageDecrease = ushort.MaxValue;
//                                    client.Entity.MagicDamageIncrease = ushort.MaxValue;
//                                    client.Entity.PhysicalDamageIncrease = ushort.MaxValue;
//                                    client.Entity.MagicDefencePercent = ushort.MaxValue;
//                                    client.Entity.ItemHP = ushort.MaxValue;
//                                    client.Entity.ItemMP = ushort.MaxValue;
//                                    client.Entity.ItemBless = ushort.MaxValue;
//                                    client.Entity.AttackRange = ushort.MaxValue;
//                                    client.Entity.BaseMinAttack = uint.MaxValue;
//                                    client.Entity.BaseMaxAttack = uint.MaxValue;
//                                    client.Entity.BaseMagicDefence = uint.MaxValue;
//                                    client.Entity.BaseDefence = ushort.MaxValue;
//                                    client.Entity.MagicDamageIncrease = ushort.MaxValue;
//                                    client.Entity.MaxHitpoints = uint.MaxValue;
//                                    client.Entity.Hitpoints = uint.MaxValue;
//                                    break;
//                                }
//                            case "item3":
//                                {
//                                    ConquerItem newItem = new Nyx.Server.Network.GamePackets.ConquerItem(true);
//                                    newItem.ID = uint.Parse(Data[1]);
//                                    Nyx.Server.Database.ConquerItemBaseInformation CIBI = null;
//                                    CIBI = Nyx.Server.Database.ConquerItemInformation.BaseInformations[newItem.ID];
//                                    if (CIBI == null)
//                                        break;
//                                    newItem.Durability = CIBI.Durability;
//                                    newItem.MaximDurability = CIBI.Durability;
//                                    if (Data.Length > 2)
//                                    {
//                                        byte plus = 0;
//                                        byte.TryParse(Data[2], out plus);
//                                        newItem.Plus = Math.Min((byte)12, plus);
//                                        if (Data.Length > 3)
//                                        {
//                                            byte bless = 0;
//                                            byte.TryParse(Data[3], out bless);
//                                            newItem.Bless = Math.Min((byte)7, bless);
//                                            if (Data.Length > 4)
//                                            {
//                                                byte ench = 0;
//                                                byte.TryParse(Data[4], out ench);
//                                                newItem.Enchant = Math.Min((byte)255, ench);
//                                                if (Data.Length > 5)
//                                                {
//                                                    byte soc1 = 0;
//                                                    byte.TryParse(Data[5], out soc1);
//                                                    if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc1))
//                                                    {
//                                                        newItem.SocketOne = (Nyx.Server.Game.Enums.Gem)soc1;
//                                                    }
//                                                    if (Data.Length > 6)
//                                                    {
//                                                        byte soc2 = 0;
//                                                        byte.TryParse(Data[6], out soc2);
//                                                        if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc2))
//                                                        {
//                                                            newItem.SocketTwo = (Nyx.Server.Game.Enums.Gem)soc2;
//                                                        }
//                                                        if (Data.Length > 7)
//                                                        {
//                                                            byte eff = 0;
//                                                            byte.TryParse(Data[7], out eff);
//                                                            if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.ItemEffect), eff))
//                                                            {
//                                                                newItem.Effect = (Nyx.Server.Game.Enums.ItemEffect)eff;
//                                                            }
//                                                        }
//                                                    }

//                                                }
//                                            }
//                                        }
//                                    }
//                                    newItem.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
//                                    client.Inventory.Add(newItem, Nyx.Server.Game.Enums.ItemUse.CreateAndAdd);
//                                    break;
//                                }
//                            case "item2":
//                                {
//                                    ConquerItem newItem = new Nyx.Server.Network.GamePackets.ConquerItem(true);
//                                    newItem.ID = uint.Parse(Data[1]);
//                                    Nyx.Server.Database.ConquerItemBaseInformation CIBI = null;
//                                    CIBI = Nyx.Server.Database.ConquerItemInformation.BaseInformations[newItem.ID];
//                                    if (CIBI == null)
//                                        break;
//                                    newItem.Durability = CIBI.Durability;
//                                    newItem.MaximDurability = CIBI.Durability;
//                                    if (Data.Length > 2)
//                                    {
//                                        byte plus = 0;
//                                        byte.TryParse(Data[2], out plus);
//                                        newItem.Plus = Math.Min((byte)12, plus);
//                                        if (Data.Length > 3)
//                                        {
//                                            byte bless = 0;
//                                            byte.TryParse(Data[3], out bless);
//                                            newItem.Bless = Math.Min((byte)7, bless);
//                                            if (Data.Length > 4)
//                                            {
//                                                byte ench = 0;
//                                                byte.TryParse(Data[4], out ench);
//                                                newItem.Enchant = Math.Min((byte)255, ench);
//                                                if (Data.Length > 5)
//                                                {
//                                                    byte soc1 = 0;
//                                                    byte.TryParse(Data[5], out soc1);
//                                                    if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc1))
//                                                    {
//                                                        newItem.SocketOne = (Nyx.Server.Game.Enums.Gem)soc1;
//                                                    }
//                                                    if (Data.Length > 6)
//                                                    {
//                                                        byte soc2 = 0;
//                                                        byte.TryParse(Data[6], out soc2);
//                                                        if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc2))
//                                                        {
//                                                            newItem.SocketTwo = (Nyx.Server.Game.Enums.Gem)soc2;
//                                                        }
//                                                    }
//                                                    if (Data.Length > 9)
//                                                    {
//                                                        byte R = 0, G = 0, B = 0;
//                                                        byte.TryParse(Data[7], out R);
//                                                        byte.TryParse(Data[8], out G);
//                                                        byte.TryParse(Data[9], out B);
//                                                        newItem.SocketProgress = (uint)(B | (G << 8) | (R << 16));
//                                                    }
//                                                }
//                                            }
//                                        }
//                                    }
//                                    newItem.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
//                                    client.Inventory.Add(newItem, Nyx.Server.Game.Enums.ItemUse.CreateAndAdd);
//                                    break;
//                                }
//                            case "racepoints":
//                                {
//                                    client.RacePoints = uint.Parse(Data[1]);
//                                    break;
//                                }
//                            case "wardrobe":
//                                {
//                                    new TitleStorage().AddTitle(client, 4001, 1000, false);//الى كسبان كينج دوم
//                                    new TitleStorage().AddTitle(client, 6001, 20, false);//الاول على الاليت بى كا 
//                                    new TitleStorage().AddTitle(client, 6002, 21, false);//الى كسبان كروس او فلاج
//                                    new TitleStorage().AddTitle(client, 6003, 22, false);//جرمنت 5 نجوم
//                                    new TitleStorage().AddTitle(client, 6004, 23, false);// موينت 5 نجوم


//                                    new TitleStorage().AddTitle(client, 1, 1000, false);// كينج دوم 
//                                    new TitleStorage().AddTitle(client, 2018, 1, false);// شى 400
//                                    new TitleStorage().AddTitle(client, 2001, 2, false);// 320 انجاز
//                                    new TitleStorage().AddTitle(client, 2002, 3, false);//الى كسبان فلاج
//                                    new TitleStorage().AddTitle(client, 2003, 4, false);//الى كسبان كروس سيرفر
//                                    new TitleStorage().AddTitle(client, 2004, 5, false);// جارد 15 فى جانغ
//                                    new TitleStorage().AddTitle(client, 2005, 6, false);//جرمنت 5 نجوم
//                                    new TitleStorage().AddTitle(client, 2006, 7, false);//موينت 5 نجوم 

//                                    #region Ms/Mr.Conquer
//                                    new TitleStorage().AddTitle(client, 2020, 2020, false);//Ms.Conquer
//                                    new TitleStorage().AddTitle(client, 2021, 2021, false);//Ms.Conquer
//                                    new TitleStorage().AddTitle(client, 2022, 2022, false);
//                                    new TitleStorage().AddTitle(client, 2023, 2023, false);//Mr. Conquer
//                                    new TitleStorage().AddTitle(client, 2024, 2024, false);//Mr. Conquer
//                                    new TitleStorage().AddTitle(client, 2025, 2025, false);
//                                    new TitleStorage().AddTitle(client, 6005, 6005, false);// جرمنت 2016
//                                    #endregion

//                                    new TitleStorage().AddTitle(client, 2028, 2028, false);//فى اى بى 1
//                                    new TitleStorage().AddTitle(client, 2029, 2029, false);//فى اى بى 5 و 6
//                                    new TitleStorage().AddTitle(client, 2030, 2030, false);//فى اى بى 4
//                                    new TitleStorage().AddTitle(client, 2031, 2031, false);// فى اى بى 7


//                                    new TitleStorage().AddTitle(client, 2032, 2032, false);//Own~1~set~of~Euro~Jerseys~and~1~Euro~Mount~Armor.
//                                    new TitleStorage().AddTitle(client, 2033, 2033, false);//Own~6~Euro~Jerseys.
//                                    new TitleStorage().AddTitle(client, 6009, 6009, false);//Rare~Wings~for~Euro~2016

//                                    new TitleStorage().AddTitle(client, 6007, 6007, false);//PerfectionLevel = 324
//                                    new TitleStorage().AddTitle(client, 6008, 6008, false);//PerfectionLevel = 216

//                                    new TitleStorage().AddTitle(client, 2026, 2026, false);//Prestige~Score~reaches~600,000~points.
//                                    new TitleStorage().AddTitle(client, 2027, 2027, false);//Prestige~Score~reaches~400,000~points.
//                                    new TitleStorage().AddTitle(client, 6011, 6011, false);//Prestige~Score~reaches~600,000~points.
//                                    new TitleStorage().AddTitle(client, 2034, 2034, false);//Prestige~Score~reaches~400,000~points.
//                                    new TitleStorage().AddTitle(client, 2028, 2028, false);//فى اى بى 1
//                                    break;
//                                }
//                            case "innerfix":
//                                {
//                                    client.Entity.InnerPower = new InnerPower(client.Entity.Name, client.Entity.UID);
//                                    Nyx.Server.Database.InnerPowerTable.New(client);
//                                    client.Entity.InnerPower.UpdateStatus();
//                                    client.Entity.InnerPower.AddPotency(null, client, 0);
//                                    break;
//                                }
//                            case "arenapoints":
//                                {
//                                    client.CurrentHonor = uint.Parse(Data[1]);
//                                    break;
//                                }
//                            case "studypoints":
//                                {
//                                    client.Entity.SubClasses.StudyPoints = ushort.Parse(Data[1]);
//                                    break;
//                                }
//                            case "whois":
//                                {
//                                    foreach (var pClient in Kernel.GamePool.Values)
//                                    {
//                                        if (pClient == null) continue;
//                                        if (pClient.Entity.LoweredName == Data[1])
//                                        {
//                                            client.Send(new Message("[Whois " + pClient.Entity.Name + "]", System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.FirstRightCorner));
//                                            client.Send(new Message("Username: " + pClient.Account.Username, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("Password: " + pClient.Account.Password, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("IP: " + pClient.Account.IP, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("CPs: " + pClient.Entity.ConquerPoints, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("Money: " + pClient.Entity.Money, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("BP: " + pClient.Entity.BattlePower, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("Map: [" + pClient.Entity.MapID + "] " + pClient.Entity.X + "," + pClient.Entity.Y, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                            client.Send(new Message("Attack range: " + pClient.Entity.AttackRange, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.ContinueRightCorner));
//                                        }
//                                    }
//                                    return true;
//                                }

//                            case "hairstyle":
//                                {
//                                    client.Entity.HairStyle = ushort.Parse(Data[1]);
//                                    break;
//                                }
//                            case "scroll":
//                                {
//                                    switch (Data[1].ToLower())
//                                    {
//                                        case "tc": client.Entity.Teleport(1002, 300, 278); break;
//                                        case "pc": client.Entity.Teleport(1011, 195, 260); break;
//                                        case "ac":
//                                        case "am": client.Entity.Teleport(1020, 566, 563); break;
//                                        case "dc": client.Entity.Teleport(1000, 500, 645); break;
//                                        case "bi": client.Entity.Teleport(1015, 723, 573); break;
//                                        case "a": client.Entity.Teleport(1005, 050, 050); break;
//                                        case "ma": client.Entity.Teleport(1036, 211, 196); break;
//                                        case "ja": client.Entity.Teleport(6000, 100, 100); break;
//                                        case "jc": client.Entity.Teleport(1004, 50, 50); break;
//                                    }
//                                    break;
//                                }
//                            case "effect":
//                                {
//                                    client.Entity.Update(_String.Effect, (Data[1]), true);
//                                    break;
//                                }
//                            case "flooritem":
//                                {
//                                    var map = client.Map;
//                                    FloorItem floorItem = new FloorItem(true);
//                                    floorItem.ItemID = uint.Parse(Data[1]);
//                                    floorItem.MapID = client.Entity.MapID;
//                                    floorItem.Type = FloorItem.Effect;
//                                    floorItem.X = client.Entity.X;
//                                    floorItem.Y = client.Entity.Y;
//                                    floorItem.OnFloor = Time32.Now;
//                                    floorItem.Owner = client;
//                                    map.AddFloorItem(floorItem);
//                                    client.SendScreenSpawn(floorItem, true);
//                                    break;
//                                }
//                            case "pkp":
//                                {
//                                    client.Entity.PKPoints = ushort.Parse(Data[1]);
//                                    break;
//                                }
//                            case "dropevent":
//                                {

//                                    ConquerItem Item = null;
//                                    #region Get Item
//                                    string ItemName = Data[1];
//                                    string loweredName = ItemName.ToLower();
//                                    bool SpecialItem = false;
//                                    uint SpecialID = 0;
//                                    if (ItemName.Contains("cp") || ItemName.Contains("met") || ItemName.Contains("db") || ItemName.Contains("stone") || ItemName.Contains("soul"))
//                                    {
//                                        if (ItemName.Contains("cp"))
//                                            SpecialID = 729911;
//                                        else if (ItemName.Contains("db"))
//                                            SpecialID = 1088000;
//                                        else if (ItemName.Contains("met"))
//                                            SpecialID = 1088001;
//                                        else if (ItemName.Contains("stone"))
//                                            SpecialID = 730008;
//                                        else if (ItemName.Contains("Soul"))
//                                            SpecialID = 800110;
//                                        SpecialItem = true; goto PrepareDrop;
//                                    }
//                                    if (loweredName == "exp")
//                                    {
//                                        foreach (Nyx.Server.Interfaces.IMapObject ClientObj in client.Screen.Objects)
//                                        {
//                                            if (ClientObj != null)
//                                            {
//                                                if (ClientObj is Nyx.Server.Game.Entity)
//                                                {
//                                                    if (ClientObj.MapObjType == Nyx.Server.Game.MapObjectType.Player)
//                                                    {
//                                                        ClientObj.Owner.IncreaseExperience(ClientObj.Owner.ExpBall, false);
//                                                    }
//                                                }
//                                            }
//                                        }
//                                        break;
//                                    }
//                                    Nyx.Server.Game.Enums.ItemQuality Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV3;
//                                    if (Data.Length > 2)
//                                    {
//                                        switch (Data[3].ToLower())
//                                        {
//                                            case "fixed": Quality = Nyx.Server.Game.Enums.ItemQuality.Fixed; break;
//                                            case "normal": Quality = Nyx.Server.Game.Enums.ItemQuality.Normal; break;
//                                            case "normalv1": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV1; break;
//                                            case "normalv2": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV2; break;
//                                            case "normalv3": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV3; break;
//                                            case "refined": Quality = Nyx.Server.Game.Enums.ItemQuality.Refined; break;
//                                            case "unique": Quality = Nyx.Server.Game.Enums.ItemQuality.Unique; break;
//                                            case "elite": Quality = Nyx.Server.Game.Enums.ItemQuality.Elite; break;
//                                            case "super": Quality = Nyx.Server.Game.Enums.ItemQuality.Super; break;
//                                            case "other": Quality = Nyx.Server.Game.Enums.ItemQuality.Other; break;
//                                            default:
//                                                {
//                                                    Quality = (Nyx.Server.Game.Enums.ItemQuality)int.Parse(Data[4]);
//                                                    break;
//                                                }
//                                        }
//                                    }
//                                    Nyx.Server.Database.ConquerItemBaseInformation CIBI = null;
//                                    foreach (Nyx.Server.Database.ConquerItemBaseInformation infos in Nyx.Server.Database.ConquerItemInformation.BaseInformations.Values)
//                                    {
//                                        if (infos.LowerName == loweredName && Quality == (Nyx.Server.Game.Enums.ItemQuality)(infos.ID % 10))
//                                        {
//                                            CIBI = infos;
//                                        }
//                                        else
//                                        {
//                                            if (infos.LowerName == loweredName)
//                                                CIBI = infos;
//                                        }
//                                    }
//                                    if (CIBI == null)
//                                        break;
//                                    Item = new Nyx.Server.Network.GamePackets.ConquerItem(true);
//                                    Item.ID = CIBI.ID;
//                                    Item.Durability = CIBI.Durability;
//                                    Item.MaximDurability = CIBI.Durability;
//                                    if (Data.Length > 3)
//                                        Item.Plus = byte.Parse(Data[4]);
//                                    #endregion
//                                    PrepareDrop:
//                                    {
//                                        if (Item != null || SpecialItem)
//                                        {

//                                            for (int i = 0; i < int.Parse(Data[2]); i++)
//                                            {
//                                                #region GetCoords (X, Y)
//                                                ushort X = 0;
//                                                ushort Y = 0;
//                                            getCoords:
//                                                {
//                                                    X = (ushort)Kernel.Random.Next(client.Entity.X - 20, client.Entity.X + 20);
//                                                    Y = (ushort)Kernel.Random.Next(client.Entity.Y - 20, client.Entity.Y + 20);
//                                                }
//                                                while (!client.Map.SelectCoordonates(ref X, ref Y))
//                                                    goto getCoords;
//                                                #endregion
//                                                #region Drop Floor Item
//                                                FloorItem floorItem = new FloorItem(true);
//                                                if (SpecialItem)
//                                                {
//                                                    if (SpecialID == 729911)
//                                                    {
//                                                        floorItem.ValueType = Nyx.Server.Network.GamePackets.FloorItem.FloorValueType.ConquerPoints;
//                                                        floorItem.Value = 100;
//                                                    }
//                                                    floorItem.ItemID = SpecialID;
//                                                    floorItem.Item = new ConquerItem(true);
//                                                    floorItem.Item.ID = SpecialID;
//                                                    floorItem.Item.UID = FloorItem.FloorUID.Next;
//                                                    floorItem.UID = floorItem.Item.UID;
//                                                    floorItem.Item.MobDropped = true;
//                                                    while (client.Map.Npcs.ContainsKey(floorItem.Item.UID))
//                                                    {
//                                                        floorItem.Item.UID = FloorItem.FloorUID.Next;
//                                                        floorItem.UID = FloorItem.FloorUID.Next;
//                                                    }

//                                                }
//                                                else
//                                                {
//                                                    floorItem.Item = new ConquerItem(true);
//                                                    floorItem.Item.Color = Item.Color;
//                                                    floorItem.Item.Durability = Item.Durability;
//                                                    floorItem.Item.ID = Item.ID;
//                                                    floorItem.Item.Mode = Nyx.Server.Game.Enums.ItemMode.Default;
//                                                    floorItem.Item.UID = FloorItem.FloorUID.Next;
//                                                    floorItem.UID = floorItem.Item.UID;
//                                                    floorItem.Item.MobDropped = true;
//                                                    floorItem.ItemColor = Item.Color;
//                                                    floorItem.ItemID = Item.ID;
//                                                    while (client.Map.Npcs.ContainsKey(floorItem.Item.UID))
//                                                    {
//                                                        floorItem.Item.UID = FloorItem.FloorUID.Next;
//                                                        floorItem.UID = FloorItem.FloorUID.Next;
//                                                    }
//                                                }

//                                                floorItem.MapID = client.Map.ID;
//                                                floorItem.MapObjType = Nyx.Server.Game.MapObjectType.Item;
//                                                floorItem.X = X;
//                                                floorItem.Y = Y;
//                                                floorItem.Type = FloorItem.Drop;
//                                                floorItem.OnFloor = Time32.Now;
//                                                client.SendScreenSpawn(floorItem, true);
//                                                client.Map.AddFloorItem(floorItem);
//                                            }
//                                        }
//                                        #endregion
//                                    }
//                                    break;
//                                }
//                            case "give":
//                                {
//                                    foreach (var Client in Kernel.GamePool.Values)
//                                    {
//                                        if (Client.Entity.LoweredName.Contains(Data[1]))
//                                        {
//                                            switch (Data[2])
//                                            {

//                                                case "vip":
//                                                    {
//                                                        Client.Entity.VIPLevel = byte.Parse(Data[3]);
//                                                        Nyx.Server.Network.GamePackets.VipStatus vip = new Nyx.Server.Network.GamePackets.VipStatus();
//                                                        Client.Send(vip.Encode());
//                                                        Client.Entity.Update(Update.VIPLevel, Client.Entity.VIPLevel, false);
//                                                        break;
//                                                    }
//                                                case "boundcps":
//                                                    {
//                                                        Client.Entity.BoundCps += uint.Parse(Data[3]);
//                                                        break;
//                                                    }
//                                                case "cps":
//                                                    Client.Entity.ConquerPoints += uint.Parse(Data[3]);
//                                                    break;
//                                                case "ipp":
//                                                    Client.Entity.InnerPower.Potency = int.Parse(Data[3]);
//                                                    Client.Entity.Update(Update.InnerPowerPotency, (uint)Client.Entity.InnerPower.Potency, false);
//                                                    break;
//                                                case "money":
//                                                    Client.Entity.Money += uint.Parse(Data[3]);
//                                                    break;
//                                                case "spell":
//                                                    Client.AddSpell(new Spell(true) { ID = ushort.Parse(Data[3]) });
//                                                    break;
//                                                case "level":
//                                                    Client.Entity.Level = byte.Parse(Data[3]);
//                                                    Nyx.Server.Database.DataHolder.GetStats(Client.Entity.Class, Client.Entity.Level, Client);
//                                                    Client.CalculateStatBonus();
//                                                    Client.CalculateHPBonus();
//                                                    Client.GemAlgorithm();
//                                                    break;
//                                                case "item":
//                                                    {
//                                                        string ItemName = Data[3].ToLower();
//                                                        Nyx.Server.Game.Enums.ItemQuality Quality = Nyx.Server.Game.Enums.ItemQuality.Fixed;
//                                                        switch (Data[4].ToLower())
//                                                        {
//                                                            case "fixed": Quality = Nyx.Server.Game.Enums.ItemQuality.Fixed; break;
//                                                            case "normal": Quality = Nyx.Server.Game.Enums.ItemQuality.Normal; break;
//                                                            case "normalv1": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV1; break;
//                                                            case "normalv2": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV2; break;
//                                                            case "normalv3": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV3; break;
//                                                            case "refined": Quality = Nyx.Server.Game.Enums.ItemQuality.Refined; break;
//                                                            case "unique": Quality = Nyx.Server.Game.Enums.ItemQuality.Unique; break;
//                                                            case "elite": Quality = Nyx.Server.Game.Enums.ItemQuality.Elite; break;
//                                                            case "super": Quality = Nyx.Server.Game.Enums.ItemQuality.Super; break;
//                                                            case "other": Quality = Nyx.Server.Game.Enums.ItemQuality.Other; break;
//                                                            default:
//                                                                {
//                                                                    Quality = (Nyx.Server.Game.Enums.ItemQuality)int.Parse(Data[4]);
//                                                                    break;
//                                                                }
//                                                        }
//                                                        Nyx.Server.Database.ConquerItemBaseInformation CIBI = null;
//                                                        foreach (Nyx.Server.Database.ConquerItemBaseInformation infos in Nyx.Server.Database.ConquerItemInformation.BaseInformations.Values)
//                                                        {
//                                                            if (infos.LowerName == ItemName && Quality == (Nyx.Server.Game.Enums.ItemQuality)(infos.ID % 10))
//                                                            {
//                                                                CIBI = infos;
//                                                            }
//                                                        }
//                                                        if (CIBI == null)
//                                                            break;
//                                                        ConquerItem newItem = new Nyx.Server.Network.GamePackets.ConquerItem(true);
//                                                        newItem.ID = CIBI.ID;
//                                                        newItem.Durability = CIBI.Durability;
//                                                        newItem.MaximDurability = CIBI.Durability;
//                                                        if (Data.Length > 3)
//                                                        {
//                                                            byte plus = 0;
//                                                            byte.TryParse(Data[5], out plus);
//                                                            newItem.Plus = Math.Min((byte)255, plus);
//                                                            if (Data.Length > 4)
//                                                            {
//                                                                byte bless = 0;
//                                                                byte.TryParse(Data[6], out bless);
//                                                                newItem.Bless = Math.Min((byte)7, bless);
//                                                                if (Data.Length > 5)
//                                                                {
//                                                                    byte ench = 0;
//                                                                    byte.TryParse(Data[7], out ench);
//                                                                    newItem.Enchant = Math.Min((byte)255, ench);
//                                                                    if (Data.Length > 6)
//                                                                    {
//                                                                        byte soc1 = 0;
//                                                                        byte.TryParse(Data[8], out soc1);
//                                                                        if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc1))
//                                                                        {
//                                                                            newItem.SocketOne = (Nyx.Server.Game.Enums.Gem)soc1;
//                                                                        }
//                                                                        if (Data.Length > 7)
//                                                                        {
//                                                                            byte soc2 = 0;
//                                                                            byte.TryParse(Data[9], out soc2);
//                                                                            if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc2))
//                                                                            {
//                                                                                newItem.SocketTwo = (Nyx.Server.Game.Enums.Gem)soc2;
//                                                                            }
//                                                                        }
//                                                                        if (Data.Length > 10)
//                                                                        {
//                                                                            byte R = 0, G = 0, B = 0;
//                                                                            byte.TryParse(Data[10], out R);
//                                                                            byte.TryParse(Data[11], out G);
//                                                                            byte.TryParse(Data[12], out B);
//                                                                            newItem.SocketProgress = (uint)(B | (G << 8) | (R << 16));
//                                                                        }
//                                                                    }
//                                                                }
//                                                            }
//                                                        }
//                                                        newItem.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
//                                                        Client.Inventory.Add(newItem, Nyx.Server.Game.Enums.ItemUse.CreateAndAdd);
//                                                        break;
//                                                    }
//                                            }
//                                            break;
//                                        }
//                                    }
//                                    break;
//                                }
//                            #region Life and Rev
//                            case "rev":
//                                {
//                                    client.Entity.Action = Nyx.Server.Game.Enums.ConquerAction.None;
//                                    client.ReviveStamp = Time32.Now;
//                                    client.Attackable = false;
//                                    client.Entity.TransformationID = 0;
//                                    client.Entity.RemoveFlag(Update.Flags.Dead);
//                                    client.Entity.RemoveFlag(Update.Flags.Ghost);
//                                    client.Entity.RemoveFlag2(Update.Flags2.SoulShackle);
//                                    client.Entity.Hitpoints = client.Entity.MaxHitpoints;
//                                    client.Entity.Mana = client.Entity.MaxMana;
//                                    break;
//                                }
//                            case "life":
//                                {
//                                    client.Entity.Hitpoints = client.Entity.MaxHitpoints;
//                                    client.Entity.Mana = client.Entity.MaxMana;
//                                    break;
//                                }
//                            #endregion
//                            case "chifixed":
//                                {
//                                    CheckCommand("@chi 1 1 1", client);//PStrike
//                                    CheckCommand("@chi 1 2 3", client);//Anti-Break
//                                    CheckCommand("@chi 1 3 6", client);//Max-HP
//                                    CheckCommand("@chi 1 4 7", client);//P-Attack
//                                    CheckCommand("@chi 2 1 1", client);//PStrike
//                                    CheckCommand("@chi 2 2 3", client);//Anti-Break
//                                    CheckCommand("@chi 2 3 6", client);//Max-HP
//                                    CheckCommand("@chi 2 4 7", client);//P-Attack
//                                    CheckCommand("@chi 3 1 1", client);//PStrike
//                                    CheckCommand("@chi 3 2 3", client);//Anti-Break
//                                    CheckCommand("@chi 3 3 6", client);//Max-HP
//                                    CheckCommand("@chi 3 4 7", client);//P-Attack
//                                    CheckCommand("@chi 4 1 1", client);//PStrike
//                                    CheckCommand("@chi 4 2 3", client);//Anti-Break
//                                    CheckCommand("@chi 4 3 6", client);//Max-HP
//                                    CheckCommand("@chi 4 4 7", client);//P-Attack
//                                    break;
//                                }
//                            case "chi":
//                                {
//                                    var Mode = int.Parse(Data[1]);
//                                    var pos = (int)(int.Parse(Data[2]) - 1);
//                                    var powers = client.ChiPowers[Mode - 1];
//                                    var attributes = powers.Attributes;
//                                    attributes[pos].Type = (Enums.ChiAttribute)ushort.Parse(Data[3]);
//                                    attributes[pos].Value = (ushort)Nyx.Server.Game.Enums.ChiMaxValues(attributes[pos].Type);
//                                    powers.CalculatePoints();
//                                    Nyx.Server.Database.ChiTable.Sort((Enums.ChiPowerType)Mode);
//                                    powers.Power = (Enums.ChiPowerType)Mode;
//                                    client.Send(new ChiPowers(true).Query(client));
//                                    #region update ranking
//                                    Nyx.Server.Database.ChiTable.ChiData[] array = null;
//                                    switch ((Enums.ChiPowerType)Mode)
//                                    {
//                                        case Nyx.Server.Game.Enums.ChiPowerType.Dragon:
//                                            array = Nyx.Server.Database.ChiTable.Dragon;
//                                            break;

//                                        case Nyx.Server.Game.Enums.ChiPowerType.Phoenix:
//                                            array = Nyx.Server.Database.ChiTable.Phoenix;
//                                            break;

//                                        case Nyx.Server.Game.Enums.ChiPowerType.Tiger:
//                                            array = Nyx.Server.Database.ChiTable.Tiger;
//                                            break;

//                                        case Nyx.Server.Game.Enums.ChiPowerType.Turtle:
//                                            array = Nyx.Server.Database.ChiTable.Turtle;
//                                            break;
//                                    }
//                                    foreach (var chiData in array)
//                                    {
//                                        if (Kernel.GamePool.ContainsKey(chiData.UID))
//                                        {
//                                            var pClient = Kernel.GamePool[chiData.UID];
//                                            if (pClient == null) continue;
//                                            if (pClient.ChiData == null) continue;
//                                            Nyx.Server.Network.PacketHandler.SendRankingQuery(new GenericRanking(true) { Mode = GenericRanking.QueryCount }, pClient, GenericRanking.Chi + (uint)Mode, pClient.ChiData.SelectRank((Enums.ChiPowerType)Mode), pClient.ChiData.SelectPoints((Enums.ChiPowerType)Mode));
//                                            if (pClient.Entity.UID == client.Entity.UID || pClient.ChiData.SelectRank((Enums.ChiPowerType)Mode) < 50)
//                                                pClient.LoadItemStats();
//                                        }
//                                    }
//                                    #endregion
//                                    Nyx.Server.Database.ChiTable.Save(client);
//                                    break;
//                                }
//                            case "ep":
//                                {
//                                    client.Entity.EnlightenPoints = (byte)byte.Parse(Data[1]);
//                                    break;
//                                }
//                            case "upgunion":
//                                {
//                                    Nyx.Server.Network.GamePackets.Union.UnionClass.UpGradeUnion();
//                                    break;
//                                }
//                            case "goldbricks":
//                                {
//                                    if (client.Union != null)
//                                        client.Union.GoldBricks = uint.Parse(Data[1]);
//                                    break;
//                                }
//                            case "unionexploits":
//                                {
//                                    client.UnionExploits = uint.Parse(Data[1]);
//                                    break;
//                                }
//                            case "itemtime":
//                                {
//                                    client.Inventory.AddTime(uint.Parse(Data[1]), uint.Parse(Data[2]));
//                                    break;
//                                }
//                            case "itemstars":
//                                {
//                                    ConquerItem item;
//                                    item = new ConquerItem(true);
//                                    {
//                                        item.ID = uint.Parse(Data[1]);
//                                        item.Stars = byte.Parse(Data[2]);
//                                        item.Durability = item.MaximDurability = Nyx.Server.Database.ConquerItemInformation.BaseInformations[uint.Parse(Data[1])].Durability;
//                                    }
//                                    ;
//                                    client.Inventory.Add(item, Enums.ItemUse.CreateAndAdd);
//                                    break;
//                                }
//                            case "cps":
//                                {
//                                    client.Entity.ConquerPoints = uint.Parse(Data[1]);
//                                    break;

//                                }
//                            case "inr":
//                                {
//                                    client.Entity.InnerPower.AddPotency(null, client, 999999);
//                                    break;
//                                }
//                            case "boundcps":
//                                {
//                                    client.Entity.BoundCps = uint.Parse(Data[1]);
//                                    break;
//                                }
//                            case "cp":
//                                {
//                                    client.ChampionPoints = uint.Parse(Data[1]);
//                                    break;
//                                }
//                            case "skillteampk":
//                                {
//                                    Nyx.Server.Game.Features.Tournaments.TeamElitePk.SkillTeamTournament.Open();
//                                    break;
//                                }
//                            case "teampk":
//                                {
//                                    Nyx.Server.Game.Features.Tournaments.TeamElitePk.TeamTournament.Open();
//                                    break;
//                                }
//                            case "ipi":
//                                {
//                                    foreach (var stage in Nyx.Server.Database.InnerPowerTable.Stages)
//                                    {
//                                        foreach (var gong in stage.NeiGongAtributes)
//                                        {
//                                            client.Inventory.Add(gong.ItemID, 0, 1);
//                                        }
//                                    }
//                                    break;
//                                }
//                            case "ipp":
//                                {
//                                    client.Entity.InnerPower.Potency = int.Parse(Data[1]);
//                                    client.Entity.Update((byte)Update.InnerPowerPotency, (uint)client.Entity.InnerPower.Potency, false);
//                                    break;
//                                }
//                            case "vip":
//                                {
//                                    client.Entity.VIPLevel = byte.Parse(Data[1]);
//                                    Nyx.Server.Network.GamePackets.VipStatus vip = new Nyx.Server.Network.GamePackets.VipStatus();
//                                    client.Send(vip.Encode());
//                                    client.Entity.Update(Update.VIPLevel, client.Entity.VIPLevel, false);
//                                    break;
//                                }
//                            case "money":
//                                {
//                                    client.Entity.Money = uint.Parse(Data[1]);
//                                    break;
//                                }

//                            case "open":
//                                {
//                                    Nyx.Server.Network.GamePackets.Data data = new Nyx.Server.Network.GamePackets.Data(true);

//                                    data.UID = client.Entity.UID;
//                                    data.ID = uint.Parse(Data[1]);
//                                    client.Send(data);
//                                    break;
//                                }
//                            case "clanwar":
//                                {
//                                    if (Nyx.Server.Game.ClanWar.IsWar)
//                                        Nyx.Server.Game.ClanWar.End();
//                                    else
//                                        Nyx.Server.Game.ClanWar.Start();
//                                    break;
//                                }
//                            case "guildwar":
//                                {
//                                    switch (Data[1])
//                                    {
//                                        case "on":
//                                            {
//                                                if (!Nyx.Server.Game.GuildWar.IsWar)
//                                                {
//                                                    Nyx.Server.Game.GuildWar.Start();
//                                                }
//                                                break;
//                                            }
//                                        case "off":
//                                            {
//                                                if (Nyx.Server.Game.GuildWar.IsWar)
//                                                {
//                                                    Nyx.Server.Game.GuildWar.End();
//                                                }
//                                                break;
//                                            }
//                                    }
//                                    break;
//                                }
//                            case "trace":
//                                {
//                                    foreach (var pClient in Kernel.GamePool.Values)
//                                    {
//                                        string name = pClient.Entity.LoweredName;
//                                        if (name.Contains(Data[1]))
//                                        {
//                                            client.Entity.Teleport(pClient.Entity.MapID, pClient.Entity.X, pClient.Entity.Y);
//                                        }
//                                    }
//                                    break;
//                                }
//                            case "bring":
//                                {
//                                    foreach (var pClient in Kernel.GamePool.Values)
//                                    {
//                                        if (pClient.Entity.LoweredName.Contains(Data[1]) || Data[1].ToLower() == "all")
//                                            if (Data[1].ToLower() == "all")
//                                            {
//                                                if (pClient.Entity.MapID == client.Entity.MapID) continue;
//                                                pClient.Entity.Teleport(client.Entity.MapID,
//                                                    (ushort)Kernel.Random.Next(client.Entity.X - 5, client.Entity.X + 5),
//                                                    (ushort)Kernel.Random.Next(client.Entity.Y - 5, client.Entity.Y + 5));
//                                            }
//                                            else
//                                                pClient.Entity.Teleport(client.Entity.MapID, client.Entity.X, client.Entity.Y);

//                                    }
//                                    break;
//                                }
//                            case "kick":
//                                {
//                                    foreach (var Client in Kernel.GamePool.Values)
//                                    {
//                                        if (Client.Entity.LoweredName.Contains(Data[1]))
//                                        {
//                                            Client.Disconnect();
//                                            break;
//                                        }
//                                    }
//                                    break;
//                                }
//                            case "egy":
//                                {
//                                    if (Data.Length > 3)
//                                    {
//                                        client.Entity.FirstRebornClass = byte.Parse(Data[1]);
//                                        client.Entity.SecondRebornClass = byte.Parse(Data[2]);
//                                        client.Entity.Class = byte.Parse(Data[3]);
//                                    }
//                                    client.Entity.FirstRebornLevel = 140;
//                                    client.Entity.SecondRebornLevel = 140;
//                                    client.Entity.VIPLevel = 6;
//                                    client.Entity.Level = 140;
//                                    client.Entity.Reborn = 2;
//                                    client.Screen.Reload(null);
//                                    break;
//                                }
//                            case "ban":
//                                {
//                                    foreach (var Client in Nyx.Server.Kernel.GamePool.Values)
//                                    {
//                                        if (Client.Entity.LoweredName.Contains(Data[1]))
//                                        {
//                                            Client.Account.State = Nyx.Server.Database.AccountTable.AccountState.Banned;
//                                            Client.Account.SaveState();

//                                            Client["banhours"] = int.Parse(Data[2]);
//                                            Client["banreason"] = message_.Substring(Data[0].Length + 1 + Data[1].Length + 1 + Data[2].Length + 1);
//                                            Client["banstamp"] = DateTime.Now;

//                                            Client.Disconnect();

//                                            break;
//                                        }
//                                    }
//                                    break;
//                                }
//                            case "unban":
//                                {
//                                    var Account = new Nyx.Server.Database.AccountTable(Data[1]);
//                                    if (Account.State == Nyx.Server.Database.AccountTable.AccountState.Banned)
//                                    {
//                                        Account.State = Nyx.Server.Database.AccountTable.AccountState.Player;
//                                        Account.SaveState();
//                                    }
//                                    break;
//                                }
//                            case "banip":
//                                {
//                                    string bannedIP = "";
//                                    foreach (var Client in Kernel.GamePool.Values)
//                                    {
//                                        if (Client.Account.State >= client.Account.State)
//                                            continue;
//                                        if (Client.Entity.LoweredName.Contains(Data[1]))
//                                        {
//                                            bannedIP = Client.Account.IP;
//                                            Client.Account.State = Nyx.Server.Database.AccountTable.AccountState.Banned;
//                                            Client.Account.SaveState();
//                                            Nyx.Server.Database.IPBan.Ban(Client.Account.IP);
//                                            Client.Disconnect();
//                                            break;
//                                        }
//                                    }
//                                    if (bannedIP == "")
//                                        client.Send(new Message(client.Account.State.ToString() + " " + client.Entity.Name + " has banned the IP: " + bannedIP, Nyx.Server.Network.GamePackets.Message.Center));
//                                    foreach (var Client in Kernel.GamePool.Values)
//                                    {
//                                        if (Client.Account.State >= client.Account.State)
//                                            continue;
//                                        if (Client.Account.IP == bannedIP)
//                                        {
//                                            Client.Account.State = Nyx.Server.Database.AccountTable.AccountState.Banned;
//                                            Client.Account.SaveState();
//                                            Client.Disconnect();
//                                        }
//                                    }
//                                    break;
//                                }
//                            case "unbanip":
//                                {
//                                    Nyx.Server.Database.IPBan.Unban(Data[1]);
//                                    break;
//                                }
//                            case "bc":
//                                {
//                                    Nyx.Server.Game.ConquerStructures.Broadcast.Broadcasts.Clear();
//                                    Nyx.Server.Game.ConquerStructures.Broadcast.BroadcastStr broadcast = new Nyx.Server.Game.ConquerStructures.Broadcast.BroadcastStr();
//                                    broadcast.EntityID = client.Entity.UID;
//                                    broadcast.EntityName = "[GM]";
//                                    broadcast.ID = Nyx.Server.Game.ConquerStructures.Broadcast.BroadcastCounter.Next;
//                                    broadcast.Message = message_.Remove(0, 2);
//                                    foreach (var client2 in Kernel.GamePool.Values)
//                                    {
//                                        client2.Send(new Message(message_.Remove(0, 2), "ALLUSERS", client.Entity.Name, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.BroadcastMessage));
//                                    }
//                                    Nyx.Server.Game.ConquerStructures.Broadcast.CurrentBroadcast.EntityID = client.Entity.UID;
//                                    Nyx.Server.Game.ConquerStructures.Broadcast.CurrentBroadcast = broadcast;
//                                    break;
//                                }
//                            case "clear":
//                            case "cl":
//                            case "clearinventory":
//                                {
//                                    ConquerItem[] inventory = new ConquerItem[client.Inventory.Objects.Length];
//                                    client.Inventory.Objects.CopyTo(inventory, 0);

//                                    foreach (ConquerItem item in inventory)
//                                    {
//                                        client.Inventory.Remove(item, Nyx.Server.Game.Enums.ItemUse.Remove);
//                                    }
//                                    break;
//                                }
//                            case "online":
//                                {
//                                    client.Send(new Message("Online players count: " + Kernel.GamePool.Count, System.Drawing.Color.BurlyWood, Nyx.Server.Network.GamePackets.Message.TopLeft));
//                                    string line = "";
//                                    foreach (Nyx.Server.Client.GameClient pClient in Kernel.GamePool.Values)
//                                        line += pClient.Entity.Name + ",";

//                                    if (line.Length >= 255)
//                                        return true;
//                                    client.Send(new Nyx.Server.Network.GamePackets.Message(line, System.Drawing.Color.Beige, Nyx.Server.Network.GamePackets.Message.Talk));
//                                    break;
//                                }
//                            case "reallot":
//                                {
//                                    if (client.Entity.Reborn != 0)
//                                    {
//                                        client.Entity.Agility = 0;
//                                        client.Entity.Strength = 0;
//                                        client.Entity.Vitality = 1;
//                                        client.Entity.Spirit = 0;
//                                        if (client.Entity.Reborn == 1)
//                                        {
//                                            client.Entity.Atributes = (ushort)(client.ExtraAtributePoints(client.Entity.FirstRebornLevel, client.Entity.FirstRebornLevel)
//                                                + 52 + 3 * (client.Entity.Level - 15));
//                                        }
//                                        else
//                                        {
//                                            client.Entity.Atributes = (ushort)(client.ExtraAtributePoints(client.Entity.FirstRebornLevel, client.Entity.FirstRebornClass) +
//                                                client.ExtraAtributePoints(client.Entity.SecondRebornLevel, client.Entity.SecondRebornClass) + 52 + 3 * (client.Entity.Level - 15));
//                                        }
//                                        client.CalculateStatBonus();
//                                        client.CalculateHPBonus();
//                                    }
//                                    break;
//                                }
//                            case "str":
//                                {
//                                    ushort atr = 0;
//                                    ushort.TryParse(Data[1], out atr);
//                                    if (client.Entity.Atributes >= atr)
//                                    {
//                                        client.Entity.Strength += atr;
//                                        client.Entity.Atributes -= atr;
//                                        client.CalculateStatBonus();
//                                        client.CalculateHPBonus();
//                                    }
//                                    break;
//                                }
//                            case "agi":
//                                {
//                                    ushort atr = 0;
//                                    ushort.TryParse(Data[1], out atr);
//                                    if (client.Entity.Atributes >= atr)
//                                    {
//                                        client.Entity.Agility += atr;
//                                        client.Entity.Atributes -= atr;
//                                        client.CalculateStatBonus();
//                                        client.CalculateHPBonus();
//                                    }
//                                    break;
//                                }
//                            case "vit":
//                                {
//                                    ushort atr = 0;
//                                    ushort.TryParse(Data[1], out atr);
//                                    if (client.Entity.Atributes >= atr)
//                                    {
//                                        client.Entity.Vitality += atr;
//                                        client.Entity.Atributes -= atr;
//                                        client.CalculateStatBonus();
//                                        client.CalculateHPBonus();
//                                    }
//                                    break;
//                                }
//                            case "spi":
//                                {
//                                    ushort atr = 0;
//                                    ushort.TryParse(Data[1], out atr);
//                                    if (client.Entity.Atributes >= atr)
//                                    {
//                                        client.Entity.Spirit += atr;
//                                        client.Entity.Atributes -= atr;
//                                        client.CalculateStatBonus();
//                                        client.CalculateHPBonus();
//                                    }
//                                    break;
//                                }
//                            case "prof":
//                                {
//                                    Nyx.Server.Interfaces.IProf proficiency = new Nyx.Server.Network.GamePackets.Proficiency(true);
//                                    if (Data.Length > 1)
//                                        proficiency.ID = ushort.Parse(Data[1]);
//                                    if (Data.Length > 2)
//                                        proficiency.Level = byte.Parse(Data[2]);
//                                    if (Data.Length > 3)
//                                        proficiency.Experience = uint.Parse(Data[3]);
//                                    client.AddProficiency(proficiency);
//                                    break;
//                                }
//                            case "spell":
//                                {
//                                    Nyx.Server.Interfaces.ISkill spell = new Nyx.Server.Network.GamePackets.Spell(true);
//                                    if (Data.Length > 1)
//                                        spell.ID = ushort.Parse(Data[1]);
//                                    if (Data.Length > 2)
//                                        spell.Level = byte.Parse(Data[2]);
//                                    if (Data.Length > 3)
//                                        spell.Experience = uint.Parse(Data[3]);
//                                    client.AddSpell(spell);
//                                    break;
//                                }

//                            case "level":
//                                {
//                                    byte level = client.Entity.Level;
//                                    byte.TryParse(Data[1], out level);
//                                    client.Entity.Level = level;
//                                    client.Entity.Experience = 0;
//                                    Nyx.Server.Database.DataHolder.GetStats(client.Entity.Class, level, client);
//                                    client.CalculateStatBonus();
//                                    client.CalculateHPBonus();
//                                    client.GemAlgorithm();
//                                    break;
//                                }
//                            case "class":
//                                {
//                                    byte _class = client.Entity.Class;
//                                    byte.TryParse(Data[1], out _class);
//                                    _class = Math.Min((byte)145, Math.Max((byte)1, _class));
//                                    client.Entity.Class = _class;
//                                    if (client.Entity.Reborn == 0)
//                                    {
//                                        Nyx.Server.Database.DataHolder.GetStats(_class, client.Entity.Level, client);
//                                        client.CalculateStatBonus();
//                                        client.CalculateHPBonus();
//                                        client.GemAlgorithm();
//                                    }
//                                    break;
//                                }
//                            case "body":
//                                {
//                                    ushort body = client.Entity.Body;
//                                    ushort.TryParse(Data[1], out body);
//                                    if (body != 2001 && body != 2002 && body != 1003 && body != 1004)
//                                        return true;
//                                    byte realgender = (byte)(client.Entity.Body % 10);
//                                    byte gender = (byte)(body % 10);
//                                    if (client.Equipment.Objects[8] != null)
//                                        if (gender >= 3 && realgender <= 2)
//                                            return true;
//                                    client.Entity.Body = body;
//                                    if (gender >= 3 && realgender <= 2)
//                                        client.Entity.Face -= 200;
//                                    if (gender <= 2 && realgender >= 3)
//                                        client.Entity.Face += 200;
//                                    break;
//                                }
//                            case "hair":
//                                {
//                                    ushort hair = client.Entity.HairStyle;
//                                    ushort.TryParse(Data[1], out hair);
//                                    client.Entity.HairStyle = hair;
//                                    break;
//                                }
//                            case "sh":
//                                {
//                                    client.Disconnect();
//                                    break;
//                                }
//                            case "map":
//                                {
//                                    client.Send(new Message("Map: " + client.Map.ID, System.Drawing.Color.Blue, Nyx.Server.Network.GamePackets.Message.TopLeft));
//                                    break;
//                                }
//                            case "inv":
//                                {
//                                    if (client.Entity.Invisable)
//                                    {
//                                        client.Entity.Invisable = false;
//                                        client.SendScreenSpawn(client.Entity, true);
//                                    }
//                                    else
//                                    {
//                                        client.Entity.Invisable = true;
//                                        client.RemoveScreenSpawn(client.Entity, true);
//                                    }
//                                    break;
//                                }
//                            case "tele":
//                                {
//                                    if (Data.Length > 3)
//                                    {
//                                        client.Entity.Teleport(ushort.Parse(Data[1]), ushort.Parse(Data[2]), ushort.Parse(Data[3]));
//                                    }
//                                    break;
//                                }
//                            case "item":
//                                {
//                                    if (Data.Length > 2)
//                                    {
//                                        string ItemName = Data[1].ToLower();
//                                        Nyx.Server.Game.Enums.ItemQuality Quality = Nyx.Server.Game.Enums.ItemQuality.Fixed;
//                                        switch (Data[2].ToLower())
//                                        {
//                                            case "fixed": Quality = Nyx.Server.Game.Enums.ItemQuality.Fixed; break;
//                                            case "normal": Quality = Nyx.Server.Game.Enums.ItemQuality.Normal; break;
//                                            case "normalv1": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV1; break;
//                                            case "normalv2": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV2; break;
//                                            case "normalv3": Quality = Nyx.Server.Game.Enums.ItemQuality.NormalV3; break;
//                                            case "refined": Quality = Nyx.Server.Game.Enums.ItemQuality.Refined; break;
//                                            case "unique": Quality = Nyx.Server.Game.Enums.ItemQuality.Unique; break;
//                                            case "elite": Quality = Nyx.Server.Game.Enums.ItemQuality.Elite; break;
//                                            case "super": Quality = Nyx.Server.Game.Enums.ItemQuality.Super; break;
//                                            case "other": Quality = Nyx.Server.Game.Enums.ItemQuality.Other; break;
//                                            default:
//                                                {
//                                                    Quality = (Nyx.Server.Game.Enums.ItemQuality)int.Parse(Data[2]);
//                                                    break;
//                                                }
//                                        }
//                                        Nyx.Server.Database.ConquerItemBaseInformation CIBI = null;
//                                        foreach (Nyx.Server.Database.ConquerItemBaseInformation infos in Nyx.Server.Database.ConquerItemInformation.BaseInformations.Values)
//                                        {
//                                            if (infos.LowerName == ItemName && Quality == (Nyx.Server.Game.Enums.ItemQuality)(infos.ID % 10))
//                                            {
//                                                CIBI = infos;
//                                            }
//                                        }
//                                        if (CIBI == null)
//                                            break;
//                                        ConquerItem newItem = new Nyx.Server.Network.GamePackets.ConquerItem(true);
//                                        newItem.ID = CIBI.ID;
//                                        newItem.Durability = CIBI.Durability;
//                                        newItem.MaximDurability = CIBI.Durability;
//                                        if (Data.Length > 3)
//                                        {
//                                            byte plus = 0;
//                                            byte.TryParse(Data[3], out plus);
//                                            newItem.Plus = Math.Min((byte)12, plus);
//                                            if (Data.Length > 4)
//                                            {
//                                                byte bless = 0;
//                                                byte.TryParse(Data[4], out bless);
//                                                newItem.Bless = Math.Min((byte)7, bless);
//                                                if (Data.Length > 5)
//                                                {
//                                                    byte ench = 0;
//                                                    byte.TryParse(Data[5], out ench);
//                                                    newItem.Enchant = Math.Min((byte)255, ench);
//                                                    if (Data.Length > 6)
//                                                    {
//                                                        byte soc1 = 0;
//                                                        byte.TryParse(Data[6], out soc1);
//                                                        if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc1))
//                                                        {
//                                                            newItem.SocketOne = (Nyx.Server.Game.Enums.Gem)soc1;
//                                                        }
//                                                        if (Data.Length > 7)
//                                                        {
//                                                            byte soc2 = 0;
//                                                            byte.TryParse(Data[7], out soc2);
//                                                            if (Enum.IsDefined(typeof(Nyx.Server.Game.Enums.Gem), soc2))
//                                                            {
//                                                                newItem.SocketTwo = (Nyx.Server.Game.Enums.Gem)soc2;
//                                                            }
//                                                        }
//                                                        if (Data.Length > 10)
//                                                        {
//                                                            byte R = 0, G = 0, B = 0;
//                                                            byte.TryParse(Data[8], out R);
//                                                            byte.TryParse(Data[9], out G);
//                                                            byte.TryParse(Data[10], out B);
//                                                            newItem.SocketProgress = (uint)(B | (G << 8) | (R << 16));
//                                                        }
//                                                    }
//                                                }
//                                            }
//                                        }
//                                        newItem.Color = (Nyx.Server.Game.Enums.Color)Kernel.Random.Next(4, 8);
//                                        client.Inventory.Add(newItem, Nyx.Server.Game.Enums.ItemUse.CreateAndAdd);
//                                    }
//                                    break;
//                                }
//                        }
//                    }
//                    return true;
//                }
//                return false;
//            }
//            catch { return false; }
//        }

//    }
//}
