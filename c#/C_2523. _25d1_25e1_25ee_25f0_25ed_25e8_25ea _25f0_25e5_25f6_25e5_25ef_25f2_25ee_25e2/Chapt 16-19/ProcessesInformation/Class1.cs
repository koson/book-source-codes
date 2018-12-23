using System;
using System.Diagnostics;

namespace ProcessesInformation
{
	class SampleProcessMgmt
	{
		static void Main()
		{
			// �������� ������ ��������� �� ��������� ������
			Process[] procs = Process.GetProcesses();
			
			// ����� �������� ������ ��������� �� ��������� ������
			// Process[] procs = Process.GetProcesses(��� ������);
            
			// "������" ������� ��� � ��������� �����
			Console.Write(Print("Image Name"));
			Console.Write(Print("PID"));
			Console.Write(Print("CPU Time"));
			Console.Write(Print("MEM Usage"));
			Console.Write(Print("Peak MEM Usage"));
			Console.Write(Print("Handles"));
			Console.Write(Print("Threads"));
			Console.WriteLine();
            
			// �������� �� ���� ���������
			foreach(Process proc in procs)
			{
				// ��������� ���������� � ��������
				proc.Refresh();

				string name = proc.ProcessName;
				string pid  = proc.Id.ToString();
                
				//format cpu time to look like hhh:mm:ss
				TimeSpan cputime = proc.TotalProcessorTime;
				string time = String.Format(
					"{0}:{1}:{2}", 
					((cputime.TotalHours-1<0?0:cputime.TotalHours-1)).ToString("##0"), 
					cputime.Minutes.ToString("00"), 
					cputime.Seconds.ToString("00")
					);

				string mem     = (proc.WorkingSet/1024).ToString()+"k";
				string peakmem = (proc.PeakWorkingSet/1024).ToString()+"k";
				string handles = proc.HandleCount.ToString();
				string threads = proc.Threads.Count.ToString();

				// ����������
				Console.Write(Print(name));
				Console.Write(Print(pid));
				Console.Write(Print(time));
				Console.Write(Print(mem));
				Console.Write(Print(peakmem));
				Console.Write(Print(handles));
				Console.Write(Print(threads));
				Console.WriteLine();

				proc.Close();
			}

			procs = null;

			Console.ReadLine();
		}

		// ��������������� �����
		static string Print(string str)
		{
			int maxlen = 16;
			if (str.Length >= maxlen)
			{
				return string.Format("{0}...", str.Substring(0, maxlen - 4));
			}
			str += new string(' ', maxlen - str.Length);
			return str;
		}
	}
}
