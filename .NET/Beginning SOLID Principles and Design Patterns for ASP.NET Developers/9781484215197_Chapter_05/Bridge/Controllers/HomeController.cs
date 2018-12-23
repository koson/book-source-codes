using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Bridge.Core;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace Bridge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                MemoryStream ms = new MemoryStream();
                Stream s = file.OpenReadStream();
                s.CopyTo(ms);
                byte[] data = ms.ToArray();
                s.Dispose();
                ms.Dispose();

                List<Customer> records = new List<Customer>();
                StringReader reader = new StringReader(System.Text.ASCIIEncoding.UTF8.GetString(data));
                while(true)
                {
                    string record = reader.ReadLine();
                    if (string.IsNullOrEmpty(record))
                    {
                        break;
                    }
                    else
                    {
                        string[] cols = record.Split(',');
                        Customer obj = new Customer()
                        {
                            CustomerID = cols[0],
                            CompanyName = cols[1],
                            ContactName = cols[2],
                            Country = cols[3]
                        };
                        records.Add(obj);
                    }
                }
                IDataImporter importer = new DataImporterBasic();
                importer.ErrorLogger = new XmlErrorLogger();
                importer.Import(records);
            }
            ViewBag.Message = "Data imported from " + files.Count + " file(s). Please see error log for any errors!";
            return View("Index");
        }
    }
}
