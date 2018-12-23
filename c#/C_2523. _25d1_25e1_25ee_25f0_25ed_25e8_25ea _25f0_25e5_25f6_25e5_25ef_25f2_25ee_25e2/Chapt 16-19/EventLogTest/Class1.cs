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

			// ��� ���������, ��������� � �����
			string eventSource = "EventLogTest";

			// ��� ���� (������ ���� ���������� � �� ���������
			// � ���������� �������, ������ ��� Application,
			// Security ��� System)
			string logName = "test app debug log";

			// ���� ��������� ��� �� ����������������,
			// �� ������������
			if (!EventLog.SourceExists(eventSource))
			{
				EventLog.CreateEventSource(eventSource, logName);
			}

			// ������ ��� ������ � �����
			elog = new EventLog();
			elog.Log = logName;
			elog.Source = eventSource;
			elog.EntryWritten += new EntryWrittenEventHandler(OnEntryWritten);
			elog.EnableRaisingEvents=true;

			// ������������ ����� �������
			if (elog.Entries.Count > 20)
			{
				// ������ ���
				elog.Clear();
				Console.WriteLine("��� {0} ������", logName);
			}
            
			// ���������� ������ � ����� Information
			elog.WriteEntry("event informational text", EventLogEntryType.Information);
			// ���������� ������ � ����� Error
			elog.WriteEntry("event error text", EventLogEntryType.Error);
			// ���������� ������ � ����� Warning
			elog.WriteEntry("event warning text", EventLogEntryType.Warning);

			// ������ ����� ������ ��������� ����� - ��������
			System.Threading.Thread.Sleep(2000);

			// ��������� ���
			elog.Close();
			elog = null;
		}

		//captures the EntryWritten event of the current event log
		protected static void OnEntryWritten(object sender, EntryWrittenEventArgs e)
		{
			Console.WriteLine("������ � ��� {0}. ����� {1}. ������� {2}.", 
				((EventLog)sender).LogDisplayName, e.Entry.TimeWritten, e.Entry.Index);
		}
	}
}
