using System;

namespace DoubleConvert
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string str_1 = "55,6";
			Console.WriteLine(Convert.ToDouble(str_1));

			// ������� ����������, ���� ���������� 
			// ����������� �������, � �� �����
			string str_2 = "55.6"; 
			try
			{
				Console.WriteLine(Convert.ToDouble(str_2));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			// TryParse ����� �������� �� ������ ���������� �����������, �� �
			// ����������� ��������
			double retNum;
			Double.TryParse(str_1, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
			Console.WriteLine(retNum); // ������������� � 556, ���� ������� �������� ������������ ��������
			Double.TryParse(str_2, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
			Console.WriteLine(retNum);   // ������������� � 55.6, ���� ����� �������� ���������� ������������

			Console.WriteLine(Double.Parse(str_1));


			// �������� � ����� � ������� �� ������� ��������
			// ����������� �����������
			string str_3 = "55,6";
			string decimal_sep = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
			str_3 = str_3.Replace(".", decimal_sep);
			str_3 = str_3.Replace(",", decimal_sep);
			Console.WriteLine(decimal.Parse(str_3));


			Console.ReadLine();
		}
	}
}
