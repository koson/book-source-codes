using System;
using System.Threading;

namespace Threads_Pool
{
    class Class1
    {
		[STAThread]
		static void Main(string[] args)
		{
			// ������� ������
			WaitCallback callback = new WaitCallback(ThreadFunction);

			// ������� ��� ������ � ����
			ThreadPool.QueueUserWorkItem(callback, "Thread1");
			ThreadPool.QueueUserWorkItem(callback, "Thread2");

			// Unsafe-����� (��� ����������� �� ������ � ��������)
			ThreadPool.UnsafeQueueUserWorkItem(callback, "Thread3");

			// ������ ������� �� Enter
			Console.WriteLine("������� Enter ��� ������...");  
			Console.ReadLine();
		}
		
		static void ThreadFunction(object state)
		{
			Console.WriteLine("����� � ���������� {0}", state.ToString());
			Thread.Sleep(1000);
			Console.WriteLine("���������� ������ {0}", state.ToString());
		}
			
    }
}
