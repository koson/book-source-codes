using System;

namespace DownloadFile
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				System.Net.WebClient client = new System.Net.WebClient();

				string url = @"http://i2.microsoft.com/h/en-us/i/msnlogo.gif";
				string loc = @"E:\test.jpg";

				client.DownloadFile(url, loc);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
			};
		}
	}
}
