using System;
using System.IO;
using System.Linq;
using System.Text;
using Nyx.Server.Game;
using Nyx.Server.Client;
using Nyx.Server.Database;
using Nyx.Server.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Nyx.Server.Network.GamePackets
{
    public class Auction
    {
        private IDisposable Subscribe;
        public Auction()
        {
            Subscribe = World.Subscribe(Work, 60000);
        }
        public void Work(int Time)
        {
            foreach (var item in Kernel.AuctionItems.Values)
            {
                if (DateTime.Now > item.AuctionInfo.EndTime)
                {
                    #region AuctionDone
                    if (item.AuctionInfo.BidderUID != 0)
                    {
                        GameClient Bidder;
                        Kernel.GamePool.TryGetValue(item.AuctionInfo.BidderUID, out Bidder);
                        if (Bidder != null)
                        {
                            Kernel.AuctionItems.Remove(item.UID);
                            item.UID = ConquerItem.ItemUID.Next;
                            item.Mode = Enums.ItemMode.Default;
                            Bidder.Inventory.Add(item, Enums.ItemUse.CreateAndAdd);
                            if (item.Purification.PurificationItemID > 0)
                            {
                                item.Purification.ItemUID = item.UID;
                                ItemAddingTable.AddPurification(item.Purification);
                            }
                            if (item.ExtraEffect.EffectID > 0)
                            {
                                item.ExtraEffect.ItemUID = item.UID;
                                ItemAddingTable.AddExtraEffect(item.ExtraEffect);
                            }
                            item.SendExtras(Bidder);
                            Bidder.Send(item);
                        }
                        else
                        {
                            Kernel.AuctionItems.Remove(item.UID);
                            ConquerItem Item2 = item;
                            Item2.UID = ConquerItem.ItemUID.Next;
                            Item2.Mode = Enums.ItemMode.Default;
                            AuctionBase.AddItem(ref Item2, Item2.AuctionInfo.BidderUID);
                            if (Item2.Purification.PurificationItemID > 0)
                            {
                                Item2.Purification.ItemUID = Item2.UID;
                                ItemAddingTable.AddPurification(Item2.Purification);
                            }
                            if (Item2.ExtraEffect.EffectID > 0)
                            {
                                Item2.ExtraEffect.ItemUID = Item2.UID;
                                ItemAddingTable.AddExtraEffect(item.ExtraEffect);
                            }
                        }
                        GameClient Seller;
                        Kernel.GamePool.TryGetValue(item.AuctionInfo.SellerUID, out Seller);
                        if (Seller != null)
                        {
                            if (item.AuctionInfo.MoneyType == 2)
                            {
                                Seller.Entity.ConquerPoints += item.AuctionInfo.BasePrice;
                            }
                            else
                            {
                                Seller.Entity.Money += item.AuctionInfo.BasePrice;
                            }
                        }
                        else if (item.AuctionInfo.MoneyType == 2)
                        {
                            EntityTable.UpdateCps(item.AuctionInfo.SellerUID, item.AuctionInfo.BasePrice);
                        }
                        else
                        {
                            EntityTable.UpdateMoney(item.AuctionInfo.SellerUID, item.AuctionInfo.BasePrice);
                        }
                        item.AuctionInfo = null;
                    }
                    else
                    {
                        GameClient Seller;
                        Kernel.GamePool.TryGetValue(item.AuctionInfo.SellerUID, out Seller);
                        if (Seller != null)
                        {
                            Kernel.AuctionItems.Remove(item.UID);
                            item.UID = ConquerItem.ItemUID.Next;
                            item.Mode = Enums.ItemMode.Default;
                            Seller.Inventory.Add(item, Enums.ItemUse.CreateAndAdd);
                            if (item.Purification.PurificationItemID > 0)
                            {
                                item.Purification.ItemUID = item.UID;
                                ItemAddingTable.AddPurification(item.Purification);
                            }
                            if (item.ExtraEffect.EffectID > 0)
                            {
                                item.ExtraEffect.ItemUID = item.UID;
                                ItemAddingTable.AddExtraEffect(item.ExtraEffect);
                            }
                            item.SendExtras(Seller);
                            Seller.Send(item);
                        }
                        else
                        {
                            Kernel.AuctionItems.Remove(item.UID);
                            ConquerItem Item2 = item;
                            Item2.UID = ConquerItem.ItemUID.Next;
                            Item2.Mode = Enums.ItemMode.Default;
                            AuctionBase.AddItem(ref Item2, Item2.AuctionInfo.SellerUID);
                            if (Item2.Purification.PurificationItemID > 0)
                            {
                                Item2.Purification.ItemUID = Item2.UID;
                                ItemAddingTable.AddPurification(Item2.Purification);
                            }
                            if (Item2.ExtraEffect.EffectID > 0)
                            {
                                Item2.ExtraEffect.ItemUID = Item2.UID;
                                ItemAddingTable.AddExtraEffect(Item2.ExtraEffect);
                            }
                        }
                        item.AuctionInfo = null;
                    }
                    #endregion
                }
                if (item.AuctionInfo != null)
                {
                    if (DateTime.Now < item.AuctionInfo.EndTime)
                    {
                        #region UpdateTime
                        if (item.AuctionInfo.Time == 0)
                        {
                            if ((item.AuctionInfo.EndTime - DateTime.Now).TotalHours < 12)
                            {
                                item.AuctionInfo.Time = 1;
                            }
                        }
                        if (item.AuctionInfo.Time == 1)
                        {
                            if ((item.AuctionInfo.EndTime - DateTime.Now).TotalHours < 2)
                            {
                                item.AuctionInfo.Time = 2;
                            }
                        }
                        if (item.AuctionInfo.Time == 2)
                        {
                            if ((item.AuctionInfo.EndTime - DateTime.Now).TotalMinutes < 30)
                            {
                                item.AuctionInfo.Time = 3;
                            }
                        }
                        #endregion
                    }
                }
            }
        }
    }
    public class AuctionBase
    {
        public static void Save()
        {
            string SQL = "DELETE FROM Auction";
            using (var conn = DataHolder.MySqlConnection)
            {
                conn.Open();
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = SQL;
                    cmd.ExecuteNonQuery();
                }
            }
            foreach (var Item in Kernel.AuctionItems.Values)
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("Auction"))
                    cmd.Insert("ID", Item.ID)
                        .Insert("UID", Item.UID)
                        .Insert("Plus", Item.Plus)
                        .Insert("Bless", Item.Bless)
                        .Insert("Enchant", Item.Enchant)
                        .Insert("SocketOne", (byte)Item.SocketOne)
                        .Insert("SocketTwo", (byte)Item.SocketTwo)
                        .Insert("Durability", Item.Durability)
                        .Insert("MaximDurability", Item.MaximDurability)
                        .Insert("SocketProgress", Item.SocketProgress)
                        .Insert("PlusProgress", Item.PlusProgress)
                        .Insert("Effect", (ushort)Item.Effect)
                        .Insert("Bound", Item.Bound)
                        .Insert("Locked", Item.Lock)
                        .Insert("UnlockEnd", Item.UnlockEnd.Ticks)
                        .Insert("Suspicious", Item.Suspicious)
                        .Insert("SuspiciousStart", Item.SuspiciousStart.Ticks)
                        .Insert("NextSteedColor", 0)
                        .Insert("Color", (ushort)Item.Color)
                        .Insert("Position", Item.Position)
                        .Insert("StackSize", Item.StackSize)
                        .Insert("RefineryItem", Item.RefineItem)
                        .Insert("RefineryTime", Item.RefineryTime.Ticks)
                        .Insert("EndTime", Item.AuctionInfo.EndTime.Ticks)
                        .Insert("BidderName", Item.AuctionInfo.BidderName)
                        .Insert("BasePrice", Item.AuctionInfo.BasePrice)
                        .Insert("BidderUID", Item.AuctionInfo.BidderUID)
                        .Insert("FixedPrice", Item.AuctionInfo.FixedPrice)
                        .Insert("MoneyType", Item.AuctionInfo.MoneyType)
                        .Insert("SellerName", Item.AuctionInfo.SellerName)
                        .Insert("SellerUID", Item.AuctionInfo.SellerUID)
                        .Insert("Time", Item.AuctionInfo.Time)
                        .Execute();
            }
        }
        public static void Load()
        {
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("Auction"))
            using (var reader = new MySqlReader(cmd))
            {
                while (reader.Read())
                {
                    var item = deserialze(reader);
                    ItemAddingTable.GetAddingsForItem(item);
                    item.Mode = Enums.ItemMode.Auction;
                    Kernel.AuctionItems.Add(item.UID, item);
                }
            }
        }
        private static ConquerItem deserialze(MySqlReader reader)
        {
            ConquerItem item = new ConquerItem(true);
            item.ID = reader.ReadUInt32("Id");
            item.UID = reader.ReadUInt32("Uid");
            item.Durability = reader.ReadUInt16("Durability");
            item.MaximDurability = reader.ReadUInt16("MaximDurability");
            item.Position = reader.ReadUInt16("Position");
            item.Agate = reader.ReadString("Agate");
            item.SocketProgress = reader.ReadUInt32("SocketProgress");
            item.PlusProgress = reader.ReadUInt32("PlusProgress");
            item.SocketOne = (Enums.Gem)reader.ReadUInt16("SocketOne");
            item.SocketTwo = (Enums.Gem)reader.ReadUInt16("SocketTwo");
            item.Effect = (Enums.ItemEffect)reader.ReadUInt16("Effect");
            item.Mode = Enums.ItemMode.Auction;
            item.Plus = reader.ReadByte("Plus");
            item.Bless = reader.ReadByte("Bless");
            item.Bound = reader.ReadBoolean("Bound");
            item.Enchant = reader.ReadByte("Enchant");
            item.Lock = reader.ReadByte("Locked");
            item.UnlockEnd = DateTime.FromBinary(reader.ReadInt64("UnlockEnd"));
            item.Suspicious = reader.ReadBoolean("Suspicious");
            item.SuspiciousStart = DateTime.FromBinary(reader.ReadInt64("SuspiciousStart"));
            item.Color = (Enums.Color)reader.ReadUInt32("Color");
            item.Warehouse = reader.ReadUInt16("Warehouse");
            item.StackSize = reader.ReadUInt16("StackSize");
            item.RefineItem = reader.ReadUInt32("RefineryItem");

            item.AuctionInfo = new MsgAuction(true);
            item.AuctionInfo.BasePrice = reader.ReadUInt32("BasePrice");
            item.AuctionInfo.BidderName = reader.ReadString("BidderName");
            item.AuctionInfo.BidderUID = reader.ReadUInt32("BidderUID");
            item.AuctionInfo.EndTime = DateTime.FromBinary(reader.ReadInt64("EndTime"));
            item.AuctionInfo.FixedPrice = reader.ReadUInt32("FixedPrice");
            item.AuctionInfo.ItemUID = item.UID;
            item.AuctionInfo.MoneyType = reader.ReadByte("MoneyType");
            item.AuctionInfo.SellerName = reader.ReadString("SellerName");
            item.AuctionInfo.SellerUID = reader.ReadUInt32("SellerUID");
            item.AuctionInfo.Time = reader.ReadByte("Time");

            if (item.ID == 300000)
            {
                uint NextSteedColor = reader.ReadUInt32("NextSteedColor");
                item.NextGreen = (byte)(NextSteedColor & 0xFF);
                item.NextBlue = (byte)((NextSteedColor >> 8) & 0xFF);
                item.NextRed = (byte)((NextSteedColor >> 16) & 0xFF);
            }

            Int64 rTime = reader.ReadInt64("RefineryTime");
            if (item.RefineItem > 0 && rTime != 0)
            {
                item.RefineryTime = DateTime.FromBinary(rTime);
                if (DateTime.Now > item.RefineryTime)
                {
                    item.RefineryTime = new DateTime(0);
                    item.RefineItem = 0;
                }
            }
            if (item.Lock == 2)
                if (DateTime.Now >= item.UnlockEnd)
                    item.Lock = 0;
            return item;
        }
        public static void AddItem(ref ConquerItem Item, uint UID)
        {
            try
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT).Insert("items"))
                    cmd.Insert("ID", Item.ID)
                        .Insert("UID", Item.UID)
                        .Insert("Plus", Item.Plus)
                        .Insert("Bless", Item.Bless)
                        .Insert("Enchant", Item.Enchant)
                        .Insert("SocketOne", (byte)Item.SocketOne)
                        .Insert("SocketTwo", (byte)Item.SocketTwo)
                        .Insert("Durability", Item.Durability)
                        .Insert("MaximDurability", Item.MaximDurability)
                        .Insert("SocketProgress", Item.SocketProgress)
                        .Insert("PlusProgress", Item.PlusProgress)
                        .Insert("Effect", (ushort)Item.Effect)
                        .Insert("Bound", Item.Bound)
                        .Insert("Locked", Item.Lock)
                        .Insert("UnlockEnd", Item.UnlockEnd.Ticks)
                        .Insert("Suspicious", Item.Suspicious)
                        .Insert("SuspiciousStart", Item.SuspiciousStart.Ticks)
                        .Insert("NextSteedColor", 0)
                        .Insert("Color", (ushort)Item.Color)
                        .Insert("Position", Item.Position)
                        .Insert("StackSize", Item.StackSize)
                        .Insert("RefineryItem", Item.RefineItem)
                        .Insert("RefineryTime", Item.RefineryTime.Ticks)
                        .Insert("EntityID", UID)
                        .Execute();
            }
            catch (Exception)
            {
                ConquerItemTable.DeleteItem(Item.UID);
                AddItem(ref Item, UID);
            }
        }
    }
    public class MsgAuction : Writer, IPacket
    {
        byte[] Packet;
        public enum Mode : byte
        {
            NewListing,
            ListingCancel,
            ListingBid,
            ListingBuyout
        }
        public MsgAuction(bool Create)
        {
            if (Create)
            {
                Packet = new byte[46];
                Write(38, 0, Packet);
                Write((ushort)1320, 2, Packet);
                Write(Time32.Now.AllMilliseconds(), 4, Packet);
            }
        }
        public Mode Action
        {
            get { return (Mode)Packet[8]; }
            set { Write((byte)value, 8, Packet); }
        }
        public uint ItemUID
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set { Write(value, 12, Packet); }
        }
        public byte MoneyType
        {
            get { return Packet[16]; }
            set { Write(value, 16, Packet); }
        }
        public uint BasePrice
        {
            get { return BitConverter.ToUInt32(Packet, 20); }
            set { Write(value, 20, Packet); }
        }
        public uint FixedPrice
        {
            get { return BitConverter.ToUInt32(Packet, 24); }
            set { Write(value, 24, Packet); }
        }
        public byte Time
        {
            get { return Packet[28]; }
            set { Write(value, 28, Packet); }
        }
        public int Margin
        {
            get { return Time == 12 ? 500 : Time == 24 ? 1100 : Time >= 48 ? 2250 : 1000000; }
        }
        public MsgAuction Confirm(bool Value)
        {
            Write((bool)Value, 32, Packet);
            return this;
        }
        public ConquerItem item;
        public DateTime EndTime;
        public int Times = 0;
        public uint SellerUID = 0;
        public uint BidderUID = 0;
        public string BidderName = "";
        public string SellerName = "";
        public byte[] Encode()
        {
            return Packet;
        }
        public void Send(GameClient client)
        {
            client.Send(Packet);
        }
        public void Deserialize(byte[] packet)
        {
            Packet = packet;
        }
        public static void Process(byte[] packet, GameClient client)
        {
            MsgAuction auc = new MsgAuction(true);
            auc.Deserialize(packet);
            switch (auc.Action)
            {
                #region NewListing
                case Mode.NewListing:
                    {
                        client.Inventory.TryGetItem(auc.ItemUID, out auc.item);
                        if (auc.item != null)
                        {
                            uint Margin = 0;
                            uint time = 0;
                            if (auc.Time == 12)
                            {
                                Margin = 500;
                                time = auc.Time;
                                auc.Time = 1;
                            }
                            if (auc.Time == 24)
                            {
                                Margin = 1100;
                                time = auc.Time;
                                auc.Time = 0;
                            }
                            if (auc.Time == 48)
                            {
                                Margin = 2250;
                                time = auc.Time;
                                auc.Time = 0;
                            }
                            if (auc.MoneyType == 1)
                            {
                                if (client.Entity.Money < Margin) return;
                                client.Entity.Money -= Margin;
                            }
                            else if (auc.MoneyType == 2)
                            {
                                if (client.Entity.ConquerPoints < Margin) return;
                                client.Entity.ConquerPoints -= Margin;
                            }
                            else return;
                            client.Inventory.Remove(auc.item, Enums.ItemUse.Remove);
                            auc.item.AuctionInfo = auc;
                            auc.item.AuctionInfo.EndTime = DateTime.Now.AddHours(time);
                            auc.item.AuctionInfo.SellerName = client.Entity.Name;
                            auc.item.AuctionInfo.SellerUID = client.Entity.UID;
                            auc.item.AuctionInfo.BidderName = "";
                            auc.item.AuctionInfo.BidderUID = 0;
                            auc.item.Mode = Enums.ItemMode.Auction;
                            Kernel.AuctionItems.Add(auc.item.UID, auc.item);
                            client.Send(auc.item);
                            client.Send(MsgAuctionQuery.ToArray(auc.item, 0, client));
                        }
                        break;
                    }
                #endregion
                #region ListingCancel
                case Mode.ListingCancel:
                    {
                        packet[32] = 1;
                        Kernel.AuctionItems.TryGetValue(auc.ItemUID, out auc.item);
                        if (auc.item != null)
                        {
                            if (auc.item.AuctionInfo.BidderUID != 0)
                            {
                                GameClient OldBidder;
                                Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.BidderUID, out OldBidder);
                                if (OldBidder != null)
                                {
                                    if (auc.item.AuctionInfo.MoneyType == 2)
                                    {
                                        OldBidder.Entity.ConquerPoints += auc.item.AuctionInfo.BasePrice;
                                        //MsgMailList.AddPrize(OldBidder, "Auction", "Bidding Failed", "Your Bid Failed", 0, auc.item.AuctionInfo.BasePrice, 0, 0, null);
                                    }
                                    else
                                    {
                                        OldBidder.Entity.Money += auc.item.AuctionInfo.BasePrice;
                                        //MsgMailList.AddPrize(OldBidder, "Auction", "Bidding Failed", "Your Bid Failed", auc.item.AuctionInfo.BasePrice, 0, 0, 0, null);
                                    }
                                }
                                else if (auc.item.AuctionInfo.MoneyType == 2)
                                {
                                    EntityTable.UpdateCps(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                }
                                else
                                {
                                    EntityTable.UpdateMoney(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                }
                            }
                            auc.item.UID = ConquerItem.ItemUID.Next;
                            auc.item.AuctionInfo = null;
                            client.Inventory.Add(auc.item, Enums.ItemUse.CreateAndAdd);
                            if (auc.item.Purification.PurificationItemID > 0)
                            {
                                auc.item.Purification.ItemUID = auc.item.UID;
                                ItemAddingTable.AddPurification(auc.item.Purification);
                            }
                            if (auc.item.ExtraEffect.EffectID > 0)
                            {
                                auc.item.ExtraEffect.ItemUID = auc.item.UID;
                                ItemAddingTable.AddExtraEffect(auc.item.ExtraEffect);
                            }
                            auc.item.SendExtras(client);
                            Kernel.AuctionItems.Remove(auc.ItemUID);
                            client.Send(packet);
                        }
                        break;
                    }
                #endregion
                #region ListingBid
                case Mode.ListingBid:
                    {
                        Kernel.AuctionItems.TryGetValue(auc.ItemUID, out auc.item);
                        if (auc.item != null)
                        {
                            if (auc.item.AuctionInfo.MoneyType == 2)
                            {
                                if (client.Entity.ConquerPoints >= auc.BasePrice)
                                {
                                    if (auc.item.AuctionInfo.BidderUID != 0)
                                    {
                                        GameClient OldBidder;
                                        Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.BidderUID, out OldBidder);
                                        if (OldBidder != null)
                                        {
                                            if (auc.item.AuctionInfo.MoneyType == 2)
                                            {
                                                OldBidder.Entity.ConquerPoints += auc.item.AuctionInfo.BasePrice;
                                                //MsgMailList.AddPrize(OldBidder, "Auction", "Bidding Failed", "Your Bid Failed", 0, auc.item.AuctionInfo.BasePrice, 0, 0, null);
                                            }
                                        }
                                        else if (auc.item.AuctionInfo.MoneyType == 2)
                                        {
                                            EntityTable.UpdateCps(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                            //EntityTable.UpdateCps(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                        }
                                    }
                                    if (auc.BasePrice < auc.item.AuctionInfo.FixedPrice)
                                    {
                                        auc.item.AuctionInfo.BidderName = client.Entity.Name;
                                        auc.item.AuctionInfo.BidderUID = client.Entity.UID;
                                        auc.item.AuctionInfo.BasePrice = auc.BasePrice;
                                        client.Entity.ConquerPoints -= auc.BasePrice;
                                        client.Send(MsgAuctionItem.ToArray(auc.item, 1, client));
                                    }
                                    else
                                    {
                                        client.Entity.ConquerPoints -= auc.BasePrice;
                                        client.Send(MsgAuctionItem.ToArray(auc.item, 2, client));
                                        if (auc.item.AuctionInfo.SellerUID != 0)
                                        {
                                            GameClient Seller;
                                            Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.SellerUID, out Seller);
                                            if (Seller != null)
                                            {
                                                if (auc.item.AuctionInfo.MoneyType == 2)
                                                {
                                                    Seller.Entity.ConquerPoints += auc.BasePrice;
                                                    //MsgMailList.AddPrize(Seller, "Auction", "Auctioning Successed", "Your Successed to sell the item", 0, auc.BasePrice, 0, 0, null);
                                                }
                                            }
                                            else if (auc.item.AuctionInfo.MoneyType == 2)
                                            {
                                                EntityTable.UpdateCps(auc.item.AuctionInfo.SellerUID, auc.BasePrice);
                                            }
                                        }
                                        auc.item.UID = ConquerItem.ItemUID.Next;
                                        auc.item.Mode = Enums.ItemMode.Default;
                                        auc.item.AuctionInfo = null;
                                        client.Inventory.Add(auc.item, Game.Enums.ItemUse.CreateAndAdd);
                                        if (auc.item.Purification.PurificationItemID > 0)
                                        {
                                            auc.item.Purification.ItemUID = auc.item.UID;
                                            ItemAddingTable.AddPurification(auc.item.Purification);
                                        }
                                        if (auc.item.ExtraEffect.EffectID > 0)
                                        {
                                            auc.item.ExtraEffect.ItemUID = auc.item.UID;
                                            ItemAddingTable.AddExtraEffect(auc.item.ExtraEffect);
                                        }
                                        auc.item.SendExtras(client);
                                        Kernel.AuctionItems.Remove(auc.ItemUID);
                                        //MsgMailList.AddPrize(client, "Auction", "Bidding Finished", "Your Bid Successed", 0, 0, 0, 0, auc.item);
                                    }
                                }
                                else client.Send("You don't have cps to bidding");
                            }
                            else if (client.Entity.Money >= auc.BasePrice)
                            {
                                if (auc.item.AuctionInfo.BidderUID != 0)
                                {
                                    GameClient OldBidder;
                                    Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.BidderUID, out OldBidder);
                                    if (OldBidder != null)
                                    {
                                        if (auc.item.AuctionInfo.MoneyType == 1)
                                        {
                                            OldBidder.Entity.Money += auc.item.AuctionInfo.BasePrice;
                                            //MsgMailList.AddPrize(OldBidder, "Auction", "Bidding Failed", "Your Bid Failed", auc.item.AuctionInfo.BasePrice, 0, 0, 0, null);
                                        }
                                    }
                                    else if (auc.item.AuctionInfo.MoneyType == 1)
                                    {
                                        EntityTable.UpdateMoney(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                    }
                                }
                                if (auc.BasePrice < auc.item.AuctionInfo.FixedPrice)
                                {
                                    auc.item.AuctionInfo.BidderName = client.Entity.Name;
                                    auc.item.AuctionInfo.BidderUID = client.Entity.UID;
                                    auc.item.AuctionInfo.BasePrice = auc.BasePrice;
                                    client.Entity.Money -= auc.BasePrice;
                                    client.Send(MsgAuctionItem.ToArray(auc.item, 1, client));
                                }
                                else
                                {
                                    client.Entity.Money -= auc.BasePrice;
                                    client.Send(MsgAuctionItem.ToArray(auc.item, 2, client));
                                    if (auc.item.AuctionInfo.SellerUID != 0)
                                    {
                                        GameClient Seller;
                                        Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.SellerUID, out Seller);
                                        if (Seller != null)
                                        {
                                            if (auc.item.AuctionInfo.MoneyType == 1)
                                            {
                                                Seller.Entity.Money += auc.BasePrice;
                                                // MsgMailList.AddPrize(Seller, "Auction", "Auctioning Successed", "Your Successed to sell the item", auc.BasePrice, 0, 0, 0, null);
                                            }
                                        }
                                        else if (auc.item.AuctionInfo.MoneyType == 1)
                                        {
                                            EntityTable.UpdateMoney(auc.item.AuctionInfo.SellerUID, auc.BasePrice);
                                        }
                                    }
                                    auc.item.UID = ConquerItem.ItemUID.Next;
                                    auc.item.Mode = Enums.ItemMode.Default;
                                    auc.item.AuctionInfo = null;
                                    client.Inventory.Add(auc.item, Enums.ItemUse.CreateAndAdd);
                                    if (auc.item.Purification.PurificationItemID > 0)
                                    {
                                        auc.item.Purification.ItemUID = auc.item.UID;
                                        ItemAddingTable.AddPurification(auc.item.Purification);
                                    }
                                    if (auc.item.ExtraEffect.EffectID > 0)
                                    {
                                        auc.item.ExtraEffect.ItemUID = auc.item.UID;
                                        ItemAddingTable.AddExtraEffect(auc.item.ExtraEffect);
                                    }
                                    auc.item.SendExtras(client);
                                    Kernel.AuctionItems.Remove(auc.ItemUID);
                                    //MsgMailList.AddPrize(client, "Auction", "Bidding Finished", "Your Bid Successed", 0, 0, 0, 0, auc.item);
                                }
                            }
                            else client.Send("You don't have cps to bidding");
                        }
                        break;
                    }
                #endregion
                #region ListingBuyout
                case Mode.ListingBuyout:
                    {
                        Kernel.AuctionItems.TryGetValue(auc.ItemUID, out auc.item);
                        if (auc.item != null)
                        {
                            auc.FixedPrice = auc.item.AuctionInfo.FixedPrice;
                            if (auc.item.AuctionInfo.MoneyType == 2)
                            {
                                if (client.Entity.ConquerPoints >= auc.FixedPrice)
                                {
                                    if (auc.item.AuctionInfo.BidderUID != 0)
                                    {
                                        GameClient OldBidder;
                                        Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.BidderUID, out OldBidder);
                                        if (OldBidder != null)
                                        {
                                            if (auc.item.AuctionInfo.MoneyType == 2)
                                            {
                                                OldBidder.Entity.ConquerPoints += auc.item.AuctionInfo.BasePrice;
                                                //MsgMailList.AddPrize(OldBidder, "Auction", "Bidding Failed", "Your Failed to sell the item", 0, auc.item.AuctionInfo.BasePrice, 0, 0, null);
                                            }
                                        }
                                        else if (auc.item.AuctionInfo.MoneyType == 2)
                                        {
                                            EntityTable.UpdateCps(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                        }
                                    }
                                    if (auc.FixedPrice == auc.item.AuctionInfo.FixedPrice)
                                    {
                                        client.Entity.ConquerPoints -= auc.FixedPrice;
                                        client.Send(MsgAuctionItem.ToArray(auc.item, 2, client));
                                        if (auc.item.AuctionInfo.SellerUID != 0)
                                        {
                                            GameClient Seller;
                                            Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.SellerUID, out Seller);
                                            if (Seller != null)
                                            {
                                                if (auc.item.AuctionInfo.MoneyType == 2)
                                                {
                                                    Seller.Entity.ConquerPoints += auc.FixedPrice;
                                                    //MsgMailList.AddPrize(Seller, "Auction", "Auctioning Successed", "Your Successed to sell the item", 0, auc.FixedPrice, 0, 0, null);
                                                }
                                            }
                                            else if (auc.item.AuctionInfo.MoneyType == 2)
                                            {
                                                EntityTable.UpdateCps(auc.item.AuctionInfo.SellerUID, auc.FixedPrice);
                                            }
                                        }
                                        auc.item.UID = ConquerItem.ItemUID.Next;
                                        auc.item.Mode = Enums.ItemMode.Default;
                                        auc.item.AuctionInfo = null;
                                        client.Inventory.Add(auc.item, Enums.ItemUse.CreateAndAdd);
                                        if (auc.item.Purification.PurificationItemID > 0)
                                        {
                                            auc.item.Purification.ItemUID = auc.item.UID;
                                            ItemAddingTable.AddPurification(auc.item.Purification);
                                        }
                                        if (auc.item.ExtraEffect.EffectID > 0)
                                        {
                                            auc.item.ExtraEffect.ItemUID = auc.item.UID;
                                            ItemAddingTable.AddExtraEffect(auc.item.ExtraEffect);
                                        }
                                        auc.item.SendExtras(client);
                                        Kernel.AuctionItems.Remove(auc.ItemUID);
                                        //MsgMailList.AddPrize(client, "Auction", "Bidding Finished", "Your Bid Successed", 0, 0, 0, 0, auc.item);
                                    }
                                }
                                else client.Send("You don't have cps to buy this item");
                            }
                            else if (client.Entity.Money >= auc.FixedPrice)
                            {
                                if (auc.item.AuctionInfo.BidderUID != 0)
                                {
                                    GameClient OldBidder;
                                    Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.BidderUID, out OldBidder);
                                    if (OldBidder != null)
                                    {
                                        if (auc.item.AuctionInfo.MoneyType == 1)
                                        {
                                            OldBidder.Entity.Money += auc.item.AuctionInfo.BasePrice;
                                            //MsgMailList.AddPrize(OldBidder, "Auction", "Bidding Failed", "Your Failed to sell the item", auc.item.AuctionInfo.BasePrice, 0, 0, 0, null);
                                        }
                                    }
                                    else if (auc.item.AuctionInfo.MoneyType == 1)
                                    {
                                        EntityTable.UpdateMoney(auc.item.AuctionInfo.BidderUID, auc.item.AuctionInfo.BasePrice);
                                    }
                                }
                                if (auc.item.AuctionInfo.FixedPrice == auc.FixedPrice)
                                {
                                    client.Entity.Money -= auc.FixedPrice;
                                    client.Send(MsgAuctionItem.ToArray(auc.item, 2, client));
                                    if (auc.item.AuctionInfo.SellerUID != 0)
                                    {
                                        GameClient Seller;
                                        Kernel.GamePool.TryGetValue(auc.item.AuctionInfo.SellerUID, out Seller);
                                        if (Seller != null)
                                        {
                                            if (auc.item.AuctionInfo.MoneyType == 1)
                                            {
                                                Seller.Entity.Money += auc.FixedPrice;
                                                //MsgMailList.AddPrize(Seller, "Auction", "Auctioning Successed", "Your Successed to sell the item", auc.FixedPrice, 0, 0, 0, null);
                                            }
                                        }
                                        else if (auc.item.AuctionInfo.MoneyType == 1)
                                        {
                                            EntityTable.UpdateMoney(auc.item.AuctionInfo.SellerUID, auc.FixedPrice);
                                        }
                                    }
                                    auc.item.UID = ConquerItem.ItemUID.Next;
                                    auc.item.Mode = Enums.ItemMode.Default;
                                    auc.item.AuctionInfo = null;
                                    client.Inventory.Add(auc.item, Enums.ItemUse.CreateAndAdd);
                                    if (auc.item.Purification.PurificationItemID > 0)
                                    {
                                        auc.item.Purification.ItemUID = auc.item.UID;
                                        ItemAddingTable.AddPurification(auc.item.Purification);
                                    }
                                    if (auc.item.ExtraEffect.EffectID > 0)
                                    {
                                        auc.item.ExtraEffect.ItemUID = auc.item.UID;
                                        ItemAddingTable.AddExtraEffect(auc.item.ExtraEffect);
                                    }
                                    auc.item.SendExtras(client);
                                    Kernel.AuctionItems.Remove(auc.ItemUID);
                                    //MsgMailList.AddPrize(client, "Auction", "Bidding Finished", "Your Bid Successed", 0, 0, 0, 0, auc.item);
                                }
                                else client.Send("You don't have Money to buy this item");
                            }
                        }
                        break;
                    }
                #endregion
                default:  break;
            }
            client.Send(auc.Confirm(true));
        }
    }
    public unsafe class MsgAuctionItem : Writer, IPacket
    {
        byte[] Packet;
        public MsgAuctionItem(bool Create)
        {
            if (Create)
            {
                Packet = new byte[128];
                Write(120, 0, Packet);
                Write((ushort)1321, 2, Packet);
                Write(Time32.Now.AllMilliseconds(), 4, Packet);
            }
        }
        public ushort PageListings
        {
            set { Write(value, 46, Packet); }
        }
        public ushort TotalListings
        {
            set { Write(value, 48, Packet); }
        }
        public ushort QueryPage
        {
            get { return BitConverter.ToUInt16(Packet, 50); }
        }
        public byte[] Encode()
        {
            return Packet;
        }
        public void Send(GameClient client)
        {
            client.Send(Packet);
        }
        public void Deserialize(byte[] Pack)
        {
            Packet = Pack;
        }
        public static byte[] ToArray(ConquerItem item, uint type, GameClient client)
        {
            var Packet = new byte[60 + 68];
            Write((ushort)(Packet.Length - 8), 0, Packet);
            Write((ushort)1321, 2, Packet);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Packet);
            Write(type, 8, Packet);
            Write(1, 46, Packet);
            Write(Kernel.AuctionItems.Count, 48, Packet);
            int offset = 52;
            Write(item.UID, offset, Packet);
            item.SendExtras(client);
            offset += 8;
            Write(item.AuctionInfo.SellerUID, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.SellerName, offset, Packet);
            offset += 16;
            Write(item.AuctionInfo.MoneyType, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.BasePrice, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.FixedPrice, offset, Packet);
            offset += 4;
            Write(0, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.Time, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.BidderUID, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.BidderName, offset, Packet);
            offset += 16;
            return Packet;
        }
        public static void Process(byte[] packet, GameClient client)
        {
            client.Send(packet);
            foreach (var item in Kernel.AuctionItems.Values)
            {
                if (packet[12] == item.AuctionInfo.MoneyType)
                {
                    client.Send(item);
                    client.Send(ToArray(item, packet[8], client));
                }
                else if (packet[12] == item.AuctionInfo.MoneyType)
                {
                    client.Send(item);
                    client.Send(ToArray(item, packet[8], client));
                }
            }
        }
    }
    public class MsgAuctionQuery : Writer, IPacket
    {
        byte[] Packet;
        public enum Mode : byte
        {
            AuctionListing,
            ActiveBids
        }
        public MsgAuctionQuery(bool Create)
        {
            if (Create)
            {
                Packet = new Byte[96];
                Write(88, 0, Packet);
                Write((ushort)1322, 2, Packet);
                Write(Time32.Now.AllMilliseconds(), 4, Packet);
            }
        }
        public Mode Action
        {
            get { return (Mode)Packet[8]; }
            set { Write((byte)value, 8, Packet); }
        }
        public int ItemCount
        {
            get { return BitConverter.ToInt32(Packet, 12); }
            set { Write(value, 12, Packet); }
        }
        public byte[] Encode()
        {
            return Packet;
        }
        public void Send(GameClient client)
        {
            client.Send(Packet);
        }
        public void Deserialize(byte[] Pack)
        {
            Packet = Pack;
        }
        public static byte[] ToArray(ConquerItem item, uint type, GameClient client)
        {
            var Packet = new byte[8 + 20 + 68];
            Write((ushort)(Packet.Length - 8), 0, Packet);
            Write((ushort)1322, 2, Packet);
            Write(Time32.timeGetTime().GetHashCode(), 4, Packet);
            Write(type, 8, Packet);
            Write(1, 12, Packet);
            int offset = 20;
            Write(item.UID, offset, Packet);
            item.SendExtras(client);
            offset += 8;
            Write(item.AuctionInfo.SellerUID, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.SellerName, offset, Packet);
            offset += 16;
            Write(item.AuctionInfo.MoneyType, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.BasePrice, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.FixedPrice, offset, Packet);
            offset += 4;
            Write(0, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.Time, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.BidderUID, offset, Packet);
            offset += 4;
            Write(item.AuctionInfo.BidderName, offset, Packet);
            offset += 16;
            return Packet;
        }
        public static void Process(byte[] packet, GameClient client)
        {
            MsgAuctionQuery query = new MsgAuctionQuery(false);
            query.Deserialize(packet);
            switch (query.Action)
            {
                case Mode.AuctionListing:
                    {
                        foreach (var item in Kernel.AuctionItems.Values)
                        {
                            if (item.AuctionInfo.SellerUID == client.Entity.UID)
                            {
                                client.Send(item);
                                client.Send(ToArray(item, 0, client));
                            }
                        }
                        break;
                    }
                case Mode.ActiveBids:
                    {
                        foreach (var item in Kernel.AuctionItems.Values)
                        {
                            if (item.AuctionInfo.BidderUID == client.Entity.UID)
                            {
                                client.Send(item);
                                MsgAuction auc = new MsgAuction(true);
                                auc.Action = MsgAuction.Mode.ListingBid;
                                auc.ItemUID = item.UID;
                                auc.BasePrice = item.AuctionInfo.BasePrice;
                                client.Send(auc.Confirm(true));
                                item.SendExtras(client);
                            }
                        }
                        break;
                    }
                default:  break;
            }
        }
    }
}