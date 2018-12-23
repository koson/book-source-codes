using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Facade.Core;

namespace Facade.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string isbn)
        {
            PriceComparer comparer = new PriceComparer();
            List<Book> books = comparer.Compare(isbn);
            return View("Results",books);
        }

    }
}
