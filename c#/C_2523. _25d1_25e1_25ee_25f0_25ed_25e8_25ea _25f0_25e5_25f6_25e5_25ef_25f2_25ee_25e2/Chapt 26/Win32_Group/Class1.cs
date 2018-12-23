using System;
using System.Management;

namespace Win32_Group
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"Select * from Win32_Group where LocalAccount = 'true'");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			Console.WriteLine("-------------");
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("Caption." + mo["Caption"]);
				Console.WriteLine("Description." + mo["Description"]);
				Console.WriteLine("Domain where group belongs." +
					mo["Domain"]);
				Console.WriteLine("Account is defined on local machine." +
					mo["LocalAccount"]);
				Console.WriteLine("Name of the group." + mo["Name"]);
				Console.WriteLine("Security identifier (SID)." + mo["SID"]);
				Console.WriteLine("Type of security identifier. " +
					GetSidType(Convert.ToInt32(mo["SIDType"])));
				Console.WriteLine("Status." + mo["Status"]);
				Console.WriteLine("------------");
			}
		}
    
		public static string GetSidType(int type)
		{
			switch (type)
			{
				case 1:  return "SidTypeUser";
				case 2:  return "SidTypeGroup";
				case 3:  return "SidTypeDomain";
				case 4:  return "SidTypeAlias";
				case 5:  return "SidTypeWellKnownGroup";
				case 6:  return "SidTypeDeletedAccount";
				case 7:  return "SidTypeInvalid";
				case 8:  return "SidTypeUnknown";
				case 9:  return "SidTypeComputer";
			}
			return string.Empty;
		}
	}
}
