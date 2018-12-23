using System;
using System.Data;
using System.Data.OleDb;

namespace ExcelDataTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ��� �����
			string filename = @"book1.xls";

			// ������ �����������
			string ConnectionString= String.Format(
				"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=No\";Data Source={0}", filename);

			// ��������� ����������
			DataSet          ds=new DataSet("EXCEL");
			OleDbConnection  cn=new OleDbConnection(ConnectionString);
			cn.Open();

			// �������� ������ ������ � �����
			DataTable schemaTable =
				cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, 
						new object[] {null, null, null, "TABLE"});

			// �������� ������ ������ � �����
			for (int i=0; i< schemaTable.Rows.Count; i++)
			{
				// ����� ������
				Console.WriteLine(schemaTable.Rows[i].ItemArray[2]);
				
				// ���� �����������
				Console.WriteLine(schemaTable.Rows[i].ItemArray[7]);
			}

			// ������ �������� ������� �����
			string sheet1 = (string) schemaTable.Rows[0].ItemArray[2];
			// �������� ��� ������ � �����
			string select = String.Format("SELECT * FROM [{0}]", sheet1);
			OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
			ad.Fill(ds);
			DataTable tb=ds.Tables[0];

			// �������� ������ � �����
			foreach (DataRow row in tb.Rows)
			{
				foreach(object col in row.ItemArray)
				{
					Console.Write(col+"\t");
				}
				Console.WriteLine();
			}

			Console.ReadLine();
		}
	}
}
