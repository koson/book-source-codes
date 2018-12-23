using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace COR.Core
{
    public interface IHandler
    {
        IHandler NextHandler { get; set; }
        void Process(string filename, string filecontent);
    }

}
