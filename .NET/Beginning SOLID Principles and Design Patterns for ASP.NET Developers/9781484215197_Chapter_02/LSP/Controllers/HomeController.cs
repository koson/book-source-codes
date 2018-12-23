//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Mvc;

//using LSP.Classes;



//namespace LSP.Controllers
//{
//    public class HomeController : Controller
//    {
//        private List<ISettings> items = new List<ISettings>();

//        public HomeController()
//        {
//            items.Add(new GlobalSettings());
//            items.Add(new UserSettings("user1"));
//            items.Add(new SectionSettings("section1"));
//            items.Add(new GuestSettings());
//        }

//        public IActionResult Index()
//        {
//            var allSettings = SettingsHelper.GetAllSettings(items);
//            return View(allSettings);
//        }

//        public IActionResult Save()
//        {
//            List<Dictionary<string, string>> settings = new List<Dictionary<string, string>>();

//            Dictionary<string, string> app = new Dictionary<string, string>();
//            app.Add("Title", "My Application");

//            Dictionary<string, string> usr = new Dictionary<string, string>();
//            usr.Add("Theme", "Summer");

//            Dictionary<string, string> sec = new Dictionary<string, string>();
//            sec.Add("Color", "blue");

//            Dictionary<string, string> guest = new Dictionary<string, string>();
//            guest.Add("DisplayName", "Guest");

//            settings.Add(app);
//            settings.Add(usr);
//            settings.Add(sec);
//            settings.Add(guest);

//            SettingsHelper.SetAllSettings(items, settings);
//            return RedirectToAction("Index");
//        }
//    }
//}
