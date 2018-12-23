using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.Entity;


namespace COR.Core
{
    public class AppDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FileStoreEntry> FileStore { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
        }
    }
}
