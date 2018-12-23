using System;
using System.Reflection;

namespace LoadWithPartialName
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string Name = "System.Windows.Forms";

			string FullName = 
				Assembly.LoadWithPartialName(Name).FullName;
			// Напечатает
			// "System.Windows.Forms, Version=1.0.5000.0, 
			//      Culture=neutral, PublicKeyToken=b77a5c561934e089"
			Console.WriteLine(FullName);
			Console.WriteLine();
			
			string Location = 
				Assembly.LoadWithPartialName(Name).Location;
			Console.WriteLine(Location);
			// Напечатает
			// "f:\winxp\assembly\gac\system.windows.forms\
			//    1.0.5000.0__b77a5c561934e089\system.windows.forms.dll"
			Console.WriteLine();

			string ImageRuntimeVersion = 
				Assembly.LoadWithPartialName(Name).ImageRuntimeVersion;
			// Напечатает
			// "v1.1.4322"
			Console.WriteLine(ImageRuntimeVersion);

			Console.ReadLine();
		}
	}
}
