using System;
using System.Runtime.InteropServices;
using System.Text;
using Nyx.Server;
using Nyx.Server.Network.GamePackets;
using Nyx.Server.Network;
using Nyx.Server.Client;
using Message = Nyx.Server.Network.GamePackets.Message;

namespace Nyx.Server.Network
{
    public class MsgMessageBoard
    {
        public const Int16 Id = 1111;
        public partial class Msg
        {
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct MsgHeader
            {
                public Int16 Length;
                public Int16 Type;
            }
        }
        public enum Action
        {
            None = 0,
            Del = 1,
            GetList = 2,
            List = 3,
            GetWords = 4,
        };

        public enum Channel
        {
            None = 0,
            MsgTrade = 2201,
            MsgFriend = 2202,
            MsgTeam = 2203,
            MsgSyn = 2204,
            MsgOther = 2205,
            MsgSystem = 2206,
        };

        public struct MsgInfo
        {
            public MsgMessageBoard.Msg.MsgHeader Header;
            public UInt16 Index;
            public UInt16 Channel;
            public Byte Action;
            public String Param;
        };


        public static void Create(Nyx.Server.Client.GameClient client, UInt16 Index, ushort Channel, String[] Params, Action Action)
        {
            try
            {
                int StrLength = 0;
                if (Params != null)
                {
                    for (int i = 0; i < Params.Length; i++)
                    {
                        if (Params[i] == null || Params[i].Length > 255)
                            return;

                        StrLength += Params[i].Length + 1;
                    }
                }

                byte[] Out = new byte[14 + StrLength + 8];
                Writer.Write((ushort)(Out.Length - 8), 0, Out);
                Writer.Write(1111, 2, Out);
                Writer.Write(Index, 8, Out);
                Writer.Write(Channel, 10, Out);
                Writer.Write((byte)Action, 12, Out);
                if (Params != null)
                {
                    int Pos = 14;
                    Writer.Write((byte)Params.Length, 13, Out);
                    for (int x = 0; x < Params.Length; x++)
                    {
                        Writer.Write((byte)Params[x].Length, Pos, Out);
                        for (byte i = 0; i < (byte)Params[x].Length; i++)
                            Writer.Write((byte)Params[x][i], (Pos + 1 + i), Out);
                        Pos += Params[x].Length + 1;
                    }
                }
                client.Send(Out);
                return;

            }
            catch (Exception) { return; }
        }
        public static Encoding Encoding = Encoding.GetEncoding("iso-8859-1");
        public static void Process(Nyx.Server.Client.GameClient Client, Byte[] Buffer)
        {
            try
            {
                ushort MsgLength = BitConverter.ToUInt16(Buffer, 0);
                ushort MsgId = BitConverter.ToUInt16(Buffer, 2);
                ushort Index = BitConverter.ToUInt16(Buffer, 8);
                ushort Channel = BitConverter.ToUInt16(Buffer, 10);
                Action Action = (Action)Buffer[12];
                string Param = null;
                Param = Encoding.Default.GetString(Buffer, 15, Buffer[14]);


                switch (Action)
                {
                    case Action.Del:
                        {
                            if (Param != Client.Entity.Name) // || GM/PM
                                return;

                            Message.MessageBoard.MessageInfo Info =
                               Message.MessageBoard.GetMsgInfoByAuthor(Param, (ushort)Channel);

                            Message.MessageBoard.Delete(Info, (ushort)Channel);
                            return;
                        }
                    case Action.GetList:
                        {
                            String[] List = Message.MessageBoard.GetList(Index, (ushort)Channel);
                            MsgMessageBoard.Create(Client, Index, Channel, List, Action.List);
                            return;

                        }
                    case Action.GetWords:
                        {
                            String Words = Message.MessageBoard.GetWords(Param, (UInt16)Channel);
                            Client.Send(new Nyx.Server.Network.GamePackets.Message(Words, Client.Entity.Name, Param, System.Drawing.Color.White, Channel));
                            return;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
            catch (Exception) {  }
        }
    }
}
