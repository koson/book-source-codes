using System;
using System.Diagnostics;
using System.Reflection;

namespace OneInstanceConsoleApplication
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Process process = RunningInstance();
			if (process != null)
			{
				Console.WriteLine("Программа уже запущена");
				return;
			}

			Console.WriteLine("Приложение запущено. Нажмите ENTER для выхода");
			Console.ReadLine();
		}

		public static Process RunningInstance() 
		{ 
			Process current = Process.GetCurrentProcess(); 
			Process[] processes = Process.GetProcessesByName (current.ProcessName); 

			//Просматриваем все процессы 
			foreach (Process process in processes) 
			{ 
				//Игнорируем текущий процесс
				if (process.Id != current.Id) 
				{ 
					//Проверяем, что процесс запущен из того же файла 
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
 					{ 
 						//Да, это и есть копия нашего приложения
 						return process; 
 					} 
 				} 
 			} 
 			//Нет, таких же процессов не найдено
 			return null; 
 		} 

	}
}
