using System;

namespace DelegateExample
{
	// Тип делегата
	public delegate void PrintDelegate(int x , int y);

	class Calculate
	{
		// Сохраняет ссылку на делегат
		private PrintDelegate print;

		public Calculate(PrintDelegate print)
		{
			this.print = print;
		}

		public void Execute()
		{
			for (int i=0; i<10; i++)
			{
				int result = i * i;
				// Использование делегата
				if (print != null)
					print(i, result);
			}
		}
	}

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Calculate c = new Calculate(new PrintDelegate(Print));
			c.Execute();

			Console.ReadLine();
		}

		// Реальный метод
		static void Print(int x, int y)
		{
			Console.WriteLine("x={0} y={1}", x, y);
		}
	}
}
