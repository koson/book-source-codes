using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Proxy.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;


namespace Proxy.Core
{
    public interface ICustomerApi
    {
        List<Customer> Get();
        Customer Get(string customerid);
        void Post(Customer obj);
        void Put(string customerid, Customer obj);
        void Delete(string customerid);
    }
}
