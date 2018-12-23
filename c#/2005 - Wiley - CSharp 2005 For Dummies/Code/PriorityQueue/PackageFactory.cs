// File PackageFactory.cs
// PackageFactory class - defines a simple factory for
//               creating Package objects with random
//               priorities
using System;
namespace PriorityQueue
{
  //PackageFactory - we need a class that knows how to
  //               create a new package of any desired 
  //               type on demand; such a class is 
  //               called a factory class
  class PackageFactory
  {
    //a random-number generator
    Random rand = new Random();

    //CreatePackage - the factory method
    //               selects a random priority, then
    //               creates a package with that priority
    //               could implement this as iterator block
    public Package CreatePackage()
    {
      // return a randomly selected package priority
      //  need a 0, 1, or 2 (values less than 3)
      int nRand = rand.Next(3);
      // use that to generate a new package
      // casting int to enum is klunky, but it saves
      // having to use ifs or a switch statement
      return new Package((Priorities)nRand);
    }
  }
}
