using System;

namespace RegexReplace
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
			string text = @"Итак, <b>поиск с заменой</b> осуществляется с помощью метода <b>Replace</b>";
			text = System.Text.RegularExpressions.Regex.Replace(text, @"<b>(.*?)</b>",@"<I>$1</I>", 
				 System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			Console.WriteLine(text);

			Console.ReadLine();
		}
	}
}
