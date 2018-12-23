using System;

namespace RefType
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static void Main(string[] args)
		{
			string String1 = "Hello ";
			string String2 = "there.";
			char Char = String1[3];
			Console.WriteLine(String1+String2);
			Console.WriteLine("The extracted character is {0}",Char);
		}
	}
}
