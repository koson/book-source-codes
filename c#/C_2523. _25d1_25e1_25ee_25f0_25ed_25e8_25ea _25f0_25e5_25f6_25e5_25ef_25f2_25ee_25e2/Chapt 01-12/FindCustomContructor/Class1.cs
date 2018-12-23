using System;
using System.Reflection;

namespace FindCustomContructor
{
	class TestClass
	{
		TestClass()
		{
		}

		TestClass(int x, string str)
		{
		}
	}

	class MainClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			Type type = typeof(TestClass);

			// Пробуем найти обычный конструтор
			ConstructorInfo ci1 = type.GetConstructor(new Type[]{});
			Console.WriteLine("Конструктор 1 "+ ((ci1 == null)?"найден":"не найден"));

			// Пробуем найти конструтор с параметрами
			ConstructorInfo ci2 = type.GetConstructor(
				new Type[]{typeof(int), typeof(string)});
			Console.WriteLine("Конструктор 2 "+ 
				((ci2 == null)?"найден":"не найден"));
			Console.ReadLine();
		}
	}
}
