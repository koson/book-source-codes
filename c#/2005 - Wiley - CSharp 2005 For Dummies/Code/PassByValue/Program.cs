// PassByValue - demonstrate pass-by-value semantics
using System;

namespace PassByValue
{
  public class Program
  {
    // Update - try to modify the values of the arguments
    //          passed to it; note that you can declare
    //          functions in any order in a class
    public static void Update(int i, double d)
    {
      i = 10;
      d = 20.0;
    }
    
    public static void Main(string[] args)
    {
      // declare two variables and initialize them
      int i = 1;
      double d = 2.0;
      Console.WriteLine("Before the call to Update(int, double):");
      Console.WriteLine("i = " + i + ", d = " + d);

      // invoke the function
      Update(i, d);

      // notice that the values 1 and 2.0 have not changed
      Console.WriteLine("After the call to Update(int, double):");
      Console.WriteLine("i = " + i + ", d = " + d);

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

