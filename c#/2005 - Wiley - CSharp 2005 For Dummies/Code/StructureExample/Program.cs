// StructureExample - demonstrate the various properties
//                    of a struct object
using System;
using System.Collections;

namespace StructureExample
{
  public interface IDisplayable
  {
    string ToString();
  }

  // a struct can implement an interface
  public struct Test : IDisplayable
  {
    // a struct can have both object and
    // class (static) data members; 
    // static members may have initializers
    private int n;
    private static double d = 20.0;

    // a constructor can be used to initialize
    // the data members of a struct
    public Test(int n)
    {
      this.n = n;
    }

    // a struct may have both object and class
    // (static) properties
    public int N
    {
      get { return n;}
      set { n = value; }
    }

    public static double D
    {
      get { return d; }
      set { d = value; }
    }

    // a struct may have methods
    public void ChangeMethod(int nNewValue, double dNewValue)
    {
      n = nNewValue;
      d = dNewValue;
    }

    // ToString - overrides the ToString method in object
    //            and implements the IDisplayable interface
    override public string ToString()
    {
      return string.Format("({0:N}, {1:N})", n, d);
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      // create a Test object
      Test test = new Test(10);
      Console.WriteLine("Initial value of test");
      OutputFunction(test);

      // try to modify the test object by passing it
      // as an argument
      ChangeValueFunction(test, 100, 200.0);
      Console.WriteLine("Value of test after calling" + 
                         " ChangeValueFunction(100, 200.0)");
      OutputFunction(test);

      // try to modify the test object by passing it
      // as an argument
      ChangeReferenceFunction(ref test, 100, 200.0);
      Console.WriteLine("Value of test after calling" + 
                     " ChangeReferenceFunction(100, 200.0)");
      OutputFunction(test);

      // a method can modify the object
      test.ChangeMethod(1000, 2000.0);
      Console.WriteLine("Value of test after calling" + 
                              " ChangeMethod(1000, 2000.0)");
      OutputFunction(test);


      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // ChangeValueFunction - pass the struct by value
    public static void ChangeValueFunction(Test t, 
                             int newValue, double dNewValue)
    {
      t.N = newValue;
      Test.D = dNewValue;
    }

    // ChangeReferenceFunction - pass the struct by reference
    public static void ChangeReferenceFunction(ref Test t, 
                             int newValue, double dNewValue)
    {
      t.N = newValue;
      Test.D = dNewValue;
    }

    // OutputFunction - outputs any method which implements ToString()
    public static void OutputFunction(IDisplayable id)
    {
      Console.WriteLine("id = {0}", id.ToString());
    }
  }
}

