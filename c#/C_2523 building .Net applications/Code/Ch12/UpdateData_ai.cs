using System;
using System.Data.SqlClient;

namespace DataAccess
{
   /// <summary>
   /// Do the same thing, but this time show it in a transaction.
   /// </summary>
   class UpdateData
   {
      static void Main()
      {
         SqlConnection cnPubs = new SqlConnection("server=(local);uid=sa;pwd=;database=pubs");
         // Change the insert contents to a book on UML.
         // Note that you will have to delete this entry in the database if you run a second time.
         String sUpdateCmd = "UPDATE authors SET zip = 30004 WHERE au_id = '172-32-1176'";
         SqlCommand cmdTitles = new SqlCommand(sUpdateCmd, cnPubs);
         cmdTitles.Connection.Open();
         // Start a local transaction
         SqlTransaction txPubs = cnPubs.BeginTransaction();
         // Assign transaction object
         cmdTitles.Transaction = txPubs;
         cmdTitles.ExecuteNonQuery();  
         Console.WriteLine("You ran the following SQL Statement:");
         Console.WriteLine(sUpdateCmd);
         Console.Write("Do you want to commit the change? [Y/N] ");
         char cCommitResponse = (char) Console.Read();
         if (cCommitResponse == char.Parse("Y"))
         {
            txPubs.Commit();
         }
         else
         {
            txPubs.Rollback();
         }
         cmdTitles.Connection.Close();
      }
   }
}
