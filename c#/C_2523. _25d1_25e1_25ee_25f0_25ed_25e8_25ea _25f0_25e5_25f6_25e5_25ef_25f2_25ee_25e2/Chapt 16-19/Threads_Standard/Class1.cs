using System;
using System.Threading;

namespace Threads_Standard
{
	public class ThreadClass
	{
		// �������� ����� ������
		public void Execute()
		{
			// �����-�� ��������...
			Thread.Sleep(30000); // 30 ���
			Console.WriteLine("END");  
		}
	}

	class Threads_Standard
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ������� ����� ������
			ThreadClass tc = new ThreadClass();
			// ������� ��� �����
			Thread t = new Thread(new ThreadStart(tc.Execute));
			// ����� ������
			t.Start();

			// ��� ���� ����� � ������� ������������ ������
			Thread t1 = new Thread(new ThreadStart(Static_Execute));
			t1.Start();

			// ����� ������� ������� Enter �������� �����
			Console.WriteLine("������� Enter ��� ���������� �������");  
			Console.ReadLine();
			t.Abort();
			t1.Abort();

			// ����� ��������� ���������� ������
			t.Join(1000); // � ������� 1 �������
			t1.Join(); // ������ �����

			// ������ ������� �� Enter
			Console.WriteLine("������� Enter ��� ������...");  
			Console.ReadLine();
		}

		// ����������� �����
		static void Static_Execute()
		{
		}

	}
}
