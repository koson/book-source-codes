using System;
using Microsoft.Win32;

namespace FileTypeByExt
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ��� (content) ��-���������
			const string DEFAULT_CONTENT_TYPE = "application/unknown";

			string  fileContentType;

			// ���������� �����
			string fileExtension = ".jpeg";

			try 
			{
				// ���� � ������� ������, ��������������� ����������
				RegistryKey fileextkey = Registry.ClassesRoot.OpenSubKey(fileExtension);
				// �������� ���
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
