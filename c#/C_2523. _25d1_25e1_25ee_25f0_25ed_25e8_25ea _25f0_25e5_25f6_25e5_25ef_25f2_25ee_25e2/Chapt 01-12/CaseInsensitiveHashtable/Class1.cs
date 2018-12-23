using System;
using System.Collections;
using System.Collections.Specialized;

namespace CaseInsensitiveHashtable
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Обычный Hashtable различает регистр
			Hashtable hash1  = new Hashtable();
			hash1["Test"] = 1;
			hash1["test"] = 2;
			Console.WriteLine(hash1["Test"]);
			Console.WriteLine(hash1["test"]);

			// Специальный Hashtable не различает регистр
			Hashtable hash2 =  CollectionsUtil.CreateCaseInsensitiveHashtable();
			hash2["Test"] = 1;
			hash2["test"] = 2;
			Console.WriteLine(hash2["Test"]);
			Console.WriteLine(hash2["test"]);

			Console.ReadLine();
		}
	}
}
