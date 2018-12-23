using System;

namespace Iterate
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		static void Main(string[] args)
		{
			int[] primes = new int[] {1,2,3,5};
			foreach (int x in primes)
			{
				int s = (x*x);
				Console.WriteLine("The square of {0} is {1}.",x,s);
			}
		}
	}
}
