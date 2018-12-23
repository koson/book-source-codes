// RemoveWhiteSpace - define a RemoveSpecialChars() function
//                    which can remove any of a set of chars
//                    from a given string. Use this function
//                    to remove whitespace from a sample string
namespace RemoveWhiteSpace
{
 using System;

  public class Program
  {
    public static void Main(string[] args)
    {
      // define the white space characters
      char[] cWhiteSpace = {' ', '\n', '\t'};

      // start with a string embedded with whitespace
      string s = " this is a\nstring"; // contains spaces & newline
      Console.WriteLine("before:" + s);

      // output the string with the whitespace missing
      Console.WriteLine("after:" + RemoveSpecialChars(s, cWhiteSpace));

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // RemoveSpecialChars - remove every occurrence of the specified 
    //                      characters from the string
    public static string RemoveSpecialChars(string sInput,
                                            char[] cTargets)
    {
      // sOutput will contain the eventual output information
      string sOutput = sInput;

      // start looking for the white space characters
      for(;;)
      {
        // find the offset of the character; exit the loop
        // if there are no more
        int nOffset = sOutput.IndexOfAny(cTargets);
        if (nOffset == -1)
        {
          break;
        }

        // break the string into the part prior to the
        // character and the part after the character
        string sBefore = sOutput.Substring(0, nOffset);
        string sAfter  = sOutput.Substring(nOffset + 1);

        // now put the two substrings back together with the
        // character in the middle missing
        sOutput = String.Concat(sBefore, sAfter);
      }

      return sOutput;
    }
  }
}

