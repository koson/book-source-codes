using System;
using System.Threading;

namespace Threads_Create
{
	// Описание callback-функции потока
	public delegate void ThreadCallback(string Name, int Counter);

	public class ThreadClass
	{
		private string Name;
		private int	   Counter;
		private ThreadCallback Callback;
		private bool   Stop;

		public ThreadClass(string Name, ThreadCallback Callback)
		{
			this.Name	  = Name;
			this.Callback = Callback;

			Counter = 0;
			Stop    = false;
		}

		// Основной метод потока
		public void Execute()
		{
			// Имя потока
			Thread.CurrentThread.Name = Name;
			// Приоритет
			Thread.CurrentThread.Priority = ThreadPriority.Normal;

			// Основной цикл потока
			while (!Stop)
			{
				try
				{
					Console.WriteLine(string.Format("Выполнение потока {0}", Name));
					Counter++;

					// Вызов callback
					if (Callback != null)
						Callback(Name, Counter);

					// Спать заданное кол-во мс
					Thread.Sleep(1);
				}
				// При вызове метода Abort будет сгенерировано исключение,
				// которое мы ловим для завершения потока
				catch (ThreadAbortException ex_abort)
				{
					// Обработать исключение мы можем, но поток уже остановлен
					// Сообщение про завершение потока не будет
					Console.WriteLine(string.Format("Завершение потока {0} (ThreadAbortException)", Name));
					Console.WriteLine(string.Format("Информация: {0}", (string)ex_abort.ExceptionState));
				}
				catch (ThreadInterruptedException)
				{
					Console.WriteLine(string.Format("Завершение потока {0} (ThreadInterruptedException)", Name));
					// Это исключение только спрашивает о возможности завершения потока
					// мы должны сами завершить поток
					Stop = true;
				}
			}

			Console.WriteLine(string.Format("Выполнение потока {0} завершено.", Name));
		}
	}
}
