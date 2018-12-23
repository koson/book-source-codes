using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.Entity;


namespace ServiceLayer.Core
{
    public class AppDbContext:DbContext
    {
        public DbSet<StockTradingEntry> StockTradingLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
        }
    }
}
