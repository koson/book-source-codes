using System;

namespace EscapeChars
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
/*
\b � ������ Backspace
\f � ������� �������� (Form feed) 
\n � ������� ������
\r � ������� ������� 
\t � �������������� ���������
\v � ������������ �������
\uxxxx � ������ Unicode � ��������� ����������������� ����� 
\xn[n][n][n] � ������ Unicode � ��������� ����� 
\Uxxxxxxxx � ������ Unicode � ��������� �����
*/
			Console.Write("\'\"\0\\");   // ������� � ������� ������
			Console.Write("\a"); // ������
			Console.Write("test\b\n\r\t\v\v\n");
			Console.Write("\u0024"); 
			Console.Write("\x24"); 
			Console.Write("\U00000024"); 


			Console.ReadLine();
		}
	}
}
