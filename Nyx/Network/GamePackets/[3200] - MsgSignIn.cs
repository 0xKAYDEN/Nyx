using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public class MsgSignIn : Interfaces.IPacket
    {
        byte[] buffer;
        public MsgSignIn()
        {
            buffer = new byte[12 + 8];
            Writer.Write((ushort)(buffer.Length - 8), 0, buffer);
            Writer.Write(3200, 2, buffer);
        }
        public Actions Type
        {
            get { return (Actions)buffer[4]; }
            set { buffer[4] = (byte)value; }
        }
        public byte RewardsClaimed
        {
            get { return buffer[5]; }
            set { buffer[5] = value; }
        }
        public byte VIPChance
        {
            get { return buffer[6]; }
            set { buffer[6] = value; }
        }
        public int SignedDays
        {
            get { return BitConverter.ToInt32(buffer, 8); }
            set { Writer.Write(value, 8, buffer); }
        }
        public enum Actions : byte
        {
            Sign,
            LateSignIn,
            Reward,
            Show
        }
        public void Deserialize(byte[] Buffer)
        {
            buffer = Buffer;
        }
        public byte[] Encode()
        {
            return buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(buffer);
        }
        public static void Handle(Client.GameClient client, byte[] packet)
        {
            MsgSignIn sign = new MsgSignIn();
            sign.Deserialize(packet);
            switch (sign.Type)
            {
                case Actions.Sign:
                    {
                        if (client.Entity.DailySignDays[DateTime.Now.Day - 1] == 0)
                        {
                            client.Inventory.AddTime(Database.SignInTable.Prizes[1].PrizeItemID, 2 * 24 * 60 * 60);
                            client.Entity.DailySignDays[DateTime.Now.Day - 1] = 1;
                            byte count = 0;
                            for (int i = 0; i < client.Entity.DailySignDays.Length; i++)
                            {
                                count += client.Entity.DailySignDays[i];
                            }
                            byte next = 0;
                            if (client.Entity.DailySignRewards == 0) next = 2;
                            else if (client.Entity.DailySignRewards == 2) next = 7;
                            else if (client.Entity.DailySignRewards == 7) next = 14;
                            else if (client.Entity.DailySignRewards == 14) next = 21;
                            else if (client.Entity.DailySignRewards == 21) next = 28;
                            if (count >= next)
                            {
                                if (client.Entity.DailySignRewards == 0) client.Entity.DailySignRewards = 2;
                                else if (client.Entity.DailySignRewards == 2) client.Entity.DailySignRewards = 7;
                                else if (client.Entity.DailySignRewards == 7) client.Entity.DailySignRewards = 14;
                                else if (client.Entity.DailySignRewards == 14) client.Entity.DailySignRewards = 21;
                                else if (client.Entity.DailySignRewards == 21) client.Entity.DailySignRewards = 28;
                                client.Inventory.AddTime(Database.SignInTable.Prizes[client.Entity.DailySignRewards].PrizeItemID, 2 * 24 * 60 * 60);
                            }
                            Show(client);
                        }
                        break;
                    }
                case Actions.LateSignIn:
                    {
                        if (client.Entity.VIPLevel == 0 || client.Entity.DailySignVIPChances == 0) break;
                        client.Entity.DailySignVIPChances--;
                        if (client.Entity.ConquerPoints >= 15)
                        {
                            for (int i = 0; i < DateTime.Now.Day; i++)
                            {
                                if (client.Entity.DailySignDays[i] == 0)
                                {
                                    client.Entity.ConquerPoints -= 15;
                                    client.Entity.DailySignDays[i] = 1;
                                    client.Inventory.AddTime(Database.SignInTable.Prizes[1].PrizeItemID, 2 * 24 * 60 * 60);
                                    byte count = 0;
                                    for (int x = 0; x < client.Entity.DailySignDays.Length; x++)
                                    {
                                        count += client.Entity.DailySignDays[x];
                                    }
                                    byte next = 0;
                                    if (client.Entity.DailySignRewards == 0) next = 2;
                                    else if (client.Entity.DailySignRewards == 2) next = 7;
                                    else if (client.Entity.DailySignRewards == 7) next = 14;
                                    else if (client.Entity.DailySignRewards == 14) next = 21;
                                    else if (client.Entity.DailySignRewards == 21) next = 28;
                                    if (count >= next)
                                    {
                                        if (client.Entity.DailySignRewards == 0) client.Entity.DailySignRewards = 2;
                                        else if (client.Entity.DailySignRewards == 2) client.Entity.DailySignRewards = 7;
                                        else if (client.Entity.DailySignRewards == 7) client.Entity.DailySignRewards = 14;
                                        else if (client.Entity.DailySignRewards == 14) client.Entity.DailySignRewards = 21;
                                        else if (client.Entity.DailySignRewards == 21) client.Entity.DailySignRewards = 28;
                                        client.Inventory.AddTime(Database.SignInTable.Prizes[client.Entity.DailySignRewards].PrizeItemID, 2 * 24 * 60 * 60);
                                    }
                                    Show(client);
                                    break;
                                }
                            }
                        }
                        else if (client.Entity.BoundCps >= 15)
                        {
                            for (int i = 0; i < DateTime.Now.Day; i++)
                            {
                                if (client.Entity.DailySignDays[i] == 0)
                                {
                                    client.Entity.BoundCps -= 15;
                                    client.Entity.DailySignDays[i] = 1;
                                    client.Inventory.AddTime(Database.SignInTable.Prizes[1].PrizeItemID, 2 * 24 * 60 * 60);
                                    byte count = 0;
                                    for (int x = 0; x < client.Entity.DailySignDays.Length; x++)
                                    {
                                        count += client.Entity.DailySignDays[x];
                                    }
                                    byte next = 0;
                                    if (client.Entity.DailySignRewards == 0) next = 2;
                                    else if (client.Entity.DailySignRewards == 2) next = 7;
                                    else if (client.Entity.DailySignRewards == 7) next = 14;
                                    else if (client.Entity.DailySignRewards == 14) next = 21;
                                    else if (client.Entity.DailySignRewards == 21) next = 28;
                                    if (count >= next)
                                    {
                                        if (client.Entity.DailySignRewards == 0) client.Entity.DailySignRewards = 2;
                                        else if (client.Entity.DailySignRewards == 2) client.Entity.DailySignRewards = 7;
                                        else if (client.Entity.DailySignRewards == 7) client.Entity.DailySignRewards = 14;
                                        else if (client.Entity.DailySignRewards == 14) client.Entity.DailySignRewards = 21;
                                        else if (client.Entity.DailySignRewards == 21) client.Entity.DailySignRewards = 28;
                                        client.Inventory.AddTime(Database.SignInTable.Prizes[client.Entity.DailySignRewards].PrizeItemID, 2 * 24 * 60 * 60);
                                    }
                                    Show(client);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                case Actions.Show:
                    {
                        Show(client);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("MsgSignIn Unknown Type : " + (byte)sign.Type);
                        break;
                    }
            }
        }
        private static string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = "";
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
        public static void Show(Client.GameClient client)
        {
            MsgSignIn msgsign = new MsgSignIn();
            msgsign.Type = Actions.Show;
            if (client.Entity.DailySignRewards == 2) msgsign.RewardsClaimed = 1;
            if (client.Entity.DailySignRewards == 7) msgsign.RewardsClaimed = 2;
            if (client.Entity.DailySignRewards == 14) msgsign.RewardsClaimed = 3;
            if (client.Entity.DailySignRewards == 21) msgsign.RewardsClaimed = 4;
            if (client.Entity.DailySignRewards == 28) msgsign.RewardsClaimed = 5;
            string bin = "";
            for (byte z = 0; z < 31; z++)
            {
                bin += Convert.ToString(client.Entity.DailySignDays[z]);
            }
            if (client.Entity.VIPLevel != 0) msgsign.VIPChance = client.Entity.DailySignVIPChances;
            msgsign.SignedDays = Convert.ToInt32((string)(Reverse(bin)), 2);
            msgsign.Send(client);
        }
        public static void Reset()
        {
            foreach (var client in Kernel.GamePool.Values)
            {
                client.Entity.DailySignDays = new byte[31];
                client.Entity.DailySignRewards = 0;
            }
            new Database.MySqlCommand(Database.MySqlCommandType.UPDATE).Update("entities").Set("DailySignedDays", "").Set("DailySignRewards", 0).Set("DailySignVIPChances", 10).Execute();
        }
    }

}
