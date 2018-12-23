using System;
using System.Threading;

namespace Threads_WaitAny
{
        public class ThreadClass
        {
                private bool Stop;
                private AutoResetEvent ev;

                public ThreadClass()
                {
                        Stop = false;
                        ev   = new AutoResetEvent(false);
                }

                public void Signal()
                {
                        ev.Set();
                }

                // �������� ����� ������
                public void Execute()
                {
                        // �������� ���� ������
                        while (!Stop)
                        {
                                try
                                {
                                        for (int i=0; i<1000; i++)
                                        {
                                                Console.WriteLine(string.Format("���������� ������ i={0}", i));
                                        }

                                        // �����, ���� �� ����� ������� ��� �� ������� 5 ���
                                        WaitHandle.WaitAny(new AutoResetEvent[]{ev}, 5000, false);
                                }
                                catch (ThreadInterruptedException)
                                {
                                        Stop = true;
                                }
                        }

                        Console.WriteLine("���������� ������ ���������.");
                }
        }
}
