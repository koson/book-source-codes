using System;
using System.ServiceProcess;

namespace WinServiceManagement
{
	class Class1
	{
		static void Main()
		{
			ServiceController sc;
			string            serviceName;
			TimeSpan          timeout = new TimeSpan(0, 0, 30);
			OperatingSystem   os;

			os = Environment.OSVersion;
			if (os.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException("Для работы нужна NT, 2000, XP или выше");
			}
			os = null;

			// Имя сервиса (например, telnet)
			serviceName = "Telnet";

			// Создаем контроллер
			sc = new ServiceController(serviceName);

			// Проверяем статус процесса
			if (sc.Status == ServiceControllerStatus.Running)
			{
				// Процесс можно остановить?
				if (sc.CanStop)
				{
					Console.WriteLine("Останов сервиса {0}", serviceName);
					sc.Stop();

					try
					{
						// Подождем 30 секунд
						sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
						Console.WriteLine("Сервис {0} успешно остановлен", serviceName);
					}
					catch(TimeoutException)
					{
						Console.WriteLine("Не удалось остановить сервис {0}", serviceName);
					}
				}
				else
				{
					Console.WriteLine("Сервис {0} не может быть остановлен", serviceName);
				}
			}

			// Если сервис остановлен - запустим его
			if (sc.Status == ServiceControllerStatus.Stopped)
			{
				// Стартуем
				sc.Start();

				try
				{
					// Ждем 30 сек
					sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
					Console.WriteLine("Сервис {0} успешно стартован", serviceName);
				}
				catch(TimeoutException)
				{
					Console.WriteLine("Не удалось запустить сервис {0}", serviceName);
				}
			}

			// Закрыть контроллер
			sc.Close();
			sc = null;

			Console.ReadLine();
		}
	}
}
