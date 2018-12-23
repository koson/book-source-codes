using System;

namespace UnsafeString
{
	class Class1
	{
		public static unsafe void ToUpper(string str)
		{
			fixed (char *pfixed = str)
				for (char *p=pfixed; *p != 0; p++)
					*p = char.ToUpper(*p);
		}


		[STAThread]
		static void Main(string[] args)
		{
			string str = "test строка";
			ToUpper(str);
			Console.WriteLine(str);
			Console.ReadLine();

		}
	}
}
