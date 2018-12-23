using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OCP.Models;
using OCP.Core;


namespace OCP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Calculate(IncomeDetails obj)
        {
            ICountryTaxCalculator t = null;
            switch(obj.Country)
            {
                case "USA":
                    t = new TaxCalculatorForUS();
                    break;
                case "UK":
                    t = new TaxCalculatorForUK();
                    break;
                case "IN":
                    t = new TaxCalculatorForIN();
                    break;
            }
            t.TotalIncome = obj.TotalIncome;
            t.TotalDeduction = obj.TotalDeduction;
            TaxCalculator cal = new TaxCalculator();
            ViewBag.TotalTax = cal.Calculate(t);
            return View("Index", obj);
        }



        //[HttpPost]
        //public IActionResult IndexWrong(IncomeDetails obj)
        //{
        //    decimal taxableIncome = obj.TotalIncome - obj.TotalDeductions;
        //    TaxCalculator t = new TaxCalculator();
        //    ViewBag.TotalTax = t.CalculateWrong(taxableIncome, obj.Country);
        //    return View("Index", obj);
        //}
    }
}
