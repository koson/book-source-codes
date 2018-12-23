using System;
using System.IO;

namespace ExceptionHandling
{
	class TryCatch
	{
		static void Main()
		{
			string sTextFile = "somenonexistingphotofile.txt";
			string sLine;
			
			try
			{
				StreamReader srTest = File.OpenText(sTextFile);
				Console.WriteLine("Preparing to write file contents....");
				while ((sLine=srTest.ReadLine()) != null) 
				{
					Console.WriteLine(sLine);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine ("The following exception occurred:  \r\n {0}", e);
			}	
		}
	}
}
