using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observer.Core
{
    public class AdminObserver:IForumObserver
    {
        public void Update(ForumPost post)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Notification notification = new Notification();
                notification.Description = $"New forum post - {post.Title} - received on {DateTime.Now}";
                notification.ReceivedOn = DateTime.Now;
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }
    }
}
