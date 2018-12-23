// ConvertTemperatureWithRoundOff - 
//      this program prompts the user for
//      a temperature which is assumed to
//      be in degrees Fahrenheit. The program
//      converts the temperature to degrees
//      Celsius and outputs the result;
//      however, the use of integer roundoff
//      causes the program to output incorrect
//      results

using System;
namespace ConvertTemperatureWithRoundOff
{
  class Program
  {
    static void Main(string[] args)
    {
      // prompt user to enter temperature
      Console.Write("Enter temp in degrees Fahrenheit:");

      // read the number entered
      string sFahr = Console.ReadLine();
      int nFahr;
      nFahr = Convert.ToInt32(sFahr);

      // convert that temperature into degrees Celsius
      int nCelsius;
      nCelsius = (nFahr - 32) * (5 / 9);

      // output the result
      Console.WriteLine(
          "Incorrect temperature in degress Celsius = "
        + nCelsius);

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
