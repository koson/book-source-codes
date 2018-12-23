using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visitor.Core
{
    public class CircuitBreaker:ISwitchboardItem
    {
        public double Cost { get; set; }

        public double Accept(ISwitchboardVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
