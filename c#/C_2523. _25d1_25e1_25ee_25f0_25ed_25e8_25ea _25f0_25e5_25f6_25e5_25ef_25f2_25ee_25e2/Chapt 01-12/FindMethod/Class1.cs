using System;
using System.Reflection;

namespace FindMethod
{
	class Test
	{
		string name;
  
		Test (string name)
		{
			this.name = name;
		}

		public void ShowName()
		{
			Console.WriteLine(name);
		}

		static void Main()
		{
			// ������� ��������� �������
			Test t = new Test ("The name");
			// �������� �����, ������� ����� �������
			MethodInfo mi = typeof(Test).GetMethod("ShowName");
			// �������
			mi.Invoke(t, null);

			Console.ReadLine();
		}
	}
}