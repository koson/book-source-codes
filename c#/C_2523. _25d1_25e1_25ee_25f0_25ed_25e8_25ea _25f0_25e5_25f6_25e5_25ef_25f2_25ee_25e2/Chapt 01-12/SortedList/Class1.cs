using System;

namespace SortedList_example
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Collections.SortedList sl = new System.Collections.SortedList();
			sl.Add(1, "����");
			sl.Add(5, "����");
			sl.Add(3, "���");
			sl.Add(2, "���");
			sl.Add(4, "������");

			foreach (System.Collections.DictionaryEntry item in sl)
			{
				Console.WriteLine(item.Value);
			}

			Console.ReadLine();
		}
	}
}
