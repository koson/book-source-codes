using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strategy.Core
{
    public interface ICompressionAlgorithm
    {
        void Compress(string source, string destination);
    }
}
