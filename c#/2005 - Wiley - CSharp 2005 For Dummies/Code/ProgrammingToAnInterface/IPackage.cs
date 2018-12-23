// IPackage - a separate interface to specify a contract for
//            members such as ToAddress, FromAddress, etc.
using System;
using System.Collections.Generic;
namespace ProgrammingToAnInterface
{
  public interface IPackage
  {
    // package-specific methods
    string ToAddress { get; }
    // etc.
  }
}
