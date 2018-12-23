using System;

namespace CommandLine
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine(System.Environment.CommandLine.ToString());
			Console.ReadLine();
		}
	}
}
