using System;
using System.Threading;

namespace Threads_Create
{
        class Class1
        {
                [STAThread]
                static void Main(string[] args)
                {
                        // ������� ����� ������. ���� �� ���������� - callback-�������
                        ThreadClass tc = new ThreadClass("Thread1",  new ThreadCallback(ResultCallback));
                        // ������� ��� �����
                        Thread t = new Thread(new ThreadStart(tc.Execute));
                        // ����� ������
                        t.Start();

                        // ��� ���� �����
                        ThreadClass tc1 = new ThreadClass("Thread2",  new ThreadCallback(ResultCallback));
                        Thread t1 = new Thread(new ThreadStart(tc1.Execute));
                        t1.Start();


                        // ����� ������� ������� Enter �������� �����
                        Console.ReadLine();
                        // ��������� ����� ����� ���������� ThreadAbortException
                        // ������ ������, ��� ������ �� �������������
                        t.Abort("���� �� main");
                        // ��������� ����� ����� ���������� ThreadInterruptedException
                        t1.Interrupt();

					    // ���� ��������� ����������
						t.Join();
						t1.Join();

                        // ������ ������� �� Enter
                        Console.WriteLine("������� Enter ��� ������...");  
                        Console.ReadLine();
                }

                public static int cnt = 1;

                // Call-back ������� ��� ������
                public static void ResultCallback(string Name, int Counter) 
                {
                        lock(Console.Out)
                        {
                                Console.WriteLine("������� ������ {0} ����� {1} cnt={2}", Name, Counter, cnt); 
                                if (Name == "Thread1") cnt++; else cnt--;
                        }
                }

        }
}
