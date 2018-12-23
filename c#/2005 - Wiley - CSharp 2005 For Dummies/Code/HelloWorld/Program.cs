#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace HelloWorld
{
  class Program
  {
    // This is where our program starts
    static void Main(string[] args)
    {
      // prompt user to enter a name
      Console.WriteLine("Enter your name, please:");

      // now read the name entered
      string sName = Console.ReadLine();

      // greet the user with the name that was entered
      Console.WriteLine("Hello, " + sName);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
