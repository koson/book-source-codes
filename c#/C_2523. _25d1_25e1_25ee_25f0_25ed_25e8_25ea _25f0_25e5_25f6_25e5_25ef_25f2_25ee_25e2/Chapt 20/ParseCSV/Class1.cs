using System;
using System.Text.RegularExpressions;

namespace ParseCSV
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string separator = ";";
			string input     = "AAA;BBB;\"CCC;DDD\";EE";
										   
			string pattern   = string.Format("{0}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", separator);

			string [] result = Regex.Split(input, pattern);

			// Выведет 4 строки
			// AAA
			// BBB
			// "CCC;DDD"
			// EE
			foreach (string str in result) 
			{
				Console.WriteLine(str);
			}


			Console.ReadLine();
		}
	}
}
