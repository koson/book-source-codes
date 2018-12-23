using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;




namespace IIFE.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAdvert()
        {
            return Json(new
            {
                Title = "Buy ONE Get one FREE!",
                Description = "Buy one large size Pizza and get one small size Pizza absolutely FREE!!!",
                Url = "http://localhost"
            });
        }

        public IActionResult IndexPlugin()
        {
            return View();
        }

    }
}
