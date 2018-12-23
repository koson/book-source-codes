// VSDebug - this program is used as an example for debugging;
//					 this is the corrected version of the VSDebug.Class1
using System;
using System.Collections;
using System.IO;

namespace VSDebugFixed
{
  class Program
  {
		static void	Main(string[] args)
	  {
			// I have	to say this to avoid getting a zillion e-mails
			Console.WriteLine("The following program works juuust fine!");

			Student	s1 = new Student("Student 1", 1);
			Student	s2 = new Student("Student 2", 2);

			// display the two students
			Console.WriteLine("Student 1 = {0}",	s1.ToString());
			Console.WriteLine("Student 2 = {0}",	s2.ToString());

			// now ask Student to	display all students
			Student.OutputAllStudents();

			// wait	for user to acknowledge the results
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }

  public class Student
  {
		static ArrayList allStudents = new ArrayList();

		private	string sStudentName;
		private	int nID;

		public Student(string	sName, int nID)
	  {
			sStudentName	=	sName;
			this.nID	= nID;

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

      while(iter.MoveNext())
		  {
				Student	s = (Student)iter.Current;
				Console.WriteLine("Student = {0}", s.ToString());
		  }
	  }
  }
}
