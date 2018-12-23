using System;

namespace TestApplication
{
	public class BaseClass
	{
		public BaseClass(int x)
		{
			Console.WriteLine("BaseClass constructor: x={0}", x);
		}
	}

	public class DeriveClass : BaseClass
	{
		public DeriveClass(int x) : base(x+10)
		{
			Console.WriteLine("DeriveClass constructor: x={0}", x);
		}
	}



	class MainClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Выведет "DeriveClass constructor: x=5"
			DeriveClass d = new DeriveClass(5);

			Console.ReadLine();
		}
	}
}
