using System;
using System.Threading;

namespace Threads_Pool
{
    class Class1
    {
		[STAThread]
		static void Main(string[] args)
		{
			// Функция потока
			WaitCallback callback = new WaitCallback(ThreadFunction);

			// Создаем три потока в пуле
			ThreadPool.QueueUserWorkItem(callback, "Thread1");
			ThreadPool.QueueUserWorkItem(callback, "Thread2");

			// Unsafe-поток (нет ограничения на доступ к ресурсам)
			ThreadPool.UnsafeQueueUserWorkItem(callback, "Thread3");

			// Просто выходим по Enter
			Console.WriteLine("Нажмите Enter для выхода...");  
			Console.ReadLine();
		}
		
		static void ThreadFunction(object state)
		{
			Console.WriteLine("Поток с параметром {0}", state.ToString());
			Thread.Sleep(1000);
			Console.WriteLine("Завершение потока {0}", state.ToString());
		}
			
    }
}
