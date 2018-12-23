// ReferencingThisExplicitly - this program demonstrates
//           how to explicitly use the reference to this
using System;

namespace ReferencingThisExplicitly
{
  public class Program
  {
		public static	void Main(string[] strings)
	  {
			// create	a student
			Student	student = new Student();
			student.Init("Stephen	Davis", 1234);

			// now enroll	the student in a course
      Console.WriteLine
             ("Enrolling Stephen Davis in Biology 101");
			student.Enroll("Biology	101");

      // display student course
      Console.WriteLine("Resulting student record:");
      student.DisplayCourse();

			// wait	for user to acknowledge the results
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }

  // Student - our class university student
  public class Student
  {
		// all students	have a name and id
		public string	sName;
		public int		nID;

		// the course	in which the student is enrolled
		CourseInstance courseInstance;

		// Init	- initialize the student object
		public void	Init(string sName, int nID)
	  {
			this.sName = sName;
			this.nID = nID;

			courseInstance = null;
	  }

		// Enroll	- enroll the current student in a course
		public void	Enroll(string sCourseID)
	  {
			courseInstance = new CourseInstance();
			courseInstance.Init(this,	sCourseID);
	  }

    // Display the name of the student
    // and the course
    public void DisplayCourse()
    {
      Console.WriteLine(sName);
      courseInstance.Display();
    }
  }

  // CourseInstance - a combination of a student with
  //									university course
  public class CourseInstance
  {
		public Student student;
		public string	sCourseID;

		// Init	- tie the student to the course
		public void	Init(Student student, string sCourseID)
	  {
			this.student = student;
			this.sCourseID = sCourseID;
	  }

    // Display - output the name of the course
    public void Display()
    {
      Console.WriteLine(sCourseID);
    }
  }
}

