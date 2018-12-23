using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interpreter.Core
{
    public interface IApiCall
    {
        string AssemblyName { get; set; }
        string ClassName { get; set; }
        string MethodName { get; set; }
        List<string> Parameters { get; set; }

        void Interpret(InterpreterContext context);
    }
}
