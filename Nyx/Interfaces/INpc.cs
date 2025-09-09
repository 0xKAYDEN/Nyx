using System;

namespace Nyx.Server.Interfaces
{
    public interface INpc
    {
        Game.Enums.NpcType Type { get; set; }
        uint UID { get; set; }
        ushort X { get; set; }
        ushort Y { get; set; }
        ushort Mesh { get; set; }
        string Name { get; set; }
        ushort MapID { get; set; }
        void SendSpawn(Client.GameClient Client);
        void SendSpawn(Client.GameClient Client, bool checkScreen);
    }
}
