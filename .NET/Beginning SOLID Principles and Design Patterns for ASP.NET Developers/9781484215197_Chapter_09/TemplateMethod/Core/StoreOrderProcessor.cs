using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateMethod.Core
{
    public class StoreOrderProcessor : OrderProcessor
    {
        public override void ValidateOrder()
        {
            using (AppDbContext db = new AppDbContext())
            {
                OrderLog log = new OrderLog();
                log.OrderId = this.orderId;
                log.Status = "Order has been validated.";
                db.OrderLog.Add(log);
                db.SaveChanges();
            }
        }

        public override void ValidatePayment()
        {
            using (AppDbContext db = new AppDbContext())
            {
                OrderLog log = new OrderLog();
                log.OrderId = this.orderId;
                log.Status = "The cash payment has been received.";
                db.OrderLog.Add(log);
                db.SaveChanges();
            }
        }

        public override void Pack()
        {
            using (AppDbContext db = new AppDbContext())
            {
                OrderLog log = new OrderLog();
                log.OrderId = this.orderId;
                log.Status = "The packing personnel have been notified.";
                db.OrderLog.Add(log);
                db.SaveChanges();
            }
        }

        public override void Ship()
        {
            using (AppDbContext db = new AppDbContext())
            {
                OrderLog log = new OrderLog();
                log.OrderId = this.orderId;
                log.Status = "Order has been sent to the salesman desk.";
                db.OrderLog.Add(log);
                db.SaveChanges();
            }
        }
    }
}
