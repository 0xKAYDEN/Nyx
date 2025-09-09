using System;

namespace Nyx.Server.Network
{
    public class NpcAttribute : Attribute
    {
        public static readonly Func<NpcAttribute, uint> Translator = (attr) => attr.NpcId;

        public uint NpcId { get; private set; }

        public NpcAttribute(uint npcId)
        {
            this.NpcId = npcId;
        }
    }
}