// AbstractInheritance - the BankAccount class is actually abstract because 
//                       there is no single implementation for Withdraw
namespace AbstractInheritance
{
  using System;

  // AbstractBaseClass - create an abstract base class with nothing
  //                     but an Output() method
  abstract public class AbstractBaseClass
  {
    // Output - abstract class output that outputs a string
    abstract public void Output(string sOutputString);
  }

  // SubClass1 - one concrete implementation of AbstractBaseClass
  public class SubClass1 : AbstractBaseClass
  {
    override public void Output(string sSource)
    {
      string s = sSource.ToUpper();
      Console.WriteLine("Call to SubClass1.Output() from within {0}", s);
    }
  }

  // SubClass2 - another concrete implementation of AbstractBaseClass
  public class SubClass2 : AbstractBaseClass
  {
    override public void Output(string sSource)
    {
      string s = sSource.ToLower();
      Console.WriteLine("Call to SubClass2.Output() from within {0}", s);
    }
  }

  class Program
  {
    public static void Test(AbstractBaseClass ba)
    {
      ba.Output("Test");
    }

    public static void Main(string[] strings)
    {
       // you can't create a AbstractBaseClass object because it's
       // abstract - duh. C# generates a compile time error if you
       // uncomment the following line

       // AbstractBaseClass ba = new AbstractBaseClass();
      
      // now repeat the experiment with Subclass1
      Console.WriteLine("\nCreating a SubClass1 object");
      SubClass1 sc1 = new SubClass1();
      Test(sc1);
      
      // and finally a Subclass2 object
      
      Console.WriteLine("\nCreating a SubClass2 object");
      SubClass2 sc2 = new SubClass2();
      Test(sc2);
      
      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate... ");
      Console.Read();
    }
  }
}
