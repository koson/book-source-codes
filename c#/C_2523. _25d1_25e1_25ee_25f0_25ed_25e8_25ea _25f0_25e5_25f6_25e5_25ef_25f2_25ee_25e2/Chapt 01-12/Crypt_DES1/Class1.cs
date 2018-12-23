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

			// ������
			password = "secretpassword1!";
   
			// �������� ������
			original = "test string";

			//��������� MD5-���� ������
			provider = new MD5CryptoServiceProvider();
			pwdhash = provider.ComputeHash(
				ASCIIEncoding.ASCII.GetBytes(password));
			provider = null;

			//----------------------------------------------
			// ����������
			//----------------------------------------------
			// DES3-�����������
			des = new TripleDESCryptoServiceProvider();
			// ���� ��� DES - ��� ��� ������
			des.Key = pwdhash;
			// ����� �����������
			des.Mode = CipherMode.ECB; //CBC, CFB

			// ����������� ������ � ������ ����
			buff = ASCIIEncoding.ASCII.GetBytes(original);

			// ��������
			encrypted = Convert.ToBase64String(
				des.CreateEncryptor().TransformFinalBlock(buff, 0,
				buff.Length)
				);
			//----------------------------------------------
			// �����������
			//----------------------------------------------
			buff = Convert.FromBase64String(encrypted);
			decrypted = ASCIIEncoding.ASCII.GetString(
				des.CreateDecryptor().TransformFinalBlock(buff, 0,
				buff.Length)
				);

			// ����������� ������
			des = null;

			// �������� ���������
			Console.WriteLine("�������� ������  : {0}", original);
			Console.WriteLine("������������� ������ : {0}", encrypted);
			Console.WriteLine("�������������� ������: {0}", decrypted);

		}
	}
}
