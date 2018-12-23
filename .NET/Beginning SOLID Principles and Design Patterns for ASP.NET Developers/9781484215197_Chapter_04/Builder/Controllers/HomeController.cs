using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Builder.Core;


namespace Builder.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.InitData();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Build(string usagetype)
        {
            IComputerBuilder builder = null;
            switch(usagetype)
            {
                case "home":
                    builder = new HomeComputerBuilder();
                    break;
                case "office":
                    builder = new OfficeComputerBuilder();
                    break;
                case "development":
                    builder = new DevelopmentComputerBuilder();
                    break;
            }
            ComputerAssembler assembler = new ComputerAssembler(builder);
            Computer computer = assembler.AssembleComputer();
            return View("Success", computer);
        }
    }
}
