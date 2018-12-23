using System;

namespace IndexProperty
{
	class TestClass
	{
		public string this[int index]
		{
			get
			{
				return string.Format("Index={0}", index);
			}
		}

		public string this[string name]
		{
			get
			{
				return string.Format("Name={0}", name);
			}
		}

		public string this[string name, int index]
		{
			get
			{
				return string.Format("Name={0} index={1}", name, index);
			}
		}

	}


	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			TestClass tc = new TestClass();
			Console.WriteLine("{0},{1},{2}", tc[1], tc[2], tc[3]);
			Console.WriteLine("{0},{1},{2}", tc["A"], tc["B"], tc["C"]);
			Console.WriteLine("{0},{1},{2}", tc["A", 1], tc["B", 2], tc["C", 3]);
			Console.ReadLine();
		}
	}
}
