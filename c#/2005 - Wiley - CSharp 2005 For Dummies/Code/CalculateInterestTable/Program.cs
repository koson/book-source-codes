// CalculateInterestTable - calculate the interest
//           paid on a given principal over a period
//           of years
using System;
namespace CalculateInterestTable
{
  using System;

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
          // legal; finally, input the number of years
          Console.Write("Enter number of years:");
          string sDuration = Console.ReadLine();
          int nDuration = Convert.ToInt32(sDuration);

          // verify the input
          Console.WriteLine();  // skip a line
          Console.WriteLine("Principal     = " + mPrincipal);
          Console.WriteLine("Interest      = " + mInterest + "%");
          Console.WriteLine("Duration      = " + nDuration + "years");
          Console.WriteLine();


          // now loop through the specified number of years
          int nYear = 1;
          while(nYear <= nDuration)
          {
            // calculate the value of the principal
            // plus interest
            decimal mInterestPaid;
            mInterestPaid = mPrincipal * (mInterest / 100);

            // now calculate the new principal by adding
            // the interest to the previous principal
            mPrincipal = mPrincipal + mInterestPaid;

            // round off the principal to the nearest cent
            mPrincipal = decimal.Round(mPrincipal, 2);

            // output the result
            Console.WriteLine(nYear + "-" + mPrincipal);

            // skip over to next year
            nYear = nYear + 1;
          }
        }
      }
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

