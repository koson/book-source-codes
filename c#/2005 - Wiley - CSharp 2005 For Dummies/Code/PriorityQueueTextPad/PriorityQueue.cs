// File PriorityQueue.cs
// PriorityQueue class - defines a priority queue
using System;
using System.Collections.Generic;
namespace PriorityQueue
{
  //PriorityQueue - a generic priority queue class
  //               types to be added to the queue *must*
  //               implement IPrioritizable interface
  class PriorityQueue<T> where T : IPrioritizable
  {
    //Queues - the three underlying queues: all generic!
    private Queue<T> queueHigh = new Queue<T>();
    private Queue<T> queueMedium = new Queue<T>();
    private Queue<T> queueLow = new Queue<T>();

    //Enqueue - prioritize T and add it to correct queue; an item of type T
    //               must know its own priority
    public void Enqueue(T item)
    {
      switch (item.Priority) // require IPrioritizable to ensure this property
      {
        case Priority.High:
          queueHigh.Enqueue(item);
          break;
        case Priority.Low:
          queueLow.Enqueue(item);
          break;
        case Priority.Medium:
          queueMedium.Enqueue(item);
          break;
        default:
          throw new ArgumentOutOfRangeException(
            item.Priority.ToString(),
            "bad priority in PriorityQueue.Enqueue");
      }
    }

    //Dequeue - get T from highest priority queue available
    public T Dequeue()
    {
      // find highest-priority queue with items
      Queue<T> queueTop = TopQueue();
      // if a non-empty queue found
      if (queueTop != null & queueTop.Count > 0)
      {
        return queueTop.Dequeue(); // return its front item
      }
      // if all queues empty, return null (we could throw exception)
      return default(T); // what's this? see discussion
    }

    //TopQueue - what's the highest-priority underlying queue with items?
    private Queue<T> TopQueue()
    {
      if (queueHigh.Count > 0) // anything in high priority queue?
        return queueHigh;
      if (queueMedium.Count > 0) // anything in medium priority queue?
        return queueMedium;
      if (queueLow.Count > 0) // anything in low priority queue?
        return queueLow;
      return queueLow; // all empty, so return an empty queue
    }

    //IsEmpty - check whether there's anything to deqeue
    public bool IsEmpty()
    {
      // true if all queues are empty
      return (queueHigh.Count == 0) & (queueMedium.Count == 0) &
          (queueLow.Count == 0);
    }

    //Count - how many items are in all queues combined?
    public int Count  // implement this one as a read-only property
    {
      get { return queueHigh.Count + queueMedium.Count + queueLow.Count; }
    }
  }
}
