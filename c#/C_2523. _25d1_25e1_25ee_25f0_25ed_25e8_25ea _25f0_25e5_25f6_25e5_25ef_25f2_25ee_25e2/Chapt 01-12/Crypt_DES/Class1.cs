using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Crypt_DES
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Исходная строка
			string source_str = "It is string!";

			Console.WriteLine(source_str);


			// Получаем из строки набор байт, которые будем шифровать
			byte[] source_data = Encoding.UTF8.GetBytes(source_str);
 
			DES des = new DESCryptoServiceProvider();

			// Генерируем IV и Key
			des.GenerateIV();
			des.GenerateKey();
 
			// Сохраняем ключи
			byte[] IVByteArray  = des.IV;
			byte[] KeyByteArray = des.Key;


			// Поток выходных данных
			MemoryStream ms_in = new MemoryStream();
			// Шифрованный поток
			CryptoStream cs_in = 
				new CryptoStream(ms_in,
				des.CreateEncryptor(KeyByteArray,
				IVByteArray),
				CryptoStreamMode.Write);
			cs_in.Write(source_data, 0, source_data.Length);
			cs_in.Close();
			// Получаем зашифрованную строку
			string crypt_str = Convert.ToBase64String(ms_in.ToArray());
			// Выводим зашифрованную строку
			Console.WriteLine(crypt_str);


			// Получаем массив байт
			byte[] crypt_data = Convert.FromBase64String(crypt_str);
			// Поток выходных данных
			MemoryStream ms_out = new MemoryStream(crypt_data);
			// Поток для расшифровки
			CryptoStream cs_out = 
				new CryptoStream(ms_out,
				des.CreateDecryptor(KeyByteArray,
				IVByteArray),
				CryptoStreamMode.Read);
			// Читаем выходную строку
			StreamReader sr_out = new StreamReader(cs_out);
			string source_out = sr_out.ReadToEnd();
			// Выводим расшифрованную строку
			Console.WriteLine(source_out);
        
			Console.ReadLine();
		}
	}
}
