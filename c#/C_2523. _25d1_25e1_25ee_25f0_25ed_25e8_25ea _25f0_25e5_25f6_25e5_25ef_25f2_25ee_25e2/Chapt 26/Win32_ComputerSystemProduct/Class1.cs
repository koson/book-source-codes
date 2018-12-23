using System;
using System.Management;

namespace Win32_ComputerSystemProduct
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_ComputerSystemProduct");
			ManagementObjectSearcher find = new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("Description." + mo["Description"]);
				Console.WriteLine("Identifying number (usually serial number)." + mo["IdentifyingNumber"]);
				Console.WriteLine("Commonly used product name." + mo["Name"]);
				Console.WriteLine("Universally Unique Identifier of  product." + mo["UUID"]);
				Console.WriteLine("Vendor of product." + mo["Vendor"]);
			}
		}
	}
}
