using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Entities
{
    [Table("hardwareban")]
    public class DbHardwareBan
    {
        [Key][Column("GUID")]
        public Guid ID { get; set; } // unsigned int, not nullable

        [Column("Username")]
        public string Username { get; set; } // unsigned int, not nullable

        [Column("IPAddress")]
        public string IPAddress { get; set; } // unsigned int, nullable

        [Column("MacAddress")]
        public string? MacAddress { get; set; } // unsigned int, nullable

        [Column("HDSerial")]
        public string? HDSerial { get; set; } // unsigned int, nullable

        [Column("GPUSerial")]
        public string? GPUSerial { get; set; } // unsigned int, nullable

        [Column("EntityID")]
        public uint? EntityID { get; set; } // unsigned int, nullable

        [Column("Reason")]
        public string Reason { get; set; } // unsigned int, nullable

        [Column("BannedAt")]
        public DateTime BannedAt { get; set; }

        [Column("ExpiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [Column("IsActive")]
        public bool IsActive => ExpiresAt == null || ExpiresAt > DateTime.UtcNow;

        [Column("TimeStamp")]
        public DateTime? TimeStamp { get; set; } // varchar(255), nullable
    }
}
