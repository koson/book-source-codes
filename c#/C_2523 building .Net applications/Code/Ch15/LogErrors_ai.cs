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
         string sErrorMsg1 = “This is the message that is for Information.";
         string sErrorMsg2 = "This is the message that is for Error.";
         string sErrorMsg3 = "This is the message that is for Warning.";

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

         // Write logs for each type.
elMain.WriteEntry(sErrorMsg1,EventLogEntryType.Information);
elMain.WriteEntry(sErrorMsg2,EventLogEntryType. Error);
elMain.WriteEntry(sErrorMsg3,EventLogEntryType. Warning);

         Console.WriteLine("Log Entry is complete!");
      }
   }
}




