using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Interfaces;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Game
{
    public class Lottery
    {

        public static void LotteryRewardMessage(Nyx.Server.Client.GameClient Hero, ConquerItem i)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            if ((Nyx.Server.Game.Enums.ItemQuality)(i.ID % 10) >= Nyx.Server.Game.Enums.ItemQuality.Elite)
            {
                str = "Elite ";
                if ((Nyx.Server.Game.Enums.ItemQuality)(i.ID % 10) >= Nyx.Server.Game.Enums.ItemQuality.Super)
                {
                    str = "Super ";
                }
                if (Nyx.Server.Database.ConquerItemInformation.BaseInformations[i.ID].Name.Contains("MoneyBag"))
                {
                    str = "";
                }
            }
            if (i.SocketOne > 0)
            {
                str2 = "1-Socket ";
                if (i.SocketTwo > 0)
                {
                    str2 = "2-Socket ";
                }
            }
            if (i.Plus > 0)
            {
                str3 = "(+" + i.Plus + ")";
            }
            string str4 = str + str2 + Nyx.Server.Database.ConquerItemInformation.BaseInformations[i.ID].Name + str3;
            string msg = string.Format("{0} won a {1} from the Lottery!", Hero.Entity.Name, str4);

            if ((Nyx.Server.Game.Enums.ItemQuality)(i.ID % 10) >= Nyx.Server.Game.Enums.ItemQuality.Elite)
            {
                foreach (var client in Kernel.GamePool.Values)
                client.Send(new Message(msg, System.Drawing.Color.Red, Nyx.Server.Network.GamePackets.Message.Talk));



            }
            else
            {
                Hero.Send(new Message(msg, System.Drawing.Color.White, Message.Talk));

            }
        }
        public static void GiveLotteryPrize(Nyx.Server.Client.GameClient client)
        {
            LotteryRewardMessage(client, client.Entity.LotteryPrize);
            Nyx.Server.Database.ConquerItemInformation Itemd = new Nyx.Server.Database.ConquerItemInformation(client.Entity.LotteryItemID, 0);
            //var Itemd = Nyx.Server.Database.ConquerItemInformation.BaseInformations[Nyx.Server.Entity.LotteryItemID];
            ConquerItem Item = new ConquerItem(true);
            Item.ID = client.Entity.LotteryItemID;

            Item.Plus = (byte)client.Entity.LotteryItemPlus;
            Item.Color = Game.Enums.Color.Blue;
            if (client.Entity.LotteryItemSoc1 > 0)
            {
                Item.SocketOne = Game.Enums.Gem.EmptySocket;
                client.Entity.LotteryItemSoc1 = 0;
            }
            if (client.Entity.LotteryItemSoc2 > 0)
            {
                Item.SocketTwo = Game.Enums.Gem.EmptySocket;
                client.Entity.LotteryItemSoc2 = 0;
            }
            Item.Durability = Item.MaximDurability = Itemd.BaseInformation.Durability;

            client.Inventory.Add(Item, Game.Enums.ItemUse.CreateAndAdd);

            
            client.Entity.LotteryItemID = 0;
            client.Entity.LotteryJadeAdd = 0;
        }
        public static void LuckyBox(uint npcID, Nyx.Server.Client.GameClient h, bool jade)
        {
            if (jade || h.Inventory.Contains(0xadb50, 3))
            {
                if (!jade)
                {
                    h.LotteryEntries += 1;
                    h.Activenes.SendSinglePacket(h, Activeness.Types.LotteryTask, h.LotteryEntries);
                }
                else
                {
                    h.Entity.LotteryJadeAdd++;
                }
                if ((npcID != 0) && !jade)
                {
                    _String packet = new _String(true);
                    packet.UID = npcID;
                    packet.TextsCount = 1;
                    packet.Type = _String.Effect;
                    packet.Texts.Add("lottery");
                    h.Send(packet);
                }
            tryagain:
                int rand = Kernel.Random.Next(Nyx.Server.Database.LotteryTable.LotteryItems.Count);
                var item = Nyx.Server.Database.LotteryTable.LotteryItems[rand];
                var Itemd = Nyx.Server.Database.ConquerItemInformation.BaseInformations[item.ID];
                if (Itemd == null || item == null)
                    goto tryagain;
                if (item.NPCID != npcID && Kernel.Rate(70))
                    goto tryagain;
                if (!(Network.PacketHandler.GetPositionFromID(item.ID) != Network.PacketHandler.Positions.Garment && Network.PacketHandler.GetPositionFromID(item.ID) != 0 && Kernel.Rate(99)))
                {
                    ConquerItem Item = new ConquerItem(true);
                    Item.ID = item.ID;
                    h.Entity.LotteryItemID = item.ID;
                    h.Entity.LotteryItemPlus = item.Plus;
                    h.Entity.LotteryItemColor = (byte)Nyx.Server.Game.Enums.Color.Blue;
                    Item.Plus = item.Plus;
                    Item.Color = Nyx.Server.Game.Enums.Color.Blue;
                    if (item.Sockets > 0)
                    {
                        Item.SocketOne = Nyx.Server.Game.Enums.Gem.EmptySocket;
                        h.Entity.LotteryItemSoc1 = 255;
                    }
                    if (item.Sockets > 1)
                    {
                        Item.SocketTwo = Nyx.Server.Game.Enums.Gem.EmptySocket;
                        h.Entity.LotteryItemSoc2 = 255;

                    }
                    Item.Durability = Item.MaximDurability = Itemd.Durability;
                    // h.Inventory.Add(Item, Game.Enums.ItemUse.Add);
                    //h.Entity.LotteryPrize = Item;
                    if ((Item != null))
                    {
                        if (!jade)
                        {
                            if (h.Inventory.Contains(0xadb50, 3))
                            {

                                h.Inventory.Remove(0xadb50, 3);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (h.Inventory.Contains(0xadb50, 1))
                            {

                                h.Inventory.Remove(0xadb50, 1);
                            }
                            else
                            {
                                return;
                            }
                        }

                        h.Entity.LotteryPrize = Item;
                        Nyx.Server.Network.GamePackets.Lottery lottery = new Nyx.Server.Network.GamePackets.Lottery
                        {
                            //Chances = (byte)(10 - h.LotteryEntries),
                            Color = Item.Color,
                            Plus = Item.Plus,
                            Prize = Item.ID,
                            SocketOne = (byte)Item.SocketOne,
                            SocketTwo = (byte)Item.SocketTwo,
                            AddJadeChances = h.Entity.LotteryJadeAdd,
                            Type = Nyx.Server.Network.GamePackets.Lottery.LotteryTypes.ShowGUI
                        };
                        h.Send((byte[])lottery);

                    }
                    else
                    {
                        // string msg = string.Format("Error generating lottery prize.", h.Entity.Name, h.Entity.Name);
                        // h.Send(new Message(msg, System.Drawing.Color.White, Message.Talk));

                    }
                }
                else
                    goto tryagain;
            }
            else
            {
                string msg = string.Format("You need 3 Small Lottery Tickets to try at the lottery!", h.Entity.Name, h.Entity.Name);
                h.Send(new Message(msg, System.Drawing.Color.White, Message.Talk));

            }
        }
        public static void Handle(byte[] Data, Nyx.Server.Client.GameClient Client)
        {

            Nyx.Server.Network.GamePackets.Lottery lottery = new Nyx.Server.Network.GamePackets.Lottery(Data);
            if (Client != null)
            {
                switch (lottery.Type)
                {
                    case Nyx.Server.Network.GamePackets.Lottery.LotteryTypes.Accept:
                        GiveLotteryPrize(Client);
                        break;

                    case Nyx.Server.Network.GamePackets.Lottery.LotteryTypes.AddJade:
                        if ((Client.Entity.LotteryJadeAdd < 2) && Client.Inventory.Contains(0xadb50, 1))
                        {
                            LuckyBox(0, Client, true);
                        }
                        break;

                    case Nyx.Server.Network.GamePackets.Lottery.LotteryTypes.Continue:
                        LuckyBox(0, Client, false);
                        break;
                }
            }
        }
    }
}
