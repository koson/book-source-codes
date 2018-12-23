using System;

namespace WeakReferenceTest
{
	class TestClass
	{
		public TestClass()
		{
			Console.WriteLine("Создание объекта TestClass");
		}

		public void Print()
		{
			Console.WriteLine("Метод Print");
		}
	}

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Сильная ссылка
			TestClass tc = new TestClass();
			// Слабая ссылка
			WeakReference wr = new WeakReference(tc);

			// Вызываем сборщик мусора
			GC.Collect();
			GC.WaitForPendingFinalizers();

			// Объект не уничтожится, т.к. 
			// есть сильная ссылка
			if (wr.IsAlive)
				(wr.Target as TestClass).Print();
			else
				Console.WriteLine("Объект уничтожен");

			// Уничтожаем сильную ссылку
			tc = null;

			// Вызываем сборщик мусора
			GC.Collect();
			GC.WaitForPendingFinalizers();

			// Объект уничтожится
			if (wr.IsAlive)
				(wr.Target as TestClass).Print();
			else
				Console.WriteLine("Объект уничтожен");

			Console.ReadLine();
		}
	}
}
