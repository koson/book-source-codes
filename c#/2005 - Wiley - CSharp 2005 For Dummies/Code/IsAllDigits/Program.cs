// IsAllDigits - demonstrate the IsAllDigits method
using System;

namespace IsAllDigits
{
  class Program
  {
    public static void Main(string[] args)
    {
      // input a string from the keyboard
      Console.WriteLine("Enter an integer number");
      string s = Console.ReadLine();
      
      // first check to see if this could be a number
      if (!IsAllDigits(s))
      {
        Console.WriteLine("Hey! That isn't a number");
      }
      else
      {
        // convert the string into an integer
        int n = Int32.Parse(s);
        
        // now write out the number times 2
        Console.WriteLine("2 * {0} = {1}", n, 2 * n);
      }
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
      string s = sRaw.Trim();  // ignore whitespace on either side
      if (s.Length == 0)
      {
        return false;
      }
    
      // loop through the string
      for(int index = 0; index < s.Length; index++)
      {
        // a non-digit indicates that the string
        // is probably not a number
        if (Char.IsDigit(s[index]) == false)
        {
          return false;
        }
      }
    
      // no non-digits found; it's probably OK
      return true;
    }
  }
}

