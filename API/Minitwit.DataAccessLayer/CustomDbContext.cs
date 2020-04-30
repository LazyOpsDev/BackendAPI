using Microsoft.EntityFrameworkCore;
using Minitwit.Models;
using System;

namespace Minitwit.DataAccessLayer
{
    public class CustomDbContext : DbContext, IDisposable
    {
        private DbContextOptionsBuilder<CustomDbContext> options;
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<LatestModel> Latest { get; set; }
    }
}
