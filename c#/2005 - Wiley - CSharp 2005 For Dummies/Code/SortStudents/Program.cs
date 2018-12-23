// SortStudents - this program demonstrates how to sort
//                an array of objects
using System;
  
namespace SortStudents
{
  class Program
  {
    public static void Main(string[] args)
    {
      // create an array of students 
      Student[] students = new Student[5];
      students[0] = Student.NewStudent("Homer", 0);
      students[1] = Student.NewStudent("Lisa", 4.0);
      students[2] = Student.NewStudent("Bart", 2.0);
      students[3] = Student.NewStudent("Marge", 3.0);
      students[4] = Student.NewStudent("Maggie", 3.5);
      
      // output the list as is:
      Console.WriteLine("Before sorting:");
      OutputStudentArray(students);
      
      // now sort the list of students by grade 
      // (best grade first)
      Console.WriteLine("\nSorting the list\n");
      Student.Sort(students);
        
      // display the resulting list
      Console.WriteLine("The students sorted by grade:");
      OutputStudentArray(students);

      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // OutputStudentArray - display all of the students in the array
    public static void OutputStudentArray(Student[] students)
    {
      foreach(Student s in students)
      {
        Console.WriteLine(s.GetString());
      }
    }
  }
  
  // Student - description of a student with name and grade
  class Student
  {
    public string sName;
    public double dGrade = 0.0;
    
    // NewStudent - return a new and initialized student object
    public static Student NewStudent(string sName, double dGrade)
    {
      Student student = new Student();
      student.sName = sName;
      student.dGrade = dGrade;
      return student;
    }
  
    // GetString - convert the current Student object into
    //             a string
    public string GetString()
    {
      string s = "";
      s += dGrade;
      s += " - ";
      s += sName;
      return s;
    }
  
  
    // Sort - sort an array of students in decreasing order
    //        of grade - use the bubble sort algorithm
    public static void Sort(Student[] students)
    {
      bool bRepeatLoop;
    
      // keep looping until the list is sorted
      do
      {
        // this flag is reset to true if an object is found
        // out of order
        bRepeatLoop = false;
   
        // loop through the list of students
        for(int index = 0; index < (students.Length - 1); index++)
        {
          // if two of the students are in the wrong order...
          if (students[index].dGrade < 
                               students[index + 1].dGrade)
          {
            // ...then swap them...
            Student to = students[index];
            Student from = students[index + 1];
            students[index]     = from;
            students[index + 1] = to;
            
            // ...and flag the fact that you'll need to make
            // another pass through the list of students
            // (keep iterating through the loop checking
            // until all of the objects are in order)
            bRepeatLoop = true;
          }
        }
      } while (bRepeatLoop);
    }
  }
}
