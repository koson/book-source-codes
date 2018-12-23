using System;

namespace SortedList_example
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Collections.SortedList sl = new System.Collections.SortedList();
			sl.Add(1, "Один");
			sl.Add(5, "Пять");
			sl.Add(3, "Три");
			sl.Add(2, "Два");
			sl.Add(4, "Четыре");

			foreach (System.Collections.DictionaryEntry item in sl)
			{
				Console.WriteLine(item.Value);
			}

			Console.ReadLine();
		}
	}
}
