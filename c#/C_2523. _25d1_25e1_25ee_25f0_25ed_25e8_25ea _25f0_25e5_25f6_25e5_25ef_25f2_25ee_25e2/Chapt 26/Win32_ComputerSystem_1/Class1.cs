using System;
using System.Management;

namespace Win32_ComputerSystem_1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string[] Roles = {
								 "Standalone Workstation", // 0
								 "Member Workstation",  // 1
								 "Standalone Server",  // 2
								 "Member Server",   // 3
								 "Backup Domain Controller", // 4
								 "Primary Domain Controller" // 5
							 };

			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_ComputerSystem");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get()) 
			{
				Console.WriteLine(Roles[Convert.ToInt32(mo["DomainRole"])]);
			}

		}
	}
}
