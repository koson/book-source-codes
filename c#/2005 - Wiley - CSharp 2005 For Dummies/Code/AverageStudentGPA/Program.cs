// AverageStudentGPA - calculate the average GPAs (grade point
//                    averages) of a number of students.
using System;

namespace AverageStudentGPA
{
  public class Student
  {
    public string sName;
    public double dGPA;         // grade point average
  }
  
  public class Program
  {
    public static void Main(string[] args)
    {
      // find out how many students
      Console.WriteLine("Enter the number of students");
      string s = Console.ReadLine();
      int nNumberOfStudents = Convert.ToInt32(s);

      // allocate an array of Student objects
      Student[] students = new Student[nNumberOfStudents];
      
      // now populate the array
      for (int i = 0; i < students.Length; i++)
      {
        // prompt the user for the name - add one to
        // the index because people are 1-oriented while
        // C# arrays are 0-oriented
        Console.Write("Enter the name of student "
                      + (i + 1) + ": ");
        string sName = Console.ReadLine();
        
        Console.Write("Enter grade point average: ");
        string sAvg = Console.ReadLine();
        double dGPA = Convert.ToDouble(sAvg);
        
        // create a Student from that data
        Student thisStudent = new Student();
        thisStudent.sName = sName;
        thisStudent.dGPA  = dGPA;
        
        // add the student object to the array
        students[i] = thisStudent;
      }
      
      // now average the students that you have
      double dSum = 0.0;
      for (int i = 0; i < students.Length; i++)
      {
        dSum += students[i].dGPA;
      }
      double dAvg = dSum/students.Length;
      
      // output the average
      Console.WriteLine();
      Console.WriteLine("The average of the "
                       + students.Length
                       + " students is " + dAvg);

      // wait for user to acknowledge
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
}

