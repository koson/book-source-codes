// InvokeMethod - invoke a member function through the object
using System;

namespace InvokeMethod
{
  class Student
  {
    // the name information to describe a student
    public string sFirstName;
    public string sLastName;

    // SetName - save off name information
    public void SetName(string sFName, string sLName)
    {
      sFirstName = sFName;
      sLastName  = sLName;
    }

    // ToNameString - convert the student object into a
    //                string for display
    public string ToNameString()
    {
      string s = sFirstName + " " + sLastName;
      return s;
    }
  }
  
  public class Program
  {
    public static void Main()
    {
      Student student = new Student();
  
      student.SetName("Stephen", "Davis");
  
      Console.WriteLine("Student's name is " 
                       + student.ToNameString());

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

