using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Sandbox.Core;



namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchByID(string id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from c in db.Customers
                            where c.CustomerID.Contains(id)
                            orderby c.CustomerID ascending
                            select c;
                return Json(query.ToList());
            }
        }

        public IActionResult SearchByCompany(string companyname)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from c in db.Customers
                            where c.CompanyName.Contains(companyname)
                            orderby c.CustomerID ascending
                            select c;
                return Json(query.ToList());
            }
        }

        public IActionResult SearchByContact(string contactname)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from c in db.Customers
                            where c.ContactName.Contains(contactname)
                            orderby c.CustomerID ascending
                            select c;
                return Json(query.ToList());
            }
        }

        public IActionResult SearchByCountry(string country)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from c in db.Customers
                            where c.Country.Contains(country)
                            orderby c.CustomerID ascending
                            select c;
                return Json(query.ToList());
            }
        }
    }
}
