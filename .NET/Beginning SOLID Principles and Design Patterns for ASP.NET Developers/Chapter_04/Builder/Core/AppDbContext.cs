using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace Builder.Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<ComputerPart> ComputerParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
        }

        public void InitData()
        {
            if (ComputerParts.Count() == 0)
            {
                ComputerParts.Add(new ComputerPart() { UseType = "HOME", Part = "CPU", PartCode = "CPU_HOME" });
                ComputerParts.Add(new ComputerPart() { UseType = "HOME", Part = "CABINET", PartCode = "CABINET_HOME" });
                ComputerParts.Add(new ComputerPart() { UseType = "HOME", Part = "MONITOR", PartCode = "MONITOR_HOME" });
                ComputerParts.Add(new ComputerPart() { UseType = "HOME", Part = "KEYBOARD", PartCode = "KEYBOARD_HOME" });
                ComputerParts.Add(new ComputerPart() { UseType = "HOME", Part = "MOUSE", PartCode = "MOUSE_HOME" });

                ComputerParts.Add(new ComputerPart() { UseType = "OFFICE", Part = "CPU", PartCode = "CPU_OFFICE" });
                ComputerParts.Add(new ComputerPart() { UseType = "OFFICE", Part = "CABINET", PartCode = "CABINET_OFFICE" });
                ComputerParts.Add(new ComputerPart() { UseType = "OFFICE", Part = "MONITOR", PartCode = "MONITOR_OFFICE" });
                ComputerParts.Add(new ComputerPart() { UseType = "OFFICE", Part = "KEYBOARD", PartCode = "KEYBOARD_OFFICE" });
                ComputerParts.Add(new ComputerPart() { UseType = "OFFICE", Part = "MOUSE", PartCode = "MOUSE_OFFICE" });

                ComputerParts.Add(new ComputerPart() { UseType = "DEV", Part = "CPU", PartCode = "CPU_DEV" });
                ComputerParts.Add(new ComputerPart() { UseType = "DEV", Part = "CABINET", PartCode = "CABINET_DEV" });
                ComputerParts.Add(new ComputerPart() { UseType = "DEV", Part = "MONITOR", PartCode = "MONITOR_DEV" });
                ComputerParts.Add(new ComputerPart() { UseType = "DEV", Part = "KEYBOARD", PartCode = "KEYBOARD_DEV" });
                ComputerParts.Add(new ComputerPart() { UseType = "DEV", Part = "MOUSE", PartCode = "MOUSE_DEV" });

                SaveChanges();
            }
        }
    }
}
