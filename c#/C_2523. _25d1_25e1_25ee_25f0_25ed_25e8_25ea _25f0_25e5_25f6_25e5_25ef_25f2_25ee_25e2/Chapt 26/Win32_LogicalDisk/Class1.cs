using System;
using System.Management;

namespace Win32_LogicalDisk
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string cmiPath = @"\root\cimv2:Win32_LogicalDisk.DeviceID='C:'";
			ManagementObject mo = new ManagementObject(cmiPath);

			Console.WriteLine("Description: " + mo["Description"]);
			Console.WriteLine("File system: " + mo["FileSystem"]);
			Console.WriteLine("Free disk space: " + mo["FreeSpace"]);
			Console.WriteLine("Size: " + mo["Size"]);
		}
	}
}
