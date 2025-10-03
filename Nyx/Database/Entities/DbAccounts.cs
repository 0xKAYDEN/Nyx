using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Entities
{
    [Table("accounts")]
    public class DbAccounts
    {
        [Key]
        [Column("Username")]
        public string Username { get; set; } // unsigned int, not nullable

        [Column("Password")]
        public string Password { get; set; } // varchar(255), not nullable

        [Column("IP")]
        public string IP { get; set; } // unsigned int, nullable

        [Column("EntityID")]
        public uint? EntityID { get; set; } // unsigned int, nullable

        [Column("State")]
        public AccountState State { get; set; } // unsigned int, nullable

        [Column("MobileNumber")]
        public string? MobileNumber { get; set; } // unsigned int, nullable

        [Column("Email")]
        public string? Email { get; set; } // unsigned int, nullable

        [Column("HDSerial")]
        public string? HDSerial { get; set; } // unsigned int, nullable

        [Column("securitycode")]
        public string? securitycode { get; set; } // varchar(255), nullable

        //[Column("ProfileImage")]
        //public Image? ProfileImage { get; set; } // varchar(255), nullable

        [Column("RegisterationDate")]
        public DateTime? RegisterationDate { get; set; } // varchar(255), nullable
    }

    public enum AccountState : byte
    { 
        Player = 1,
        Suspended = 2,
        HardwareBanned = 3,
        GameMaster = 4,
    }
}
