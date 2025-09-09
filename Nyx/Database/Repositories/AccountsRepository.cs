using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Nyx.Server.Database.Context;
using Nyx.Server.Database.Entities;
using Org.BouncyCastle.Crypto.Agreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Database.Repositories
{
    public class AccountsRepository
    {
        public static async Task<DbAccounts> FindAsync(string Username)
        {
            await using var db = new ServerDbContext();
            return db.Accounts.FirstOrDefault(x => x.Username == Username);
        }

        public static async Task<DbAccounts> MatchAsync(string Username, string Password)
        {
            await using var db = new ServerDbContext();

            // Find account with matching username (case-sensitive)
            var account = await db.Accounts
                .AsNoTracking()  // Better performance for read-only operation
                .FirstOrDefaultAsync(x => x.Username == Username);

            // Return the account if found AND password matches, otherwise return null
            return account != null && account.Password == Password ? account : null;
        }

        public static async Task<DbAccounts> FindAsync(uint accountID)
        {
            await using var db = new ServerDbContext();
            return await db.Accounts
                .Where(x => x.EntityID == accountID)
                .SingleOrDefaultAsync();
        }
    }
}
