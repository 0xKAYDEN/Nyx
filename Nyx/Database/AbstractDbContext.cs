using Microsoft.EntityFrameworkCore;
using Nyx.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database
{
    public class AbstractDbContext : DbContext
    {
        private static string ConnectionString => $"server={Configuration.Hostname};database={Configuration.Schema};user={Configuration.Username};password={Configuration.Password};port={Configuration.Port}";
        /// <summary>
        ///     Configures the database to be used for this context. This method is called
        ///     for each instance of the context that is created. For this project, the MySQL
        ///     connector will be initialized with a connection string from the server's
        ///     configuration file.
        /// </summary>
        /// <param name="options">Builder to create the context</param>
        /// 
        public virtual DbSet<DbNpc> Npcs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies(false); // This requires the Microsoft.EntityFrameworkCore.Proxies package
            options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }

        public static DatabaseConfiguration Configuration { get; set; }
    }
}
