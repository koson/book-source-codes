using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace JSObjects.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult IndexObjectLiteral()
        {
            return View();
        }

        public IActionResult IndexFunctionObject()
        {
            return View();
        }

        public IActionResult IndexClosures()
        {
            return View();
        }

        public IActionResult IndexFunctionPrototype()
        {
            return View();
        }

      

    }
}
