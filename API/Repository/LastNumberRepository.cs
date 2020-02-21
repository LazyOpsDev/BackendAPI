using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Minitwit.DataAccessLayer;

namespace Repository
{
    public class LastNumberRepository : ILastNumberRepository
    {
        private readonly CustomDbContext _context;

        public LastNumberRepository(CustomDbContext context)
        {
            _context = context;
        }

        public int ReadLatest()
        {
            //using (var _context = new CustomDbContext())
            //{
                var r = _context.Latest.FirstOrDefault();
                return r?.latest ?? 0;
            //}
        }

        public void WriteLatest(int i)
        {
            //using (var _context = new CustomDbContext())
            //{
                var r = _context.Latest.FirstOrDefault();
                if (r == null)
                {
                    _context.Latest.Add(new Minitwit.Models.LatestModel { latest = i });
                    _context.SaveChanges();
                    return;
                }
                r.latest = i;
                _context.Update(r);
                _context.SaveChanges();
            //}

        }
    }
}
