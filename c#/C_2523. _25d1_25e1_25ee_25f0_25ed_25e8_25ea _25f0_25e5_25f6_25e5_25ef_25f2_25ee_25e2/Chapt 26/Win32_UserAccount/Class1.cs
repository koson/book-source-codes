using System;
using System.Management;

namespace Win32_UserAccount
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Выбираем все локальные группы, не группы домена
			// Можно заменить условие LocalAccount на, например,
			// условие Domain = 'domain_name'
			WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_UserAccount WHERE LocalAccount =  'true'");
			ManagementObjectSearcher find = new ManagementObjectSearcher(query);
			Console.WriteLine("------------");
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("Caption." + mo["Caption"]);
				Console.WriteLine("Description." + mo["Description"]);
				Console.WriteLine("Domain where account belongs." +
					mo["Domain"]);
				Console.WriteLine("Account is defined on local machine. " +
					mo["LocalAccount"]);
				Console.WriteLine("Name of the account " + mo["Name"]);
				Console.WriteLine("Password can be changed." +
					mo["PasswordChangeable"]);
				Console.WriteLine("Password expires." +
					mo["PasswordExpires"]);
				Console.WriteLine("Password is required for this account." +
					mo["PasswordRequired"]);
				Console.WriteLine("Security identifier (SID)." + mo["SID"]);
				Console.WriteLine("Type of security identifier." +
					GetSidType(Convert.ToInt32(mo["SIDType"])));
				Console.WriteLine("Status." + mo["Status"]);
				Console.WriteLine("-------------");
			}
		}
  
		public static string GetSidType(int type)
		{
			switch (type)
			{
				case 1: return "SidTypeUser";
				case 2: return "SidTypeGroup";
				case 3: return "SidTypeDomain";
				case 4: return "SidTypeAlias";
				case 5: return "SidTypeWellKnownGroup";
				case 6: return "SidTypeDeletedAccount";
				case 7: return "SidTypeInvalid";
				case 8: return "SidTypeUnknown";
				case 9: return "SidTypeComputer";
			}
			return string.Empty;
		}
	}
}
