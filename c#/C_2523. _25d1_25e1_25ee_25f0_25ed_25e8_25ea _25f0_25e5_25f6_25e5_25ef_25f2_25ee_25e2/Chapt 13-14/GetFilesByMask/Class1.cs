using System;

namespace ConsoleApplication9
{

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			try 
			{
				string[] dirs = System.IO.Directory.GetFiles(@"c:\", "*.log");
				Console.WriteLine("����� ������ {0}.", dirs.Length);
				foreach (string dir in dirs) 
				{
					Console.WriteLine(dir);
				}
			} 
			catch (Exception e) 
			{
				Console.WriteLine("������: {0}", e.ToString());
			}

			Console.ReadLine();
		}

	}
}
