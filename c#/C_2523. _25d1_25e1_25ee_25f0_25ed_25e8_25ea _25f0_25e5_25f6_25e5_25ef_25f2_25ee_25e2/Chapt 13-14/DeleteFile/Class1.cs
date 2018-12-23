using System;

namespace DeleteFile
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.IO.FileInfo file = new System.IO.FileInfo(@"E:\tmp1");
			file.Delete();
			Console.ReadLine();
		}
	}
}
