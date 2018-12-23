using System;
using System.Management;

namespace WMITest
{
	class Class1
	{
		static void Main(string[] args)
		{
			ConnectionOptions options = new ConnectionOptions();
			options.Username = @"domain\username";
			options.Password = "password";
			ManagementScope scope = 
				new ManagementScope(@"\\machine_name\root\cimv2", options);
			scope.Connect();

			try
			{
				// работа с функциями WMI
			} 
			catch (Exception e)  
			{
				// обработка ошибок
			}
		}
	}
}
