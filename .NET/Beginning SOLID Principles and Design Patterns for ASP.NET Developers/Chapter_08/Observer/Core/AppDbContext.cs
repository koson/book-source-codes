using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace Observer.Core
{
    public class AppDbContext:DbContext
    {
        public DbSet<ForumPost> ForumPosts{ get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Activity> ActivityLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
        }
    }
}
