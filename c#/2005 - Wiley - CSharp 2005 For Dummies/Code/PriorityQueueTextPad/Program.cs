// File Program.cs
// PriorityQueueTextPad - demonstrates writing a C# app with the
//                        TextPad tool
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
      // create a priority queue
      PriorityQueue<Package> pq = new PriorityQueue<Package>();
      // add a random number (0 - 20) of random packages to queue
      Package pack;
      PackageFactory fact = new PackageFactory();
      // we want a random number less than 20
      Random rand = new Random();
      int numToCreate = rand.Next(20); // random int from 0 - 20
      for (int i = 0; i < numToCreate; i++)
      {
        // generate a random package and add to priority queue
        pack = fact.CreatePackage();
        pq.Enqueue(pack);
      }
      // see what we got
      int nTotal = pq.Count;
      Console.WriteLine("Packages received: {0}", nTotal);

      // remove a random number of packages: 0-20
      int numToRemove = rand.Next(20);
      for (int i = 0; i < numToRemove; i++)
      {
        pack = pq.Dequeue();
        if (pack != null)
        {
          Console.WriteLine("Shipped package with priority {0}",
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
}
