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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql("Server=localhost;Database=minitwit;Uid=root;Pwd=hej123");                            //
        }
    }
}
