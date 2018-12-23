// IteratorBlockIterator - implements a separate iterator object as a
//              companion to a collection class, a la LinkedList, but
//              implements the actual iterator with an iterator block
using System;
using System.Collections;
namespace IteratorBlockIterator
{
  class Program
  {
    // create a collection and use two iterator objects to iterate
    // it independently (each using an iterator block)
    static void Main(string[] args)
    {
      string[] strs = new string[] { "Joe", "Bob", "Tony", "Fred" };
      MyCollection mc = new MyCollection(strs);
      // create the first iterator and start the iteration
      MyCollectionIterator mci1 = mc.GetEnumerator();
      foreach (string s1 in mci1)  // uses the first iterator object
      {
        // do some useful work with each string
        Console.WriteLine(s1);
        // find Tony's boss
        if (s1 == "Tony")
        {
          // in the middle of that iteration, start a new one, using
          // a second iterator; this is repeated for each outer loop pass
          MyCollectionIterator mci2 = mc.GetEnumerator();
          foreach (string s2 in mci2)  // uses the second iterator object
          {
            // do some useful work with each string
            if (s2 == "Bob")
            {
              Console.WriteLine("\t{0} is {1}'s boss", s2, s1);
            }
          }
        }
      }
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
    // a simple collection of strings
    public class MyCollection
    {
      // implement collection with an ArrayList
      // internal so separate iterator object can access the strings
      internal ArrayList list = new ArrayList();
      public MyCollection(string[] strs)
      {
        foreach (string s in strs)
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
}

