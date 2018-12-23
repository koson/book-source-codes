// CalculateInterest - 
//              calculate the interest amount
//              paid on a given principal. If either
//              the principal or the interest rate is
//              negative, then generate an error message.
using System;

namespace CalculateInterest
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // prompt user to enter source principal
      Console.Write("Enter principal:");
      string sPrincipal = Console.ReadLine();
      decimal mPrincipal = Convert.ToDecimal(sPrincipal);

      // make sure that the principal is not negative
      if (mPrincipal < 0)
      {
        Console.WriteLine("Principal cannot be negative");
        mPrincipal = 0;
      }

      // enter the interest rate
      Console.Write("Enter interest:");
      string sInterest = Console.ReadLine();
      decimal mInterest = Convert.ToDecimal(sInterest);

      // make sure that the interest is not negative either
      if (mInterest < 0)
      {
        Console.WriteLine("Interest cannot be negative");
        mInterest = 0;
      }

      // calculate the value of the principal
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

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

