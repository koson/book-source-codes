using System;
using Microsoft.Win32;

namespace ReadRegistry
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			RegistryKey hklm =Registry.LocalMachine;
			hklm=hklm.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
			Object obp=hklm.GetValue("Identifier");
			Console.WriteLine("Processor Identifier :{0}",obp);

			RegistryKey hklp =Registry.LocalMachine;
			hklp=hklp.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
			Object obc=hklp.GetValue("VendorIdentifier");
			Console.WriteLine("Vendor Identifier    :{0}",obc);

			RegistryKey biosv =Registry.LocalMachine;
			biosv=biosv.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\MultiFunctionAdapter\\4");
			Object obv=biosv.GetValue("Identifier");
			Console.WriteLine("Bios Status          :{0}",obv);

			RegistryKey biosd =Registry.LocalMachine;
			biosd=biosd.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\");
			Object obd=biosd.GetValue("SystemBiosDate");
			Console.WriteLine("Bios Date            :{0}",obd);

			RegistryKey bios =Registry.LocalMachine;
			bios=bios.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\");
			Object obs=bios.GetValue("Identifier");
			Console.WriteLine("System Identifer     :{0}",obs);

			Console.ReadLine();
		}
	}
}
