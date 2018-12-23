using System;
using System.Management;

namespace Win32_ComputerSystem
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
			ManagementObjectSearcher find = new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get()) 
			{
				Console.WriteLine("Computer belongs to domain " + mo["Domain"]);
				Console.WriteLine("Computer manufacturer."+ mo["Manufacturer"]);
				Console.WriteLine("Model name given by manufacturer " +	mo["Model"]);
			}
		}
	}
}
