using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flyweight.Core
{
    public class WebsiteStatsFactory
    {
        private static Dictionary<string, WebsiteStats> dictionary = new Dictionary<string, WebsiteStats>();

        public IWebsiteStats this[string host]
        {
            get
            {
                if(!dictionary.ContainsKey(host))
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var query = from stats in db.WebsiteStats
                                    where stats.Host == host
                                    select stats;
                        dictionary[host] = query.SingleOrDefault();
                    }
                }
                return dictionary[host];
            }
        }
    }
}
