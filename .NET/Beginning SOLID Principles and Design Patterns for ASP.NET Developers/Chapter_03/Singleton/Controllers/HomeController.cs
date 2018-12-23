using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Singleton.Core;


namespace Singleton.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index1()
        {
            WebsiteMetadata metadata = WebsiteMetadata.GetInstance();
            return View("Index",metadata);
        }

        public IActionResult Index2()
        {
            WebsiteMetadata metadata = WebsiteMetadata.GetInstance();
            return View("Index",metadata);
        }

    }
}
