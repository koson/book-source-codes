using System;
using System.IO;

namespace ExceptionHandling
{
	class TryCatch
	{
		static void Main(string[] args)
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
			catch (FileNotFoundException)
			{
				Console.WriteLine ("The photo's filename provided " + sTextFile + " was not found." +
					"  Please check to see if the file exists and then try again.");
			}				
			catch (Exception e)
			{
				Console.WriteLine ("The following exception occurred:  \r\n {0}", e);
			}				
		}
	}
}
