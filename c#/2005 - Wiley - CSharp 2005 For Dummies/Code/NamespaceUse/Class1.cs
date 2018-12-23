// NamespaceUse - demonstrates accessing items with the same
//                name in different namespaces.
using System;  // all namespaces need this for things like
               // String and Console

namespace MainCode
{
  using LibraryCode1;   // this simplifies MainCode

  public class Class1
  {
    public void AMethod()
    {
      Console.WriteLine("MainCode.Class1.AMethod()");
    }

    static void Main(string[] args)
    {
      // Create an instance of Main's class, in this namespace.
      Class1 c1 = new Class1();  // MainCode.Class1
      c1.AMethod();

      // Create an instance of LibraryCode1.Class1
      // The following just creates a new MainCode.Class1,
      // not what you intended; no using directive and
      // not fully qualified
      Class1 c2 = new Class1();
      c2.AMethod();

      // but a qualified name does create the right class
      // must qualify this even given the using directive, since
      // both namespaces have a Class1
      LibraryCode1.Class1 c3 = new LibraryCode1.Class1();
      c3.AMethod();

      // meanwhile, creating a LibraryCode1.Class2 doesn't require
      // qualification because there's a using directive and
      // no name clash
      Class2 c4 = new Class2();
      c4.AMethod();

      // Create an instance of LibraryCode2.Class1
      // requires qualification, both because there is no using
      // directive for LibraryCode2 and both namespaces have
      // a Class1
      // Note: also works even though LibraryCode2.Class1 is
      // declared internal, not public, because both classes
      // are in the same compiled assembly
      LibraryCode2.Class1 c5 = new LibraryCode2.Class1();
      c5.AMethod();

      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

namespace LibraryCode1
{
  public class Class1
  {
    public void AMethod()
    {
      Console.WriteLine("LibraryCode1.Class1.AMethod()");
    }
  }

  public class Class2
  {
    public void AMethod( )
    {
      Console.WriteLine("LibraryCode1.Class2.AMethod()");
    }
  }
}

namespace LibraryCode2
{
  class Class1   // no access keyword: defaults to internal access
  {
    public void AMethod()
    {
      Console.WriteLine("LibraryCode2.Class1.AMethod()");
    }
  }
}
