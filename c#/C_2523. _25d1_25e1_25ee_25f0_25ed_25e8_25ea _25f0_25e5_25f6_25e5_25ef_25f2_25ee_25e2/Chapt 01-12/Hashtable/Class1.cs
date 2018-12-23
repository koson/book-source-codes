using System;
using System.Collections;

namespace ConsoleApplication50
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Заполняем хеш
			Hashtable hash = new Hashtable();
			hash.Add(1, "1");
			hash.Add(2, "2");
			hash.Add(3, "3");

			foreach (DictionaryEntry d in hash)
			{
				Console.WriteLine("{0}={1}", d.Key, d.Value);
			}

			// Установка null не удаляет элемент
			Console.WriteLine(new string('-', 10));
			hash[2] = null;

			foreach (DictionaryEntry d in hash)
			{
				Console.WriteLine("{0}={1}", d.Key, d.Value);
			}

			// Метод Remove удаляет элемент
			Console.WriteLine(new string('-', 10));
			hash.Remove(2);

			foreach (DictionaryEntry d in hash)
			{
				Console.WriteLine("{0}={1}", d.Key, d.Value);
			}

			Console.ReadLine();
		}
	}
}
