using System;

namespace Params
{
	class Class1
	{
		static int Sum(params int[] numbers)
		{
			int total = 0;
			foreach(int i in numbers)
			{
				total += i;
			}
			return total;
		}

		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine(Sum(1,2,3));
			Console.WriteLine(Sum(10,20,30,40,50,60));
			Console.ReadLine();
		}
	}
}
