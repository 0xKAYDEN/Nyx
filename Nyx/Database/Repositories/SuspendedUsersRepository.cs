using Microsoft.EntityFrameworkCore;
using Nyx.Server.Database.Context;
using Nyx.Server.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Repositories
{
    public class SuspendedUsersRepository
    {
        // Find ban by username
        public static async Task<DbSuspendedUsers> FindAsync(string Username)
        {
            await using var db = new ServerDbContext();
            return await db.SuspendedUsers.Where(x => x.Username == Username && x.IsActive)
                .FirstOrDefaultAsync();
        }
        // Check if user is banned (using Username)
        public static async Task<bool> IsBannedAsync(string username)
        {
            await using var db = new ServerDbContext();

            return await db.SuspendedUsers.AnyAsync(x =>
                (x.Username == username) &&
                x.IsActive);
        }
    }
}
