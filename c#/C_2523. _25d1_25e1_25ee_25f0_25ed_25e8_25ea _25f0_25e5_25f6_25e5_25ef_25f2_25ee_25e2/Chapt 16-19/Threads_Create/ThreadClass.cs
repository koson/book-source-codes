using System;
using System.Threading;

namespace Threads_Create
{
	// �������� callback-������� ������
	public delegate void ThreadCallback(string Name, int Counter);

	public class ThreadClass
	{
		private string Name;
		private int	   Counter;
		private ThreadCallback Callback;
		private bool   Stop;

		public ThreadClass(string Name, ThreadCallback Callback)
		{
			this.Name	  = Name;
			this.Callback = Callback;

			Counter = 0;
			Stop    = false;
		}

		// �������� ����� ������
		public void Execute()
		{
			// ��� ������
			Thread.CurrentThread.Name = Name;
			// ���������
			Thread.CurrentThread.Priority = ThreadPriority.Normal;

			// �������� ���� ������
			while (!Stop)
			{
				try
				{
					Console.WriteLine(string.Format("���������� ������ {0}", Name));
					Counter++;

					// ����� callback
					if (Callback != null)
						Callback(Name, Counter);

					// ����� �������� ���-�� ��
					Thread.Sleep(1);
				}
				// ��� ������ ������ Abort ����� ������������� ����������,
				// ������� �� ����� ��� ���������� ������
				catch (ThreadAbortException ex_abort)
				{
					// ���������� ���������� �� �����, �� ����� ��� ����������
					// ��������� ��� ���������� ������ �� �����
					Console.WriteLine(string.Format("���������� ������ {0} (ThreadAbortException)", Name));
					Console.WriteLine(string.Format("����������: {0}", (string)ex_abort.ExceptionState));
				}
				catch (ThreadInterruptedException)
				{
					Console.WriteLine(string.Format("���������� ������ {0} (ThreadInterruptedException)", Name));
					// ��� ���������� ������ ���������� � ����������� ���������� ������
					// �� ������ ���� ��������� �����
					Stop = true;
				}
			}

			Console.WriteLine(string.Format("���������� ������ {0} ���������.", Name));
		}
	}
}
