﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Flyweight.Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<WebsiteStats> WebsiteStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
        }
    }
}
