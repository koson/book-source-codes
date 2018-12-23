using System;
using System.Threading;

namespace Threads_Events
{
	public class ThreadClass
	{
		private bool Stop;
		private AutoResetEvent ev;

		public ThreadClass()
		{
			Stop = false;
			ev   = new AutoResetEvent(false);
		}

		public void Signal()
		{
			ev.Set();
		}

		// Основной метод потока
		public void Execute()
		{
			// Основной цикл потока
			while (!Stop)
			{
				try
				{
					for (int i=0; i<1000; i++)
					{
						Console.WriteLine(string.Format("Выполнение потока i={0}", i));
					}

					// Спать, пока не будет сигнала
					ev.WaitOne();
				}
				catch (ThreadInterruptedException)
				{
					Stop = true;
				}
			}

			Console.WriteLine("Выполнение потока завершено.");
		}
	}
}
