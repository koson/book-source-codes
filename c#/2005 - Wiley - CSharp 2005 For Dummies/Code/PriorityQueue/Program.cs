// PriorityQueue - demonstrates using lower-level queue collection objects 
//               (generic ones at that) to implement a higher-level generic 
//               queue that stores objects in priority order
using System;
using System.Collections.Generic; 
namespace PriorityQueue
{
  class Program
  {
    //Main - fill the priority queue with packages, then 
    // remove a random number of them
    static void Main(string[] args)
    {
      Console.WriteLine("Create a priority queue:");
      PriorityQueue<Package> pq = new PriorityQueue<Package>();
      Console.WriteLine(
        "Add a random number (0 - 20) of random packages to queue:");
      Package pack;
      PackageFactory fact = new PackageFactory();
      // we want a random number less than 20
      Random rand = new Random();
      int numToCreate = rand.Next(20); // random int from 0 - 20
      Console.WriteLine("\tCreating {0} packages: ", numToCreate);
      for (int i = 0; i < numToCreate; i++)
      {
        Console.Write("\t\tGenerating and adding random package {0}", i);
        pack = fact.CreatePackage();
        Console.WriteLine(" with priority {0}", pack.Priority);
        pq.Enqueue(pack);
      }
      Console.WriteLine("See what we got:");
      int nTotal = pq.Count;
      Console.WriteLine("Packages received: {0}", nTotal);

      Console.WriteLine("Remove a random number of packages (0-20): ");
      int numToRemove = rand.Next(20);
      Console.WriteLine("\tRemoving up to {0} packages", numToRemove);
      for (int i = 0; i < numToRemove; i++)
      {
        pack = pq.Dequeue();
        if (pack != null)
        {
          Console.WriteLine("\t\tShipped package with priority {0}", 
              pack.Priority);
        }
      }
      // see how many we "shipped"
      Console.WriteLine("Shipped {0} packages", nTotal - pq.Count);
          
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
  // Priority enumeration - defines a set of priorities
  //      instead of priorities like 1, 2, 3, ... these have names             
  enum Priority
  {
    Low, Medium, High
  }
  // IPrioritizable interface - defines ability to prioritize
  //           define a custom interface: classes that can be added to
  //           PriorityQueue must implement this interface
  interface IPrioritizable
  {
    Priority Priority { get; } // Example of a property in an interface
  }
  
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
      if (queueHigh.Count > 0)   // anything in high priority queue?
        return queueHigh;
      if (queueMedium.Count > 0) // anything in medium priority queue?
        return queueMedium;
      if (queueLow.Count > 0)    // anything in low priority queue?
        return queueLow;
      return queueLow;           // all empty, so return an empty queue
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

  //Package - an example of a prioritizable class that can be stored in
  //          the priority queue; any class that implements
  //          IPrioritizable would look something like Package
  class Package : IPrioritizable
  {
    private Priority priority;
    //constructor
    public Package(Priority priority)
    {
      this.priority = priority;
    }

    //Priority - return package's priority - read-only
    public Priority Priority
    {
      get { return priority; }
    }
    // plus ToAddress, FromAddress, Insurance, etc.
  }

  //PackageFactory - we need a class that knows how to create a new 
  //               package of any desired type on demand; such a class 
  //               is called a factory class
  class PackageFactory
  {
    //a random-number generator
    Random rand = new Random();

    //CreatePackage - the factory method selects a random priority, 
    //               then creates a package with that priority
    //               could implement this as iterator block
    public Package CreatePackage()
    {
      // return a randomly selected package priority
      //  need a 0, 1, or 2 (values less than 3)
      int nRand = rand.Next(3);
      // use that to generate a new package
      // casting int to enum is klunky, but it saves
      // having to use ifs or a switch statement
      return new Package((Priority)nRand);
    }
  }
}
