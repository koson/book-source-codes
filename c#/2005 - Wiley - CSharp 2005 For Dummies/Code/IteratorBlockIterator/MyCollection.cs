// MyCollection class and its companion iterator, MyCollectionIterator
//     the iterator uses an iterator block
using System;
using System.Collections;
namespace IteratorBlockIterator
{
  // a simple collection of strings
  public class MyCollection
  {
    // implement collection with an ArrayList
    // internal so separate iterator object can access the strings
    internal ArrayList list = new ArrayList();
    public MyCollection(string[] strs)
    {
      foreach(string s in strs)
      {
        list.Add(s);
      }
    }
    // GetEnumerator - as in LinkedList, this returns one of our
    //          iterator objects
    public MyCollectionIterator GetEnumerator()
    {
      return new MyCollectionIterator(this);
    }
  }
  // MyCollectionIterator - the iterator class for MyCollection
  public class MyCollectionIterator
  {
    // store a reference to the collection
    private MyCollection mc;
    public MyCollectionIterator(MyCollection mc)
    {
      this.mc = mc;
    }
    // GetEnumerator - this is the iterator block, which carries
    //            out the actual iteration for the iterator object
    public System.Collections.IEnumerator GetEnumerator()
    {
      // iterate the associated collection's underlying list
      // which is accessible because it's declared internal
      foreach (string s in mc.list)
      {
        yield return s;   // the iterator block's heart
      }
    }
  }
}
