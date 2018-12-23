// ClassLibrary driver program
// File Program.cs in ClassLibraryDriver project
using System;
using ClassLibrary;
namespace ClassLibraryDriver
{
  class Program
  {
    static void Main(string[] args)
    {
      // create a library object and use its methods
      MyLibrary ml = new MyLibrary();
      ml.LibraryFunction1();
      // call its static functions through the class
      int nResult = MyLibrary.LibraryFunction2(27);
      Console.WriteLine(nResult.ToString());
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
