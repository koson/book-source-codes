using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Observer.Core
{
    public class ActivityObserver:IForumObserver
    {
        public void Update(ForumPost post)
        {
            using (AppDbContext db = new AppDbContext())
            {
                Activity activity = new Activity();
                activity.Description = $"User {post.UserName} added a forum post on {DateTime.Now}";
                activity.TimeStamp = DateTime.Now;
                db.ActivityLog.Add(activity);
                db.SaveChanges();
            }
        }
    }
}
