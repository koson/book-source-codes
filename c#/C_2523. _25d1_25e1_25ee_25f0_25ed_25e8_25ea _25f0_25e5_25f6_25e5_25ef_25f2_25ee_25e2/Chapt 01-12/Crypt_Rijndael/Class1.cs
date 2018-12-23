using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Crypt_Rijndael
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Исходная строка
			string source_str = "It is string!";
			// Пароль
			string password = "123";

			Console.WriteLine(source_str);

			// Получаем из строки набор байт, которые будем шифровать
			byte[] source_data = Encoding.UTF8.GetBytes(source_str);
			// Алгоритм 
			SymmetricAlgorithm sa_in = Rijndael.Create();
			// Объект для преобразования данных
			ICryptoTransform ct_in = sa_in.CreateEncryptor(
				(new PasswordDeriveBytes(password, null)).GetBytes(16),	new byte[16]);
			// Поток
			MemoryStream ms_in = new MemoryStream();
			// Шифровальщик потока
			CryptoStream cs_in = new CryptoStream(ms_in, ct_in, CryptoStreamMode.Write);
			// Записываем шифрованные данные в поток
			cs_in.Write(source_data, 0, source_data.Length);
			cs_in.FlushFinalBlock();
			// Создаем строку
			string crypt_str = Convert.ToBase64String(ms_in.ToArray());
			// Выводим зашифрованную строку
			Console.WriteLine(crypt_str);


			// Получаем массив байт
			byte[] crypt_data = Convert.FromBase64String(crypt_str);

			// Алгоритм 
			SymmetricAlgorithm sa_out = Rijndael.Create();
			// Объект для преобразования данных
			ICryptoTransform ct_out = sa_out.CreateDecryptor(
				(new PasswordDeriveBytes(password, null)).GetBytes(16),
				new byte[16]);
			// Поток
			MemoryStream ms_out = new MemoryStream(crypt_data);
			// Расшифровываем поток
			CryptoStream cs_out = new CryptoStream(ms_out, ct_out, CryptoStreamMode.Read);
			// Создаем строку
			StreamReader sr_out = new StreamReader(cs_out);
			string source_out = sr_out.ReadToEnd();
			// Выводим расшифрованную строку
			Console.WriteLine(source_out);


			Console.ReadLine();
		}
	}
}
