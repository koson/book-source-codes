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

                // Основной метод потока
                public void Execute()
                {
                        // Основной цикл потока
                        while (!Stop)
                        {
                                try
                                {
                                        for (int i=0; i<1000; i++)
                                        {
                                                Console.WriteLine(string.Format("Выполнение потока i={0}", i));
                                        }

                                        // Спать, пока не будет сигнала или не пройдет 5 сек
                                        WaitHandle.WaitAny(new AutoResetEvent[]{ev}, 5000, false);
                                }
                                catch (ThreadInterruptedException)
                                {
                                        Stop = true;
                                }
                        }

                        Console.WriteLine("Выполнение потока завершено.");
                }
        }
}
