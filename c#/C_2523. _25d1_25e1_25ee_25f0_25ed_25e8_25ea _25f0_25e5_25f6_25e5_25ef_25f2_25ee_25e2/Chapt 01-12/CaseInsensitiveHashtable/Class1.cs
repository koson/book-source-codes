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
			// ������� Hashtable ��������� �������
			Hashtable hash1  = new Hashtable();
			hash1["Test"] = 1;
			hash1["test"] = 2;
			Console.WriteLine(hash1["Test"]);
			Console.WriteLine(hash1["test"]);

			// ����������� Hashtable �� ��������� �������
			Hashtable hash2 =  CollectionsUtil.CreateCaseInsensitiveHashtable();
			hash2["Test"] = 1;
			hash2["test"] = 2;
			Console.WriteLine(hash2["Test"]);
			Console.WriteLine(hash2["test"]);

			Console.ReadLine();
		}
	}
}
