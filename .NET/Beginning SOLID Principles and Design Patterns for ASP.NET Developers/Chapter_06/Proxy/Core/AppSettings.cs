using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proxy.Core
{
    public class AppSettings
    {
        public static string ServiceBaseAddress { get; set; }
        public static string ServiceUrl { get; set; }
        public static string ConnectionString { get; set; }
        public static string LogFilePath { get; set; }
    }
}
