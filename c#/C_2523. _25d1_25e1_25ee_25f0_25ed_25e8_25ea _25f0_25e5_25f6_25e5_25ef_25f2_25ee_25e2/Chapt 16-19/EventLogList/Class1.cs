using System;
using System.Diagnostics;

namespace EventLogList
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			EventLog[] elogs = EventLog.GetEventLogs();
			foreach(EventLog elog in elogs)
			{
				Console.WriteLine("{0}", elog.LogDisplayName);
				elog.Close();
			}
			elogs = null;

			Console.ReadLine();
		}
	}
}
