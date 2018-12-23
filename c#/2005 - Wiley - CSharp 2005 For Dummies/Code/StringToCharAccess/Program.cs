// StringToCharAccess - access the characters in a string
//                      as if the string were an array
using System;

namespace StringToCharAccess
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // read a string in from the keyboard
      Console.WriteLine("Input some random character string."
                      + " Make sure it's completely random");
      string sRandom = Console.ReadLine();
      Console.WriteLine();

      // first output as a string
      Console.WriteLine("When output as a string: " + sRandom);
      Console.WriteLine();

      // now output as a series of characters
      Console.Write("When output using the foreach: ");
      foreach(char c in sRandom)
      {
        Console.Write(c);
      }
      Console.WriteLine(); // terminate the line
      
      // put a blank line divider
      Console.WriteLine();

      // now output as a series of characters
      Console.Write("When output using the for: ");
      for(int i = 0; i < sRandom.Length; i++)
      {
        Console.Write(sRandom[i]);
      }
      Console.WriteLine(); // terminate the line

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

