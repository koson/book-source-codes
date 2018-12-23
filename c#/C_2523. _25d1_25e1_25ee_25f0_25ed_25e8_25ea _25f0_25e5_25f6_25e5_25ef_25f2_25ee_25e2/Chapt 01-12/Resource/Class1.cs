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
			// ��� ���������� ����� (������)
			string [] resNames = Assembly.GetCallingAssembly().GetManifestResourceNames();
			resource = resNames[0];

			// ���������
			string baseName = resource.Substring(0, resource.LastIndexOf('.'));
			ResourceManager resourceManager = new ResourceManager(baseName, Assembly.GetExecutingAssembly());

			// Hashtable ��������
			ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
			
			// ������ � ��������
			//   ������
			Console.WriteLine(resourceSet.GetString("Str1"));
			//   ��� System.Int32
			Console.WriteLine(resourceSet.GetObject("Int1").GetType());
			Console.WriteLine((Int32)resourceSet.GetObject("Int1"));
				
			Console.ReadLine();
		}
	}
}
