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
			// �������� ������
			string source_str = "It is string!";
			// ������
			string password = "123";

			Console.WriteLine(source_str);

			// �������� �� ������ ����� ����, ������� ����� ���������
			byte[] source_data = Encoding.UTF8.GetBytes(source_str);
			// �������� 
			SymmetricAlgorithm sa_in = Rijndael.Create();
			// ������ ��� �������������� ������
			ICryptoTransform ct_in = sa_in.CreateEncryptor(
				(new PasswordDeriveBytes(password, null)).GetBytes(16),	new byte[16]);
			// �����
			MemoryStream ms_in = new MemoryStream();
			// ������������ ������
			CryptoStream cs_in = new CryptoStream(ms_in, ct_in, CryptoStreamMode.Write);
			// ���������� ����������� ������ � �����
			cs_in.Write(source_data, 0, source_data.Length);
			cs_in.FlushFinalBlock();
			// ������� ������
			string crypt_str = Convert.ToBase64String(ms_in.ToArray());
			// ������� ������������� ������
			Console.WriteLine(crypt_str);


			// �������� ������ ����
			byte[] crypt_data = Convert.FromBase64String(crypt_str);

			// �������� 
			SymmetricAlgorithm sa_out = Rijndael.Create();
			// ������ ��� �������������� ������
			ICryptoTransform ct_out = sa_out.CreateDecryptor(
				(new PasswordDeriveBytes(password, null)).GetBytes(16),
				new byte[16]);
			// �����
			MemoryStream ms_out = new MemoryStream(crypt_data);
			// �������������� �����
			CryptoStream cs_out = new CryptoStream(ms_out, ct_out, CryptoStreamMode.Read);
			// ������� ������
			StreamReader sr_out = new StreamReader(cs_out);
			string source_out = sr_out.ReadToEnd();
			// ������� �������������� ������
			Console.WriteLine(source_out);


			Console.ReadLine();
		}
	}
}
