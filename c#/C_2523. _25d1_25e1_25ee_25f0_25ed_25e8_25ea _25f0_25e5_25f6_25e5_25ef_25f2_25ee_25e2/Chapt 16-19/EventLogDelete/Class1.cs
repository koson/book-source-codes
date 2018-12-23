using System;
using System.Diagnostics;

namespace EventLogDelete
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Имя программы, связанной с логом
			string eventSource = "EventLogTest";

			// Имя лога (должно быть уникальным и не совпадать
			// с системными именами, такими как Application,
			// Security или System)
			string logName = "test app debug log";

			// Удаляем регистрацию программы
			if (EventLog.SourceExists(eventSource))
			{
				EventLog.DeleteEventSource(eventSource);
			}

			// Удалить лог
			if (EventLog.Exists(logName))
			{
				EventLog.Delete(logName);
			}
		}
	}
}
