// PackageFactory - a factory to create randomly-prioritized
//                  Package and Parcel items
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace ProgrammingToAnInterface
{
  //PackageFactory - we need a class that knows how to
  //               create a new package of any desired 
  //               type on demand; such a class is 
  //               called a factory class
  // This is one of the few parts of the program that would
  // need to change if we add more package types. Hence, we
  // use the factory class to encapsulate variability.
  // Note: a generic factory would work here too.
  public class PackageFactory
  {
    //a random-number generator
    Random rand = new Random();

    // CreatePackage - call to create a package of random type with
    // a random priority
    public IPrioritizable CreatePackage(int nPriorities)
    {
      // return a randomly selected package priority
      // loop until it's nonzero
      int nPrior;
      do
      {
        nPrior = rand.Next(nPriorities);
      }
      while (nPrior <= 0);  // exit on a nonzero nPrior
      // return a randomly selected package type
      int nType = rand.Next(100);
      IPrioritizable pack;
      if (nType % 2 == 0)  // even number
      {
        Trace.WriteLine("*****TRACE: creating a Package - generated #" +
          nType);
        // create the package with random priority and default address
        pack = new Package(nPrior, "Woodland Park, CO");
      }
      else  // odd number
      {
        Trace.WriteLine("*****TRACE: creating a Parcel - generated #" +
          nType);
        // create the parcel with random priority and default address
        pack = new Parcel(nPrior, "Las Cruces, NM");
      }
      return pack;
    }

  }
}
