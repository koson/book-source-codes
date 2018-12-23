using System;
using System.Management;

namespace Win32_ComputerSystem_2
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_ComputerSystem");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			// Новое имя компьютера
			object[] methodArgs = {"New name"};
			foreach (ManagementObject mo in find.Get()) 
			{
				// Вызвать метод Rename
				mo.InvokeMethod("Rename", methodArgs);
			}
		}
	}
}
