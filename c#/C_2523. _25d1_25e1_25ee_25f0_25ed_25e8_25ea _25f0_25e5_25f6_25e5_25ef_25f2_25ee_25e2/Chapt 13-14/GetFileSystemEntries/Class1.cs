using System;

namespace GetFileSystemEntries
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string[] contents = System.IO.Directory.GetFileSystemEntries(@"E:\#Task"); 
 			foreach(string s in contents) 
 				Console.WriteLine(s); 

			Console.ReadLine();
 
		}
	}
}
