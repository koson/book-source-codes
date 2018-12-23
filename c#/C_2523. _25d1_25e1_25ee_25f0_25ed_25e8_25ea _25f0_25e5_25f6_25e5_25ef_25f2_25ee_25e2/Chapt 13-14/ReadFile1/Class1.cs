using System;
using System.IO;

namespace ReadFile1
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Создать объект для чтения файла
			StreamReader reader;
			try
			{
				reader = new StreamReader("input.txt");
			}
			catch
			{
				// При открытии файла получили исключение
				Console.WriteLine("Ошибка открытия файла");
				return;
			}
			// Посимвольное чтение файла
			int ch;
			while ((ch = reader.Read()) != -1)
			{
				Console.WriteLine(ch);
			}
		}
	}
}
