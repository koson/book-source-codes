using System;

namespace Construct
{
	class Amount
	{
		public int a;
		public Amount()
		{
			a = 5; // sets initial amount to 5
		}
		public Amount (int total)
		{
			a = total; // sets initial total value
		}
		public static void Main()
		{
			Amount t1 = new Amount(); // output is 5
			Amount t2 = new Amount(20); // output is 20
			Amount t3 = new Amount(40); // output is 40
			Console.WriteLine(t1.a);
			Console.WriteLine(t2.a);
			Console.WriteLine(t3.a);
		}
	}
}
