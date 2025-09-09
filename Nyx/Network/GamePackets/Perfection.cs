using ProtoBuf;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using System.Text;
using Nyx.Server;
using Nyx.Server.Database;
using System.Threading.Tasks;

namespace Nyx.Server.Network.GamePackets
{
    public class MsgUserAbilityScore
    {
        public MsgUserAbilityScore() { }
        public MsgUserAbilityScoreProto Info;
        [ProtoContract]
        public class MsgUserAbilityScoreProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ActionId;
            [ProtoMember(2, IsRequired = true)]
            public uint UID;
            [ProtoMember(3, IsRequired = true)]
            public uint Level;
            [ProtoMember(4, IsRequired = true)]
            public Scores[] Scores;
        }
        [ProtoContract]
        public class Scores
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Type;
            [ProtoMember(2, IsRequired = true)]
            public uint Score;
        }
        public static List<Game.Entity> RankingList;
        public void GetRankingList()
        {
            RankingList = new List<Game.Entity>();
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("entities"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    if (reader.ReadUInt32("TotalPerfectionScore") < 20000) continue;
                    Game.Entity client = new Game.Entity(Game.EntityFlag.Player, false);
                    client.Name = reader.ReadString("Name");
                    client.UID = reader.ReadUInt32("UID");
                    client.Level = reader.ReadByte("Level");
                    client.Class = reader.ReadByte("Class");
                    client.Body = reader.ReadUInt16("Body");
                    client.Face = reader.ReadUInt16("Face");
                    client.TotalPerfectionScore = reader.ReadUInt32("TotalPerfectionScore");
                    RankingList.Add(Kernel.GamePool.ContainsKey(client.UID) ? Kernel.GamePool[client.UID].Entity : client);

                }
            }
        }
        public bool Read(byte[] packet)
        {
            try
            {
                var mypkt = new byte[packet.Length - 4];
                Array.Copy(packet, 4, mypkt, 0, mypkt.Length);
                using (var memoryStream = new MemoryStream(packet))
                {
                    Info = Serializer.DeserializeWithLengthPrefix<MsgUserAbilityScoreProto>(memoryStream, PrefixStyle.Fixed32);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
        public void SendScore(Client.GameClient client, Client.GameClient Observer)
        {
            var packet = new MsgUserAbilityScoreProto();
            packet.ActionId = 0;
            packet.UID = client.Entity.UID;
            packet.Level = client.Entity.Level;
            packet.Scores = new Scores[20];
            for (int i = 0; i < packet.Scores.Length; i++)
            {
                byte Type = (byte)(i + 1);
                packet.Scores[i] = new Scores();
                packet.Scores[i].Type = Type;
                packet.Scores[i].Score = GetScoreValue(client, Type);
            }


            var proto = Kernel.FinalizeProtoBuf(packet, 3253);
            Observer.Send(proto);
        }
        public uint GetScoreValue(Client.GameClient client, uint Type)
        {
            uint Score = 0;
            if (Type == 1) Score = (uint)MsgUserAbilityScore.CalculatePerfectionJiangPoints(client);
            if (Type == 2) Score = (uint)MsgUserAbilityScore.CalculatePerfectionChiPoints(client);
            if (Type == 3) Score = (uint)(client.Entity.InnerPower != null ? client.Entity.InnerPower.TotalScore * 2 : 0);
            if (Type == 4) Score = client.Equipment.GetFullEquipmentEnchantPoints;
            if (Type == 5) Score = (uint)(client.Entity.Level < 140 ? client.Entity.Level * 20 : client.Entity.Level * 25);
            if (Type == 6) Score = client.Equipment.GetFullEquipmentPerfecetionLevelPoints;
            if (Type == 7) Score = (uint)((client.Entity.Vitality + client.Entity.Atributes + client.Entity.Spirit + client.Entity.Strength + client.Entity.Agility) * 5);
            if (Type == 8) Score = (uint)(client.Entity.Reborn * 1000);
            if (Type == 9) Score = client.Equipment.GetFullEquipmentEnumPoints;
            if (Type == 10) Score = client.Equipment.GetFullEquipmentSocketPoints;
            if (Type == 11) Score = client.Equipment.GetFullEquipmentGemPoints;
            if (Type == 12) Score = client.Equipment.GetFullEquipmentRefinePoints;
            if (Type == 13) Score = client.Equipment.GetFullEquipmentPlusPoints;
            if (Type == 14) Score = client.Equipment.GetFullEquipmentSoulPoints;
            if (Type == 15) Score = client.Equipment.GetFullEquipmentBlessPoints;
            if (Type == 16) Score = CalculateSubClassPoints(client);
            if (Type == 17) Score = Database.StorageItem.PerfectionPoints(client, true);
            if (Type == 18) Score = Database.StorageItem.PerfectionPoints(client, false);
            if (Type == 19) Score = (uint)((uint)client.Entity.NobilityRank * 1000);
            if (Type == 20) Score = client.Equipment.GetFullEquipmentLevelPoints;
            return Score;
        }
        public void Handle(Client.GameClient client)
        {
            switch (Info.ActionId)
            {
                case 1://Observ
                    {
                        if (Kernel.GamePool.ContainsKey(Info.UID))
                        {
                            SendScore(client, client);
                            SendScore(Kernel.GamePool[Info.UID], client);
                        }
                        else
                        {
                            client.Send(Kernel.FinalizeProtoBuf(new MsgUserAbilityScoreProto()
                            {
                                ActionId = 1,
                                UID = 0
                            }, 3253));
                        }
                        break;
                    }
            }
        }
        public static int CalculatePerfectionChiPoints(Client.GameClient client)
        {
            int Point = 0;
            for (int i = 0; i < 4; i++)
            {
                if (client.ChiData.Powers.Count < 1) break;
                Point += ((client.ChiData.Powers[0].Attributes[i].Value / Game.Enums.ChiMaxValues(client.ChiData.Powers[0].Attributes[i].Type)) * 100) * 60;
            }
            for (int i = 0; i < 4; i++)
            {
                if (client.ChiData.Powers.Count < 2) break;
                Point += ((client.ChiData.Powers[1].Attributes[i].Value / Game.Enums.ChiMaxValues(client.ChiData.Powers[1].Attributes[i].Type)) * 100) * 60;
            }
            for (int i = 0; i < 4; i++)
            {
                if (client.ChiData.Powers.Count < 3) break;
                Point += ((client.ChiData.Powers[2].Attributes[i].Value / Game.Enums.ChiMaxValues(client.ChiData.Powers[2].Attributes[i].Type)) * 100) * 60;
            }
            for (int i = 0; i < 4; i++)
            {
                if (client.ChiData.Powers.Count < 4) break;
                Point += ((client.ChiData.Powers[3].Attributes[i].Value / Game.Enums.ChiMaxValues(client.ChiData.Powers[3].Attributes[i].Type)) * 100) * 60;
            }
            return Point;
        }
        public static int CalculatePerfectionJiangPoints(Client.GameClient client)
        {
            int Points = 0;
            if (client.Entity.MyJiang != null)
            {
                if (client.Entity.MyJiang.Inner_Strength == 0) return Points;
                if (client.Entity.MyJiang.Inner_Strength <= 16200) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.3);
                else if (client.Entity.MyJiang.Inner_Strength <= 40500) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.33);
                else if (client.Entity.MyJiang.Inner_Strength <= 60750) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.36);
                else if (client.Entity.MyJiang.Inner_Strength <= 72000) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.40);
                else if (client.Entity.MyJiang.Inner_Strength <= 79200) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.45);
                else if (client.Entity.MyJiang.Inner_Strength <= 80800) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.50);
                else if (client.Entity.MyJiang.Inner_Strength <= 81000) Points = (int)(client.Entity.MyJiang.Inner_Strength * 0.60);
            }
            return Points;
        }
        public static uint CalculateSubClassPoints(Client.GameClient client)
        {
            uint Points = 0;
            if (client.Entity.SubClasses.Classes != null)
            {
                foreach (var sub in client.Entity.SubClasses.Classes.Values)
                {
                    if (sub == null) continue;
                    Points += (uint)(sub.Level == 9 ? 1000 : sub.Level * 100);
                }
            }
            return Points;
        }
        public static uint CalculatePerfectionItemPoints(ConquerItem item)
        {
            uint Points = 50;
            if (item == null || !Database.ConquerItemInformation.BaseInformations.ContainsKey(item.ID)) return 50;
            #region Plus
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.Plus == 1) Points += 200;
                if (item.Plus == 2) Points += 600;
                if (item.Plus == 3) Points += 1200;
                if (item.Plus == 4) Points += 1800;
                if (item.Plus == 5) Points += 2600;
                if (item.Plus == 6) Points += 3500;
                if (item.Plus == 7) Points += 4800;
                if (item.Plus == 8) Points += 5800;
                if (item.Plus == 9) Points += 6800;
                if (item.Plus == 10) Points += 7800;
                if (item.Plus == 11) Points += 8800;
                if (item.Plus == 12) Points += 10000;
            }
            else
            {
                if (item.Plus == 1) Points += 400;
                if (item.Plus == 2) Points += 1200;
                if (item.Plus == 3) Points += 2400;
                if (item.Plus == 4) Points += 3600;
                if (item.Plus == 5) Points += 5200;
                if (item.Plus == 6) Points += 7000;
                if (item.Plus == 7) Points += 9600;
                if (item.Plus == 8) Points += 11600;
                if (item.Plus == 9) Points += 13600;
                if (item.Plus == 10) Points += 15600;
                if (item.Plus == 11) Points += 17600;
                if (item.Plus == 12) Points += 20000;
            }
            #endregion
            #region Quality
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.ID % 10 == 9) Points += 500;
                if (item.ID % 10 == 8) Points += 300;
                if (item.ID % 10 == 7) Points += 200;
                if (item.ID % 10 == 6) Points += 100;
                if (item.ID % 10 > 0 && item.ID % 10 < 6) Points += 50;
            }
            else
            {
                if (item.ID % 10 == 9) Points += 1000;
                if (item.ID % 10 == 8) Points += 600;
                if (item.ID % 10 == 7) Points += 400;
                if (item.ID % 10 == 6) Points += 200;
                if (item.ID % 10 > 0 && item.ID % 10 < 6) Points += 100;
            }
            #endregion
            #region Soul
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.Purification.PurificationLevel == 1) Points += 100;
                if (item.Purification.PurificationLevel == 2) Points += 300;
                if (item.Purification.PurificationLevel == 3) Points += 500;
                if (item.Purification.PurificationLevel == 4) Points += 800;
                if (item.Purification.PurificationLevel == 5) Points += 1200;
                if (item.Purification.PurificationLevel == 6) Points += 1600;
                if (item.Purification.PurificationLevel == 7) Points += 2000;
            }
            else
            {
                if (item.Purification.PurificationLevel == 1) Points += 200;
                if (item.Purification.PurificationLevel == 2) Points += 600;
                if (item.Purification.PurificationLevel == 3) Points += 1000;
                if (item.Purification.PurificationLevel == 4) Points += 1600;
                if (item.Purification.PurificationLevel == 5) Points += 2400;
                if (item.Purification.PurificationLevel == 6) Points += 3200;
                if (item.Purification.PurificationLevel == 7) Points += 4000;
            }
            #endregion
            #region Bless
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                Points += (uint)(item.Bless * 100);
            }
            else
            {
                Points += (uint)(item.Bless * 200);
            }
            #region Refine
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.ExtraEffect.EffectLevel == 1) Points += 100;
                if (item.ExtraEffect.EffectLevel == 2) Points += 400;
                if (item.ExtraEffect.EffectLevel == 3) Points += 800;
                if (item.ExtraEffect.EffectLevel == 4) Points += 1200;
                if (item.ExtraEffect.EffectLevel == 5) Points += 1600;
                if (item.ExtraEffect.EffectLevel == 6) Points += 2000;
            }
            else
            {
                if (item.ExtraEffect.EffectLevel == 1) Points += 200;
                if (item.ExtraEffect.EffectLevel == 2) Points += 800;
                if (item.ExtraEffect.EffectLevel == 3) Points += 1600;
                if (item.ExtraEffect.EffectLevel == 4) Points += 2400;
                if (item.ExtraEffect.EffectLevel == 5) Points += 3200;
                if (item.ExtraEffect.EffectLevel == 6) Points += 4000;
            }
            #endregion
            #endregion
            #region Level
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                var lvl = (uint)Database.ConquerItemInformation.BaseInformations[item.ID].Level;
                if (lvl <= 120)
                    Points += lvl * 3;
                else if (lvl <= 130)
                    Points += lvl * 5;
                else if (lvl <= 140)
                    Points += lvl * 6;
            }
            else
            {
                var lvl = (uint)Database.ConquerItemInformation.BaseInformations[item.ID].Level;
                if (lvl <= 120)
                    Points += lvl * 6;
                else if (lvl <= 130)
                    Points += lvl * 10;
                else if (lvl <= 140)
                    Points += lvl * 12;
            }
            #endregion
            #region Gem
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.SocketOne != (Game.Enums.Gem)0)
                {
                    if (item.SocketOne2 % 10 == 1) Points += 200;
                    if (item.SocketOne2 % 10 == 2) Points += 500;
                    if (item.SocketOne2 % 10 == 3) Points += 800;
                }
                if (item.SocketTwo != (Game.Enums.Gem)0)
                {
                    if (item.SocketTwo2 % 10 == 1) Points += 200;
                    if (item.SocketTwo2 % 10 == 2) Points += 500;
                    if (item.SocketTwo2 % 10 == 3) Points += 800;
                }
            }
            else
            {
                if (item.SocketOne != (Game.Enums.Gem)0)
                {
                    if (item.SocketOne2 % 10 == 1) Points += 400;
                    if (item.SocketOne2 % 10 == 2) Points += 1000;
                    if (item.SocketOne2 % 10 == 3) Points += 1600;
                }
                if (item.SocketTwo != (Game.Enums.Gem)0)
                {
                    if (item.SocketTwo2 % 10 == 1) Points += 400;
                    if (item.SocketTwo2 % 10 == 2) Points += 1000;
                    if (item.SocketTwo2 % 10 == 3) Points += 1600;
                }
            }
            #endregion
            #region PerfectionLevel
            if (item.Stars >= 1) Points += 180;
            if (item.Stars >= 2) Points += 180;
            if (item.Stars >= 3) Points += 180;
            if (item.Stars >= 4) Points += 180;
            if (item.Stars >= 5) Points += 180;
            if (item.Stars >= 6) Points += 180;
            if (item.Stars >= 7) Points += 180;
            if (item.Stars >= 8) Points += 180;
            if (item.Stars >= 9) Points += 180;
            if (item.Stars >= 10) Points += 2380;
            if (item.Stars >= 11) Points += 400;
            if (item.Stars >= 12) Points += 400;
            if (item.Stars >= 13) Points += 400;
            if (item.Stars >= 14) Points += 400;
            if (item.Stars >= 15) Points += 400;
            if (item.Stars >= 16) Points += 400;
            if (item.Stars >= 17) Points += 400;
            if (item.Stars >= 18) Points += 400;
            if (item.Stars >= 19) Points += 5150;
            if (item.Stars >= 20) Points += 650;
            if (item.Stars >= 21) Points += 650;
            if (item.Stars >= 22) Points += 650;
            if (item.Stars >= 23) Points += 650;
            if (item.Stars >= 24) Points += 650;
            if (item.Stars >= 25) Points += 650;
            if (item.Stars >= 26) Points += 650;
            if (item.Stars >= 27) Points += 650;
            if (item.Stars >= 28) Points += 100;
            if (item.Stars >= 29) Points += 100;
            if (item.Stars >= 30) Points += 100;
            if (item.Stars >= 31) Points += 100;
            if (item.Stars >= 32) Points += 100;
            if (item.Stars >= 33) Points += 100;
            if (item.Stars >= 34) Points += 100;
            if (item.Stars >= 35) Points += 100;
            if (item.Stars >= 36) Points += 100;
            if (item.Stars >= 37) Points += 100;
            if (item.Stars >= 38) Points += 100;
            if (item.Stars >= 39) Points += 100;
            if (item.Stars >= 40) Points += 100;
            if (item.Stars >= 41) Points += 100;
            if (item.Stars >= 42) Points += 100;
            if (item.Stars >= 43) Points += 100;
            if (item.Stars >= 44) Points += 100;
            if (item.Stars >= 45) Points += 100;
            if (item.Stars >= 46) Points += 100;
            if (item.Stars >= 47) Points += 100;
            if (item.Stars >= 48) Points += 100;
            if (item.Stars >= 49) Points += 100;
            if (item.Stars >= 50) Points += 100;
            if (item.Stars >= 51) Points += 100;
            if (item.Stars >= 52) Points += 100;
            if (item.Stars >= 53) Points += 100;
            if (item.Stars >= 54) Points += 100;
            if (Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.Stars >= 1) Points += 180;
                if (item.Stars >= 2) Points += 180;
                if (item.Stars >= 3) Points += 180;
                if (item.Stars >= 4) Points += 180;
                if (item.Stars >= 5) Points += 180;
                if (item.Stars >= 6) Points += 180;
                if (item.Stars >= 7) Points += 180;
                if (item.Stars >= 8) Points += 180;
                if (item.Stars >= 9) Points += 180;
                if (item.Stars >= 10) Points += 2380;
                if (item.Stars >= 11) Points += 400;
                if (item.Stars >= 12) Points += 400;
                if (item.Stars >= 13) Points += 400;
                if (item.Stars >= 14) Points += 400;
                if (item.Stars >= 15) Points += 400;
                if (item.Stars >= 16) Points += 400;
                if (item.Stars >= 17) Points += 400;
                if (item.Stars >= 18) Points += 400;
                if (item.Stars >= 19) Points += 5150;
                if (item.Stars >= 20) Points += 650;
                if (item.Stars >= 21) Points += 650;
                if (item.Stars >= 22) Points += 650;
                if (item.Stars >= 23) Points += 650;
                if (item.Stars >= 24) Points += 650;
                if (item.Stars >= 25) Points += 650;
                if (item.Stars >= 26) Points += 650;
                if (item.Stars >= 27) Points += 650;
                if (item.Stars >= 28) Points += 100;
                if (item.Stars >= 29) Points += 100;
                if (item.Stars >= 30) Points += 100;
                if (item.Stars >= 31) Points += 100;
                if (item.Stars >= 32) Points += 100;
                if (item.Stars >= 33) Points += 100;
                if (item.Stars >= 34) Points += 100;
                if (item.Stars >= 35) Points += 100;
                if (item.Stars >= 36) Points += 100;
                if (item.Stars >= 37) Points += 100;
                if (item.Stars >= 38) Points += 100;
                if (item.Stars >= 39) Points += 100;
                if (item.Stars >= 40) Points += 100;
                if (item.Stars >= 41) Points += 100;
                if (item.Stars >= 42) Points += 100;
                if (item.Stars >= 43) Points += 100;
                if (item.Stars >= 44) Points += 100;
                if (item.Stars >= 45) Points += 100;
                if (item.Stars >= 46) Points += 100;
                if (item.Stars >= 47) Points += 100;
                if (item.Stars >= 48) Points += 100;
                if (item.Stars >= 49) Points += 100;
                if (item.Stars >= 50) Points += 100;
                if (item.Stars >= 51) Points += 100;
                if (item.Stars >= 52) Points += 100;
                if (item.Stars >= 53) Points += 100;
                if (item.Stars >= 54) Points += 100;
            }
            #endregion
            #region Socket
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                if (item.SocketOne != (Game.Enums.Gem)0) Points += 1000;
                if (item.SocketTwo != (Game.Enums.Gem)0) Points += 2500;
            }
            else
            {
                if (item.SocketOne != (Game.Enums.Gem)0) Points += 2000;
                if (item.SocketTwo != (Game.Enums.Gem)0) Points += 5000;
            }
            #endregion
            #region Enchant
            if (!Network.PacketHandler.IsTwoHand(item.ID))
            {
                var enc = (uint)(item.Enchant);
                if (enc != 0)
                {
                    if (enc <= 200) Points += enc * 1;
                    if (enc <= 240) Points += (uint)(enc * 1.3);
                    if (enc <= 254) Points += (uint)(enc * 1.6);
                    if (enc <= 255) Points += enc * 2;
                }
            }
            else
            {
                var enc = (uint)(item.Enchant);
                if (enc != 0)
                {
                    if (enc <= 200) Points += enc * 2;
                    if (enc <= 240) Points += (uint)(enc * 2.6);
                    if (enc <= 254) Points += (uint)(enc * 3.2);
                    if (enc <= 255) Points += enc * 4;
                }
            }
            #endregion
            return Points;
        }
    }
    public class MsgItemRefineOpt
    {
        public MsgItemRefineOpt() { }
        public MsgItemRefineOptProto Info;
        public MsgItemRefineOptProto2 Info2;
        [ProtoContract]
        public class MsgItemRefineOptProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ActionId;
            [ProtoMember(2, IsRequired = true)]
            public uint ItemUID;
            [ProtoMember(3, IsRequired = true)]
            public string Signature;
            [ProtoMember(4, IsRequired = true)]
            public uint ItemPlusUID;
        }
        [ProtoContract]
        public class MsgItemRefineOptProto2
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ItemUID;
            [ProtoMember(2, IsRequired = true)]
            public uint EntityUID;
            [ProtoMember(3, IsRequired = true)]
            public uint Stars;
            [ProtoMember(4, IsRequired = true)]
            public uint Progress;
            [ProtoMember(5, IsRequired = true)]
            public uint OwnerUID;
            [ProtoMember(6, IsRequired = true)]
            public string OwnerName;
        }
        public double GetPerfectionProgress(uint value)
        {
            return (value * 1.25);
        }
        public uint GetPerfectionProgress(ConquerItem item)
        {
            if (item.Stars == 0) return 30;
            if (item.Stars == 1) return 60;
            if (item.Stars == 2) return 100;
            if (item.Stars == 3) return 200;
            if (item.Stars == 4) return 350;
            if (item.Stars == 5) return 600;
            if (item.Stars == 6) return 1000;
            if (item.Stars == 7) return 1500;
            if (item.Stars == 8) return 2300;
            if (item.Stars == 9) return 3500;
            if (item.Stars == 10) return 5000;
            if (item.Stars == 11) return 6500;
            if (item.Stars == 12) return 8000;
            if (item.Stars == 13) return 9500;
            if (item.Stars == 14) return 11000;
            return 12000;
        }
        public uint GetPlusStonePoints(uint value)
        {
            if (value == 3009000) return 10;
            if (value == 3009001) return 100;
            if (value == 3009002) return 1000;
            if (value == 3009003) return 10000;
            if (value == 730001 || value == 1) return 10;
            if (value == 730002 || value == 2) return 40;
            if (value == 730003 || value == 3) return 120;
            if (value == 730004 || value == 4) return 360;
            if (value == 730005 || value == 5) return 1080;
            if (value == 730006 || value == 6) return 3240;
            if (value == 730007 || value == 7) return 9720;
            if (value == 730008 || value == 8) return 29160;
            if (value == 9) return 87480;
            if (value == 10) return 90180;
            if (value == 11) return 95680;
            if (value == 12) return 104680;
            return 0;
        }
        public uint GetTransferCost(Network.GamePackets.ConquerItem item)
        {
            if (item.Stars == 2) return 30;
            if (item.Stars == 3) return 70;
            if (item.Stars == 4) return 150;
            if (item.Stars == 5) return 300;
            if (item.Stars == 6) return 500;
            if (item.Stars == 7) return 800;
            if (item.Stars == 8) return 1100;
            if (item.Stars >= 9) return 1500;
            return 10;
        }
        public void Handle(Client.GameClient client, byte[] myPacket)
        {
            try
            {
                switch (Info.ActionId)
                {
                    case 0://Temper
                        {
                            #region Minors
                            var packet2 = new byte[myPacket.Length - 4];
                            Array.Copy(myPacket, 4, packet2, 0, packet2.Length);
                            var variable = Kernel.Read7BitEncodedInt(packet2).SelectMany(BitConverter.GetBytes).ToArray();
                            var variable2 = new byte[variable.Length - 8];
                            Array.Copy(variable, 8, variable2, 0, variable.Length - 8);
                            var MinorsCount = (variable2.Length / 4);
                            uint[] Minors = new uint[MinorsCount];
                            byte x = 0;
                            for (int i = 0; i < MinorsCount; i++)
                            {
                                Minors[i] = BitConverter.ToUInt32(variable2, x);
                                x += 4;
                            }
                            #endregion
                            if (Minors != null)
                            {
                                ConquerItem Item;
                                if (client.Equipment.TryGetItem(Info.ItemUID, out Item))
                                {
                                    foreach (var minor in Minors)
                                    {
                                        ConquerItem ItemPlus;
                                        if (client.Inventory.TryGetItem(minor, out ItemPlus))
                                        {
                                            Item.PerfectionProgress += GetPlusStonePoints(ItemPlus.ID == 300000 ? ItemPlus.Plus : ItemPlus.ID)/*Database.PerfectionTable.CostList[ItemPlus.ID].Progress*/;
                                            client.Inventory.Remove(minor, Game.Enums.ItemUse.Remove, true);
                                        }
                                    }
                                    while (Item.PerfectionProgress >= GetPerfectionProgress(Item)/*Database.PerfectionTable.UpgradeList.Where(i => i.Stars == (Item.Stars + 1)).FirstOrDefault().Progress*/)
                                    {
                                        Item.PerfectionProgress -= GetPerfectionProgress(Item)/*Database.PerfectionTable.UpgradeList.Where(i => i.Stars == (Item.Stars + 1)).FirstOrDefault().Progress*/;
                                        Item.Stars++;
                                    }
                                    while (Item.Stars >= 54 && Item.PerfectionProgress > 0)
                                    {
                                        Item.Stars = 54;
                                        Item.PerfectionProgress = 0;
                                    }
                                    Item.Owner = client.Entity.Name;
                                    Item.OwnerUID = client.Entity.UID;
                                    Item.Mode = Game.Enums.ItemMode.Update;
                                    Item.Send(client);
                                    var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                    {
                                        ItemUID = Item.UID,
                                        EntityUID = client.Entity.UID,
                                        OwnerUID = Item.OwnerUID,
                                        OwnerName = Item.Owner,
                                        Progress = Item.PerfectionProgress,
                                        Stars = Item.Stars
                                    }), 3250);
                                    client.LoadItemStats();
                                    client.Send(proto);
                                    Database.ConquerItemTable.UpdatePerfection(Item);
                                    new MsgUserTotalRefineLev().Send(client);

                                }
                            }

                            break;
                        }
                    case 1://Transfer
                        {
                            ConquerItem Item;
                            if (client.Equipment.TryGetItem(Info.ItemUID, out Item))
                            {
                                if (client.Entity.ConquerPoints >= GetTransferCost(Item))
                                {
                                    client.Entity.ConquerPoints -= GetTransferCost(Item);
                                    Item.Owner = client.Entity.Name;
                                    Item.OwnerUID = client.Entity.UID;
                                    Item.Mode = Game.Enums.ItemMode.Update;
                                    Item.Send(client);
                                    var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                    {
                                        ItemUID = Item.UID,
                                        EntityUID = client.Entity.UID,
                                        OwnerUID = Item.OwnerUID,
                                        OwnerName = Item.Owner,
                                        Progress = Item.PerfectionProgress,
                                        Stars = Item.Stars
                                    }), 3250);
                                    client.Send(proto);
                                    Database.ConquerItemTable.UpdatePerfection(Item);
                                }
                            }
                            break;
                        }
                    case 2://Signature
                        {
                            ConquerItem Item;
                            if (client.Equipment.TryGetItem(Info.ItemUID, out Item))
                            {
                                if (Item.Signature == String.Empty || Item.Signature == null)
                                {
                                    Item.Signature = Info.Signature;
                                    Item.Mode = Game.Enums.ItemMode.Update;
                                    Item.Send(client);
                                    var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                    {
                                        ItemUID = Item.UID,
                                        EntityUID = client.Entity.UID,
                                        OwnerUID = Item.OwnerUID,
                                        OwnerName = Item.Owner,
                                        Progress = Item.PerfectionProgress,
                                        Stars = Item.Stars
                                    }), 3250);
                                    Database.ConquerItemTable.UpdatePerfection(Item);
                                }
                                else
                                {
                                    if (client.Entity.ConquerPoints >= 270)
                                    {
                                        client.Entity.ConquerPoints -= 270;
                                        Item.Signature = Info.Signature;
                                        Item.Mode = Game.Enums.ItemMode.Update;
                                        Item.Send(client);
                                        var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                        {
                                            ItemUID = Item.UID,
                                            EntityUID = client.Entity.UID,
                                            OwnerUID = Item.OwnerUID,
                                            OwnerName = Item.Owner,
                                            Progress = Item.PerfectionProgress,
                                            Stars = Item.Stars
                                        }), 3250);
                                        Database.ConquerItemTable.UpdatePerfection(Item);
                                    }
                                    else if (client.Entity.BoundCps >= 270)
                                    {
                                        client.Entity.BoundCps -= 270;
                                        Item.Signature = Info.Signature;
                                        Item.Mode = Game.Enums.ItemMode.Update;
                                        Item.Send(client);
                                        var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                        {
                                            ItemUID = Item.UID,
                                            EntityUID = client.Entity.UID,
                                            OwnerUID = Item.OwnerUID,
                                            OwnerName = Item.Owner,
                                            Progress = Item.PerfectionProgress,
                                            Stars = Item.Stars
                                        }), 3250);
                                        Database.ConquerItemTable.UpdatePerfection(Item);
                                    }
                                }

                            }
                            break;
                        }
                    case 3://CPBoost
                        {
                            ConquerItem Item;
                            if (client.Equipment.TryGetItem(Info.ItemUID, out Item) && Item != null)
                            {
                                uint value;
                                value = Math.Min(414112, client.Entity.ConquerPoints);
                                if (client.Entity.ConquerPoints >= value)
                                {
                                    client.Entity.ConquerPoints -= value;
                                    Item.Owner = client.Entity.Name;
                                    Item.OwnerUID = client.Entity.UID;
                                    Item.PerfectionProgress += (uint)GetPerfectionProgress(value);
                                    while (Item.PerfectionProgress >= GetPerfectionProgress(Item))
                                    {
                                        Item.PerfectionProgress -= GetPerfectionProgress(Item);
                                        Item.Stars++;
                                    }
                                    while (Item.Stars >= 54 && Item.PerfectionProgress > 0)
                                    {
                                        Item.Stars = 54;
                                        Item.PerfectionProgress = 0;
                                    }
                                    Item.Mode = Game.Enums.ItemMode.Update;
                                    Item.Send(client);
                                    var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                    {
                                        ItemUID = Item.UID,
                                        EntityUID = client.Entity.UID,
                                        OwnerUID = Item.OwnerUID,
                                        OwnerName = Item.Owner,
                                        Progress = Item.PerfectionProgress,
                                        Stars = Item.Stars
                                    }), 3250);
                                    client.Send(proto);
                                    Database.ConquerItemTable.UpdatePerfection(Item);
                                }
                            }
                            break;
                        }
                    case 4://Exchange
                        {
                            ConquerItem Item;
                            ConquerItem Item2;
                            if (client.Equipment.TryGetItem(Info.ItemUID, out Item2) || client.Inventory.TryGetItem(Info.ItemUID, out Item2))
                            {
                                if (client.Inventory.TryGetItem(Info.ItemPlusUID, out Item) || client.Equipment.TryGetItem(Info.ItemPlusUID, out Item))
                                {
                                    if (client.Entity.ConquerPoints >= 1000)
                                    {
                                        client.Entity.ConquerPoints -= 1000;
                                        Item.PerfectionProgress = Item2.PerfectionProgress;
                                        Item.Stars = Item2.Stars;
                                        Item.Mode = Game.Enums.ItemMode.Update;
                                        Item.Send(client);
                                        Item2.PerfectionProgress = 0;
                                        Item2.Stars = 0;
                                        Item2.Mode = Game.Enums.ItemMode.Update;
                                        Item2.Send(client);
                                        var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                        {
                                            ItemUID = Item.UID,
                                            EntityUID = client.Entity.UID,
                                            OwnerUID = Item.OwnerUID,
                                            OwnerName = Item.Owner,
                                            Progress = Item.PerfectionProgress,
                                            Stars = Item.Stars
                                        }), 3250);
                                        Database.ConquerItemTable.UpdatePerfection(Item);
                                    }
                                }
                            }
                            break;
                        }
                    case 5://Quick
                        {
                            ConquerItem Item;
                            if (client.Equipment.TryGetItem(Info.ItemUID, out Item))
                            {
                                if (Item.PerfectionProgress == 0 || Item.Stars >= 54) break;
                                double percent = (double)Item.PerfectionProgress / (double)GetPerfectionProgress(Item) * 100;
                                if (Kernel.Rate(percent))
                                {
                                    Item.Stars++;
                                }
                                Item.PerfectionProgress = 0;
                                Item.Owner = client.Entity.Name;
                                Item.OwnerUID = client.Entity.UID;
                                Item.Mode = Game.Enums.ItemMode.Update;
                                Item.Send(client);
                                var proto = Kernel.FinalizeProtoBuf((new MsgItemRefineOptProto2()
                                {
                                    ItemUID = Item.UID,
                                    EntityUID = client.Entity.UID,
                                    OwnerUID = Item.OwnerUID,
                                    OwnerName = Item.Owner,
                                    Progress = Item.PerfectionProgress,
                                    Stars = Item.Stars
                                }), 3250);
                                Database.ConquerItemTable.UpdatePerfection(Item);
                                client.LoadItemStats();
                            }
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public byte GetStars(byte Stars)
        {
            if (Stars >= 0 && Stars <= 8) return 9;
            if (Stars >= 9 && Stars <= 17) return 18;
            if (Stars >= 18 && Stars <= 26) return 27;
            if (Stars >= 27 && Stars <= 35) return 36;
            if (Stars >= 36 && Stars <= 44) return 45;
            if (Stars >= 45 && Stars <= 53) return 54;
            return 0;
        }
        public byte[] GetBytes(string packetString)
        {
            if (packetString != "" && packetString != null && packetString != string.Empty && packetString != " ")
            {
                byte[] bytes = new byte[(packetString.Length / 3) + 1];
                int t = 0;
                for (int i = 0; i < packetString.Length; i += 3)
                {
                    string cha = packetString.Substring(i, 2);
                    bytes[t] = byte.Parse(cha, System.Globalization.NumberStyles.HexNumber);
                    t++;
                }
                return bytes;
            }
            return null;
        }
        public bool Read(byte[] packet)
        {
            try
            {
                var mypkt = new byte[packet.Length - 4];
                Array.Copy(packet, 4, mypkt, 0, mypkt.Length);
                using (var memoryStream = new MemoryStream(packet))
                {
                    Info = Serializer.DeserializeWithLengthPrefix<MsgItemRefineOptProto>(memoryStream, PrefixStyle.Fixed32);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
    public class MsgItemRefineRecord
    {
        public MsgItemRefineRecord() { }
        public MsgItemRefineRecordProto Info;
        [ProtoContract]
        public class MsgItemRefineRecordProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ActionId;
            [ProtoMember(2, IsRequired = true)]
            public uint MaxPerfectionLevel;
        }
        public void Handle(Client.GameClient client)
        {
            switch (Info.ActionId)
            {
                case 0:
                    {
                        Send(client);
                        break;
                    }
            }
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Kernel.FinalizeProtoBuf(new MsgItemRefineRecordProto()
            {
                ActionId = 0,
                MaxPerfectionLevel = 54
            }, 3255));
        }
        public bool Read(byte[] packet)
        {
            var mypkt = new byte[packet.Length - 4];
            Array.Copy(packet, 4, mypkt, 0, mypkt.Length);
            using (var memoryStream = new MemoryStream(packet))
            {
                Info = Serializer.DeserializeWithLengthPrefix<MsgItemRefineRecordProto>(memoryStream, PrefixStyle.Fixed32);
            }
            return true;
        }
    }
    public class MsgRankMemberShow
    {
        public MsgRankMemberShow() { }
        public MsgRankMemberShowProto Info;
        [ProtoContract]
        public class MsgRankMemberShowProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ActionId;
            [ProtoMember(2, IsRequired = true)]
            public uint dwParam;
            [ProtoMember(3, IsRequired = true)]
            public uint Count;
        }
        [ProtoContract]
        public class MsgRankMemberShowProto2
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ActionId;
            [ProtoMember(2, IsRequired = true)]
            public uint dwParam;
            [ProtoMember(3, IsRequired = true)]
            public uint Count;
            [ProtoMember(4, IsRequired = true)]
            public uint UID;
            [ProtoMember(5, IsRequired = true)]
            public uint Mesh;
            [ProtoMember(6, IsRequired = true)]
            public string Name;
            [ProtoMember(7, IsRequired = true)]
            public string GuildName;
            [ProtoMember(8, IsRequired = true)]
            public uint[] GearItemsID;
        }
        public void GetEntityStuff(ref uint[] index, uint entityid)
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("items").Where("EntityID", entityid))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    if (reader.ReadByte("Position") == 1)
                    {
                        index[0] = reader.ReadUInt32("ID");
                    }
                    if (reader.ReadByte("Position") == 9)
                    {
                        index[1] = reader.ReadUInt32("ID");
                    }
                    if (reader.ReadByte("Position") == 4)
                    {
                        index[2] = reader.ReadUInt32("ID");
                    }
                    if (reader.ReadByte("Position") == 15)
                    {
                        index[3] = reader.ReadUInt32("ID");
                    }
                    if (reader.ReadByte("Position") == 5)
                    {
                        index[4] = reader.ReadUInt32("ID");
                    }
                    if (reader.ReadByte("Position") == 16)
                    {
                        index[5] = reader.ReadUInt32("ID");
                    }
                    if (reader.ReadByte("Position") == 17)
                    {
                        index[6] = reader.ReadUInt32("ID");
                    }
                }
                index[7] = 9350736;
                index[8] = 1;
                index[9] = 1;
                index[10] = 144;
                index[11] = 1;
            }
        }
        public void Handle(Client.GameClient client)
        {
            var Entity = MsgUserAbilityScore.RankingList.OrderByDescending(i => i.TotalPerfectionScore).ThenByDescending(i => i.Level).ThenBy(i => i.UID).FirstOrDefault();
            if (Entity == null) return;
            var index = new uint[12];
            GetEntityStuff(ref index, Entity.UID);
            var proto = new MsgRankMemberShowProto2()
            {
                ActionId = 0,
                Count = 1,
                dwParam = 1,
                Mesh = Entity.Mesh,
                UID = Entity.UID,
                Name = Entity.Name,
                GearItemsID = new uint[index.Length],
                GuildName = "pineAppleXpresS"
            };
            for (byte i = 0; i < index.Length; i++)
            {
                proto.GearItemsID[i] = index[i];
            }
            client.Send(Kernel.FinalizeProtoBuf(proto, 3257));

        }
        public bool Read(byte[] packet)
        {
            var mypkt = new byte[packet.Length - 4];
            Array.Copy(packet, 4, mypkt, 0, mypkt.Length);
            try
            {
                using (var memoryStream = new MemoryStream(packet))
                {
                    Info = Serializer.DeserializeWithLengthPrefix<MsgRankMemberShowProto>(memoryStream, PrefixStyle.None);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
    public class MsgEquipRefineRank
    {
        public MsgEquipRefineRank() { }
        public MsgEquipRefineRankProto Info;
        [Flags]
        public enum ActionID
        {
            MainRank = 0,
            RankItems = 1,
            UserItemRanking = 2,
            MyRanks = 3,
            View = 4,

        }
        [ProtoContract]
        public class MsgEquipRefineRankProto
        {
            [ProtoMember(1, IsRequired = true)]
            public ActionID Action;
            [ProtoMember(2)]
            public uint RegisteredCount;
            [ProtoMember(3)]
            public uint Page;
            [ProtoMember(4, IsRequired = true)]
            public uint Index;
            [ProtoMember(5, IsRequired = true)]
            public EquipProto[] items;
        }
        [ProtoContract]
        public class EquipProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Rank;
            [ProtoMember(2, IsRequired = true)]
            public uint UnKnow2;
            [ProtoMember(3, IsRequired = true)]
            public uint Position;
            [ProtoMember(4, IsRequired = true)]
            public uint RankScore;
            [ProtoMember(5, IsRequired = true)]
            public uint UID;
            [ProtoMember(6, IsRequired = true)]
            public uint ItemID;
            [ProtoMember(7, IsRequired = true)]
            public uint PurificationID;
            [ProtoMember(8, IsRequired = true)]
            public uint Plus;
            [ProtoMember(9, IsRequired = true)]
            public uint PerfectionLevel;
            [ProtoMember(10, IsRequired = true)]
            public string Name = "";
        }
        public byte GetRealyPosition(byte FakePosition)
        {
            if (FakePosition == 5) return 19;
            if (FakePosition == 7) return 18;
            if (FakePosition == 9) return 12;
            return FakePosition;
        }
        public static Dictionary<byte, ConquerItem> MainRank;
        public void UpdateRanking()
        {
            AllItemsRanking = new List<ConquerItem>();
            MainRank = new Dictionary<byte, ConquerItem>();
            for (byte i = 0; i < 11; i++)
            {
                byte z = GetRealyPosition((byte)(i + 1));
                List<ConquerItem> items = new List<ConquerItem>();
                MySqlCommand command = new MySqlCommand(MySqlCommandType.SELECT);
                command.Select("items").Where("Position", z).And("Stars", 0, true);
                MySqlReader reader = new MySqlReader(command);
                while (reader.Read())
                {
                    ConquerItem item = new ConquerItem(true);
                    item.ID = reader.ReadUInt32("ID");
                    item.UID = reader.ReadUInt32("UID");
                    item.MaximDurability = reader.ReadUInt16("MaximDurability");
                    item.Durability = reader.ReadUInt16("Durability");
                    item.Position = reader.ReadUInt16("Position");
                    item.Signature = reader.ReadString("Signature");
                    item.SocketProgress = reader.ReadUInt32("SocketProgress");
                    item.PlusProgress = reader.ReadUInt32("PlusProgress");
                    item.Stars = reader.ReadByte("Stars");
                    item.PerfectionProgress = reader.ReadUInt32("PerfectionProgress");
                    item.Owner = reader.ReadString("Owner");
                    item.OwnerUID = reader.ReadUInt32("OwnerUID");
                    item.SocketOne = (Game.Enums.Gem)reader.ReadByte("SocketOne");
                    item.SocketTwo = (Game.Enums.Gem)reader.ReadByte("SocketTwo");
                    item.Effect = (Game.Enums.ItemEffect)reader.ReadByte("Effect");
                    item.Mode = Game.Enums.ItemMode.Default;
                    item.Plus = reader.ReadByte("Plus");
                    item.Bless = reader.ReadByte("Bless");
                    item.Bound = reader.ReadBoolean("Bound");
                    item.TimeStamp = DateTime.FromBinary(reader.ReadInt64("TimeStamp"));
                    item.Minutes = reader.ReadUInt64("MinutesLeft");
                    item.Enchant = reader.ReadByte("Enchant");
                    item.Lock = reader.ReadByte("Locked");
                    item.UnlockEnd = DateTime.FromBinary(reader.ReadInt64("UnlockEnd"));
                    item.Suspicious = reader.ReadBoolean("Suspicious");
                    item.SuspiciousStart = DateTime.FromBinary(reader.ReadInt64("SuspiciousStart"));
                    item.Color = (Game.Enums.Color)reader.ReadByte("Color");
                    item.Inscribed = reader.ReadByte("Inscribed") == 1;
                    item.StackSize = reader.ReadUInt16("StackSize");
                    item.Warehouse = reader.ReadByte("Warehouse");
                    item.RefineItem = reader.ReadUInt32("RefineryItem");
                    item.InWardrobe = reader.ReadBoolean("InWardrobe");
                    Int64 rTime = reader.ReadInt64("RefineryTime");
                    ItemAddingTable.GetAddingsForItem(item);
                    items.Add(item);
                    if (!AllItemsRanking.Contains(item))
                        AllItemsRanking.Add(item);

                }
                MainRank[z] = items.OrderByDescending(x => MsgUserAbilityScore.CalculatePerfectionItemPoints(x)).FirstOrDefault();
            }
        }
        public static List<ConquerItem> AllItemsRanking;
        public void Handle(Client.GameClient client)
        {
            switch (Info.Action)
            {
                case ActionID.UserItemRanking:
                    {
                        var item = AllItemsRanking.Where(p => p.Position == GetRealyPosition((byte)Info.Index)).OrderByDescending(i => MsgUserAbilityScore.CalculatePerfectionItemPoints(i)).ToArray();
                        var min = Math.Min(item.Length, 50);
                        for (int i = 0; i < min; i++)
                        {
                            if (client.Equipment.IsWearingItemUID(item[i].UID))
                            {
                                var packet = new MsgEquipRefineRankProto();
                                packet.Page = Info.Page;
                                packet.RegisteredCount = Info.RegisteredCount;
                                packet.Action = Info.Action;
                                packet.Index = Info.Index;
                                packet.items = new EquipProto[1];
                                packet.items[0] = new EquipProto();
                                var itemm = item[i];
                                packet.items[0].Rank = (uint)(i + 1);
                                packet.items[0].RankScore = MsgUserAbilityScore.CalculatePerfectionItemPoints(itemm);
                                packet.items[0].ItemID = itemm.ID;
                                packet.items[0].Name = itemm.Owner.Replace("/0", "");
                                packet.items[0].PerfectionLevel = itemm.Stars;
                                packet.items[0].Position = GetRealyPosition((byte)Info.Index);
                                packet.items[0].Plus = itemm.Plus;
                                packet.items[0].UID = itemm.UID;
                                packet.items[0].UnKnow2 = (uint)Kernel.Random.Next(800, 3000);
                                client.Send(Kernel.FinalizeProtoBuf(packet, 3256));
                                break;
                            }
                        }
                        break;
                    }
                case ActionID.MyRanks:
                    {
                        var packet = new MsgEquipRefineRankProto();
                        packet.Action = ActionID.MyRanks;
                        packet.items = new EquipProto[11];
                        for (byte i = 0; i < packet.items.Length; i++)
                        {
                            byte z = GetRealyPosition((byte)(i + 1));
                            var itemmm = AllItemsRanking.Where(p => p.Position == z).OrderByDescending(m => MsgUserAbilityScore.CalculatePerfectionItemPoints(m)).ToArray();
                            for (int h = 0; h < itemmm.Length; h++)
                            {
                                if (client.Equipment.IsWearingItemUID(itemmm[h].UID))
                                {
                                    packet.items[i] = new EquipProto();
                                    packet.items[i].ItemID = itemmm[h].ID;
                                    packet.items[i].Name = itemmm[h].Owner.Replace("/0", "");
                                    packet.items[i].PerfectionLevel = itemmm[h].Stars;
                                    packet.items[i].Plus = itemmm[h].Plus;
                                    packet.items[i].Position = (uint)(i + 1);
                                    if (itemmm[h].Purification.Available)
                                        packet.items[i].PurificationID = itemmm[h].Purification.PurificationItemID;
                                    packet.items[i].Rank = (uint)(h + 1);
                                    packet.items[i].RankScore = MsgUserAbilityScore.CalculatePerfectionItemPoints(itemmm[h]);
                                    packet.items[i].UID = itemmm[h].UID;
                                    packet.items[i].UnKnow2 = (uint)Kernel.Random.Next(800, 3000);
                                    break;
                                }
                            }
                        }



                        var proto = Kernel.FinalizeProtoBuf(packet, 3256);
                        client.Send(proto);
                        break;
                    }
                case ActionID.View://ViewItem
                    {
                        if (Database.ConquerItemTable.LoadItem(Info.Index) != null)
                        {
                            var item = Database.ConquerItemTable.LoadItem(Info.Index);
                            item.Mode = Game.Enums.ItemMode.Perfection;
                            item.Send(client);
                        }
                        else
                        {
                            client.Send(Kernel.FinalizeProtoBuf(Info, 3256));
                        }
                        break;
                    }
                case ActionID.RankItems://AllRanking
                    {
                        var cnt = AllItemsRanking.Where(p => p.Position == GetRealyPosition((byte)Info.Index)).Count();
                        var packet = new MsgEquipRefineRankProto();
                        packet.Action = ActionID.RankItems;
                        packet.RegisteredCount = (uint)Math.Min(cnt, 50);
                        packet.Page = Info.Page;
                        packet.Index = Info.Index;
                        uint sss = (ushort)Math.Min(cnt - (packet.Page * 10), 10);

                        int rank = (int)packet.Page * 10;
                        packet.items = new EquipProto[sss];
                        for (int i = rank; i < rank + sss; i++)
                        {
                            var iteeeem = AllItemsRanking.Where(p => p.Position == GetRealyPosition((byte)Info.Index)).OrderByDescending(x => MsgUserAbilityScore.CalculatePerfectionItemPoints(x)).ToArray()[i];
                            if (iteeeem == null) continue;
                            packet.items[i] = new EquipProto();
                            packet.items[i].ItemID = iteeeem.ID;
                            packet.items[i].Name = iteeeem.Owner.Replace("/0", "");
                            packet.items[i].PerfectionLevel = iteeeem.Stars;
                            packet.items[i].Plus = iteeeem.Plus;
                            packet.items[i].Position = GetRealyPosition((byte)Info.Index);
                            if (iteeeem.Purification.Available)
                                packet.items[i].PurificationID = iteeeem.Purification.PurificationItemID;
                            packet.items[i].Rank = (uint)(i + 1);
                            packet.items[i].RankScore = MsgUserAbilityScore.CalculatePerfectionItemPoints(iteeeem);
                            packet.items[i].UID = iteeeem.UID;
                            packet.items[i].UnKnow2 = (uint)Kernel.Random.Next(800, 3000);
                        }
                        var proto = Kernel.FinalizeProtoBuf(packet, 3256);
                        client.Send(proto);
                        break;
                    }
                case ActionID.MainRank:
                    {


                        var packet = new MsgEquipRefineRankProto();
                        packet.Action = ActionID.MainRank;
                        packet.items = new EquipProto[11];
                        for (byte i = 0; i < packet.items.Length; i++)
                        {
                            byte z = GetRealyPosition((byte)(i + 1));
                            ConquerItem itemmm = MainRank[z];
                            if (itemmm == null) continue;
                            packet.items[i] = new EquipProto();
                            packet.items[i].ItemID = itemmm.ID;
                            packet.items[i].Name = itemmm.Owner.Replace("/0", "");
                            packet.items[i].PerfectionLevel = itemmm.Stars;
                            packet.items[i].Plus = itemmm.Plus;
                            packet.items[i].Position = (uint)(i + 1);
                            if (itemmm.Purification.Available)
                                packet.items[i].PurificationID = itemmm.Purification.PurificationItemID;
                            packet.items[i].Rank = 1;
                            packet.items[i].RankScore = MsgUserAbilityScore.CalculatePerfectionItemPoints(itemmm);
                            packet.items[i].UID = itemmm.UID;
                            packet.items[i].UnKnow2 = (uint)Kernel.Random.Next(800, 3000);
                        }



                        var proto = Kernel.FinalizeProtoBuf(packet, 3256);
                        client.Send(proto);
                        break;
                    }
            }
        }
        public bool Read(byte[] packet)
        {
            using (var memoryStream = new MemoryStream(packet))
            {
                Info = Serializer.DeserializeWithLengthPrefix<MsgEquipRefineRankProto>(memoryStream, PrefixStyle.Fixed32);
            }
            return true;
        }
    }
    public class MsgUserTotalRefineLev
    {
        public MsgUserTotalRefineLev() { }
        public MsgUserTotalRefineLevProto Info;
        [ProtoContract]
        public class MsgUserTotalRefineLevProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ActionId;
            [ProtoMember(2, IsRequired = true)]
            public uint EntityUID;
            [ProtoMember(3, IsRequired = true)]
            public uint TotalPrefectionLevel;
        }
        public void SendPerfectionEffects(Client.GameClient client)
        {
            if (client.Equipment.TotalPerfectionLevel >= 10) { client.Entity.MaxAttack += 100; client.Entity.MinAttack += 100; client.Entity.MagicAttack += 100; }
            if (client.Equipment.TotalPerfectionLevel >= 70) { client.Entity.Defence += 100; client.Entity.MagicDefence += 100; }
            if (client.Equipment.TotalPerfectionLevel >= 75) { client.Entity.MaxAttack += 200; client.Entity.MinAttack += 200; client.Entity.MagicAttack += 300; }
            if (client.Equipment.TotalPerfectionLevel >= 100) { client.Entity.Defence += 200; client.Entity.MagicDefence += 50; }
            if (client.Equipment.TotalPerfectionLevel >= 140) { client.Entity.MaxAttack += 200; client.Entity.MinAttack += 200; client.Entity.MagicAttack += 400; }
            if (client.Equipment.TotalPerfectionLevel >= 150) { client.Entity.MaxAttack += 300; client.Entity.MinAttack += 300; client.Entity.MagicAttack += 500; }
            if (client.Equipment.TotalPerfectionLevel >= 160) { client.Entity.Defence += 300; client.Entity.MagicDefence += 125; }
            if (client.Equipment.TotalPerfectionLevel >= 180) { client.Entity.Defence += 400; client.Entity.MagicDefence += 125; }
            if (client.Equipment.TotalPerfectionLevel >= 235) { client.Entity.MaxAttack += 400; client.Entity.MinAttack += 400; client.Entity.MagicAttack += 500; }
            if (client.Equipment.TotalPerfectionLevel >= 270) { client.Entity.Defence += 400; client.Entity.MagicDefence += 125; }
            if (client.Equipment.TotalPerfectionLevel >= 273) { client.Entity.MaxAttack += 400; client.Entity.MinAttack += 400; client.Entity.MagicAttack += 500; }
            if (client.Equipment.TotalPerfectionLevel >= 306) { client.Entity.MaxAttack += 400; client.Entity.MinAttack += 400; client.Entity.MagicAttack += 500; }
            if (client.Equipment.TotalPerfectionLevel >= 310) { client.Entity.Defence += 400; client.Entity.MagicDefence += 125; }
            if (client.Equipment.TotalPerfectionLevel >= 316) { client.Entity.Defence += 500; client.Entity.MagicDefence += 125; }
            if (client.Equipment.TotalPerfectionLevel >= 318) { client.Entity.MaxAttack += 500; client.Entity.MinAttack += 500; client.Entity.MagicAttack += 500; }
            if (client.Equipment.TotalPerfectionLevel >= 323) { client.Entity.Defence += 500; client.Entity.MagicDefence += 125; }
            if (client.Equipment.TotalPerfectionLevel >= 324) { client.Entity.MaxAttack += 500; client.Entity.MinAttack += 500; client.Entity.MagicAttack += 500; }
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Kernel.FinalizeProtoBuf(new MsgUserTotalRefineLevProto()
            {
                ActionId = 0,
                EntityUID = client.Entity.UID,
                TotalPrefectionLevel = client.Equipment.TotalPerfectionLevel
            }, 3252));
        }
    }
    public class MsgRefineEffect
    {
        public MsgRefineEffect() { }
        public MsgRefineEffectProto Info;
        [ProtoContract]
        public class MsgRefineEffectProto
        {
            [ProtoMember(1, IsRequired = true)]
            public uint AttackerUID;
            [ProtoMember(2, IsRequired = true)]
            public uint AttackedUID;
            [ProtoMember(3, IsRequired = true)]
            public uint Effect;
        }
        public byte GetChance(Client.GameClient client, Game.Enums.PerfectionEffect effect)
        {
            if (client.Equipment.TotalPerfectionLevel == 0) return 0;
            byte Chance = 255;
            if (effect == Game.Enums.PerfectionEffect.CalmWind)
            {
                if (client.Equipment.TotalPerfectionLevel >= 3) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 90) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 170) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 250) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 285) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.BloodSpawn)
            {
                if (client.Equipment.TotalPerfectionLevel >= 40) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 240) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 288) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.DivineGuard)
            {
                if (client.Equipment.TotalPerfectionLevel >= 1) Chance += 5;
            }
            if (effect == Game.Enums.PerfectionEffect.CoreStrike)
            {
                if (client.Equipment.TotalPerfectionLevel >= 50) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 80) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 135) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 205) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 245) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.DrainingTouch)
            {
                if (client.Equipment.TotalPerfectionLevel >= 35) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 220) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 276) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.FreeSoul)
            {
                if (client.Equipment.TotalPerfectionLevel >= 25) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 95) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 130) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 195) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 279) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.InvisibleArrow)
            {
                if (client.Equipment.TotalPerfectionLevel >= 65) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 225) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 282) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.KillingFlash)
            {
                if (client.Equipment.TotalPerfectionLevel >= 55) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 165) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 294) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.LightofStamina)
            {
                if (client.Equipment.TotalPerfectionLevel >= 1) Chance += 5;
            }
            if (effect == Game.Enums.PerfectionEffect.LuckyStrike)
            {
                if (client.Equipment.TotalPerfectionLevel >= 15) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 115) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 215) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 314) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 320) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.MirrorofSin)
            {
                if (client.Equipment.TotalPerfectionLevel >= 60) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 190) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 291) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.ShieldBreak)
            {
                if (client.Equipment.TotalPerfectionLevel >= 155) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 255) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 297) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 312) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 322) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.StraightLife)
            {
                if (client.Equipment.TotalPerfectionLevel >= 30) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 125) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 210) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 265) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 303) Chance += 1;
            }
            if (effect == Game.Enums.PerfectionEffect.Stricklock)
            {
                if (client.Equipment.TotalPerfectionLevel >= 308) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 321) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 175) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 105) Chance += 1;
                if (client.Equipment.TotalPerfectionLevel >= 20) Chance += 1;
            }
            return Chance;
        }
        public void SendEffect(Game.Entity attacker, Game.Entity attacked, Game.Enums.PerfectionEffect effect)
        {
            if (attacker == null || attacked == null) return;
            var proto = new MsgRefineEffectProto();
            proto.AttackerUID = attacker.UID;
            if (attacked.EntityFlag == Game.EntityFlag.Player)
                proto.AttackedUID = attacked.UID;
            else if (attacked.EntityFlag == Game.EntityFlag.Monster && attacked != null && attacked.UID != 0)
            {
                proto.AttackedUID = attacked.UID;
            }
            proto.Effect = (uint)effect;
            var proto2 = Kernel.FinalizeProtoBuf(proto, 3254);
            attacker.Owner.SendScreen(proto2, true);
            if (effect == Game.Enums.PerfectionEffect.DrainingTouch)
            {
                attacker.Hitpoints = attacker.MaxHitpoints;
            }
            if (effect == Game.Enums.PerfectionEffect.KillingFlash)
            {
                attacker.AddFlag(Network.GamePackets.Update.Flags.XPList);
            }
            if (effect == Game.Enums.PerfectionEffect.DivineGuard)
            {
                attacker.AddFlag3(Network.GamePackets.Update.Flags3.DivineGuard);
                attacker.Defence = (ushort)(((attacker.Defence) * 300) / 100);
                attacker.DivineGuardStamp = Time32.Now;
            }
            if (effect == Game.Enums.PerfectionEffect.ShieldBreak)
            {
                attacker.AddFlag3(Network.GamePackets.Update.Flags3.ShieldBreak);
                attacker.ShieldBreakStamp = Time32.Now;
                attacker.Block += (ushort)(((attacker.Block) * 20) / 10);
            }
            if (effect == Game.Enums.PerfectionEffect.StraightLife)
            {
                attacker.BringToLife();
            }
            if (effect == Game.Enums.PerfectionEffect.LightofStamina && attacked.EntityFlag == Game.EntityFlag.Player && attacker.Owner.Equipment.TotalPerfectionLevel > attacked.Owner.Equipment.TotalPerfectionLevel || effect == Game.Enums.PerfectionEffect.LightofStamina && attacked.EntityFlag == Game.EntityFlag.Monster) attacker.Stamina = 100;
        }

        public void GenerateEffectandChance(Client.GameClient client, ref Game.Enums.PerfectionEffect effect, ref byte chance)
        {
            effect = (Game.Enums.PerfectionEffect)Kernel.Random.Next(3, 14);
            chance = GetChance(client, effect);
            while (chance == 255)
            {
                effect = (Game.Enums.PerfectionEffect)Kernel.Random.Next(3, 14);
                chance = GetChance(client, effect);
            }
        }
        public void HandleStrickeLock(Game.Entity client, ref byte chance)
        {
            chance = GetChance(client.Owner, Game.Enums.PerfectionEffect.Stricklock);
        }
        public void HandleFreeSoul(Game.Entity client, ref byte chance)
        {
            chance = GetChance(client.Owner, Game.Enums.PerfectionEffect.FreeSoul);
        }
        public void HandleStraightLife(Game.Entity client, ref byte chance)
        {
            chance = GetChance(client.Owner, Game.Enums.PerfectionEffect.StraightLife);
        }
        public void HandleBloodSpawn(Game.Entity client, ref byte chance)
        {
            chance = GetChance(client.Owner, Game.Enums.PerfectionEffect.BloodSpawn);
        }
        public void HandleMirror(Game.Entity client, ref byte chance)
        {
            chance = GetChance(client.Owner, Game.Enums.PerfectionEffect.MirrorofSin);
        }
        public void HandleLuckyStrike(Game.Entity client, ref byte chance)
        {
            chance = GetChance(client.Owner, Game.Enums.PerfectionEffect.LuckyStrike);
        }
    }
}