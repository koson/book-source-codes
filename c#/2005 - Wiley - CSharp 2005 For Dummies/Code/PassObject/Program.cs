// PassObject - demonstrate how to pass an object
//              to a function
using System;

namespace PassObject
{
  public class Student
  {
    public string sName;
  }
  
  public class Program
  {
    public static void Main(string[] args)
    {
      Student student = new Student();

      // set the name by accessing it directly
      Console.WriteLine("The first time:");
      student.sName = "Madeleine";
      OutputName(student);
      
      // change the name using a function
      Console.WriteLine("After being modified:");
      SetName(student, "Willa");
      OutputName(student);

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // OutputName - output the student's name
    public static void OutputName(Student student)
    {
      // output current student's name
      Console.WriteLine("Student's name is {0}", student.sName);
    }

    // SetName - modify the student object's name
    public static void SetName(Student student, string sName)
    {
      student.sName = sName;
    }
  }
}

