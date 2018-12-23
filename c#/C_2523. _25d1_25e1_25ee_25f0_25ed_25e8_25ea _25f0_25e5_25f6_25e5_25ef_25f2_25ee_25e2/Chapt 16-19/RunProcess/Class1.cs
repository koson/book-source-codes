using System;
using System.Diagnostics;
using System.IO;

namespace RunProcess
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ProcessStartInfo startinfo;
			Process          process = null;
			OperatingSystem  os;
			string           command, stdoutline;
			StreamReader     stdoutreader;

			// ������� ������� ����� ���������
			command = "NETSTAT -a";
            
			try
			{
				// �������� ������ ���� ��� WinNT
				os = Environment.OSVersion;
				if (os.Platform != PlatformID.Win32NT)
				{
					throw new PlatformNotSupportedException("Supported on Windows NT or later only");
				}
				os = null;

				// ��������
				if (command == null || command.Trim().Length == 0)
				{
					throw new ArgumentNullException("command");
				}

				startinfo = new ProcessStartInfo();
				// ��������� ����� cmd
				startinfo.FileName = "cmd.exe";  
				// ���� /c - ���������� �������
				startinfo.Arguments = "/C " + command;  
				// �� ���������� shellexecute
				startinfo.UseShellExecute = false;   
				// ������������� ����� �� ������� �������
				startinfo.RedirectStandardOutput = true;
				// �� ���� ����
				startinfo.CreateNoWindow = true;
				// ��������
				process = Process.Start(startinfo);
                
				// �������� �����
				stdoutreader = process.StandardOutput;
				while((stdoutline=stdoutreader.ReadLine())!= null)
				{
					// �������
					Console.WriteLine(stdoutline);
				}
				stdoutreader.Close();
				stdoutreader = null;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (process != null)
				{
					// ���������
					process.Close();
				}

				// �����������
				process = null;
				startinfo = null;
			}

			Console.ReadLine();
		}
	}
}
