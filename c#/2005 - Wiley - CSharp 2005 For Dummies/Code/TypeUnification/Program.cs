// TypeUnification - demonstrate how int and Int32
//                   are actually the same thing and
//                   how they derive from Object
using System;

namespace TypeUnification
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // create an int and initialize it to zero
      int i = new int();  // yes, you can do this

      // assign it a value and output it via the
      // IFormattable interface that Int32 implements
      i = 1;
      OutputFunction(i);

      // the constant 2 also implements IFormattable
      OutputFunction(2);

      // in fact, you can call method of a constant
      Console.WriteLine("Output directly = {0}", 3.ToString());

      // this can be truly useful; you can pick an int out of a list:
      Console.WriteLine("\nPick the integers out of a list");
      object[] objects = new object[5];
      objects[0] = "this is a string";
      objects[1] = 2;
      objects[2] = new Program();
      objects[3] = 4;
      objects[4] = 5.5;
      for(int index = 0; index < objects.Length; index++)
      {
        if (objects[index] is int)
        {
          int n = (int)objects[index];
          Console.WriteLine("the {0}th element is a {1}", index, n);
        }
      }

      // type unity allows you to display value and
      // reference typeswithout differentiating them
      Console.WriteLine("\nDisplay all the objects in the list");
      int nCount = 0;
      foreach(object o in objects)
      {
        Console.WriteLine("Objects[{0}] is <{1}>",
          nCount++, o.ToString());
      }

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // OutputFunction - outputs any object that implements ToString()
    public static void OutputFunction(IFormattable id)
    {
      Console.WriteLine("Value from OutputFunction = {0}", id.ToString());
    }

    // ToString - provide a simple string function
    override public string ToString()
    {
      return "TypeUnification Program";
    }
  }
}
