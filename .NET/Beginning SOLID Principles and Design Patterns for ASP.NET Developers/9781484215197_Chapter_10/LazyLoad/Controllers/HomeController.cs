using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using LazyLoad.Core;


namespace LazyLoad.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDetails(int employeeid)
        {
            Employee emp = new Employee(employeeid);
            return View("Index",emp);
        }

        //public IActionResult Report()
        //{
        //    ReportGenerator gen = new ReportGenerator();
        //    ViewBag.Message = gen.GenerateReport();
        //    return View();
        //}


    }
}
