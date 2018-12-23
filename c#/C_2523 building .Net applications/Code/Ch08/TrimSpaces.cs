using System;

namespace StringSample
{
	class TrimSpaces
	{
		static void Main()
		{
			String sGreeting = " Welcome to My Shared Photo Album! " ;
			string sUser = "Madeline Ryan";

			// Concatenate without trimming
			Console.WriteLine(sGreeting + sUser);

			// Concatenate with trimming leading and trailing spaces
			Console.WriteLine(sGreeting.Trim() + sUser);

			// Concatenate with trimming the trailing spaces
			Console.WriteLine(sGreeting.TrimEnd() + sUser);

			// Concatenate with trimming the leading spaces
			Console.WriteLine(sGreeting.TrimStart() + sUser);	
		}
	}
}
