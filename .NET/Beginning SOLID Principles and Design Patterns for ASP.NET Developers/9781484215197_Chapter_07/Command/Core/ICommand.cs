using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Microsoft.Data.Entity;

namespace Command.Core
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

}
