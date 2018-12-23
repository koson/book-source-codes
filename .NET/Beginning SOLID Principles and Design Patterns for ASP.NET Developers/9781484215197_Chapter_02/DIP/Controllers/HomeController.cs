using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using DIP.Core;


namespace DIP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string notificationtype)
        {
            INotifier notifier = null;
            switch(notificationtype)
            {
                case "email":
                    notifier = new EmailNotifier();
                    break;
                case "sms":
                    notifier = new SMSNotifier();
                    break;
                case "popup":
                    notifier = new PopupNotifier();
                    break;
            }
            UserManager mgr = new UserManager(notifier);
            mgr.ChangePassword("user1", "oldpwd", "newpwd");
            return View("Success");
        }

    }
}
