using System;

namespace IsValueType
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine(typeof(int).IsValueType);		// true
			Console.WriteLine(typeof(string).IsValueType);	// false
			Console.WriteLine(typeof(Enum).IsValueType);	// false

			Console.ReadLine();
		}
	}
}
