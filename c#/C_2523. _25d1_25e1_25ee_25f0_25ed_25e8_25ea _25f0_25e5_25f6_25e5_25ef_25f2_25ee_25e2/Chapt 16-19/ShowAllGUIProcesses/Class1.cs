using System;
using System.Diagnostics;

namespace ShowAllGUIProcesses
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			foreach ( Process p in Process.GetProcesses(System.Environment.MachineName) ) 
			{ 
				if( p.MainWindowHandle != IntPtr.Zero) 
				{ 
					Console.WriteLine(p.ToString());
				} 
			} 
			Console.ReadLine();
		}
	}
}
