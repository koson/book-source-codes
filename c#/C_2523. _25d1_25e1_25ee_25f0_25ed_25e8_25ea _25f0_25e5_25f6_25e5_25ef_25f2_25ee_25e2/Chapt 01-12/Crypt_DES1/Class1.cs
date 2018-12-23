using System;
using System.Security.Cryptography;
using System.Text;

namespace Crypt_DES1
{
	class DES_Test
	{
		static void Main()
		{
			string       original, encrypted, decrypted, password;
			TripleDESCryptoServiceProvider des;
			MD5CryptoServiceProvider  provider;
			byte[]       pwdhash, buff;

			// Пароль
			password = "secretpassword1!";
   
			// Исходная строка
			original = "test string";

			//Генерация MD5-хеша пароля
			provider = new MD5CryptoServiceProvider();
			pwdhash = provider.ComputeHash(
				ASCIIEncoding.ASCII.GetBytes(password));
			provider = null;

			//----------------------------------------------
			// Шифрование
			//----------------------------------------------
			// DES3-криптование
			des = new TripleDESCryptoServiceProvider();
			// Ключ для DES - это хеш пароля
			des.Key = pwdhash;
			// Режим криптования
			des.Mode = CipherMode.ECB; //CBC, CFB

			// Преобразуем строку в массив байт
			buff = ASCIIEncoding.ASCII.GetBytes(original);

			// Криптуем
			encrypted = Convert.ToBase64String(
				des.CreateEncryptor().TransformFinalBlock(buff, 0,
				buff.Length)
				);
			//----------------------------------------------
			// Расшифровка
			//----------------------------------------------
			buff = Convert.FromBase64String(encrypted);
			decrypted = ASCIIEncoding.ASCII.GetString(
				des.CreateDecryptor().TransformFinalBlock(buff, 0,
				buff.Length)
				);

			// Освобождаем ресурс
			des = null;

			// Печатаем результат
			Console.WriteLine("Исходная строка  : {0}", original);
			Console.WriteLine("Зашифрованная строка : {0}", encrypted);
			Console.WriteLine("Расшифрованная строка: {0}", decrypted);

		}
	}
}
