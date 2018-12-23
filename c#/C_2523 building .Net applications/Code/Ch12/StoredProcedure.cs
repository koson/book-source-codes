using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace DataAccess
{
	class StoredProcedure
	{
		static void Main()
		{
			SqlConnection cnPubs = new SqlConnection("server=(local);uid=sa;pwd=;database=pubs");		

			SqlCommand cmdByRoyalty = new SqlCommand("ByRoyalty", cnPubs);
			cmdByRoyalty.Connection.Open();

			cmdByRoyalty.CommandType = CommandType.StoredProcedure;

			SqlParameter prmPercentage = new SqlParameter();

			prmPercentage.ParameterName = "@percentage";
			prmPercentage.SqlDbType= SqlDbType.Int;
			prmPercentage.Direction= ParameterDirection.Input;
			prmPercentage.Value=50;

			cmdByRoyalty.Parameters.Add(prmPercentage);

			SqlDataReader drRoyaltyList;
			drRoyaltyList =	cmdByRoyalty.ExecuteReader();
			
			Console.WriteLine("The returned data for the query is:");

			while(drRoyaltyList.Read()) 
			{
				Console.WriteLine(drRoyaltyList.GetString(0));
			}

			cmdByRoyalty.Connection.Close();

		}
	}
}
