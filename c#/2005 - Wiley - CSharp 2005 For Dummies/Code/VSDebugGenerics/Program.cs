// VSDebugGenerics - this program illustrates using List<T> to
//                   contain the Student collection instead of ArrayList
// Unlike VSDebug, this version works ok, though you still get
// two Warnings, which affect the output - see the Error List
using System;
using System.Collections.Generic;
using System.IO;

namespace VSDebugGenerics
{
  class Program
  {
		static void	Main(string[] args)
	  {
			Console.WriteLine("This version of VSDebug does work (mostly)!");

			Student	s1 = new Student("Student 1", 1);
			Student	s2 = new Student("Student 2", 2);

			// display the two students, but ...
      Console.WriteLine("The next display is incorrect");
      Console.WriteLine("Student 1 = ",	s1.ToString());
			Console.WriteLine("Student 2 = ",	s2.ToString());

			// now ask Student to	display all students
      Console.WriteLine("The next display works fine ");
      Console.WriteLine("  except for missing student numbers");
      Student.OutputAllStudents();

      Console.WriteLine("\n\nFix the problems still due to VSDebug ");
      Console.WriteLine("  and all will be well...\n");

      // wait	for user to acknowledge the results
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }

  public class Student
  {
    // use List<T> instead of ArrayList
		static List<Student> allStudents = new List<Student>();

		private	string sStudentName;
		private	int nID;          // Warning: never assigned to

		public Student(string	sName, int nID)
	  {
			sStudentName	=	sName;
			nID	= nID;              // Warning: problem assignment (the key!)

			allStudents.Add(this);
	  }

		//	ToString - return	the student name and ID
		public override	string ToString()
	  {
			return String.Format("{0}	({1})", sStudentName, nID);
	  }

		public static	void OutputAllStudents()
	  {
//			IEnumerator	iter = allStudents.GetEnumerator();
//
//			// use a for loop	instead of the usual while
//			for(iter.Reset();	iter.Current != null; iter.MoveNext())
//		  {
//				Student	s = (Student)iter.Current;
//      }
      foreach(Student stu in allStudents)
      {
				Console.WriteLine("Student = {0}", stu.ToString());
		  }
	  }
  }
}
