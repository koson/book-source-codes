using System;
using System.Threading;

namespace Threads_Standard
{
	public class ThreadClass
	{
		// Основной метод потока
		public void Execute()
		{
			// Какие-то действия...
			Thread.Sleep(30000); // 30 сек
			Console.WriteLine("END");  
		}
	}

	class Threads_Standard
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Создаем класс потока
			ThreadClass tc = new ThreadClass();
			// Создаем сам поток
			Thread t = new Thread(new ThreadStart(tc.Execute));
			// Старт потока
			t.Start();

			// Еще один поток с помощью статического метода
			Thread t1 = new Thread(new ThreadStart(Static_Execute));
			t1.Start();

			// После первого нажатия Enter завершим поток
			Console.WriteLine("Нажмите Enter для завершения потоков");  
			Console.ReadLine();
			t.Abort();
			t1.Abort();

			// Ждать реального завершения потока
			t.Join(1000); // в течении 1 секунды
			t1.Join(); // просто ждать

			// Просто выходим по Enter
			Console.WriteLine("Нажмите Enter для выхода...");  
			Console.ReadLine();
		}

		// Статический метод
		static void Static_Execute()
		{
		}

	}
}
