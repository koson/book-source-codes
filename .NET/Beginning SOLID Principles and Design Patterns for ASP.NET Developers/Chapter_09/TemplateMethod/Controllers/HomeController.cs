using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using TemplateMethod.Core;

namespace TemplateMethod.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessOrder(string type)
        {
            OrderProcessor processor = null;

            if(type=="store")
            {
                processor = new StoreOrderProcessor();
            }
            else
            {
                processor = new OnlineOrderProcessor();
            }
            int orderId = new Random().Next(100, 1000);
            processor.ProcessOrder(orderId);
            using (AppDbContext db = new AppDbContext())
            {
                List<OrderLog> logs = db.OrderLog.Where(o => o.OrderId == orderId).ToList();
                return View("Success", logs);
            }
        }
    }
}
