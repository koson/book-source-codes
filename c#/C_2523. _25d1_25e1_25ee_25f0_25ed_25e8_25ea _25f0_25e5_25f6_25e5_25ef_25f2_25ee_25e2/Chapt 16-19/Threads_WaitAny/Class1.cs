using System;
using System.Threading;
using System.Diagnostics;

namespace Threads_WaitAny
{
        class Class1
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

                                                // ����� ���� ������ �������
                                                Console.ReadLine();
                                                tc.Signal();

                                                // ����� ���� ������ �������
                                                Console.ReadLine();
                                                tc.Signal();

                                                // ��������� �����
                        Console.ReadLine();
                        t.Interrupt();
                                                

                        // ������ ������� �� Enter
                        Console.WriteLine("������� Enter ��� ������...");  
                        Console.ReadLine();
                }
        }
}
