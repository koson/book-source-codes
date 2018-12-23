// DisplayXWithNestedLoops - use a pair of nested loops to
//                           create an X pattern
using System;

namespace DisplayXWithNestedLoops
{
  public class Program
  {
    public static void Main(string[] args)
    {
      int nConsoleWidth = 40;
      
      // iterate through the rows of the "box"
      for(int nRowNum = 0;
          nRowNum < nConsoleWidth;
          nRowNum += 2)
      {
        // now iterate through the columns
        for (int nColumnNum = 0; nColumnNum < nConsoleWidth; nColumnNum++)
        {
          // the default character is a space
          char c = ' ';
          
          // if the column number and row number are the same...
          if (nColumnNum == nRowNum)
          {
            // ...replace the space with a backslash
            c = '\\';
          }
          
          // if the column is on the opposite side of the row...
          int nMirrorColumn = nConsoleWidth - nRowNum;
          if (nColumnNum == nMirrorColumn)
          {
            // ...replace the space with a slash
            c = '/';
          }
          
          // output whatever character at the current
          // row and column
          Console.Write(c);
        }
        Console.WriteLine();
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

