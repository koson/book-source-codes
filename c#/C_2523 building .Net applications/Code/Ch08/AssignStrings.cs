using System;

namespace StringSample
{
	class AssignmentAndLength
	{
		static void Main()
		{
			String sGreeting = "Welcome to 'My Personal Photo Album'";
			int iGreetingLength = sGreeting.Length;

			Console.WriteLine ("The greating: \n{0}\nis {1} characters long.",
				sGreeting, iGreetingLength);
		}
	}
}
