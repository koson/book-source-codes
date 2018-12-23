using System;

namespace StringSample
{
	class Compare
	{
		static void Main()
		{
			Console.WriteLine("Please enter your password " + 
				"to enter the specified Photo Gallery:");
			
			string sPassword = Console.ReadLine();
			string sDatabasedPassword = "opensaysme";

			if (String.Compare(sPassword, sDatabasedPassword)==0)
			{
				Console.WriteLine("You can now view the photos");
			}
			else
			{
				Console.WriteLine("You do not have permissions " + 
					"to view the photos");
			}
		}	
	}
}
