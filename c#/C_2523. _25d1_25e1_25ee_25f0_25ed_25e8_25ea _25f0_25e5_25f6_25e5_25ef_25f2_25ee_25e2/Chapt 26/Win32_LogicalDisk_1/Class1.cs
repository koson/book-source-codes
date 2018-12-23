using System;
using System.Management;

namespace Win32_LogicalDisk_1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_LogicalDisk WHERE DeviceID = 'C:'");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get()) 
			{
				Console.WriteLine("Description: " + mo["Description"]);
				Console.WriteLine("File system: " + mo["FileSystem"]);
				Console.WriteLine("Free disk space: " + mo["FreeSpace"]);
				Console.WriteLine("Size: " + mo["Size"]);
			}
		}
	}
}
