using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Builder.Core
{
    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer computer;

        public OfficeComputerBuilder()
        {
            computer = new Computer();
            computer.Parts = new List<ComputerPart>();
        }

        public void AddCPU()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from p in db.ComputerParts
                            where p.UseType == "OFFICE" && p.Part == "CPU"
                            select p;
                computer.Parts.Add(query.SingleOrDefault());
            }
        }

        public void AddCabinet()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from p in db.ComputerParts
                            where p.UseType == "OFFICE" && p.Part == "CABINET"
                            select p;
                computer.Parts.Add(query.SingleOrDefault());
            }
        }

        public void AddMouse()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from p in db.ComputerParts
                            where p.UseType == "OFFICE" && p.Part == "MOUSE"
                            select p;
                computer.Parts.Add(query.SingleOrDefault());
            }
        }

        public void AddKeyboard()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from p in db.ComputerParts
                            where p.UseType == "OFFICE" && p.Part == "KEYBOARD"
                            select p;
                computer.Parts.Add(query.SingleOrDefault());
            }
        }

        public void AddMonitor()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from p in db.ComputerParts
                            where p.UseType == "OFFICE" && p.Part == "MONITOR"
                            select p;
                computer.Parts.Add(query.SingleOrDefault());
            }
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }
}
