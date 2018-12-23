using System;
using System.Runtime.InteropServices;

namespace ConsoleTitle
{
	class Class1
	{
		[DllImport("kernel32.dll")]
		public static extern bool SetConsoleTitle(String lpConsoleTitle);

		[STAThread]
		static void Main(string[] args)
		{
			SetConsoleTitle("New title");
			Console.ReadLine();
		}
	}
}
