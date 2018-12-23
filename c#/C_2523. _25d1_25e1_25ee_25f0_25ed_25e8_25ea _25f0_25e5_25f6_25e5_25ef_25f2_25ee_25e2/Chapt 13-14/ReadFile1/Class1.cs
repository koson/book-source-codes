using System;
using System.IO;

namespace ReadFile1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ������� ������ ��� ������ �����
			StreamReader reader;
			try
			{
				reader = new StreamReader("input.txt");
			}
			catch
			{
				// ��� �������� ����� �������� ����������
				Console.WriteLine("������ �������� �����");
				return;
			}
			// ������������ ������ �����
			int ch;
			while ((ch = reader.Read()) != -1)
			{
				Console.WriteLine(ch);
			}
		}
	}
}
