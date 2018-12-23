using System;
using System.Threading;
using System.Diagnostics;

namespace Threads_Events
{
        class Class1
        {

                [STAThread]
                static void Main(string[] args)
                {
                        // Создаем класс потока
                        ThreadClass tc = new ThreadClass();
                        // Создаем сам поток
                        Thread t = new Thread(new ThreadStart(tc.Execute));
                        // Старт потока
                        t.Start();

						// Поток ждет нашего сигнала
						Console.ReadLine();
						tc.Signal();

						// Поток ждет нашего сигнала
						Console.ReadLine();
						tc.Signal();

						// Завершаем поток
                        Console.ReadLine();
                        t.Interrupt();
						

                        // Просто выходим по Enter
                        Console.WriteLine("Нажмите Enter для выхода...");  
                        Console.ReadLine();
                }
        }
}
