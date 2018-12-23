// LinkedListContainer - demonstrate a "home grown" linked list.
//       This container implements IEnumerable
//       to support operators such as foreach. This
//       example also includes an iterator which
//       implements the IEnumerator interface
using System;
using System.Collections;

namespace LinkedListContainer
{
  // LLNode - Each LLNode forms a node in the list.
  // Each LLNode node references a target data object to be
  // incorporated into the list.
  public class LLNode
  {
    // references the object hanging from ("contained by") this node
    // this is the data stored in the list node
    internal object linkedData = null;

    // forward and backward pointer
    // internal so LinkedList can access them directly
    internal LLNode forward  = null; // next node in list
    internal LLNode backward = null; // previous node in list

    internal LLNode(object linkedData)
    {
      this.linkedData = linkedData;
    }

    // retrieve the data stored in this node
    public object Data
    {
      get
      {
        return linkedData;
      }
    }
  }

  // LinkedList - implements a doubly linked list container
  public class LinkedList : IEnumerable
  {
    // the ends of the linked list
    // internal so iterator can access them directly
    internal LLNode head = null;  // "front" of list
    internal LLNode tail = null;  // "end" of list

    public IEnumerator GetEnumerator()
    {
      return new LinkedListIterator(this);
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

  // LinkedListIterator - give application access to LinkedList lists
  public class LinkedListIterator : IEnumerator 
  {
    // the linked list we're iterating
    private LinkedList linkedList;
 
    // the "current" and "next" linked list elements
    // private to prevent any outside direct access
    private LLNode currentNode = null;
    private LLNode nextNode = null;

    //LinkedListIterator - constructor
    public LinkedListIterator(LinkedList linkedList)
    {
      this.linkedList = linkedList;
      Reset();
    }

    // Current- return the data object at the current location
    public object Current
    {
      get
      {
        if (currentNode == null)
        {
          return null;
        }
        return currentNode.linkedData;
      }
    }

    //  Reset - move the iterator back to immediately prior
    //          to the first node in the list
    public void Reset()
    {
      currentNode = null;
      nextNode = linkedList.head;
    }

    // MoveNext - move to the next item in the list unless we
    //            have already reached the end
    public bool MoveNext()
    {
      currentNode = nextNode;
      if (currentNode == null)
      {
        return false;
      }

      nextNode = nextNode.forward;
      return true;
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      // create a container and add three elements to it
      LinkedList llc = new LinkedList();
      LLNode first = llc.AddObject("This is first string");
      LLNode second = llc.AddObject("This is second string");
      LLNode third = llc.AddObject("This is last string");

      // add one at the beginning and one in the middle
      LLNode newfirst = llc.AddObject(null, "Insert before the first string");
      LLNode newmiddle = llc.AddObject(second, "Insert between the second and third strings");

      // we can manipulate the iterator "manually"
      Console.WriteLine("Iterate through the container manually");
      LinkedListIterator lli = (LinkedListIterator)llc.GetEnumerator();
      lli.Reset();
      while(lli.MoveNext())
      {
        string s = (string)lli.Current;
        Console.WriteLine(s);
      }

      // or we can let the foreach do it for us
      Console.WriteLine("\nIterator using the foreach");
      foreach(string s in llc)
      {
        Console.WriteLine(s);
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}
