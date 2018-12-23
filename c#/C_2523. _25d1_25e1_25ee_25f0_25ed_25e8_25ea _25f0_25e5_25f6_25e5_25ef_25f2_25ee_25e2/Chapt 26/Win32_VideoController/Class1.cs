using System;
using System.Management;

namespace Win32_VideoController
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ManagementScope sc= 
				new ManagementScope(@"\\.\root\cimv2", null); 
			ManagementPath ph= 
				new ManagementPath(@"Win32_VideoController");
			ManagementClass mc= 
				new  ManagementClass(sc, ph, null);

			foreach(ManagementObject ss in mc.GetInstances())
			{
				Console.WriteLine("Name: {0}", ss.GetPropertyValue("Name"));
				Console.WriteLine(" Processor  {0}",
					ss.GetPropertyValue("VideoProcessor"));
				Console.WriteLine(" VideoRAM   {0}",
					ss.GetPropertyValue("AdapterRAM"));
				Console.WriteLine(" Resolution {0}",
					ss.GetPropertyValue("VideoModeDescription"));
				Console.WriteLine(" Refresh    {0}",
					ss.GetPropertyValue("CurrentRefreshRate"));
			}
		}
	}
}
