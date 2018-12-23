// AbstractInterface - demonstrate that an interface can be 
//                     implemented with an abstract class
using System;

namespace AbstractInterface
{

  // ICompare - an interface that can both compare itself
  //            and display its own value
  public interface ICompare : IComparable
  {
    // GetValue - returns the value of itself as an int
    int GetValue();
  }

  // BaseClass - implement the ICompare interface by
  //             providing a concrete GetValue() method and
  //             an abstract CompareTo()
  abstract public class BaseClass : ICompare
  {
    int nValue;

    public BaseClass(int nInitialValue)
    {
      nValue = nInitialValue;
    }

    // implement the ICompare interface:
    // first with a concrete method
    public int GetValue()
    {
      return nValue;
    }

    // complete the ICompare interface with an abstract method
    abstract public int CompareTo(object rightObject);
  }

  // SubClass - complete the base class by overriding the
  //            abstract CompareTo() method
  public class SubClass: BaseClass
  {
    // pass the value passed to the constructor up to the
    // base class constructor
    public SubClass(int nInitialValue) : base(nInitialValue)
    {
    }

    // CompareTo - implement the IComparable interface; return
    //             an indication of whether a subclass object is
    //             greater than another
    override public int CompareTo(object rightObject)
    {
      BaseClass bc = (BaseClass)rightObject;
      return GetValue().CompareTo(bc.GetValue());
    }
  }

  public class Program
  {
    public static void Main(string[] strings)
    {
      SubClass sc1 = new SubClass(10);
      SubClass sc2 = new SubClass(20);

      MyFunc(sc1, sc2);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // MyFunc - use the methods provided by the ICompare interface
    //          to display the value of two objects and then an indication
    //          of which is greater (according to the object itself)
    public static void MyFunc(ICompare ic1, ICompare ic2)
    {
      Console.WriteLine("The value of ic1 is {0} and ic2 is {1}",
                        ic1.GetValue(), ic2.GetValue());

      string s;
      switch (ic1.CompareTo(ic2))
      {
        case 0:
          s = "is equal to";
          break;
        case -1:
          s = "is less than";
          break;
        case 1:
          s = "is greater than";
          break;
        default:
          s = "something messed up";
          break;
      }
      Console.WriteLine(
             "The objects themselves think that ic1 {0} ic2", s);
    }
  }
}

