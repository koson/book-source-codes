using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using Microsoft.AspNet.Hosting;

using ContactManagerApp.Core;

namespace ContactManagerApp.Controllers
{
    public class HomeController : Controller
    {
        //private AppDbContext db;
        //private AppSettings settings;
        //private IApplicationEnvironment appEnv;
        //private IHostingEnvironment env;

        //public HomeController(AppDbContext db, IOptions<AppSettings> settings, IApplicationEnvironment appEnv,IHostingEnvironment env)
        //{
        //    this.settings = settings.Options;
        //    this.db = db;
        //    this.env = env;
        //    this.appEnv = appEnv;
        //}

        public IActionResult Index()
        {
            ViewBag.Title = AppSettings.Title;
          
            using (AppDbContext db = new AppDbContext())
            {
                var query = from c in db.Contacts
                            orderby c.Id ascending
                            select c;
                List<Contact> model = query.ToList();
                return View(model);
            }
        }


        public IActionResult AddContact()
        {
            ViewBag.Title = AppSettings.Title;
            return View();
        }

        [HttpPost]
        public IActionResult AddContact(Contact obj)
        {
            ViewBag.Title = AppSettings.Title;
            if (ModelState.IsValid)
            {
                using (AppDbContext db = new AppDbContext())
                {
                    db.Contacts.Add(obj);
                    db.SaveChanges();
                    ViewBag.Message = "Contact added successfully!";
                }
            }
            return View(obj);
        }

        public IActionResult DeleteContact(int id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var contact = (from c in db.Contacts
                                where c.Id == id
                                select c).SingleOrDefault();
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
