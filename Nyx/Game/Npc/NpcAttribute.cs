using System;

namespace Nyx.Server.Game.Npc
{
    public class NpcAttribute : Attribute
    {
        public static readonly Func<NpcAttribute, uint> Translator = (attr) => attr.NpcId;

        public uint NpcId { get; private set; }

        public NpcAttribute(uint npcId)
        {
            NpcId = npcId;
        }
    }
}