using System;
using System.Web;

namespace HTMLTextConverter
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			string source = "test&test;";
			string html = HttpUtility.HtmlEncode(source);
			Console.WriteLine(html);
			string text = HttpUtility.HtmlDecode(html);
			Console.WriteLine(text);

			Console.ReadLine();
		}
	}
}
