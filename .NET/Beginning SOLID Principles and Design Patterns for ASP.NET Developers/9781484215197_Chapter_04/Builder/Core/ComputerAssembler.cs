using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Builder.Core
{
    public class ComputerAssembler
    {
        private IComputerBuilder builder;

        public ComputerAssembler(IComputerBuilder builder)
        {
            this.builder = builder;
        }


        public Computer AssembleComputer()
        {
            builder.AddCPU();
            builder.AddCabinet();
            builder.AddMonitor();
            builder.AddKeyboard();
            builder.AddMouse();

            return builder.GetComputer();
        }

    }
}
