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
    public class HardwareBanRepository
    {

        // Find ban by username
        public static async Task<DbHardwareBan> FindAsync(string Username)
        {
            await using var db = new ServerDbContext();
            return await db.HardwareBan.Where(x => x.Username == Username && x.IsActive )
                .FirstOrDefaultAsync();
        }

        // Find ban by hardware ID
        public static async Task<DbHardwareBan> FindByHardwareIdAsync(string GpuSerial)
        {
            await using var db = new ServerDbContext();
            return await db.HardwareBan
                .Where(x => x.GPUSerial == GpuSerial && x.IsActive)
                .FirstOrDefaultAsync();
        }

        // Find ban by IP address
        public static async Task<DbHardwareBan> FindByIPAddressAsync(string ipAddress)
        {
            await using var db = new ServerDbContext();
            return await db.HardwareBan
                .Where(x => x.IPAddress == ipAddress && x.IsActive)
                .FirstOrDefaultAsync();
        }

        // Check if user is banned (using Username, GPU Serial , HDSerial , MacAddress, IPAddress )
        public static async Task<bool> IsBannedAsync(string username, string GPUSerial, string HdSerial,string MacAddress,  string ipAddress)
        {
            await using var db = new ServerDbContext();

            return await db.HardwareBan.AnyAsync(x =>
                (x.Username == username ||
                 x.GPUSerial == GPUSerial ||
                 x.HDSerial == HdSerial ||
                 x.MacAddress == MacAddress ||
                 x.IPAddress == ipAddress) &&
                x.IsActive);
        }

        // Check if user is banned (using Username)
        public static async Task<bool> IsBannedAsync(string username)
        {
            await using var db = new ServerDbContext();

            return await db.HardwareBan.AnyAsync(x =>
                (x.Username == username) &&
                x.IsActive);
        }

        // Get all active bans
        public static async Task<List<DbHardwareBan>> GetActiveBansAsync()
        {
            await using var db = new ServerDbContext();
            return await db.HardwareBan
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        // Add new ban
        public static async Task<bool> AddBanAsync(string username, uint Identity, string GPUSerial, string HDSerial, string MacAddress, string ipAddress,
            string reason, DateTime? expiresAt = null)
        {
            try
            {
                await using var db = new ServerDbContext();

                var existingBan = await FindAsync(username);
                if (existingBan != null)
                {
                    // Update existing ban
                    existingBan.GPUSerial = GPUSerial;
                    existingBan.HDSerial = HDSerial;
                    existingBan.MacAddress = MacAddress;
                    existingBan.IPAddress = ipAddress;
                    existingBan.Reason = reason;
                    existingBan.ExpiresAt = expiresAt;
                }
                else
                {
                    // Create new ban
                    var ban = new DbHardwareBan
                    {
                        ID = Guid.NewGuid(),
                        Username = username,
                        EntityID = Identity,
                        GPUSerial = GPUSerial,
                        HDSerial = HDSerial,
                        MacAddress = MacAddress,
                        IPAddress = ipAddress,
                        Reason = reason,
                        BannedAt = DateTime.UtcNow,
                        ExpiresAt = expiresAt
                    };

                    db.HardwareBan.Add(ban);
                }

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> RemoveBanAsync(string username)
        {
            try
            {
                await using var db = new ServerDbContext();

                var ban = await FindAsync(username);
                if (ban != null)
                {
                    db.HardwareBan.Remove(ban);
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> UnbanAsync(string username)
        {
            try
            {
                await using var db = new ServerDbContext();

                var ban = await FindAsync(username);
                if (ban != null)
                {
                    ban.ExpiresAt = DateTime.UtcNow;
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Clean up expired bans
        public static async Task<int> CleanExpiredBansAsync()
        {
            await using var db = new ServerDbContext();

            var expiredBans = await db.HardwareBan
                .Where(x => x.ExpiresAt != null && x.ExpiresAt < DateTime.UtcNow)
                .ToListAsync();

            db.HardwareBan.RemoveRange(expiredBans);
            return await db.SaveChangesAsync();
        }

    }
}
