using System;

namespace JoinString
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// ������ �����
			string[] val = {"apple", "orange", "grape", "pear"};
			// ���������� ������ ����� �������
			string result= string.Join(",", val, 2, 2);
			// ���������: <apple,orange,grape,pear>
			Console.WriteLine(result);

			result= string.Concat(val);
			// ���������: <apple,orange,grape,pear>
			Console.WriteLine(result);

			Console.ReadLine();
		}
	}
}
