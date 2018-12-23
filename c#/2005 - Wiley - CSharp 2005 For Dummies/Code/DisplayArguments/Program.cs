// DisplayArguments - display the arguments to the
//                    program
using System;

namespace DisplayArguments
{
  class Test  // the class containing Main() doesn't
              // have to be called Program
  {
    public static void Main(string[] args)
    {
      // count the number of arguments
      Console.WriteLine("There are {0} program arguments",
                        args.Length);

      // the arguments are:
      int nCount = 0;
      foreach (string arg in args)
      {
        Console.WriteLine("Argument {0} is {1}",
                          nCount++, arg);
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
