using System;
using System.Threading;

namespace Threads_Create
{
        class Class1
        {
                [STAThread]
                static void Main(string[] args)
                {
                        // Создаем класс потока. Один из параметров - callback-функция
                        ThreadClass tc = new ThreadClass("Thread1",  new ThreadCallback(ResultCallback));
                        // Создаем сам поток
                        Thread t = new Thread(new ThreadStart(tc.Execute));
                        // Старт потока
                        t.Start();

                        // Еще один поток
                        ThreadClass tc1 = new ThreadClass("Thread2",  new ThreadCallback(ResultCallback));
                        Thread t1 = new Thread(new ThreadStart(tc1.Execute));
                        t1.Start();


                        // После первого нажатия Enter завершим поток
                        Console.ReadLine();
                        // Завершаем через вызов исключения ThreadAbortException
                        // Вообще говоря, так делать не рекомендуется
                        t.Abort("Стоп из main");
                        // Завершаем через вызов исключения ThreadInterruptedException
                        t1.Interrupt();

					    // Ждем реального завершения
						t.Join();
						t1.Join();

                        // Просто выходим по Enter
                        Console.WriteLine("Нажмите Enter для выхода...");  
                        Console.ReadLine();
                }

                public static int cnt = 1;

                // Call-back функция для потока
                public static void ResultCallback(string Name, int Counter) 
                {
                        lock(Console.Out)
                        {
                                Console.WriteLine("Счетчик потока {0} равен {1} cnt={2}", Name, Counter, cnt); 
                                if (Name == "Thread1") cnt++; else cnt--;
                        }
                }

        }
}
