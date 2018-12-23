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
			// Файл для вывода
			FileStream file = new FileStream("Test.txt", FileMode.Create);
			// Сохраняем стандартную консоль
			TextWriter out_save = Console.Out;
			// Устанавливаем файл для вывода данных 
			TextWriter out_file = new StreamWriter(file);
			Console.SetOut(out_file);

			// Пробуем выводить в файл
			Console.WriteLine("write to file");
 
			// Восстанавливаем стандартную консоль
			Console.SetOut(out_save);
			Console.WriteLine("write to console");
 
			// Закрываем файл
			out_file.Close();
		}
	}
}
