using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Entities
{
    [Table("suspendedusers")]
    public class DbSuspendedUsers
    {
        [Key][Column("GUID")]
        public virtual Guid GUID { get; set; }

        [Column("Username")]
        public virtual string Username { get; set; }

        [Column("Identity")]
        public virtual uint Identity {  get; set; }

        [Column("Reason")]
        public virtual string Reason { get; set; }

        [Column("BannedAt")]
        public DateTime BannedAt { get; set; }

        [Column("ExpiresAt")]
        public DateTime? ExpiresAt { get; set; }

        [Column("IsActive")]
        public virtual bool IsActive => ExpiresAt == null || ExpiresAt > DateTime.UtcNow;

        [Column("TimeStamp")]
        public DateTime? TimeStamp { get; set; } // varchar(255), nullable

    }
}
