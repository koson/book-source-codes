// NongenericCollections - demonstrate using the nongeneric collection classes
using System;
using System.Collections;
namespace NongenericCollections
{
  public class Program
  {
    // demonstrate ArrayList, Stack, Queue, and Hashtable
    public static void Main(string[] args)
    {
      // ArrayList
      //
      // instantiate an ArrayList (you can give an initial size or not)
      ArrayList aListWithSpecifiedSize = new ArrayList(1000);
      ArrayList aList = new ArrayList();  // default size (16)
      aList.Add("one");   // adds to the “end” of empty list
      aList.Add("two");   // order is now “one”, “two”
      aList.Add("three"); // now "one", "two", "three"
      Console.WriteLine("{0} items in the ArrayList:", aList.Count);
      // collection classes work with foreach
      foreach(string s in aList)
      {
        // write string and its index in the ArrayList
        Console.WriteLine(s + " at index ({0})", aList.IndexOf(s));
      }
      // 
      // Stack
      //
      // instantiate a stack
      Stack stack = new Stack();
      // "push" several items onto it, then "pop" one off
      stack.Push("one");
      stack.Push("two");   // order now "two", "one"”, with "two" on top
      stack.Push("three"); // "three", "two", "one"
      Console.WriteLine("{0} items on the stack: ", stack.Count);
      foreach (string s in stack)
      {
        Console.WriteLine(s);
      }
      string sval = (string)stack.Pop(); // back to "two", "one"
      Console.WriteLine("Item popped:");
      Console.WriteLine(sval);
      Console.WriteLine("Top of stack is now: {0}", stack.Peek());
      //
      // Queue
      //
      // instantiate a queue
      Queue queue = new Queue();
      // "enqueue" several items
      queue.Enqueue("one");
      queue.Enqueue("two");
      queue.Enqueue("three"); // order is "one", "two", "three"
      Console.WriteLine("{0} items in the queue:", queue.Count);
      foreach (string s in queue)
      {
        Console.WriteLine(s);
      }
      Console.WriteLine("Dequeueing an item: {0}", queue.Dequeue());
      Console.WriteLine("Head of queue is: {0}", queue.Peek());
      //
      // Hashtable
      //
      // instantiate a Hashtable (dictionary)
      Hashtable table = new Hashtable();
      Student student1 = new Student("Randy");
      Student student2 = new Student("Chuck");
      table.Add(student1.Name, student1); // add Student object, "key" is name
      table.Add(student2.Name, student2); // order is unknown
      Console.WriteLine("{0} items in the dictionary:", table.Count);
      // items returned from dictionary are of type DictionaryEntry
      foreach(DictionaryEntry de in table)
      {
        // cast DictionaryEntry’s Value property to Student
        Student stu = (Student)de.Value;
        Console.WriteLine(stu.Name); 
      }
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
   }
  }
  public class Student
  {
    private string name;
    public Student(string name)
    {
      this.name = name;
    }
    public string Name
    {
      get { return name; }
    }
  }
 }

