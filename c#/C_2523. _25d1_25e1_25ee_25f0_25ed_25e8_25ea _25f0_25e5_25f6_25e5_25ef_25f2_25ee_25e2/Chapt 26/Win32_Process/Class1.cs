using System;
using System.Management;

namespace Win32_Process
{
	class Class1
	{
		const string Query = "SELECT * FROM Win32_Process";
		const int TextSize = 256;
		enum ResultCode:uint
		{
			SuccessfullCompletion = 0, 
			AccessDenied              = 2, 
			InsufficientPrivilege = 3, 
			UnknownFailure = 4, 
			PathNotFound = 9, 
			InvalidParameter = 21
		}

		[STAThread]
		static void Main(string[] args)
		{
			ManagementObjectSearcher processEnumerator = new ManagementObjectSearcher(Query);
			foreach(ManagementObject process in processEnumerator.Get())
			{
				Console.WriteLine("Процесс: " + process["Name"] as string);
				object[] parameters = new object[2];
				ResultCode result = (ResultCode)process.InvokeMethod("GetOwner", parameters);
				if (result == ResultCode.SuccessfullCompletion)
				{
					Console.WriteLine("Пользователь: " + parameters[0]);
					Console.WriteLine("Домен: " + parameters[1]);
				}
				else
				{
					Console.Write("Ошибка: " + result);
				}
				Console.WriteLine(Environment.NewLine);
			}
			Console.ReadLine();
		}
	}
}
