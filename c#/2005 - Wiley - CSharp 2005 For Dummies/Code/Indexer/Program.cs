// Indexer - this program demonstrates the use of the index operator to provide 
//           access to an array using a string as an index
using System;

namespace Indexer
{
  public class KeyedArray
  {
    // the following string provides the "key" into the array - 
    // the key is the string used to identify an element
    private string[] sKeys;

    // the object is the actual data associated with that key
    private object[] oArrayElements;

    // KeyedArray - Create a fixed-size KeyedArray.
    public KeyedArray(int nSize)
    {
      sKeys = new string[nSize];
      oArrayElements = new object[nSize];
    }

    // Find - find the index of the record corresponding to the string 
    //        sTargetKey (return a negative if can't be found)
    private int Find(string sTargetKey)
    {
      for(int i = 0; i < sKeys.Length; i++)
      {
        if (String.Compare(sKeys[i], sTargetKey) == 0)
        {
          return i;
        }
      }
      return -1;
    }

    // FindEmpty - find room in the array for a new entry
    private int FindEmpty()
    {
      for (int i = 0; i < sKeys.Length; i++)
      {
        if (sKeys[i] == null)
        {
          return i;
        }
      }

      throw new Exception("Array is full");
    }
    // look up contents by string key - this is the indexer
    public object this[string sKey]
    {
      set
      {
        // see if the string isn't already there
        int index = Find(sKey);
        if (index < 0)
        {
          // it isn't -  find a new spot
          index = FindEmpty();
          sKeys[index] = sKey;
        }

        // save the object off in the corresponding spot
        oArrayElements[index] = value;
      }

      get 
      {
        int index = Find(sKey);
        if (index < 0)
        {
          return null;
        }
        return oArrayElements[index];
      }
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      // create an array with enough room
      KeyedArray ma = new KeyedArray(100);

      // save off the ages of the Simpsons' kids
      ma["Bart"] = 8;
      ma["Lisa"] = 10;
      ma["Maggie"] = 2;

      // look up the age of Lisa
      Console.WriteLine("Let's find Lisa's age");
      int age = (int)ma["Lisa"];
      Console.WriteLine("Lisa is {0}", age);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

