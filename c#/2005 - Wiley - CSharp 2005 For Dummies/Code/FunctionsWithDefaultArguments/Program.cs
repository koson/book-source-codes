using System;

// FunctionsWithDefaultArguments - provide variations of the same
//                      function, some with default arguments by
//                      overloading the function name

namespace FunctionsWithDefaultArguments
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // access the member function
      Console.WriteLine("{0}", DisplayRoundedDecimal(12.345678M, 3));

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // DisplayRoundedDecimal - convert a decimal value into a string
    //                         with the specified number of signficant
    //                         digits
    public static string DisplayRoundedDecimal(decimal mValue,
                                        int nNumberOfSignificantDigits)
    {
      // first round the number off to the specified number
      // of significant digits
      decimal mRoundedValue =
                     decimal.Round(mValue,
                                   nNumberOfSignificantDigits);
                                   
      // convert that to a string
      string s = Convert.ToString(mRoundedValue);
      
      return s;
    }
    public static string DisplayRoundedDecimal(decimal mValue)
    {
      // invoke DisplayRoundedDecimal(decimal, int) specifying
      // the default number of digits
      string s = DisplayRoundedDecimal(mValue, 2);
      return s;
    }
  }
}

