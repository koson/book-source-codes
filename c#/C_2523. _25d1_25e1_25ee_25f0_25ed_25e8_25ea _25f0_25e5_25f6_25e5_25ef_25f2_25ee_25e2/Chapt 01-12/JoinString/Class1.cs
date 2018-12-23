using System;

namespace JoinString
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Массив строк
			string[] val = {"apple", "orange", "grape", "pear"};
			// Объединяем строки через запятую
			string result= string.Join(",", val, 2, 2);
			// Результат: <apple,orange,grape,pear>
			Console.WriteLine(result);

			result= string.Concat(val);
			// Результат: <apple,orange,grape,pear>
			Console.WriteLine(result);

			Console.ReadLine();
		}
	}
}
