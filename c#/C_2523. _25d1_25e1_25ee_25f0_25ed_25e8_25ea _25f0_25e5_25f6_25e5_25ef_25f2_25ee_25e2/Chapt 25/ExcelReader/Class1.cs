using Excel;
using System;
using System.IO;
using System.Reflection;

namespace ExcelReader
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			ApplicationClass application = null;
			string           filename;
			Workbook         workbook;
			Worksheet        worksheet;
			object           objsheet, objrange;
			Range            range;
    
			// ƒолжно быть полное им€
			filename = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory , @"test.xls");

			try
			{
				application = new ApplicationClass();
				application.Visible=false;
				application.DisplayAlerts=false;

				workbook = application.Workbooks.Open(
					filename, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value, 
					Missing.Value
					);

				objsheet = workbook.ActiveSheet;
				if (objsheet != null)
				{
					worksheet = (Worksheet) objsheet;
					objrange = worksheet.Cells[1, 1];
					if (objrange != null)
					{
						range = (Range) objrange;
						range.Font.Name="Tahoma";
						range.Font.Size=8;
						range.Font.Bold=false;
						range.Value = "новое значение";
						range = null;
					}
					objrange = null;
					worksheet = null;
				}
				objsheet = null;

				workbook.Save();
				workbook = null;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				if (application != null)
				{
					application.Quit();
				}
				application = null;
			}
		}
	}
}
