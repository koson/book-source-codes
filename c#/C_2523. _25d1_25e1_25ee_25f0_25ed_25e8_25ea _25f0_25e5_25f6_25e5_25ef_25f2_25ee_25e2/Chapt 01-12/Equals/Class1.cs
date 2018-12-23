using System;

namespace EqualsTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{

			// ������
			string a = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
			string b = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
        
			Console.WriteLine (a==b); // ������ true
			Console.WriteLine (a.Equals(b)); // ������ true
        
			// �������
			object c = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
			object d = new string(new char[] {'h', 'e', 'l', 'l', 'o'});
        
			Console.WriteLine (c==d); // ������ false
			Console.WriteLine (c.Equals(d)); // ������ true

			Console.ReadLine();
		}
	}
}
