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
			// Создание бинарного потока
			BinaryFormatter formatter = new BinaryFormatter(); 
			MemoryStream	stream	  = new MemoryStream(); 
 			
			// Сериализация в бинарный поток
			formatter.Serialize(stream, obj); 
  
			// Описание ключа реестра
			RegistryKey regKey; 
			// Открытие ключа реестра
			regKey = Registry.CurrentUser.CreateSubKey(akey);
			// Запись  в реестр
			regKey.SetValue(avalue, stream.ToArray()); 
		}

		static void Load(ref object obj, string akey, string avalue)
		{
			// Создание бинарного потока
			BinaryFormatter formatter = new BinaryFormatter(); 
			MemoryStream stream = new MemoryStream(); 

			// Описание ключа реестра
			RegistryKey regKey; 
			// Открытие ключа реестра
			regKey = Registry.CurrentUser.OpenSubKey(akey);
 			
			// Чтение из реестра в байтовый массив
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
			// объект, поддерживающий сериализацию, например, ArrayList
			ArrayList names = new ArrayList();
			
			// инициализация объекта
			names.Add("1");
			names.Add("2");
			names.Add("3");

			// Сохранение
			Save(names, "test", "ValueName");

			// Загружаем сохраненный объект
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
