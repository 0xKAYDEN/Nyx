using Microsoft.EntityFrameworkCore;
using Nyx.Server.Database.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Context
{
    public class ServerDbContext : AbstractDbContext
    {
        private static readonly ILogger logger = Log.ForContext<ServerDbContext>();
        public virtual DbSet<DbNpc> Npcs { get; set; }
        public virtual DbSet<DbAccounts> Accounts { get; set; }

    }
}
