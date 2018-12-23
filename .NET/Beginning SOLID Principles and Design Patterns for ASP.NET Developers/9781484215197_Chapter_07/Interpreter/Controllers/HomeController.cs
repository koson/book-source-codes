using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Interpreter.Core;
using Microsoft.AspNet.Hosting;
using Newtonsoft.Json;

namespace Interpreter.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment env;

        public HomeController(IHostingEnvironment env)
        {
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExecuteJSON(List<IFormFile> files)
        {
            foreach (IFormFile file in files)
            {
                //save file
                ContentDispositionHeaderValue header = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                string fileName = header.FileName;
                fileName = fileName.Trim('"');
                fileName = Path.GetFileName(fileName);
                string filePath = env.MapPath("BatchFiles\\" + fileName);
                file.SaveAs(filePath);

                //load file
                List<ApiCall> apiCalls = JsonConvert.DeserializeObject<List<ApiCall>>(System.IO.File.ReadAllText(filePath));
                InterpreterContext context = new InterpreterContext();
                context.AssemblyStore = env.MapPath("AssemblyStore");
                context.BasePath = env.WebRootPath;

                //execute commands
                foreach (ApiCall call in apiCalls)
                {
                    call.Interpret(context);
                }
            }
            ViewBag.Message = "API calls from the file(s) have been executed!";
            return View("Index");
        }

       
    }
}
