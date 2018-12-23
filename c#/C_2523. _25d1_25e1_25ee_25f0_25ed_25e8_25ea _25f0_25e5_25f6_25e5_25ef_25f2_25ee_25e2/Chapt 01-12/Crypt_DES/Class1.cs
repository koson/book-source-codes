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
			// �������� ������
			string source_str = "It is string!";

			Console.WriteLine(source_str);


			// �������� �� ������ ����� ����, ������� ����� ���������
			byte[] source_data = Encoding.UTF8.GetBytes(source_str);
 
			DES des = new DESCryptoServiceProvider();

			// ���������� IV � Key
			des.GenerateIV();
			des.GenerateKey();
 
			// ��������� �����
			byte[] IVByteArray  = des.IV;
			byte[] KeyByteArray = des.Key;


			// ����� �������� ������
			MemoryStream ms_in = new MemoryStream();
			// ����������� �����
			CryptoStream cs_in = 
				new CryptoStream(ms_in,
				des.CreateEncryptor(KeyByteArray,
				IVByteArray),
				CryptoStreamMode.Write);
			cs_in.Write(source_data, 0, source_data.Length);
			cs_in.Close();
			// �������� ������������� ������
			string crypt_str = Convert.ToBase64String(ms_in.ToArray());
			// ������� ������������� ������
			Console.WriteLine(crypt_str);


			// �������� ������ ����
			byte[] crypt_data = Convert.FromBase64String(crypt_str);
			// ����� �������� ������
			MemoryStream ms_out = new MemoryStream(crypt_data);
			// ����� ��� �����������
			CryptoStream cs_out = 
				new CryptoStream(ms_out,
				des.CreateDecryptor(KeyByteArray,
				IVByteArray),
				CryptoStreamMode.Read);
			// ������ �������� ������
			StreamReader sr_out = new StreamReader(cs_out);
			string source_out = sr_out.ReadToEnd();
			// ������� �������������� ������
			Console.WriteLine(source_out);
        
			Console.ReadLine();
		}
	}
}
