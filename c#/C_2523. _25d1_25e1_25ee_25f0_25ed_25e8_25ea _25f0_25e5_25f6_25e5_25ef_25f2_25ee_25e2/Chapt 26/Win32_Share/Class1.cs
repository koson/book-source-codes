using System;
using System.Management;

namespace Win32_Share
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_Share");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("List of shares = " + mo["Name"]);
			}
		}

	}
}
