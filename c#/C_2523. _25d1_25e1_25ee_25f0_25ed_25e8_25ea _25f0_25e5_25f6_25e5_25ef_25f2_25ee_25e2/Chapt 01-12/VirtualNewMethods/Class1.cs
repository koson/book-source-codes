using System;

namespace VirtualNewMethods
{
	class Base
	{
		public virtual void Test()
		{
			Console.WriteLine("Base::Test");
		}
	}

	class Derived1 : Base
	{
		public override void Test()
		{
			Console.WriteLine("Derived1::Test");
		}
	}

	class Derived2 : Base
	{
		public new void Test()
		{
			Console.WriteLine("Derived2::Test");
		}
	}

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Derived1 d1 = new Derived1();
			d1.Test(); // Напечатает "Derived1::Test"

			Derived2 d2 = new Derived2();
			d2.Test(); // Напечатает "Derived2::Test"

			Base b1 = new Derived1();
			b1.Test(); // Напечатает "Derived1::Test"

			Base b2 = new Derived2();
			b2.Test(); // Напечатает "Base::Test" (!)

			Console.ReadLine();
		}

	}
}
