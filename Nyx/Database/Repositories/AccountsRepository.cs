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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Nyx.Server.Database.Repositories
{
    public class AccountsRepository
    {
        public int RandomKey { get; set; }
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

        public static uint GenerateAuthKey(string username, string password)
        {
            int randomKey = Kernel.Random.Next(11, 253) % 100 + 1;
            return (uint)(username.GetHashCode() * password.GetHashCode() * randomKey);
        }

        public static bool ValidateAuthKey(uint key, string username, string password)
        {
            // You might need to store the randomKey used during generation
            // or use a deterministic approach
            for (int i = 1; i <= 100; i++)
            {
                uint testKey = (uint)(username.GetHashCode() * password.GetHashCode() * i);
                if (testKey == key)
                    return true;
            }
            return false;
        }

        //public async Task<bool> MatchKey(uint key)
        //{
        //    return key = await GenerateKey(RandomKey);
        //}

        //public async Task<uint> GenerateKey(int randomKey = 0 ,string Username, string Password)
        //{
        //    if (randomKey == 0)
        //        RandomKey = Kernel.Random.Next(11, 253) % 100 + 1;
        //    return (uint)
        //                 (Username.GetHashCode() *
        //                Password.GetHashCode() *
        //                RandomKey);
        //}

    }
}
