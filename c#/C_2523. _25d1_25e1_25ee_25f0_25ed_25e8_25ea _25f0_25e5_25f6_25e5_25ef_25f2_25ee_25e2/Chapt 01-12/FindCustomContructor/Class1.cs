using System;
using System.Reflection;

namespace FindCustomContructor
{
	class TestClass
	{
		TestClass()
		{
		}

		TestClass(int x, string str)
		{
		}
	}

	class MainClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			Type type = typeof(TestClass);

			// ������� ����� ������� ����������
			ConstructorInfo ci1 = type.GetConstructor(new Type[]{});
			Console.WriteLine("����������� 1 "+ ((ci1 == null)?"������":"�� ������"));

			// ������� ����� ���������� � �����������
			ConstructorInfo ci2 = type.GetConstructor(
				new Type[]{typeof(int), typeof(string)});
			Console.WriteLine("����������� 2 "+ 
				((ci2 == null)?"������":"�� ������"));
			Console.ReadLine();
		}
	}
}
