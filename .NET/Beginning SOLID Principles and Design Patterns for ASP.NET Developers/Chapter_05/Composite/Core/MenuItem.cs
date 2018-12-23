using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Xml;


namespace Composite.Core
{
    public class MenuItem : IMenuComponent
    {
        public string Text { get; set; }
        public string NavigateUrl { get; set; }
        public List<IMenuComponent> Children { get; set; }
    }
}
