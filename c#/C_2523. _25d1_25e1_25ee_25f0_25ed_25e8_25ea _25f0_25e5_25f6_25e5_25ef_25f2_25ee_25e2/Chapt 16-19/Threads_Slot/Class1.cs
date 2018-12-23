using System;
using System.Threading;
using System.IO;

namespace Threads_Slot
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Thread t1 = new Thread(new ThreadStart(ThreadClass.Execute));
			t1.Start();

			Thread t2 = new Thread(new ThreadStart(ThreadClass.Execute));
			t2.Start();


			// ����� ������� ������� Enter �������� �����
			Console.ReadLine();
			// ���������
			t1.Interrupt(); t1.Join();
			t2.Interrupt(); t2.Join();

			// ������ ������� �� Enter
			Console.WriteLine("������� Enter ��� ������...");  
			Console.ReadLine();
		}

	}
}
