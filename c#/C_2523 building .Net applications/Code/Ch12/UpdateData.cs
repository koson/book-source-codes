using System;
using System.Data.SqlClient;

namespace DataAccess
{
	class UpdateData
	{
		static void Main()
		{
			SqlConnection cnPubs = new SqlConnection("server=(local);uid=sa;pwd=;database=pubs");
			
			String sUpdateCmd = "UPDATE authors SET zip = 30004 WHERE au_id = '172-32-1176'";

			SqlCommand cmdTitles = new SqlCommand(sUpdateCmd, cnPubs);
			cmdTitles.Connection.Open();
			cmdTitles.ExecuteNonQuery();	
			cmdTitles.Connection.Close();
			Console.WriteLine("Your SQL was executed");	
			
		}
	}
}