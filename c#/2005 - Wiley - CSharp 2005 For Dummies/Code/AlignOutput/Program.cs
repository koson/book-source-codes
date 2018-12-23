// AlignOutput - left justify and align a set of strings
//               to improve the appearance of program output
namespace AlignOutput
{
  using System;

  class Program
  {
    public static void Main(string[] args)
    {
      string[] names = {"Christa  ",
                        "  Sarah",
                        "Jonathan",
                        "Sam",
                        " Schmekowitz "};

      // first output the names as they start out
      Console.WriteLine("The following names are of "
                        + "different lengths");
      
      foreach(string s in names)
      {
        Console.WriteLine("This is the name '{0}' before", s);
      }
      Console.WriteLine();
      
      // this time, fix the strings so they are
      // left justified and all the same length
      string[] sAlignedNames = TrimAndPad(names);

      // finally output the resulting padded, justified strings
      Console.WriteLine("The following are the same names "
                      + "normalized to the same length");
      foreach(string s in sAlignedNames)
      {
        Console.WriteLine("This is the name '{0}' afterwards", s);
      }

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // TrimAndPad - given an array of strings, trim whitespace from
    //              both ends and then repad the strings to align
    //              them with the longest member
    public static string[] TrimAndPad(string[] strings)
    {
      // copy the source array into an array that you can manipulate
      string[] stringsToAlign = new String[strings.Length];
      
      // first remove any unnecessary spaces from either
      // end of the names
      for(int i = 0; i < stringsToAlign.Length; i++)
      {
        stringsToAlign[i] = strings[i].Trim();
      }
      
      // now find the length of the longest string so that
      // all other strings line up with that string
      int nMaxLength = 0;
      foreach(string s in stringsToAlign)
      {
        if (s.Length > nMaxLength)
        {
          nMaxLength = s.Length;
        }
      }

      // finally justify all the strings to the length
      // of the maximum string
      for(int i = 0; i < stringsToAlign.Length; i++)
      {
        stringsToAlign[i] = stringsToAlign[i].PadRight(nMaxLength + 1);    
      }

      // return the result to the caller
      return stringsToAlign;
    }
  }
}

