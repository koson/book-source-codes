using System;
using System.Management;

namespace Win32_Environment
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"Select * from Win32_Environment");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine(mo["Description"] + " - " + mo["Name"] + 
					" - " + mo["UserName"] + " - " + mo["VariableValue"]);
			}
		}
	}
}
