using System;

namespace WeakReferenceTest
{
	class TestClass
	{
		public TestClass()
		{
			Console.WriteLine("�������� ������� TestClass");
		}

		public void Print()
		{
			Console.WriteLine("����� Print");
		}
	}

	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ������� ������
			TestClass tc = new TestClass();
			// ������ ������
			WeakReference wr = new WeakReference(tc);

			// �������� ������� ������
			GC.Collect();
			GC.WaitForPendingFinalizers();

			// ������ �� �����������, �.�. 
			// ���� ������� ������
			if (wr.IsAlive)
				(wr.Target as TestClass).Print();
			else
				Console.WriteLine("������ ���������");

			// ���������� ������� ������
			tc = null;

			// �������� ������� ������
			GC.Collect();
			GC.WaitForPendingFinalizers();

			// ������ �����������
			if (wr.IsAlive)
				(wr.Target as TestClass).Print();
			else
				Console.WriteLine("������ ���������");

			Console.ReadLine();
		}
	}
}
