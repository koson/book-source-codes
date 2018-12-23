using System;
using Microsoft.Win32;

namespace FileTypeByExt
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Тип (content) по-умолчанию
			const string DEFAULT_CONTENT_TYPE = "application/unknown";

			string  fileContentType;

			// Расширение файла
			string fileExtension = ".jpeg";

			try 
			{
				// Ищем в реестре вертку, соответствующую расширению
				RegistryKey fileextkey = Registry.ClassesRoot.OpenSubKey(fileExtension);
				// Получаем тип
				fileContentType = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();
			}
			catch (Exception e)
			{
				fileContentType = DEFAULT_CONTENT_TYPE;
				Console.WriteLine(e.Message);
			}
            
			Console.WriteLine(fileContentType);
			Console.ReadLine();
		}
	}
}
