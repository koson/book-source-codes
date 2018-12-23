// University - a simplistic container of students
using System;
using System.Collections;
namespace VSInterface
{
  /// <summary>
  /// University - an institute of higher learning.
  /// </summary>
  public class University
  {
		private	string sName;
		private	SortedList students;

		public University(string sName)
	  {
			this.sName = sName;

			students = new SortedList();
	  }

		///	<summary>
		///	Enroll - add a student to the university
		///	</summary>
		public void	Enroll(Student student)
	  {
			students.Add(student.Name, student);
	  }

		public override	string ToString()
	  {
			string s = sName + "\n";

			s	+= "The Student Body:" + "\n";
		  
			// iterate through the students	at the university
      // using a conventional enumerator
			IEnumerator	iter = students.GetEnumerator();
			while(iter.MoveNext())
		  {
				object o = iter.Current;

				// the following approach doesn't work	since the
				// iterator	for SortedList returns a dictionary
				// entry which includes	both the student and the key:

				// Student student = (Student)o;

        // get the DictionaryEntry
				DictionaryEntry	de = (DictionaryEntry)o;
        // get the entry's Value member
				Student	student = (Student)de.Value;
				s	+= student.ToString() + "\n";
		  }
			return s;
	  }
  }
}
