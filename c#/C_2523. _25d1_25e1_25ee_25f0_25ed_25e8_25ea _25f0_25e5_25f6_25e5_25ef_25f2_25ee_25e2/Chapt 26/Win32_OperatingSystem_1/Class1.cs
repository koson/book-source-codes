using System;
using System.Management;

namespace Win32_OperatingSystem_1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			object[] FLAG_LOGOFF = {0};
			object[] FLAG_SHUTDOWN = {1};
			object[] FLAG_REBOOT = {2};
			object[] FLAG_FORCELOGOFF = {4};
			object[] FLAG_FORCESHUTDOWN = {5};
			object[] FLAG_FORCEREBOOT = {6};
			object[] FLAG_POWEROFF = {8};
			object[] FLAG_FORCEPOWEROFF = {12};

			SelectQuery query = new SelectQuery("Win32_OperatingSystem");
			ManagementObjectSearcher find =
				new ManagementObjectSearcher(query);

			try 
			{
				// Выполняемая команда 
				// указывается в командной строке приложения
				int mode = Convert.ToInt32(args[0]);
  
				foreach (ManagementObject mo in find.Get()) 
				{
					if (mode == (int)FLAG_LOGOFF[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_LOGOFF);
					else if (mode == (int)FLAG_SHUTDOWN[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_SHUTDOWN);
					else if (mode == (int)FLAG_REBOOT[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_REBOOT);
					else if (mode == (int)FLAG_FORCELOGOFF[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_FORCELOGOFF);
					else if (mode == (int)FLAG_FORCESHUTDOWN[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_FORCESHUTDOWN);
					else if (mode == (int)FLAG_FORCEREBOOT[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_FORCEREBOOT);
					else if (mode == (int)FLAG_POWEROFF[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_POWEROFF);
					else if (mode == (int)FLAG_FORCEPOWEROFF[0])
						mo.InvokeMethod("Win32Shutdown", FLAG_FORCEPOWEROFF);
					else
						Console.WriteLine("Неизвестный режим.");
				}
			} 
			catch (Exception e) 
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
