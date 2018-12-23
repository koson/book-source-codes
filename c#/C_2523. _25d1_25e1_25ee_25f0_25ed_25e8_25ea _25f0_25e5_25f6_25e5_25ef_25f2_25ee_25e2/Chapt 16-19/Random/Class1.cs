using System;

namespace Random_test
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Random r = new Random( DateTime.Now.Millisecond );
			int value1 = r.Next();
  			int value2 = r.Next(1, 10);

			Console.ReadLine();
		}

	}
}
