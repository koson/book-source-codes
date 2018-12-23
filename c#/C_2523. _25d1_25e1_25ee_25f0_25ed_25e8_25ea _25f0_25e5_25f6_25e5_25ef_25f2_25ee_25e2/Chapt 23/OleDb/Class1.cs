using System;
using System.Data.OleDb;

namespace OleDb
{
 	class OleDbTest
	{
		public static void Main()
		{
			// Создаем соединение
			OleDbConnection aConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\db1.mdb");
			// Создаем объект для выполнения SQL
			OleDbCommand aCommand = new OleDbCommand("select * from emp_test", aConnection);
			try
			{
				aConnection.Open();
				// Создаем DataReader
				OleDbDataReader aReader = aCommand.ExecuteReader();
				Console.WriteLine("This is the returned data from emp_test table");

				// Читаем данные
				while(aReader.Read())
				{
					Console.WriteLine(aReader.GetInt32(0).ToString());
				}
				// закрываем DataReader
				aReader.Close();
				// закрываем соединение
				aConnection.Close();
			}
				// обработка ошибок
			catch(OleDbException e)
			{
				Console.WriteLine("Error: {0}", e.Errors[0].Message);
			}
		}
	}
}