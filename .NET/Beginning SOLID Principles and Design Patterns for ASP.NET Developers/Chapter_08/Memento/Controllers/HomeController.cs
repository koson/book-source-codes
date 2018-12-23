using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Memento.Core;

namespace Memento.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return View(db.Questions.ToList());
            }
        }

        [HttpPost]
        public IActionResult ProcessForm(List<int> question,List<string> answer,string submit,string save,string restore)
        {
            SurveyState state = new SurveyState();
            state.Questions = question;
            state.Answers = answer;
            Survey survey = new Survey(state);

            if (submit != null)
            {
                Caretaker.Snapshot = null;
                survey.Submit();
                ViewBag.Message = "Survey data submitted!";
            }
            if (save != null)
            {
                Caretaker.Snapshot = survey.CreateSnapshot();
                ViewBag.Message = "Snapshot created!";
            }

            if (restore!=null)
            {
                survey.RestoreSnapshot(Caretaker.Snapshot);
                ViewBag.Message = "Survey restored!";
            }

            ViewBag.Answers = survey.GetAnswers();

            using (AppDbContext db = new AppDbContext())
            {
                List<Question> model = db.Questions.ToList();
                return View("Index", model);
            }
        }

    }
}
