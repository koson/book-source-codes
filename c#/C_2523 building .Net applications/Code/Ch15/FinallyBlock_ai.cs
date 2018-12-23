using System;
using System.IO;
using System.Data.SqlClient;

namespace ExceptionHandling
{
   /// <summary>
   /// Apply the Try/Catch/Finally to use of Data Access with ADO.NET
   /// This is a must see... 
   /// You should always wrap this kind of coding with exception handling.
   /// </summary>
   class FinallyBlock
   {
      static void Main()
      {

         SqlConnection cnPubs = new SqlConnection();
         SqlCommand cmdTitles = new SqlCommand();
         
         try
         {

            cnPubs.ConnectionString =   "server=(local);uid=sa;pwd=;database=pubs";
            cnPubs.Open();

               // Change the insert contents to a book on UML.
               // Note that you will have to delete this entry in the database if you run a second time.
               String sInsertCmd = "INSERT INTO titles(title_id, title, type, pub_id, price, advance, royalty, ytd_sales, notes, pubdate) " +
                  "VALUES('BU2222', 'Visual UML', 'popular_comp', '1389', 45.00, 1000.00, 10, 1000, 'Your visual blueprint for designing software applications.', '2001-06-12 00:00:00.000')";
         
            Console.WriteLine("You are about to run the following SQL Statement:");
            Console.WriteLine(sInsertCmd);
            Console.Write("Would you like to continue? [Y/N] ");

            char cResponse = (char) Console.Read();

            if (cResponse == char.Parse("Y"))
            {
               cmdTitles.Connection = cnPubs;
               cmdTitles.CommandText = sInsertCmd;
               cmdTitles.ExecuteNonQuery();  
               Console.WriteLine("Your SQL was executed");
            }
            else
            {
               Console.WriteLine("You choose not to execute your SQL");
            }
         }
         catch (Exception e)
         {
            Console.WriteLine ("The following exception occurred:  \r\n {0}", e);
         }  
         finally
         {
            Console.WriteLine("Starting Cleanup Code");        
            cmdTitles.Connection.Close();
            Console.WriteLine("Ending Cleanup Code");
         }
      }
   }
}




