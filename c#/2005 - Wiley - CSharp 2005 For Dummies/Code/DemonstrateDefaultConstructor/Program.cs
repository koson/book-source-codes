// DemonstrateDefaultConstructor - demonstrate how default constructors
//                         work; create a class with a constructor
//                         and then step through a few scenarios
using System;

namespace DemonstrateDefaultConstructor
{
  // MyObject - create a class with a noisy constructor
  //            and an internal data object
  public class MyObject
  {
    // this data member is a property of the class
    static MyOtherObject staticObj = new MyOtherObject();

    // this data member is a property of the object
    MyOtherObject dynamicObj;
    // constructor (a real chatterbox)
    public MyObject()
    {
      Console.WriteLine("MyObject constructor starting");
      Console.WriteLine("(Static data member constructed before this constructor)");
      Console.WriteLine("Now create nonstatic data member dynamically:");
      dynamicObj = new MyOtherObject();
      Console.WriteLine("MyObject constructor ending");
    }
  }

  // MyOtherObject - this class also has a noisy constructor
  //                 but no internal members
  public class MyOtherObject
  {
    public MyOtherObject()
    {
      Console.WriteLine("MyOtherObject constructing");
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Main() starting");
      Console.WriteLine("Creating a local MyObject in Main():");
      MyObject localObject = new MyObject();

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
