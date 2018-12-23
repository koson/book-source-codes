// Package - one kind of shippable object
using System;
using System.Collections.Generic;
namespace ProgrammingToAnInterface
{
  // Package - implements both interfaces
  public class Package : IPrioritizable, IPackage
  {
    private int priority;
    private string toAddress;

    public Package(int priority, string toAddress)
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
      return String.Format("Package (priority {0})", Priority);
    }

    // ToAddress - get/set the address of package recipient
    public string ToAddress
    {
      get { return toAddress; }
      set { toAddress = value; }
    }

    // other stuff: FromAddress, Insurance, etc.
  }
}
