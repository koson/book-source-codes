using System;
using System.Threading;
using System.IO;

namespace Threads_Slot
{
	public class ThreadClass
	{
		static Random randomGenerator = new Random();

		// Такой static-объект будет общим между всеми потоками
		// этого класса
		static int intShare;

		// Такой static-объект будет уникальным для потока
		[ThreadStatic]
		static int intUnic;

		// Данные из слота будут разными для всех потоков
		protected static int SlotData
		{
			get
			{
				object data = Thread.GetData(Thread.GetNamedDataSlot("MySlot"));
				if (data == null)
					return 0;
				return Convert.ToInt32(data);
			}
			set
			{
				Thread.SetData(Thread.GetNamedDataSlot("MySlot"), value);
			}
		}
		
		// Статический метод потока
		public static void Execute()
		{
			SlotData = randomGenerator.Next(1, 200);
			intShare = SlotData;
			intUnic  = SlotData;

			bool Stop = false;

			while (!Stop)
			{
				try
				{
					// Хотя класс потока статический
					// Slot-данные будут иметь разное значение
					// а обычный static будет перезаписан 
					Console.WriteLine("SlotData={0} intShare={1} intUnic={2}", SlotData, intShare, intUnic);
					Thread.Sleep(1000);
				}
				catch (ThreadAbortException)
				{
				}
				catch (ThreadInterruptedException)
				{
					Stop = true;
				}
			}
		}

	}
}
