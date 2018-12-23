using System;

namespace StringSample
{
	class LiteralsAndVariables
	{
		static void Main()
		{
			string sRegular = "This\nis\na\nexample";
			string sVerbatim = @"This\nis\na\nexample";

			Console.WriteLine("This is an example for a Regular String:");
			Console.WriteLine(sRegular);
			Console.WriteLine();
			Console.WriteLine("This is an example for a Verbatim String:");
			Console.WriteLine(sVerbatim);
		}
	}
}
