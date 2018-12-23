using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Builder.Core
{
    public interface IComputerBuilder
    {
        void AddCPU();
        void AddCabinet();
        void AddMouse();
        void AddKeyboard();
        void AddMonitor();
        Computer GetComputer();
    }
}
