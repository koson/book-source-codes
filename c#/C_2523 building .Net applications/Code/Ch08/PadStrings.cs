using System;

namespace StringSample
{
	class Pad
	{
		static void Main(string[] args)
		{
			string sGreeting = "Welcome to 'My Personal Photo Album'";

			//First pad with length of sGreeting + 5, then with just 5.
			Console.WriteLine(sGreeting.PadLeft(41)); 
			Console.WriteLine(sGreeting.PadLeft(5)); 
		}
	}
}
