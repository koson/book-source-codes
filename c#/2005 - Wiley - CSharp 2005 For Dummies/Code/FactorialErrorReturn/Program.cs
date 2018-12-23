// FactorialErrorReturn - create a factorial program that returns an 
//                        error indication when something goes wrong
using System;

namespace FactorialErrorReturn
{
  // MyMathFunctions - a collection of mathematical functions
  //                   I created (it's not much to look at yet)
  public class MyMathFunctions
  {
    // the following represent illegal values
    public const int NEGATIVE_NUMBER = -1;
    public const int NON_INTEGER_VALUE = -2;

    // Factorial - return the factorial of the provided value 
    public static double Factorial(double dValue)
    {
      // don't allow negative numbers
      if (dValue < 0)
      {
        return NEGATIVE_NUMBER;
      }

      // check for non-integer
      int nValue = (int)dValue;
      if (nValue != dValue)
      {
        return NON_INTEGER_VALUE;
      }

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
        // calculate the factorial of the number
        double dFactorial = MyMathFunctions.Factorial(i);
        if (dFactorial == MyMathFunctions.NEGATIVE_NUMBER)
        {
          Console.WriteLine
                      ("Factorial() passed a negative number");
          break;
        }

        if (dFactorial == MyMathFunctions.NON_INTEGER_VALUE)
        {
          Console.WriteLine
                      ("Factorial() passed a non-integer number");
          break;
        }

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

