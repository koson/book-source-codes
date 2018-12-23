using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateMethod.Core
{
    public class OnlineOrderProcessor : OrderProcessor
    {
        public override void ValidateOrder()
        {
            using (AppDbContext db = new AppDbContext())
            {
                OrderLog log = new OrderLog();
                log.OrderId = this.orderId;
                log.Status = "Order has been validated against availity";
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
                log.Status = "The credit card has been charged successfully.";
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
                log.Status = "Packaging department has been notified.";
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
                log.Status = "Order has been shipped to the customer's address.";
                db.OrderLog.Add(log);
                db.SaveChanges();
            }
        }
    }
}
