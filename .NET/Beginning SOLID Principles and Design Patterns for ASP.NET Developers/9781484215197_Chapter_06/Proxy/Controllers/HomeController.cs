using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Proxy.Core;


namespace Proxy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ServiceProxy proxy = new ServiceProxy();
            List<Customer> data = proxy.Get();
            return View(data);
        }

        public IActionResult Update(string id)
        {
            ServiceProxy proxy = new ServiceProxy();
            Customer data = proxy.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Customer obj)
        {
            if (ModelState.IsValid)
            {
                ServiceProxy proxy = new ServiceProxy();
                proxy.Put(obj.CustomerID,obj);
                TempData["Message"] = "Customer modified successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Customer obj)
        {
            if (ModelState.IsValid)
            {
                ServiceProxy proxy = new ServiceProxy();
                proxy.Post(obj);
                TempData["Message"] = "Customer added successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }


        public IActionResult Delete(string id)
        {
            ServiceProxy proxy = new ServiceProxy();
            proxy.Delete(id);
            TempData["Message"] = "Customer deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
