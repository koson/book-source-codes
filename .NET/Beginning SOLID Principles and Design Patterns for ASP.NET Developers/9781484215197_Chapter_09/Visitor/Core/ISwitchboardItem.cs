using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visitor.Core
{
    public interface ISwitchboardItem
    {
        double Accept(ISwitchboardVisitor visitor);
    }
}
