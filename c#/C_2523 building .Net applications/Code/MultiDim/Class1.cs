using System;

namespace MultiDim
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static void Output(int[,] x) 
		{
			for (int a=0; a < 5; a++) 
				for (int b=0; b < 2; b++)
					Console.WriteLine("Element({0},{1})={2}", a, b, x[a,b]);
		}

		public static void Main(string[] args) 
		{
			Output(new int[,] {{1,2}, {3,4}, {5,6}, {7,8}, {9,10}});
		}
	}
}
