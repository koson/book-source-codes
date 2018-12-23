// PassByReferenceError - demonstrate a potential error situation
//                        when calling a function using reference
//                        arguments
using System;

namespace PassByReferenceError
{
  public class Program
  {
    // Update - try to modify the values of the arguments
    //          passed to it
    public static void DisplayAndUpdate(ref int nVar1, ref int nVar2)
    {
      Console.WriteLine("The initial value of nVar1 is " + nVar1);
      nVar1 = 10;

      Console.WriteLine("The initial value of nVar2 is " + nVar2);
      nVar2 = 20;
    }
    
    public static void Main(string[] args)
    {
      // declare two variables and initialize them
      int n = 1;
      Console.WriteLine("Before the call to Update(ref n, ref n):");
      Console.WriteLine("n = " + n);
      Console.WriteLine();

      // invoke the function
      DisplayAndUpdate(ref n, ref n);

      // notice that n changes in an unexpected way
      Console.WriteLine();
      Console.WriteLine("After the call to Update(ref n, ref n):");
      Console.WriteLine("n = " + n);

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

