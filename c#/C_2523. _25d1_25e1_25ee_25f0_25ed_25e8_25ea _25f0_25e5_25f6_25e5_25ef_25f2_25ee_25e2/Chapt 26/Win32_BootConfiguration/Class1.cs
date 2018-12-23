using System;
using System.Management;

namespace Win32_BootConfiguration
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_BootConfiguration");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("Boot directory with files required for booting." + mo["BootDirectory"]);
				Console.WriteLine("Description." + mo["Description"]);
				Console.WriteLine("Directory with temporary files for booting." + mo["ScratchDirectory"]);
				Console.WriteLine("Directory with temporary files." + mo["TempDirectory"]);
			}
		}
	}
}
