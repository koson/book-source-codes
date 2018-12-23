using System;

namespace DownloadWebPage
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Net.WebRequest req = System.Net.WebRequest.Create(@"http://www.microsoft.com"); 
			System.Net.WebResponse resp = req.GetResponse(); 
			System.IO.Stream stream = resp.GetResponseStream(); 
			System.IO.StreamReader sr = new System.IO.StreamReader(stream); 
			string s = sr.ReadToEnd(); 
			Console.WriteLine(s);
		}
	}
}
