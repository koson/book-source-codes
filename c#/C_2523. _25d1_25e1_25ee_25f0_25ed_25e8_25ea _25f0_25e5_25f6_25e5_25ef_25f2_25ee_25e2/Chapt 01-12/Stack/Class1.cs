using System;

namespace Stack_example
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Collections.Stack stack = new System.Collections.Stack();

			for (int i=0; i<800000; i++)
			{
				stack.Push(i);
			}

			while (stack.Count > 0)
			{
				Console.WriteLine(stack.Pop());
			}

			Console.ReadLine();
		}
	}
}
