using System;
using System.IO;

namespace ExceptionHandling
{
	class FinallyBlock
	{
		static void Main()
		{
			string sTextFile = "somenonexistingphotofile.txt";
			string sLine;
			
			try
			{
				Console.WriteLine("Starting Try Block");
				StreamReader srTest = File.OpenText(sTextFile);

				Console.WriteLine("Preparing to write file contents....");

				while ((sLine=srTest.ReadLine()) != null) 
				{
					Console.WriteLine(sLine);
				}
				Console.WriteLine("Ending Try Block");
			}
			catch (Exception e)
			{
				Console.WriteLine("Starting Catch Block");
				Console.WriteLine ("The following exception occurred:  \r\n {0}", e);
				Console.WriteLine("Ending Catch Block");
			}	
			finally
			{
				Console.WriteLine("Starting Finally Block");			
				Console.WriteLine("Ending Finally Block");
			}
		}
	}
}
