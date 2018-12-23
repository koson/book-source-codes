// FactorialWithError - create and exercise a factorial
//                      function that has no error checks at all
using System;

namespace FactorialWithError
{
  // MyMathFunctions - a collection of mathematical functions
  //                   we created (it's not much to look at yet)
  public class MyMathFunctions
  {
    // Factorial - return the factorial of the provided value 
    public static double Factorial(double dValue)
    {
      // begin with an "accumulator" of 1
      double dFactorial = 1.0;
  
      // loop from nValue down to one, each time multiplying
      // the previous accumulator value by the result
      do
      {
        dFactorial *= dValue;

        dValue -= 1.0;
      } while(dValue > 1);
  
      // return the accumulated value
      return dFactorial;
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {    
      // call factorial in a loop from 6 down to -6.
      for (int i = 6; i > -6; i--)
      {
        // display the result of each pass
        Console.WriteLine("i = {0}, factorial = {1}",
                           i, MyMathFunctions.Factorial(i));
      }
  
      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

