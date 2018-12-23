using System;
using System.Diagnostics;

namespace EventLogTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			EventLog elog;

			// Имя программы, связанной с логом
			string eventSource = "EventLogTest";

			// Имя лога (должно быть уникальным и не совпадать
			// с системными именами, такими как Application,
			// Security или System)
			string logName = "test app debug log";

			// Если программа еще не зарегистрирована,
			// то регистрируем
			if (!EventLog.SourceExists(eventSource))
			{
				EventLog.CreateEventSource(eventSource, logName);
			}

			// Объект для работы с логом
			elog = new EventLog();
			elog.Log = logName;
			elog.Source = eventSource;
			elog.EntryWritten += new EntryWrittenEventHandler(OnEntryWritten);
			elog.EnableRaisingEvents=true;

			// Ограничиваем число записей
			if (elog.Entries.Count > 20)
			{
				// Чистим лог
				elog.Clear();
				Console.WriteLine("Лог {0} очищен", logName);
			}
            
			// Записываем строку с типом Information
			elog.WriteEntry("event informational text", EventLogEntryType.Information);
			// Записываем строку с типом Error
			elog.WriteEntry("event error text", EventLogEntryType.Error);
			// Записываем строку с типом Warning
			elog.WriteEntry("event warning text", EventLogEntryType.Warning);

			// Запись может занять некоторое время - подождем
			System.Threading.Thread.Sleep(2000);

			// Закрываем лог
			elog.Close();
			elog = null;
		}

		//captures the EntryWritten event of the current event log
		protected static void OnEntryWritten(object sender, EntryWrittenEventArgs e)
		{
			Console.WriteLine("Запись в лог {0}. Время {1}. Позиция {2}.", 
				((EventLog)sender).LogDisplayName, e.Entry.TimeWritten, e.Entry.Index);
		}
	}
}
