using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Iterator.Core;

using Microsoft.AspNet.Hosting;


namespace Iterator.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            OrderHistory history = new OrderHistory(true);
            List<Order> orders = new List<Order>();
            foreach(Order o in history)
            {
                orders.Add(o);
            }
            //orders.Clear();
            //foreach (Order o in history)
            //{
            //    orders.Add(o);
            //}
            return View(orders);
        }

    }
}
