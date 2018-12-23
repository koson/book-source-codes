// DemonstrateDefaultConstructor - demonstrate how default constructors
//                         work; create a class with a constructor
//                         and then step through a few scenarios
using System;

namespace DemonstrateConstructorWithInitializer
{
  // MyObject - create a class with a noisy constructor
  //            and an internal object
  public class MyObject
  {
    // this member is a property of the class
    static MyOtherObject staticObj = new MyOtherObject();

    // this member is a property of the object
    MyOtherObject dynamicObj = new MyOtherObject();
    // constructor (a real chatterbox)
    public MyObject()
    {
      Console.WriteLine("MyObject constructor starting");
      Console.WriteLine(
        "(Both static data members initialized before this constructor)");
      // dynamicObj construction was here, now moved up
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
