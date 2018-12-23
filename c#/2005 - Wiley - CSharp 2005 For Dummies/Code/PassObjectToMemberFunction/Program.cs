// PassObjectToMemberFunction - rely upon static member functions
//                              to manipulate fields within the object
using System;

namespace PassObjectMemberToFunction
{
  public class Student
  {
    public string sName;

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
  
  public class Program
  {
    public static void Main(string[] args)
    {
      Student student = new Student();

      // set the name by accessing it directly
      Console.WriteLine("The first time:");
      student.sName = "Madeleine";
      Student.OutputName(student); // function now belongs to Student
      
      // change the name using a function
      Console.WriteLine("After being modified:");
      Student.SetName(student, "Willa");
      Student.OutputName(student);

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

