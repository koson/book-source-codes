// VSInterface - demonstrate a program consisting of multiple
//							 classes.	The Student and University classes
//							 are each	housed in their own source files.
using System;

namespace VSInterface
{
  class Program
  {
		static void	Main(string[] args)
	  {
			University university =
						 new University("Scruffs Non-Technical Institute");

			university.Enroll(new	Student("Dwayne", 1234));
			university.Enroll(new	Student("Mikey", 1235));
			university.Enroll(new	Student("Mark", 1236));

			Console.WriteLine(university.ToString());
			Console.WriteLine();

			// wait	for user to acknowledge the results
			Console.WriteLine("Press Enter to terminate...");
			Console.Read();
	  }
  }
}
