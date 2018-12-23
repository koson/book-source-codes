// BuildASentence - the following program constructs sentences
//                  by concatenating user input until the user
//                  enters one of the termination characters -
//                  this program shows when you need to look for
//                  string equality
using System;
namespace BuildASentence
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Each line you enter will be "
                      + "added to a sentence until you "
                      + "enter EXIT or QUIT");
      // ask the user for input; continue concatenating
      // the phrases input until the user enters exit or
      // quit (start with a null sentence)
      string sSentence = "";
      for (; ; )
      {
        // get the next line
        Console.WriteLine("Enter a string ");
        string sLine = Console.ReadLine();
        // exit the loop if it's a terminator
        if (IsTerminateString(sLine))
        {
          break;
        }
        // otherwise, add it to the sentence
        sSentence = String.Concat(sSentence, sLine);
        // let the user know how she's doing
        Console.WriteLine("\nYou've entered: {0}", sSentence);
      }
      Console.WriteLine("\nTotal sentence:\n{0}", sSentence);
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
    // IsTerminateString - return a true if the source
    // string is equal to any of the termination strings
    public static bool IsTerminateString(string source)
    {
      string[] sTerms = { "EXIT", "exit", "QUIT", "quit" };
      // compare the string entered to each of the
      // legal exit commands
      foreach (string sTerm in sTerms)
      {
        // return a true if you have a match
        if (String.Compare(source, sTerm) == 0)
        {
          return true;
        }
      }
      return false;
    }
  }
}
