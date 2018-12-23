// GenericInterface - uses a generic interface to implement generic factories
using System;
using System.Collections.Generic;
namespace GenericInterface
{
  // an object to pass complex Thing data around
  public struct ThingData
  {
    public string description;
    public int size;
  }
  class Program
  {
    static void Main(string[] args)
    {
      // create the factories
      Console.WriteLine("Create a factory to create Blobs without params");
      GenericFactory<Blob> blobFact = new GenericFactory<Blob>();
      Console.WriteLine("Create a factory to create Students, " +
        "parameterized with a string");
      GenericFactory1<Student, string> stuFact = 
        new GenericFactory1<Student, string>();
      Console.WriteLine("Create a Thing factory");
      GenericFactory2<Thing, string, ThingData> thingFact = 
        new GenericFactory2<Thing, string, ThingData>();
      // set up places to save the created objects
      const int NUM_TO_CREATE = 10;
      List<Blob> bList = new List<Blob>();
      Student[] students = new Student[NUM_TO_CREATE];
      List<Thing> tList = new List<Thing>();
      Console.WriteLine("Create and store the objects:");
      for (int i = 0; i < NUM_TO_CREATE; i++)
      {
        // first, a Blob
        Console.WriteLine("\tCreating a Blob - ");
        Console.WriteLine("\t  Invokes the parameterless constructor.");
        Blob b = blobFact.Create();
        b.name = "blob" + i.ToString();
        bList.Add(b);
        // now a Student
        Console.WriteLine("\tCreating a Student with its name member set - ");
        Console.WriteLine("\t  Invokes the one-parameter constructor.");
        string nameS = "student" + i.ToString();
        students[i] = stuFact.Create(nameS);
        // now a Thing
        Console.WriteLine("\tCreating a Thing - ");
        Console.WriteLine("\t  Invokes the two-parameter constructor.");
        string nameT = "thing" + i.ToString();
        ThingData tData;
        tData.description = "big ugly, hairy monster";
        tData.size = i;
        Thing t = thingFact.Create(nameT, tData);
        tList.Add(t);
      }
      Console.WriteLine("Display results of the object creation:");
      Console.WriteLine("\tBlobs:");
      foreach(Blob b in bList)
      {
        Console.WriteLine("\t\t" + b.ToString());
      }
      Console.WriteLine("\tStudents:");
      foreach(Student s in students)
      {
        Console.WriteLine("\t\t" + s.ToString());
      }
      Console.WriteLine("\tThings:");
      foreach (Thing t in tList)
      {
        Console.WriteLine("\t\t" + t.ToString());
      }
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }

  // data classes: Student, Blob, Thing

  // Blob - a simple class with only a default 
  //        (parameterless) constructor
  class Blob
  {
    public string name;
    // C# provides the paramless constructor in this default case,
    // but you can provide your own
    public override string ToString()
    {
      return "Blob: name = " + this.name;
    }

  }

  // Student - a class with two constructors, default and 
  //           one-param; implements ISettable so it's 
  //           guaranteed to have a SetParameter() method
  class Student : ISettable<string>
  {
    public string name;
    // this parameterless constructor required in addition 
    // to the one-param constructor
    public Student()
    {
    }
    // the one-param constructor we'll use
    public Student(string name)
    {
      this.name = name;
    }
    // implementation of ISettable
    public void SetParameter(string name)
    {
      this.name = name;
    }
    public override string ToString()
    {
      return "Student: name = " + this.name;
    }

  }

  // Thing - a class with a two-param constructor
  // implements ISettable2, so has SetParameter2() method
  class Thing : ISettable2<string, ThingData>
  {
    public string name;
    public int size;
    public string description;
    // Needs this constructor + a parameterless constructor + 
    // SetParameter2() method.
    public Thing(string name, ThingData tData)
    {
      this.name = name;
      this.size = tData.size;
      this.description = tData.description;
    }
    // Required parameterless constructor for use with GenericFactories.
    public Thing()
    {
      // C# won't create this one when there are other constructors
      // in the class, so you have to write it explicitly
    }
    public void SetParameter(string name, ThingData tData)
    {
      this.name = name;
      this.size = tData.size;
      this.description = tData.description;
    }

    public override string ToString()
    {
      return "Thing: (name = " + this.name + "): description = " + this.description;
    }

  }

  // Generic interfaces: ISettable, ISettable2

  // Classes implementing this must have a SetParameter method
  // that takes one parameter of type V.
  interface ISettable<V>
  {
    void SetParameter(V v);
  }
  interface ISettable2<V, W>
  {
    void SetParameter(V v, W w);
  }

  // Generic factories

  // factory for objects with an unparameterized constructor
  // objects using this factory don't need to implement ISettable
  class GenericFactory<T> where T : new()
  {
    public T Create()
    {
      return new T();
    }
  }

  // factory for creating objects that have a constructor with one parameter 
  class GenericFactory1<T, U> where T : ISettable<U>, new()
  {
    // create makes a new T with parameter U and returns T
    public T Create(U u)
    {
      T t = new T();
      t.SetParameter(u);     // T must implement ISettable, 
                             // so it has SetParameter(param)
      return t;
    }
  }

  // factory for creating objects that have
  // a constructor with two parameters
  class GenericFactory2<T, U, V> where T : ISettable2<U, V>, new()
  {
    // create makes a T with params U and V, returns T
    public T Create(U u, V v)
    {
      T t = new T();
      t.SetParameter(u, v);   // T implements ISettable2, 
                              // so has SetParameter(param, param)
      return t;
    }
  }

}
