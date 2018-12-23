using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCP.Core
{
    public interface ICountryTaxCalculator
    {
        decimal TotalIncome { get; set; }
        decimal TotalDeduction { get; set; }
        decimal CalculateTaxAmount();
    }
}
