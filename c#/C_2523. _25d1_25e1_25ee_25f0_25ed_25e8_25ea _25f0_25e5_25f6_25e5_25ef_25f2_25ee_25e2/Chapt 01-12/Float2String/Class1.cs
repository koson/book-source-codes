using System;

namespace Float2String
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			double d = 3.1415926;

            Console.WriteLine("{0}", d); // 3,1415926
			Console.WriteLine("{0:F2}", d); // 3,14
			Console.WriteLine(string.Format("{0}", d)); // 3,1415926
			Console.WriteLine(string.Format("{0:F3}", d)); // 3,142
			Console.WriteLine(d.ToString("F2")); // 3,14
			Console.WriteLine(d.ToString("E"));  // 3,141593E+000
			Console.WriteLine(d.ToString("C"));	 // 3,14ð.
			Console.WriteLine(d.ToString("G"));	 // 3,1415926
			Console.WriteLine(d.ToString("R"));	 // 3,1415926

			Console.ReadLine();
		}
	}
}
