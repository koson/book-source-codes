// ModifyString - the methods provided by the class String do not
//                modify the object itself (s.ToUpper() does not 
//                modify s; rather it returns a new string that
//                has been converted)
using System;

namespace ModifyString
{
  class Program
  {
    public static void Main(string[] args)
    {
      // create a student object
      Student s1 = new Student();
      s1.sName = "Jenny";

      // now make a new object with the same name
      Student s2 = new Student();
      s2.sName = s1.sName;

      // "changing" the name in the s1 object does not
      // change the object itself because ToUpper() returns
      // a new string without modifying the original
      s2.sName = s1.sName.ToUpper();

      Console.WriteLine("s1 - {0}, s2 - {1}",
                        s1.sName,
                        s2.sName);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }

  // Student - we just need a class with a string in it
  class Student
  {
    public String sName;
  }
}

