using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Command.Core;

namespace Command.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessEmployee(int employeeid)
        {
            EmployeeManager manager = new EmployeeManager(employeeid);
            Invoker invoker = new Invoker();
            ICommand command = null;

            command = new CreateEmailAccount(manager);
            invoker.Commands.Add(command);
            command = new OrderVisitingCards(manager);
            invoker.Commands.Add(command);
            command = new PrepareIdentityCard(manager);
            invoker.Commands.Add(command);

            invoker.Execute();

            ViewBag.Message = $"Commands executed for employee #{employeeid}";
            return View("Index", employeeid);
        }
    }
}
