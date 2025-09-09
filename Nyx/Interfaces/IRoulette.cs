using System;

namespace Nyx.Server.Interfaces
{
    public interface IRoulette
    {
        uint UID { get; set; }
        ushort X { get; set; }
        ushort Y { get; set; }
        ushort Mesh { get; set; }
        ushort TableNumber { get; set; }
        ushort StackType { get; set; }
        System.Collections.Generic.List<Client.GameClient> Recored { get; set; }

        ushort LotteryNumber { get; set; }
        ushort PlayerNumber { get; set; }
        Time32 Time { get; set; }
        int Stamp { get; set; }
        bool Place0 { get; set; }
        bool Place1 { get; set; }
        bool Place2 { get; set; }
        bool Place3 { get; set; }
        bool Place4 { get; set; }
        void SendSpawn(Client.GameClient Client);
        void SendSpawn(Client.GameClient Client, bool checkScreen);
    }
}