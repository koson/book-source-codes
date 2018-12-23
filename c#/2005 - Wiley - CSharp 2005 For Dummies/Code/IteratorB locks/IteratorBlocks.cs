// IteratorBlocks - demonstrates using new iterator
//               block approach to writing collection iterators
using System;
namespace IteratorBlocks
{
  class IteratorBlocks
  {
    //Main - demonstrate five different applications of
    //               iterator blocks
    static void Main(string[] args)
    {
      // instantiate a MonthDays "collection" class
      MonthDays md = new MonthDays();
      // iterate it
      Console.WriteLine("Stream of months:\n");
      foreach (string sMonth in md)
      {
        Console.WriteLine(sMonth);
      }

      // instantiate a StringChunks "collection" class
      StringChunks sc = new StringChunks();
      // iterate it: prints pieces of text
      //  this iteration puts each chunk on its own line
      Console.WriteLine("\nStream of string chunks:\n");
      foreach (string sChunk in sc)
      {
        Console.WriteLine(sChunk);
      }
      // and this iteration puts it all on one line
      Console.WriteLine("\nStream of string chunks on one line:\n");
      foreach (string sChunk in sc)
      {
        Console.Write(sChunk);
      }
      Console.WriteLine();

      // instantiate a YieldBreakEx "collection" class
      YieldBreakEx yb = new YieldBreakEx();
      // iterate it, but stop after 13
      Console.WriteLine("\nStream of primes:\n");
      foreach (int nPrime in yb)
      {
        Console.WriteLine(nPrime);
      }


      // instantiate an EvenNumbers "collection" class
      EvenNumbers en = new EvenNumbers();
      // iterate it: prints even numbers from 10 down to 4
      Console.WriteLine("\nStream of descending evens :\n");
      foreach (int nEven in en.DescendingEvens(11, 3))
      {
        Console.WriteLine(nEven);
      }

      // instantiate a PropertyIterator "collection" class
      PropertyIterator prop = new PropertyIterator();
      // iterate it: produces one double at a time
      Console.WriteLine("\nStream of double values:\n");
      foreach (double db in prop.DoubleProp)
      {
        Console.WriteLine(db);
      }

      // wait for the user to acknowledge
      Console.WriteLine("Press enter to terminate...");
      Console.Read();

    }
  }

  //MonthDays - define an iterator that returns the months and their lengths in 
  //               days - sort of a "collection" class
  class MonthDays
  {
    // here's the "collection"
    string[] months = 
            { "January 31", "February 28", "March 31",
              "April 30", "May 31", "June 30", "July 31",
              "August 31", "September 30", "October 31",
              "November 30", "December 31" };

    //GetEnumerator - here's the iterator - see how it's invoked in Main()
    //               with foreach
    public System.Collections.IEnumerator GetEnumerator()
    {
      foreach (string sMonth in months)
      {
        // return one month per iteration
        yield return sMonth;
      }
    }
  }

  //StringChunks - define an iterator that returns chunks of text, one per 
  //               iteration - another oddball "collection" class
  class StringChunks
  {
    //GetEnumerator - this is an iterator; see how it's invoked (twice) in Main
    public System.Collections.IEnumerator GetEnumerator()
    {
      // return a different chunk of text on each iteration
      yield return "Using iterator ";
      yield return "blocks ";
      yield return "isn't all ";
      yield return "that hard";
      yield return ".";
    }
  }

  //YieldBreakEx - another example of the yield break keyword
  class YieldBreakEx
  {
    int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
    //GetEnumerator - returns a sequence of prime numbers
    //               demonstrates yield return and yield break
    public System.Collections.IEnumerator GetEnumerator()
    {
      foreach (int nPrime in primes)
      {
        if (nPrime > 13) yield break;
        yield return nPrime;
      }
    }
  }

  //EvenNumbers - define a named iterator that returns even numbers from the 
  //               "top" value you pass in DOWN to the "stop" value
  //               another oddball "collection" class
  class EvenNumbers
  {
    //DescendingEvens - this is a "named iterator" 
    //               also demonstrates the yield break keyword
    //               see how it's invoked in Main() with foreach
    public System.Collections.IEnumerable DescendingEvens(int nTop,
                                                          int nStop)
    {
      // start nTop at nearest lower even number
      if (nTop % 2 != 0) // if remainder after nTop / 2 isn't 0
        nTop -= 1;
      // iterate from nTop down to nearest even above nStop
      for (int i = nTop; i >= nStop; i -= 2)
      {
        if (i < nStop)
          yield break;
        // return the next even number on each iteration
        yield return i;
      }
    }
  }

  //PropertyIterator - demonstrate implementing a class
  //               property's get accessor as an iterator block
  class PropertyIterator
  {
    double[] doubles = { 1.0, 2.0, 3.5, 4.67 };
    // DoubleProp - a "get" property with an iterator block
    public System.Collections.IEnumerable DoubleProp
    {
      get
      {
        foreach (double db in doubles)
        {
          yield return db;
        }
      }
    }
  }
}
