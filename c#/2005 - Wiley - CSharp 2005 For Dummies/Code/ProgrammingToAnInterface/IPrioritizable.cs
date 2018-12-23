// IPrioritizable - an interface for making objects prioritizable
//
using System;
namespace ProgrammingToAnInterface
{
  // classes that want to be stored in the PriorityQueue must
  // implement this simple interface
  public interface IPrioritizable
  {
    int Priority { get; }
  }
}
