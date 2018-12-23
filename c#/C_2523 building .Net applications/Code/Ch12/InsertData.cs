using System;
using System.Data.SqlClient;

namespace DataAccess
{
	class InsertData
	{
		static void Main()
		{
			SqlConnection cnPubs = 
				new SqlConnection("server=(local);uid=sa;" +
				"pwd=;database=pubs");
			
			String sInsertCmd = "INSERT INTO titles(title_id, " +
				"title, type, pub_id, price, advance, royalty, " +
				"ytd_sales, notes, pubdate) VALUES('BU2222', " +
				"'Visual UML', 'popular_comp', '1389', 45.00, " +
				"1000.00, 10, 1000, 'Your visual blueprint for designing " +
				"software applications.', '2001-06-12 00:00:00.000')";

			SqlCommand cmdTitles = 
				new SqlCommand(sInsertCmd, cnPubs);
			cmdTitles.Connection.Open();
			cmdTitles.ExecuteNonQuery();	
			cmdTitles.Connection.Close();

			Console.WriteLine("Your SQL was executed");
			
		}
	}
}
