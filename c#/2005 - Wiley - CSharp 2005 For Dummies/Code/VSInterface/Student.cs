// Student - mimic a student that's capable of	displaying its own name
using System;

namespace VSInterface
{
  /// <summary>
  /// Student - a student in a school
  /// </summary>
  public class Student
  {
		private	string sStudentName;
		private	int nID;

		public Student(string	sStudentName, int nID)
	  {
			this.sStudentName	= sStudentName;
			this.nID = nID;
	  }

		///	<summary>
		///	the name of the student
		///	</summary>
		public string	Name { get{ return sStudentName;}}

		///	<summary>
		///	return the student name and ID
		///	</summary>
		public override	string ToString()
	  {
			return String.Format("{0}	({1})", sStudentName, nID);
	  }
  }
}
