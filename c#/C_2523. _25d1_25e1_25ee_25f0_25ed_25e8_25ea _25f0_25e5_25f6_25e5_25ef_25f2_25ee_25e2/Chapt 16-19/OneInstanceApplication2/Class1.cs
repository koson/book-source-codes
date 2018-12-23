using System;
using System.Diagnostics;
using System.Reflection;

namespace OneInstanceConsoleApplication
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Process process = RunningInstance();
			if (process != null)
			{
				Console.WriteLine("��������� ��� ��������");
				return;
			}

			Console.WriteLine("���������� ��������. ������� ENTER ��� ������");
			Console.ReadLine();
		}

		public static Process RunningInstance() 
		{ 
			Process current = Process.GetCurrentProcess(); 
			Process[] processes = Process.GetProcessesByName (current.ProcessName); 

			//������������� ��� �������� 
			foreach (Process process in processes) 
			{ 
				//���������� ������� �������
				if (process.Id != current.Id) 
				{ 
					//���������, ��� ������� ������� �� ���� �� ����� 
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
 					{ 
 						//��, ��� � ���� ����� ������ ����������
 						return process; 
 					} 
 				} 
 			} 
 			//���, ����� �� ��������� �� �������
 			return null; 
 		} 

	}
}
