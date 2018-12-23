using System;
using System.IO;
using System.Text;

namespace ConsoleOutput
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ���� ��� ������
			FileStream file = new FileStream("Test.txt", FileMode.Create);
			// ��������� ����������� �������
			TextWriter out_save = Console.Out;
			// ������������� ���� ��� ������ ������ 
			TextWriter out_file = new StreamWriter(file);
			Console.SetOut(out_file);

			// ������� �������� � ����
			Console.WriteLine("write to file");
 
			// ��������������� ����������� �������
			Console.SetOut(out_save);
			Console.WriteLine("write to console");
 
			// ��������� ����
			out_file.Close();
		}
	}
}
