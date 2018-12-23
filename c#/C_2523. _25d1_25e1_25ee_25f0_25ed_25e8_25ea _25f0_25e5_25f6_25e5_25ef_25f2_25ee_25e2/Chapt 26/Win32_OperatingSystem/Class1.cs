using System;
using System.Management;

namespace Win32_OperatingSystem
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = 
				new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("Boot device name " + mo["BootDevice"]);
				Console.WriteLine("Build number" + mo["BuildNumber"]);
				Console.WriteLine("Caption" + mo["Caption"]);
				Console.WriteLine("Code page used by OS" + mo["CodeSet"]);
				Console.WriteLine("Country code" + mo["CountryCode"]);
				Console.WriteLine("Latest service pack installed" +
					mo["CSDVersion"]);
				Console.WriteLine("Computer system name" + mo["CSName"]);
				Console.WriteLine("Time zone (minute offset from GMT" +
					mo["CurrentTimeZone"]);
				Console.WriteLine("OS is debug build" + mo["Debug"]);
				Console.WriteLine("OS is distributed across several nodes." +
					mo["Distributed"]);
				Console.WriteLine("Encryption level of transactions" +
					mo["EncryptionLevel"] + " bits");
				Console.WriteLine(">>Priority increase for foreground app" +
					GetForeground(mo));
				Console.WriteLine("Available physical memory" +
					mo["FreePhysicalMemory"] + " kilobytes");
				Console.WriteLine("Available virtual memory" +
					mo["FreeVirtualMemory"] + " kilobytes");
				Console.WriteLine("Free paging-space withou swapping." +
					mo["FreeSpaceInPagingFiles"]);
				Console.WriteLine("Installation date " +
					ManagementDateTimeConverter.ToDateTime(
					mo["InstallDate"].ToString()));
				Console.WriteLine("What type of memory optimization......" +
					(Convert.ToInt16(mo["LargeSystemCache"]) == 0 ?
					"for applications" : "for system performance"));
				Console.WriteLine("Time from last boot " +
					mo["LastBootUpTime"]);
				Console.WriteLine("Local date and time " +
					ManagementDateTimeConverter.ToDateTime(
					mo["LocalDateTime"].ToString()));
				Console.WriteLine("Language identifier (LANGID) " +
					mo["Locale"]);
				Console.WriteLine("Local date and time. " +
					ManagementDateTimeConverter.ToDateTime(
					mo["LocalDateTime"].ToString()));
				Console.WriteLine("Max# of processes supported by OS" +
					mo["MaxNumberOfProcesses"]);
				Console.WriteLine("Max memory available for process." +
					mo["MaxProcessMemorySize"] + " kilobytes");
				Console.WriteLine("Current number of processes" +
					mo["NumberOfProcesses"]);
				Console.WriteLine("Currently stored user sessions." +
					mo["NumberOfUsers"]);
				Console.WriteLine("OS language version" + mo["OSLanguage"]);
				Console.WriteLine("OS product suite version" + GetSuite(mo));
				Console.WriteLine("OS type" + GetOSType(mo));
				Console.WriteLine("Number of Windows Plus! " +
					mo["PlusProductID"]);
				Console.WriteLine("Version of Windows Plus! " +
					mo["PlusVersionNumber"]);
				Console.WriteLine("Type of installed OS. " +
					GetProductType(mo));
				Console.WriteLine("Registered user of OS. " +
					mo["RegisteredUser"]);
				Console.WriteLine("Serial number of product. " +
					mo["SerialNumber"]);
				Console.WriteLine("Serial number of product. " +
					mo["SerialNumber"]);
				Console.WriteLine("ServicePack major version. " +
					mo["ServicePackMajorVersion"]);
				Console.WriteLine("ServicePack minor version. " +
					mo["ServicePackMinorVersion"]);
				Console.WriteLine("Total number to store in paging files" +
					mo["SizeStoredInPagingFiles"] + " kilobytes");
				Console.WriteLine("Status. " + mo["Status"]);
				Console.WriteLine("ServicePack minor version. " +
					mo["ServicePackMinorVersion"]);
				Console.WriteLine("OS suite. " + GetOSSuite(mo));
				Console.WriteLine("Physical disk partition with OS. " +
					mo["SystemDevice"]);
				Console.WriteLine("System directory. " +
					mo["SystemDirectory"]);
				Console.WriteLine("Total virtual memory. " +
					mo["TotalVirtualMemorySize"] + " kilobytes");
				Console.WriteLine("Total physical memory. " +
					mo["TotalVisibleMemorySize"] + " kilobytes");
				Console.WriteLine("Version number of OS. " + mo["Version"]);
				Console.WriteLine("Windows directory. " +
					mo["WindowsDirectory"]);
			}
		}

		private static string GetForeground(ManagementObject mo)
		{
			int i = Convert.ToInt16(mo["ForegroundApplicationBoost"]);
			switch (i)
			{
				case 0:  return "None";
				case 1:  return "Minimum";
				case 2:  return "Maximum (defualt value)";
			}
			return "Boost not defined.";
		}

		private static string GetSuite(ManagementObject mo)
		{
			uint i = Convert.ToUInt32(mo["OSProductSuite"]);
			switch (i)
			{
				case 1:   return "Small Business";
				case 2:   return "Enterprise";
				case 4:   return "BackOffice";
				case 8:   return "Communication Server";
				case 16:  return "Terminal Server";
				case 32:  return "Small Business (Restricted)";
				case 64:  return "Embedded NT";
				case 128: return "Data Center";
			}
			return "OS suite not defined.";
		}

		// Тип операционной системы
		private static string GetOSType(ManagementObject mo)
		{
			uint i = Convert.ToUInt16(mo["OSType"]);
			switch (i)
			{
				case 16:  return "WIN95";
				case 17:  return "WIN98";
				case 18:  return "WINNT";
				case 19:  return "WINCE";
			}
			return "Other OS systems aren not covered.";
		}

		private static string GetProductType(ManagementObject mo)
		{
			uint i = Convert.ToUInt32(mo["ProductType"]);
			switch (i)
			{
				case 1:  return "Work Station";
				case 2:  return "Domain Controller";
				case 3:  return "Server";
			}
			return "Product type not defined.";
		}

		private static string GetOSSuite(ManagementObject mo)
		{
			uint i = Convert.ToUInt32(mo["SuiteMask"]);
			
			string suite = "";
			if ((i & 1) == 1) suite += "Small Business";
			if ((i & 2) == 2)
			{
				if (suite.Length > 0) suite += ", "; suite += "Enterprise";
			}
			if ((i & 4) == 4)
			{
				if (suite.Length > 0) suite += ", "; suite += "Back Office";
			}
			if ((i & 8) == 8)
			{
				if (suite.Length > 0) 
					suite += ", ";
				suite += "Communications";
			}
			if ((i & 16) == 16)
			{
				if (suite.Length > 0) 
					suite += ", "; 
				suite += "Terminal";
			}
			if ((i & 32) == 32)
			{
				if (suite.Length > 0) 
					suite += ", "; 
				suite += "Small Business Restricted";
			}
			if ((i & 64) == 64)
			{
				if (suite.Length > 0) 
					suite += ", "; 
				suite += "Embedded NT";
			}
			if ((i & 128) == 128)
			{
				if (suite.Length > 0) 
					suite += ", "; 
				suite += "Data Center";
			}
			if ((i & 256) == 256)
			{
				if (suite.Length > 0) 
					suite += ", ";
				suite += "Single User";
			}
			if ((i & 512) == 512)
			{
				if (suite.Length > 0) 
					suite += ", "; 
				suite += "Personal";
			}
			if ((i & 1024) == 1024)
			{
				if (suite.Length > 0) 
					suite += ", "; 
				suite += "Blade";
			}
			return suite;
		}
	}
}
