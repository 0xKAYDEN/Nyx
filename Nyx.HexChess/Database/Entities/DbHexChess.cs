using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.HexChess.Database.Entities
{
    [Table("db_hexchess")]
    public class DbHexChess
    {
        [Key]
        [Column("id")] public uint Identity { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("point")] public byte Point { get; set; }
        [Column("timestamp")] public DateTime timestamp { get; set; }
    }
}
