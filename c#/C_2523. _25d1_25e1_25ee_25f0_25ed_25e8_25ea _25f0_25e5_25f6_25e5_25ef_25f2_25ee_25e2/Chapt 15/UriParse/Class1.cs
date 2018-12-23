using System;

namespace UriParse
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			UriBuilder parser = new UriBuilder("http://microsoft.com:80/default.aspx?id=55");
			Console.WriteLine(parser.Host);		// microsoft.com
			Console.WriteLine(parser.Scheme);	// http
			Console.WriteLine(parser.Uri);		// http://microsoft.com/default.aspx?id=55
			Console.WriteLine(parser.Path);		// /default.aspx
			Console.WriteLine(parser.Port);		// 80
			Console.WriteLine(parser.Query);	// ?id=55

			UriBuilder builder = new UriBuilder("https", "microsoft.com", 81, "/default.aspx","?id=77");
			// Получим https://microsoft.com:81/default.aspx?id=77
			Console.WriteLine(builder.ToString());
            

			Console.ReadLine();
		}
	}
}
