using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iterator.Core
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
    }
}
