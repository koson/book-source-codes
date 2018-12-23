// CalculateInterestTableMoreForgiving - calculate the interest
//           paid on a given principal over a period
//           of years. This version gives the user multiple chances
//           to input the legal principal and interest.
using System;
namespace CalculateInterestTableMoreForgiving
{
  using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      // define a maximum interest rate
      int nMaximumInterest = 50;
      
      // prompt user to enter source principal; keep prompting
      // until you get the correct value
      decimal mPrincipal;
      while(true)
      {
        Console.Write("Enter principal:");
        string sPrincipal = Console.ReadLine();
        mPrincipal = Convert.ToDecimal(sPrincipal);
       
        // exit if the value entered is correct
        if (mPrincipal >= 0)
        {
          break;
        }
       
        // generate an error on incorrect input
        Console.WriteLine("Principal cannot be negative");
        Console.WriteLine("Try again");
        Console.WriteLine();
      }
      
      // now enter the interest rate
      decimal mInterest;
      while(true)
      {
        Console.Write("Enter interest:");
        string sInterest = Console.ReadLine();
        mInterest = Convert.ToDecimal(sInterest);

        // don't accept interest that is negative or too large...
        if (mInterest >= 0 && mInterest <= nMaximumInterest)
        {
          break;
        }
        
        // ...generate an error message as well
        Console.WriteLine("Interest cannot be negative " +
                          "or greater than " + nMaximumInterest);
        Console.WriteLine("Try again");
        Console.WriteLine();
      }

      // both the principal and the interest appear to be
      // legal; finally, input the number of years
      Console.Write("Enter number of years:");
      string sDuration = Console.ReadLine();
      int nDuration = Convert.ToInt32(sDuration);

      // verify the input
      Console.WriteLine();  // skip a line
      Console.WriteLine("Principal     = " + mPrincipal);
      Console.WriteLine("Interest      = " + mInterest + "%");
      Console.WriteLine("Duration      = " + nDuration + " years");
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

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

