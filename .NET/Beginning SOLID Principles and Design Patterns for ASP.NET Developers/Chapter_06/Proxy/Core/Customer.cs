using System;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proxy.Core
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [StringLength(5)]
        public string CustomerID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Country { get; set; }
    }
}