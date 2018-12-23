using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using UnitOfWork.Core;


namespace UnitOfWork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(Project project, List<TeamMember> members)
        {
            ProjectUnit unit = new ProjectUnit();
            project.ProjectID = Guid.NewGuid();
            unit.ProjectRepository.Insert(project);
            foreach (var item in members)
            {
                item.ProjectID = project.ProjectID;
                unit.TeamRepository.Insert(item);
            }
            unit.CreateProject();
            ViewBag.Message = "Project created successfully!";
            return View("Index");
        }
    }
}
