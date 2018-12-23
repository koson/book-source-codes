using System;
using System.Diagnostics;

namespace EventLogDelete
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// ��� ���������, ��������� � �����
			string eventSource = "EventLogTest";

			// ��� ���� (������ ���� ���������� � �� ���������
			// � ���������� �������, ������ ��� Application,
			// Security ��� System)
			string logName = "test app debug log";

			// ������� ����������� ���������
			if (EventLog.SourceExists(eventSource))
			{
				EventLog.DeleteEventSource(eventSource);
			}

			// ������� ���
			if (EventLog.Exists(logName))
			{
				EventLog.Delete(logName);
			}
		}
	}
}
