// RemoveWhiteSpaceWithSplit - get rid of special characters in
//                        a string like RemoveWhiteSpace program 
//                        but use split this time
namespace RemoveWhiteSpace
{
 using System;

  public class Program
  {
    public static void Main(string[] strings)
    {
      // define the white space characters
      char[] cWhiteSpace = {' ', '\n', '\t'};

      // start with a string embedded with whitespace
      string s = " this is a\nstring";
      Console.WriteLine("before:" + s);

      // output the string with the whitespace missing
      Console.WriteLine("after:" +
                    RemoveSpecialChars(s, cWhiteSpace));

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // RemoveSpecialChars - remove every occurrence of the
    //                      specified character from the string
    public static string RemoveSpecialChars(string sInput,
                                            char[] cTargets)
    {
      // split the input string up using the target
      // characters as the delimiters
      string[] sSubStrings = sInput.Split(cTargets);

      // sOutput will contain the eventual output information
      string sOutput = "";

      // loop through the substrings originating from the split
      foreach(string subString in sSubStrings)
      {
        sOutput = String.Concat(sOutput, subString);
      }
      return sOutput;
    }
  }
}

