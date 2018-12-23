using System;
using System.Text.RegularExpressions;

namespace MacroNames
{
	class Class1
	{
		static string paramPattern = @"%[^\s%]+%";

		[STAThread]
		static void Main(string[] args)
		{
			// Строка с макро-параметрами
			string input = "start %M1% test %M2% end";

			// Разбор строки
			MatchCollection matches = Regex.Matches(input, paramPattern);
			foreach(Match match in matches)
			{   
				string paramName = match.Value.Trim('%');
				Console.WriteLine("Имя параметра: {0}", paramName);

				// Здесь как-то вычисляем значение параметра
				string paramValue = string.Format("!{0}!", paramName);

				// Заменяем
				input = Regex.Replace(input, match.Value, paramValue);
			}    

			Console.WriteLine(input);
			Console.ReadLine();
		}
	}
}
