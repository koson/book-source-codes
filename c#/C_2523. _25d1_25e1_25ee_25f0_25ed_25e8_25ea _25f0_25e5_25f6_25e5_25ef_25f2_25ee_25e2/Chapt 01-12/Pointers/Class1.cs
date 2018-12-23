using System;

namespace Pointers
{
	class Class1
	{
		unsafe static void TestPointer() 
		{
			int *p;	 // ���������
			int i;   // ����������

			i = 10;  // ������������� ����������
			p = &i;  // ��������� �� ����������


			// ������� �������� ���������� ������� � ����� ���������
			Console.WriteLine(i);
			Console.WriteLine(*p);
			Console.WriteLine("----------");

			// ����������� ���������� ����� ���������
			*p = 333;
			Console.WriteLine(i);
			Console.WriteLine(*p);
			Console.WriteLine("----------");

			// �������� ���������� ����� ����������� ���������
			i = *(&i) + 10;
			Console.WriteLine(i);
			Console.WriteLine(*p);
		}

		[STAThread]
		static void Main(string[] args)
		{
			TestPointer();
			Console.ReadLine();
		}
	}
}
