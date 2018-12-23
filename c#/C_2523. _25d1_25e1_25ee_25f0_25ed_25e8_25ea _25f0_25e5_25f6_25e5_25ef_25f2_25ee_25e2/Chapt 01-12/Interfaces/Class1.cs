using System;

namespace Interfaces
{
	interface I1
	{
		void PrintName();
	}

	interface I2
	{
		void PrintName();
	}

	class BaseTest
	{
		public void PrintName()
		{
			Console.WriteLine("BaseTest.GetName");
		}
	}

	class Test : BaseTest, I1, I2 
	{
		void I1.PrintName()
		{
			Console.WriteLine("I1.PrintName");
		}

		void I2.PrintName()
		{
			Console.WriteLine("I2.PrintName");
		}
	}


	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Test t = new Test();
			t.PrintName();         // גûחמגוע base-לועמה
			(t as I1).PrintName(); // גûחמגוע I1-לועמה
			(t as I2).PrintName(); // גûחמגוע I2-לועמה

			Console.ReadLine();
		}
	}
}
