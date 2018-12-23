using System;

namespace EnvironmentTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string PlatformText = Environment.OSVersion.Platform.ToString();
			string VersionText = Environment.OSVersion.Version.ToString(); 
			Console.WriteLine(PlatformText + " " + VersionText);
 
			Console.ReadLine();
		}

	}
}
