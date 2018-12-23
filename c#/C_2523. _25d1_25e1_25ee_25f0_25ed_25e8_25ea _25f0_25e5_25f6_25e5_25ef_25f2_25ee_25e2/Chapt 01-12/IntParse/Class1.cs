using System;
using System.Globalization;

namespace IntParse
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			int i;

			// �������������� �� ������ � �����
			// ���� �������� ����� null ����� ����������
			i = int.Parse("123");
			Console.WriteLine(i);

			// �������������� �� ������ � �����, ��
			// ���� �������� ����� null, ������ 0
			i = Convert.ToInt32("123");
			Console.WriteLine(i);

			// ��������� ����������� � ������
			i = int.Parse("  123", NumberStyles.AllowLeadingWhite, null);
			Console.WriteLine(i);

			// ��������� ����������� � ������� �����
			i = int.Parse("  -123 ", NumberStyles.Integer, null);
			Console.WriteLine(i);

			// ���������������� �����
			i = int.Parse("1F3", NumberStyles.HexNumber, null);
			Console.WriteLine(i);

			// ���������������� �����
			i = Convert.ToInt32("1F3", 16);
			Console.WriteLine(i);

			Console.ReadLine();
		}
	}
}
