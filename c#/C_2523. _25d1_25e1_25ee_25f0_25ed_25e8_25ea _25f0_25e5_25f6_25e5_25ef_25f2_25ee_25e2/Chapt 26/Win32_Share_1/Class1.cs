using System;
using System.Management;

namespace Win32_Share_1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string folderName = @"C:\MyTestShare";
			string shareName  = @"My Files Share";

			try
			{
				// ������� ������ ManagementClass
				ManagementClass managementClass = 
					new ManagementClass("Win32_Share");
				// ������� ������ ManagementBaseObjects
				ManagementBaseObject inParams =
					managementClass.GetMethodParameters("Create");
				ManagementBaseObject outParams;
				// ������ ��������� ������
				inParams["Description"] = shareName;
				inParams["Name"] = shareName;
				inParams["Path"] = folderName;
				inParams["Type"] = 0x0; // Disk Drive
				// �������� ����� Create
				outParams = managementClass.InvokeMethod(
					"Create", inParams, null);
				// ��������� ���������
				if((uint)(outParams.Properties["ReturnValue"].Value) != 0)
				{
					throw new Exception("�� ������� ������� ������� �����.");
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
