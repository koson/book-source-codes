// VSDebug - this program is used as an example for debugging;
//					 the program does	not work as it sits (look at
//					 VSDebugFixed for the fixed version)
//           Compiling produces two Warnings - see Error List window
//           Running produces a run time exception
using System;
using System.Collections;
using System.IO;

namespace VSDebug
{
  class Program
  {
		static void	Main(string[] args)
	  {
			// I have	to say this to avoid getting a zillion e-mails
			Console.WriteLine("The following program does.not work!");

			Student	s1 = new Student("Student 1", 1);
			Student	s2 = new Student("Student 2", 2);

			// display the two students
			Console.WriteLine("Student 1 = ",	s1.ToString());
			Console.WriteLine("Student 2 = ",	s2.ToString());

			// now ask Student to	display all students
			Student.OutputAllStudents();

			// wait	for user to acknowledge the results
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }

  public class Student
  {
    // we'll stick with ArrayList rather than switching to, say,
    // List<T>
		static ArrayList allStudents = new ArrayList();

		private	string sStudentName;
		private	int nID;

		public Student(string	sName, int nID)
	  {
			sStudentName	=	sName;
			nID	= nID;

			allStudents.Add(this);
	  }

		//	ToString - return	the student name and ID
		public override	string ToString()
	  {
	    string s = String.Format("{0}	({1})", sStudentName, nID);
			return s;
	  }

		public static	void OutputAllStudents()
	  {
			IEnumerator	iter = allStudents.GetEnumerator();

			// use a for loop	instead of the usual while
			for(iter.Reset();	iter.Current != null; iter.MoveNext())
		  {
				Student	s = (Student)iter.Current;
				Console.WriteLine("Student = {0}", s.ToString());
		  }
	  }
  }
}
