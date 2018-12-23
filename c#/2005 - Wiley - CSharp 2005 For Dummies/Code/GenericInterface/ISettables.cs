using System;
using System.Collections.Generic;
namespace GenericInterface
{
  // Classes implementing this must have a SetParameter method
  // that takes one parameter of type V.
  interface ISettable<V>
  {
    void SetParameter(V s);
  }
  interface ISettable2<V, W>
  {
    void SetParameter2(V v, W w);
  }
}
