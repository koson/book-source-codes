using System;
using System.Globalization;
using System.Threading;

namespace DecimalSeparator
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine(1.5F); // ������� 1.5

			//������ �������� ������ CurrentCulture
			//CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator= "#";

			//��� ��������� ����� ������� ����� ��������� CultureInfor
			//� �������� � ��� ������ ����
			CultureInfo newCInfo = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
			newCInfo.NumberFormat.NumberDecimalSeparator = "#";
			Thread.CurrentThread.CurrentCulture = newCInfo;

			Console.WriteLine(1.5F); // ������� 1#5

			Console.ReadLine();
		}
	}
}
