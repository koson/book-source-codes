// GenericInterface - demonstrate using a generic interface
using System;
using System.Collections.Generic;
namespace GenericInterface
{
  // factory for objects with an unparameterized constructor
  // objects using this factory don't need to implement ISettable
  class GenericFactory<T> where T : new()
  {
    public T Create()
    {
      return new T();
    }
  }
 
  // factory for creating objects that have 
  // a constructor with one parameter
  class GenericFactory1<T, U> where T : ISettable<U>, new()
  {
    // create makes a new T with parameter U and returns T
    public T Create(U u)
    {
      T t = new T();
      t.SetParameter(u);     // T must implement ISettable, 
                             // so it has SetParameter()
      return t;
    }
  }

  // factory for creating objects that have
  // a constructor with two parameters
  class GenericFactory2<T, U, V> where T : ISettable2<U, V>, new()
  {
    // create makes a T with params U and V, returns T
    public T Create(U u, V v)
    {
      T t = new T();
      t.SetParameter2(u, v);   // T implements ISettable2, so has SetParameter2()
      return t;
    }
  }
}
