// CalculateInterestTableWithFunctions - generate an interest table
//                                  much like the other interest table
//                                  programs, but this time using a
//                                  reasonable division of labor among
//                                  several functions.
using System;
namespace CalculateInterestTableWithFunctions
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // input the data you will need to create the table
      decimal mPrincipal = 0;
      decimal mInterest = 0;
      decimal mDuration = 0;
      InputInterestData(ref mPrincipal,
                        ref mInterest,
                        ref mDuration);
                        

      // verify the data by mirroring it back to the user
      Console.WriteLine();  // skip a line
      Console.WriteLine("Principal     = " + mPrincipal);
      Console.WriteLine("Interest      = " + mInterest + "%");
      Console.WriteLine("Duration      = " + mDuration + " years");
      Console.WriteLine();

      // finally, output the interest table
      OutputInterestTable(mPrincipal, mInterest, mDuration);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // InputInterestData - retrieve from the keyboard the
    //                     principal, interest and duration
    //                     information needed to create the
    //                     future value table
    public static void InputInterestData(ref decimal mPrincipal,
                                         ref decimal mInterest,
                                         ref decimal mDuration)
    {
      // retrieve the principal
      mPrincipal = InputPositiveDecimal("principal");
      
      // now enter the interest rate
      mInterest = InputPositiveDecimal("interest");

      // finally, the duration
      mDuration = InputPositiveDecimal("duration");
    }


    // InputPositiveDecimal - return a positive decimal number from
    //                        the keyboard.
    public static decimal InputPositiveDecimal(string sPrompt)
    {
      // keep trying until the user gets it right
      while(true)
      {
        // prompt the user for input
        Console.Write("Enter " + sPrompt + ":");

        // retrieve a decimal value from the keyboard
        string sInput = Console.ReadLine();
        decimal mValue = Convert.ToDecimal(sInput);
       
        // exit the loop if the value entered is correct
        if (mValue >= 0)
        {
          // return the valid decimal value entered by the user
          return mValue;
        }
       
        // otherwise, generate an error on incorrect input
        Console.WriteLine(sPrompt + " cannot be negative");
        Console.WriteLine("Try again");
        Console.WriteLine();
      }
    }
    
    // OutputInterestTable - given the principal and interest
    //                       generate a future value table for
    //                       the number of periods indicated in
    //                       mDuration.
    public static void OutputInterestTable(decimal mPrincipal,
                                           decimal mInterest,
                                           decimal mDuration)
    {
      for (int nYear = 1; nYear <= mDuration; nYear++)
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
      }
    }
    
   }
}

