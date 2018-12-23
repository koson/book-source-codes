using System;

namespace Args
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			foreach (string arg in args)
				Console.WriteLine(arg);
		}
	}
}
