using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Observer.Core;

using Microsoft.Extensions.Caching.Memory;


namespace Observer.Controllers
{
    public class HomeController : Controller
    {
        IMemoryCache cache;

        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public IActionResult Index()
        {
            AdminObserver observer1 = new AdminObserver();
            ActivityObserver observer2 = new ActivityObserver();
            ForumNotifier notifier = new ForumNotifier();

            notifier.Subscribe(observer1);
            notifier.Subscribe(observer2);

            //cache.Set("notifier", notifier);

            return View();
        }

        public IActionResult AddPost(ForumPost post)
        {
            post.PostedOn = DateTime.Now;
            using (AppDbContext db = new AppDbContext())
            {
                db.ForumPosts.Add(post);
                db.SaveChanges();
            }
            ViewBag.Message = "Post submitted successfully!";
            //ForumNotifier notifier = cache.Get<ForumNotifier>("notifier");
            ForumNotifier notifier = new ForumNotifier();
            notifier.Notify(post);
            return View("Index", post);
        }

        public IActionResult ShowNotifications()
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<Notification> notifications = db.Notifications.ToList();
                return View(notifications);
            }
        }



        public IActionResult ShowActivityLog()
        {
            using (AppDbContext db = new AppDbContext())
            {
                List<Activity> activitylog = db.ActivityLog.ToList();
                return View(activitylog);
            }
        }


    }
}
