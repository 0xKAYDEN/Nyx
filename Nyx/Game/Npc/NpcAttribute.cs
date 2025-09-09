using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Game.Npc
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]

    public class NpcAttribute : Attribute
    {
        public static readonly Func<NpcAttribute, NpcID> Translator = (a) => a.Type;
        public NpcID Type { get; private set; }
        public NpcAttribute(NpcID type)
        {
            this.Type = type;
        }
    }
}
