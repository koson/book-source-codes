using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using LSP.Core;



namespace LSP.Controllers
{
    public class HomeController : Controller
    {
        List<IReadableSettings> readableSettings = new List<IReadableSettings>();
        List<IWritableSettings> writableSettings = new List<IWritableSettings>();

        //List<ISettings> settings = new List<ISettings>();

        public HomeController()
        {
            GlobalSettings g = new GlobalSettings();
            SectionSettings s = new SectionSettings("Sports");
            UserSettings u = new UserSettings("User1");
            GuestSettings gu = new GuestSettings();

            readableSettings.Add(g);
            readableSettings.Add(s);
            readableSettings.Add(u);
            readableSettings.Add(gu);

            writableSettings.Add(g);
            writableSettings.Add(s);
            writableSettings.Add(u);

            //settings.Add(g);
            //settings.Add(s);
            //settings.Add(u);
            //settings.Add(gu);
        }

        public IActionResult Index()
        {
            var allSettings = SettingsHelper.GetAllSettings(readableSettings);
            //var allSettings = SettingsHelper.GetAllSettings(settings);
            return View(allSettings);
        }

        [HttpPost]
        public IActionResult Save()
        {
            List<Dictionary<string, string>> newSettings = new List<Dictionary<string, string>>();

            Dictionary<string, string> app = new Dictionary<string, string>();
            app.Add("Theme", "Winter");

            Dictionary<string, string> sec = new Dictionary<string, string>();
            sec.Add("Title", "Music");

            Dictionary<string, string> usr = new Dictionary<string, string>();
            usr.Add("DisplayName", "Tom");

            Dictionary<string, string> gst = new Dictionary<string, string>();
            gst.Add("GuestName", "Jerry");

            newSettings.Add(app);
            newSettings.Add(sec);
            newSettings.Add(usr);
            //settings.Add(gst);

            List<string> model = SettingsHelper.SetAllSettings(writableSettings, newSettings);
            //List<string> model = SettingsHelper.SetAllSettings(this.settings, settings);

            return View(model);
        }
    }
}
