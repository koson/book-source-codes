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

			// Преобразование из строки в число
			// Если аргумент будет null будет исключение
			i = int.Parse("123");
			Console.WriteLine(i);

			// Преобразование из строки в число, но
			// если аргумент будет null, вернет 0
			i = Convert.ToInt32("123");
			Console.WriteLine(i);

			// Разрешаем разделители в начале
			i = int.Parse("  123", NumberStyles.AllowLeadingWhite, null);
			Console.WriteLine(i);

			// Разрешаем разделители и символы знака
			i = int.Parse("  -123 ", NumberStyles.Integer, null);
			Console.WriteLine(i);

			// Шеснадцатиричное число
			i = int.Parse("1F3", NumberStyles.HexNumber, null);
			Console.WriteLine(i);

			// Шеснадцатиричное число
			i = Convert.ToInt32("1F3", 16);
			Console.WriteLine(i);

			Console.ReadLine();
		}
	}
}
