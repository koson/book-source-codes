// LinkedListWithIteratorBlock - a variation on our "home grown" linked list.
//       This version uses an iterator block to enumerate the collection.
using System;
using System.Collections;

namespace LinkedListWithIteratorBlock
{
  // LLNode - Each LLNode forms a node in the list.
  // Each LLNode node references a target object to be
  // incorporated into the list.
  public class LLNode
  {
    // references the object hanging from ("contained by") this node
    // this is the data stored in the list node
    internal object linkedObject = null;

    // forward and backward pointer
    // internal so LinkedList can access them directly
    internal LLNode forward  = null; // next node in list
    internal LLNode backward = null; // previous node in list
    // constructor
    internal LLNode(object linkedObject)
    {
      this.linkedObject = linkedObject;
    }

    // property to retrieve the data stored in this node
    public object Object
    {
      get
      {
        return linkedObject;
      }
    }
  }

  // LinkedList - implements a doubly linked list container
  public class LinkedList  // no longer need ": IEnumerable" here
  {
    // the ends of the linked list
    // internal so iterator can access them directly
    internal LLNode head = null;
    internal LLNode tail = null;

    // need to track currentNode here - this used to be in the
    // homegrown LinkedListIterator class, which is now extinct
    internal LLNode currentNode;

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
        yield return currentNode.Object;
        currentNode = currentNode.forward;
      }
    }

    // AddObject - add an object to the end of list
    public LLNode AddObject(object objectToAdd)
    {
      return AddObject(tail, objectToAdd);
    }

    // AddObject- add an object to the list
    public LLNode AddObject(LLNode previousNode, object objectToAdd)

    {
      // create a new node with the object attached
      LLNode newNode = new LLNode(objectToAdd);

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
        LLNode nextNode = previousNode.forward;

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
        LLNode nextNode = head;
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
    public void RemoveObject(LLNode currentNode)
    {
      // get the current node's neighbors
      LLNode previousNode = currentNode.backward;
      LLNode nextNode     = currentNode.forward;

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
      // create a container and add three elements to it
      LinkedList llc = new LinkedList();
      // AddObject returns a reference to the object added
      LLNode first = llc.AddObject("This is first string");
      LLNode second = llc.AddObject("This is second string");
      LLNode third = llc.AddObject("This is last string");

      // add one at the beginning and one in the middle
      LLNode newfirst = llc.AddObject(null, "Insert before the first string");
      LLNode newmiddle = llc.AddObject(second, "Insert between the second and third strings");

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

      // iterate with foreach
      Console.WriteLine("\nIterator using foreach");
      foreach(string s in llc)  // foreach does the cast for you
      {
          Console.WriteLine(s);
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
