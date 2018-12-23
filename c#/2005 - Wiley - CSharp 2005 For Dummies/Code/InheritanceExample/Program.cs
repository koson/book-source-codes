// InheritanceExample - provide the simplest possible
//                      demonstration of inheritance
using System;

namespace InheritanceExample
{
  public class BaseClass
  {
    public int nDataMember;

    public void SomeMethod()
    {
      Console.WriteLine("SomeMethod()");
    }
  }
  
  public class SubClass : BaseClass
  {
    public void SomeOtherMethod() 
    {
      Console.WriteLine("SomeOtherMethod()");
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      // create a base class object
      Console.WriteLine("Exercising a base class object:");
      BaseClass bc = new BaseClass();
      bc.nDataMember = 1;
      bc.SomeMethod();

      // now create a subclass element
      Console.WriteLine("Exercising a subclass object:");
      SubClass sc = new SubClass();
      sc.nDataMember = 2;
      sc.SomeMethod();
      sc.SomeOtherMethod();
      
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

