// PackageFactoryWithIterator - demonstrates a version of
//               the package factory that uses the new
//               C# iterator block
using System;
using System.Collections.Generic;
namespace PackageFactoryWithIterator
{
   class Program
   {
      static void Main(string[] args)
      {
         // fill the priority queue with packages, then remove a
         //  random number of them

         // create a priority queue
         PriorityQueue<Package> pq = new PriorityQueue<Package>();
         // create a random number (0 - 20) of random packages
         //  add them to the priority queue
         Package pack;
         PackageFactory fact = new PackageFactory();
         // we want a random number less than 20
         Random rand = new Random();
         // get a random int from 0 - 20
         int nToCreate = rand.Next(20);
         // no longer need code like this, foreach works
//      for (int i = 0; i < nToCreate; i++)
//      {
//        pack = fact.CreatePackage();
//        pq.Enqueue(pack);
//      }
         // this invokes the iterator within PackageFactory
         foreach (Package pkg in fact.CreatePackage(nToCreate))
         {
            pq.Enqueue(pkg);
         }
         int nCountTotal = pq.Count();
         Console.WriteLine("Packages received: {0}", nCountTotal);
         // remove a random number of packages
         int nToRemove = rand.Next(20);
         for (int i = 0; i < nToRemove; i++)
         {
            pack = pq.Dequeue();
            if (pack != null)
            {
               Console.WriteLine("Shipped package with priority {0}", 
                 pack.Priority());
            }
         }
         Console.WriteLine("Shipped {0} packages", 
           nCountTotal - pq.Count());

         // wait for user to acknowledge the results
         Console.WriteLine("Press Enter to terminate...");
         Console.Read();
      }
   }

   // IPrioritizable - define a custom interface: all classes
   //                    that can be added to our PriorityQueue 
   //                    must implement this interface so they
   //                    will have the right methods
   interface IPrioritizable
   {
      bool HasHighPriority();
      bool HasLowPriority();
   }

   // PriorityQueue - a generic priority queue class
   //                    types to be added to the queue must
   //                    implement IPrioritizable interface
   class PriorityQueue<T> where T : IPrioritizable
   {
      // the three underlying queues
      private Queue<T> hiQ = new Queue<T>();
      private Queue<T> medQ = new Queue<T>();
      private Queue<T> loQ = new Queue<T>();

      public PriorityQueue()
      {
      }

      // prioritize T and add it to correct queue
      //  an item of type T must know its own priority
      public void Enqueue(T item)
      {
         // note that item must have these methods
         //  that's why its type must implement IPrioritizable
         if (item.HasHighPriority())
         {
            hiQ.Enqueue(item);
         }
         else if (item.HasLowPriority())
         {
            loQ.Enqueue(item);
         }
         else
         {
            medQ.Enqueue(item);
         }
      }

      // get T from highest priority queue available
      public T Dequeue()
      {
         // anything in the high-priority queue?
         if (hiQ.Count > 0)
            return hiQ.Dequeue();
         // anything in the medium-priority queue?
         if (medQ.Count > 0)
            return medQ.Dequeue();
         // anything in the low-priority queue?
         if (loQ.Count > 0)
            return loQ.Dequeue();
         // if all queues empty, return null
         //  (we could throw an exception instead)
         // note use of 'default(T)' - see discussion
         //  in the chapter text
         return default(T);
      }

      // look at the highest priority T available
      //                      without removing it from
      //                      its queue
      public T Peek()
      {
         // could return null, so check result
         return TopQueue().Peek();
      }

      // what's the highest-priority underlying queue
      //  that has items in it?
      private Queue<T> TopQueue()
      {
         if (hiQ.Count != 0)
            return hiQ;
         if (medQ.Count != 0)
            return medQ;
         if (loQ.Count != 0)
            return loQ;
         return null;
      }

      // check whether there's anything to deqeue
      public bool IsEmpty()
      {
         // true if all queues are empty
         return (hiQ.Count == 0) & (medQ.Count == 0) & (loQ.Count == 0);
      }

      // how many items are in all queues combined?
      public int Count()
      {
         return hiQ.Count + medQ.Count + loQ.Count;
      }
   }

   // instead of priorities like 1, 2, 3, ... we
   //  give them names
   enum Priorities
   {
      Low, Medium, High
   }

   // Package - an example of a prioritizable class that
   //                    can be stored in the priority
   //                    queue; any class that implements
   //                    IPrioritizable would look
   //                    something like Package
   class Package : IPrioritizable
   {

      public Package()
      {
      }

      public virtual Priorities Priority()
      {
         return Priorities.Medium;
      }

      public virtual bool HasHighPriority()
      {
         return false;
      }

      public virtual bool HasLowPriority()
      {
         return false;
      }

      // plus ToAddress, FromAddress, Insurance, etc.
   }

   // now two Package subclasses to cover the high
   //  and low range of priorities; Package itself
   //  represents the medium priority

   // HighPriorityPackage - "knows" its priority is
   //                    high because it overrides
   //                    the IPrioritizable implementation
   //                    in the base class, Package
   class HighPriorityPackage : Package
   {
      public HighPriorityPackage()
      {
      }

      public override Priorities Priority()
      {
         return Priorities.High;
      }

      public override bool HasHighPriority()
      {
         return true;
      }

      public override bool HasLowPriority()
      {
         return false;
      }

      // and more stuff
   }

   // LowPriorityPackage - "knows" its priority is low
   class LowPriorityPackage : Package
   {

      public LowPriorityPackage()
      {
      }

      public override Priorities Priority()
      {
         return Priorities.Low;
      }

      public override bool HasHighPriority()
      {
         return false;
      }

      public override bool HasLowPriority()
      {
         return true;
      }

      // and more stuff

   }

   // PackageFactory - we need a class that knows how to create a new 
   //                    package of any desired type on demand; such a
   //                    class is called a factory class
   //                    this one uses an iterator block
   class PackageFactory
   {
      Random rand;

      public PackageFactory()
      {
         rand = new Random();
      }
      static int numCreated = 0;

      public System.Collections.IEnumerable CreatePackage(int numToCreate)
      {
         int nRand = 0;
         for (nRand = rand.Next(3); ; nRand = rand.Next(3))
         {
            numCreated += 1;
            if (numCreated > numToCreate)
               yield break;
            switch (nRand)
            {
               case 0:   // we let this represent Package
                  yield return new Package();
                  break;
               case 1:   // while 1 & 2 represent packages ...
                  yield return new HighPriorityPackage();
                  break;
               case 2:   // with other priorities
                  yield return new LowPriorityPackage();
                  break;
               default:  // always cover the "it can't happen" case
                  throw new InvalidOperationException();
            }
         }
      }
   }
   // here's what we replaced with the code above
   //    public Package CreatePackage()
   //    {
   //      // return a randomly selected package priority
   //      //  need a 0, 1, or 2 (values less than 3)
   //      int nRand = rand.Next(3);
   //      // use that to generate a new package
   //      switch (nRand)
   //      {
   //        case 0:   // we let this represent Package
   //          return new Package();
   //        case 1:   // while 1 & 2 represent packages ...
   //          return new HighPriorityPackage();
   //        case 2:   // with other priorities
   //          return new LowPriorityPackage();
   //        default:  // always cover the "it can't happen" case
   //          throw new InvalidOperationException();
   //      }
   //
   //    }
   //  }
}
