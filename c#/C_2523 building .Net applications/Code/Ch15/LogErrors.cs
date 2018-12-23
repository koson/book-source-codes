using System;
using System.Diagnostics;

namespace ExceptionHandling
{
	public class LogErrors
	{
		public static void Main() 
		{
			string sLog = "Application";
			string sSource = "MySharedPhotoAlbum";
			string sErrorMsg = "The photo accessed was missing.";

			if ( !EventLog.SourceExists(sSource) ) 
			{
				EventLog.CreateEventSource(sSource,sLog);
			}

			EventLog elMain = new EventLog();
			elMain.Source = sSource;

			if ( elMain.Log.ToUpper() != sLog.ToUpper() ) 
			{
				Console.WriteLine("Some other application is using the source!");
				return;
			}

			elMain.WriteEntry(sErrorMsg,EventLogEntryType.Information);
			Console.WriteLine("Log Entry is complete!");
		}
	}
}
