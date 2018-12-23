using System;

namespace StringSample
{
	class Concatenate
	{
		static void Main()
		{
			// Concatenate the string with the + arithmetic operator.
			string sFullName = "Alexander Ryan";
			string sBaseGreeting = "welcome to MySharedPhotoAlbum!";

			// Concatenate again with the Concat method that is available
			// on the String Base Class.
			string sPersonalizedGreeting = 
				String.Concat(sFullName, ", ", sBaseGreeting);
			Console.WriteLine(sPersonalizedGreeting);
		}
	}
}
