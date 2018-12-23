using System;
using System.Management;

namespace Win32_NetworkConnection
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ManagementClass managementClass = new ManagementClass("Win32_NetworkConnection");
			ManagementObjectCollection managementObj = managementClass.GetInstances();

			foreach(ManagementObject mo in managementObj)
			{
				Console.WriteLine("AccessMask:\t{0}", mo["AccessMask"]);
				Console.WriteLine("Caption:\t{0}", mo["Caption"]);
				Console.WriteLine("Comment:\t{0}", mo["Comment"]);
				Console.WriteLine("ConnectionState:\t{0}", mo["ConnectionState"]);
				Console.WriteLine("ConnectionType:\t{0}", mo["ConnectionType"]);
				Console.WriteLine("Description:\t{0}", mo["Description"]);
				Console.WriteLine("DisplayType:\t{0}", mo["DisplayType"]);
				Console.WriteLine("InstallDate:\t{0}", mo["InstallDate"]);
				Console.WriteLine("LocalName:\t{0}", mo["LocalName"]);
				Console.WriteLine("Name:\t{0}", mo["Name"]);
				Console.WriteLine("Persistent:\t{0}", mo["Persistent"]);
				Console.WriteLine("ProviderName:\t{0}", mo["ProviderName"]);
				Console.WriteLine("RemoteName:\t{0}", mo["RemoteName"]);
				Console.WriteLine("RemotePath:\t{0}", mo["RemotePath"]);
				Console.WriteLine("ResourceType:\t{0}", mo["ResourceType"]);
				Console.WriteLine("Status:\t{0}", mo["Status"]);
				Console.WriteLine("UserName:\t{0}", mo["UserName"]);
				Console.WriteLine("-----------------------------------------");
			}
			Console.ReadLine();
		}
	}
}
