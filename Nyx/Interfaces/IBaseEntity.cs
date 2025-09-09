﻿using System;

namespace Nyx.Server.Game
{
    public enum EntityFlag : byte
    {
        Monster = 2,
        Player = 1
    }
}
namespace Nyx.Server.Interfaces
{
    public interface IBaseEntity
    {
        bool Dead { get; set; }
        ushort Defence { get; set; }
        byte Dodge { get; set; }
        Game.EntityFlag EntityFlag { get; set; }
        uint Hitpoints { get; set; }
        uint MagicAttack { get; set; }
        ushort MapID { get; set; }
        uint MaxAttack { get; set; }
        uint MaxHitpoints { get; set; }
        ushort MagicDefence { get; set; }
        uint MinAttack { get; set; }
        Client.GameClient Owner { get; set; }
        uint UID { get; set; }
        ushort X { get; set; }
        ushort Y { get; set; }
    }
}
