using System;

namespace Struct
{
	struct Struct1 {
		private int IntValue;
		public int x {
			get 
			{
				return IntValue;
			}
			set 
			{
				if (value < 100)
					IntValue = value;
			}
		}
		public void Display()
		{
			Console.WriteLine("The integer is {0}.", IntValue);
		}
	}

	class Class1
	{
		public static void Main()
		{
			Struct1 ss = new Struct1();
			ss.x = 20;
			ss.Display();
		}
	}
}
