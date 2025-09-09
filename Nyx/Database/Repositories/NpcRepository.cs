using Nyx.Server.Database.Context;
using Nyx.Server.Database.Entities;

namespace Nyx.Server.Database.Repositories
{
    public static class NpcRepository
    {
        public static async Task<List<DbNpc>> GetAsync()
        {
            await using var context = new ServerDbContext();
            return context.Npcs.ToList();
        }

        //public static async Task<List<DbDynanpc>> GetDynamicAsync()
        //{
        //    await using var context = new ServerDbContext();
        //    return context.DynamicNpcs.ToList();
        //}
    }
}
