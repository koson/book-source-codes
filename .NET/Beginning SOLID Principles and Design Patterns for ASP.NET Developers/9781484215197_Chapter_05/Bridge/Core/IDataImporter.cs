using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bridge.Core
{
    public interface IDataImporter
    {
        IErrorLogger ErrorLogger { get; set; }
        void Import(List<Customer> data);
    }
}
