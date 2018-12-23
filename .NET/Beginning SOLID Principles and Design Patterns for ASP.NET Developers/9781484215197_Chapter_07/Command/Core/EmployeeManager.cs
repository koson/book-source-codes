using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Command.Core
{
    public class EmployeeManager
    {
        private int employeeId;

        public EmployeeManager(int employeeid)
        {
            this.employeeId = employeeid;
        }

        public void CreateEmailAccount()
        {
            using (AppDbContext db = new AppDbContext())
            {
                CommandQueueItem item = new CommandQueueItem();
                item.EmployeeId = this.employeeId;
                item.CommandText = "EMAIL_ACCOUNT";
                db.CommandQueue.Add(item);
                db.SaveChanges();
            }
        }

        public void UndoCreateEmailAccount()
        {
            using (AppDbContext db = new AppDbContext())
            {
                CommandQueueItem item = db.CommandQueue.Where(i => i.EmployeeId == employeeId && i.CommandText == "EMAIL_ACCOUNT").SingleOrDefault();
                if (item != null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void OrderVisitingCards()
        {
            using (AppDbContext db = new AppDbContext())
            {
                CommandQueueItem item = new CommandQueueItem();
                item.EmployeeId = this.employeeId;
                item.CommandText = "VISITING_CARDS";
                db.CommandQueue.Add(item);
                db.SaveChanges();
            }
        }

        public void UndoOrderVisitingCards()
        {
            using (AppDbContext db = new AppDbContext())
            {
                CommandQueueItem item = db.CommandQueue.Where(i => i.EmployeeId == employeeId && i.CommandText == "VISITING_CARDS").SingleOrDefault();
                if (item != null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void PrepareIdentityCard()
        {
            using (AppDbContext db = new AppDbContext())
            {
                CommandQueueItem item = new CommandQueueItem();
                item.EmployeeId = this.employeeId;
                item.CommandText = "IDENTITY_CARD";
                db.CommandQueue.Add(item);
                db.SaveChanges();
            }
        }

        public void UndoPrepareIdentityCard()
        {
            using (AppDbContext db = new AppDbContext())
            {
                CommandQueueItem item = db.CommandQueue.Where(i => i.EmployeeId == employeeId && i.CommandText == "IDENTITY_CARD").SingleOrDefault();
                if (item != null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

    }
}
