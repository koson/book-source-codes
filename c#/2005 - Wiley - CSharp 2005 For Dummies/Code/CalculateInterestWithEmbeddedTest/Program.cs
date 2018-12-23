// CalculateInterestWithEmbeddedTest -
//              calculate the interest amount
//              paid on a given principal. If either
//              the principal or the interest rate is
//              negative, then generate an error message
//              and don't proceed with the calculation.
using System;

namespace CalculateInterestWithEmbeddedTest
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // define a maximum interest rate
      int nMaximumInterest = 50;

      // prompt user to enter source principal
      Console.Write("Enter principal:");
      string sPrincipal = Console.ReadLine();
      decimal mPrincipal = Convert.ToDecimal(sPrincipal);
    
      // if the principal is negative...
      if (mPrincipal < 0)
      {
        //...generate an error message...
        Console.WriteLine("Principal cannot be negative");
      }
      else
      {
        // ...otherwise, enter the interest rate
        Console.Write("Enter interest:");
        string sInterest = Console.ReadLine();
        decimal mInterest = Convert.ToDecimal(sInterest);
    
        // if the interest is negative or too large...
        if (mInterest < 0 || mInterest > nMaximumInterest)
        {
          // ...generate an error message as well
          Console.WriteLine("Interest cannot be negative " +
                            "or greater than " + nMaximumInterest);
          mInterest = 0;
        }
        else
        {
          // both the principal and the interest appear to be
          // legal; calculate the value of the principal
          // plus interest
          decimal mInterestPaid;
          mInterestPaid = mPrincipal * (mInterest / 100);
    
          // now calculate the total
          decimal mTotal = mPrincipal + mInterestPaid;
    
          // output the result
          Console.WriteLine();  // skip a line
          Console.WriteLine("Principal     = " + mPrincipal);
          Console.WriteLine("Interest      = " + mInterest + "%");
          Console.WriteLine();
          Console.WriteLine("Interest paid = " + mInterestPaid);
          Console.WriteLine("Total         = " + mTotal);
        }
      }
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

