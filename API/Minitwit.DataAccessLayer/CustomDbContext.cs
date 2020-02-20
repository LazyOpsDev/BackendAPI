using Microsoft.EntityFrameworkCore;
using Minitwit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minitwit.DataAccessLayer
{
    public class CustomDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<LatestModel> Latest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
            
        }

        public CustomDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = @"Server=db;Database=minitwit;Uid=user;Pwd=P4sSw0rd";
            optionsBuilder.UseMySql(connString);
        }
    }
}
