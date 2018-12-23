using System;

namespace GenerateGUID
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Guid guid = System.Guid.NewGuid();
			Console.WriteLine(guid.ToString());
			Console.ReadLine();
		}
	}
}
