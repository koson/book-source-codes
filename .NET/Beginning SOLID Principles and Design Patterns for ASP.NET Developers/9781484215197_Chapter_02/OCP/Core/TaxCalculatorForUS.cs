using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCP.Core
{
    public class TaxCalculatorForUS: ICountryTaxCalculator
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalDeduction { get; set; }

        public decimal CalculateTaxAmount()
        {
            decimal taxableIncome = TotalIncome - TotalDeduction;
            return taxableIncome * 30 / 100;
        }
    }
}
