using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Minitwit.DataAccessLayer;

namespace Repository
{
    public class LastNumberRepository : ILastNumberRepository
    {
        //private readonly CustomDbContext _context;

        //public LastNumberRepository(CustomDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<int> ReadLatest()
        {
            using (var _context = new CustomDbContext())
            {
                var r = await _context.Latest.FirstOrDefaultAsync();
                return r?.latest ?? 0;
            }
        }

        public async Task WriteLatest(int i)
        {
            using (var _context = new CustomDbContext())
            {
                var r = await _context.Latest.FirstOrDefaultAsync();
                if (r == null)
                {
                    _context.Latest.Add(new Minitwit.Models.LatestModel { latest = i });
                    await _context.SaveChangesAsync();
                    return;
                }
                r.latest = i;
                _context.Update(r);
                await _context.SaveChangesAsync();
            }

        }
    }
}
