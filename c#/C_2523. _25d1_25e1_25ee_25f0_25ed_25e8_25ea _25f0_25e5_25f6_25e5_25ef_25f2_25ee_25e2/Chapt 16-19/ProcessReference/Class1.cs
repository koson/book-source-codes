using System;
using System.Diagnostics;
using System.IO;

namespace ProcessReference
{
	class Class1
	{
		static void Main()
		{
			Process p;
            
			// �������� ������� �������
			p = Process.GetCurrentProcess();

			// ������� ��� DLL, ��������� � ���
			foreach(ProcessModule module in p.Modules)
			{
				Print(module.ModuleName, 20);
				Print(module.FileName  , 75);
				Print(module.FileVersionInfo.FileVersion, 20);
				Print(File.GetLastWriteTime(module.FileName).ToShortDateString(), 20);
				Console.WriteLine();
			}
			p.Close();
			p = null;
			Console.ReadLine();
		}

		// ����������� ������� �������������� ������
		static void Print(string towrite, int maxlen)
		{
			if (towrite.Length >= maxlen)
			{
				Console.Write(towrite.Substring(0, maxlen - 4)+"... ");
				return;
			}
			towrite += new string(' ', maxlen - towrite.Length);

			Console.Write(towrite);
		}
	}
}

