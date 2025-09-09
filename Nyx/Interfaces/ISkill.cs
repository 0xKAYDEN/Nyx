using System;

namespace Nyx.Server.Interfaces
{
    public interface ISkill
    {
        uint Experience { get; set; }
        ushort ID { get; set; }
        byte Level { get; set; }
        byte PreviousLevel { get; set; }
        byte LevelHu { get; set; }
        byte LevelHu2 { get; set; }
        bool Available { get; set; }
        byte TempLevel { get; set; }
        void Send(Client.GameClient client);
    }
}
