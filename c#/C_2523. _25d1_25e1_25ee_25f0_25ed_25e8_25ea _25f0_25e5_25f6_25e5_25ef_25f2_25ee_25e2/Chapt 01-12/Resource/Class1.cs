using System;
using System.Reflection;
using System.Resources;
using System.IO;
using System.Globalization;

namespace Resource
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Имя ресурсного файла (файлов)
			string [] resNames = Assembly.GetCallingAssembly().GetManifestResourceNames();
			resource = resNames[0];

			// Загружаем
			string baseName = resource.Substring(0, resource.LastIndexOf('.'));
			ResourceManager resourceManager = new ResourceManager(baseName, Assembly.GetExecutingAssembly());

			// Hashtable ресурсов
			ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
			
			// Доступ к ресурсам
			//   Строка
			Console.WriteLine(resourceSet.GetString("Str1"));
			//   Тип System.Int32
			Console.WriteLine(resourceSet.GetObject("Int1").GetType());
			Console.WriteLine((Int32)resourceSet.GetObject("Int1"));
				
			Console.ReadLine();
		}
	}
}
