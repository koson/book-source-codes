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
			// ������ � �����-�����������
			string input = "start %M1% test %M2% end";

			// ������ ������
			MatchCollection matches = Regex.Matches(input, paramPattern);
			foreach(Match match in matches)
			{   
				string paramName = match.Value.Trim('%');
				Console.WriteLine("��� ���������: {0}", paramName);

				// ����� ���-�� ��������� �������� ���������
				string paramValue = string.Format("!{0}!", paramName);

				// ��������
				input = Regex.Replace(input, match.Value, paramValue);
			}    

			Console.WriteLine(input);
			Console.ReadLine();
		}
	}
}
