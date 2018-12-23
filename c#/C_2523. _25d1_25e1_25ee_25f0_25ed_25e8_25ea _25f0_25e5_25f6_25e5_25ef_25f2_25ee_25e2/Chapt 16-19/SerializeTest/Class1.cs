using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Win32;
using System.Security.Permissions;

namespace SerializeTest
{
	class TestClass
	{

		static void Save(object obj, string akey, string avalue)
		{
			// �������� ��������� ������
			BinaryFormatter formatter = new BinaryFormatter(); 
			MemoryStream	stream	  = new MemoryStream(); 
 			
			// ������������ � �������� �����
			formatter.Serialize(stream, obj); 
  
			// �������� ����� �������
			RegistryKey regKey; 
			// �������� ����� �������
			regKey = Registry.CurrentUser.CreateSubKey(akey);
			// ������  � ������
			regKey.SetValue(avalue, stream.ToArray()); 
		}

		static void Load(ref object obj, string akey, string avalue)
		{
			// �������� ��������� ������
			BinaryFormatter formatter = new BinaryFormatter(); 
			MemoryStream stream = new MemoryStream(); 

			// �������� ����� �������
			RegistryKey regKey; 
			// �������� ����� �������
			regKey = Registry.CurrentUser.OpenSubKey(akey);
 			
			// ������ �� ������� � �������� ������
			byte[] barray = null; 
 			barray = (byte[])regKey.GetValue(avalue); 
 
			if(barray != null) 
			{ 
				stream.Write(barray, 0, barray.Length); 
				stream.Position = 0; 
				obj = formatter.Deserialize(stream) as ArrayList; 
			}

		}

		[STAThread]
		static void Main(string[] args)
		{
			// ������, �������������� ������������, ��������, ArrayList
			ArrayList names = new ArrayList();
			
			// ������������� �������
			names.Add("1");
			names.Add("2");
			names.Add("3");

			// ����������
			Save(names, "test", "ValueName");

			// ��������� ����������� ������
			object obj = null;
			Load(ref obj,"test", "ValueName");
			foreach (string str in (obj as ArrayList))
			{
				Console.WriteLine(str);
			}
			Console.ReadLine();
		}
	}
}
