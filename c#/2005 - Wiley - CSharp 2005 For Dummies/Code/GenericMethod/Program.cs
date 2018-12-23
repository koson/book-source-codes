// GenericMethod - a method that can process different data types
using System;
namespace GenericMethod
{
  class Program
  {
    // Main - tests two versions of a generic method; one lives in
    //        this class on same level as Main(); other lives in
    //        a generic class
    static void Main(string[] args)
    {
      Console.WriteLine("Generic method in non-generic class:\n");

      Console.WriteLine("\tFirst, test it for int arguments");
      int nOne = 1;
      int nTwo = 2;
      Console.WriteLine("\t\tBefore swap: nOne = {0}, nTwo = {1}", nOne, nTwo);
      // next line instantiates Swap for ints and calls the function 
      Swap<int>(ref nOne, ref nTwo);
      Console.WriteLine("\t\tAfter swap: nOne = {0}, nTwo = {1}", nOne, nTwo);

      Console.WriteLine("\tSecond, test it for string arguments");
      string sOne = "one";
      string sTwo = "two";
      Console.WriteLine("\t\tBefore swap: sOne = {0}, sTwo = {1}", sOne, sTwo);
      Swap<string>(ref sOne, ref sTwo); // generic instantiation for string
      Console.WriteLine("\t\tAfter swap: sOne = {0}, sTwo = {1}", sOne, sTwo);

      Console.WriteLine("\nGeneric method in a generic class:\n");

      Console.WriteLine("\tFirst, test it for int and call");
      Console.WriteLine("\t  GenericClass.Swap with int arguments");
      nOne = 1;
      nTwo = 2;
      GenericClass<int> intClass = new GenericClass<int>();
      Console.WriteLine("\t\tBefore swap: nOne = {0}, nTwo = {1}", nOne, nTwo);
      intClass.Swap(ref nOne, ref nTwo);
      Console.WriteLine("\t\tAfter swap: nOne = {0}, nTwo = {1}", nOne, nTwo);

      Console.WriteLine("\tSecond, test it for string and call");
      Console.WriteLine("\t  GenericClass.Swap with string arguments");
      sOne = "one";
      sTwo = "two";
      GenericClass<string> strClass = new GenericClass<string>();
      Console.WriteLine("\t\tBefore swap: sOne = {0}, sTwo = {1}", sOne, sTwo);
      strClass.Swap(ref sOne, ref sTwo);
      Console.WriteLine("\t\tAfter swap: sOne = {0}, sTwo = {1}", sOne, sTwo);

      // wait for the user to acknowledge
      Console.WriteLine("\nPress Enter to terminate...");
      Console.Read();
    } // end Main

    //static Swap - this is a generic method in a non-generic class
    public static void Swap<T>(ref T leftSide, ref T rightSide)
    {
      T temp;
      temp = leftSide;
      leftSide = rightSide;
      rightSide = temp;
    }
  }

  //GenericClass - a generic class with its own Swap method
  class GenericClass<T>
  {
    //Swap - this method is generic because it takes a T parameter; 
    //       note that we can't use Swap<T> or we get a
    //       compiler warning about duplicating the
    //       parameter on the class itself
    public void Swap(ref T leftSide, ref T rightSide)
    {
      T temp;
      temp = leftSide;
      leftSide = rightSide;
      rightSide = temp;
    }
  }
}
