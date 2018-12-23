using System;

namespace ArrayListAdapter
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			int [] array = {1,4,2,3};

			System.Collections.ArrayList al = System.Collections.ArrayList.Adapter(array);
			al.Sort();
			foreach (int i in al)
			{
				Console.WriteLine(i);
			}

			Console.ReadLine();
		}
	}
}
