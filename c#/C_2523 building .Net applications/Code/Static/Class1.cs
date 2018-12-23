using System;

namespace Static
{
	class Record
	{
		public string name;
		public Record ()
		{
		}
		public Record (string name)
		{
			this.name = name;
		}
		public static int RecordCounter;
		public static int AddRecord()
		{
			return ++RecordCounter;
		}
	}
	
	class Class1 : Record
	{
		static void Main(string[] args)
		{
			Console.Write("Record name: ");
			string name = Console.ReadLine();
			Record r = new Record (name);
			Console.Write("Number of records: ");
			string n = Console.ReadLine();
			Record.RecordCounter = Int32.Parse(n);
			Record.AddRecord();
			Console.WriteLine("Name: {0}", r.name);
			Console.WriteLine("Record total: {0}", Record.RecordCounter);
		}
	}
}
