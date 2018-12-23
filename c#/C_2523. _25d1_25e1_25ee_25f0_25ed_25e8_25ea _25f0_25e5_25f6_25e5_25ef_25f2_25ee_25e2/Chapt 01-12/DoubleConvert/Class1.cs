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

			// вызовет исключение, если десятичный 
			// разделитель запятая, а не точка
			string str_2 = "55.6"; 
			try
			{
				Console.WriteLine(Convert.ToDouble(str_2));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			// TryParse умеет понимать не только десятичные разделители, но и
			// разделители разрядов
			double retNum;
			Double.TryParse(str_1, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
			Console.WriteLine(retNum); // преобразуется в 556, если запятая является разделителем разрядов
			Double.TryParse(str_2, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
			Console.WriteLine(retNum);   // преобразуется в 55.6, если точка является десятичным разделителем

			Console.WriteLine(Double.Parse(str_1));


			// Заменяем и точку и запятую на текущее значение
			// десятичного разделителя
			string str_3 = "55,6";
			string decimal_sep = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
			str_3 = str_3.Replace(".", decimal_sep);
			str_3 = str_3.Replace(",", decimal_sep);
			Console.WriteLine(decimal.Parse(str_3));


			Console.ReadLine();
		}
	}
}
