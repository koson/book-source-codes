using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flyweight.Core
{
    public class WebsiteStats : IWebsiteStats
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Host { get; set; }
        [Required]
        public int PageViews { get; set; }
        [Required]
        public int SiteVisits { get; set; }
        [Required]
        public string TopKeywords { get; set; }
        [Required]
        public int Bandwidth { get; set; }

        public int GetActiveUsers()
        {
            return new Random().Next(100, 10000);
        }
    }
}
