
using System;
namespace Nyx.Server.Network.GamePackets
{
    public class AutoHunt
    {
        public enum Mode : byte
        {
            None,
            Start,
            EndAuto,
            EXPGained,
            Unknown4,
            Unknown5,
            KilledBy,
            ChangedMap
        }
        byte[] Packet;
        public AutoHunt()
        {
            Packet = new byte[100];
            Writer.Write(100 - 8, 0, Packet);
            Writer.Write(1070, 2, Packet);
        }
        public Mode Action
        {
            get { return (Mode)Packet[4]; }
            set { Writer.Write((byte)value, 4, Packet); }
        }
        public ushort Icon
        {
            get
            {
                return BitConverter.ToUInt16(Packet, 6);
            }
            set
            {
                Writer.Write(value, 6, Packet);
            }
        }
        public ulong EXPGained
        {
            get
            {
                return BitConverter.ToUInt16(Packet, 14);
            }
            set
            {
                Writer.Write(value, 14, Packet);
            }
        }
        public uint Unknown
        {
            get
            {
                return BitConverter.ToUInt16(Packet, 22);
            }
            set
            {
                Writer.Write(value, 22, Packet);
            }
        }
        public string KilledName
        {
            set { Writer.WriteWithLength(value, 23, Packet); }
        }
        public byte[] Encode()
        {
            
                return Packet;
        }
        public void Deserialize(byte[] Pack)
        {
            Packet = Pack;
        }
        public static void Process(byte[] packet, Client.GameClient client)
        {
            AutoHunt AutoHunt = new AutoHunt();
            AutoHunt.Deserialize(packet);
            switch (AutoHunt.Action)
            {
                case AutoHunt.Mode.Start:
                    {
                        AutoHunt.Action = AutoHunt.Mode.Start;
                        client.Entity.AddFlag3((ulong)Update.Flags3.AutoHunting);
                        client.Entity.AutoHuntEXP = 0;
                        client.Entity.InAutoHunt = true;
                        break;
                    }
                case AutoHunt.Mode.EndAuto:
                    {
                        client.Entity.InAutoHunt = false;
                        AutoHunt.Action = AutoHunt.Mode.EndAuto;
                        AutoHunt.EXPGained = client.Entity.AutoHuntEXP;
                        client.Entity.RemoveFlag3((ulong)Update.Flags3.AutoHunting);
                        break;
                    }
                case AutoHunt.Mode.EXPGained:
                    {
                        client.Entity.InAutoHunt = false;
                        AutoHunt.Action = AutoHunt.Mode.EXPGained;
                        AutoHunt.EXPGained = client.Entity.AutoHuntEXP;
                        client.Entity.RemoveFlag3((ulong)Update.Flags3.AutoHunting);
                        client.IncreaseExperience(client.Entity.AutoHuntEXP, false);
                        client.Entity.AutoHuntEXP = 0;
                        break;
                    }
                default:  break;
            }
            client.Send(AutoHunt.Encode());
        }
    }
}