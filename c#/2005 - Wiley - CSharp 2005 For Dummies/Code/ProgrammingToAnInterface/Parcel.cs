// Parcel - a different type of package
using System;
using System.Collections.Generic;
namespace ProgrammingToAnInterface
{
  // Parcel - another class similar to Package (but unrelated)
  //          implements both interfaces
  public class Parcel : IPrioritizable, IPackage
  {
    private int priority;
    private string toAddress;

    public Parcel(int priority, string toAddress)
    {
      this.priority = priority;
      this.toAddress = toAddress;
    }

    // implement IPrioritizable
    // return PriorityLevel as an int
    public int Priority
    {
      get { return priority; }
    }

    // ToString - display package essentials
    public override string ToString()
    {
      return String.Format("Parcel (priority {0})", Priority);
    }

    // ToAddress - get/set the address of package's recipient
    public string ToAddress
    {
      get { return toAddress; }
      set { toAddress = value; }
    }

    // other stuff: FromAddress, Insurance, etc.
  }
}
