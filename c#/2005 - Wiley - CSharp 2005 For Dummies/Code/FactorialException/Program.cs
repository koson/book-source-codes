// FactorialException - create a factorial program that reports illegal 
//                      arguments to Factorial() using an Exception
using System;

namespace FactorialException
{
  // MyMathFunctions - a collection of mathematical functions
  //                   we created (it's not much to look at yet)
  public class MyMathFunctions
  {
    // Factorial - return the factorial of the provided value 
    public static double Factorial(int nValue)
    {
      // don't allow negative numbers
      if (nValue < 0)
      {
        // report negative argument
        string s = String.Format(
             "Illegal negative argument to Factorial {0}", nValue);
             
        throw new Exception(s);
      }

      // begin with an "accumulator" of 1
      double dFactorial = 1.0;
  
      // loop from nValue down to one, each time multiplying
      // the previous accumulator value by the result
      do
      {
        dFactorial *= nValue;
      } while(--nValue > 1);
  
      // return the accumulated value
      return dFactorial;
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        // call factorial in a loop from 6 down to -6.
        for (int i = 6; i > -6; i--)
        {
          // calculate the factorial of the number
          double dFactorial = MyMathFunctions.Factorial(i);

          // display the result of each pass
          Console.WriteLine("i = {0}, factorial = {1}",
                            i, MyMathFunctions.Factorial(i));
        }
      }
      catch(Exception e)
      {
        Console.WriteLine("Fatal error:");
        Console.WriteLine(e.ToString());
      }
  
      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

