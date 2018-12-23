using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Flyweight.Core;


namespace Flyweight.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowStats(string host)
        {
            WebsiteStatsFactory factory = new WebsiteStatsFactory();
            WebsiteStats stats = (WebsiteStats)factory[host];
            if (stats == null)
            {
                ViewBag.Message = "Invalid Host Name!";
                return View("Index");
            }
            else
            {
                return View(stats);
            }
        }
    }
}
