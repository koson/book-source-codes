// ParseSequenceWithSplit - input a series of numbers 
//                  separated by commas, parse them into  
//                  integers and output the sum
namespace ParseSequenceWithSplit
{
  using System;

  class Program
  {
    public static void Main(string[] args)
    {
      // prompt the user to input a sequence of numbers
      Console.WriteLine(
           "Input a series of numbers separated by commas:");

      // read a line of text
      string input = Console.ReadLine();
      Console.WriteLine();

      // now convert the line into individual segments
      // based upon either commas or spaces
      char[] cDividers = {',', ' '};
      string[] segments = input.Split(cDividers);

      // convert each segment into a number
      int nSum = 0;
      foreach(string s in segments)
      {
        // (skip any empty segments)
        if (s.Length > 0)
        {
          // skip strings that aren't numbers
          if (IsAllDigits(s))
          {
            // convert the string into a 32-bit int
            int num = Int32.Parse(s);
            Console.WriteLine("Next number = {0}", num);

            // add this number into the sum
            nSum += num;
          }
        }
      }

      // output the sum
      Console.WriteLine("Sum = {0}", nSum);
      
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // IsAllDigits - return a true if all of the characters
    //               in the string are digits
    public static bool IsAllDigits(string sRaw)
    {
      // first get rid of any benign characters
      // at either end; if there's nothing left
      // then we don't have a number
      string s = sRaw.Trim();
      if (s.Length == 0)
      {
        return false;
      }

      // loop through the string
      for(int index = 0; index < s.Length; index++)
      {
        // a non-digit indicates that the string
        // probably is not a number
        if (Char.IsDigit(s[index]) == false)
        {
          return false;
        }
      }

      // no non-digit found; it's probably OK
      return true;
    }
  }
}

