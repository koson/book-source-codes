using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using System.Data.Common;
using System.Dynamic;
using System.Reflection;
using System.Runtime.Remoting;

using AbstractFactory.Core;


namespace AbstractFactory.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExecuteQuery(string factorytype,string query)
        {
            IDatabaseFactory factory = null;
            if (factorytype == "sqlclient")
            {
                factory = new SqlClientFactory();
            }
            else
            {
                factory = new OleDbFactory();
            }
            DatabaseHelper helper = new DatabaseHelper(factory);
            query = query.ToLower();
            if(query.StartsWith("select"))
            {
                DbDataReader reader = helper.ExecuteSelect(query);
                return View("ShowTable", reader);
            }
            else
            {
                int i = helper.ExecuteAction(query);
                return View("ShowResult", i);
            }
        }


        [HttpPost]
        public IActionResult ExecuteQueryConfig(string query)
        {
            IDatabaseFactory factory = null;

            string factorytype = AppSettings.Factory;


            if (factorytype == "sqlclient")
            {
                factory = new SqlClientFactory();
            }
            else
            {
                factory = new OleDbFactory();
            }

            DatabaseHelper helper = new DatabaseHelper(factory);

            query = query.ToLower();

            if (query.StartsWith("select"))
            {
                DbDataReader reader = helper.ExecuteSelect(query);
                return View("ShowTable", reader);
            }
            else
            {
                int i = helper.ExecuteAction(query);
                return View("ShowResult", i);
            }
        }


        [HttpPost]
        public IActionResult ExecuteQueryReflection(string query)
        {
            IDatabaseFactory factory = null;

            string factorytype = AppSettings.FactoryType;

            ObjectHandle o = Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName, factorytype);
            factory = (IDatabaseFactory)o.Unwrap();

            DatabaseHelper helper = new DatabaseHelper(factory);

            query = query.ToLower();

            if (query.StartsWith("select"))
            {
                DbDataReader reader = helper.ExecuteSelect(query);
                return View("ShowTable", reader);
            }
            else
            {
                int i = helper.ExecuteAction(query);
                return View("ShowResult", i);
            }
        }


    }
}
