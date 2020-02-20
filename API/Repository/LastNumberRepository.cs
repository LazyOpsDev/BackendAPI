using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Minitwit.DataAccessLayer;

namespace Repository
{
    public class LastNumberRepository : ILastNumberRepository
    {
        public async Task<int> ReadLatest()
        {
            return 0;
            using (var ctx = new CustomDbContext())
            {
                var r = await ctx.Latest.FirstOrDefaultAsync();
                return r?.latest ?? 0;
            }
        }

        public async Task WriteLatest(int i)
        {
            return;
            using (var ctx = new CustomDbContext())
            {
                var r = await ctx.Latest.FirstOrDefaultAsync();
                if (r == null)
                {
                    ctx.Latest.Add(new Minitwit.Models.LatestModel { latest = i });
                    await ctx.SaveChangesAsync();
                    return;
                }
                r.latest = i;
                ctx.Update(r);
            }
        }
    }
}
