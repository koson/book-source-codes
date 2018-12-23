using System;

namespace Reflection1
{
	public class TestClass
	{
		public TestClass()
		{
			Console.WriteLine("����������� TestClass");
		}

	}

	class ReflectionDemoClass
	{
		[STAThread]
		static void Main(string[] args)
		{
			// �������� ������ �� ��� �� �����
			Type TestType = Type.GetType("Reflection1.TestClass", false, true);
			// ���� ����� ������
			if (TestType != null)
			{
				// �������� �����������
				System.Reflection.ConstructorInfo ci = TestType.GetConstructor(new Type[]{});
				// �������� �����������
				object Obj = ci.Invoke(new object[]{});
			}
			else
			{
				Console.WriteLine("����� �� ������");
			}
			Console.ReadLine();
		}
	}
}
