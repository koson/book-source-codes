// MixingFunctionsAndMethodsWithXMLTags - mixing class functions 
//                         and object methods can cause problems
using System;

namespace MixingFunctionsAndMethods
{
  /// <summary>
  /// Simple description of a student
  /// </summary>
  public class Student
  {
    /// <summary>
    /// Student's given name
    /// </summary>
    public string sFirstName;
    /// <summary>
    /// Student's family name
    /// </summary>
    public string sLastName;
    
    // InitStudent
    /// <summary>
    /// Initializes the student object before it can be used.
    /// </summary>
    /// <param name="sFirstName">Student's given name</param>
    /// <param name="sLastName">Student's family name</param>
    public void InitStudent(string sFirstName, string sLastName)
    {
      this.sFirstName = sFirstName;
      this.sLastName = sLastName;
    }

    // OutputBanner
    /// <summary>
    /// Output a banner before displaying student names
    /// </summary>
    public static void OutputBanner()
    {
      Console.WriteLine("Aren't we clever:");
      // Console.WriteLine(? what student do we use ?);
    }
    
    // OutputBannerAndName
    /// <summary>
    /// Output a banner followed by the current student's name
    /// </summary>
    public void OutputBannerAndName()
    {
      // the class Student is implied but no this
      // object is passed to the static method
      OutputBanner();
      
      // the current Student object is passed explicitly
      OutputName(this, 5);
    }
      
    // OutputName
    /// <summary>
    /// Outputs the student's name to the console
    /// </summary>
    /// <param name="student">The student whose name you
    ///                       want to display</param>
    /// <param name="nIndent">Number of spaces to indent</param>
    /// <returns>The string that was output</returns>
    public static string OutputName(Student student,
                                    int nIndent)
    {
      // here the Student object is referenced explicitly
      string s = new String(' ', nIndent);
      s += String.Format("Student's name is {0}",
                        student.ToNameString());
      Console.WriteLine(s);
      return s;
    }

    // ToNameString
    /// <summary>
    /// Convert the student's name into a string for display
    /// </summary>
    /// <returns>The stringified student name</returns>
    public string ToNameString()
    {
      // here the current object is implicit -
      // this could have been written:
      // return this.sFirstName + " " + this.sLastName;
      return sFirstName + " " + sLastName;
    }
  }
 
  /// <summary>
  /// Class to exercise the Student class
  /// </summary>
  public class Program
  {
    /// <summary>
    /// The program starts here.
    /// </summary>
    /// <param name="args">Command-line arguments</param>
    public static void Main(string[] args)
    {
      Student student = new Student();
      student.InitStudent("Madeleine", "Cather");

      // output the banner and name
      Student.OutputBanner();
      string s = Student.OutputName(student, 5);      
      Console.WriteLine();

      // output the banner and name again
      student.OutputBannerAndName();
      
      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

