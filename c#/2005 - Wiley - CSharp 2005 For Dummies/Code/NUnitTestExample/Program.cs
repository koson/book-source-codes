// NUnitTestExample - demonstrate writing "unit tests"
//                   for a C# program, to be run with
//                   the NUnit test tool
using System;
namespace NUnitTestExample
{
   class Program
   {
      // here's the start of the program we're testing
      static void Main(string[] args)
      {
        Console.Write("Run the tests in this program ");
        Console.WriteLine("from NUnit instead of Visual Studio");
        // wait for user to acknowledge the results
         Console.WriteLine("Press Enter to terminate...");
         Console.Read();
      }
      // the rest of your program ...
      // here's a method you want to test
      // TrimAndPad() - given an array of strings, trim
      //              whitespace from both ends and then
      //              repad the strings to align them
      //              with the longest member
      public static string[] TrimAndPad(string[] strings)
      {
         // copy the source array into an array that you can
         // manipulate
         string[] stringsToAlign = new String[strings.Length];
         // first remove any unnecessary spaces from either
         // end of the names
         for (int i = 0; i < stringsToAlign.Length; i++)
         {
            stringsToAlign[i] = strings[i].Trim();
         }
         // now find the length of the longest string so that
         // all other strings line up with that string
         int nMaxLength = 0;
         foreach (string s in stringsToAlign)
         {
            if (s.Length > nMaxLength)
            {
               nMaxLength = s.Length;
            }
         }
         // finally justify all the strings to the length
         // of the maximum string
         for (int i = 0; i < stringsToAlign.Length; i++)
         {
            stringsToAlign[i] =
              stringsToAlign[i].PadRight(nMaxLength + 1);
         }
         // return the result to the caller
         return stringsToAlign;
      }
   }
}
