using System;
using System.Data.OleDb;

namespace OleDb
{
 	class OleDbTest
	{
		public static void Main()
		{
			// ������� ����������
			OleDbConnection aConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\db1.mdb");
			// ������� ������ ��� ���������� SQL
			OleDbCommand aCommand = new OleDbCommand("select * from emp_test", aConnection);
			try
			{
				aConnection.Open();
				// ������� DataReader
				OleDbDataReader aReader = aCommand.ExecuteReader();
				Console.WriteLine("This is the returned data from emp_test table");

				// ������ ������
				while(aReader.Read())
				{
					Console.WriteLine(aReader.GetInt32(0).ToString());
				}
				// ��������� DataReader
				aReader.Close();
				// ��������� ����������
				aConnection.Close();
			}
				// ��������� ������
			catch(OleDbException e)
			{
				Console.WriteLine("Error: {0}", e.Errors[0].Message);
			}
		}
	}
}