using System;

namespace UnhandledExceptionTest
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException +=new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			for (int i=0; i<10; i++) 
			{
				int j = 10/(i-9);
			}
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Console.WriteLine("CurrentDomain_UnhandledException:");
			Console.WriteLine(sender.ToString());
			Console.WriteLine(e.ExceptionObject.ToString());
			Console.WriteLine(e.IsTerminating);
		}
	}
}
