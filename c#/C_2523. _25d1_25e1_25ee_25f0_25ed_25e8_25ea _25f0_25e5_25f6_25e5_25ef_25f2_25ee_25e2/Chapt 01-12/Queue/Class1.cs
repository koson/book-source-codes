using System;

namespace Queue_example
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Collections.Queue queue = new System.Collections.Queue();
			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Enqueue(3);

			while (queue.Count > 0)
			{
				Console.WriteLine(queue.Dequeue());
			}

			Console.ReadLine();
		}
	}
}
