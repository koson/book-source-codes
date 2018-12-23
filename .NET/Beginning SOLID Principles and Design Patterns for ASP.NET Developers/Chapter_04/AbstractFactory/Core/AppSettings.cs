using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractFactory.Core
{
    public class AppSettings
    {
        public static string ConnectionString { get; set; }
        public static string Factory { get; set; }
        public static string FactoryType { get; set; }
    }
}
