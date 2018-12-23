using System;

namespace Convert
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		public static void Main(string[] args)
		{
			int a = 25;
			object b = a;
			a = 625;
			Console.WriteLine("Original (boxed) value: {0}",b);
			Console.WriteLine("Square Value: {0}",a);
		}
	}
}
