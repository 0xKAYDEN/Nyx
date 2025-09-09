using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Entities
{
    [Table("npcs")]
    public class DbNpc
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; } // unsigned int, not nullable

        [Column("name")]
        public string Name { get; set; } // varchar(255), not nullable

        [Column("type")]
        public uint? Type { get; set; } // unsigned int, nullable

        [Column("lookface")]
        public uint? Lookface { get; set; } // unsigned int, nullable

        [Column("mapid")]
        public uint? Mapid { get; set; } // unsigned int, nullable

        [Column("cellx")]
        public uint? Cellx { get; set; } // unsigned int, nullable

        [Column("celly")]
        public uint? Celly { get; set; } // unsigned int, nullable

        [Column("task0")]
        public uint? Task0 { get; set; } // unsigned int, nullable

        [Column("category")]
        public string? Sort { get; set; } // varchar(255), nullable
    }
}

