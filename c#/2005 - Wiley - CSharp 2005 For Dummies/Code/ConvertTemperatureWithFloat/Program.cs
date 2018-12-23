// ConvertTemperatureWithFloat - 
//      this program prompts the user for
//      a temperature which is assumed to
//      be in degrees Fahrenheit. The program
//      converts the temperature to degrees
//      Celsius and outputs the result.
//      This version of the program avoids
//      roundoff error by using floating point
//      arithmetic.
using System;
namespace ConvertTemperatureWithFloat
{

    public class Program
    {
        public static int Main(string[] args)
        {
            // prompt user to enter temperature
            Console.Write("Enter temp in degrees Fahrenheit:");

            // read the number entered
            string sFahr = Console.ReadLine();
            double dFahr;
            dFahr = Convert.ToDouble(sFahr);

            // convert that temperature into degrees Celsius
            double dCelsius;
            dCelsius = (dFahr - 32.0) * (5.0 / 9.0 );

            // output the result
            Console.WriteLine("Temperature in degrees Celsius = "
                             + dCelsius);

            // wait for user to acknowledge the results
            Console.WriteLine("Press Enter to terminate...");
            Console.Read();
            return 0;
        }
    }
}
