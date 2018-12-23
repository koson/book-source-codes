using System;

namespace SingleDim
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static void Main(string[] args)
		{
			Console.WriteLine("The first seven primes are:");
			int[] primes = {1, 2, 3, 5, 7, 9, 11};
			foreach (int n in primes)
			{
				Console.WriteLine(n);
			}
		}
	}
}
