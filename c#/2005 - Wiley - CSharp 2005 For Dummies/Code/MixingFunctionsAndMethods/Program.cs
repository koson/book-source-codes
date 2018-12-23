// MixingFunctionsAndMethods - mixing class functions and object
//                             methods can cause problems
using System;

namespace MixingFunctionsAndMethods
{
  public class Student
  {
    public string sFirstName;
    public string sLastName;
    
    // InitStudent - initialize the student object
    public void InitStudent(string sFirstName, string sLastName)
    {
      this.sFirstName = sFirstName;
      this.sLastName = sLastName;
    }

    // OutputBanner - output the introduction
    public static void OutputBanner()
    {
      Console.WriteLine("Aren't we clever:");
      // Console.WriteLine(? what student do we use ?);
    }
    
    public void OutputBannerAndName()
    {
      // the class Student is implied but no this
      // object is passed to the static method
      OutputBanner();
      
      // the current Student object is passed explicitly
      OutputName(this);
    }
          
    // OutputName - output the student's name
    public static void OutputName(Student student)
    {
      // here the Student object is referenced explicitly
      Console.WriteLine("Student's name is {0}",
                        student.ToNameString());
    }

    // ToNameString - fetch the student's name
    public string ToNameString()
    {
      // here the current object is implicit -
      // this could have been written:
      // return this.sFirstName + " " + this.sLastName;
      return sFirstName + " " + sLastName;
    }
  }
  
  public class Program
  {
    public static void Main(string[] args)
    {
      Student student = new Student();
      student.InitStudent("Madeleine", "Cather");
      // output the banner and name
      Student.OutputBanner();
      Student.OutputName(student);
      Console.WriteLine();
      
      // output the banner and name again
      student.OutputBannerAndName();
      
      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

