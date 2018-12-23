using System;
using System.Management;

namespace Win32_Process_1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ������ ���������� ��������
			WqlObjectQuery query_run = new WqlObjectQuery(
				"SELECT * FROM Win32_Service WHERE state='running'");
			ManagementObjectSearcher find_run =
				new ManagementObjectSearcher(query_run);
  
			foreach (ManagementObject mo in find_run.Get())
			{
				Console.WriteLine(
					"��� �������    : " + mo["DisplayName"] + 
					"- �����        : " + mo["StartMode"]   + 
					"- ��������     : " + mo["Description"]
					);
			}
  
			Console.WriteLine(new string('-', 20));
  
			// ������ ������������� ��������
			WqlObjectQuery query_stp = new WqlObjectQuery(
				"SELECT * FROM Win32_Service WHERE state='stopped'");
			ManagementObjectSearcher find_stp = 
				new ManagementObjectSearcher(query_stp);
  
			foreach (ManagementObject mo in find_stp.Get())
			{
				Console.WriteLine(
					"������     : " + mo["DisplayName"] + 
					"�- �����   : " + mo["StartMode"]   + 
					"-- ��������: " + mo["Description"]
					);
			}
		}
	}
}
