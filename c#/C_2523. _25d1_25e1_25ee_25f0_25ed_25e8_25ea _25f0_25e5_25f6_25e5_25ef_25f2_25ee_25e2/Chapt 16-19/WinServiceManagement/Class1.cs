using System;
using System.ServiceProcess;

namespace WinServiceManagement
{
	class Class1
	{
		static void Main()
		{
			ServiceController sc;
			string            serviceName;
			TimeSpan          timeout = new TimeSpan(0, 0, 30);
			OperatingSystem   os;

			os = Environment.OSVersion;
			if (os.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException("��� ������ ����� NT, 2000, XP ��� ����");
			}
			os = null;

			// ��� ������� (��������, telnet)
			serviceName = "Telnet";

			// ������� ����������
			sc = new ServiceController(serviceName);

			// ��������� ������ ��������
			if (sc.Status == ServiceControllerStatus.Running)
			{
				// ������� ����� ����������?
				if (sc.CanStop)
				{
					Console.WriteLine("������� ������� {0}", serviceName);
					sc.Stop();

					try
					{
						// �������� 30 ������
						sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
						Console.WriteLine("������ {0} ������� ����������", serviceName);
					}
					catch(TimeoutException)
					{
						Console.WriteLine("�� ������� ���������� ������ {0}", serviceName);
					}
				}
				else
				{
					Console.WriteLine("������ {0} �� ����� ���� ����������", serviceName);
				}
			}

			// ���� ������ ���������� - �������� ���
			if (sc.Status == ServiceControllerStatus.Stopped)
			{
				// ��������
				sc.Start();

				try
				{
					// ���� 30 ���
					sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
					Console.WriteLine("������ {0} ������� ���������", serviceName);
				}
				catch(TimeoutException)
				{
					Console.WriteLine("�� ������� ��������� ������ {0}", serviceName);
				}
			}

			// ������� ����������
			sc.Close();
			sc = null;

			Console.ReadLine();
		}
	}
}
