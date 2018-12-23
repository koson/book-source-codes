using System;

namespace EqualsTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{

			// Строки
			string a = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
			string b = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
        
			Console.WriteLine (a==b); // Вернет true
			Console.WriteLine (a.Equals(b)); // Вернет true
        
			// Объекты
			object c = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
			object d = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
        
			Console.WriteLine (c==d); // Вернет false
			Console.WriteLine (c.Equals(d)); // Вернет true

			Console.ReadLine();
		}
	}
}
