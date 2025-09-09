using System;
using System.IO;
using System.Linq;
using System.Text;
using Nyx.Server.Game;
using Nyx.Server.Client;
using Nyx.Server.Network;
using Nyx.Server.Database;
using System.Collections.Generic;

namespace Nyx.Server.Network.GamePackets
{
    public class Mailbox : Writer, Interfaces.IPacket
    {
        byte[] Packet;
        ushort Offset = 16;
        public Mailbox()
        {
            Packet = new byte[20];
            Write((ushort)(Packet.Length - 8), 0, Packet);
            Write(1046, 2, Packet);
        }
        public uint Count
        {
            get { return BitConverter.ToUInt32(Packet, 4); }
            set {Writer.Write(value, 4, Packet); }
        }
        public uint Page
        {
            get { return BitConverter.ToUInt32(Packet, 8); }
            set {Writer.Write(value, 8, Packet); }
        }
        public uint Unknown
        {
            get { return BitConverter.ToUInt32(Packet, 12); }
            set {Writer.Write(value, 12, Packet); }
        }
        public class PrizeInfo
        {
            public void WriteItem(BinaryWriter writer)
            {
                writer.Write(ID);
                writer.Write(Time);
                writer.Write(Sender);
                writer.Write(Subject);
                writer.Write(Message);
                writer.Write(goldprize);
                writer.Write(cpsprize);
                writer.Write(Attackment);
                if (ItemPrize != null)
                    writer.Write(ItemPrize.UID);
                else
                    writer.Write(0);
            }
            public PrizeInfo ReadItem(BinaryReader reader)
            {
                ID = reader.ReadUInt32();//4
                Time = reader.ReadUInt32();//8
                Sender = reader.ReadString();//10
                Subject = reader.ReadString();//12
                Message = reader.ReadString();//14
                goldprize = reader.ReadUInt32();//18
                cpsprize = reader.ReadUInt32();//22  
                Attackment = reader.ReadUInt32();//22
                ItemPrize = Database.ConquerItemTable.LoadItem(reader.ReadUInt32());
                Database.ItemAddingTable.GetAddingsForItem(ItemPrize);
                return this;
            }
            public ConquerItem ItemPrize;
            public string Subject, Sender, Message;
            public uint ID, Time, cpsprize, Attackment, goldprize;
        }
        Random R = new Random();
        List<PrizeInfo> List = new List<PrizeInfo>();
        public void Check(GameClient client, uint page)
        {
            List<Mailbox.PrizeInfo> prizes = client.Prizes.Values.ToList();
            List.Clear();
            for (int i = (int)page; i < page + 7; i++)
            {
                if (i < prizes.Count)
                {
                    List.Add(prizes[i]);
                }
            }
            if (List.Count > 0)
            {
                Packet = new byte[20 + ((92 + 32) * List.Count)];
                Write((ushort)(Packet.Length - 8), 0, Packet);
                Write(1046, 2, Packet);
                Count = (uint)List.Count;
                Page = page;
                Unknown = (uint)prizes.Count;
                for (int i = 0; i < List.Count; i++)
                    Append(List[i], client);
            }
        }
        public void Append(PrizeInfo prize, GameClient C)
        {
            Write(prize.ID, Offset, Packet);//uid 
            Offset += 4;
            Writer.Write(prize.Sender, Offset, Packet);//sender
            Offset += 32;
            Writer.Write(prize.Subject, Offset, Packet);//Subject
            Offset += 64;
            Write(prize.goldprize, Offset, Packet);//attachment
            Offset += 4;
            Write(prize.cpsprize, Offset, Packet);//attachment
            Offset += 4;
            Write(prize.Time, Offset, Packet);//Time
            Offset += 4;
            if (prize.ItemPrize != null)
            {
                Write(prize.ItemPrize.UID, Offset, Packet);//image  
                prize.ItemPrize.Mode = Enums.ItemMode.ItemInbox;
                prize.ItemPrize.Send(C);
            }
            Offset += 4;
            Write(prize.Attackment, Offset, Packet);//Attackment
            Offset += 4;
        }
        public static void SendMailbox(GameClient client, bool On)
        {
            byte[] test = new byte[12 + 8];
            Writer.Write((ushort)(test.Length - 8), 0, test);
            Writer.Write(1047, 2, test);
            if (On)
                Writer.Write(3, 4, test);
            else
                Writer.Write(2, 4, test);
            client.Send(test);
        }
        public static void Add(GameClient client, string Sender = "Support", string Subject = "Prize!", string Message = "You have a prize to claim.", uint money = 0, uint cps = 0, uint attachment = 0, ConquerItem item = null)
        {
            int id = 0;
            id = Kernel.Random.Next(0, int.MaxValue);
            if (!client.Prizes.ContainsKey((uint)id))
            {
                PrizeInfo prize = new PrizeInfo();
                prize.ID = (uint)id;
                prize.Sender = Sender;
                prize.Subject = Subject;
                prize.Message = Message;
                prize.goldprize = money;
                prize.cpsprize = cps;
                prize.Time = 600;
                if (attachment == 0 && item != null)
                    attachment += 1;
                prize.ItemPrize = item;
                prize.Attackment = attachment;
                client.Prizes.Add(prize.ID, prize);
                if (client.Prizes.Count > 0)
                    SendMailbox(client, true);
            }
        }
        public byte[] Encode()
        {
            return Packet;
        }
        public void Send(GameClient client)
        {
            client.Send(Encode());
        }
        public void Deserialize(byte[] Data)
        {
            Packet = Data;
        }
        public static void Process(byte[] Packet, GameClient client)
        {
            uint Action = BitConverter.ToUInt32(Packet, 4);
            uint ID = BitConverter.ToUInt32(Packet, 8);
            switch (Action)
            {
                case 1://show
                    {
                        byte[] inbox = new byte[272];
                        Write((ushort)(inbox.Length - 8), 0, inbox);
                        Write(1048, 2, inbox);
                        Write(ID, 4, inbox);//ID    
                        Writer.Write(client.Prizes[ID].Message, 8, inbox);//String
                        client.Send(inbox);
                        break;
                    }
                case 2://delete
                    {
                        if (client.Prizes.ContainsKey(ID))
                            client.Prizes.Remove(ID);
                        if (client.Prizes.Count == 0)
                            Mailbox.SendMailbox(client, false);
                        break;
                    }
                case 3://Money
                    {
                        if (client.Prizes[ID] != null)
                        {
                            client.Entity.Money += client.Prizes[ID].goldprize;
                            client.Prizes[ID].goldprize = 0;
                        }
                        break;
                    }
                case 4://ConquerPoints
                    {
                        if (client.Prizes[ID] != null)
                        {
                            client.Entity.ConquerPoints += client.Prizes[ID].cpsprize;
                            client.Prizes[ID].cpsprize = 0;
                        }
                        break;
                    }
                case 5://ItemPrize
                    {
                        if (client.Prizes[ID] != null)
                        {
                            client.Prizes[ID].ItemPrize.UID = ConquerItem.ItemUID.Next;
                            client.Prizes[ID].ItemPrize.Mode = Enums.ItemMode.Default;
                            client.Inventory.Add(client.Prizes[ID].ItemPrize, Enums.ItemUse.CreateAndAdd);
                            if (client.Prizes[ID].ItemPrize.Purification.PurificationItemID > 0)
                            {
                                client.Prizes[ID].ItemPrize.Purification.ItemUID = client.Prizes[ID].ItemPrize.UID;
                                Database.ItemAddingTable.AddPurification(client.Prizes[ID].ItemPrize.Purification);
                            }
                            if (client.Prizes[ID].ItemPrize.ExtraEffect.EffectID > 0)
                            {
                                client.Prizes[ID].ItemPrize.ExtraEffect.ItemUID = client.Prizes[ID].ItemPrize.UID;
                                Database.ItemAddingTable.AddExtraEffect(client.Prizes[ID].ItemPrize.ExtraEffect);
                            }
                            client.Prizes[ID].ItemPrize.SendExtras(client);
                            client.Prizes[ID].ItemPrize = null;
                        }
                        break;
                    }
                case 6://Attackment
                    {
                        client.Inventory.Add(client.Prizes[ID].Attackment, 0, 1);
                        client.Prizes[ID].Attackment = 0;
                        break;
                    }
                default:  break;
            }
            client.Send(Packet);
        }
    }
}