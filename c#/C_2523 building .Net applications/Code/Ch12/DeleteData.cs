using System;
using System.Data.SqlClient;

namespace DataAccess
{
	class DeleteData
	{
		static void Main()
		{
			SqlConnection cnPubs = new SqlConnection("server=(local);uid=sa;pwd=;database=pubs");
			
			String sDeleteCmd = "delete from titles where title_id='BU2222'";

			SqlCommand cmdTitles = new SqlCommand(sDeleteCmd, cnPubs);
			cmdTitles.Connection.Open();
			cmdTitles.ExecuteNonQuery();	
			cmdTitles.Connection.Close();
			Console.WriteLine("Your SQL was executed");

		}
	}
}
