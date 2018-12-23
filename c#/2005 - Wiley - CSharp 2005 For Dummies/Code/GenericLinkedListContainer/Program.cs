// GenericLinkedListContainer - demonstrate a "home grown" linked list.
//       This example  includes an iterator which
//       implements the IEnumerator interface, so it can use foreach

// This version is generic.
// class LinkedList becomes LinkedList<T> and class LLNode becomes
// LLNode<T>; the GetEnumerator() method uses an iterator block.
// Compare this to System.Collections.Generic.LinkedList<T>, which
// is similar but looks a bit different. Prefer the built-in version.
using System;
using System.Collections;

namespace GenericLinkedListContainer
{
  // LLNode - Each LLNode forms a node in the list.
  // Each LLNode node references a target object to be
  // incorporated into the list.
  public class LLNode<T>
  {
    // references the object hanging from ("contained by") this node
    // this is the data stored in the list node
    internal T linkedData = default(T); // don't know "null" for T

    // forward and backward pointer
    // internal so LinkedList can access them directly
    // these are real nulls because these are LLNode objects, not
    // the stored T type
    internal LLNode<T> forward  = null; // next node in list
    internal LLNode<T> backward = null; // previous node in list
    // constructor
    internal LLNode(T linkedData)
    {
      this.linkedData = linkedData;
    }

    // property to retrieve the data stored in this node
    public T Data
    {
      get
      {
        return linkedData;
      }
    }
  }

  // LinkedList - implements a doubly linked list container
  public class LinkedList<T>  // no longer need ": IEnumerable" here
  {
    // the ends of the linked list
    // internal so iterator can access them directly
    internal LLNode<T> head = null;
    internal LLNode<T> tail = null;
    // need to track currentNode here - this used to be in the
    // homegrown LinkedListIterator class, which is now extinct
    internal LLNode<T> currentNode;

    // here's the iterator, implemented as an iterator block
    public IEnumerator GetEnumerator()
    {
      // make sure the current node is legal
      // if it's null, it hasn't yet been set to point into the list
      // so point it at the head
      if (currentNode == null)
      {
        currentNode = head;
      }
      // here's the iteration for the enumerator that
      // GetEnumerator() returns
      while (currentNode != null)
      {
        yield return currentNode.Data;
        currentNode = currentNode.forward;
      }
    }

    // AddObject - add an object to the end of list
    public LLNode<T> AddObject(T objectToAdd)
    {
      return AddObject(tail, objectToAdd);
    }

    // AddObject- add an object to the list
    public LLNode<T> AddObject(LLNode<T> previousNode, T objectToAdd)                 
    {
      // create a new node with the object attached
      LLNode<T> newNode = new LLNode<T>(objectToAdd);

      // start with the easiest case:
      // if the list is empty then...
      if (head == null && tail == null)
      {
        // ...start off with just the one node in the list
        head = newNode;
        tail = newNode;
        return newNode;
      }

      // OK, are we adding the new node to the middle of the list?
      if (previousNode != null && previousNode.forward != null)
      {
        // just switch the pointers around and we're done
        LLNode<T> nextNode = previousNode.forward;

        // first, store the forward pointers
        newNode.forward = nextNode;
        previousNode.forward = newNode;

        // now the backward pointers
        nextNode.backward = newNode;
        newNode.backward = previousNode;

        return newNode;
      }

      // are we adding it to the beginning?
      if (previousNode == null)
      {
        // make this the head man
        LLNode<T> nextNode = head;
        newNode.forward = nextNode;
        nextNode.backward = newNode;
        head = newNode;
        return newNode;
      }

      // must be the end of the list
      newNode.backward = previousNode;
      previousNode.forward = newNode;
      tail = newNode;
      return newNode;
    }

    // RemoveObject - remove an object from the list
    public void RemoveObject(LLNode<T> currentNode)
    {
      // get the current node's neighbors
      LLNode<T> previousNode = currentNode.backward;
      LLNode<T> nextNode     = currentNode.forward;

      // remove the current object's pointers
      currentNode.forward = currentNode.backward = null;

      // now... if this was the last element in the list
      if (head == currentNode && tail == currentNode)
      {
        head = tail = null;
        return;
      }

      // ok, if this node is in the middle...
      if (head != currentNode && tail != currentNode)
      {
        previousNode.forward = nextNode;
        nextNode.backward = previousNode;
        return;
      }

      // at the front of the list?
      if (head == currentNode && tail != currentNode)
      {
        head = nextNode;
        nextNode.backward = null;
        return;
      }

      // must be at the end of the list
      tail = previousNode;
      previousNode.forward = null;
    }
  }

  // LinkedListIterator - no longer exists!


  public class Program
  {
    public static void Main(string[] args)
    {
      // create a string container and add three elements to it
      LinkedList<string> llc = new LinkedList<string>();
      LLNode<string> first = llc.AddObject("This is first string");
      LLNode<string> second = llc.AddObject("This is second string");
      LLNode<string> third = llc.AddObject("This is last string");

      // add one at the beginning and one in the middle
      LLNode<string> newfirst = llc.AddObject(null, "Insert before the first string");
      LLNode<string> newmiddle = llc.AddObject(second, "Insert between the second and third strings");

      // we can no longer manipulate the iterator "manually"
      // (who wants to?)
      // this old manual code no longer works: there's no
      // LinkedListIterator class, no MoveNext(), and no Current.
      // (Well, there is, but it's deep under the hood where you can't
      // get at it, but foreach can.)
//      Console.WriteLine("Iterate through the container manually");
//      LinkedListIterator lli = (LinkedListIterator)llc.GetEnumerator();
//      lli.Reset();
//      while (lli.MoveNext())
//      {
//        string s = (string)lli.Current;
//        Console.WriteLine(s);
//      }

      // iterate with foreach - the preferred way now
      Console.WriteLine("\nIterate string version using foreach");
      foreach(string s in llc)
      {
        Console.WriteLine(s);
      }

      // instantiate for int
      LinkedList<int> llci = new LinkedList<int>();
      LLNode<int> one = llci.AddObject(1);
      LLNode<int> two = llci.AddObject(2);
      // insert a node between one and two
      LLNode<int> newTwo = llci.AddObject(one, 3);

      Console.WriteLine("\nIterate int version using foreach");
      foreach (int i in llci)
      {
        Console.WriteLine(i.ToString());
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
