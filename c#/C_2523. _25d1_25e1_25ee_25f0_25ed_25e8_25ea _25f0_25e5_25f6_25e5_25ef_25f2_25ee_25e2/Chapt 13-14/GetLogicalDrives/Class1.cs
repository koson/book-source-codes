using System;

namespace GetLogicalDrives
{

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			GetLogicalDrives();
			Console.ReadLine();
		}

		static void GetLogicalDrives() 
		{
			try 
			{
				string[] drives = System.IO.Directory.GetLogicalDrives();

				foreach (string driver in drives) 
				{
					System.Console.WriteLine(driver);
				}
			}
			catch (System.IO.IOException) 
			{
				System.Console.WriteLine("Ошибка (I/O error)");
			}
			catch (System.Security.SecurityException) 
			{
				System.Console.WriteLine("Недостаточно прав для выполнения операции");
			}
		}

	}
}
