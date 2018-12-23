using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ColorTranslatorTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{

			string html = ColorTranslator.ToHtml(Color.Red);
			Color cl = ColorTranslator.FromHtml(html);
			Console.WriteLine("{0}={1}", cl.Name, html);

			Console.ReadLine();
		}
	}
}
