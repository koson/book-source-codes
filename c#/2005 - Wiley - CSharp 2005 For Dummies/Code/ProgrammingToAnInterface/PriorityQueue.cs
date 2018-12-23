// PriorityQueue - a structure to hold prioritized objects
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace ProgrammingToAnInterface
{
  //PriorityQueue - a generic priority queue class
  //               types to be added to the queue *must*
  //               implement IPrioritizable interface
  public class PriorityQueue<T> where T : IPrioritizable
  {
    private int nQueues;  // number of underlying queues

    // queues - the underlying queues: all generic!
    //               Q: why an array? Why not a generic List<T>? 
    //               Because the type stored in these queues is Queue<T>,
    //               not T! Why not an ArrayList? Because ArrayList
    //               holds Objects, and C# doesn't permit casting an Object 
    //               to a Queue<T>
    Queue<T>[] queues;

    //constructor
    public PriorityQueue(int nPriorities)
    {
      // create the array of queues
      nQueues = nPriorities;
      queues = new Queue<T>[nQueues];

      // create the required number of queues - fill queues array
      for (int i = 0; i < nQueues; i++)
      {
        Trace.WriteLine("TRACE: Creating underlying-queue " + i);
        queues[i] = CreateQueue();
      }
      Trace.WriteLine("TRACE: Total number of queues created: " + nQueues);
    }

    // CreateQueue - create a new queue to store in the list
    private Queue<T> CreateQueue()
    {
      Queue<T> queueNew = new Queue<T>();
      return queueNew;
    }

    //Enqueue - prioritize T and add it to correct queue
    //               an item of type T must know its own priority
    public void Enqueue(T item)
    {
      // note that item must have a Priority method
      // that's why its type must implement IPrioritizable
      // we use the array indexer to grab a particular queue
      // based on the int Priority value
      Trace.WriteLine(
        String.Format("TRACE: Enqueuing a {0}", item.ToString()));
      queues[item.Priority].Enqueue(item);
    }

    //Dequeue - get T from highest priority queue available
    public T Dequeue()
    {
      // find highest-priority queue with items
      Queue<T> queueTop = TopQueue();
      // if a non-empty queue found
      if (queueTop != null & queueTop.Count > 0)
      {
        Trace.WriteLine(
          String.Format("TRACE: Dequeue returning a {0} from " +
          "a queue containing {1} items", queueTop.Peek().ToString(), 
          queueTop.Count));
        // return its front item
        T item = queueTop.Dequeue();
        return item;
      }
      else
      {
        // if all queues empty, return null
        // (we could throw an exception instead)
        // note use of 'default(T)' - see PriorityQueue 
        // discussion in the chapter 15 text
        return default(T);
      }
    }

    //Peek - look at the highest priority T available
    //               without removing it from its queue
    public T Peek()
    {
      // find highest-priority queue with items
      Queue<T> queueTop = TopQueue();
      // if a non-empty queue found
      if (queueTop != null && queueTop.Count > 0)
      {
        // peek at its front item
        return queueTop.Peek();
      }
      else
      {
        // if all queues empty, return null
        // (we could throw an exception instead)
        // note use of 'default(T)' - see discussion
        // in the chapter 15 text
        return default(T);
      }
    }

    //TopQueue - what's the highest-priority underlying 
    //              queue that has items in it?
    private Queue<T> TopQueue()
    {
      // starting with the highest queue in queues array,
      // work backward through queues looking for a
      // non-empty queue
      // create an empty queue to return if all queues empty
      Queue<T> queue = CreateQueue();
      for (int i = queues.Length - 1; i >= 0; i--)
      {
        if (queues[i].Count > 0)
        {
          Trace.WriteLine(
            String.Format("TRACE: TopQueue returning queue {0} with {1} items",
            i, queues[i].Count));
          queue = queues[i];
        }
      }
      return queue;
    }

    //IsEmpty - check whether there's anything in 
    //               the PriorityQueue to dequeue
    public bool IsEmpty()
    {
      // true if all queues are empty
      for (int i = 0; i < queues.Length; i++)
      {
        // if any queue not empty, IsEmpty is false
        if (queues[i].Count > 0) return false;
      }
      return true;
    }

    //Count - how many items are in all queues combined?
    public int Count  // implement this one as a read-only property
    {
      get
      {
        // add up number in each queue
        int nSumCounts = 0;
        for (int i = 0; i < queues.Length; i++)
        {
          int nCount = queues[i].Count;
          Trace.WriteLine(
            String.Format("TRACE: items in queue {0} = {1}", i, nCount));
          nSumCounts += nCount;
        }
        return nSumCounts;
      }
    }

    // GetEnumerator - implemented with an iterator block
    public IEnumerator<T> GetEnumerator()
    {
      Trace.WriteLine("TRACE: At start of enumerator, count is " + Count);
      int nItem = 0;
      // iterate the underlying queues
      foreach(Queue<T> q in queues)
      {
        // iterate items in this queue
        foreach(T t in q)
        {
          Trace.WriteLine(
            String.Format("\tTRACE: Iterator returning package {0}, a {1}", 
              nItem, t.ToString()));
          yield return t;
          ++nItem;
        }
      }
    }
  }
}
